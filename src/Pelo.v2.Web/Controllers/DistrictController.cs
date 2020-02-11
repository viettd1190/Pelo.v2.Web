﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.District;
using Pelo.v2.Web.Services.District;

namespace Pelo.v2.Web.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IDistrictService _districtService;

        private readonly IBaseModelFactory _baseModelFactory;

        public DistrictController(IDistrictService districtService,
                                  IBaseModelFactory baseModelFactory)
        {
            _districtService = districtService;
            _baseModelFactory = baseModelFactory;
        }

        public IActionResult Index()
        {
            var searchModel = new DistrictSearchModel();

            _baseModelFactory.PrepareProvinces(searchModel.AvaiableProvinces);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(DistrictSearchModel model)
        {
            var result = await _districtService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _districtService.Delete(id);
            return Json(result);
        }
    }
}
