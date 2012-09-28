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
            List<UploadFileModel> fileModels = new List<UploadFileModel>();
            if (form.Files != null)
            {
                foreach (ProcessFile file in form.Files)
                {
                    fileModels.Add(new UploadFileModel(file));
                }
            }

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

#if DEBUG
            this.taskStatus = 1;
#else
            Task task = WebHelper.VocProcess.GetStartTask(processForm.IncidentNo);
            this.taskId = task.strTaskId;
            this.incidentNo = task.nIncidentNo;
            this.taskStatus = WebHelper.VocProcess.GetIncidentStatus(task.nIncidentNo);
#endif
        }

        public string taskId { set; get; }

        public int incidentNo { set; get; }

        public int taskStatus { set; get; }

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