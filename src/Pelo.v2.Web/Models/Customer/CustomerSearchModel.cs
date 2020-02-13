using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerSearchModel : BaseSearchModel
    {
        public CustomerSearchModel()
        {
            ProvinceId = 0;
            DistrictId = 0;
            WardId = 0;
            CustomerGroupId = 0;
            CustomerVipId = 0;
            AvaiableCustomerGroups = new List<SelectListItem>();
            AvaiableCustomerVips = new List<SelectListItem>();
            AvaiableDistricts = new List<SelectListItem>();
            AvaiableProvinces = new List<SelectListItem>();
            AvaiableWards = new List<SelectListItem>();
        }

        public string Code { get; set; } = "";

        public string Name { get; set; } = "";

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public string Address { get; set; } = "";

        public string Phone { get; set; } = "";

        public string Email { get; set; } = "";

        public int CustomerGroupId { get; set; }

        public int CustomerVipId { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }

        public IList<SelectListItem> AvaiableWards { get; set; }

        public IList<SelectListItem> AvaiableCustomerGroups { get; set; }

        public IList<SelectListItem> AvaiableCustomerVips { get; set; }
    }
}
