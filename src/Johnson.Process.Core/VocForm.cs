using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class VocForm
    {
        public string ApplyUserName { set; get; }

        public string ApplyUserDepartmentName { set; get; }

        public DateTime ApplyTime { set; get; }

        public string VocCode { set; get; }

        public string ProjectName { set; get; }

        public string MachineModel { set; get; }

        public string MachineCode { set; get; }

        public int FaultQuantity { set; get; }

        public string FaultCategory { set; get; }

        public string TempMeasure { set; get; }

        public DateTime NeedCompleteDate { set; get; }

        public string FaultRemark { set; get; }

        public string ResponsibleUserAccount { set; get; }

        public string ResponsibleUserName { set; get; }

        public string ResponsibleUserPreviousAccount { set; get; }

        public string ResponsibleUserPreviousName { set; get; }

        public string MeasureUserName { set; get; }

        public string MeasureUserAccount { set; get; }

        public List<VocAction> Actions { set; get; }

        public string Reason { set; get; }

        public DateTime? ReasonWanchengShijian { set; get; }

        public string Measures { set; get; }

        public List<ProcessFile> Files { set; get; }

        public List<ProcessFile> ReasonFiles { set; get; }

        public List<ProcessFile> MeasuresFiles { set; get; }

        public string Solutions { set; get; }

        public DateTime? SolutionsStartTime { set; get; }

        public DateTime? SolutionsCompleteTime { set; get; }

        public List<ProcessFile> SolutionsFiles { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }

        public DateTime? WanchengShijian { set; get; }
    }
}
