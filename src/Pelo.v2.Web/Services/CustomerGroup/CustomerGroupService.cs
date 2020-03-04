using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Customer;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CustomerGroup;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CustomerGroup
{
    public interface ICustomerGroupService
    {
        Task<IEnumerable<CustomerGroupModel>> GetAll();

        Task<CustomerGroupListModel> GetByPaging(CustomerGroupSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<CustomerGroupModel>> GetById(int id);

        Task<TResponse<bool>> Update(UpdateCustomerGroupRequest model);

        Task<TResponse<bool>> Insert(InsertCustomerGroupRequest model);
    }

    public class CustomerGroupService : BaseService,
                                        ICustomerGroupService
    {
        public CustomerGroupService(IHttpService httpService,
                                    ILogger<BaseService> logger) : base(httpService,
                                                                        logger)
        {
        }

        #region ICustomerGroupService Members

        public async Task<CustomerGroupListModel> GetByPaging(CustomerGroupSearchModel request)
        {
            try
            {
                var columnOrder = "Name";
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

                    var url = string.Format(ApiUrl.CUSTOMER_GROUP_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCustomerGroupPagingResponse>>(url,
                                                                                                      null,
                                                                                                      HttpMethod.Get,
                                                                                                      true);

                    if(response.IsSuccess)
                        return new CustomerGroupListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CustomerGroupModel
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

        public async Task<IEnumerable<CustomerGroupModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CustomerGroupModel>>(ApiUrl.CUSTOMER_GROUP_GET_ALL,
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

        public async Task<TResponse<CustomerGroupModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CUSTOMER_GROUP_GET_BY_ID, id);
                var response = await HttpService.Send<GetCustomerGroupByIdResponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(new CustomerGroupModel { Id = response.Data.Id, Name = response.Data.Name});
                }

                return await Fail<CustomerGroupModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CustomerGroupModel>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateCustomerGroupRequest model)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_GROUP_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            model,
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

        public async Task<TResponse<bool>> Insert(InsertCustomerGroupRequest model)
        {
            try
            {
                var url = ApiUrl.CUSTOMER_GROUP_UPDATE;
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
