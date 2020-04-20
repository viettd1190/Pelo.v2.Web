using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.WarrantyStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.WarrantyStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.WarrantyStatus
{
    public interface IWarrantyStatusService
    {
        Task<WarrantyStatusListModel> GetByPaging(WarrantyStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<GetWarrantyStatusResponse>> GetById(int id);

        Task<TResponse<bool>> Add(InsertWarrantyStatus warrantyStatus);

        Task<TResponse<bool>> Update(UpdateWarrantyStatus warrantyStatus);

        Task<IEnumerable<WarrantyStatusSimpleModel>> GetAll();
    }

    public class WarrantyStatusService : BaseService,
                                    IWarrantyStatusService
    {
        public WarrantyStatusService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IWarrantyStatusService Members

        public async Task<WarrantyStatusListModel> GetByPaging(WarrantyStatusSearchModel request)
        {
            try
            {
                var columnOrder = "Name";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.WARRANTY_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetWarrantyStatusPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new WarrantyStatusListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new WarrantyStatusModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
                                SortOrder = c.SortOrder,
                                SmsContent = c.SmsContent,
                                IsSendSms = c.IsSendSms,
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
                var url = string.Format(ApiUrl.WARRANTY_STATUS_DELETE,
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

        public async Task<IEnumerable<WarrantyStatusSimpleModel>> GetAll()
        {
            try
            {

                var response = await HttpService.Send<IEnumerable<WarrantyStatusSimpleModel>>(ApiUrl.WARRANTY_STATUS_GET_ALL,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return response.Data;
                }

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TResponse<GetWarrantyStatusResponse>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.WARRANTY_STATUS_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetWarrantyStatusResponse>(url,
                                                            null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<TResponse<bool>> Add(InsertWarrantyStatus warrantyStatus)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARRANTY_STATUS_UPDATE,
                                                            warrantyStatus,
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

        public async Task<TResponse<bool>> Update(UpdateWarrantyStatus warrantyStatus)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.WARRANTY_STATUS_UPDATE,
                                                            warrantyStatus,
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
