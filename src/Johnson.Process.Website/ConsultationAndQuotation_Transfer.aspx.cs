using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class ConsultationAndQuotation_Transfer : ProcessTransfer
    {
        protected override void Transfer()
        {
            TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(TaskId);
            object objStepId = WebHelper.VocProcess.GetVariableValue(TaskId, "StepId");
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }
            switch (objStepId.ToString())
            {
                case "11":
                    if (taskInfo.SubStatus == 16)
                    {
                        Response.Redirect("ConsultationAndQuotation_Start_Return.aspx?" + Request.QueryString.ToString());
                    }
                    else if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_Start.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("ConsultationAndQuotation_Start_Completed.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "21":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_Marketing.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "31":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_CSD.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "41":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_CsdTrace.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "51":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_LOG.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "61":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_SCM.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "71":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_CID.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "81":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_ENG.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "91":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_QAD.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "101":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_CsdTrace2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "111":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ConsultationAndQuotation_Marketing2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
            }
            if (taskInfo.Status != 1)
            {
                Response.Redirect("ConsultationAndQuotation_Completed.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}