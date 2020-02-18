using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Invoice;
using Pelo.v2.Web.Services.Invoice;

namespace Pelo.v2.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService,
                                 IBaseModelFactory baseModelFactory)
        {
            _invoiceService = invoiceService;
            _baseModelFactory = baseModelFactory;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new InvoiceSearchModel();

            await _baseModelFactory.PrepareBranches(searchModel.AvaiableBranches);
            await _baseModelFactory.PrepareInvoiceStatuses(searchModel.AvaiableInvoiceStatuses);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCreateds);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserDeliveries);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserSells);

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(InvoiceSearchModel model)
        {
            var result = await _invoiceService.GetByPaging(model);
            return Json(result);
        }
    }
}
