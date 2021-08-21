using DataEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DbOperation
{
    public class OpServiceEnrollment
    {
        public MethodOutput<string> SaveServiceEnrollmentRequest(ServiceEnrollmentRequest objSER)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[20];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CustomerId";
                objListSqlParam[0].Value = objSER.CustomerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = objSER.ServiceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@VehicleRegistrationNumber";
                objListSqlParam[2].Value = objSER.VehicleRegistrationNumber;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@VehicleBody";
                objListSqlParam[3].Value = objSER.VehicleBody;
;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@ModelType";
                objListSqlParam[4].Value = objSER.ModelType;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@YearOfManufacture";
                objListSqlParam[5].Value = objSER.YearOfManufacture;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@YearOfConstruction";
                objListSqlParam[6].Value = objSER.YearOfConstruction;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@ChessisNo";
                objListSqlParam[7].Value = objSER.ChessisNo;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@CountryId";
                objListSqlParam[8].Value = objSER.CountryId;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@GovernotesId";
                objListSqlParam[9].Value = objSER.GovernotesId;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@PlaceId";
                objListSqlParam[10].Value = objSER.PlaceId;


                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@BlockId";
                objListSqlParam[11].Value = objSER.BlockId ;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@NoOfLocation";
                objListSqlParam[12].Value = objSER.NoOfLocation;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@RiskAddress";
                objListSqlParam[13].Value = objSER.RiskAddress;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@Remarks";
                objListSqlParam[14].Value = objSER.Remarks;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@VehicleManufacturer";
                objListSqlParam[15].Value = objSER.VehicleManufacturer;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@ServiceEnrollmentRequestId";
                objListSqlParam[16].Value = objSER.ServiceEnrollmentRequestId;

                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@TypeOfService";
                objListSqlParam[17].Value = objSER.TypeOfService;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceEnrollmentRequest", objListSqlParam).Tables[0];
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


        public MethodOutput<ServiceEnrollmentRequest> GetAllServiceEnrollementRequst(int CustomerId, int ServiceId, int EnrollmentStatusId, string CustomerName)
        {
            MethodOutput<ServiceEnrollmentRequest> objOutput = new MethodOutput<ServiceEnrollmentRequest>();
            List<ServiceEnrollmentRequest> objLstService = new List<ServiceEnrollmentRequest>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[4];

                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CustomerId";
                objListSqlParam[0].Value = CustomerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@StatusId";
                objListSqlParam[2].Value = EnrollmentStatusId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@CustomerName";
                objListSqlParam[3].Value = CustomerName;
                
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceEnrollmentRequest", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new ServiceEnrollmentRequest
                    {
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceCode = x.Field<string>("ServiceCode"),
                        ApproverRemarks= x.Field<string>("ApproverRemarks"),
                        BlockCode= x.Field<string>("BlockCode"),
                        BlockId=x.Field<int?>("BlockId"),
                        ChessisNo= x.Field<string>("ChessisNo"),
                        CountryId= x.Field<int?>("CountryId"),
                        CountryName = x.Field<string>("CountryName"),
                        CreatedDate=x.Field<DateTime>("CreatedDate"),
                        CustomerId= x.Field<int>("CustomerId"),
                        CustomerName= x.Field<string>("CustomerName"),
                        GovernotesId= x.Field<int?>("GovernotesId"),
                        GovernotesName= x.Field<string>("GovernotesName"),
                        InsurancePolicyNumber= x.Field<string>("InsurancePolicyNumber"),
                        ModelType= x.Field<string>("ModelType"),
                        ModifiedBy =x.Field<int?>("ModifiedBy"),
                        ModifiedDate= x.Field<DateTime?>("ModifiedDate"),
                        NoOfLocation= x.Field<int>("NoOfLocation"),
                        PlaceId= x.Field<int?>("PlaceId"),
                        PlaceName= x.Field<string>("PlaceName"),
                        PolicyEndDate= x.Field<DateTime?>("PolicyEndDate"),
                        PolicyStartDate= x.Field<DateTime?>("PolicyStartDate"),
                        Remarks= x.Field<string>("Remarks"),
                        RiskAddress= x.Field<string>("RiskAddress"),
                        ServiceEnrollmentRequestId = x.Field<int>("ServiceEnrollmentRequestId"),
                        ServiceEnrollmentStatus= x.Field<int>("ServiceEnrollmentStatus"),
                        Status= x.Field<string>("Status_Name"),
                        TypeOfService= x.Field<string>("TypeOfServices"),
                        VehicleBody = x.Field<string>("VehicleBody"),
                        VehicleManufacturer= x.Field<string>("VehicleManufacturer"),
                        VehicleRegistrationNumber = x.Field<string>("VehicleRegistrationNumber"),
                        YearOfConstruction = x.Field<int>("YearOfConstruction"),
                        YearOfManufacture= x.Field<int>("YearOfManufacture"),
                        RequestedDate= x.Field<DateTime>("CreatedDate").AddDays(3).ToString("dd-MMM-yyyy"),
                        CPRNumber= x.Field<string>("CPRNumber"),
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


        public MethodOutput<string> UpdateServiceEnrollmentRequest(ServiceEnrollmentRequest objSER)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[20];

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceEnrollmentRequestId";
                objListSqlParam[1].Value = objSER.ServiceEnrollmentRequestId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@Branch";
                objListSqlParam[2].Value = objSER.Branch;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@PolicyType";
                objListSqlParam[3].Value = objSER.PolicyType;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@InsurancePolicyNumber";
                objListSqlParam[4].Value = objSER.InsurancePolicyNumber;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@PolicyStartDate";
                objListSqlParam[5].Value = objSER.PolicyStartDate;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@PolicyEndDate";
                objListSqlParam[6].Value = objSER.PolicyEndDate;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@ServiceEnrollmentStatus";
                objListSqlParam[7].Value = objSER.ServiceEnrollmentStatus;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@ApproverRemarks";
                objListSqlParam[8].Value = objSER.ApproverRemarks;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@CreatedBy";
                objListSqlParam[9].Value = objSER.ApprovedBy;

      dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_UpdateServiceEnrollmentRequest", objListSqlParam).Tables[0];
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

    }
}
