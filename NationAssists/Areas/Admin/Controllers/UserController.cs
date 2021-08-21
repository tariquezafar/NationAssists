using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NationAssists.Areas.Admin.Models;
using DataEngine;
using DataServices;
using NationAssists.Models;

namespace NationAssists.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User

        mUser objUser = new mUser();
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public ActionResult AddUser()
        {
            if (Session["UserId"] != null)
            {
                objUser.UserId = 0;
                objUser.Name = "";
                objUser.IsActive = false;
              
                objUser.UsersList = GetUsers(0,0,0,string.Empty,string.Empty);
                return View(objUser);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        [HttpGet]
        public ActionResult GetUserDetail(int UserId)
        {
            List<Users> objUser = GetUsers(UserId,0,0, string.Empty, string.Empty);
            return Json(objUser[0], JsonRequestBehavior.AllowGet);
        }
        public List<Users> GetUsers(int UserId, int UserTypeId, int UserReferenceId, string UserCode, string UserName)
        {
            MethodOutput<Users> objMO = new MethodOutput<Users>();
            UserServices obj = new UserServices();
            objMO = obj.GetUsers(UserId, UserTypeId, UserReferenceId, UserCode, UserName);
            return objMO.DataList;
        }

        [HttpPost]
        public ActionResult SaveUsers(Users objUsers)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                UserServices obj = new UserServices();
                objMO = obj.SaveUsers(objUsers);
                IsSaved = objMO.ErrorMessage == string.Empty ? true : false;
                strMsg = objMO.ErrorMessage;
            }
            catch (Exception ex)
            {

                IsSaved = false;
                strMsg = ex.Message;
            }
            return Json(new { Result = IsSaved, Msg = strMsg,DuplicateEmail= objMO.ErrorMessage == SqlOutput.Duplicate_Email? true :false,DuplicateMobile= objMO.ErrorMessage == SqlOutput.Duplicate_Mobile ? true : false }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BindUsers(int UserId, int UserTypeId, int UserReferenceId, string UserCode, string UserName)
        {
            string strError = string.Empty;
            List<Users> objList = new List<Users>();
            try
            {
                objList = this.GetUsers(UserId, UserTypeId, UserReferenceId, UserCode, UserName);
                mUser objUsers = new mUser();
                objUsers.UsersList = objList;
                return PartialView("_UserList", objUsers);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }
            return Json(new { ErrorMsg = strError }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteUsers(int UserId)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            try
            {
                UserServices obj = new UserServices();
                objMO = obj.DeleteUsers(UserId);
                IsSaved = objMO.ErrorMessage == string.Empty ? true : false;
                strMsg = objMO.ErrorMessage;
            }
            catch (Exception ex)
            {

                IsSaved = false;
                strMsg = ex.Message;
            }
            return Json(new { Result = IsSaved, Msg = strMsg }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult BindUserTypeDetail(string UserType)
        {
            CommonServices objComm = new CommonServices();
            UserType = UserType == "SP" ? "" : UserType;
            return Json(objComm.BindServiceProvider(UserType).DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword()
        {
            if (Session["UserId"] != null)
            {
                mChangePassword obj = new mChangePassword();
                obj.UserType = UserTypeCode.Employee;
                obj.UserId = Convert.ToInt32(Session["UserId"]);
                return View(obj);
            }
            else
            {
                return Redirect("../../Home");
            }
            
        }
    }
}