using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;
using Newtonsoft.Json;
using Johnson.Process.Website.Models;

namespace Johnson.Process.Website
{
    public partial class OrderPingShenController : System.Web.UI.Page
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
            else if (action.Equals("DeptPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.DeptPingShenSubmit();
            }
            else if (action.Equals("EngFuZeRenPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.EngFuZeRenPingShenSubmit();
            }
            else if (action.Equals("DianQiPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.DianQiPingShenSubmit();
            }
            else if (action.Equals("SheJiEngineerPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.SheJiEngineerPingShenSubmit();
            }
            else if (action.Equals("EngFuZeRen2PingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.EngFuZeRen2PingShenSubmit();
            }
            else if (action.Equals("BOMPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.BOMPingShenSubmit();
            }
            else if (action.Equals("PMCPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.PMCPingShenSubmit();
            }
            else if (action.Equals("ScmPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.ScmPingShenSubmit();
            }
            else if (action.Equals("JiZuWanGongPingShenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiZuWanGongPingShenSubmit();
            }
            else if (action.Equals("SheJiTiJiaoSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.SheJiTiJiaoSubmit();
            }
            else if (action.Equals("JiShuJianChaSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiShuJianChaSubmit();
            }
            else if (action.Equals("JiShuZhuGuanShenPiSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiShuZhuGuanShenPiSubmit();
            }
            else if (action.Equals("BomLuRuSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.BomLuRuSubmit();
            }
            else if (action.Equals("FilePublishSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.FilePublishSubmit();
            }
            else if (action.Equals("XinWuLiaoXinXiWeiHuSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.XinWuLiaoXinXiWeiHuSubmit();
            }
            else if (action.Equals("CidSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.CidSubmit();
            }
            else if (action.Equals("QadSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.QadSubmit();
            }
            else if (action.Equals("JiZuWanGongQueRenSubmit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.JiZuWanGongQueRenSubmit();
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
                OrderPingShenModel model = null;
                OrderPingShenForm form = WebHelper.OrderPingShenProcess.Get(taskId);
                if (form == null)
                {
                    model = new OrderPingShenModel();
                    model.startUserName = WebHelper.CurrentUserInfo.UserRealName;
                }
                else
                {
                    model = new OrderPingShenModel(form);
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
                OrderPingShenStartModel model = JsonConvert.DeserializeObject<OrderPingShenStartModel>(formJson);
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(model.taskId);
                OrderPingShenStartInfo startInfo = new OrderPingShenStartInfo() ;
                startInfo.ApproveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName };
                startInfo.CidPingShenRenAccounts = model.cidPingShenRenAccounts;
                startInfo.CsdPingShenRenAccounts = model.csdPingShenRenAccounts;
                startInfo.EngPingShenRenAccounts = model.engPingShenRenAccounts;
                startInfo.IsStandard = model.isStandard;
                startInfo.PmcEngineerAccount = model.pmcEngineerAccount;
                startInfo.PmcEngineerName = model.pmcEngineerName;
                startInfo.SheJiFuZeRenAccount = model.sheJiFuZeRenAccount;
                startInfo.SheJiFuZeRenName = model.sheJiFuZeRenName;
                startInfo.StartUserAccount = WebHelper.CurrentUserInfo.UserLoginName;
                startInfo.StartUserName = WebHelper.CurrentUserInfo.UserRealName;
                startInfo.TaskId = model.taskId;
                startInfo.BanShiChu = model.banShiChu;
                startInfo.BanShiChuLianXiRen = model.banShiChuLianXiRen;
                startInfo.BeiZhu = model.beiZhu;
                startInfo.ChanPinLeiXing = model.chanPinLeiXing;
                startInfo.JDSNO = model.JDSNO;
                startInfo.JiaoHuoRiQi = model.jiaoHuoRiQi;
                startInfo.JiShuYaoQiu = model.jiShuYaoQiu;
                startInfo.Level = model.level;
                startInfo.QiTaYaoQiuShuoMing = model.qiTaYaoQiuShuoMing;
                startInfo.SapItem = model.sapItem;
                startInfo.SapMaterial = model.sapMaterial;
                if (model.shuLiang.HasValue)
                {
                    startInfo.ShuLiang = model.shuLiang.Value;
                }
                startInfo.SONO = model.SONO;
                startInfo.TuZiQueRen = model.tuZiQueRen;
                startInfo.XiangMingCheng = model.xiangMingCheng;
                startInfo.Files = model.files;
                WebHelper.OrderPingShenProcess.Start(startInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void DeptPingShenSubmit()
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
                string result = Request["result"];
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };
                
                WebHelper.OrderPingShenProcess.DeptPingShenResult(taskId, result, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void EngFuZeRenPingShenSubmit()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                OrderPingEngFuZeRenSubmitModel model = JsonConvert.DeserializeObject<OrderPingEngFuZeRenSubmitModel>(formJson);
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(model.taskId);
                OrderEngFuZeRenPingShenInfo pingShenInfo = new OrderEngFuZeRenPingShenInfo();
                pingShenInfo.ApproveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName };
                pingShenInfo.DianQiEngineerAccount = model.dianQiEngineerAccount;
                pingShenInfo.DianQiEngineerName = model.dianQiEngineerName;
                pingShenInfo.EngEngineerAccount = model.engEngineerAccount;
                pingShenInfo.EngEngineerName = model.engEngineerName;
                pingShenInfo.IsStandard = model.isStandard;
                pingShenInfo.SheJiWanChengRiQi = model.sheJiWanChengRiQi;
                pingShenInfo.TaskId = model.taskId;
                pingShenInfo.WaiGouQingDanRiQi = model.waiGouQingDanRiQi;

                WebHelper.OrderPingShenProcess.EngFuZeRenPingShen(pingShenInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void DianQiPingShenSubmit()
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
                DateTime wanChengRiQi = DateTime.Parse(Request["wanChengRiQi"]);
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.DianKongPingShen(taskId, wanChengRiQi, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void SheJiEngineerPingShenSubmit()
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
                DateTime wanChengRiQi = DateTime.Parse(Request["wanChengRiQi"]);
                DateTime? waiGouWanChengRiQi = null;
                DateTime dateOutput;
                if (DateTime.TryParse(Request["waiGouWanChengRiQi"], out dateOutput))
                {
                    waiGouWanChengRiQi = dateOutput;
                }
                
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.EngEngineerPingShen(taskId, wanChengRiQi, approveInfo, waiGouWanChengRiQi);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void EngFuZeRen2PingShenSubmit()
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
                DateTime sheJiWanChengRiQi = DateTime.Parse(Request["sheJiWanChengRiQi"]);
                DateTime? waiGouWanChengRiQi = null;
                DateTime dateOutput;
                if (DateTime.TryParse(Request["waiGouWanChengRiQi"], out dateOutput))
                {
                    waiGouWanChengRiQi = dateOutput;
                }
                
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.EngPingShenConfirm(taskId, sheJiWanChengRiQi, approveInfo, waiGouWanChengRiQi);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void BOMPingShenSubmit()
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
                DateTime zhengJiWanChengRiQi = DateTime.Parse(Request["zhengJiWanChengRiQi"]);
                DateTime? shouCiWanChengRiQi = null;
                DateTime dateOutput;
                if (DateTime.TryParse(Request["shouCiWanChengRiQi"], out dateOutput))
                {
                    shouCiWanChengRiQi = dateOutput;
                }

                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.BomPingShen(taskId, approveInfo, zhengJiWanChengRiQi, shouCiWanChengRiQi);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void PMCPingShenSubmit()
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
                string scmEngineerAccount = Request["scmEngineerAccount"];

                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.PmcPingShen(taskId, approveInfo, scmEngineerAccount);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void ScmPingShenSubmit()
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
                DateTime? wuLiaoDaoHuoRiQi = null;
                DateTime dateOutput;
                if (DateTime.TryParse(Request["wuLiaoDaoHuoRiQi"], out dateOutput))
                {
                    wuLiaoDaoHuoRiQi = dateOutput;
                }

                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.ScmPingShen(taskId, approveInfo, wuLiaoDaoHuoRiQi);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void JiZuWanGongPingShenSubmit()
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
                DateTime jiZuWanGongRiQi = DateTime.Parse(Request["jiZuWanGongRiQi"]);

                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.JiZuWanGongPingShen(taskId, approveInfo, jiZuWanGongRiQi);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void SheJiTiJiaoSubmit()
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

                WebHelper.OrderPingShenProcess.SheJiTiJiao(model.taskId, approveInfo, model.hasXinWuLiao, model.jianChaEngineerAccount,
                    model.jianChaEngineerName, model.zhuGuanAccount, model.zhuGuanName, model.sheJiShuoMing, model.ziLiaoWanZhengDu, model.sheJiZiLiao);
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

                WebHelper.OrderPingShenProcess.JiShuChaJian(taskId, approveInfo);
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

                WebHelper.OrderPingShenProcess.JiShuZhuGuanShenPi(taskId, approveInfo);
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

                WebHelper.OrderPingShenProcess.BomLuRu(taskId, approveInfo);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void FilePublishSubmit()
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

                WebHelper.OrderPingShenProcess.WenJianFaFang(taskId, approveInfo);
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

                WebHelper.OrderPingShenProcess.XinWuLiaoXinXiWeiHu(taskId, approveInfo);
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
                CidSubmitModel model = JsonConvert.DeserializeObject<CidSubmitModel>(formJson);
                if (string.IsNullOrEmpty(model.taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(model.taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.CidQueRen(model.taskId, approveInfo, model.cidZiLiao);
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
                QadSubmitModel model = JsonConvert.DeserializeObject<QadSubmitModel>(formJson);
                if (string.IsNullOrEmpty(model.taskId))
                {
                    throw new ArgumentNullException("taskId");
                }
                TaskInfo taskInfo = WebHelper.OrderPingShenProcess.GetTaskInfo(model.taskId);
                TaskApproveInfo approveInfo = new TaskApproveInfo { ApproveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ApproveUserName = WebHelper.CurrentUserInfo.UserRealName, Remark = model.submitRemark, StepName = taskInfo.StepName };

                WebHelper.OrderPingShenProcess.QadQueRen(model.taskId, approveInfo, model.qadZiLiao);
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }

        private void JiZuWanGongQueRenSubmit()
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

                WebHelper.OrderPingShenProcess.JiZuWanChengQueRen(taskId, approveInfo);
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