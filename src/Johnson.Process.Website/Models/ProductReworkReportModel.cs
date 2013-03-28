using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkReportModel
    {
        public ProductReworkReportModel()
        {

        }

        public ProductReworkReportModel(ProcessForm<ProductReworkForm> processForm)
        {
            ProductReworkForm form = processForm.Form;
            this.startUserName = form.StartUserName;
            this.startTime = form.StartTime.ToString("yyyy-MM-dd");
            this.FailureNo = form.FailureNo;
            this.ProductType = this.Map(form.ProductType);
            this.XLH = form.XLH;
            this.Name = form.Name;
            this.SapNo = form.SapNo;
            this.Quantity = form.Quantity;
            this.OrderNumber = form.OrderNumber;
            this.StartDepartment = form.StartDepartment;
            this.incidentNo = processForm.IncidentNo;
            this.GS = form.GS;
            this.GSFY = form.GSFY.ToString();
            this.WLFY = form.WLFY.ToString();
            this.ZFY = form.ZFY.ToString();
        }

        private string Map(ProductType productType)
        {
            if (productType == Core.ProductType.CP)
            {
                return "产品";
            }
            else if (productType == Core.ProductType.LJ)
            {
                return "零部件";
            }
            return "";
        }

        public string startUserName;
        public string startTime;
        public string FailureNo;
        public string ProductType;
        public string XLH;
        public string Name;
        public string SapNo;
        public string Quantity;
        public string OrderNumber;
        public string StartDepartment;
        public string taskId;
        public int incidentNo;
        public string GS { set; get; }

        /// <summary>
        /// 工时费用
        /// </summary>
        public string GSFY { set; get; }

        /// <summary>
        /// 物料费用
        /// </summary>
        public string WLFY { set; get; }

        /// <summary>
        /// 总费用
        /// </summary>
        public string ZFY { set; get; }
    }
}