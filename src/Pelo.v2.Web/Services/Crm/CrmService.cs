using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pelo.Common.Dtos.Crm;
using Pelo.Common.Exceptions;
using Pelo.Common.Extensions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models;
using Pelo.v2.Web.Models.Crm;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Services.Http;

namespace Pelo.v2.Web.Services.Crm
{
    public interface ICrmService
    {
        Task<CrmListModel> GetByPaging(CrmSearchModel request);

        Task<CrmListModel> KhachChuaXuLyTrongNgay(BaseSearchModel request);

        Task<CrmListModel> KhachToiHenCanChamSoc(BaseSearchModel request);

        Task<CrmListModel> KhachQuaHenChamSoc(BaseSearchModel request);

        Task<CrmListModel> KhachToiHenNgayMai(BaseSearchModel request);

        Task<TResponse<bool>> Insert(InsertCrmModel model);

        Task<TResponse<bool>> Update(UpdateCrmModel model);

        Task<CrmListModel> GetByCustomerIdPaging(CustomerComponentSearchModel request);

        Task<UpdateCrmModel> GetById(int id);

        Task<IEnumerable<CrmLogResponse>> GetLogs(int id);

        Task<TResponse<bool>> Comment(CrmCommentModel model,
                                      List<IFormFile> files);
    }

