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

      
    }
}
