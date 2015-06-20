using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Common.Logging;
using System.Configuration;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.Web.Hosting;
using StatisticalSolutions.Models;
using StatisticalSolutions.DataAccess;
using StatisticalSolutions.Util;
using StatisticalSolutions.Filters;
using StatisticalSolutions.Helpers;
using StatisticalSolutions.Controllers.Base;

namespace StatisticalSolutions.Controllers
{

    [Authorize(Roles="Instructor")]
    public class InstructorController : BaseController 
    {
              
        #region variables
        DAL dataAccess;
        
        #endregion

        #region constructor

        public InstructorController()
        {
            dataAccess = new DAL();          
                
        }
        #endregion


        /// <summary>
        /// get action for consultant page
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Consultants()
        {
            WorkshopInstructorViewModel model = new WorkshopInstructorViewModel();


            //List<instructor> instructors = dataAccess.getinstructors(); ;

            //TempData["Instructors"] = instructors;
            model.Instructors = dataAccess.getinstructors(); ;

            List<seminar> seminars = dataAccess.getfutureseminars();

            //TempData["Seminars"] = seminars;
            model.Seminars = seminars;
            
            //TempData.Keep();

            return View(model);
        }

        /// <summary>
        /// get action for Consultant details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Consultant(int id)   
        {
            try
            {
                WorkshopInstructorViewModel model = new WorkshopInstructorViewModel();
                                    
                model.Seminars = dataAccess.getfutureseminars();  
                    
                model.Instructors = dataAccess.getinstructors();

                //get instructor by innstructor id
                model.Instructor = dataAccess.getinstructorbyid(id);
                                
                return View("Consultants", model);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Consultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Consultant Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


        /// <summary>
        /// get action for Consultant details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ConsultantDetails(string id)  
        {
            try
            {

                int instructor_id = Convert.ToInt32(id);              

                // get instructor by innstructor id
                instructor instructor = dataAccess.getinstructorbyid(instructor_id);

                return PartialView("_ConsultantDetailsPartial", instructor);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Consultant Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Consultant Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


        /// <summary>
        /// get action for logged in instructor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Instructor() 
        {
            try
            {
                WorkshopInstructorViewModel model = new WorkshopInstructorViewModel();
               
                //Method to get instructor based on instructor user id
                instructor instructor = dataAccess.getinstructorbyid(WebSecurity.CurrentUserId);               
              
                model.Instructor = instructor;
           
                //get list of all seminars taught by particular instructor  
                model.Seminars = dataAccess.getseminarsbyinstructorid(instructor.instructor_id);                 
                
                return View("Consultant", model);
            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Instructor Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Instructor Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }


        }


    }
}
