using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.TaskStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.TaskStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.TaskStatus
{
    public interface ITaskStatusService
    {
        Task<IEnumerable<TaskStatusModel>> GetAll();

        Task<TaskStatusListModel> GetByPaging(TaskStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<UpdateTaskStatusModel>> GetById(int id);
        Task<TResponse<bool>> Update(UpdateTaskStatusModel model);
        Task<TResponse<bool>> Insert(UpdateTaskStatusModel model);
    }

    public class TaskStatusService : BaseService,
                                     ITaskStatusService
    {
        public TaskStatusService(IHttpService httpService,
                                 ILogger<BaseService> logger) : base(httpService,
                                                                     logger)
        {
        }

        #region ITaskStatusService Members

        public async Task<IEnumerable<TaskStatusModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<TaskStatusModel>>(ApiUrl.TASK_STATUS_GET_ALL,
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

        public async Task<TaskStatusListModel> GetByPaging(TaskStatusSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
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

                    var url = string.Format(ApiUrl.TASK_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetTaskStatusPagingResponse>>(url,
                                                                                                   null,
                                                                                                   HttpMethod.Get,
                                                                                                   true);

                    if (response.IsSuccess)
                        return new TaskStatusListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new TaskStatusModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
                                IsSendSms = c.IsSendSms,
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
                var url = string.Format(ApiUrl.TASK_STATUS_DELETE,
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

        public async Task<TResponse<UpdateTaskStatusModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.TASK_TYPE_GET_BY_ID, id);
                var response = await HttpService.Send<GetTaskStatusPagingResponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(new UpdateTaskStatusModel { Id = response.Data.Id, Name = response.Data.Name, Color = response.Data.Color, SortOrder = response.Data.SortOrder });
                }

                return await Fail<UpdateTaskStatusModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<UpdateTaskStatusModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateTaskStatusModel model)
        {
            try
            {
                var url = ApiUrl.TASK_STATUS_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new InsertTaskStatus { Name = model.Name, Color = model.Color, SortOrder = model.SortOrder, IsSendSms = model.IsSendSms, SmsContent = model.SmsContent },
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

        public async Task<TResponse<bool>> Insert(UpdateTaskStatusModel model)
        {
            try
            {
                var url = ApiUrl.TASK_STATUS_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new InsertTaskStatus { Name = model.Name, Color = model.Color, SortOrder = model.SortOrder, IsSendSms = model.IsSendSms, SmsContent = model.SmsContent },
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
