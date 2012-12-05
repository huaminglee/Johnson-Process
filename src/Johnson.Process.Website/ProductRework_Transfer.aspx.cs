using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class ProductRework_Transfer : ProcessTransfer
    {
        protected override void Transfer()
        {
            TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(TaskId);
            object objStepId = WebHelper.ProductReworkProcess.GetVariableValue(TaskId, "StepId");
            if (objStepId == null || string.IsNullOrEmpty(objStepId.ToString()))
            {
                throw new ArgumentNullException("objStepId");
            }

            switch (objStepId.ToString())
            {
                case "101":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_QC.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "111":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_Eng.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "121":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_Cid.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "131":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_QE.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "141":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_PMC.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "151":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_FIN.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "161":

                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("ProductRework_QC2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
            }
            if (taskInfo.Status != 1)
            {
                Response.Redirect("ProductRework_Completed.aspx?" + Request.QueryString.ToString());
            }
        }
    }
}