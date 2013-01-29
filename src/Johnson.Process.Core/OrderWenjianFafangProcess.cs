using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class OrderWenjianFafangProcess : UltimusFormProcess<OrderWenjianFafangForm>
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

        public OrderWenjianFafangProcess(string processName)
            :base(processName)
        {

        }

        public TaskSendResult Start(string taskId, string startUserAccount, string startUserName, TaskApproveInfo approveInfo, bool hasXinWuLiao, string jianChaEngineerAccount, string jianChaEngineerName,
            string zhuGuanAccount, string zhuGuanName, string sheJiShuoMing, bool fafangWancheng, List<ProcessFile> sheJiZiLiao, int orderPingshenIncidentNo,
            string scmEngineerAccount)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (string.IsNullOrEmpty(jianChaEngineerAccount))
            {
                throw new ArgumentNullException("jiShuJianChaGongChengShi");
            }
            if (string.IsNullOrEmpty(zhuGuanAccount))
            {
                throw new ArgumentNullException("zhuGuanAccount");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }
            Variable[] variable = new Variable[]{
                new Variable{ strVariableName = PARAM_HAS_NEW_WULIAO, objVariableValue = new object[]{hasXinWuLiao ? "是" : "否"} },
                new Variable{ strVariableName = PARAM_ENG_JIANCHA_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(jianChaEngineerAccount)} },
                new Variable{ strVariableName = PARAM_SHEJI_ENGINEER_ZHUGUAN, objVariableValue = new object[]{this.GetUltimusUserAccount(zhuGuanAccount)} },
                new Variable{ strVariableName = PARAM_SCM_ENGINEER, objVariableValue = new object[]{this.GetUltimusUserAccount(scmEngineerAccount)} }
            };
            OrderWenjianFafangForm form = new OrderWenjianFafangForm();
            form.StartUserAccount = startUserAccount;
            form.StartUserName = startUserName;
            form.StartTime = DateTime.Now;
            form.HasXinWuLiao = hasXinWuLiao;
            form.JianChaEngineerAccount = jianChaEngineerAccount;
            form.JianChaEngineerName = jianChaEngineerName;
            form.ZhuGuanAccount = zhuGuanAccount;
            form.ZhuGuanName = zhuGuanName;
            form.SheJiShuoMing = sheJiShuoMing;
            form.FafangWancheng = fafangWancheng;
            form.SheJiZiLiao = sheJiZiLiao;
            form.OrderPingshenIncidentNo = orderPingshenIncidentNo;
            form.Approves = new List<TaskApproveInfo>();
            form.Approves.Insert(0, approveInfo);

            TaskSendResult result = this.Start(startUserAccount, taskId, variable, "", this.GetSummary(form), form);
            form = this.Get(result.IncidentNo);
            form.IncidentNo = result.IncidentNo;
            this.Save(form.IncidentNo, form);
            return result;
        }

        private string GetSummary(OrderWenjianFafangForm form)
        {
            return "";
        }

        public TaskSendResult JiShuChaJian(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderWenjianFafangForm form = this.Get(taskId);
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult JiShuZhuGuanShenPi(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderWenjianFafangForm form = this.Get(taskId);
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult WenJianFaFang(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderWenjianFafangForm form = this.Get(taskId);
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult BomLuRu(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderWenjianFafangForm form = this.Get(taskId);
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }

        public TaskSendResult XinWuLiaoXinXiWeiHu(string taskId, TaskApproveInfo approveInfo)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                throw new ArgumentNullException("taskId");
            }
            if (approveInfo == null)
            {
                throw new ArgumentNullException("approveInfo");
            }

            OrderWenjianFafangForm form = this.Get(taskId);
            form.Approves.Insert(0, approveInfo);

            return this.Send(taskId, null, "", this.GetSummary(form), form);
        }
    }
}
