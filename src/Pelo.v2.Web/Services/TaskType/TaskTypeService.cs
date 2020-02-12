using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.TaskType;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.TaskType;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.TaskType
{
    public interface ITaskTypeService
    {
        Task<TaskTypeListModel> GetByPaging(TaskTypeSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class TaskTypeService : BaseService,
                                      ITaskTypeService
    {
        public TaskTypeService(IHttpService httpService,
                                  ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ITaskTypeService Members

        public async Task<TaskTypeListModel> GetByPaging(TaskTypeSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.TASK_TYPE_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetTaskTypePagingResponse>>(url,
                                                                                                    null,
                                                                                                    HttpMethod.Get,
                                                                                                    true);

                    if (response.IsSuccess)
                        return new TaskTypeListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new TaskTypeModel
                            {
                                Id = c.Id,
                                Name = c.Name,
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
                var url = string.Format(ApiUrl.TASK_TYPE_GET_ALL,
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

        #endregion
    }
}
