using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEngine;
using DataServices;
using NationAssists.Models;

namespace NationAssists.Controllers
{
    public class RaiseServiceRequestController : Controller
    {
        mServiceRequest objSRModel = new mServiceRequest();
        // GET: RaiseServiceRequest
        public ActionResult Index()
        {
            if (Session["CustomerId"] != null && Session["CPRNumber"] != null)
            {

                objSRModel.CPRNumber = Convert.ToString(Session["CPRNumber"]);
                objSRModel.CustomerId= Convert.ToInt32(Session["CustomerId"]);
                objSRModel.ServiceRequestList = GetServiceRequestList(Convert.ToInt32(Session["CustomerId"]));
                return View(objSRModel);
            }
            else
            {
             return   Redirect("../Home");
            }
        }

        public ActionResult BindServiceType(int ServiceId)
        {
            MethodOutput<ServiceSubCategory> objMO = new MethodOutput<ServiceSubCategory>();
            ServiceSubCategoryService obj = new ServiceSubCategoryService();
            objMO = obj.GetServiceSubCategoryByServiceId(ServiceId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindGovernotes(int CountryId)
        {
            MethodOutput<Governotes> objMO = new MethodOutput<Governotes>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindGovernotes(CountryId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BindPlaces(int GovernotesId)
        {
            MethodOutput<Place> objMO = new MethodOutput<Place>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindPlaces(GovernotesId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindBlocks(int PlaceId)
        {
            MethodOutput<Block> objMO = new MethodOutput<Block>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindBlock(PlaceId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SubmitServiceRequest(ServiceRequest objSR)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            string TicketNo = string.Empty;

            try
            {
                ServiceRequestService obj = new ServiceRequestService();
                EmailServices objEmailService = new EmailServices();
                objMO = obj.SaveServiceRequest(objSR);
                IsSaved = objMO.ErrorMessage == string.Empty ? true : false;
                strMsg = objMO.ErrorMessage;
                if (IsSaved)
                {
                    TicketNo = objMO.Data;
                }
            }
            catch (Exception ex)
            {
                TicketNo = string.Empty;
                IsSaved = false;
                strMsg = ex.Message;
            }
            return Json(new { Result = IsSaved, Msg = strMsg, TicketNo=TicketNo}, JsonRequestBehavior.AllowGet);
        }

        public List<ServiceRequest> GetServiceRequestList(int CustomerId)
        {
            ServiceRequestService objSR = new ServiceRequestService();
            MethodOutput<ServiceRequest> objLst = new MethodOutput<ServiceRequest>();
            objLst = objSR.GetServiceRequestListByCustomer(CustomerId);
            return objLst.DataList;
        }

        public ActionResult BindChessisListByCPRNumber(string CPRNumber, int ServiceId)
        {
            MethodOutput<String> objMO = new MethodOutput<string>();
            ServiceRequestService obj = new ServiceRequestService();
            objMO = obj.BindChessisListByCPRNumber(CPRNumber,ServiceId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindVehicleDetailByCPRNumber(string CPRNumber, int ServiceId)
        {
            MethodOutput<VehicleDetail> objMO = new MethodOutput<VehicleDetail>();
            ServiceRequestService obj = new ServiceRequestService();
            objMO = obj.BindVehicleDetailByCPRNumber(CPRNumber, ServiceId);
            return Json(objMO.Data, JsonRequestBehavior.AllowGet);
        }
    }
}