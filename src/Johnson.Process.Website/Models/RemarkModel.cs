using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class RemarkModel
    {
        public RemarkModel()
        {
        }

        public RemarkModel(TaskApproveInfo taskApproveInfo)
        {
            this.remark = taskApproveInfo.Remark;
            this.remarkStepName = taskApproveInfo.StepName;
            this.remarkTime = taskApproveInfo.ApproveTime.ToString();
            this.remarkUserName = taskApproveInfo.ApproveUserName;
        }

        public string remarkStepName;

        public string remarkUserName;

        public string remarkTime;

        public string remark;
    }
}