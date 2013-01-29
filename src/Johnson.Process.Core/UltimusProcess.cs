using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;
using System.Configuration;

namespace Johnson.Process.Core
{
    public class UltimusProcess
    {
        protected const string SYSTEM_ACCOUNT = "system_gz_process";

        public string TaskTransferAddress { set; get; }

        public string Name { set; get; }

        public Tasklist GetAllTask()
        {
            Tasklist taskList = new Tasklist();
            TasklistFilter filter = new TasklistFilter { };
            filter.nFiltersMask = Filters.nFilter_Current | Filters.nFilter_Overdue | Filters.nFilter_Urgent;
            filter.strProcessNameFilter = this.Name;
            taskList.LoadFilteredTasks(filter);
            return taskList;
        }

        public Incident GetIncident(int incidentNo)
        {
            Incident incident = new Incident();
            if (!incident.LoadIncident(this.Name, incidentNo))
            {
                throw new Exception(string.Format("加载流程{0}实例{1}失败", this.Name, incidentNo));
            }
            return incident;
        }

        public int GetIncidentStatus(int incidentNo)
        {
#if DEBUG
            return 1;
#endif
            Incident incident = new Incident();
            if (!incident.LoadIncident(this.Name, incidentNo))
            {
                throw new Exception(string.Format("加载流程{0}实例{1}失败", this.Name, incidentNo));
            }
            Incident.Status status;
            if (!incident.GetIncidentStatus(out status))
            {
                throw new Exception(string.Format("加载流程{0}实例{1}Status失败", this.Name, incidentNo));
            }
            return status.nIncidentStatus;
        }

        public Task GetIncidentStartTask(int incidentNo)
        {
            TasklistFilter filter = new TasklistFilter();
            filter.strProcessNameFilter = this.Name;
            filter.nIncidentNo = incidentNo;
            Tasklist tasklist = new Tasklist();
            if (!tasklist.LoadFilteredTasks(filter))
            {
                throw new Exception(string.Format("加载流程{0}实例{1}任务失败", this.Name, incidentNo));
            }
            for (int position = 0; position < tasklist.GetTasksCount(); ++position)
            {
                Task task = tasklist.GetAt(position);
                if (task.nStepType == 2)
                {
                    return task;
                }
            }
            throw new Exception(string.Format("加载流程{0}实例{1}开始任务失败", this.Name, incidentNo));
        }

        public string GetStartSetpName(string taskId)
        {
#if DEBUG
            return "";
#endif
            TaskInfo taskInfo = this.GetTaskInfo(taskId);
            Task task = this.GetIncidentStartTask(taskInfo.IncidentNo);
            return task.strStepName;
        }

        public Task GetStartTask(int incidentNo)
        {
            Task task = this.GetIncidentStartTask(incidentNo);
            return task;
        }

        public string GetIncidentTaskId(string account, int incidentNo)
        {
#if DEBUG
            return "test";
#endif
            Tasklist taskList = new Tasklist();
            TasklistFilter filter = new TasklistFilter { };
            filter.nFiltersMask = Filters.nFilter_Current | Filters.nFilter_Overdue | Filters.nFilter_Urgent;
            filter.strProcessNameFilter = this.Name;
            filter.nIncidentNo = incidentNo;
            filter.strArrUserName = new string[] { this.GetUltimusUserAccount(account) };
            taskList.LoadFilteredTasks(filter);
            Task task = taskList.GetAt(0);
            if (task == null)
            {
                throw new Exception("task 为空");
            }
            return task.strTaskId;
        }

        public TaskInfo GetTaskInfo(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
#if DEBUG
            return new TaskInfo {  };
#else
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new Exception(string.Format("初始化taskId为{0}的Task失败", taskId));
            }
            return new TaskInfo(task);
#endif

        }

        public string GetUltimusUserAccount(string account)
        {
            if (string.IsNullOrEmpty(account))
            {
                return "";
            }
            return string.Format("{0}/{1}", ConfigurationManager.AppSettings["ultimusDomain"], account);
        }

        public object[] GetUltimusUserAccounts(string accounts)
        {
            if (string.IsNullOrEmpty(accounts))
            {
                return null;
            }
            List<string> formatAccounts = new List<string>();
            string[] accountSplit = accounts.Split(',');
            foreach (string account in accountSplit)
            {
                string fomartAccount = string.Format("{0}/{1}", ConfigurationManager.AppSettings["ultimusDomain"], account);
                if (!formatAccounts.Contains(fomartAccount))
                {
                    formatAccounts.Add(fomartAccount);
                }
            }
            return formatAccounts.ToArray();
        }

        public object GetVariableValue(string taskId, string variableName)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException("variableName");
            }
#if DEBUG
            return "";
#endif
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new Exception(string.Format("初始化taskId为{0}的Task失败", taskId));
            }
            object value;
            string errorMessage;
            if (!task.GetVariableValue(variableName, out value, out errorMessage))
            {
                throw new Exception("获取变量失败:" + errorMessage);
            }
            return value;
        }

        public object GetVariableValue(string processName, int incidentNo, string variableName)
        {
            if (string.IsNullOrEmpty(processName))
            {
                throw new ArgumentNullException("processName");
            }
            if (incidentNo <= 0)
            {
                throw new ArgumentException("incidentNo必须大于0");
            }
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException("variableName");
            }
#if DEBUG
            return "";
#endif
            Incident incident = new Incident();
            if (!incident.LoadIncident(processName, incidentNo))
            {
                throw new Exception(string.Format("初始化incidentNo为{0}的Incident失败", incidentNo));
            }
            object value;
            string errorMessage;
            if (!incident.GetVariableValue(variableName, out value, out errorMessage))
            {
                throw new Exception("获取变量失败:" + errorMessage);
            }
            return value;
        }

        protected int GetIncidentNo(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            int incidentNo = 0;
#if DEBUG

#else
            Task taskInfo = new Task();
            taskInfo.InitializeFromTaskId(taskId);
            incidentNo = taskInfo.nIncidentNo;
#endif

            return incidentNo;
        }
    }
}
