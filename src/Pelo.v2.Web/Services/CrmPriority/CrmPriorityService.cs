using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CrmPriority;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CrmPriority;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CrmPriority
{
    public interface ICrmPriorityService
    {
        Task<IEnumerable<CrmPriorityModel>> GetAll();

        Task<CrmPriorityListModel> GetByPaging(CrmPrioritySearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<bool>> Add(CrmPriorityModel model);

        Task<TResponse<bool>> Edit(CrmPriorityModel model);
        
        Task<TResponse<CrmPriorityModel>> GetById(int id);
    }

    public class CrmPriorityService : BaseService,
                                      ICrmPriorityService
    {
        public CrmPriorityService(IHttpService httpService,
                                  ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICrmPriorityService Members

        public async Task<IEnumerable<CrmPriorityModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CrmPriorityModel>>(ApiUrl.CRM_PRIORITY_GET_ALL,
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

        public async Task<CrmPriorityListModel> GetByPaging(CrmPrioritySearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
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

                    var url = string.Format(ApiUrl.CRM_PRIORITY_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCrmPriorityPagingResponse>>(url,
                                                                                                    null,
                                                                                                    HttpMethod.Get,
                                                                                                    true);

                    if(response.IsSuccess)
                        return new CrmPriorityListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CrmPriorityModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     Color=c.Color,
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
                var url = string.Format(ApiUrl.CRM_PRIORITY_GET_ALL,
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

        public async Task<TResponse<bool>> Add(CrmPriorityModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_PRIORITY_UPDATE,
                                                            new InsertCrmPriority
                                                            {
                                                                Color = model.Color,
                                                                Name = model.Name
                                                            },
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

        public async Task<TResponse<bool>> Edit(CrmPriorityModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_PRIORITY_UPDATE,
                                                            new UpdateCrmPriority
                                                            {
                                                                Id = model.Id,
                                                                Color = model.Color,
                                                                Name = model.Name
                                                            },
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

        public async Task<TResponse<CrmPriorityModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CRM_PRIORITY_GET_BY_ID, id);
                var response = await HttpService.Send<GetCrmPriorityResponse>(url, null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(new CrmPriorityModel { Color = response.Data.Color, Id = response.Data.Id, Name = response.Data.Name});
                }

                return await Fail<CrmPriorityModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CrmPriorityModel>(exception);
            }
        }
    }
}
