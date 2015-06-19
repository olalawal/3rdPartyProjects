using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticalSolutions.Models
{
    class Mails
    {


        public string Name { get; set; } 

        public string From { get; set; }

        public string To { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string[] FilesToAttach { get; set; }

        public string HostServer { get; private set; }

        public int Port { get; private set; }

        public bool IsCredentialRequired { get; private set; }

        public string AuthUserName { get; private set; } 


        public string AuthPassword { get; private set; }


        public bool IsEnableSSL { get; private set; }

        public bool IsBodyHtml { get; private set; }


        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="hostServer"></param>
        /// <param name="port"></param>      
        /// <param name="authUsername"></param>
        /// <param name="authPassword"></param>
        /// <param name="isEnableSSL"></param>
        /// <param name="isCredentialRequired"></param>
        /// <param name="isBodyHtml"></param>
        public Mails(string hostServer, int port, string authUserName, string authPassword, bool isEnableSSL, bool isCredentialRequired, bool isBodyHtml)
        {           
            HostServer = hostServer;
            Port = port;         
            //FilesToAttach = filesToAttach; 
            IsCredentialRequired = isCredentialRequired;
            AuthUserName = authUserName;
            AuthPassword = authPassword;
            IsEnableSSL = isEnableSSL;
            IsBodyHtml = isBodyHtml;
        }


    }
}