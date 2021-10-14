using DataEngine;
using DataServices;
using NationAssists.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;
using System.Threading.Tasks;
using NationAssists.Models;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace NationAssists.Areas.Admin.Controllers
{
    public class BrokersController : Controller
    {
        // GET: Admin/Brokers
        mBroker objBrokerModel = new mBroker();
        mMembership objMM = new mMembership();

        #region Broker
        public ActionResult ManageBroker()
        {
            if (Session["UserId"] != null)
            {
                objBrokerModel.BrokerOptedServices = objBrokerModel.GetAllServices();
               
                objBrokerModel.BrokerList = this.GetAllBrokers(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                return View(objBrokerModel);
            }
            else
            {
                return Redirect("../../Home");
            }
        }


        public ActionResult SourceList()
        {
            if (Session["UserId"] != null)
            {
                objBrokerModel.BrokerOptedServices = objBrokerModel.GetAllServices();

                objBrokerModel.BrokerList = this.GetAllBrokers(0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                return View(objBrokerModel);
            }
            else
            {
                return Redirect("../../Home");
            }
        }
        [HttpPost]
        [ValidateInput(false)]
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
                objBroker.CRNumber = Convert.ToString(Request.Form["CRNumber"]);
                objBroker.CRExpiryDate = Convert.ToDateTime(Request.Form["CRExpiryDate"]);
                objBroker.VATRegistrationNumber = Convert.ToString(Request.Form["VATRegistrationNumber"]);
                objBroker.Landline = Convert.ToString(Request.Form["Landline"]);
                objBroker.EscalationLandlineNo = Convert.ToString(Request.Form["EscalationLandlineNo"]);
                objBroker.DeclarationPeriod = Convert.ToInt32(Request.Form["DeclarationPeriod"] == "" ? null : Request.Form["DeclarationPeriod"]);
                objBroker.BranchLocation = Convert.ToString(Request.Form["BranchLocation"]);
                objBroker.Password = Convert.ToString(Request.Form["Password"]);
                string strBrokerCommission = Convert.ToString(Request.Form["BrokerCommissionList"]);
                List<BrokerServiceCommissionPayable> objList = new List<BrokerServiceCommissionPayable>();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                objList = jss.Deserialize<List<BrokerServiceCommissionPayable>>(strBrokerCommission);
                objBroker.lstBrokerServiceCommissionPayable = objList;



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

        public List<Broker> GetAllBrokers(int BrokerId, string BrokerType, string SourceName, string CRNumber, string MobileNo, string EmailId)
        {
            MethodOutput<Broker> objMO = new MethodOutput<Broker>();
            BrokerService obj = new BrokerService();
            objMO = obj.GetAllBrokers(BrokerId, BrokerType, SourceName, CRNumber, MobileNo, EmailId);
            return objMO.DataList;
        }

        [HttpGet]
        public ActionResult GetBrokerDetail(int BrokerId)
        {
            List<Broker> objBroker = GetAllBrokers(BrokerId, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            return Json(objBroker[0], JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchSource(string BrokerType, string SourceName, string CRNumber, string MobileNo, string EmailId)
        {
            string strError = string.Empty;
            List<Broker> objLst = new List<Broker>();
            try
            {
                objLst = this.GetAllBrokers(0, BrokerType, SourceName, CRNumber, MobileNo, EmailId);
                mBroker objMB = new mBroker();
                objMB.BrokerList = objLst;
                return PartialView("_BrokerList", objMB);
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { ErrorMsg = strError }, JsonRequestBehavior.AllowGet);
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
        #endregion

        #region Broker Price

        public ActionResult ManageBrokerPrice()
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

        [HttpGet]
        public ActionResult BindBroker(string UserType)
        {
            CommonServices objComm = new CommonServices();
            UserType = UserType == "SP" ? "" : UserType;
            return Json(objComm.BindServiceProvider(UserType).DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindServices(int BrokerId)
        {
            MethodOutput<Service> objMO = new MethodOutput<Service>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindServiceOptedByBrokerId(BrokerId);
            return Json(objMO.DataList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowBrokerPriceList(int BrokerId, int ServiceId)
        {

            string strError = string.Empty;
            mBrokerPrice objBrokerPrice = new mBrokerPrice();

            try
            {
                objBrokerPrice = this.GetBrokerPriceList(BrokerId, ServiceId);

                if (objBrokerPrice.PriceOption == "SB")
                {
                 return PartialView("_BrokerPriceList", objBrokerPrice);
                    
                }

            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { Error = strError, ServiceProviderPrice = objBrokerPrice }, JsonRequestBehavior.AllowGet);
        }

        public mBrokerPrice GetBrokerPriceList(int BrokerId, int ServiceId)
        {
            mBrokerPrice objSPP = new mBrokerPrice();
            List<BrokerPrice> objServiceSubCategory = new List<BrokerPrice>();
            MethodOutput<BrokerPrice> objMO = new MethodOutput<BrokerPrice>();
            BrokerService obj = new BrokerService();
            objMO = obj.GetBrokerPriceList(BrokerId, ServiceId);
            objServiceSubCategory = objMO.DataList;
            objSPP.PriceOption = objServiceSubCategory[0].PriceOption;
            objSPP.PriceCount = objServiceSubCategory[0].PriceCount;
            objSPP.BrokerPriceList = objServiceSubCategory[0].BrokerPriceList;

            return objSPP;
        }

        [HttpPost]
        public ActionResult SaveBrokerPrice(BrokerPrice brokerPrice)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                BrokerService obj = new BrokerService();
                objMO = obj.SaveBrokerPrice(brokerPrice);
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

        #region Membership

        public ActionResult ManageMembership()
        {
            if (Session["UserId"] != null && Session["User"] != null)
            {
                Users objUser = (Users)Session["User"];
              
                objMM.MembershipList = BindMemberShip(objUser.UserReferenceId, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

                return View(objMM);
            }
            else

                return Redirect("../../Home");
        }

        public ActionResult SaveMemberShip(Membership objMembership)
        {
            MethodOutput<string> objMO = new MethodOutput<string>();
            bool IsSaved = false;
            string strMsg = String.Empty;

            try
            {
                MembershipServices obj = new MembershipServices();
                objMembership.CreatedBy = Convert.ToInt32(Session["UserId"]);
                objMembership.ModifiedBy = Convert.ToInt32(Session["UserId"]);
                objMO = obj.SaveMembership(objMembership);
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
        public ActionResult GetMembershipDetail(Int64 MembershipId)
        {
            List<Membership> objMember = BindMemberShip(0, 0, MembershipId, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            return Json(objMember[0], JsonRequestBehavior.AllowGet);
        }

        public List<Membership> BindMemberShip(int SourceId, int PackageId, Int64 MembershipId, string CPRNumber, string PolicyType, string PolicyNo, string InsuredName, string MobileNo, string EmailId, string VehicleRegistrationNo, string ChassisNo, string SourceType)
        {

            List<Membership> objMembershiplist = new List<Membership>();
            MethodOutput<Membership> objMO = new MethodOutput<Membership>();
            MembershipServices obj = new MembershipServices();
            objMO = obj.GetAllMemberShip(SourceId, PackageId, MembershipId, CPRNumber, PolicyType, PolicyNo, InsuredName
            , MobileNo, EmailId, VehicleRegistrationNo, ChassisNo, SourceType);
            objMembershiplist = objMO.DataList;


            return objMembershiplist;
        }

        public ActionResult SearchMemberShip(string SourceType,int SourceId, int PackageId, string CPRNumber, string PolicyType, string PolicyNo, string InsuredName, string MobileNo, string EmailId, string VehicleRegistrationNo, string ChassisNo)
        {
            string strError = string.Empty;
            try
            {
                List<Membership> objList = new List<Membership>();
                objList = this.BindMemberShip(SourceId, PackageId, 0, CPRNumber, PolicyType, PolicyNo, InsuredName, MobileNo,EmailId,VehicleRegistrationNo,ChassisNo,SourceType);
                mMembership objMem = new mMembership();
                objMem.MembershipList = objList;
                return PartialView("_MembershipList", objMem);

            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }
            return Json(new { Error=strError},JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateCertificate( int PackageId,DateTime ? IssueDate, DateTime ? EffectiveDate, DateTime ? ExpiryDate, string MembershipReferenceNo,string InsuredName, string RiskAddress,string PackageName,string VehicleRegistrationNo,string VehicleMake,string ChassisNo,int? VehicleYear)
        {
            string strError = string.Empty;
            try
            {
                string CertificateFolderPath = ConfigurationManager.AppSettings["CerificateTemplatePath"];
                string CertificatePath = string.Empty;
                if (PackageId == 1)
                {
                    CertificatePath = Path.Combine(CertificateFolderPath, "RSA_Certificate.html");
                }
                else if (PackageId == 11)
                {
                    CertificatePath = Path.Combine(CertificateFolderPath, "HA_Certificate.html");
                }
                else
                {
                    CertificatePath = Path.Combine(CertificateFolderPath, "RSACR_Certificate.html");
                }

                using (StreamReader str = new StreamReader(CertificatePath))
                {
                    string HtmlText = str.ReadToEnd();

                    HtmlText = HtmlText.Replace("@IssueDate", Convert.ToDateTime(IssueDate).ToString("dd-MMM-yyyy"));
                    HtmlText = HtmlText.Replace("@FromDate", Convert.ToDateTime(EffectiveDate).ToString("dd-MMM-yyyy"));
                    HtmlText = HtmlText.Replace("@ToDate", Convert.ToDateTime(ExpiryDate).ToString("dd-MMM-yyyy"));
                    HtmlText = HtmlText.Replace("@CertificateNo", MembershipReferenceNo);
                    HtmlText = HtmlText.Replace("@CustomerName", InsuredName.ToUpper());
                    if (PackageId == 11)
                    {
                        HtmlText = HtmlText.Replace("@RiskAddress", RiskAddress);
                    }
                    else
                    {
                        if (PackageId != 1)
                        {
                            HtmlText = HtmlText.Replace("@CarReplacement", PackageName);
                        }
                        HtmlText = HtmlText.Replace("@VehicleRegistrationNo", VehicleRegistrationNo);
                        HtmlText = HtmlText.Replace("@Make", VehicleMake);
                        HtmlText = HtmlText.Replace("@ChassisNo", ChassisNo);
                        HtmlText = HtmlText.Replace("@ManufacturingYear", VehicleYear.ToString());

                    }

                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 40f, 0f);
                    //string imageFilePath = Server.MapPath("../../") + "/images/Watermark1.png";

                    //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
                    ////jpg.ScaleToFit(3000, 770);

                    ////If you want to choose image as background then,

                    //jpg.Alignment = iTextSharp.text.Image.DIV;

                    //If you want to give absolute/specified fix position to image.
                   // jpg.SetAbsolutePosition(100, 69);
                    
                    using (MemoryStream stream = new System.IO.MemoryStream())
                    {
                        try
                        {
                            StringReader sr = new StringReader(HtmlText);

                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                            pdfDoc.Open();
                     

                            iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                            //pdfDoc.Add(jpg);
                            pdfDoc.Close();

                            byte[] arrDoc = stream.ToArray();
                            return File(arrDoc, "application/pdf", MembershipReferenceNo + ".pdf");

                        }
                        catch (Exception ex)
                        {
                            pdfDoc.Close();
                            strError = ex.Message;
                        }
                        finally
                        {
                            stream.Flush();
                            stream.Close();
                        }
                    }

                    str.Close();
                }
            }
            catch (Exception ex)
            {

                strError = ex.Message;
            }

            return Json(new { Error =strError},JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendGeneratedCertificate(Membership objMembership)
        {
            string strError = string.Empty;
            try
            {
                string CertificateFolderPath = ConfigurationManager.AppSettings["CerificateTemplatePath"];
                string CertificatePath = string.Empty;
                if (objMembership.PackageId == 1)
                {
                    CertificatePath = Path.Combine(CertificateFolderPath, "RSA_Certificate.html");
                }
                else if (objMembership.PackageId == 11)
                {
                    CertificatePath = Path.Combine(CertificateFolderPath, "HA_Certificate.html");
                }
                else
                {
                    CertificatePath = Path.Combine(CertificateFolderPath, "RSACR_Certificate.html");
                }

                StreamReader str = new StreamReader(CertificatePath);
                string HtmlText = str.ReadToEnd();

                HtmlText = HtmlText.Replace("@IssueDate", Convert.ToDateTime(objMembership.IssueDate).ToString("dd-MMM-yyyy"));
                HtmlText = HtmlText.Replace("@FromDate", Convert.ToDateTime(objMembership.EffectiveDate).ToString("dd-MMM-yyyy"));
                HtmlText = HtmlText.Replace("@ToDate", Convert.ToDateTime(objMembership.ExpiryDate).ToString("dd-MMM-yyyy"));
                HtmlText = HtmlText.Replace("@CertificateNo", objMembership.MembershipReferenceNo);
                HtmlText = HtmlText.Replace("@CustomerName", objMembership.InsuredName);
                if (objMembership.PackageId == 11)
                {
                    HtmlText = HtmlText.Replace("@RiskAddress", objMembership.RiskAddress);
                }
                else
                {
                    if (objMembership.PackageId != 1)
                    {
                        HtmlText = HtmlText.Replace("@CarReplacement", objMembership.PackageName);
                    }
                    HtmlText = HtmlText.Replace("@VehicleRegistrationNo", objMembership.VehicleRegistrationNo);
                    HtmlText = HtmlText.Replace("@Make", objMembership.VehicleMake);
                    HtmlText = HtmlText.Replace("@ChassisNo", objMembership.ChassisNo);
                    HtmlText = HtmlText.Replace("@ManufacturingYear", objMembership.VehicleYear.ToString());

                }

                iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 100f, 0f);

                using (MemoryStream stream = new System.IO.MemoryStream())
                {
                    try
                    {
                        StringReader sr = new StringReader(HtmlText);

                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                        pdfDoc.Close();
                        byte[] bytes = stream.ToArray();
                        MemoryStream ms = new MemoryStream(bytes);
                    
                        string strTempPath = Path.GetTempPath();
                        DirectoryInfo di = new DirectoryInfo(string.Format("{0}/{1}", strTempPath, Session.SessionID));
                        if (!di.Exists)
                        {
                            di.Create();
                        }
                        string path = Path.Combine(strTempPath, Session.SessionID, objMembership.MembershipReferenceNo.Replace("/", "_") + ".pdf");
                        if (!System.IO.File.Exists(path))
                        {
                            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
                            {
                                ms.CopyTo(outputFileStream);
                            }
                        }

                        EmailServices objES = new EmailServices();
                        string CustomerName = objMembership.InsuredName;
                        string DownloadURL = string.Empty;
                        if (objMembership.PackageId == 1)
                        {
                            DownloadURL = Request.Url.Authority + "/DownloadDocument/Nation Assist Roadside Assistance.pdf";
                        }
                        else if (objMembership.PackageId == 11)
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
                        objTempValues.Add("MembershipRefNo", objMembership.MembershipReferenceNo);
                        objTempValues.Add("CertificatePath", path);
                        EmailServices objEmailService = new EmailServices();
                        List<string> ToEmail = new List<string> { objMembership.EmailId };
                        objEmailService.MailSent(ToEmail, objTempValues, "SERVICE_ENROLLMENT", true, null);
                        strError = string.Empty;

                    }
                    catch (Exception ex)
                    {
                        pdfDoc.Close();
                        strError = ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                strError = ex.Message;
            }
            return Json(new { Error = strError }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Membership Upload
        public ActionResult UploadMembership()
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

        public ActionResult UploadFile()
        {
            bool IsUploadDone = false;
            string strError = string.Empty;
            List<ExcelUploaderResponse> objUR = new List<ExcelUploaderResponse>();

            try
            {
                ExcelValidationResponse obj = new ExcelValidationResponse();


                if (Request.Files.Count > 0)
                {
                    obj.SourceId = Convert.ToInt32(Request.Form["SourceId"]);
                    obj.SourceName = Convert.ToString(Request.Form["SourceName"]);
                    string strTempPath = Path.GetTempPath();
                    HttpFileCollectionBase files = Request.Files;
                    DirectoryInfo di = new DirectoryInfo(string.Format("{0}/{1}", strTempPath, Session.SessionID));
                    if (!di.Exists)
                    {
                        di.Create();
                    }

                    string strFileName = DateTime.Now.ToFileTime().ToString() + "_" + files[0].FileName;
                    files[0].SaveAs(di + "/" + strFileName);


                    FileStream objFStream = new FileStream(di + "/" + strFileName, FileMode.Open);


                    UploadUtility objUpload = new UploadUtility();
                    objUR = objUpload.ProcessExcelData(objFStream, obj.SourceId, Convert.ToInt32(Session["UserId"]));

                    IsUploadDone = true;
                    strError = string.Empty;

                }
                else
                {
                    IsUploadDone = false;
                    strError = "No file found";
                }


            }
            catch (Exception ex)
            {
                strError = ex.Message;
                IsUploadDone = false;
            }
            return Json(new { Result = IsUploadDone, Error = strError, Response = objUR }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadTemplate(string FileName)
        {
            string FilePath = Path.Combine(ConfigurationManager.AppSettings["TemplateDownloadPath"], FileName);
            string mimetype = "application/vnd.ms-excel";
            MemoryStream ms = new MemoryStream();
            using (var stream = new FileStream(FilePath, FileMode.Open))
            {
                stream.CopyTo(ms);
            }
            return File(ms, mimetype, Path.GetFileName(FilePath));
        }
        #endregion

        public ActionResult BindServiceOptedForBroker(int BrokerId)
        {
            MethodOutput<Broker> objMO = new MethodOutput<Broker>();
            CommonServices obj = new CommonServices();
            objMO = obj.BindServiceOptedForPriceByBrokerId(BrokerId);
            return Json(objMO.Data, JsonRequestBehavior.AllowGet);
        }
    }
}