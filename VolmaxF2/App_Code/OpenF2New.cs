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
/// Summary description for OpenF2New
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class OpenF2New : System.Web.Services.WebService
{

    public OpenF2New()
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
        DataSet dst = new DataSet();
        try
        {
            dst = vcmProductNamespace.cDbHandler.VCM_AutoURL_Validate_User_Info(URLEncrypted, IPAddress, ApplicationCode);
        }
        catch (Exception ex)
        {
            LogUtility.Error("openf2New.cs", "VCM_AutoURL_Validate_User_Info", ex.Message, ex);
        }
        return dst;
    }
    [WebMethod(EnableSession = true)]
    public string GetUtcSqlServerDate()
    {
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
            LogUtility.Error("openf2New.cs", "GetUtcSqlServerDate", ex.Message, ex);
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
            LogUtility.Error("openf2New.cs", "CheckUserStatus", ex.Message, ex);
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
            LogUtility.Error("openf2New.cs", "SetUserLoginActivatationFLag", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "SetUserLoginActivatationFLag", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "ChangePassword", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "GetEmailIDFromUserID", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "GetUserCustomerDetails", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "GetUserSecurityQuestion_And_Answer", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "GetUserID", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "GetCMEUserIdFromGuid", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "SaveCMEUserGuid", ex.Message, ex);
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
            // throw;
            LogUtility.Error("openf2New.cs", "ValidateUser", ex.Message, ex);
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
        DataSet dsGeoIP = new DataSet();
        try
        {
            dsGeoIP = vcmProductNamespace.cDbHandler.VCM_AutoURL_GeoIP_Info(IPAddress);
        }
        catch (Exception ex)
        {
            // throw;
            LogUtility.Error("openf2New.cs", "GetAutoURLGeoIPInfo", ex.Message, ex);
        }
        return dsGeoIP;
    }

    [WebMethod(EnableSession = true)]
    public DataSet BeastApps_SharedAutoURL_Validate(string pRefId)
    {
        DataSet dstResult = new DataSet();

        try
        {
            dstResult = vcmProductNamespace.cDbHandler.BeastApps_SharedAutoURL_Validate(pRefId);
        }
        catch (Exception ex)
        {
            // throw;
            LogUtility.Error("openf2New.cs", "BeastApps_SharedAutoURL_Validate", ex.Message, ex);
        }
        return dstResult;
    }
}