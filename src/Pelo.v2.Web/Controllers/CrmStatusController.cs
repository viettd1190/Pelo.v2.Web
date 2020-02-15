using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.CrmStatus;
using Pelo.v2.Web.Services.CrmStatus;

namespace Pelo.v2.Web.Controllers
{
    public class CrmStatusController : Controller
    {
        private readonly ICrmStatusService _crmStatusService;

        public CrmStatusController(ICrmStatusService crmStatusService)
        {
            _crmStatusService = crmStatusService;
        }

        public IActionResult Index()
        {
            return View(new CrmStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CrmStatusSearchModel model)
        {
            var result = await _crmStatusService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _crmStatusService.Delete(id);
            return Json(result);
        }
    }
}
