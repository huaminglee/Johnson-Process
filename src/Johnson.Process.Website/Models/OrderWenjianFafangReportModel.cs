using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class OrderWenjianFafangReportModel
    {
        public OrderWenjianFafangReportModel(OrderWenjianFafangForm form)
        {
            this.startUserName = form.StartUserName;
            this.startTime = form.StartTime.ToString("yyyy-MM-dd");
            this.hasXinWuLiao = form.HasXinWuLiao;
            this.sheJiShuoMing = form.SheJiShuoMing;
            this.fafangWancheng = form.FafangWancheng ? "全部完成" : "部分完成";
            this.incidentNo = form.IncidentNo;
        }
        public string startUserName;
        public string startTime;
        public bool hasXinWuLiao;
        public string sheJiShuoMing;
        public string fafangWancheng;
        public int incidentNo;
    }
}