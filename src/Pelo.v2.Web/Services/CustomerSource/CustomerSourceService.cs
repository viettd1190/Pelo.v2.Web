using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CustomerSource;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CustomerSource;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CustomerSource
{
    public interface ICustomerSourceService
    {
        Task<CustomerSourceListModel> GetByPaging(CustomerSourceSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class CustomerSourceService : BaseService,
                                         ICustomerSourceService
    {
        public CustomerSourceService(IHttpService httpService,
                                     ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICustomerSourceService Members

        public async Task<CustomerSourceListModel> GetByPaging(CustomerSourceSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.CUSTOMER_GROUP_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCustomerSourcePagingResponse>>(url,
                                                                                                       null,
                                                                                                       HttpMethod.Get,
                                                                                                       true);

                    if(response.IsSuccess)
                        return new CustomerSourceListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CustomerSourceModel
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
                var url = string.Format(ApiUrl.CUSTOMER_GROUP_DELETE,
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
