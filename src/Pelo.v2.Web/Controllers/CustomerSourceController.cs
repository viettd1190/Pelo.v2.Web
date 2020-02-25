using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pelo.v2.Web.Models.CustomerSource;
using Pelo.v2.Web.Services.CustomerSource;

namespace Pelo.v2.Web.Controllers
{
    public class CustomerSourceController : Controller
    {
        private readonly ICustomerSourceService _customerSourceService;

        public CustomerSourceController(ICustomerSourceService customerSourceService)
        {
            _customerSourceService = customerSourceService;
        }

        public IActionResult Index()
        {
            return View(new CustomerSourceSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(CustomerSourceSearchModel model)
        {
            var result = await _customerSourceService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerSourceService.Delete(id);
            return Json(result);
        }
        public IActionResult Add()
        {
            return View(new CustomerSourceModel());
        }
        [HttpPost]
        public async Task<IActionResult> Add(CustomerSourceModel model)
        {
            var result = await _customerSourceService.Add(model);
            if (result.IsSuccess)
            {
                TempData["Update"] = JsonConvert.SerializeObject(result);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _customerSourceService.GetById(id);
            if (result.IsSuccess)
            {
                return View(new CustomerSourceModel
                {
                    Id = result.Data.Id,
                    Name = result.Data.Name,
                });
            }
            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerSourceModel model)
        {
            var result = await _customerSourceService.Edit(model);
            TempData["Update"] = JsonConvert.SerializeObject(result);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
