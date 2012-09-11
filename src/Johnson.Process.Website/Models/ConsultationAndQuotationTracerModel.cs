using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationTracerModel
    {
        public string taskId;
        public bool needEngReply;
        public bool needLogReply;
        public bool needScmReply;
        public string logEngineer;
        public string engEngineer;
        public string scmEngineer;
        public string csdReply;
        public string submitRemark;
        public int? leadTime;
        public string leadTimeRemark;
        public string csdTracerEmailTo;
        public List<ConsultationAndQuotationProductModel> products;
    }
}