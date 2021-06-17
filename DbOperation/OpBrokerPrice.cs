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
  public  class OpBrokerPrice
    {
        public MethodOutput<BrokerPrice> GetBrokerPriceList(int BrokerId, int ServiceId)
        {
            MethodOutput<BrokerPrice> output = new MethodOutput<BrokerPrice>();
            List<BrokerPrice> objPriceLst = new List<BrokerPrice>();
            List<BrokerPrice> objLst = new List<BrokerPrice>();
            try
            {

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@BrokerId";
                objListSqlParam[0].Value = BrokerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetBrokerPrice", objListSqlParam);
                if (ds.Tables.Count > 0)
                {
                    BrokerPrice objServiceCatPrice = new BrokerPrice();
                    DataTable dtServ = new DataTable();
                    dtServ = ds.Tables[0];
                    objServiceCatPrice.PriceOption = Convert.ToString(dtServ.Rows[0]["Pricing_option"]);
                    objServiceCatPrice.PriceCount = Convert.ToInt32(dtServ.Rows[0]["PRICE_COUNT"]);
                    dt = ds.Tables[1];
                    if (dt.Rows.Count > 0)
                    {
                        if (objServiceCatPrice.PriceOption == "SB")
                        {
                            objLst = dt.AsEnumerable().Select(x => new BrokerPrice
                            {

                                SubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                                SubCategoryName = x.Field<string>("SUBCAT_NAME"),
                                Price = x.Field<decimal?>("Price"),
                                StartDate = x.Field<DateTime?>("StartDate"),
                                EndDate = x.Field<DateTime?>("EndDate"),
                                IsActive = x.Field<bool?>("IsActive") == null ? false : x.Field<bool?>("IsActive"),
                                Created_Date = x.Field<DateTime?>("CreatedDate"),

                            }).ToList();

                        }
                        else
                        {
                            if (objServiceCatPrice.PriceOption == "PB" && objServiceCatPrice.PriceCount > 0)
                            {

                                foreach (DataRow item in dt.Rows)
                                {
                                    if (objLst.Where(x => x.BatchNo == Convert.ToInt32(item["BatchNo"])).Any() == false)
                                    {
                                        objLst.Add(new BrokerPrice
                                        {

                                            BatchNo = Convert.ToInt32(item["BatchNo"]),
                                            Created_Date = Convert.ToDateTime(item["CreatedDate"]),
                                            StartDate = Convert.ToDateTime(item["StartDate"]),
                                            EndDate = Convert.ToDateTime(item["EndDate"]),
                                            IsActive = Convert.ToBoolean(item["IsActive"]),
                                            Price = Convert.ToDecimal(item["Price"]),
                                            SubCategoryName = Convert.ToString(item["SUBCAT_NAME"]),

                                        });
                                    }
                                }
                            }
                        }

                        objServiceCatPrice.BrokerPriceList = objLst;
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

        public MethodOutput<string> SaveBrokerPrice(BrokerPrice objBrokerPrice)

        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                string strServiceOptedXML = string.Empty;
                string SubCategoryPriceXml = string.Empty;
                #region Generate XML For ServcieOpted


                if (objBrokerPrice.BrokerPriceList != null && objBrokerPrice.BrokerPriceList.Any())
                {
                    SubCategoryPriceXml += "<Broker>";
                    foreach (BrokerPrice item in objBrokerPrice.BrokerPriceList)
                    {
                        SubCategoryPriceXml += "<SubCategoryPrice>";
                        SubCategoryPriceXml += "<ServiceSubCategoryId>" + item.SubCategoryId + "</ServiceSubCategoryId>";
                        SubCategoryPriceXml += "<Price>" + item.Price + "</Price>";
                        SubCategoryPriceXml += "<StartDate>" + item.StartDate + "</StartDate>";
                        SubCategoryPriceXml += "<EndDate>" + item.EndDate + "</EndDate>";
                        SubCategoryPriceXml += "</SubCategoryPrice>";

                    }
                    SubCategoryPriceXml += "</Broker>";

                }
                #endregion

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[10];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@BrokerId";
                objListSqlParam[0].Value = objBrokerPrice.BrokerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = objBrokerPrice.ServiceId;


                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@BrokerPriceXml";
                objListSqlParam[2].Value = SubCategoryPriceXml;


                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveBrokerPrice", objListSqlParam).Tables[0];
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
