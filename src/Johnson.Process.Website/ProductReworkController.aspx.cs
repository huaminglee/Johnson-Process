﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;
using Newtonsoft.Json;
using Johnson.Process.Website.Models;

namespace Johnson.Process.Website
{
    public partial class ProductReworkController : ProcessPage
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
            else if (action.Equals("QCSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QCSubmit();
            }
            else if (action.Equals("EngSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.EngSubmit();
            }
            else if (action.Equals("CidSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CidSubmit();
            }
            else if (action.Equals("QESubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QESubmit();
            }
            else if (action.Equals("PMCSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.PMCSubmit();
            }
            else if (action.Equals("QC2Submit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QC2Submit();
            }
            else if (action.Equals("FinSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.FinSubmit();
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
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                if (form == null)
                {
                    form = new ProductReworkForm();
                    form.CompletedTime = DateTime.Now;
                }
                Response.Write(JsonConvert.SerializeObject(new ProductReworkModel(form)));
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
                string submitRemark = Request["submitRemark"];
                ProductReworkForm form = JsonConvert.DeserializeObject<ProductReworkForm>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                form.Approves = new List<TaskApproveInfo>();
                form.Approves.Add(new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Start(currentUserName, taskId, form);
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
                string formJson = Request["formJson"];
                ProductReworkQcModel model = JsonConvert.DeserializeObject<ProductReworkQcModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.CidUserAccount = model.CidUserAccount;
                form.CidUserName = model.CidUserName;
                form.EngUserAccount = model.EngUserAccount;
                form.EngUserName = model.EngUserName;
                form.FinUserAccount = model.FinUserAccount;
                form.FinUserName = model.FinUserName;
                form.PmcUserAccount = model.PmcUserAccount;
                form.PmcUserName = model.PmcUserName;
                form.QEUserAccount = model.QEUserAccount;
                form.QEUserName = model.QEUserName;
                form.PZZTMS = model.PZZTMS;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.QCSend(taskId, form);
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
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string formJson = Request["formJson"];
                ProductReworkEngModel model = JsonConvert.DeserializeObject<ProductReworkEngModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.EngFiles = model.EngFiles;
                form.Materials = model.Materials;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Send(taskId, form);
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
                string taskId = Request["taskid"];
                if (string.IsNullOrEmpty(taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                taskId = taskId.Trim();
                string formJson = Request["formJson"];
                ProductReworkCidModel model = JsonConvert.DeserializeObject<ProductReworkCidModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.CidFiles = model.CidFiles;
                form.GYFA = model.GYFA;
                form.GSJS = model.GSJS;
                form.HYBZ = model.HYBZ;
                form.ZPX = model.ZPX;
                form.BanJin = model.BanJin;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Send(taskId, form);
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
                string submitRemark = Request["submitRemark"];
                string QADFAQR = Request["QADFAQR"];
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.QADFAQR = QADFAQR;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void PMCSubmit()
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
                string submitRemark = Request["submitRemark"];
                string WLJHAP = Request["WLJHAP"];
                string SCJHAP = Request["SCJHAP"];
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.WLJHAP = WLJHAP;
                form.SCJHAP = SCJHAP;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void QC2Submit()
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

                ProductReworkQC2Model model = JsonConvert.DeserializeObject<ProductReworkQC2Model>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.FGJG = model.FGJG;
                form.FGJGBZ = model.FGJGBZ;
                form.XGCLDH = model.XGCLDH;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Send(taskId, form);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void FinSubmit()
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

                ProductReworkFinModel model = JsonConvert.DeserializeObject<ProductReworkFinModel>(formJson);
                string currentUserName = WebHelper.CurrentUserInfo.UserRealName;
                TaskInfo taskInfo = WebHelper.ProductReworkProcess.GetTaskInfo(taskId);
                ProductReworkForm form = WebHelper.ProductReworkProcess.Get(taskId);
                form.GSFY = model.GSFY;
                form.WLFY = model.WLFY;
                form.ZFY = model.ZFY;
                form.Approves.Insert(0, new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = currentUserName, Remark = model.submitRemark, StepName = taskInfo.StepName });

                WebHelper.ProductReworkProcess.Send(taskId, form);
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