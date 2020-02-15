using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.CrmType;
using Pelo.v2.Web.Services.CrmType;

namespace Pelo.v2.Web.Controllers
{
    public class CrmTypeController : Controller
    {
        private readonly ICrmTypeService _crmTypeService;

        public CrmTypeController(ICrmTypeService crmTypeService)
        {
            _crmTypeService = crmTypeService;
        }

        public IActionResult Index()
        {
            return View(new CrmTypeSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CrmTypeSearchModel model)
        {
            var result = await _crmTypeService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crmTypeService.Delete(id);
            return Json(result);
        }
    }
}
