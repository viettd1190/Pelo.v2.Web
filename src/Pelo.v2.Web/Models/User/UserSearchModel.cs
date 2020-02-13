using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Pelo.v2.Web.Models.User
{
    public class UserSearchModel : BaseSearchModel
    {
        public UserSearchModel()
        {
            AvaiableBranches = new List<SelectListItem>();
            AvaiableDepartments = new List<SelectListItem>();
            AvaiableRoles = new List<SelectListItem>();
            AvaiableStatuses = new List<SelectListItem>();
        }

        public string Code { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int BranchId { get; set; }

        public int DepartmentId { get; set; }

        public int RoleId { get; set; }

        public int Status { get; set; }

        public IList<SelectListItem> AvaiableBranches { get; set; }

        public IList<SelectListItem> AvaiableDepartments { get; set; }

        public IList<SelectListItem> AvaiableRoles { get; set; }

        public IList<SelectListItem> AvaiableStatuses { get; set; }
    }
}
