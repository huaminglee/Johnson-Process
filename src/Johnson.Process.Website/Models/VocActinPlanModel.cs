using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocActinPlanModel
    {
        public string taskId;

        public List<VocActionModel> actions;

        public string solutions { set; get; }

        public List<UploadFileModel> solutionsFiles { set; get; }

        public string submitRemark;
    }
}