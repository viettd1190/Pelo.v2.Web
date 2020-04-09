using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Pelo.v2.Web.Models.Candidate
{
    public class CandidateSearchModel : BaseSearchModel
    {
        public CandidateSearchModel()
        {
            AvaiableCandidateStatus = new List<SelectListItem>();
            FromDate = string.Empty;
            ToDate = string.Empty;
        }
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Code { get; set; }

        public string CandidateStatusId { get; set; }

        [DataType(DataType.Date)]
        [UIHint("DateTimeEn_Insert")]
        [UIHint("DateAjaxCalendar")]
        [UIHint("DateOnly")]
        [DefaultValue(null)]
        public string FromDate { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [UIHint("DateTimeEn_Insert")]
        [UIHint("DateAjaxCalendar")]
        [UIHint("DateOnly")]
        [DefaultValue(null)]
        public string ToDate { get; set; } = string.Empty;

        public string ColumnOrder{ get; set; }

        public IList<SelectListItem> AvaiableCandidateStatus{ get; set; }
    }
}
