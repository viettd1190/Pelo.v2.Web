using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pelo.v2.Web.Models.District
{
    public class UpdateDistrictModel
    {
        public UpdateDistrictModel()
        {
            AvaiableProvinces = new List<SelectListItem>();
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ProvinceId")]
        public int ProvinceId { get; set; }

        [JsonProperty("SortOrder")]
        public int SortOrder { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }
    }
}
