using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEngine;
using Utility;
using System.Data;
using System.Data.SqlClient;

namespace DbOperation
{
    public class OpServiceRequest
    {
        public MethodOutput<string> SaveServiceRequest(ServiceRequest objSR)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[21];

                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceRequestId";
                objListSqlParam[0].Value = objSR.ServiceRequestId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@CustomerId";
                objListSqlParam[1].Value = objSR.CustomerId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@ServiceId";
                objListSqlParam[2].Value = objSR.ServiceId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@ServiceSubCategoryId";
                objListSqlParam[3].Value = objSR.ServiceSubCategoryId;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@VehicleRegistrationNumber";
                objListSqlParam[4].Value = objSR.VehicleRegistrationNumber;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@ChessisNo";
                objListSqlParam[5].Value = objSR.ChessisNo;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@ServiceLocation";
                objListSqlParam[6].Value = objSR.ServiceLocation;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@CountryID";
                objListSqlParam[7].Value = objSR.CountryID;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@GovernotesId";
                objListSqlParam[8].Value = objSR.GovernotesId;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@PlaceId";
                objListSqlParam[9].Value = objSR.PlaceId;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@LocationPinCode";
                objListSqlParam[10].Value = objSR.LocationPinCode;

                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@DateOfAccident";
                objListSqlParam[11].Value = objSR.DateOfAccident;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@NameOfWorkShop";
                objListSqlParam[12].Value = objSR.NameOfWorkShop;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@BuildingNo";
                objListSqlParam[13].Value = objSR.BuildingNo;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@CreatedBy";
                objListSqlParam[14].Value = objSR.CreatedBy;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@StepiniCondtion";
                objListSqlParam[15].Value = objSR.StepiniCondtion;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@CollectRepairVehicleAddress";
                objListSqlParam[16].Value = objSR.CollectRepairVehicleAddress;

                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@ContactMobileNo";
                objListSqlParam[17].Value = objSR.ContactMobileNo;

                objListSqlParam[18] = new SqlParameter();
                objListSqlParam[18].ParameterName = "@ReleventDetails";
                objListSqlParam[18].Value = objSR.ReleventDetails;

                objListSqlParam[19] = new SqlParameter();
                objListSqlParam[19].ParameterName = "@Remarks";
                objListSqlParam[19].Value = objSR.Remarks;

