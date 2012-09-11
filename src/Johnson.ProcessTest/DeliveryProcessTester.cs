using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Ultimus.WFServer;
using System.Threading;
using Johnson.Process.Core;

namespace Johnson.ProcessTest
{
    public class DeliveryProcessTester : ProcessTester
    {
        [Test]
        public void DeliveryProcessTest()
        {
            string processName = "货期管理1";
            DeliveryProcessForm form = new DeliveryProcessForm { BookDate = DateTime.Now, ProjectName = "ssd" };

            DeliveryProcess process = new DeliveryProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //客服部处理-退回发起人
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Return(taskId, form);
            Thread.Sleep(2000);

            //发起人处理退回任务
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //客服部处理-需要物流答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CustomerServiceEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //物流部答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, null, "", "", form);
            Thread.Sleep(2000);

            //客服部处理
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, null, "", "", form);
            Thread.Sleep(2000);
            //流程结束


            //发起流程
            taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //客服部处理-不需要物流部答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CustomerServiceEngineerSend(taskId, form);
            Thread.Sleep(2000);
            //流程结束
        }
    }
}
