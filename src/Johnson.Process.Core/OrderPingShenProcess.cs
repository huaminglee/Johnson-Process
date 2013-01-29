using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class OrderPingShenProcess : UltimusFormProcess<OrderPingShenForm>
    {

        private const string PARAM_NEED_DEPT_PINGSHEN = "是否需要组织审批";
        private const string PARAM_DIANQI_ENGINEER = "电气工程师";
        private const string PARAM_ENG_ENGINEER = "设计工程师";
        private const string PARAM_ENG_JIANCHA_ENGINEER = "技术检查工程师";
        private const string PARAM_HAS_NEW_WULIAO = "是否有新物料";
        private const string PARAM_NEED_CID_PINGSHEN = "是否需要CID评审";
        private const string PARAM_NEED_QAD_PINGSHEN = "是否需要QAD评审";
        private const string PARAM_NEED_WULIAO_JIHUA_PINGSHEN = "是否需要物料计划评审";
        private const string PARAM_IS_STANDARD_ORDER = "是标准订单";
        private const string PARAM_CID_QUEREN_REN = "CID确认人";
        private const string PARAM_QAD_QUEREN_REN = "QAD确认人";
        private const string PARAM_NEED_ENG_ENGINEER_PINGSHEN = "是否需要设计工程师评审";
        private const string PARAM_SHEJI_FUZEREN = "设计负责人";
        private const string PARAM_BOM_WENYUAN = "Bom文员";
        private const string PARAM_WENJIAN_FAFAN_WENYUAN = "文件发放文员";
        private const string PARAM_PMC_ENGINEER = "PMC工程师";
        private const string PARAM_SCM_ENGINEER = "SCM工程师";
        private const string PARAM_SHEJI_ENGINEER_ZHUGUAN = "设计工程师主管";
        private const string PARAM_SYSTEM_USER = "流程系统用户";

        public OrderPingShenProcess(string processName)
            :base(processName)
        {
            
        }

        private string GetSummary(OrderPingShenForm form)
        {
            return "";
        }

        public TaskSendResult Start(OrderPingShenStartInfo startInfo)
        {
            if (string.IsNullOrEmpty(startInfo.StartUserAccount))
            {
                throw new ArgumentNullException("startInfo.StartUserAccount");
            }
            if (string.IsNullOrEmpty(startInfo.TaskId))
            {
                throw new ArgumentNullException("startInfo.TaskId");
            }
            if (startInfo.ApproveInfo == null)
            {
                throw new ArgumentNullException("startInfo.ApproveInfo");
            }
            if (string.IsNullOrEmpty(startInfo.PmcEngineerAccount))
            {
                throw new ArgumentNullException("startInfo.PmcEngineer");
            }
            if (!startInfo.IsStandard)
            {
                if (string.IsNullOrEmpty(startInfo.SheJiFuZeRenAccount))
                {
                    throw new ArgumentNullException("startInfo.SheJiFuZeRen");
                }
            }

            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_NEED_DEPT_PINGSHEN, objVariableValue = new object[]{string.IsNullOrEmpty(startInfo.PingShenRenAccounts) ? "否" : "是"} },
                new Variable{ strVariableName = PARAM_NEED_CID_PINGSHEN, objVariableValue = new object[]{string.IsNullOrEmpty(startInfo.CidPingShenRenAccounts) ? "否" : "是"} },
                new Variable{ strVariableName = PARAM_NEED_QAD_PINGSHEN, objVariableValue = new object[]{string.IsNullOrEmpty(startInfo.QadPingShenRenAccounts) ? "否" : "是"} },
                new Variable{ strVariableName = PARAM_CID_QUEREN_REN, objVariableValue = this.GetUltimusUserAccounts(startInfo.CidPingShenRenAccounts) },
                new Variable{ strVariableName = PARAM_QAD_QUEREN_REN, objVariableValue = this.GetUltimusUserAccounts(startInfo.QadPingShenRenAccounts) },
                new Variable{ strVariableName = PARAM_IS_STANDARD_ORDER, objVariableValue = new object[]{startInfo.IsStandard ? "是" : "否"} },
                new Variable{ strVariableName = PARAM_PMC_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(startInfo.PmcEngineerAccount) }},
                new Variable{ strVariableName = PARAM_SHEJI_FUZEREN, objVariableValue = new object[]{this.GetUltimusUserAccount(startInfo.SheJiFuZeRenAccount) }},
                new Variable{ strVariableName = PARAM_SYSTEM_USER, objVariableValue = new object[]{this.GetUltimusUserAccount(SYSTEM_ACCOUNT) }}
            };
            OrderPingShenForm form = new OrderPingShenForm();
            form.StartTime = DateTime.Now;
            form.StartUserAccount = startInfo.StartUserAccount;
            form.BanShiChu = startInfo.BanShiChu;
            form.BanShiChuLianXiRen = startInfo.BanShiChuLianXiRen;
            form.BeiZhu = startInfo.BeiZhu;
            form.ChanPinLeiXing = startInfo.ChanPinLeiXing;
            form.IsStandard = startInfo.IsStandard;
            form.JDSNO = startInfo.JDSNO;
            form.JiaoHuoRiQi = startInfo.JiaoHuoRiQi;
            form.JiShuYaoQiu = startInfo.JiShuYaoQiu;
            form.Level = startInfo.Level;
            form.PmcEngineerAccount = startInfo.PmcEngineerAccount;
            form.PmcEngineerName = startInfo.PmcEngineerName;
            form.QiTaYaoQiuShuoMing = startInfo.QiTaYaoQiuShuoMing;
            form.SapItem = startInfo.SapItem;
            form.SapMaterial = startInfo.SapMaterial;
            form.SheJiFuZeRenAccount = startInfo.SheJiFuZeRenAccount;
            form.SheJiFuZeRenName = startInfo.SheJiFuZeRenName;
            form.ShuLiang = startInfo.ShuLiang;
            form.SONO = startInfo.SONO;
            form.StartUserAccount = startInfo.StartUserAccount;
            form.StartUserName = startInfo.StartUserName;
            form.TuZiQueRen = startInfo.TuZiQueRen;
            form.XiangMingCheng = startInfo.XiangMingCheng;
            form.Files = startInfo.Files;
            form.Items = startInfo.Items;
            form.Approves = new List<TaskApproveInfo>();
            form.Approves.Add(startInfo.ApproveInfo);

            return this.Start(startInfo.StartUserAccount, startInfo.TaskId, variable, "", this.GetSummary(form), form);
        }

        public TaskSendResult DeptPingShenResult(string taskId, string result, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }
            OrderPingShenForm form = this.Get(taskId);
            form.DeptPingShenResult = result;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult EngFuZeRenPingShen(OrderEngFuZeRenPingShenInfo info)
        {
            if (string.IsNullOrEmpty(info.TaskId))
            {
                throw new ArgumentNullException("info.TaskId");
            }
            if (info.ApproveInfo == null)
            {
                throw new ArgumentNullException("info.ApproveInfo");
            }
            bool needEngineerPingShen = !string.IsNullOrEmpty(info.DianQiEngineerAccount);
            if (!info.IsStandard)
            {
                if (string.IsNullOrEmpty(info.EngEngineerAccount))
                {
                    throw new ArgumentNullException("info.EngEngineerAccount");
                }
            }
            List<Variable> variables = new List<Variable>();
            variables.Add(new Variable { strVariableName = PARAM_IS_STANDARD_ORDER, objVariableValue = new object[] { info.IsStandard ? "是" : "否" } });
            variables.Add(new Variable{ strVariableName = PARAM_ENG_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(info.EngEngineerAccount)} });
            variables.Add(new Variable { strVariableName = PARAM_DIANQI_ENGINEER, objVariableValue = new object[] { this.GetUltimusUserAccount(info.DianQiEngineerAccount) } });
            variables.Add(new Variable { strVariableName = PARAM_NEED_ENG_ENGINEER_PINGSHEN, objVariableValue = new object[] { needEngineerPingShen ? "是" : "否" } });

            if (!string.IsNullOrEmpty(info.PingShenRenAccounts))
            {
                variables.Add(new Variable { strVariableName = PARAM_NEED_DEPT_PINGSHEN, objVariableValue = new object[] { "是" } });
            }
            else
            {
                variables.Add(new Variable { strVariableName = PARAM_NEED_DEPT_PINGSHEN, objVariableValue = new object[] { "否" } });
            }
            if (!string.IsNullOrEmpty(info.CidPingShenRenAccounts))
            {
                variables.Add(new Variable { strVariableName = PARAM_NEED_CID_PINGSHEN, objVariableValue = new object[] { "是" } });
                variables.Add(new Variable { strVariableName = PARAM_CID_QUEREN_REN, objVariableValue = this.GetUltimusUserAccounts(info.CidPingShenRenAccounts) });
            }
            if (!string.IsNullOrEmpty(info.QadPingShenRenAccounts))
            {
                variables.Add(new Variable { strVariableName = PARAM_NEED_QAD_PINGSHEN, objVariableValue = new object[] { "是" } });
                variables.Add(new Variable { strVariableName = PARAM_QAD_QUEREN_REN, objVariableValue = this.GetUltimusUserAccounts(info.QadPingShenRenAccounts) });
            }
            Variable[] variable = variables.ToArray();

            OrderPingShenForm form = this.Get(info.TaskId);
            form.EngEngineerAccount = info.EngEngineerAccount;
            form.EngEngineerName = info.EngEngineerName;
            form.DianQiEngineerAccount = info.DianQiEngineerAccount;
            form.DianQiEngineerName = info.DianQiEngineerName;
            form.WaiGouWanChengRiQi = info.WaiGouQingDanRiQi;
            form.SheJiWanChengRiQi = info.SheJiWanChengRiQi;
            form.IsStandard = info.IsStandard;

            form.Approves.Insert(0, info.ApproveInfo);

            return this.Send(info.TaskId, variable, "", this.GetSummary(form), form);
        }

        public TaskSendResult DianKongPingShen(string taskId, DateTime wanChengRiQi, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.DianQiWanChengRiQi = wanChengRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult EngEngineerPingShen(string taskId, DateTime wanChengRiQi, TaskApproveInfo approveInfo, DateTime? waiGouWanChengRiQi)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.SheJiWanChengRiQi = wanChengRiQi;
            form.WaiGouWanChengRiQi = waiGouWanChengRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult EngPingShenConfirm(string taskId, DateTime wanChengRiQi, TaskApproveInfo approveInfo, DateTime? waiGouWanChengRiQi)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.SheJiWanChengRiQi = wanChengRiQi;
            form.WaiGouWanChengRiQi = waiGouWanChengRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult BomPingShen(string taskId, TaskApproveInfo approveInfo, DateTime zhengJiWanChengRiQi, DateTime? shouCiWanChengRiQi)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.ZhengJiWanChengRiQi = zhengJiWanChengRiQi;
            form.ShouCiWanChengRiQi = shouCiWanChengRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult PmcPingShen(string taskId, TaskApproveInfo approveInfo, string scmEngineerAccount, DateTime? jiZuWanGongRiQi)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_NEED_WULIAO_JIHUA_PINGSHEN, objVariableValue = new object[]{string.IsNullOrEmpty(scmEngineerAccount) ? "否" : "是"} },
                new Variable{ strVariableName = PARAM_SCM_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(scmEngineerAccount)} }
            };
            OrderPingShenForm form = this.Get(taskId);
            form.ScmEngineerAccount = scmEngineerAccount;
            form.JiZuWanGongRiQi = jiZuWanGongRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, variable, "", this.GetSummary(form), form);
        }

        public TaskSendResult ScmPingShen(string taskId, TaskApproveInfo approveInfo, DateTime? wuLiaoDaoHuoRiQi)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.WuLiaoDaoHuoRiQi = wuLiaoDaoHuoRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult JiZuWanGongPingShen(string taskId, TaskApproveInfo approveInfo, DateTime jiZuWanGongRiQi)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.JiZuWanGongRiQi = jiZuWanGongRiQi;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult FaqirenQueren(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.PingshenWancheng = true;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult CidQueRen(string taskId, TaskApproveInfo approveInfo, List<ProcessFile> cidZiLiao)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.CidZiLiao = cidZiLiao;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult QadQueRen(string taskId, TaskApproveInfo approveInfo, List<ProcessFile> qadZiLiao)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.QadZiLiao = qadZiLiao;
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult WenJianFaFang(List<ProcessFile> shejiZiliao, int instanceNo)
        {
            string taskId = this.GetIncidentTaskId(SYSTEM_ACCOUNT, instanceNo);
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.FafangWancheng = true;
            if (form.SheJiZiLiao == null)
            {
                form.SheJiZiLiao = new List<ProcessFile>();
            }
            form.SheJiZiLiao.AddRange(shejiZiliao);
            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public void SaveWenJianFaFang(List<ProcessFile> shejiZiliao, int instanceNo)
        {
            string taskId = this.GetIncidentTaskId("system_gz_process", instanceNo);
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }

            OrderPingShenForm form = this.Get(taskId);
            if (form.SheJiZiLiao == null)
            {
                form.SheJiZiLiao = new List<ProcessFile>();
            }
            form.SheJiZiLiao.AddRange(shejiZiliao);
            this.Save(taskId, form);
        }

        public void AddWenJianFaFangLiucheng(OrderWenjianFafangForm wenjianFafangForm, int instanceNo)
        {
            OrderPingShenForm form = this.Get(instanceNo);
            if (form.WenjianFafangForms == null)
            {
                form.WenjianFafangForms = new List<OrderWenjianFafangForm>();
            }
            form.WenjianFafangForms.Add(wenjianFafangForm);
            this.Save(instanceNo, form);
        }

        public TaskSendResult JiZuWanChengQueRen(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderPingShenForm form = this.Get(taskId);
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }
    }
}
