using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pelo.v2.Web.Models.Branch
{
    public class UpdateBranchModel
    {
        public UpdateBranchModel()
        {
            AvaiableProvinces = new List<SelectListItem>();
            AvaiableDistricts = new List<SelectListItem>();
            AvaiableWards = new List<SelectListItem>();
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Province")]
        public int ProvinceId { get; set; }

        [JsonProperty("Hotline")]
        public string Hotline { get; set; }

        [JsonProperty("DistrictId")]
        public int DistrictId { get; set; }

        [JsonProperty("WardId")]
        public int WardId { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        public IList<SelectListItem> AvaiableProvinces{ get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }

        public IList<SelectListItem> AvaiableWards { get; set; }
    }
}
