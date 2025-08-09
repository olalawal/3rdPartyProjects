using System;
using System.Collections.Generic;
using System.Web;

namespace LanguageInstitute.Models 
{
    public class CustomException : ApplicationException 
    {
        /// <summary>
        /// </summary>
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        public CustomException(string errorCode): base(errorCode)
        {
            var dictErrorCode = (Dictionary<string, string>)HttpContext.Current.Application["errorcode"];
            this.ErrorCode = errorCode;
            this.ErrorMessage = dictErrorCode[errorCode];
        }

        public CustomException(string errorCode, Exception inner)
            : base(errorCode, inner)
        {
            var dictErrorCode = (Dictionary<string, string>)HttpContext.Current.Application["errorcode"];
            this.ErrorCode = errorCode;
            this.ErrorMessage = dictErrorCode[errorCode];
        }
    }
}
