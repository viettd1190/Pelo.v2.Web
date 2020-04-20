using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.WarrantyDescription;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.WarrantyDescription;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.WarrantyDescription
{
    public interface IWarrantyDescriptionService
    {
        Task<WarrantyDescriptionListModel> GetByPaging(WarrantyDescriptionSearchModel request);

        Task<TResponse<bool>> Delete(int id);
        Task<TResponse<bool>> Add(InsertWarrantyDescription insertWarrantyDescription);
        Task<TResponse<GetWarrantyDescriptionPagingResponse>> GetById(int id);
        Task<TResponse<bool>> Update(UpdateWarrantyDescription updateWarrantyDescription);
    }

    public class WarrantyDescriptionService : BaseService,
                                    IWarrantyDescriptionService
    {
        public WarrantyDescriptionService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IWarrantyDescriptionService Members

        public async Task<WarrantyDescriptionListModel> GetByPaging(WarrantyDescriptionSearchModel request)
        {
            try
            {
                var columnOrder = "Name";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.WARRANTY_DESCRIPTION_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetWarrantyDescriptionPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new WarrantyDescriptionListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new WarrantyDescriptionModel
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
                var url = string.Format(ApiUrl.WARRANTY_DESCRIPTION_DELETE,
                                        id);
                var response = await HttpService.Send<bool>(url,
                                                            null,
                                                            HttpMethod.Delete,
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

        public async Task<TResponse<bool>> Add(InsertWarrantyDescription insertWarrantyDescription)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARRANTY_DESCRIPTION_INSERT,
                                                            insertWarrantyDescription,
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

        public async Task<TResponse<GetWarrantyDescriptionPagingResponse>> GetById(int id)
        {
            try
            {
                string url = string.Format(ApiUrl.GET_WARRANTY_DESCRIPTION_ID, id);
                var response = await HttpService.Send<GetWarrantyDescriptionPagingResponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<GetWarrantyDescriptionPagingResponse>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<GetWarrantyDescriptionPagingResponse>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(UpdateWarrantyDescription updateWarrantyDescription)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARRANTY_DESCRIPTION_UPDATE,
                                                            updateWarrantyDescription,
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

        #endregion
    }
}
