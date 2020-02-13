using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.User;
using Pelo.v2.Web.Services.User;

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
                                                   new SelectListItem("Tất cả tình trạng",
                                                                      "-1",
                                                                      true),
                                                   new SelectListItem("Vẫn còn làm",
                                                                      "1"),
                                                   new SelectListItem("Đã nghỉ việc",
                                                                      "0")
                                           };

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(UserSearchModel model)
        {
            var result = await _userService.GetByPaging(model);
            return Json(result);
        }
    }
}
