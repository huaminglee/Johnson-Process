using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationCsdModel
    {
        public string taskId;
        public bool needTrace;
        public string csdTracerAccount;
        public string csdTracerName;
        public string csdReply;
        public string csdEmailTo;
        public string submitRemark;
        public int? leadTime;
        public string leadTimeRemark;
        public List<ConsultationAndQuotationProductModel> products;
    }
}