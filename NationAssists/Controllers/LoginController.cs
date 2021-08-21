using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(LoginInput objLogin)
        {

            LoginOutput objLoginOutput = new LoginOutput();
            try
            {
                LoginServices objLoginServices = new LoginServices();
                objLoginOutput = objLoginServices.ValidateLogin(objLogin);
                if (objLoginOutput.IsValid)
                {
                    if (objLogin.LoginType == "Customer" && objLoginOutput.CustomerDetail!=null)
                    {
                        Session["CustomerId"] = Convert.ToString(objLoginOutput.CustomerDetail.CustomerId);
                        Session["CustomerName"] = Convert.ToString(objLoginOutput.CustomerDetail.Name);
                        Session["IsHavingMembership"] = Convert.ToBoolean(objLoginOutput.CustomerDetail.IsHavingMembership);
                        Session["AccountType"] = Convert.ToString(objLoginOutput.CustomerDetail.AccountType);
                        Session["CPRNumber"] = Convert.ToString(objLoginOutput.CustomerDetail.NationalId);
                        Session["Customer"] = objLoginOutput.CustomerDetail;
                    }
                    else
                    {
                        if (objLoginOutput.UserDetail != null)
                        {
                            Session["UserId"] = Convert.ToString(objLoginOutput.UserDetail.UserId);
                            Session["UserName"] = Convert.ToString(objLoginOutput.UserDetail.Name);

                            Session["User"] = objLoginOutput.UserDetail;
                            
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                objLoginOutput.IsValid = false;
                objLoginOutput.ErrorMessage = ex.Message;
            }

            return Json(objLoginOutput, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return Json(true,JsonRequestBehavior.AllowGet);
        }
    }

}