using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
                var FromAddress = " admin@loyabconsulting.com"; // (message.systemaddress == null | message.systemaddress.emailaddress == null | message.systemaddress.emailaddress == "") ? "MISReporting@wellsfargo.com" : message.systemaddress.emailaddress;

              
               //     // Create the email object first, then add the properties.
               // string username = ConfigurationManager.AppSettings["SENDGRID_USER"];
               // string password = ConfigurationManager.AppSettings["SENDGRID_PASS"];

               // // Create the email object first, then add the properties.
               // SendGridMessage myMessage = new SendGridMessage();
               // myMessage.AddTo(emailaddress);
               // myMessage.From = new MailAddress(FromAddress, "Do Not Reply");
               // myMessage.Subject = subject;// "Testing the SendGrid Library";
               // myMessage.Text = body; //"Hello World!";

               // // Create credentials, specifying your user name and password.
               // var credentials = new NetworkCredential(username, password);

               // // Create an Web transport for sending email, using credentials...
               // var transportWeb = new Web(credentials);

               // // ...OR create a Web transport, using API Key (preferred)
               //// var transportWeb = new Web("This string is an API key");

               // // Send the email.
               // transportWeb.DeliverAsync(myMessage);


               //     MailMessage mailMsg = new MailMessage();




                SmtpClient client = new SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                client.Port = 25;


                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(FromAddress, emailaddress);
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
               // SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SENDGRID_HOST"]);
                //using GO Daddy btw from address should be a godaddy address too
                //var smtp = new SmtpClient("relay-hosting.secureserver.net");

                /// http://stackoverflow.com/questions/8554567/godaddy-send-email
                /// 
                //string host = ConfigurationManager.AppSettings["SENDGRID_HOST"];
                //smtp.Credentials()
                
                    //Setup credentials to login to our sender email address ("UserName", "Password")
                    client.UseDefaultCredentials = false;
                    NetworkCredential credentials = new NetworkCredential("admin@loyabconsulting.com ", "kayode02");
                    client.Credentials = credentials;

                    client.Host = "smtpout.secureserver.net";
 



              

                client.Send(mailMessage);
                
                
             //   smtp.Send(mailMessage);
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