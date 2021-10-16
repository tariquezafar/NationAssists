using DataEngine;
using DataServices;
using NationAssists.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {

            if (Session["UserId"] != null)
            {
                mDashboard objDS = new mDashboard();
                objDS = GetDashboardData();
                return View(objDS);
            }
            else
            {
                return Redirect("../../Home");
            }
        }

        public mDashboard GetDashboardData()
        {
            mDashboard objDashBoard = new mDashboard();
            try
            {
                DashboardService objDS = new DashboardService();
                MethodOutput<DashboardModel> output = new MethodOutput<DashboardModel>();
                output = objDS.GetDashBoardData();
                ViewBag.ErrorMessage = output.ErrorMessage;
                Users objUsers = (Users)Session["User"];


                if (string.IsNullOrEmpty(output.ErrorMessage))
                {
                    DashboardModel objDM = output.Data;

                    #region Employee
                    if (objUsers.UserTypeCode == "EMP")
                    {
                        objDashBoard.TotalNoOfUsers = objDM.UserList.Count;
                        objDashBoard.TotalNoOfServiceRequests = objDM.ServiceRequestList.Count;
                        objDashBoard.TotalNoOfAgentsBroker = objDM.SourceList.Count;
                        objDashBoard.TotalNoOfCustomer = objDM.CustomerList.Count;
                        objDashBoard.TotalNoOfServiceProvider = objDM.ServiceProviderList.Count;
                        objDashBoard.TotalNoOfOpenServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus ==(int)ServiceRequestStatusEnum.Open).ToList().Count;
                        objDashBoard.TotalNoOfClosedCRServiceRequest= objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Closed && x.ServiceId !=1 && x.ServiceId !=11).ToList().Count;
                        objDashBoard.TotalNoOfClosedHAServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Closed &&  x.ServiceId == 11).ToList().Count;
                        objDashBoard.TotalNoOfClosedRAServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Closed && x.ServiceId == 1).ToList().Count;
                        objDashBoard.TotalNoOfClosedServiceRequestAllocation = objDM.ServiceAllocationList.Where(x => x.ServiceAllocationStatus == (int)ServiceRequestAllocationStatusEnum.Closed).ToList().Count;
                        objDashBoard.TotalNoOfOpenCRServiceRequest= objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Open && x.ServiceId != 1 && x.ServiceId != 11).ToList().Count;
                        objDashBoard.TotalNoOfOpenHAServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Open && x.ServiceId == 11).ToList().Count;
                        objDashBoard.TotalNoOfOpenRAServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Open && x.ServiceId == 1).ToList().Count;
                        objDashBoard.TotalNoOfPendingCRServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Pending && x.ServiceId != 1 && x.ServiceId != 11).ToList().Count;
                        objDashBoard.TotalNoOfPendingHAServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Pending && x.ServiceId == 11).ToList().Count;
                        objDashBoard.TotalNoOfPendingRAServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Pending && x.ServiceId == 1).ToList().Count;
                        objDashBoard.TotalNoOfClosedServiceRequest = objDM.ServiceRequestList.Where(x => x.ServiceRequestStatus == (int)ServiceRequestStatusEnum.Closed).ToList().Count;
                        objDashBoard.TotalNoOfOpenServiceRequestAllocation = objDM.ServiceAllocationList.Where(x => x.ServiceAllocationStatus == (int)ServiceRequestAllocationStatusEnum.Open).ToList().Count;
                    }
                    #endregion

                    #region Source
                    if (objUsers.UserTypeCode != "SP" && objUsers.UserTypeCode != "EMP")
                    {
                        objDashBoard.TotalNoOfActiveSourceUser = objDM.UserList.Where(x => x.CreatedBy == objUsers.UserId).ToList().Count;
                        objDashBoard.TotalNoOfMembeshipIssuedTillDate = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId).ToList().Count;
                        objDashBoard.TotalNoOfMembeshipIssuedForTheDay = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && Convert.ToDateTime(x.CreatedDate).Date == DateTime.Now.Date).ToList().Count;
                        objDashBoard.NoOfMembershipSoldForTheMonth = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && Convert.ToDateTime(x.CreatedDate).Month == DateTime.Now.Month).ToList().Count;
                        objDashBoard.NoOfHAMembeshipIssuedForTheDay = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && x.PackageId == 11 && Convert.ToDateTime(x.CreatedDate) == DateTime.Now.Date).ToList().Count;
                        objDashBoard.NoOfRSAMembeshipIssuedForTheDay = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && x.PackageId == 1 && Convert.ToDateTime(x.CreatedDate) == DateTime.Now.Date).ToList().Count;
                        objDashBoard.NoOfRSAplusCRMembeshipIssuedForTheDay = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && x.PackageId != 1 && x.PackageId != 11 && Convert.ToDateTime(x.CreatedDate) == DateTime.Now.Date).ToList().Count;
                        objDashBoard.NoOfHAMembeshipIssuedTillDate = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && x.PackageId == 11).ToList().Count;
                        objDashBoard.NoOfRSAMembeshipIssuedTillDate = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && x.PackageId == 1).ToList().Count;
                        objDashBoard.NoOfRSAplusCRMembeshipIssuedTillDate = objDM.MembershipList.Where(x => x.SourceId == objUsers.UserReferenceId && x.PackageId != 1 && x.PackageId != 11).ToList().Count;
                    }
                    #endregion
                    #region Service Provider
                    if (objUsers.UserTypeCode == "SP")
                    {
                        objDashBoard.NoOfRSAServiceRequestAccepted = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && x.ServiceAllocationStatus== (int)ServiceRequestAllocationStatusEnum.Accepted && x.ServiceId==1).ToList().Count;
                        objDashBoard.NoOfCRServiceRequestAccepted = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && x.ServiceAllocationStatus == (int)ServiceRequestAllocationStatusEnum.Accepted && x.ServiceId != 1 && x.ServiceId!=11).ToList().Count;
                        objDashBoard.NoOfHAServiceRequestAccepted = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && x.ServiceAllocationStatus== (int)ServiceRequestAllocationStatusEnum.Accepted && x.ServiceId != 1 && x.ServiceId ==11).ToList().Count;
                        objDashBoard.NoOfServiceRequestAllocated = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId).ToList().Count;
                        objDashBoard.NoOfServiceRequestPending = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && x.ServiceAllocationStatus== (int)ServiceRequestStatusEnum.Open).ToList().Count;
                        objDashBoard.NoOfServiceRequestAccepted = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && x.ServiceAllocationStatus == (int)ServiceRequestAllocationStatusEnum.Accepted).ToList().Count;
                        objDashBoard.NoOfServiceRequestAllocatedToday = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && Convert.ToDateTime( x.CreatedDate)==DateTime.Now.Date).ToList().Count;
                        objDashBoard.NoOfServiceRequestClosed = objDM.ServiceAllocationList.Where(x => x.ServiceProviderId == objUsers.UserReferenceId && x.ServiceAllocationStatus == (int)ServiceRequestStatusEnum.Closed).ToList().Count;
                      
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {


            }
            return objDashBoard;
        }


    }
}