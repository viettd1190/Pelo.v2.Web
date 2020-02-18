using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Services.Branch;
using Pelo.v2.Web.Services.CrmPriority;
using Pelo.v2.Web.Services.CrmStatus;
using Pelo.v2.Web.Services.CrmType;
using Pelo.v2.Web.Services.CustomerGroup;
using Pelo.v2.Web.Services.CustomerSource;
using Pelo.v2.Web.Services.CustomerVip;
using Pelo.v2.Web.Services.Department;
using Pelo.v2.Web.Services.InvoiceStatus;
using Pelo.v2.Web.Services.Manufacturer;
using Pelo.v2.Web.Services.PayMethod;
using Pelo.v2.Web.Services.Product;
using Pelo.v2.Web.Services.ProductGroup;
using Pelo.v2.Web.Services.ProductStatus;
using Pelo.v2.Web.Services.ProductUnit;
using Pelo.v2.Web.Services.Province;
using Pelo.v2.Web.Services.ReceiptDescription;
using Pelo.v2.Web.Services.ReceiptStatus;
using Pelo.v2.Web.Services.Role;
using Pelo.v2.Web.Services.TaskLoop;
using Pelo.v2.Web.Services.TaskPriority;
using Pelo.v2.Web.Services.TaskStatus;
using Pelo.v2.Web.Services.TaskType;
using Pelo.v2.Web.Services.User;

namespace Pelo.v2.Web.Factories
{
    public interface IBaseModelFactory
    {
        /// <summary>
        ///     Get all provinces
        /// </summary>
        /// <param name="items">List store provinces</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        Task PrepareProvinces(IList<SelectListItem> items,
                              bool withSpecialDefaultItem = true,
                              string defaultItemText = null);

        /// <summary>
        ///     Get all districts by province id
        /// </summary>
        /// <param name="items">List store districts</param>
        /// <param name="provinceId"></param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        /// <returns></returns>
        Task PrepareDistricts(IList<SelectListItem> items,
                              int provinceId = 0,
                              bool withSpecialDefaultItem = true,
                              string defaultItemText = null);

        /// <summary>
        ///     Get all wards by district id
        /// </summary>
        /// <param name="items">List store wards</param>
        /// <param name="districtId"></param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        /// <returns></returns>
        Task PrepareWards(IList<SelectListItem> items,
                          int districtId = 0,
                          bool withSpecialDefaultItem = true,
                          string defaultItemText = null);

        /// <summary>
        ///     Get all branches
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareBranches(IList<SelectListItem> items,
                             bool withSpecialDefaultItem = true,
                             string defaultItemText = null);

        /// <summary>
        ///     Get all roles
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareRoles(IList<SelectListItem> items,
                          bool withSpecialDefaultItem = true,
                          string defaultItemText = null);

        /// <summary>
        ///     Get all departments
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareDepartments(IList<SelectListItem> items,
                                bool withSpecialDefaultItem = true,
                                string defaultItemText = null);

        /// <summary>
        ///     Get all customer vips
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareCustomerVips(IList<SelectListItem> items,
                                 bool withSpecialDefaultItem = true,
                                 string defaultItemText = null);

        /// <summary>
        ///     Get all customer groups
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareCustomerGroups(IList<SelectListItem> items,
                                   bool withSpecialDefaultItem = true,
                                   string defaultItemText = null);

        /// <summary>
        ///     Get all product groups
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareProductGroups(IList<SelectListItem> items,
                                  bool withSpecialDefaultItem = true,
                                  string defaultItemText = null);

        /// <summary>
        ///     Get all product units
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareProductUnits(IList<SelectListItem> items,
                                 bool withSpecialDefaultItem = true,
                                 string defaultItemText = null);

        /// <summary>
        ///     Get all product statuses
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareProductStatuses(IList<SelectListItem> items,
                                    bool withSpecialDefaultItem = true,
                                    string defaultItemText = null);

        /// <summary>
        ///     Get all manufacturers
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareManufacturers(IList<SelectListItem> items,
                                  bool withSpecialDefaultItem = true,
                                  string defaultItemText = null);

        /// <summary>
        ///     Get all products
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareProducts(IList<SelectListItem> items,
                             bool withSpecialDefaultItem = true,
                             string defaultItemText = null);

        /// <summary>
        ///     Get all crm statuses
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareCrmStatuses(IList<SelectListItem> items,
                                bool withSpecialDefaultItem = true,
                                string defaultItemText = null);

