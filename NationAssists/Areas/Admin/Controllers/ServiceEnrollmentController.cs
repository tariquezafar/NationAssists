using DataEngine;
using DataServices;
using NationAssists.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NationAssists.Areas.Admin.Controllers
{
    public class ServiceEnrollmentController : Controller
    {
        mServiceEnrollment objMSE = new mServiceEnrollment();
        // GET: Admin/ServiceEnrollment
        public ActionResult Index()
        {

            if (Session["UserId"] != null)
            {
                 objMSE.ServiceEnrollmentRequestList = GetAllServiceEnrollmentRequest(0,0,0,string.Empty);
                return View(objMSE);
                //GetAllServiceEnrollmentRequest(0);
                //return View();
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public List<ServiceEnrollmentRequest> GetAllServiceEnrollmentRequest(int CustomerId, int ServiceId, int EnrollmentStatusId, string CustomerName)
        {
            ServiceEnrollmentService objSES = new ServiceEnrollmentService();
            MethodOutput<ServiceEnrollmentRequest> output = new MethodOutput<ServiceEnrollmentRequest>();
            output = objSES.GetAllServiceEnrollementRequst(CustomerId, ServiceId, EnrollmentStatusId, CustomerName);
            ViewBag.ErrorMessage = output.ErrorMessage;
            return output.DataList;
        }

        public ActionResult UpdateServiceEnrollment(ServiceEnrollmentRequest objSER)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            string TicketNo = string.Empty;

            try
            {
                ServiceEnrollmentService obj = new ServiceEnrollmentService();
                objSER.ApprovedBy = Convert.ToInt32(Session["UserId"]);
                objMO = obj.UpdateServiceEnrollmentRequest(objSER);
                if (objSER.ServiceEnrollmentStatus==(int)ServiceEnrollmentStatusEnum.Accepted && objMO.ErrorMessage == string.Empty)
                {
                    string CustomerName = objSER.CustomerName;
                    string DownloadURL = string.Empty;


                    if (objSER.ServiceId == 1)
                    {
                        DownloadURL = Request.Url.Authority + "/DownloadDocument/Nation Assist Roadside Assistance.pdf";
                    }
                    else if (objSER.ServiceId == 11)
                    {
                        DownloadURL = Request.Url.Authority + "/DownloadDocument/Nation Assist Home Assistance.pdf";
                    }
                    else
                    {
                        DownloadURL = Request.Url.Authority + "/DownloadDocument/Nation Assist Roadside and Car Replacement.pdf";
                    }

                    Dictionary<string, string> objTempValues = new Dictionary<string, string>();
                    objTempValues.Add("CustomerName", CustomerName);
                    objTempValues.Add("DownloadURL", DownloadURL);

                    EmailServices objEmailService = new EmailServices();
                    List<string> ToEmail = new List<string> { objSER.CustomerEmail };
                    objEmailService.MailSent(ToEmail, objTempValues, "SERVICE_ENROLLMENT");
                }
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


        public ActionResult SearchServiceEnrollmentRequest( int ServiceId, int EnrollmentStatusId, string CustomerName)
        {
            string strError = string.Empty;
            try
            {
                List<ServiceEnrollmentRequest> objList = new List<ServiceEnrollmentRequest>();
                objList = GetAllServiceEnrollmentRequest(0, ServiceId, EnrollmentStatusId,CustomerName);
                mServiceEnrollment objMSE = new mServiceEnrollment();
                objMSE.ServiceEnrollmentRequestList = objList;
                return PartialView("_ServiceEnrollmentRequestList",objMSE);

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }

            return Json(new { ErrorMsg = strError }, JsonRequestBehavior.AllowGet);
                 
        }

      
    }
}