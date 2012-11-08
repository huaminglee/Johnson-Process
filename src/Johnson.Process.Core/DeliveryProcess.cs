using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class DeliveryProcess: UltimusProcess
    {
        public DeliveryProcess(string processName)
            :base(processName)
        {
            
        }

        public DeliveryProcessForm Get(string taskId)
        {
            return this.Get<DeliveryProcessForm>(taskId);
        }

        public List<ProcessForm<DeliveryProcessForm>> Get()
        {
            return this.Get<DeliveryProcessForm>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="customerServiceEngineer">客服部工程师</param>
        /// <param name="task"></param>
        /// <returns></returns>
        public TaskSendResult Start(string startUserAccount, string customerServiceEngineer, string taskId, DeliveryProcessForm form)
        {
            if (string.IsNullOrEmpty(startUserAccount))
            {
                throw new ArgumentNullException("startUserAccount");
            }
            if (string.IsNullOrEmpty(customerServiceEngineer))
            {
                throw new ArgumentNullException("customerServiceEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = "customerServiceEngineer", objVariableValue = new object[]{this.GetUltimusUserAccount(customerServiceEngineer)} }
                };
            return this.Start(startUserAccount, taskId, variable, "", form.ProjectName, form);
        }

        public void CustomerServiceEngineerSend(string logisticsEngineer, string taskId, DeliveryProcessForm form)
        {
            if (string.IsNullOrEmpty(logisticsEngineer))
            {
                throw new ArgumentNullException("logisticsEngineer");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = "logisticsEngineer", objVariableValue = new object[]{this.GetUltimusUserAccount(logisticsEngineer)} },
                    new Variable{ strVariableName = "needLogisticsReply", objVariableValue = new object[]{"true"} }
                };
            this.Send(taskId, variable, "", form.ProjectName, form);
        }

        public void CustomerServiceEngineerSend(string taskId, DeliveryProcessForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = "logisticsEngineer", objVariableValue = new object[]{this.GetUltimusUserAccount("")} },
                    new Variable{ strVariableName = "needLogisticsReply", objVariableValue = new object[]{"false"} }
                };
            this.Send(taskId, variable, "", form.ProjectName, form);
        }

        public void Send(string taskId, DeliveryProcessForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, null, "", form.ProjectName, form);
        }

        public void Return(string taskId, DeliveryProcessForm form)
        {
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Return(taskId, null, "", form.ProjectName, form);
        }
    }
}
