using System;
using System.Collections.Generic;
using System.Web;
using JueKit.Web.UI;
using Johnson.Process.Core;
using Ultimus.WFServer;
using EDoc2.Website;

namespace Johnson.Process.Website
{
    public class ProcessTransfer : ProcessPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(WebHelper.CurrentUserInfo == null)
            {
                Response.Write("请无法获取身份信息，请选登录！");
                Response.End();
            }
            Task task = UltimusHelper.GetTask(TaskId);
            WebHelper.Logger.Info(task.nStepType);
            if (task.nStepType != 2)
            {
                string taskUser = task.strAssignedToUser;
                string[] taskUserSplit = taskUser.Split('/');
                if (taskUserSplit.Length == 2)
                {
                    taskUser = taskUserSplit[1];
                }
                if (!WebHelper.CurrentUserInfo.UserLoginName.Equals(taskUser, StringComparison.InvariantCultureIgnoreCase))
                {
                    Response.Write(string.Format("请无法获取{0}身份信息，请先使用{0}选登录！", taskUser));
                    Response.End();
                }
            }
            
            this.Transfer();
        }

        protected virtual void Transfer()
        {

        }
    }
}