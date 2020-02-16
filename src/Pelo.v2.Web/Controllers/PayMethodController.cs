using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.PayMethod;
using Pelo.v2.Web.Services.PayMethod;

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
    }
}
