using System;
using System.Collections.Generic;
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
        Task<IEnumerable<CustomerSourceModel>> GetAll();

        Task<CustomerSourceListModel> GetByPaging(CustomerSourceSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Add(CustomerSourceModel model);

        Task<TResponse<bool>> Edit(CustomerSourceModel model);

        Task<TResponse<CustomerSourceModel>> GetById(int id);
    }

    public class CustomerSourceService : BaseService,
                                         ICustomerSourceService
    {
        public CustomerSourceService(IHttpService httpService,
                                     ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICustomerSourceService Members

        public async Task<IEnumerable<CustomerSourceModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CustomerSourceModel>>(ApiUrl.CUSTOMER_SOURCE_GET_ALL,
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

        public async Task<CustomerSourceListModel> GetByPaging(CustomerSourceSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    if (request.Columns != null
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

                    var url = string.Format(ApiUrl.CUSTOMER_SOURCE_GET_BY_PAGING,
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
                var url = string.Format(ApiUrl.CUSTOMER_SOURCE_DELETE,
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

        public async Task<TResponse<bool>> Add(CustomerSourceModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CUSTOMER_SOURCE_UPDATE,
                                                            new InsertCustomerSource
                                                            {
                                                                Name = model.Name
                                                            },
                                                            HttpMethod.Post,
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

        public async Task<TResponse<bool>> Edit(CustomerSourceModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CUSTOMER_SOURCE_UPDATE,
                                                            new UpdateCustomerSource
                                                            {
                                                                Id = model.Id,
                                                                Name = model.Name
                                                            },
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

        public async Task<TResponse<CustomerSourceModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_SOURCE_GET_BY_ID, id);
                var response = await HttpService.Send<GetCustomerSourceResponse>(url, null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(new CustomerSourceModel { Id = response.Data.Id, Name = response.Data.Name });
                }

                return await Fail<CustomerSourceModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CustomerSourceModel>(exception);
            }
        }

        #endregion
    }
}
