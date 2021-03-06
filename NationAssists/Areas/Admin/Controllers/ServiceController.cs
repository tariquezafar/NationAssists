using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NationAssists.Areas.Admin.Models;
using DataEngine;
using DataServices;
using System.IO;
using System.Configuration;

namespace NationAssists.Areas.Admin.Controllers
{
    public class ServiceController : Controller
    {
        private static Random random = new Random();
        mServiceSubCategory objServiceSubCategory = new mServiceSubCategory();
        mServiceProvider objServiceProiders = new mServiceProvider();
        mServiceProviderPrice objServiceProviderPrice = new mServiceProviderPrice();
        mServicePrice objServicePrice = new mServicePrice();
        mServiceProviderServiceArea objServiceProviderServiceArea = new mServiceProviderServiceArea();

        // GET: Admin/Role
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



        #region Service Sub Category
        public ActionResult ManageServiceSubCategory()
        {
            if (Session["UserId"] != null)
            {

                objServiceSubCategory.ServiceId = 0;
                objServiceSubCategory.Name = "";
                objServiceSubCategory.IsActive = false;
                objServiceSubCategory.ServiceSubCategroyList = GetServiceSubCategoryByServiceId(0);
                return View(objServiceSubCategory);

            }
            else
            {
                return Redirect("../../Home");
            }
        }
        [HttpPost]
        public ActionResult SaveServiceSubCategory(ServiceSubCategory objSSC)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                ServiceSubCategoryService obj = new ServiceSubCategoryService();
                objMO = obj.SaveServiceSubCategory(objSSC);
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


