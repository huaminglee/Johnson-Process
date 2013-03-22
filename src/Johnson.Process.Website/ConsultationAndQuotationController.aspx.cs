using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using Newtonsoft.Json;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website
{
    public partial class ConsultationAndQuotationController : System.Web.UI.Page
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
            else if (action.Equals("MarketingGet", StringComparison.InvariantCultureIgnoreCase))
            {
                this.MarketingGet();
            }
            else if (action.Equals("start", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Start();
            }
            else if (action.Equals("StartReturnedSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.StartReturnedSubmit();
            }
            else if (action.Equals("MarketingSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.MarketingSubmit();
            }
            else if (action.Equals("MarketingReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.MarketingReturn();
            }
            else if (action.Equals("CsdSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CsdSubmit();
            }
            else if (action.Equals("CsdReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CsdReturn();
            }
            else if (action.Equals("tracerSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.TracerSubmit();
            }
            else if (action.Equals("engSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.EngSubmit();
            }
            else if (action.Equals("cidSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CidSubmit();
            }
            else if (action.Equals("QadSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QadSubmit();
            }
            else if (action.Equals("ScmSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ScmSubmit();
            }
            else if (action.Equals("LogSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.LogSubmit();
            }
            else if (action.Equals("tracer2Submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Tracer2Submit();
            }
            else if (action.Equals("Marketing2Submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Marketing2Submit();
            }
            else if (action.Equals("Marketing2Return", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Marketing2Return();
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
                string formJson = Request["formJson"];
                ConsultationAndQuotationStartModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationStartModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(taskId);
                ConsultationAndQuotationForm newForm = model.Map();
                newForm.Approves = new List<TaskApproveInfo>();
                newForm.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.Start(currentUserName, model.marketingEngineer, taskId, newForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void StartReturnedSubmit()
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
                ConsultationAndQuotationStartModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationStartModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(taskId);
                ConsultationAndQuotationForm oldForm = WebHelper.ConsultationAndQuotationProcess.Get(taskId);
                ConsultationAndQuotationForm newForm = model.Map();
                newForm.Approves = oldForm.Approves;
                newForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.StartReturnedSend(model.marketingEngineer, taskId, newForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void MarketingSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationMarketingModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationMarketingModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CsdEngineerAccount = model.csdEngineerAccount;
                form.CsdEngineerName = model.csdEngineerName;
                form.MarketingReply = model.marketingReply;
                form.MarketingEmailTo = model.toCsdEmailAddress;
                List<ProcessFile> files = new List<ProcessFile>();
                foreach (UploadFileModel fileModel in model.files)
                {
                    files.Add(fileModel.Map());
                }
                form.Files = files;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                if (model.needCsdReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.MarketingSend(model.csdEngineerAccount, model.taskId, form);
                }
                else
                {
                    form.LeadTime = model.leadTime;
                    form.LeadTimeRemark = model.leadTimeRemark;
                    List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                    if (model.products != null)
                    {
                        foreach (ConsultationAndQuotationProductModel productModel in model.products)
                        {
                            productList.Add(productModel.Map());
                        }
                    }
                    form.Products = productList;
                    WebHelper.ConsultationAndQuotationProcess.MarketingSend(model.taskId, form);
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

        private void MarketingReturn()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationMarketingModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationMarketingModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CsdEngineerAccount = model.csdEngineerAccount;
                form.CsdEngineerName = model.csdEngineerName;
                form.MarketingReply = model.marketingReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                if (model.needCsdReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.Return(model.taskId, form);
                }
                else
                {
                    form.LeadTime = model.leadTime;
                    form.LeadTimeRemark = model.leadTimeRemark;
                    List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                    if (model.products != null)
                    {
                        foreach (ConsultationAndQuotationProductModel productModel in model.products)
                        {
                            productList.Add(productModel.Map());
                        }
                    }
                    form.Products = productList;
                    WebHelper.ConsultationAndQuotationProcess.Return(model.taskId, form);
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

        private void CsdSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationCsdModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationCsdModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CsdTracerAccount = model.csdTracerAccount;
                form.CsdTracerName = model.csdTracerName;
                form.CsdReply = model.csdReply;
                form.CsdEmailTo = model.csdEmailTo;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                if (model.needTrace)
                {
                    WebHelper.ConsultationAndQuotationProcess.CsdEngineerSend(model.csdTracerAccount, model.taskId, form);
                }
                else
                {
                    form.LeadTime = model.leadTime;
                    form.LeadTimeRemark = model.leadTimeRemark;
                    List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                    if (model.products != null)
                    {
                        foreach (ConsultationAndQuotationProductModel productModel in model.products)
                        {
                            productList.Add(productModel.Map());
                        }
                    }
                    form.Products = productList;
                    WebHelper.ConsultationAndQuotationProcess.CsdEngineerSend(model.taskId, form);
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

        private void CsdReturn()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationCsdModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationCsdModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CsdTracerAccount = model.csdTracerAccount;
                form.CsdTracerName = model.csdTracerName;
                form.CsdReply = model.csdReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                if (model.needTrace)
                {
                    WebHelper.ConsultationAndQuotationProcess.Return(model.taskId, form);
                }
                else
                {
                    form.LeadTime = model.leadTime;
                    form.LeadTimeRemark = model.leadTimeRemark;
                    List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                    if (model.products != null)
                    {
                        foreach (ConsultationAndQuotationProductModel productModel in model.products)
                        {
                            productList.Add(productModel.Map());
                        }
                    }
                    form.Products = productList;
                    WebHelper.ConsultationAndQuotationProcess.Return(model.taskId, form);
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

        private void TracerSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationTracerModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationTracerModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CsdReply = model.csdReply;
                form.CsdTracerEmailTo = model.csdTracerEmailTo;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                if (model.needEngReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.CsdTracerSendToEng(model.engEngineer, model.taskId, form);
                }
                else if (model.needLogReply && model.needScmReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.CsdTracerSendToLogAndScm(model.logEngineer, model.scmEngineer, model.taskId, form);
                }
                else if (model.needLogReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.CsdTracerSendToLog(model.logEngineer, model.taskId, form);
                }
                else if (model.needScmReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.CsdTracerSendToScm(model.scmEngineer, model.taskId, form);
                }
                else
                {
                    form.LeadTime = model.leadTime;
                    form.LeadTimeRemark = model.leadTimeRemark;
                    List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                    if (model.products != null)
                    {
                        foreach (ConsultationAndQuotationProductModel productModel in model.products)
                        {
                            productList.Add(productModel.Map());
                        }
                    }
                    form.Products = productList;
                    WebHelper.ConsultationAndQuotationProcess.CsdTracerSend(model.taskId, form);
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

        private void EngSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationEngModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationEngModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.EngReply = model.engReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                if (model.needCidReply && model.needQadReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.EngEngineerSendToQadAndCid(model.cidEngineer, model.qadEngineer, model.taskId, form);
                }
                else if (model.needCidReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.EngEngineerSendToCid(model.cidEngineer, model.taskId, form);
                }
                else if (model.needQadReply)
                {
                    WebHelper.ConsultationAndQuotationProcess.EngEngineerSendToQad(model.qadEngineer, model.taskId, form);
                }
                else
                {
                    WebHelper.ConsultationAndQuotationProcess.EngEngineerSend(model.taskId, form);
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

        private void CidSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationCidModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationCidModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CidReply = model.cidReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.CidSend(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QadSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationQadModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationQadModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.QadReply = model.qadReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.QadSend(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ScmSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationScmModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationScmModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.ScmReply = model.scmReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.ScmSend(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void LogSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationLogModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationLogModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.LogReply = model.logReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.LogSend(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Tracer2Submit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationTracerModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationTracerModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.CsdReply = model.csdReply;
                form.CsdTracerEmailTo = model.csdTracerEmailTo;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                form.LeadTime = model.leadTime;
                form.LeadTimeRemark = model.leadTimeRemark;
                List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                if (model.products != null)
                {
                    foreach (ConsultationAndQuotationProductModel productModel in model.products)
                    {
                        productList.Add(productModel.Map());
                    }
                }
                form.Products = productList;
                WebHelper.ConsultationAndQuotationProcess.CsdTracer2Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Marketing2Submit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationMarketingModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationMarketingModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.MarketingReply = model.marketingReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                form.LeadTime = model.leadTime;
                form.LeadTimeRemark = model.leadTimeRemark;
                List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                if (model.products != null)
                {
                    foreach (ConsultationAndQuotationProductModel productModel in model.products)
                    {
                        productList.Add(productModel.Map());
                    }
                }
                form.Products = productList;
                WebHelper.ConsultationAndQuotationProcess.Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void Marketing2Return()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ConsultationAndQuotationMarketingModel model = JsonConvert.DeserializeObject<ConsultationAndQuotationMarketingModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(model.taskId);
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(model.taskId);
                form.MarketingReply = model.marketingReply;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                form.LeadTime = model.leadTime;
                form.LeadTimeRemark = model.leadTimeRemark;
                List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
                if (model.products != null)
                {
                    foreach (ConsultationAndQuotationProductModel productModel in model.products)
                    {
                        productList.Add(productModel.Map());
                    }
                }
                form.Products = productList;
                WebHelper.ConsultationAndQuotationProcess.Return(model.taskId, form);
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
                string submitRemark = Request["submitRemark"];

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(taskId);

                ConsultationAndQuotationForm oldForm = WebHelper.ConsultationAndQuotationProcess.Get(taskId);
                oldForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.Send(taskId, oldForm);
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
                string submitRemark = Request["submitRemark"];

                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                TaskInfo taskInfo = WebHelper.ConsultationAndQuotationProcess.GetTaskInfo(taskId);

                ConsultationAndQuotationForm oldForm = WebHelper.ConsultationAndQuotationProcess.Get(taskId);
                oldForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.ConsultationAndQuotationProcess.Send(taskId, oldForm);
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
                ConsultationAndQuotationForm form = null;
                if (Request["incidentNo"] != null)
                {
                    form = WebHelper.ConsultationAndQuotationProcess.Get(int.Parse(Request["incidentNo"]));
                }
                else
                {
                    string taskId = Request["taskid"];
                    if (string.IsNullOrEmpty(taskId))
                    {
                        throw new ArgumentNullException("taskId");
                    }
                    taskId = taskId.Trim();
                    form = WebHelper.ConsultationAndQuotationProcess.Get(taskId);
                }
                ConsultationAndQuotationModel model = null;
                if (form != null)
                {
                    model = new ConsultationAndQuotationModel(form);
                }
                if (model == null)
                {
                    model = new ConsultationAndQuotationModel();
                    model.applyTime = DateTime.Now.ToString("yyyy-MM-dd");
                    model.applyUserName = WebHelper.CurrentUserInfo.UserRealName;
                    model.products = new List<ConsultationAndQuotationProductModel>();
                }
                Response.Write(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void MarketingGet()
        {
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                ConsultationAndQuotationForm form = WebHelper.ConsultationAndQuotationProcess.Get(taskId);
                ConsultationAndQuotationModel model = null;
                if (form != null)
                {
                    string startSetpName = WebHelper.ConsultationAndQuotationProcess.GetStartSetpName(taskId);
                    form.Approves = form.Approves.FindAll(x => x.StepName != null && x.StepName.Equals(startSetpName, StringComparison.InvariantCultureIgnoreCase));
                    model = new ConsultationAndQuotationModel(form);
                }
                if (model == null)
                {
                    model = new ConsultationAndQuotationModel();
                    model.applyTime = DateTime.Now.ToString("yyyy-MM-dd");
                    model.applyUserName = WebHelper.CurrentUserInfo.UserRealName;
                    model.products = new List<ConsultationAndQuotationProductModel>();
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