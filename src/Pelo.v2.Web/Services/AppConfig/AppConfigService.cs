using System;
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

        Task<TResponse<bool>> Delete(int id);
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
                var url = string.Format(ApiUrl.APP_CONFIG_DELETE,
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

        #endregion
    }
}
