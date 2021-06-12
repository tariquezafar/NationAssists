using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            EmailServices obj = new EmailServices();
          //  obj.MailSent();
            return View();
        }

        

     
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}