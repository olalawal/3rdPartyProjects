using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Logging;
using System.IO;
using LanguageInstitute.Helpers;
using LanguageInstitute.Models;
using LanguageInstitute.Filters;


namespace LanguageInstitute.Controllers.Base
{
    [InitializeSimpleMembership]
    public class BaseController : Controller
    {

        #region Instances
        protected readonly ILog Log = LogManager.GetCurrentClassLogger();
        #endregion


        #region Properties

        public string ErrorMessage { get; set; }

        #endregion


        #region Override Methods

        /// <summary>
        /// Method for show page notification on particular date,It will call on each action
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                Log.Info(m => m("Application URL - {0} ", Request.Path));
                base.OnActionExecuting(filterContext);
            }
            catch (CustomException ex)
            {
                //if there is any error then put it into the log file...
                Log.Error(m => m("Statistical Error Code - {0}", ex.ErrorCode));
            }
            catch (Exception ex)
            {
                //if there is any error then put it into the log file...
                Log.Error(m => m("Statistical exception message - {0}", ex.Message));
                Log.Error(m => m("Statistical exception stack trace - {0}", ex.StackTrace));
            }
        }

        /// <summary>
        /// Method called when any exception occured
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            try
            {
                Log.Info(m => m("Application URL - {0} ", Request.Path));
                if (filterContext == null)
                    return;

                base.OnException(filterContext);
                var ex = filterContext.Exception ?? new Exception("No further information exists.");
                Session["StatisticalError"] = ex.Message;
                Log.Error(m => m("Statistical exception message - {0}", ex.Message));
                Log.Error(m => m("Statistical exception stack trace - {0}", ex.StackTrace));

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                                                    new
                                                                    {
                                                                        controller = "Base",
                                                                        action = "StatisticalError",
                                                                    
                                                                    }
                                                                    )
                                                                );
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            catch (CustomException ex)
            {
                //if there is any error then put it into the log file...
                Log.Error(m => m("Statistical Error Code - {0}", ex.ErrorCode));
            }
            catch (Exception ex)
            {
                //if there is any error then put it into the log file...
                Log.Error(m => m("Statistical exception message - {0}", ex.Message));
                Log.Error(m => m("Statistical exception stack trace - {0}", ex.StackTrace));
            }
        }

        #endregion


        #region Actions


        /// <summary>
        /// this will call when handled exception occured
        /// </summary>
        /// <returns></returns>
        public ActionResult StatisticalError() 
        {
            try
            {
                if (Session["StatisticalError"] == null)
                {
                    Session["StatisticalError"] = "No further information exists.";
                }
                Log.Info(m => m("Application URL - {0} ", Request.Path));
                return View();
            }
            catch (CustomException ex)
            {
                return CustomExceptionCatcher(ex);
            }
            catch (Exception ex)
            {
                return SystemExceptionCatcher(ex);
            }
        }


        /// <summary>
        /// Catch the custom exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public ActionResult CustomExceptionCatcher(CustomException ex)
        {
            //if there is any error then put it into the log file...
            Log.Error(m => m("Custom Error Code - {0}", ex.ErrorCode));

            //get mesg from the error code
            var errorMesg = GetMessage(ex.ErrorCode);
            Session["StatisticalError"] = errorMesg;
            //return ThrowJsonError(ex, errorMesg);
            return View("~/Views/Base/StatisticalError.cshtml");
        }


        /// <summary>
        /// Catch the system thrown exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public ActionResult SystemExceptionCatcher(Exception ex)
        {
            //if there is any error then put it into the log file...
            Log.Error(m => m("Statistical exception message - {0}", ex.Message), ex);         

            //Log your exception
            var message = string.IsNullOrEmpty(ErrorMessage) ? ex.Message : ErrorMessage + " " + ex.Message;
            Session["StatisticalError"] = message;
            //return ThrowJsonError(ex, message);
            return View("~/Views/Base/StatisticalError.cshtml");
        }


        #endregion

        #region Methods

        /// <summary>
        /// Method to get message for provided code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetMessage(string code)
        {
            try
            {
                Log.Info(m => m("Application URL - {0} ", Request.Path));
                var dictMessages = (Dictionary<string, string>)HttpContext.Application["errorcode"];
                return dictMessages[code];
            }
            catch (CustomException ex)
            {
                //if there is any error then put it into the log file...
                Log.Error(m => m("Statistical Error Code - {0}", ex.ErrorCode));
                return "";
            }
            catch (Exception ex)
            {
                //if there is any error then put it into the log file...
                Log.Error(m => m("Statistical exception message - {0}", ex.Message));
                Log.Error(m => m("Statistical exception stack trace - {0}", ex.StackTrace));
                return "";
            }
        }



     

        #endregion

    }
}
