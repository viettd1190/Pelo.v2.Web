using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.TaskLoop;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.TaskLoop;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.TaskLoop
{
    public interface ITaskLoopService
    {
        Task<IEnumerable<TaskLoopModel>> GetAll();

        Task<TaskLoopListModel> GetByPaging(TaskLoopSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<TaskLoopModel>> GetById(int id);
        Task<TResponse<bool>> Insert(TaskLoopModel model);
        Task<TResponse<bool>> Update(TaskLoopModel model);
    }

    public class TaskLoopService : BaseService,
                                   ITaskLoopService
    {
        public TaskLoopService(IHttpService httpService,
                               ILogger<BaseService> logger) : base(httpService,
                                                                   logger)
        {
        }

        #region ITaskLoopService Members

        public async Task<IEnumerable<TaskLoopModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<TaskLoopModel>>(ApiUrl.TASK_LOOP_GET_ALL,
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

        public async Task<TaskLoopListModel> GetByPaging(TaskLoopSearchModel request)
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

                    var url = string.Format(ApiUrl.TASK_LOOP_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetTaskLoopPagingResponse>>(url,
                                                                                                 null,
                                                                                                 HttpMethod.Get,
                                                                                                 true);

                    if (response.IsSuccess)
                        return new TaskLoopListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new TaskLoopModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                DayCount = c.DayCount,
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
                var url = string.Format(ApiUrl.TASK_LOOP_DELETE,
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

        public async Task<TResponse<TaskLoopModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.TASK_LOOP_GET_BY_ID, id);
                var response = await HttpService.Send<GetTaskLoopPagingResponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(new TaskLoopModel { Id = response.Data.Id, DayCount = response.Data.DayCount, Name = response.Data.Name, SortOrder = response.Data.SortOrder });
                }

                return await Fail<TaskLoopModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<TaskLoopModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Insert(TaskLoopModel model)
        {
            try
            {
                var url = ApiUrl.TASK_LOOP_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new InsertTaskLoop { Name = model.Name, SortOrder = model.SortOrder, DayCount = model.DayCount },
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

        public async Task<TResponse<bool>> Update(TaskLoopModel model)
        {
            try
            {
                var url = ApiUrl.TASK_LOOP_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new UpdateTaskLoop { Id = model.Id, Name = model.Name, SortOrder = model.SortOrder, DayCount = model.DayCount },
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
