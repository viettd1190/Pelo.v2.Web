using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Services.Branch;
using Pelo.v2.Web.Services.Department;
using Pelo.v2.Web.Services.Province;
using Pelo.v2.Web.Services.Role;

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
    }

    public class BaseModelFactory : IBaseModelFactory
    {
        private readonly IBranchService _branchService;

        private readonly IDistrictService _districtService;

        private readonly IProvinceService _provinceService;

        private readonly IWardService _wardService;

        private readonly IDepartmentService _departmentService;

        private readonly IRoleService _roleService;

        public BaseModelFactory(IProvinceService provinceService,
                                IDistrictService districtService,
                                IWardService wardService,
                                IBranchService branchService,
                                IRoleService roleService,
                                IDepartmentService departmentService)
        {
            _provinceService = provinceService;
            _districtService = districtService;
            _wardService = wardService;
            _branchService = branchService;
            _roleService = roleService;
            _departmentService = departmentService;
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
            foreach (var role in avaiableDepartments)
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
