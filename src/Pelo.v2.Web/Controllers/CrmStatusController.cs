using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public IActionResult Add()
        {
            return View(new CrmStatusModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CrmStatusModel model)
        {
            var result = await _crmStatusService.Add(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _crmStatusService.GetById(id);
            if (result.IsSuccess)
            {
                return View(new CrmStatusModel
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    SmsContent = result.Data.SmsContent,
                    IsSendSms = result.Data.IsSendSms,
                    Color = result.Data.Color
                });
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CrmStatusModel model)
        {
            var result = await _crmStatusService.Edit(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
