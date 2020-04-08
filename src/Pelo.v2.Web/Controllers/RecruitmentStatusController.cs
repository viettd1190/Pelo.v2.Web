using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.RecruitmentStatus;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Models.RecruitmentStatus;
using Pelo.v2.Web.Services.RecruitmentStatus;

namespace Pelo.v2.Web.Controllers
{
    public class RecruitmentStatusController : BaseController
    {
        private readonly IRecruitmentStatusService _recruimentStatus;

        public RecruitmentStatusController(IRecruitmentStatusService service)
        {
            _recruimentStatus = service;
        }

        public IActionResult Index()
        {
            return View(new RecruitmentStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(RecruitmentStatusSearchModel model)
        {
            var result = await _recruimentStatus.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _recruimentStatus.Delete(id);
            return Json(result);
        }

        public IActionResult Add()
        {
            return View(new RecruitmentStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(RecruitmentStatusModel model)
        {
            var result = await _recruimentStatus.Add(new InsertRecruitmentStatus { SortOrder = model.SortOrder, Color = model.Color, Name = model.Name });
            if (result.IsSuccess)
            {
                TempData["Update"] = result.ToJson();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _recruimentStatus.GetById(id);
            if (result.IsSuccess)
            {
                return View(new RecruitmentStatusModel
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    Color = result.Data.Color,
                    SortOrder = result.Data.SortOrder
                });
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RecruitmentStatusModel model)
        {
            var result = await _recruimentStatus.Edit(new UpdateRecruitmentStatus { Id = model.Id, SortOrder = model.SortOrder, Color = model.Color, Name = model.Name });
            if (result.IsSuccess)
            {
                TempData["Update"] = result.ToJson();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}