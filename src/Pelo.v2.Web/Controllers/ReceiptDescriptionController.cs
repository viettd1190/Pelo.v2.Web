using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Models.ReceiptDescription;
using Pelo.v2.Web.Services.ReceiptDescription;

namespace Pelo.v2.Web.Controllers
{
    public class ReceiptDescriptionController : Controller
    {
        private readonly IReceiptDescriptionService _receiptDescriptionService;

        public ReceiptDescriptionController(IReceiptDescriptionService receiptDescriptionService)
        {
            _receiptDescriptionService = receiptDescriptionService;
        }

        public IActionResult Index()
        {
            return View(new ReceiptDescriptionSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ReceiptDescriptionSearchModel model)
        {
            var result = await _receiptDescriptionService.GetByPaging(model);
            return Json(result);
        }

        public IActionResult Add()
        {
            return View(new ReceiptDescriptionInsert());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ReceiptDescriptionInsert model)
        {
            if(ModelState.IsValid)
            {
                var result = await _receiptDescriptionService.Insert(model);
                if(result.IsSuccess)
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
            var model = await _receiptDescriptionService.GetById(id);
            if(model.IsSuccess)
            {
                return View(model.Data);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReceiptDescriptionUpdate model)
        {
            if(ModelState.IsValid)
            {
                var result = await _receiptDescriptionService.Update(model);
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _receiptDescriptionService.Delete(id);
            return Json(result);
        }
    }
}
