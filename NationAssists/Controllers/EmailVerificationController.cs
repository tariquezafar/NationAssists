using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class EmailVerificationController : Controller
    {
        // GET: EmailVerification http://localhost:3352/EmailVerification?Code=CUST0000001
        public ActionResult Index()
        {
            if (Request.Url.AbsoluteUri.IndexOf("?") != -1)
            {
                string strURL = Request.Url.AbsoluteUri;
                string CustomerCode = strURL.Substring(strURL.IndexOf("?")+1);

                if (!string.IsNullOrEmpty(CustomerCode))
                {
                    MethodOutput<string> objMO = new MethodOutput<string>();
                    CustomerService obj = new CustomerService();
                    objMO = obj.UpdateCustomerEmailVerificationStatus(CustomerCode);
                    if (objMO.ErrorMessage == string.Empty)
                    {
                        ViewBag.IsVerificationSuccess = true;
                    }
                    else
                    {
                        ViewBag.IsVerificationSuccess = false;
                    }

                    ViewBag.IsValidCode = true;
                    
                }
                else
                {
                    ViewBag.IsValidCode = false;
                    ViewBag.IsVerificationSuccess = false;
                }


                return View();
            }
            else
            {
                return Redirect("../Home");
            }
           
        }
    }
}