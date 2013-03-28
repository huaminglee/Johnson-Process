using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class ConsultationAndQuotationProcess : UltimusFormProcess<ConsultationAndQuotationForm>
    {

        /// <summary>
        /// 市场部工程师
        /// </summary>
        private const string PARAM_MARKETING_ENGINEER = "marketingEngineer";
        /// <summary>
        /// 市场部工程师是否能决策
        /// </summary>
        private const string PARAM_MARKETING_ENGINEER_CAN_CECISION = "marketingEngineerCanDecision";

        /// <summary>
        /// 客服部工程师
        /// </summary>
        private const string PARAM_CSD_ENGINEER = "csdEngineer";
        /// <summary>
        /// 客服部工程师是否能决策
        /// </summary>
        private const string PARAM_CSD_ENGINEER_CAN_CECISION = "csdEngineerCanDecision";

        /// <summary>
        /// 客服部跟进人
        /// </summary>
        private const string PARAM_CSD_TRACER = "csdTracer";
        /// <summary>
        /// 客服部跟进人是否能决策
        /// </summary>
        private const string PARAM_CSD_TRACER_CAN_CECISION = "csdTracerCanDecision";
        /// <summary>
        /// 需要技术部工程师决策
        /// </summary>
        private const string PARAM_NEED_ENG_CECISION = "needEngDecision";
        /// <summary>
        /// 需要物流部工程师决策
        /// </summary>
        private const string PARAM_NEED_LOG_CECISION = "needLogDecision";
        /// <summary>
        /// 需要采购部工程师决策
        /// </summary>
        private const string PARAM_NEED_SCM_CECISION = "needScmDecision";

        /// <summary>
        /// 技术部工程师
        /// </summary>
        private const string PARAM_ENG_ENGINEER = "engEngineer";
        /// <summary>
        /// 技术部工程师是否能决策
        /// </summary>
        private const string PARAM_ENG_ENGINEER_CAN_CECISION = "engEngineerCanDecision";

        /// <summary>
        /// 物流部工程师
        /// </summary>
        private const string PARAM_LOG_ENGINEER = "logEngineer";

        /// <summary>
        /// 采购部工程师
        /// </summary>
        private const string PARAM_SCM_ENGINEER = "scmEngineer";

        /// <summary>
        /// 质保部工程师
        /// </summary>
        private const string PARAM_QAD_ENGINEER = "qadEngineer";
        /// <summary>
        /// 是否需要质保部工程师建议
        /// </summary>
        private const string PARAM_NEED_QAD_ENGINEER_SUGGESTION = "needQadEngineerSuggestion";

        /// <summary>
        /// 工艺部工程师
        /// </summary>
        private const string PARAM_CID_ENGINEER = "cidEngineer";
        /// <summary>
        /// 是否需要工艺部工程师建议
        /// </summary>
        private const string PARAM_NEED_CID_ENGINEER_SUGGESTION = "needCidEngineerSuggestion";

        private const string PARAM_LOG_STEP_CAN_ACTIVE_NEXT = "log_step_can_active_next";
        private const string PARAM_SCM_STEP_CAN_ACTIVE_NEXT = "scm_step_can_active_next";
        private const string PARAM_CID_STEP_CAN_ACTIVE_NEXT = "cid_step_can_active_next";
        private const string PARAM_QAD_STEP_CAN_ACTIVE_NEXT = "qad_step_can_active_next";

        string _toCsdEmailContentTemplate;
        string _tracerMailTemplate;

        public ConsultationAndQuotationProcess(string processName, string toCsdEmailContentTemplate, string tracerMailTemplate)
            :base(processName)
        {
            this._toCsdEmailContentTemplate = toCsdEmailContentTemplate;
            this._tracerMailTemplate = tracerMailTemplate;
        }

        private string GetSummary(ConsultationAndQuotationForm form)
        {
            return form.ProjectName;
        }

        public TaskSendResult Start(string startUserAccount, string marketingEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(startUserAccount))
            {
                throw new ArgumentNullException("startUserAccount");
            }
            if (string.IsNullOrEmpty(marketingEngineer))
            {
                throw new ArgumentNullException("marketingEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_MARKETING_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(marketingEngineer)} }
                };
            return this.Start(startUserAccount, taskId, variable, "", this.GetSummary(form), form);
        }

        public TaskSendResult StartReturnedSend(string marketingEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(marketingEngineer))
            {
                throw new ArgumentNullException("marketingEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_MARKETING_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(marketingEngineer)} }
                };
            return this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void MarketingSend(string csdEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(csdEngineer))
            {
                throw new ArgumentNullException("csdEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_MARKETING_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                    new Variable{ strVariableName = PARAM_CSD_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(csdEngineer)} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void MarketingSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_MARKETING_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"true"} },
            };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
            if(!string.IsNullOrEmpty(form.MarketingEmailTo))
            {
                ProcessEmailDataProvider.Current.Insert(form.MarketingEmailTo, this.GetMarketingEmailSubject(form), this.GetMarketingEmailContent(form));
            }
        }

        private string GetMarketingEmailSubject(ConsultationAndQuotationForm form)
        {
            return "技术咨询及报价-市场部报价";
        }

        private string GetCsdEmailSubject(ConsultationAndQuotationForm form)
        {
            return "技术咨询及报价-CSD工程师报价";
        }

        private string GetMarketingEmailContent(ConsultationAndQuotationForm form)
        {
            StringBuilder sb = new StringBuilder();
            if (form.Products != null)
            {
                foreach (ConsultationAndQuotationProductInfo productInfo in form.Products)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", 
                        productInfo.ProductModel, productInfo.Quantity, HtmlHelper.GetTextareaHtml(productInfo.Remark),
                        productInfo.MarketingWithoutSalesTP, productInfo.MarketingWithSalesTP, productInfo.MarketingTotalSalesTP);
                }
            }
            return this._toCsdEmailContentTemplate
                .Replace("${ProjectName}", form.ProjectName)
                .Replace("${ApplyUserDepartmentName}", form.ApplyUserDepartmentName)
                .Replace("${ApplyUserName}", form.ApplyUserName)
                .Replace("${SucceedProbability}", form.SucceedProbability)
                .Replace("${ExpectSignContact}", form.ExpectSignContact.ToString("yyyy-MM-dd"))
                .Replace("${LeadTime}", form.LeadTime.Value.ToString())
                .Replace("${LeadTimeRemark}", HtmlHelper.GetTextareaHtml(form.LeadTimeRemark))
                .Replace("${products}", sb.ToString());
        }

        private string GetCsdEmailContent(ConsultationAndQuotationForm form)
        {
            StringBuilder sb = new StringBuilder();
            if (form.Products != null)
            {
                foreach (ConsultationAndQuotationProductInfo productInfo in form.Products)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", 
                        productInfo.ProductModel, productInfo.Quantity, HtmlHelper.GetTextareaHtml(productInfo.Remark),
                        productInfo.CsdWithoutSalesTP, productInfo.CsdWithSalesTP, productInfo.CsdTotalSalesTP);
                }
            }
            return this._tracerMailTemplate
                .Replace("${ProjectName}", form.ProjectName)
                .Replace("${ApplyUserDepartmentName}", form.ApplyUserDepartmentName)
                .Replace("${ApplyUserName}", form.ApplyUserName)
                .Replace("${SucceedProbability}", form.SucceedProbability)
                .Replace("${ExpectSignContact}", form.ExpectSignContact.ToString("yyyy-MM-dd"))
                .Replace("${LeadTime}", form.LeadTime.Value.ToString())
                .Replace("${LeadTimeRemark}", HtmlHelper.GetTextareaHtml(form.LeadTimeRemark))
                .Replace("${products}", sb.ToString());
        }

        public void CsdEngineerSend(string csdTracer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(csdTracer))
            {
                throw new ArgumentNullException("csdTracer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                    new Variable{ strVariableName = PARAM_CSD_TRACER, objVariableValue = new object[]{this.GetUltimusUserAccount(csdTracer)} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void CsdEngineerSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"true"} },
            };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
            if (!string.IsNullOrEmpty(form.CsdEmailTo))
            {
                ProcessEmailDataProvider.Current.Insert(form.CsdEmailTo, this.GetCsdEmailSubject(form), this.GetCsdEmailContent(form));
            }
        }

        public void CsdTracerSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_TRACER_CAN_CECISION, objVariableValue = new object[]{"true"} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
            if (!string.IsNullOrEmpty(form.CsdTracerEmailTo))
            {
                ProcessEmailDataProvider.Current.Insert(form.CsdTracerEmailTo, this.GetCsdEmailSubject(form), this.GetCsdEmailContent(form));
            }
        }

        public void CsdTracerSendToEng(string engEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(engEngineer))
            {
                throw new ArgumentNullException("engEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_TRACER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                    new Variable{ strVariableName = PARAM_NEED_ENG_CECISION, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_ENG_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(engEngineer)} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void CsdTracerSendToLog(string logEnigneer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(logEnigneer))
            {
                throw new ArgumentNullException("logEnigneer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            object[] accounts = this.GetUltimusUserAccounts(logEnigneer);
            List<object> objAccounts = new List<object>();
            foreach (object account in accounts)
            {
                objAccounts.Add(account);
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_TRACER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                    new Variable{ strVariableName = PARAM_NEED_LOG_CECISION, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_LOG_ENGINEER, objVariableValue = objAccounts.ToArray() }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void CsdTracerSendToScm(string scmEnigneer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(scmEnigneer))
            {
                throw new ArgumentNullException("scmEnigneer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_TRACER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                    new Variable{ strVariableName = PARAM_NEED_SCM_CECISION, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_SCM_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(scmEnigneer)} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void CsdTracerSendToLogAndScm(string logEnigneer, string scmEnigneer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(scmEnigneer))
            {
                throw new ArgumentNullException("scmEnigneer");
            }
            if (string.IsNullOrEmpty(scmEnigneer))
            {
                throw new ArgumentNullException("logEnigneer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_CSD_TRACER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                    new Variable{ strVariableName = PARAM_NEED_SCM_CECISION, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_SCM_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(scmEnigneer)}},
                    new Variable{ strVariableName = PARAM_NEED_LOG_CECISION, objVariableValue = new object[]{"true"} },
                    new Variable{ strVariableName = PARAM_LOG_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(logEnigneer)} }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void LogSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            string canActiveNext = "true";
            object needScmDecision = this.GetVariableValue(taskId, PARAM_NEED_SCM_CECISION);
            if (needScmDecision != null && needScmDecision.ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!form.ScmExecuted)
                {
                    canActiveNext = "false";
                }
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_LOG_STEP_CAN_ACTIVE_NEXT, objVariableValue = new object[]{canActiveNext} } 
                };
            form.LogExecuted = true;
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void ScmSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            string canActiveNext = "true";
            object needLogDecision = this.GetVariableValue(taskId, PARAM_NEED_LOG_CECISION);
            if (needLogDecision != null && needLogDecision.ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!form.LogExecuted)
                {
                    canActiveNext = "false";
                }
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_SCM_STEP_CAN_ACTIVE_NEXT, objVariableValue = new object[]{canActiveNext} } 
            };
            form.ScmExecuted = true;
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void CidSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            string canActiveNext = "true";
            object needQadSuggestion = this.GetVariableValue(taskId, PARAM_NEED_QAD_ENGINEER_SUGGESTION);
            if (needQadSuggestion != null && needQadSuggestion.ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!form.QadExecuted)
                {
                    canActiveNext = "false";
                }
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_CID_STEP_CAN_ACTIVE_NEXT, objVariableValue = new object[]{canActiveNext} }
            };
            form.CidExecuted = true;
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void QadSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            string canActiveNext = "true";
            object needCidSuggestion = this.GetVariableValue(taskId, PARAM_NEED_CID_ENGINEER_SUGGESTION);
            if (needCidSuggestion != null && needCidSuggestion.ToString().Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!form.CidExecuted)
                {
                    canActiveNext = "false";
                }
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_QAD_STEP_CAN_ACTIVE_NEXT, objVariableValue = new object[]{canActiveNext} } 
            };
            form.QadExecuted = true;
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void EngEngineerSendToCid(string cidEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(cidEngineer))
            {
                throw new ArgumentNullException("cidEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_ENG_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                new Variable{ strVariableName = PARAM_CID_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(cidEngineer)} },
                new Variable{ strVariableName = PARAM_NEED_CID_ENGINEER_SUGGESTION, objVariableValue = new object[]{"true"} },
                new Variable{ strVariableName = PARAM_NEED_QAD_ENGINEER_SUGGESTION, objVariableValue = new object[]{"false"} }
            };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void EngEngineerSendToQad(string qadEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(qadEngineer))
            {
                throw new ArgumentNullException("qadEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_ENG_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                new Variable{ strVariableName = PARAM_QAD_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(qadEngineer)} },
                new Variable{ strVariableName = PARAM_NEED_CID_ENGINEER_SUGGESTION, objVariableValue = new object[]{"false"} },
                new Variable{ strVariableName = PARAM_NEED_QAD_ENGINEER_SUGGESTION, objVariableValue = new object[]{"true"} }
            };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void EngEngineerSendToQadAndCid(string cidEngineer, string qadEngineer, string taskId, ConsultationAndQuotationForm form)
        {
            if (string.IsNullOrEmpty(cidEngineer))
            {
                throw new ArgumentNullException("cidEngineer");
            }
            if (string.IsNullOrEmpty(qadEngineer))
            {
                throw new ArgumentNullException("qadEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_ENG_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"false"} },
                new Variable{ strVariableName = PARAM_CID_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(cidEngineer)} },
                new Variable{ strVariableName = PARAM_QAD_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(qadEngineer)} } ,
                new Variable{ strVariableName = PARAM_NEED_CID_ENGINEER_SUGGESTION, objVariableValue = new object[]{"true"} },
                new Variable{ strVariableName = PARAM_NEED_QAD_ENGINEER_SUGGESTION, objVariableValue = new object[]{"true"} }
            };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void EngEngineerSend(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_ENG_ENGINEER_CAN_CECISION, objVariableValue = new object[]{"true"} },
                new Variable{ strVariableName = PARAM_NEED_CID_ENGINEER_SUGGESTION, objVariableValue = new object[]{"false"} },
                new Variable{ strVariableName = PARAM_NEED_QAD_ENGINEER_SUGGESTION, objVariableValue = new object[]{"false"} }
            };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void CsdTracer2Send(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, form);
            if (!string.IsNullOrEmpty(form.CsdTracerEmailTo))
            {
                ProcessEmailDataProvider.Current.Insert(form.CsdTracerEmailTo, this.GetCsdEmailSubject(form), this.GetCsdEmailContent(form));
            }
        }

        public void Send(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public void Return(string taskId, ConsultationAndQuotationForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Return(taskId, null, "", this.GetSummary(form), form);
        }
    }
}
