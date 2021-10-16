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

        public MethodOutput<Membership> GetAllMemberShip(int SourceId,int PackageId, Int64 MembershipId,string CPRNumber ,string PolicyType,string PolicyNo,string InsuredName
            ,string MobileNo,string EmailId,string VehicleRegistrationNo,string ChassisNo,string SourceType,string service, DateTime? StartDate, DateTime? EndDate)
        {
            MethodOutput<Membership> output = new MethodOutput<Membership>();
            List<Membership> objLst = new List<Membership>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[18];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@SourceId";
                objListSqlParam[0].Value = SourceId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@PackageId";
                objListSqlParam[1].Value = PackageId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@MembershipId";
                objListSqlParam[2].Value = MembershipId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@CPRNumber";
                objListSqlParam[3].Value = CPRNumber;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@PolicyType";
                objListSqlParam[4].Value = PolicyType;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@PolicyNo";
                objListSqlParam[5].Value = PolicyNo;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@InsuredName";
                objListSqlParam[6].Value = InsuredName;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@MobileNo";
                objListSqlParam[7].Value = MobileNo;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@EmailId";
                objListSqlParam[8].Value = EmailId;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@VehicleRegistrationNo";
                objListSqlParam[10].Value = VehicleRegistrationNo;

                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@ChassisNo";
                objListSqlParam[11].Value = ChassisNo;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@SourceType";
                objListSqlParam[12].Value = SourceType;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@service";
                objListSqlParam[13].Value = service;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@StartDate";
                objListSqlParam[14].Value = StartDate;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@EndDate";
                objListSqlParam[15].Value = EndDate;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ShowAllMemberShip", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new Membership
                    {
                        SourceType = x.Field<string>("Broker_Type"),
                        Branch = x.Field<string>("Branch"),
                        ChassisNo = x.Field<string>("ChassisNo"),
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
                        MembershipReferenceNo = x.Field<string>("MembershipReferenceNo"),
                        Issue_Date = x.Field<string>("Issue_Date"),
                        Effective_Date = x.Field<string>("Effective_Date"),
                        Expiry_Date = x.Field<string>("Expiry_Date")
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

        public MethodOutput<string> MemembershipBulkInsert(ExcelData exceldata,int SourceId, int UserId)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                exceldata.dt.Columns["Policy No"].ColumnName = "PolicyNo";
                
                exceldata.dt.Columns["Insured Name"].ColumnName = "InsuredName";
                exceldata.dt.Columns["CPR Number"].ColumnName = "CPRNumber";
                exceldata.dt.Columns["Mobile Number"].ColumnName = "MobileNo";
                exceldata.dt.Columns["Email ID"].ColumnName = "EmailId";
                if (exceldata.SheetName == "HA")
                {
                    exceldata.dt.Columns["House No."].ColumnName = "HouseNo";
                    exceldata.dt.Columns["Risk Address"].ColumnName = "RiskAddress";
                    exceldata.dt.Columns["No. of Location"].ColumnName = "NoOfLocation";
                    exceldata.dt.Columns.Add("PolicyType", typeof(string));
                    exceldata.dt.Columns["PolicyType"].DefaultValue = string.Empty;
                    exceldata.dt.Columns.Add("VehicleRegistrationNo", typeof(string));
                    exceldata.dt.Columns["VehicleRegistrationNo"].DefaultValue = string.Empty;

                    exceldata.dt.Columns.Add("VehicleMake", typeof(string));
                    exceldata.dt.Columns["VehicleMake"].DefaultValue = string.Empty;

                    exceldata.dt.Columns.Add("ChassisNo", typeof(string));
                    exceldata.dt.Columns["ChassisNo"].DefaultValue = string.Empty;

                    exceldata.dt.Columns.Add("VehicleType", typeof(string));
                    exceldata.dt.Columns["VehicleType"].DefaultValue = string.Empty;

                    exceldata.dt.Columns.Add("VehicleBody", typeof(string));
                    exceldata.dt.Columns["VehicleBody"].DefaultValue = string.Empty;

                    exceldata.dt.Columns.Add("VehicleYear", typeof(int));
                    exceldata.dt.Columns["VehicleYear"].DefaultValue = 0;

                }
                else
                {
                    exceldata.dt.Columns["Policy Type"].ColumnName = "PolicyType";
                    exceldata.dt.Columns.Add("RiskAddress", typeof(string));
                    exceldata.dt.Columns["RiskAddress"].DefaultValue = string.Empty;
                    exceldata.dt.Columns.Add("HouseNo", typeof(string));
                    exceldata.dt.Columns["HouseNo"].DefaultValue = string.Empty;
                    exceldata.dt.Columns.Add("Road", typeof(string));
                    exceldata.dt.Columns["Road"].DefaultValue = string.Empty;
                    exceldata.dt.Columns.Add("Block", typeof(string));
                    exceldata.dt.Columns["Block"].DefaultValue = string.Empty;
                    exceldata.dt.Columns.Add("Area", typeof(string));
                    exceldata.dt.Columns["Area"].DefaultValue = string.Empty;
                    exceldata.dt.Columns.Add("Governorate", typeof(string));
                    exceldata.dt.Columns["Governorate"].DefaultValue = string.Empty;
                    exceldata.dt.Columns["Veh. No"].ColumnName = "VehicleRegistrationNo";
                    exceldata.dt.Columns["Chassis No"].ColumnName = "ChassisNo";
                    exceldata.dt.Columns["Veh. Make"].ColumnName = "VehicleMake";
                    exceldata.dt.Columns["Veh. Type"].ColumnName = "VehicleType";
                    exceldata.dt.Columns["Veh. Body"].ColumnName = "VehicleBody";
                    exceldata.dt.Columns["Veh. Year"].ColumnName = "VehicleYear";
                }
                
                exceldata.dt.Columns["Eff. Date"].ColumnName = "EffectiveDate";

                exceldata.dt.Columns["Exp. date"].ColumnName = "ExpiryDate";
                
               
                

                

                exceldata.dt.Columns.Add("TypeOfService", typeof(string));
                exceldata.dt.Columns["TypeOfService"].DefaultValue = "";

                exceldata.dt.Columns.Add("CreatedBy", typeof(int));
                exceldata.dt.Columns["CreatedBy"].DefaultValue = UserId;

                exceldata.dt.Columns.Add("ModifiedBy", typeof(int));
                exceldata.dt.Columns["ModifiedBy"].DefaultValue = UserId;

                exceldata.dt.Columns.Add("CreatedDate", typeof(DateTime));
                exceldata.dt.Columns["CreatedDate"].DefaultValue = DateTime.Now;

                exceldata.dt.Columns.Add("ModifiedDate", typeof(DateTime));
                exceldata.dt.Columns["ModifiedDate"].DefaultValue = DateTime.Now;

                exceldata.dt.Columns.Add("SourceId", typeof(int));
                exceldata.dt.Columns["SourceId"].DefaultValue = SourceId;

                exceldata.dt.Columns.Add("PackageId", typeof(int));

                exceldata.dt.Columns.Add("VehicleReplacement", typeof(bool));


                exceldata.dt.AcceptChanges();

                foreach (DataRow dr in exceldata.dt.Rows)
                {
                    dr["SourceId"] = SourceId;
                    dr["ModifiedDate"] = DateTime.Now;
                    dr["CreatedDate"] = DateTime.Now;
                    dr["ModifiedBy"] = UserId;
                    dr["CreatedBy"] = UserId;

                    if (dr.Table.Columns["Packages"]!=null && dr["Packages"] != null)
                    {

                        
                        if (string.IsNullOrEmpty( Convert.ToString(dr["Packages"])) && Convert.ToString(dr["Packages"]).ToUpper() == ServiceEnum.NO.ToString())
                        {
                            dr["VehicleReplacement"] = false;
                            dr["PackageId"] = 1;
                        }
                        else
                        {
                            dr["VehicleReplacement"] = true;
                            dr["PackageId"]=(int)Enum.Parse(typeof(ServiceEnum), (Convert.ToString(dr["Packages"]).ToUpper()));
                          
                        }
                        

                    }
                    else
                    {
                        dr["VehicleReplacement"] = false;
                        dr["PackageId"] = 11;
                    }
                }

                DataSet ds = new DataSet();
                    ds.Tables.Add(exceldata.dt);
                string strXML = ds.GetXml();

                SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString);
                connection.Open();
                SqlCommand cmd = new SqlCommand("usp_MembershipBulkUpload", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                //Pass table Valued parameter to Store Procedure
                SqlParameter sqlParam = cmd.Parameters.AddWithValue("@MemberShipXml", strXML);
                sqlParam.SqlDbType = SqlDbType.Xml;
            output.RowAffected=    cmd.ExecuteNonQuery();
                connection.Close();
                



            }
            catch (Exception ex)
            {

                output.ErrorMessage = ex.Message;
            }

            return output;
        }

        public MethodOutput<bool> CheckDuplicateChassisNo(string ChassisNo, DateTime StartDate)

        {
            MethodOutput<bool> objOutPut = new MethodOutput<bool>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[3];

                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ChassisNo";
                objListSqlParam[0].Value = ChassisNo;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@PolicyStartDate";
                objListSqlParam[1].Value = StartDate;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ValidateDuplicateMemebership", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0  )
                {
                    objOutPut.Data = Convert.ToString(dt.Rows[0]["DUP_CHASSIS"]) == "1" ? false : true;
                }
                else
                {
                    objOutPut.Data = false;
                }
               
            }
            catch (Exception ex)
            {

                objOutPut.ErrorMessage = ex.Message;

            }

            return objOutPut;
        }
    }

    public enum ServiceEnum
    {
        NO = 1,
        SC8=2,
        SC10=3 ,
        SC15 = 4,
        MC8=5,
        MC10=6,
        MC15 = 7,
        LC8=8,
        LC10=9,
        LC15=10
       
    }
}
