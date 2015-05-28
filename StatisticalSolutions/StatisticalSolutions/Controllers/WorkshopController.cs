using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Common.Logging;
using System.Web.Hosting;
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
    public class WorkshopController : BaseController
    {

         #region variables
        DAL dataAccess;
       // MailSender _maillSender;
        #endregion

        #region constructor

        public WorkshopController()
        {
            dataAccess = new DAL();
           // _maillSender = new MailSender();
                
        }
        #endregion

       

        public ActionResult WorkShops()
        {
            //get the list of seminars
            ViewBag.Seminars = dataAccess.getfutureseminars();
            
            ViewBag.Seminar_Instructors = dataAccess.getseminarinstructors();

            ViewBag.Instructors = dataAccess.getinstructors();

            ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;

            return View();
        }

        /// <summary>
        /// Workshop details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Workshop(int id)  
        {
            try
            {
                //get the list of seminars            
                ViewBag.Seminars = dataAccess.getfutureseminars();

                IQueryable<SeminarInstructorModel> seminarInstructor = dataAccess.getseminarinstructor(id);

                ViewBag.Seminar = dataAccess.getseminarbyid(id);

                ViewBag.Consultant = seminarInstructor.AsEnumerable().FirstOrDefault().Instructor.Name;

                return View("Workshops");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Workshop Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Workshop Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


        /// <summary>
        /// seminar registration
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            try
            {               
                //code for fill value in workshop dropdown 
                ViewBag.Seminars = dataAccess.getfutureseminars();

                registration model = new registration();
               
                //// code for start dates 
               // ViewBag.StartDates = GetDisplayDates(new List<seminar>());

              //  model.StartDates = GetDisplayDates(new List<seminar>()); ;
             
                

                //code to fill country in dropdown
                ViewBag.Countries = dataAccess.getCountries(); ;
               

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
                
                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                ViewBag.Companies = companies;

                ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;


                model.Starttime = "Select a Seminar to see Date";
                return View(model);               
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

           
        }



        /// <summary>
        /// Seminar registration by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>  
        [HttpPost]
        public ActionResult Registration(string id)  
        {
            try
            {
                //code for fill value in workshop dropdown 
                ViewBag.Seminars = dataAccess.getfutureseminars();

                registration model = new registration();
                model.seminar_id = Convert.ToInt32(id);

                model.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));
                ViewBag.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));

                ViewBag.SingleStatDate = model.StartDates.FirstOrDefault();
           
             
                seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
                ViewBag.StartTime = seminar.Starttime;
                model.Starttime = seminar.Starttime;         
                ViewBag.EndTime = seminar.Endtime;
                model.Endtime = seminar.Endtime;

                    var formatteddatetime  = model.StartDates.FirstOrDefault().LongDate +  " " + seminar.Starttime + " to " +seminar.Endtime ;


                //code to fill country in dropdown
                ViewBag.Countries = dataAccess.getCountries(); ;

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();

                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                ViewBag.Companies = companies;

                ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;

                TempData.Keep();


                return Content(formatteddatetime);

              //  return View("Register", model);

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


        }


        /// <summary>
        /// Register Seminar by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult RegisterNow(string name)   
        {
            try
            {
                
                registration model = new Models.registration();

                //get seminar id from database based on seminar name
                model.seminar_id = dataAccess.getseminaridbyname(name);


                //code for fill value in workshop dropdown 
                ViewBag.Seminars = dataAccess.getseminars();

                seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
                model.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));
                ViewBag.StartTime = seminar.Starttime;
                ViewBag.EndTime = seminar.Endtime;

                //// code for start dates 
                ViewBag.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));


                //code to fill country in dropdown
                ViewBag.Countries = dataAccess.getCountries(); ;

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
             
                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                ViewBag.Companies = companies;

                ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;

                model.Starttime = model.StartDates.FirstOrDefault().LongDate + " " + seminar.Starttime + " to " + seminar.Endtime;

                return View("Register", model);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function RegisterNow Error  - {0}", ex.Message));
                //throw ex;
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function RegisterNow Error  - {0}", ex.Message));
                //throw ex;
                return SystemExceptionCatcher(ex);
            }

           
        } 

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(registration model)
        {

            try
            {
                if (!dataAccess.CheckIfStudentIsRegistereredForSeminar(model.student, model.seminar_id))
                {
                    seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
                    model.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));
                    student student = new student();
                    student = model.student;
                    // code for insert record in registration table                
                    Log.Info("Function Register - Insetion in regastration tablr ");
                    dataAccess.registerforseminarbystudentandseminarid(model);

                    //code for sending mail to registratant and admin
                    Log.Info("Function Register - Start of mail sending");
                    MailSender _maillSender = new MailSender();
                    Mails mail = _maillSender.SetMailsProperty();
                    mail.Name = model.student.FirstName + " " + model.student.LastName;

                   
                    mail.Body = "Hi " + model.student.FirstName + ", <br/><br/> You have successfully registered for workshop " + model.seminar.TitleHtml + " starting from " 
                        +seminar.StartDate.ToShortDateString() + "to" + seminar.Enddate.ToShortDateString() + ":" + seminar.Starttime +  "-" + seminar.Endtime +  " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team"

                        + " <br/><br/>please make your  make your payment of + " + seminar.EarlyBirdPrice + " to :  <b>GTB</b> bank AC/#: <b>0171482631<b></b></span>"
                        ;
                    mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                    mail.To = model.student.Email;
                    //mail.CC = ConfigurationManager.AppSettings["AdminEmail"];
                    mail.Subject = "You have successfully registered for " + model.seminar.TitleHtml;
                    MailExtention.sendemail(model.student.Email, mail.Subject, mail.Body);
                    // _maillSender.SendMail(mail);
                    Log.Info("Function Register - End of mail sending");

                    //send message to admin as well
                    
                    mail.Body =  model.student.FirstName + ", has registered for the workshop " + model.seminar.TitleHtml + " starting from " + model.StartDate + " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team";
                    mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                    mail.To = ConfigurationManager.AppSettings["EmailTo"];                  
                    mail.Subject = "A new student has registered for the seminar :" + model.seminar.TitleHtml;
                    MailExtention.sendemail(mail.To, mail.Subject, mail.Body);


                    return View("RegisterComplete", model);
                }
                else
                {
                 seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
                 model.seminar = seminar;
                 // model.student = dataAccess.getstudentbyid(registration.student_id);
                  return View("RegisteredAlready", model);
                
                
                }


            }

            catch (CustomException ex)
            {
                Log.Error(m => m("Function Register Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Register Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }
            return RedirectToAction("register", new registration());

        }


        public ActionResult RegisterComplete(registration registration)
        {
            ViewBag.Message = "Register Complete, please check your spam folders if you do not see your registration email";
            registration.student = dataAccess.getstudentbyid(registration.student_id);
            return View(registration);
        }

 


        public List<DisplayDateTime> GetDisplayDates(List<seminar> seminars)
        {
            var displayDates = new List<DisplayDateTime>();
            foreach (seminar seminar in seminars)
            {
                displayDates.Add(new DisplayDateTime(seminar.StartDate));
            }

            return displayDates;
        }
    }
}
