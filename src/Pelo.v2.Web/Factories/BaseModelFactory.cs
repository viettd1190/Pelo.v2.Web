using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Services.Province;

namespace Pelo.v2.Web.Factories
{
    public interface IBaseModelFactory
    {
        /// <summary>
        ///     Prepare available stores
        /// </summary>
        /// <param name="items">Store items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        Task PrepareProvinces(IList<SelectListItem> items,
                              bool withSpecialDefaultItem = true,
                              string defaultItemText = null);
    }

    public class BaseModelFactory : IBaseModelFactory
    {
        private readonly IProvinceService _provinceService;

        public BaseModelFactory(IProvinceService provinceService)
        {
            _provinceService = provinceService;
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
            foreach (var province in avaiableProvinces)
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
