using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Crm
{
    public class CrmSearchModel : BaseSearchModel
    {
        public CrmSearchModel()
        {
            AvaiableCustomerGroups = new List<SelectListItem>();
            AvaiableCrmStatuses = new List<SelectListItem>();
            AvaiableCustomerSources = new List<SelectListItem>();
            AvaiableCustomerVips = new List<SelectListItem>();
            AvaiableProvinces = new List<SelectListItem>();
            AvaiableDistricts = new List<SelectListItem>();
            AvaiableWards = new List<SelectListItem>();
            AvaiableCrmTypes = new List<SelectListItem>();
            AvaiableCrmPriorities = new List<SelectListItem>();
            AvaiableUserCares = new List<SelectListItem>();
            AvaiableProductGroups = new List<SelectListItem>();
            AvaiableVisits = new List<SelectListItem>
                             {
                                     new SelectListItem("Tất cả",
                                                        "-1",
                                                        selected: true),
                                     new SelectListItem("Đã đến cửa hàng",
                                                        "1"),
                                     new SelectListItem("Chưa đến cửa hàng",
                                                        "0")
                             };
            AvaiableUserCreateds = new List<SelectListItem>();
        }

        public string CustomerCode { get; set; }

        public int CustomerGroupId { get; set; }

        public string CustomerName { get; set; }

        public int CustomerVipId { get; set; }

        public string CustomerPhone { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public string CustomerAddress { get; set; }

        public string Need { get; set; }

        public string CrmTypeId { get; set; }

        public int CrmStatusId { get; set; }

        public int CrmPriorityId { get; set; }

        public int CustomerSourceId { get; set; }

        public int Code { get; set; }

        public int UserCareId { get; set; }

        public int ProductGroupId { get; set; }

        public int IsVisit { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int UserCreatedId { get; set; }

        public DateTime? DateCreated { get; set; }

        public IList<SelectListItem> AvaiableCustomerGroups { get; set; }

        public IList<SelectListItem> AvaiableCustomerSources { get; set; }

        public IList<SelectListItem> AvaiableCustomerVips { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }

        public IList<SelectListItem> AvaiableWards { get; set; }

        public IList<SelectListItem> AvaiableCrmTypes { get; set; }

        public IList<SelectListItem> AvaiableCrmStatuses { get; set; }

        public IList<SelectListItem> AvaiableCrmPriorities { get; set; }

        public IList<SelectListItem> AvaiableUserCares { get; set; }

        public IList<SelectListItem> AvaiableProductGroups { get; set; }

        public IList<SelectListItem> AvaiableVisits { get; set; }

        public IList<SelectListItem> AvaiableUserCreateds { get; set; }
    }
}
