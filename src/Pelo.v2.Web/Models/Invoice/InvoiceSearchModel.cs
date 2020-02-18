using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Invoice
{
    public class InvoiceSearchModel : BaseSearchModel
    {
        public InvoiceSearchModel()
        {
            AvaiableBranches=new List<SelectListItem>();
            AvaiableInvoiceStatuses=new List<SelectListItem>();
            AvaiableUserSells=new List<SelectListItem>();
            AvaiableUserDeliveries=new List<SelectListItem>();
            AvaiableUserCreateds=new List<SelectListItem>();
        }

        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public string Code { get; set; }

        public int BranchId { get; set; }

        public int InvoiceStatusId { get; set; }

        public int UserSellId { get; set; }

        public int UserDeliveryId { get; set; }

        public int UserCreatedId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public IList<SelectListItem> AvaiableBranches { get; set; }

        public IList<SelectListItem> AvaiableInvoiceStatuses { get; set; }

        public IList<SelectListItem> AvaiableUserSells { get; set; }

        public IList<SelectListItem> AvaiableUserDeliveries { get; set; }

        public IList<SelectListItem> AvaiableUserCreateds { get; set; }
    }
}
