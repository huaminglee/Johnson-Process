using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using Johnson.Process.Core;
using Newtonsoft.Json;

namespace Johnson.Process.Website
{
    public partial class OrderWenjianFafang_Controller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }
            Response.ContentType = "application/json";

            if (action.Equals("Get", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Get();
            }
            else if (action.Equals("Start", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Start();
            }
            else if (action.Equals("JiShuJianChaSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiShuJianChaSubmit();
            }
            else if (action.Equals("JiShuZhuGuanShenPiSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiShuZhuGuanShenPiSubmit();
            }
            else if (action.Equals("FafangSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.FafangSubmit();
            }
            else if (action.Equals("BomLuRuSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.BomLuRuSubmit();
            }
            else if (action.Equals("XinWuLiaoXinXiWeiHuSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.XinWuLiaoXinXiWeiHuSubmit();
            }
        }

        private void Get()
        {
            try
            {
                OrderWenjianFafangForm orderWenjianFafangForm = null;
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    orderWenjianFafangForm = WebHelper.OrderWenjianFafangProcess.Get(int.Parse(Request["incNo"]));
                }
                else
                {
                    taskId = taskId.Trim();
                    orderWenjianFafangForm = WebHelper.OrderWenjianFafangProcess.Get(taskId);
                }

                OrderWenjianFafangModel model = null;
                if (orderWenjianFafangForm == null)
                {
                    int pingshenIncNo = int.Parse(Request["pingshenIncNo"]);
                    OrderPingShenForm orderPingShenForm = WebHelper.OrderPingShenProcess.Get(pingshenIncNo);
                    model = new OrderWenjianFafangModel(orderPingShenForm);
                    model.startUserName = WebHelper.CurrentUserInfo.UserRealName;
                }
                else
                {
                    OrderPingShenForm orderPingShenForm = WebHelper.OrderPingShenProcess.Get(orderWenjianFafangForm.OrderPingshenIncidentNo);
                    model = new OrderWenjianFafangModel(orderPingShenForm, orderWenjianFafangForm);
                }
                Response.Write(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void Start()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                SheJiTiJiaoSubmitModel model = JsonConvert.DeserializeObject<SheJiTiJiaoSubmitModel>(formJson);
                if (string.IsNullOrEmpty(model.taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(model.taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName };

                OrderPingShenForm orderPingShenForm = WebHelper.OrderPingShenProcess.Get(model.pingshenIncNo);

                TaskSendResult result = WebHelper.OrderWenjianFafangProcess.Start(model.taskId, WebHelper.CurrentUserInfo.UserLoginName, WebHelper.CurrentUserInfo.UserRealName, 
                    approveInfo, model.hasXinWuLiao, model.jianChaEngineerAccount, model.jianChaEngineerName, model.zhuGuanAccount,
                    model.zhuGuanName, model.sheJiShuoMing, model.fafangWancheng, model.sheJiZiLiao, model.pingshenIncNo, model.scmEngineerAccount);

                OrderWenjianFafangForm orderWenjianFafangForm = WebHelper.OrderWenjianFafangProcess.Get(result.IncidentNo);
                WebHelper.OrderPingShenProcess.AddWenJianFaFangLiucheng(orderWenjianFafangForm, model.pingshenIncNo);

            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void JiShuJianChaSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderWenjianFafangProcess.JiShuChaJian(taskId, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void JiShuZhuGuanShenPiSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderWenjianFafangProcess.JiShuZhuGuanShenPi(taskId, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void FafangSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderWenjianFafangProcess.WenJianFaFang(taskId, approveInfo);
                OrderWenjianFafangForm form = WebHelper.OrderWenjianFafangProcess.Get(taskId);
                if (form.FafangWancheng)
                {
                    WebHelper.OrderPingShenProcess.WenJianFaFang(form.SheJiZiLiao, form.OrderPingshenIncidentNo);
                }
                else
                {
                    WebHelper.OrderPingShenProcess.SaveWenJianFaFang(form.SheJiZiLiao, form.OrderPingshenIncidentNo);
                }
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void BomLuRuSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderWenjianFafangProcess.BomLuRu(taskId, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void XinWuLiaoXinXiWeiHuSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderWenjianFafangProcess.XinWuLiaoXinXiWeiHu(taskId, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }
    }
}