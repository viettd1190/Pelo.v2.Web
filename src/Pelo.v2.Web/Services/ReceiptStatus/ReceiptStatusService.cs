using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.ReceiptStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.ReceiptStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.ReceiptStatus
{
    public interface IReceiptStatusService
    {
        Task<IEnumerable<ReceiptStatusModel>> GetAll();

        Task<ReceiptStatusListModel> GetByPaging(ReceiptStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);
    }

    public class ReceiptStatusService : BaseService,
                                     IReceiptStatusService
    {
        public ReceiptStatusService(IHttpService httpService,
                                 ILogger<BaseService> logger) : base(httpService,
                                                                     logger)
        {
        }

        #region IReceiptStatusService Members

        public async Task<IEnumerable<ReceiptStatusModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<ReceiptStatusModel>>(ApiUrl.RECEIPT_STATUS_GET_ALL,
                                                                                    null,
                                                                                    HttpMethod.Get,
                                                                                    true);

                if (response.IsSuccess)
                    return response.Data;

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<ReceiptStatusListModel> GetByPaging(ReceiptStatusSearchModel request)
        {
            try
            {
                var columnOrder = "SortOrder";
                var sortDir = "ASC";

                if (request != null)
                {
                    var start = request.Start / request.Length + 1;

                    if (request.Columns != null
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

                    var url = string.Format(ApiUrl.RECEIPT_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetReceiptStatusPagingResponse>>(url,
                                                                                                   null,
                                                                                                   HttpMethod.Get,
                                                                                                   true);

                    if (response.IsSuccess)
                        return new ReceiptStatusListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new ReceiptStatusModel
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Color = c.Color,
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
                var url = string.Format(ApiUrl.RECEIPT_STATUS_DELETE,
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

        #endregion
    }
}
