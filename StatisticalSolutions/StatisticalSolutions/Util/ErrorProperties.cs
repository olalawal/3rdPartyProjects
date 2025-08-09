using System.Collections.Generic;
using System.Web.Mvc;

namespace StatisticalSolutions.Util 
{
    public static class ErrorProperties
    {
        public static Dictionary<string, string> ErrorCodeProp { get; set; }

        /// <summary>
        /// Get error from dictionary based on error caode
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string GetErrorMessageBasedOnCode(this HtmlHelper htmlHelper, string errorCode)
        {
            return  ErrorCodeProp[errorCode];
        }

      
    }
}