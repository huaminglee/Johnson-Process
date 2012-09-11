using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using System.Data.SqlClient;
using Ultimus.WFServer;
using Newtonsoft.Json;
using EDoc2.Website;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class DeliveryController : System.Web.UI.Page
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
            else if (action.Equals("start", StringComparison.InvariantCultureIgnoreCase))
            {
                    this.Start();
            }
            else if (action.Equals("Custom_Service_Submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Custom_Service_Submit();
            }
            else if (action.Equals("Delivery_Logistics_Submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Delivery_Logistics_Submit();
            }
            else if (action.Equals("Delivery_Logistics_Return", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Delivery_Logistics_Return();
            }
            else if (action.Equals("Delivery_Custom2_Submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Delivery_Custom2_Submit();
            }
            else if (action.Equals("Delivery_Custom2_Return", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Delivery_Custom2_Return();
            }
            else if (action.Equals("return", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Return();
            }
            else if (action.Equals("submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Submit();
            }
        }

        private void Start()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string deliveryJson = Request["delivery"];
                DeliveryStartModel model = JsonConvert.DeserializeObject<DeliveryStartModel>(deliveryJson);
                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                DeliveryProcessForm newForm = model.Map();
                newForm.Approves = new List<TaskApproveInfo>();
                newForm.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });
                
                WebHelper.DeliveryProcess.Start(currentUserName, model.csdEngineer, taskId, newForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Custom_Service_Submit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string deliveryJson = Request["delivery"];
                DeliveryCustomerServiceSendModel model = JsonConvert.DeserializeObject<DeliveryCustomerServiceSendModel>(deliveryJson);

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                DeliveryProcessForm newForm = model.Map();
                newForm.Approves = new List<TaskApproveInfo>();
                newForm.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });
                newForm.Approves.AddRange(oldForm.Approves);

                if (model.needLogisticsReply)
                {
                    WebHelper.DeliveryProcess.CustomerServiceEngineerSend(model.logEngineer, taskId, newForm);
                }
                else
                {
                    WebHelper.DeliveryProcess.CustomerServiceEngineerSend(taskId, newForm);
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

        private void Delivery_Logistics_Submit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string logReply = Request["logReply"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                oldForm.LogReply = logReply;
                oldForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.DeliveryProcess.Send(taskId, oldForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Delivery_Logistics_Return()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string logReply = Request["logReply"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                oldForm.LogReply = logReply;
                oldForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.DeliveryProcess.Return(taskId, oldForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Delivery_Custom2_Submit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string csdReply = Request["csdReply"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                oldForm.CsdReply = csdReply;
                oldForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.DeliveryProcess.Send(taskId, oldForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Delivery_Custom2_Return()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string csdReply = Request["csdReply"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                oldForm.CsdReply = csdReply;
                oldForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.DeliveryProcess.Return(taskId, oldForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Submit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string deliveryJson = Request["delivery"];
                DeliveryModel model = JsonConvert.DeserializeObject<DeliveryModel>(deliveryJson);

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                DeliveryProcessForm newForm = model.Map();
                newForm.Approves = new List<TaskApproveInfo>();
                newForm.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });
                newForm.Approves.AddRange(oldForm.Approves);

                WebHelper.DeliveryProcess.Send(taskId, newForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Return()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string deliveryJson = Request["delivery"];
                DeliveryModel model = JsonConvert.DeserializeObject<DeliveryModel>(deliveryJson);

                string currentUserName = "";
#if DEBUG
                currentUserName = "debuggerUser";
#else
                currentUserName = WebsiteUtility.CurrentUser.UserLoginName;
#endif
                DeliveryProcessForm oldForm = WebHelper.DeliveryProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.DeliveryProcess.GetTaskInfo(taskId);
                DeliveryProcessForm newForm = model.Map();
                newForm.Approves = new List<TaskApproveInfo>();
                newForm.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });
                newForm.Approves.AddRange(oldForm.Approves);

                WebHelper.DeliveryProcess.Return(taskId, newForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Get()
        {
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                DeliveryProcessForm form = WebHelper.DeliveryProcess.Get(taskId);
                DeliveryModel model = null;
                if (form != null)
                {
                    model = new DeliveryModel(form);
                }
                if (model == null)
                {
                    model = new DeliveryModel();
                    model.bookDate = DateTime.Now.ToString("yyyy-MM-dd");
                    model.requestOutDate = DateTime.Now.ToString("yyyy-MM-dd");
                    model.materials = new List<MaterialModel>();
                    model.files = new List<UploadFileModel>();
                }
                Response.Write(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }
    }
}
