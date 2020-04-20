using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.WarrantyStatus;
using Pelo.v2.Web.Models.WarrantyStatus;
using Pelo.v2.Web.Services.WarrantyStatus;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class WarrantyStatusController : BaseController
    {
        private readonly IWarrantyStatusService _warrantyStatusService;

        public WarrantyStatusController(IWarrantyStatusService warrantyStatusService)
        {
            _warrantyStatusService = warrantyStatusService;
        }

        public IActionResult Index()
        {
            return View(new WarrantyStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(WarrantyStatusSearchModel model)
        {
            var result = await _warrantyStatusService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new UpdateWarrantyStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(UpdateWarrantyStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _warrantyStatusService.Add(new InsertWarrantyStatus { Color = model.Color, Name = model.Name, SortOrder = model.SortOrder, IsSendSms = model.IsSendSms, SmsContent = model.SmsContent });
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
            var model = await _warrantyStatusService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new UpdateWarrantyStatusModel { Id = model.Data.Id, Name = model.Data.Name, SortOrder = model.Data.SortOrder, Color = model.Data.Color,IsSendSms=model.Data.IsSendSms,SmsContent=model.Data.SmsContent });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateWarrantyStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _warrantyStatusService.Update(new UpdateWarrantyStatus { Id = model.Id, Color = model.Color, Name = model.Name, SortOrder = model.SortOrder, IsSendSms = model.IsSendSms, SmsContent = model.SmsContent });
                if (result.IsSuccess)
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
            var result = await _warrantyStatusService.Delete(id);
            return Json(result);
        }
    }
}