using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;
using VCM.Common.Log;

/// <summary>
/// Summary description for cUserDbHandler
/// </summary>
/// 
namespace VcmUserNamespace
{
    public class cUserDbHandler : System.Web.UI.Page
    {
        static string tc_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString();
        static string ss_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SessionServerConnectionString"].ToString();
        static string connectionString_AppStore = System.Configuration.ConfigurationManager.ConnectionStrings["AppStoreConnectionString"].ToString();

        public cUserDbHandler()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region LOGIN Information

        public static string GetUtcSqlServerDate()
        {
            string returnDate = string.Empty;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("GET_UTCSERVERDATE", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCon.Open();
                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            if (sqlReader.Read())
                            {
                                returnDate = Convert.ToString(sqlReader.GetValue(0));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "GetUtcSqlServerDate", ex.Message, ex);
            }
            return returnDate;
        }

        public static long CheckUserStatus(string strEmailId, string strIpAddress)
        {
            long lReturnValue = -1;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Check_User_Status", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_UserLogin", strEmailId));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_IPAddress", strIpAddress));
                        sqlCon.Open();
                        lReturnValue = Convert.ToInt64(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "CheckUserStatus", ex.Message, ex);
            }
            return lReturnValue;
        }

        public static string _userState;
        public static string _userDetail;

        public static bool ValidateUser(string username, string password)
        {
            int userLogin = 1;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ss_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_User_Validate", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_LoginId", username));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_Password", sMD5(password)));
                        sqlCon.Open();
                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            if (sqlReader.Read())
                            {
                                if (sqlReader["MsgId"].ToString() == "1")    // means user validate
                                {
                                    //get all users details
                                    _userState = sqlReader["UserId"].ToString() + "#" + sqlReader["EmailId"].ToString() + "#" + sqlReader["CustomerId"].ToString() + "#" + sqlReader["UserName"].ToString() + "#" + sqlReader["User_Type"].ToString();
                                    _userState += "#" + sqlReader["Sec_Que_Change_Req_Falg"].ToString() + "#" + sqlReader["Password_Chagne_Req_Flag"].ToString() + "#" + sqlReader["LoginTypeId"].ToString() + "#" + sqlReader["LastActivityDate"].ToString() + "#" + sqlReader["CustomerName"].ToString() + "#" + sqlReader["Mnemonic"].ToString();
                                    userLogin = 1;
                                }
                                else
                                {
                                    // user not validate
                                    userLogin = Convert.ToInt16(sqlReader["MsgId"].ToString());
                                    _userState = sqlReader["MsgId"].ToString() + "#" + sqlReader["UserId"].ToString() + "#" + sqlReader["EmailId"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "ValidateUser", ex.Message, ex);
            }
            if (userLogin == 1)
                return true;
            else
                return false;
        }

        private static string sMD5(string str)
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

        public static string GetUserPrimarySetting(long lUserId, int iFlagFor)
        {
            string strReturnValue = "-1";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("proc_Get_User_Primary_Setting", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_UserId", lUserId));
                        sqlCon.Open();
                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            if (sqlReader.Read())
                            {
                                if (iFlagFor == 0)  //Get Password Flag
                                {
                                    strReturnValue = Convert.ToString(sqlReader["CHANGE_PWD_FLAG"]);
                                }
                                else if (iFlagFor == 1) //Get SecurityQuestion Flag
                                {
                                    strReturnValue = Convert.ToString(sqlReader["SEC_QTN_FLAG"]);
                                }
                                else if (iFlagFor == 2) //Get Password Flag # SecurityQuestion Flag
                                {
                                    if (sqlReader.FieldCount > 1)
                                    {
                                        strReturnValue = Convert.ToString(sqlReader["SEC_QTN_FLAG"]) + "#" + Convert.ToString(sqlReader["CHANGE_PWD_FLAG"]);
                                    }
                                    else
                                    {
                                        strReturnValue = Convert.ToString(sqlReader["Err_Msg"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "GetUserPrimarySetting", ex.Message, ex);
            }
            return strReturnValue;
        }

        public static int SetUserLoginActivatationFLag(long lUserID)
        {
            int intReturnValue = -1;
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Update User_Login_DTL set Login_Activate_FLag='0' Where UserId=" + lUserID, sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.Text;
                        sqlCon.Open();
                        sqlCmd.ExecuteNonQuery();
                        intReturnValue = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "SetUserLoginActivatationFLag", ex.Message, ex);
            }
            return intReturnValue;
        }

        public static string GetUserID(string strEmailID)
        {
            string strReturn = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("PROC_GET_USER_ID", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_Login_Id", strEmailID));
                        sqlCon.Open();
                        strReturn = Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "GetUserID", ex.Message, ex);
            }
            return strReturn;
        }

        public static string GetCustomerId(string strUserId, string SessionId)
        {
            string strReturn = "";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_ISwap_Customer_List", sqlCon)) //PROC_GET_CUSTOMER_LIST
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_UserId", strUserId));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_SessionId", SessionId));
                        sqlCon.Open();
                        strReturn = Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "GetCustomerId", ex.Message, ex);
            }
            return strReturn;
        }

        public static string CheckIsTrader(string strUserId)
        {
            string strIsTrader = "FALSE";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_ISwap_User_Customer_Dtl", sqlCon))//Proc_Get_User_Customer_Dtl
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_UserId", strUserId));
                        sqlCon.Open();
                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            while (sqlReader.Read())
                            {
                                strIsTrader = (Convert.ToString(sqlReader["CustomerName"]) == "" ? "FALSE" : "TRUE");
                                HttpContext.Current.Session["UName"] = sqlReader["UserName"];
                                HttpContext.Current.Session["CustName"] = sqlReader["CustomerName"];
                                HttpContext.Current.Session["CUSTNAME"] = sqlReader["Mnemonic"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "CheckIsTrader", ex.Message, ex);
            }
            return strIsTrader;
        }

        public static DataTable CreateUserWithMinInfo(string sFirstName, string sLastName, string stEmailId, bool bIsCmeUser, string sPass, string sCreatedBY)
        {
            DataTable dtTmp = new DataTable();
            string usrType = bIsCmeUser ? "CME_ICAP" : "Users";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(tc_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Web_Create_User_NEW", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_New_User_FirstName", sFirstName));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_New_User_LastName", sLastName));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_LoginId", stEmailId));
                        sqlCmd.Parameters.Add(new SqlParameter("@p_Password", sMD5(sPass)));
                      
                        sqlCon.Open();

                        SqlDataAdapter sqlDA = new SqlDataAdapter();
                        sqlDA.SelectCommand = sqlCmd;
                        sqlDA.Fill(dtTmp);

                        //= Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "CreateUserWithMinInfo", ex.Message, ex);
            }
            return dtTmp;
        }

        #endregion
        public static string CheckIsTraderNew(string strUserId)
        {
            string strIsTrader = "TRUE";
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ss_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_User_Rights", sqlCon)) //Proc_Get_ISwap_User_Customer_Dtl/Proc_Get_User_Customer_Dtl
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.Add(new SqlParameter("@p_UserId", strUserId));
                        sqlCon.Open();
                        using (SqlDataReader sqlReader = sqlCmd.ExecuteReader())
                        {
                            if (sqlReader.Read())
                            {
                                if (Convert.ToInt16(sqlReader["IsTBAAdmin"]) == 1)
                                {
                                    strIsTrader = "FALSE";
                                }
                                else if (Convert.ToInt16(sqlReader["UserAccessFlag"]) == 1)
                                {
                                    strIsTrader = "FALSE";
                                }
                                else
                                {
                                    strIsTrader = "TRUE";
                                }
                                //strIsTrader = (Convert.ToString(sqlReader["CustomerName"]) == "" ? "FALSE" : "TRUE");
                                HttpContext.Current.Session["IsTBAAdmin"] = sqlReader["IsTBAAdmin"];
                                HttpContext.Current.Session["UserAccessFlag"] = sqlReader["UserAccessFlag"];
                                HttpContext.Current.Session["UserGroup"] = sqlReader["UserGroup"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "CheckIsTrader", ex.Message, ex);
            }
            return strIsTrader;
        }


        #region Registration

        public static DataTable VendorList()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ss_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_UM_Get_User_Vendor_Mst", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCon.Open();

                        SqlDataAdapter sqlDA = new SqlDataAdapter();
                        sqlDA.SelectCommand = sqlCmd;
                        sqlDA.Fill(dtTmp);

                        //= Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "VendorList", ex.Message, ex);
            }
            return dtTmp;
        }

        public static DataTable CategoryList()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_Category", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCon.Open();

                        SqlDataAdapter sqlDA = new SqlDataAdapter();
                        sqlDA.SelectCommand = sqlCmd;
                        sqlDA.Fill(dtTmp);

                        //= Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "CategoryList", ex.Message, ex);
            }
            return dtTmp;
        }

        public static DataTable SIFList()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(ss_ConnectionString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("proc_get_free_Sif_info", sqlCon))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCon.Open();

                        SqlDataAdapter sqlDA = new SqlDataAdapter();
                        sqlDA.SelectCommand = sqlCmd;
                        sqlDA.Fill(dtTmp);

                        //= Convert.ToString(sqlCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cUserDbHandler.cs", "CategoryList", ex.Message, ex);
            }
            return dtTmp;
        }

        public static DataTable FillRegisterList(string RegId, string SIDId)
        {
            DataTable myDataTable = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_Get_AppStore_Registrations_Dtl", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@p_RegId", (RegId == "0" ? Convert.DBNull : Convert.ToInt32(RegId))));
                        cmd.Parameters.Add(new SqlParameter("@p_sifid", (SIDId == "0" ? Convert.DBNull : Convert.ToInt32(SIDId))));
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
                LogUtility.Error("cDbHandler.cs", "FillRegisterList", ex.Message, ex);
            }
            return myDataTable;
        }

        //string AppIconImage, 
        public static void InsertUpdateRegistration(int RegId, string AppName, string AppTitle, int UserId, string Description, string AppVersion, int CategoryId,
                                                    string AppSupportedOS, string AppFileSize, string AppPrice, string AppTags, string AppSupportedLanguage,
                                                    string Email, string ContactNumber, string Action, string AppPackageURL, string Currency,
                                                    string SupportURL, string YouTubeURL, string AppFilePath, string AppSupportedDevices,
                                                    int VendorId, DateTime AppReleasedDate, string AppAgeCriteria, string AppVendorInformation, int SIFID, int IsGridImage,
                                                    bool ISshareable, int shareminitues)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString_AppStore))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_Registrations", cn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.Add("@p_RegId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_RegId"].Value = RegId;

                        sqlCmd.Parameters.Add("@p_AppName", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppName"].Value = AppName;

                        sqlCmd.Parameters.Add("@p_AppTitle", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppTitle"].Value = AppTitle;

                        sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_UserId"].Value = UserId;

                        sqlCmd.Parameters.Add("@p_AppDescription", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppDescription"].Value = Description;

                        sqlCmd.Parameters.Add("@p_AppVersion", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppVersion"].Value = AppVersion;

                        sqlCmd.Parameters.Add("@p_CategoryId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_CategoryId"].Value = CategoryId;

                        sqlCmd.Parameters.Add("@p_AppSupportedOS", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppSupportedOS"].Value = AppSupportedOS;

                        sqlCmd.Parameters.Add("@p_AppFileSize", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppFileSize"].Value = AppFileSize;

                        sqlCmd.Parameters.Add("@p_AppPrice", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppPrice"].Value = AppPrice;

                        sqlCmd.Parameters.Add("@p_AppTags", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppTags"].Value = AppTags;

                        sqlCmd.Parameters.Add("@p_AppSupportedLanguage", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppSupportedLanguage"].Value = AppSupportedLanguage;

                        //sqlCmd.Parameters.Add("@p_AppIconImage", SqlDbType.VarBinary);
                        //sqlCmd.Parameters["@p_AppIconImage"].Value = AppIconImage;

                        sqlCmd.Parameters.Add("@p_Email", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Email"].Value = Email;

                        sqlCmd.Parameters.Add("@p_ContactNumber", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_ContactNumber"].Value = ContactNumber;

                        sqlCmd.Parameters.Add("@p_Action", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Action"].Value = Action;

                        sqlCmd.Parameters.Add("@p_AppPackageURL", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppPackageURL"].Value = AppPackageURL;

                        sqlCmd.Parameters.Add("@p_Currency", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_Currency"].Value = Currency;

                        sqlCmd.Parameters.Add("@p_SupportURL", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_SupportURL"].Value = SupportURL;

                        sqlCmd.Parameters.Add("@p_YouTubeURL", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_YouTubeURL"].Value = YouTubeURL;

                        sqlCmd.Parameters.Add("@p_AppFilePath", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppFilePath"].Value = AppFilePath;

                        sqlCmd.Parameters.Add("@p_AppSupportedDevices", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppSupportedDevices"].Value = AppSupportedDevices;

                        sqlCmd.Parameters.Add("@p_VendorId", SqlDbType.Int);
                        sqlCmd.Parameters["@p_VendorId"].Value = VendorId;

                        sqlCmd.Parameters.Add("@p_AppReleasedDate", SqlDbType.DateTime);
                        sqlCmd.Parameters["@p_AppReleasedDate"].Value = AppReleasedDate;

                        sqlCmd.Parameters.Add("@p_AppAgeCriteria", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppAgeCriteria"].Value = AppAgeCriteria;

                        sqlCmd.Parameters.Add("@p_AppVendorInformation", SqlDbType.VarChar);
                        sqlCmd.Parameters["@p_AppVendorInformation"].Value = AppVendorInformation;

                        sqlCmd.Parameters.Add("@p_BeastImageSID", SqlDbType.Int);
                        sqlCmd.Parameters["@p_BeastImageSID"].Value = SIFID;

                        sqlCmd.Parameters.Add("@p_IsGridImage", SqlDbType.Int);
                        sqlCmd.Parameters["@p_IsGridImage"].Value = IsGridImage;

                        sqlCmd.Parameters.Add("@p_ISshareable", SqlDbType.Bit);
                        sqlCmd.Parameters["@p_ISshareable"].Value = ISshareable;

                        sqlCmd.Parameters.Add("@p_shareminitues", SqlDbType.Int);
                        sqlCmd.Parameters["@p_shareminitues"].Value = shareminitues;

                        cn.Open();
                        sqlCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("cDbHandler.cs", "InsertUpdateRegistration", ex.Message, ex);
            }
        }

        #endregion

        #region Apps


        public static DataTable FillAppsList(int VendorId)
        {
            DataTable myDataTable = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ss_ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Proc_UM_GET_Vendor_App_Mst", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@p_Vendorid", (Convert.ToInt32(VendorId))));
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
                LogUtility.Error("cDbHandler.cs", "FillAppsList", ex.Message, ex);
            }
            return myDataTable;
        }

        #endregion 
    }
}