//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Pelo.v2.Web.Models.Province;
//using Pelo.v2.Web.Services.Province;

//namespace Pelo.v2.Web.Controllers
//{
//    public class ProvinceController : Controller
//    {
//        private readonly IProvinceService _provinceService;

//        public ProvinceController(IProvinceService provinceService)
//        {
//            _provinceService = provinceService;
//        }

//        public IActionResult Index()
//        {
//            return View(new ProvinceSearchModel());
//        }

//        [HttpPost]
//        public async Task<IActionResult> GetList(ProvinceSearchModel model)
//        {
//            var result = await _provinceService.GetByPaging(model);
//            return Json(result);
//        }
//    }
//}
