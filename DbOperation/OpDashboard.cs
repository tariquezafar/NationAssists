using DataEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DbOperation
{
  public  class OpDashboard
    {
        public MethodOutput<DashboardModel> GetDashBoardData()
        {
            MethodOutput<DashboardModel> output = new MethodOutput<DashboardModel>();
            DashboardModel objDM = new DashboardModel();
            DataSet ds = new DataSet();
            try
            {
                DataTable dtUser = new DataTable();
                DataTable dtMemberShip = new DataTable();
                DataTable dtSP = new DataTable();
                DataTable dtSource = new DataTable();
                DataTable dtSR = new DataTable();
                DataTable dtSA = new DataTable();
                DataTable dtSER = new DataTable();
                DataTable dtCustomer = new DataTable();
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetDashboard");
                dtUser = ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null ? ds.Tables[0] : new DataTable();
                dtMemberShip = ds != null && ds.Tables.Count > 1 && ds.Tables[1] != null ? ds.Tables[1] : new DataTable();
                dtSP = ds != null && ds.Tables.Count > 2 && ds.Tables[2] != null ? ds.Tables[2] : new DataTable();
                dtSource = ds != null && ds.Tables.Count > 3 && ds.Tables[3] != null ? ds.Tables[3] : new DataTable();
                dtSR = ds != null && ds.Tables.Count > 4 && ds.Tables[4] != null ? ds.Tables[4] : new DataTable();
                dtSA = ds != null && ds.Tables.Count > 5 && ds.Tables[5] != null ? ds.Tables[5] : new DataTable();
                dtSER = ds != null && ds.Tables.Count > 6 && ds.Tables[6] != null ? ds.Tables[6] : new DataTable();
                dtCustomer = ds != null && ds.Tables.Count > 7 && ds.Tables[7] != null ? ds.Tables[7] : new DataTable();
                objDM.UserList = dtUser.Rows.Count > 0 ? dtUser.AsEnumerable().Select(x => new Users
                {
                    CreatedBy=x.Field<int?>("CreatedBy"),
                    UserTypeId = x.Field<int>("UserTypeId"),
                    UserReferenceId = x.Field<int>("UserReferenceId"),
                    CreatedDate = x.Field<DateTime>("Createdate")

                }).ToList() : new List<Users>();

                objDM.MembershipList = dtMemberShip.Rows.Count > 0 ? dtMemberShip.AsEnumerable().Select(x => new Membership
                {
                    SourceId= x.Field<int?>("SourceId"),
                    PackageId = x.Field<int>("PackageId"),
                    CreatedDate = x.Field<DateTime>("CreatedDate"),
                    CreatedBy = x.Field<int>("CreatedBy")

                }).ToList() : new List<Membership>();

                objDM.SourceList= dtSource.Rows.Count > 0 ? dtSource.AsEnumerable().Select(x => new Broker
                {
                   Broker_Type = x.Field<string>("Broker_Type"),
                    CreatedDate = x.Field<DateTime>("CreatedDate"),
                }).ToList() : new List<Broker>();


                objDM.ServiceProviderList = dtSP.Rows.Count > 0 ? dtSP.AsEnumerable().Select(x => new ServiceProvider
                {
                    CreatedDate = x.Field<DateTime>("Createdate")
                }).ToList() : new List<ServiceProvider>();

                objDM.ServiceRequestList = dtSR.Rows.Count > 0 ? dtSR.AsEnumerable().Select(x => new ServiceRequest
                {
                    CreatedDate = x.Field<DateTime>("CreateDate"),
                    ServiceId=x.Field<int>("ServiceId"),
                    ServiceRequestStatus= x.Field<int>("ServiceRequestStatus"),
                }).ToList() : new List<ServiceRequest>();

                objDM.ServiceAllocationList= dtSA.Rows.Count > 0 ? dtSA.AsEnumerable().Select(x => new ServiceAllocation
                {
                    ServiceId= x.Field<int>("ServiceId"),
                    CreatedDate = x.Field<DateTime>("CreatedDate"),
                    ServiceAllocationStatus = x.Field<int>("ServiceAllocationStatus"),
                    AssignedToUser= x.Field<int?>("AssignedToUser"),
                    ServiceProviderId=x.Field<int>("ServiceProviderId")
                }).ToList() : new List<ServiceAllocation>();

                objDM.ServiceEnrollmentRequestList= dtSER.Rows.Count > 0 ? dtSER.AsEnumerable().Select(x => new ServiceEnrollmentRequest

                {
                    ServiceId= x.Field<int>("ServiceId"),
                    CreatedDate = x.Field<DateTime>("CreatedDate"),
                    ServiceEnrollmentStatus = x.Field<int>("ServiceEnrollmentStatus"),
                }).ToList() : new List<ServiceEnrollmentRequest>();

                objDM.CustomerList = dtCustomer.Rows.Count > 0 ? dtCustomer.AsEnumerable().Select(x => new Customer

                {
                    AccountType = x.Field<string>("AccountType"),
                    CreatedDate = x.Field<DateTime>("Createdate"),
                    AccountSubType = x.Field<string>("AccountSubType"),
                }).ToList() : new List<Customer>();
            }
            catch (Exception ex)
            {
                output.ErrorMessage = ex.Message;

            }
            output.Data = objDM;
            return output;
        }
    }
}
