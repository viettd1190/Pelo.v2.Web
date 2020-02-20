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
        public IActionResult Add()
        {
            return View(new CrmPrioritySearchModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CrmPriorityModel model)
        {
            var result = await _crmPriorityService.Add(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _crmPriorityService.GetById(id);
            if (result.IsSuccess)
            {
                return View(new CrmPriorityModel
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    Color = result.Data.Color
                });
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CrmPriorityModel model)
        {
            var result = await _crmPriorityService.Edit(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
