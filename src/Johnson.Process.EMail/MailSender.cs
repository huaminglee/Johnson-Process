using System;
using System.Collections.Generic;
using System.Text;
using edoc2 = EDoc2;
using Johnson.Process.EMail.Exceptions;
using EDoc2.Email;
using EDoc2;

namespace Johnson.Process.EMail
{
    public class MailSender
    {
        static MailSender _current;
        public static MailSender Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new MailSender();
                }
                return _current;
            }
        }

        private MailSender()
        {

        }

        public void Send(string recipientsAddress, string subject, string emailAddressCc, string emailAddressBcc, bool isBodyHtml, string mailContent)
        {
            try
            {
                

#if DEBUG
                EDoc2.Mail.CommonMailSender mailSender = new EDoc2.Mail.CommonMailSender(1);
                mailSender.Server = "smtp.vip.163.com";
                mailSender.UserName = "edoc2";
                mailSender.Password = "13651752165";
                mailSender.From = new System.Net.Mail.MailAddress("edoc2@vip.163.com");
#else
                EDoc2.InstanceConfigInfo config;
                ApiManager.Api.SystemManagement.GetInstanceConfig(ApiManager.CurrentUserToken, out config);
                EDoc2.Mail.CommonMailSender mailSender = new EDoc2.Mail.CommonMailSender(config.SendMailServerType);
                mailSender.Server = config.SmtpServer;
                mailSender.UserName = config.SmtpUserName;
                mailSender.Password = config.SmtpPassword;
                string smtpMail = config.SmtpEmail.Trim();
                mailSender.From = new System.Net.Mail.MailAddress(smtpMail);
#endif

                mailSender.Subject = subject;
                mailSender.IsBodyHtml = true;
                mailSender.Body = mailContent;

                mailSender.To.Add(recipientsAddress);

                mailSender.Send();
            }
            catch(Exception ex)
            {
                throw new TaskEmailNotifySerivceException(string.Format("邮件发送失败, 邮件地址:{0}, subject: {1}, 错误信息: {2} ", 
                    recipientsAddress, subject, ex.Message));
            }
        }
    }
}
