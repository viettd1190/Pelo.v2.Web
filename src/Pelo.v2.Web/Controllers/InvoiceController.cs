﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Models.Invoice;
using Pelo.v2.Web.Services.Customer;
using Pelo.v2.Web.Services.Invoice;
using Pelo.v2.Web.Services.Product;

namespace Pelo.v2.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly IInvoiceService _invoiceService;

        private readonly ICustomerService _customerService;

        private IProductService _productService;

        public InvoiceController(IInvoiceService invoiceService,
                                 IBaseModelFactory baseModelFactory,
                                 ICustomerService customerService,
                                 IProductService productService)
        {
            _invoiceService = invoiceService;
            _baseModelFactory = baseModelFactory;
            _customerService = customerService;
            _productService = productService;
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
                                            "Invoice",
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
                var model = new InvoiceInsertModel
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

                await _baseModelFactory.PrepareBranches(model.AvaiableBranches,
                                                        false);
                await _baseModelFactory.PreparePayMethods(model.AvaiablePayMethods,
                                                          false);
                await _baseModelFactory.PrepareUsers(model.AvaiableUsers,
                                                     false);
                await _baseModelFactory.PrepareProducts(model.AvaiableProducts,
                                                        false);

                ViewBag.Products =(await _productService.GetAll()).ToList();

                return View(model);
            }

            return View("Notfound");
        }
    }
}
