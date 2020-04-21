using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Warranty
{
    public class WarrantyDetailModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int WarrantyStatusId { get; set; }

        public int CustomerId { get; set; }

        public CustomerDetailModel Customer { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserCreated { get; set; }

        public string UserCreatedPhone { get; set; }

        public DateTime DeliveryDate { get; set; }

        public List<int> UsersInChargeIds { get; set; }

        public List<int> UserCareIds { get; set; }

        public int Deposit { get; set; }

        public int Total { get; set; }

        public List<ProductInWarrantyDetailModel> Products { get; set; } = new List<ProductInWarrantyDetailModel>();

        public string Description { get; set; }

        public IList<SelectListItem> AvaiableWarrantyStatuses { get; set; } = new List<SelectListItem>();

        public int WarrantyStatusDeleted { get; set; }
    }
    public class ProductInWarrantyDetailModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SerialNumber { get; set; }

        public string WarrantyDescription { get; set; }
    }
}
