using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;
using System.Data.SqlClient;

namespace Johnson.Process.Core
{
    public class FailureProductProcess : UltimusFormProcess<FailureProductForm>
    {
        private string _resultEmailContentTemplate;

        public FailureProductProcess(string processName, string resultEmailContentTemplate)
            :base(processName)
        {
            this._resultEmailContentTemplate = resultEmailContentTemplate;
            this.ProductReworkProcess = new ProductReworkProcess(processName);
        }

        /// <summary>
        /// 不合格品处理方式：返工返修流程处理，不合格品流程处理
        /// </summary>
        private const string PARAM_DEAL_WAY = "dealWay";
        /// <summary>
        /// pmc执行人
        /// </summary>
        private const string PARAM_PMC_USER = "pmcUser";
        /// <summary>
        /// qc执行人
        /// </summary>
        private const string PARAM_QC_USER = "qcUser";
        /// <summary>
        /// qe执行人
        /// </summary>
        private const string PARAM_QE_USER = "qeUser";
        /// <summary>
        /// mrb执行人
        /// </summary>
        private const string PARAM_MRB_USERS = "mrbUsers";
        /// <summary>
        /// mrb结果数
        /// </summary>
        private const string PARAM_MRB_RESULT_COUNT = "mrbResultCount";
        /// <summary>
        /// 是否需要mrb
        /// </summary>
        private const string PARAM_NEED_MRB = "needMrb";
        /// <summary>
        /// 返工返修pmc执行人
        /// </summary>
        private const string PARAM_REWORK_PMC_USER = "reworkPmcUser";
        /// <summary>
        /// 处理结果
        /// </summary>
        private const string PARAM_RESULT = "result";
        /// <summary>
        /// 技术执行人
        /// </summary>
        private const string PARAM_ENG_USER = "engUser";
        /// <summary>
        /// 工艺执行人
        /// </summary>
        private const string PARAM_CID_USER = "cidUser";
        /// <summary>
        /// csd执行人
        /// </summary>
        private const string PARAM_CSD_USER = "csdUser";
        /// <summary>
        /// 财务核算人
        /// </summary>
        private const string PARAM_FIN_USER = "finUser";
        /// <summary>
        /// 仓库执行人
        /// </summary>
        private const string PARAM_STO_USER = "stoUser";

        private const string PARAM_HAS_CID_USER = "hasCidUser";

        private const string PARAM_HAS_ENG_USER = "hasEngUser";

        private const string PARAM_HAS_FIN_USER = "hasFinUser";

        public ProductReworkProcess ProductReworkProcess { private set; get; }

        public FailureProductForm GetFromThirdDatabase(string id)
        {
            FailureProductForm form = this.GetFromIncoming(id);
            if (form == null)
            {
                form = this.GetFromInprocess(id);
            }
            if (form != null)
            {
                form.No = id;
            }
            return form;
        }

        private FailureProductForm GetFromInprocess(string id)
        {
            FailureProductForm form = null; 
            string sql = "select * from V_Record_Inprocess where id = @id";
            using (SqlConnection conn = new SqlConnection(SqlHelper.FailPdct_ConnectionString))
            {
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("id", id) };
                SqlDataReader reader = SqlHelper.ExecuteReader(conn, sql, System.Data.CommandType.Text, paras);
                if (reader.Read())
                {
                    form = new FailureProductForm();
                    form.ProductType = ProductType.CP;
                    if (reader[2] != DBNull.Value)
                    {
                        form.ComponentName = reader.GetString(2);
                    }
                    if (reader[3] != DBNull.Value)
                    {
                        form.UM = reader.GetString(3);
                    }
                    if (reader[4] != DBNull.Value)
                    {
                        form.GYSDM = reader.GetString(4);
                    }
                    if (reader[5] != DBNull.Value)
                    {
                        form.GYSMC = reader.GetString(5);
                    }
                    if (reader[6] != DBNull.Value)
                    {
                        form.JZXLH = reader.GetString(6);
                    }
                    if (reader[7] != DBNull.Value)
                    {
                        form.BJXLH = reader.GetString(7);
                    }
                    if (reader[8] != DBNull.Value)
                    {
                        form.MO = reader.GetString(8);
                    }
                    if (reader[9] != DBNull.Value)
                    {
                        form.FailurePlace = reader.GetString(9);
                    }
                    if (reader[10] != DBNull.Value)
                    {
                        form.Quantity = reader[10].ToString();
                    }
                    if (reader[11] != DBNull.Value)
                    {
                        form.Remark = reader.GetString(11);
                    }
                    if (reader[13] != DBNull.Value)
                    {
                        form.ZRBM = reader.GetString(13);
                    }
                }
                reader.Close();
            }

