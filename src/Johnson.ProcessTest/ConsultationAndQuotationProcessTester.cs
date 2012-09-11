using System;
using System.Collections.Generic;
using System.Text;
using Johnson.Process.Core;
using NUnit.Framework;
using System.Threading;

namespace Johnson.ProcessTest
{
    public class ConsultationAndQuotationProcessTester : ProcessTester
    {
        string processName = "技术咨询及报价2";

        [Test]
        public void TestMarketingDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestCsdEngineerDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestCsdTracerDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部跟进决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSend(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestEngDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部给技术部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToEng(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //技术部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.EngEngineerSend(taskId, form);
            Thread.Sleep(2000);

            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestEngDecisionCidSuggestion()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部跟进
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToEng(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //技术部给工艺部建议
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.EngEngineerSendToCid(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //工艺部建议
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CidSend(taskId, form);
            Thread.Sleep(2000);

            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestEngDecisionQadSuggestion()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部跟进
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToEng(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //技术部给质保部建议
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.EngEngineerSendToQad(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //质保部建议
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QadSend(taskId, form);
            Thread.Sleep(2000);

            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestEngDecisionQadAndCidSuggestion()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部给技术部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToEng(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //技术部给质保部和工艺部建议
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.EngEngineerSendToQadAndCid(qiAccount, qiAccount, taskId, form);
            Thread.Sleep(2000);

            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            object stepId = process.GetVariableValue(taskId, "stepId");
            if (stepId.ToString() == "71")
            {
                //工艺部建议
                process.CidSend(taskId, form);
                Thread.Sleep(2000);
            }
            else
            {
                //质保部建议
                process.QadSend(taskId, form);
                Thread.Sleep(2000);
            }

            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            stepId = process.GetVariableValue(taskId, "stepId");
            if (stepId.ToString() == "91")
            {
                //质保部建议
                process.QadSend(taskId, form);
                Thread.Sleep(2000);
            }
            else
            {
                //工艺部建议
                process.CidSend(taskId, form);
                Thread.Sleep(2000);
            }

            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestLogDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部更近人给物流部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToLog(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //物流部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.LogSend(taskId, form);
            Thread.Sleep(2000);

            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestScmDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部给采购部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToScm(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //采购部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.ScmSend(taskId, form);
            Thread.Sleep(2000);

            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        [Test]
        public void TestLogAndScmDecision()
        {
            ConsultationAndQuotationForm form = new ConsultationAndQuotationForm();
            form.ProjectName = "test";
            ConsultationAndQuotationProcess process = new ConsultationAndQuotationProcess(processName, "", "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);

            //发起
            TaskSendResult result = process.Start(qiAccount, qiAccount, taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //市场部
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.MarketingSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部工程师
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdEngineerSend(qiAccount, taskId, form);
            Thread.Sleep(2000);

            //客服部给物流部和采购部决策
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.CsdTracerSendToLogAndScm(qiAccount, qiAccount, taskId, form);
            Thread.Sleep(2000);

            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            object stepId = process.GetVariableValue(taskId, "stepId");
            if (stepId.ToString() == "51")
            {
                //物流部决策
                process.LogSend(taskId, form);
                Thread.Sleep(2000);
            }
            else
            {
                //采购部决策
                process.ScmSend(taskId, form);
                Thread.Sleep(2000);
            }

            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            stepId = process.GetVariableValue(taskId, "stepId");
            if (stepId.ToString() == "61")
            {
                //采购部决策
                process.ScmSend(taskId, form);
                Thread.Sleep(2000);
            }
            else
            {
                //物流部决策
                process.LogSend(taskId, form);
                Thread.Sleep(2000);
            }


            //客服部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //市场部最终答复
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }
    }
}
