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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.Delete(id);
            return Json(result);
        }
    }
}
