using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class DeliveryReportModel
    {
        public DeliveryReportModel()
        {

        }

        public DeliveryReportModel(ProcessForm<DeliveryProcessForm> processForm)
        {
            DeliveryProcessForm form = processForm.Form;
            this.applyUserName = form.ApplyUserName;
            this.applyTime = form.ApplyTime.ToString("yyyy-MM-dd");
            this.orderNumber = form.OrderNumber;
            this.projectName = form.ProjectName; 
            this.bookDate = form.BookDate.ToString("yyyy-MM-dd");
            this.requestOutDate = form.RequestOutDate.ToString("yyyy-MM-dd");
            this.saleEngineerYT = form.SaleEngineer;
            this.saleGroup = form.SaleGroup;
            this.saleOffice = form.SaleOffice;
#if DEBUG
            this.taskStatus = 1;
#else
            Task task = WebHelper.VocProcess.GetStartTask(processForm.IncidentNo);
            this.taskId = task.strTaskId;
            this.incidentNo = task.nIncidentNo;
            this.taskStatus = WebHelper.VocProcess.GetIncidentStatus(task.nIncidentNo);
#endif
        }
        public string applyUserName;
        public string applyTime;
        public string taskId;
        public int incidentNo;
        public int taskStatus;
        public string orderNumber;
        public string projectName;
        public string saleOffice;
        public string saleGroup;
        public string saleEngineerYT;
        public string bookDate;
        public string requestOutDate;
    }
}