using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.PayMethod;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.PayMethod;
using Pelo.v2.Web.Services.Http;
using PayMethodModel = Pelo.v2.Web.Models.PayMethod.PayMethodModel;

namespace Pelo.v2.Web.Services.PayMethod
{
    public interface IPayMethodService
    {
        Task<IEnumerable<PayMethodSimpleModel>> GetAll();

        Task<PayMethodListModel> GetByPaging(PayMethodSearchModel request);
    }

    public class PayMethodService : BaseService,
                                    IPayMethodService
    {
        public PayMethodService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService,
                                                                    logger)
        {
        }

        #region IPayMethodService Members

        public async Task<IEnumerable<PayMethodSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<PayMethodSimpleModel>>(ApiUrl.PAY_METHOD_GET_ALL,
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

        public async Task<PayMethodListModel> GetByPaging(PayMethodSearchModel request)
        {
            try
            {
                if(request != null)
                {
                    var start = request.Start / request.Length + 1;
                    var columnOrder = "Name";
                    var sortDir = "ASC";

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

                    var url = string.Format(ApiUrl.PAY_METHOD_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetPayMethodPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if(response.IsSuccess)
                        return new PayMethodListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new PayMethodModel
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

        #endregion
    }
}
