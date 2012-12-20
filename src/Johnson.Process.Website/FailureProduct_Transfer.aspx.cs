using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class FailureProduct_Transfer : ProcessTransfer
    {
        protected override void Transfer()
        {
            TaskInfo taskInfo = WebHelper.FailureProductProcess.GetTaskInfo(TaskId);
            
            object objStepId = WebHelper.FailureProductProcess.GetVariableValue(TaskId, "StepId");
            
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }
            string[] stepIds = new string[]{"11", "21", "31", "41", "51", "61", "71", "81", "91", "101"};
            List<string> stepIdList = new List<string>();
            stepIdList.AddRange(stepIds);
            if (!stepIdList.Contains(objStepId.ToString()))
            {
                //object dealWay = WebHelper.FailureProductProcess.GetVariableValue(TaskId, "dealWay");
                Response.Redirect("ProductRework_Transfer.aspx?" + Request.QueryString.ToString());
            }

            switch (objStepId.ToString())
            {
                case "11":
                    if (taskInfo.IncidentNo > 0 && taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_Start_Return.aspx?" + Request.QueryString.ToString());
                    }
                    else if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_Start.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "21":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_PMC.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "31":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_QE.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "41":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_MRB.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "51":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_QA.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "61":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_QAOnReceive.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "71":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_QC.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "81":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_Storehouse.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "91":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_Rework.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "101":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("FailureProduct_StarterQueren.aspx?" + Request.QueryString.ToString());
                    }
                    break;

            }

            if (taskInfo.Status != 1)
            {
                Response.Redirect("FailureProduct_Completed.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}