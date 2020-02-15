using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.CustomerSource;
using Pelo.v2.Web.Services.CustomerSource;

namespace Pelo.v2.Web.Controllers
{
    public class CustomerSourceController : Controller
    {
        private readonly ICustomerSourceService _customerSourceService;

        public CustomerSourceController(ICustomerSourceService customerSourceService)
        {
            _customerSourceService = customerSourceService;
        }

        public IActionResult Index()
        {
            return View(new CustomerSourceSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CustomerSourceSearchModel model)
        {
            var result = await _customerSourceService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerSourceService.Delete(id);
            return Json(result);
        }
    }
}
