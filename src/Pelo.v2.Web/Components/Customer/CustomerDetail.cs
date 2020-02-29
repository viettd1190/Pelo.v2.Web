using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Components.Customer
{
    public class CustomerDetail : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(CustomerDetailModel model)
        {
            return View(model);
        }
    }
}
