using Pelo.v2.Web.Models.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pelo.v2.Web.Models.Candidate
{
    public class CandidateLogDetail
    {
        public CandidateLogDetail() { }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string Content { get; set; }
        public string LogDate { get; set; }
        public List<AttachmentModel> AttachmentModels { get; set; }
    }
}
