using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskStatus;
using Pelo.v2.Web.Services.TaskStatus;

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskStatusService.Delete(id);
            return Json(result);
        }
    }
}
