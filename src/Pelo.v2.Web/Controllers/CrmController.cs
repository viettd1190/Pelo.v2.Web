using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Crm;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Services.Crm;
using Pelo.v2.Web.Services.CrmPriority;
using Pelo.v2.Web.Services.CrmType;
using Pelo.v2.Web.Services.Customer;
using Pelo.v2.Web.Services.CustomerSource;
using Pelo.v2.Web.Services.ProductGroup;
using Pelo.v2.Web.Services.User;

namespace Pelo.v2.Web.Controllers
{
    public class CrmController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly ICrmService _crmService;

        private ICrmPriorityService _crmPriorityService;

        private ICrmTypeService _crmTypeService;

        private readonly ICustomerService _customerService;

        private ICustomerSourceService _customerSourceService;

        private IProductGroupService _productGroupService;

        private IUserService _userService;

        public CrmController(ICrmService crmService,
                             IBaseModelFactory baseModelFactory,
                             ICustomerService customerService,
                             IUserService userService,
                             ICrmPriorityService crmPriorityService,
                             ICustomerSourceService customerSourceService,
                             IProductGroupService productGroupService,
                             ICrmTypeService crmTypeService)
        {
            _crmService = crmService;
            _baseModelFactory = baseModelFactory;
            _customerService = customerService;
            _userService = userService;
            _crmPriorityService = crmPriorityService;
            _customerSourceService = customerSourceService;
            _productGroupService = productGroupService;
            _crmTypeService = crmTypeService;
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

        public IActionResult FindCustomer()
        {
            return View(new CustomerFindByPhoneModel());
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomer(CustomerFindByPhoneModel model)
        {
            if(ModelState.IsValid)
            {
                var customer = await _customerService.GetCustomerByPhone(model.PhoneNumber);
                if(customer.IsSuccess)
                {
                    return RedirectToAction("Add",
                                            "Crm",
                                            new
                                            {
                                                    customerPhone = model.PhoneNumber
                                            });
                }

                //todo: Sửa lại chuyển qua trang thêm mới khách hàng
                return View("Notfound");
            }

            return View(model);
        }

        public async Task<IActionResult> Add(string customerPhone)
        {
            var customer = await _customerService.GetCustomerByPhone(customerPhone);

            if(customer.IsSuccess)
            {
                var searchModel = new InsertCrmModel
                                  {
                                          CustomerId = customer.Data.Id,
                                          Customer = new CustomerDetailModel
                                                     {
                                                             Code = customer.Data.Code,
                                                             Name = customer.Data.Name,
                                                             Phone = customer.Data.Phone,
                                                             Phone2 = customer.Data.Phone2,
                                                             Phone3 = customer.Data.Phone3,
                                                             Province = customer.Data.Province,
                                                             District = customer.Data.District,
                                                             Ward = customer.Data.Ward,
                                                             Address = customer.Data.Address,
                                                             CustomerGroup = customer.Data.CustomerGroup,
                                                             CustomerVip = customer.Data.CustomerVip,
                                                             Email=customer.Data.Email,
                                                             DateCreated = customer.Data.DateCreated,
                                                             Description = customer.Data.Description
                                                     }
                                  };

                await _baseModelFactory.PrepareCustomerSources(searchModel.AvaiableCustomerSources);
                await _baseModelFactory.PrepareCrmTypes(searchModel.AvaiableCrmTypes);
                await _baseModelFactory.PrepareCrmPriorities(searchModel.AvaiableCrmPriorities);
                await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCares);
                await _baseModelFactory.PrepareProductGroups(searchModel.AvaiableProductGroups);

                return View(searchModel);
            }

            return View("Notfound");
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(InsertCrmModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _crmService.Insert(model);
        //        if (result.IsSuccess)
        //        {
        //            TempData["Update"] = result.ToJson();
        //            return RedirectToAction("Index");
        //        }

        //        ModelState.AddModelError("",
        //                                 result.Message);
        //    }
        //    await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
        //    await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts);
        //    await _baseModelFactory.PrepareWards(model.AvaiableWards);
        //    await _baseModelFactory.PrepareCustomerGroups(model.AvaiableCustomerGroups);
        //    await _baseModelFactory.PrepareCustomerSources(model.AvaiableCustomerSources);
        //    await _baseModelFactory.PrepareCustomerVips(model.AvaiableCustomerVips);
        //    await _baseModelFactory.PrepareCrmTypes(model.AvaiableCrmTypes);
        //    await _baseModelFactory.PrepareCrmStatuses(model.AvaiableCrmStatuses);
        //    await _baseModelFactory.PrepareCrmPriorities(model.AvaiableCrmPriorities);
        //    await _baseModelFactory.PrepareUsers(model.AvaiableUserCreateds);
        //    await _baseModelFactory.PrepareUsers(model.AvaiableUserCares);
        //    await _baseModelFactory.PrepareProductGroups(model.AvaiableProductGroups);

        //    return View(model);
        //}
    }
}
