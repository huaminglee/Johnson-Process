using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationEngModel
    {
        public string taskId;
        public bool needCidReply;
        public bool needQadReply;
        public string qadEngineer;
        public string cidEngineer;
        public string engReply;
        public string submitRemark;
    }
}