using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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

        Task<TResponse<bool>> Insert(AppConfigInsert request);

        Task<TResponse<AppConfigUpdate>> GetById(int id);

        Task<TResponse<bool>> Update(AppConfigUpdate request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class AppConfigService : BaseService,
                                    IAppConfigService
    {
        public AppConfigService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IAppConfigService Members

        public async Task<AppConfigListModel> GetByPaging(AppConfigSearchModel request)
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

                    var url = string.Format(ApiUrl.APP_CONFIG_GET_BY_PAGING,
                                            request.AppConfigName,
                                            request.AppConfigDescription,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

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

                throw new PeloException("Request is null");
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TResponse<bool>> Insert(AppConfigInsert request)
        {
            try
            {
                var url = ApiUrl.APP_CONFIG_INSERT;
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

        public async Task<TResponse<AppConfigUpdate>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.APP_CONFIG_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<AppConfigUpdate>(url,
                                                                       null,
                                                                       HttpMethod.Get,
                                                                       true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<AppConfigUpdate>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<AppConfigUpdate>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(AppConfigUpdate request)
        {
            try
            {
                var url = ApiUrl.APP_CONFIG_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            request,
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

        public async Task<TResponse<bool>> Delete(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.APP_CONFIG_DELETE,
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
