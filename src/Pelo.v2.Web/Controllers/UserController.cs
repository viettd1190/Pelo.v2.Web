using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.Common.Dtos.User;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.User;
using Pelo.v2.Web.Services.User;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IUserService _userService;

        public UserController(IUserService userService,
                              IBaseModelFactory baseModelFactory)
        {
            _userService = userService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new UserSearchModel();

            await _baseModelFactory.PrepareBranches(searchModel.AvaiableBranches);
            await _baseModelFactory.PrepareRoles(searchModel.AvaiableRoles);
            await _baseModelFactory.PrepareDepartments(searchModel.AvaiableDepartments);
            searchModel.AvaiableStatuses = new List<SelectListItem>
                                           {
                                                   new SelectListItem("Đã nghỉ việc",
                                                                      "0"),
                                                   new SelectListItem("Tất cả tình trạng",
                                                                      "-1"),
                                                   new SelectListItem("Vẫn còn làm",
                                                                      "1",true)
                                           };

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(UserSearchModel model)
        {
            var result = await _userService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            var model = new UpdateUserModel();
            await _baseModelFactory.PrepareBranches(model.AvaiableBranches);
            await _baseModelFactory.PrepareRoles(model.AvaiableRoles);
            await _baseModelFactory.PrepareDepartments(model.AvaiableDepartments);
            model.AvaiableStatuses = new List<SelectListItem>
                                           {
                                                   new SelectListItem("Đã nghỉ việc",
                                                                      "0"),
                                                   new SelectListItem("Tất cả tình trạng",
                                                                      "-1"),
                                                   new SelectListItem("Vẫn còn làm",
                                                                      "1",true)
                                           };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UpdateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Insert(new InsertUserRequest { IsActive = model.IsActive, BranchId = model.BranchId, DepartmentId = model.DepartmentId, Description = model.Description, DisplayName = model.DisplayName, Email = model.Email, FullName = model.FullName, Password = model.Password, PhoneNumber = model.PhoneNumber, RoleId = model.RoleId, Username = model.Username });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            await _baseModelFactory.PrepareBranches(model.AvaiableBranches);
            await _baseModelFactory.PrepareRoles(model.AvaiableRoles);
            await _baseModelFactory.PrepareDepartments(model.AvaiableDepartments);
            model.AvaiableStatuses = new List<SelectListItem>
                                           {
                                                   new SelectListItem("Đã nghỉ việc",
                                                                      "0"),
                                                   new SelectListItem("Tất cả tình trạng",
                                                                      "-1"),
                                                   new SelectListItem("Vẫn còn làm",
                                                                      "1",true)
                                           };
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _userService.GetById(id);
            if (result.IsSuccess)
            {
                var model = new UpdateUserModel();
                await _baseModelFactory.PrepareBranches(model.AvaiableBranches);
                await _baseModelFactory.PrepareRoles(model.AvaiableRoles);
                await _baseModelFactory.PrepareDepartments(model.AvaiableDepartments);
                model.AvaiableStatuses = new List<SelectListItem>
                                           {
                                                   new SelectListItem("Đã nghỉ việc",
                                                                      "0"),
                                                   new SelectListItem("Tất cả tình trạng",
                                                                      "-1"),
                                                   new SelectListItem("Vẫn còn làm",
                                                                      "1",true)
                                           };
                model.Id = result.Data.Id;
                model.IsActive = result.Data.IsActive; model.BranchId = result.Data.BranchId; model.DepartmentId = result.Data.DepartmentId; model.Description = result.Data.Description; model.DisplayName = result.Data.DisplayName; model.Email = result.Data.Email; model.FullName = result.Data.FullName; model.PhoneNumber = result.Data.PhoneNumber; model.RoleId = result.Data.RoleId;
                return View(model);
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Update(new UpdateUserRequest { Id = model.Id, IsActive = model.IsActive, BranchId = model.BranchId, DepartmentId = model.DepartmentId, Description = model.Description, DisplayName = model.DisplayName, Email = model.Email, FullName = model.FullName, PhoneNumber = model.PhoneNumber, RoleId = model.RoleId });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            await _baseModelFactory.PrepareBranches(model.AvaiableBranches);
            await _baseModelFactory.PrepareRoles(model.AvaiableRoles);
            await _baseModelFactory.PrepareDepartments(model.AvaiableDepartments);
            model.AvaiableStatuses = new List<SelectListItem>
                                           {
                                                   new SelectListItem("Đã nghỉ việc",
                                                                      "0"),
                                                   new SelectListItem("Tất cả tình trạng",
                                                                      "-1"),
                                                   new SelectListItem("Vẫn còn làm",
                                                                      "1",true)
                                           };
            return View(model);
        }
    }
}
