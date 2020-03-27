using System;
using System.Collections.Generic;
using Pelo.v2.Web.Models.Customer;

namespace Pelo.v2.Web.Models.Invoice
{
    public class InvoiceDetailModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int InvoiceStatusId { get; set; }

        public int CustomerId { get; set; }

        public CustomerDetailModel Customer { get; set; }

        public DateTime DateCreated { get; set; }

        public string UserCreated { get; set; }

        public string UserCreatedPhone { get; set; }

        public string UserSell { get; set; }

        public string UserSellPhone { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int DeliveryCost { get; set; }

        public List<int> UserDeliveryIds { get; set; }

        public List<int> UserCareIds { get; set; }

        public int Deposit { get; set; }

        public int Discount { get; set; }

        public List<ProductInInvoiceDetailModel> Products { get; set; }

        public string Description { get; set; }

        public string PayMethod { get; set; }
    }

    public class ProductInInvoiceDetailModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int ImportPrice { get; set; }

        public int SellPrice { get; set; }

        public int Quantity { get; set; }
    }
}
