using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Models.Warranty;
using Pelo.v2.Web.Services.AppConfig;
using Pelo.v2.Web.Services.Customer;
using Pelo.v2.Web.Services.Product;
using Pelo.v2.Web.Services.Role;
using Pelo.v2.Web.Services.Warranty;

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

        public WarrantyController(IWarrantyService warrantyService,
                                 IBaseModelFactory baseModelFactory,
                                 ICustomerService customerService,
                                 IProductService productService,
                                 IRoleService roleService,
                                 IAppConfigService appConfigService)
        {
            _warrantyService = warrantyService;
            _baseModelFactory = baseModelFactory;
            _customerService = customerService;
            _productService = productService;
            _roleService = roleService;
            _appConfigService = appConfigService;
        }

        public async Task<IActionResult> Index()
        {
            var searchModel = new WarrantySearchModel();

            await _baseModelFactory.PrepareWarrantyStatuses(searchModel.AvaiableWarrantyStatus);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCreateds);
            await _baseModelFactory.PrepareUsers(searchModel.AvaiableUserCares);

            return View(searchModel);
        }
    }
}