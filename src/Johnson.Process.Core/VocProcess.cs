﻿using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class VocProcess : UltimusFormProcess<VocForm>
    {
        private const string PARAM_RESPONSIBLE_USER = "responsibleUser";
        private const string PARAM_RESPONSIBLE_USER_PREVIOUS = "responsibleUserPrevious";
        private const string PARAM_RESPONSIBLE_NEED_PREVIOUS_AUDIT = "needPreviousAudit";
        private const string PARAM_MEASURES_USER = "measuresUser";
        private const string PARAM_ACTION_USERS = "actionUsers";
        private const string PARAM_XUYAO_JIUZHENG_YUFANG_CUOSHI = "xuyaoJiuzhengYufangCuoshi";

        public VocProcess(string processName)
            :base(processName)
        {
            
        }

        private string GetSummary(VocForm form)
        {
            return string.Format("项目:{0},型号:{1},故障:{2}", form.ProjectName, form.MachineModel, form.FaultCategory);
        }

        public TaskSendResult Start(string startUserAccount, string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(startUserAccount))
            {
                throw new ArgumentNullException("startUserAccount");
            }
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (string.IsNullOrEmpty(form.ResponsibleUserAccount))
            {
                throw new ArgumentNullException("form.ResponsibleUserAccount");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_RESPONSIBLE_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.ResponsibleUserAccount)} }
                };
            return this.Start(startUserAccount, taskId, variable, "", this.GetSummary(form), form);
        }

        public TaskSendResult StartResend(string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (string.IsNullOrEmpty(form.ResponsibleUserAccount))
            {
                throw new ArgumentNullException("form.ResponsibleUserAccount");
            }
            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_RESPONSIBLE_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(form.ResponsibleUserAccount)} }
                };
            return this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void ResponsiblePlanAction(string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (form.Actions == null)
            {
                throw new ArgumentNullException("form.Actions");
            }

            List<object> actionUsers = new List<object>();
            foreach (VocAction act in form.Actions)
            {
                if (string.IsNullOrEmpty(act.UserAccount))
                {
                    throw new ArgumentNullException("act.UserAccount");
                }
                if (actionUsers.Find(x => x.ToString() == this.GetUltimusUserAccount(act.UserAccount)) == null)
                {
                    actionUsers.Add(this.GetUltimusUserAccount(act.UserAccount));
                }
            }

            Variable[] variable = new Variable[]{
                    new Variable{ strVariableName = PARAM_ACTION_USERS, objVariableValue = actionUsers.ToArray() }
                };
            this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public void ResponsibleHandle(string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            string needPreviousAudit = string.IsNullOrEmpty(form.ResponsibleUserPreviousAccount) ? "false" : "true";
            Variable[] variables = new Variable[]{
                    new Variable{ strVariableName = PARAM_RESPONSIBLE_USER_PREVIOUS, objVariableValue = new object[]{ this.GetUltimusUserAccount(form.ResponsibleUserPreviousAccount)} },
                    new Variable{ strVariableName = PARAM_RESPONSIBLE_NEED_PREVIOUS_AUDIT, objVariableValue = new object[]{ needPreviousAudit} }
                };
            this.Send(taskId, variables, "", this.GetSummary(form), form);
        }

        public void ResponsibleReason(string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }

            string xuyaoJiuzhengYufangCuoshi = string.IsNullOrEmpty(form.MeasureUserAccount) ? "否" : "是";
            Variable[] variables = new Variable[]{
                    new Variable{ strVariableName = PARAM_MEASURES_USER, objVariableValue = new object[]{ this.GetUltimusUserAccount(form.MeasureUserAccount)} },
                    new Variable{ strVariableName = PARAM_XUYAO_JIUZHENG_YUFANG_CUOSHI, objVariableValue = new object[]{ xuyaoJiuzhengYufangCuoshi} }
                };
            this.Send(taskId, variables, "", this.GetSummary(form), form);
        }

        public void Send(string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public void Return(string taskId, VocForm form)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            this.Return(taskId, null, "", this.GetSummary(form), form);
        }

        public string GetVocCode()
        {
            string vocCode = MetadataDataProvider.Current.SelectValue("vocCode");
            int intCode;
            int.TryParse(vocCode, out intCode);
            if (intCode == 0)
            {
                intCode = 1;
            }
            else
            {
                intCode++;
            }
            return intCode.ToString();
        }

        public string GetAndSetVocCode()
        {
            string code = this.GetVocCode();
            MetadataDataProvider.Current.Set("vocCode", code.ToString());
            return code;
        }
    }
}
