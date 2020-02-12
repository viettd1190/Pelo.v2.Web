using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.District;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.District;
using Pelo.v2.Web.Services.Http;
using DistrictModel = Pelo.v2.Web.Models.District.DistrictModel;

namespace Pelo.v2.Web.Services.Province
{
    public interface IDistrictService
    {
        Task<TResponse<IEnumerable<DistrictModel>>> GetAll(int provinceId);

        Task<DistrictListModel> GetByPaging(DistrictSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class DistrictService : BaseService,
                                   IDistrictService
    {
        public DistrictService(IHttpService httpService,
                               ILogger<BaseService> logger) : base(httpService,
                                                                   logger)
        {
        }

        #region IDistrictService Members

        public async Task<TResponse<IEnumerable<DistrictModel>>> GetAll(int provinceId)
        {
            try
            {
                var url = string.Format(ApiUrl.DISTRICT_GET_ALL,
                                        provinceId);
                var response = await HttpService.Send<IEnumerable<DistrictModel>>(url,
                                                                                  null,
                                                                                  HttpMethod.Get,
                                                                                  true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<IEnumerable<DistrictModel>>(response.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                return await Fail<IEnumerable<DistrictModel>>(exception);
            }
        }

        public async Task<DistrictListModel> GetByPaging(DistrictSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.DISTRICT_GET_BY_PAGING,
                                            request.DistrictName,
                                            request.ProvinceId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetDistrictPagingResponse>>(url,
                                                                                                 null,
                                                                                                 HttpMethod.Get,
                                                                                                 true);

                    if(response.IsSuccess)
                        return new DistrictListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new DistrictModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     Province = c.Province,
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

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.DISTRICT_DELETE,
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
