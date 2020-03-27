using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Candidate
{
    public class UpdateCandidateModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int CandidateStatusId { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
