using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.Candidate;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Candidate;
using Pelo.v2.Web.Services.Candidate;

namespace Pelo.v2.Web.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateService _candidate;

        private readonly IBaseModelFactory _baseModelFactory;

        public CandidateController(ICandidateService service,
                                  IBaseModelFactory baseModelFactory)
        {
            _candidate = service;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var search = new CandidateSearchModel();
            await _baseModelFactory.PrepareCandidateStatus(search.AvaiableCandidateStatus);
            return View(search);
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
                var result = await _candidate.Insert(new InsertCandidate { Name = model.Name, Email = model.Email, Phone = model.Phone, CandidateStatusId = model.CandidateStatusId, Address = model.Address, Description = model.Description });
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
                return View(new UpdateCandidateModel { Id = model.Data.Id, Name = model.Data.Name, Email = model.Data.Email, Phone = model.Data.Phone, CandidateStatusId = model.Data.CandidateStatusId, Address = model.Data.Address, Description = model.Data.Description });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCandidateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _candidate.Update(new UpdateCandidate { Id = model.Id, Name = model.Name, Email = model.Email, Phone = model.Phone, CandidateStatusId = model.CandidateStatusId, Address = model.Address, Description = model.Description });
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

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _candidate.GetById(id);
            if (model != null)
            {
                CandidateDetailModel candidate = new CandidateDetailModel
                {
                    Id = model.Data.Id,
                    Code = model.Data.Code,
                    CandidateStatusId = model.Data.CandidateStatusId,
                    Address = model.Data.Address,
                    DateCreated = model.Data.DateCreated,
                    Description = model.Data.Description,
                    Email = model.Data.Email,
                    Name = model.Data.Name,
                    Phone = model.Data.Phone,
                    UserNameCreated = model.Data.UserNameCreated,
                    UserPhoneCreated = model.Data.UserPhoneCreated,
                };
                await _baseModelFactory.PrepareCandidateStatus(candidate.AvaiableCandidateStatuses);
                return View(candidate);
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> GetLogs(int id)
        {
            //var logs = await _candidate.GetLogs(id);
            //var result = new List<CrmLogDetail>();

            //foreach (var log in logs)
            //{
            //    var crmLog = new CrmLogDetail
            //    {
            //        Name = log.User?.Name ?? string.Empty,
            //        PhoneNumber = log.User?.PhoneNumber ?? string.Empty,
            //        Avatar = log.User?.Avatar ?? string.Empty,
            //        Content = log.Comment,
            //        LogDate = string.Format(AppUtil.DATE_TIME_FORMAT,
            //                                                 log.LogDate),
            //        AttachmentModels = new List<AttachmentModel>()
            //    };

            //    crmLog.AttachmentModels.AddRange(log.Attachments.Where(c => !string.IsNullOrEmpty(c.AttachmentName))
            //                                        .Select(c => new AttachmentModel
            //                                        {
            //                                            Name = c.AttachmentName,
            //                                            Url = $"http://103.77.167.96:20001/Attachments/{c.Attachment}",
            //                                            IsImage = CheckStringIsImageExtension(c.AttachmentName)
            //                                        })
            //                                        .OrderByDescending(c => CheckStringIsImageExtension(c.Name)));

            //    result.Add(crmLog);
            //}

            return Json("");
        }
    }
}