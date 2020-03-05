using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Branch;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Services.Http;
using BranchModel = Pelo.Common.Dtos.Branch.BranchModel;

namespace Pelo.v2.Web.Services.Branch
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchSimpleModel>> GetAll();

        Task<BranchListModel> GetByPaging(BranchSearchModel request);

        Task<TResponse<bool>> Insert(InsertBranch request);

        Task<TResponse<BranchModel>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateBranch request);

        //Task<TResponse<bool>> Delete(int id);
    }

    public class BranchService : BaseService,
                                 IBranchService
    {
        public BranchService(IHttpService httpService,
                             ILogger<BaseService> logger) : base(httpService,
                                                                 logger)
        {
        }

        #region IBranchService Members

        public async Task<IEnumerable<BranchSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<BranchSimpleModel>>(ApiUrl.BRANCH_GET_ALL,
                                                                                      null,
                                                                                      HttpMethod.Get,
                                                                                      true);

                if (response.IsSuccess)
                    return response.Data;

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<BranchListModel> GetByPaging(BranchSearchModel request)
        {
            try
            {
                var columnOrder = "Name";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    if (request.Columns != null
                       && request.Columns.Any()
                       && request.Order != null
                       && request.Order.Any())
                    {
                        sortDir = request.Order[0]
                                         .Dir;
                        columnOrder = request.Columns[request.Order[0]
                                                             .Column]
                                             .Data;
                    }

                    var url = string.Format(ApiUrl.BRANCH_PAGING,
                                            request.Name,
                                            request.HotLine,
                                            request.ProvinceId,
                                            request.DistrictId,
                                            request.WardId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetBranchPagingResponse>>(url,
                                                                                               null,
                                                                                               HttpMethod.Get,
                                                                                               true);

                    if (response.IsSuccess)
                        return new BranchListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new Models.Branch.BranchModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Address = c.Address,
                                PageSize = request.PageSize,
                                District = c.District,
                                Hotline = c.Hotline,
                                Province = c.Province,
                                Ward = c.Ward,
                                PageSizeOptions = request.AvailablePageSizes
                            })
                        };

                    throw new PeloException(response.Message);
                }

                throw new PeloException("Request is null");
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        #endregion

        public Task<TResponse<bool>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse<bool>> Insert(InsertBranch request)
        {
            try
            {
                var url = ApiUrl.BRANCH_INSERT;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Post,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(true);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateBranch request)
        {
            try
            {
                var url = ApiUrl.BRANCH_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Put,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(true);
                }

                return await Fail<bool>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }

        public async Task<TResponse<BranchModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.GET_BRANCH_ID, id);
                var response = await HttpService.Send<BranchModel>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<BranchModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<BranchModel>(exception);
            }
        }
    }
}
