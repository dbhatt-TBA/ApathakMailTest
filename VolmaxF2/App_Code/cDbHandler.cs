using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Xml;
using VcmUserNamespace;
using VCM.Common.Log;
using VCM.Common;

/// <summary>
/// Summary description for volDbHandler
/// </summary>
/// 
namespace vcmProductNamespace
{
    public class cDbHandler : System.Web.UI.Page
    {

        static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString();
        static string connectionString_AppStore = System.Configuration.ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString();

        public cDbHandler()
        {

            //
            // TODO: Add constructor logic here
            //
        }

        #region Session Information & Menu Information

        #endregion

        #region Writing logs for the beast client plugin
        public static void SubmitUserLoginNotification_BeastPlugin(string UserId, string UserName, string sessionid, string EventId, string PageName, string IPAddress, String eventDesc, string customerid)
        {
            //SessionInfo _session = new SessionInfo(HttpContext.Current.Session);

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("PROC_SUBMIT_UserLoginNotification_BeastPlugin", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@p_UserID", SqlDbType.Int);
                        cmd.Parameters["@p_UserID"].Value = UserId;

                        cmd.Parameters.Add("@p_UserName", SqlDbType.NVarChar);
                        cmd.Parameters["@p_UserName"].Value = UserName;

                        cmd.Parameters.Add("@p_SessionID", SqlDbType.NVarChar);
                        cmd.Parameters["@p_SessionID"].Value = sessionid;

                        cmd.Parameters.Add("@p_EventID", SqlDbType.NVarChar);
                        cmd.Parameters["@p_EventID"].Value = EventId;

                        cmd.Parameters.Add("@p_PageName", SqlDbType.NVarChar);
                        cmd.Parameters["@p_PageName"].Value = PageName;

                        cmd.Parameters.Add("@p_IpAddress", SqlDbType.NVarChar);
                        cmd.Parameters["@p_IpAddress"].Value = IPAddress;

                        cmd.Parameters.Add("@p_ForApplication", SqlDbType.VarChar);
                        cmd.Parameters["@p_ForApplication"].Value = "SWAP";

                        cmd.Parameters.Add("@p_EventDescriptions", SqlDbType.VarChar);
                        cmd.Parameters["@p_EventDescriptions"].Value = eventDesc;

                        cmd.Parameters.Add("@P_CustomerId", SqlDbType.VarChar);
                        cmd.Parameters["@P_CustomerId"].Value = customerid;

                        cn.Open();
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "SubmitUserLoginNotification_BeastPlugin", ex.Message, ex);
            }
        }

        #endregion

