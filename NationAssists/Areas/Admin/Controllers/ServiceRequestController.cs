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

        #region Service Request Listing
        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                mASR.StartDate = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                mASR.EndDate = DateTime.Now.ToString("dd/MMM/yyyy");
                int UserId = Convert.ToInt32(Session["UserId"]);
                mASR.UserId= Convert.ToInt32(Session["UserId"]);
                mASR.ServiceRequestList = BindServiceRequestList(0, "", "", 0, "", DateTime.Now.AddMonths(-1), DateTime.Now,UserId);
                return View(mASR);
            }
            else
            {
                return Redirect("../../Home");
            }    
        }

        public List<ServiceRequest> BindServiceRequestList(int ServiceRequestStatusId,
           string TicketNo, string AccountType, int BrokerId, string AccountSubType, DateTime StartDate, DateTime EndDate, int UserId)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindAllServiceRequest(ServiceRequestStatusId, TicketNo, AccountType, BrokerId, AccountSubType, StartDate, EndDate, UserId);
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
                int UserId = Convert.ToInt32(Session["UserId"]);

                objLst = BindServiceRequestList(objSearchSR.ServiceRequestStatusId, objSearchSR.TicketNo,
                    objSearchSR.AccountType, objSearchSR.BrokerId, objSearchSR.AccountSubType, objSearchSR.StartDate, objSearchSR.EndDate, UserId);
                objSRSearchResult.ServiceRequestList = objLst;
                return PartialView("_ServiceRequestList", objSRSearchResult);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }
            return Json(new { Error = strError }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Service Request Service Provider Allocation
        public ActionResult AllocateServiceRequest()
        {
            if (Session["UserId"] != null)
            {
                mASR.PendingServiceRequestList = GetPendingServiceRequestList((int) ServiceRequestStatusEnum.Open, "",string.Empty,string.Empty,string.Empty);
                return View(mASR);
            }
            else
            {
                return Redirect("../../Home");
            }
            
        }

        public ActionResult ShowServiceRequestList( string TicketNo, string CustomerName, string ContactNo, string EmailId)
        {
            string strError = string.Empty;
            AllocateServiceRequest objAllocateServiceRequest = new AllocateServiceRequest();

            try
            {
                objAllocateServiceRequest.PendingServiceRequestList = this.GetPendingServiceRequestList(Convert.ToInt32( ServiceRequestStatusEnum.Open), TicketNo, CustomerName, ContactNo, EmailId);
                return PartialView("_PendingServiceRequestList", objAllocateServiceRequest);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }

            return Json(new { Error = strError, ServiceRequestList = objAllocateServiceRequest }, JsonRequestBehavior.AllowGet);
        }


        public List<ServiceRequest> GetPendingServiceRequestList(int ServiceRequestStatusId, string TicketNo, string CustomerName, string ContactNo, string EmailId)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindServiceRequestForAllocation(ServiceRequestStatusId,TicketNo, CustomerName, ContactNo, EmailId);
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
                objSA.AcceptedBy= Convert.ToInt32(Session["UserId"]);
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

        #endregion


        #region Respond To Service Request Allocation
        public ActionResult RespondToServiceAllocation()
        {
            if (Session["UserId"] != null && Session["User"]!=null)
            {
                Users objUser = (Users)Session["User"];
                if ( objUser.UserReferenceId != 0)
                {
                    mASR.AllocateServiceRequestList = GetAllocationList(objUser.UserReferenceId);
                    mASR.UserList = GetAssignedUser(Convert.ToInt32(Session["UserId"]));
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

        #endregion


        #region Assigned Service Request 
        public List<ServiceRequest> BindAssignedServiceRequestList(int UserId)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.BindAssignedServiceRequest(UserId);
            return objLst.DataList;
        }

        #endregion

        #region Raise Service Request

        public ActionResult RaiseServiceRequest()
        {
            return View(mASR);
        }

        public ActionResult SearchCustomerStatus(string CPRNumber)
        {
            ServiceRequestService objSRS = new ServiceRequestService();
            MethodOutput<CustomerStatus> objCUS = new MethodOutput<CustomerStatus>();
            objCUS = objSRS.GetCustomerStatus(CPRNumber);
            return Json(objCUS.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindServiceByCPRNumber(string CPRNumber)
        {
            ServiceRequestService objSRS = new ServiceRequestService();
            MethodOutput<Service> objCUS = new MethodOutput<Service>();
            objCUS = objSRS.BindServicesByCPRNumber(CPRNumber);
            return Json(objCUS.DataList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BindCustomerDetail(string CPRNumber)
        {
            string strError = string.Empty;
            MethodOutput<Customer> objOutput = new MethodOutput<Customer>();
            ServiceRequestService objSRS = new ServiceRequestService();
            try
            {
                objOutput = objSRS.GetCustomerDetail(CPRNumber);
                Customer objCust = new Customer();
                objCust = objOutput.DataList[0];
                return PartialView("_CustomerDetail", objCust);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }
            return Json(new { Error = strError },JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllServiceProviderByServiceRequest(Int64 ServiceRequestId)
        {
            MethodOutput<ServiceProvider> objOutput = new MethodOutput<ServiceProvider>();
            ServiceRequestService objSRS = new ServiceRequestService();
            objOutput = objSRS.GetAllServiceProviderByServiceRequest(ServiceRequestId);
            return Json(objOutput.DataList, JsonRequestBehavior.AllowGet);


        }

        public ActionResult BindAssignedToUser(int UserId)
        {
            MethodOutput<Users> objOutput = new MethodOutput<Users>();
            ServiceRequestService objSRS = new ServiceRequestService();
            objOutput = objSRS.GetAllAssignToUser(UserId);
            return Json(objOutput.DataList, JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult UpdateServiceRequest(ServiceAllocation objSA)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                objSA.AcceptedBy = Convert.ToInt32(Session["UserId"]);
                ServiceRequestService obj = new ServiceRequestService();
                objMO = obj.UpdateServiceRequest(objSA);
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


        public ActionResult GetAllServiceRemarks(Int64 ServiceRequestId)
        {

            MethodOutput<ServiceRemarks> objOutput = new MethodOutput<ServiceRemarks>();
            ServiceRequestService objSRS = new ServiceRequestService();
            objOutput = objSRS.GetAllServiceRemarks(ServiceRequestId);
            return Json(objOutput.DataList, JsonRequestBehavior.AllowGet);
        }

        public List<Users> GetAssignedUser(int RefernceId)

        {

            UserServices objCS = new UserServices();
            return objCS.GetUserByReference(RefernceId).DataList;

        }



    }
}