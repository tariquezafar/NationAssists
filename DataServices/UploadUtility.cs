using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Presentation;
using System.Configuration;
using static DataServices.AppEnumerators;
using DataServices.Models;
using DbOperation;
using DataEngine;

namespace DataServices
{
   public class UploadUtility
    {
        //private static readonly string[] HomeAssistanceMadatoryColumns = { "ORIGINAL POLICY NO.", "NAME", "CPR NUMBER", "MOBILE NUMBER", "EMAIL ID", "RISK ADDRESS.", "ISSUEDATE", "EFF. DATE", "EXP. DATE", "NO. OF LOCATION" };
        //private static readonly string[] RoadSideAssistanceMandatoryColumns = { "POLICY TYPE" , "ISSUEDATE", "EFF. DATE", "EXP. DATE", "INSURED NAME", "CPR NUMBER", "MOBILE NO.", "EMAIL ID", "VEH. NO", "CHASSIS NO", "VEH. YEAR", "PACKAGES" };
        //public static readonly string[] Sheets = { "HA", "RA & CR" };


        public List<ExcelUploaderResponse> ProcessExcelData(FileStream fileSteam ,int SourceId,int UserID)
        {
            List<ExcelUploaderResponse> objLstEUR = new List<ExcelUploaderResponse>();
            
            ExcelValidationResponse objExcelValidationResponse = new ExcelValidationResponse();
            objExcelValidationResponse = this.ExcelValidation(fileSteam);
            if (objExcelValidationResponse.ExcelData != null && objExcelValidationResponse.ExcelData.Count > 0)
            {
                foreach (ExcelData item in objExcelValidationResponse.ExcelData)
                {
                    ExcelUploaderResponse objEUR = new ExcelUploaderResponse();
                    objEUR.sheetName = item.SheetName;

                    if (string.IsNullOrEmpty(item.ErrorMessage))
                    {
                        OpMembership objMembership = new OpMembership();
                        MethodOutput<string> objOp = new MethodOutput<string>();
                        objOp = objMembership.MemembershipBulkInsert(item, SourceId, UserID);
                        if (objOp.RowAffected > 0)
                        {
                            objEUR.ErrorMessage = string.Empty;

                        }
                        else
                        {
                            objEUR.ErrorMessage = objOp.ErrorMessage;
                        }
                    }
                    else
                    {
                        objEUR.ErrorMessage = objExcelValidationResponse.ErrorMessage;
                    }
                    objLstEUR.Add(objEUR);
                }
            }
            else
            {


                objLstEUR.Add(new ExcelUploaderResponse { sheetName = string.Empty ,ErrorMessage = objExcelValidationResponse.ErrorMessage }); ;
            }


            return objLstEUR;
        }

