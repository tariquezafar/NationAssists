using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NationAssists.Areas.Admin.Models;
using DataEngine;
using DataServices;
namespace NationAssists.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User

        mUser objUser = new mUser();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            objUser.UserId = 0;
            objUser.Name = "";
            objUser.IsActive = false;
            objUser.UsersList = GetUsers(0);
            return View(objUser);
        }

        [HttpGet]
        public ActionResult GetUserDetail(int UserId)
        {
            List<Users> objUser = GetUsers(UserId);
            return Json(objUser[0], JsonRequestBehavior.AllowGet);
        }
        public List<Users> GetUsers(int UserId)
        {
            MethodOutput<Users> objMO = new MethodOutput<Users>();
            User obj = new User();
            objMO = obj.GetUsers(UserId);
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
                User obj = new User();
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

        [HttpPost]
        public ActionResult BindUsers(int UserId)
        {
            string strError = string.Empty;
            List<Users> objList = new List<Users>();
            try
            {
                objList = this.GetUsers(UserId);
                return PartialView("_UserList.cshtml", objList);
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
                User obj = new User();
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
    }
}