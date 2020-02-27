using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Services.Customer;
using Pelo.v2.Web.Services.Invoice;

namespace Pelo.v2.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly ICustomerService _customerService;

        private IInvoiceService _invoiceService;

        public CustomerController(ICustomerService customerService,
                                  IInvoiceService invoiceService,
                                  IBaseModelFactory baseModelFactory)
        {
            _customerService = customerService;
            _invoiceService = invoiceService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new CustomerSearchModel();

            await _baseModelFactory.PrepareProvinces(searchModel.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(searchModel.AvaiableDistricts);
            await _baseModelFactory.PrepareWards(searchModel.AvaiableWards);
            await _baseModelFactory.PrepareCustomerGroups(searchModel.AvaiableCustomerGroups);
            await _baseModelFactory.PrepareCustomerVips(searchModel.AvaiableCustomerVips);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CustomerSearchModel model)
        {
            var result = await _customerService.GetByPaging(model);
            return Json(result);
        }

        public async Task<IActionResult> Detail(int id, string nextAction = "")
        {
            var customer = await _customerService.GetDetail(id);
            if (customer.IsSuccess)
            {
                return View(new CustomerDetailModel
                {
                    Address = customer.Data.Address,
                    BirthDate = customer.Data.BirthDate,
                    Code = customer.Data.Code,
                    CustomerGroup = customer.Data.CustomerGroup,
                    CustomerVip = customer.Data.CustomerVip,
                    DateCreated = customer.Data.DateCreated,
                    DateUpdated = customer.Data.DateUpdated,
                    Description = customer.Data.Description,
                    District = customer.Data.District,
                    Email = customer.Data.Email,
                    Id = customer.Data.Id,
                    Name = customer.Data.Name,
                    Phone = customer.Data.Phone,
                    Phone2 = customer.Data.Phone2,
                    Phone3 = customer.Data.Phone3,
                    Profit = customer.Data.Profit,
                    ProfitUpdate = customer.Data.ProfitUpdate,
                    Province = customer.Data.Province,
                    UserCare = customer.Data.UserCare,
                    UserCarePhone = customer.Data.UserCarePhone,
                    UserCreated = customer.Data.UserCreated,
                    UserCreatedPhone = customer.Data.UserCreatedPhone,
                    UserFirst = customer.Data.UserFirst,
                    UserFirstPhone = customer.Data.UserFirstPhone,
                    Ward = customer.Data.Ward,
                    NextAction = nextAction
                });
            }

            return View("Notfound");
        }

        public async Task<IActionResult> Add(string nextAction)
        {
            var model = new CustomerInsertModel();
            model.Action = nextAction;
            await _baseModelFactory.PrepareCustomerGroups(model.AvaiableCustomerGroups);
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts);
            await _baseModelFactory.PrepareWards(model.AvaiableWards);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerInsertModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.Insert(model);
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    if (string.IsNullOrEmpty(model.Action))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var customer = await _customerService.GetCustomerByPhone(model.Phone);
                        if (customer.IsSuccess)
                        {
                            return RedirectToAction("Add", model.Action, new
                            {
                                customerId = customer.Data.Id
                            });
                        }                            
                    }

                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            await _baseModelFactory.PrepareCustomerGroups(model.AvaiableCustomerGroups);
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts);
            await _baseModelFactory.PrepareWards(model.AvaiableWards);

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetById(id);
            if (customer.IsSuccess)
            {
                var model = customer.Data;

                await _baseModelFactory.PrepareCustomerGroups(model.AvaiableCustomerGroups);
                await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
                await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts);
                await _baseModelFactory.PrepareWards(model.AvaiableWards);

                return View(model);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _customerService.Update(model);
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Detail",
                                            "Customer",
                                            new
                                            {
                                                id = model.Id
                                            });
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            await _baseModelFactory.PrepareCustomerGroups(model.AvaiableCustomerGroups);
            await _baseModelFactory.PrepareProvinces(model.AvaiableProvinces);
            await _baseModelFactory.PrepareDistricts(model.AvaiableDistricts);
            await _baseModelFactory.PrepareWards(model.AvaiableWards);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetInvoicesByCustomerId(CustomerInvoiceSearchModel model)
        {
            var result = await _invoiceService.GetByCustomerIdPaging(model);
            return Json(result);
        }

        public IActionResult FindByPhoneNumber(string nextAction)
        {
            return View(new FindCustomerByPhoneViewModel
            {
                NextAction = nextAction
            });
        }
        [HttpPost]
        public async Task<IActionResult> FindByPhoneNumber(FindCustomerByPhoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerService.GetCustomerByPhone(model.PhoneNumber);
                if (customer.IsSuccess)
                {
                    return RedirectToAction("Detail",
                                            "Customer",
                                            new
                                            {
                                                id = customer.Data.Id,
                                                nextAction = model.NextAction
                                            });
                }
            }
            return RedirectToAction("Add",
                                    "Customer",
                                    new
                                    {
                                        nextAction = model.NextAction
                                    });
        }
    }
}
