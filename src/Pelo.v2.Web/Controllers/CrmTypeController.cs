using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult Add()
        {
            return View(new CrmTypeModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CrmTypeModel model)
        {
            var result = await _crmTypeService.Add(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _crmTypeService.GetById(id);
            if (result.IsSuccess)
            {
                return View(new CrmTypeModel
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                });
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CrmTypeModel model)
        {
            var result = await _crmTypeService.Edit(model);
            TempData["Update"] = JsonConvert.SerializeObject(result);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
