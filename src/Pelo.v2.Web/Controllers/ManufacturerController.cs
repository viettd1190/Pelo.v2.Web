using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Manufacturer;
using Pelo.v2.Web.Services.Manufacturer;
using Pelo.Common.Extensions;
using Pelo.Common.Dtos.Manufacturer;

namespace Pelo.v2.Web.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        public IActionResult Index()
        {
            return View(new ManufacturerSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ManufacturerSearchModel model)
        {
            var result = await _manufacturerService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _manufacturerService.Delete(id);
            return Json(result);
        }
        public IActionResult Add()
        {
            return View(new ManufacturerModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ManufacturerModel model)
        {
            var result = await _manufacturerService.Add(new InsertManufacturerRequest { Name = model.Name });
            if (result.IsSuccess)
            {
                TempData["Update"] = result.ToJson();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _manufacturerService.GetById(id);
            if (result.IsSuccess)
            {
                return View(result.Data);
            }
            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ManufacturerModel model)
        {
            var result = await _manufacturerService.Edit(new UpdateManufacturerRequest { Id = model.Id, Name = model.Name });
            TempData["Update"] = result.ToJson();
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
