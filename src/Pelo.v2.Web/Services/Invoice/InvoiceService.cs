using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Invoice;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Models.Invoice;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Invoice
{
    public interface IInvoiceService
    {
        Task<InvoiceListModel> GetByPaging(InvoiceSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<InvoiceListModel> GetByCustomerIdPaging(CustomerInvoiceSearchModel request);
    }

    public class InvoiceService : BaseService,
                                  IInvoiceService
    {
        public InvoiceService(IHttpService httpService,
                              ILogger<BaseService> logger) : base(httpService,
                                                                  logger)
        {
        }

        #region IInvoiceService Members

        public async Task<InvoiceListModel> GetByPaging(InvoiceSearchModel request)
        {
            try
            {
                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.INVOICE_GET_BY_PAGING,
                                            request.CustomerCode,
                                            request.CustomerPhone,
                                            request.CustomerName,
                                            request.Code,
                                            request.BranchId,
                                            request.InvoiceStatusId,
                                            request.UserCreatedId,
                                            request.UserSellId,
                                            request.UserDeliveryId,
                                            request.FromDate,
                                            request.ToDate,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetInvoicePagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                    if(response.IsSuccess)
                        return new InvoiceListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new InvoiceModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Code = c.Code,
                                                                                     Branch = c.Branch,
                                                                                     Customer = c.CustomerName,
                                                                                     CustomerCode = c.CustomerCode,
                                                                                     CustomerAddress = c.CustomerAddress,
                                                                                     CustomerPhone = c.CustomerPhone,
                                                                                     CustomerPhone2 = c.CustomerPhone2,
                                                                                     CustomerPhone3 = c.CustomerPhone3,
                                                                                     Province = c.Province,
                                                                                     District = c.District,
                                                                                     InvoiceStatus = c.InvoiceStatus,
                                                                                     InvoiceStatusColor = c.InvoiceStatusColor,
                                                                                     Ward = c.Ward,
                                                                                     UserSellPhone = c.UserSellPhone,
                                                                                     UserDeliveries = c.UsersDelivery.Select(v => new UserDisplaySimpleList
                                                                                                                                  {
                                                                                                                                          Id = v.Id,
                                                                                                                                          DisplayName = v.DisplayName,
                                                                                                                                          PhoneNumber = v.PhoneNumber
                                                                                                                                  })
                                                                                                       .ToList(),
                                                                                     UserSell = c.UserSell,
                                                                                     UserCreatedPhone = c.UserCreatedPhone,
                                                                                     DateCreated = c.DateCreated,
                                                                                     DeliveryDate = c.DeliveryDate,
                                                                                     Products = c.Products.Select(v => new ProductInInvoiceSimpleList
                                                                                                                       {
                                                                                                                               Description = v.Description,
                                                                                                                               Id = v.Id,
                                                                                                                               Name = v.Name
                                                                                                                       })
                                                                                                 .ToList(),
                                                                                     UserCreated = c.UserCreated,
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
                var url = string.Format(ApiUrl.INVOICE_DELETE,
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

        public async Task<InvoiceListModel> GetByCustomerIdPaging(CustomerInvoiceSearchModel request)
        {
            try
            {
                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.GET_INVOICE_CUSTOMER_BY_PAGING,
                                            request.CustomerId,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetInvoicePagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                    if(response.IsSuccess)
                        return new InvoiceListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new InvoiceModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Code = c.Code,
                                                                                     Branch = c.Branch,
                                                                                     InvoiceStatus = c.InvoiceStatus,
                                                                                     InvoiceStatusColor = c.InvoiceStatusColor,
                                                                                     UserSellPhone = c.UserSellPhone,
                                                                                     UserDeliveries = c.UsersDelivery.Select(v => new UserDisplaySimpleList
                                                                                                                                  {
                                                                                                                                          Id = v.Id,
                                                                                                                                          DisplayName = v.DisplayName,
                                                                                                                                          PhoneNumber = v.PhoneNumber
                                                                                                                                  })
                                                                                                       .ToList(),
                                                                                     UserSell = c.UserSell,
                                                                                     UserCreatedPhone = c.UserCreatedPhone,
                                                                                     DateCreated = c.DateCreated,
                                                                                     DeliveryDate = c.DeliveryDate,
                                                                                     Products = c.Products.Select(v => new ProductInInvoiceSimpleList
                                                                                                                       {
                                                                                                                               Description = v.Description,
                                                                                                                               Id = v.Id,
                                                                                                                               Name = v.Name
                                                                                                                       })
                                                                                                 .ToList(),
                                                                                     UserCreated = c.UserCreated,
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

        #endregion
    }
}
