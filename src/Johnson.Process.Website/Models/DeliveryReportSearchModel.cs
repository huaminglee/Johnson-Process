using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class DeliveryReportSearchModel
    {
        public string applyUserName;
        public DateTime? applyTimeStart;
        public DateTime? applyTimeEnd;
        public int taskStatus;

        public string orderNumber;
        public string projectName;
        public string saleOffice;
        public string saleGroup;
        public string saleEngineerYT;
        public DateTime? bookDateStart;
        public DateTime? bookDateEnd;
        public DateTime? requestOutDateStart;
        public DateTime? requestOutDateEnd;
    }
}