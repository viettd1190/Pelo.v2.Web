using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Components.Customer
{
    public class CustomerInvoice : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            return View(new CustomerInvoiceSearchModel
                        {
                                CustomerId = customerId
                        });
        }
    }
}
