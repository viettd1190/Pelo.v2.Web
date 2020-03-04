using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.TaskType;
using Pelo.v2.Web.Models.TaskType;
using Pelo.v2.Web.Services.TaskType;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class TaskTypeController : Controller
    {
        private readonly ITaskTypeService _taskTypeService;

        public TaskTypeController(ITaskTypeService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        public IActionResult Index()
        {
            return View(new TaskTypeSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(TaskTypeSearchModel model)
        {
            var result = await _taskTypeService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskTypeService.Delete(id);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new TaskTypeModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskTypeModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskTypeService.Insert(new InsertTaskType { Name = model.Name, SortOrder = model.SortOrder });
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
            var model = await _taskTypeService.GetById(id);
            if (model.IsSuccess)
            {
                return View(model.Data);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskTypeModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskTypeService.Update(new UpdateTaskType { Id = model.Id, Name = model.Name, SortOrder = model.SortOrder });
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
