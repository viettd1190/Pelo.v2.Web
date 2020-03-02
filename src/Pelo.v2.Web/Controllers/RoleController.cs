using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Role;
using Pelo.v2.Web.Services.Role;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            return View(new RoleSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(RoleSearchModel model)
        {
            var result = await _roleService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.Insert(model);
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
            var model = await _roleService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new RoleModel { Id = model.Data.Id, Name = model.Data.Name});
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.Update(model);
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
            var result = await _roleService.Delete(id);
            return Json(result);
        }
    }
}
