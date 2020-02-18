using System.ComponentModel.DataAnnotations;

namespace Pelo.v2.Web.Models.ReceiptDescription
{
    public class ReceiptDescriptionInsert
    {
        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        public string Name { get; set; }
    }
}
