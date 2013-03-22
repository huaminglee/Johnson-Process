using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationReportModel
    {
        public ConsultationAndQuotationReportModel()
        {

        }

        public ConsultationAndQuotationReportModel(ProcessForm<ConsultationAndQuotationForm> processForm)
        {
            ConsultationAndQuotationForm form = processForm.Form;
            this.applyUserName = form.ApplyUserName;
            this.applyTime = form.ApplyTime.ToString("yyyy-MM-dd");
            this.applyUserDepartmentName = form.ApplyUserDepartmentName;
            this.expectSignContact = form.ExpectSignContact.ToString("yyyy-MM-dd");
            this.projectName = form.ProjectName;
            this.succeedProbability = form.SucceedProbability;
            this.marketingEngineer = form.MarketingEngineer;

            this.incidentNo = processForm.IncidentNo;
        }
        public string applyUserName;
        public string applyTime;
        public string applyUserDepartmentName;
        public int incidentNo;
        public string projectName;
        public string succeedProbability;
        public string expectSignContact;
        public string marketingEngineer;
    }
}