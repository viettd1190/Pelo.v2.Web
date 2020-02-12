using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Branch;
using Pelo.v2.Web.Services.Branch;
using Pelo.v2.Web.Services.Province;

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

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var result = await _branchService.Delete(id);
        //    return Json(result);
        //}
    }
}
