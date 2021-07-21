using DataEngine;
using DataServices;
using NationAssists.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Controllers
{
    public class BuyPackageRequestController : Controller
    {
        // GET: BuyPackageRequest
        mServiceEnrollment objMSE = new mServiceEnrollment();
        public ActionResult Index()
        {
            if (Session["CustomerId"] != null && Session["CPRNumber"] != null && TempData.ContainsKey("Service"))
            {
                Service objService = (Service)TempData["Service"];
                objMSE.Package = objService;
                objMSE.CustomerId = Convert.ToInt32(Session["CustomerId"]);
                return View(objMSE);
            }
            else
            {
                return Redirect("Home");
            }
        }

        public ActionResult SaveServiceEnrollmentRequest(ServiceEnrollmentRequest objSER)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            string TicketNo = string.Empty;

            try
            {
                ServiceEnrollmentService obj = new ServiceEnrollmentService();
             
                objMO = obj.SaveServiceEnrollmentRequest(objSER);
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


        public ActionResult ViewServiceEnrollmentRequest()
        {
            if (Session["CustomerId"] != null && Session["CPRNumber"] != null)
            {
                objMSE.ServiceEnrollmentRequestList = GetAllServiceEnrollmentRequest(Convert.ToInt32(Session["CustomerId"]));
                return View(objMSE);
            }
            else
            {
                return Redirect("../Home");
            }
        }

        public List<ServiceEnrollmentRequest> GetAllServiceEnrollmentRequest(int CustomerId)
        {
            ServiceEnrollmentService objSES = new ServiceEnrollmentService();
            MethodOutput<ServiceEnrollmentRequest> output = new MethodOutput<ServiceEnrollmentRequest>();
            output = objSES.GetAllServiceEnrollementRequst(CustomerId);
            return output.DataList;
        }

    }
}