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

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Province")]
        public string Province { get; set; }

        [JsonProperty("Hotline")]
        public string Hotline { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }

        [JsonProperty("Ward")]
        public string Ward { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
