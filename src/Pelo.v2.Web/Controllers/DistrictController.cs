using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.District;
using Pelo.v2.Web.Services.District;

namespace Pelo.v2.Web.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        public IActionResult Index()
        {
            return View(new DistrictSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DistrictSearchModel model)
        {
            var result = await _districtService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _districtService.Delete(id);
            return Json(result);
        }
    }
}
