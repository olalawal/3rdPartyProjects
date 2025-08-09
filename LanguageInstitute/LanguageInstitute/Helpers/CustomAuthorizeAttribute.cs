using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.Configuration;
using System;

namespace LanguageInstitute.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new Exception("httpContext");

            //check if user is Authenticated or not.
            //it checks that wither user is logged in the application or it is trying to run the application without proper login.
            //var isAuthenticated = httpContext.User.Identity.IsAuthenticated;

            //check if session is out or not. Two known cases are -- either exceeds from sessionTimeOut value of WebConfig or some changes in -
            //webConfig during the application running etc..
            var isSessionTimeOut = httpContext.Session == null || (httpContext.Session.Count == 0);

            if (isSessionTimeOut)
            {
                httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.RequestTimeout;
                httpContext.Response.Status = "408 Request Timeout";
                if (httpContext.Request.IsAjaxRequest())
                    httpContext.Response.End();
                //return false to indicate that User is authenticated but there is session out.
               
            }
            //Authrize for direct login
            return !isSessionTimeOut;
        }
    }
}