using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Extensions;
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

        public IActionResult Add()
        {
            return View(new AppConfigInsert());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AppConfigInsert model)
        {
            if(ModelState.IsValid)
            {
                var result = await _appConfigService.Insert(model);
                if(result.IsSuccess)
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
            var model = await _appConfigService.GetById(id);
            if(model.IsSuccess)
            {
                return View(model.Data);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _appConfigService.Delete(id);
            return Json(result);
        }
    }
}
