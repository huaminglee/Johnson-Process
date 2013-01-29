using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class OrderPingShenReportSearchModel
    {
        public string startUserName;
        public DateTime? startTimeStart;
        public DateTime? startTimeEnd;
        public int taskStatus;
        public DateTime? jiaoHuoRiQiStart;
        public DateTime? jiaoHuoRiQiEnd;
        public string SONO;
        public string JDSNO;
        public string tuZiQueRen;
        public string xiangMingCheng;
        public string banShiChu;
        public string banShiChuLianXiRen;
        public bool? isStandard;
        public string chanPinLeiXing;
        public string sapItem;
    }
}