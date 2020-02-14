using Newtonsoft.Json;
using System;

namespace Pelo.v2.Web.Models.TaskModel
{
    public class TaskModel
    {
        public TaskModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("status_color")] public string StatusColor { get; set; }

        [JsonProperty("priority_color")] public string PriorityColor { get; set; }

        [JsonProperty("task_loop_name")] public string TaskLoopName { get; set; }

        [JsonProperty("task_status_name")] public string TaskStatusName { get; set; }

        [JsonProperty("task_priority_name")] public string TaskPriorityName { get; set; }

        [JsonProperty("user_phone_created")] public string UserPhoneCreated { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("customer_name")] public string CustomerName { get; set; }

        [JsonProperty("customer_phone")] public string CustomerPhone { get; set; }

        [JsonProperty("user_name_care")] public string UserNameCare { get; set; }

        [JsonProperty("user_phone_care")] public string UserPhoneCare { get; set; }

        [JsonProperty("content")] public string Content { get; set; }

        [JsonProperty("user_name_created")] public string UserNameCreated { get; set; }

        [JsonProperty("from_date_time")] public DateTime FromDateTime { get; set; }

        [JsonProperty("to_date_time")] public DateTime ToDateTime { get; set; }

        [JsonProperty("customer_address")] public string CustomerAddress { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
