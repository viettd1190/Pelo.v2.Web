using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerUpdateModel
    {
        public CustomerUpdateModel()
        {
            AvaiableProvinces = new List<SelectListItem>();
            AvaiableDistricts = new List<SelectListItem>();
            AvaiableWards = new List<SelectListItem>();
            AvaiableCustomerGroups = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Số điện thoại khách hàng không được để trống")]
        public string Phone { get; set; }

        public string Phone2 { get; set; }

        public string Phone3 { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public int CustomerGroupId { get; set; }

        public string Description { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }

        public IList<SelectListItem> AvaiableWards { get; set; }

        public IList<SelectListItem> AvaiableCustomerGroups { get; set; }

        public string Code { get; set; }

        public int Profit { get; set; }

        public int ProfitUpdate { get; set; }
    }
}
