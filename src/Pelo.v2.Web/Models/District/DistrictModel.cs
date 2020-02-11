using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.District
{
    public class DistrictModel
    {
        public DistrictModel()
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

        [JsonProperty("SortOrder")]
        public int SortOrder { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
