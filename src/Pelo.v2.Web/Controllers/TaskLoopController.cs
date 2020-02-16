using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.TaskLoop;
using Pelo.v2.Web.Services.TaskLoop;

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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskLoopService.Delete(id);
            return Json(result);
        }
    }
}