        /// <summary>
        ///     Get all crm priorities
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareCrmPriorities(IList<SelectListItem> items,
                                  bool withSpecialDefaultItem = true,
                                  string defaultItemText = null);

        /// <summary>
        ///     Get all crm types
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareCrmTypes(IList<SelectListItem> items,
                             bool withSpecialDefaultItem = true,
                             string defaultItemText = null);

        /// <summary>
        ///     Get all customer sources
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareCustomerSources(IList<SelectListItem> items,
                                    bool withSpecialDefaultItem = true,
                                    string defaultItemText = null);

        /// <summary>
        ///     Get all users
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareUsers(IList<SelectListItem> items,
                          bool withSpecialDefaultItem = true,
                          string defaultItemText = null);

        /// <summary>
        ///     Get all task types
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareTaskTypes(IList<SelectListItem> items,
                              bool withSpecialDefaultItem = true,
                              string defaultItemText = null);

        /// <summary>
        ///     Get all task statuses
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareTaskStatuses(IList<SelectListItem> items,
                                 bool withSpecialDefaultItem = true,
                                 string defaultItemText = null);

        /// <summary>
        ///     Get all task priorities
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareTaskPriorities(IList<SelectListItem> items,
                                   bool withSpecialDefaultItem = true,
                                   string defaultItemText = null);

        /// <summary>
        ///     Get all task loops
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareTaskLoops(IList<SelectListItem> items,
                              bool withSpecialDefaultItem = true,
                              string defaultItemText = null);

        /// <summary>
        ///     Get all pay methods
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PreparePayMethods(IList<SelectListItem> items,
                               bool withSpecialDefaultItem = true,
                               string defaultItemText = null);

        /// <summary>
        ///     Get all invoice statuses
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareInvoiceStatuses(IList<SelectListItem> items,
                                    bool withSpecialDefaultItem = true,
                                    string defaultItemText = null);

        /// <summary>
        ///     Get all receipt statuses
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareReceiptStatuses(IList<SelectListItem> items,
                                    bool withSpecialDefaultItem = true,
                                    string defaultItemText = null);

