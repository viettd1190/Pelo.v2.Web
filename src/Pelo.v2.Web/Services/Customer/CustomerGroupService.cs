using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Customer;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Customer
{
    public interface ICustomerService
    {
        Task<CustomerListModel> GetByPaging(CustomerSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class CustomerService : BaseService,
                                        ICustomerService
    {
        public CustomerService(IHttpService httpService,
                                    ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region ICustomerService Members

        public async Task<CustomerListModel> GetByPaging(CustomerSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.CUSTOMER_GET_BY_PAGING,
                        request.Code,request.Name,
                                            request.ProvinceId,
                                            request.DistrictId,
                                            request.WardId,
                                            request.Address,
                                            request.Phone,
                                            request.Email,
                                            request.CustomerGroupId,
                                            request.CustomerVipId,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCustomerPagingResponse>>(url,
                                                                                                      null,
                                                                                                      HttpMethod.Get,
                                                                                                      true);

                    if(response.IsSuccess)
                        return new CustomerListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CustomerModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     Code = c.Code,
                                                                                     Address = c.Address,
                                                                                     CustomerGroup = c.CustomerGroup,
                                                                                     CustomerVip = c.CustomerVip,
                                                                                     Description = c.Description,
                                                                                     District = c.District,
                                                                                     Email = c.Email,
                                                                                     Phone = c.Phone,
                                                                                     Phone2 = c.Phone2,
                                                                                     Phone3 = c.Phone3,
                                                                                     Province = c.Province,
                                                                                     Ward = c.Ward,
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
                var url = string.Format(ApiUrl.CUSTOMER_DELETE,
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