        public List<ExcelData> ImprortExcelToDateTable(FileStream FileStream)
        {
            List<ExcelData> sheetIds = new List<ExcelData>();
            List<string> lstsheets = new List<string>();
            List<string> RA_Columns = new List<string>();
            List<string> HA_Columns = new List<string>();
            List<string> RA_Mandatory_Columns= new List<string>();
            List<string> HA_Mandatory_Columns = new List<string>();

            lstsheets = GetConfigValue("SheetsName");
            RA_Columns = GetConfigValue("RoadSideAssistanceColumns");
            HA_Columns = GetConfigValue("HomeAssistanceColumns");
            RA_Mandatory_Columns = GetConfigValue("RoadSideAssistanceMandatoryColumns");
            HA_Mandatory_Columns = GetConfigValue("HomeAssistanceMadatoryColumns");
            string[] DateTypeColumns = { "EXP. DATE", "EFF. DATE", "ISSUEDATE" };
            string[] intTypeColumns = { "NO. OF LOCATION" , "VEH. YEAR" };
            try
            {               
                using (var ms=new MemoryStream())
                {
                    FileStream.CopyTo(ms);

                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(ms,false))
                    {                       
                        var sheets = doc.WorkbookPart.Workbook.Sheets;
                        foreach (var sheet in sheets)
                        {
                            if (sheet.GetAttributes().Any())
                            {
                                if (lstsheets.Contains( sheet.GetAttributes()[0].Value ))
                                {
                                    sheetIds.Add(
                                        new ExcelData
                                        {
                                            SheetId = sheet.GetAttributes()[2].Value,
                                            SheetName = sheet.GetAttributes()[0].Value,
                                            dt = new DataTable(),
                                            MandatoryColumns = sheet.GetAttributes()[0].Value.ToUpper() == "HA" ? HA_Mandatory_Columns : RA_Mandatory_Columns,
                                            Columns = sheet.GetAttributes()[0].Value.ToUpper() == "HA" ? HA_Columns : RA_Columns
                                        });
                                }
                            }
                        }

                        foreach (ExcelData objSheet in sheetIds)
                        {

                            if (!string.IsNullOrEmpty(objSheet.SheetName))
                            {
                                Worksheet worksheet = (doc.WorkbookPart.GetPartById(objSheet.SheetId) as WorksheetPart).Worksheet;
                                IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                                foreach (Row row in rows)
                                {
                                    if (row.RowIndex.Value == 1)
                                    {
                                        foreach (Cell cell in row.Descendants<Cell>())
                                        {
                                            string ColumnName = GetValue(doc, cell);
                                            if (objSheet.Columns.Any(x=> x==ColumnName.Trim().ToUpper()))
                                            {
                                                if (DateTypeColumns.Contains(ColumnName.Trim().ToUpper()))
                                                {
                                                    objSheet.dt.Columns.Add(ColumnName.Trim().ToUpper(), typeof(DateTime));
                                                }
                                                else if (intTypeColumns.Contains(ColumnName.Trim().ToUpper()))
                                                {
                                                    objSheet.dt.Columns.Add(ColumnName.Trim().ToUpper(), typeof(int));
                                                }
                                                else
                                                {
                                                    objSheet.dt.Columns.Add(ColumnName.Trim().ToUpper(), typeof(string));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        objSheet.dt.Rows.Add();

                                        for (int j = 0; j < row.Descendants<Cell>().Count(); j++)
                                        {
                                            Cell cell = row.Descendants<Cell>().ElementAt(j);
                                            int actualCellIndex = CellReferenceToIndex(cell);
                                            string CellValue = GetValue(doc, cell);
                                            if (objSheet.dt.Columns[actualCellIndex].DataType == typeof(DateTime) && !string.IsNullOrEmpty( CellValue))
                                            {
                                                objSheet.dt.Rows[objSheet.dt.Rows.Count - 1][actualCellIndex] = DateTime.FromOADate(Convert.ToDouble(CellValue)).ToString("yyyy-MM-dd");
                                            }
                                            else if (objSheet.dt.Columns[actualCellIndex].DataType == typeof(int) && !string.IsNullOrEmpty(CellValue))
                                            {
                                                objSheet.dt.Rows[objSheet.dt.Rows.Count - 1][actualCellIndex] = Convert.ToInt32(CellValue);
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(CellValue))
                                                {
                                                    objSheet.dt.Rows[objSheet.dt.Rows.Count - 1][actualCellIndex] = DBNull.Value;
                                                }
                                                else
                                                {
                                                    objSheet.dt.Rows[objSheet.dt.Rows.Count - 1][actualCellIndex] = CellValue;
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }

            }
            catch (Exception e)
            {

                throw e;
            }

            return sheetIds;
        }

        private string GetValue(SpreadsheetDocument doc,Cell cell)
        {
            string value = string.Empty;
            if (cell.CellValue != null)
            {
                value = cell.CellValue.InnerText;
                if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
                {
                    return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
                }
            }

            return value;
            
        }

        private static int CellReferenceToIndex(Cell cell)
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                {
                    return index;
                }
            }

            return index;
        }

        public ExcelValidationResponse ExcelValidation(FileStream fileSteam)
        {
            List<string> lstRAExcelColumn = new List<string>();
            List<string> lstHAExcelColumn = new List<string>();
            List<string> lstVehNo = new List<string>();
            List<string> lstChessisNo = new List<string>();
            ExcelValidationResponse objResponse = new ExcelValidationResponse();
            int uploadedRecordCount =Convert.ToInt32( ConfigurationManager.AppSettings["ExcelMaxCount"]);
            List<ExcelData> objLstExcelData = new List<ExcelData>();


            try
            {
               
                if (Path.GetExtension(fileSteam.Name) == ".xlsx")
                {
                    List<ExcelData> objLstData = ImprortExcelToDateTable(fileSteam);

                    if (objLstData.Any())
                    {
                        foreach (ExcelData objExcelData in objLstData)
                        {

                            if (objExcelData.dt != null && objExcelData.dt.Rows.Count > 0)
                            {
                                if (objExcelData.dt.Rows.Count <= uploadedRecordCount)
                                {
                                    foreach (var column in objExcelData.dt.Columns)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(column)))
                                        {
                                            lstRAExcelColumn.Add(Convert.ToString(column).Trim().ToUpper());
                                        }
                                    }

                                    foreach (var columnName in objExcelData.MandatoryColumns)
                                    {
                                        if (lstRAExcelColumn.Contains(columnName))
                                        {
                                            //for (int i = 0; i < objExcelData.dt.Columns.Count; i++)
                                            //{
                                                for (int j = 0; j < objExcelData.dt.Rows.Count; j++)
                                                {
                                                    if (string.IsNullOrEmpty(Convert.ToString(objExcelData.dt.Rows[j][columnName])))
                                                    {
                                                        objExcelData.ErrorMessage = objExcelData.ErrorMessage + "Missing mendatory value identified : Column '" + columnName + "', Row #" + Convert.ToString(j + 1) + " in sheet " + objExcelData.SheetName + " .\n";
                                                        objResponse.ErrorMessage = objResponse.ErrorMessage + "Missing mendatory value identified : Column '" + columnName + "', Row #" + Convert.ToString(j + 1) + " in sheet " + objExcelData.SheetName + " .\n";
                                                        
                                                    }
                                                    else
                                                    {

                                                         if (objExcelData.dt.Columns[j].ColumnName.ToUpper() == "CHASSIS NO")
                                                        {
                                                            if (lstChessisNo.Any(x => x.Equals(Convert.ToString(objExcelData.dt.Rows[j][columnName]))))
                                                            {
                                                                objExcelData.ErrorMessage = objExcelData.ErrorMessage + "Duplicate Chessis No identified : Column '" + columnName + "', Row #" + Convert.ToString(j + 1) + " in sheet " + objExcelData.SheetName + " .\n";
                                                                objResponse.ErrorMessage = objResponse.ErrorMessage + "Duplicate Chessis No identified : Column '" + columnName + "', Row #" + Convert.ToString(j + 1) + " in sheet " + objExcelData.SheetName + " .\n";
                                                            }
                                                            else
                                                            {
                                                                lstChessisNo.Add(Convert.ToString(objExcelData.dt.Rows[j][columnName]));
                                                            }


                                                        }


                                                    }
                                                }
                                            //}
                                        }
                                    }

                                    objLstExcelData.Add(objExcelData);

                                }
                                else
                                {
                                    objResponse.ErrorMessage = objExcelData.ErrorMessage + "Excel sheet " + objExcelData.SheetName + " has more than " + uploadedRecordCount + " row count.Current Row is " + objExcelData.dt.Rows.Count + " in sheet " + objExcelData.SheetName + " .\n";
                                    objResponse.ErrorMessage = objResponse.ErrorMessage + "Excel sheet " + objExcelData.SheetName + " has more than " + uploadedRecordCount + " row count.Current Row is " + objExcelData.dt.Rows.Count + " in sheet " + objExcelData.SheetName + " .\n";
                                }
                            }
                            else
                            {
                                objExcelData.ErrorMessage = objExcelData.ErrorMessage + objExcelData.SheetName+ " Sheet has no data.\n";
                                objResponse.ErrorMessage = objResponse.ErrorMessage + objExcelData.SheetName + " Sheet has no data.\n";
                                
                            }
                            


                        }
                    }
                    else
                    {
                        objResponse.ErrorMessage = objResponse.ErrorMessage + "Sheet is missing or Excel has no data.\n";
                    }
                }
                else
                {
                    objResponse.ErrorMessage = objResponse.ErrorMessage + "File Uplaoded is not  valid type.\n";
                }

                objResponse.ExcelData = objLstExcelData;

            }
            catch (Exception ex)
            {

                throw;
            }

            return objResponse;
        }

        private bool ValidateExcel(BulkUploadTypes uploadType, List<ExcelData> objExcelDataList, out List<ErrorMessageType> objErroMsg)
        {
            bool IsValid = true;
            List<ErrorMessageType> objErrList = new List<ErrorMessageType>();
            string[] strValidExtensions = { "xls", "xlsx" };           
            string strSheetName = (uploadType == BulkUploadTypes.HomeAssistance) ? "HA" : "R & A";

            ExcelData objExcelData = objExcelDataList.Where(s => s.SheetName == strSheetName).FirstOrDefault();

            if (objExcelData == null)
            {
                objErrList.Add(new ErrorMessageType
                {
                    ErrorCode = "UnMatchedSheet",
                    ErrorMessage = "No matching sheet available to process."
                });
            }
            else
            {
                if (objExcelData.dt.Rows.Count == 0)
                {
                    objErrList.Add(new ErrorMessageType
                    {
                        ErrorCode = "ZeroRows",
                        ErrorMessage = "No rows to process."
                    });
                }
            }

            objErroMsg = objErrList;
            return IsValid;
        }


        private List<string> GetConfigValue(string Key)
        {
            List<string> Values = new List<string>();
            if (ConfigurationManager.AppSettings[Key] != null && ConfigurationManager.AppSettings[Key] != "")
            {
                string KeyValues = ConfigurationManager.AppSettings[Key];
                if (KeyValues.IndexOf(",") != -1)
                {
                    string[] keys = KeyValues.Split(',');
                    foreach (string item in keys)
                    {
                        Values.Add(item.Trim().ToUpper());
                    }
                }
            }

            return Values;
        }
    }

  

  


   
}
