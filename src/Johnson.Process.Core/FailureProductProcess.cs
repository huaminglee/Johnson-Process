using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class FailureProductProcess : UltimusProcess
    {
        public FailureProductProcess(string processName)
            :base(processName)
        {
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

        public ProductReworkProcess ProductReworkProcess { private set; get; }

        public FailureProductForm Get(string taskId)
        {
            return this.Get<FailureProductForm>(taskId);
        }

        private string GetSummary(FailureProductForm form)
        {
            return "";
        }

        public TaskSendResult Start(string startUserAccount, string taskId, FailureProductForm form)
        {
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

        public void QESend(string taskId, bool needMrb, FailureProductForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (needMrb)
            {
                if (string.IsNullOrEmpty(form.EngUserAccount))
                {
                    throw new ArgumentNullException("form.EngUserAccount");
                }
                if (string.IsNullOrEmpty(form.CidUserAccount))
                {
                    throw new ArgumentNullException("form.CidUserAccount");
                }
                if (string.IsNullOrEmpty(form.CsdUserAccount))
                {
                    throw new ArgumentNullException("form.CsdUserAccount");
                }
                if (string.IsNullOrEmpty(form.FinUserAccount))
                {
                    throw new ArgumentNullException("form.FinUserAccount");
                }
            }
            if (string.IsNullOrEmpty(form.ReworkPmcUserAccount))
            {
                throw new ArgumentNullException("form.ReworkPmcUserAccount");
            }
            if (string.IsNullOrEmpty(form.StorehouseUserAccount))
            {
                throw new ArgumentNullException("form.StorehouseUserAccount");
            }
            if (string.IsNullOrEmpty(form.QCUserAccount))
            {
                throw new ArgumentNullException("form.QCUserAccount");
            }
            FailureResult result = FailureResult.None;
            if (!needMrb)
            {
                result = form.QEResult;
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_ENG_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.EngUserAccount)} },
                    new Variable{ strVariableName = PARAM_CID_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.CidUserAccount)} },
                    new Variable{ strVariableName = PARAM_CSD_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.CsdUserAccount)} },
                    new Variable{ strVariableName = PARAM_REWORK_PMC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.ReworkPmcUserAccount)} },
                    new Variable{ strVariableName = PARAM_FIN_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.FinUserAccount)} },
                    new Variable{ strVariableName = PARAM_STO_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.StorehouseUserAccount)} },
                    new Variable{ strVariableName = PARAM_QC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QCUserAccount)} },
                    new Variable{ strVariableName = PARAM_MRB_USERS, objVariableValue = new object[]{this.GetUltimusUserAccount(form.CidUserAccount), this.GetUltimusUserAccount(form.CsdUserAccount),this.GetUltimusUserAccount(form.EngUserAccount)} },
                    new Variable{ strVariableName = PARAM_NEED_MRB, objVariableValue = new object[]{needMrb.ToString().ToLower()} },
                    new Variable{ strVariableName = PARAM_RESULT, objVariableValue = new object[]{result} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
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
            FailureResult finalResult = form.QEResult;
            foreach (MrbFailureResult mrbResult in form.MrbResults)
            {
                if (mrbResult.Result != finalResult)
                {
                    finalResult = FailureResult.None;
                    break;
                }
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_RESULT, objVariableValue = new object[]{finalResult} }
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
    }
}
