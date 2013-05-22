using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureProductReportSearchModel
    {
        public string startUserName;
        public DateTime? startTimeStart;
        public DateTime? startTimeEnd;
        public string No;
        public string ComponentCode;
        public string ComponentName;
        public string BJXLH;
        public string JZXLH;
        public string GYSMC;
        public FailureResult Result;
    }
}