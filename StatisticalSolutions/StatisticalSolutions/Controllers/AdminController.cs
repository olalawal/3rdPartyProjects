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
    [Authorize(Roles="Admin")]
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

        public ActionResult Admin()
        {
            return View();
        }

        #region workshops

     
        /// <summary>
        /// Add Workshop
        /// </summary>
        /// <returns></returns>
        public ActionResult AddWorkshop()
        {
            try
            {
                WorkshopViewModel model = new WorkshopViewModel();               

                model.Countries =  ListClass.CountryList;

                model.Instructors = dataAccess.getinstructors();

                return View(model);
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
        public ActionResult AddWorkshop(WorkshopViewModel model)
        {

            try
            {
                if (model == null)
                    throw new CustomException("SEMINAR_MODEL_IS_NULL");

                seminar seminar = new seminar();

                seminar.instructor_id = model.instructor_id;
                seminar.TitleHtml = model.TitleHtml;
                seminar.Description = model.Description;
                seminar.EventDetailsHtml = model.EventDetailsHtml;

                seminar.StartDate =Convert.ToDateTime( model.StartDate);
                seminar.Enddate = Convert.ToDateTime(model.Enddate);
                seminar.Starttime = model.Starttime;
                seminar.Endtime = model.Endtime;

                seminar.Address1 = model.Address1;
                seminar.Address2 = model.Address2;
                seminar.City = model.City;
                seminar.StateProvince = model.StateProvince;
                seminar.Country = model.Country;
                seminar.Email = model.Email; 
                seminar.Phone = model.Phone;
                seminar.Fax = model.Fax;

                seminar.ContactEmail = model.ContactEmail;
                seminar.ContactPhone = model.ContactPhone;
                seminar.ContactWebsite = model.ContactWebsite;

                seminar.IsActive = true;
              
                dataAccess.addseminar(seminar);
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
                List<seminar> seminars = dataAccess.getfutureseminars();
               
                ViewBag.InactiveWorkshops = "InactiveWorkshops";

                return View(seminars);
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

        public ActionResult AssignedSeminars(int id)
        {
            try
            {
                List<seminar> seminars = dataAccess.getseminarsbyinstructorid(id); 
              
                ViewBag.InactiveWorkshops = "InactiveWorkshops";

                return View("WorkshopList", seminars);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function AssignedSeminars Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function AssignedSeminars Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }
        

        [HttpPost]
        public ActionResult InactiveWorkshops(bool IsActive)
        {
            try
            {
                List<seminar> seminars = dataAccess.getseminars(IsActive);  
               
                return View("WorkshopList", seminars);
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
                WorkshopViewModel model = new WorkshopViewModel();

                //get seminar by seminar id
                seminar seminar= dataAccess.getseminarbyid(id);

                model.seminar_id = seminar.seminar_id;
                model.instructor_id = seminar.instructor_id;
                model.TitleHtml = seminar.TitleHtml;
                model.Description = seminar.Description;
                model.EventDetailsHtml = seminar.EventDetailsHtml;

                model.StartDate = seminar.StartDate;
                model.Enddate = seminar.Enddate;
                model.Starttime = seminar.Starttime;
                model.Endtime = seminar.Endtime;

                model.Address1 = seminar.Address1;
                model.Address2 = seminar.Address2;
                model.City = seminar.City;
                model.StateProvince = seminar.StateProvince;
                model.Country = seminar.Country;
                model.Email = seminar.Email;
                model.Phone = seminar.Phone;
                model.Fax = seminar.Fax;

                model.ContactEmail = seminar.ContactEmail;
                model.ContactPhone = seminar.ContactPhone;
                model.ContactWebsite = seminar.ContactWebsite;

                model.IsActive = seminar.IsActive;

                //get list of instructors
                model.Instructors = dataAccess.getinstructors();

                //get list of countries
                model.Countries = ListClass.CountryList;


                return View(model);
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
        /// Post action of edit workshop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult EditWorkshop(WorkshopViewModel model)
        {

            try
            {
                if (model == null)
                    throw new CustomException("SEMINAR_MODEL_IS_NULL");

                seminar seminar = new seminar();

                seminar.seminar_id = model.seminar_id;
                seminar.instructor_id = model.instructor_id;
                seminar.TitleHtml = model.TitleHtml;
                seminar.Description = model.Description;
                seminar.EventDetailsHtml = model.EventDetailsHtml;

                seminar.StartDate = Convert.ToDateTime(model.StartDate);
                seminar.Enddate = Convert.ToDateTime(model.Enddate);
                seminar.Starttime = model.Starttime;
                seminar.Endtime = model.Endtime;

                seminar.Address1 = model.Address1;
                seminar.Address2 = model.Address2;
                seminar.City = model.City;
                seminar.StateProvince = model.StateProvince;
                seminar.Country = model.Country;
                seminar.Email = model.Email;
                seminar.Phone = model.Phone;
                seminar.Fax = model.Fax;

                seminar.ContactEmail = model.ContactEmail;
                seminar.ContactPhone = model.ContactPhone;
                seminar.ContactWebsite = model.ContactWebsite;

                seminar.IsActive = model.IsActive;

                dataAccess.updateseminar(seminar);

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
                InstructorViewModel model = new InstructorViewModel();

                model.Countries = ListClass.CountryList;

                return View(model);
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
        public ActionResult AddConsultant(InstructorViewModel model)
        {

            try
            {

                if (model == null)
                    throw new CustomException("INSTRUCTOR_MODEL_IS_NULL");

                instructor instructor = new instructor();

                if(model.File!=null)
                {
                    string fileName = System.IO.Path.GetFileName(model.File.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/images/consultants"), fileName);
                    // file is uploaded
                    model.File.SaveAs(path);
                    instructor.ImageName = model.File.FileName;
                    instructor.ImagePath = "images/consultants/" + fileName;
                }
              
                instructor.Name = model.InstructorName;
                instructor.Address1 = model.Address1;
                instructor.Address2 = model.Address2;
                instructor.City = model.City;
                instructor.StateProvince = model.StateProvince;
                instructor.Country = model.Country;
                instructor.Email = model.Email;
                instructor.Description = model.Description;
                instructor.DetailsHtml = model.DetailsHtml;
                instructor.Phone = model.Phone;
                instructor.Fax = model.Fax;
                instructor.IsActive = model.IsActive;
                instructor.IsActive = true;

                dataAccess.addInstructor(instructor);

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
                ViewBag.InativeConsultants = "InativeConsultants";

                List<instructor> instructors = dataAccess.getinstructors();

                return View(instructors);
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
        /// post action for inactive consultants
        /// </summary>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public ActionResult InactiveConsultants(bool IsActive)        {
            try
            {
                List<instructor> consultantList = dataAccess.getinstructors(IsActive);

                return View("ConsultantList", consultantList);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function InactiveConsultants Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function InactiveConsultants Error  - {0}", ex.Message));
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
                InstructorViewModel model = new InstructorViewModel();

                //get instructor by id
                instructor instructor = dataAccess.getinstructorbyid(id);
                if (instructor==null)
                    throw new CustomException("INSTRUCTOR_MODEL_IS_NULL");

                model.instructor_id = instructor.instructor_id;
                model.InstructorName = instructor.Name;            
                model.Address1 = instructor.Address1;
                model.Address2 = instructor.Address2;
                model.City = instructor.City;
                model.StateProvince = instructor.StateProvince;
                model.Country = instructor.Country;
                model.Email = instructor.Email;
                model.Description = instructor.Description;
                model.DetailsHtml = instructor.DetailsHtml;
                model.Phone = instructor.Phone;
                model.Fax = instructor.Fax;
                model.IsActive = instructor.IsActive;
             
                //get list of countries
                model.Countries = ListClass.CountryList;

                return View(model); 
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
        public ActionResult EditConsultant(InstructorViewModel model)
        {
            try
            {
                if (model== null)
                    throw new CustomException("INSTRUCTOR_MODEL_IS_NULL");
                instructor instructor = new instructor();
                if (model.File != null)
                {
                    string fileName = System.IO.Path.GetFileName(model.File.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/images/consultants"), fileName);
                    // file is uploaded
                    model.File.SaveAs(path);
                    instructor.ImageName = model.File.FileName;
                    instructor.ImagePath = "images/consultants/" + fileName;
                }
                
                instructor.instructor_id = model.instructor_id;
                instructor.Name = model.InstructorName;            
                instructor.Address1 = model.Address1;
                instructor.Address2 = model.Address2;
                instructor.City = model.City;
                instructor.StateProvince = model.StateProvince;
                instructor.Country = model.Country;
                instructor.Email = model.Email;
                instructor.Description = model.Description;
                instructor.DetailsHtml = model.DetailsHtml;
                instructor.Phone = model.Phone;
                instructor.Fax = model.Fax;
                instructor.IsActive = model.IsActive;

                dataAccess.updateInstructor(instructor);

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

                ViewBag.InactiveClients = "InactiveClients";

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
        /// post action for inactive clients
        /// </summary>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public ActionResult InactiveClients(bool IsActive) 
        {
            try
            {  
                List<client> clientList = dataAccess.getCompanies(IsActive);


                return View("ClientsList", clientList);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function InactiveClients Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function InactiveClients Error  - {0}", ex.Message));
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
                ClientViewModel model = new ClientViewModel();
                model.Countries = ListClass.CountryList;
                return View(model);
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
        public ActionResult AddClient(ClientViewModel model)
        {
            try
            {
                client client = new client();
                if (model == null)
                    throw new CustomException("CLIENT_MODEL_IS_NULL");
                else
                {                  
                    client.Name = model.ClientName;                 
                    client.Address1 = model.Address1;
                    client.Address2 = model.Address2;
                    client.City = model.City;
                    client.StateProvince = model.StateProvince;
                    client.Country = model.Country;
                    client.Email = model.Email;
                    client.Description = model.Description;
                    client.Phone = model.Phone;
                    client.Fax = model.Fax;
                    client.IsActive = true;                   
                }               
                
                dataAccess.addclient(client);
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
                ClientViewModel model = new ClientViewModel();

                client client=dataAccess.getcompaniesbyid(id);

                if (client != null)                   
                {
                    model.client_id = client.client_id;
                    model.ClientName = client.Name;
                    model.Address1 = client.Address1;
                    model.Address2 = client.Address2;
                    model.City = client.City;
                    model.StateProvince = client.StateProvince;
                    model.Country = client.Country;
                    model.Email = client.Email;
                    model.Description = client.Description;
                    model.Phone = client.Phone;
                    model.Fax = client.Fax;
                    model.IsActive = client.IsActive;
                }

                model.Countries = ListClass.CountryList;

                return View(model);
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


        /// <summary>
        /// action method of EditClient
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClient(ClientViewModel model)
        {
            try
            {
                client client = new client();
                if (model == null)
                    throw new CustomException("CLIENT_MODEL_IS_NULL");
                else
                {
                    client.client_id = model.client_id;
                    client.Name = model.ClientName;               
                    client.Address1 = model.Address1;
                    client.Address2 = model.Address2;
                    client.City = model.City;
                    client.StateProvince = model.StateProvince;
                    client.Country = model.Country;
                    client.Email = model.Email;
                    client.Description = model.Description;
                    client.Phone = model.Phone;
                    client.Fax = model.Fax;
                    client.IsActive = model.IsActive;                
                }             

                dataAccess.updateclient(client);

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
        /// action method of delete client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get action for bulk mails
        /// </summary>
        /// <returns></returns>
        public ActionResult BulkMails()
        {
            try
            {
                BulkMailModel bulkMailModel = new BulkMailModel();
                bulkMailModel.Students =new List<student>();
                bulkMailModel.Registration = new registration();

                string[] filteres = getFilterList();
                bulkMailModel.Filteres = filteres;
                TempData["Filters"] = filteres;

                List<seminar> seminars = dataAccess.getregisteredseminars();
                bulkMailModel.Seminars = seminars;           

                TempData.Keep();

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
        /// filter action of bulk mails
        /// </summary>
        /// <param name="FilterText"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BulkMailsFilter(string FilterText)
        {
            try
            {
                //BulkMailModel model = new BulkMailModel();
                //model.Registration = new registration();
                //model.Students = new List<student>();

                //if (TempData["Filters"] != null)
                //    model.Filteres = TempData["Filters"] as string[];
                //else
                //    model.Filteres = getFilterList();

                //TempData["filteredText"] = FilterText;
                //TempData.Keep();
               

                //model.Seminars = dataAccess.getfilterregisteredseminars(FilterText);

                //return View("BulkMails", model);

                var seminars = dataAccess.getfilterregisteredseminars(FilterText);

                //return Json( seminars , JsonRequestBehavior.AllowGet);
                return Json(new { value = seminars, status = "success" }, JsonRequestBehavior.AllowGet);

            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function BulkMailsFilter Error  - {0}", ex.Message));
                return Json(new { value = ex.Message, status = "error" }, JsonRequestBehavior.AllowGet);
                //return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function BulkMailsFilter Error  - {0}", ex.Message));
                return Json(new { value = ex.Message, status = "error" }, JsonRequestBehavior.AllowGet);
                //return SystemExceptionCatcher(ex);
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


                seminar seminar = dataAccess.getseminarbyid(model.seminar_id);
               
                if (model.Students.Any())
                {
                    foreach (student st in model.Students)
                    {
                       
                        mail.Name = st.FirstName + " " + st.LastName;
                        mail.Body = "Hi " + st.FirstName + ", <br/><br/> You have registered for workshop " + seminar.TitleHtml + " starting from " + seminar.StartDate + " at statistical solutions. <br/><br/> Regards<br/>Statistical Solutions Team";
                        mail.From = ConfigurationManager.AppSettings["EmailFrom"];
                        mail.To = st.Email;
                        mail.CC = ConfigurationManager.AppSettings["AdminEmail"];
                        mail.Subject = "You have registered for " + seminar.TitleHtml;
                        // _maillSender.SendMail(mail);
                        //MailExtention.sendemail(mail.To, mail.Subject, mail.Body);  //using sendmail
                        Log.Info("Function BulkMails - End of mail sending");
                        
                    }
                }

                return Json(new { totalCount = model.Students.Count, status="success"}, JsonRequestBehavior.AllowGet); 
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
        /// action for get list of to registered students
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BulkMailStudents(string id)     
        {
            try
            {

                int seminar_id=Convert.ToInt32(id);
                    
                BulkMailModel model = new BulkMailModel();

                model.Registration = new registration();
                model.Registration.seminar_id = seminar_id;

                //get list of students registered for a particular seminar
                List<student> students = dataAccess.getseminarregisteredstudents(seminar_id);                

                model.Students = students;

                return PartialView("BulkMailsPartial", model); 
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
                //get list of students
                List<student> students = dataAccess.getstudents();

                ViewBag.InactiveStudents = "InactiveStudents";

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


        /// <summary>
        ///  get list of registered students by seminar id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult RegisteredStudents(int  id)
        {
            try
            {
                //get student by student id
                List<student> students = dataAccess.getstudents(id);
               
                return View("StudentList", students);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function RegisteredStudents Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function RegisteredStudents Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }

        /// <summary>
        /// post action for inactive students
        /// </summary>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public ActionResult InactiveStudents(bool IsActive)
        {
            try
            {
                //get list of active students
                List<student> students = dataAccess.getinactivestudents(IsActive);

                return View("StudentList", students);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function InactiveStudents Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function InactiveStudents Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }


        ///// <summary>
        ///// post action of add student
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddStudent(student model)
        //{
        //    try
        //    {
        //        model.IsActive = true;
        //        dataAccess.addstudent(model);

        //        return RedirectToAction("StudentList");
        //    }
        //    catch (CustomException ex)
        //    {
        //        Log.Error(m => m("Function AddStudent Error  - {0}", ex.Message));
        //        return CustomExceptionCatcher(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(m => m("Function AddStudent Error  - {0}", ex.Message));
        //        return SystemExceptionCatcher(ex);
        //    }


        //}


        public ActionResult EditStudent(int id)
        {
            try
            {
                StudentViewModel model = new StudentViewModel();

                //get student by student id
                student student= dataAccess.getstudentbyid(id);
                if (student!=null)
                {
                    model.student_id = student.student_id;
                    model.FirstName = student.FirstName;
                    model.LastName = student.LastName;
                    model.Address1 = student.Address1;
                    model.Address2 = student.Address2;
                    model.City = student.City;
                    model.StateProvince = student.StateProvince;
                    model.Country = student.Country;
                    model.Email = student.Email;
                    model.BankAccountNumber = student.BankAccountNumber;
                    model.Phone = student.Phone;
                    model.Fax = student.Fax;
                    model.IsActive = student.IsActive;
                }

                //get list of countries
                model.Countries = ListClass.CountryList;

                return View("EditStudent", model);
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
        public ActionResult EditStudent(StudentViewModel  model)
        {
            try
            {
                student student = new student();
                if(model !=null)
                {
                    student.student_id = model.student_id;
                    student.FirstName = model.FirstName;
                    student.LastName = model.LastName;
                    student.Address1 = model.Address1;
                    student.Address2 = model.Address2;
                    student.City = model.City;
                    student.StateProvince = model.StateProvince;
                    student.Country = model.Country;
                    student.Email = model.Email;
                    student.BankAccountNumber = model.BankAccountNumber;
                    student.Phone = model.Phone;
                    student.Fax = model.Fax;
                    student.IsActive = model.IsActive;
                }
                else
                    throw new CustomException("STUDENT_MODEL_IS_NULL");

                dataAccess.updatestudent(student);

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


        #region Methods

        /// <summary>
        /// Method to get list of filter fields
        /// </summary>
        /// <returns></returns>
        public string[] getFilterList()
        {
            string[] filters = new string[] { "Paid", "Unpaid", "Attended", "Not Attended" };
            return filters;
        }

        /// <summary>
        /// Method to add Start Dates
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
        
        #endregion
        

    }
}
