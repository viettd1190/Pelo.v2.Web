﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Ward;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.District;
using Pelo.v2.Web.Models.Ward;
using Pelo.v2.Web.Services.Http;
using WardModel = Pelo.v2.Web.Models.Ward.WardModel;

namespace Pelo.v2.Web.Services.Province
{
    public interface IWardService
    {
        Task<WardListModel> GetByPaging(WardSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class WardService : BaseService,
                               IWardService
    {
        public WardService(IHttpService httpService,
                           ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IWardService Members

        public async Task<WardListModel> GetByPaging(WardSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.WARD_GET_BY_PAGING,
                                            request.WardName,
                                            request.DistrictId,
                                            request.ProvinceId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetWardPagingResponse>>(url,
                                                                                             null,
                                                                                             HttpMethod.Get,
                                                                                             true);

                    if(response.IsSuccess)
                        return new WardListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new WardModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     District = c.District,
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
                var url = string.Format(ApiUrl.WARD_DELETE,
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
