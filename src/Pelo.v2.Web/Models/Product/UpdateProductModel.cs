using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Product
{
    public class UpdateProductModel
    {
        public UpdateProductModel()
        {
            AvaiableProductGroups = new List<SelectListItem>();
            AvaiableProductUnits = new List<SelectListItem>();
            AvaiableProductStatuses = new List<SelectListItem>();
            AvaiableManufacturers = new List<SelectListItem>();
            AvaiableCountries = new List<SelectListItem>();
        }

        [JsonProperty("Id")] public int Id { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("ImportPrice")] public int ImportPrice { get; set; }

        [JsonProperty("SellPrice")] public int SellPrice { get; set; }

        [JsonProperty("WarrantyMonth")] public int WarrantyMonth { get; set; }

        [JsonProperty("CountryId")] public int CountryId { get; set; }

        [JsonProperty("ProductStatusId")] public int ProductStatusId { get; set; }

        [JsonProperty("ProductUnitId")] public int ProductUnitId { get; set; }

        [JsonProperty("ProductGroupId")] public int ProductGroupId { get; set; }

        [JsonProperty("ManufacturerId")] public int ManufacturerId { get; set; }

        [JsonProperty("MaxCount")] public int MaxCount { get; set; }

        [JsonProperty("MinCount")] public int MinCount { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        public IList<SelectListItem> AvaiableProductGroups { get; set; }

        public IList<SelectListItem> AvaiableProductUnits { get; set; }

        public IList<SelectListItem> AvaiableProductStatuses { get; set; }

        public IList<SelectListItem> AvaiableManufacturers { get; set; }

        public IList<SelectListItem> AvaiableCountries { get; set; }
    }
}
