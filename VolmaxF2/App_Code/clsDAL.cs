using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.Sql;
using Microsoft.ApplicationBlocks.Data;
using MySql.Data.MySqlClient;



namespace DAL 
{
    /// <summary>
    /// Summary description for clsDAL
    /// </summary>

    public class clsDAL
    {

        SqlConnection con;
        static string str = string.Empty;
        static int ctr = 0;

        public clsDAL(Boolean isTradeCapture)
        {
            if (isTradeCapture)
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString());
            }
            else
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoTestConnection"].ToString());
            }
        }

        static void conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            str += (e.Message + "<br>");
            ctr += 1;
        }
        public string info()
        {
            return str;
        }
        public int retctr()
        {
            return ctr;
        }

        public void dest()
        {
            str = string.Empty;
            ctr = 0;
        }

        public Object RunQuery_Scaler(string strquery, SqlParameter[] par, CommandType cmdTypeObj)
        {
            Object Obj;

            try
            {
                Obj = SqlHelper.ExecuteScalar(con, cmdTypeObj, strquery, par);
            }
            catch (Exception e)
            {
                return null;
            }

            return Obj;
        }

        public DataSet RunQuery_Dataset(string strquery, SqlParameter[] par, CommandType cmdTypeObj)
        {
            DataSet ds = new DataSet();

            try
            {
                ds = SqlHelper.ExecuteDataset(con, cmdTypeObj, strquery, par);
            }
            catch (Exception e)
            {
                return null;
            }

            finally
            {
                ds.Dispose();
            }

            return ds;
        }

        public DataSet PopulateCombo(DropDownList cmb, string SQL, SqlParameter[] par, CommandType cmdTypeObj, string FirstText, string FirstValue, int DefaultValue, int flag)
        {
            DataSet ds = new DataSet();
            try
            {
                cmb.Items.Clear();

                if ((FirstText.Trim().Length + FirstValue.Trim().Length) > 0)
                {

                    cmb.Items.Add(FirstText);
                    cmb.Items[cmb.Items.Count - 1].Value = FirstValue;
                }


                ds = RunQuery_Dataset(SQL, par, cmdTypeObj);


                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    cmb.Items.Add(new ListItem((ds.Tables[0].Rows[i][1]).ToString().Replace("() ", ""), ds.Tables[0].Rows[i][0].ToString()));
                    //cmb.Items(cmb.Items.Count -1).Value = ds.Tables[0].Rows[0][0];
                }
                cmb.SelectedValue = DefaultValue.ToString();

            }
            catch (Exception e)
            {

            }
            return ds;
        }

        public void PopulateCombo(DropDownList cmb, string SQL, SqlParameter[] par, CommandType cmdTypeObj, string FirstText, string FirstValue, int DefaultValue)
        {
            try
            {
                cmb.Items.Clear();

                if ((FirstText.Trim().Length + FirstValue.Trim().Length) > 0)
                {
                    cmb.Items.Add(FirstText);
                    // cmb.Items(cmb.Items.Count - 1).Value = FirstValue;
                }
                DataSet ds = new DataSet();

                ds = RunQuery_Dataset(SQL, par, cmdTypeObj);


                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    cmb.Items.Add(new ListItem((ds.Tables[0].Rows[i][1]).ToString().Replace("() ", ""), ds.Tables[0].Rows[i][0].ToString()));
                    //cmb.Items(cmb.Items.Count -1).Value = ds.Tables[0].Rows[0][0];
                }
                cmb.SelectedValue = DefaultValue.ToString();
            }
            catch (Exception e)
            {

            }
        }

        public void PopulateCombo(DropDownList cmb, DataSet ds, string FirstText, string FirstValue, int DefaultValue)
        {
            try
            {
                cmb.Items.Clear();

                if ((FirstText.Trim().Length + FirstValue.Trim().Length) > 0)
                {
                    cmb.Items.Add(FirstText);
                }

                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    cmb.Items.Add(new ListItem((ds.Tables[0].Rows[i][1]).ToString().Replace("() ", ""), ds.Tables[0].Rows[i][0].ToString()));
                    //cmb.Items(cmb.Items.Count -1).Value = ds.Tables[0].Rows[0][0];
                }
                cmb.SelectedValue = DefaultValue.ToString();
            }
            catch (Exception e)
            {

            }
        }

        public DataSet PopulateList(ListBox cmb, string SQL, SqlParameter[] par, CommandType cmdTypeObj, string FirstText, string FirstValue, int DefaultValue, int flag)
        {
            DataSet ds = new DataSet();
            try
            {
                cmb.Items.Clear();

                if ((FirstText.Trim().Length + FirstValue.Trim().Length) > 0)
                {
                    cmb.Items.Add(FirstText);
                    // cmb.Items(cmb.Items.Count - 1).Value = FirstValue;
                }


                ds = RunQuery_Dataset(SQL, par, cmdTypeObj);


                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    cmb.Items.Add(new ListItem((ds.Tables[0].Rows[i][1]).ToString().Replace("() ", ""), ds.Tables[0].Rows[i][0].ToString()));
                    //cmb.Items(cmb.Items.Count -1).Value = ds.Tables[0].Rows[0][0];
                }
                cmb.SelectedValue = DefaultValue.ToString();
            }
            catch (Exception e)
            {

            }
            return ds;
        }

        public void PopulateList(ListBox cmb, string SQL, SqlParameter[] par, CommandType cmdTypeObj, string FirstText, string FirstValue, int DefaultValue)
        {
            try
            {
                cmb.Items.Clear();

                if ((FirstText.Trim().Length + FirstValue.Trim().Length) > 0)
                {
                    cmb.Items.Add(FirstText);
                    // cmb.Items(cmb.Items.Count - 1).Value = FirstValue;
                }
                DataSet ds = new DataSet();

                ds = RunQuery_Dataset(SQL, par, cmdTypeObj);


                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    cmb.Items.Add(new ListItem((ds.Tables[0].Rows[i][1]).ToString().Replace("() ", ""), ds.Tables[0].Rows[i][0].ToString()));
                    //cmb.Items(cmb.Items.Count -1).Value = ds.Tables[0].Rows[0][0];
                }
                cmb.SelectedValue = DefaultValue.ToString();
            }
            catch (Exception e)
            {

            }
        }

        public DataSet PopulateCombo(ListBox cmb, string SQL, SqlParameter[] par, CommandType cmdTypeObj, string FirstText, string FirstValue, int DefaultValue, int flag)
        {
            DataSet ds = new DataSet();
            try
            {
                cmb.Items.Clear();

                if ((FirstText.Trim().Length + FirstValue.Trim().Length) > 0)
                {
                    cmb.Items.Add(FirstText);
                    // cmb.Items(cmb.Items.Count - 1).Value = FirstValue;
                }


                ds = RunQuery_Dataset(SQL, par, cmdTypeObj);


                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    cmb.Items.Add(new ListItem((ds.Tables[0].Rows[i][1]).ToString().Replace("() ", ""), ds.Tables[0].Rows[i][0].ToString()));
                    //cmb.Items(cmb.Items.Count -1).Value = ds.Tables[0].Rows[0][0];
                }
                cmb.SelectedValue = DefaultValue.ToString();

            }
            catch (Exception e)
            {

            }
            return ds;
        }



        public DataSet GetLogsFromMySql(DateTime FromDate, DateTime ToDate, int Count, int PageNo, string UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_WeblogWithFilter";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;

            if (UserID == "")
            {
                cmd.Parameters.Add("@_UserID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@_UserID", UserID);
            }
            cmd.Parameters["@_UserID"].Direction = ParameterDirection.Input;

            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }

        public DataSet GetUserActivityLogs(DateTime FromDate, DateTime ToDate, int Count, int PageNo, string UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_UserActivity";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@FromDate", FromDate);
            cmd.Parameters["@FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@ToDate", ToDate);
            cmd.Parameters["@ToDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@SessionID", "");
            cmd.Parameters["@SessionID"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@UserID", UserID);
            cmd.Parameters["@UserID"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@PageNo", PageNo);
            cmd.Parameters["@PageNo"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@PageSize", Count);
            cmd.Parameters["@PageSize"].Direction = ParameterDirection.Input;


            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }

        public MySqlConnectionStringBuilder MySqlConnection(MySqlConnectionStringBuilder mscsb)
        {

            mscsb.UserID = System.Configuration.ConfigurationManager.AppSettings["Webloguserid"].ToString();
            mscsb.Password = System.Configuration.ConfigurationManager.AppSettings["WeblogPassword"].ToString();
            mscsb.Database = System.Configuration.ConfigurationManager.AppSettings["WebLogDatabase"].ToString();
            mscsb.Server = System.Configuration.ConfigurationManager.AppSettings["WeblogServer"].ToString();
            mscsb.Pooling = true;
            mscsb.ConvertZeroDateTime = true;
            mscsb.MaximumPoolSize = 5000;
            mscsb.ConnectionTimeout = 300;
            return mscsb;
        }

        public DataSet GetSharedLogsFromMySql(DateTime FromDate, DateTime ToDate, string UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();
            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_SharedDetail";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;
            if (UserID == "")
            {
                cmd.Parameters.Add("@_User_ID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@_User_ID", UserID);
            }
            cmd.Parameters["@_User_ID"].Direction = ParameterDirection.Input;

            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet GetLastLogs(string UserID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();
            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = " SP_LastLogs ";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_UserID", UserID);
            cmd.Parameters["@_UserID"].Direction = ParameterDirection.Input;

            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }


        public DataSet GetActiveUserLogsFromMySql()
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();
                mscsb = MySqlConnection(mscsb);
                MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
                myConnection.Open();

                MySqlCommand cmd = myConnection.CreateCommand();

                cmd.CommandText = "SP_ActiveUsers";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();

                MySqlDataAdapter adap = new MySqlDataAdapter();
                adap.SelectCommand = cmd;
                adap.Fill(ds);

                Console.WriteLine(myConnection.ServerVersion);
                myConnection.Close();
            }

            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetUserAllActivityFromMySql(string Userid, string LastSeen, string VendorID)
        {
            DataSet ds = new DataSet();
            try
            {

                MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();
                mscsb = MySqlConnection(mscsb);
                MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
                myConnection.Open();

                MySqlCommand cmd = myConnection.CreateCommand();

                cmd.CommandText = "SP_UserLatestActivities";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@_USERID", Userid);
                cmd.Parameters["@_USERID"].Direction = ParameterDirection.Input;

                cmd.Parameters.Add("@_LASTSEEN", Convert.ToDateTime(LastSeen));
                cmd.Parameters["@_LASTSEEN"].Direction = ParameterDirection.Input;

                if (VendorID == "" || VendorID == "0")
                {
                    cmd.Parameters.Add("_VENDORID", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.Add("_VENDORID", VendorID);
                }
                cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;


                cmd.ExecuteNonQuery();

                MySqlDataAdapter adap = new MySqlDataAdapter();
                adap.SelectCommand = cmd;
                adap.Fill(ds);

                Console.WriteLine(myConnection.ServerVersion);
                myConnection.Close();
            }

            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet AppInteraction(DateTime FromDate, DateTime ToDate, string SIFid, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_AppInteraction";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;


            cmd.Parameters.Add("@_SIFID", SIFid);
            cmd.Parameters["@_SIFID"].Direction = ParameterDirection.Input;

            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", VendorID);
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;



            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet SharedAppInteraction(DateTime FromDate, DateTime ToDate, string SIFid, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "Sp_SharedAppInteraction";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;


            cmd.Parameters.Add("@_SIFID", SIFid);
            cmd.Parameters["@_SIFID"].Direction = ParameterDirection.Input;

            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", VendorID);
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;



            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }

        public DataSet UserDetails(DateTime FromDate, DateTime ToDate, string UserID, string appid, string Pageno, string Activity, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_Search_One";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd HH:mm:ss"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;
            if (UserID == "")
            {
                cmd.Parameters.Add("_USERIDS", DBNull.Value);
            }
            else
            {

                cmd.Parameters.Add("@_USERIDS", UserID);
            }
            cmd.Parameters["@_USERIDS"].Direction = ParameterDirection.Input;

            if (appid == "")
            {
                cmd.Parameters.Add("_APPS", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@_APPS", appid);
            }
            cmd.Parameters["@_APPS"].Direction = ParameterDirection.Input;

            //if (sharedappid == "")
            //{
            //    cmd.Parameters.Add("_SHAREDAPPS", DBNull.Value);
            //}
            //else
            //{
            //    cmd.Parameters.Add("@_SHAREDAPPS", sharedappid);
            //}
            //cmd.Parameters["@_SHAREDAPPS"].Direction = ParameterDirection.Input;

            if (Activity == "")
            {
                cmd.Parameters.Add("_ACTIVITIES", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@_ACTIVITIES", Activity);
            }
            cmd.Parameters["@_ACTIVITIES"].Direction = ParameterDirection.Input;


            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", VendorID);
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_PAGESIZE", "20");
            cmd.Parameters["@_PAGESIZE"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_PAGENO", Pageno);
            cmd.Parameters["@_PAGENO"].Direction = ParameterDirection.Input;


            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet GetBeastImageAppName()
        {
            DataSet dsResult = new DataSet();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString());
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_BeastImageSID", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@p_AppName", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppName"].Value = DBNull.Value;
                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = 0;


                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsResult);
                            sqlCmd.Parameters.Clear();
                        }

                    }

                }

            }

            catch (Exception ex)
            {

            }
            return dsResult;

        }
        public DataSet MostUsedUsers(DateTime FromDate, DateTime ToDate, int Count, int PageNo, string UserID, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_MostRecentUsers";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;

            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", VendorID);
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;

            //cmd.Parameters.Add("@_LIMIT", 10);
            //cmd.Parameters["@_LIMIT"].Direction = ParameterDirection.Input;


            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet GetSessionList(string UserID, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_UserSessionList";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("_USERID", UserID);
            cmd.Parameters["_USERID"].Direction = ParameterDirection.Input;

            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", VendorID);
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;

          
            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet MostUsedApps(DateTime FromDate, DateTime ToDate, int Count, int PageNo, string UserID, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_MostUsedApps";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;



            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", Convert.ToInt32(VendorID));
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_LIMIT", 10);
            cmd.Parameters["@_LIMIT"].Direction = ParameterDirection.Input;

            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet MostSharedApps(DateTime FromDate, DateTime ToDate, int Count, int PageNo, string UserID, string VendorID)
        {
            DataSet ds = new DataSet();
            MySqlConnectionStringBuilder mscsb = new MySqlConnectionStringBuilder();

            mscsb = MySqlConnection(mscsb);
            MySqlConnection myConnection = new MySqlConnection(mscsb.ConnectionString);
            myConnection.Open();

            MySqlCommand cmd = myConnection.CreateCommand();

            cmd.CommandText = "SP_MostSharedApps";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_FromDate", FromDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_FromDate"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_ToDate", ToDate.ToString("yyyy-MM-dd"));
            cmd.Parameters["@_ToDate"].Direction = ParameterDirection.Input;

            if (VendorID == "" || VendorID == "0")
            {
                cmd.Parameters.Add("_VENDORID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("_VENDORID", VendorID);
            }
            cmd.Parameters["_VENDORID"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add("@_LIMIT", 10);
            cmd.Parameters["@_LIMIT"].Direction = ParameterDirection.Input;


            cmd.ExecuteNonQuery();

            MySqlDataAdapter adap = new MySqlDataAdapter();
            adap.SelectCommand = cmd;
            adap.Fill(ds);

            Console.WriteLine(myConnection.ServerVersion);
            myConnection.Close();
            return ds;
        }
        public DataSet GetVendorID(string Userid)
        {
            DataSet dsResult = new DataSet();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_User_Rights", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = Convert.ToInt32(Userid);
                        sqlCmd.Parameters.Add("@p_AccessName", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AccessName"].Value = DBNull.Value;

                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsResult);
                            sqlCmd.Parameters.Clear();
                        }

                    }

                }

            }

            catch (Exception ex)
            {

            }
            return dsResult;

        }
        public DataSet GetuserlistbyVendorID(string Userid)
        {
            DataSet dsResult = new DataSet();

            try
            {

                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Admin_Get_All_Fix_Cust_User_List", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = Convert.ToInt32(Userid);


                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsResult);
                            sqlCmd.Parameters.Clear();
                        }

                    }

                }

            }

            catch (Exception ex)
            {

            }
            return dsResult;

        }
        public DataSet GetAppListbyVendorID(string VendorId)
        {
            DataSet dsResult = new DataSet();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString());
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_ImageList_For_Group", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@P_GroupId", SqlDbType.Int);
                        sqlCmd.Parameters["@P_GroupId"].Value = Convert.ToInt32(VendorId);


                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsResult);
                            sqlCmd.Parameters.Clear();
                        }

                    }

                }

            }

            catch (Exception ex)
            {

            }
            return dsResult;

        }
    }
}
