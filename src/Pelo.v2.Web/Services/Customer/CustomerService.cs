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

        Task<TResponse<bool>> Insert(CustomerInsertModel request);

        Task<TResponse<GetCustomerDetailResponse>> GetDetail(int id);

        Task<TResponse<CustomerUpdateModel>> GetById(int id);

        Task<TResponse<bool>> Update(CustomerUpdateModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<GetCustomerDetailResponse>> GetCustomerByPhone(string phone);
    }

    public class CustomerService : BaseService,
                                   ICustomerService
    {
        public CustomerService(IHttpService httpService,
                               ILogger<BaseService> logger) : base(httpService,
                                                                   logger)
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

                    var url = string.Format(ApiUrl.CUSTOMER_GET_BY_PAGING,
                                            request.Code,
                                            request.Name,
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
                                                                                     DateUpdated = c.DateUpdated,
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

        public async Task<TResponse<bool>> Insert(CustomerInsertModel request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_INSERT;
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

        public async Task<TResponse<GetCustomerDetailResponse>> GetDetail(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_GET_DETAIL,
                                        id);
                var response = await HttpService.Send<GetCustomerDetailResponse>(url,
                                                                                 null,
                                                                                 HttpMethod.Get,
                                                                                 true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetCustomerDetailResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCustomerDetailResponse>(exception);
            }
        }

        public async Task<TResponse<CustomerUpdateModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetCustomerByIdResponse>(url,
                                                                               null,
                                                                               HttpMethod.Get,
                                                                               true);
                if(response.IsSuccess)
                {
                    return await Ok(new CustomerUpdateModel
                                    {
                                            Id = response.Data.Id,
                                            Address = response.Data.Address,
                                            CustomerGroupId = response.Data.CustomerGroupId,
                                            Description = response.Data.Description,
                                            DistrictId = response.Data.DistrictId,
                                            Email = response.Data.Email,
                                            Name = response.Data.Name,
                                            Code = response.Data.Code,
                                            Phone = response.Data.Phone,
                                            Phone2 = response.Data.Phone2,
                                            Phone3 = response.Data.Phone3,
                                            ProvinceId = response.Data.ProvinceId,
                                            Profit = response.Data.Profit,
                                            ProfitUpdate = response.Data.ProfitUpdate,
                                            WardId = response.Data.WardId
                                    });
                }

                return await Fail<CustomerUpdateModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CustomerUpdateModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(CustomerUpdateModel request)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new
                                                            {
                                                                    request.Id,
                                                                    request.Name,
                                                                    request.Phone,
                                                                    request.Phone2,
                                                                    request.Phone3,
                                                                    request.Email,
                                                                    request.ProvinceId,
                                                                    request.DistrictId,
                                                                    request.WardId,
                                                                    request.Address,
                                                                    request.CustomerGroupId,
                                                                    request.Description
                                                            },
                                                            HttpMethod.Put,
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

        public async Task<TResponse<GetCustomerDetailResponse>> GetCustomerByPhone(string phone)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_GET_BY_PHONE, phone);
                var response = await HttpService.Send<CustomerByPhoneResponse>(url,
                                                                               null,
                                                                               HttpMethod.Get,
                                                                               true);
                if (response.IsSuccess)
                {
                    return await Ok(new GetCustomerDetailResponse
                    {
                        Id = response.Data.Id,
                        Address = response.Data.Address,
                        Description = response.Data.Description,
                        Name = response.Data.Name,
                        Code = response.Data.Code,
                        Phone = response.Data.Phone,
                        Phone2 = response.Data.Phone2,
                        Phone3 = response.Data.Phone3,
                    });
                }

                return await Fail<GetCustomerDetailResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetCustomerDetailResponse>(exception);
            }
        }

        #endregion
    }
}
