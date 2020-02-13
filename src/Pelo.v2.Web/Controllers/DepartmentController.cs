using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Department;
using Pelo.v2.Web.Services.Department;

namespace Pelo.v2.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View(new DepartmentSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DepartmentSearchModel model)
        {
            var result = await _departmentService.GetByPaging(model);
            return Json(result);
        }
    }
}
