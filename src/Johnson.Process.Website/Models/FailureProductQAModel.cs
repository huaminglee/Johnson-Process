using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureProductQAModel
    {
        public FailureResult QAResult { set; get; }

        public string ReceiveQARemark { set; get; }

        public string submitRemark { set; get; }
    }
}