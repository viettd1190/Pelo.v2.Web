using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.CrmPriority;
using Pelo.v2.Web.Services.CrmPriority;

namespace Pelo.v2.Web.Controllers
{
    public class CrmPriorityController : Controller
    {
        private readonly ICrmPriorityService _crmPriorityService;

        public CrmPriorityController(ICrmPriorityService crmPriorityService)
        {
            _crmPriorityService = crmPriorityService;
        }

        public IActionResult Index()
        {
            return View(new CrmPrioritySearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CrmPrioritySearchModel model)
        {
            var result = await _crmPriorityService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crmPriorityService.Delete(id);
            return Json(result);
        }
    }
}
