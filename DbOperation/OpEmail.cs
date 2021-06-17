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
   public class OpEmail
    {

        public EmailConfiguration GetEmailConfiguration()
        {
            EmailConfiguration objEC = new EmailConfiguration();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetEmailConfiguration").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objEC.EmailConfigurationId = Convert.ToInt32(dt.Rows[0]["EmailConfigurationId"]);
                    objEC.Port = Convert.ToInt32(dt.Rows[0]["Port"]);
                    objEC.EmailId= Convert.ToString(dt.Rows[0]["EmailId"]);
                    objEC.Host = Convert.ToString(dt.Rows[0]["Host"]);
                    objEC.EnableSSl=Convert.ToBoolean(dt.Rows[0]["EnableSSl"]);
                    objEC.Password = Convert.ToString(dt.Rows[0]["Password"]);
                    objEC.DisplayName = Convert.ToString(dt.Rows[0]["DisplayName"]);
                }
            }
            catch (Exception ex)
            {
                
            }
            return objEC;
        }

        public MethodOutput<Template> GetEmailTemplates(string TemplateCode)
        {
            MethodOutput<Template> objOutput = new MethodOutput<Template>();
            List<Template> objLstTemplate = new List<Template>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@TemplateCode";
                objListSqlParam[0].Value = TemplateCode;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetEmailTemplate", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstTemplate = dt.AsEnumerable().Select(x => new Template
                    {
                        EmailTemplateMasterId = x.Field<int>("EmailTemplateMasterId"),
                        TemplateCode = x.Field<string>("TemplateCode"),
                        Body = x.Field<string>("Template")

                    }).ToList();

                    objOutput.DataList = objLstTemplate;
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
