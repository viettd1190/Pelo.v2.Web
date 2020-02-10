﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pelo.Common.Dtos.AppConfig;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.AppConfig;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.AppConfig
{
    public interface IAppConfigService
    {
        Task<AppConfigListModel> GetByPaging(AppConfigSearchModel request);
    }

    public class AppConfigService : BaseService,
                                    IAppConfigService
    {
        public AppConfigService(IHttpService httpService) : base(httpService)
        {
        }

        #region IAppConfigService Members

        public async Task<AppConfigListModel> GetByPaging(AppConfigSearchModel request)
        {
            try
            {
                var start = 1;

                if(request != null) start = request.Start / request.Length + 1;

                var columnOrder = "name";
                var sortDir = "ASC";

                var url = string.Format(ApiUrl.APP_CONFIG_GET_BY_PAGING,
                                        request.AppConfigName,
                                        request.AppConfigDescription,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetAppConfigPagingResponse>>(url,
                                                                                              null,
                                                                                              HttpMethod.Get,
                                                                                              true);

                if(response.IsSuccess)
                    return new AppConfigListModel
                           {
                                   Draw = request.Draw,
                                   RecordsFiltered = response.Data.TotalCount,
                                   Total = response.Data.TotalCount,
                                   RecordsTotal = response.Data.TotalCount,
                                   Data = response.Data.Data.Select(c => new AppConfigModel
                                                                         {
                                                                                 Id = c.Id,
                                                                                 Name = c.Name,
                                                                                 Description = c.Description,
                                                                                 PageSize = request.PageSize,
                                                                                 Value = c.Value,
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