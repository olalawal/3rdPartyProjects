using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace StatisticalSolutions.Helpers
{
    public static class MailExtention
    {

            //Private reusable internal functions 
        //TO DO this should be handled as a separate send for each so we can update the susccess individually
        //Private reusable internal functions  
        //TO DO this should be handled as a separate send for each so we can update the susccess individually
        public static bool sendemail(string emailaddress,string subject,string body)
        {
            bool isEmailSendSuccessfully = false;


            try
            {
                //SmtpClient oSmtpClient = new SmtpClient();
                //MailMessage oMailMessage = new MailMessage();
                var FromAddress = "noreply@loyabconsulting.com"; // (message.systemaddress == null | message.systemaddress.emailaddress == null | message.systemaddress.emailaddress == "") ? "MISReporting@wellsfargo.com" : message.systemaddress.emailaddress;

               
                    // Create the email object first, then add the properties.
                    var myMessage = new SendGridMessage();

                
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(FromAddress,emailaddress);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                   // SmtpClient smtp = new SmtpClient();
                    //using GO Daddy btw from address should be a godaddy address too
                    var smtp = new SmtpClient("relay-hosting.secureserver.net");
                   /// http://stackoverflow.com/questions/8554567/godaddy-send-email
                   /// 
                    smtp.Host = ConfigurationManager.AppSettings["SENDGRID_HOST"];
                    //smtp.Credentials()
                    //TO DO no credentials required i think
                    smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SENDGRID_USER"], ConfigurationManager.AppSettings["SENDGRID_PASS"]);
                    smtp.Send(mailMessage);
                    isEmailSendSuccessfully = true;
                

                isEmailSendSuccessfully = true;
            }
            catch (Exception ex)
            {
                //TO DO log this
                string ErrorMessage = ex.Message;
                isEmailSendSuccessfully = false;

                return isEmailSendSuccessfully;
            }

            return isEmailSendSuccessfully;
        }

    }
}