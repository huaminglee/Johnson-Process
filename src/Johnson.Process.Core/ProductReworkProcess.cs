using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;
using Newtonsoft.Json;

namespace Johnson.Process.Core
{
    public class ProductReworkProcess : UltimusFormProcess<ProductReworkForm>
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

        private const string PARAM_HAS_CID_USER = "hasCidUser";

        private const string PARAM_HAS_ENG_USER = "hasEngUser";

        private const string PARAM_HAS_FIN_USER = "hasFinUser";

        private string _chaosongEmailTemplate;

        public ProductReworkProcess(string processName, string chaosongEmailTemplate)
            :base(processName)
        {
            this._chaosongEmailTemplate = chaosongEmailTemplate;
        }

        private string GetSummary(ProductReworkForm form)
        {
            return string.Format("不合格品编号:{0},名称:{1}", form.FailureNo, form.Name);
        }

        public TaskSendResult Start(string startUserAccount, string startUserName, string taskId, ProductReworkForm form, string emailTo)
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
                    new Variable{ strVariableName = PARAM_QC_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.QCUserAccount)}} ,
                    new Variable{ strVariableName = PARAM_HAS_CID_USER, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_HAS_ENG_USER, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_HAS_FIN_USER, objVariableValue = new object[]{"true"} }
                };
            form.StartUserAccount = startUserAccount;
            form.StartUserName = startUserName;
            form.StartTime = DateTime.Now;
            TaskSendResult result = this.Start(startUserAccount, taskId, variable, "", this.GetSummary(form), form);
            if (!string.IsNullOrEmpty(emailTo))
            {
                this.SendResultEmail(form, emailTo, result.IncidentNo);
            }
            return result;
        }

        public void StartReturnSubmit(string taskId, ProductReworkForm form, string emailTo)
        {
            if (!string.IsNullOrEmpty(emailTo))
            {
                this.SendResultEmail(form, emailTo, this.GetIncidentNo(taskId));
            }
            this.Send(taskId, form);
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

            this.AddForm(form, this.GetIncidentNo(taskId));

            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void PmcSend(string taskId, ProductReworkForm form, string emailTo)
        {
            if (!string.IsNullOrEmpty(emailTo))
            {
                this.SendResultEmail(form, emailTo, this.GetIncidentNo(taskId));
            }
            this.Send(taskId, form);
        }

        public void QC2Send(string taskId, ProductReworkForm form, string emailTo)
        {
            if (!string.IsNullOrEmpty(emailTo))
            {
                this.SendResultEmail(form, emailTo, this.GetIncidentNo(taskId));
            }
            this.Send(taskId, form);
        }

        public void Send(string taskId, ProductReworkForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public void Return(string taskId, ProductReworkForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Return(taskId, null, "", this.GetSummary(form), form);
        }

        private void SendResultEmail(ProductReworkForm form, string emailTo, int incidentNo)
        {
            string content = this._chaosongEmailTemplate;
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Replace("${ProductType}", this.Map(form.ProductType))
                    .Replace("${XLH}", form.XLH)
                    .Replace("${Name}", form.Name)
                    .Replace("${SapNo}", form.SapNo)
                    .Replace("${Quantity}", form.Quantity)
                    .Replace("${OrderNumber}", form.OrderNumber)
                    .Replace("${StartDepartment}", form.StartDepartment)
                    .Replace("${ProductArea}", form.ProductArea)
                    .Replace("${CompletedTime}", form.CompletedTime.ToString("yyyy-MM-dd"))
                    .Replace("${Source}", this.MapSouce(form.Source))
                    .Replace("${FYCD}", this.MapFYCD(form.FYCD))
                    .Replace("${FYCDZ}", form.FYCDZ)
                    .Replace("${SPDH}", form.SPDH)
                    .Replace("${FYQRR}", form.FYQRR)
                    .Replace("${YYMS}", form.YYMS)
                    .Replace("${WLJHAP}", form.WLJHAP)
                    .Replace("${SCJHAP}", form.SCJHAP)
                    .Replace("${FGJG}", this.MapFGJG(form.FGJG))
                    .Replace("${XGCLDH}", form.XGCLDH)
                    .Replace("${incidentNo}", incidentNo.ToString());
                ProcessEmailDataProvider.Current.Insert(emailTo, "返工返修抄送邮件", content);
            }
        }

        public string Map(ProductType productType)
        {
            switch(productType)
            {
                case ProductType.LJ: return "零部件";
                case ProductType.CP: return "产品";
            }
            return productType.ToString();
        }

        public string MapSouce(string souce)
        {
            switch (souce)
            {
                case "0": return "客户退货";
                case "1": return "合同更改";
                case "2": return "样机/小批机";
                case "3": return "在线不合格";
                case "4": return "库存不合格";
                case "5": return "来料检验不合格";
                case "6": return "其它";
            }
            return souce;
        }

        public string MapFYCD(string FYCD)
        {
            switch (FYCD)
            {
                case "0": return "客户";
                case "1": return "办事处";
                case "2": return "保险公司";
                case "3": return "供方";
                case "4": return "工厂内部";
                case "5": return "无费用承担";
            }
            return FYCD;
        }

        public string MapFGJG(string FGJG)
        {
            switch (FGJG)
            {
                case "1": return "合格";
                case "2": return "不合格";
                case "3": return "其它";
            }
            return FGJG;
        }
    }
}
