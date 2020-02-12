using System;

namespace Pelo.v2.Web.Models.Invoice
{
    public class InvoiceSearchModel : BaseSearchModel
    {
        public string CustomerCode { get; set; }

        public string CustomerName { get; set; }

        public string Code { get; set; }

        public string Phone { get; set; }

        public int BranchId { get; set; }

        public int InvoiceStatusId { get; set; }

        public int UserSellId { get; set; }

        public int UserDeliveryId { get; set; }

        public int UserCreated { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ColumnOrder{ get; set; }
    }
}
