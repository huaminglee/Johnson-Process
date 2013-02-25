using System;
using System.Collections.Generic;
using System.Text;
using Johnson.Process.Core;
using Ultimus.WFServer;
using EDoc2.Organization;
using log4net;
using Johnson.Process.EMail.Exceptions;
using System.Threading;
using System.Data.SqlClient;

namespace Johnson.Process.EMail
{
    public class TaskEmailNotifySerivce
    {
        UltimusProcess _process;
        private const string SUBJECT_TEMPLATE = "收到${initiateDate}由${initiator}发起了${processName}流程待办任务";
        private const string CONTENT_TEMPLATE = "您好：收到${initiateDate}由${initiator}发起了${processName}流程待办任务。</br>流程摘要:${summary}。<a href='${taskLink}' target='_blank'>查看详细信息</a>";

        ILog _logger;

        public TaskEmailNotifySerivce(UltimusProcess process, ILog _logger)
        {
            this._process = process;

            this._logger = _logger;
        }

        public void Start()
        {
            Tasklist tasklist = null;
            try
            {
                ApiManager.NewApi();
                tasklist = this._process.GetAllTask();
                if (tasklist != null)
                {
                    int taskCount = tasklist.GetTasksCount();
                    for (int i = 0; i < taskCount; i++)
                    {
                        try
                        {
                            Task task = tasklist.GetAt(i);
                            if (!this.Notified(task))
                            {
                                string[] strUserSplit = task.strAssignedToUser.Split('/');
                                if (strUserSplit.Length == 2)
                                {
                                    string userLoginName = strUserSplit[1];
                                    string token;
                                    ApiManager.Api.OrgnizationManagement.ImpersonateByLoginName(userLoginName, "127.0.0.1", out token);
                                    EDoc2UserInfo userInfo;
                                    ApiManager.Api.OrgnizationManagement.GetUserByLoginName(token, userLoginName, out userInfo);
                                    if (userInfo == null)
                                    {
                                        throw new TaskEmailNotifySerivceException(string.Format("无法获取userLoginName:{0}的用户", userLoginName));
                                    }

                                    if (!string.IsNullOrEmpty(userInfo.UserEmail))
                                    {
                                        string subject = this.GetSubject(task, userInfo);
                                        string content = this.GetContent(task, userInfo);
                                        MailSender.Current.Send(userInfo.UserEmail, subject, "", "", true, content);
                                        this.Record(task, userInfo.UserEmail, subject, content);
                                    }
                                }
                            }
                        }
                        catch (TaskEmailNotifySerivceException ex)
                        {
                            this._logger.Error(ex.Message, ex);
                        }
                        Thread.Sleep(1000 * 30);
                    }
                }
            }
            catch (TaskEmailNotifySerivceException ex)
            {
                this._logger.Error(ex.Message, ex);
            }
            finally
            {
                tasklist = null;
            }
        }

        private bool Notified(Task task)
        {
            int count = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                object obj = SqlHelper.ExecuteScalar(conn, "select count(1) from wf_myToDoTaskMail where taskId = '"+ task.strTaskId +"'", System.Data.CommandType.Text, null);
                if (obj != null)
                {
                    count = (int)obj;
                }
            }

            return count > 0;
        }

        private void Record(Task task, string emailAddress, string subject, string content)
        {
            string sql = "insert wf_myToDoTaskMail  values(@taskId, @email, @subject, @content)";
            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("taskId", task.strTaskId),
                new SqlParameter("email", emailAddress),
                new SqlParameter("subject", subject),
                new SqlParameter("content", content),
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectString))
            {
                SqlHelper.ExecuteNonQuery(conn, sql, System.Data.CommandType.Text, paras);
            }
        }

        protected string GetSubject(Task task, EDoc2UserInfo userInfo)
        {
            return this.InterpreterTaskTemplate(task, SUBJECT_TEMPLATE);
        }

        protected string GetContent(Task task, EDoc2UserInfo userInfo)
        {
            return this.InterpreterTaskTemplate(task, CONTENT_TEMPLATE);
        }

        private string InterpreterTaskTemplate(Task task, string template)
        {
            Task startTask = this._process.GetIncidentStartTask(task.nIncidentNo);
            return template.Replace("${initiateDate}", this.GetDateTime(startTask.dStartTime).ToString())
                .Replace("${initiator}", startTask.strUserFullName)
                .Replace("${processName}", this._process.Name)
                .Replace("${summary}", task.strSummary)
                .Replace("${taskLink}", string.Format("{0}?TaskId={1}&TaskType=InboxTask&procName={2}", 
                this._process.TaskTransferAddress, task.strTaskId, this._process.Name));
        }

        private DateTime GetDateTime(double doubleDate)
        {
            return DateTime.FromOADate(doubleDate);
        }
    }
}
