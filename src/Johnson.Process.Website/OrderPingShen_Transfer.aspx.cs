using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class OrderPingShen_Transfer : ProcessTransfer
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
                    if (taskInfo.IncidentNo == 0)
                    {
                        Response.Redirect("OrderPingShen_Start.aspx?" + Request.QueryString.ToString());
                    }
                    else if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_Start_Return.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "21":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_Dept.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "31":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SheJiFuZeRen.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "41":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_DianQiEngineer.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "51":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SheJiEngineer.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "61":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SheJiFuZeRen2.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "71":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_BOM.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "81":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_PMC.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "91":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SCM.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "101":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_JiZuWanGong.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "111":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SheJiTiJiao.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "121":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_JiShuJianCha.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "131":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_JiShuZuGuanShenPi.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "141":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SapBom.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "151":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_FilePublish.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "161":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_XinWuLiaoXinXiWeiHu.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "171":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_CID.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "181":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_QAD.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed1.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "191":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_JiZuWanGongQueRen.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
                    }
                    break;
                case "201":
                    if (taskInfo.Status == 1)
                    {
                        Response.Redirect("OrderPingShen_SapBom.aspx?" + Request.QueryString.ToString());
                    }
                    else
                    {
                        Response.Redirect("OrderPingShen_Completed2.aspx?" + Request.QueryString.ToString());
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