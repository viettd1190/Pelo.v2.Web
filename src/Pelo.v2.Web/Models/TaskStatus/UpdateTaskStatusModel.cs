using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.TaskStatus
{
    public class UpdateTaskStatusModel
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SortOrder")]
        public int SortOrder { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("IsSendSms")]
        public bool IsSendSms { get; set; }

        [JsonProperty("SmsContent")]
        public string SmsContent { get; set; }
    }
}
