using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Common.Logging;
using System.Configuration;
using StatisticalSolutions.Models;
using StatisticalSolutions.DataAccess;
using StatisticalSolutions.Util;
using StatisticalSolutions.Filters;
using StatisticalSolutions.Helpers;
using StatisticalSolutions.Controllers.Base;

namespace StatisticalSolutions.Controllers
{
 
    public class HomeController : BaseController
    {
        #region variables
        DAL dataAccess;
        MailSender _maillSender;
        #endregion

        #region constructor

        public HomeController()
        {
            dataAccess = new DAL();
            _maillSender = new MailSender();
                
        }
        #endregion


        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Statistical Solutions";
            
            return View();
        }

        public ActionResult IndexNew()
        {
            ViewBag.Message = "Welcome to Statistical Solutions";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
       

     

        //bios 

        public ActionResult bioBayoLawal()
        {
            ViewBag.Message = "Professor Bayo Lawal";

            return View();
        }


        public ActionResult bioFelixFamoye()
        {
            ViewBag.Message = "Professor Felix Famoye";

            return View();
        }

        /// <summary>
        ///  code for contact us mail
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUsMail(message model)     
        {
             try
                {
                    //code for sending mails                    
                    Mails mail = _maillSender.SetMailsProperty();
                    mail.Name = model.Name;
                    mail.Body = model.Body;
                    mail.From = model.Email;
                    mail.To = ConfigurationManager.AppSettings["EmailTo"];

                    mail.Subject = model.Name + " has sent contact request";
                    model.Subject= "Contact Us Request";                   
                    Log.Info("Function ContactUsMail - Start of mail sending");
                  

                  //  if (_maillSender.SendMail(mail))
                    if (MailExtention.sendemail(mail.To, mail.Subject, mail.Body))
                    {
                        //code for insert message in database
                        Log.Info("Function ContactUsMail - Insertion of message in table");
                        dataAccess.addcontactmessage(model);
                    }                     
                    

                   //send message to the sender as well

                    //code for sending mails                    
                 
                    mail.Name = model.Name;
                    mail.Body = "please allow 24 hours for us to reply to your request";
                  //  mail.From = model.Email;
                    mail.To = model.Email;

                    mail.Subject = model.Name + " your contact request has been sent";
                    model.Subject = "Contact Us Request";
                    //Log.Info("Function ContactUsMail - Start of mail sending");


                    //  if (_maillSender.SendMail(mail))
                    if (MailExtention.sendemail(mail.To, mail.Subject, mail.Body))
                    {
                        //code for insert message in database
                        Log.Info("Function ContactUsMail - Insertion of message in table");
                        dataAccess.addcontactmessage(model);
                       
                    }


                    TempData["StatisticalError"] = "Message sent successfully";
                    TempData.Keep();
                    return RedirectToAction("index", "Home"); //View("~/Views/Home/index.cshtml", model);

                }
             catch (CustomException ex)
             {
                 Log.Error(m => m("Function ContactUsMail Error  - {0}", ex.Message));
                 return CustomExceptionCatcher(ex);
             }
             catch (Exception ex)
             {
                 Log.Error(m => m("Function ContactUsMail Error  - {0}", ex.Message));
                 return SystemExceptionCatcher(ex);
             }
            
        }     

        
    }
}
