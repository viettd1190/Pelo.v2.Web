using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Branch
{
    public class BranchSearchModel : BaseSearchModel
    {
        public BranchSearchModel()
        {
            ProvinceId = 0;
            DistrictId = 0;
            WardId = 0;
            AvaiableProvinces = new List<SelectListItem>();
            AvaiableDistricts = new List<SelectListItem>();
            AvaiableWards = new List<SelectListItem>();
        }

        public string Name { get; set; }

        public string HotLine { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }

        public IList<SelectListItem> AvaiableWards { get; set; }
    }
}