        /// <summary>
        ///     Get all receipt description
        /// </summary>
        /// <param name="items"></param>
        /// <param name="withSpecialDefaultItem"></param>
        /// <param name="defaultItemText"></param>
        /// <returns></returns>
        Task PrepareReceiptDescriptions(IList<SelectListItem> items,
                                        bool withSpecialDefaultItem = true,
                                        string defaultItemText = null);
    }

    public class BaseModelFactory : IBaseModelFactory
    {
        private readonly IBranchService _branchService;

        private readonly ICrmPriorityService _crmPriorityService;

        private readonly ICrmStatusService _crmStatusService;

        private readonly ICrmTypeService _crmTypeService;

        private readonly ICustomerGroupService _customerGroupService;

        private readonly ICustomerSourceService _customerSourceService;

        private readonly ICustomerVipService _customerVipService;

        private readonly IDepartmentService _departmentService;

        private readonly IDistrictService _districtService;

        private readonly IInvoiceStatusService _invoiceStatusService;

        private readonly IManufacturerService _manufacturerService;

        private readonly IPayMethodService _payMethodService;

        private readonly IProductGroupService _productGroupService;

        private readonly IProductService _productService;

        private readonly IProductStatusService _productStatusService;

        private readonly IProductUnitService _productUnitService;

        private readonly IProvinceService _provinceService;

        private readonly IReceiptStatusService _receiptStatusService;

        private readonly IRoleService _roleService;

        private readonly ITaskLoopService _taskLoopService;

        private readonly ITaskPriorityService _taskPriorityService;

        private readonly ITaskStatusService _taskStatusService;

        private readonly ITaskTypeService _taskTypeService;

        private readonly IUserService _userService;

        private readonly IWardService _wardService;

        private readonly IReceiptDescriptionService _receiptDescriptionService;

        public BaseModelFactory(IProvinceService provinceService,
                                IDistrictService districtService,
                                IWardService wardService,
                                IBranchService branchService,
                                IRoleService roleService,
                                IDepartmentService departmentService,
                                ICustomerVipService customerVipService,
                                ICustomerGroupService customerGroupService,
                                IProductGroupService productGroupService,
                                IProductUnitService productUnitService,
                                IProductStatusService productStatusService,
                                IManufacturerService manufacturerService,
                                IProductService productService,
                                ICrmStatusService crmStatusService,
                                ICrmPriorityService crmPriorityService,
                                ICrmTypeService crmTypeService,
                                ICustomerSourceService customerSourceService,
                                IUserService userService,
                                ITaskTypeService taskTypeService,
                                ITaskStatusService taskStatusService,
                                ITaskPriorityService taskPriorityService,
                                ITaskLoopService taskLoopService,
                                IPayMethodService payMethodService,
                                IInvoiceStatusService invoiceStatusService,
                                IReceiptStatusService receiptStatusService,
                                IReceiptDescriptionService receiptDescriptionService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;
            _branchService = branchService;
            _roleService = roleService;
            _departmentService = departmentService;
            _customerVipService = customerVipService;
            _customerGroupService = customerGroupService;
            _productGroupService = productGroupService;
            _productUnitService = productUnitService;
            _productStatusService = productStatusService;
            _manufacturerService = manufacturerService;
            _productService = productService;
            _crmStatusService = crmStatusService;
            _crmPriorityService = crmPriorityService;
            _crmTypeService = crmTypeService;
            _customerSourceService = customerSourceService;
            _userService = userService;
            _taskTypeService = taskTypeService;
            _taskStatusService = taskStatusService;
            _taskPriorityService = taskPriorityService;
            _taskLoopService = taskLoopService;
            _payMethodService = payMethodService;
            _invoiceStatusService = invoiceStatusService;
            _receiptStatusService = receiptStatusService;
            _receiptDescriptionService = receiptDescriptionService;
        }

        #region IBaseModelFactory Members

        /// <summary>
        ///     Prepare available stores
        /// </summary>
        /// <param name="items">Store items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public async Task PrepareProvinces(IList<SelectListItem> items,
                                           bool withSpecialDefaultItem = true,
                                           string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available provinces
            var avaiableProvinces = await _provinceService.GetAll();
            if(avaiableProvinces.IsSuccess)
            {
                foreach (var province in avaiableProvinces.Data)
                {
                    items.Add(new SelectListItem
                              {
                                      Value = province.Id.ToString(),
                                      Text = province.Name
                              });
                }

                //insert special item for the default value
                PrepareDefaultItem(items,
                                   withSpecialDefaultItem,
                                   defaultItemText);
            }
        }

        public async Task PrepareDistricts(IList<SelectListItem> items,
                                           int provinceId = 0,
                                           bool withSpecialDefaultItem = true,
                                           string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available districts
            var avaiableDistricts = await _districtService.GetAll(provinceId);
            if(avaiableDistricts.IsSuccess)
            {
                foreach (var district in avaiableDistricts.Data)
                {
                    items.Add(new SelectListItem
                              {
                                      Value = district.Id.ToString(),
                                      Text = $"{district.Type} {district.Name}"
                              });
                }

                //insert special item for the default value
                PrepareDefaultItem(items,
                                   withSpecialDefaultItem,
                                   defaultItemText);
            }
        }

        public async Task PrepareWards(IList<SelectListItem> items,
                                       int districtId = 0,
                                       bool withSpecialDefaultItem = true,
                                       string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available wards
            var avaiableWards = await _wardService.GetAll(districtId);
            if(avaiableWards.IsSuccess)
            {
                foreach (var ward in avaiableWards.Data)
                {
                    items.Add(new SelectListItem
                              {
                                      Value = ward.Id.ToString(),
                                      Text = $"{ward.Type} {ward.Name}"
                              });
                }

                //insert special item for the default value
                PrepareDefaultItem(items,
                                   withSpecialDefaultItem,
                                   defaultItemText);
            }
        }

        public async Task PrepareBranches(IList<SelectListItem> items,
                                          bool withSpecialDefaultItem = true,
                                          string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available branches
            var avaiableBranches = await _branchService.GetAll();
            foreach (var branch in avaiableBranches)
            {
                items.Add(new SelectListItem
                          {
                                  Value = branch.Id.ToString(),
                                  Text = $"{branch.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareRoles(IList<SelectListItem> items,
                                       bool withSpecialDefaultItem = true,
                                       string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available roles
            var avaiableRoles = await _roleService.GetAll();
            foreach (var role in avaiableRoles)
            {
                items.Add(new SelectListItem
                          {
                                  Value = role.Id.ToString(),
                                  Text = $"{role.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareDepartments(IList<SelectListItem> items,
                                             bool withSpecialDefaultItem = true,
                                             string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available departments
            var avaiableDepartments = await _departmentService.GetAll();
            foreach (var department in avaiableDepartments)
            {
                items.Add(new SelectListItem
                          {
                                  Value = department.Id.ToString(),
                                  Text = $"{department.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareCustomerVips(IList<SelectListItem> items,
                                              bool withSpecialDefaultItem = true,
                                              string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available customer vips
            var avaiableCustomerVips = await _customerVipService.GetAll();
            foreach (var customerVip in avaiableCustomerVips)
            {
                items.Add(new SelectListItem
                          {
                                  Value = customerVip.Id.ToString(),
                                  Text = $"{customerVip.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareCustomerGroups(IList<SelectListItem> items,
                                                bool withSpecialDefaultItem = true,
                                                string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available customer groups
            var avaiableCustomerGroups = await _customerGroupService.GetAll();
            foreach (var customerGroup in avaiableCustomerGroups)
            {
                items.Add(new SelectListItem
                          {
                                  Value = customerGroup.Id.ToString(),
                                  Text = $"{customerGroup.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareProductGroups(IList<SelectListItem> items,
                                               bool withSpecialDefaultItem = true,
                                               string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available product groups
            var avaiableProductGroups = await _productGroupService.GetAll();
            foreach (var productGroup in avaiableProductGroups)
            {
                items.Add(new SelectListItem
                          {
                                  Value = productGroup.Id.ToString(),
                                  Text = $"{productGroup.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareProductUnits(IList<SelectListItem> items,
                                              bool withSpecialDefaultItem = true,
                                              string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available product groups
            var avaiableProductUnits = await _productUnitService.GetAll();
            foreach (var productUnit in avaiableProductUnits)
            {
                items.Add(new SelectListItem
                          {
                                  Value = productUnit.Id.ToString(),
                                  Text = $"{productUnit.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareProductStatuses(IList<SelectListItem> items,
                                                 bool withSpecialDefaultItem = true,
                                                 string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available product groups
            var avaiableProductStatuses = await _productStatusService.GetAll();
            foreach (var productStatus in avaiableProductStatuses)
            {
                items.Add(new SelectListItem
                          {
                                  Value = productStatus.Id.ToString(),
                                  Text = $"{productStatus.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareManufacturers(IList<SelectListItem> items,
                                               bool withSpecialDefaultItem = true,
                                               string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available product groups
            var avaiableManufacturers = await _manufacturerService.GetAll();
            foreach (var manufacturer in avaiableManufacturers)
            {
                items.Add(new SelectListItem
                          {
                                  Value = manufacturer.Id.ToString(),
                                  Text = $"{manufacturer.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareProducts(IList<SelectListItem> items,
                                          bool withSpecialDefaultItem = true,
                                          string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available product
            var avaiableProducts = await _productService.GetAll();
            foreach (var product in avaiableProducts)
            {
                items.Add(new SelectListItem
                          {
                                  Value = product.Id.ToString(),
                                  Text = $"{product.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareCrmStatuses(IList<SelectListItem> items,
                                             bool withSpecialDefaultItem = true,
                                             string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available crmStatus
            var avaiableCrmStatuses = await _crmStatusService.GetAll();
            foreach (var crmStatus in avaiableCrmStatuses)
            {
                items.Add(new SelectListItem
                          {
                                  Value = crmStatus.Id.ToString(),
                                  Text = $"{crmStatus.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareCrmPriorities(IList<SelectListItem> items,
                                               bool withSpecialDefaultItem = true,
                                               string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available crm priorities
            var avaiableCrmPriorities = await _crmPriorityService.GetAll();
            foreach (var crmPriority in avaiableCrmPriorities)
            {
                items.Add(new SelectListItem
                          {
                                  Value = crmPriority.Id.ToString(),
                                  Text = $"{crmPriority.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareCrmTypes(IList<SelectListItem> items,
                                          bool withSpecialDefaultItem = true,
                                          string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available crm types
            var avaiableCrmTypes = await _crmTypeService.GetAll();
            foreach (var crmType in avaiableCrmTypes)
            {
                items.Add(new SelectListItem
                          {
                                  Value = crmType.Id.ToString(),
                                  Text = $"{crmType.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareCustomerSources(IList<SelectListItem> items,
                                                 bool withSpecialDefaultItem = true,
                                                 string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available customer sources
            var avaiableCustomerSources = await _customerSourceService.GetAll();
            foreach (var customerSource in avaiableCustomerSources)
            {
                items.Add(new SelectListItem
                          {
                                  Value = customerSource.Id.ToString(),
                                  Text = $"{customerSource.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareUsers(IList<SelectListItem> items,
                                       bool withSpecialDefaultItem = true,
                                       string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available customer sources
            var avaiableUsers = await _userService.GetAll();
            foreach (var user in avaiableUsers)
            {
                items.Add(new SelectListItem
                          {
                                  Value = user.Id.ToString(),
                                  Text = $"{user.DisplayName}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareTaskTypes(IList<SelectListItem> items,
                                           bool withSpecialDefaultItem = true,
                                           string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available task types
            var avaiableTaskTypes = await _taskTypeService.GetAll();
            foreach (var taskType in avaiableTaskTypes)
            {
                items.Add(new SelectListItem
                          {
                                  Value = taskType.Id.ToString(),
                                  Text = $"{taskType.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareTaskStatuses(IList<SelectListItem> items,
                                              bool withSpecialDefaultItem = true,
                                              string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available taskStatus
            var avaiableTaskStatuses = await _taskStatusService.GetAll();
            foreach (var taskStatus in avaiableTaskStatuses)
            {
                items.Add(new SelectListItem
                          {
                                  Value = taskStatus.Id.ToString(),
                                  Text = $"{taskStatus.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareTaskPriorities(IList<SelectListItem> items,
                                                bool withSpecialDefaultItem = true,
                                                string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available taskPriority
            var avaiableTaskPriorities = await _taskPriorityService.GetAll();
            foreach (var taskPriority in avaiableTaskPriorities)
            {
                items.Add(new SelectListItem
                          {
                                  Value = taskPriority.Id.ToString(),
                                  Text = $"{taskPriority.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareTaskLoops(IList<SelectListItem> items,
                                           bool withSpecialDefaultItem = true,
                                           string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available task loops
            var avaiableTaskLoops = await _taskLoopService.GetAll();
            foreach (var taskLoop in avaiableTaskLoops)
            {
                items.Add(new SelectListItem
                          {
                                  Value = taskLoop.Id.ToString(),
                                  Text = $"{taskLoop.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PreparePayMethods(IList<SelectListItem> items,
                                            bool withSpecialDefaultItem = true,
                                            string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available pay methods
            var avaiablePayMethods = await _payMethodService.GetAll();
            foreach (var payMethod in avaiablePayMethods)
            {
                items.Add(new SelectListItem
                          {
                                  Value = payMethod.Id.ToString(),
                                  Text = $"{payMethod.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareInvoiceStatuses(IList<SelectListItem> items,
                                                 bool withSpecialDefaultItem = true,
                                                 string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available invoice statuses
            var avaiableInvoiceStatuses = await _invoiceStatusService.GetAll();
            foreach (var invoiceStatus in avaiableInvoiceStatuses)
            {
                items.Add(new SelectListItem
                          {
                                  Value = invoiceStatus.Id.ToString(),
                                  Text = $"{invoiceStatus.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareReceiptStatuses(IList<SelectListItem> items,
                                                 bool withSpecialDefaultItem = true,
                                                 string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available receipt statuses
            var avaiableReceiptStatuses = await _receiptStatusService.GetAll();
            foreach (var receiptStatus in avaiableReceiptStatuses)
            {
                items.Add(new SelectListItem
                          {
                                  Value = receiptStatus.Id.ToString(),
                                  Text = $"{receiptStatus.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        public async Task PrepareReceiptDescriptions(IList<SelectListItem> items,
                                                     bool withSpecialDefaultItem = true,
                                                     string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available pay methods
            var avaiableReceiptDescriptions = await _receiptDescriptionService.GetAll();
            foreach (var receiptDescription in avaiableReceiptDescriptions)
            {
                items.Add(new SelectListItem
                          {
                                  Value = receiptDescription.Id.ToString(),
                                  Text = $"{receiptDescription.Name}"
                          });
            }

            //insert special item for the default value
            PrepareDefaultItem(items,
                               withSpecialDefaultItem,
                               defaultItemText);
        }

        #endregion

        /// <summary>
        ///     Prepare default item
        /// </summary>
        /// <param name="items">Available items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use "All" text</param>
        protected virtual void PrepareDefaultItem(IList<SelectListItem> items,
                                                  bool withSpecialDefaultItem,
                                                  string defaultItemText = null)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            //whether to insert the first special item for the default value
            if(!withSpecialDefaultItem)
                return;

            //at now we use "0" as the default value
            const string value = "0";

            //prepare item text
            defaultItemText = defaultItemText ?? "Tất cả";

            //insert this default item at first
            items.Insert(0,
                         new SelectListItem
                         {
                                 Text = defaultItemText,
                                 Value = value
                         });
        }
    }
}
