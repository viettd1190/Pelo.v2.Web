using System;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Product
{
    public class ProductModel
    {
        public ProductModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")] public int Id { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("ImportPrice")] public int ImportPrice { get; set; }

        [JsonProperty("SellPrice")] public int SellPrice { get; set; }

        [JsonProperty("WarrantyMonth")] public int WarrantyMonth { get; set; }

        [JsonProperty("ProductStatus")] public string ProductStatus { get; set; }

        [JsonProperty("ProductUnit")] public string ProductUnit { get; set; }

        [JsonProperty("ProductGroup")] public string ProductGroup { get; set; }

        [JsonProperty("Manufacturer")] public string Manufacturer { get; set; }

        [JsonProperty("Country")] public string Country { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("DateUpdated")] public DateTime DateUpdated { get; set; }

        [JsonProperty("PageSize")] public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")] public string PageSizeOptions { get; set; }
    }
}
