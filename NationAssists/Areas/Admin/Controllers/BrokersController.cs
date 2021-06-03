using DataEngine;
using DataServices;
using ICardPrinter.Areas.Admin.Models;
using NationAssists.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Areas.Admin.Controllers
{
    public class BrokersController : Controller
    {
        // GET: Admin/Brokers
        mBroker objBrokerModel = new mBroker();
        public ActionResult ManageBroker()
        {
            objBrokerModel.BrokerOptedServices = objBrokerModel.GetAllServices();
            objBrokerModel.BrokerList = this.GetAllBrokers(0,"");
            return View(objBrokerModel);
        }

        [HttpPost]
        public ActionResult SaveBroker()
        {

            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;
            string strFolder = string.Empty;

            try
            {
                strFolder = ConfigurationManager.AppSettings["BrokerDocumentFolder"].ToString();
                Broker objBroker = new Broker();
                #region Create Service Provider Object
                objBroker.BrokerId = Convert.ToInt32(Request.Form["BrokerId"]);
                objBroker.Contact_Person_Name = Convert.ToString(Request.Form["Contact_Person_Name"]);
                objBroker.Broker_Type = Convert.ToString(Request.Form["Broker_Type"]);
                objBroker.Broker_Name = Convert.ToString(Request.Form["Broker_Name"]);
                objBroker.Contact_Person_Contact_No = Convert.ToString(Request.Form["Contact_Person_Contact_No"]);
                objBroker.Commission_Paybable = Convert.ToDecimal(Request.Form["Commission_Paybable"]);
                objBroker.EmailId = Convert.ToString(Request.Form["EmailId"]);
                objBroker.Escalation_Person_Name = Convert.ToString(Request.Form["Escalation_Person_Name"]);
                objBroker.Escalation_Person_Contact_No = Convert.ToString(Request.Form["Escalation_Person_Contact_No"]);
                objBroker.Remarks = Convert.ToString(Request.Form["Remarks"]);
                objBroker.Payment_Terms_Credit_Terms = Convert.ToString(Request.Form["Payment_Terms_Credit_Terms"]);
                objBroker.Estimated_Business_In_A_Year = Convert.ToString(Request.Form["Estimated_Business_In_A_Year"]);
                objBroker.MobileNo = Convert.ToString(Request.Form["MobileNo"]);
                objBroker.Office_Location_Address = Convert.ToString(Request.Form["Office_Location_Address"]);
                objBroker.Office_Postal_Address = Convert.ToString(Request.Form["Office_Postal_Address"]);
                objBroker.IsActive = Convert.ToBoolean(Request.Form["IsActive"]);
                objBroker.SelectedServiceOpted = Convert.ToString(Request.Form["SelectedServiceOpted"]);
                objBroker.Agreement_Start_Date = Convert.ToDateTime(Request.Form["Agreement_Start_Date"]);
                objBroker.Agreement_End_Date = Convert.ToDateTime(Request.Form["Agreement_End_Date"]);
                objBroker.Price_Option = Convert.ToString(Request.Form["Price_Option"]);


                List<BrokerDocument> lstBrokerDocuments = new List<BrokerDocument>();
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        // Get the complete folder path and store the file inside it.      
                        string strFileName = DateTime.Now.ToFileTime().ToString() + "_" + file.FileName;
                        file.SaveAs(Server.MapPath(strFolder + strFileName));
                        lstBrokerDocuments.Add(new BrokerDocument { DocumentPath = strFileName });


                    }

                    objBroker.BrokerDocuments = lstBrokerDocuments;
                }
                else
                {
                   // ServiceProvider objSP = GetAllServiceProvider(objBroker.ServiceProviderId)[0];
                   // objBroker.ServiceProviderDocuments = objSP.ServiceProviderDocuments;
                }
                #endregion
                BrokerService obj = new BrokerService();
                objMO = obj.SaveBroker(objBroker);
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

        public List<Broker> GetAllBrokers(int BrokerId, string BrokerType)
        {
            MethodOutput<Broker> objMO = new MethodOutput<Broker>();
            BrokerService obj = new BrokerService();
            objMO = obj.GetAllBrokers(BrokerId,BrokerType);
            return objMO.Data;
        }

        [HttpGet]
        public ActionResult GetBrokerDetail(int BrokerId)
        {
            List<Broker> objBroker = GetAllBrokers(BrokerId,"");
            return Json(objBroker[0], JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteBroker(int BrokerId)
        {

            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                BrokerService obj = new BrokerService();
                objMO = obj.DeleteBroker(BrokerId);
                IsSaved = objMO.ErrorMessage == string.Empty ? true : false;
                strMsg = objMO.ErrorMessage;

            }
            catch (Exception ex)
            {

                IsSaved = false;
                strMsg = ex.Message;
            }

            return Json(new { Result = IsSaved, Msg = strMsg, IsReferenceDataExist = objMO.ErrorMessage == SqlOutput.Reference_Data ? true : false }, JsonRequestBehavior.AllowGet);


        }

    }
}