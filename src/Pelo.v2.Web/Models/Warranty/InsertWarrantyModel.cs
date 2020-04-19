using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Models.Warranty
{
    public class InsertWarrantyModel
    {
        public InsertWarrantyModel()
        {
            AvaiableCustomerSources = new List<SelectListItem>();
            AvaiableCrmTypes = new List<SelectListItem>();
            AvaiableCrmPriorities = new List<SelectListItem>();
            AvaiableUserCares = new List<SelectListItem>();
            AvaiableProductGroups = new List<SelectListItem>();
            AvaiableCrmStatuses=new List<SelectListItem>();
            AvaiableVisits = new List<SelectListItem>
                             {
                                     new SelectListItem("Đã đến cửa hàng",
                                                        "1"),
                                     new SelectListItem("Chưa đến cửa hàng",
                                                        "0")
                             };
            Customer=new CustomerDetailModel();
            UserCareIds=new List<int>();
        }

        public CustomerDetailModel Customer { get; set; }

        public string Need { get; set; }

        public int CrmTypeId { get; set; }

        public int CrmStatusId { get; set; }

        public int CrmPriorityId { get; set; }

        public int CustomerSourceId { get; set; }

        public int CustomerId { get; set; }

        public IList<int> UserCareIds { get; set; }

        public int ProductGroupId { get; set; }

        public int IsVisit { get; set; }

        [UIHint("DateTime")]
        public DateTime ContactDate { get; set; }

        public string Description { get; set; }

        public IList<SelectListItem> AvaiableCustomerSources { get; set; }

        public IList<SelectListItem> AvaiableCrmTypes { get; set; }

        public IList<SelectListItem> AvaiableCrmPriorities { get; set; }

        public IList<SelectListItem> AvaiableUserCares { get; set; }

        public IList<SelectListItem> AvaiableProductGroups { get; set; }

        public IList<SelectListItem> AvaiableVisits { get; set; }

        public IList<SelectListItem> AvaiableCrmStatuses { get; set; }
    }
}
