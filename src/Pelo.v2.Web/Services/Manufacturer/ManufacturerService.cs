using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Manufacturer;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Models.Manufacturer;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Manufacturer
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerModel>> GetAll();

        Task<ManufacturerListModel> GetByPaging(ManufacturerSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class ManufacturerService : BaseService,
                                       IManufacturerService
    {
        public ManufacturerService(IHttpService httpService,
                                   ILogger<BaseService> logger) : base(httpService,
                                                                       logger)
        {
        }

        #region IManufacturerService Members

        public async Task<ManufacturerListModel> GetByPaging(ManufacturerSearchModel request)
        {
            try
            {
                var columnOrder = "name";
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

                    var url = string.Format(ApiUrl.MANUFACTURER_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetManufacturerPagingResponse>>(url,
                                                                                                     null,
                                                                                                     HttpMethod.Get,
                                                                                                     true);

                    if(response.IsSuccess)
                        return new ManufacturerListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ManufacturerModel
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
                var url = string.Format(ApiUrl.MANUFACTURER_DELETE,
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

        public async Task<IEnumerable<ManufacturerModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ManufacturerModel>>(ApiUrl.MANUFACTURER_GET_ALL,
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

        #endregion
    }
}
