using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using log4net;

namespace Johnson.Process.WindowsServiceManagementService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Thread thread = new Thread(new ThreadStart(this._Start));
            thread.IsBackground = true;
            thread.Start();
        }
        private void _Start()
        {
            log4net.Config.XmlConfigurator.Configure();
            ILog logger = log4net.LogManager.GetLogger("Johnson_Process_logger");
            JohnsonProcessWindowsServiceRestartor restartor = new JohnsonProcessWindowsServiceRestartor();
            try
            {
                while (true)
                {
                    try
                    {
                        restartor.Monitor();

                        Thread.Sleep(1000* 60 * 50);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                throw;
            }
        }
        protected override void OnStop()
        {
        }
    }
}
