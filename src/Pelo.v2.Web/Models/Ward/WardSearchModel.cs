using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Ward
{
    public class WardSearchModel : BaseSearchModel
    {
        public WardSearchModel()
        {
            ProvinceId = 0;
            AvaiableProvinces = new List<SelectListItem>();
            AvaiableDistricts = new List<SelectListItem>();
        }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public string WardName { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }
    }
}
