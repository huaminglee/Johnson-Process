using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;

namespace Johnson.Process.Core
{
    public class UltimusProcess
    {
        public string Name { set; get; }

        public string TaskTransferAddress { set; get; }

        public UltimusProcess(string processName)
        {
            Name = processName;
        }

        public string GetUltimusUserAccount(string account)
        {
            return string.Format("{0}/{1}", ConfigurationManager.AppSettings["ultimusDomain"], account);
        }

        public string[] GetUltimusUserAccounts(string accounts)
        {
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

        public TaskSendResult Start(string userAccount, string taskId, Variable[] listVars, string strMemo, string strSummary, object taskForm)
        {
            if (string.IsNullOrEmpty(userAccount))
            {
                throw new ArgumentNullException("userAccount");
            }
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (taskForm == null)
            {
                throw new ArgumentNullException("taskForm");
            }
            int incidentNo = 0;
            string errorMessage;
            string processName = "";
#if DEBUG

#else
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new ArgumentException(string.Format("通过TaskId:{0}初始化Task失败", taskId));
            }
            if (!task.SendFrom(this.GetUltimusUserAccount(userAccount), listVars, strMemo, strSummary, ref incidentNo, out errorMessage))
            {
                throw new Exception("流程提交失败:" + errorMessage);
            }
            processName = task.strProcessName;
#endif

            string json = JsonConvert.SerializeObject(taskForm);
            Insert(taskForm.ToString(), json, incidentNo, processName);
            TaskSendResult result = new TaskSendResult();
            result.IncidentNo = incidentNo;
            return result;
        }

        public TaskSendResult Send(string taskId, Variable[] listVars, string strMemo, string strSummary, object taskForm)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (taskForm == null)
            {
                throw new ArgumentNullException("taskForm");
            }
            int incidentNo = 0;
            string errorMessage;
            string processName = "";
#if DEBUG

#else
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new ArgumentException(string.Format("通过TaskId:{0}初始化Task失败", taskId));
            }
            if (!task.Send(listVars, strMemo, strSummary, ref incidentNo, out errorMessage))
            {
                throw new Exception("流程提交失败:" + errorMessage);
            }
            
            incidentNo = task.nIncidentNo;
            processName = task.strProcessName;
#endif

            string json = JsonConvert.SerializeObject(taskForm);
            Update(incidentNo, processName, json);
            TaskSendResult result = new TaskSendResult();
            result.IncidentNo = incidentNo;
            return result;
        }

        public void Return(string taskId, Variable[] listVars, string strMemo, string strSummary, object taskForm)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (taskForm == null)
            {
                throw new ArgumentNullException("taskForm");
            }
            int incidentNo = 0;
            string processName = "";
#if DEBUG   

#else
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new ArgumentException(string.Format("通过TaskId:{0}初始化Task失败", taskId));
            }
            string errorMessage;
            if (!task.Return(listVars, strMemo, strSummary, out errorMessage))
            {
                throw new Exception("流程退回失败:" + errorMessage);
            }
            incidentNo = task.nIncidentNo;
            processName = task.strProcessName;
#endif
            string json = JsonConvert.SerializeObject(taskForm);
            Update(incidentNo, processName, json);
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
            if(!task.InitializeFromTaskId(taskId))
            {
                throw new Exception(string.Format("初始化taskId为{0}的Task失败", taskId));
            }
            return new TaskInfo(task);
#endif

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

        public T Get<T>(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            int incidentNo = 0;
            string processName = "";
#if DEBUG

#else
            Task taskInfo = new Task();
            taskInfo.InitializeFromTaskId(taskId);
            incidentNo = taskInfo.nIncidentNo;
            processName = taskInfo.strProcessName;
#endif

            string processFormJson = "";
            string sql = string.Format("select processForm from gz_johnson_process_form where incident = {0} and processName = '{1}'",
                incidentNo, processName);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                object obj = SqlHelper.ExecuteScalar(conn, sql, System.Data.CommandType.Text, null);
                if (obj != null)
                {
                    processFormJson = obj.ToString();
                }
            }

            return JsonConvert.DeserializeObject<T>(processFormJson);
        }

        private void Insert(string processType, string json, int incident, string processName)
        {
            string sql = string.Format("insert gz_johnson_process_form  values('{0}', {1}, '{2}', '{3}')",
                processName, incident, json, processType);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, sql, System.Data.CommandType.Text, null);
            }
        }

        private void Update(int incident, string processName, string json)
        {
            string sql = string.Format("update gz_johnson_process_form  set  processForm = '{0}' where Incident = {1} and processName = '{2}'",
                json, incident, processName);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, sql, System.Data.CommandType.Text, null);
            }
        }

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
                throw new Exception(string.Format("加载流程{0}实例{1}失败",this.Name,incidentNo));
            }
            return incident;
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
    }
}
