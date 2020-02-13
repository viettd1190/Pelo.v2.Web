using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Product;
using Pelo.v2.Web.Services.Product;
using Pelo.v2.Web.Services.Province;

namespace Pelo.v2.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IProductService _productService;

        public ProductController(IProductService productService,
                                  IBaseModelFactory baseModelFactory)
        {
            _productService = productService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new ProductSearchModel();

            await _baseModelFactory.PrepareManufacturers(searchModel.AvaiableManufacturers);
            await _baseModelFactory.PrepareProductGroups(searchModel.AvaiableProductGroups);
            await _baseModelFactory.PrepareProductUnits(searchModel.AvaiableProductUnits);
            await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableProductStatuses);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ProductSearchModel model)
        {
            var result = await _productService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.Delete(id);
            return Json(result);
        }
    }
}
