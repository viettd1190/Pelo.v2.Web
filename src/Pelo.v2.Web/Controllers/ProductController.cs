using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableCountries);

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
        public async Task<IActionResult> Add()
        {
            var searchModel = new UpdateProductModel();

            await _baseModelFactory.PrepareManufacturers(searchModel.AvaiableManufacturers);
            await _baseModelFactory.PrepareProductGroups(searchModel.AvaiableProductGroups);
            await _baseModelFactory.PrepareProductUnits(searchModel.AvaiableProductUnits);
            await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableProductStatuses);
            await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableCountries);
            return View(searchModel);
        }
        [HttpPost]
        public async Task<IActionResult> Add(UpdateProductModel model)
        {
            var result = await _productService.Add(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            var searchModel = new UpdateProductModel();

            await _baseModelFactory.PrepareManufacturers(searchModel.AvaiableManufacturers);
            await _baseModelFactory.PrepareProductGroups(searchModel.AvaiableProductGroups);
            await _baseModelFactory.PrepareProductUnits(searchModel.AvaiableProductUnits);
            await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableProductStatuses);
            await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableCountries);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productService.GetById(id);
            if (result.IsSuccess)
            {
                var searchModel = new UpdateProductModel();

                await _baseModelFactory.PrepareManufacturers(searchModel.AvaiableManufacturers);
                await _baseModelFactory.PrepareProductGroups(searchModel.AvaiableProductGroups);
                await _baseModelFactory.PrepareProductUnits(searchModel.AvaiableProductUnits);
                await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableProductStatuses);
                await _baseModelFactory.PrepareProductStatuses(searchModel.AvaiableCountries);
                searchModel.Id = result.Data.Id;
                searchModel.Name = result.Data.Name;
                searchModel.WarrantyMonth = result.Data.WarrantyMonth;
                searchModel.SellPrice = result.Data.SellPrice;
                searchModel.ProductUnitId = result.Data.ProductUnitId;
                searchModel.ProductStatusId = result.Data.ProductStatusId;
                searchModel.ProductGroupId = result.Data.ProductGroupId;
                searchModel.ImportPrice = result.Data.ImportPrice;
                searchModel.CountryId = result.Data.CountryId;
                searchModel.ManufacturerId = result.Data.ManufacturerId;
                searchModel.Description = result.Data.Description;
                searchModel.MaxCount = result.Data.MaxCount;
                searchModel.MinCount = result.Data.MinCount;
                return View(searchModel);
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductModel model)
        {
            var result = await _productService.Edit(model);
            TempData["Update"] = JsonConvert.SerializeObject(result);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            await _baseModelFactory.PrepareManufacturers(model.AvaiableManufacturers);
            await _baseModelFactory.PrepareProductGroups(model.AvaiableProductGroups);
            await _baseModelFactory.PrepareProductUnits(model.AvaiableProductUnits);
            await _baseModelFactory.PrepareProductStatuses(model.AvaiableProductStatuses);
            await _baseModelFactory.PrepareProductStatuses(model.AvaiableCountries);
            return View(model);
        }
    }
}
