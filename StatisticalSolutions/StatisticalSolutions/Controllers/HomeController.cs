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
                  ViewBag.Seminars = dataAccess.getseminars();


                // code for start dates 
                 ViewBag.StartDates = dataAccess.getfutureseminarsstartdate(); ;


                //code to fill country in dropdown
                 ViewBag.Countries = dataAccess.getCountries(); ;

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
                //List<client> clients = new List<client>();
                //clients = dataAccess.getCompanies();
                foreach (client c in dataAccess.getCompanies())
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
                  

                  //  if (_maillSender.SendMail(mail))
                    if (MailExtention.sendemail(mail.To,mail.Subject,mail.Body))
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


        #region workshop


        /// <summary>
        /// Workshop details
        /// </summary>
        /// <param name="seminar_id"></param>
        /// <returns></returns>
        [HttpGet]       
        public ActionResult WorkshopDetails(int seminar_id) 
        {
            try
            {
                seminar seminar = dataAccess.getseminarbyid(seminar_id);
                return View(seminar);
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
        /// Add Workshop
        /// </summary>
        /// <returns></returns>
        public ActionResult AddWorkshop()
        {  
            try 
            {
                ViewBag.Countries = dataAccess.getCountries();
                return View();
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddWorkshop Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddWorkshop Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }
            
        }

        /// <summary>
        /// post action of Add workshop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult AddWorkshop(seminar model)
        {

            try
            {
                model.IsActive = true;
                dataAccess.addseminar(model);
                return RedirectToAction("WorkshopList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddWorkshop Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddWorkshop Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

            
        }


        /// <summary>
        /// render workshop lists
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkshopList() 
        {
            try
            {
                List<seminar> seminarList = dataAccess.getseminars();
                return View(seminarList);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function WorkshopList Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function WorkshopList Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        /// <summary>
        /// get action of edit workshop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>     
        public ActionResult EditWorkshop(int id)     
        {
            try
            {
                
                seminar seminar = dataAccess.getseminarbyid(id);
                
                ViewBag.Seminars = dataAccess.getseminars();

                ViewBag.StartDates = dataAccess.getfutureseminarsstartdate(); ;
                             
                ViewBag.Countries = dataAccess.getCountries(); 

                //code which fetch list of companies for autocomplete
                List<string> companies = new List<string>();
               
                foreach (client c in dataAccess.getCompanies())
                    companies.Add(c.Name);

                ViewBag.Companies = companies;  
                return View(seminar);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Seminar Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Seminar Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        /// <summary>
        /// Post action of edit workshop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,  ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditWorkshop(seminar model) 
        {

            try
            {
                dataAccess.updateseminar(model);
                return RedirectToAction("WorkshopList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function EditWorkshop Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function EditWorkshop Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }
        }

        /// <summary>
        /// get action of edit workshop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>     
        public ActionResult DeleteWorkshop(int id)
        {
            try
            {
                dataAccess.deleteseminar(id);
                return RedirectToAction("WorkshopList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function DeleteWorkshop Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function DeleteWorkshop Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        #endregion


        #region Students Actions

        /// <summary>
        /// get list of students
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentList() 
        {
            try
            {
                List<student> students = dataAccess.getstudents();
                return View(students);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function StudentList Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function StudentList Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }

           
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(student model)
        {
            try
            {
                model.IsActive = true;
                dataAccess.addstudent(model);

                return RedirectToAction("StudentList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddStudent Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddStudent Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


        public ActionResult EditStudent(int id)
        {
            try
            {
                student student = dataAccess.getstudentbyid(id);
                ViewBag.Countries = dataAccess.getCountries();

                return View("EditStudent", student);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function EditStudent Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function EditStudent Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(student model) 
        {
            try
            {
                dataAccess.updatestudent(model);

                return RedirectToAction("StudentList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddStudent Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddStudent Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


        public ActionResult DeleteStudent(int id)  
        {
            try
            {
                dataAccess.deletestudent(id);

                return RedirectToAction("StudentList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function DeleteStudent Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function DeleteStudent Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }
        }

        #endregion

        #region Bulk Mails Actions

        public ActionResult BulkMails() 
        {
            try
            {
                ViewBag.Filter = getFilterList();

                ViewBag.regseminars = dataAccess.getregisteredseminars();  
                return View();
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function BulkMails Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function BulkMails Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }
           

        /// <summary>
        /// contrrlller  for sending bulk mails
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BulkMails(registration model)
        {
            try
            {
                ViewBag.Filter = getFilterList();

                MailSender _maillSender = new MailSender();
                Mails mail = _maillSender.SetMailsProperty();

                seminar seminar = dataAccess.getseminarbyid(model.seminar_id); 
                
                //get list of students registered for a particular seminar
                List<student> students = dataAccess.getseminarregisteredstudents(model.seminar_id);

                foreach (student st in students)
                {

                    mail.Name = st.FirstName + " " + st.LastName;
                    mail.Body = "Hi " + st.FirstName + ", <br/><br/> You have registered for workshop " + seminar.TitleHtml + " starting from " + seminar.StartDate + " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team";
                    mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                    mail.To = st.Email;
                    mail.CC = ConfigurationManager.AppSettings["AdminEmail"];
                    mail.Subject = "You have registered for " + seminar.TitleHtml;
                   // _maillSender.SendMail(mail);
                    MailExtention.sendemail(mail.To, mail.Subject, mail.Body);  //using sendmail
                    Log.Info("Function BulkMails - End of mail sending");
                }
                return View("BulkMailSent");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function BulkMails Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function BulkMails Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        [HttpPost]
        public ActionResult BulkMailsFilter(string FilterText)
        {
            try
            {

                ViewBag.Filter = getFilterList();

                ViewBag.regseminars = dataAccess.getfilterregisteredseminars(FilterText);
                return View("BulkMails");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function BulkMails Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function BulkMails Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }
        #endregion


        # region Clients Actions


        /// <summary>
        /// Client List
        /// </summary>
        /// <returns></returns>
        public ActionResult ClientsList()
        { 
            try
            {
                List<client> companies = dataAccess.getCompanies();
                return View(companies);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function ClientsList Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function ClientsList Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }
         
    
       

        /// <summary>
        /// Add company
        /// </summary>
        /// <returns></returns>
        public ActionResult AddClient() 
        {
            try
            {
                ViewBag.Countries = dataAccess.getCountries();
                return View();
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddClient Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddClient Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }

        /// <summary>
        /// post action of  company
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClient(client model) 
        {
            try
            {
                model.IsActive = true;
                dataAccess.addclient(model);
                return RedirectToAction("ClientsList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddClient Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddClient Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }

        /// <summary>
        /// edit client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditClient(int id)
        {
            try
            {
                client client = dataAccess.getcompaniesbyid(id);
                ViewBag.Countries = dataAccess.getCountries();

                return View(client);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function SaveClient Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function SaveClient Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient(client model)
        {

            try
            {
                dataAccess.updateclient(model);
                return RedirectToAction("ClientsList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddClient Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddClient Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }

       
        public ActionResult DeleteClient(int id)
        {
            try
            {
                dataAccess.deleteclient(id);

                return RedirectToAction("ClientsList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function DeleteClient Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function DeleteClient Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }
        }

        #endregion


        public string[] getFilterList()
        {
            string[] filters = new string[] { "Paid", "Unpaid", "Attended", "Not Attended" };
            return filters;
        }
    }
}
