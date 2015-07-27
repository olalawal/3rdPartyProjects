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

            WorkshopInstructorViewModel model = new WorkshopInstructorViewModel();

            //get list of seminars
            List<seminar> seminars = dataAccess.getfutureseminars();  
            model.Seminars= seminars;
            
            //get list of instructors
            List<instructor> instructors = dataAccess.getinstructors();
            model.Instructors = instructors;  

            return View(model);
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
                WorkshopInstructorViewModel model = new WorkshopInstructorViewModel();

                //get the list of seminars  from tempdata other wise get from database    
                model.Seminars = dataAccess.getfutureseminars();

                //get the list of instructors  from tempdata other wise get from database    
                model.Instructors = dataAccess.getinstructors();

                //get seminar by id
                model.Seminar = dataAccess.getseminarbyid(id);
               
                return View("Workshops", model);
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
        /// Workshop details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WorkshopDetails(string id)
        {
            try
            {
                int seminar_id = Convert.ToInt32(id);

                //get seminar by id
                seminar seminar = dataAccess.getseminarbyid(seminar_id);

                return PartialView("_SeminarDetailsPartial", seminar);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function WorkshopDetails Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function WorkshopDetails Error  - {0}", ex.Message));
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
                RegistrationViewModel model = new RegistrationViewModel();
               

                //code for fill value in workshop dropdown 
              
                model.Seminars = dataAccess.getfutureseminars(); ;

                //code to fill country in dropdown
                model.Countries = ListClass.CountryList;
               
                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
                
                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                model.Companies = companies;
             
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
                RegistrationViewModel model = new RegistrationViewModel();
                model.seminar_id = Convert.ToInt32(id);

                //model.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));                

                seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
                DisplayDateTime displayDateTime = new DisplayDateTime(seminar.StartDate);

                var formatteddatetime =  displayDateTime.LongDate + (!string.IsNullOrEmpty(seminar.Starttime) ? (" " + seminar.Starttime) : "") + ((!string.IsNullOrEmpty(seminar.Starttime) ? (" to " + seminar.Endtime) : ""));


                return Content(formatteddatetime);

            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Registration Error  - {0}", ex.Message));
                //throw ex;
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Registration Error  - {0}", ex.Message));
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

                RegistrationViewModel model = new RegistrationViewModel();
           

                

                //code for fill value in workshop dropdown 
                model.Seminars = dataAccess.getseminars().ToList();

                //find a seminar with name matching or clsoe to passed in
                var dd = model.Seminars.Where(z=>z.TitleHtml.Contains(name)).FirstOrDefault();
                if (dd == null) {
                    model.seminar_id = model.Seminars.FirstOrDefault().seminar_id; 
                }
                else
                {
                    model.seminar_id = dd.seminar_id;
                }



                seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
                //model.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));
         




                //code to fill country in dropdown
                model.Countries = ListClass.CountryList;

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
             
                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                model.Companies = companies;


                DisplayDateTime displayDateTime = new DisplayDateTime(seminar.StartDate);

                model.Starttime = displayDateTime.LongDate + (!string.IsNullOrEmpty(seminar.Starttime) ? (" " + seminar.Starttime) : "") + ((!string.IsNullOrEmpty(seminar.Starttime) ? (" to " + seminar.Endtime) : ""));

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

        /// <summary>
        /// Register Seminar by id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RegisterSeminar(int id) 
        {
            try
            {

                RegistrationViewModel model = new RegistrationViewModel();

              
                model.seminar_id = id;
               
                //code for fill value in workshop dropdown 
                model.Seminars = dataAccess.getseminars();

                seminar seminar = dataAccess.getseminarbyid(id);

                //model.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(model.seminar_id));


                //code to fill country in dropdown
                model.Countries = ListClass.CountryList;

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();

                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                model.Companies = companies;

                DisplayDateTime displayDateTime = new DisplayDateTime(seminar.StartDate);

                model.Starttime = displayDateTime.LongDate + (!string.IsNullOrEmpty(seminar.Starttime) ? (" " + seminar.Starttime) : "") + ((!string.IsNullOrEmpty(seminar.Starttime) ? (" to " + seminar.Endtime) : ""));


                return View("Register", model);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function RegisterSeminar Error  - {0}", ex.Message));
                //throw ex;
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function RegisterSeminar Error  - {0}", ex.Message));
                //throw ex;
                return SystemExceptionCatcher(ex);
            }


        }  


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegistrationViewModel model)
        {

            try
            {
                registration registration = new registration();
                seminar seminar = new seminar();                
                student student = new student();

                student.FirstName = model.FirstName.Trim();
                student.LastName = model.LastName.Trim();
                student.Email = model.Email.Trim();
                student.Phone = model.Phone.Trim();

                seminar = dataAccess.getseminarbyid(model.seminar_id);
                registration.seminar_id = model.seminar_id;             

                              
                registration.student = student;

                registration.StartDate = seminar.StartDate;
                registration.Starttime = model.Starttime;

                registration.client = new client();
                registration.client.Name = model.ClientName;

                if (!dataAccess.CheckIfStudentIsRegistereredForSeminar(student, model.seminar_id))
                {   

                    // code for insert record in registration table                
                    Log.Info("Function Register - Insetion in regastration tablr ");
                    dataAccess.registerforseminarbystudentandseminarid(registration);

                    registration.seminar = seminar;   

                    //code for sending mail to registratant and admin
                    Log.Info("Function Register - Start of mail sending");
                    MailSender _maillSender = new MailSender();
                    Mails mail = _maillSender.SetMailsProperty();
                    mail.Name = model.FirstName + " " + model.LastName;


                    mail.Body = "Hi " + student.FirstName + ", <br/><br/> You have successfully registered for workshop " + seminar.TitleHtml + " starting from "
                        + seminar.StartDate.ToShortDateString() + ((seminar.Enddate!=null && seminar.Enddate!=default(DateTime))? ("to" + seminar.Enddate.ToShortDateString()):"") + " : " + seminar.Starttime + " To " + seminar.Endtime + " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team"

                        + " <br/><br/>please make your  make your payment of :" + seminar.EarlyBirdPrice + " to :  <b>GTB</b> bank AC/#: <b>0171482631<b></b></span>"
                        ;
                    mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                    mail.To = student.Email;
                    //mail.CC = ConfigurationManager.AppSettings["AdminEmail"];
                    mail.Subject = "You have successfully registered for " + seminar.TitleHtml;
                    MailExtention.sendemail(student.Email, mail.Subject, mail.Body);
                    // _maillSender.SendMail(mail);
                    Log.Info("Function Register - End of mail sending");

                    //send message to admin as well

                    mail.Body = student.FirstName + ", has registered for the workshop " + seminar.TitleHtml + " starting from " + seminar.StartDate.ToShortDateString() + ((seminar.Enddate != null && seminar.Enddate != default(DateTime)) ? ("to" + seminar.Enddate.ToShortDateString()) : "") + " : " + seminar.Starttime + " To " + seminar.Endtime + " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team";
                    mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                    mail.To = ConfigurationManager.AppSettings["EmailTo"];
                    mail.Subject = "A new student has registered for the seminar :" + seminar.TitleHtml;
                    MailExtention.sendemail(mail.To, mail.Subject, mail.Body);


                    return View("RegisterComplete", registration);
                }
                else
                {
                    registration.seminar = seminar;  
                    // model.student = dataAccess.getstudentbyid(registration.student_id);
                    return View("RegisteredAlready", registration);               
                
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
         

        }

        /// <summary>
        /// get action of registration complete
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        public ActionResult RegisterComplete(registration registration)
        {
            ViewBag.Message = "Register Complete, please check your spam folders if you do not see your registration email";
            registration.student = dataAccess.getstudentbyid(registration.student_id);
            return View(registration);
        }

 

        /// <summary>
        /// Method which returns as start dates 
        /// </summary>
        /// <param name="seminars"></param>
        /// <returns></returns>
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
