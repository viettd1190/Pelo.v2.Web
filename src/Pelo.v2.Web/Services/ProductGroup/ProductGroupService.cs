using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.ProductGroup;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.ProductGroup;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.ProductGroup
{
    public interface IProductGroupService
    {
        Task<IEnumerable<ProductGroupModel>> GetAll();

        Task<ProductGroupListModel> GetByPaging(ProductGroupSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class ProductGroupService : BaseService,
                                       IProductGroupService
    {
        public ProductGroupService(IHttpService httpService,
                                   ILogger<BaseService> logger) : base(httpService,
                                                                       logger)
        {
        }

        #region IProductGroupService Members

        public async Task<ProductGroupListModel> GetByPaging(ProductGroupSearchModel request)
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

                    var url = string.Format(ApiUrl.PRODUCT_GROUP_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetProductGroupPagingResponse>>(url,
                                                                                                     null,
                                                                                                     HttpMethod.Get,
                                                                                                     true);

                    if(response.IsSuccess)
                        return new ProductGroupListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ProductGroupModel
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
                var url = string.Format(ApiUrl.PRODUCT_GROUP_DELETE,
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

        public async Task<IEnumerable<ProductGroupModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ProductGroupModel>>(ApiUrl.PRODUCT_GROUP_GET_ALL,
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

        #endregion
    }
}
