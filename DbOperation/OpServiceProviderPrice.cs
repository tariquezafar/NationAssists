using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEngine;
using Utility;
using System.Data.SqlClient;
using System.Data;

namespace DbOperation
{
    public class OpServiceProviderPrice
    {
        public MethodOutput<ServiceSubCategoryPrice> GetServiceSubCategoryPriceList(int ServiceProviderId, int ServiceId)
        {
            MethodOutput<ServiceSubCategoryPrice> output = new MethodOutput<ServiceSubCategoryPrice>();
            List<ServiceSubCategoryPrice> objPriceLst = new List<ServiceSubCategoryPrice>();
            List<ServiceSubCategoryPrice> objLst = new List<ServiceSubCategoryPrice>();
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = ServiceProviderId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetServiceProviderPrice", objListSqlParam);
                if (ds.Tables.Count > 0)
                {
                    ServiceSubCategoryPrice objServiceCatPrice = new ServiceSubCategoryPrice();
                    DataTable dtServ = new DataTable();
                    dtServ = ds.Tables[0];
                    objServiceCatPrice.PriceOption = Convert.ToString(dtServ.Rows[0]["Pricing_option"]);
                    objServiceCatPrice.PriceCount = Convert.ToInt32(dtServ.Rows[0]["PRICE_COUNT"]);
                    dt= ds.Tables[1];
                    if (  dt.Rows.Count > 0 )
                    {
                        if (objServiceCatPrice.PriceOption == "SB")
                        {
                            objLst = dt.AsEnumerable().Select(x => new ServiceSubCategoryPrice
                            {

                                ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                                SubCategoryName = x.Field<string>("SUBCAT_NAME"),
                                Price = x.Field<decimal?>("Price"),
                                StartDate = x.Field<DateTime?>("StartDate"),
                                EndDate = x.Field<DateTime?>("EndDate"),
                                IsActive = x.Field<bool?>("IsActive") == null ? false : x.Field<bool?>("IsActive"),
                                Created_Date = x.Field<DateTime?>("CREATED_DATE"),

                            }).ToList();
                            
                        }
                        else
                        {
                            if (objServiceCatPrice.PriceOption == "PB" && objServiceCatPrice.PriceCount > 0)
                            {
                                
                                foreach (DataRow item in dt.Rows)
                                {
                                    if (Convert.ToInt32(item["BatchNo"]) !=0 && objLst.Where(x => x.BatchNo == Convert.ToInt32(item["BatchNo"])).Any() == false)
                                    {
                                        objLst.Add(new ServiceSubCategoryPrice
                                        {

                                            BatchNo = Convert.ToInt32(item["BatchNo"]),
                                            Created_Date = Convert.ToDateTime(item["CREATED_DATE"]),
                                            StartDate = Convert.ToDateTime(item["StartDate"]),
                                            EndDate = Convert.ToDateTime(item["EndDate"]),
                                            IsActive = Convert.ToBoolean(item["IsActive"]),
                                            Price = Convert.ToDecimal(item["Price"]),
                                            SubCategoryName= Convert.ToString(item["SUBCAT_NAME"]),

                                        }) ;
                                    }
                                }
                            }
                        }

                        objServiceCatPrice.SubCategoryPriceList = objLst;
                        objPriceLst.Add(objServiceCatPrice);
                        output.DataList = objPriceLst;
                        output.ErrorMessage = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                output.DataList = objLst;
                output.ErrorMessage = ex.Message;
            }
            return output;
        }

        public MethodOutput<string> SaveSubCategoryPrice(ServiceSubCategoryPrice objSubCategoryPrice)

        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                string strServiceOptedXML = string.Empty;
                string SubCategoryPriceXml = string.Empty;
                #region Generate XML For ServcieOpted
                

                if (objSubCategoryPrice.SubCategoryPriceList!= null && objSubCategoryPrice.SubCategoryPriceList.Any())
                {
                    SubCategoryPriceXml += "<ServiceProvider>";
                    foreach (ServiceSubCategoryPrice item in objSubCategoryPrice.SubCategoryPriceList)
                    {
                        SubCategoryPriceXml += "<SubCategoryPrice>";
                        SubCategoryPriceXml += "<ServiceSubCategoryId>" + item.ServiceSubCategoryId + "</ServiceSubCategoryId>";
                        SubCategoryPriceXml += "<Price>" + item.Price + "</Price>";
                        SubCategoryPriceXml += "<StartDate>" + item.StartDate + "</StartDate>";
                        SubCategoryPriceXml += "<EndDate>" + item.EndDate + "</EndDate>";
                        SubCategoryPriceXml += "</SubCategoryPrice>";

                    }
                    SubCategoryPriceXml += "</ServiceProvider>";

                }
                #endregion

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[10];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = objSubCategoryPrice.ServiceProviderId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = objSubCategoryPrice.ServiceId;

                
                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@SubCategoryPriceXml";
                objListSqlParam[2].Value = SubCategoryPriceXml;


                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceProviderPrice", objListSqlParam).Tables[0];
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
