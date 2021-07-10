using DataEngine;
using DataServices;
using NationAssists.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveCustomer(Customer objCustomer)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                CustomerService obj = new CustomerService();
                EmailServices objEmailService = new EmailServices(); 
                objMO = obj.SaveCustomer(objCustomer);
                IsSaved = objMO.ErrorMessage == string.Empty ? true : false;
                if (objMO.ErrorMessage == "" && objMO.Data!="" )
                {
                    // Get Mail Template
                    List<string> ToEmail = new List<string> { objCustomer.EmailId,"t.zafar007@gmail.com" };
                    Dictionary<string, string> objTempValues = new Dictionary<string, string>();
                    objTempValues.Add("UserName", (objCustomer.FirstName + " " + objCustomer.LastName));
                    objTempValues.Add("URL", Request.Url.Authority + "/EmailVerification?"+objMO.Data);

                    IsSaved = objEmailService.MailSent(ToEmail,objTempValues, "EMAILVERIFICATION");
                }
               
            }
            catch (Exception ex)
            {

                IsSaved = false;
                strMsg = ex.Message;
            }
            return Json(new { Result = IsSaved, Msg = strMsg, DuplicateEmail = objMO.ErrorMessage == SqlOutput.Duplicate_Email ? true : false, DuplicateMobile = objMO.ErrorMessage == SqlOutput.Duplicate_Mobile ? true : false }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SearchCPRNumber(string CPRNumber, int SourceId)
        {
            SearchCPROutput output = new SearchCPROutput();
            try
            {
                MembershipServices objMM = new MembershipServices();
                output = objMM.SearchMemberShipByCPRNumber(CPRNumber,SourceId);
            }
            catch (Exception ex)
            {
                output.IsValidCPR = false;
                output.ErrorMessage = ex.Message;
            }

            return Json(output);

        }
    }
}