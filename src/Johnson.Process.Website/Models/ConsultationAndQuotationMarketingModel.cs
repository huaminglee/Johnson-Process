using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationMarketingModel
    {
        public string taskId;
        public bool needCsdReply;
        public string csdEngineerAccount;
        public string csdEngineerName;
        public string marketingReply;
        public string submitRemark;
        public int? leadTime;
        public string leadTimeRemark;
        public string toCsdEmailAddress;
        public List<UploadFileModel> files;
        public List<ConsultationAndQuotationProductModel> products;
    }
}