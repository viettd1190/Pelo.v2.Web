using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.CustomerVip;
using Pelo.v2.Web.Models.CustomerVip;
using Pelo.v2.Web.Services.CustomerVip;
using Pelo.Common.Extensions;

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
        public async Task<IActionResult> Add()
        {
            return View(new CustomerVipModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerVipModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerVipService.Insert(new InsertCustomerVipRequest { Name = model.Name, Profit = model.Profit, Color = model.Color, SortOder = model.SortOrder });
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
            var model = await _customerVipService.GetById(id);
            if (model.IsSuccess)
            {
                return View(model.Data);
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerVipModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerVipService.Update(new UpdateCustomerVipRequest { Id = model.Id, Name = model.Name, Profit = model.Profit, Color = model.Color });
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
            var result = await _customerVipService.Delete(id);
            return Json(result);
        }
    }
}
