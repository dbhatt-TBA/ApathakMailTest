<%@ WebHandler Language="C#" Class="CSVHandler" %>

using System;
using System.Web;
using System.Data;

public class CSVHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            string localDateTime = context.Request.QueryString["curntDate"].ToString() + " Local";
            DateTime dat = DateTime.Now;
            string serverDateTime = dat.Year.ToString() + "-" + dat.Month.ToString() + "-" + dat.Day.ToString() + " " + dat.Hour.ToString() + ":" + dat.Minute.ToString() + ":" + dat.Second.ToString() + "." + dat.Millisecond.ToString() + " Server";

            string str = ",,,,,,,,,,,,,,,   Local Time  " + "," + "  Server Time  " + ",\n";
            str += ",,,,,,,,,,,,,,," + localDateTime + "," + serverDateTime + ",";

            string[] array = { "Vol", "1M", "3M", "6M", "9M", "1Y", "2Y", "3Y", "4Y", "5Y", "7Y", "10Y", "15Y", "20Y" };

            string lineTmp = "\r\n" + array[0] + ",";

            for (int col = 1; col <= 14; col++)
            {
                string colName = "";

                if (col <= 10)
                    colName = col.ToString() + "Y";
                else
                {
                    int tmpCol = 0;
                    if (col == 11)
                        tmpCol = 15;
                    else if (col == 12)
                        tmpCol = 20;
                    else if (col == 13)
                        tmpCol = 25;
                    else if (col == 14)
                        tmpCol = 30;

                    colName = tmpCol.ToString() + "Y";
                }

                lineTmp += colName + ",";

            }

            str += lineTmp + "\r\n";

            SwaptionVolPrimGrid.Class1 cls = SwaptionVolPrimGrid.Class1.Instance();

            DataTable dtVol = cls.GetdtVolGrid();

            for (int row = 0; row < dtVol.Rows.Count - 1; row++)
            {
                string line = "";

                line = array[row + 1] + ",";

                for (int coll = 0; coll < dtVol.Columns.Count; coll++)
                {
                    //line += dtVol.Rows[row][coll].ToString() + ".00" + ",";
                    line += dtVol.Rows[row][coll].ToString() + ",";
                }

                str += line + "\r\n";
            }
            str += "\n";

            str += lineTmp.Replace("Vol", "Prem") + "\r\n";

            DataTable dtPrem = cls.GetdtPremGrid();

            for (int row = 0; row < dtPrem.Rows.Count - 1; row++)
            {
                string line = "";

                line = array[row + 1] + ",";

                for (int coll = 0; coll < dtPrem.Columns.Count; coll++)
                {
                    line += dtPrem.Rows[row][coll].ToString() + ",";
                }

                str += line + "\r\n";
            }

            str += "\n";

            str += lineTmp.Replace("Vol", "Strike") + "\r\n";

            DataTable dtStrike = cls.GetdtStrikeGrid();

            for (int row = 0; row < dtStrike.Rows.Count - 1; row++)
            {
                string line = "";

                line = array[row + 1] + ",";

                for (int coll = 0; coll < dtStrike.Columns.Count; coll++)
                {
                    line += dtStrike.Rows[row][coll].ToString() + ",";
                }

                str += line + "\r\n";
            }

            context.Response.AddHeader("Content-Disposition", "attachment; filename=VolPremStrike.csv");
            context.Response.ContentType = "application/csv";
            context.Response.Write(str);
        }
        catch (Exception ee)
        {
            context.Response.AddHeader("Content-Disposition", "attachment; filename=VolPremStrike.csv");
            context.Response.ContentType = "application/csv";
            context.Response.Write("Error In Creating CSV File");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}