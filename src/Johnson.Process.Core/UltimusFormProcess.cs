using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;

namespace Johnson.Process.Core
{
    public class UltimusFormProcess<TForm> : UltimusProcess
    {
        List<ProcessForm<TForm>> _formList;
        Dictionary<int, ProcessForm<TForm>> _formListDictionaryByIncNo;

        public UltimusFormProcess(string processName)
        {
            Name = processName;
            _formList = this.Load();
            this.Index();
        }

        private void Index()
        {
            _formListDictionaryByIncNo = new Dictionary<int, ProcessForm<TForm>>();
            foreach (ProcessForm<TForm> pform in this._formList)
            {
                _formListDictionaryByIncNo.Add(pform.IncidentNo, pform);
            }
        }

        public TForm Get(string taskId)
        {
            int incNo = this.GetIncidentNo(taskId);
            if (this._formListDictionaryByIncNo.ContainsKey(incNo))
            {
                return this._formListDictionaryByIncNo[incNo].Form;
            }
            return default(TForm);
        }

        public TForm Get(int incNo)
        {
            if (this._formListDictionaryByIncNo.ContainsKey(incNo))
            {
                return this._formListDictionaryByIncNo[incNo].Form;
            }
            return default(TForm);
        }

        public List<ProcessForm<TForm>> Get()
        {
            return this._formList;
        }

        public TaskSendResult Start(string userAccount, string taskId, Variable[] listVars, string strMemo, string strSummary, TForm taskForm)
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
#endif

            string json = JsonConvert.SerializeObject(taskForm);
            Insert(taskForm.ToString(), json, incidentNo, this.Name);

            ProcessForm<TForm> processForm = this.Load(incidentNo);
            this._formList.Insert(0, processForm);
            this._formListDictionaryByIncNo.Add(processForm.IncidentNo, processForm);

            TaskSendResult result = new TaskSendResult();
            result.IncidentNo = incidentNo;
            return result;
        }

        protected void Save(string taskId, object taskForm)
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
#if DEBUG

#else
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new ArgumentException(string.Format("通过TaskId:{0}初始化Task失败", taskId));
            }

            incidentNo = task.nIncidentNo;
#endif

            string json = JsonConvert.SerializeObject(taskForm);
            Update(incidentNo, this.Name, json, taskForm.ToString());
        }

        protected void Save(int incidentNo, object taskForm)
        {
            string json = JsonConvert.SerializeObject(taskForm);
            Update(incidentNo, this.Name, json, taskForm.ToString());
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
#endif

            string json = JsonConvert.SerializeObject(taskForm);
            Update(incidentNo, this.Name, json, taskForm.ToString());
            TaskSendResult result = new TaskSendResult();
            result.IncidentNo = incidentNo;
            return result;
        }

        public void Close()
        {

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
#endif
            string json = JsonConvert.SerializeObject(taskForm);
            Update(incidentNo, this.Name, json, taskForm.ToString());
        }

        public string GetStartTaskId(string account)
        {
#if DEBUG
            return "test";
#endif
            Tasklist taskList = new Tasklist();
            TasklistFilter filter = new TasklistFilter { };
            filter.nFiltersMask = Filters.nFilter_Initiate;
            filter.strArrUserName = new string[] { this.GetUltimusUserAccount(account) };
            filter.strProcessNameFilter = this.Name;
            bool result = taskList.LoadFilteredTasks(filter);
            if (!result)
            {
                throw new Exception("taskList.LoadFilteredTasks失败");
            }

            Task task = taskList.GetAt(0);
            if (task == null)
            {
                throw new Exception("task 为空");
            }
            return task.strTaskId;
        }

        private ProcessForm<TForm> Load(int incidentNo)
        {
            ProcessForm<TForm> processForm = null;
            string sql = string.Format("select * from gz_johnson_process_form where incident = {0} and processName = '{1}' and processType = '{2}'",
                incidentNo, this.Name, typeof(TForm).FullName);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(conn, sql, System.Data.CommandType.Text, null);
                if (reader.Read())
                {
                    processForm = this.Map(reader);
                }
            }

            return processForm;
        }

        private ProcessForm<TForm> Map(SqlDataReader reader)
        {
            ProcessForm<TForm> processForm = new ProcessForm<TForm>();

            try
            {
                if (reader["id"] != DBNull.Value)
                {
                    processForm.ID = int.Parse(reader["id"].ToString());
                }
                if (reader["processName"] != DBNull.Value)
                {
                    processForm.ProcessName = reader["processName"].ToString();
                }
                if (reader["incident"] != DBNull.Value)
                {
                    processForm.IncidentNo = int.Parse(reader["incident"].ToString());
                }
                if (reader["processType"] != DBNull.Value)
                {
                    processForm.ProcessType = reader["processType"].ToString();
                }
                if (reader["processForm"] != DBNull.Value)
                {
                    processForm.Form = JsonConvert.DeserializeObject<TForm>(reader["processForm"].ToString());
                }
                if (reader["status"] != DBNull.Value)
                {
                    processForm.Status = int.Parse(reader["status"].ToString());
                }
            }
            catch
            {
                processForm = null;
            }
            return processForm;
        }

        private List<ProcessForm<TForm>> Load()
        {
            List<ProcessForm<TForm>> list = new List<ProcessForm<TForm>>();
            string sql = string.Format("select * from gz_johnson_process_form where processName = '{0}' and processType = '{1}' order by id desc", this.Name, typeof(TForm).FullName);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlDataReader reader = SqlHelper.ExecuteReader(conn, sql, System.Data.CommandType.Text, null);
                while (reader.Read())
                {
                    ProcessForm<TForm> processForm = this.Map(reader);
                    if (processForm != null)
                    {
                        list.Add(processForm);
                    }
                }
            }

            return list;
        }

        private void Insert(string processType, string json, int incident, string processName)
        {
            string sql = string.Format("insert gz_johnson_process_form(processName, Incident, processForm, processType, status)  values('{0}', {1}, '{2}', '{3}', 1)",
                processName, incident, json, processType);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, sql, System.Data.CommandType.Text, null);
            }
        }

        protected void Update(int incident, string processName, string json, string processType)
        {
            string sql = string.Format("update gz_johnson_process_form set processForm = '{0}' where Incident = {1} and processName = '{2}' and processType = '{3}'",
                json, incident, processName, processType);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, sql, System.Data.CommandType.Text, null);
            }
        }

        protected void Update(int incident, string processName, string json, string processType, int status)
        {
            string sql = string.Format("update gz_johnson_process_form set processForm = '{0}', status = {4} where Incident = {1} and processName = '{2}' and processType = '{3}'",
                json, incident, processName, processType, status);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, sql, System.Data.CommandType.Text, null);
            }
        }

        
    }
}
