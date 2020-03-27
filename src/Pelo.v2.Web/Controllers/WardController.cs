using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pelo.Common.Dtos.Ward;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Ward;
using Pelo.v2.Web.Services.Province;

namespace Pelo.v2.Web.Controllers
{
    public class WardController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IWardService _wardService;

        public WardController(IWardService wardService,
                              IBaseModelFactory baseModelFactory)
        {
            _wardService = wardService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new WardSearchModel();

            await _baseModelFactory.PrepareProvinces(searchModel.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(searchModel.AvaiableDistricts,
                                                     searchModel.ProvinceId);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(WardSearchModel model)
        {
            var result = await _wardService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetWardByDistrictId(int districtId,
                                                             bool addAll = true)
        {
            var districts = await _wardService.GetAll(districtId);
            if (districts.IsSuccess)
            {
                if (districts.Data != null)
                {
                    var result = districts.Data.ToList();
                    if (addAll)
                    {
                        result.Insert(0,
                                      new Models.Ward.WardModel
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
            var result = await _wardService.Delete(id);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            var model = new UpdateWardModel();
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts,
                                                     model.ProvinceId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UpdateWardModel model)
        {
            if (ModelState.IsValid)
            {
                var rs = await _wardService.Add(new InsertWard
                {
                    SortOrder = model.SortOrder,
                    Name = model.Name,
                    DistrictId = model.DistrictId,
                    ProvinceId  = model.ProvinceId,
                    Type = model.Type
                });
                if (rs.IsSuccess)
                {
                    TempData["Update"] = JsonConvert.SerializeObject(rs);
                    return RedirectToAction("Index");
                }
            }
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts,
                                                     model.ProvinceId);
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _wardService.GetById(id);
            if (result.IsSuccess)
            {
                var model = new UpdateWardModel
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                    Type = result.Data.Type,
                    SortOrder = result.Data.SortOrder,
                    DistrictId = result.Data.DistrictId,
                    ProvinceId = result.Data.ProvinceId,
                };
                await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
                await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts,
                                                         model.ProvinceId);
                return View(model);
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateWardModel model)
        {
            var result = await _wardService.Edit(new UpdateWard
            {
                Id = model.Id,
                SortOrder = model.SortOrder,
                Name = model.Name,
                DistrictId = model.DistrictId,
                ProvinceId = model.ProvinceId,
                Type = model.Type
            });
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts, model.ProvinceId);
            return View(model);
        }
    }
}
