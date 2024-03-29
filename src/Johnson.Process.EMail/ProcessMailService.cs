﻿using System;
using System.Collections.Generic;
using System.Text;
using Johnson.Process.Core;
using Johnson.Process.EMail.Exceptions;
using System.Threading;
using log4net;

namespace Johnson.Process.EMail
{
    public class ProcessMailService
    {
        ILog _logger;
        string _liuchengBaseUrl;

        public ProcessMailService(ILog _logger, string liuchengBaseUrl)
        {
            this._logger = _logger;
            this._liuchengBaseUrl = liuchengBaseUrl;
        }

        public void Start()
        {
            this._logger.Info("start ProcessMailService");
            List<ProcessEmailEntity> entitys = ProcessEmailDataProvider.Current.SelectStatusIs0();
            foreach (ProcessEmailEntity entity in entitys)
            {
                try
                {
                    MailSender.Current.Send(entity.Email, entity.Subject, "", "", true, entity.Content.Replace("${liuchengBaseUrl}", this._liuchengBaseUrl));
                }
                catch (TaskEmailNotifySerivceException ex)
                {
                    this._logger.Error(ex.Message, ex);
                }
                ProcessEmailDataProvider.Current.UpdateStatusAs1(entity.ID);
            }
        }
    }
}
