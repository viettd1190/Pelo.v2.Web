using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.PayMethod;
using Pelo.v2.Web.Services.PayMethod;
using Pelo.Common.Extensions;

namespace Pelo.v2.Web.Controllers
{
    public class PayMethodController : Controller
    {
        private readonly IPayMethodService _payMethodService;

        public PayMethodController(IPayMethodService payMethodService)
        {
            _payMethodService = payMethodService;
        }

        public IActionResult Index()
        {
            return View(new PayMethodSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(PayMethodSearchModel model)
        {
            var result = await _payMethodService.GetByPaging(model);
            return Json(result);
        }
        public IActionResult Add()
        {
            return View(new PayMethodModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(PayMethodModel model)
        {
            var result = await _payMethodService.Add(new Common.Dtos.PayMethod.InsertPayMethod { Name = model.Name });
            if (result.IsSuccess)
            {
                TempData["Update"] = result.ToJson();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _payMethodService.GetById(id);
            if (result.IsSuccess)
            {
                return View(result);
            }
            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PayMethodModel model)
        {
            var result = await _payMethodService.Edit(new Common.Dtos.PayMethod.UpdatePayMethod { Id = model.Id, Name = model.Name });
            TempData["Update"] = result;
            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
