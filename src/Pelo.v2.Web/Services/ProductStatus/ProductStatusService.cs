using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.ProductStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.ProductStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.ProductStatus
{
    public interface IProductStatusService
    {
        Task<IEnumerable<ProductStatusModel>> GetAll();

        Task<ProductStatusListModel> GetByPaging(ProductStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<bool>> Edit(UpdateProductStatus updateProductStatus);
        Task<TResponse<bool>> Add(InsertProductStatus insertProductStatus);
        Task<TResponse<ProductStatusModel>> GetById(int id);
    }

    public class ProductStatusService : BaseService,
                                        IProductStatusService
    {
        public ProductStatusService(IHttpService httpService,
                                    ILogger<BaseService> logger) : base(httpService,
                                                                        logger)
        {
        }

        #region IProductStatusService Members

        public async Task<ProductStatusListModel> GetByPaging(ProductStatusSearchModel request)
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

                    var url = string.Format(ApiUrl.PRODUCT_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetProductStatusPagingResponse>>(url,
                                                                                                      null,
                                                                                                      HttpMethod.Get,
                                                                                                      true);

                    if(response.IsSuccess)
                        return new ProductStatusListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ProductStatusModel
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
                var url = string.Format(ApiUrl.PRODUCT_STATUS_DELETE,
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

        public async Task<IEnumerable<ProductStatusModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ProductStatusModel>>(ApiUrl.PRODUCT_STATUS_GET_ALL,
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

        public async Task<TResponse<bool>> Edit(UpdateProductStatus updateProductStatus)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.PRODUCT_GROUP_UPDATE,
                                                            updateProductStatus,
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

        public async Task<TResponse<bool>> Add(InsertProductStatus insertProductStatus)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.PRODUCT_GROUP_UPDATE,
                                                            insertProductStatus,
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

        public async Task<TResponse<ProductStatusModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.PRODUCT_STATUS_GET_BY_ID, id);
                var response = await HttpService.Send<GetProductStatusReponse>(url, null, HttpMethod.Get, true);
                if (response.IsSuccess)
                {
                    return await Ok(new ProductStatusModel { Id = response.Data.Id, Name = response.Data.Name });
                }

                return await Fail<ProductStatusModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<ProductStatusModel>(exception);
            }
        }

        #endregion
    }
}
