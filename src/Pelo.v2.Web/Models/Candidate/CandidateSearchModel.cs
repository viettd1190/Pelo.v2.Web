using System;

namespace Pelo.v2.Web.Models.Candidate
{
    public class CandidateSearchModel : BaseSearchModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Code { get; set; }

        public string CandidateStatusId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ColumnOrder{ get; set; }
    }
}
