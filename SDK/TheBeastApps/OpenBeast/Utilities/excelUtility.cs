using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using VCM.Common.Log;

/// <summary>
/// Summary description for excelUtility
/// </summary>
/// 
public class excelUtility
{
    public static DataSet ImportExcelXLS(string FileName, bool hasHeaders, string path)
    {
        DataSet output = new DataSet();

        try
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    string sheet = row["TABLE_NAME"].ToString();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    DataTable outputTable = new DataTable(sheet);
                    output.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);
                }
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("excelUtility.cs", "ImportExcelXLS()", ex.Message, ex);
        }
        return output;
    }
}
