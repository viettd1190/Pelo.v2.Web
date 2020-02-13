using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.ProductStatus;
using Pelo.v2.Web.Services.ProductStatus;

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
    }
}
