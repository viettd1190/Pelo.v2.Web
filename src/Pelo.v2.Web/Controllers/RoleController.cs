using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.Role;
using Pelo.v2.Web.Services.Role;

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
    }
}
