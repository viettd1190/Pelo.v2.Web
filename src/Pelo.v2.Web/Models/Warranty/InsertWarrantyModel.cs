using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Models.Warranty
{
    public class InsertWarrantyModel
    {
        public InsertWarrantyModel()
        {
            AvaiableBranches = new List<SelectListItem>();
            AvaiableProducts = new List<SelectListItem>();
            Products = new List<ProductInWarrantyModel>();
            Customer=new CustomerDetailModel();
            DeliveryDate = DateTime.Now;
            AvaiableWarrantyStatuses = new List<SelectListItem>();
            AvaiableWarrantyDescriptions = new List<SelectListItem>();
        }

        public CustomerDetailModel Customer { get; set; }

        public int Deposit { get; set; }

        public int Total { get; set; }

        public int CustomerId { get; set; }

        public int BranchId { get; set; }

        [UIHint("DateTime")]
        public DateTime DeliveryDate { get; set; }

        public string Description { get; set; }

        public IList<SelectListItem> AvaiableBranches { get; set; }

        public IList<SelectListItem> AvaiableProducts { get; set; }

        public IList<SelectListItem> AvaiableWarrantyStatuses { get; set; }

        public IList<SelectListItem> AvaiableWarrantyDescriptions { get; set; }

        public IList<ProductInWarrantyModel> Products { get; set; }

        public string ProductRaw { get; set; }
    }
    public class ProductInWarrantyModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("warranty_description_id")]
        public int WarrantyDescriptionId { get; set; }

        [JsonProperty("serialnumber")]
        public string SertialNumber { get; set; }

    }
}
