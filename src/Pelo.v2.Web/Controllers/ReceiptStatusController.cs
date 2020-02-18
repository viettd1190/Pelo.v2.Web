using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.ReceiptStatus;
using Pelo.v2.Web.Services.ReceiptStatus;

namespace Pelo.v2.Web.Controllers
{
    public class ReceiptStatusController : Controller
    {
        private readonly IReceiptStatusService _receiptStatusService;

        public ReceiptStatusController(IReceiptStatusService receiptStatusService)
        {
            _receiptStatusService = receiptStatusService;
        }

        public IActionResult Index()
        {
            return View(new ReceiptStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(ReceiptStatusSearchModel model)
        {
            var result = await _receiptStatusService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _receiptStatusService.Delete(id);
            return Json(result);
        }
    }
}
