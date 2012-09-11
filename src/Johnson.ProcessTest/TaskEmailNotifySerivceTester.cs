using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Johnson.Process.EMail;
using Johnson.Process.Core;
using log4net;

namespace Johnson.ProcessTest
{
    public class TaskEmailNotifySerivceTester
    {
        [Test]
        public void Test()
        {
            VocProcess vocProcess = new VocProcess("货期管理");
            vocProcess.TaskTransferAddress = "http://localhost/EDoc2V4/JohnsonProcess/DeliveryController.aspx";
            
            log4net.Config.XmlConfigurator.Configure();
            ILog logger = log4net.LogManager.GetLogger("Johnson_Process_logger");

            TaskEmailNotifySerivce service = new TaskEmailNotifySerivce(vocProcess, logger);
            service.Start();
        }
    }
}
