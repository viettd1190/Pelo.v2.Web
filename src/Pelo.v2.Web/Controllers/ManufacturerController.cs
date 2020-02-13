using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Models.Manufacturer;
using Pelo.v2.Web.Services.Manufacturer;

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
    }
}
