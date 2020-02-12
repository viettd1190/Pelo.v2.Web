using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Department;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Department;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Department
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentSimpleModel>> GetAll();

        Task<DepartmentListModel> GetByPaging(DepartmentSearchModel request);
    }

    public class DepartmentService : BaseService,
                                     IDepartmentService
    {
        public DepartmentService(IHttpService httpService,
                                 ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IDepartmentService Members

        public async Task<IEnumerable<DepartmentSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<DepartmentSimpleModel>>(ApiUrl.DEPARTMENT_GET_ALL,
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

        public async Task<DepartmentListModel> GetByPaging(DepartmentSearchModel request)
        {
            try
            {
                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var columnOrder = "name";
                var sortDir = "ASC";

                var url = string.Format(ApiUrl.DEPARTMENT_GET_BY_PAGING,
                                        request.Name,
                                        request.ColumnOrder,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetDepartmentPagingResponse>>(url,
                                                                                               null,
                                                                                               HttpMethod.Get,
                                                                                               true);

                if(response.IsSuccess)
                    return new DepartmentListModel
                           {
                                   Draw = request.Draw,
                                   RecordsFiltered = response.Data.TotalCount,
                                   Total = response.Data.TotalCount,
                                   RecordsTotal = response.Data.TotalCount,
                                   Data = response.Data.Data.Select(c => new DepartmentModel
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
