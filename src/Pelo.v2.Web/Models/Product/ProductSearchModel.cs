using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.Product
{
    public class ProductSearchModel : BaseSearchModel
    {
        public ProductSearchModel()
        {
            AvaiableProductGroups = new List<SelectListItem>();
            AvaiableProductUnits = new List<SelectListItem>();
            AvaiableProductStatuses = new List<SelectListItem>();
            AvaiableManufacturers = new List<SelectListItem>();
        }

        public string Name { get; set; }

        public int ProductGroupId { get; set; }

        public int ProductUnitId { get; set; }

        public int ProductStatusId { get; set; }

        public int ManufacturerId { get; set; }

        public int WarrantyMonth { get; set; }

        public string Description { get; set; }

        public IList<SelectListItem> AvaiableProductGroups { get; set; }

        public IList<SelectListItem> AvaiableProductUnits { get; set; }

        public IList<SelectListItem> AvaiableProductStatuses { get; set; }

        public IList<SelectListItem> AvaiableManufacturers { get; set; }
    }
}
