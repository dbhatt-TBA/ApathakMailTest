using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Text;
using System.Web.Script.Services;
using System.Xml;


/// <summary>
/// Summary description for Service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{

    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getSysLogsForUserBySession(int SessionID, int UserID, int Count)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@SessionID", SessionID);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@Count", Count);


            tempDS = dbObjAutoTest.RunQuery_Dataset("Proc_get_syslog_for_user_on_sessionid", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    public DataTable bindSysLogGrid(DataTable dt)
    {
        //string contents;
        string[] arInfo;
        string line;
        DataTable table = new DataTable();
        table = dt.Clone();
        // Create new DataTable.
        DataRow row;
        bool flag = false;
        openf2 ws = new openf2();
        try
        {


            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["LogDetails"]) != "")
                {
                    line = Convert.ToString(dr["LogDetails"]);
                    string[] textdelimiter = { "$$" };
                    // if (contents.Contains("$$"))
                    if (line.Contains("$$"))
                    {
                        //arInfo = contents.Split(textdelimiter, System.StringSplitOptions.RemoveEmptyEntries);
                        arInfo = line.Split(textdelimiter, System.StringSplitOptions.RemoveEmptyEntries);
                        row = dr;
                        flag = false;
                        for (int i = 0; i < arInfo.Length; i++)
                        {
                            //if (i < arInfo.Length)
                            //{
                            //if ((arInfo[i].Contains("NT AUTHORITY")) || (arInfo[i].Contains("weblog")))
                            //{
                            //    //row["System Description"] = string.IsNullOrEmpty(arInfo[i].ToString()) ? " " : arInfo[i].ToString();
                            //    row["Others"] = string.IsNullOrEmpty(arInfo[i].ToString()) ? " " : arInfo[i].ToString();
                            //    flag = true;
                            //}
                            if (arInfo[i].Contains("UserID"))
                            {
                                row["UserId"] = string.IsNullOrEmpty(arInfo[1].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[1].Split(':')[1];
                                flag = true;
                            }
                            if (arInfo[i].Contains("UserName"))
                            {
                                row["Username"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                                flag = true;
                            }
                            if (arInfo[i].Contains("Token"))  //('Token')==1))
                            {
                                if (arInfo[i].IndexOf("Token") == 0)
                                    row["Token"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                                flag = true;
                            }
                            if (arInfo[i].Contains("ClientType"))
                            {
                                if (arInfo[i].Split(':')[1] == "Web" || arInfo[i].Split(':')[1] == "Excel" || arInfo[i].Split(':')[1] == "mobile")
                                {
                                    row["ClientType"] = arInfo[i].Split(':')[1];
                                }
                                flag = true;
                            }
                            if (arInfo[i].Contains("Log-InTime"))
                            {
                                //row["LogInTime"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                                row["LogInTime"] = arInfo[i].Split(new char[] { ':' }, 2)[1].Split('^')[0];// string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1].Split('^')[0]) ? (arInfo[i].Split(new char[] { ':' }, 2)[0]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                flag = true;
                                if (Convert.ToString(row["LogInTime"]) != "")
                                {
                                    row["LogInTime"] = Convert.ToDateTime(row["LogInTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");

                                }
                            }
                            if (arInfo[i].Contains("LoginValidity"))
                            {
                                if (arInfo[i].Contains('^'))
                                {
                                    row["LastAuthTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["LastAuthTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[0]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["LastAuthTime"]) != "")
                                {
                                    row["LastAuthTime"] = Convert.ToDateTime(row["LastAuthTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                                flag = true;
                            }
                            if (arInfo[i].Contains("Log-OutTime"))
                            {
                                if (arInfo[i].Contains('^'))
                                {
                                    row["LogOutTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["LogOutTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["LogOutTime"]) != "")
                                {
                                    row["LogOutTime"] = Convert.ToDateTime(row["LogOutTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                                flag = true;
                            }
                            //if (arInfo[i].Contains("UserLastActivity"))
                            //{
                            //    if (arInfo[i].Contains('^'))
                            //    {

                            //        row["UserLastActivity"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                            //    }
                            //    else
                            //    {
                            //        row["UserLastActivity"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                            //    }
                            //    if (Convert.ToString(row["UserLastActivity"]) != "")
                            //    {
                            //        row["UserLastActivity"] = Convert.ToDateTime(row["UserLastActivity"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                            //    }

                            //}
                            if (arInfo[i].Contains("IPAddress"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    //  row["IPAddress"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["IPAddress"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);

                                    string Ipaddress = Convert.ToString(row["IPAddress"]);
                                    DataSet dsGeoIP = new DataSet();
                                    dsGeoIP = ws.GetAutoURLGeoIPInfo(Ipaddress);
                                    string city = "";
                                    string org = "";
                                    string Country = "";

                                    if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
                                    {
                                        city = dsGeoIP.Tables[0].Rows[0][4].ToString();
                                        org = dsGeoIP.Tables[0].Rows[0][2].ToString();
                                        Country = dsGeoIP.Tables[0].Rows[0][3].ToString();

                                        row["City"] = dsGeoIP.Tables[0].Rows[0][4].ToString();
                                        row["Org"] = dsGeoIP.Tables[0].Rows[0][2].ToString();
                                        row["Country"] = dsGeoIP.Tables[0].Rows[0][3].ToString();

                                    }

                                }
                                flag = true;
                            }

                            if (arInfo[i].Contains("Org"))
                            {
                                flag = true;
                                row["Org"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("City"))
                            {
                                flag = true;
                                row["City"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("Country"))
                            {
                                flag = true;
                                row["Country"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("ConnectionID"))
                            {
                                row["ConnectionId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("UserAgent"))
                            {
                                row["UserAgent"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("ImageName"))
                            {
                                row["ImageName"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("ImageSIFID"))
                            {
                                row["ImageSIFId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("ImageValidity"))
                            {
                                row["ImageValidity"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                                if (Convert.ToString(row["ImageValidity"]) != "")
                                {
                                    row["ImageValidity"] = Convert.ToDateTime(row["ImageValidity"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }
                            if (arInfo[i].Contains("ImageCreationTime"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    row["ImageCreatedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["ImageCreatedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);

                                }
                                if (Convert.ToString(row["ImageCreatedTime"]) != "")
                                {
                                    row["ImageCreatedTime"] = Convert.ToDateTime(row["ImageCreatedTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }


                            }
                            if (arInfo[i].Contains("ImageCloseTime"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    row["ImageClosedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["ImageClosedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["ImageClosedTime"]) != "")
                                {
                                    row["ImageClosedTime"] = Convert.ToDateTime(row["ImageClosedTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");

                                }
                            }
                            if (arInfo[i].Contains("LastActivityOn"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    row["LastActivityOn"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["LastActivityOn"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["LastActivityOn"]) != "")
                                {
                                    row["LastActivityOn"] = Convert.ToDateTime(row["LastActivityOn"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }

                            if (arInfo[i].Contains("senderDet"))
                            {
                                row["SenderDetails"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("parentId"))
                            {
                                row["ParentId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("Value"))
                            {
                                row["Value"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("SendValueTime"))
                            {
                                // row["SendValueTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1];
                                if (arInfo[i].Contains('^'))
                                {

                                    row["SendValueTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["SendValueTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["SendValueTime"]) != "")
                                {
                                    row["SendValueTime"] = Convert.ToDateTime(row["SendValueTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }


                        }
                        //if (flag == true)
                        //{
                        row["LogDate"] = Convert.ToDateTime(row["DateTimeLocal"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");

                        table.ImportRow(row);
                        //}
                    }
                }


            }
            return table;

        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    public DataTable bindSysLogGridForImageDetail(DataTable dt)
    {
        //string contents;
        string[] arInfo;
        string line;
        DataTable table = new DataTable();
        table = dt.Clone();
        // Create new DataTable.
        DataRow row;

        try
        {


            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr[2]) != "")
                {
                    line = Convert.ToString(dr[2]);
                    string[] textdelimiter = { "$$" };
                    // if (contents.Contains("$$"))
                    if (line.Contains("$$"))
                    {
                        //arInfo = contents.Split(textdelimiter, System.StringSplitOptions.RemoveEmptyEntries);
                        arInfo = line.Split(textdelimiter, System.StringSplitOptions.RemoveEmptyEntries);
                        row = dr;
                        for (int i = 0; i < arInfo.Length; i++)
                        {
                            //if (i < arInfo.Length)
                            //{
                            if ((arInfo[i].Contains("NT AUTHORITY")) || (arInfo[i].Contains("weblog")))
                            {
                                //row["System Description"] = string.IsNullOrEmpty(arInfo[i].ToString()) ? " " : arInfo[i].ToString();
                                row["Others"] = string.IsNullOrEmpty(arInfo[i].ToString()) ? " " : arInfo[i].ToString();
                            }
                            if (arInfo[i].Contains("UserID"))
                            {
                                row["UserId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("UserName"))
                            {
                                row["Username"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("Token"))  //('Token')==1))
                            {
                                if (arInfo[i].IndexOf("Token") == 0)
                                    row["Token"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("ClientType"))
                            {
                                row["ClientType"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("Log-InTime"))
                            {
                                //row["LogInTime"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? arInfo[i].Split(':')[0] : arInfo[i].Split(':')[1];
                                row["LogInTime"] = arInfo[i].Split(new char[] { ':' }, 2)[1].Split('^')[0];// string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1].Split('^')[0]) ? (arInfo[i].Split(new char[] { ':' }, 2)[0]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                if (Convert.ToString(row["LogInTime"]) != "")
                                {
                                    row["LogInTime"] = Convert.ToDateTime(row["LogInTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }
                            if (arInfo[i].Contains("LoginValidity"))
                            {
                                if (arInfo[i].Contains('^'))
                                {
                                    row["LastAuthTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["LastAuthTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[0]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["LastAuthTime"]) != "")
                                {
                                    row["LastAuthTime"] = Convert.ToDateTime(row["LastAuthTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }
                            if (arInfo[i].Contains("Log-OutTime"))
                            {
                                if (arInfo[i].Contains('^'))
                                {
                                    row["Log-Out/ExpiredTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["Log-Out/ExpiredTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["Log-Out/ExpiredTime"]) != "")
                                {
                                    row["Log-Out/ExpiredTime"] = Convert.ToDateTime(row["Log-Out/ExpiredTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }

                            }
                            if (arInfo[i].Contains("UserLastActivity"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    row["UserLastActivity"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["UserLastActivity"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["UserLastActivity"]) != "")
                                {
                                    row["UserLastActivity"] = Convert.ToDateTime(row["UserLastActivity"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }

                            }
                            if (arInfo[i].Contains("IPAddress"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    //  row["IPAddress"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["IPAddress"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                            }

                            if (arInfo[i].Contains("Org"))
                            {
                                row["Org"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("City"))
                            {
                                row["City"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("Country"))
                            {
                                row["Country"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("ConnectionID"))
                            {
                                row["ConnectionId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("UserAgent"))
                            {
                                row["UserAgent"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }

                            if (arInfo[i].Contains("ImageName"))
                            {
                                row["ImageName"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("ImageSIFID"))
                            {
                                row["ImageSIFId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("ImageValidity"))
                            {
                                row["ImageValidity"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                                if (Convert.ToString(row["ImageValidity"]) != "")
                                {
                                    row["ImageValidity"] = Convert.ToDateTime(row["ImageValidity"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }
                            if (arInfo[i].Contains("ImageCreationTime"))
                            {
                                // row["ImageCreatedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0]) : (arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0]);
                                if (arInfo[i].Contains('^'))
                                {

                                    row["ImageCreatedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["ImageCreatedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);

                                }
                                if (Convert.ToString(row["ImageCreatedTime"]) != "")
                                {
                                    row["ImageCreatedTime"] = Convert.ToDateTime(row["ImageCreatedTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }


                            }
                            if (arInfo[i].Contains("ImageCloseTime"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    row["ImageClosedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["ImageClosedTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["ImageClosedTime"]) != "")
                                {
                                    row["ImageClosedTime"] = Convert.ToDateTime(row["ImageClosedTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");

                                }
                            }
                            if (arInfo[i].Contains("LastActivityOn"))
                            {
                                if (arInfo[i].Contains('^'))
                                {

                                    row["LastActivityOn"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["LastActivityOn"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["LastActivityOn"]) != "")
                                {
                                    row["LastActivityOn"] = Convert.ToDateTime(row["LastActivityOn"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }

                            if (arInfo[i].Contains("senderDet"))
                            {
                                row["SenderDetails"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("parentId"))
                            {
                                row["ParentId"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("Value"))
                            {
                                row["Value"] = string.IsNullOrEmpty(arInfo[i].Split(':')[1]) ? "" : arInfo[i].Split(':')[1];
                            }
                            if (arInfo[i].Contains("SendValueTime"))
                            {
                                // row["SendValueTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1];
                                if (arInfo[i].Contains('^'))
                                {

                                    row["SendValueTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0] : arInfo[i].Split(new char[] { ':' }, 2)[1].Split(new char[] { '^' }, 2)[0];
                                }
                                else
                                {
                                    row["SendValueTime"] = string.IsNullOrEmpty(arInfo[i].Split(new char[] { ':' }, 2)[1]) ? (arInfo[i].Split(new char[] { ':' }, 2)[1]) : (arInfo[i].Split(new char[] { ':' }, 2)[1]);
                                }
                                if (Convert.ToString(row["SendValueTime"]) != "")
                                {
                                    row["SendValueTime"] = Convert.ToDateTime(row["SendValueTime"]).ToString("dd/MMM/yyyy :hh:mm:ss tt");
                                }
                            }


                        }
                        //DataRow drNewrow;
                        //drNewrow = row;
                        table.ImportRow(row);
                    }
                }


            }
            return table;

        }
        catch (Exception ex)
        {
            return dt;
        }
    }
    private DataTable CreateDataTable(DataTable table)
    {
        try
        {

            // Declare DataColumn and DataRow variables.
            DataColumn column;

            // Create new DataColumn, set DataType, ColumnName
            // and add to DataTable.  

            //column = new DataColumn();
            //column.DataType = System.Type.GetType("System.String");
            //column.ColumnName = "System Description";
            //table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "LogDate";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserId";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "UserName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Token";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ClientType";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "LastAuthTime";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "LogInTime";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "LogOutTime";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserLastActivity";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "IPAddress";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Org";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "City";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Country";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ConnectionId";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "UserAgent";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ImageName";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ImageSIFId";
            table.Columns.Add(column);

            // Create second column.
            column = new DataColumn();
            column.DataType = Type.GetType("System.String");
            column.ColumnName = "ImageValidity";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ImageCreatedTime";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ImageClosedTime";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "LastActivityOn";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "SenderDetails";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ParentId";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Value";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "SendValueTime";
            table.Columns.Add(column);


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Others";
            table.Columns.Add(column);

            return table;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string MostUsedUsers(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();


            tempDS = dbObjAutoTest.MostUsedUsers(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID, VendorID); /* MySQL*/



            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetSessionList(string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();


            tempDS = dbObjAutoTest.GetSessionList(UserID, VendorID); /* MySQL*/



            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string MostUsedApps(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            DataView Dv = new DataView();
            DataSet AppName = new DataSet();
            tempDS = dbObjAutoTest.MostUsedApps(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID, VendorID); /* MySQL*/
            AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]).Trim() != "")
                {
                    Dv.RowFilter = "AppName ='" + Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]) + "'";
                    tempDS.Tables[0].Rows[index]["AppName"] = Dv.ToTable().Rows[0]["AppTitle"];

                }
            }

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string MostUsedUserChart(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            //  DataView Dv = new DataView();
            //   DataSet AppName = new DataSet();
            tempDS = dbObjAutoTest.MostUsedUsers(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID, VendorID); /* MySQL*/
            tempDS.Tables[0].Columns.Add("fillColor");
            tempDS.Tables[0].Columns.Add("strokeColor");
            tempDS.Tables[0].Columns.Add("highlightFill");
            tempDS.Tables[0].Columns.Add("highlightStroke");
            tempDS.Tables[0].Columns.Add("data", typeof(Int32));


            //  AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            //  Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (index % 2 == 0)
                {
                    tempDS.Tables[0].Rows[index]["data"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["fillColor"] = "rgba(220,220,220,0.5)";
                    tempDS.Tables[0].Rows[index]["strokeColor"] = "rgba(220,220,220,0.8)";
                    tempDS.Tables[0].Rows[index]["highlightFill"] = "rgba(220,220,220,0.75)";
                    tempDS.Tables[0].Rows[index]["highlightStroke"] = "rgba(220,220,220,1)";
                }
                else
                {
                    tempDS.Tables[0].Rows[index]["data"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["fillColor"] = "rgba(151,187,205,0.5)";
                    tempDS.Tables[0].Rows[index]["strokeColor"] = "rgba(151,187,205,0.8)";
                    tempDS.Tables[0].Rows[index]["highlightFill"] = "rgba(151,187,205,0.75)";
                    tempDS.Tables[0].Rows[index]["highlightStroke"] = "rgba(151,187,205,1)";
                }

            }
            tempDS.Tables[0].TableName = "datasets";
            return GetJSONString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string MostSharedAppsChart(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            DataView Dv = new DataView();
            DataSet AppName = new DataSet();
            tempDS = dbObjAutoTest.MostSharedApps(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID, VendorID); /* MySQL*/
            tempDS.Tables[0].Columns.Add("value", typeof(Int32));
            tempDS.Tables[0].Columns.Add("color");

            tempDS.Tables[0].Columns.Add("highlight");
            tempDS.Tables[0].Columns.Add("label");


            AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]).Trim() != "")
                {
                    Dv.RowFilter = "AppName ='" + Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]) + "'";
                    tempDS.Tables[0].Rows[index]["AppName"] = Dv.ToTable().Rows[0]["AppTitle"];

                }
                if (index == 0)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#F7464A";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#FF5A5E";


                }
                if (index == 1)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#5AFF5F";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#5AFF5F";


                }
                if (index == 2)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#FFFD5A";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#FFFD5A";


                }
                if (index == 3)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#5A75FF";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#5A75FF";


                }
                if (index == 4)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#FF5AFD";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#FF5AFD";


                }
                if (index == 5)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#75E1F5";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#75E1F5";


                }
                if (index == 6)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#EF8615";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#EF8615";


                }
                if (index == 7)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#6F4E37";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#6F4E37";


                }
                if (index == 8)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#842DCE";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#842DCE";


                }
                if (index == 9)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#736F6E";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#736F6E";


                }


            }
            tempDS.Tables[0].Columns.Remove("ImageSIFID");
            tempDS.Tables[0].Columns.Remove("Seen");
            tempDS.Tables[0].Columns.Remove("LastSeen");
            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string MostUsedAppsChart(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            DataView Dv = new DataView();
            DataSet AppName = new DataSet();
            tempDS = dbObjAutoTest.MostUsedApps(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID, VendorID); /* MySQL*/
            tempDS.Tables[0].Columns.Add("value", typeof(Int32));
            tempDS.Tables[0].Columns.Add("color");

            tempDS.Tables[0].Columns.Add("highlight");
            tempDS.Tables[0].Columns.Add("label");


            AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]).Trim() != "")
                {
                    Dv.RowFilter = "AppName ='" + Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]) + "'";
                    tempDS.Tables[0].Rows[index]["AppName"] = Dv.ToTable().Rows[0]["AppTitle"];

                }
                if (index == 0)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#F7464A";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#FF5A5E";


                }
                if (index == 1)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#5AFF5F";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#5AFF5F";


                }
                if (index == 2)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#FFFD5A";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#FFFD5A";


                }
                if (index == 3)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#5A75FF";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#5A75FF";


                }
                if (index == 4)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#FF5AFD";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#FF5AFD";


                }
                if (index == 5)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#75E1F5";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#75E1F5";


                }
                if (index == 6)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#EF8615";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#EF8615";


                }
                if (index == 7)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#6F4E37";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#6F4E37";


                }
                if (index == 8)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#842DCE";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#842DCE";


                }
                if (index == 9)
                {
                    tempDS.Tables[0].Rows[index]["value"] = Convert.ToInt32(tempDS.Tables[0].Rows[index]["Seen"]);
                    tempDS.Tables[0].Rows[index]["label"] = Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]);
                    tempDS.Tables[0].Rows[index]["color"] = "#736F6E";
                    tempDS.Tables[0].Rows[index]["highlight"] = "#736F6E";


                }


            }
            tempDS.Tables[0].Columns.Remove("ImageSIFID");
            tempDS.Tables[0].Columns.Remove("Seen");
            tempDS.Tables[0].Columns.Remove("LastSeen");
            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string MostSharedApps(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            DataView Dv = new DataView();
            DataSet AppName = new DataSet();
            tempDS = dbObjAutoTest.MostSharedApps(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID, VendorID); /* MySQL*/
            AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]).Trim() != "")
                {
                    Dv.RowFilter = "AppName ='" + Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]) + "'";
                    tempDS.Tables[0].Rows[index]["AppName"] = Dv.ToTable().Rows[0]["AppTitle"];

                }
            }

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string AppInteraction(string FromDate, string ToDate, string SIFid, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();


            tempDS = dbObjAutoTest.AppInteraction(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), SIFid, VendorID); /* MySQL*/

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string SharedAppInteraction(string FromDate, string ToDate, string SIFid, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();


            tempDS = dbObjAutoTest.SharedAppInteraction(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), SIFid, VendorID); /* MySQL*/

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetuserlistbyVendorID(string UserId)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(true);

            DataSet tempDS = new DataSet();
            DataView Dv = new DataView();

            tempDS = dbObjAutoTest.GetuserlistbyVendorID(UserId); /* MySQL*/
            // Dv = new DataView(tempDS.Tables[0]);

            //  Dv.RowFilter = "UserName <> '' ";
            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetAppListbyVendorID(string VendorId)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();


            tempDS = dbObjAutoTest.GetAppListbyVendorID(VendorId); /* MySQL*/

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getSharedSysLogsByDateTime(string FromDate, string ToDate, int Count, string UserID, string VendorID)
    {
        try
        {
            string result = "";

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();

            if (UserID != "")
            {
                long lUserid = VcmUserNamespace.cUserDbHandler.CheckUserStatus(UserID, "");
                if (lUserid > 0)
                {
                    UserID = Convert.ToString(lUserid);
                }
                else
                {
                    UserID = "0";
                }
            }
            else
            {
                UserID = "";
            }


            // tempDS = dbObjAutoTest.GetSharedLogsFromMySql(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), UserID, VendorID); /* MySQL*/

            result = ConvertDataTabletoString(tempDS.Tables[0]);
            return result;

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetUserActivity(string FromDate, string ToDate, int Count, string UserID)
    {

        try
        {
            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();

            tempDS = dbObjAutoTest.GetUserActivityLogs(Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate), Count, 1, UserID); /* MySQL*/


            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetLastLogsOf(string UserID)
    {

        try
        {
            clsDAL dbObjAutoTest = new clsDAL(false);
            openf2 ws = new openf2();
            DataSet tempDS = new DataSet();

            tempDS = dbObjAutoTest.GetLastLogs(UserID); /* MySQL*/
            //tempDS.Tables[0].Columns.Add("City");
            //tempDS.Tables[0].Columns.Add("Org");
            //tempDS.Tables[0].Columns.Add("Country");

            //for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            //{
            //    if (tempDS.Tables[0].Rows[index]["IP"] != "")
            //    {
            //        DataSet dsGeoIP = new DataSet();
            //        dsGeoIP = ws.GetAutoURLGeoIPInfo(Convert.ToString(tempDS.Tables[0].Rows[index]["IP"]));

            //        if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
            //        {
            //            tempDS.Tables[0].Rows[index]["City"] = dsGeoIP.Tables[0].Rows[0][4].ToString();
            //            tempDS.Tables[0].Rows[index]["Org"] = dsGeoIP.Tables[0].Rows[0][2].ToString();
            //            tempDS.Tables[0].Rows[index]["Country"] = dsGeoIP.Tables[0].Rows[0][3].ToString();

            //        }
            //    }
            //}

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getActiveUserSysLogsByDateTime(string VendorID)
    {
        try
        {
            string result = "";
            openf2 ws = new openf2();
            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();

            //tempDS = dbObjAutoTest.GetActiveUserLogsFromMySql(VendorID); /* MySQL*/
            //tempDS.Tables[0].Columns.Add("City");
            //tempDS.Tables[0].Columns.Add("Org");
            //tempDS.Tables[0].Columns.Add("Country");
            //for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            //{
            //    if (tempDS.Tables[0].Rows[index]["IP"] != "")
            //    {
            //        DataSet dsGeoIP = new DataSet();
            //        dsGeoIP = ws.GetAutoURLGeoIPInfo(Convert.ToString(tempDS.Tables[0].Rows[index]["IP"]));

            //        if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
            //        {
            //            tempDS.Tables[0].Rows[index]["City"] = dsGeoIP.Tables[0].Rows[0][4].ToString();
            //            tempDS.Tables[0].Rows[index]["Org"] = dsGeoIP.Tables[0].Rows[0][2].ToString();
            //            tempDS.Tables[0].Rows[index]["Country"] = dsGeoIP.Tables[0].Rows[0][3].ToString();

            //        }
            //    }
            //}

            result = ConvertDataTabletoString(tempDS.Tables[0]);
            return result;

        }
        catch (Exception e)
        {
            return null;
        }
    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getUserAllActivity(string Userid, string LastSeen, string VendorID)
    {
        try
        {
            string result = "";

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            DataSet AppName = new DataSet();
            DataView Dv = new DataView();

            LastSeen = LastSeen.Replace("|", " ");
            tempDS = dbObjAutoTest.GetUserAllActivityFromMySql(Userid, LastSeen, VendorID); /* MySQL*/
            AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]).Trim() != "")
                {
                    Dv.RowFilter = "AppName ='" + Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]) + "'";

                    tempDS.Tables[0].Rows[index]["AppName"] = Dv.ToTable().Rows[0]["AppTitle"];

                }
            }

            result = ConvertDataTabletoString(tempDS.Tables[0]);
            return result;

        }
        catch (Exception e)
        {
            return null;
        }
    }
    public string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);


    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getW3CLogsByDateTime(DateTime FromDate, DateTime ToDate, int Count)
    {

        try
        {
            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@FromDate", FromDate);
            param[1] = new SqlParameter("@ToDate", ToDate);
            param[2] = new SqlParameter("@Count", Count);


            tempDS = dbObjAutoTest.RunQuery_Dataset("Proc_Get_W3CLogs_On_Date", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getW3CLogsForUserBySession(int SessionID, int UserID, int Count)
    {

        try
        {
            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@SessionID", SessionID);
            param[1] = new SqlParameter("@UserID", UserID);
            param[2] = new SqlParameter("@Count", Count);

            tempDS = dbObjAutoTest.RunQuery_Dataset("Proc_Get_w3clogs_for_user_on_sessionid", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getSessionAverageFromDateToDate(string FromDate, string ToDate, int IsINDReq)
    {

        try
        {
            clsDAL dbObj = new clsDAL(true);
            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@FromDate", FromDate);
            param[1] = new SqlParameter("@ToDate", ToDate);
            param[2] = new SqlParameter("@IsINDReq", IsINDReq);

            tempDS = dbObj.RunQuery_Dataset("QOS_GetSessionAverageFromDateToDate", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getSessionFromDateToDate(string FromDate, string ToDate)
    {

        try
        {
            clsDAL dbObj = new clsDAL(true);
            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@FromDate", FromDate);
            param[1] = new SqlParameter("@ToDate", ToDate);

            tempDS = dbObj.RunQuery_Dataset("QOS_GetSessionFromDateToDate", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getResponseAverageForUserFromDateToDate(string FromDate, string ToDate, string UserID)
    {

        try
        {
            clsDAL dbObj = new clsDAL(true);
            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@FromDate", FromDate);
            param[1] = new SqlParameter("@ToDate", ToDate);
            param[2] = new SqlParameter("@UserID", UserID);

            tempDS = dbObj.RunQuery_Dataset("QOS_GetResponseAverageForUserFromDateToDate", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string getSessionAverageForUserFromDateToDate(string FromDate, string ToDate, string UserID)
    {

        try
        {
            clsDAL dbObj = new clsDAL(true);
            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@FromDate", FromDate);
            param[1] = new SqlParameter("@ToDate", ToDate);
            param[2] = new SqlParameter("@UserID", UserID);

            tempDS = dbObj.RunQuery_Dataset("QOS_GetSessionAverageForUserFromDateToDate", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetSessionDetailsBySessionID(int SessionID, int IsINDReq)
    {
        try
        {
            clsDAL dbObj = new clsDAL(true);
            DataSet tempDS = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SessionID", SessionID);
            param[1] = new SqlParameter("@IsINDReq", IsINDReq);

            tempDS = dbObj.RunQuery_Dataset("QOS_GetSessionDetailsBySessionID", param, CommandType.StoredProcedure);

            return GetJSONString(tempDS.Tables[0]);
        }
        catch (Exception ee)
        {
            return null;
        }
    }

    public static string GetJSONString(DataTable Dt)
    {

        string[] StrDc = new string[Dt.Columns.Count];

        string HeadStr = string.Empty;
        for (int i = 0; i < Dt.Columns.Count; i++)
        {

            StrDc[i] = Dt.Columns[i].Caption;
            HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";

        }

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);
        StringBuilder Sb = new StringBuilder();

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
        return Sb.ToString();

    }
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string UserDetails(string FromDate, string ToDate, string UserID, string appid, string pageno, string Activity, string VendorID)
    {

        try
        {

            clsDAL dbObjAutoTest = new clsDAL(false);

            DataSet tempDS = new DataSet();
            DataView Dv = new DataView();
            DataSet AppName = new DataSet();
            tempDS = dbObjAutoTest.UserDetails(Convert.ToDateTime(Convert.ToDateTime(FromDate)),Convert.ToDateTime(ToDate), UserID, appid, pageno, Activity, VendorID); /* MySQL*/
            AppName = dbObjAutoTest.GetBeastImageAppName(); /* SQL*/
            Dv = new DataView(AppName.Tables[0]);

            for (int index = 0; index < tempDS.Tables[0].Rows.Count; index++)
            {
                if (Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]).Trim() != "")
                {
                    Dv.RowFilter = "AppName ='" + Convert.ToString(tempDS.Tables[0].Rows[index]["AppName"]) + "'";
                    tempDS.Tables[0].Rows[index]["AppName"] = Dv.ToTable().Rows[0]["AppTitle"];

                }
            }

            return ConvertDataTabletoString(tempDS.Tables[0]);

        }
        catch (Exception e)
        {
            return null;
        }

    }


}

