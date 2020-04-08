using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CandidateStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CandidateStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CandidateStatus
{
    public interface ICandidateStatusService
    {
        Task<CandidateStatusListModel> GetByPaging(CandidateStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<IEnumerable<CandidateStatusSimpleModel>> GetAll();

        Task<TResponse<GetCandidateStatusResponse>> GetById(int id);
        Task<TResponse<bool>> Update(UpdateCandidateStatus model);
        Task<TResponse<bool>> Insert(InsertCandidateStatus model);
    }

    public class CandidateStatusService : BaseService,
                                    ICandidateStatusService
    {
        public CandidateStatusService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICandidateStatusService Members

        public async Task<CandidateStatusListModel> GetByPaging(CandidateStatusSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.CANDIDATE_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCandidateStatusPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if(response.IsSuccess)
                        return new CandidateStatusListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CandidateStatusModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     Color = c.Color,
                                                                                     SortOrder = c.SortOrder,
                                                                                     PageSize = request.PageSize,
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
                var url = string.Format(ApiUrl.CANDIDATE_STATUS_DELETE,
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

        public async Task<IEnumerable<CandidateStatusSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CandidateStatusSimpleModel>>(ApiUrl.CANDIDATE_STATUS_GET_ALL,
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

        public async Task<TResponse<GetCandidateStatusResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CANDIDATE_STATUS_GET_BY_ID, id);
                var response = await HttpService.Send<GetCandidateStatusResponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCandidateStatusResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCandidateStatusResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCandidateStatus model)
        {
            try
            {
                var url = ApiUrl.CANDIDATE_STATUS_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            model,
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

        public async Task<TResponse<bool>> Insert(InsertCandidateStatus model)
        {
            try
            {
                var url = ApiUrl.CANDIDATE_STATUS_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                           model,
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

        #endregion
    }
}
