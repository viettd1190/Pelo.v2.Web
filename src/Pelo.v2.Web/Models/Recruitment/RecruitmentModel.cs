﻿using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.Recruitment
{
    public class RecruitmentModel
    {
        public RecruitmentModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")] public int Id { get; set; }

        [JsonProperty("Name")] public string Name { get; set; }

        [JsonProperty("Color")] public string Color { get; set; }

        [JsonProperty("Code")] public string Code { get; set; }

        [JsonProperty("RecruitmentStatusName")] public string RecruitmentStatusName { get; set; }

        [JsonProperty("Description")] public string Description { get; set; }

        [JsonProperty("UserNameCreated")] public string UserNameCreated { get; set; }

        [JsonProperty("UserPhoneCreated")] public string UserPhoneCreated { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
