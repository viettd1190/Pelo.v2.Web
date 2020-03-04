using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Crm;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Services.Crm;
using Pelo.v2.Web.Services.Customer;

namespace Pelo.v2.Web.Controllers
{
    public class CrmController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly ICrmService _crmService;

        private readonly ICustomerService _customerService;

        public CrmController(ICrmService crmService,
                             IBaseModelFactory baseModelFactory,
                             ICustomerService customerService)
        {
            _crmService = crmService;
            _baseModelFactory = baseModelFactory;
            _customerService = customerService;
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
                var model = new InsertCrmModel
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
                                                       Email = customer.Data.Email,
                                                       DateCreated = customer.Data.DateCreated,
                                                       Description = customer.Data.Description
                                               }
                            };

                await _baseModelFactory.PrepareCustomerSources(model.AvaiableCustomerSources,false);
                await _baseModelFactory.PrepareCrmTypes(model.AvaiableCrmTypes, false);
                await _baseModelFactory.PrepareCrmPriorities(model.AvaiableCrmPriorities, false);
                await _baseModelFactory.PrepareUsers(model.AvaiableUserCares, false);
                await _baseModelFactory.PrepareProductGroups(model.AvaiableProductGroups, false);
                await _baseModelFactory.PrepareCrmStatuses(model.AvaiableCrmStatuses, false);

                return View(model);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsertCrmModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _crmService.Insert(model);
                if(result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            await _baseModelFactory.PrepareCustomerSources(model.AvaiableCustomerSources, false);
            await _baseModelFactory.PrepareCrmTypes(model.AvaiableCrmTypes, false);
            await _baseModelFactory.PrepareCrmPriorities(model.AvaiableCrmPriorities, false);
            await _baseModelFactory.PrepareUsers(model.AvaiableUserCares, false);
            await _baseModelFactory.PrepareProductGroups(model.AvaiableProductGroups, false);
            await _baseModelFactory.PrepareCrmStatuses(model.AvaiableCrmStatuses, false);

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var model = await _crmService.GetById(id);
            if(model!=null)
            {
                
                var customer = await _customerService.GetDetail(model.CustomerId);
                if(customer!=null)
                {
                    model.Customer = new CustomerDetailModel
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
                                             Email = customer.Data.Email,
                                             DateCreated = customer.Data.DateCreated,
                                             Description = customer.Data.Description
                                     };

                    await _baseModelFactory.PrepareCustomerSources(model.AvaiableCustomerSources, false);
                    await _baseModelFactory.PrepareCrmTypes(model.AvaiableCrmTypes, false);
                    await _baseModelFactory.PrepareCrmPriorities(model.AvaiableCrmPriorities, false);
                    await _baseModelFactory.PrepareUsers(model.AvaiableUserCares, false);
                    await _baseModelFactory.PrepareProductGroups(model.AvaiableProductGroups, false);
                    await _baseModelFactory.PrepareCrmStatuses(model.AvaiableCrmStatuses, false);

                    return View(model);
                }
            }

            return View("Notfound");
        }
    }
}
