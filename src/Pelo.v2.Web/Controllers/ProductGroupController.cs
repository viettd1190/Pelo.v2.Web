using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.ProductGroup;
using Pelo.v2.Web.Services.ProductGroup;

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
    }
}
