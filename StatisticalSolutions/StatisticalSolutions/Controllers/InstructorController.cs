using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using Common.Logging;
using System.Configuration;
using System.Web.Hosting;
using StatisticalSolutions.Models;
using StatisticalSolutions.ViewModels;
using StatisticalSolutions.DataAccess;
using StatisticalSolutions.Util;
using StatisticalSolutions.Filters;
using StatisticalSolutions.Helpers;
using StatisticalSolutions.Controllers.Base;

namespace StatisticalSolutions.Controllers
{
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

        public ActionResult Consultants()
        {
           ViewBag.Consultants = dataAccess.getinstructors(); ;

            ViewBag.Seminar_Instructor = dataAccess.getinstructorseminars();
            
            ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;

           return View();
        }

        /// <summary>
        /// Consultant details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Consultant(int id)   
        {
            try
            {
                ViewBag.Instructor = dataAccess.getinstructorbyid(id);
                
                ViewBag.Seminar_Instructor = dataAccess.getinstructorseminars(); ;

                ViewBag.Consultants = dataAccess.getinstructors();

                ViewBag.Instructor = dataAccess.getinstructorbyid(id);

                ViewBag.VirtualPath = HostingEnvironment.ApplicationVirtualPath;
                return View("Consultants");
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


    }
}
