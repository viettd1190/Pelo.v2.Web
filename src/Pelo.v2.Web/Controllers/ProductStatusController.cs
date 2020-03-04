using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.ProductStatus;
using Pelo.v2.Web.Models.ProductStatus;
using Pelo.v2.Web.Services.ProductStatus;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class ProductStatusController : Controller
    {
        private readonly IProductStatusService _productStatusService;

        public ProductStatusController(IProductStatusService productStatusService)
        {
            _productStatusService = productStatusService;
        }

        public IActionResult Index()
        {
            return View(new ProductStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ProductStatusSearchModel model)
        {
            var result = await _productStatusService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productStatusService.Delete(id);
            return Json(result);
        }

        public IActionResult Add()
        {
            return View(new ProductStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductStatusModel model)
        {
            var result = await _productStatusService.Add(new InsertProductStatus { Name = model.Name });
            if (result.IsSuccess)
            {
                TempData["Update"] = result.ToJson();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productStatusService.GetById(id);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductStatusModel model)
        {
            var result = await _productStatusService.Edit(new UpdateProductStatus { Id = model.Id, Name = model.Name });
            TempData["Update"] = result.ToJson();
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
