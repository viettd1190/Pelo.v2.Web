using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Candidate;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Candidate;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Candidate
{
    public interface ICandidateService
    {
        Task<CandidateListModel> GetByPaging(CandidateSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class CandidateService : BaseService,
                                    ICandidateService
    {
        public CandidateService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICandidateService Members

        public async Task<CandidateListModel> GetByPaging(CandidateSearchModel request)
        {
            try
            {
                var columnOrder = request.ColumnOrder ?? "name";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.CANDIDATE_GET_BY_PAGING,
                                            request.Name,
                                            request.FromDate,
                                            request.ToDate,
                                            request.Phone,
                                            request.Code,
                                            request.CandidateStatusId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCandidatePagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new CandidateListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new CandidateModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
                                Code = c.Code,
                                Phone = c.Phone,
                                CandidateStatusName = c.CandidateStatusName,
                                Description = c.Description,
                                UserNameCreated = c.UserNameCreated,
                                UserPhoneCreated = c.UserPhoneCreated,
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
                var url = string.Format(ApiUrl.CANDIDATE_DELETE,
                                        id);
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Delete,
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
