using DataEngine;
using DataServices;
using NationAssists.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }






        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ChangePassword()
        {
            if (Session["CustomerId"] != null)
            {
                mChangePassword obj = new mChangePassword();
                obj.UserId = Convert.ToInt32(Session["CustomerId"]);
                obj.UserType = UserTypeCode.Customer;

                return View(obj);
            }
            else
            {
                return Redirect("../Home");
            }
        }

        public ActionResult UpdateUserPassword(mChangePassword objCP)
        {
            if (Session["CustomerId"] != null || Session["UserId"]!=null)
            {
                MethodOutput<string> objOutput = new MethodOutput<string>();
                bool IsValidOldPassword = false;
                if (objCP.UserType == UserTypeCode.Customer)
                {
                    Customer objCustomer = (Customer)Session["Customer"];
                    IsValidOldPassword = objCustomer.Password.Equals(objCP.OldPassword);
                }
                else
                {
                    Users objUsers = (Users)Session["User"];
                    IsValidOldPassword = objUsers.Password.Equals(objCP.OldPassword);
                }
                if (IsValidOldPassword)
                {
                  
                    UserServices objUser = new UserServices();
                    objOutput = objUser.UpdateUserPassword(objCP.UserId, objCP.UserType, objCP.Password);
                    
                }
                else
                {
                    objOutput.ErrorMessage = "Old password is incorrect.";
                   
                }
                return Json(objOutput, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (objCP.UserType == "CS")
                {
                    return Redirect("../Home");
                }
                else
                {
                    return Redirect("../../Home");
                }
            }

        }
    }
}