using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.CustomerGroup;
using Pelo.v2.Web.Services.CustomerGroup;

namespace Pelo.v2.Web.Controllers
{
    public class CustomerGroupController : Controller
    {
        private readonly ICustomerGroupService _customerGroupService;

        public CustomerGroupController(ICustomerGroupService customerGroupService)
        {
            _customerGroupService = customerGroupService;
        }

        public IActionResult Index()
        {
            return View(new CustomerGroupSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CustomerGroupSearchModel model)
        {
            var result = await _customerGroupService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerGroupService.Delete(id);
            return Json(result);
        }
    }
}
