using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.ProductUnit;
using Pelo.v2.Web.Services.ProductUnit;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class ProductUnitController : Controller
    {
        private readonly IProductUnitService _productUnitService;

        public ProductUnitController(IProductUnitService productUnitService)
        {
            _productUnitService = productUnitService;
        }

        public IActionResult Index()
        {
            return View(new ProductUnitSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ProductUnitSearchModel model)
        {
            var result = await _productUnitService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productUnitService.Delete(id);
            return Json(result);
        }
        public async Task<IActionResult> Add()
        {
            return View(new ProductUnitModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductUnitModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productUnitService.Insert(model);
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
            var model = await _productUnitService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new ProductUnitModel { Id = model.Data.Id, Name = model.Data.Name });
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductUnitModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productUnitService.Update(model);
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
    }
}