            return form;
        }

        private FailureProductForm GetFromIncoming(string id)
        {
            FailureProductForm form = null;
            string sql = "select * from V_Record_Incoming where id = @id";
            using (SqlConnection conn = new SqlConnection(SqlHelper.FailPdct_ConnectionString))
            {
                SqlParameter[] paras = new SqlParameter[] { new SqlParameter("id", id) };
                SqlDataReader reader = SqlHelper.ExecuteReader(conn, sql, System.Data.CommandType.Text, paras);
                if (reader.Read())
                {
                    form = new FailureProductForm();
                    form.ProductType = ProductType.LJ;
                    if (reader[1] != DBNull.Value)
                    {
                        form.ComponentCode = reader.GetString(1);
                    }
                    if (reader[2] != DBNull.Value)
                    {
                        form.ComponentName = reader.GetString(2);
                    }
                    if (reader[3] != DBNull.Value)
                    {
                        form.OrderCode = reader.GetString(3);
                    }
                    if (reader[4] != DBNull.Value)
                    {
                        form.UM = reader.GetString(4);
                    }
                    if (reader[5] != DBNull.Value)
                    {
                        form.GYSDM = reader.GetString(5);
                    }
                    if (reader[6] != DBNull.Value)
                    {
                        form.GYSMC = reader.GetString(6);
                    }
                    if (reader[7] != DBNull.Value)
                    {
                        form.BJXLH = reader.GetString(7);
                    }
                    if (reader[8] != DBNull.Value)
                    {
                        form.FailurePlace = reader.GetString(8);
                    }
                    if (reader[9] != DBNull.Value)
                    {
                        form.Quantity = reader[9].ToString();
                    }
                    if (reader[10] != DBNull.Value)
                    {
                        form.Remark = reader.GetString(10);
                    }
                }
                reader.Close();
            }

            return form;
        }

        private string GetSummary(FailureProductForm form)
        {
            return form.ComponentName + "," + form.Remark;
        }

