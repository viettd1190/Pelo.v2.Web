using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Recruitment;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Recruitment;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Recruitment
{
    public interface IRecruitmentService
    {
        Task<RecruitmentListModel> GetByPaging(RecruitmentSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class RecruitmentService : BaseService,
                                    IRecruitmentService
    {
        public RecruitmentService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IRecruitmentService Members

        public async Task<RecruitmentListModel> GetByPaging(RecruitmentSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.RECRUITMENT_GET_BY_PAGING,
                                            request.Name,
                                            request.CandidateStatusId,
                                            request.FromDate,
                                            request.ToDate,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetRecruitmentPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new RecruitmentListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new RecruitmentModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
                                Code = c.Code,
                                Description = c.Description,
                                RecruitmentStatusName = c.RecruitmentStatusName,
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
                var url = string.Format(ApiUrl.RECRUITMENT_DELETE,
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
