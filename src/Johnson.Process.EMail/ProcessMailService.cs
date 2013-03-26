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
        string _edoc2BaseUrl;

        public ProcessMailService(ILog _logger, string edoc2BaseUrl)
        {
            this._logger = _logger;
            this._edoc2BaseUrl = edoc2BaseUrl;
        }

        public void Start()
        {
            try
            {
                List<ProcessEmailEntity> entitys = ProcessEmailDataProvider.Current.SelectStatusIs0();
                foreach (ProcessEmailEntity entity in entitys)
                {
                    MailSender.Current.Send(entity.Email, entity.Subject, "", "", true, entity.Content.Replace("${edoc2BaseUrl}", this._edoc2BaseUrl));
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
