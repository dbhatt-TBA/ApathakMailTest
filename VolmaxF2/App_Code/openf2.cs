using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Diagnostics;
using VCM.Common;
using VCM.Common.Log;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data.Common;

/// <summary>
/// Summary description for openf2
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class openf2 : System.Web.Services.WebService
{
    public openf2()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    public static string _userState;
    public static string _userDetail;
    string IPAddress;

    [WebMethod(EnableSession = true)]
    public DataSet VCM_AutoURL_Validate_User_Info(string URLEncrypted, string IPAddress, int ApplicationCode)
    {
        LogUtility.Info("openf2.cs", "VCM_AutoURL_Validate_User_Info", "param=" + URLEncrypted + "," + IPAddress + "," + ApplicationCode);
        DataSet dst = new DataSet();
        try
        {
            dst = vcmProductNamespace.cDbHandler.VCM_AutoURL_Validate_User_Info(URLEncrypted, IPAddress, ApplicationCode);
        }
        catch (Exception ex)
        {
            LogUtility.Error("openf2.cs", "VCM_AutoURL_Validate_User_Info", ex.Message, ex);
        }
        return dst;
    }

    [WebMethod(EnableSession = true)]
    public string GetUtcSqlServerDate()
    {
        LogUtility.Info("openf2.cs", "GetUtcSqlServerDate", "param=NA");
        string returnDate = string.Empty;
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;

        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("GET_UTCSERVERDATE");
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);

            if (sqlReader.Read())
            {
                returnDate = Convert.ToString(sqlReader.GetValue(0));
            }
            sqlReader.Close();

        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetUtcSqlServerDate", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }
        return returnDate;
    }

    [WebMethod(EnableSession = true)]
    public long CheckUserStatus(string strEmailId, string strIpAddress)
    {
        long lReturnValue = -1;
        Database dbHelper = null;
        DbCommand cmd = null;
        LogUtility.Info("openf2.cs", "CheckUserStatus", "param=" + strEmailId + "," + strIpAddress);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("Proc_Check_User_Status");
            dbHelper.AddInParameter(cmd, "@p_UserLogin", DbType.String, strEmailId);
            dbHelper.AddInParameter(cmd, "@p_IPAddress", DbType.String, strIpAddress);
            lReturnValue = Convert.ToInt64(dbHelper.ExecuteScalar(cmd));
            cmd = null;
            dbHelper = null;
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "CheckUserStatus", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
        }
        return lReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public int SetUserLoginActivatationFLag(long lUserID)
    {
        LogUtility.Info("openf2.cs", "SetUserLoginActivatationFLag", "param=" + lUserID);
        int intReturnValue = -1;
        Database dbHelper = null;
        DbCommand cmd = null;
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetSqlStringCommand("Update User_Login_DTL set Login_Activate_FLag='0' Where UserId=" + lUserID);
            dbHelper.ExecuteNonQuery(cmd);
            cmd = null;
            dbHelper = null;
            intReturnValue = 1;
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "SetUserLoginActivatationFLag", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
        }
        return intReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public bool SetUserPasswordFlag(long lUserID, int iFlag, string strEmail)
    {
        LogUtility.Info("openf2.cs", "SetUserPasswordFlag", "param=" + lUserID + "," + iFlag + "," + strEmail);
        bool bFlag = false;
        Database dbHelper = null;
        DbCommand cmd = null;
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            if (string.IsNullOrEmpty(strEmail))
            {
                cmd = dbHelper.GetSqlStringCommand("UPDATE user_login_dtl SET change_pwd_flag = " + iFlag + " WHERE userid=" + lUserID);
            }
            else
            {
                cmd = dbHelper.GetSqlStringCommand("Update User_Login_Dtl Set Change_Pwd_Flag = " + iFlag + " Where Login_ID ='" + strEmail + "'");
            }
            dbHelper.ExecuteNonQuery(cmd);

            bFlag = true;
        }
        catch (Exception ex)
        {
            bFlag = false;
            //throw;
            LogUtility.Error("openf2.cs", "SetUserPasswordFlag", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
        }

        return bFlag;
    }

    [WebMethod(EnableSession = true)]
    public bool ChangePassword(Int64 lUserId, string oldPassword, string newPassword)
    {
        bool bFlag = false;
        Database dbHelper = null;
        DbCommand cmd = null;
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            cmd = dbHelper.GetStoredProcCommand("Proc_Web_Submit_User_Pwd_SecQueAns");
            dbHelper.AddInParameter(cmd, "@p_UserID", DbType.Int64, lUserId);
            dbHelper.AddInParameter(cmd, "@p_Password", DbType.String, sMD5(newPassword));
            dbHelper.AddInParameter(cmd, "@p_Security_Question", DbType.String, DBNull.Value);
            dbHelper.AddInParameter(cmd, "@p_Security_Answer", DbType.String, DBNull.Value);
            dbHelper.ExecuteNonQuery(cmd);
            bFlag = true;
        }
        catch (Exception ex)
        {
            bFlag = false;
            //throw;
            LogUtility.Error("openf2.cs", "ChangePassword", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
        }
        return bFlag;
    }

    [WebMethod(EnableSession = true)]
    public string GetEmailIDFromUserID(long lUserId)
    {
        string strReturnValue = "0";
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;
        LogUtility.Info("openf2.cs", "GetEmailIDFromUserID", "param=" + lUserId);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("Proc_Get_LoginId_From_UserId");
            dbHelper.AddInParameter(cmd, "@p_UserId", DbType.Int32, lUserId);
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);

            if (sqlReader.Read())
            {
                strReturnValue = Convert.ToString(sqlReader["LoginId"]);
            }
            sqlReader.Close();
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetEmailIDFromUserID", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        return strReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public string GetUserCustomerDetails(long lUserId)
    {
        string strReturnValue = "0#0";
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;
        LogUtility.Info("openf2.cs", "GetUserCustomerDetails", "param=" + lUserId);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("Proc_Get_AppStore_User_Customer_Dtl");
            dbHelper.AddInParameter(cmd, "@p_UserId", DbType.Int64, lUserId);
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);

            if (sqlReader.Read())
            {
                strReturnValue = sqlReader["UserName"] + "#" + sqlReader["Mnemonic"];
            }
            sqlReader.Close();
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetUserCustomerDetails", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        return strReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public string GetUserSecurityQuestion_And_Answer(string strEmail)
    {
        string strReturnValue = "0#0";
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;
        LogUtility.Info("openf2.cs", "GetUserSecurityQuestion_And_Answer", "param=" + strEmail);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            cmd = dbHelper.GetSqlStringCommand("Select SecQuestion As PasswordQuestion,  lower(SecAnswer) PasswordAnswer from userconfig where EmailId ='" + strEmail + "'");
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);

            if (sqlReader.Read())
            {
                strReturnValue = sqlReader["PasswordQuestion"] + "#" + sqlReader["PasswordAnswer"];
            }
            sqlReader.Close();
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetUserSecurityQuestion_And_Answer", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        return strReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public long GetUserID(string strEmailId)
    {
        long lReturnValue = -1;
        Database dbHelper = null;
        DbCommand cmd = null;
        LogUtility.Info("openf2.cs", "GetUserID", "param=" + strEmailId);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("PROC_GET_USER_ID");
            dbHelper.AddInParameter(cmd, "@p_Login_Id", DbType.String, strEmailId);
            lReturnValue = Convert.ToInt64(dbHelper.ExecuteScalar(cmd));
            cmd = null;
            dbHelper = null;
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetUserID", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
        }
        return lReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public string GetCMEUserIdFromGuid(string pGuid)
    {
        string strReturnValue = "0#0";
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;
        LogUtility.Info("openf2.cs", "GetCMEUserIdFromGuid", "param=" + pGuid);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("Proc_Get_CME_UserID_From_GUID");
            dbHelper.AddInParameter(cmd, "@p_GUID", DbType.String, pGuid);
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);

            if (sqlReader.Read())
            {
                strReturnValue = sqlReader["UserId"] + "#" + sqlReader["User_GUID"];
            }
            sqlReader.Close();
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetCMEUserIdFromGuid", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        return strReturnValue;
    }

    [WebMethod(EnableSession = true)]
    public int SaveCMEUserGuid(long pUserId, string pGUID)
    {
        LogUtility.Info("openf2.cs", "SaveCMEUserGuid", "param=" + pUserId + "," + pGUID);
        int intReturnValue = -1;
        Database dbHelper = null;
        DbCommand cmd = null;
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("Proc_Web_Submit_CME_User_GUID");
            dbHelper.AddInParameter(cmd, "@p_UserId", DbType.Int64, pUserId);
            dbHelper.AddInParameter(cmd, "@p_GUID", DbType.String, pGUID);
            dbHelper.ExecuteNonQuery(cmd);
            cmd = null;
            dbHelper = null;
            intReturnValue = 1;
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "SaveCMEUserGuid", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
        }
        return intReturnValue;
    }

    [WebMethod(EnableSession = true)]
    private string sMD5(string str)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5Prov = new System.Security.Cryptography.MD5CryptoServiceProvider();
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] md5 = md5Prov.ComputeHash(encoding.GetBytes(str));
        string _result = "";
        for (int i = 0; i < md5.Length; i++)
        {
            // _result += String.Format("{0:x}", md5[i]);
            _result += ("0" + String.Format("{0:X}", md5[i])).Substring(Convert.ToInt32(md5[i]) <= 15 ? 0 : 1, 2);
        }
        return _result;
    }

    [WebMethod(EnableSession = true)]
    public bool ValidateUser(string username, string password)
    {
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;
        int userLogin = 1;
        LogUtility.Info("openf2.cs", "ValidateUser", "param=" + username + "," + password);
        try
        {
            dbHelper = DatabaseFactory.CreateDatabase(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            cmd = dbHelper.GetStoredProcCommand("Proc_Web_User_Validate");
            dbHelper.AddInParameter(cmd, "@p_LoginId", DbType.String, username);
            dbHelper.AddInParameter(cmd, "@p_Password", DbType.String, sMD5(password));
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);
            if (sqlReader.Read())
            {
                if (sqlReader["MsgId"].ToString() == "1")    // means user validate
                {
                    //get all users details
                    _userState = sqlReader["UserId"].ToString() + "#" + sqlReader["EmailId"].ToString() + "#" + sqlReader["CustomerId"].ToString() + "#" + sqlReader["UserName"].ToString() + "#" + sqlReader["User_Type"].ToString();
                    _userState += "#" + sqlReader["Sec_Que_Change_Req_Falg"].ToString() + "#" + sqlReader["Password_Chagne_Req_Flag"].ToString() + "#" + sqlReader["LoginTypeId"].ToString() + "#" + sqlReader["LastActivityDate"].ToString();
                    userLogin = 1;
                }
                else
                {
                    // user not validate
                    userLogin = Convert.ToInt16(sqlReader["MsgId"].ToString());
                    _userState = sqlReader["MsgId"].ToString() + "#" + sqlReader["UserId"].ToString() + "#" + sqlReader["EmailId"].ToString();
                }
            }

            sqlReader.Close();
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "ValidateUser", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        if (userLogin == 1)
            return true;
        else
            return false;
    }

    [WebMethod(EnableSession = true)]
    public string GetUserstate()
    {
        return _userState;
    }

    [WebMethod(EnableSession = true)]
    public DataSet GetAutoURLGeoIPInfo(string IPAddress)
    {
        LogUtility.Info("openf2.cs", "GetAutoURLGeoIPInfo", "param=" + IPAddress);
        DataSet dsGeoIP = new DataSet();
        try
        {
            dsGeoIP = vcmProductNamespace.cDbHandler.VCM_AutoURL_GeoIP_Info(IPAddress);
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetAutoURLGeoIPInfo", ex.Message, ex);
        }
        return dsGeoIP;
    }

    [WebMethod(EnableSession = true)]
    public DataSet BeastApps_SharedAutoURL_Validate(string pRefId)
    {
        DataSet dstResult = new DataSet();
        LogUtility.Info("openf2.cs", "BeastApps_SharedAutoURL_Validate", "param=" + pRefId);
        try
        {
            dstResult = vcmProductNamespace.cDbHandler.BeastApps_SharedAutoURL_Validate(pRefId);
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "BeastApps_SharedAutoURL_Validate", ex.Message, ex);
        }

        return dstResult;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public string GetUserName()
    {
        LogUtility.Info("openf2.cs", "GetUserName", "param=NA");
        string returnUserName = string.Empty;
        try
        {
            SessionInfo CurrentSession = new SessionInfo(HttpContext.Current.Session);
            returnUserName = CurrentSession.User.UserName + "#" + CurrentSession.User.UserID;
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "GetUserName", ex.Message, ex);
        }
        finally
        {
        }
        return returnUserName;
    }

    [WebMethod(EnableSession = true)]
    public string[] AuthenticateUser(string username, string password)
    {
        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;
        int userLogin = -999;
        string[] userInfo = { "false", "" };
        string strUserState = "";

        LogUtility.Info("openf2.cs", "AuthenticateUser", "param=" + username + "," + password);

        try
        {
            dbHelper = DatabaseFactory.CreateDatabase(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString());
            cmd = dbHelper.GetStoredProcCommand("Proc_Web_User_Validate");
            dbHelper.AddInParameter(cmd, "@p_LoginId", DbType.String, username);
            dbHelper.AddInParameter(cmd, "@p_Password", DbType.String, sMD5(password));
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);
            if (sqlReader.Read())
            {
                if (sqlReader["MsgId"].ToString() == "1")    // means user validate
                {
                    //get all users details
                    strUserState = sqlReader["UserId"].ToString() + "#" + sqlReader["EmailId"].ToString() + "#" + sqlReader["CustomerId"].ToString() + "#" + sqlReader["UserName"].ToString() + "#" + sqlReader["User_Type"].ToString();
                    strUserState += "#" + sqlReader["Sec_Que_Change_Req_Falg"].ToString() + "#" + sqlReader["Password_Chagne_Req_Flag"].ToString() + "#" + sqlReader["LoginTypeId"].ToString() + "#" + sqlReader["LastActivityDate"].ToString();
                    userLogin = 1;
                    LogUtility.Info("openf2.cs", "AuthenticateUser", username + ":DB Validated");
                    userInfo[0] = "true";
                }
                else
                {
                    // user not validate
                    userLogin = Convert.ToInt16(sqlReader["MsgId"].ToString());
                    strUserState = sqlReader["MsgId"].ToString()
                                    + "#" + sqlReader["UserId"].ToString()
                                    + "#" + sqlReader["EmailId"].ToString()
                                    + "#" + sqlReader["retrycount"].ToString()
                                    + "#" + sqlReader["maxretrycount"].ToString();
                    LogUtility.Info("openf2.cs", "AuthenticateUser", username + ":DB Validation Failed. MsgId:" + userLogin);
                    userInfo[0] = "false";
                }
            }
            sqlReader.Close();
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "AuthenticateUser", ex.Message, ex);
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        userInfo[1] = strUserState;

        return userInfo;
    }

    [WebMethod(EnableSession = true)]
    public void BeastApps_SharedAutoURL_UpdateClickCount(string pRefId)
    {
        LogUtility.Info("openf2.cs", "BeastApps_SharedAutoURL_UpdateClickCount", "param=" + pRefId);
        try
        {
            vcmProductNamespace.cDbHandler.BeastApps_SharedAutoURL_UpdateClickCount(pRefId);
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "BeastApps_SharedAutoURL_UpdateClickCount", ex.Message, ex);
        }
    }

    [WebMethod(EnableSession = true)]
    public void BeastApps_SharedAutoURL_StoppedByInitiator(int pUserID, string pInstanceID)
    {
        LogUtility.Info("openf2.cs", "BeastApps_SharedAutoURL_StoppedByInitiator", "Userid = " + pUserID + "InstanceID=" + pInstanceID);
        try
        {
            vcmProductNamespace.cDbHandler.BeastApps_SharedAutoURL_StoppedByInitiator(pUserID, pInstanceID);
        }
        catch (Exception ex)
        {
            //throw;
            LogUtility.Error("openf2.cs", "BeastApps_SharedAutoURL_StoppedByInitiator", "Userid = " + pUserID + "InstanceID=" + pInstanceID + "; " + ex.Message, ex);
        }
    }

    #region Mail service to send user login notification

    [WebMethod(EnableSession = true)]
    public void SendUserLoginMail(string pMailType, string pLoginUserEmail, string pClient, string pIpAddress)
    {
        try
        {
            MailManager _mailManager;
            switch (pMailType)
            {
                case "0":
                    _mailManager = new MailManager(MailManager.MailType.Login_Success, pLoginUserEmail, pIpAddress, pClient);
                    _mailManager.SendMail();
                    break;

                case "1":
                    _mailManager = new MailManager(MailManager.MailType.Login_AccountLocked, pLoginUserEmail, pIpAddress, pClient);
                    _mailManager.SendMail();
                    break;

                case "2":
                    _mailManager = new MailManager(MailManager.MailType.Login_AccountBlocked, pLoginUserEmail, pIpAddress, pClient);
                    _mailManager.SendMail();
                    break;

                case "3":
                    _mailManager = new MailManager(MailManager.MailType.Login_OutOfDomainAccess, pLoginUserEmail, pIpAddress, pClient);
                    _mailManager.SendMail();
                    break;

                case "4":
                    _mailManager = new MailManager(MailManager.MailType.Login_IPUnauthorized, pLoginUserEmail, pIpAddress, pClient);
                    _mailManager.SendMail();
                    break;

                case "5":
                    _mailManager = new MailManager(MailManager.MailType.Login_UserNotRegistered, pLoginUserEmail, pIpAddress, pClient);
                    _mailManager.SendMail();
                    break;
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("openF2.cs", "SendUserLoginMail()", "Mail service dead", ex);
        }
    }

    [WebMethod(EnableSession = true)]
    public string ResetUserPassword(string strUserLoginId, string strIpAddress, string strClient)
    {
        string retValue = "-1#fail";
        try
        {
            long lUserId = VcmUserNamespace.cUserDbHandler.CheckUserStatus(strUserLoginId, strIpAddress);

            Random rdm = new Random();
            long lNewPassword = rdm.Next(100000, 999999);

            bool chngPwd = this.ChangePassword(lUserId, "", lNewPassword.ToString());

            bool setPwdFlag = this.SetUserPasswordFlag(lUserId, 0, strUserLoginId);

            MailManager _mailManager = new MailManager(MailManager.MailType.User_ResetPassword, strUserLoginId, lNewPassword.ToString(), strIpAddress, strClient);
            _mailManager.SendMail();

            retValue = "1#Your password is reset and email for your new temporary password is sent to you on your email id. Please login with that and change your password.";
        }
        catch (Exception ex)
        {
            LogUtility.Error("openF2.cs", "ResetUserPassword()", "Mail service dead", ex);
            retValue = "-1#An Error occured retting password. Please try again or if problem persists, please contact us";
        }

        return retValue;
    }

    #endregion



    [WebMethod(EnableSession = true)]
    public DataSet RegisterUser(string LoginId, string FirstName, string LastName, string Password, Int16 chngPwd)
    {
        int iResult = 0;
        DataSet dt = new DataSet();
        try
        {
            string tc_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString();
            using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Create_User_NEW", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@p_LoginId", LoginId);
                    sqlCmd.Parameters.AddWithValue("@p_New_User_FirstName", FirstName);
                    sqlCmd.Parameters.AddWithValue("@p_New_User_LastName", LastName);
                    sqlCmd.Parameters.AddWithValue("@p_Password", sMD5(Password));
                    sqlCmd.Parameters.Add(new SqlParameter("@MustChangePassword", chngPwd));
                    //sqlCmd.Parameters.AddWithValue("@p_CreatedBy", 0);
                    //sqlCmd.Parameters.AddWithValue("@p_Default_Beast_Group", "CME_ICAP");
                    sqlCon.Open();

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = sqlCmd;
                    sda.Fill(dt);
                    sqlCon.Close();
                }
            }
        }
        catch (Exception ex)
        {
            iResult = 0;
            throw;
        }
        return dt;
    }

    [WebMethod(EnableSession = true)]
    public DataSet Submit_User_Subscription(int UserId, DateTime SubStartDate, DateTime SubEndDate, string profileId)
    {
        int iResult = 0;
        DataSet dt = new DataSet();
        try
        {
            string AppStore_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString();
            using (SqlConnection sqlCon = new SqlConnection(AppStore_ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_UserSubscriptionMaster", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@P_UserId", UserId);
                    sqlCmd.Parameters.AddWithValue("@P_SubscriptionId", 1);
                    sqlCmd.Parameters.AddWithValue("@P_SubStartDate", SubStartDate);
                    sqlCmd.Parameters.AddWithValue("@P_SubEndDate", SubEndDate);
                    sqlCmd.Parameters.AddWithValue("@P_IsActive", true);
                    sqlCmd.Parameters.AddWithValue("@P_IsDeleted", false);
                    sqlCmd.Parameters.AddWithValue("@P_Record_Create_Date", SubStartDate);
                    sqlCmd.Parameters.AddWithValue("@P_Record_Created_By", "77777");
                    if (profileId == "")
                        sqlCmd.Parameters.AddWithValue("@P_ProfileId", DBNull.Value);
                    else
                        sqlCmd.Parameters.AddWithValue("@P_ProfileId", profileId);
                    sqlCon.Open();

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = sqlCmd;
                    sda.Fill(dt);
                    sqlCon.Close();
                }
            }
        }
        catch (Exception ex)
        {
            iResult = 0;
            throw;
        }
        return dt;
    }

    [WebMethod(EnableSession = true)]
    public int Save_User_Details(int userid, string FirstName, string LastName, string PhoneNumber, string Company,
        string EntityName, string FirmType, string Department, string Position, string PaymentMethod,
        string Address, string State, string City, string Country, string Zipcode)
    {
        int iResult = 0;
        try
        {
            string ss_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString();
            using (SqlConnection sqlCon = new SqlConnection(ss_ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand("Proc_submit_User_Additional_dtl", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@p_userid", userid);
                    sqlCmd.Parameters.AddWithValue("@p_LastName", LastName);
                    sqlCmd.Parameters.AddWithValue("@p_FirstName", FirstName);
                    sqlCmd.Parameters.AddWithValue("@p_Phone1", PhoneNumber);
                    sqlCmd.Parameters.AddWithValue("@p_Phone2", "");
                    sqlCmd.Parameters.AddWithValue("@p_AddressString1", Address);
                    sqlCmd.Parameters.AddWithValue("@p_AddressString2", "");
                    sqlCmd.Parameters.AddWithValue("@p_legal_entity_name", EntityName);
                    sqlCmd.Parameters.AddWithValue("@p_Company", Company);
                    sqlCmd.Parameters.AddWithValue("@p_firm_type", FirmType);
                    sqlCmd.Parameters.AddWithValue("@p_Department", Department);
                    sqlCmd.Parameters.AddWithValue("@p_position", Position);
                    sqlCmd.Parameters.AddWithValue("@p_Payment_Method", PaymentMethod);
                    sqlCmd.Parameters.AddWithValue("@p_Birthday", "");
                    sqlCmd.Parameters.AddWithValue("@p_Anniversary", "");
                    sqlCmd.Parameters.AddWithValue("@p_ReturningUser", "");
                    sqlCmd.Parameters.AddWithValue("@p_EmailPromotion", "");
                    sqlCmd.Parameters.AddWithValue("@p_TextPromotion", "");
                    sqlCmd.Parameters.AddWithValue("@p_MustChangePassword", 0);
                    sqlCmd.Parameters.AddWithValue("@p_State", State);
                    sqlCmd.Parameters.AddWithValue("@p_city", City);
                    sqlCmd.Parameters.AddWithValue("@p_Country", Country);
                    sqlCmd.Parameters.AddWithValue("@p_zipcode", Zipcode);
                    sqlCon.Open();
                    iResult = sqlCmd.ExecuteNonQuery();
                    sqlCon.Close();
                }
            }
        }
        catch (Exception ex)
        {
            iResult = 0;
            throw;
        }
        return iResult;
    }

    [WebMethod(EnableSession = true)]
    public string[] ValidateUser_New_UM(string username, string password, string aspSessionId, int ssid)
    {
        string[] userinfo = { "", "" };
        DataTable dt = new DataTable();

        string Session_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString();
        //     dbHelper = DatabaseFactory.CreateDatabase(System.Configuration.ConfigurationManager.AppSettings["SessionServerConnectionString"].ToString());
        int userLogin = 1;
        LogUtility.Info("openf2.cs", "ValidateUser", "param=" + username + "," + password);
        try
        {
            _userState = "";
            userLogin = -999;
            using (SqlConnection sqlCon = new SqlConnection(Session_ConnectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand("PROC_UM_GET_VALIDATE_USER", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@P_Password", sMD5(password)));
                    sqlCmd.Parameters.Add(new SqlParameter("@P_Name", username));
                    sqlCmd.Parameters.Add(new SqlParameter("@ssid", ssid));
                    sqlCmd.Parameters.Add(new SqlParameter("@SessionID", aspSessionId));

                    sqlCon.Open();

                    using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                    {
                        if (sqlReader.Read())
                        {
                            //if (sqlReader["MsgId"].ToString() == "1")    // means user validate
                            //{
                            //get all users details
                            if (Convert.ToString(sqlReader["MsgId"].ToString()) == "1")
                                userinfo[0] = "true";
                            else
                                userinfo[0] = "false";

                            userinfo[1] = sqlReader["UserId"].ToString()
                                       + "#" + sqlReader["EmailId"].ToString()
                                        + "#" + sqlReader["Name"].ToString()
                                        + "#" + sqlReader["Sec_Que_Change_Req_Falg"].ToString()
                                        + "#" + sqlReader["Password_Chagne_Req_Flag"].ToString()
                                        + "#" + sqlReader["LoginTypeId"].ToString()
                                        + "#" + sqlReader["LastActivityDate"].ToString()
                                        + "#" + sqlReader["FirstName"].ToString()
                                        + "#" + sqlReader["LastName"].ToString()
                                        + "#" + sqlReader["Validationflag"].ToString()
                                        + "#" + sqlReader["UserRoleId"].ToString()
                                        + "#" + sqlReader["UserRole"].ToString()
                                        + "#" + sqlReader["RetryCount"].ToString()
                                        + "#" + sqlReader["LoginTime"].ToString();
                            //    userLogin = Convert.ToInt16(sqlReader["MsgId"].ToString());

                            //}
                            //else
                            //{
                            //    // user not validate
                            //    userLogin = Convert.ToInt16(sqlReader["MsgId"].ToString());
                            //    _userState = sqlReader["MsgId"].ToString() + "#" + sqlReader["UserId"].ToString() + "#" + sqlReader["EmailId"].ToString();
                            //}

                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("DataAccess.cs", "ValidateUser", ex.Message, ex);
        }

        return userinfo;
    }
    [WebMethod(EnableSession = true)]
    public void SaveAutoUrlAccessInfo(string autourltype, string productType, string product, string autourl, string SenderIP, int SenderId, string SenderName, DateTime TimeOfSend,
            string Receiverip, string ReceiverEmail, DateTime TimeOfAccess, string ISprovider, string Locationcity, string LocationCountry, string autourlvalidity,
             int Record_create_by)
    {

        try
        {
            string Appstore_comectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString();
            using (SqlConnection sqlCon = new SqlConnection(Appstore_comectionString))
            {
                using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_submit_AutoURL_History", sqlCon))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.Add(new SqlParameter("@p_autourltype", autourltype));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_Producttype", product));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_ProductName", product));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_autourl", autourl));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_SenderIP ", SenderIP));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_SenderId", SenderId));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_SenderName", SenderName));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_TimeOfSend", TimeOfSend));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_Receiverip", Receiverip));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_ReceiverEmail", ReceiverEmail));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_TimeOfAccess", TimeOfAccess));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_ISprovider", ISprovider));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_Locationcity", Locationcity));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_LocationCountry", LocationCountry));
                    sqlCmd.Parameters.Add(new SqlParameter("@p_autourlvalidity", autourlvalidity));

                    sqlCmd.Parameters.Add(new SqlParameter("@p_Record_create_by", Record_create_by));

                    sqlCon.Open();
                    sqlCmd.ExecuteNonQuery();



                }
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("cUserDbHandler.cs", "SaveAutoUrlAccessInfo", ex.Message, ex);
        }
    }
    [WebMethod(EnableSession = true)]
    public string Submit_AutoURL_ExtendExpiry(string AutoURLID, int MintInterval, int type)
    {
        string data = "";
        try
        {
          data=  vcmProductNamespace.cDbHandler.Submit_AutoURL_ExtendExpiry(AutoURLID, MintInterval, type);
        }
        catch (Exception ex)
        {
            LogUtility.Error("DataAccess.cs", "Proc_Web_Submit_VCM_AutoURL_ExtendExpiry", ex.Message, ex);
        }
        return data;
    }

}