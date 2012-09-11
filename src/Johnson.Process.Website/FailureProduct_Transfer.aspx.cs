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
            object dealWay = WebHelper.FailureProductProcess.GetVariableValue(TaskId, "dealWay");
            if (dealWay != null && dealWay.ToString().Equals("Rework", StringComparison.InvariantCultureIgnoreCase))
            {
                Response.Redirect("ProductRework_Transfer.aspx?" + Request.QueryString.ToString());
            }

            TaskInfo taskInfo = WebHelper.FailureProductProcess.GetTaskInfo(TaskId);
            object objStepId = WebHelper.FailureProductProcess.GetVariableValue(TaskId, "StepId");
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }

            switch (objStepId.ToString())
            {
                case "11":
                    if (taskInfo.SubStatus == 16)
                    {
                        
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
            }
            if (taskInfo.Status != 1)
            {
                Response.Redirect("FailureProduct_Completed.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}