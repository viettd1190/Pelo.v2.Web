using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskLoop;
using Pelo.v2.Web.Services.TaskLoop;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class TaskLoopController : Controller
    {
        private readonly ITaskLoopService _taskLoopService;

        public TaskLoopController(ITaskLoopService taskLoopService)
        {
            _taskLoopService = taskLoopService;
        }

        public IActionResult Index()
        {
            return View(new TaskLoopSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(TaskLoopSearchModel model)
        {
            var result = await _taskLoopService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            return View(new TaskLoopModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(TaskLoopModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskLoopService.Insert(model);
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
            var response = await _taskLoopService.GetById(id);
            if (response.IsSuccess)
            {
                
                return View(new TaskLoopModel { Id = response.Data.Id, DayCount = response.Data.DayCount, Name = response.Data.Name, SortOrder = response.Data.SortOrder });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskLoopModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskLoopService.Update(model);
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
            var result = await _taskLoopService.Delete(id);
            return Json(result);
        }
    }
}
