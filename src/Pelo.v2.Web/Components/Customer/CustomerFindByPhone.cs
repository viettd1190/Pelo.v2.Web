using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Components.Customer
{
    public class CustomerFindByPhone : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new CustomerFindByPhoneModel());
        }
    }
}
