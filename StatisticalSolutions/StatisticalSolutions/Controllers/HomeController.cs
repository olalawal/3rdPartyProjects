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

namespace StatisticalSolutions.Controllers
{
    public class HomeController : Controller 
    {

        ILog Log = LogManager.GetCurrentClassLogger();
        DAL dataAccess = new DAL();
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
            catch (Exception e)
            {
                //code for exception handling                
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
                        
                    //code for insertion of student in student table               
                    Log.Info("Function RegisterWorkshop - Insertion Student table");					
                    model.student_id=dataAccess.addstudent(model.student);				
				
                    // code for insert record in registration table                
                    Log.Info("Function RegisterWorkshop - Insetion in regastration tablr ");              
                    dataAccess.registerforseminarbystudentandseminarid(model);
                    //get seminar by seminar_id 
                    model.seminar = dataAccess.getseminarbyid(model.seminar_id);

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
                   
                catch (Exception ex)
                {
                    Log.Error(m => m("Function Register Error  - {0}", ex.Message));
                   //code for exception handling
                }
                return RedirectToAction("register");
                
        }


        public ActionResult RegisterComplete(registration registration)
        {
            ViewBag.Message = "Register Complete";

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
                    MailSender _maillSender = new MailSender();
                    Mails mail = _maillSender.SetMailsProperty();
                    mail.Name = model.Name;
                    mail.Body = model.Body;
                    mail.From = model.Email;
                    mail.To = ConfigurationManager.AppSettings["EmailTo"];
                    mail.Subject = model.Name + " has sent contact request";
                    Log.Info("Function ContactUsMail - Start of mail sending");
                   _maillSender.SendMail(mail);

                 //code for insert message in database
                   Log.Info("Function ContactUsMail - Insertion of message in table");
                   dataAccess.addcontactmessage(model);                     
                }

                catch (Exception ex)
                {
                    Log.Error(m => m("Function SendMail Error  - {0}", ex.Message));
                   //code fot exception handling
                }         
            
            return RedirectToAction("Index", "Home");
        }


   

    }
}
