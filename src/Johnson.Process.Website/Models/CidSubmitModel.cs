using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class CidSubmitModel
    {
        public string taskId;
        public List<ProcessFile> cidZiLiao;
        public string submitRemark;

    }
}