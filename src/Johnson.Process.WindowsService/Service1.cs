﻿using System;
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
                string liuchengBaseUrl = ConfigurationManager.AppSettings["liuchengBaseUrl"];


                VocProcess vocProcess = new VocProcess("VOC");
                vocProcess.TaskTransferAddress = liuchengBaseUrl + "Voc_Transfer.aspx";
                TaskEmailNotifySerivce vocService = new TaskEmailNotifySerivce(vocProcess, logger);

                DeliveryProcess deliveryProcess = new DeliveryProcess("货期管理");
                deliveryProcess.TaskTransferAddress = liuchengBaseUrl + "Delivery_Transfer.aspx";
                TaskEmailNotifySerivce deliveryService = new TaskEmailNotifySerivce(deliveryProcess, logger);

                ConsultationAndQuotationProcess consultationAndQuotationProcess = new ConsultationAndQuotationProcess("技术咨询及报价", "", "");
                consultationAndQuotationProcess.TaskTransferAddress = liuchengBaseUrl + "ConsultationAndQuotation_Transfer.aspx";
                TaskEmailNotifySerivce consultationAndQuotationService = new TaskEmailNotifySerivce(consultationAndQuotationProcess, logger);

                FailureProductProcess failureProductProcess = new FailureProductProcess("不合格品处理", "", "");
                failureProductProcess.TaskTransferAddress = liuchengBaseUrl + "FailureProduct_Transfer.aspx";
                TaskEmailNotifySerivce failureProductProcessService = new TaskEmailNotifySerivce(failureProductProcess, logger);

                ProcessMailService processMailService = new ProcessMailService(logger, liuchengBaseUrl);

                while (true)
                {
                    try
                    {
                        processMailService.Start();

                        vocService.Start();
                        deliveryService.Start();
                        consultationAndQuotationService.Start();
                        failureProductProcessService.Start();

                        Thread.Sleep(15000);
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
