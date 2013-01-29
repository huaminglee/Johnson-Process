using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class OrderWenjianFafang_Transfer : ProcessTransfer
    {
        protected override void Transfer()
        {
            TaskInfo taskInfo = WebHelper.OrderWenjianFafangProcess.GetTaskInfo(TaskId);
            object objStepId = WebHelper.OrderWenjianFafangProcess.GetVariableValue(TaskId, "StepId");
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }
            switch (objStepId.ToString())
            {
                case "111":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_Start.aspx?" + Request.QueryString.ToString());
                    }
                    else if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_Start_Return.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "121":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_Jiancha.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "131":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_Zhuguan.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "141":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_BOM.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "151":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_Fafang.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "161":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_WuliaoWeihu.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "201":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderWenjianFafang_BOM.aspx?" + Request.QueryString.ToString());
                    }
                    break;
            }
            if (taskInfo.Status != 1)
            {
                Response.Redirect("OrderWenjianFafang_Completed.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}