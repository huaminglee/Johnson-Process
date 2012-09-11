using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class VocModel
    {
        public VocModel()
        {
        }

        public VocModel(VocForm form)
        {
            List<UploadFileModel> fileModels = new List<UploadFileModel>();
            if (form.Files != null)
            {
                foreach (ProcessFile file in form.Files)
                {
                    fileModels.Add(new UploadFileModel(file));
                }
            }
            this.files = fileModels;

            this.reasonFiles = new List<UploadFileModel>();
            if (form.ReasonFiles != null)
            {
                foreach (ProcessFile file in form.ReasonFiles)
                {
                    this.reasonFiles.Add(new UploadFileModel(file));
                }
            }

            this.measuresFiles = new List<UploadFileModel>();
            if (form.MeasuresFiles != null)
            {
                foreach (ProcessFile file in form.MeasuresFiles)
                {
                    this.measuresFiles.Add(new UploadFileModel(file));
                }
            }
            this.solutionsFiles = new List<UploadFileModel>();
            if (form.SolutionsFiles != null)
            {
                foreach (ProcessFile file in form.SolutionsFiles)
                {
                    this.solutionsFiles.Add(new UploadFileModel(file));
                }
            }

            this.remarks = new List<RemarkModel>();
            if (form.Approves != null)
            {
                foreach (TaskApproveInfo approveInfo in form.Approves)
                {
                    this.remarks.Add(new RemarkModel(approveInfo));
                }
            }

            this.actions = new List<VocActionModel>();
            if (form.Actions != null)
            {
                foreach (VocAction act in form.Actions)
                {
                    this.actions.Add(new VocActionModel(act));
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
            this.measures = form.Measures;
            this.needCompleteDate = form.NeedCompleteDate.ToString("yyyy-MM-dd");
            this.projectName = form.ProjectName;
            this.reason = form.Reason;
            this.responsibleUserAccount = form.ResponsibleUserAccount;
            this.responsibleUserName = form.ResponsibleUserName;
            this.measureUserName = form.MeasureUserName;
            this.measureUserAccount = form.MeasureUserAccount;
            this.responsibleUserPreviousAccount = form.ResponsibleUserPreviousAccount;
            this.responsibleUserPreviousName = form.ResponsibleUserPreviousName;
            this.tempMeasure = form.TempMeasure;
            this.solutions = form.Solutions;
            if (form.SolutionsCompleteTime.HasValue)
            {
                this.solutionsCompleteTime = form.SolutionsCompleteTime.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                this.solutionsCompleteTime = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

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

        public string responsibleUserAccount { set; get; }

        public string responsibleUserName { set; get; }

        public string measureUserName { set; get; }

        public string measureUserAccount { set; get; }

        public string responsibleUserPreviousAccount { set; get; }

        public string responsibleUserPreviousName { set; get; }

        public List<VocActionModel> actions { set; get; }

        public string reason { set; get; }

        public string measures { set; get; }

        public List<UploadFileModel> files { set; get; }

        public List<UploadFileModel> reasonFiles{ set; get; }

        public List<UploadFileModel> measuresFiles{ set; get; }

        public string solutions { set; get; }

        public string solutionsCompleteTime { set; get; }

        public List<UploadFileModel> solutionsFiles { set; get; }

        public List<RemarkModel> remarks;

        public VocForm Map()
        {
            List<ProcessFile> files = new List<ProcessFile>();
            if (this.files != null)
            {
                foreach (UploadFileModel fileModel in this.files)
                {
                    files.Add(fileModel.Map());
                }
            }

            List<ProcessFile> reasonFiles = new List<ProcessFile>();
            if (this.reasonFiles != null)
            {
                foreach (UploadFileModel fileModel in this.reasonFiles)
                {
                    reasonFiles.Add(fileModel.Map());
                }
            }
            List<ProcessFile> measuresFiles = new List<ProcessFile>();
            if (this.measuresFiles != null)
            {
                foreach (UploadFileModel fileModel in this.measuresFiles)
                {
                    measuresFiles.Add(fileModel.Map());
                }
            }
            List<ProcessFile> solutionsFiles = new List<ProcessFile>();
            if (this.solutionsFiles != null)
            {
                foreach (UploadFileModel fileModel in this.solutionsFiles)
                {
                    solutionsFiles.Add(fileModel.Map());
                }
            }

            List<VocAction> actions = new List<VocAction>();
            if (this.actions != null)
            {
                foreach (VocActionModel actionModel in this.actions)
                {
                    actions.Add(actionModel.Map());
                }
            }

            return new VocForm 
            {
                Actions = actions,
                ApplyTime = DateTime.Parse(this.applyTime),
                ApplyUserDepartmentName = this.applyUserDepartmentName,
                ApplyUserName = this.applyUserName,
                VocCode = this.vocCode,
                Files = files,
                FaultCategory = this.faultCategory,
                FaultQuantity = this.faultQuantity.Value,
                FaultRemark = this.faultRemark,
                ReasonFiles = reasonFiles,
                MeasuresFiles = measuresFiles,
                MachineCode = this.machineCode,
                MachineModel = this.machineModel,
                Measures = this.measures,
                NeedCompleteDate = DateTime.Parse(this.needCompleteDate),
                ProjectName = this.projectName,
                Reason = this.reason,
                ResponsibleUserAccount = this.responsibleUserAccount,
                ResponsibleUserName = this.responsibleUserName,
                MeasureUserName = this.measureUserName,
                MeasureUserAccount = this.measureUserAccount,
                ResponsibleUserPreviousAccount = this.responsibleUserPreviousAccount,
                ResponsibleUserPreviousName = this.responsibleUserPreviousName,
                TempMeasure = this.tempMeasure,
                Solutions = this.solutions,
                SolutionsFiles = solutionsFiles
            };
        }
    }

    public class VocStartModel : VocModel
    {
        public string submitRemark { set; get; }
    }
}