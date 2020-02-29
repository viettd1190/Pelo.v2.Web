using System.ComponentModel.DataAnnotations;

namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerFindByPhoneModel
    {
        [Required(ErrorMessage = "Số điện thoại khách hàng không được để trống")]
        public string PhoneNumber { get; set; }
    }
}
