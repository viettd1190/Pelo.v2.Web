using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.CandidateStatus;
using Pelo.v2.Web.Models.CandidateStatus;
using Pelo.v2.Web.Services.CandidateStatus;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class CandidateStatusController : Controller
    {
        private readonly ICandidateStatusService _candidateStatus;

        public CandidateStatusController(ICandidateStatusService taskStatusService)
        {
            _candidateStatus = taskStatusService;
        }

        public IActionResult Index()
        {
            return View(new CandidateStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CandidateStatusSearchModel model)
        {
            var result = await _candidateStatus.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new CandidateStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CandidateStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _candidateStatus.Insert(new InsertCandidateStatus { Color = model.Color, IsSendSms = model.IsSendSms, Name = model.Name, SmsContent = model.SmsContent, SortOrder = model.SortOrder });
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
            var model = await _candidateStatus.GetById(id);
            if (model.IsSuccess)
            {
                return View(new CandidateStatusModel { Id = model.Data.Id, Name = model.Data.Name });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CandidateStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _candidateStatus.Update(new UpdateCandidateStatus { Id = model.Id, Color = model.Color, IsSendSms = model.IsSendSms, Name = model.Name, SmsContent = model.SmsContent, SortOrder = model.SortOrder });
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
            var result = await _candidateStatus.Delete(id);
            return Json(result);
        }
    }
}