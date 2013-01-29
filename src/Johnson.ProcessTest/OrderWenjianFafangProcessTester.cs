using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Johnson.Process.Core;
using System.Threading;

namespace Johnson.ProcessTest
{
    public class OrderWenjianFafangProcessTester : ProcessTester
    {
        string processName = "广州订单文件发放";

        [Test]
        public void Test1()
        {
            OrderPingShenStartInfo startInfo = new OrderPingShenStartInfo();
            OrderWenjianFafangProcess process = new OrderWenjianFafangProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = process.GetStartTaskId(qiAccount);
            //发起
            TaskSendResult result = process.Start(taskId, qiAccount, qiAccount, new TaskApproveInfo(),
                false, qiAccount, qiAccount, qiAccount, qiAccount, "", false, new List<ProcessFile>(), 0, qiAccount);
            Assert.IsTrue(result.IncidentNo > 0);
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
        }

        [Test]
        public void Test2()
        {
            OrderPingShenStartInfo startInfo = new OrderPingShenStartInfo();
            OrderWenjianFafangProcess process = new OrderWenjianFafangProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            //发起
            TaskSendResult result = process.Start(taskId, qiAccount, qiAccount, new TaskApproveInfo(),
                true, qiAccount, qiAccount, qiAccount, qiAccount, "", false, new List<ProcessFile>(), 0, qiAccount);
            Assert.IsTrue(result.IncidentNo > 0);
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

            //新物料维护
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.XinWuLiaoXinXiWeiHu(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);

            //Bom更改
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.BomLuRu(taskId, new TaskApproveInfo { });
            Thread.Sleep(2000);
        }
    }
}
