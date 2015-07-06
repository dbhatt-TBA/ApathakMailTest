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
using OpenBeast.TradeCaptureService;
using VCM.Common.Log;
using VolmaxLauncherLibrary;
using System.Collections.Generic;

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
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString());
            }
        }

        public int SubmitBeastCalcSharing(long pUserId, DataTable dtAutoUrls, DateTime pStartDate, DateTime pEndDate, string pLandingPageName, string pIpAddress, int pMinuteInterval, string pRemarks, string pInstanceId, string pInstanceInfo)
        {
            int iResult = 0;
            try
            {
                //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_AutoURL", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_AutoURL", SqlDbType.Structured);
                        sqlCmd.Parameters["@p_AutoURL"].Value = dtAutoUrls;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = pUserId;

                        sqlCmd.Parameters.Add("@p_StartDate", SqlDbType.DateTime);
                        sqlCmd.Parameters["@p_StartDate"].Value = pStartDate;

                        sqlCmd.Parameters.Add("@p_EndDate", SqlDbType.DateTime);
                        sqlCmd.Parameters["@p_EndDate"].Value = pEndDate;

                        sqlCmd.Parameters.Add("@p_MovetoPage", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_MovetoPage"].Value = pLandingPageName;

                        sqlCmd.Parameters.Add("@p_SuccessFlag", SqlDbType.TinyInt);
                        sqlCmd.Parameters["@p_SuccessFlag"].Value = 0;

                        sqlCmd.Parameters.Add("@p_IpAddress", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_IpAddress"].Value = pIpAddress;

                        sqlCmd.Parameters.Add("@p_MinuteInterval", SqlDbType.Int);
                        sqlCmd.Parameters["@p_MinuteInterval"].Value = pMinuteInterval;

                        sqlCmd.Parameters.Add("@p_Remarks", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_Remarks"].Value = pRemarks;

                        sqlCmd.Parameters.Add("@p_InstanceId", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_InstanceId"].Value = pInstanceId;

                        sqlCmd.Parameters.Add("@p_InstanceInfo", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_InstanceInfo"].Value = pInstanceInfo;

                        con.Open();
                        sqlCmd.ExecuteNonQuery();
                        iResult = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                iResult = 0;
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: SubmitBeastCalcSharing :: " + errorMessage.ToString());
                string strErrorDesc = "UserId: " + pUserId + "; " + "InstanceId: " + pInstanceId + "; " + "InstanceInfo: " + pInstanceInfo + "; " + "IpAddress: " + pIpAddress + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "SubmitBeastCalcSharing", strErrorDesc, ex);
            }

            return iResult;
        }

        public int SubmitLastCalcDetail(long pUserId, string pInstanceId, string pInstanceInfo)
        {
            int iResult = 0;

            try
            {
                //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_Appstore_User_LastInstance", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = pUserId;

                        sqlCmd.Parameters.Add("@p_InstanceId", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_InstanceId"].Value = pInstanceId;

                        sqlCmd.Parameters.Add("@p_InstanceInfo", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_InstanceInfo"].Value = pInstanceInfo;

                        con.Open();
                        sqlCmd.ExecuteNonQuery();
                        iResult = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                iResult = 0;
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: SubmitLastCalcDetail :: " + errorMessage.ToString());

                string strErrorDesc = "UserId: " + pUserId + "; " + "InstanceId: " + pInstanceId + "; " + "InstanceInfo: " + pInstanceInfo + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "SubmitLastCalcDetail", strErrorDesc, ex);
            }

            return iResult;
        }

        public DataSet GetLastCalcDetail(long pUserId)
        {
            DataSet dsResult = new DataSet();

            try
            {
                //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_Appstore_User_LastInstance", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = pUserId;

                        con.Open();
                        //strResult = sqlCmd. ().ToString();

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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: GetLastCalcDetail :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetLastCalcDetail", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }

            return dsResult;
        }

        public DataSet GetMenuCategoryDetail(int BeastCategory, int CategoryId, long pUserId)
        {
            DataSet dsResult = new DataSet();

            try
            {
                //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_ImageCategory_SDK", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;


                        sqlCmd.Parameters.Add("@p_BeastCategoryOnly", SqlDbType.TinyInt);
                        sqlCmd.Parameters["@p_BeastCategoryOnly"].Value = BeastCategory;

                        sqlCmd.Parameters.Add("@p_CategoryId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_CategoryId"].Value = CategoryId;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = pUserId;

                        sqlCmd.Parameters.Add("@p_WithAll", SqlDbType.Bit);
                        sqlCmd.Parameters["@p_WithAll"].Value = 0;

                        con.Open();
                        //strResult = sqlCmd. ().ToString();

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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: GetMenuCategoruDetail :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetMenuCategoruDetail", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }

            return dsResult;
        }

        //Added by cpkabra
        public DataSet GetUserGroups(long pUserId)
        {
            DataSet dsResult = new DataSet();
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("um_GetUserGroups", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@UserId"].Value = pUserId;
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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                //UtilityHandler.SendEmailForError("clsDAL.cs:: GetSubMenCategoryDetail :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetSubMenCategoryDetail", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }

            return dsResult;
        }
        //Ended   

        public DataSet GetSubMenCategoryDetail(int? CategoryId, long pUserId)
        {
            DataSet dsResult = new DataSet();
            try
            {
                using (con)
                {
                    //NU Changes
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_Appstore_application_sdk", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_CategoryId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_CategoryId"].Value = CategoryId;

                        //sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        //sqlCmd.Parameters["@p_UserId"].Value = pUserId;

                        con.Open();
                        //strResult = sqlCmd. ().ToString();

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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                //UtilityHandler.SendEmailForError("clsDAL.cs:: GetSubMenCategoryDetail :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetSubMenCategoryDetail", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }

            return dsResult;
        }

        public DataSet GetSubMenCategoryDetailVendorWise(int? VendorId, long pUserId)
        {
            DataSet dsResult = new DataSet();
            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_AppList_ByVendor", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_Vendorid", SqlDbType.Int);
                        sqlCmd.Parameters["@p_Vendorid"].Value = VendorId;

                        sqlCmd.Parameters.Add("@P_UserID", SqlDbType.Int);
                        sqlCmd.Parameters["@P_UserID"].Value = pUserId;

                        con.Open();
                        //strResult = sqlCmd. ().ToString();

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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: GetSubMenCategoryDetailVendorWise :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetSubMenCategoryDetailVendorWise", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }

            return dsResult;
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
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "RunQuery_Scaler", ex.Message, ex);
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
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "RunQuery_Dataset", ex.Message, ex);
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
            catch (Exception ex)
            {
                string strErrorDesc = "DropdownList: " + cmb + "; " + "FirstText: " + FirstText + "; " + "FirstValue: " + FirstValue + "; " + "DefaultValue: " + DefaultValue + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "PopulateCombo1", strErrorDesc, ex);
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
            catch (Exception ex)
            {
                string strErrorDesc = "DropdownList: " + cmb + "; " + "FirstText: " + FirstText + "; " + "FirstValue: " + FirstValue + "; " + "DefaultValue: " + DefaultValue + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "PopulateCombo2", strErrorDesc, ex);
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
            catch (Exception ex)
            {
                string strErrorDesc = "DropdownList: " + cmb + "; " + "FirstText: " + FirstText + "; " + "FirstValue: " + FirstValue + "; " + "DefaultValue: " + DefaultValue + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "PopulateCombo3", strErrorDesc, ex);
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
            catch (Exception ex)
            {
                string strErrorDesc = "PopulateList: " + cmb + "; " + "FirstText: " + FirstText + "; " + "FirstValue: " + FirstValue + "; " + "DefaultValue: " + DefaultValue + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "PopulateList1", ex.Message, ex);
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
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "PopulateList2", ex.Message, ex);
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
            catch (Exception ex)
            {
                string strErrorDesc = "PopulateList: " + cmb + "; " + "FirstText: " + FirstText + "; " + "FirstValue: " + FirstValue + "; " + "DefaultValue: " + DefaultValue + "; " + ex.Message;
                LogUtility.Error("clsDAL.cs", "PopulateCombo4", ex.Message, ex);
            }
            return ds;
        }

        public string GetUserNameFromUserId(long pUserId)
        {
            string uName = "";
            try
            {
                openf2 wsObj = new openf2();
                uName = wsObj.GetUserCustomerDetails(pUserId);
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "GetUserNameFromUserId", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }
            return uName;
        }

        public string GetEmailIDFromUserId(long pUserId)
        {
            string uEmailID = "";
            try
            {
                openf2 wsObj = new openf2();
                uEmailID = wsObj.GetEmailIDFromUserID(pUserId);
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "GetEmailIDFromUserID", ("UserId: " + pUserId + "; " + ex.Message).ToString(), ex);
            }
            return uEmailID;
        }

        public DataSet GetLastOpenAppNameDetail(string InstanceId)
        {
            DataSet dsResult = new DataSet();
            DataSet ds = new DataSet();
            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_Application_Mst", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@p_CategoryID", SqlDbType.Int);
                        sqlCmd.Parameters["@p_CategoryID"].Value = 0;
                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = 0;
                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sqlCmd))
                        {
                            da.Fill(dsResult);
                            sqlCmd.Parameters.Clear();

                            DataTable dt = ((DataSet)dsResult).Tables[0];
                            DataView dv = new DataView(dt);
                            dv.RowFilter = "AppName = '" + InstanceId + "'";
                            DataTable tb1 = dv.ToTable();
                            ds.Tables.Add(tb1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: GetLastOpenAppNameDetail :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetLastOpenAppNameDetail", ("UserId: " + 0 + "; " + ex.Message).ToString(), ex);
            }
            return ds;
        }
        public DataSet GetBeastImageSID()
        {
            DataSet dsResult = new DataSet();

            try
            {
                using (con)
                {
                    //NU Changes
                    //using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_BeastImageSID", con))
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_Appstore_application_sdk", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        //sqlCmd.Parameters.Add("@p_AppName", SqlDbType.VarChar);
                        //sqlCmd.Parameters["@p_AppName"].Value = DBNull.Value;
                        //sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        //sqlCmd.Parameters["@p_UserId"].Value = 0;


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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: GetBeastImageSID :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetBeastImageSID", (ex.Message).ToString(), ex);
            }
            return dsResult;

        }

        public ExeChunk GetLatestVersionID(String ObjectID)
        {
            ExeChunk volEntityObj = new ExeChunk();
            bool isVersionInfoAvail = false;

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_KCGExcel_LatestVersionID", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@p_ObjectID", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_ObjectID"].Value = ObjectID;
                        con.Open();

                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                byte[] singleChunk = null;

                                singleChunk = (byte[])sqlReader["Data"];

                                volEntityObj.productVersion = sqlReader["Version"].ToString();
                                volEntityObj.NoOfChunks = "0";
                                volEntityObj.chunkName = sqlReader["Name"].ToString();
                                volEntityObj.chunkData = singleChunk;
                                isVersionInfoAvail = true;
                            }
                        }
                    }
                }

                if (isVersionInfoAvail == false)
                {
                    volEntityObj.productVersion = "";
                    volEntityObj.NoOfChunks = "0";
                    volEntityObj.chunkName = "";
                    volEntityObj.chunkData = null;
                }
            }
            catch (Exception ex)
            {
                volEntityObj.productVersion = "";
                volEntityObj.NoOfChunks = "0";
                volEntityObj.chunkName = "";
                volEntityObj.chunkData = null;
                LogUtility.Error("clsDAL.cs", "GetLatestVersionID", ("UserId: " + 0 + "; " + ex.Message).ToString(), ex);
            }

            return volEntityObj;
        }

        public List<ExeChunk> GetLatestVersionSetup(String ObjectID)
        {
            List<ExeChunk> volEntityObj = new List<ExeChunk>();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());

                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_KCGExcel_LatestVersionSetup", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@p_ObjectID", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_ObjectID"].Value = ObjectID;

                        con.Open();

                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            int tot = sqlReader.RecordsAffected;

                            while (sqlReader.Read())
                            {
                                byte[] singleChunk = null;
                                singleChunk = (byte[])sqlReader["Data"];

                                volEntityObj.Add(new ExeChunk(sqlReader["Version"].ToString(), tot.ToString(), sqlReader["Name"].ToString(), singleChunk));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  VcmLogManager.Log.LogUserActivity(Convert.ToString("VolmaxLauncher"), Convert.ToString(0), "", "cUserDbHandler.cs - GetVolmaxLauncher_LatestVersionSetup()", ex.Message.ToString(), VcmLogManager.Log.LogEntryType.Error, VcmLogManager.Log.ApplicationName.VolmaxLauncher, "");
            }

            return volEntityObj;
        }

        #region Get XML from db runtime

        public DataSet GetXML(string CalcID, string LastUpdatedDate)
        {
            DataSet dsResult = new DataSet();
            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_Calculator_XML", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_Calcname", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Calcname"].Value = CalcID;
                        if (LastUpdatedDate != "")
                        {
                            sqlCmd.Parameters.Add("@p_Modified_Date", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_Modified_Date"].Value = LastUpdatedDate;
                        }
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
                string msg = "Calculator Name: " + CalcID;
                LogUtility.Error("Utilities.cs", "GetXML();", msg, ex);

            }
            return dsResult;
        }

        #endregion

        public int SubmitUserTokenDetail(int pUserId, string emailid, string AuthToken, string ExipireFlag, string Record_Last_Action)
        {
            int iResult = 0;

            try
            {
                //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_User_Token", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        if (pUserId == 0)
                        {
                            sqlCmd.Parameters["@p_UserId"].Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCmd.Parameters["@p_UserId"].Value = pUserId;
                        }

                        sqlCmd.Parameters.Add("@p_emailid", SqlDbType.VarChar);
                        if (emailid == null)
                        {
                            sqlCmd.Parameters["@p_emailid"].Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCmd.Parameters["@p_emailid"].Value = emailid;
                        }
                        sqlCmd.Parameters.Add("@p_Token", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Token"].Value = AuthToken;

                        sqlCmd.Parameters.Add("@p_ExipireFlag", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_ExipireFlag"].Value = ExipireFlag;

                        sqlCmd.Parameters.Add("@p_Record_Last_Action", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Record_Last_Action"].Value = Record_Last_Action;

                        con.Open();
                        sqlCmd.ExecuteNonQuery();
                        iResult = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                iResult = 0;
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: SubmitUserTokenSetail :: " + errorMessage.ToString());

            }

            return iResult;
        }

        public void SubmitWebActiveLogins(string pUserId, string SessionID, string ConnectionId, string ClientType, string ssid)
        {
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_submit_Web_ActiveLogins_Dtl", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = Convert.ToInt32(pUserId);

                        sqlCmd.Parameters.Add("@p_SessionID", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_SessionID"].Value = SessionID;

                        sqlCmd.Parameters.Add("@p_ConnectionId", SqlDbType.VarChar);
                        if (ConnectionId == "")
                        {
                            sqlCmd.Parameters["@p_ConnectionId"].Value = DBNull.Value;
                        }
                        else
                        {
                            sqlCmd.Parameters["@p_ConnectionId"].Value = ConnectionId;
                        }

                        sqlCmd.Parameters.Add("@p_ClientType", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_ClientType"].Value = ClientType;

                        sqlCmd.Parameters.Add("@p_ssid", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_ssid"].Value = ssid;

                        con.Open();
                        sqlCmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                LogUtility.Error("clsDAL.cs", "ProcsubmitWebActiveLogins", ex.Message, ex);
            }
        }
        public DataSet GeWeActiveLoginsDtl(string userid)
        {
            DataSet dsResult = new DataSet();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());

                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_Web_ActiveLogins_Dtl", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = Convert.ToInt32(userid);
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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                LogUtility.Error("clsDAL.cs", "GeWeActiveLoginsDtl", (ex.Message).ToString(), ex);
            }
            return dsResult;

        }
        public DataSet GetVALIDATEUSERLOGIN(string userid)
        {
            DataSet dsResult = new DataSet();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());

                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("PROC_UM_VALIDATE_USER_LOGIN", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = Convert.ToInt32(userid);


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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                LogUtility.Error("clsDAL.cs", "GetVALIDATEUSERLOGIN", (ex.Message).ToString(), ex);
            }
            return dsResult;

        }

        public DataSet SubmitValidateLogin(string userid, string SessionID)
        {
            DataSet dsResult = new DataSet();

            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());

                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("PROC_UM_submit_VALIDATE_USER_LOGIN", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = Convert.ToInt32(userid);

                        sqlCmd.Parameters.Add("@SessionID", SqlDbType.VarChar);
                        sqlCmd.Parameters["@SessionID"].Value = SessionID;


                        sqlCmd.Parameters.Add("@ssid", SqlDbType.Int);
                        sqlCmd.Parameters["@ssid"].Value = 0;

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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                LogUtility.Error("clsDAL.cs", "SubmitValidateLogin", (ex.Message).ToString(), ex);
            }
            return dsResult;

        }

        public void Set_SharedCalc_StoppedFlag(int pUserId, string pInstanceId)
        {
            try
            {
                LogUtility.Info("clsDAL", "Set_SharedCalc_StoppedFlag", "Sharing Stopped by initiator:" + pUserId + ", " + pInstanceId + "; ");
                openf2 wsObj = new openf2();
                wsObj.BeastApps_SharedAutoURL_StoppedByInitiator(pUserId, pInstanceId);
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "Set_SharedCalc_StoppedFlag", "Sharing Stopped by initiator: " + pUserId + ", " + pInstanceId + "; " + ex.Message, ex);
            }
        }

        #region getsetInstance

        public DataSet GetTokenInstanceID(int iSifId)
        {
            DataSet dsResult = new DataSet();

            try
            {
                //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_ImageKey_InstanceID", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@P_SifId", SqlDbType.Int);
                        sqlCmd.Parameters["@P_SifId"].Value = iSifId;

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
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: GetTokenInstanceID :: " + errorMessage.ToString());
                LogUtility.Error("clsDAL.cs", "GetTokenInstanceID", (ex.Message).ToString(), ex);
            }

            return dsResult;
        }

        public int SubmitTokenImageKey(string InstanceID, string UserID, int sifid)
        {
            int iResult = 0;

            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_ImageKey_InstanceID", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_InstanceID", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_InstanceID"].Value = InstanceID;

                        sqlCmd.Parameters.Add("@p_Record_create_by", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Record_create_by"].Value = UserID;

                        sqlCmd.Parameters.Add("@P_SifID", SqlDbType.Int);
                        sqlCmd.Parameters["@P_SifID"].Value = sifid;

                        con.Open();
                        sqlCmd.ExecuteNonQuery();
                        iResult = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                iResult = 0;
                string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
                UtilityHandler.SendEmailForError("clsDAL.cs:: SubmitTokenImageKey :: " + errorMessage.ToString());
            }

            return iResult;
        }

        #endregion

        public DataSet VCM_AutoURL_GeoIP_Info(string IPNumber)
        {
            DataSet myDataset = new DataSet();

            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Beast_Get_IPDetails", con))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@IP_Address", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@IP_Address"].Value = IPNumber;

                        SqlDataAdapter myDBAdapter = new SqlDataAdapter();
                        myDBAdapter.SelectCommand = sqlCmd;

                        con.Open();
                        myDBAdapter.Fill(myDataset);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "VCM_AutoURL_GeoIP_Info", ex.Message, ex);
            }
            return myDataset;
        }

        public void SendExcelDownloadNotification(string pExcelVersion, string pClientIp, string pInstallationInfoHtml)
        {
            string _from, _to, _mailSubject, _mailBody, _city = "", _org = "", _country = "";
            _from = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
            _to = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
            _mailSubject = "Excel Installed : Version " + pExcelVersion;

            try
            {
                pClientIp = pClientIp.Trim();
                bool isIp = string.IsNullOrEmpty(pClientIp) ? false : true;

                if (isIp)
                {
                    DataSet dsGeoIP = new DataSet();
                    dsGeoIP = VCM_AutoURL_GeoIP_Info(pClientIp);

                    if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
                    {
                        _city = dsGeoIP.Tables[0].Rows[0][4].ToString();
                        _org = dsGeoIP.Tables[0].Rows[0][2].ToString();
                        _country = dsGeoIP.Tables[0].Rows[0][3].ToString();
                    }
                }

                _mailBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> A user has installed Excel with below detail." +
                             "<table><tr><td>Excel Version:</td> <td>&nbsp;" + pExcelVersion + "</td></tr> " +
                             "<tr><td>Ip Address:</td> <td>&nbsp;" + (isIp ? pClientIp : "--NA--") + "</td></tr> " +
                             "<tr><td>Organization:</td> <td>&nbsp;" + (isIp ? _org : "--NA--") + "</td>" +
                             "<tr><td>City:</td> <td>&nbsp;" + (isIp ? _city : "--NA--") + "</td>" +
                             "<tr><td>Country:</td> <td>&nbsp;" + (isIp ? _country : "--NA--") + "</td>" +
                             "<tr><td>Date:</td> <td>&nbsp;" + DateTime.UtcNow.ToString() + " GMT</td>" +
                             (string.IsNullOrEmpty(pInstallationInfoHtml.Trim()) ? "" : pInstallationInfoHtml) +
                             "</table>" +
                             "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html + "</div>";

                VCM_Mail _vcmMail = new VCM_Mail();

                _vcmMail.From = _from;
                _vcmMail.To = _to;

                _vcmMail.SendAsync = false;
                _vcmMail.Subject = _mailSubject;
                _vcmMail.Body = _mailBody;
                _vcmMail.IsBodyHtml = true;
                _vcmMail.SendMail();
                _vcmMail = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "SendExcelDownloadNotification()", ex.Message, ex);
            }
        }

        /*Trumid get user id from emailid*/
        public long GetUserID(string strEmailId)
        {
            long lReturnValue = -1;
            openf2 wsObj = new openf2();
            try
            {
                lReturnValue = wsObj.GetUserID(strEmailId);
            }
            catch (Exception)
            {
                throw;
            }

            return lReturnValue;
        }

        #region LauncherAutoLogin

        public bool Launcher_SubmitAuthTicket(string pUserId, string pAuthToken, DateTime pStartDate, DateTime pEndDate)
        {
            bool bRetVal = false;
            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_Trumid_Launcher_AuthToken", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@P_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@P_UserId"].Value = pUserId;

                        sqlCmd.Parameters.Add("@P_AuthToken", SqlDbType.VarChar);
                        sqlCmd.Parameters["@P_AuthToken"].Value = pAuthToken;

                        sqlCmd.Parameters.Add("@P_ValidFrom", SqlDbType.DateTime);
                        sqlCmd.Parameters["@P_ValidFrom"].Value = pStartDate;

                        sqlCmd.Parameters.Add("@P_ValidTo", SqlDbType.DateTime);
                        sqlCmd.Parameters["@P_ValidTo"].Value = pEndDate;

                        con.Open();
                        int iExecute = sqlCmd.ExecuteNonQuery();
                        bRetVal = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "Launcher_SubmitAuthTicket", ex.Message, ex);
            }
            return bRetVal;
        }

        public string Launcher_GetAuthTicket(string pUserId, string pAuthToken)
        {
            string strRetVal = string.Empty;
            SqlDataReader sqlReader;
            try
            {
                using (con)
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_Trumid_Launcher_AuthToken", con))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@P_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@P_UserId"].Value = pUserId;

                        sqlCmd.Parameters.Add("@P_AuthToken", SqlDbType.VarChar);
                        sqlCmd.Parameters["@P_AuthToken"].Value = pAuthToken;

                        con.Open();
                        sqlReader = sqlCmd.ExecuteReader();

                        if (sqlReader.Read())
                        {
                            strRetVal = sqlReader["MsgId"] + "#" + sqlReader["Msg"]
                                        + sqlReader["UserID"] + "#" + sqlReader["AuthToken"]
                                        + sqlReader["ValidFrom"] + "#" + sqlReader["ValidTO"];
                        }
                        sqlReader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("clsDAL.cs", "Launcher_ValidateAuthTicket", ex.Message, ex);
            }
            return strRetVal;
        }

        #endregion
    }
}