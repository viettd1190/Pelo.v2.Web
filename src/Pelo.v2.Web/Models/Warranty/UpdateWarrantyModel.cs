using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Models.Warranty
{
    public class UpdateWarrantyModel
    {
        public UpdateWarrantyModel()
        {
            AvaiableBranchs= new List<SelectListItem>();
            AvaiableProducts= new List<SelectListItem>();
            AvaiableUserCares = new List<SelectListItem>();
            Products = new List<ProductInWarrantyModel>();
            AvaiableWarrantyDescriptions = new List<SelectListItem>();
            Customer =new CustomerDetailModel();
            UserCareIds=new List<int>();
        }

        public int Id { get; set; }

        public CustomerDetailModel Customer { get; set; }

        public string Code { get; set; }

        public int BranchId { get; set; }

        public int CustomerId { get; set; }

        public IList<int> UserCareIds { get; set; }

        public int Total { get; set; }

        public int Deposit { get; set; }

        [UIHint("DateTime")]
        public DateTime DeliveryDate { get; set; }

        public string Description { get; set; }

        public IList<SelectListItem> AvaiableUserCares { get; set; }

        public IList<SelectListItem> AvaiableBranchs{ get; set; }

        public IList<SelectListItem> AvaiableProducts { get; set; }

        public IList<ProductInWarrantyModel> Products { get; set; }

        public IList<SelectListItem> AvaiableWarrantyDescriptions { get; set; }
    }
}
