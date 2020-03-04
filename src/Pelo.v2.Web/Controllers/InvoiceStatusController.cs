using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.Invoice;
using Pelo.Common.Dtos.InvoiceStatus;
using Pelo.v2.Web.Models.InvoiceStatus;
using Pelo.v2.Web.Services.InvoiceStatus;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class InvoiceStatusController : Controller
    {
        private readonly IInvoiceStatusService _invoiceStatusService;

        public InvoiceStatusController(IInvoiceStatusService invoiceStatusService)
        {
            _invoiceStatusService = invoiceStatusService;
        }

        public IActionResult Index()
        {
            return View(new InvoiceStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(InvoiceStatusSearchModel model)
        {
            var result = await _invoiceStatusService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invoiceStatusService.Delete(id);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new InvoiceStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(InvoiceStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _invoiceStatusService.Insert(new InsertInvoiceStatus{ Color = model.Color, IsSendSms = model.IsSendSms, Name = model.Name, SmsContent = model.SmsContent});
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
            var model = await _invoiceStatusService.GetById(id);
            if (model.IsSuccess)
            {
                return View(model.Data);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InvoiceStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _invoiceStatusService.Update(new UpdateInvoiceStatus{ Id = model.Id, Color = model.Color, IsSendSms = model.IsSendSms, Name = model.Name, SmsContent = model.SmsContent});
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
