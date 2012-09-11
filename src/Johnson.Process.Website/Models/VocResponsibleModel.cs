using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocResponsibleModel
    {
        public string taskId { set; get; }

        public string responsibleUserPreviousAccount { set; get; }

        public string responsibleUserPreviousName { set; get; }

        public string submitRemark { set; get; }

        public string reason { set; get; }

        public string measures { set; get; }

        public List<UploadFileModel> reasonFiles;

        public List<UploadFileModel> measuresFiles;
    }
}