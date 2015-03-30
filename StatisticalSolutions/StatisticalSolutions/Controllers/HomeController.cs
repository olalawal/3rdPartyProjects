using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Common.Logging;
using System.Configuration;
using StatisticalSolutions.Models;
using StatisticalSolutions.ViewModels;
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

        public ActionResult Consultants()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult WorkShops()
        {
            ViewBag.Message = "Your contact page.";

            //get the list of seminars
            List<seminar> seminars = dataAccess.getseminars();
            return View(seminars);
        }

       

        public ActionResult Register(registration model)
        {
            try
            {
                ViewBag.Message = "Register Now";


                //code for fill value in workshop dropdown 
                List<seminar> seminars = new List<seminar>();
                seminars = dataAccess.getseminars();
                ViewBag.Seminars = seminars;


                // code for start dates 
                List<seminar> StartDates = new List<seminar>();
                StartDates = dataAccess.getfuturesemnarsstartdate();
                ViewBag.StartDates = StartDates;


                //code to fill country in dropdown
                List<Countries> countries = new List<Countries>();
                countries = dataAccess.getCountries();
                ViewBag.Countries = countries;

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
                List<client> clients = new List<client>();
                clients = dataAccess.getCompanies();
                foreach (client c in clients)
                    companies.Add(c.Name);

                ViewBag.Companies = companies;             
               
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Register Error  - {0}", ex.Message));
                //throw ex;
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Register Error  - {0}", ex.Message));
                //throw ex;
                return SystemExceptionCatcher(ex);
            }   

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterWorkshop(registration model) 
        {           
                
            try                
            {                    
                if (!dataAccess.CheckIfStudentIsRegistereredForSeminar(model.student, model.seminar_id))
                {
                    student  student = new student();
                    student = model.student;
                    // code for insert record in registration table                
                    Log.Info("Function RegisterWorkshop - Insetion in regastration tablr ");
                    dataAccess.registerforseminarbystudentandseminarid(model);                

                    //code for sending mail to registratant and admin
                    Log.Info("Function RegisterWorkshop - Start of mail sending");
                    MailSender _maillSender = new MailSender();
                    Mails mail = _maillSender.SetMailsProperty();
                    mail.Name = model.student.FirstName + " " + model.student.LastName;
                    mail.Body = "Hi " + model.student.FirstName + ", <br/><br/> You have successfully registered for workshop " + model.seminar.TitleHtml + " starting from " + model.StartDate + " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team";
                    mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                    mail.To = model.student.Email;
                    mail.CC = ConfigurationManager.AppSettings["AdminEmail"];
                    mail.Subject = "You have successfully registered for " + model.seminar.TitleHtml;
                    _maillSender.SendMail(mail);
                    Log.Info("Function RegisterWorkshop - End of mail sending");
                    
                    return View("RegisterComplete", model);
                    }				
                }

                catch (CustomException ex)
                {
                    Log.Error(m => m("Function RegisterWorkshop Error  - {0}", ex.Message));
                    return CustomExceptionCatcher(ex);
                }
                catch (Exception ex)
                {
                    Log.Error(m => m("Function RegisterWorkshop Error  - {0}", ex.Message));                    
                    return SystemExceptionCatcher(ex);
                }   
                return RedirectToAction("register",new registration());
                
        }


        public ActionResult RegisterComplete(registration registration)
        {
            ViewBag.Message = "Register Complete";
            registration.student = dataAccess.getstudentbyid(registration.student_id);
            return View(registration);
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
                  
                    if (_maillSender.SendMail(mail))
                    { 
                        //code for insert message in database
                        Log.Info("Function ContactUsMail - Insertion of message in table");
                        dataAccess.addcontactmessage(model);
                        TempData["StatisticalError"] = "Message sent successfully";
                    }
                    else
                    {
                        TempData["StatisticalError"] = "Error in sending mail";
                    }
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

             TempData.Keep();
             return RedirectToAction("index", "Home"); //View("~/Views/Home/index.cshtml", model);
        }


   

    }
}
