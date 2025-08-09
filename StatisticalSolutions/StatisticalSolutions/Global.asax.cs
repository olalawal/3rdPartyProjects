using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.IO;
using StatisticalSolutions.Util;
using StatisticalSolutions.DataAccess;
using StatisticalSolutions.Models;


namespace StatisticalSolutions
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ReadErrorCodeProperties();
            GetCountries();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }


        protected void Session_Start()
        {

        }

        /// <summary>
        /// Reading the error property file.
        /// </summary>
        private void ReadErrorCodeProperties()
        {
            var data = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(Server.MapPath("~/errorcode.properties")))
                data.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));

            Application["errorcode"] = data;
            //set this value into a static property of ErrorProperties class.
            //we are using that properties in our views to display alert messages.
            ErrorProperties.ErrorCodeProp = data;
        }

        private void GetCountries()
        {
            DAL dataAccess=new DAL();

            List<Countries> countries = dataAccess.getCountries();

          ListClass.CountryList = countries; 
           
        }
    }

    
}