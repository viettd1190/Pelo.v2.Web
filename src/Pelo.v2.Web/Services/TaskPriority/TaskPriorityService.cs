using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.TaskPriority;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.TaskPriority;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.TaskPriority
{
    public interface ITaskPriorityService
    {
        Task<IEnumerable<TaskPriorityModel>> GetAll();

        Task<TaskPriorityListModel> GetByPaging(TaskPrioritySearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Update(TaskPriorityModel model);
        
        Task<TResponse<TaskPriorityModel>> GetById(int id);
        
        Task<TResponse<bool>> Insert(TaskPriorityModel model);
    }

    public class TaskPriorityService : BaseService,
                                       ITaskPriorityService
    {
        public TaskPriorityService(IHttpService httpService,
                                   ILogger<BaseService> logger) : base(httpService,
                                                                       logger)
        {
        }

        #region ITaskPriorityService Members

        public async Task<IEnumerable<TaskPriorityModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<TaskPriorityModel>>(ApiUrl.TASK_PRIORITY_GET_ALL,
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

        public async Task<TaskPriorityListModel> GetByPaging(TaskPrioritySearchModel request)
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

                    var url = string.Format(ApiUrl.TASK_PRIORITY_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetTaskPriorityPagingResponse>>(url,
                                                                                                     null,
                                                                                                     HttpMethod.Get,
                                                                                                     true);

                    if (response.IsSuccess)
                        return new TaskPriorityListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new TaskPriorityModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
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
                var url = string.Format(ApiUrl.TASK_PRIORITY_GET_ALL,
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

        public async Task<TResponse<bool>> Update(TaskPriorityModel model)
        {
            try
            {
                var url = ApiUrl.TASK_PRIORITY_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new UpdateTaskPriority { Id = model.Id, Name = model.Name, Color = model.Color, SortOrder = model.SortOrder },
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

        public async Task<TResponse<TaskPriorityModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.TASK_PRIORITY_GET_BY_ID, id);
                var response = await HttpService.Send<GetTaskPriorityResponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(new TaskPriorityModel { Id = response.Data.Id, Name = response.Data.Name, Color = response.Data.Color, SortOrder = response.Data.SortOrder });
                }

                return await Fail<TaskPriorityModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<TaskPriorityModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(TaskPriorityModel model)
        {
            try
            {
                var url = ApiUrl.TASK_PRIORITY_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new InsertTaskPriority { Name = model.Name, Color = model.Color, SortOrder = model.SortOrder },
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
