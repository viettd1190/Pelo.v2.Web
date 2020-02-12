using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Province;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Province;
using Pelo.v2.Web.Services.Http;
using ProvinceModel = Pelo.v2.Web.Models.Province.ProvinceModel;

namespace Pelo.v2.Web.Services.Province
{
    public interface IProvinceService
    {
        Task<TResponse<IEnumerable<ProvinceModel>>> GetAll();

        Task<ProvinceListModel> GetByPaging(ProvinceSearchModel request);

        Task<TResponse<bool>> Insert(ProvinceInsert request);

        Task<TResponse<ProvinceUpdate>> GetById(int id);

        Task<TResponse<bool>> Delete(int id);
    }

    public class ProvinceService : BaseService,
                                   IProvinceService
    {
        public ProvinceService(IHttpService httpService,
                               ILogger<ProvinceService> logger) : base(httpService, logger)
        {
        }

        #region IProvinceService Members

        public async Task<TResponse<IEnumerable<ProvinceModel>>> GetAll()
        {
            try
            {
                var url = ApiUrl.PROVINCE_GET_ALL;
                var response = await HttpService.Send<IEnumerable<ProvinceModel>>(url,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<ProvinceModel>>(response.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                return await Fail<IEnumerable<ProvinceModel>>(exception);
            }
        }

        public async Task<ProvinceListModel> GetByPaging(ProvinceSearchModel request)
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

                    var url = string.Format(ApiUrl.PROVINCE_GET_BY_PAGING,
                                            request.ProvinceName,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetProvincePagingResponse>>(url,
                                                                                                 null,
                                                                                                 HttpMethod.Get,
                                                                                                 true);

                    if(response.IsSuccess)
                        return new ProvinceListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ProvinceModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     SortOrder = c.SortOrder,
                                                                                     PageSize = request.PageSize,
                                                                                     Type = c.Type,
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

        public async Task<TResponse<bool>> Insert(ProvinceInsert request)
        {
            try
            {
                var url = ApiUrl.PROVINCE_INSERT;
                var response = await HttpService.Send<bool>(url,
                                                            request,
                                                            HttpMethod.Post,
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

        public async Task<TResponse<ProvinceUpdate>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.PROVINCE_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<ProvinceUpdate>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<ProvinceUpdate>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<ProvinceUpdate>(exception);
            }
        }

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.PROVINCE_DELETE,
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
    }
}
