using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class QadSubmitModel
    {
        public string taskId;
        public List<ProcessFile> qadZiLiao;
        public string submitRemark;
    }
}