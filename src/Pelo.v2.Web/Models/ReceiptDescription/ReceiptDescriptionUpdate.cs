using System.ComponentModel.DataAnnotations;

namespace Pelo.v2.Web.Models.ReceiptDescription
{
    public class ReceiptDescriptionUpdate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên dịch vụ không được để trống")]
        public string Name { get; set; }
    }
}
