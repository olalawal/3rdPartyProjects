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
using LanguageInstitute.Models;
using LanguageInstitute.DataAccess;
using LanguageInstitute.Util;
using LanguageInstitute.Filters;
using LanguageInstitute.Helpers;
using LanguageInstitute.Controllers.Base;

namespace LanguageInstitute.Controllers
{

    [Authorize(Roles="Student")]
    public class StudentController : BaseController
    {

         #region variables
        DAL dataAccess;
        
        #endregion

        #region constructor

        public StudentController()
        {
            dataAccess = new DAL();          
                
        }
        #endregion

        /// <summary>
        /// get action for logged in student
        /// </summary>
        /// <returns></returns>
        public ActionResult Student() 
        {
            try
            {
                StudentViewModel model = new StudentViewModel();
                //get student details by student user id
                student student = dataAccess.getstudentbyid(WebSecurity.CurrentUserId);

                if (student != null)
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

                // get list of all seminars registered by particular student
                 model.Seminars = dataAccess.getseminarsbystudentid(student.student_id);

                 return View(model);

            }
            catch (CustomException ex)
            {
                Log.Error(m => m("Function Student Error  - {0}", ex.Message));
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                Log.Error(m => m("Function Student Error  - {0}", ex.Message));
                return SystemExceptionCatcher(ex);
            }

        }

        

      
    }
}
