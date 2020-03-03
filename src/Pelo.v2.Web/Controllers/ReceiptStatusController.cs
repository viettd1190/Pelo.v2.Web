using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.ReceiptStatus;
using Pelo.v2.Web.Models.ReceiptStatus;
using Pelo.v2.Web.Services.ReceiptStatus;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class ReceiptStatusController : Controller
    {
        private readonly IReceiptStatusService _receiptStatusService;

        public ReceiptStatusController(IReceiptStatusService receiptStatusService)
        {
            _receiptStatusService = receiptStatusService;
        }

        public IActionResult Index()
        {
            return View(new ReceiptStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ReceiptStatusSearchModel model)
        {
            var result = await _receiptStatusService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _receiptStatusService.Delete(id);
            return Json(result);
        }
        
        public async Task<IActionResult> Add()
        {
            return View(new ReceiptStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReceiptStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _receiptStatusService.Insert(new InsertReceiptStatus { Color = model.Color, IsSendSms = model.IsSendSms, Name = model.Name, SmsContent = model.SmsContent, SortOrder = model.SortOrder });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _receiptStatusService.GetById(id);
            if (model.IsSuccess)
            {
                return View(model);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReceiptStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _receiptStatusService.Update(new UpdateReceiptStatus { Id = model.Id, Color = model.Color, IsSendSms = model.IsSendSms, Name = model.Name, SmsContent = model.SmsContent, SortOrder = model.SortOrder });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
    }
}
