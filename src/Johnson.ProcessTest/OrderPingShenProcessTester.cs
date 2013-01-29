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

        /// <summary>
        /// 标准订单
        /// </summary>
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

            //PMC评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.PmcPingShen(taskId, new TaskApproveInfo { }, null, DateTime.Now);
            Thread.Sleep(2000);

            //机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 发起人组织CID,QAD评审
        /// </summary>
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
            process.PmcPingShen(taskId, new TaskApproveInfo { }, qiAccount, DateTime.Now);
            Thread.Sleep(2000);

            //物料评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //发起人确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //qad确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QadQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //cid确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CidQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //文件发放
            process.WenJianFaFang(new List<ProcessFile>(), result.IncidentNo);
            Thread.Sleep(2000);

            ////机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 发起人组织QAD评审
        /// </summary>
        [Test]
        public void Test3()
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
            process.PmcPingShen(taskId, new TaskApproveInfo { }, qiAccount, DateTime.Now);
            Thread.Sleep(2000);

            //物料评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000); 
            
            //发起人确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //qad确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QadQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //文件发放
            process.WenJianFaFang(new List<ProcessFile>(), result.IncidentNo);
            Thread.Sleep(2000);

            ////机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 发起人组织CID评审
        /// </summary>
        [Test]
        public void Test4()
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
            process.PmcPingShen(taskId, new TaskApproveInfo { }, qiAccount, DateTime.Now);
            Thread.Sleep(2000);

            //物料评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //发起人确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //文件发放
            process.WenJianFaFang(new List<ProcessFile>(), result.IncidentNo);
            Thread.Sleep(2000);

            //cid确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QadQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            ////机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 发起人不组织评审
        /// </summary>
        [Test]
        public void Test5()
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
            process.PmcPingShen(taskId, new TaskApproveInfo { }, qiAccount, DateTime.Now);
            Thread.Sleep(2000);

            //物料评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //发起人确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //文件发放
            process.WenJianFaFang(new List<ProcessFile>(), result.IncidentNo);
            Thread.Sleep(2000);

            ////机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 设计负责人组织评审
        /// </summary>
        [Test]
        public void Test6()
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
                EngEngineerAccount = qiAccount,
                DianQiEngineerAccount = qiAccount,
                TaskId = taskId,
                QadPingShenRenAccounts = qiAccount
            };
            process.EngFuZeRenPingShen(engPingShenInfo);
            Thread.Sleep(2000);

            //组织评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.DeptPingShenResult(taskId, "", new TaskApproveInfo { });
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
            process.PmcPingShen(taskId, new TaskApproveInfo { }, qiAccount, DateTime.Now);
            Thread.Sleep(2000);

            //物料评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmPingShen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //机组完工评审
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //发起人确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanGongPingShen(taskId, new TaskApproveInfo { }, DateTime.Now);
            Thread.Sleep(2000);

            //qad确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QadQueRen(taskId, new TaskApproveInfo { }, null);
            Thread.Sleep(2000);

            //文件发放
            process.WenJianFaFang(new List<ProcessFile>(), result.IncidentNo);
            Thread.Sleep(2000);

            ////机组完成确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.JiZuWanChengQueRen(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }
    }
}
