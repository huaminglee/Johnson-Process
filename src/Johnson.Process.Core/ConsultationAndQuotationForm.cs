using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class ConsultationAndQuotationForm
    {
        public string ApplyUserName { set; get; }
        public string ApplyUserDepartmentName { set; get; }
        public DateTime ApplyTime { set; get; }
        public string ProjectName { set; get; }
        public string SucceedProbability { set; get; }
        public DateTime ExpectSignContact { set; get; }
        public string MarketingEngineer { set; get; }
        public string MarketingEmailTo { set; get; }
        public string CsdTracerEmailTo { set; get; }

        public string MarketingReply { set; get; }
        public string EngReply { set; get; }
        public string ScmReply { set; get; }
        public string LogReply { set; get; }
        public string QadReply { set; get; }
        public string CidReply { set; get; }
        public string CsdReply { set; get; }
        public string CsdEmailTo { set; get; }

        public string CsdTracerName { set; get; }
        public string CsdTracerAccount { set; get; }
        public string CsdEngineerName { set; get; }
        public string CsdEngineerAccount { set; get; }

        public int? LeadTime { set; get; }
        public string LeadTimeRemark { set; get; }

        public bool CidExecuted { set; get; }
        public bool QadExecuted { set; get; }
        public bool ScmExecuted { set; get; }
        public bool LogExecuted { set; get; }

        public List<ConsultationAndQuotationProductInfo> Products { set; get; }
        public List<ProcessFile> Files { set; get; }
        public List<TaskApproveInfo> Approves { set; get; }
    }
}
