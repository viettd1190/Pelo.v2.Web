using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("invoice_status")] public string InvoiceStatus { get; set; }

        [JsonProperty("invoice_status_color")] public string InvoiceStatusColor { get; set; }

        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("customer")] public string CustomerName { get; set; }

        [JsonProperty("customer_phone")] public string CustomerPhone { get; set; }

        [JsonProperty("customer_phone_2")] public string CustomerPhone2 { get; set; }

        [JsonProperty("customer_phone_3")] public string CustomerPhone3 { get; set; }

        [JsonProperty("province")] public string Province { get; set; }

        [JsonProperty("district")] public string District { get; set; }

        [JsonProperty("ward")] public string Ward { get; set; }

        [JsonProperty("customer_address")] public string CustomerAddress { get; set; }

        [JsonProperty("customer_code")] public string CustomerCode { get; set; }

        [JsonProperty("branch")] public string Branch { get; set; }

        [JsonProperty("user_created")] public string UserCreated { get; set; }

        [JsonProperty("user_created_phone")] public string UserCreatedPhone { get; set; }

        [JsonProperty("user_sell")] public string UserSell { get; set; }

        [JsonProperty("user_sell_phone")] public string UserSellPhone { get; set; }

        [JsonProperty("delivery_date")] public DateTime DeliveryDate { get; set; }

        [JsonProperty("date_created")] public DateTime DateCreated { get; set; }

        [JsonProperty("products")] public List<ProductInInvoiceSimpleList> Products { get; set; } = new List<ProductInInvoiceSimpleList>();

        [JsonProperty("users_delivery")] public List<UserDisplaySimpleList> UsersDelivery { get; set; } = new List<UserDisplaySimpleList>();

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
