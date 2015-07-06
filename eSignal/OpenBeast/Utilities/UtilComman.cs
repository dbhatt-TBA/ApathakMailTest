using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using VCM.Common.Log;

/// <summary>
/// Summary description for UtilComman
/// </summary>
public class UtilComman
{
    public UtilComman()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static bool GetRowColGridField(string name, out int row, out int col)
    {
        int rowPos, colPos;
        string rowStr, colStr;
        row = -1;
        col = -1;
        rowPos = name.IndexOf('R');
        if (rowPos != -1)
        {
            try
            {
                colPos = name.IndexOf('C');
                if (colPos != -1)
                {
                    rowStr = name.Substring(rowPos + 1, colPos - rowPos - 1);
                    colStr = name.Substring(colPos + 1);
                    row = Convert.ToInt32(rowStr);
                    col = Convert.ToInt32(colStr);
                    // Row and column indices "-1" are reserved for column and row headings.
                    return row >= 0 && col >= 0;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UtilComman.cs", "GetRowColGridField()", ex.Message, ex);
            }
        }
        return false;
    }

    public static string getValueFromHashTable(Hashtable ht, string htKey)
    {
        if (ht.ContainsKey(htKey))
            return ht[htKey].ToString();

        return "";
    }

    public static void populateHashTable(ref Hashtable ht, string htString)
    {
        try
        {
            ht.Clear();

            string[] eleAry = htString.Split('|');

            string eleValue = "";
            string eleIndex = "";

            int crntCtr = -1;

            for (int iEleCtr = 0; iEleCtr < eleAry.Length; iEleCtr++)
            {
                if (eleAry[iEleCtr].Trim().Split('=').Length == 2)
                {
                    eleValue = eleAry[iEleCtr].Trim().Split('=')[0].ToString().Trim();
                    eleIndex = eleAry[iEleCtr].Trim().Split('=')[1].ToString().Trim();
                    crntCtr = Convert.ToInt32(eleIndex);
                }
                else
                {
                    eleValue = eleAry[iEleCtr].Trim().Split('=')[0].ToString().Trim();
                    crntCtr += 1;
                    eleIndex = Convert.ToString(crntCtr);
                }

                ht.Add(eleIndex, eleValue);
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilComman.cs", "populateHashTable()", ex.Message, ex);
        }
    }

    public static DataSet ConvertXMLToDataSet(XDocument xmlData)
    {
        try
        {
            DataSet xmlDS = new DataSet();

            XmlReader reader = xmlData.CreateReader();
            xmlDS.ReadXml(reader);

            return xmlDS;
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilComman.cs", "ConvertXMLToDataSet()", ex.Message, ex);
            return null;
        }
    }

    public static string ConvertXMLToJSONString(XDocument xmlData)
    {
        try
        {
            DataSet xmlDS = ConvertXMLToDataSet(xmlData);

            return GetJSONString(xmlDS.Tables[0]);
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilComman.cs", "ConverXMLToJSONString()", ex.Message, ex);
            return null;
        }
    }

    public static string GetJSONString(DataTable Dt)
    {
        string retValue = null;
        string HeadStr = string.Empty;
        StringBuilder Sb = null;
        Sb = new StringBuilder();
        try
        {
            string[] StrDc = new string[Dt.Columns.Count];

            
            for (int i = 0; i < Dt.Columns.Count; i++)
            {

                StrDc[i] = Dt.Columns[i].Caption;
                HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";

            }

            HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

            Sb.Append("{\"" + Dt.TableName + "\" : [");

            for (int i = 0; i < Dt.Rows.Count; i++)
            {

                string TempStr = HeadStr;

                Sb.Append("{");
                for (int j = 0; j < Dt.Columns.Count; j++)
                {
                    string tmpStr = Dt.Rows[i][j].ToString();

                    if (tmpStr.Contains("ERROR"))
                    {

                    }
                    tmpStr = tmpStr.Replace('[', ' ').Replace(']', ' ').Replace(@"""", "").Replace(Environment.NewLine, "_").Replace("\r\n", "_").Replace("\n", "_");

                    TempStr = TempStr.Replace(Dt.Columns[j] + j.ToString() + "¾", tmpStr);
                }
                Sb.Append(TempStr + "},");

            }
            Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));

            Sb.Append("]}");
            retValue = Sb.ToString();
        }
        catch (Exception ex)
        {
            Sb = null;
            HeadStr = null;
            LogUtility.Error("UtilComman.cs", "GetJSONString()", ex.Message, ex);
        }
        finally
        {
            Sb = null;
            HeadStr = null;
        }
        return retValue;
    }
}