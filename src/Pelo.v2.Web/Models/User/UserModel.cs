using System;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.User
{
    public class UserModel
    {
        public UserModel()
        {
            if(PageSize < 1)
            {
                PageSize = 20;
            }
        }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("Branch")]
        public string Branch { get; set; }

        [JsonProperty("Role")]
        public string Role { get; set; }

        [JsonProperty("Department")]
        public string Department { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("DateCreated")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("PageSize")]
        public int PageSize { get; set; }

        [JsonProperty("PageSizeOptions")]
        public string PageSizeOptions { get; set; }
    }
}
