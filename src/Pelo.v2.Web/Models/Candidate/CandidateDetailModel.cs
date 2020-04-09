using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Candidate
{
    public class CandidateDetailModel
    {
        public CandidateDetailModel()
        {
            AvaiableCandidateStatuses = new List<SelectListItem>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int CandidateStatusId { get; set; }

        public string Code { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string UserNameCreated { get; set; }

        public string UserPhoneCreated { get; set; }

        public DateTime DateCreated { get; set; }
        public IList<SelectListItem> AvaiableCandidateStatuses { get; set; }
    }
}
