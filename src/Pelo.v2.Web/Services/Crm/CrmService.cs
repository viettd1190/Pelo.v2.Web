﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pelo.Common.Dtos.Crm;
using Pelo.Common.Exceptions;
using Pelo.Common.Models;
using Pelo.v2.Web.Commons;
using Pelo.v2.Web.Models;
using Pelo.v2.Web.Models.Crm;
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
    }

    public class CrmService : BaseService,
                              ICrmService
    {
        public CrmService(IHttpService httpService,
                          ILogger<BaseService> logger) : base(httpService,
                                                              logger)
        {
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

        #endregion

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