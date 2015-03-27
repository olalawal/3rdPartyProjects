using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Logging;
using System.Net.Mail;
using System.Configuration;
using StatisticalSolutions.ViewModels;

namespace StatisticalSolutions.Util
{

    internal class MailSender
    {
        protected readonly ILog _log = LogManager.GetCurrentClassLogger();
        MailMessage _mailMessage;     


        public MailSender()
        {
            _mailMessage = new MailMessage();

        }

        /// <summary>
        /// Retrieve File location path from web.config and initialize FilePaths class
        /// </summary>
        /// <returns></returns>
        public Mails SetMailsProperty()  
        {
            string hostServer = ConfigurationManager.AppSettings["SmtpServer"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            //string mailFrom = ConfigurationManager.AppSettings["MailFrom"];
            string authUsername = ConfigurationManager.AppSettings["_authUsername"].ToString();
            string authPassword = ConfigurationManager.AppSettings["_authPassword"].ToString();
            bool isCredentialRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["IsCredentialRequired"]);
            bool isEnableSSl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
            bool isBodyHtml = Convert.ToBoolean(ConfigurationManager.AppSettings["IsBodyHtml"]);
            Mails mails = new Mails(hostServer, port, authUsername, authPassword, isEnableSSl, isCredentialRequired, isBodyHtml);
            return mails;
        }

         /// <summary>
       /// Sending Mail
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
        public bool SendMail(Mails mail)
        {
            try
            {

                _log.Info(m => m("Mail Sender - {0}", "SendMail Method Start"));
                _mailMessage.To.Add(mail.To);
                if (!string.IsNullOrEmpty(mail.CC))
                    _mailMessage.CC.Add(mail.CC);
                if (!string.IsNullOrEmpty(mail.BCC))
                    _mailMessage.Bcc.Add(mail.BCC);
                _mailMessage.From = new MailAddress(mail.From, mail.Name, System.Text.Encoding.UTF8);
                _mailMessage.Subject = mail.Subject;
                _mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                _mailMessage.Body = mail.Body;
                _mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                _mailMessage.IsBodyHtml = mail.IsBodyHtml;
                _mailMessage.Priority = MailPriority.High;
                SmtpClient smtpServer = new SmtpClient(mail.HostServer);
                smtpServer.Port = mail.Port;
                _log.Info(m => m("Mail Sender - Attaching Files"));
                //if (mail.FilesToAttach.Length > 0)
                //{
                //    foreach (string fileName in mail.FilesToAttach)
                //    {
                //        if (!string.IsNullOrEmpty(fileName))
                //            _mailMessage.Attachments.Add(new Attachment(fileName));
                //    }
                //}
                //_log.Info(m => m("Mail Sender -  Files attachment completed"));
                if (mail.IsCredentialRequired)
                {
                    smtpServer.Credentials = new System.Net.NetworkCredential(mail.AuthUserName, mail.AuthPassword);
                }
                smtpServer.EnableSsl = mail.IsEnableSSL;
                smtpServer.Send(_mailMessage);
                _log.Info(m => m("Mail sent successfully."));
                return true;

            }

            catch (Exception ex)
            {
                //if there is any error then put it into the log file...
                _log.Error(m => m("Mail Sender exception message - {0}", ex.Message));
                _log.Error(m => m("Mail Sender exception stack trace - {0}", ex.StackTrace));
                return false;
            }
        }

    }

    
}