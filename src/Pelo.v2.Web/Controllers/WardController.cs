﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            if(districts.IsSuccess)
            {
                if(districts.Data != null)
                {
                    var result = districts.Data.ToList();
                    if(addAll)
                    {
                        result.Insert(0,
                                      new WardModel
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
    }
}
