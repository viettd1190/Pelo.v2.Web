using System;

namespace Pelo.v2.Web.Models.Recruitment
{
    public class RecruitmentSearchModel : BaseSearchModel
    {
        public string Name { get; set; }

        public string CandidateStatusId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string ColumnOrder{ get; set; }
    }
}
