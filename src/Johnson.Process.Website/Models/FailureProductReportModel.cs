using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class FailureProductReportModel
    {
        public FailureProductReportModel()
        {

        }

        public FailureProductReportModel(ProcessForm<FailureProductForm> processForm)
        {
            FailureProductForm form = processForm.Form;
            this.startUserName = form.StartUserName;
            this.startTime = form.StartTime.ToString("yyyy-MM-dd");
            this.No = form.No;
            this.ComponentCode = form.ComponentCode;
            this.ComponentName = form.ComponentName;
            this.BJXLH = form.BJXLH;
            this.JZXLH = form.JZXLH;
            this.GYSMC = form.GYSMC;
            this.ZRBM = form.ZRBM;
            this.Quantity = form.Quantity;
            this.qeResult = FailureResultHelper.MapName(form.QEResult); 
            this.incidentNo = processForm.IncidentNo;
        }

        public string startUserName;
        public string startTime;
        public int incidentNo;
        public string No;
        public string ComponentCode;
        public string ComponentName;
        public string BJXLH;
        public string JZXLH;
        public string GYSMC;
        public string ZRBM;
        public string Quantity;
        public string qeResult;
    }
}