using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using Newtonsoft.Json;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class VocController : System.Web.UI.Page
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
            else if (action.Equals("startResubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.StartResubmit();
            }
            else if (action.Equals("SolutionsSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.SolutionsSubmit();
            }
            else if (action.Equals("ActinPlanSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ActinPlanSubmit();
            }
            else if (action.Equals("ActinPlanReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ActinPlanReturn();
            }
            else if (action.Equals("CompletedSolutionsSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CompletedSolutionsSubmit();
            }
            else if (action.Equals("CompletedSolutionsReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CompletedSolutionsReturn();
            }
            else if (action.Equals("ActionCompletedGet", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ActionCompletedGet();
            }
            else if (action.Equals("ActionCompletedSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ActionCompletedSubmit();
            }
            else if (action.Equals("ResponsibleSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ResponsibleSubmit();
            }
            else if (action.Equals("ResponsibleReasonSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ResponsibleReasonSubmit();
            }
            else if (action.Equals("ResponsibleMeasuresSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ResponsibleMeasuresSubmit();
            }
            else if (action.Equals("ResponsiblePreviousSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ResponsiblePreviousSubmit();
            }
            else if (action.Equals("ResponsiblePreviousReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ResponsiblePreviousReturn();
            }
            else if (action.Equals("ASDSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ASDSubmit();
            }
            else if (action.Equals("ASDReturn", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ASDReturn();
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
                VocMultiStart multiStartModel = JsonConvert.DeserializeObject<VocMultiStart>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;
                string vocCode = WebHelper.VocProcess.GetAndSetVocCode();
                foreach (VocStartModel model in multiStartModel.complaints)
                {
                    model.applyTime = multiStartModel.applyTime;
                    model.applyUserDepartmentName = multiStartModel.applyUserDepartmentName;
                    model.applyUserName = multiStartModel.applyUserName;
                    model.projectName = multiStartModel.projectName;
                    model.submitRemark = multiStartModel.submitRemark;
                    TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                    VocForm newForm = model.Map();
                    newForm.VocCode = vocCode;
                    newForm.Approves = new List<TaskApproveInfo>();
                    newForm.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                    WebHelper.VocProcess.Start(currentUserName, taskId, newForm);
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
                VocStartModel model = JsonConvert.DeserializeObject<VocStartModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                VocForm oldForm = WebHelper.VocProcess.Get(taskId);
                VocForm newForm = model.Map();
                newForm.Actions = oldForm.Actions;
                newForm.Approves = oldForm.Approves;
                newForm.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.StartResend(taskId, newForm);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void SolutionsSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocActinPlanModel model = JsonConvert.DeserializeObject<VocActinPlanModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                
                form.Solutions = model.solutions;
                List<ProcessFile> solutionsFiles = new List<ProcessFile>();
                if (model.solutionsFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.solutionsFiles)
                    {
                        solutionsFiles.Add(fileModel.Map());
                    }
                }
                form.SolutionsFiles = solutionsFiles;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ActinPlanSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocActinPlanModel model = JsonConvert.DeserializeObject<VocActinPlanModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Actions = new List<VocAction>();
                foreach (VocActionModel actMdl in model.actions)
                {
                    form.Actions.Add(actMdl.Map());
                }
                form.Solutions = model.solutions;
                List<ProcessFile> solutionsFiles = new List<ProcessFile>();
                if (model.solutionsFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.solutionsFiles)
                    {
                        solutionsFiles.Add(fileModel.Map());
                    }
                }
                form.SolutionsFiles = solutionsFiles;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.ResponsiblePlanAction(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ActinPlanReturn()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocActinPlanModel model = JsonConvert.DeserializeObject<VocActinPlanModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Actions = new List<VocAction>();
                foreach (VocActionModel actMdl in model.actions)
                {
                    form.Actions.Add(actMdl.Map());
                }
                form.Solutions = model.solutions;
                List<ProcessFile> solutionsFiles = new List<ProcessFile>();
                if (model.solutionsFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.solutionsFiles)
                    {
                        solutionsFiles.Add(fileModel.Map());
                    }
                }
                form.SolutionsFiles = solutionsFiles;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Return(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void CompletedSolutionsSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocCompletedSolutionsModel model = JsonConvert.DeserializeObject<VocCompletedSolutionsModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.SolutionsCompleteTime = model.solutionsCompleteTime;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void CompletedSolutionsReturn()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocCompletedSolutionsModel model = JsonConvert.DeserializeObject<VocCompletedSolutionsModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Return(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ActionCompletedSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocActinCompletedModel model = JsonConvert.DeserializeObject<VocActinCompletedModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Actions.RemoveAll(x => x.UserAccount.Equals(WebHelper.CurrentUserInfo.UserLoginName, StringComparison.InvariantCultureIgnoreCase));
                foreach (VocActionModel actMdl in model.actions)
                {
                    form.Actions.Add(actMdl.Map());
                }

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ResponsibleReasonSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocResponsibleReasonModel model = JsonConvert.DeserializeObject<VocResponsibleReasonModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.MeasureUserName = model.measureUserName;
                form.MeasureUserAccount = model.measureUserAccount;
                form.Reason = model.reason;

                List<ProcessFile> reasonFiles = new List<ProcessFile>();
                if (model.reasonFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.reasonFiles)
                    {
                        reasonFiles.Add(fileModel.Map());
                    }
                }
                form.ReasonFiles = reasonFiles;

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.ResponsibleReason(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ResponsibleMeasuresSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocMeasuresModel model = JsonConvert.DeserializeObject<VocMeasuresModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Measures = model.measures;
                List<ProcessFile> measuresFiles = new List<ProcessFile>();
                if (model.measuresFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.measuresFiles)
                    {
                        measuresFiles.Add(fileModel.Map());
                    }
                }
                form.MeasuresFiles = measuresFiles;

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ResponsibleSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocResponsibleModel model = JsonConvert.DeserializeObject<VocResponsibleModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.ResponsibleUserPreviousAccount = model.responsibleUserPreviousAccount;
                form.ResponsibleUserPreviousName = model.responsibleUserPreviousName;
                form.Reason = model.reason;
                form.Measures = model.measures;

                List<ProcessFile> reasonFiles = new List<ProcessFile>();
                if (model.reasonFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.reasonFiles)
                    {
                        reasonFiles.Add(fileModel.Map());
                    }
                }
                form.ReasonFiles = reasonFiles;
                List<ProcessFile> measuresFiles = new List<ProcessFile>();
                if (model.measuresFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.measuresFiles)
                    {
                        measuresFiles.Add(fileModel.Map());
                    }
                }
                form.MeasuresFiles = measuresFiles;

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.ResponsibleHandle(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ResponsiblePreviousSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocResponsibleModel model = JsonConvert.DeserializeObject<VocResponsibleModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Reason = model.reason;
                form.Measures = model.measures;

                List<ProcessFile> reasonFiles = new List<ProcessFile>();
                if (model.reasonFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.reasonFiles)
                    {
                        reasonFiles.Add(fileModel.Map());
                    }
                }
                form.ReasonFiles = reasonFiles;
                List<ProcessFile> measuresFiles = new List<ProcessFile>();
                if (model.measuresFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.measuresFiles)
                    {
                        measuresFiles.Add(fileModel.Map());
                    }
                }
                form.MeasuresFiles = measuresFiles;

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Send(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ResponsiblePreviousReturn()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocResponsibleModel model = JsonConvert.DeserializeObject<VocResponsibleModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(model.taskId);
                VocForm form = WebHelper.VocProcess.Get(model.taskId);
                form.Reason = model.reason;
                form.Measures = model.measures;

                List<ProcessFile> reasonFiles = new List<ProcessFile>();
                if (model.reasonFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.reasonFiles)
                    {
                        reasonFiles.Add(fileModel.Map());
                    }
                }
                form.ReasonFiles = reasonFiles;
                List<ProcessFile> measuresFiles = new List<ProcessFile>();
                if (model.measuresFiles != null)
                {
                    foreach (UploadFileModel fileModel in model.measuresFiles)
                    {
                        measuresFiles.Add(fileModel.Map());
                    }
                }
                form.MeasuresFiles = measuresFiles;

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Return(model.taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ASDSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);
                VocForm form = WebHelper.VocProcess.Get(taskId);

                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ASDReturn()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string taskId = Request["taskId"];
                string submitRemark = Request["submitRemark"];
                string currentUserName = WebHelper.CurrentUserInfo.UserLoginName;

                TaskInfo taskInfo = WebHelper.VocProcess.GetTaskInfo(taskId);

                VocForm form = WebHelper.VocProcess.Get(taskId);
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.VocProcess.Return(taskId, form);
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
                VocForm form = WebHelper.VocProcess.Get(taskId);
                VocModel model = null;
                if (form != null)
                {
                    model = new VocModel(form);
                }
                if (model == null)
                {
                    model = new VocModel();
                    model.applyTime = DateTime.Now.ToString("yyyy-MM-dd");
                    model.applyUserName = WebHelper.CurrentUserInfo.UserRealName;
                    model.vocCode = WebHelper.VocProcess.GetVocCode();
                }
                Response.Write(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void ActionCompletedGet()
        {
            try
            {
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                VocForm form = WebHelper.VocProcess.Get(taskId);
                VocModel model = new VocModel(form);
                model.actions.RemoveAll(x => !x.userAccount.Equals(WebHelper.CurrentUserInfo.UserLoginName, StringComparison.InvariantCultureIgnoreCase));
                Response.Write(JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }
    }
}