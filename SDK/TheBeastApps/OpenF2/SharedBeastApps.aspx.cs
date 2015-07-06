using System;
using System.Data;
using System.Web;
using System.Net;
using System.Web.UI;
using VCM.Common.Log;
using OpenF2.OpenBeastService;
using System.Web.Services.Protocols;

namespace OpenF2
{
    public partial class SharedBeastApps : System.Web.UI.Page
    {
        string AutoURL;
        DAL objadmin = null;
        int MsgID;
        protected void Page_Load(object sender, EventArgs e)
        {
            LogUtility.Debug("SharedBeastApps.aspx", "Page_Load", ": Page_Load event fired");

            Response.Cache.SetCacheability(HttpCacheability.NoCache);


            if (!IsPostBack)
            {
                LogUtility.Debug("SharedBeastApps.aspx", "Page_Load", ": before ValidateRequest");

                //ValidateRequestFromAutoURL();
                ValidateRequest();

            }
        }

        private void ValidateRequest()
        {
            hdn_SignalRSetup.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SignalRSetup"]);                
            
            objadmin = new DAL();
            string IPAddress = UtilityHandler.Get_IPAddress(Request.UserHostAddress);
            int IntiatorId = 0;
            DateTime startDate = DateTime.UtcNow;
            DataSet dsGeoIP = new DataSet();
            dsGeoIP = objadmin.VCM_AutoURL_GeoIP_Info(IPAddress);
            string City = "";
            string Org = "";
            string Country = "";
            string product = "";
            string vInstanceInfo = "", vStartDate = "", vEndDate = "", strInitiatorName = "", vInstanceId = "", vInitiatorEmailId = "";
            if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
            {
                City = dsGeoIP.Tables[0].Rows[0][4].ToString();
                Org = dsGeoIP.Tables[0].Rows[0][2].ToString();
                Country = dsGeoIP.Tables[0].Rows[0][3].ToString();
            }

            string strBrows = Request.Browser.Version.ToString();
            DataSet dst = new DataSet();
            Session.Clear();
            AutoURL = Request.Url.ToString().Replace(" ", "%20");
            string InitiatorName = "";
            bool bIsValidate = false;
            bool _isImp = false;
            string RedirectTo = string.Empty;

            string MsgBody = string.Empty;
            string Subject = string.Empty;
            string LoginID = string.Empty;

            string vEmail = string.Empty;
            int iUrlExpiredFlag = 0;

            try
            {
                if (Request.QueryString["id"] != null)
                {
                    objadmin = new DAL();
                    objadmin.BeastApps_SharedAutoURL_UpdateClickCount(Request.QueryString["id"].Trim());
                    LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", ": Create Object of BeastAppsCore Webservice");
                    OpenBeastService.Service ws = new OpenBeastService.Service();
                    ////Set Cookie
                    LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", ": GetCookie() function called");
                    if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
                    {
                        Cookie AmazonCookie = UtilityHandler.GetCookies();

                        LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", ": SetCookiesForAmazon() function called");

                        UtilityHandler.SetCookiesForAmazon("SharedBeastApps.aspx", ws, AmazonCookie);
                    }

                    dst = ws.SharedAutoURL_Validate(Request.QueryString["id"], "Web");

                    LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", "Token Created");

                    string strIpAddr = UtilityHandler.Get_IPAddress(Request.UserHostAddress);

                    if (dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", "dst count > 0");

                        MsgID = Convert.ToInt32(dst.Tables[0].Rows[0]["MsgId"]);

                        long vInitiatiorUserId = Convert.ToInt64(dst.Tables[0].Rows[0]["InitiatorUserId"]);
                        vInitiatorEmailId = Convert.ToString(dst.Tables[0].Rows[0]["InitiatorEmailId"]);
                        vInstanceId = Convert.ToString(dst.Tables[0].Rows[0]["InstanceId"]);
                        vInstanceInfo = Convert.ToString(dst.Tables[0].Rows[0]["InstanceInfo"]);
                        vStartDate = Convert.ToString(dst.Tables[0].Rows[0]["StartDate"]);
                        vEndDate = Convert.ToString(dst.Tables[0].Rows[0]["EndDate"]);
                        vEmail = Convert.ToString(dst.Tables[0].Rows[0]["EmailId"]);
                        RedirectTo = Convert.ToString(dst.Tables[0].Rows[0]["MovetoPage"]);
                        AutoURL = Convert.ToString(dst.Tables[0].Rows[0]["AutoURL"]);
                        hdn_AuthToken.Value = Convert.ToString(dst.Tables[0].Rows[0]["AuthToken"]);
                        IntiatorId = Convert.ToInt32(dst.Tables[0].Rows[0]["InitiatorUserId"]);
                        InitiatorName = dst.Tables[0].Rows[0]["InitiatorName"].ToString();
                        startDate = Convert.ToDateTime(dst.Tables[0].Rows[0]["StartDate"]);
                        string[] instancedetails = vInstanceInfo.Split('#');
                        product = instancedetails[2];
                        _isImp = false;
                        if (UtilityHandler.bIsImportantMail(vInitiatorEmailId) || UtilityHandler.bIsImportantMail(vEmail))
                            _isImp = true;
                        else
                            _isImp = false;

                        strInitiatorName = objadmin.GetUserCustomerDetails(vInitiatiorUserId).Split('#')[0];

                        hdn_ClientType.Value = "Web";

                        Session["URL_EXP"] = dst.Tables[0];

                        if (MsgID != 0 && MsgID != -9)
                        {
                            LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", "MsgID!=0");

                            // ............fail..............

                            switch (MsgID)
                            {
                                case -2:
                                    Subject = "Failed to login for AutoURL";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login for AutoURL, MsgID=-2");
                                    break;

                                case -6:
                                    Subject = "Failed to login because of no rights for the Session";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login because of no rights for the Session, MsgID=-6");
                                    break;

                                case -5:
                                    Subject = "Failed to login because of Unauthorized IP Address";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login because of Unauthorized IP Address, MsgID=-5");
                                    break;

                                case -3:
                                    Subject = "Failed to login with the AutoURL";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login with the AutoURL, MsgID=-3");
                                    break;

                                case -1:
                                    Subject = "Failed to login with the AutoURL as User is either not registered or blocked";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login with the AutoURL as User is either not registered or blocked, MsgID=-1");
                                    break;

                                case -9:
                                    Subject = "Failed to login with the AutoURL as URL Expired";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login with the AutoURL as URL Expired, MsgID=-9");

                                    iUrlExpiredFlag = 1;
                                    break;

                                case -10:
                                    Subject = "Failed to login with the AutoURL as the initiator has closed the calculator";
                                    LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login with the AutoURL as the initiator has closed the calculator, MsgID=-10");

                                    iUrlExpiredFlag = 1;
                                    break;
                            }

                            // .........................

                            //MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<br/><br/>"
                            //            + "User " + vEmail + " failed to open the shared calculator AutoURL mentioned below:&nbsp;"
                            //            + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                            //            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                            //            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Shared By: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + strInitiatorName + " (" + vInitiatorEmailId + ") </td></tr>"
                            //            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">IP Address: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + IPAddress + " </td></tr>"
                            //            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Organization: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Org + " </td></tr>"
                            //            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">City: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + City + " </td></tr>"
                            //            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Country: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Country + " </td></tr>"
                            //            + "</table>"
                            //            + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                            //            + "</div>";

                            //if (_isImp)
                            //{
                            //    SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                            //        , System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                            //        , "Shared BeastCalc -" + Subject
                            //        , MsgBody, false);
                            //}
                            //else
                            //{
                            //    SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                            //        , System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString()
                            //        , "Shared BeastCalc -" + Subject
                            //        , MsgBody, false);
                            //}
                            // ..........................

                            //System.Threading.Thread.Sleep(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["emailDelay"]) * 1000);

                            // ........E mail to Initiator..............                          

                            MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear " + strInitiatorName + ",<br/><br/>"
                                        + "User " + vEmail + " failed to open the BEAST Calculator shared by you."
                                        + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Auto URL:</td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\"> " + AutoURL + "</td></tr>"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\" valign=\"top\">Validity:</td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\"> " + Convert.ToDateTime(vStartDate).ToString("dd-MMM-yyyy HH:mm:ss tt") + " to <br/> " + Convert.ToDateTime(vEndDate).ToString("dd-MMM-yyyy HH:mm:ss tt") + "  </td></tr>"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">IP Address: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + IPAddress + " </td></tr>"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Organization: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Org + " </td></tr>"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">City: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + City + " </td></tr>"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Country: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Country + " </td></tr>"
                                        + "</table>"
                                        + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                        + "</div>";

                            LogUtility.Info("SharedBeastApps", "ValidateRequest()", "Email to initiator(id=" + vInitiatorEmailId + ").Case=FAIL.UseMirror=NO");

                            if (_isImp)
                            {
                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].Trim()
                                   , vInitiatorEmailId
                                   , System.Configuration.ConfigurationManager.AppSettings["FromEmail"].Trim()
                                   , "Shared Beast Calc - " + Subject
                                   , MsgBody, false);
                            }
                            else
                            {
                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].Trim()
                                   , vInitiatorEmailId
                                   , System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].Trim()
                                   , "Shared Beast Calc - " + Subject
                                   , MsgBody, false);
                            }

