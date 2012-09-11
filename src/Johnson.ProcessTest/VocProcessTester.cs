using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Johnson.Process.Core;
using System.Threading;

namespace Johnson.ProcessTest
{
    public class VocProcessTester : ProcessTester
    {

        string processName = "VOC2";

        [Test]
        public void Test1()
        {
            VocForm form = new VocForm();
            form.ResponsibleUserAccount = "qi";
            VocProcess process = new VocProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //行动计划
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.Actions = new List<VocAction>();
            form.Actions.Add(new VocAction { UserAccount = "qi" });
            form.Actions.Add(new VocAction { UserAccount = "qi" });
            form.Actions.Add(new VocAction { UserAccount = "qi1" });
            process.ResponsiblePlanAction(taskId, form);
            Thread.Sleep(2000);

            //完成现场解决方案
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //行动完成1
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //行动完成2
            taskId = this.GetIncidentTaskId(processName, process.GetUltimusUserAccount("qi1"), result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //原因分析
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MeasureUserAccount = "qi";
            process.ResponsibleReason(taskId, form);
            Thread.Sleep(2000);

            //纠正预防处理措施
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //改善负责人
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.ResponsibleUserPreviousAccount = "qi";
            process.ResponsibleHandle(taskId, form);
            Thread.Sleep(2000);

            //改善负责人上级
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //ASD确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void Test2()
        {
            VocForm form = new VocForm();
            form.ResponsibleUserAccount = "qi";
            VocProcess process = new VocProcess(processName);
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string qi2Account = "qi2";
            string qi2UltimusAccount = process.GetUltimusUserAccount("qi2");

            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qi2Account, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //行动计划
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.Actions = new List<VocAction>();
            form.Actions.Add(new VocAction { UserAccount = "qi" });
            form.Actions.Add(new VocAction { UserAccount = "qi" });
            form.Actions.Add(new VocAction { UserAccount = "qi1" });
            process.ResponsiblePlanAction(taskId, form);
            Thread.Sleep(2000);

            //退回重新给出现场解决方案
            taskId = this.GetIncidentTaskId(processName, qi2UltimusAccount, result.IncidentNo, "完成现场解决方案");
            process.Return(taskId, form);
            Thread.Sleep(2000);

            //给出现场解决方案
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo, "给出现场解决方案");
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //完成现场解决方案
            taskId = this.GetIncidentTaskId(processName, qi2UltimusAccount, result.IncidentNo, "完成现场解决方案");
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //行动完成1
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //行动完成2
            taskId = this.GetIncidentTaskId(processName, process.GetUltimusUserAccount("qi1"), result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //原因分析
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MeasureUserAccount = "qi";
            process.ResponsibleReason(taskId, form);
            Thread.Sleep(2000);

            //纠正预防处理措施
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //改善负责人
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.ResponsibleUserPreviousAccount = "qi";
            process.ResponsibleHandle(taskId, form);
            Thread.Sleep(2000);

            //改善负责人上级
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //ASD确认
            taskId = this.GetIncidentTaskId(processName, qi2UltimusAccount, result.IncidentNo, "ASD确认");
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }
    }
}
