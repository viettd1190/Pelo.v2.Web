using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Country;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Country;
using Pelo.v2.Web.Services.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Services.Country
{
    public interface ICountryService
    {
        Task<IEnumerable<GetCountryReponse>> GetAll();

        Task<CountryListModel> GetByPaging(CountrySearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }
    public class CountryService : BaseService,
                                   ICountryService
    {
        public CountryService(IHttpService httpService,
                               ILogger<BaseService> logger) : base(httpService,
                                                                   logger)
        {
        }
        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.COUNTRY_DELETE,
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

        public async Task<IEnumerable<GetCountryReponse>> GetAll()
        {
            try
            {
                var url = string.Format(ApiUrl.COUNTRY_GET_ALL);
                var response = await HttpService.Send<IEnumerable<GetCountryReponse>>(url,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);
                if (response.IsSuccess)
                {
                    return response.Data;
                }

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                throw new PeloException(exception.Message);
            }
        }

        public async Task<CountryListModel> GetByPaging(CountrySearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.COUNTRY_GET_BY_PAGING,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCountryPagingResponse>>(url,
                                                                                                 null,
                                                                                                 HttpMethod.Get,
                                                                                                 true);

                    if (response.IsSuccess)
                        return new CountryListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new CountryModel
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
    }
}
