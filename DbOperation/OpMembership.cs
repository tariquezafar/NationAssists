using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEngine;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DbOperation
{
    public class OpMembership
    {
        public MethodOutput<string> SaveMembership(Membership objMembership)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {


                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[28];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@MembershipId";
                objListSqlParam[0].Value = objMembership.MembershipId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@SourceId";
                objListSqlParam[1].Value = objMembership.SourceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@PackageId";
                objListSqlParam[2].Value = objMembership.PackageId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@Branch";
                objListSqlParam[3].Value = objMembership.Branch
;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@PolicyType";
                objListSqlParam[4].Value = objMembership.PolicyType;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@PolicyNo";
                objListSqlParam[5].Value = objMembership.PolicyNo;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@IssueDate";
                objListSqlParam[6].Value = objMembership.IssueDate;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@EffectiveDate";
                objListSqlParam[7].Value = objMembership.EffectiveDate;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@ExpiryDate";
                objListSqlParam[8].Value = objMembership.ExpiryDate;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@InsuredName";
                objListSqlParam[9].Value = objMembership.InsuredName;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@CPRNumber";
                objListSqlParam[10].Value = objMembership.CPRNumber;

                //objListSqlParam[11] = new SqlParameter();
                //objListSqlParam[11].ParameterName = "@CPRNumber";
                //objListSqlParam[11].Value = objMembership.CPRNumber;

                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@MobileNo";
                objListSqlParam[11].Value = objMembership.MobileNo;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@EmailId";
                objListSqlParam[12].Value = objMembership.EmailId;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@VehicleRegistrationNo";
                objListSqlParam[13].Value = objMembership.VehicleRegistrationNo;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@ChassisNo";
                objListSqlParam[14].Value = objMembership.ChassisNo;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@VehicleMake";
                objListSqlParam[15].Value = objMembership.VehicleMake;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@VehicleType";
                objListSqlParam[16].Value = objMembership.VehicleType;

                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@VehicleBody";
                objListSqlParam[17].Value = objMembership.VehicleBody;

                objListSqlParam[18] = new SqlParameter();
                objListSqlParam[18].ParameterName = "@VehicleYear";
                objListSqlParam[18].Value = objMembership.VehicleYear;

                objListSqlParam[19] = new SqlParameter();
                objListSqlParam[19].ParameterName = "@VehicleReplacement";
                objListSqlParam[19].Value = objMembership.VehicleReplacement;

                objListSqlParam[20] = new SqlParameter();
                objListSqlParam[20].ParameterName = "@RiskAddress";
                objListSqlParam[20].Value = objMembership.RiskAddress;

                objListSqlParam[21] = new SqlParameter();
                objListSqlParam[21].ParameterName = "@NoOfLocation";
                objListSqlParam[21].Value = objMembership.NoOfLocation;

                objListSqlParam[22] = new SqlParameter();
                objListSqlParam[22].ParameterName = "@TypeOfService";
                objListSqlParam[22].Value = objMembership.TypeOfService;

                objListSqlParam[23] = new SqlParameter();
                objListSqlParam[23].ParameterName = "@CreatedBy";
                objListSqlParam[23].Value = objMembership.CreatedBy;

                objListSqlParam[24] = new SqlParameter();
                objListSqlParam[24].ParameterName = "@ModifiedBy";
                objListSqlParam[24].Value = objMembership.ModifiedBy;


                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveMemebership", objListSqlParam).Tables[0];
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

        public MethodOutput<Membership> GetAllMemberShip(int SourceId,int PackageId, Int64 MembershipId)
        {
            MethodOutput<Membership> output = new MethodOutput<Membership>();
            List<Membership> objLst = new List<Membership>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[3];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@SourceId";
                objListSqlParam[0].Value = SourceId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@PackageId";
                objListSqlParam[1].Value = PackageId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@MembershipId";
                objListSqlParam[2].Value = MembershipId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ShowAllMemberShip", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new Membership
                    {
                        SourceType= x.Field<string>("Broker_Type"),
                        Branch = x.Field<string>("Branch"),
                        ChassisNo= x.Field<string>("ChassisNo"),
                        CPRNumber = x.Field<string>("CPRNumber"),
                        CreatedBy = x.Field<int?>("CreatedBy"),
                        EmailId = x.Field<string>("EmailId"),
                        CreatedByUser = x.Field<string>("CreatedByUser"),
                        CreatedDate = x.Field<DateTime?>("CreatedDate"),
                        EffectiveDate = x.Field<DateTime?>("EffectiveDate"),
                        ExpiryDate = x.Field<DateTime?>("ExpiryDate"),
                        InsuredName = x.Field<string>("InsuredName"),
                        IssueDate = x.Field<DateTime?>("IssueDate"),
                        MembershipId = x.Field<long>("MembershipId"),
                        MobileNo = x.Field<string>("MobileNo"),
                        ModifiedBy = x.Field<int?>("ModifiedBy"),
                   //     ModifiedByUser = x.Field<string>("ModifiedByUser"),
                        ModifiedDate = x.Field<DateTime?>("ModifiedDate"),
                        NoOfLocation = x.Field<int?>("NoOfLocation"),
                        PackageId = x.Field<int>("PackageId"),
                        PackageName = x.Field<string>("PackageName"),
                        PolicyNo = x.Field<string>("PolicyNo"),
                        PolicyType = x.Field<string>("PolicyType"),
                        RiskAddress = x.Field<string>("RiskAddress"),
                        SourceId = x.Field<int>("SourceId"),
                        SourceName = x.Field<string>("SourceName"),
                        TypeOfService = x.Field<string>("TypeOfService"),
                        VehicleBody = x.Field<string>("VehicleBody"),
                        VehicleMake = x.Field<string>("VehicleMake"),
                        VehicleRegistrationNo = x.Field<string>("VehicleRegistrationNo"),
                        VehicleReplacement = x.Field<bool>("VehicleReplacement"),
                        VehicleType = x.Field<string>("VehicleType"),
                        VehicleYear = x.Field<int?>("VehicleYear"),
                    }).ToList();

                    output.DataList = objLst;
                    output.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                output.DataList = objLst;
                output.ErrorMessage = ex.Message;
            }
            return output;
        }


        public SearchCPROutput SearchMemberShipByCPRNumber(string CPRNumber, int SourceId)
        {
            SearchCPROutput objOutput = new SearchCPROutput();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[3];
               
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CPRNumber";
                objListSqlParam[0].Value = CPRNumber;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SeachMemberShipByCPRNumber", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objOutput.IsValidCPR = true;

                    if (Convert.ToInt32(dt.Rows[0]["SourceId"]) == SourceId)
                    {
                        objOutput.IsCPRMatchesWithSource = true;
                    }
                    else
                    {
                        objOutput.IsCPRMatchesWithSource = false;
                    }
                }
                else
                {
                    objOutput.IsValidCPR = false;
                    objOutput.IsCPRMatchesWithSource = false;
                }
                objOutput.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                objOutput.IsCPRMatchesWithSource = false;
                objOutput.ErrorMessage = ex.Message;
                objOutput.IsValidCPR = false;
            }
            return objOutput;
        }
    }
}
