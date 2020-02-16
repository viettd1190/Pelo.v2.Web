using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskPriority;
using Pelo.v2.Web.Services.TaskPriority;

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskPriorityService.Delete(id);
            return Json(result);
        }
    }
}
