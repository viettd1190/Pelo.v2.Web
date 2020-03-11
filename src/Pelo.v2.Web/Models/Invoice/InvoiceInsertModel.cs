using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
            Products = new List<ProductInInvoiceModel>();
        }

        public CustomerDetailModel Customer { get; set; }

        public int PayMethodId { get; set; }

        public int Discount { get; set; }

        public int Deposit { get; set; }

        public int BranchId { get; set; }

        [UIHint("DateTime")]
        public DateTime DeliveryDate { get; set; }

        public string Description { get; set; }

        public int CustomerId { get; set; }

        public int UserSellId { get; set; }

        public IList<SelectListItem> AvaiableBranches { get; set; }

        public IList<SelectListItem> AvaiablePayMethods { get; set; }

        public IList<SelectListItem> AvaiableProducts { get; set; }

        public IList<SelectListItem> AvaiableUsers { get; set; }

        public IList<ProductInInvoiceModel> Products { get; set; }

        public string ProductRaw { get; set; }
    }

    public class ProductInInvoiceModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public int SellPrice { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("amount")]
        public int Amount => SellPrice * Quantity;
    }
}
