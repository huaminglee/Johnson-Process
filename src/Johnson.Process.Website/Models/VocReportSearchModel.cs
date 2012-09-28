using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocReportSearchModel
    {
        public string applyUserName;
        public string applyUserDepartmentName;
        public DateTime? applyTimeStart;
        public DateTime? applyTimeEnd;
        public string projectName;
        public string machineModel;
        public string machineCode;
        public string vocCode;
        public string faultRemark;
        public int taskStatus;
    }
}