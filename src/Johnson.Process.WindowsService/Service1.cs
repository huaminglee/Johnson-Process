using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Johnson.Process.Core;
using log4net;
using Johnson.Process.EMail;
using System.Configuration;
using System.Threading;

namespace Johnson.Process.WindowsService
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
            try
            {
                string edoc2BaseUrl = ConfigurationManager.AppSettings["edoc2BaseUrl"];


                VocProcess vocProcess = new VocProcess("VOC");
                vocProcess.TaskTransferAddress = edoc2BaseUrl + "/JohnsonProcess/Voc_Transfer.aspx";
                TaskEmailNotifySerivce vocService = new TaskEmailNotifySerivce(vocProcess, logger);

                VocProcess deliveryProcess = new VocProcess("货期管理");
                deliveryProcess.TaskTransferAddress = edoc2BaseUrl + "/JohnsonProcess/Delivery_Transfer.aspx";
                TaskEmailNotifySerivce deliveryService = new TaskEmailNotifySerivce(deliveryProcess, logger);

                VocProcess consultationAndQuotationProcess = new VocProcess("技术咨询及报价");
                consultationAndQuotationProcess.TaskTransferAddress = edoc2BaseUrl + "/JohnsonProcess/ConsultationAndQuotation_Transfer.aspx";
                TaskEmailNotifySerivce consultationAndQuotationService = new TaskEmailNotifySerivce(consultationAndQuotationProcess, logger);

                ProcessMailService processMailService = new ProcessMailService(logger);

                while (true)
                {
                    try
                    {
                        vocService.Start();
                        deliveryService.Start();
                        consultationAndQuotationService.Start();
                        processMailService.Start();

                        Thread.Sleep(5000);
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
