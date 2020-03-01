using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Ward
{
    public class UpdateWardModel : BaseSearchModel
    {
        public UpdateWardModel()
        {
            AvaiableDistricts = new List<SelectListItem>();
            AvaiableProvinces = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public int DistrictId { get; set; }

        public int ProvinceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public int SortOrder { get; set; }

        public IList<SelectListItem> AvaiableDistricts { get; set; }

        public IList<SelectListItem> AvaiableProvinces { get; set; }
    }
}
