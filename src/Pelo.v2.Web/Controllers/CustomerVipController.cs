using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.CustomerVip;
using Pelo.v2.Web.Services.CustomerVip;

namespace Pelo.v2.Web.Controllers
{
    public class CustomerVipController : Controller
    {
        private readonly ICustomerVipService _customerVipService;

        public CustomerVipController(ICustomerVipService customerVipService)
        {
            _customerVipService = customerVipService;
        }

        public IActionResult Index()
        {
            return View(new CustomerVipSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CustomerVipSearchModel model)
        {
            var result = await _customerVipService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerVipService.Delete(id);
            return Json(result);
        }
    }
}