        public TaskSendResult Start(string startUserAccount, string startUserName, string taskId, FailureProductForm form)
        {
            form.StartUserAccount = startUserAccount;
            form.StartUserName = startUserName;
            form.StartTime = DateTime.Now;
            if (string.IsNullOrEmpty(startUserAccount))
            {
                throw new ArgumentNullException("startUserAccount");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (string.IsNullOrEmpty(form.PmcUserAccount))
            {
                throw new ArgumentNullException("form.PmcUserAccount");
            }
            if (string.IsNullOrEmpty(form.QEUserAccount))
            {
                throw new ArgumentNullException("form.QEUserAccount");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_DEAL_WAY, objVariableValue = new object[]{"Failure"} },
                    new Variable{ strVariableName = PARAM_PMC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.PmcUserAccount)} },
                    new Variable{ strVariableName = PARAM_QE_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QEUserAccount)} }
                };
            return this.Start(startUserAccount, taskId, variable, "", this.GetSummary(form), form);
        }

        public void QESend(string taskId, FailureProductForm form, string emailTo)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            
            FailureResult result = FailureResult.None;
            if (form.QEResult != FailureResult.MRB)
            {
                result = form.QEResult;
            }
            switch (form.QEResult)
            {
                case FailureResult.Rework:
                    if (string.IsNullOrEmpty(form.FinUserAccount))
                    {
                        throw new ArgumentNullException("form.FinUserAccount");
                    }
                    if (string.IsNullOrEmpty(form.EngUserAccount))
                    {
                        throw new ArgumentNullException("form.EngUserAccount");
                    }
                    if (string.IsNullOrEmpty(form.CidUserAccount))
                    {
                        throw new ArgumentNullException("form.CidUserAccount");
                    }
                    break;
                case FailureResult.MRB:
                    if (string.IsNullOrEmpty(form.EngUserAccount) && string.IsNullOrEmpty(form.CidUserAccount)
                        && string.IsNullOrEmpty(form.CsdUserAccount) && string.IsNullOrEmpty(form.FinUserAccount))
                    {
                        throw new ArgumentException("没有mrb成员");
                    }
                    break;
            }

            string hasCidUser = string.IsNullOrEmpty(form.CidUserAccount) ? "false" : "true";
            string hasEngUser = string.IsNullOrEmpty(form.EngUserAccount) ? "false" : "true";
            string hasFinUser = string.IsNullOrEmpty(form.FinUserAccount) ? "false" : "true";

            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_ENG_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.EngUserAccount)} },
                    new Variable{ strVariableName = PARAM_CID_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.CidUserAccount)} },
                    new Variable{ strVariableName = PARAM_CSD_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.CsdUserAccount)} },
                    new Variable{ strVariableName = PARAM_REWORK_PMC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.PmcUserAccount)} },
                    new Variable{ strVariableName = PARAM_FIN_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.FinUserAccount)} },
                    new Variable{ strVariableName = PARAM_QC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QCUserAccount)} },
                    new Variable{ strVariableName = PARAM_MRB_USERS, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QEUserAccount), this.GetUltimusUserAccount(form.CidUserAccount), this.GetUltimusUserAccount(form.CsdUserAccount),this.GetUltimusUserAccount(form.EngUserAccount)} },
                    new Variable{ strVariableName = PARAM_NEED_MRB, objVariableValue = new object[]{(form.QEResult == FailureResult.MRB).ToString().ToLower()} },
                    new Variable{ strVariableName = PARAM_RESULT, objVariableValue = new object[]{result} },
                    new Variable{ strVariableName = PARAM_HAS_CID_USER, objVariableValue = new object[]{hasCidUser} },
                    new Variable{ strVariableName = PARAM_HAS_ENG_USER, objVariableValue = new object[]{hasEngUser} },
                    new Variable{ strVariableName = PARAM_HAS_FIN_USER, objVariableValue = new object[]{hasFinUser} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
            if (!string.IsNullOrEmpty(emailTo))
            {
                this.SendResultEmail(form, emailTo);
            }
        }

        private void SendResultEmail(FailureProductForm form, string emailTo)
        {
            string content = this._resultEmailContentTemplate;
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Replace("${ComponentCode}", form.ComponentCode)
                    .Replace("${ComponentName}", form.ComponentName)
                    .Replace("${BJXLH}", form.BJXLH)
                    .Replace("${JZXLH}", form.JZXLH)
                    .Replace("${GYSDM}", form.GYSDM)
                    .Replace("${GYSMC}", form.GYSMC)
                    .Replace("${MO}", form.MO)
                    .Replace("${UM}", form.UM)
                    .Replace("${OrderCode}", form.OrderCode)
                    .Replace("${FailurePlace}", form.FailurePlace)
                    .Replace("${ZRBM}", form.ZRBM)
                    .Replace("${Quantity}", form.Quantity)
                    .Replace("${Source}", form.Source)
                    .Replace("${Reason}", string.IsNullOrEmpty(form.ReasonRemark) ? form.Reason : form.ReasonRemark)
                    .Replace("${Remark}", form.Remark)
                    .Replace("${Level}", form.Level)
                    .Replace("${QEResult}", this.Map(form.QEResult))
                    .Replace("${Analysis}", form.Analysis);
                ProcessEmailDataProvider.Current.Insert(emailTo, "不合品流程处理判断结果", content);
            }
        }

        public string Map(FailureResult result)
        {
            switch (result)
            {
                case FailureResult.MRB: return "MRB会议";
                case FailureResult.Pick: return "挑选";
                case FailureResult.Receive: return "让步接收";
                case FailureResult.Return: return "退回供应商";
                case FailureResult.Rework: return "返工/返修";
                case FailureResult.Scrap: return "报废";
            }
            return "";
        }

        public void MrbSend(string taskId, FailureProductForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (form.MrbResults == null || form.MrbResults.Count == 0)
            {
                throw new ArgumentNullException("form.MrbResults");
            }
            FailureResult finalResult = form.MrbResults[0].Result;
            foreach (MrbFailureResult mrbResult in form.MrbResults)
            {
                if (mrbResult.Result != finalResult)
                {
                    finalResult = FailureResult.None;
                    break;
                }
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_RESULT, objVariableValue = new object[]{finalResult} },
                    new Variable{ strVariableName = PARAM_MRB_RESULT_COUNT, objVariableValue = new object[]{form.MrbResults.Count} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void QASend(string taskId, FailureProductForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (form.QAResult == FailureResult.None)
            {
                throw new ArgumentException("form.QAResult 不能为none");
            }
            FailureResult finalResult = form.QAResult;
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_RESULT, objVariableValue = new object[]{finalResult} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void Send(string taskId, FailureProductForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public void Return(string taskId, FailureProductForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Return(taskId, null, "", this.GetSummary(form), form);
        }
    }
}
