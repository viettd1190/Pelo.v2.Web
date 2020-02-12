using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Invoice
{
    public class ProductInInvoiceSimpleList
    {
        public ProductInInvoiceSimpleList()
        {

        }
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
