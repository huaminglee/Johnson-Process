using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationReportSearchModel
    {
        public string applyUserName;
        public DateTime? applyTimeStart;
        public DateTime? applyTimeEnd;
        public int taskStatus;

        public string applyUserDepartmentName;
        public string projectName;
        public string marketingEngineer;
        public DateTime? expectSignContactDateStart;
        public DateTime? expectSignContactDateEnd;
    }
}