                            // ........E mail to Initiator..............
                        }
                        else if (MsgID == -9)
                        {
                            string endTime = "60";
                            //    Subject = "Failed to login with the AutoURL as URL Expired";
                            string data = objadmin.submit_extend_validity(Request.QueryString["id"].Trim(), 60, 2);
                            LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Validoty extend, MsgID=-9");
                            if (data == "0")
                            {
                                vEndDate = Convert.ToString(DateTime.UtcNow.AddMinutes(60));
                                string productType = "calc";
                                objadmin.SaveAutoUrlAccessInfo("share", productType, product, AutoURL, "", IntiatorId, InitiatorName, startDate, IPAddress, vEmail, DateTime.UtcNow, "", City, Country, endTime, 1);
                                LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest", "Failed to login with the AutoURL as URL Expired, MsgID=-9");
                                //   Response.Write("<script>alert('Url you clicked was expired but extended automatically for 1 hour.')</script>");
                                hdnStst.Value = "1";
                                bIsValidate = true;
                            }
                            else
                            {
                                Subject = "Failed to login with the AutoURL as URL Expired";
                                MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear " + strInitiatorName + ",<br/><br/>"
                                       + "User " + vEmail + " failed to open the BEAST Calculator shared by you."
                                       + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                       + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Auto URL:</td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\"> " + AutoURL + "</td></tr>"
                                       + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\" valign=\"top\">Validity:</td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\"> " + Convert.ToDateTime(vStartDate).ToString("dd-MMM-yyyy HH:mm:ss tt") + " to <br/> " + Convert.ToDateTime(vEndDate).ToString("dd-MMM-yyyy HH:mm:ss tt") + "  </td></tr>"
                                       + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">IP Address: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + IPAddress + " </td></tr>"
                                       + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Organization: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Org + " </td></tr>"
                                       + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">City: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + City + " </td></tr>"
                                       + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">Country: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Country + " </td></tr>"
                                       + "</table>"
                                       + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                       + "</div>";

                                LogUtility.Info("SharedBeastApps", "ValidateRequest()", "Email to initiator(id=" + vInitiatorEmailId + ").Case=FAIL.UseMirror=NO");
                                string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                                string mailto = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                        , mailto
                                        , ""
                                        , "Shared Bease Calc - " + Subject
                                        , MsgBody, false);
                                bIsValidate = false;
                                iUrlExpiredFlag = 1;
                            }
                        }
                        else
                        {
                            // ..........success...............      

                            bIsValidate = true;
                        }
                    }

                    else
                    {
                        bIsValidate = false;

                        // .........fail due to changed refNo which is not found in db................

                        Subject = "Login failed";

                        MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<br/><br/>"
                                + "User " + vEmail + " Failed to login with the shared calculator AutoURL mentioned below:&nbsp;"
                                + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                                + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td></tr>"
                                + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Reason:</td> <td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;No records found for the url id (Ref. no.)</td></tr>"
                                + "</table>"
                                + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                + "</div>";

                        string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                        string mailto = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                , mailto
                                , ""
                                , "Shared Bease Calc - " + Subject
                                , MsgBody, false);

                        // ..........................
                    }


                }
                else
                {
                    //lblMessage.Text = "Invalid AutoURL";
                }
                if (bIsValidate)
                {
                    LogUtility.Debug("SharedBeastApps.aspx", "ValidateRequest", "bIsValidate=true");
                    string ConnectionIDSignalR = "0";
                    string ConnectionID = "0";
                    string UserID = "0";
                    string CustomerID = "0";
                    string UserMode = "0";
                    string SpecialImageID = "0";
                    string InstanceID = "";

                    string ProductID = "0";
                    string GroupName = "";

                    //2006148#100601#conn~d5b7fd85-daa2-414f-afb5-27736e677ae6~e76e03b5-ba14-4036-9f7e-c5acb8c8df17

                    string[] userInfoAry = vInstanceInfo.Split('#');

                    if (userInfoAry.Length > 0)
                    {
                        if (userInfoAry[2].Split('~').Length > 1)
                        {
                            UserMode = userInfoAry[2].Split('~')[0];
                            InstanceID = userInfoAry[2].Split('~')[1];
                            ConnectionIDSignalR = userInfoAry[2].Split('~')[2];
                        }
                        else
                        {
                            UserMode = userInfoAry[2];
                        }
                    }

                    if (UserMode.Trim().ToLower() == "spe")
                    {
                        SpecialImageID = "sp1";
                    }
                    else if (UserMode.Trim().ToLower() == "user")
                    {
                        UserID = userInfoAry[0];
                    }
                    else if (UserMode.Trim().ToLower() == "cust")
                    {
                        CustomerID = userInfoAry[1];
                    }
                    else if (UserMode.Trim().ToLower() == "conn")
                    {
                        ConnectionID = userInfoAry[2].Split('~')[2];
                    }

                    if (InstanceID == "vcm_calc_ExchangeDataMarketView")
                    { UserID = userInfoAry[0]; }
                    objadmin = new DAL();
                    string username = objadmin.GetEmailIDFromUserId(Convert.ToInt32(userInfoAry[0]));

                    hdn_userId.Value = UserID;
                    hdn_custId.Value = CustomerID;
                    hdn_instanceMode.Value = UserMode;
                    hdn_InstanceInfo.Value = userInfoAry[2].Split('~')[0] + "~" + vInstanceId + "~" + userInfoAry[2].Split('~')[2] + "~" + vEmail + "~" + username;

                    hdn_InstanceID.Value = vInstanceId;
                    hdn_instanceType.Value = userInfoAry[2].Split('~')[1];
                    hdn_description.Value = "Beast Apps";
                    hdn_sharedUserMailID.Value = vEmail;
                    hdnEmailId.Value = vEmail;
                    hdn_name.Value = "DefaultBeastApp";
                    Session["SharedUserMail"] = vEmail;
                    Session["SharedUserToken"] = hdn_AuthToken.Value;
                    hdnLogoutpath.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["LogoutPath"]);
                    //hdn_Service.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Services"]);
                    //hdn_Setup.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Setup"]);
                    //2006148#100601#conn~d5b7fd85-daa2-414f-afb5-27736e677ae6~e76e03b5-ba14-4036-9f7e-c5acb8c8df17

                    Subject = "Successful login";

                    //MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<br/><br/>"
                    //        + "User " + vEmail + " logged in with the AutoURL mentioned below:&nbsp;</br>"
                    //        + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                    //        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                    //        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address: </td><td width=\"80%\"style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + IPAddress + " </td></tr>"
                    //        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Shared By: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + strInitiatorName + " (" + vInitiatorEmailId + ") </td></tr>"
                    //        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Org + " </td></tr>"
                    //        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + City + " </td></tr>"
                    //        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Country + " </td></tr>"
                    //        + "</table>"
                    //        + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                    //        + "</div>";

                    //if (_isImp)
                    //    SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                    //            , System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                    //            , "Shared Beast Calc - " + Subject
                    //            , MsgBody, false);
                    //else
                    //    SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                    //            , System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString()
                    //            , "Shared Beast Calc - " + Subject
                    //            , MsgBody, false);

                    //System.Threading.Thread.Sleep(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["emailDelay"]) * 1000);

                    // ........E mail to Initiator..............                            

                    MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear " + strInitiatorName + ",<br/><br/>"
                            + "User " + vEmail + " has opened the BEAST Calculator successfully shared by you.<br/><br/>"
                            + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\" valign=\"top\">Validity:</td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\"> " + Convert.ToDateTime(vStartDate).ToString("dd-MMM-yyyy HH:mm:ss tt") + " to <br/> " + Convert.ToDateTime(vEndDate).ToString("dd-MMM-yyyy HH:mm:ss tt") + "  </td></tr>"
                            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + IPAddress + " </td></tr>"
                            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Org + " </td></tr>"
                            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + City + " </td></tr>"
                            + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country: </td><td width=\"80%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + Country + " </td></tr>"
                            + "</table>"
                            + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                            + "</div>";

                    LogUtility.Info("SharedBeastApps", "ValidateRequest()", "Email to initiator(id=" + vInitiatorEmailId + ").Case=SUCCESS.UseMirror=NO");

                    if (_isImp)
                    {
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                , vInitiatorEmailId
                                , System.Configuration.ConfigurationManager.AppSettings["FromEmail"].Trim()
                                , "Shared Beast Calc - " + Subject
                                , MsgBody, false);
                    }
                    else
                    {
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                               , vInitiatorEmailId
                               , System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].Trim()
                               , "Shared Beast Calc - " + Subject
                               , MsgBody, false);
                    }

                    // ........E mail to Initiator..............
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharedBeastApps.aspx", "ValidateRequest", "Browser ::" + HttpContext.Current.Request.UserAgent, ex);
                UtilityHandler.SendEmailForError("SharedBeastApps.aspx.cs :: ValidateRequest() :: Browser ::" + HttpContext.Current.Request.UserAgent + ex.Message.ToString());
            }

            if (iUrlExpiredFlag == 1) //if Url expired
            {
                LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest()", "Expired AutoUrl.RefNo:" + Request.QueryString["id"].Trim());
                Server.Transfer("UrlExpired.aspx");
            }
            else if (!bIsValidate)
            {
                LogUtility.Info("SharedBeastApps.aspx", "ValidateRequest()", "Invalid AutoUrl. Not found in DB.Redirecting to Login. RefNo:" + Request.QueryString["id"].Trim());
                Response.Redirect("Index.aspx");
            }

        }

        private void SendMail(string strFrom, string strTo, string strCC, string strSubject, string strBodyMsg, bool bFlag)
        {
            try
            {
                VCM_Mail _vcmMail = new VCM_Mail();

                strFrom = strFrom.ToLower();

                _vcmMail.From = strFrom;
                _vcmMail.To = strTo;

                if (!string.IsNullOrEmpty(strCC.Trim()))
                {
                    _vcmMail.CC = strCC;
                }

                _vcmMail.SendAsync = true;
                _vcmMail.Subject = strSubject;
                _vcmMail.Body = strBodyMsg;
                _vcmMail.IsBodyHtml = true;
                _vcmMail.SendMail();
                _vcmMail = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SharedBeastApps", "SendMail()", "Send mail error", ex);
            }
        }
    }
}