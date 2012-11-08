using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;
using Johnson.Process.Website.Models;
using Newtonsoft.Json;

namespace Johnson.Process.Website
{
    public partial class FailureProductController : System.Web.UI.Page
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
            else if (action.Equals("GetReworkModel", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetReworkModel();
            }
            else if (action.Equals("start", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Start();
            }
            else if (action.Equals("startResubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.StartResubmit();
            }
            else if (action.Equals("PmcSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.PmcSubmit();
            }
            else if (action.Equals("QESubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QESubmit();
            }
            else if (action.Equals("QEReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QEReturn();
            }
            else if (action.Equals("MrbSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.MrbSubmit();
            }
            else if (action.Equals("QASubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QASubmit();
            }
            else if (action.Equals("QAOnReceiveSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QAOnReceiveSubmit();
            }
            else if (action.Equals("QAOnReceiveReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QAOnReceiveReturn();
            }
            else if (action.Equals("QCSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QCSubmit();
            }
            else if (action.Equals("StorehouseSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.StorehouseSubmit();
            }
            else if (action.Equals("ReworkSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ReworkSubmit();
            }
            else if (action.Equals("GetFromThirdDatabase", StringComparison.InvariantCultureIgnoreCase))
            {
                this.GetFromThirdDatabase();
            }
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
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                if (form == null)
                {
                    form = new FailureProductForm();
                }
                Response.Write(JsonConvert.SerializeObject(form));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void GetReworkModel()
        {
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                if (form == null)
                {
                    form = new FailureProductForm();
                }

                Response.Write(JsonConvert.SerializeObject(new ProductReworkModel(form)));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void GetFromThirdDatabase()
        {
            try
            {
                string id = Request["id"];
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException("id");
                }

                FailureProductForm form = WebHelper.FailureProductProcess.GetFromThirdDatabase(id);
                Response.Write(JsonConvert.SerializeObject(form));
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
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string formJson = Request["formJson"];
                FailureStartModel model = JsonConvert.DeserializeObject<FailureStartModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = new FailureProductForm();
                form.StartDepartment = WebHelper.CurrentUserInfo.DepartmentName;
                this.SetFormProperty(form, model);
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves = new List<TaskApproveInfo>();
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Start(currentUserName, taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void StartResubmit()
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
                string formJson = Request["formJson"];
                FailureStartModel model = JsonConvert.DeserializeObject<FailureStartModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.StartDepartment = WebHelper.CurrentUserInfo.DepartmentName;
                this.SetFormProperty(form, model);
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void PmcSubmit()
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
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.PmcOpinion = Request["PmcOpinion"];
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = Request["submitRemark"], StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QESubmit()
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
                string formJson = Request["formJson"];
                FailureProductQEModel model = JsonConvert.DeserializeObject<FailureProductQEModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.Analysis = model.Analysis;
                form.CidUserAccount = model.CidUserAccount;
                form.CidUserName = model.CidUserName;
                form.CsdUserAccount = model.CsdUserAccount;
                form.CsdUserName = model.CsdUserName;
                form.EngUserAccount = model.EngUserAccount;
                form.EngUserName = model.EngUserName;
                form.FinUserAccount = model.FinUserAccount;
                form.FinUserName = model.FinUserName;
                form.QCUserAccount = form.StartUserAccount;
                form.Level = model.Level;
                form.ProduceDeal = model.ProduceDeal;
                form.ProduceDealNumber = model.ProduceDealNumber;
                form.QEResult = model.QEResult;
                form.StorehouseUserAccount = model.StorehouseUserAccount;
                form.StorehouseUserName = model.StorehouseUserName;
                form.SupplierDeal = model.SupplierDeal;
                form.SupplierDealBillNumber = model.SupplierDealBillNumber;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.QESend(taskId, form, model.emailTo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QEReturn()
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
                string formJson = Request["formJson"];
                FailureProductQEModel model = JsonConvert.DeserializeObject<FailureProductQEModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.Analysis = model.Analysis;
                form.CidUserAccount = model.CidUserAccount;
                form.CidUserName = model.CidUserName;
                form.CsdUserAccount = model.CsdUserAccount;
                form.CsdUserName = model.CsdUserName;
                form.EngUserAccount = model.EngUserAccount;
                form.EngUserName = model.EngUserName;
                form.FinUserAccount = model.FinUserAccount;
                form.FinUserName = model.FinUserName;
                form.QCUserAccount = form.StartUserAccount;
                form.Level = model.Level;
                form.ProduceDeal = model.ProduceDeal;
                form.ProduceDealNumber = model.ProduceDealNumber;
                form.QEResult = model.QEResult;
                form.StorehouseUserAccount = model.StorehouseUserAccount;
                form.StorehouseUserName = model.StorehouseUserName;
                form.SupplierDeal = model.SupplierDeal;
                form.SupplierDealBillNumber = model.SupplierDealBillNumber;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Return(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void MrbSubmit()
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
                string formJson = Request["formJson"];
                FailureProductMrbModel model = JsonConvert.DeserializeObject<FailureProductMrbModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                if (form.MrbResults == null)
                {
                    form.MrbResults = new List<MrbFailureResult>();
                }
                MrbFailureResult mrbResult = new MrbFailureResult
                {
                    Result = model.MrbResult,
                    UserAccount = WebHelper.CurrentUserInfo.UserLoginName,
                    UserName = currentUserName
                };
                form.MrbResults.Add(mrbResult);
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.MrbSend(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QASubmit()
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
                string formJson = Request["formJson"];
                FailureProductQAModel model = JsonConvert.DeserializeObject<FailureProductQAModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.QAResult = model.QAResult;
                form.ReceiveQARemark = model.ReceiveQARemark;
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.QASend(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QAOnReceiveSubmit()
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
                string ReceiveQARemark = Request["ReceiveQARemark"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.ReceiveQARemark = ReceiveQARemark;
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QAOnReceiveReturn()
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
                string ReceiveQARemark = Request["ReceiveQARemark"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.ReceiveQARemark = ReceiveQARemark;
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Return(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QCSubmit()
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
                string QCValidateResult = Request["QCValidateResult"];
                string submitRemark = Request["submitRemark"];

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                form.QCValidateResult = QCValidateResult;
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void StorehouseSubmit()
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
                string submitRemark = Request["submitRemark"];

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                FailureProductForm form = WebHelper.FailureProductProcess.Get(taskId);
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.FailureProductProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ReworkSubmit()
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
                string submitRemark = Request["submitRemark"];
                string formJson = Request["formJson"];
                FailureProductForm failureProductForm = WebHelper.FailureProductProcess.Get(taskId);
                ProductReworkForm form = JsonConvert.DeserializeObject<ProductReworkForm>(formJson);

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                form.Approves = failureProductForm.Approves;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.ReworkSend(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void SetFormProperty(FailureProductForm form, FailureStartModel model)
        {
            form.No = model.No;
            form.ProductType = model.ProductType;
            form.BJXLH = model.BJXLH;
            form.JZXLH = model.JZXLH;
            form.ZRBM = model.ZRBM;
            form.UM = model.UM;
            form.MO = model.MO;
            form.GYSDM = model.GYSDM;
            form.GYSMC = model.GYSMC;
            form.ComponentCode = model.ComponentCode;

            form.ComponentName = model.ComponentName;

            form.OrderCode = model.OrderCode;

            form.FailurePlace = model.FailurePlace;

            form.Source = model.Source;

            form.Quantity = model.Quantity;

            form.Reason = model.Reason;

            form.ReasonRemark = model.ReasonRemark;

            form.Remark = model.Remark;

            form.PmcUserAccount = model.PmcUserAccount;

            form.PmcUserName = model.PmcUserName;
            form.QEUserAccount = model.QEUserAccount;
            form.QEUserName = model.QEUserName;
        }
    }
}