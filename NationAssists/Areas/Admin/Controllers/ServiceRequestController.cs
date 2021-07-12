using DataEngine;
using NationAssists.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataServices;

namespace NationAssists.Areas.Admin.Controllers
{
    public class ServiceRequestController : Controller
    {
        AllocateServiceRequest mASR = new AllocateServiceRequest();
        // GET: Admin/ServiceRequest

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                mASR.StartDate = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                mASR.EndDate = DateTime.Now.ToString("dd/MMM/yyyy");
                mASR.ServiceRequestList = BindServiceRequestList(0, "", "", 0, "", DateTime.Now.AddMonths(-1), DateTime.Now);
                return View(mASR);
            }
            else
            {
                return Redirect("../../Home");
            }


            
        }
        public ActionResult AllocateServiceRequest()
        {
            if (Session["UserId"] != null)
            {
                mASR.PendingServiceRequestList = GetServiceRequestList((int) ServiceRequestStatusEnum.Open, "");
                return View(mASR);
            }
            else
            {
                return Redirect("../../Home");
            }
            
        }

        public ActionResult ShowServiceRequestList(int ServiceRequestStatusId, string TicketNo)
        {
            string strError = string.Empty;
            AllocateServiceRequest objAllocateServiceRequest = new AllocateServiceRequest();

            try
            {
                objAllocateServiceRequest.PendingServiceRequestList = this.GetServiceRequestList(ServiceRequestStatusId, TicketNo);
                return PartialView("_ServiceSubCategoryPriceList", objAllocateServiceRequest);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }

            return Json(new { Error = strError, ServiceRequestList = objAllocateServiceRequest }, JsonRequestBehavior.AllowGet);
        }


        public List<ServiceRequest> GetServiceRequestList(int ServiceRequestStatusId, string TicketNo)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindServiceRequestForAllocation(ServiceRequestStatusId,TicketNo);
            return objLst.DataList;
        }

        public ActionResult GetAllServiceProviderForServiceAllocation(int BlockId, int PlaceId, int GovernotesId, int CountryId, int SubCategoryId, int BrokerId, int ServiceId)
        {
            List<AllocatedServiceProvider> objProviderlist = new List<AllocatedServiceProvider>();
           ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<AllocatedServiceProvider> objLst = new MethodOutput<AllocatedServiceProvider>();
            objLst = objSR.BindServiceProviderForAllocation(BlockId,PlaceId,GovernotesId,CountryId,SubCategoryId,BrokerId,ServiceId);
            objProviderlist= objLst.DataList;
            return Json(objProviderlist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveServiceAllocation(ServiceAllocation objSA)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                ServiceRequestService obj = new ServiceRequestService();
                objMO = obj.SaveServiceRequestAllocation(objSA);
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


        public ActionResult RespondToServiceAllocation()
        {
            if (Session["UserId"] != null && Session["User"]!=null)
            {
                Users objUser = (Users)Session["User"];
                if ( objUser.UserReferenceId != 0)
                {
                    mASR.AllocateServiceRequestList = GetAllocationList(objUser.UserReferenceId);
                    ViewBag.AllocateServiceRequestList = mASR;
                    return View(mASR);
                }
                else
                {
                    return Redirect("../../Home");
                }
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public List<ServiceRequest> GetAllocationList(int ServiceProviderId)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindAllocation(ServiceProviderId);
            ViewBag.ErrorMessage = objLst.ErrorMessage;
                return objLst.DataList;
        }


        public ActionResult AssignedServiceRequest()
        {
            if (Session["UserId"] != null)
            {
                mASR.AssignedServiceRequestList =BindAssignedServiceRequestList(Convert.ToInt32(Session["UserId"]));
                return View(mASR);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public List<ServiceRequest> BindAssignedServiceRequestList(int UserId)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindAssignedServiceRequest(UserId);
            return objLst.DataList;
        }

        public List<ServiceRequest> BindServiceRequestList(int ServiceRequestStatusId,
            string TicketNo, string AccountType, int BrokerId, string AccountSubType, DateTime StartDate, DateTime EndDate)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindAllServiceRequest(ServiceRequestStatusId, TicketNo, AccountType, BrokerId, AccountSubType, StartDate, EndDate);
            return objLst.DataList;
        }

        [HttpPost]
        public ActionResult SearchServiceRequest(SearchSR objSearchSR)
        {
            string strError = string.Empty;
            AllocateServiceRequest objSRSearchResult = new AllocateServiceRequest();
            List<ServiceRequest> objLst = new List<ServiceRequest>();
            try
            {


                objLst = BindServiceRequestList(objSearchSR.ServiceRequestStatusId, objSearchSR.TicketNo,
                    objSearchSR.AccountType, objSearchSR.BrokerId, objSearchSR.AccountSubType, objSearchSR.StartDate, objSearchSR.EndDate);
                objSRSearchResult.ServiceRequestList = objLst;
                return PartialView("_ServiceRequestList", objSRSearchResult);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }
            return Json(new { Error = strError }, JsonRequestBehavior.AllowGet);
        }






    }
}