using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Warranty
{
    public class WarrantyModel
    {
        public WarrantyModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")] public int Id { get; set; }

        [JsonProperty("WarrantyStatus")] public string WarrantyStatus { get; set; }

        [JsonProperty("WarrantyStatusColor")] public string WarrantyStatusColor { get; set; }

        [JsonProperty("Code")] public string Code { get; set; }

        [JsonProperty("CustomerName")] public string CustomerName { get; set; }

        [JsonProperty("CustomerPhone1")] public string CustomerPhone1 { get; set; }

        [JsonProperty("CustomerPhone2")] public string CustomerPhone2 { get; set; }

        [JsonProperty("CustomerPhone3")] public string CustomerPhone3 { get; set; }

        [JsonProperty("CustomerAddress")] public string CustomerAddress { get; set; }

        [JsonProperty("ProductsWarranties")] public List<ProductInWarrantySimpleList> Products { get; set; }

        [JsonProperty("Branch")] public string Branch { get; set; }

        [JsonProperty("UserCreated")] public string UserCreated { get; set; }

        [JsonProperty("UserCreatedPhone")] public string UserCreatedPhone { get; set; }

        [JsonProperty("PlanDate")] public string DeliveryDate { get; set; }

        [JsonProperty("PageSize")] public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")] public string PageSizeOptions { get; set; }

        public string DateCreated { get; set; }
    }

    public class UserCareModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }
    }
}
