using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Invoice
{
    public class InvoiceModel
    {
        public InvoiceModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("InvoiceStatus")]
        public string InvoiceStatus { get; set; }

        [JsonProperty("InvoiceStatusColor")]
        public string InvoiceStatusColor { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Customer")]
        public string Customer { get; set; }

        [JsonProperty("CustomerPhone")]
        public string CustomerPhone { get; set; }

        [JsonProperty("CustomerPhone2")]
        public string CustomerPhone2 { get; set; }

        [JsonProperty("CustomerPhone3")]
        public string CustomerPhone3 { get; set; }

        [JsonProperty("Province")]
        public string Province { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }

        [JsonProperty("Ward")]
        public string Ward { get; set; }

        [JsonProperty("CustomerAddress")]
        public string CustomerAddress { get; set; }

        [JsonProperty("CustomerCode")]
        public string CustomerCode { get; set; }

        [JsonProperty("Branch")]
        public string Branch { get; set; }

        [JsonProperty("UserCreated")]
        public string UserCreated { get; set; }

        [JsonProperty("UserCreatedPhone")]
        public string UserCreatedPhone { get; set; }

        [JsonProperty("UserSell")]
        public string UserSell { get; set; }

        [JsonProperty("UserSellPhone")]
        public string UserSellPhone { get; set; }

        [JsonProperty("DeliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("Products")]
        public List<ProductInInvoiceSimpleList> Products { get; set; } = new List<ProductInInvoiceSimpleList>();

        [JsonProperty("UserDeliveries")]
        public List<UserDisplaySimpleList> UserDeliveries { get; set; } = new List<UserDisplaySimpleList>();

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
