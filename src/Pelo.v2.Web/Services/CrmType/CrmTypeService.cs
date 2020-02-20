using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CrmType;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CrmType;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CrmType
{
    public interface ICrmTypeService
    {
        Task<IEnumerable<CrmTypeModel>> GetAll();

        Task<CrmTypeListModel> GetByPaging(CrmTypeSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Add(CrmTypeModel model);

        Task<TResponse<bool>> Edit(CrmTypeModel model);

        Task<TResponse<CrmTypeModel>> GetById(int id);
    }

    public class CrmTypeService : BaseService,
                                  ICrmTypeService
    {
        public CrmTypeService(IHttpService httpService,
                              ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICrmTypeService Members

        public async Task<IEnumerable<CrmTypeModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CrmTypeModel>>(ApiUrl.CRM_TYPE_GET_ALL,
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

        public async Task<CrmTypeListModel> GetByPaging(CrmTypeSearchModel request)
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

                    var url = string.Format(ApiUrl.CRM_TYPE_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCrmTypePagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                    if (response.IsSuccess)
                        return new CrmTypeListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new CrmTypeModel
                            {
                                Id = c.Id,
                                Name = c.Name,
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
                var url = string.Format(ApiUrl.CRM_TYPE_DELETE,
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

        public async Task<TResponse<bool>> Add(CrmTypeModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_TYPE_UPDATE,
                                                            new InsertCrmType
                                                            {
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

        public async Task<TResponse<bool>> Edit(CrmTypeModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_TYPE_UPDATE,
                                                            new UpdateCrmType
                                                            {
                                                                Id = model.Id,
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

        public async Task<TResponse<CrmTypeModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CRM_TYPE_GET_BY_ID, id);
                var response = await HttpService.Send<GetCrmTypeResponse>(url, null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(new CrmTypeModel { Id = response.Data.Id, Name = response.Data.Name });
                }

                return await Fail<CrmTypeModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CrmTypeModel>(exception);
            }
        }
    }
}
