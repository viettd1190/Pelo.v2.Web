using Newtonsoft.Json;

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

        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("color")] public string Color { get; set; }

        [JsonProperty("code")] public string Code { get; set; }

        [JsonProperty("recruitment_status_name")] public string RecruitmentStatusName { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("user_name_created")] public string UserNameCreated { get; set; }

        [JsonProperty("user_phone_created")] public string UserPhoneCreated { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
