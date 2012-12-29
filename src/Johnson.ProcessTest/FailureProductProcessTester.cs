using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Johnson.Process.Core;
using System.Threading;

namespace Johnson.ProcessTest
{
    public class FailureProductProcessTester : ProcessTester
    {
        #region qe 决定
        /// <summary>
        /// 挑选
        /// </summary>
        [Test]
        public void Test1()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.Pick;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //QC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 让步接收
        /// </summary>
        [Test]
        public void Test2()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            form.FinUserAccount = qiAccount;
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.Receive;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //QA
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 退货
        /// </summary>
        [Test]
        public void Test3()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.Return;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 报废
        /// </summary>
        [Test]
        public void Test4()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.Scrap;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 返工返修
        /// </summary>
        [Test]
        public void Test5()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.Rework;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //发起人
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //Eng
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //工艺方案
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //PMC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //FIN确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        #endregion

        #region mrb 意见一致
        /// <summary>
        /// 挑选
        /// </summary>
        [Test]
        public void Test6()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //QC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 让步接收
        /// </summary>
        [Test]
        public void Test7()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 退货
        /// </summary>
        [Test]
        public void Test8()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Return });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Return });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Return });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Return });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 报废
        /// </summary>
        [Test]
        public void Test9()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Scrap });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Scrap });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Scrap });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Scrap });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
        }

        /// <summary>
        /// 返工返修
        /// </summary>
        [Test]
        public void Test10()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Rework });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Rework });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Rework });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Rework });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //填写返工返修单
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //Eng
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //工艺方案
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //PMC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //FIN确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }
        #endregion

        #region mrb 意见不一致
        /// <summary>
        /// 挑选
        /// </summary>
        [Test]
        public void Test11()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //QA
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Pick;
            process.QASend(taskId, form);
            Thread.Sleep(2000);

            //QC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Pick;
            process.QASend(taskId, form);
        }

        /// <summary>
        /// 让步接收
        /// </summary>
        [Test]
        public void Test12()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //QA
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Receive;
            process.QASend(taskId, form);
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 退货
        /// </summary>
        [Test]
        public void Test13()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //QA
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Return;
            process.QASend(taskId, form);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Pick;
            process.QASend(taskId, form);
        }

        /// <summary>
        /// 报废
        /// </summary>
        [Test]
        public void Test14()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //QA
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Scrap;
            process.QASend(taskId, form);
            Thread.Sleep(2000);

            //仓库
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Pick;
            process.QASend(taskId, form);
        }

        /// <summary>
        /// 返工返修
        /// </summary>
        [Test]
        public void Test15()
        {
            string processName = "不合格品处理";
            FailureProductForm form = new FailureProductForm { };

            FailureProductProcess process = new FailureProductProcess(processName, "");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //PMC
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.EngUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            form.CsdUserAccount = qiAccount;
            
            form.FinUserAccount = qiAccount;
            
            form.QCUserAccount = qiAccount;
            form.QEResult = FailureResult.MRB;
            process.QESend(taskId, form, null);
            Thread.Sleep(2000);

            //MRB
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults = new List<MrbFailureResult>();
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Receive });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.MrbResults.Add(new MrbFailureResult { Result = FailureResult.Pick });
            process.MrbSend(taskId, form);
            Thread.Sleep(2000);

            //QA
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            form.QAResult = FailureResult.Rework;
            process.QASend(taskId, form);
            Thread.Sleep(2000);

            //填写返工返修单
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //Eng
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //工艺方案
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //PMC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //FIN确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }

        #endregion

        #region 直接返工返修

        /// <summary>
        /// 返工返修
        /// </summary>
        [Test]
        public void Test16()
        {
            string processName = "不合格品处理";
            ProductReworkForm form = new ProductReworkForm { };

            ProductReworkProcess process = new FailureProductProcess(processName, "").ProductReworkProcess;
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount(qiAccount);

            //发起流程
            string taskId = this.GetProcessStartTaskId(processName, qiUltimusAccount);
            form.PmcUserAccount = qiAccount;
            form.QEUserAccount = qiAccount;
            form.EngUserAccount = qiAccount;
            form.FinUserAccount = qiAccount;
            form.QCUserAccount = qiAccount;
            form.CidUserAccount = qiAccount;
            TaskSendResult result = process.Start(qiAccount, "", taskId, form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            //QC确认返工返修
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.QCSend(taskId, form);
            Thread.Sleep(2000);

            //Eng
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //工艺方案
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QE确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //PMC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //FIN确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);

            //QC确认
            taskId = this.GetIncidentTaskId(processName, qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, form);
            Thread.Sleep(2000);
        }
        #endregion
    }
}
