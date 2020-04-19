using System;
using System.Collections.Generic;

namespace Pelo.v2.Web.Models.Warranty
{
    public class WarrantyLogDetail
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Avatar { get; set; }

        public string LogDate { get; set; }

        public string Content { get; set; }

        public List<AttachmentModel> AttachmentModels { get; set; }

    }

    public class AttachmentModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsImage { get; set; }
    }
}
