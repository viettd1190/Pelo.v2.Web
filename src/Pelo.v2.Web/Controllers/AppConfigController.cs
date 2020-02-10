using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.AppConfig;
using Pelo.v2.Web.Services.AppConfig;

namespace Pelo.v2.Web.Controllers
{
    public class AppConfigController : Controller
    {
        private readonly IAppConfigService _appConfigService;

        public AppConfigController(IAppConfigService appConfigService)
        {
            _appConfigService = appConfigService;
        }

        public IActionResult Index()
        {
            return View(new AppConfigSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(AppConfigSearchModel model)
        {
            var result = await _appConfigService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appConfigService.Delete(id);
            return Json(result);
        }
    }
}
