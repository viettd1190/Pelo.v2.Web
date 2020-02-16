using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskType;
using Pelo.v2.Web.Services.TaskType;

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
    }
}
