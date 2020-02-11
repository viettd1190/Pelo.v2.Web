using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.CrmStatus
{
    public class CrmStatusModel
    {
        public CrmStatusModel()
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

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("issend_sms")]
        public bool IsSendSms{ get; set; }

        [JsonProperty("sms_content")]
        public string SmsContent { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
