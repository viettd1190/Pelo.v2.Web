using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.District
{
    public class DistrictSearchModel : BaseSearchModel
    {
        public DistrictSearchModel()
        {
            AvaiableProvinces=new List<SelectListItem>();
        }

        public int ProvinceId { get; set; }

        public string DistrictName { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }
    }
}
