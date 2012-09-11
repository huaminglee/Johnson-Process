using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureStartModel
    {
        public string ComponentCode { set; get; }

        public string ComponentName { set; get; }

        public string OrderCode { set; get; }

        public string FailurePlace { set; get; }

        public string Supplier { set; get; }

        public FailureSource Source { set; get; }

        public string Quantity { set; get; }

        public FailureReason Reason { set; get; }

        public string ReasonRemark { set; get; }

        public string Remark { set; get; }

        public string PmcUserAccount { set; get; }

        public string PmcUserName { set; get; }

        public string QEUserAccount { set; get; }

        public string QEUserName { set; get; }

        public string submitRemark { set; get; }
    }
}