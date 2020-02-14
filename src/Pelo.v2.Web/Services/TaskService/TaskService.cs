using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Task;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.TaskModel;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.TaskService
{
    public interface ITaskService
    {
        Task<TaskListModel> GetByPaging(TaskSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class TaskService : BaseService,
                                      ITaskService
    {
        public TaskService(IHttpService httpService,
                                  ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ITaskService Members

        public async Task<TaskListModel> GetByPaging(TaskSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.TASK_GET_BY_PAGING,
                                            request.Name,
                                            request.CustomerName,
                                            request.Phone,
                                            request.Code,
                                            request.TaskStatusId,
                                            request.TaskPriorityId,
                                            request.TaskLoopId,
                                            request.TaskTypeId,
                                            request.UserCreatedId,
                                            request.UserCareId,
                                            request.FromDate,
                                            request.ToDate,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetTaskPagingResponse>>(url,
                                                                                                    null,
                                                                                                    HttpMethod.Get,
                                                                                                    true);

                    if (response.IsSuccess)
                        return new TaskListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new TaskModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Code = c.Code,
                                Content = c.Content,
                                CustomerAddress = c.CustomerAddress,
                                CustomerName = c.CustomerName,
                                CustomerPhone = c.CustomerPhone,
                                PriorityColor = c.PriorityColor,
                                Description = c.Description,
                                FromDateTime = c.FromDateTime,
                                StatusColor = c.StatusColor,
                                TaskLoopName = c.TaskLoopName,
                                TaskPriorityName = c.TaskPriorityName,
                                TaskStatusName = c.TaskStatusName,
                                ToDateTime = c.ToDateTime,
                                UserNameCare = c.UserNameCare,
                                UserNameCreated = c.UserNameCreated,
                                UserPhoneCare = c.UserPhoneCare,
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
                var url = string.Format(ApiUrl.TASK_GET_ALL,
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
