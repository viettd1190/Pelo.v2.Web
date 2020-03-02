using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.ProductUnit;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.ProductUnit;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.ProductUnit
{
    public interface IProductUnitService
    {
        Task<IEnumerable<ProductUnitModel>> GetAll();

        Task<ProductUnitListModel> GetByPaging(ProductUnitSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<bool>> Insert(ProductUnitModel model);
        Task<TResponse<bool>> Update(ProductUnitModel model);
        Task<TResponse<GetProductUnitReponse>> GetById(int id);
    }

    public class ProductUnitService : BaseService,
                                      IProductUnitService
    {
        public ProductUnitService(IHttpService httpService,
                                  ILogger<BaseService> logger) : base(httpService,
                                                                      logger)
        {
        }

        #region IProductUnitService Members

        public async Task<ProductUnitListModel> GetByPaging(ProductUnitSearchModel request)
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

                    var url = string.Format(ApiUrl.PRODUCT_UNIT_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetProductUnitPagingResponse>>(url,
                                                                                                    null,
                                                                                                    HttpMethod.Get,
                                                                                                    true);

                    if(response.IsSuccess)
                        return new ProductUnitListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ProductUnitModel
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
                var url = string.Format(ApiUrl.PRODUCT_UNIT_DELETE,
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

        public async Task<IEnumerable<ProductUnitModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ProductUnitModel>>(ApiUrl.PRODUCT_UNIT_GET_ALL,
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

        public async Task<TResponse<bool>> Insert(ProductUnitModel model)
        {
            try
            {
                var url = ApiUrl.PRODUCT_UNIT_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new InsertProductUnit { Name = model.Name },
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

        public async Task<TResponse<bool>> Update(ProductUnitModel model)
        {
            try
            {
                var url = ApiUrl.PRODUCT_UNIT_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            new UpdateProductUnit { Id = model.Id, Name = model.Name },
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

        public async Task<TResponse<GetProductUnitReponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.PRODUCT_UNIT_GET_BY_ID, id);
                var response = await HttpService.Send<GetProductUnitReponse>(url,
                                                                      null,
                                                                      HttpMethod.Get,
                                                                      true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetProductUnitReponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetProductUnitReponse>(exception);
            }
        }

        #endregion
    }
}
