using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.ProductStatus
{
    public class ProductStatusModel
    {
        public ProductStatusModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }        

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
