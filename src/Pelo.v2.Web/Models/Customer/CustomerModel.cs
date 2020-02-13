using System;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Customer
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Phone2")]
        public string Phone2 { get; set; }

        [JsonProperty("Phone3")]
        public string Phone3 { get; set; }

        [JsonProperty("Province")]
        public string Province { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }

        [JsonProperty("Ward")]
        public string Ward { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("CustomerGroup")]
        public string CustomerGroup { get; set; }

        [JsonProperty("CustomerVip")]
        public string CustomerVip { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DateUpdated")]
        public DateTime DateUpdated { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