    public class CrmService : BaseService,
                              ICrmService
    {
        private readonly ContextHelper _contextHelper;

        public CrmService(IHttpService httpService,
                          ContextHelper contextHelper,
                          ILogger<BaseService> logger) : base(httpService,
                                                              logger)
        {
            _contextHelper = contextHelper;
        }

        #region ICrmService Members

        public async Task<CrmListModel> GetByPaging(CrmSearchModel request)
        {
            try
            {
                var columnOrder = "DateCreated";
                var sortDir = "DESC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.CRM_GET_BY_PAGING,
                                            request.Code,
                                            request.CustomerCode,
                                            request.CustomerName,
                                            request.CustomerPhone,
                                            request.CustomerAddress,
                                            request.ProvinceId,
                                            request.DistrictId,
                                            request.WardId,
                                            request.CustomerGroupId,
                                            request.CustomerVipId,
                                            request.CustomerSourceId,
                                            request.ProductGroupId,
                                            request.CrmStatusId,
                                            request.CrmTypeId,
                                            request.CrmPriorityId,
                                            request.IsVisit,
                                            request.FromDate,
                                            request.ToDate,
                                            request.UserCreatedId,
                                            request.DateCreated,
                                            request.UserCareId,
                                            request.Need,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCrmPagingResponse>>(url,
                                                                                            null,
                                                                                            HttpMethod.Get,
                                                                                            true);

                    if(response.IsSuccess)
                        return new CrmListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CrmModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Code = c.Code,
                                                                                     ContactDate = c.ContactDate,
                                                                                     CrmPriority = c.CrmPriority,
                                                                                     CrmStatus = c.CrmStatus,
                                                                                     CrmStatusColor = c.CrmStatusColor,
                                                                                     CrmType = c.CrmType,
                                                                                     CustomerAddress = c.CustomerAddress,
                                                                                     CustomerGroup = c.CustomerGroup,
                                                                                     CustomerName = c.CustomerName,
                                                                                     CustomerPhone1 = c.CustomerPhone,
                                                                                     CustomerPhone2 = c.CustomerPhone2,
                                                                                     CustomerPhone3 = c.CustomerPhone3,
                                                                                     CustomerSource = c.CustomerSource,
                                                                                     CustomerVip = c.CustomerVip,
                                                                                     DateCreated = c.DateCreated,
                                                                                     District = c.District,
                                                                                     Province = c.Province,
                                                                                     Ward = c.Ward,
                                                                                     Need = c.Need,
                                                                                     ProductGroup = c.ProductGroup,
                                                                                     UserCreated = c.UserCreated,
                                                                                     UserCreatedPhone = c.UserCreatedPhone,
                                                                                     Visit = c.Visit == 1
                                                                                                     ? "Đã đến"
                                                                                                     : "Chưa đến",
                                                                                     UserCares = c.UserCares.Select(v => new UserCareModel
                                                                                                                         {
                                                                                                                                 Name = v.DisplayName,
                                                                                                                                 Phone = v.PhoneNumber
                                                                                                                         })
                                                                                                  .ToList(),
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

        public async Task<CrmListModel> KhachChuaXuLyTrongNgay(BaseSearchModel request)
        {
            return await XuLyCrm(request,
                                 ApiUrl.CRM_KHACH_CHUA_XU_LY_TRONG_NGAY);
        }

        public async Task<CrmListModel> KhachToiHenCanChamSoc(BaseSearchModel request)
        {
            return await XuLyCrm(request,
                                 ApiUrl.CRM_KHACH_TOI_HEN_CAN_CHAM_SOC);
        }

        public async Task<CrmListModel> KhachQuaHenChamSoc(BaseSearchModel request)
        {
            return await XuLyCrm(request,
                                 ApiUrl.CRM_KHACH_QUA_HEN_CHAM_SOC);
        }

        public async Task<CrmListModel> KhachToiHenNgayMai(BaseSearchModel request)
        {
            return await XuLyCrm(request,
                                 ApiUrl.CRM_KHACH_TOI_HEN_NGAY_MAI);
        }

        public async Task<TResponse<bool>> Insert(InsertCrmModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_INSERT,
                                                            new InsertCrmRequest
                                                            {
                                                                    CustomerId = model.CustomerId,
                                                                    CrmStatusId = model.CrmStatusId,
                                                                    ProductGroupId = model.ProductGroupId,
                                                                    CrmPriorityId = model.CrmPriorityId,
                                                                    CrmTypeId = model.CrmTypeId,
                                                                    Need = model.Need,
                                                                    Description = model.Description,
                                                                    CustomerSourceId = model.CustomerSourceId,
                                                                    Visit = model.IsVisit,
                                                                    ContactDate = model.ContactDate,
                                                                    UserIds = model.UserCareIds.ToList()
                                                            },
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

        public async Task<TResponse<bool>> Update(UpdateCrmModel model)
        {
            try
            {
                var response = await HttpService.Send<bool>(ApiUrl.CRM_UPDATE,
                                                            new UpdateCrmRequest
                                                            {
                                                                    Id = model.Id,
                                                                    ContactDate = model.ContactDate,
                                                                    CrmPriorityId = model.CrmPriorityId,
                                                                    CrmStatusId = model.CrmStatusId,
                                                                    CrmTypeId = model.CrmTypeId,
                                                                    CustomerSourceId = model.CustomerSourceId,
                                                                    Description = model.Description,
                                                                    Need = model.Need,
                                                                    ProductGroupId = model.ProductGroupId,
                                                                    UserIds = model.UserCareIds.ToList(),
                                                                    Visit = model.IsVisit
                                                            },
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

        public async Task<CrmListModel> GetByCustomerIdPaging(CustomerComponentSearchModel request)
        {
            try
            {
                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    var url = string.Format(ApiUrl.GET_CRM_CUSTOMER_BY_PAGING,
                                            request.CustomerId,
                                            start,
                                            request?.Length ?? 10);

                    var response = await HttpService.Send<PageResult<GetCrmPagingResponse>>(url,
                                                                                            null,
                                                                                            HttpMethod.Get,
                                                                                            true);

                    if(response.IsSuccess)
                        return new CrmListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CrmModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Code = c.Code,
                                                                                     ContactDate = c.ContactDate,
                                                                                     CustomerAddress = c.CustomerAddress,
                                                                                     CrmStatus = c.CrmStatus,
                                                                                     CrmPriority = c.CrmPriority,
                                                                                     CrmStatusColor = c.CrmStatusColor,
                                                                                     CrmType = c.CrmType,
                                                                                     CustomerGroup = c.CustomerGroup,
                                                                                     CustomerName = c.CustomerName,
                                                                                     CustomerPhone1 = c.CustomerPhone,
                                                                                     CustomerPhone2 = c.CustomerPhone2,
                                                                                     CustomerPhone3 = c.CustomerPhone3,
                                                                                     CustomerSource = c.CustomerSource,
                                                                                     CustomerVip = c.CustomerVip,
                                                                                     DateCreated = c.DateCreated,
                                                                                     Description = c.Description,
                                                                                     District = c.District,
                                                                                     Need = c.Need,
                                                                                     ProductGroup = c.ProductGroup,
                                                                                     Province = c.Province,
                                                                                     UserCreatedPhone = c.UserCreatedPhone,
                                                                                     Visit = c.Visit == 1
                                                                                                     ? "Đã đến"
                                                                                                     : "Chưa đến",
                                                                                     Ward = c.Ward,
                                                                                     UserCares = c.UserCares.Select(v => new UserCareModel
                                                                                                                         {
                                                                                                                                 Name = v.DisplayName,
                                                                                                                                 Phone = v.PhoneNumber
                                                                                                                         })
                                                                                                  .ToList(),
                                                                                     UserCreated = c.UserCreated,
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

        public async Task<UpdateCrmModel> GetById(int id)
        {
            try
            {
                var url = string.Format(ApiUrl.CRM_GET_BY_ID,
                                        id);
                var response = await HttpService.Send<GetCrmModelReponse>(url,
                                                                          null,
                                                                          HttpMethod.Get,
                                                                          true);
                if(response.IsSuccess)
                {
                    return new UpdateCrmModel
                           {
                                   Id = id,
                                   AvaiableCrmPriorities = new List<SelectListItem>(),
                                   AvaiableCrmStatuses = new List<SelectListItem>(),
                                   AvaiableCrmTypes = new List<SelectListItem>(),
                                   AvaiableCustomerSources = new List<SelectListItem>(),
                                   AvaiableProductGroups = new List<SelectListItem>(),
                                   AvaiableUserCares = new List<SelectListItem>(),
                                   AvaiableVisits = new List<SelectListItem>
                                                    {
                                                            new SelectListItem("Đã đến cửa hàng",
                                                                               "1"),
                                                            new SelectListItem("Chưa đến cửa hàng",
                                                                               "0")
                                                    },
                                   Code = response.Data.Code,
                                   ContactDate = response.Data.ContactDate,
                                   CrmPriorityId = response.Data.CrmPriorityId,
                                   CrmStatusId = response.Data.CrmStatusId,
                                   CrmTypeId = response.Data.CrmTypeId,
                                   Customer = new CustomerDetailModel(),
                                   CustomerId = response.Data.CustomerId,
                                   CustomerSourceId = response.Data.CustomerSourceId,
                                   DateCreated = response.Data.DateCreated,
                                   Description = response.Data.Description,
                                   IsVisit = response.Data.Visit,
                                   Need = response.Data.Need,
                                   ProductGroupId = response.Data.ProductGroupId,
                                   UserCareIds = response.Data.UserCares.Select(c => c.Id)
                                                         .ToList(),
                                   UserCreated = response.Data.UserCreated,
                                   UserCreatedPhone = response.Data.UserCreatedPhone
                           };
                }

                throw new PeloException(response.Message);
            }
            catch (Exception exception)
            {
                throw new PeloException(exception.Message);
            }
        }

        public async Task<IEnumerable<CrmLogResponse>> GetLogs(int id)
        {
            try
            {
                string url = string.Format(ApiUrl.CRM_GET_LOGS,
                                           id);
                var response = await HttpService.Send<IEnumerable<CrmLogResponse>>(url,
                                                                                   null,
                                                                                   HttpMethod.Get,
                                                                                   true);
                if(response.IsSuccess)
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

        public async Task<TResponse<bool>> Comment(CrmCommentModel model,
                                                   List<IFormFile> files)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var form = new MultipartFormDataContent())
                    {
                        foreach (var file in files)
                        {
                            var fileStream = file.OpenReadStream();
                            form.Add(CreateFileContent(fileStream,
                                                       file.FileName,
                                                       file.ContentType));
                        }

                        var paras = new List<KeyValuePair<string, string>>();
                        var para = new Tuple<int, string>(model.Id, model.Comment);
                        paras.Add(new KeyValuePair<string, string>("para", para.ToJson()));

                        form.Add(new FormUrlEncodedContent(paras));

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _contextHelper.GetToken());

                        var response = await client.PostAsync(ApiUrl.CRM_COMMENT,
                                                              form);
                        response.EnsureSuccessStatusCode();

                        var res = await response.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<TResponse<bool>>(res);
                        if(obj.IsSuccess)
                        {
                            return await Task.FromResult(new TResponse<bool>
                                                         {
                                                                 Data = obj.Data,
                                                                 IsSuccess = true,
                                                                 Message = string.Empty
                                                         });
                        }

                        return await Task.FromResult(new TResponse<bool>
                                                     {
                                                             Data = default,
                                                             IsSuccess = false,
                                                             Message = obj.Message
                                                     });
                    }
                }
            }
            catch (Exception exception)
            {
                return await Fail<bool>(exception);
            }
        }

        #endregion

        private StreamContent CreateFileContent(Stream stream,
                                                string fileName,
                                                string contentType)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                                                     {
                                                             Name = "\"files\"",
                                                             FileName = "\"" + fileName + "\""
                                                     }; // the extra quotes are key here
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            return fileContent;
        }

        public async Task<CrmListModel> XuLyCrm(BaseSearchModel request,
                                                string baseUrl)
        {
            try
            {
                var columnOrder = "DateCreated";
                var sortDir = "DESC";

                if(request != null)
                {
                    var start = request.Start / request.Length + 1;

                    string url = string.Format(baseUrl,
                                               start,
                                               request.Length);

                    var response = await HttpService.Send<PageResult<GetCrmPagingResponse>>(url,
                                                                                            null,
                                                                                            HttpMethod.Get,
                                                                                            true);

                    if(response.IsSuccess)
                        return new CrmListModel
                               {
                                       Draw = request.Draw,
                                       RecordsFiltered = response.Data.TotalCount,
                                       Total = response.Data.TotalCount,
                                       RecordsTotal = response.Data.TotalCount,
                                       Data = response.Data.Data.Select(c => new CrmModel
                                                                             {
                                                                                     Id = c.Id,
                                                                                     Code = c.Code,
                                                                                     ContactDate = c.ContactDate,
                                                                                     CrmPriority = c.CrmPriority,
                                                                                     CrmStatus = c.CrmStatus,
                                                                                     CrmStatusColor = c.CrmStatusColor,
                                                                                     CrmType = c.CrmType,
                                                                                     CustomerAddress = c.CustomerAddress,
                                                                                     CustomerGroup = c.CustomerGroup,
                                                                                     CustomerName = c.CustomerName,
                                                                                     CustomerPhone1 = c.CustomerPhone,
                                                                                     CustomerPhone2 = c.CustomerPhone2,
                                                                                     CustomerPhone3 = c.CustomerPhone3,
                                                                                     CustomerSource = c.CustomerSource,
                                                                                     CustomerVip = c.CustomerVip,
                                                                                     DateCreated = c.DateCreated,
                                                                                     District = c.District,
                                                                                     Province = c.Province,
                                                                                     Ward = c.Ward,
                                                                                     Need = c.Need,
                                                                                     ProductGroup = c.ProductGroup,
                                                                                     UserCreated = c.UserCreated,
                                                                                     UserCreatedPhone = c.UserCreatedPhone,
                                                                                     Visit = c.Visit == 1
                                                                                                     ? "Đã đến"
                                                                                                     : "Chưa đến",
                                                                                     UserCares = c.UserCares.Select(v => new UserCareModel
                                                                                                                         {
                                                                                                                                 Name = v.DisplayName,
                                                                                                                                 Phone = v.PhoneNumber
                                                                                                                         })
                                                                                                  .ToList(),
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
    }
}
