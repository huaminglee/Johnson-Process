using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class Delivery_Transfer : ProcessTransfer
    {
        protected override void Transfer()
        {
            TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(TaskId);
            object objStepId = WebHelper.DeliveryProcess.GetVariableValue(TaskId, "StepId");
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }
            WebHelper.Logger.Info(taskInfo.Status);
            WebHelper.Logger.Info(taskInfo.SubStatus);
            switch (objStepId.ToString())
            {
                case "11":
                    if (taskInfo.IncidentNo == 0)
                    {
                        Response.Redirect("Delivery_Start.aspx?" + Request.QueryString.ToString());
                    }
                    else if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Delivery_Start_Return.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("Delivery_Start_Completed.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "21":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Delivery_Custom.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "31":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Delivery_Logistics.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "41":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Delivery_Custom2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
            }
            if (taskInfo.Status != 1)
            {
                Response.Redirect("Delivery_Completed.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}