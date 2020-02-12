using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}
