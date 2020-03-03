using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.Department;
using Pelo.v2.Web.Models.Department;
using Pelo.v2.Web.Services.Department;
using Pelo.Common.Extensions;

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

        public async Task<IActionResult> Add()
        {
            return View(new DepartmentModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.Insert(new InsertDepartment { Name = model.Name });
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
            var model = await _departmentService.GetById(id);
            if (model.IsSuccess)
            {
                return View(model);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _departmentService.Update(new UpdateDepartment { Id = model.Id, Name = model.Name });
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
        public async Task<IActionResult> GetList(DepartmentSearchModel model)
        {
            var result = await _departmentService.GetByPaging(model);
            return Json(result);
        }
    }
}
