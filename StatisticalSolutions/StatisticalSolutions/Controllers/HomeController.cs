using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticalSolutions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Statistical Solutions";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        //bios 

        public ActionResult bioBayoLawal()
        {
            ViewBag.Message = "Professor Bayo Lawal";

            return View();
        }


        public ActionResult bioFelixFamoye()
        {
            ViewBag.Message = "Professor Felix Famoye";

            return View();
        }

    }
}
