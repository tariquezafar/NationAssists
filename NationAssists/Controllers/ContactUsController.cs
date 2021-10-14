using DataServices;
using NationAssists.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmitEnquiry(CustomerEnquiry objCE)
        {
            string strError = string.Empty;
            try
            {
                string ServiceEmailId = ConfigurationManager.AppSettings["ServiceEmailID"];
                EmailServices objEmailServices = new EmailServices();
                Dictionary<string, string> objTempValues = new Dictionary<string, string>();
                objTempValues.Add("Name", objCE.Name);
                objTempValues.Add("Reason", objCE.Reason);
                objTempValues.Add("MobileNumber", objCE.MobileNumber);
                objTempValues.Add("EmailAddress", objCE.EmailAddress);
                objTempValues.Add("Message", objCE.Message);

                List<string> ToEmail = new List<string> { "info@nassistbh.com", ServiceEmailId };
                objEmailServices.MailSent(ToEmail, objTempValues, "CUSTOMER_ENQUIRY");
            }
            catch ( Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { Error=strError},JsonRequestBehavior.AllowGet);
        }
    }
}