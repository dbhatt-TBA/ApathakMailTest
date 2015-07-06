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
using System.Net.Mail;
using System.Configuration;
using VolmaxLauncherLibrary;
using OpenBeast.TradeCaptureService;
using VCM.Common.Log;
/// <summary>
/// Summary description for Service
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
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

    #region SwaptionVolPrem

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetCurrentMarketDataVol(string UserID, string ProductID)
    {
        if ((AppsInfo.Instance._dirImgSID.ContainsKey("vcm_calc_swaptionVolPremStrike")) == true)
        {
            string ProductIDStr = Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"]);
            SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage(ProductIDStr, "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
            DataTable dt = bi.dtVolGrid;

            //return dt.SerializeTableToString();
            //dt.TableName = "Table";
            return UtilComman.GetJSONString(dt);
        }
        else
            return "";
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetCurrentMarketDataPrem()
    {
        string ProductIDStr = Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"]);
        SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage(ProductIDStr, "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
        DataTable dt = bi.dtPremGrid;

        //return dt.SerializeTableToString();
        //dt.TableName = "Table";
        return UtilComman.GetJSONString(dt);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetCurrentMarketDataStrike()
    {
        string ProductIDStr = Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"]);
        SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage(ProductIDStr, "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
        DataTable dt = bi.dtStrikeGrid;

        //return dt.SerializeTableToString();
        //dt.TableName = "Table";
        return UtilComman.GetJSONString(dt);
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
    public string GetGridNodeIDs()
    {
        string ProductIDStr = Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"]);
        SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage(ProductIDStr, "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
        DataTable dtVoldNodeIDs = bi.dtVolGridNodeIDs;

        return UtilComman.GetJSONString(dtVoldNodeIDs);
    }
    #endregion

    #region Share Calc

    [WebMethod(EnableSession = true)]
    public string SubmitBeastCalcAutoUrlShare(long pUserId, string pInstanceId, string pInstanceInfo, string pIpAddress, string pEmailIdList, string SenderEmail, string SenderMessage, string ClientType, string GUID, int expMinutes, bool isBulkShare)
    {
        string[] ValidToken = null;
        string strReturnMsg = "Failed to Share.  Please try again.";
        string defaultLandingPage = ConfigurationManager.AppSettings["defaultLandingPage"].ToString();
        string moveToPage = "SharedBeastApps.aspx";

        try
        {
            ValidToken = ValidateAuthToken(Convert.ToString(pUserId), ClientType, GUID);
            if (ValidToken[0] == "False")
            {
                strReturnMsg = "AuthInvalid#" + ValidToken[1];

                return strReturnMsg;
            }

            char pSeparator = '#';

            string vGuid = "";

            DataTable dtTableParam = new DataTable();
            DataColumn dcTmp = new DataColumn("AutoURLId");
            dtTableParam.Columns.Add(dcTmp);
            dcTmp = new DataColumn("AutoURL");
            dtTableParam.Columns.Add(dcTmp);
            dcTmp = new DataColumn("EmailId");
            dtTableParam.Columns.Add(dcTmp);
            DataRow _dr;

            if (isBulkShare)
            {
                BulkShare oBulkShare = new BulkShare();

                pEmailIdList = oBulkShare.GetSharedUsersList(pSeparator);

                if (pEmailIdList == "-1")
                {
                    pEmailIdList = pEmailIdList + "#No emails found to send share url";

                    return strReturnMsg;
                }
            }
            else
            {
                pEmailIdList = pEmailIdList.Replace("\r\n", "").Replace("\n", "");
            }

            string[] ArrEmailIds = pEmailIdList.Split(pSeparator);

            if (ArrEmailIds.Length > 0)
            {
                for (int i = 0; i < ArrEmailIds.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ArrEmailIds[i].Trim()))
                    {
                        vGuid = Convert.ToString(Guid.NewGuid());

                        _dr = dtTableParam.NewRow();
                        _dr["AutoURLId"] = vGuid;
                        _dr["AutoURL"] = defaultLandingPage + "?id=" + vGuid;
                        _dr["EmailId"] = ArrEmailIds[i].Trim();
                        dtTableParam.Rows.Add(_dr);
                    }
                }
            }

            DateTime defaultStart = DateTime.UtcNow;
            DateTime defaultEnd = DateTime.UtcNow.AddMinutes(expMinutes);
            int defaultMinutes = expMinutes;

            //Submit to DB

            clsDAL oClsDAL = new clsDAL(false);
            int iResult = oClsDAL.SubmitBeastCalcSharing(pUserId, dtTableParam, defaultStart, defaultEnd, moveToPage, pIpAddress, defaultMinutes, "", pInstanceId, pInstanceInfo);

            //strReturnMsg = iResult == 1 ? "[{'IsSave': 'TRUE'}]" : "[{'IsSave': 'FALSE'}]";
            strReturnMsg = iResult == 1 ? "Calculator shared successfully." : "Failed to Share.  Please try again.";

            if (iResult == 1)
            {
                string vUserName = oClsDAL.GetUserNameFromUserId(pUserId);
                vUserName = vUserName.Split('#')[0];
                SendAutoUrlMail(dtTableParam, vUserName, defaultStart.ToString(), defaultEnd.ToString(), SenderEmail, pUserId, SenderMessage);
            }
        }
        catch (Exception ex)
        {

        }
        return strReturnMsg;
    }

    #endregion

    #region Last Calc Open

    [WebMethod(EnableSession = true)]
    public void SubmitLastOpenCalc(long pUserId, string pInstanceId, string pInstanceInfo)
    {
        try
        {
            //Submit to DB

            clsDAL oClsDAL = new clsDAL(false);
            int iResult = oClsDAL.SubmitLastCalcDetail(pUserId, pInstanceId, pInstanceInfo);

            //strReturnMsg = iResult == 1 ? "[{'IsSave': 'TRUE'}]" : "[{'IsSave': 'FALSE'}]";
            //strReturnMsg = iResult == 1 ? "Calculator shared successfully." : "Failed to Share.  Please try again.";
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetLastOpenCalc(string UserId, string ClientType, string GUID)
    {
        string calcDetails = "";
        DataSet ds = null;
        ds = new DataSet();
        DataTable dt = new DataTable();

        try
        {
            string[] ValidToken = null;

            //ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            //if (ValidToken[0] == "False")
            //{
            //    dt.Columns.Add("InstanceId");
            //    dt.Columns.Add("InstanceInfo");


            //    dt.Rows.Add("False", ValidToken[1]);
            //    dt.TableName = "d";
            //}
            //else
            //{
            DataSet dsnew = null;
            dsnew = new DataSet();
            clsDAL oClsDAL = new clsDAL(false);
            ds = oClsDAL.GetLastCalcDetail(Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
            dt = ds.Tables[0];
            // }

            return UtilComman.GetJSONString(dt);

        }
        catch (Exception ex)
        {
            ds = null;
            dt = null;
        }
        finally
        {
            ds = null;
            dt = null;
        }
        return calcDetails;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetMainMenuCategory(string UserId, string ClientType, string GUID)
    {
        string calcDetails = "";
        DataSet ds = new DataSet();


        try
        {
            string[] ValidToken = null;

            ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            if (ValidToken[0] == "False")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryId");
                dt.Columns.Add("CategoryName");
                dt.Rows.Add("False", ValidToken[1]);
                dt.TableName = "d";
                ds.Tables.Add(dt);
            }
            else
            {
                clsDAL oClsDAL = new clsDAL(false);
                ds = oClsDAL.GetMenuCategoryDetail(1, 0, Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
            }
            return UtilComman.GetJSONString(ds.Tables[0]);
        }
        catch (Exception ex)
        {

        }

        return calcDetails;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetSubMenuCategory(string CategoryId, string UserId, string ClientType, string GUID)
    {
        string calcDetails = "";
        DataSet ds = new DataSet();


        try
        {
            string[] ValidToken = null;

            ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            if (ValidToken[0] == "False")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AppName");
                dt.Columns.Add("AppTitle");
                dt.Rows.Add("False", ValidToken[1]);
                dt.TableName = "d";
                ds.Tables.Add(dt);
            }
            else
            {
                clsDAL oClsDAL = new clsDAL(false);
                if (CategoryId != "null")
                    //    ds = oClsDAL.GetSubMenCategoryDetail(null, Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
                    ds = oClsDAL.GetSubMenCategoryDetail(Convert.ToInt32(CategoryId.Replace('\"', ' ').Trim()), Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
                else
                    ds = oClsDAL.GetSubMenCategoryDetail(null, Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
            }
            return UtilComman.GetJSONString(ds.Tables[0]);
        }
        catch (Exception ex)
        {

        }

        return calcDetails;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetSubMenuCategoryVendorWise(string VendorId, string UserId, string ClientType, string GUID)
    {
        string calcDetails = "";
        DataSet ds = new DataSet();

        try
        {
            string[] ValidToken = null;

            ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            if (ValidToken[0] == "False")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("AppName");
                dt.Columns.Add("AppTitle");
                dt.Rows.Add("False", ValidToken[1]);
                dt.TableName = "d";
                ds.Tables.Add(dt);
            }
            else
            {
                clsDAL oClsDAL = new clsDAL(false);
                if (VendorId != "null")
                    //    ds = oClsDAL.GetSubMenCategoryDetail(null, Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
                    ds = oClsDAL.GetSubMenCategoryDetailVendorWise(Convert.ToInt32(VendorId.Replace('\"', ' ').Trim()), Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
                else
                    ds = oClsDAL.GetSubMenCategoryDetailVendorWise(null, Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
            }
            return UtilComman.GetJSONString(ds.Tables[0]);
        }
        catch (Exception ex)
        {

        }

        return calcDetails;
    }


    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string GetLastOpenAppName(string InstanceId, string ClientType, string GUID, string UserId)
    {
        string calcDetails = "";
        DataSet ds = new DataSet();


        try
        {
            string[] ValidToken = null;

            ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            if (ValidToken[0] == "False")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryId");
                dt.Columns.Add("Message");
                dt.Rows.Add("False", ValidToken[1]);
                dt.TableName = "d";
                ds.Tables.Add(dt);
            }
            else
            {
                clsDAL oClsDAL = new clsDAL(false);
                ds = oClsDAL.GetLastOpenAppNameDetail(InstanceId.Replace('\"', ' ').Trim());
            }
            return UtilComman.GetJSONString(ds.Tables[0]);
        }
        catch (Exception ex)
        {

        }
        return calcDetails;
    }

    //Excel

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public DataSet Excel_GetMainMenuCategory(string UserId, string ClientType, string GUID)
    {

        DataSet ds = null;
        try
        {
            string[] ValidToken = null;

            ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            if (ValidToken[0] == "False")
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CategoryId");
                dt.Columns.Add("CategoryName");
                dt.Rows.Add("False", ValidToken[1]);
                dt.TableName = "d";
                ds.Tables.Add(dt);
            }
            else
            {
                clsDAL oClsDAL = new clsDAL(false);
                ds = oClsDAL.GetMenuCategoryDetail(1, 0, Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
            }
        }
        catch (Exception ex)
        {

        }
        return ds;

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]

    public DataSet Excel_GetSubMenuCategory(string CategoryId, string UserId, string ClientType, string GUID)
    {
        DataSet ds = null;
        try
        {
            string[] ValidToken = null;
            ValidToken = ValidateAuthToken(Convert.ToString(UserId.Replace('\"', ' ').Trim()), ClientType.Replace('\"', ' ').Trim(), GUID.Replace('\"', ' ').Trim());
            if (ValidToken[0] != "False")
            {
                clsDAL oClsDAL = new clsDAL(false);
                ds = oClsDAL.GetSubMenCategoryDetail(Convert.ToInt32(CategoryId.Replace('\"', ' ').Trim()), Convert.ToInt64(UserId.Replace('\"', ' ').Trim()));
            }
        }
        catch (Exception ex)
        {
            string str = "CategoryId=" + CategoryId + ";UserId" + UserId + ";GUID" + GUID;
            LogUtility.Error("Service.asmx.cs", "Excel_GetSubMenuCategory", "Inner excp:" + ex.InnerException.ToString() + ";Parameter Value:" + str, ex);
        }
        return ds;
    }

    #endregion

    private void SendAutoUrlMail(DataTable tblUserList, string pUserName, string fromTm, string toDtTm, string SenderEmail, long pUserId, string SenderMessage)
    {
        try
        {
            string strSubject = "The BEAST Financial Framework - Shared AutoURL";

            string _customMessage = string.IsNullOrEmpty(SenderMessage.Trim()) ? "" : "<p><div style=\"border-top:1px dashed Gray;border-bottom:1px dashed Gray;padding:3px 0px;\">Note from " + pUserName + " : <br/> " + SenderMessage + " </div></p>";
            //_customMessage +

            string strMailBody = "<div style=\"color:navy;font:normal 12px verdana\"><p>Dear User,</p><p>A BEAST Calculator has been shared with you by our customer " + pUserName + ".</p>" +
                           "<p>You may access <strong>The BEAST Apps</strong> by clicking on the following URL. You may copy and paste this URL in your browser as well.</p>" +
                           "<p><a href=\"[AUTOURL]\">[AUTOURL]</a></p>" +
                           "<p>This URL is valid as follows:</p> " +
                           "<p>User: " + pUserName + "<br/>" +
                           "URL Valid for: " + Convert.ToDateTime(fromTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " GMT To " + Convert.ToDateTime(toDtTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " GMT</p>" +
                           "<p> <b>NOTE:</b><b><i>&nbsp;Please treat this URL confidential as this URL will give the recipient an access to your account.</i></b></p>" +
                           "<p>If you do not wish to receive these URLs, please let us know.</p>" +
                           "<p>Please contact us if you have any questions.<br/><br/></p>" +
                            UtilityHandler.VCM_MailAddress_In_Html +
                            "</div>";

            string tmplateBody = "";

            for (int i = 0; i < tblUserList.Rows.Count; i++)
            {
                tmplateBody = strMailBody.Replace("[AUTOURL]", Convert.ToString(tblUserList.Rows[i]["AutoURL"]));
                UtilityHandler.SendMail(Convert.ToString(tblUserList.Rows[i]["EmailId"]), "", "", strSubject, tmplateBody, false);
            }

            /*Summary mail to initiator*/

            bool _isImp = false;

            strSubject = "The BEAST Financial Framework - Calculator Shared";

            _customMessage = string.IsNullOrEmpty(SenderMessage.Trim()) ? "" : "<p><div style=\"border-top:1px dashed Gray;border-bottom:1px dashed Gray;padding:3px 0px;\">Your message : <br/> " + SenderMessage + " </div></p>";

            strMailBody = "<div style=\"color:navy;font:normal 12px verdana\"><p>Dear " + pUserName + ",</p>"
                + "<p>You have shared TheBeast calculator to your following " + (tblUserList.Rows.Count == 1 ? "contact" : "contacts") + ":</p>"
                + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5px\">";

            for (int i = 0; i < tblUserList.Rows.Count; i++)
            {
                if (!_isImp)
                    _isImp = UtilityHandler.bIsImportantMail(Convert.ToString(tblUserList.Rows[i]["EmailId"]));
                strMailBody += "<tr><td>" + tblUserList.Rows[i]["EmailId"] + "</td><td> - </td><td><a href=\"" + tblUserList.Rows[i]["AutoURL"] + "\">" + tblUserList.Rows[i]["AutoURL"] + "</a> " + "</td></tr>";
            }

            //_customMessage +
            strMailBody += "</table>"
                        + "<p>URL Valid for: " + Convert.ToDateTime(fromTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " GMT To " + Convert.ToDateTime(toDtTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " GMT</p>"
                        + "<p>Please contact us if you have any questions.</p><br/>"
                        + UtilityHandler.VCM_MailAddress_In_Html
                        + "</div>";

            //clsDAL oClsDAL = new clsDAL(false);
            //string eMailID_User = oClsDAL.GetEmailIDFromUserId(pUserId);
            /*Commented below call as the sender email id repeates in CC parameter*/
            //UtilityHandler.SendMail(SenderEmail, System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString() + "," + eMailID_User, "", strSubject, strMailBody);

            //Mail separation internaluser@thebeastapps.com            
            if (UtilityHandler.bIsImportantMail(SenderEmail) || _isImp)
                UtilityHandler.SendMail(SenderEmail, System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), "", strSubject, strMailBody, false);
            else
                UtilityHandler.SendMail(SenderEmail, System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString(), "", strSubject, strMailBody, true);

        }
        catch (Exception ex)
        {
            LogUtility.Error("Service.asmx,cs", "SendAutoUrlMail()", ex.Message, ex);
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string SetDirImgSIDNull()
    {
        AppsInfo.Instance._dirImgSID = null;
        return "";
    }

    #region Token

    //[WebMethod]
    //[ScriptMethod(UseHttpGet = true)]
    public string getAuthToken(string UserID, string ClientType)
    {
        string AuthToken = "";
        try
        {
            AuthToken = AddTokenInDir(UserID, ClientType);

        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: getAuthToken() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "getAuthToken()" + ex.Message.ToString());
        }
        return AuthToken;
    }

    public string AddTokenInDir(string UserID, string ClientTypeWithVersion)
    {

        string AuthToken = "";

        try
        {
            string Excelversion = "";
            string ClientType = ClientTypeWithVersion;

            if (ClientTypeWithVersion.Contains("~"))
            {
                ClientType = Convert.ToString(ClientTypeWithVersion.Split('~')[0]);
                Excelversion = Convert.ToString(ClientTypeWithVersion.Split('~')[1]);
            }

            AuthToken = Guid.NewGuid().ToString();
            //openf2 ws = new openf2();
            //clsDAL objDAL = new clsDAL(false);

            //string UserName = "";
            //string groupid = "";

            //if (!string.IsNullOrEmpty(UserID.Trim()))
            //{
            //    if (UserID.Trim().Contains("@"))
            //    {
            //        UserName = UserID;
            //    }
            //    else
            //    {
            //        DataSet ds = GetUserGroups(Convert.ToInt32(UserID));

            //        if (ds != null && ds.Tables[0].Rows.Count > 0)
            //        {
            //            groupid = Convert.ToString(ds.Tables[0].Rows[0]["GroupID"]);
            //        }

            //        UserName = objDAL.GetEmailIDFromUserId(Convert.ToInt32(UserID));
            //    }
            //}

            //if (UserName.Contains("#"))
            //{
            //    UserName = UserName.Split('#')[0];
            //}

            //if (OpenBeast.Utilities.AuthenticationToken.Instance == null)
            //{
            //    OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken = new Dictionary<string, OpenBeast.Utilities.dirAuthToken>();
            //}

            //DateTime dateTimeSevenPM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0);

            //DateTime currentTime = DateTime.Now;
            //OpenBeast.Utilities.UserDetail objUserDetail = new OpenBeast.Utilities.UserDetail();

            //System.TimeSpan timeDiff = dateTimeSevenPM.Subtract(currentTime);

            //if (timeDiff.Hours >= 0 && timeDiff.Minutes >= 0)
            //{
            //    objUserDetail.LastAuthTime = dateTimeSevenPM;
            //}
            //else
            //{
            //    DateTime authTokenNextExpiryTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 0, 0);
            //    authTokenNextExpiryTime = authTokenNextExpiryTime.AddDays(1);
            //    objUserDetail.LastAuthTime = authTokenNextExpiryTime;
            //}
            //string Ipaddress = "";

            //objUserDetail.ClientType = ClientType;
            //objUserDetail.GUid = AuthToken;
            //objUserDetail.Login = System.DateTime.Now;
            //objUserDetail.IPAddres = UtilityHandler.Get_IPAddress(HttpContext.Current.Request.UserHostAddress);
            //Ipaddress = objUserDetail.IPAddres;
            //objUserDetail.UserName = UserName;
            //DataSet dsGeoIP = new DataSet();
            //dsGeoIP = ws.GetAutoURLGeoIPInfo(objUserDetail.IPAddres);
            //string city = "";
            //string org = "";
            //string Country = "";

            //if (dsGeoIP != null && dsGeoIP.Tables[0].Rows.Count > 0)
            //{
            //    city = dsGeoIP.Tables[0].Rows[0][4].ToString();
            //    org = dsGeoIP.Tables[0].Rows[0][2].ToString();
            //    Country = dsGeoIP.Tables[0].Rows[0][3].ToString();

            //    objUserDetail.City = dsGeoIP.Tables[0].Rows[0][4].ToString();
            //    objUserDetail.Org = dsGeoIP.Tables[0].Rows[0][2].ToString();
            //    objUserDetail.Country = dsGeoIP.Tables[0].Rows[0][3].ToString();
            //}

            //OpenBeast.Utilities.dirAuthToken objdirAuthToken = new OpenBeast.Utilities.dirAuthToken();
            //List<OpenBeast.Utilities.UserDetail> ListUserDetail = new List<OpenBeast.Utilities.UserDetail>();

            //objdirAuthToken.UserDetails = ListUserDetail;
            //objdirAuthToken.UserDetails.Add(objUserDetail);
            //objdirAuthToken.UserID = UserID;

            //if (OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken.ContainsKey(UserID))
            //{
            //    OpenBeast.Utilities.dirAuthToken ExistGUid = OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken[UserID];
            //    ExistGUid.UserDetails.Add(objUserDetail);
            //}
            //else
            //{
            //    OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken.Add(UserID, objdirAuthToken);
            //}

            //if (!UserID.Contains("@"))
            //{
            //    LogUtility.Info("Service.asmx.cs", "AddTokenInDir()", "$$UserID:" + UserID + ":" + (int)OpenBeast.Utilities.SysLogEnum.ADDAUTHENTICATIONTOKEN + " $$Token:" + AuthToken + "$$ClientType:" + ClientType + "$$Log-InTime:" + System.DateTime.Now + " $$UserName:" + UserName + " $$VendorId=" + groupid + "  $$ExcelVersion:" + Excelversion + " $$");
            //}
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: AddTokenInDir() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "AddTokenInDir()" + ex.Message.ToString());
        }
        return AuthToken;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string[] ValidateAuthToken(string UserID, string ClientType, string GUID)
    {
        string[] TokenMessage = { "True", "" };

        //try
        //{
        //    if (OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken != null)
        //    {
        //        LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "dirAuthToken is not null" + " GUID:" + GUID);

        //        if (OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken.ContainsKey(UserID) == true)
        //        {
        //            LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "UserID exist" + " GUID:" + GUID);
        //            OpenBeast.Utilities.dirAuthToken ExistGUid = OpenBeast.Utilities.AuthenticationToken.Instance._dirAuthToken[UserID];

        //            if (ExistGUid != null)
        //            {
        //                LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "Token exist" + " GUID:" + GUID);

        //                var ExistToken = ExistGUid.UserDetails.Where(x => x.GUid == GUID).FirstOrDefault();

        //                if (ExistToken != null)
        //                {

        //                    DateTime currentTime = DateTime.Now;

        //                    System.TimeSpan timeDiff = ExistToken.LastAuthTime.Subtract(currentTime);

        //                    if (timeDiff.Hours >= 0 && timeDiff.Minutes >= 0)
        //                    {

        //                        LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "Validate True" + " GUID:" + GUID);

        //                        TokenMessage[0] = "True";
        //                    }
        //                    else
        //                    {
        //                        LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "LastAuthenticationTime expired" + " GUID:" + GUID);

        //                        TokenMessage[1] = "Your session has expired. Please relogin, LastAuthenticationTime:" + ExistToken.LastAuthTime.ToString();
        //                        VCM_Mail objVCM_Mail = new VCM_Mail();
        //                        objVCM_Mail.SendMailForSessionExpired(UserID, ClientType, GUID, "Session Expired due to daily pool recycle at 7.00 p.m.");
        //                        LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + ":" + (int)OpenBeast.Utilities.SysLogEnum.REMOVEAUTHENTICATIONTOKEN + " $$Token:" + GUID + "$$ClientType:" + ClientType + "$$Log-OutTime:" + System.DateTime.Now);

        //                    }
        //                }
        //                else
        //                {
        //                    LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "session has expired" + " GUID:" + GUID);

        //                    TokenMessage[1] = "Your session has expired. Please relogin";
        //                    VCM_Mail objVCM_Mail = new VCM_Mail();
        //                    objVCM_Mail.SendMailForSessionExpired(UserID, ClientType, GUID, "Token does not exist.");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "Userid Does not exist" + " GUID:" + GUID);

        //            TokenMessage[1] = "Your session has expired. Please relogin";
        //            VCM_Mail objVCM_Mail = new VCM_Mail();
        //            objVCM_Mail.SendMailForSessionExpired(UserID, ClientType, GUID, "UserId not found in Token directory.");
        //        }
        //    }
        //    else
        //    {
        //        LogUtility.Info("Service.asmx.cs", "ValidateAuthToken()", "$$UserID:" + UserID + "$$ClientType:" + ClientType + "$$Time:" + System.DateTime.Now + "dirAuthToken is null" + " GUID:" + GUID);

        //        TokenMessage[1] = "Authentication list is empty";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    UtilityHandler.SendEmailForError("service.asmx :: ValidateAuthToken() :: " + ex.Message.ToString() + "UserID:" + UserID + "ClientType:" + ClientType + "GUID:" + GUID);
        //    TokenMessage[1] = ex.Message != null ? ex.Message : "Error" + "Authentication list is empty";
        //    LogUtility.Error("Service.asmx.cs", "ValidateAuthToken()" + ex.Message.ToString());
        //}
        return TokenMessage;
    }

   
   
  
 
    #endregion

    #region UrlAdminNew calls

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public DataSet SharedAutoURL_Validate(string RefID, string ClientType)
    {
        DataSet dst = new DataSet();
        string AuthToken = "";
        try
        {
            //clsDAL objDAL = new clsDAL(false);
            openf2 ws = new openf2();
            dst = ws.BeastApps_SharedAutoURL_Validate(RefID);

            if (dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {

                AuthToken = AddTokenInDir(Convert.ToString(dst.Tables[0].Rows[0]["EmailId"]), ClientType);

                dst.Tables[0].Columns.Add("AuthToken");
                dst.Tables[0].Rows[0]["AuthToken"] = AuthToken;
                // objDAL.SubmitUserTokenSetail(0, Convert.ToString(dst.Tables[0].Rows[0]["EmailId"]), AuthToken, "N", "N");
                LogUtility.Info("Service.asmx.cs", "SharedAutoURL_Validate()", "$$UserID:" + Convert.ToString(dst.Tables[0].Rows[0]["InitiatorUserId"]) + " :" + (int)OpenBeast.Utilities.SysLogEnum.SHAREDTOUSERAUTHENTICATED + " $$Token:" + AuthToken + " $$SharedToUser:" + Convert.ToString(dst.Tables[0].Rows[0]["EmailId"]) + " $$ClientType:" + ClientType + " $$Log-InTime:" + System.DateTime.Now + " $$");


            }
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: SharedAutoURL_Validate() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "SharedAutoURL_Validate()" + ex.Message.ToString());
        }

        return dst;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public void BeastApps_SharedAutoURL_UpdateClickCount(string pRefId)
    {
        try
        {
            openf2 wsObj = new openf2();
            wsObj.BeastApps_SharedAutoURL_UpdateClickCount(pRefId);
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: BeastApps_SharedAutoURL_UpdateClickCount() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "BeastApps_SharedAutoURL_UpdateClickCount()" + ex.Message.ToString());
        }
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string[] ValidateUser(string strUserName, string strPass, string ClientType)
    {
        string[] userInfo = null;
        openf2 objadmin = new openf2();
        bool isValid = false;
        string AuthToken = "";
        string AuthTicket = "";
        string userdtl = "";

        try
        {
            userInfo = objadmin.AuthenticateUser(strUserName.Trim(), strPass.Trim());
            isValid = Convert.ToBoolean(userInfo[0]);

            if (isValid == true)
            {
                AuthToken = getAuthToken(userInfo[1].ToString().Split('#')[0], ClientType);

                int actLength = 12;
                string[] _tempElements = new string[actLength];
                string[] _tempUserdtl = userInfo[1].Split('#');
                int iLength = userInfo[1].Split('#').Length;

                for (int i = 0; i < iLength; i++)
                {
                    _tempElements[i] = _tempUserdtl[i];
                }

                _tempElements[9] = AuthToken;
                _tempElements[10] = "";
                _tempElements[11] = "";

                //Save if client is launcher
                if (ClientType.ToLower() == "launcher")
                {
                    clsDAL objCls = new clsDAL(true);

                    //Generate new token and save to db
                    AuthTicket = Guid.NewGuid().ToString();
                    objCls.Launcher_SubmitAuthTicket(_tempElements[0], AuthTicket, DateTime.Now, DateTime.Now.AddDays(5));

                    _tempElements[10] = AuthTicket;
                }

                for (int i = 0; i < actLength; i++)
                {
                    userdtl += _tempElements[i] + "#";
                }

                //Remove last #. Important! Affects all clients : web/excel/launcher
                userdtl = userdtl.Substring(0, userdtl.LastIndexOf('#') - 1);

                //userdtl = userInfo[1] + "#" + AuthToken;
                userInfo[1] = userdtl;
            }

            if (ClientType.Contains("~"))
            {
                ClientType = Convert.ToString(ClientType.Split('~')[0]);
            }

            if (ClientType.Trim().ToLower() != "web")
            {
                VCM_Mail objVCM_Mail = new VCM_Mail();
                objVCM_Mail.SendMailForLoginNotification(userInfo, strUserName, ClientType);
                objVCM_Mail = null;
            }
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: ValidateUser() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "ValidateUser()" + ex.Message.ToString());
        }

        return userInfo;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string[] ValidateUser_New_UM(string strUserName, string strPass, string ClientType, string strAspSesionId, Int32 ssId)
    {
        string[] userInfo = null;
        openf2 objadmin = new openf2();
        bool isValid = false;
        string AuthToken = "";
        string userdtl = "";
        string sessionid = "";
        try
        {
            sessionid = Guid.NewGuid().ToString();
            userInfo = objadmin.ValidateUser_New_UM(strUserName.Trim(), strPass.Trim(), sessionid, ssId);
            isValid = Convert.ToBoolean(userInfo[0]);

            if (isValid == true)
            {
                AuthToken = getAuthToken(userInfo[1].ToString().Split('#')[0], ClientType);
                userdtl = userInfo[1] + "#" + AuthToken;
                userInfo[1] = userdtl;
            }

            if (ClientType.Trim().ToLower() != "web")
            {
                VCM_Mail objVCM_Mail = new VCM_Mail();
                objVCM_Mail.SendMailForLoginNotification(userInfo, strUserName, ClientType);
                objVCM_Mail = null;
            }
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: ValidateUser() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "ValidateUser()" + ex.Message.ToString());
        }

        return userInfo;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public DataSet AutoURL_Validate_User_Info(string refNo, string UserHostAddress, int ApplicationCode, string ClientType)
    {
        DataSet dst = new DataSet();
        string AuthToken = "";
        try
        {
            // clsDAL objDAL = new clsDAL(false);
            openf2 ws = new openf2();
            dst = ws.VCM_AutoURL_Validate_User_Info(refNo, UserHostAddress, ApplicationCode);

            if (dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                AuthToken = AddTokenInDir(Convert.ToString(dst.Tables[0].Rows[0]["UserId"]), ClientType);
                dst.Tables[0].Columns.Add("AuthToken");
                dst.Tables[0].Rows[0]["AuthToken"] = AuthToken;

            }
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: SharedAutoURL_Validate() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "SharedAutoURL_Validate()" + ex.Message.ToString());
        }

        return dst;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public bool ChangePassword(Int64 lUserId, string oldPassword, string newPassword)
    {
        bool retVal = false;
        try
        {
            openf2 ws = new openf2();
            retVal = ws.ChangePassword(lUserId, oldPassword, newPassword);
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: ChangePassword() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "ChangePassword()" + ex.Message.ToString());
        }

        return retVal;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public bool LogoutUserForcefully(string UserId)
    {
        bool retVal = false;
        try
        {
            DAL.clsDAL objDAl = new clsDAL(false);
            DataSet ds = new DataSet();

            ds = objDAl.GeWeActiveLoginsDtl(UserId);

            for (int index = 0; index < ds.Tables[0].Rows.Count; index++)
            {
                objDAl.SubmitWebActiveLogins(UserId, Convert.ToString(ds.Tables[0].Rows[index]["SessionID"]), "", "", "");
                //   retVal= DisableToken(Convert.ToString(ds.Tables[0].Rows[index]["SessionID"]), UserId, Convert.ToString(ds.Tables[0].Rows[index]["ClientType"]));
                VCMComet.Instance.Send_Message_To_Client_Connection_Generic(Convert.ToString(ds.Tables[0].Rows[index]["ConnectionId"]), "alrt", "m", "eleID", "Logout", "ForceLogout");
            }

        }
        catch (Exception ex)
        {

            LogUtility.Error("Service.asmx.cs", "LogoutUserForcefully()" + ex.Message.ToString());
        }

        return retVal;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string CheckLoginPolicy(string UserId, string ClientType)
    {
        string AuthToken = "";
        try
        {
            DAL.clsDAL objDAl = new clsDAL(false);
            DataSet ds = new DataSet();
            ds = objDAl.GetVALIDATEUSERLOGIN(UserId);

            //if (Convert.ToString(ds.Tables[0].Rows[0][0]) == "-7")
            //{
            LogoutUserForcefully(UserId);
            AuthToken = Guid.NewGuid().ToString();
            objDAl.SubmitValidateLogin(UserId, AuthToken);
            AuthToken = AddTokenInDir(UserId, ClientType);

            //}

        }
        catch (Exception ex)
        {

            LogUtility.Error("Service.asmx.cs", "CheckLoginPolicy()" + ex.Message.ToString());
        }

        return AuthToken;
    }

    #endregion

    #region Excel

    [WebMethod]
    public DataSet Excel_GetXml(string CalcID, string LastUpdatedDate)
    {
        DataSet dsxml = new DataSet();
        DAL.clsDAL OBJdal = new DAL.clsDAL(false);
        dsxml = OBJdal.GetXML(CalcID, LastUpdatedDate);
        return dsxml;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public ExeChunk GetLatestVersionID(string UserID, string ObjectID)
    {
        try
        {
            clsDAL oClsDAL = new clsDAL(false);
            return oClsDAL.GetLatestVersionID(ObjectID);
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: GetLatestVersionID() :: " + ex.Message.ToString());
            ExeChunk volEntityObj = new ExeChunk();
            return volEntityObj;
        }

    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public List<ExeChunk> GetLatestVersionSetup(string UserID, string ObjectID)
    {
        try
        {
            clsDAL oClsDAL = new clsDAL(false);
            return oClsDAL.GetLatestVersionSetup(ObjectID);
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: GetLatestVersionSetup() :: " + ex.Message.ToString());
            List<ExeChunk> volEntityObj = new List<ExeChunk>();
            return volEntityObj;
        }
    }

    [WebMethod]
    public void NotifyDownload(string pExcelVersion, string pClientIp, string pInstallationInfoHtml)
    {
        try
        {
            clsDAL _obj = new clsDAL(true);
            _obj.SendExcelDownloadNotification(pExcelVersion, pClientIp, pInstallationInfoHtml);
            _obj = null;
        }
        catch (Exception ex)
        {
            LogUtility.Error("Service.asmx.cs", "NotifyDownload()", ex.Message, ex);
        }
    }

    #endregion

    //Added by cpkabra   
    [WebMethod(EnableSession = true)]
    public DataSet GetUserGroups(long UserId)
    {
        DataSet ds = new DataSet();
        try
        {
            clsDAL oClsDAL = new clsDAL(false);
            return ds = oClsDAL.GetUserGroups(UserId);
        }
        catch (Exception ex)
        {
            return ds;
        }

    }

    //Ended

    //TruMid AutoUrl
    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string AutoURL_Validate(string userid, string ClientType)
    {
        string AuthToken = "";
        try
        {
            return AuthToken = AddTokenInDir(userid, ClientType);
        }
        catch (Exception ex)
        {
            LogUtility.Error("Service.asmx.cs", "SharedAutoURL_Validate()" + ex.Message.ToString());
        }

        return AuthToken;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public int AutoLoginByAuthTicket(string strUserId, string strAuthToken, string ClientType)
    {
        int iRetVal = 0;    // 1 = Valid, Load respective swarm directly | 0 = Invalid, Show login page
        // ClientType : Not used now. Kept for future scope
        try
        {
            clsDAL objDAL = new clsDAL(true);
            string _result = objDAL.Launcher_GetAuthTicket(strUserId, strAuthToken);

            switch (_result.Split('#')[0])
            {
                case "1":
                    iRetVal = 1;
                    break;

                case "0":
                    // Token Expired
                    break;

                case "-1":
                    // User not found
                    break;

                case "-2":
                    // Invalid token/ Token not found
                    break;

            }
        }
        catch (Exception ex)
        {
            UtilityHandler.SendEmailForError("service.asmx :: AutoLoginByAuthToken() :: " + ex.Message.ToString());
            LogUtility.Error("Service.asmx.cs", "AutoLoginByAuthToken()" + ex.Message.ToString());
        }
        return iRetVal;
    }
}