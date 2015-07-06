using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Configuration;
using System.Globalization;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using Savory.AutoURLValidateService;

/// <summary>
/// Summary description for DAL
/// </summary>

public class DAL
{
    SqlConnection con;
    public static string _userState;
    public static string _userDetail;

    public DAL()
    {
    }

    public DAL(int isTradeCapture)
    {
        if (isTradeCapture == 0)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString());
        }
        else if (isTradeCapture == 1)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString());
        }
        else
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString());
        }
    }
    openf2 wsObj = new openf2();

    public string GetUtcSqlServerDate()
    {
        string result = wsObj.GetUtcSqlServerDate();
        return result;
    }

    public long CheckUserStatus(string strEmailId, string strIpAddress)
    {
        long result = wsObj.CheckUserStatus(strEmailId, strIpAddress);
        return result;
    }

    public int SetUserLoginActivatationFLag(long lUserID)
    {
        int intReturnValue = -1;

        try
        {
            wsObj.SetUserLoginActivatationFLag(lUserID);
            intReturnValue = 1;
        }
        catch (Exception)
        {
            throw;
        }

        return intReturnValue;
    }

    public bool SetUserPasswordFlag(long lUserID, int iFlag, string strEmail)
    {
        bool bFlag = false;

        try
        {
            wsObj.SetUserPasswordFlag(lUserID, iFlag, strEmail);

            bFlag = true;
        }
        catch
        {
            bFlag = false;
            throw;
        }

        return bFlag;
    }

    public bool ChangePassword(Int64 lUserId, string oldPassword, string newPassword)
    {
        bool bFlag = false;

        try
        {
            wsObj.ChangePassword(lUserId, oldPassword, newPassword);
            bFlag = true;
        }
        catch (Exception ex)
        {
            bFlag = false;
            throw;
        }

        return bFlag;
    }

    public string GetUserCustomerDetails(long lUserId)
    {
        string strReturnValue = "0#0";

        try
        {
            strReturnValue = wsObj.GetUserCustomerDetails(lUserId);
        }
        catch
        {
            throw;
        }

        return strReturnValue;
    }

    public string GetUserSecurityQuestion_And_Answer(string strEmail)
    {
        string strReturnValue = "0#0";

        try
        {
            strReturnValue = wsObj.GetUserSecurityQuestion_And_Answer(strEmail);
        }
        catch
        {
            throw;
        }

        return strReturnValue;
    }

    public long GetUserID(string strEmailId)
    {
        long lReturnValue = -1;

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

    public string GetCMEUserIdFromGuid(string pGuid)
    {
        string strReturnValue = "0#0";

        try
        {
            strReturnValue = wsObj.GetCMEUserIdFromGuid(pGuid);
        }
        catch
        {
            throw;
        }

        return strReturnValue;
    }

    public int SaveCMEUserGuid(long pUserId, string pGUID)
    {
        int intReturnValue = -1;

        try
        {
            intReturnValue = wsObj.SaveCMEUserGuid(pUserId, pGUID);
        }
        catch (Exception)
        {
            throw;
        }

        return intReturnValue;
    }

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

    public bool ValidateUser(string username, string password)
    {
        int userLogin = 1;
        bool bReturn = false;

        try
        {
            bReturn = wsObj.ValidateUser(username, password);

            _userState = wsObj.GetUserstate();
        }
        catch
        {
            bReturn = false;
            throw;
        }
        return bReturn;
    }

    public Cadmin GetUserDetailsForBilling(string strEmailId)
    {
        Cadmin oDetails = new Cadmin();

        Database dbHelper = null;
        DbCommand cmd = null;
        SqlDataReader sqlReader = null;

        try
        {
            dbHelper = DatabaseFactory.CreateDatabase();
            cmd = dbHelper.GetStoredProcCommand("Proc_Get_User_Dtl_ForBilling");
            dbHelper.AddInParameter(cmd, "@p_LoginId", DbType.String, strEmailId);
            sqlReader = (SqlDataReader)dbHelper.ExecuteReader(cmd);

            if (sqlReader.Read())
            {
                oDetails.EmailId = Convert.ToString(sqlReader["LoginId"]);
                oDetails.BillingPhone = Convert.ToString(sqlReader["Billing_Phone"]);
                oDetails.BillingAddress1 = Convert.ToString(sqlReader["Billing_Address1"]);
                oDetails.BillingAddress2 = Convert.ToString(sqlReader["Billing_Address2"]);
                oDetails.BillingCity = Convert.ToString(sqlReader["Billing_City"]);
                oDetails.BillingCountry = Convert.ToString(sqlReader["Billing_Country"]);
                oDetails.BillingZip = Convert.ToString(sqlReader["Billing_ZipCode"]);
                oDetails.BillingSecurityQuestion = Convert.ToString(sqlReader["Security_Question"]);
                oDetails.BillingSecurityAnswer = Convert.ToString(sqlReader["Security_Answer"]);
                oDetails.BillingLastAccessCode = Convert.ToString(sqlReader["LastTempAccessCode"]);
            }
            sqlReader.Close();
        }
        catch
        {
            throw;
        }
        finally
        {
            cmd = null;
            dbHelper = null;
            sqlReader = null;
        }

        return oDetails;
    }

    public DataSet VCM_AutoURL_GeoIP_Info(string IPAddress)
    {
        DataSet dsResult = new DataSet();
        try
        {
            dsResult = wsObj.GetAutoURLGeoIPInfo(IPAddress);
        }
        catch
        {
            throw;
        }
        return dsResult;
    }

    public DataSet BeastApps_SharedAutoURL_Validate(string pRefId, string pClient)
    {
        DataSet dstResult = new DataSet();
        try
        {
            dstResult = wsObj.BeastApps_SharedAutoURL_Validate(pRefId);
        }
        catch (Exception ex)
        {
            throw;
        }

        return dstResult;
    }

    public DataSet VCM_AutoURL_Validate_User_Info(string URLEncrypted, string IPAddress, int ApplicationCode)
    {
        DataSet dst = new DataSet();
        try
        {
            dst = wsObj.VCM_AutoURL_Validate_User_Info(URLEncrypted, IPAddress, ApplicationCode);
        }
        catch (Exception ex)
        {
            throw;
        }
        return dst;
    }

    public void BeastApps_SharedAutoURL_UpdateClickCount(string pRefId)
    {
        try
        {
            wsObj.BeastApps_SharedAutoURL_UpdateClickCount(pRefId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    #region Registration

    public DataTable RegisterUser(string LoginId, string FirstName, string LastName, string Password)
    {
        int iResult = 0;
        DataTable dt = new DataTable();
        try
        {
            //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
            using (con)
            {
                using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Create_User_NEW", con))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@p_LoginId", LoginId);
                    sqlCmd.Parameters.AddWithValue("@p_New_User_FirstName", FirstName);
                    sqlCmd.Parameters.AddWithValue("@p_New_User_LastName", LastName);
                    sqlCmd.Parameters.AddWithValue("@p_Password", sMD5(Password));
                    con.Open();

                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = sqlCmd;
                    sda.Fill(dt);
                    con.Close();
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
    public int RegisterUserDetail(int userid, string FirstName, string LastName, string PhoneNumber, string MobileNumber, DateTime Birthday, DateTime Anniversary, bool ReturningUser, bool EmailPromotion, bool TextPromotion)
    {
        DataTable dt=new DataTable();
        int iResult = 0;
        try
        {
            //using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
            using (con)
            {
                using (SqlCommand sqlCmd = new SqlCommand("Proc_submit_User_Additional_dtl", con))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@p_userid", userid);
                    sqlCmd.Parameters.AddWithValue("@p_LastName", LastName);
                    sqlCmd.Parameters.AddWithValue("@p_FirstName", FirstName);
                    sqlCmd.Parameters.AddWithValue("@p_Phone1", PhoneNumber);
                    sqlCmd.Parameters.AddWithValue("@p_Phone2", MobileNumber);
                    sqlCmd.Parameters.AddWithValue("@p_AddressString1", "");
                    sqlCmd.Parameters.AddWithValue("@p_AddressString2", "");
                    sqlCmd.Parameters.AddWithValue("@p_legal_entity_name", "");
                    sqlCmd.Parameters.AddWithValue("@p_Company", "");
                    sqlCmd.Parameters.AddWithValue("@p_firm_type", "");
                    sqlCmd.Parameters.AddWithValue("@p_Department", "");
                    sqlCmd.Parameters.AddWithValue("@p_position", "");
                    sqlCmd.Parameters.AddWithValue("@p_Payment_Method", "");
                    sqlCmd.Parameters.AddWithValue("@p_Birthday", Birthday);
                    sqlCmd.Parameters.AddWithValue("@p_Anniversary", Anniversary);
                    sqlCmd.Parameters.AddWithValue("@p_ReturningUser", ReturningUser);
                    sqlCmd.Parameters.AddWithValue("@p_EmailPromotion", EmailPromotion);
                    sqlCmd.Parameters.AddWithValue("@p_TextPromotion", TextPromotion);
                    sqlCmd.Parameters.AddWithValue("@p_MustChangePassword", 0);
                    sqlCmd.Parameters.AddWithValue("@p_State", "");
                    sqlCmd.Parameters.AddWithValue("@p_city", "");
                    sqlCmd.Parameters.AddWithValue("@p_Country", "");
                    sqlCmd.Parameters.AddWithValue("@p_zipcode", "");
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = sqlCmd;
                    sda.Fill(dt);

                    iResult= dt.Rows.Count;
                    con.Close();
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
    #endregion
}
