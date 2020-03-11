using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Pelo.v2.Web.Models.User
{
    public class UpdateUserModel
    {
        public UpdateUserModel()
        {
            AvaiableBranches = new List<SelectListItem>();
            AvaiableDepartments = new List<SelectListItem>();
            AvaiableRoles = new List<SelectListItem>();
            AvaiableStatuses = new List<SelectListItem>();
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

        [JsonProperty("BranchId")]
        public int BranchId { get; set; }

        [JsonProperty("RoleId")]
        public int RoleId { get; set; }

        [JsonProperty("DepartmentId")]
        public int DepartmentId { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Avatar")]
        public string Avatar { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("ConfirmPassword")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public IList<SelectListItem> AvaiableBranches { get; set; }

        public IList<SelectListItem> AvaiableDepartments { get; set; }

        public IList<SelectListItem> AvaiableRoles { get; set; }

        public IList<SelectListItem> AvaiableStatuses { get; set; }
    }
}
