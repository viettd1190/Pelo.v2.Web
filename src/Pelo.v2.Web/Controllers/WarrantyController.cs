using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Customer;
using Pelo.v2.Web.Models.Warranty;
using Pelo.v2.Web.Services.AppConfig;
using Pelo.v2.Web.Services.Customer;
using Pelo.v2.Web.Services.Product;
using Pelo.v2.Web.Services.Role;
using Pelo.v2.Web.Services.Warranty;
using Pelo.Common.Extensions;
using Pelo.v2.Web.Services.WarrantyStatus;
using Pelo.v2.Web.Services.WarrantyDescription;
using Microsoft.Extensions.Configuration;

namespace Pelo.v2.Web.Controllers
{
    public class WarrantyController : BaseController
    {
        private readonly IBaseModelFactory _baseModelFactory;

        private readonly ICustomerService _customerService;

        private readonly IWarrantyService _warrantyService;

        private readonly IAppConfigService _appConfigService;

        private readonly IProductService _productService;

        private readonly IRoleService _roleService;

        private readonly IWarrantyStatusService _warrantyStatusService;

        private readonly IWarrantyDescriptionService _warrantyDescriptionService;

        private readonly IConfiguration _configuration;

        public WarrantyController(IWarrantyService warrantyService,
                                 IBaseModelFactory baseModelFactory,
                                 ICustomerService customerService,
                                 IProductService productService,
                                 IRoleService roleService,
                                 IAppConfigService appConfigService,
                                 IWarrantyDescriptionService warrantyDescriptionService,
                                 IWarrantyStatusService warrantyStatusService,IConfiguration configuration)
        {
            _warrantyService = warrantyService;
            _baseModelFactory = baseModelFactory;
            _customerService = customerService;
            _productService = productService;
            _roleService = roleService;
            _appConfigService = appConfigService;
            _warrantyDescriptionService = warrantyDescriptionService;
            _warrantyStatusService = warrantyStatusService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new WarrantySearchModel();

            await _baseModelFactory.PrepareWarrantyStatuses(searchModel.AvaiableWarrantyStatus);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCreateds);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCares);

