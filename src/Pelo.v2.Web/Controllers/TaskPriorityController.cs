using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskPriority;
using Pelo.v2.Web.Services.TaskPriority;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class TaskPriorityController : Controller
    {
        private readonly ITaskPriorityService _taskPriorityService;

        public TaskPriorityController(ITaskPriorityService taskPriorityService)
        {
            _taskPriorityService = taskPriorityService;
        }

        public IActionResult Index()
        {
            return View(new TaskPrioritySearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(TaskPrioritySearchModel model)
        {
            var result = await _taskPriorityService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new TaskPriorityModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskPriorityModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskPriorityService.Insert(model);
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
            var model = await _taskPriorityService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new TaskPriorityModel { Id = model.Data.Id, Name = model.Data.Name });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskPriorityModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskPriorityService.Update(model);
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
            var result = await _taskPriorityService.Delete(id);
            return Json(result);
        }
    }
}
