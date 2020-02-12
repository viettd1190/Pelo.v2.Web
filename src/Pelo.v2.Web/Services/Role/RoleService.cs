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
    }

    public class RoleService : BaseService, IRoleService
    {
        public RoleService(IHttpService httpService,
                           ILogger<BaseService> logger) : base(httpService, logger)
        {
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

        public async Task<RoleListModel> GetByPaging(RoleSearchModel request)
        {
            try
            {
                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var columnOrder = request.ColumnOrder??"name";
                var sortDir = "ASC";

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
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        #endregion
    }
}
