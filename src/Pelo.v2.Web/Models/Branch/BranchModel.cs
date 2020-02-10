using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Branch
{
    public class BranchModel
    {
        public BranchModel()
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

        [JsonProperty("province")]
        public string Province { get; set; }

        [JsonProperty("hotline")]
        public string Hotline { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("ward_id")]
        public string Ward { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
