using System;
using System.Data;
using System.IO;
using VCM.Common.Log;

public class ExportAppData
{
    private static ExportAppData _instance = new ExportAppData();
    public static ExportAppData Instance()
    {
        return _instance;
    }

    public DataTable GetExportData(string Calcname, string ConnectionID)
    {
        DataTable dt = new DataTable();
        try
        {
            string ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[Calcname]);

            ProductClassGeneric pcg = (ProductClassGeneric)BeastConn.Instance.getBeastImage(ProductID, ConnectionID, "0", "0", "0", Calcname);
            string strExportDataValue = pcg.ExportField.Trim();


            if (!string.IsNullOrEmpty(strExportDataValue))
            {
                if (strExportDataValue.Substring(0, 20).ToLower().Contains("<currnex>"))
                {
                    StringReader strReader = new StringReader(strExportDataValue);
                    DataSet ds = new DataSet();
                    ds.ReadXml(strReader);
                    dt = ds.Tables[1];
                }
                else
                {
                    string _header = strExportDataValue.Substring(0, strExportDataValue.IndexOf('\n'));
                    string[] arrHeader = _header.Split(',');

                    string _rows = strExportDataValue.Remove(0, _header.Length);
                    string[] arrRows = _rows.Split('\n');

                    string[] fields = null;

                    foreach (string header in arrHeader)
                    {
                        dt.Columns.Add(header);
                    }

                    foreach (string _row in arrRows)
                    {
                        fields = _row.Split(',');
                        DataRow dr = dt.NewRow();
                        for (int x = 0; x < fields.Length; x++)
                        {
                            dr[x] = fields[x];
                        }
                        dt.Rows.Add(dr);
                    }

                    dt.Rows[0].Delete();
                    dt.AcceptChanges();
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("ExportAppData.cs", "GetExportData()", "Error getting export data", ex);
        }
        return dt;
    }
}
