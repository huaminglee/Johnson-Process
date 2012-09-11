using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocResponsibleReasonModel
    {
        public string taskId { set; get; }

        public string measureUserName { set; get; }

        public string measureUserAccount { set; get; }

        public string submitRemark { set; get; }

        public string reason { set; get; }

        public List<UploadFileModel> reasonFiles;
    }
}