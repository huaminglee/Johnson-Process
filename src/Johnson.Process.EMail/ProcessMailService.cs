using System;
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

        public ProcessMailService(ILog _logger)
        {
            this._logger = _logger;
        }

        public void Start()
        {
            try
            {
                List<ProcessEmailEntity> entitys = ProcessEmailDataProvider.Current.SelectStatusIs0();
                foreach (ProcessEmailEntity entity in entitys)
                {
                    MailSender.Current.Send(entity.Email, entity.Subject, "", "", true, entity.Content);
                    ProcessEmailDataProvider.Current.UpdateStatusAs1(entity.ID);
                }
            }
            catch (TaskEmailNotifySerivceException ex)
            {
                this._logger.Error(ex.Message, ex);
            }
        }
    }
}
