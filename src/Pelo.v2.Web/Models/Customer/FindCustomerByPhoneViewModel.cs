using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Customer
{
    public class FindCustomerByPhoneViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string NextAction { get; set; }
    }
}