        #region Auto URL
        public static DataTable FillCustomerList(string userID, string SessionId)
        {
            DataTable myDataTable = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_Admin_Get_ISwap_All_Customer_List", cn)) //Proc_Admin_Get_All_Customer_List
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        cmd.Parameters["@p_UserId"].Value = userID;

                        cmd.Parameters.Add("@p_SessionId", SqlDbType.Int);
                        cmd.Parameters["@p_SessionId"].Value = SessionId;

                        cn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            myDataTable.Load(rdr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "FillCustomerList", ex.Message, ex);
            }
            return myDataTable;
        }

        public static bool SubmitAutoUrl(string UserID, string URLEncryptedMsg, string URLEncrypted, DateTime ValidDate, string PageName, string sessionid)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_ISwap_AutoURL_SentMail", cn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_UserID", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserID"].Value = UserID;

                        sqlCmd.Parameters.Add("@p_SessionID", SqlDbType.Int);
                        sqlCmd.Parameters["@p_SessionID"].Value = sessionid;

                        sqlCmd.Parameters.Add("@p_MovetoPage", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_MovetoPage"].Value = PageName;

                        sqlCmd.Parameters.Add("@p_ValidDate", SqlDbType.DateTime);
                        sqlCmd.Parameters["@p_ValidDate"].Value = ValidDate;

                        sqlCmd.Parameters.Add("@p_URLEncrypted", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_URLEncrypted"].Value = URLEncrypted;

                        sqlCmd.Parameters.Add("@p_URLEncryptedMsg", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_URLEncryptedMsg"].Value = URLEncryptedMsg;

                        cn.Open();
                        sqlCmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "SubmitAutoUrl", ex.Message, ex);
                return false;
            }
        }

        public static void UpdatingAutoURL(string URLEncrypted)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_ISwap_Autourl_SentMail_Update", cn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_RefNo", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_RefNo"].Value = URLEncrypted.Trim();

                        cn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "UpdatingAutoURL", ex.Message, ex);
            }
        }

        public static DataSet GET_SESSIONDTL_FRM_USERID(string URLEncrypted, string IPAddress)
        {
            DataSet myDataset = new DataSet();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_ISwap_AutoUrl_Validate", cn))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_RefNo", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_RefNo"].Value = URLEncrypted;

                        sqlCmd.Parameters.Add("@p_IPAddress", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_IPAddress"].Value = IPAddress;

                        SqlDataAdapter myDBAdapter = new SqlDataAdapter();
                        myDBAdapter.SelectCommand = sqlCmd;

                        cn.Open();
                        myDBAdapter.Fill(myDataset);

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "GET_SESSIONDTL_FRM_USERID", ex.Message, ex);
            }
            return myDataset;
        }

        #endregion

        #region VCM_AutoURL

        public static DataTable FillUsersList(string sUserId)
        {
            DataTable myDataTable = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_Admin_Get_All_Fix_Cust_User_List", cn)) //"Proc_Admin_Get_All_Dummy_Customer_List",
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@p_UserId", sUserId));

                        cn.Open();
                        myDataTable = new DataTable();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            myDataTable.Load(rdr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "FillUsersList", ex.Message, ex);
            }
            return myDataTable;
        }

        public static DataTable SuccessfulSendAutoURL(string strSessionID, string strUserID, string strSuccessFlag, string strURLEncrypted, string strIpAddress, string strRecoredCreateBy)
        {
            DataTable myDataTable = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_WEB_Submit_VCM_Autourl_Sentmail_Flag", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@p_SessionId", SqlDbType.Int);
                        cmd.Parameters["@p_SessionId"].Value = Convert.ToInt32(strSessionID);

                        cmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        cmd.Parameters["@p_UserId"].Value = Convert.ToInt32(strUserID);

                        cmd.Parameters.Add("@p_SuccessFlag", SqlDbType.TinyInt);
                        cmd.Parameters["@p_SuccessFlag"].Value = Convert.ToInt32(strSuccessFlag);

                        cmd.Parameters.Add("@p_AutoUrlID", SqlDbType.VarChar);
                        cmd.Parameters["@p_AutoUrlID"].Value = Convert.ToString(strURLEncrypted);

                        cmd.Parameters.Add("@p_IpAddress", SqlDbType.VarChar);
                        cmd.Parameters["@p_IpAddress"].Value = Convert.ToString(strIpAddress);

                        cmd.Parameters.Add("@p_Record_Create_By", SqlDbType.Int);
                        cmd.Parameters["@p_Record_Create_By"].Value = Convert.ToInt32(strRecoredCreateBy);

                        cn.Open();
                        myDataTable = new DataTable();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            myDataTable.Load(rdr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "SuccessfulSendAutoURL", ex.Message, ex);
            }
            return myDataTable;
        }

        public static DataTable SubmitVCMAutoURL(string strUserID, string strSessionID, string strApplicationCode, string strStartDate, string strEndDate, string strURLEncrypted, string strMessageEncrypted, string strMovetoPage, string strMailSubject, string strMailBody, string strMnemonic, string strCustomerID, string strCompanyLegalEntity, string strIpAddress, string strRecoredCreateBy, string strMinuteInterval, string Email, bool bIsCME)
        {
            DataTable myDataTable = new DataTable();

            string usrType = bIsCME ? "CME_ICAP" : "Users";

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_web_submit_vcm_autourl", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@p_UserID", SqlDbType.Int);
                        cmd.Parameters["@p_UserID"].Value = Convert.ToInt32(strUserID);

                        cmd.Parameters.Add("@p_SessionId", SqlDbType.Int);
                        cmd.Parameters["@p_SessionId"].Value = Convert.ToInt32(strSessionID);

                        cmd.Parameters.Add("@p_ApplicationCode", SqlDbType.Int);
                        cmd.Parameters["@p_ApplicationCode"].Value = Convert.ToInt32(strApplicationCode);

                        cmd.Parameters.Add("@p_StartDate", SqlDbType.DateTime);
                        cmd.Parameters["@p_StartDate"].Value = Convert.ToDateTime(strStartDate);

                        cmd.Parameters.Add("@p_EndDate", SqlDbType.DateTime);
                        cmd.Parameters["@p_EndDate"].Value = Convert.ToDateTime(strEndDate);

                        cmd.Parameters.Add("@p_AutoUrlID", SqlDbType.VarChar);
                        cmd.Parameters["@p_AutoUrlID"].Value = Convert.ToString(strURLEncrypted);

                        cmd.Parameters.Add("@p_MessageEncrypted", SqlDbType.VarChar);
                        cmd.Parameters["@p_MessageEncrypted"].Value = Convert.ToString(strMessageEncrypted);

                        cmd.Parameters.Add("@p_MovetoPage", SqlDbType.VarChar);
                        cmd.Parameters["@p_MovetoPage"].Value = Convert.ToString(strMovetoPage);

                        cmd.Parameters.Add("@p_Mail_Subject", SqlDbType.VarChar);
                        cmd.Parameters["@p_Mail_Subject"].Value = Convert.ToString(strMailSubject);

                        cmd.Parameters.Add("@p_Mail_Body", SqlDbType.VarChar);
                        cmd.Parameters["@p_Mail_Body"].Value = Convert.ToString(strMailBody);

                        cmd.Parameters.Add("@p_ClickCount", SqlDbType.Int);
                        cmd.Parameters["@p_ClickCount"].Value = 0;

                        cmd.Parameters.Add("@p_SuccessFlag", SqlDbType.TinyInt);
                        cmd.Parameters["@p_SuccessFlag"].Value = 1;

                        cmd.Parameters.Add("@p_Mnemonic", SqlDbType.VarChar);
                        cmd.Parameters["@p_Mnemonic"].Value = Convert.ToString(strMnemonic);

                        cmd.Parameters.Add("@p_CustomerID", SqlDbType.Int);
                        cmd.Parameters["@p_CustomerID"].Value = Convert.ToInt32(strCustomerID);

                        cmd.Parameters.Add("@p_Company_LegalEntity", SqlDbType.VarChar);
                        cmd.Parameters["@p_Company_LegalEntity"].Value = Convert.ToString(strCompanyLegalEntity);

                        cmd.Parameters.Add("@p_IpAddress", SqlDbType.VarChar);
                        cmd.Parameters["@p_IpAddress"].Value = Convert.ToString(strIpAddress);

                        cmd.Parameters.Add("@p_Record_Create_By", SqlDbType.Int);
                        cmd.Parameters["@p_Record_Create_By"].Value = Convert.ToInt32(strRecoredCreateBy);

                        cmd.Parameters.Add("@p_MinuteInterval", SqlDbType.Int);
                        cmd.Parameters["@p_MinuteInterval"].Value = Convert.ToInt32(strMinuteInterval);

                        cmd.Parameters.Add("@p_Remarks", SqlDbType.VarChar);
                        cmd.Parameters["@p_Remarks"].Value = "Mail sent successfully";

                        cmd.Parameters.Add("@p_Email", SqlDbType.VarChar);
                        cmd.Parameters["@p_Email"].Value = Convert.ToString(Email);

                        cmd.Parameters.Add("@p_Default_Beast_Group", SqlDbType.VarChar);
                        cmd.Parameters["@p_Default_Beast_Group"].Value = Convert.ToString(usrType);

                        cn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            myDataTable.Load(rdr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "SubmitVCMAutoURL", ex.Message, ex);
            }
            return myDataTable;
        }

        public static DataSet VCM_AutoURL_Validate_User_Info(string URLEncrypted, string IPAddress, int ApplicationCode)
        {
            DataSet myDataset = new DataSet();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Get_OpenF2_VCM_AutoURL_ValiDate", cn))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_RefNo", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_RefNo"].Value = URLEncrypted;

                        sqlCmd.Parameters.Add("@p_IPAddress", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_IPAddress"].Value = IPAddress;

                        //sqlCmd.Parameters.Add("@p_ApplicationCode", SqlDbType.Int);
                        //sqlCmd.Parameters["@p_ApplicationCode"].Value = ApplicationCode;

                        SqlDataAdapter myDBAdapter = new SqlDataAdapter();
                        myDBAdapter.SelectCommand = sqlCmd;

                        cn.Open();
                        myDBAdapter.Fill(myDataset);

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "VCM_AutoURL_Validate_User_Info", ex.Message, ex);
            }
            return myDataset;
        }

        public static DataSet VCM_AutoURL_GeoIP_Info(string IPNumber)
        {
            DataSet myDataset = new DataSet();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Beast_Get_IPDetails", cn))
                    {

                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@IP_Address", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@IP_Address"].Value = IPNumber;

                        SqlDataAdapter myDBAdapter = new SqlDataAdapter();
                        myDBAdapter.SelectCommand = sqlCmd;

                        cn.Open();
                        myDBAdapter.Fill(myDataset);

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "VCM_AutoURL_GeoIP_Info", ex.Message, ex);
            }
            return myDataset;
        }

        public static void UpdatingClickCountVCMAutoURL(string URLEncrypted, string strIPAddress)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Submit_VCM_Autourl_ClickCount_Update", cn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_RefNo", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_RefNo"].Value = URLEncrypted.Trim();

                        sqlCmd.Parameters.Add("@p_IPAddress", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_IPAddress"].Value = strIPAddress;

                        cn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "UpdatingClickCountVCMAutoURL", ex.Message, ex);
            }
        }

        public static DataTable GetLatestVCMAutoURL(string SessionID, string UserID, string ApplicationCode, string strMovetoPage, string strIpAddress, string strRecordCreateBy, string strMinuteInterval)
        {
            DataTable myDataTable = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_Web_Get_VCM_AutoURL_Latest", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@p_UserID", SqlDbType.Int);
                        cmd.Parameters["@p_UserID"].Value = Convert.ToInt32(UserID);

                        cmd.Parameters.Add("@p_SessionId", SqlDbType.Int);
                        cmd.Parameters["@p_SessionId"].Value = Convert.ToInt32(SessionID);

                        cmd.Parameters.Add("@p_ApplicationCode", SqlDbType.Int);
                        cmd.Parameters["@p_ApplicationCode"].Value = Convert.ToInt32(ApplicationCode);

                        cmd.Parameters.Add("@p_MovetoPage", SqlDbType.VarChar);
                        cmd.Parameters["@p_MovetoPage"].Value = strMovetoPage;

                        cmd.Parameters.Add("@p_IpAddress", SqlDbType.VarChar);
                        cmd.Parameters["@p_IpAddress"].Value = strIpAddress;

                        cmd.Parameters.Add("@p_Record_Create_By", SqlDbType.Int);
                        cmd.Parameters["@p_Record_Create_By"].Value = Convert.ToInt32(strRecordCreateBy);

                        cmd.Parameters.Add("@p_MinuteInterval", SqlDbType.Int);
                        cmd.Parameters["@p_MinuteInterval"].Value = Convert.ToInt32(strMinuteInterval);


                        cn.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            myDataTable.Load(rdr);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "GetLatestVCMAutoURL", ex.Message, ex);
            }
            return myDataTable;
        }

        public static int GetSessionCount(int iSessionID, int iUserID)
        {
            int LoginSessionCnt = 1;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("PROC_SessionLoginCount", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_SessionID", SqlDbType.Int);
                        sqlCmd.Parameters["@p_SessionID"].Value = iSessionID;

                        sqlCmd.Parameters.Add("@p_UserID", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserID"].Value = iUserID;

                        sqlCmd.Parameters.Add("@p_ForApplication", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_ForApplication"].Value = "RR";

                        sqlCon.Open();
                        LoginSessionCnt = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "GetSessionCount", ex.Message, ex);
            }
            return LoginSessionCnt;
        }
        #endregion

        public static DataSet BeastApps_SharedAutoURL_Validate(string pRefId)
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_AutoURL_Validate", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_AutoURLId", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AutoURLId"].Value = pRefId;

                        sqlCon.Open();

                        SqlDataAdapter myDBAdapter = new SqlDataAdapter();
                        myDBAdapter.SelectCommand = sqlCmd;

                        myDBAdapter.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "BeastApps_SharedAutoURL_Validate", ex.Message, ex);
            }
            finally { }

            return ds;
        }

        public static void BeastApps_SharedAutoURL_UpdateClickCount(string pRefId)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Submit_AppStore_AutoURL_ClickCount_Update", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_AutoURLId", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_AutoURLId"].Value = pRefId.Trim();

                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "BeastApps_SharedAutoURL_UpdateClickCount", ex.Message, ex);
            }
            finally { }
        }

        public static string Submit_AutoURL_ExtendExpiry(string AutoURLID, int MintInterval, int type)
        {
            string data = "";

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Submit_VCM_AutoURL_ExtendExpiry", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add(new SqlParameter("@p_AutoUrlID", AutoURLID));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_MinuteInterval", MintInterval));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_Block", type));
                        sqlCon.Open();

                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            if (sqlReader.Read())
                            {
                                data = Convert.ToString(sqlReader.GetValue(0));
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DataAccess.cs", "Proc_Web_Submit_VCM_AutoURL_ExtendExpiry", ex.Message, ex);
            }
            return data;
        }
        public static void BeastApps_SharedAutoURL_StoppedByInitiator(int pUserID, string pInstanceID)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Submit_AppStore_AutoURL_Initiator_Update", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        //User ID is passed but not used in sp. Remove later. [3/2/2013]
                        sqlCmd.Parameters.Add("@p_userid", SqlDbType.Int);
                        sqlCmd.Parameters["@p_userid"].Value = pUserID;

                        sqlCmd.Parameters.Add("@p_InstanceId", SqlDbType.NVarChar);
                        sqlCmd.Parameters["@p_InstanceId"].Value = pInstanceID.Trim();

                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "BeastApps_SharedAutoURL_UpdateClickCount", ex.Message, ex);
            }
            finally { }
        }
      #region File Upload

        public static void SubmitFileUploadTracking(string UserID, string ActualFileName, string UploadedFileName, string FilePath)
        {

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_FileUploadTracking", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@P_UploadedDateTime", System.DateTime.Now));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_UserID", UserID));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_ActualFileName", ActualFileName));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_UploadedFileName", UploadedFileName));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_FilePath", FilePath));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_Record_Last_Action", "N"));
                        sqlCmd.Parameters.Add(new SqlParameter("@P_id", DBNull.Value));

                        sqlCon.Open();


                        sqlCmd.ExecuteNonQuery();


                        //= Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "SubmitFileUploadTracking", ex.Message, ex);
            }

        }
        public static DataSet GetFileUploadTracking(string UserId)
        {
            DataSet dsResult = new DataSet();

            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString_AppStore))
                {

                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_FileUploadTracking", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add("@P_UserID", SqlDbType.VarChar);
                        sqlCmd.Parameters["@P_UserID"].Value = UserId;


                        sqlCon.Open();

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
                LogUtility.Error("cUserDbHandler.cs", "GetFileUploadTracking", (ex.Message).ToString(), ex);
            }
            return dsResult;

        }

        #endregion

    }
}