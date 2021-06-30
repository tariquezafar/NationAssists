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
                SqlParameter[] objListSqlParam = new SqlParameter[20];

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
    }
}
