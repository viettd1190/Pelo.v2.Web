using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CustomerVip;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CustomerVip
{
    public interface ICustomerVipService
    {
        Task<IEnumerable<CustomerVipModel>> GetAll();

        Task<CustomerVipListModel> GetByPaging(CustomerVipSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<bool>> Insert(InsertCustomerVipRequest insertCustomerVipRequest);
        Task<TResponse<CustomerVipModel>> GetById(int id);
        Task<TResponse<bool>> Update(UpdateCustomerVipRequest updateCustomerVipRequest);
    }

    public class CustomerVipService : BaseService,
                                      ICustomerVipService
    {
        public CustomerVipService(IHttpService httpService,
                                  ILogger<BaseService> logger) : base(httpService,
                                                                      logger)
        {
        }

        #region ICustomerVipService Members

        public async Task<IEnumerable<CustomerVipModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CustomerVipModel>>(ApiUrl.CUSTOMER_VIP_GET_ALL,
                                                                                     null,
                                                                                     HttpMethod.Get,
                                                                                     true);

                if(response.IsSuccess)
                    return response.Data;

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<CustomerVipListModel> GetByPaging(CustomerVipSearchModel request)
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

                    var url = string.Format(ApiUrl.CUSTOMER_VIP_GET_BY_PAGING,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCustomerVipPagingResponse>>(url,
                                                                                                    null,
                                                                                                    HttpMethod.Get,
                                                                                                    true);

                    if(response.IsSuccess)
                        return new CustomerVipListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CustomerVipModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     Color = c.Color,
                                                                                     Profit = c.Profit,
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
                var url = string.Format(ApiUrl.CUSTOMER_VIP_DELETE,
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

        public async Task<TResponse<bool>> Insert(InsertCustomerVipRequest model)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_VIP_INSERT;
                var response = await HttpService.Send<bool>(url,
                                                            model,
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

        public async Task<TResponse<CustomerVipModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_VIP_GET_BY_ID, id);
                var response = await HttpService.Send<GetCustomerVipByIdResponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(new CustomerVipModel { Id = response.Data.Id, Name = response.Data.Name, Color = response.Data.Color, Profit = response.Data.Profit, SortOrder = response.Data.SortOder });
                }

                return await Fail<CustomerVipModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CustomerVipModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCustomerVipRequest model)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_VIP_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            model,
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

        #endregion
    }
}
