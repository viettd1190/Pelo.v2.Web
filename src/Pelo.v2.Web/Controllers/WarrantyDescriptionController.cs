using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.WarrantyDescription;
using Pelo.v2.Web.Models.WarrantyDescription;
using Pelo.v2.Web.Services.WarrantyDescription;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class WarrantyDescriptionController : BaseController
    {
        private readonly IWarrantyDescriptionService _descriptionService;

        public WarrantyDescriptionController(IWarrantyDescriptionService warrantyDescriptionService)
        {
            _descriptionService = warrantyDescriptionService;
        }

        public IActionResult Index()
        {
            return View(new WarrantyDescriptionSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(WarrantyDescriptionSearchModel model)
        {
            var result = await _descriptionService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new WarrantyDescriptionModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(WarrantyDescriptionModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _descriptionService.Add(new InsertWarrantyDescription { Name = model.Name});
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
            var model = await _descriptionService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new WarrantyDescriptionModel { Id = model.Data.Id, Name = model.Data.Name});
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WarrantyDescriptionModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _descriptionService.Update(new UpdateWarrantyDescription { Id = model.Id, Name = model.Name});
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
            var result = await _descriptionService.Delete(id);
            return Json(result);
        }
    }
}