                objListSqlParam[20] = new SqlParameter();
                objListSqlParam[20].ParameterName = "@BlockId";
                objListSqlParam[20].Value = objSR.BlockId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceRequest", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    output.ErrorMessage = Convert.ToString(dt.Rows[0]["Error"]);
                    output.Data = Convert.ToString(dt.Rows[0]["TicketNo"]);

                }
            }
            catch (Exception ex)
            {

                output.ErrorMessage = ex.Message;
            }

            return output;
        }

        public MethodOutput<Service> BindServicesByCPRNumber(string CPRNumber)
        {
            MethodOutput<Service> objOutput = new MethodOutput<Service>();
            List<Service> objLstService = new List<Service>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CPRNumber";
                objListSqlParam[0].Value = CPRNumber;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindServiceByCPRNumber", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new Service
                    {
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceCode = x.Field<string>("ServiceCode"),
                        IsActive = x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }

        public MethodOutput<ServiceRequest> BindServiceRequestForAllocation(int ServiceRequestStatusId, string TicketNo)
        {
            MethodOutput<ServiceRequest> objOutput = new MethodOutput<ServiceRequest>();
            List<ServiceRequest> objLstService = new List<ServiceRequest>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceRequestStatusId";
                objListSqlParam[0].Value = ServiceRequestStatusId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@TicketNo";
                objListSqlParam[1].Value = TicketNo;


                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ShowAllRaisedServiceRequest", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new ServiceRequest
                    {
                        //   BlockId= x.Field<int>("BlockId"),
                        Block_Code = x.Field<string>("Block_Code"),
                        BuildingNo = x.Field<string>("BuildingNo"),
                        CarHandledWorkShopDate = x.Field<string>("CarHandledWorkShopDate"),
                        ChessisNo = x.Field<string>("ChessisNo"),
                        CollectRepairVehicleAddress = x.Field<string>("CollectRepairVehicleAddress"),
                        ContactMobileNo = x.Field<string>("ContactMobileNo"),
                        CountryID = x.Field<int>("CountryID"),
                        CountryName = x.Field<string>("CountryName"),
                        CustomerId = x.Field<int>("CustomerId"),
                        Customer_Name = x.Field<string>("Customer_Name"),
                        DateOfAccident = x.Field<DateTime>("DateOfAccident"),
                        EstimatedRepairCompletedDate = x.Field<DateTime?>("EstimatedRepairCompletedDate"),
                        GovernoratesName = x.Field<string>("GovernoratesName"),
                        GovernotesId = x.Field<int>("GovernotesId"),
                        LocationPinCode = x.Field<string>("LocationPinCode"),
                        NameOfWorkShop = x.Field<string>("NameOfWorkShop"),
                        PlaceId = x.Field<int>("PlaceId"),
                        ReleventDetails = x.Field<string>("ReleventDetails"),
                        Place_Name = x.Field<string>("Place_Name"),
                        Remarks = x.Field<string>("Remarks"),
                        ServiceFeedBack = x.Field<string>("ServiceFeedBack"),
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceLocation = x.Field<string>("ServiceLocation"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceRequestedDate = x.Field<DateTime>("ServiceRequestedDate"),
                        ServiceRequestId = x.Field<Int64>("ServiceRequestId"),
                        TicketNo = x.Field<string>("TicketNo"),
                        SubCategoryName = x.Field<string>("SubCategoryName"),
                        Status_Name = x.Field<string>("Status_Name"),
                        BrokerId = x.Field<int>("BrokerId"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        ServiceAllocationId= x.Field<Int64>("ServiceAllocationId"),
                        StepiniCondtion = x.Field<bool>("StepiniCondtion"),
                        VehicleRegistrationNumber = x.Field<string>("VehicleRegistrationNumber"),

                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                objOutput.ErrorMessage = ex.Message;
            }

            return objOutput;
        }


        public MethodOutput<AllocatedServiceProvider> BindServiceProviderForAllocation(int BlockId, int PlaceId, int GovernotesId, int CountryId, int SubCategoryId, int BrokerId, int ServiceId)
        {
            MethodOutput<AllocatedServiceProvider> objOutput = new MethodOutput<AllocatedServiceProvider>();
            List<AllocatedServiceProvider> objLstService = new List<AllocatedServiceProvider>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[7];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@BlockId";
                objListSqlParam[0].Value = BlockId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@PlaceId";
                objListSqlParam[1].Value = PlaceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "GovernotesId";
                objListSqlParam[2].Value = GovernotesId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@CountryId";
                objListSqlParam[3].Value = CountryId;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@SubCategoryId";
                objListSqlParam[4].Value = SubCategoryId;


                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@BrokerId";
                objListSqlParam[5].Value = BrokerId;


                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@ServiceId";
                objListSqlParam[6].Value = ServiceId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindServiceProviderForAllocation", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new AllocatedServiceProvider
                    {
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        ServiceProviderCode = x.Field<string>("ServiceProviderCode"),
                        ServiceProviderName = x.Field<string>("ServiceProviderName"),
                        ContactPersonName = x.Field<string>("ContactPersonName"),
                        ServicePriceId = x.Field<Int64>("ServicePriceId"),
                        MobileNo = x.Field<string>("MobileNo"),
                        EmailId = x.Field<string>("EmailId"),
                        ServicePrice = x.Field<decimal>("ServicePrice"),

                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }


        public MethodOutput<string> SaveServiceRequestAllocation(ServiceAllocation objSA)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[15];

                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceAllocationId";
                objListSqlParam[0].Value = objSA.ServiceAllocationId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceRequestId";
                objListSqlParam[1].Value = objSA.ServiceRequestId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@ServiceProviderId";
                objListSqlParam[2].Value = objSA.ServiceProviderId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@ServiceId";
                objListSqlParam[3].Value = objSA.ServiceId;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@ServiceSubCategoryId";
                objListSqlParam[4].Value = objSA.ServiceSubCategoryId;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@ServiceAllocationStatus";
                objListSqlParam[5].Value = objSA.ServiceAllocationStatus;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@AssignedToUser";
                objListSqlParam[6].Value = objSA.AssignedToUser;


                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@ReasonForRejection";
                objListSqlParam[7].Value = objSA.ReasonForRejection;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@Remarks";
                objListSqlParam[8].Value = objSA.Remarks;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@ServicePrice";
                objListSqlParam[9].Value = objSA.ServicePrice;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@ServicePriceId";
                objListSqlParam[10].Value = objSA.ServicePriceId;



                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceRequestAllocation", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    output.ErrorMessage = Convert.ToString(dt.Rows[0]["Error"]);
                }
            }
            catch (Exception ex)
            {
                output.ErrorMessage = ex.Message;
            }
            return output;
        }


        public MethodOutput<ServiceRequest> BindServiceAllocation(int ServiceProviderId)
        {

            MethodOutput<ServiceRequest> objOutput = new MethodOutput<ServiceRequest>();
            List<ServiceRequest> objLstService = new List<ServiceRequest>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = ServiceProviderId;



                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceRequestAllocation", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new ServiceRequest
                    {
                        //   BlockId= x.Field<int>("BlockId"),
                        ServiceAllocationId = x.Field<Int64>("ServiceAllocationId"),
                        Allocation_Status = x.Field<string>("Allocation_Status"),
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        ServiceAllocationStatus = x.Field<int>("ServiceAllocationStatus"),
                        Block_Code = x.Field<string>("Block_Code"),
                        BuildingNo = x.Field<string>("BuildingNo"),
                        CarHandledWorkShopDate = x.Field<string>("CarHandledWorkShopDate"),
                        ChessisNo = x.Field<string>("ChessisNo"),
                        CollectRepairVehicleAddress = x.Field<string>("CollectRepairVehicleAddress"),
                        ContactMobileNo = x.Field<string>("ContactMobileNo"),
                        CountryID = x.Field<int>("CountryID"),
                        CountryName = x.Field<string>("CountryName"),
                        CustomerId = x.Field<int>("CustomerId"),
                        Customer_Name = x.Field<string>("Customer_Name"),
                        DateOfAccident = x.Field<DateTime>("DateOfAccident"),
                        EstimatedRepairCompletedDate = x.Field<DateTime?>("EstimatedRepairCompletedDate"),
                        GovernoratesName = x.Field<string>("GovernoratesName"),
                        GovernotesId = x.Field<int>("GovernotesId"),
                        LocationPinCode = x.Field<string>("LocationPinCode"),
                        NameOfWorkShop = x.Field<string>("NameOfWorkShop"),
                        PlaceId = x.Field<int>("PlaceId"),
                        ReleventDetails = x.Field<string>("ReleventDetails"),
                        Place_Name = x.Field<string>("Place_Name"),
                        Remarks = x.Field<string>("Remarks"),
                        ServiceFeedBack = x.Field<string>("ServiceFeedBack"),
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceLocation = x.Field<string>("ServiceLocation"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceRequestedDate = x.Field<DateTime>("ServiceRequestedDate"),
                        ServiceRequestId = x.Field<Int64>("ServiceRequestId"),
                        TicketNo = x.Field<string>("TicketNo"),
                        SubCategoryName = x.Field<string>("SubCategoryName"),
                        Status_Name = x.Field<string>("Status_Name"),
                        BrokerId = x.Field<int>("BrokerId"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        VehicleRegistrationNumber = x.Field<string>("VehicleRegistrationNumber")
                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                objOutput.ErrorMessage = ex.Message;
            }

            return objOutput;
        }


        public MethodOutput<ServiceRequest> BindAssignedServiceRequest(int UserId)
        {

            MethodOutput<ServiceRequest> objOutput = new MethodOutput<ServiceRequest>();
            List<ServiceRequest> objLstService = new List<ServiceRequest>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@UserId";
                objListSqlParam[0].Value = UserId;



                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllAssignedServiceRequest", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new ServiceRequest
                    {
                        //   BlockId= x.Field<int>("BlockId"),
                        ServiceAllocationId = x.Field<Int64>("ServiceAllocationId"),
                        Allocation_Status = x.Field<string>("Allocation_Status"),
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        ServiceAllocationStatus = x.Field<int>("ServiceAllocationStatus"),
                        Block_Code = x.Field<string>("Block_Code"),
                        BuildingNo = x.Field<string>("BuildingNo"),
                        CarHandledWorkShopDate = x.Field<string>("CarHandledWorkShopDate"),
                        ChessisNo = x.Field<string>("ChessisNo"),
                        CollectRepairVehicleAddress = x.Field<string>("CollectRepairVehicleAddress"),
                        ContactMobileNo = x.Field<string>("ContactMobileNo"),
                        CountryID = x.Field<int>("CountryID"),
                        CountryName = x.Field<string>("CountryName"),
                        CustomerId = x.Field<int>("CustomerId"),
                        Customer_Name = x.Field<string>("Customer_Name"),
                        DateOfAccident = x.Field<DateTime>("DateOfAccident"),
                        EstimatedRepairCompletedDate = x.Field<DateTime?>("EstimatedRepairCompletedDate"),
                        GovernoratesName = x.Field<string>("GovernoratesName"),
                        GovernotesId = x.Field<int>("GovernotesId"),
                        LocationPinCode = x.Field<string>("LocationPinCode"),
                        NameOfWorkShop = x.Field<string>("NameOfWorkShop"),
                        PlaceId = x.Field<int>("PlaceId"),
                        ReleventDetails = x.Field<string>("ReleventDetails"),
                        Place_Name = x.Field<string>("Place_Name"),
                        Remarks = x.Field<string>("Remarks"),
                        ServiceFeedBack = x.Field<string>("ServiceFeedBack"),
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceLocation = x.Field<string>("ServiceLocation"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceRequestedDate = x.Field<DateTime>("ServiceRequestedDate"),
                        ServiceRequestId = x.Field<Int64>("ServiceRequestId"),
                        TicketNo = x.Field<string>("TicketNo"),
                        SubCategoryName = x.Field<string>("SubCategoryName"),
                        Status_Name = x.Field<string>("Status_Name"),
                        BrokerId = x.Field<int>("BrokerId"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        VehicleRegistrationNumber = x.Field<string>("VehicleRegistrationNumber")
                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                objOutput.ErrorMessage = ex.Message;
            }

            return objOutput;
        }


        public MethodOutput<ServiceRequest> BindAllServiceRequest(int ServiceRequestStatusId, string TicketNo, string AccountType, int BrokerId, string AccountSubType, DateTime StartDate, DateTime EndDate)
        {
            MethodOutput<ServiceRequest> objOutput = new MethodOutput<ServiceRequest>();
            List<ServiceRequest> objLstService = new List<ServiceRequest>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[8];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceRequestStatusId";
                objListSqlParam[0].Value = ServiceRequestStatusId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@TicketNo";
                objListSqlParam[1].Value = TicketNo;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@AccountType";
                objListSqlParam[2].Value = AccountType;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@BrokerId";
                objListSqlParam[3].Value = BrokerId;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@AccountSubType";
                objListSqlParam[4].Value = AccountSubType;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@StartDate";
                objListSqlParam[5].Value = StartDate;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@EndDate";
                objListSqlParam[6].Value = EndDate;



                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ShowAllServiceRequest", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new ServiceRequest
                    {
                        //   BlockId= x.Field<int>("BlockId"),
                        Block_Code = x.Field<string>("Block_Code"),
                        BuildingNo = x.Field<string>("BuildingNo"),
                        CarHandledWorkShopDate = x.Field<string>("CarHandledWorkShopDate"),
                        ChessisNo = x.Field<string>("ChessisNo"),
                        CollectRepairVehicleAddress = x.Field<string>("CollectRepairVehicleAddress"),
                        ContactMobileNo = x.Field<string>("ContactMobileNo"),
                        CountryID = x.Field<int>("CountryID"),
                        CountryName = x.Field<string>("CountryName"),
                        CustomerId = x.Field<int>("CustomerId"),
                        Customer_Name = x.Field<string>("Customer_Name"),
                        DateOfAccident = x.Field<DateTime>("DateOfAccident"),
                        EstimatedRepairCompletedDate = x.Field<DateTime?>("EstimatedRepairCompletedDate"),
                        GovernoratesName = x.Field<string>("GovernoratesName"),
                        GovernotesId = x.Field<int>("GovernotesId"),
                        LocationPinCode = x.Field<string>("LocationPinCode"),
                        NameOfWorkShop = x.Field<string>("NameOfWorkShop"),
                        PlaceId = x.Field<int>("PlaceId"),
                        ReleventDetails = x.Field<string>("ReleventDetails"),
                        Place_Name = x.Field<string>("Place_Name"),
                        Remarks = x.Field<string>("Remarks"),
                        ServiceFeedBack = x.Field<string>("ServiceFeedBack"),
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceLocation = x.Field<string>("ServiceLocation"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceRequestedDate = x.Field<DateTime>("ServiceRequestedDate"),
                        ServiceRequestId = x.Field<Int64>("ServiceRequestId"),
                        TicketNo = x.Field<string>("TicketNo"),
                        SubCategoryName = x.Field<string>("SubCategoryName"),
                        Status_Name = x.Field<string>("Status_Name"),
                        BrokerId = x.Field<int>("BrokerId"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        VehicleRegistrationNumber = x.Field<string>("VehicleRegistrationNumber")
                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                objOutput.ErrorMessage = ex.Message;
            }

            return objOutput;
        }

        public MethodOutput<string> BindChessisListByCPRNumber(string CPRNumber, int ServiceId)
        {
            MethodOutput<string> objOutput = new MethodOutput<string>();
            List<string> objLstChessis = new List<string>();

            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CPRNumber";
                objListSqlParam[0].Value = CPRNumber;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindChessisNoByCPRNumber", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        objLstChessis.Add(Convert.ToString(dt.Rows[0]["ChassisNo"]));
                    }
              

                    objOutput.DataList = objLstChessis;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }
        public MethodOutput<string> BindVehicleRegistrationNoListByCPRNumber(string CPRNumber, int ServiceId)
        {
            MethodOutput<string> objOutput = new MethodOutput<string>();
            List<string> objLstVehicleRegistration = new List<string>();

            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CPRNumber";
                objListSqlParam[0].Value = CPRNumber;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindVehicleRegistrationNoByCPRNumber", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        objLstVehicleRegistration.Add(Convert.ToString(dt.Rows[0]["VehicleRegistrationNo"]));
                    }


                    objOutput.DataList = objLstVehicleRegistration;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }

        public MethodOutput<ServiceRequest> BindServiceRequestByCustomer(int CustomerId)
        {
            MethodOutput<ServiceRequest> objOutput = new MethodOutput<ServiceRequest>();
            List<ServiceRequest> objLstService = new List<ServiceRequest>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CustomerId";
                objListSqlParam[0].Value = CustomerId;

        
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ShowAllServiceRequestByCustomerId", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new ServiceRequest
                    {
                        //   BlockId= x.Field<int>("BlockId"),
                        Block_Code = x.Field<string>("Block_Code"),
                        BuildingNo = x.Field<string>("BuildingNo"),
                        CarHandledWorkShopDate = x.Field<string>("CarHandledWorkShopDate"),
                        ChessisNo = x.Field<string>("ChessisNo"),
                        CollectRepairVehicleAddress = x.Field<string>("CollectRepairVehicleAddress"),
                        ContactMobileNo = x.Field<string>("ContactMobileNo"),
                        CountryID = x.Field<int>("CountryID"),
                        CountryName = x.Field<string>("CountryName"),
                        CustomerId = x.Field<int>("CustomerId"),
                        Customer_Name = x.Field<string>("Customer_Name"),
                        DateOfAccident = x.Field<DateTime>("DateOfAccident"),
                        EstimatedRepairCompletedDate = x.Field<DateTime?>("EstimatedRepairCompletedDate"),
                        GovernoratesName = x.Field<string>("GovernoratesName"),
                        GovernotesId = x.Field<int>("GovernotesId"),
                        LocationPinCode = x.Field<string>("LocationPinCode"),
                        NameOfWorkShop = x.Field<string>("NameOfWorkShop"),
                        PlaceId = x.Field<int>("PlaceId"),
                        ReleventDetails = x.Field<string>("ReleventDetails"),
                        Place_Name = x.Field<string>("Place_Name"),
                        Remarks = x.Field<string>("Remarks"),
                        ServiceFeedBack = x.Field<string>("ServiceFeedBack"),
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceLocation = x.Field<string>("ServiceLocation"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceRequestedDate = x.Field<DateTime>("ServiceRequestedDate"),
                        ServiceRequestId = x.Field<Int64>("ServiceRequestId"),
                        TicketNo = x.Field<string>("TicketNo"),
                        SubCategoryName = x.Field<string>("SubCategoryName"),
                        Status_Name = x.Field<string>("Status_Name"),
                        BrokerId = x.Field<int>("BrokerId"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        StepiniCondtion = x.Field<bool>("StepiniCondtion"),
                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                objOutput.ErrorMessage = ex.Message;
            }

            return objOutput;
        }

        public MethodOutput<CustomerStatus> BindCustomerStatus(string CPRNumber)
        {
            MethodOutput<CustomerStatus> objOutput = new MethodOutput<CustomerStatus>();
            List<CustomerStatus> objLstCustomer = new List<CustomerStatus>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CPRNumber";
                objListSqlParam[0].Value = CPRNumber;


                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SearchCustomerStatusByCRNumber", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstCustomer = dt.AsEnumerable().Select(x => new CustomerStatus
                    {
                        IsHavingMembership = x.Field<bool>("IsHavingMembership"),
                        IsMemberShipExpired = x.Field<bool>("IsMemberShipExpired"),
                        IsSignUpCompleted= x.Field<bool>("IsSignUpCompleted"),
                        CustomerType= x.Field<string>("CustomerType"),
                        CustomerId=x.Field<int>("CustomerId"),
                        SourceId= x.Field<int>("SourceId"),
                        SourceName = x.Field<string>("SourceName"),
                        SourceType= x.Field<string>("SourceType"),
                        SourceTypeCode= x.Field<string>("SourceTypeCode")

                    }).ToList();

                    objOutput.DataList = objLstCustomer;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {

                objOutput.ErrorMessage = ex.Message;
            }

            return objOutput;
        }

        public MethodOutput<VehicleDetail> BindVehicleDetailByCPRNumber(string CPRNumber, int ServiceId)
        {
            MethodOutput<VehicleDetail> objOutput = new MethodOutput<VehicleDetail>();
            List<string> objLstVehicleRegistration = new List<string>();
            List<string> objLstChessisNo = new List<string>();
            VehicleDetail objVehicleDetail = new VehicleDetail();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CPRNumber";
                objListSqlParam[0].Value = CPRNumber;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindVehicleDetailByCPRNumber", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        objLstVehicleRegistration.Add(Convert.ToString(dt.Rows[0]["VehicleRegistrationNo"]));
                        objLstChessisNo.Add(Convert.ToString(dt.Rows[0]["ChassisNo"]));
                    }
                    objVehicleDetail.ChessisList = objLstChessisNo;
                    objVehicleDetail.VehicleRegistrationNoList = objLstVehicleRegistration;

                    objOutput.Data = objVehicleDetail;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }
        
    }
}
