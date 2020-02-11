using Pelo.Common.Dtos.Branch;
using Pelo.Common.Dtos.Manufacturer;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Models.Manufacturer;
using Pelo.v2.Web.Services.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Services.MasterService
{
    public interface IManufacturerService
    {
        Task<IEnumerable<ManufacturerSimpleModel>> GetAll();

        Task<ManufacturerListModel> GetByPaging(ManufacturerSearchModel request);

    }
    public class ManufacturerService:BaseService, IManufacturerService
    {
        public ManufacturerService(IHttpService httpService) : base(httpService)
        {
        }

        public async Task<IEnumerable<ManufacturerSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ManufacturerSimpleModel>>(ApiUrl.MANUFACTURER_GET_ALL,
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

        public async Task<ManufacturerListModel> GetByPaging(ManufacturerSearchModel request)
        {
            try
            {
                var start = 1;

                if (request != null) start = request.Start / request.Length + 1;

                var columnOrder = "name";
                var sortDir = "ASC";

                var url = string.Format(ApiUrl.MANUFACTURER_GET_BY_PAGING,
                                        request.Name,
                                        request.ColumnOrder,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetManufacturerPagingResponse>>(url,
                                                                                              null,
                                                                                              HttpMethod.Get,
                                                                                              true);

                if (response.IsSuccess)
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
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }       
    }
}
