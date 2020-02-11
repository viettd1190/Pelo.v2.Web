using System.ComponentModel.DataAnnotations;

namespace Pelo.v2.Web.Models.Province
{
    public class ProvinceInsert
    {
        [Required(ErrorMessage = "Tên cấu hình không được để trống")]
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public string Type { get; set; }
    }
}
