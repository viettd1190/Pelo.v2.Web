using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.CrmStatus;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models.CrmStatus;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.CrmStatus
{
    public interface ICrmStatusService
    {
        Task<IEnumerable<CrmStatusModel>> GetAll();

        Task<CrmStatusListModel> GetByPaging(CrmStatusSearchModel request);

        Task<TResponse<bool>> Delete(int id);

        Task<TResponse<bool>> Add(CrmStatusModel model);

        Task<TResponse<bool>> Edit(CrmStatusModel model);

        Task<TResponse<CrmStatusModel>> GetById(int id);
    }

    public class CrmStatusService : BaseService,
                                    ICrmStatusService
    {
        public CrmStatusService(IHttpService httpService,
                                ILogger<BaseService> logger) : base(httpService,
                                                                    logger)
        {
        }

        #region ICrmStatusService Members

        public async Task<IEnumerable<CrmStatusModel>> GetAll()
        {
            try
            {
                var response = await HttpService.Send<IEnumerable<CrmStatusModel>>(ApiUrl.CRM_STATUS_GET_ALL,
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

        public async Task<CrmStatusListModel> GetByPaging(CrmStatusSearchModel request)
        {
            try
            {
                var columnOrder = "name";
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

                    var url = string.Format(ApiUrl.CRM_STATUS_GET_BY_PAGING,
                                            request.Name,
                                            columnOrder,
                                            sortDir,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCrmStatusPagingResponse>>(url,
                                                                                                  null,
                                                                                                  HttpMethod.Get,
                                                                                                  true);

                    if (response.IsSuccess)
                        return new CrmStatusListModel
                        {
                            Draw = request.Draw,
                            RecordsFiltered = response.Data.TotalCount,
                            Total = response.Data.TotalCount,
                            RecordsTotal = response.Data.TotalCount,
                            Data = response.Data.Data.Select(c => new CrmStatusModel
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
                var url = string.Format(ApiUrl.CRM_STATUS_DELETE,
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

        public async Task<TResponse<bool>> Add(CrmStatusModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_STATUS_UPDATE,
                                                            new InsertCrmStatus
                                                            {
                                                                Color = model.Color,
                                                                IsSendSms = model.IsSendSms,
                                                                SmsContent = model.SmsContent,
                                                                Name = model.Name
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

        public async Task<TResponse<bool>> Edit(CrmStatusModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_STATUS_UPDATE,
                                                            new UpdateCrmStatus
                                                            {
                                                                Id = model.Id,
                                                                Color = model.Color,
                                                                IsSendSms = model.IsSendSms,
                                                                SmsContent = model.SmsContent,
                                                                Name = model.Name
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

        public async Task<TResponse<CrmStatusModel>> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CRM_STATUS_GET_BY_ID, id);
                var response = await HttpService.Send<GetCrmStatusResponse>(url, null,
                                                            HttpMethod.Get,
                                                            true);
                if (response.IsSuccess)
                {
                    return await Ok(new CrmStatusModel { Color = response.Data.Color, Id = response.Data.Id, IsSendSms = response.Data.IsSendSms, Name = response.Data.Name, SmsContent = response.Data.SmsContent });
                }

                return await Fail<CrmStatusModel>(response.Message);
            }
            catch (Exception exception)
            {
                return await Fail<CrmStatusModel>(exception);
            }
        }
        #endregion
    }
}