            return View(searchModel);
        }
        [HttpPost]
        public async Task<IActionResult> GetList(WarrantySearchModel model)
        {
            var result = await _warrantyService.GetByPaging(model);
            return Json(result);
        }

        public IActionResult FindCustomer()
        {
            return View(new CustomerFindByPhoneModel());
        }

        [HttpPost]
        public async Task<IActionResult> FindCustomer(CustomerFindByPhoneModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerService.GetCustomerByPhone(model.PhoneNumber);
                if (customer.IsSuccess)
                {
                    return RedirectToAction("Add",
                                            "Warranty",
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

            if (customer.IsSuccess)
            {
                var model = new InsertWarrantyModel
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
                await _baseModelFactory.PrepareProducts(model.AvaiableProducts,
                                                    false);
                await _baseModelFactory.PrepareWarrantyDescription(model.AvaiableWarrantyDescriptions,
                                                    false);
                await _baseModelFactory.PrepareWarrantyStatuses(model.AvaiableWarrantyStatuses,
                                                    false);
                ViewBag.Products = (await _productService.GetAll()).ToList();
                ViewBag.DefaultRole = false;

                var currentRole = (await _roleService.GetCurrentRoleName()).Data;
                var defaultRoleAcceptWarranty = (await _appConfigService.GetByName("DefaultWarrantyAcceptRoles"));
                if (defaultRoleAcceptWarranty.IsSuccess)
                {
                    if (defaultRoleAcceptWarranty.Data.Split(' ')
                                               .Contains(currentRole))
                    {
                        ViewBag.DefaultRole = true;
                    }
                }

                return View(model);
            }

            return View("Notfound");
        }

        [HttpPost]
        public async Task<IActionResult> Add(InsertWarrantyModel model)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.ProductRaw))
                {
                    var products = new List<ProductInWarrantyModel>();
                    products.AddRange(model.ProductRaw.ToObject<IEnumerable<ProductInWarrantyModel>>());
                    model.Products = products;
                }

                var result = await _warrantyService.Insert(model);
                if (result.IsSuccess)
                {
                    TempData["Update"] = result.ToJson();
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("",
                                         result.Message);
            }

            var customer = await _customerService.GetDetail(model.CustomerId);
            if (customer.IsSuccess)
            {
                model.Customer = new CustomerDetailModel
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
                };
            }

            await _baseModelFactory.PrepareBranches(model.AvaiableBranches,
                                                        false);
            await _baseModelFactory.PrepareProducts(model.AvaiableProducts,
                                                false);
            await _baseModelFactory.PrepareWarrantyDescription(model.AvaiableWarrantyDescriptions,
                                                false);
            await _baseModelFactory.PrepareWarrantyStatuses(model.AvaiableWarrantyStatuses,
                                                false);
            ViewBag.Products = (await _productService.GetAll()).ToList();
            ViewBag.DefaultRole = false;

            var currentRole = (await _roleService.GetCurrentRoleName()).Data;
            var defaultRoleAcceptInvoice = (await _appConfigService.GetByName("DefaultWarrantyAcceptRoles"));
            if (defaultRoleAcceptInvoice.IsSuccess)
            {
                if (defaultRoleAcceptInvoice.Data.Split(' ')
                                            .Contains(currentRole))
                {
                    ViewBag.DefaultRole = true;
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var response = await _warrantyService.GetById(id);
            if (response.IsSuccess)
            {
                var warranty = response.Data;

                if (warranty != null)
                {
                    var model = new WarrantyDetailModel
                    {
                        Id = warranty.Id,
                        Code = warranty.Code,
                        Customer = new CustomerDetailModel(),
                        CustomerId = warranty.CustomerId,
                        DateCreated = warranty.DateCreated,
                        UserCreated = warranty.UserCreated,
                        UserCreatedPhone = warranty.UserCreatedPhone,
                        Deposit = warranty.Deposit,
                        Description = warranty.Description,
                        Total = warranty.Total,
                        DeliveryDate = warranty.DeliveryDate,
                        WarrantyStatusId = warranty.WarrantyStatusId,

                        UserCareIds = warranty.UsersCare.Select(c => c.Id)
                                                             .ToList(),
                        UsersInChargeIds = warranty.UsersInCharge.Select(c => c.Id)
                                                                 .ToList(),
                        Products = warranty.Products.Select(c => new ProductInWarrantyDetailModel
                        {
                            Name = c.Name,
                            Description = c.Description,
                            SerialNumber = c.SerialNumber,
                            WarrantyDescription = c.WarrantyDescription
                        }).ToList()
                    };
                    var customer = await _customerService.GetDetail(warranty.CustomerId);
                    if (customer.IsSuccess && customer.Data != null)
                    {
                        model.Customer = new CustomerDetailModel
                        {
                            Id = customer.Data.Id,
                            Code = customer.Data.Code,
                            Name = customer.Data.Name,
                            Phone = customer.Data.Phone,
                            Phone2 = customer.Data.Phone2,
                            Phone3 = customer.Data.Phone3,
                            Address = customer.Data.Address,
                            DateCreated = customer.Data.DateCreated,
                            Description = customer.Data.Description,
                            UserCreated = customer.Data.UserCreated,
                            UserCreatedPhone = customer.Data.UserCreatedPhone
                        };
                    }
                    var crmStatusDeleted = await _appConfigService.GetByName("WarrantyStatusDeleted");
                    if (crmStatusDeleted.IsSuccess)
                    {
                        model.WarrantyStatusDeleted = Convert.ToInt32(crmStatusDeleted.Data);
                    }
                    await _baseModelFactory.PrepareWarrantyStatuses(model.AvaiableWarrantyStatuses,
                                                        false);
                    return View(model);
                }
            }

            return View("Notfound");
        }
        [HttpPost]
        public async Task<IActionResult> GetLogs(int id)
        {
            var logs = await _warrantyService.GetLogs(id);
            var result = new List<WarrantyLogDetail>();
            foreach (var log in logs)
            {
                var crmLog = new WarrantyLogDetail
                {
                    Name = log.User?.Name ?? string.Empty,
                    PhoneNumber = log.User?.PhoneNumber ?? string.Empty,
                    Avatar = log.User?.Avatar ?? string.Empty,
                    Content = log.Comment,
                    LogDate = string.Format(AppUtil.DATE_TIME_FORMAT,
                                                             log.LogDate),
                    AttachmentModels = new List<AttachmentModel>()
                };

                crmLog.AttachmentModels.AddRange(log.Attachments.Where(c => !string.IsNullOrEmpty(c.AttachmentName))
                                                    .Select(c => new AttachmentModel
                                                    {
                                                        Name = c.AttachmentName,
                                                        Url = $"{_configuration["AttachFileUrl"]}/{c.Attachment}",
                                                        IsImage = CheckStringIsImageExtension(c.AttachmentName)
                                                    })
                                                    .OrderByDescending(c => CheckStringIsImageExtension(c.Name)));

                result.Add(crmLog);
            }

            return Json(result);
        }
        private bool CheckStringIsImageExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            if (fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".png") || fileName.EndsWith(".bmp") || fileName.EndsWith(".gif"))
            {
                return true;
            }

            return false;
        }
    }
}