using System;

namespace Pelo.v2.Web.Models.Crm
{
    public class CrmSearchModel : BaseSearchModel
    {
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
    }
}
