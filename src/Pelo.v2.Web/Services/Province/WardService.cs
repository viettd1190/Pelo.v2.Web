using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Ward;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Ward;
using Pelo.v2.Web.Services.Http;
using WardModel = Pelo.v2.Web.Models.Ward.WardModel;

namespace Pelo.v2.Web.Services.Province
{
    public interface IWardService
    {
        Task<TResponse<IEnumerable<WardModel>>> GetAll(int districtId);

        Task<WardListModel> GetByPaging(WardSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Add(UpdateWardModel model);

        Task<TResponse<bool>> Edit(UpdateWardModel model);

        Task<TResponse<UpdateWardModel>> GetById(int id);
    }

    public class WardService : BaseService,
                               IWardService
    {
        public WardService(IHttpService httpService,
                           ILogger<BaseService> logger) : base(httpService,
                                                               logger)
        {
        }

        #region IWardService Members

        public async Task<TResponse<IEnumerable<WardModel>>> GetAll(int districtId)
        {
            try
            {
                var url = string.Format(ApiUrl.WARD_GET_ALL,
                                        districtId);
                var response = await HttpService.Send<IEnumerable<Common.Dtos.Ward.WardModel>>(url,
                                                                                               null,
                                                                                               HttpMethod.Get,
                                                                                               true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data.Select(c => new WardModel
                                                              {
                                                                      Id = c.Id,
                                                                      Type = c.Type,
                                                                      Name = c.Name
                                                              }));
                }

                return await Fail<IEnumerable<WardModel>>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<IEnumerable<WardModel>>(exception);
            }
        }

        public async Task<WardListModel> GetByPaging(WardSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.WARD_GET_BY_PAGING,
                                            request.WardName,
                                            request.DistrictId,
                                            request.ProvinceId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetWardPagingResponse>>(url,
                                                                                             null,
                                                                                             HttpMethod.Get,
                                                                                             true);

                    if(response.IsSuccess)
                        return new WardListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new WardModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     District = c.District,
                                                                                     Province = c.Province,
                                                                                     SortOrder = c.SortOrder,
                                                                                     PageSize = request.PageSize,
                                                                                     Type = c.Type,
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

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.WARD_DELETE,
                                        id);
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Delete,
                                                            true);
                if(response.IsSuccess)
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

        public async Task<TResponse<bool>> Add(UpdateWardModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARD_UPDATE,
                                                            new InsertWard
                                                            {
                                                                SortOrder= model.SortOrder,
                                                                Name = model.Name,
                                                                DistrictId = model.DistrictId,
                                                                Type = model.Type
                                                            },
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

        public async Task<TResponse<bool>> Edit(UpdateWardModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARD_UPDATE,
                                                            new UpdateWard
                                                            {
                                                                Id = model.Id,
                                                                SortOrder = model.SortOrder,
                                                                Name = model.Name,
                                                                DistrictId = model.DistrictId,
                                                                Type = model.Type
                                                            },
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

        public async Task<TResponse<UpdateWardModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.WARD_GET_BY_ID, id);
                var response = await HttpService.Send<GetWardReponse>(url, null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(new UpdateWardModel { DistrictId = response.Data.DistrictId, Id = response.Data.Id, Name = response.Data.Name, Type = response.Data.Type, SortOrder = response.Data.SortOrder });
                }

                return await Fail<UpdateWardModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<UpdateWardModel>(exception);
            }
        }

        #endregion
    }
}
