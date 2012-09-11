using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureProductMrbModel
    {
        public FailureResult MrbResult { set; get; }
        public string submitRemark { set; get; }
    }
}