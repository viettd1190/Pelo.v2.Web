﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.ReceiptDescription;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.ReceiptDescription;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.ReceiptDescription
{
    public interface IReceiptDescriptionService
    {
        Task<IEnumerable<ReceiptDescriptionModel>> GetAll();

        Task<ReceiptDescriptionListModel> GetByPaging(ReceiptDescriptionSearchModel request);

        Task<TResponse<bool>> Insert(ReceiptDescriptionInsert request);

        Task<TResponse<ReceiptDescriptionUpdate>> GetById(int id);

        Task<TResponse<bool>> Update(ReceiptDescriptionUpdate request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class ReceiptDescriptionService : BaseService,
                                             IReceiptDescriptionService
    {
        public ReceiptDescriptionService(IHttpService httpService,
                                         ILogger<BaseService> logger) : base(httpService, logger)
        {
        }

        #region IReceiptDescriptionService Members

        public async Task<IEnumerable<ReceiptDescriptionModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ReceiptDescriptionModel>>(ApiUrl.RECEIPT_DESCRIPTION_GET_ALL,
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

        public async Task<ReceiptDescriptionListModel> GetByPaging(ReceiptDescriptionSearchModel request)
        {
            try
            {
                var columnOrder = "name";
                var sortDir = "ASC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.RECEIPT_DESCRIPTION_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetReceiptDescriptionPagingResponse>>(url,
                                                                                                           null,
                                                                                                           HttpMethod.Get,
                                                                                                           true);

                    if(response.IsSuccess)
                        return new ReceiptDescriptionListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new ReceiptDescriptionModel
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

        public async Task<TResponse<bool>> Insert(ReceiptDescriptionInsert request)
        {
            try
            {
                var url = ApiUrl.RECEIPT_DESCRIPTION_INSERT;
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

        public async Task<TResponse<ReceiptDescriptionUpdate>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.RECEIPT_DESCRIPTION_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<ReceiptDescriptionUpdate>(url,
                                                                                null,
                                                                                HttpMethod.Get,
                                                                                true);
                if(response.IsSuccess)
                {
                    return await Ok(response.Data);
                }

                return await Fail<ReceiptDescriptionUpdate>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<ReceiptDescriptionUpdate>(exception);
            }
        }

        public async Task<TResponse<bool>> Update(ReceiptDescriptionUpdate request)
        {
            try
            {
                var url = ApiUrl.RECEIPT_DESCRIPTION_UPDATE;
                var response = await HttpService.Send<bool>(url,
                                                            request,
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
                var url = string.Format(ApiUrl.RECEIPT_DESCRIPTION_DELETE,
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
