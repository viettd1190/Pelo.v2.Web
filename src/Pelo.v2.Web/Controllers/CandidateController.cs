using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.Candidate;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Models.Candidate;
using Pelo.v2.Web.Services.Candidate;

namespace Pelo.v2.Web.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidate;

        public CandidateController(ICandidateService service)
        {
            _candidate = service;
        }

        public IActionResult Index()
        {
            return View(new CandidateSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CandidateSearchModel model)
        {
            var result = await _candidate.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new Models.Candidate.UpdateCandidateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Candidate.UpdateCandidateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _candidate.Insert(new InsertCandidate {Name = model.Name, Email = model.Email, Phone = model.Phone, CandidateStatusId = model.CandidateStatusId });
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
            var model = await _candidate.GetById(id);
            if (model.IsSuccess)
            {
                return View(new UpdateCandidateModel { Id = model.Data.Id, Name = model.Data.Name, Email = model.Data.Email, Phone = model.Data.Phone, CandidateStatusId = model.Data.CandidateStatusId });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCandidateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _candidate.Update(new UpdateCandidate { Id = model.Id, Name = model.Name, Email = model.Email, Phone = model.Phone, CandidateStatusId = model.CandidateStatusId });
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
            var result = await _candidate.Delete(id);
            return Json(result);
        }
    }
}