using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Warranty
{
    public class WarrantySearchModel : BaseSearchModel
    {
        public WarrantySearchModel()
        {
            AvaiableWarrantyStatus= new List<SelectListItem>();
            AvaiableUserCares = new List<SelectListItem>();
            AvaiableUserCreateds = new List<SelectListItem>();
        }

        public string CustomerName { get; set; }

        public string CustomerPhone { get; set; }

        public int WarrantyStatusId { get; set; }

        public int Code { get; set; }

        public int UserCareId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int UserCreatedId { get; set; }

        public DateTime? DateCreated { get; set; }

        public IList<SelectListItem> AvaiableUserCares { get; set; }

        public IList<SelectListItem> AvaiableWarrantyStatus { get; set; }

        public IList<SelectListItem> AvaiableUserCreateds { get; set; }

    }
}
