using System;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Crm
{
    public class CrmModel
    {
        public CrmModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CrmStatus")]
        public string CrmStatus { get; set; }

        [JsonProperty("CrmStatusColor")]
        public string CrmStatusColor { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty("CustomerPhone1")]
        public string CustomerPhone1 { get; set; }

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

        [JsonProperty("CustomerGroup")]
        public string CustomerGroup { get; set; }

        [JsonProperty("CustomerVip")]
        public string CustomerVip { get; set; }

        [JsonProperty("Need")]
        public string Need { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("ProductGroup")]
        public string ProductGroup { get; set; }

        [JsonProperty("CrmPriority")]
        public string CrmPriority { get; set; }

        [JsonProperty("CustomerSource")]
        public string CustomerSource { get; set; }

        [JsonProperty("CrmType")]
        public string CrmType { get; set; }

        [JsonProperty("Visit")]
        public string Visit { get; set; }

        [JsonProperty("UserCreated")]
        public string UserCreated { get; set; }

        [JsonProperty("UserCares")]
        public string UserCares { get; set; }

        [JsonProperty("ContactDate")]
        public DateTime ContactDate { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
