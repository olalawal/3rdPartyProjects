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
    [Authorize]
    public class AdminController : BaseController
    {
        #region variables
        DAL dataAccess;
        
        #endregion

        #region constructor

        public AdminController()
        {
            dataAccess = new DAL();          
                
        }
        #endregion

        #region workshops

     
        /// <summary>
        /// Add Workshop
        /// </summary>
        /// <returns></returns>
        public ActionResult AddWorkshop()
        {
            try
            {
                ViewBag.Countries = dataAccess.getCountries();

                ViewBag.Instructors = dataAccess.getinstructors();  

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

                ViewBag.StartDates = GetDisplayDates(dataAccess.getfutureseminarsstartdate(id));

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
        [HttpPost, ValidateInput(false)]
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
        /// get action of delete workshop
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

        #region Consultants


        /// <summary>
        /// Add Consultant
        /// </summary>
        /// <returns></returns>
        public ActionResult AddConsultant()  
        {
            try
            {
                ViewBag.Seminars = dataAccess.getseminars();

                ViewBag.Countries = dataAccess.getCountries();
                return View();
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddConsultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddConsultant Error  - {0}", ex.Message));
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
        public ActionResult AddConsultant(instructor model, HttpPostedFileBase file)
        {

            try
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/images/consultants"), fileName);
                // file is uploaded
                file.SaveAs(path);
                model.ImageName = file.FileName;
                model.ImagePath = "/images/consultants/" + fileName;
                model.IsActive = true;

                dataAccess.addInstructor(model);

                return RedirectToAction("ConsultantList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AddConsultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AddConsultant Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


        /// <summary>
        /// render Consultant lists
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsultantList()
        {
            try
            {
                ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;

                // List<instructor> instructorList = dataAccess.getinstructors();

                List<SeminarInstructorModel> seminarIns = dataAccess.getseminarinstructors();

                return View(seminarIns);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function ConsultantList Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function ConsultantList Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        /// <summary>
        /// get action of edit workshop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>     
        public ActionResult EditConsultant(int id)
        {
            try
            {

                instructor instructor = dataAccess.getinstructorbyid(id);

                //ViewBag.Instructors = dataAccess.getinstructors();

                ViewBag.Seminars = dataAccess.getseminars();

                ViewBag.Countries = dataAccess.getCountries();

               
                return View(instructor); 
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function EditConsultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function EditConsultant Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        /// <summary>
        /// Post action of edit Consultant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditConsultant(instructor model, HttpPostedFileBase file)
        {
            try
            {
                string fileName = System.IO.Path.GetFileName(file.FileName); 
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/consultants"), fileName);
                // file is uploaded
                file.SaveAs(path);
                model.ImageName = file.FileName;
                model.ImagePath = "/images/consultants/" + fileName;
                dataAccess.updateInstructor(model);
                return RedirectToAction("ConsultantList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function EditConsultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function EditConsultant Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }
        }

        /// <summary>
        /// get action of delete Consultant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>     
        public ActionResult DeleteConsultant(int id) 
        {
            try
            {
                dataAccess.deleteInstructor(id);
                return RedirectToAction("ConsultantList");
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function DeleteConsultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function DeleteConsultant Error  - {0}", ex.Message));
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


        #region Bulk Mails Actions

        public ActionResult BulkMails()
        {
            try
            {
                BulkMailModel bulkMailModel = new BulkMailModel();
                bulkMailModel.Students =new List<student>();
                bulkMailModel.Registration = new registration();

               
                ViewBag.Filter = getFilterList();


                ViewBag.regseminars = dataAccess.getregisteredseminars();

                return View(bulkMailModel);
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
        public ActionResult BulkMails(BulkMailModel model)  
        {
            try
            {
                MailSender _maillSender = new MailSender();
                Mails mail = _maillSender.SetMailsProperty();

                seminar seminar = dataAccess.getseminarbyid(model.Registration.seminar_id);

                //get list of students registered for a particular seminar
                List<student> students = dataAccess.getseminarregisteredstudents(model.Registration.seminar_id);

                if (students.Any())
                {
                      foreach (student st in students)
                    {
                        if(st.IsActive)
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
            
                    }
                }

                ViewBag.MailCount = students.Count; ;
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


        /// <summary>
        /// contrrlller  for sending bulk mails
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BulkMailStudents(int id)     
        {
            try
            {
                BulkMailModel model = new BulkMailModel();
             
                ViewBag.Filter = getFilterList();
                string filtertext = TempData["filteredText"] as string;
                if(!string.IsNullOrEmpty(filtertext))                
                    ViewBag.regseminars = dataAccess.getfilterregisteredseminars(filtertext);              
                else
                    ViewBag.regseminars = dataAccess.getregisteredseminars();


                //get list of students registered for a particular seminar
                List<student> students = dataAccess.getseminarregisteredstudents(id);

                model.Registration = new registration();
                model.Registration.seminar_id = id;

                model.Students = new List<student>();
                model.Students = students;

                return View("BulkMails", model);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function BulkMailStudents Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function BulkMailStudents Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        [HttpPost]
        public ActionResult BulkMailsFilter(string FilterText)
        {
            try
            {
                BulkMailModel model=new BulkMailModel();
                model.Registration = new registration();
                model.Students = new List<student>();
                ViewBag.Filter = getFilterList();

                TempData["filteredText"] = FilterText;
                TempData.Keep();

                ViewBag.regseminars = dataAccess.getfilterregisteredseminars(FilterText);
              
                return View("BulkMails", model);
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

        /// <summary>
        /// delete action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


        public string[] getFilterList()
        {
            string[] filters = new string[] { "Paid", "Unpaid", "Attended", "Not Attended" };
            return filters;
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
