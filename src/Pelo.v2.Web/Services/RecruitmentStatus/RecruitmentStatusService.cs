using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.RecruitmentStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.RecruitmentStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.RecruitmentStatus
{
    public interface IRecruitmentStatusService
    {
        Task<RecruitmentStatusListModel> GetByPaging(RecruitmentStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<bool>> Add(InsertRecruitmentStatus insertRecruitmentStatus);
        Task<TResponse<RecruitmentStatusResponse>> GetById(int id);
        Task<TResponse<bool>> Edit(UpdateRecruitmentStatus updateRecruitmentStatus);
    }

    public class RecruitmentStatusService : BaseService,
                                    IRecruitmentStatusService
    {
        public RecruitmentStatusService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IRecruitmentStatusService Members

        public async Task<RecruitmentStatusListModel> GetByPaging(RecruitmentStatusSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.RECRUITMENT_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetRecruitmentStatusPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new RecruitmentStatusListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new RecruitmentStatusModel
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
                var url = string.Format(ApiUrl.RECRUITMENT_STATUS_DELETE,
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

        public async Task<TResponse<bool>> Add(InsertRecruitmentStatus insertRecruitmentStatus)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.RECRUITMENT_STATUS_UPDATE,
                                                            insertRecruitmentStatus,
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

        public async Task<TResponse<RecruitmentStatusResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.RECRUITMENT_STATUS_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<RecruitmentStatusResponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<RecruitmentStatusResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<RecruitmentStatusResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Edit(UpdateRecruitmentStatus updateRecruitmentStatus)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.RECRUITMENT_STATUS_UPDATE,
                                                            updateRecruitmentStatus,
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

        #endregion
    }
}
