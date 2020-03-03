using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskStatus;
using Pelo.v2.Web.Services.TaskStatus;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class TaskStatusController : Controller
    {
        private readonly ITaskStatusService _taskStatusService;

        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        public IActionResult Index()
        {
            return View(new TaskStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(TaskStatusSearchModel model)
        {
            var result = await _taskStatusService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new TaskStatusModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(UpdateTaskStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskStatusService.Insert(model);
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
            var model = await _taskStatusService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new TaskStatusModel { Id = model.Data.Id, Name = model.Data.Name });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTaskStatusModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskStatusService.Update(model);
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
            var result = await _taskStatusService.Delete(id);
            return Json(result);
        }
    }
}
