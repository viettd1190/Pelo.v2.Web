using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.ProductGroup;
using Pelo.v2.Web.Models.ProductGroup;
using Pelo.v2.Web.Services.ProductGroup;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }

        public IActionResult Index()
        {
            return View(new ProductGroupSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ProductGroupSearchModel model)
        {
            var result = await _productGroupService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productGroupService.Delete(id);
            return Json(result);
        }

        public IActionResult Add()
        {
            return View(new ProductGroupModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductGroupModel model)
        {
            var result = await _productGroupService.Add(new InsertProductGroup { Name = model.Name });
            if (result.IsSuccess)
            {
                TempData["Update"] = result.ToJson();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productGroupService.GetById(id);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductGroupModel model)
        {
            var result = await _productGroupService.Edit(new UpdateProductGroup { Id = model.Id, Name = model.Name });
            TempData["Update"] = result.ToJson();
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
