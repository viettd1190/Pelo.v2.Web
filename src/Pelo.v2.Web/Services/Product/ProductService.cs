using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Product;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Product;
using Pelo.v2.Web.Services.Http;
using ProductModel = Pelo.v2.Web.Models.Product.ProductModel;

namespace Pelo.v2.Web.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAll();

        Task<ProductListModel> GetByPaging(ProductSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Add(UpdateProductModel model);

        Task<TResponse<bool>> Edit(UpdateProductModel model);

        Task<TResponse<ProductModel>> GetById(int id);
    }

    public class ProductService : BaseService,
                                  IProductService
    {
        public ProductService(IHttpService httpService,
                              ILogger<BaseService> logger) : base(httpService,
                                                                  logger)
        {
        }

        #region IProductService Members

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ProductModel>>(ApiUrl.PRODUCT_GET_ALL,
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

        public async Task<ProductListModel> GetByPaging(ProductSearchModel request)
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

                    var url = string.Format(ApiUrl.PRODUCT_GET_BY_PAGING,
                                            request.Name,
                                            request.ProductStatusId,
                                            request.ProductGroupId,
                                            request.ProductUnitId,
                                            request.ManufacturerId,
                                            0, // country
                                            request.Description,
                                            request.WarrantyMonth,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetProductPagingResponse>>(url,
                                                                                                null,
                                                                                                HttpMethod.Get,
                                                                                                true);

                    if(response.IsSuccess)
                        return new ProductListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ProductModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Name = c.Name,
                                                                                     ImportPrice = c.ImportPrice,
                                                                                     SellPrice = c.SellPrice,
                                                                                     ProductGroup = c.ProductGroup,
                                                                                     ProductUnit = c.ProductUnit,
                                                                                     ProductStatus = c.ProductStatus,
                                                                                     Country = c.Country,
                                                                                     DateUpdated = c.DateUpdated,
                                                                                     Manufacturer = c.Manufacturer,
                                                                                     WarrantyMonth = c.WarrantyMonth,
                                                                                     Description = c.Description,
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
                var url = string.Format(ApiUrl.PRODUCT_DELETE,
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

        public async Task<TResponse<bool>> Add(UpdateProductModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.PRODUCT_UPDATE,
                                                            new InsertProduct
                                                            {
                                                                Name = model.Name,
                                                                CountryId = model.CountryId,
                                                                Description = model.Description,
                                                                ImportPrice = model.ImportPrice,
                                                                ManufacturerId = model.ManufacturerId,
                                                                MaxCount = model.MaxCount,
                                                                MinCount = model.MinCount,
                                                                ProductGroupId = model.ProductGroupId,
                                                                ProductStatusId = model.ProductStatusId,
                                                                ProductUnitId = model.ProductUnitId,
                                                                SellPrice = model.SellPrice,
                                                                WarrantyMonth = model.WarrantyMonth
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

        public async Task<TResponse<bool>> Edit(UpdateProductModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.PRODUCT_UPDATE,
                                                            new UpdateProduct
                                                            {
                                                                Id = model.Id,
                                                                Name = model.Name,
                                                                CountryId = model.CountryId,
                                                                Description = model.Description,
                                                                ImportPrice = model.ImportPrice,
                                                                ManufacturerId = model.ManufacturerId,
                                                                MaxCount = model.MaxCount,
                                                                MinCount = model.MinCount,
                                                                ProductGroupId = model.ProductGroupId,
                                                                ProductStatusId = model.ProductStatusId,
                                                                ProductUnitId = model.ProductUnitId,
                                                                SellPrice = model.SellPrice,
                                                                WarrantyMonth = model.WarrantyMonth
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

        public async Task<TResponse<ProductModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CRM_TYPE_GET_BY_ID, id);
                var response = await HttpService.Send<Pelo.Common.Dtos.Product.ProductModel>(url, null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(new ProductModel { Id = response.Data.Id, Name = response.Data.Name });
                }

                return await Fail<ProductModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<ProductModel>(exception);
            }
        }

        #endregion
    }
}