        [HttpPost]
        public ActionResult BindServiceSubCategoryByServiceId(int ServiceId)
        {
            string strError = string.Empty;
            List<ServiceSubCategory> objList = new List<ServiceSubCategory>();

            try
            {
                objList = this.GetServiceSubCategoryByServiceId(ServiceId);
                //  return PartialView("_ServiceSubCategoryList.cshtml",objList);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(objList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ServiceSubCategoryByServiceId(int ServiceId)
        {
            string strError = string.Empty;
            mServiceSubCategory objSSC = new mServiceSubCategory();
            List<ServiceSubCategory> objList = new List<ServiceSubCategory>();

            try
            {
                objList = this.GetServiceSubCategoryByServiceId(ServiceId);
                objSSC.ServiceSubCategroyList = objList;
                return PartialView("_ServiceSubCategoryList", objSSC);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { Error=strError}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteServiceSubCategory(int ServiceId)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                ServiceSubCategoryService obj = new ServiceSubCategoryService();
                objMO = obj.DeleteServiceSubCategory(ServiceId);
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

        public List<ServiceSubCategory> GetServiceSubCategoryByServiceId(int ServiceId)
        {
            MethodOutput<ServiceSubCategory> objMO = new MethodOutput<ServiceSubCategory>();
            ServiceSubCategoryService obj = new ServiceSubCategoryService();
            objMO = obj.GetServiceSubCategoryByServiceId(ServiceId);
            return objMO.DataList;
        }

        #endregion

        #region Service Provider
        [HttpPost]
        public ActionResult SaveServiceProvider()
        {

            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            string strFolder = string.Empty;

            try
            {
                strFolder = ConfigurationManager.AppSettings["ServiceProviderDocumentFolder"].ToString();
                ServiceProvider objServiceProvider = new ServiceProvider();
                #region Create Service Provider Object
                objServiceProvider.ServiceProviderId = Convert.ToInt32(Request.Form["ServiceProviderId"]);
                objServiceProvider.ContactPersonName = Convert.ToString(Request.Form["ContactPersonName"]);
                objServiceProvider.ContactPersonNo= Convert.ToString(Request.Form["ContactPersonNo"]);
                objServiceProvider.CRNumber= Convert.ToString(Request.Form["CRNumber"]);
                objServiceProvider.EmailId = Convert.ToString(Request.Form["EmailId"]);
                objServiceProvider.EscalationPersonContactNo = Convert.ToString(Request.Form["EscalationPersonContactNo"]);
                objServiceProvider.EscalationPersonName = Convert.ToString(Request.Form["EscalationPersonName"]);
                objServiceProvider.FirstName = Convert.ToString(Request.Form["FirstName"]);
                objServiceProvider.LastName = Convert.ToString(Request.Form["LastName"]);
                objServiceProvider.MiddleName = Convert.ToString(Request.Form["MiddleName"]);
                objServiceProvider.MobileNo = Convert.ToString(Request.Form["MobileNo"]);
                objServiceProvider.OfficeLocationAddress = Convert.ToString(Request.Form["OfficeLocationAddress"]);
                objServiceProvider.Password = Convert.ToString(Request.Form["Password"]);
                objServiceProvider.PhoneNo = Convert.ToString(Request.Form["PhoneNo"]);
                objServiceProvider.SelectedServiceOpted = Convert.ToString(Request.Form["SelectedServiceOpted"]);
                objServiceProvider.ServiceProviderAgreementFromDate =Convert.ToDateTime( Request.Form["ServiceProviderAgreementFromDate"]);
                objServiceProvider.ServiceProviderAgreementToDate =Convert.ToDateTime( Request.Form["ServiceProviderAgreementToDate"]);
                objServiceProvider.PriceOption = Convert.ToString(Request.Form["PriceOption"]);


                List<ServiceProviderDocument> lstServiceProviderDocuments = new List<ServiceProviderDocument>();
                if (objServiceProvider.ServiceProviderId != 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;

                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];
                            // Get the complete folder path and store the file inside it.      
                            string strFileName = DateTime.Now.ToFileTime().ToString() + "_" + file.FileName;
                            file.SaveAs(Server.MapPath(strFolder + strFileName));
                            lstServiceProviderDocuments.Add(new ServiceProviderDocument { DocumentPath = strFileName });


                        }

                        objServiceProvider.ServiceProviderDocuments = lstServiceProviderDocuments;
                    }
                    else
                    {
                        ServiceProvider objSP = GetAllServiceProvider(objServiceProvider.ServiceProviderId, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)[0];
                        objServiceProvider.ServiceProviderDocuments = objSP.ServiceProviderDocuments;
                    }
                }
                #endregion
                ServiceProviderService obj = new ServiceProviderService();
                objMO = obj.SaveServiceProvider(objServiceProvider);
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

        public ActionResult ManageServiceProvider()
        {
            if (Session["UserId"] != null)
            {
                objServiceProiders.Editable = true;
                objServiceProiders.ServiceProviderServicesOpted = objServiceProiders.GetAllServices();
                objServiceProiders.ServiceProviderList = GetAllServiceProvider(0,string.Empty,string.Empty,string.Empty,string.Empty,string.Empty);
                return View(objServiceProiders);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public List<ServiceProvider> GetAllServiceProvider(int ServiceProviderId, string CompanyName, string MobileNo, string PhoneNo, string EmailId, string CRNumber)
        {
            MethodOutput<ServiceProvider> objMO = new MethodOutput<ServiceProvider>();
            ServiceProviderService obj = new ServiceProviderService();
            objMO = obj.GetAllServiceProvider(ServiceProviderId, CompanyName, MobileNo, PhoneNo, EmailId, CRNumber);
            return objMO.DataList;
        }

        public ActionResult BindServiceProvider(int ServiceProviderId, string CompanyName, string MobileNo, string PhoneNo, string EmailId, string CRNumber)
        {
            string strError = string.Empty;
            List<ServiceProvider> objList = new List<ServiceProvider>();
            try
            {
                objList = this.GetAllServiceProvider(ServiceProviderId, CompanyName, MobileNo, PhoneNo, EmailId, CRNumber);
                mServiceProvider objSP = new mServiceProvider();
                objSP.ServiceProviderList = objList;
                return PartialView("_ServiceProviderList", objSP);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }
            return Json(new { ErrorMsg = strError }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteServiceProvider(int ServiceProviderId)
        {
        
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                ServiceProviderService obj = new ServiceProviderService();
                objMO = obj.DeleteServiceServiceProvider(ServiceProviderId);
                IsSaved = objMO.ErrorMessage == string.Empty ? true : false;
                strMsg = objMO.ErrorMessage;

            }
            catch (Exception ex)
            {

                IsSaved = false;
                strMsg = ex.Message;
            }

            return Json(new { Result = IsSaved, Msg = strMsg,IsReferenceDataExist=objMO.ErrorMessage==SqlOutput.Reference_Data? true:false }, JsonRequestBehavior.AllowGet);


        }



        [HttpGet]
        public ActionResult GetServiceProviderDetail(int ServiceProviderId)
        {
            List<ServiceProvider> objServiceProvider = GetAllServiceProvider(ServiceProviderId,string.Empty,string.Empty,string.Empty,string.Empty,string.Empty);
            return Json(objServiceProvider[0], JsonRequestBehavior.AllowGet);
        }



        public ActionResult ServiceProviderList()
        {
            if (Session["UserId"] != null)
            {
                objServiceProiders.Editable = false;
                objServiceProiders.ServiceProviderServicesOpted = objServiceProiders.GetAllServices();
                objServiceProiders.ServiceProviderList = GetAllServiceProvider(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                return View(objServiceProiders);
            }
            else
            {
                return Redirect("../../Home");
            }
        }


        #endregion



        #region Service Category Price

        public ActionResult ManageServiceProviderPrice()
        {
            if (Session["UserId"] != null)
            {
                return View(objServiceProviderPrice);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public ActionResult BindServices(int ServiceProviderId)
        {
            MethodOutput<ServiceProvider> objMO = new MethodOutput<ServiceProvider>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindServicesByServiceProviderId(ServiceProviderId);
            return Json( objMO.Data,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowServiceSubCategoryPriceList(int ServiceProviderId,int ServiceId)
        {
           
            string strError = string.Empty;
            mServiceProviderPrice objServProviderPrice = new mServiceProviderPrice();

            try
            {
                objServProviderPrice = this.GetServiceSubCategoryPriceList(ServiceProviderId, ServiceId);

                if (objServProviderPrice.PriceOption == "SB")
                {
                    return PartialView("_ServiceSubCategoryPriceList", objServProviderPrice);
                }
                
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { Error = strError, ServiceProviderPrice= objServProviderPrice }, JsonRequestBehavior.AllowGet);
        }

        public mServiceProviderPrice GetServiceSubCategoryPriceList( int ServiceProviderId, int ServiceId)
        {
            mServiceProviderPrice objSPP = new mServiceProviderPrice();
            List<ServiceSubCategoryPrice> objServiceSubCategory = new List<ServiceSubCategoryPrice>();
            MethodOutput<ServiceSubCategoryPrice> objMO = new MethodOutput<ServiceSubCategoryPrice>();
            ServiceProviderPriceService obj = new ServiceProviderPriceService();
            objMO = obj.GetServiceSubCategoryPriceList(ServiceProviderId, ServiceId);
            objServiceSubCategory= objMO.DataList;
            objSPP.PriceOption = objServiceSubCategory[0].PriceOption;
            objSPP.PriceCount = objServiceSubCategory[0].PriceCount;
            objSPP.ServiceSubCategoryPriceList= objServiceSubCategory[0].SubCategoryPriceList;

            return objSPP;
        }

        public ActionResult SaveServiceProviderPrice(ServiceSubCategoryPrice serviceSubCategoryPrice)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                ServiceProviderPriceService obj = new ServiceProviderPriceService();
                objMO = obj.SaveSubCategoryPrice(serviceSubCategoryPrice);
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


        #region Service Price Master
        public ActionResult ManageServicePrice()
        {
            if (Session["UserId"] != null)
            {
                return View(objServicePrice);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        [HttpPost]
        public ActionResult SavePrices(ServicePrice objPrice)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            try
            {
                ServicePriceService obj = new ServicePriceService();
                objMO = obj.SavePrices(objPrice);
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


        public ActionResult ShowServicePriceList(int ServiceId)
        {

            string strError = string.Empty;
            List<ServicePrice> objList = new List<ServicePrice>();

            try
            {
                objList = this.GetServicePriceList(ServiceId);

                objServicePrice.ServicePriceList = objList;
                return PartialView("_ServicePriceList", objServicePrice);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { Msg = strError }, JsonRequestBehavior.AllowGet);
        }
        public List<ServicePrice> GetServicePriceList( int ServiceId)
        {
            MethodOutput<ServicePrice> objMO = new MethodOutput<ServicePrice>();
            ServicePriceService obj = new ServicePriceService();
            objMO = obj.GetServicePrices( ServiceId);
            return objMO.DataList;
        }

        #endregion

        #region Service Provider Service Area

        public ActionResult ManageServiceProviderServiceArea()
        {
            if (Session["UserId"] != null)
            {
                return View(objServiceProviderServiceArea);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public ActionResult BindServiceCategory(int ServiceId)
        {
            MethodOutput<ServiceSubCategory> objMO = new MethodOutput<ServiceSubCategory>();
            ServiceSubCategoryService obj = new ServiceSubCategoryService();
            objMO = obj.GetServiceSubCategoryByServiceId(ServiceId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveServiceAreaMapping(ServiceProviderServiceArea objSA)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                ServiceAreaMappingServices obj = new ServiceAreaMappingServices();
                objMO = obj.SaveServiceAreaMapping(objSA);
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

        public ActionResult GetAllServiceArea(int ServiceProviderId, int ServiceId, int SubCategoryId)
        {
            ServiceProviderServiceArea objSPP = new ServiceProviderServiceArea();
            MethodOutput<ServiceProviderServiceArea> objMO = new MethodOutput<ServiceProviderServiceArea>();
            ServiceAreaMappingServices obj = new ServiceAreaMappingServices();
            objMO = obj.GetAllServiceArea(ServiceProviderId, ServiceId, SubCategoryId);
            objSPP.ServiceAreas = objMO.DataList;
            return Json( objSPP,JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindServiceOptedByServiceProviderId(int ServiceId, int ServiceProviderId)
        {
            MethodOutput<ServiceSubCategory> objMO = new MethodOutput<ServiceSubCategory>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindServiceCategoryByProviderId(ServiceId,ServiceProviderId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Service
        public ActionResult SaveServices(Service objService)
{
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            try
            {
                ServiceSubCategoryService obj = new ServiceSubCategoryService();

             
                objMO = obj.SaveService(objService);
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

        public List<Service> BindService()
        {
            MethodOutput<Service> objMO = new MethodOutput<Service>();
            ServiceSubCategoryService obj = new ServiceSubCategoryService();
            objMO = obj.GetAllServices();
            return objMO.DataList;
        }


        public ActionResult ManageService()
        {
            mService objService = new mService();
            objService.ServiceList = BindService();

            return View(objService);
        }
        #endregion
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        




    }
}