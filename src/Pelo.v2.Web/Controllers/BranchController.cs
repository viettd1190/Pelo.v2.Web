using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Dtos.Branch;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Services.Branch;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService,
                                  IBaseModelFactory baseModelFactory)
        {
            _branchService = branchService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new BranchSearchModel();

            await _baseModelFactory.PrepareProvinces(searchModel.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(searchModel.AvaiableDistricts,
                                                     searchModel.ProvinceId);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(BranchSearchModel model)
        {

            var result = await _branchService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Add()
        {
            var model = new UpdateBranchModel();
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts, model.ProvinceId);
            await _baseModelFactory.PrepareWards(model.AvaiableWards, model.WardId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UpdateBranchModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _branchService.Insert(new InsertBranch { Address = model.Address, ProvinceId = model.ProvinceId, Hotline = model.Hotline, WardId = model.WardId, DistrictId = model.DistrictId, Name = model.Name });
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
            var rs = await _branchService.GetById(id);
            if (rs.IsSuccess)
            {
                var model = new UpdateBranchModel();
                model.Id = rs.Data.Id; 
                model.Address = rs.Data.Address; 
                model.ProvinceId = rs.Data.ProvinceId; 
                model.Hotline = rs.Data.Hotline; 
                model.WardId = rs.Data.WardId; 
                model.DistrictId = rs.Data.DistrictId; 
                model.Name = rs.Data.Name;
                await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
                await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts, model.ProvinceId);
                await _baseModelFactory.PrepareWards(model.AvaiableWards, model.DistrictId);
                return View(model);
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateBranchModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _branchService.Update(new UpdateBranch { Id = model.Id, Address = model.Address, ProvinceId = model.ProvinceId, Hotline = model.Hotline, WardId = model.WardId, DistrictId = model.DistrictId, Name = model.Name });
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", result.Message);
            }
            return View(model);
        }
        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _branchService.Delete(id);
        //    return Json(result);
        //}
    }
}
