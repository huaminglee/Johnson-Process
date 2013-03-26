using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class VocReportModel
    {
        public VocReportModel()
        {
        }

        public VocReportModel(ProcessForm<VocForm> processForm)
        {
            VocForm form = processForm.Form;

            this.applyTime = form.ApplyTime.ToString("yyyy-MM-dd");
            this.applyUserDepartmentName = form.ApplyUserDepartmentName;
            this.applyUserName = form.ApplyUserName;
            this.vocCode = form.VocCode;
            this.faultCategory = form.FaultCategory;
            this.faultQuantity = form.FaultQuantity;
            this.faultRemark = form.FaultRemark;
            this.machineCode = form.MachineCode;
            this.machineModel = form.MachineModel;
            this.needCompleteDate = form.NeedCompleteDate.ToString("yyyy-MM-dd");
            this.projectName = form.ProjectName;
            this.tempMeasure = form.TempMeasure;
            this.incidentNo = processForm.IncidentNo;
        }

        public int incidentNo { set; get; }

        public string applyUserName { set; get; }

        public string applyUserDepartmentName { set; get; }

        public string applyTime { set; get; }

        public string vocCode { set; get; }

        public string projectName { set; get; }

        public string machineModel { set; get; }

        public string machineCode { set; get; }

        public int? faultQuantity { set; get; }

        public string faultCategory { set; get; }

        public string tempMeasure { set; get; }

        public string needCompleteDate { set; get; }

        public string faultRemark { set; get; }
    }
}