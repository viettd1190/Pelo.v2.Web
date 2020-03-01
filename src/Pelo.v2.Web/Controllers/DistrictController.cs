using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.District;
using Pelo.v2.Web.Services.Province;

namespace Pelo.v2.Web.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService,
                                  IBaseModelFactory baseModelFactory)
        {
            _districtService = districtService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new DistrictSearchModel();

            await _baseModelFactory.PrepareProvinces(searchModel.AvaiableProvinces);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DistrictSearchModel model)
        {
            var result = await _districtService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetDistrictByProvinceId(int provinceId,
                                                                 bool addAll = true)
        {
            var districts = await _districtService.GetAll(provinceId);
            if(districts.IsSuccess)
            {
                if(districts.Data!=null)
                {
                    var result = districts.Data.ToList();
                    if(addAll)
                    {
                        result.Insert(0,new DistrictModel
                                        {
                                                Id = 0,
                                                Type = "Tất cả",
                                                Name = string.Empty
                                        });
                    }

                    return Json(result);
                }
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _districtService.Delete(id);
            return Json(result);
        }
        public async Task<IActionResult> Add()
        {
            var model = new UpdateDistrictModel();
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UpdateDistrictModel model)
        {
            if (ModelState.IsValid)
            {
                var rs = await _districtService.Add(model);
                if (rs.IsSuccess)
                {
                    TempData["Update"] = JsonConvert.SerializeObject(rs);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _districtService.GetById(id);
            if (result.IsSuccess)
            {
                var model = new UpdateDistrictModel();
                await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
                model.Id = result.Data.Id;
                model.Name = result.Data.Name;
                model.Type = result.Data.Type;
                model.SortOrder = result.Data.SortOrder;
                model.ProvinceId = result.Data.ProvinceId;
                return View(model);
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateDistrictModel model)
        {
            var result = await _districtService.Edit(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
