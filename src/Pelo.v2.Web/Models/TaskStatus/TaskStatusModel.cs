using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.TaskStatus
{
    public class TaskStatusModel
    {
        public TaskStatusModel()
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

        [JsonProperty("sort_order")]
        public int SortOrder { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("send_sms")]
        public bool IsSendSms{ get; set; }

        [JsonProperty("sms_content")]
        public string SmsContent { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
