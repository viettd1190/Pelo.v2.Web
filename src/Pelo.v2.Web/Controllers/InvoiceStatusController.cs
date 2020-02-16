using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Models.InvoiceStatus;
using Pelo.v2.Web.Services.InvoiceStatus;

namespace Pelo.v2.Web.Controllers
{
    public class InvoiceStatusController : Controller
    {
        private readonly IInvoiceStatusService _invoiceStatusService;

        public InvoiceStatusController(IInvoiceStatusService invoiceStatusService)
        {
            _invoiceStatusService = invoiceStatusService;
        }

        public IActionResult Index()
        {
            return View(new InvoiceStatusSearchModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetList(InvoiceStatusSearchModel model)
        {
            var result = await _invoiceStatusService.GetByPaging(model);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _invoiceStatusService.Delete(id);
            return Json(result);
        }
    }
}
