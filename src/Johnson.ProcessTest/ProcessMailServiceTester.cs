using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Johnson.Process.EMail;
using log4net;

namespace Johnson.ProcessTest
{
    public class ProcessMailServiceTester
    {
        [Test]
        public void Test()
        {
            log4net.Config.XmlConfigurator.Configure();
            ILog logger = log4net.LogManager.GetLogger("Johnson_Process_logger");

            ProcessMailService service = new ProcessMailService(logger);
            service.Start();
        }
    }
}
