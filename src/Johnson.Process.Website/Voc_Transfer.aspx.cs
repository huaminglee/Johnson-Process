using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class Voc_Transfer : ProcessTransfer
    {
        protected override void Transfer()
        {
            TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(TaskId);
            object objStepId = WebHelper.VocProcess.GetVariableValue(TaskId, "StepId");
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }
            WebHelper.Logger.Info("IncidentNo:" + taskInfo.IncidentNo);
            switch (objStepId.ToString())
            {
                case "11":
                    if (taskInfo.IncidentNo == 0)
                    {
                        Response.Redirect("Voc_Start.aspx?" + Request.QueryString.ToString());
                    }
                    else if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Voc_Start_Return.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("Voc_StartCompleted.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "21":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Voc_ResponsibleActinPlan.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("Voc_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "22":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("VOC_Solutions.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "31":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("VOC_ActionCompleted.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("Voc_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "32":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("VOC_CompletedSolutions.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "41":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Voc_Responsible.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("Voc_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "42":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Voc_ResponsibleReason.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "43":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("VOC_Measures.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "51":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Voc_ResponsiblePrevious.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("Voc_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "61":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("Voc_ASD.aspx?" + Request.QueryString.ToString());
                    }
                    break;
            }
            if (taskInfo.Status != 1)
            {
                Response.Redirect("Voc_Completed2.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}