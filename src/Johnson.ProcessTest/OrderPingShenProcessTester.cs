using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Johnson.Process.Core;
using System.Threading;

namespace Johnson.ProcessTest
{
    public class OrderPingShenProcessTester : ProcessTester
    {
        string processName = "广州订单评审";

        [Test]
        public void Test1()
        {
            OrderPingShenStartInfo startInfo = new OrderPingShenStartInfo();
            OrderPingShenProcess process = new OrderPingShenProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            startInfo.StartUserAccount = qiAccount;
            startInfo.TaskId = taskId;
            startInfo.SheJiFuZeRenAccount = qiAccount;
            startInfo.PmcEngineerAccount = qiAccount;
            startInfo.ApproveInfo = new TaskApproveInfo();
            //发起
            TaskSendResult result = process.Start(startInfo);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //设计评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            OrderEngFuZeRenPingShenInfo engPingShenInfo = new OrderEngFuZeRenPingShenInfo
            {
                ApproveInfo = new TaskApproveInfo { },
                IsStandard = true,
                EngEngineerAccount = qiAccount,
                TaskId = taskId
            };
            process.EngFuZeRenPingShen(engPingShenInfo);
            Thread.Sleep(2000);

            //Bom完成评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.BomPingShen(taskId, new TaskApproveInfo { }, DateTime.Now, null);
            Thread.Sleep(2000);

            //PMC评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.PmcPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //设计提交
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.SheJiTiJiao(taskId, new TaskApproveInfo { }, false, qiAccount, "", qiAccount, "", "", "", null);
            Thread.Sleep(2000);

            //技术检查
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiShuChaJian(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //技术主管审批
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiShuZhuGuanShenPi(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //文件发放
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.WenJianFaFang(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //Bom更改
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.BomLuRu(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }

        [Test]
        public void Test2()
        {
            OrderPingShenStartInfo startInfo = new OrderPingShenStartInfo();
            OrderPingShenProcess process = new OrderPingShenProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            startInfo.StartUserAccount = qiAccount;
            startInfo.TaskId = taskId;
            startInfo.SheJiFuZeRenAccount = qiAccount;
            startInfo.PmcEngineerAccount = qiAccount;
            startInfo.CidPingShenRenAccounts = qiAccount;
            startInfo.QadPingShenRenAccounts = qiAccount;
            startInfo.ApproveInfo = new TaskApproveInfo();
            //发起
            TaskSendResult result = process.Start(startInfo);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //组织评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.DeptPingShenResult(taskId, "", new TaskApproveInfo { });
            Thread.Sleep(2000);

            //设计评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            OrderEngFuZeRenPingShenInfo engPingShenInfo = new OrderEngFuZeRenPingShenInfo
            {
                ApproveInfo = new TaskApproveInfo { },
                EngEngineerAccount = qiAccount,
                DianQiEngineerAccount = qiAccount,
                TaskId = taskId
            };
            process.EngFuZeRenPingShen(engPingShenInfo);
            Thread.Sleep(2000);

            //电气工程师评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.DianKongPingShen(taskId, DateTime.Now, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //设计工程师评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.EngEngineerPingShen(taskId, DateTime.Now, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //设计负责人确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.EngPingShenConfirm(taskId, DateTime.Now, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //Bom完成评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.BomPingShen(taskId, new TaskApproveInfo { }, DateTime.Now, null);
            Thread.Sleep(2000);

            //PMC评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.PmcPingShen(taskId, new TaskApproveInfo { }, qiAccount);
            Thread.Sleep(2000);

            //物料评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //设计提交
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo, "提交设计");
            process.SheJiTiJiao(taskId, new TaskApproveInfo { }, true, qiAccount, "", qiAccount, "", "", "", null);
            Thread.Sleep(2000);

            //qad确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QadQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //cid确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CidQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //技术检查
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiShuChaJian(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //技术主管审批
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiShuZhuGuanShenPi(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //文件发放
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.WenJianFaFang(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //新物料信息维护
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.XinWuLiaoXinXiWeiHu(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //Bom录入
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.BomLuRu(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }
    }
}
