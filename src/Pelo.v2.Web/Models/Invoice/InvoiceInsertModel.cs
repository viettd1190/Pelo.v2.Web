using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Models.Invoice
{
    public class InvoiceInsertModel
    {
        public InvoiceInsertModel()
        {
            AvaiableBranches = new List<SelectListItem>();
            AvaiableUsers = new List<SelectListItem>();
            AvaiablePayMethods = new List<SelectListItem>();
            AvaiableProducts = new List<SelectListItem>();
            Products=new List<ProductInInvoiceModel>();
        }

        public CustomerDetailModel Customer { get; set; }

        public int PayMethodId { get; set; }

        public int Discount { get; set; }

        public int Deposit { get; set; }

        public int BranchId { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string Description { get; set; }

        public int CustomerId { get; set; }

        public int UserSellId { get; set; }

        public IList<SelectListItem> AvaiableBranches { get; set; }

        public IList<SelectListItem> AvaiablePayMethods { get; set; }

        public IList<SelectListItem> AvaiableProducts { get; set; }

        public IList<SelectListItem> AvaiableUsers { get; set; }

        public IList<ProductInInvoiceModel> Products { get; set; }

        
    }

    public class ProductInInvoiceModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int SellPrice { get; set; }

        public int Quantity { get; set; }

        public string Amount { get; set; }
    }
}
