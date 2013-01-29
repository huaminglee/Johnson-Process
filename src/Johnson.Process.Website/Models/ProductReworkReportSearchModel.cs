using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkReportSearchModel
    {
        public string startUserName;
        public DateTime? startTimeStart;
        public DateTime? startTimeEnd;
        public string FailureNo;
        public ProductType? ProductType;
        public string XLH;
        public string Name;
        public string SapNo;
        public string Quantity;
        public string OrderNumber;
        public string StartDepartment;
    }
}