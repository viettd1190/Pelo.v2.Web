using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Pelo.v2.Web.Models.Candidate
{
    public class CandidateSearchModel : BaseSearchModel
    {
        public CandidateSearchModel()
        {
            AvaiableCandidateStatus = new List<SelectListItem>();
        }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Code { get; set; }

        public string CandidateStatusId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ColumnOrder{ get; set; }

        public IList<SelectListItem> AvaiableCandidateStatus{ get; set; }
    }
}
