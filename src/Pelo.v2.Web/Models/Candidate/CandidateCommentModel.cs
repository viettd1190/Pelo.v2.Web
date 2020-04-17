using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Candidate
{
    public class CandidateCommentModel
    {
        public int Id { get; set; }

        public string Comment { get; set; }

        public int CandidateStatusId { get; set; }
    }
}
