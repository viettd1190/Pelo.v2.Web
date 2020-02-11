using System.ComponentModel.DataAnnotations;

namespace Pelo.v2.Web.Models.AppConfig
{
    public class AppConfigInsert
    {
        [Required(ErrorMessage = "Tên cấu hình không được để trống")]
        public string Name { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }
    }
}
