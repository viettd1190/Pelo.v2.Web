using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.CustomerVip
{
    public class CustomerVipModel
    {
        public CustomerVipModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("Profit")]
        public int Profit { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
