using System;
using System.Collections.Generic;
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
        Task<IEnumerable<TaskTypeModel>> GetAll();

        Task<TaskTypeListModel> GetByPaging(TaskTypeSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Insert(InsertTaskType insertTaskType);
        
        Task<TResponse<bool>> Update(UpdateTaskType updateTaskType);
        
        Task<TResponse<TaskTypeModel>> GetById(int id);
    }

    public class TaskTypeService : BaseService,
                                   ITaskTypeService
    {
        public TaskTypeService(IHttpService httpService,
                               ILogger<BaseService> logger) : base(httpService,
                                                                   logger)
        {
        }

        #region ITaskTypeService Members

        public async Task<IEnumerable<TaskTypeModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<TaskTypeModel>>(ApiUrl.TASK_TYPE_GET_ALL,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);

                if(response.IsSuccess)
                    return response.Data;

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TaskTypeListModel> GetByPaging(TaskTypeSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    if(request.Columns != null
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

                    if(response.IsSuccess)
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

        public async Task<TResponse<bool>> Insert(InsertTaskType insertTaskType)
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse<bool>> Update(UpdateTaskType updateTaskType)
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse<TaskTypeModel>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
