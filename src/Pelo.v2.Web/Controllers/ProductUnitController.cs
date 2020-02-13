using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.ProductUnit;
using Pelo.v2.Web.Services.ProductUnit;

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
    }
}
