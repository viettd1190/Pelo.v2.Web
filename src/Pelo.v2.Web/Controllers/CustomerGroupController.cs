using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.CustomerGroup;
using Pelo.v2.Web.Models.CustomerGroup;
using Pelo.v2.Web.Services.CustomerGroup;
using Pelo.Common.Extensions;

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
        
        public async Task<IActionResult> Add()
        {
            return View(new CustomerGroupModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerGroupModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerGroupService.Insert(new InsertCustomerGroupRequest { Name = model.Name });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _customerGroupService.GetById(id);
            if (model.IsSuccess)
            {
                return View(model.Data);
            }

            return View("Notfound");
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerGroupModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerGroupService.Update(new UpdateCustomerGroupRequest { Id = model.Id, Name = model.Name });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerGroupService.Delete(id);
            return Json(result);
        }
    }
}
