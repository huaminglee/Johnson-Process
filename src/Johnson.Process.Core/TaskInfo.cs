using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class TaskInfo
    {
        public TaskInfo()
        {
        }

        public TaskInfo(Task task)
        {
            this.IncidentNo = task.nIncidentNo;
            this.StepName = task.strStepName;
            this.Summary = task.strSummary;
            this.TaskId = task.strTaskId;
            this.Status = task.nTaskStatus;
            this.SubStatus = task.nTaskSubStatus;
            this.User = task.strUser;
            this.UserFullName = task.strUserFullName;
        }

        public int IncidentNo { private set; get; }
        public string StepName { private set; get; }
        public string Summary { private set; get; }
        public string TaskId { private set; get; }
        /// <summary>
        /// 1我的代办,流程申请、3我的已办,以归档
        /// </summary>
        public int Status { private set; get; }
        public int SubStatus { private set; get; }
        public string User { private set; get; }
        public string UserFullName { private set; get; }
    }
}
