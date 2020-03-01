using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Models.Province;
using Pelo.v2.Web.Services.Province;

namespace Pelo.v2.Web.Controllers
{
    public class ProvinceController : Controller
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        public IActionResult Index()
        {
            return View(new ProvinceSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ProvinceSearchModel model)
        {
            var result = await _provinceService.GetByPaging(model);
            return Json(result);
        }

        public IActionResult Add()
        {
            return View(new ProvinceInsert());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProvinceInsert model)
        {
            if (ModelState.IsValid)
            {
                var result = await _provinceService.Insert(model);
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
            var model = await _provinceService.GetById(id);
            if (model.IsSuccess)
            {
                return View(new ProvinceUpdate { Id = model.Data.Id, Name = model.Data.Name, SortOrder = model.Data.SortOrder, Type = model.Data.Type });
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProvinceUpdate model)
        {
            if (ModelState.IsValid)
            {
                var result = await _provinceService.Update(model);
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
            var result = await _provinceService.Delete(id);
            return Json(result);
        }
    }
}
