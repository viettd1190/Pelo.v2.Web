using Pelo.Common.Dtos.Branch;
using Pelo.Common.Dtos.PayMethod;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Models.Paymethod;
using Pelo.v2.Web.Services.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Services.MasterService
{
    public interface IPayMethodService
    {
        Task<IEnumerable<PayMethodSimpleModel>> GetAll();

        Task<PaymethodListModel> GetByPaging(PaymethodSearchModel request);

    }
    public class PayMethodService:BaseService, IPayMethodService
    {
        public PayMethodService(IHttpService httpService) : base(httpService)
        {
        }

        public async Task<IEnumerable<PayMethodSimpleModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<PayMethodSimpleModel>>(ApiUrl.PAY_METHOD_GET_ALL,
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

        public async Task<PaymethodListModel> GetByPaging(PaymethodSearchModel request)
        {
            try
            {
                var start = 1;

                if (request != null) start = request.Start / request.Length + 1;

                var columnOrder = "name";
                var sortDir = "ASC";

                var url = string.Format(ApiUrl.PAY_METHOD_GET_BY_PAGING,
                                        request.Name,
                                        request.ColumnOrder,
                                        start,
                                        request?.Length ?? 10,
                                        columnOrder,
                                        sortDir);

                var response = await HttpService.Send<PageResult<GetPayMethodPagingResponse>>(url,
                                                                                              null,
                                                                                              HttpMethod.Get,
                                                                                              true);

                if (response.IsSuccess)
                    return new PaymethodListModel
                    {
                        Draw = request.Draw,
                        RecordsFiltered = response.Data.TotalCount,
                        Total = response.Data.TotalCount,
                        RecordsTotal = response.Data.TotalCount,
                        Data = response.Data.Data.Select(c => new PaymethodModel
                        {
                            Id = c.Id,
                            Name = c.Name,
                            PageSize = request.PageSize,
                            PageSizeOptions = request.AvailablePageSizes
                        })
                    };

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }       
    }
}
