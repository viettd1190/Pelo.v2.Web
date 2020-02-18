using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Services.Customer;

namespace Pelo.v2.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService,
                                  IBaseModelFactory baseModelFactory)
        {
            _customerService = customerService;
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

        public async Task<IActionResult> Detail(int id)
        {
            var customer = await _customerService.GetDetail(id);
            if(customer.IsSuccess)
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
                                    Ward = customer.Data.Ward
                            });
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            return Json(result);
        }
    }
}
