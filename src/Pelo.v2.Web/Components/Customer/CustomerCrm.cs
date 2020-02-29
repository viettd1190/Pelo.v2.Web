using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Components.Customer
{
    public class CustomerCrm : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            return View(new CustomerComponentSearchModel()
                        {
                                CustomerId = customerId
                        });
        }
    }
}
