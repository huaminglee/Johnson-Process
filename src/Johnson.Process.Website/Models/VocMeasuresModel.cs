using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocMeasuresModel
    {
        public string taskId { set; get; }

        public string submitRemark { set; get; }

        public string measures { set; get; }

        public List<UploadFileModel> measuresFiles;
    }
}