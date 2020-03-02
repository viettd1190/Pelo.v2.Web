using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Role;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Role;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Role
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleSimpleModel>> GetAll();

        Task<RoleListModel> GetByPaging(RoleSearchModel request);
        Task<TResponse<bool>> Insert(RoleModel model);
        Task<TResponse<GetRoleReponse>> GetById(int id);
        Task<TResponse<bool>> Update(RoleModel model);
        Task<TResponse<bool>> Delete(int id);
    }

    public class RoleService : BaseService,
                               IRoleService
    {
        public RoleService(IHttpService httpService,
                           ILogger<BaseService> logger) : base(httpService,
                                                               logger)
        {
        }

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.ROLE_DELETE,
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

        #region IRoleService Members

        public async Task<IEnumerable<RoleSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<RoleSimpleModel>>(ApiUrl.ROLE_GET_ALL,
                                                                                    null,
                                                                                    HttpMethod.Get,
                                                                                    true);
                if(response.IsSuccess) return response.Data;

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TResponse<GetRoleReponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.GET_ROLE_ID, id);
                var response = await HttpService.Send<GetRoleReponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetRoleReponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetRoleReponse>(exception);
            }
        }

        public async Task<RoleListModel> GetByPaging(RoleSearchModel request)
        {
            try
            {
                if(request != null)
                {
                    var start = request.Start / request.Length + 1;
                    var columnOrder = "Name";
                    var sortDir = "ASC";

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

                    var url = string.Format(ApiUrl.ROLE_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetRolePagingResponse>>(url,
                                                                                             null,
                                                                                             HttpMethod.Get,
                                                                                             true);

                    if(response.IsSuccess)
                        return new RoleListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new RoleModel
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

        public async Task<TResponse<bool>> Insert(RoleModel model)
        {
            try
            {
                var url = ApiUrl.ROLE_INSERT;
                var response = await HttpService.Send<bool>(url,
                                                            new InsertRole { Name = model.Name },
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

        public async Task<TResponse<bool>> Update(RoleModel model)
        {
            try
            {
                var url = ApiUrl.ROLE_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new UpdateRole { Id = model.Id, Name = model.Name },
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
