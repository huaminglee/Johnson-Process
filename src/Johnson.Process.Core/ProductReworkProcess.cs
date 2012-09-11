using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class ProductReworkProcess : UltimusProcess
    {
        private const string PARAM_DEAL_WAY = "dealWay";
        /// <summary>
        /// 财务核算人
        /// </summary>
        private const string PARAM_FIN_USER = "finUser";
        /// <summary>
        /// 技术执行人
        /// </summary>
        private const string PARAM_ENG_USER = "engUser";
        /// <summary>
        /// 返工返修pmc执行人
        /// </summary>
        private const string PARAM_REWORK_PMC_USER = "reworkPmcUser";
        /// <summary>
        /// qc执行人
        /// </summary>
        private const string PARAM_QC_USER = "qcUser";
        /// <summary>
        /// qe执行人
        /// </summary>
        private const string PARAM_QE_USER = "qeUser";
        /// <summary>
        /// 工艺执行人
        /// </summary>
        private const string PARAM_CID_USER = "cidUser";

        public ProductReworkProcess(string processName)
            :base(processName)
        {

        }

        private string GetSummary(ProductReworkForm form)
        {
            return "";
        }

        public TaskSendResult Start(string startUserAccount, string taskId, ProductReworkForm form)
        {
            if (string.IsNullOrEmpty(startUserAccount))
            {
                throw new ArgumentNullException("startUserAccount");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (string.IsNullOrEmpty(form.QCUserAccount))
            {
                throw new ArgumentNullException("form.QCUserAccount");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_DEAL_WAY, objVariableValue = new object[]{"Rework"}},
                    new Variable{ strVariableName = PARAM_QC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QCUserAccount)} } 
                };
            return this.Start(startUserAccount, taskId, variable, "", this.GetSummary(form), form);
        }

        public void QCSend(string taskId, ProductReworkForm form)
        {

            if (string.IsNullOrEmpty(form.EngUserAccount))
            {
                throw new ArgumentNullException("form.EngUserAccount");
            }
            if (string.IsNullOrEmpty(form.PmcUserAccount))
            {
                throw new ArgumentNullException("form.PmcUserAccount");
            }
            if (string.IsNullOrEmpty(form.FinUserAccount))
            {
                throw new ArgumentNullException("form.FinUserAccount");
            }
            if (string.IsNullOrEmpty(form.QEUserAccount))
            {
                throw new ArgumentNullException("form.QEUserAccount");
            }
            if (string.IsNullOrEmpty(form.CidUserAccount))
            {
                throw new ArgumentNullException("form.CidUserAccount");
            } 
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_ENG_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.EngUserAccount)} },
                    new Variable{ strVariableName = PARAM_REWORK_PMC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.PmcUserAccount)} },
                    new Variable{ strVariableName = PARAM_FIN_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.FinUserAccount)} },
                    new Variable{ strVariableName = PARAM_QE_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QEUserAccount)} },
                    new Variable{ strVariableName = PARAM_CID_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.CidUserAccount)} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void ReworkSend(string taskId, ProductReworkForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_DEAL_WAY, objVariableValue = new object[]{"Rework"}}
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void Send(string taskId, ProductReworkForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public ProductReworkForm Get(string taskId)
        {
            return this.Get<ProductReworkForm>(taskId);
        }
    }
}
