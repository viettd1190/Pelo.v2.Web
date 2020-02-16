using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Crm;
using Pelo.v2.Web.Services.Crm;

namespace Pelo.v2.Web.Controllers
{
    public class CrmController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly ICrmService _crmService;

        public CrmController(ICrmService crmService,
                             IBaseModelFactory baseModelFactory)
        {
            _crmService = crmService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new CrmSearchModel();

            await _baseModelFactory.PrepareProvinces(searchModel.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(searchModel.AvaiableDistricts);
            await _baseModelFactory.PrepareWards(searchModel.AvaiableWards);
            await _baseModelFactory.PrepareCustomerGroups(searchModel.AvaiableCustomerGroups);
            await _baseModelFactory.PrepareCustomerSources(searchModel.AvaiableCustomerSources);
            await _baseModelFactory.PrepareCustomerVips(searchModel.AvaiableCustomerVips);
            await _baseModelFactory.PrepareCrmTypes(searchModel.AvaiableCrmTypes);
            await _baseModelFactory.PrepareCrmStatuses(searchModel.AvaiableCrmStatuses);
            await _baseModelFactory.PrepareCrmPriorities(searchModel.AvaiableCrmPriorities);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCreateds);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCares);
            await _baseModelFactory.PrepareProductGroups(searchModel.AvaiableProductGroups);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CrmSearchModel model)
        {
            var result = await _crmService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> KhachChuaXuLyTrongNgay()
        {
            var searchModel = new CrmKhachChuaXuLyTrongNgaySearchModel();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetKhachChuaXuLyTrongNgay(CrmKhachChuaXuLyTrongNgaySearchModel model)
        {
            var result = await _crmService.KhachChuaXuLyTrongNgay(model);
            return Json(result);
        }

        public async Task<IActionResult> KhachToiHenCanChamSoc()
        {
            var searchModel = new CrmKhachToiHenCanChamSocSearchModel();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetKhachToiHenCanChamSoc(CrmKhachToiHenCanChamSocSearchModel model)
        {
            var result = await _crmService.KhachToiHenCanChamSoc(model);
            return Json(result);
        }

        public async Task<IActionResult> KhachQuaHenChamSoc()
        {
            var searchModel = new CrmKhachQuaHenChamSocSearchModel();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetKhachQuaHenChamSoc(CrmKhachQuaHenChamSocSearchModel model)
        {
            var result = await _crmService.KhachQuaHenChamSoc(model);
            return Json(result);
        }

        public async Task<IActionResult> KhachToiHenNgayMai()
        {
            var searchModel = new CrmKhachToiHenNgayMaiSearchModel();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetKhachToiHenNgayMai(CrmKhachToiHenNgayMaiSearchModel model)
        {
            var result = await _crmService.KhachToiHenNgayMai(model);
            return Json(result);
        }
    }
}
