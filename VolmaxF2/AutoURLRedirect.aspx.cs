using System;
using System.Data;
using System.Web;
using VCM.Common.Log;
using VCM.Common;

public partial class AutoURLRedirect : System.Web.UI.Page
{
    string currentPage = "AutoURLRedirect";
    string Loc_des, sector, Mdate, AutoURL, logindt, clkcnt, validdt, username, IPAddress, MsgBody, RedirectTo, LoginId, BankName, SessionID;
    int userID;
    string ApplicationCode = Convert.ToString((int)AutoURLApplicationCode.ISWAP);
    int intcount = 0;

    private SessionInfo _session;
    public SessionInfo CurrentSession
    {
        get
        {
            if (_session == null)
            {
                _session = new SessionInfo(HttpContext.Current.Session);
            }
            return _session;
        }
        set
        {
            _session = value;
        }
    }
    string[] timerFlagValueAry;

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            ValidateRequest();
        }
    }

    protected void lnkbtnReload_Click(object sender, EventArgs e)
    {
        LogUtility.Info(currentPage, "lnkbtnReload_Click", "User clicked on ClickHere link to redirect.");
        ValidateRequest();
    }

    #endregion

    #region Function

    private void ValidateRequest()
    {
        DataSet dst = new DataSet();
        DataSet dsGeoIP = new DataSet();
        Session.Clear();
        AutoURL = Request.Url.ToString().Replace(" ", "%20");
        IPAddress = VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress);
        string mailto = System.Configuration.ConfigurationManager.AppSettings["Openf2Admin"].ToString();
        string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
        SessionInfo CurrentSession = new SessionInfo(HttpContext.Current.Session);
        dsGeoIP = vcmProductNamespace.cDbHandler.VCM_AutoURL_GeoIP_Info(IPAddress);
        string City = "";
        string Org = "";
        string Country = "";
        string ToEmailId = "";

        if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
        {
            City = dsGeoIP.Tables[0].Rows[0][4].ToString();
            Org = dsGeoIP.Tables[0].Rows[0][2].ToString();
            Country = dsGeoIP.Tables[0].Rows[0][3].ToString();
        }

        if (Request.QueryString["RefNo"] != null)
        {
            try
            {
                //Update click count
                vcmProductNamespace.cDbHandler.UpdatingClickCountVCMAutoURL(Request.QueryString["RefNo"], IPAddress);
            }
            catch (Exception ex)
            {
                LogUtility.Error("AutoURLRedirect.cs", "ValidateRequest()", "Null or modified RefNo", ex);
            }

            dst = vcmProductNamespace.cDbHandler.VCM_AutoURL_Validate_User_Info(Request.QueryString["RefNo"], IPAddress, Convert.ToInt32(ApplicationCode));
            try
            {
                if (dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                {
                    
                    int MsgID = Convert.ToInt32(dst.Tables[0].Rows[0]["MsgId"]);
                    if (MsgID == -2 || MsgID == 0 || MsgID == -5)
                    {
                        logindt = dst.Tables[0].Rows[0]["UTC_Record_CreateDateTime"].ToString();
                        clkcnt = dst.Tables[0].Rows[0]["clkcnt"].ToString();
                        validdt = dst.Tables[0].Rows[0]["validdt"].ToString();
                        username = dst.Tables[0].Rows[0]["Username"].ToString();
                        RedirectTo = dst.Tables[0].Rows[0]["MovetoPage"].ToString();
                        BankName = dst.Tables[0].Rows[0]["BankName"].ToString();
                        userID = Convert.ToInt32(dst.Tables[0].Rows[0]["UserID"]);
                        LoginId = dst.Tables[0].Rows[0]["LoginId"].ToString();
                        ToEmailId = dst.Tables[0].Rows[0]["ToEmailId"].ToString();
                    }

                    if (MsgID != 0)
                    {
                        if (MsgID == -2)
                        {
                            MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User named " + username + ", was <b>not able to login</b> for the Auto URL because <b>Url is expired</b>.&nbsp;" +
                                                       " <table><tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr> " +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">User:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + username + "</td></tr> " +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Login Id:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + LoginId + "</td></tr> " +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Org + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + City + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Country + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Login Date:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(logindt).ToString("dd-MMM-yyyy") + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Valid Till:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(validdt).ToString("dd-MMM-yyyy") + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Number of Time:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + clkcnt + "</td></tr></table>" +
                                                       "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7500</div>";

                            SendMail(FromEmail, ToEmailId, "The BEAST Financial Framework - User Failed to login with the AutoURL " + username + " clicked", MsgBody, UtilityHandler.bIsImportantMail(LoginId));
                            MsgBody = "";

                            Session["SessionDetails"] = BankName + "#" + username;
                            Response.Redirect("sto.aspx?RefNo=" + MsgID, false);
                            return;
                        }
                        else if (MsgID == -5)
                        {
                            MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User named " + username + ", was not able to login because of <b>Unauthorized IP Address</b>.&nbsp;" +
                                                       " <table><tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr> " +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">User:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + username + "</td></tr> " +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Login Id:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + LoginId + "</td></tr> " +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Org + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + City + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Country + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Login Date:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(logindt).ToString("dd-MMM-yyyy") + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Valid Till:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(validdt).ToString("dd-MMM-yyyy") + "</td>" +
                                                       "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Number of Time:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + clkcnt + "</td></tr></table>" +
                                                       "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7500</div>";

                            SendMail(FromEmail, ToEmailId, "The BEAST Financial Framework  - User Failed to login with the AutoURL", MsgBody, UtilityHandler.bIsImportantMail(LoginId));
                            MsgBody = "";

                            Response.Redirect("sto.aspx?RefNo=" + MsgID, false);
                            return;
                        }
                        else if (MsgID == -3)
                        {
                            MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User Failed to login with the AutoURL mentioned below becasue <b>URL (GUID) is not valid</b>.&nbsp;" +
                                                                   " <table><tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Org + "</td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + City + "</td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Country + "</td></tr>" +
                                                                   "</table><br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7500</div>";

                            SendMail(FromEmail, ToEmailId, "The BEAST Financial Framework - User Failed to login with the AutoURL", MsgBody, false);
                            MsgBody = "";

                            Response.Redirect("sto.aspx?RefNo=" + MsgID, false);
                            return;
                        }
                        else if (MsgID == -1)
                        {
                            MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User Failed to login with the AutoURL mentioned below because <b>User is invalid</b>.&nbsp;" +
                                                                   " <table><tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Org + "</td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + City + "</td></tr>" +
                                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Country + "</td></tr>" +
                                                                   "</table> <br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7500</div>";

                            SendMail(FromEmail, ToEmailId, "The BEAST Financial Framework - User Failed to login with the AutoURL", MsgBody, true);
                            MsgBody = "";

                            Response.Redirect("sto.aspx?RefNo=" + MsgID, false);
                            return;
                        }
                    }
                    else
                    {
                        // User Login Information 

                        MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User named " + username + ", has successfully logged in for the Auto URL mentioned below.&nbsp;<br/>" +
                                                   " <table><tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr> " +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">User:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + username + "</td></tr> " +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Login Id:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + LoginId + "</td></tr> " +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Page:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + RedirectTo + "</td></tr> " +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td>" +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Organization:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Org + "</td>" +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">City:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + City + "</td>" +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Country:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Country + "</td>" +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Login Date:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(logindt).ToString("dd-MMM-yyyy") + "</td>" +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Valid Till:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(validdt).ToString("dd-MMM-yyyy") + "</td>" +
                                                   "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Number of Time:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + clkcnt + "</td></tr></table>" +
                                                   "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7500</div>";
                        SendMail(FromEmail, ToEmailId, "The BEAST Financial Framework - AutoURL", MsgBody, UtilityHandler.bIsImportantMail(LoginId));
                        MsgBody = "";

                        /**/

                        CurrentSession.User = new SiteUserInfo(Convert.ToInt64(dst.Tables[0].Rows[0]["UserID"].ToString()), dst.Tables[0].Rows[0]["LoginID"].ToString(), dst.Tables[0].Rows[0]["LoginID"].ToString(), 1, -100, DateTime.Now.ToString(), Convert.ToString(Guid.NewGuid()), "", "");

                        //LoginTypeID remain - right now set -100
                        // For Currency 
                        CurrentSession.User.IPAddress = VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress);
                        CurrentSession.User.EmailID = dst.Tables[0].Rows[0]["LoginID"].ToString();
                        CurrentSession.User.LastActivityDate = DateTime.Now.ToString();
                        CurrentSession.User.UserName = dst.Tables[0].Rows[0]["Username"].ToString();
                        CurrentSession.User.ASPSessionID = System.Convert.ToString(Guid.NewGuid());
                        CurrentSession.User.ImpersonateUserID = Convert.ToInt64(dst.Tables[0].Rows[0]["UserID"].ToString());
                        CurrentSession.User.UserID = CurrentSession.User.ImpersonateUserID;
                        CurrentSession.User.ImpersonateCustomerId = CurrentSession.User.CustomerId;
                        CurrentSession.User.ImpersonateEmailID = dst.Tables[0].Rows[0]["LoginID"].ToString();
                        CurrentSession.User.BankName = BankName;
                        /**/
                        if (CurrentSession.User.SessionLogincount == null)
                        {
                            CurrentSession.User.SessionLogincount = Convert.ToString(vcmProductNamespace.cDbHandler.GetSessionCount(Convert.ToInt32(SessionID), Convert.ToInt32(CurrentSession.User.ImpersonateUserID)));
                            VcmLogManager.Log.SendUserLoginNotification(Convert.ToString(CurrentSession.User.UserID), Convert.ToString(CurrentSession.User.UserName), 1, -1, "Clicked on Auto URL Link", "Auto URL", VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress), "0", Convert.ToString(CurrentSession.User.ASPSessionID), Convert.ToString(CurrentSession.User.SessionLogincount));
                        }

                        string _url = System.Configuration.ConfigurationManager.AppSettings["BeastAppsRedirectUrl"].ToString() + Convert.ToString(Request.QueryString["RefNo"]);
                        Response.Redirect(_url, false);
                    }
                }
                else
                {
                    MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User Failed to login with the AutoURL mentioned below because <b>URL(GUID) is incorrect</b>.&nbsp;" +
                                                                                     " <table><tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>" +
                                                                                     "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td></tr></table>" +
                                                                                     "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7500</div>";
                    //Session["EnableEmail"] = System.Configuration.ConfigurationManager.AppSettings["EnableEmail"].ToString();
                    SendMail(FromEmail, mailto, "The BEAST Financial Framework  - User Failed to login with the AutoURL", MsgBody, true);
                    MsgBody = "";

                    Response.Redirect("sto.aspx?RefNo=999992", false);
                    return;
                }
            }
            catch (Exception ex)
            {
                VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "AutoURLPage", "Page_Load()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
                LogUtility.Error("AutoURLPage", "Page_Load", ex.Message, ex);
            }
            finally
            {

                AutoURL = null;
                logindt = null;
                clkcnt = null;
                validdt = null;
                username = null;
                IPAddress = null;
                RedirectTo = null;
                ToEmailId = null;
            }
        }
        else
        {
            MsgBody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> User Failed to login with the AutoURL mentioned below because <b>URL (Querystring Name) is invalid</b>.&nbsp;" +
                                                                              " <table><tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>" +
                                                                              "<tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td></tr></table>" +
                                                                              "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
            SendMail(FromEmail, mailto, "The BEAST Financial Framework - User Failed to login with the AutoURL", MsgBody, true);
            MsgBody = "";
            Response.Redirect("sto.aspx?RefNo=", false);
            return;
        }
    }

    private void SendMail(string strFrom, string strTo, string strSubject, string strBodyMsg, bool bFlag)
    {
        try
        {
            VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();
            if (strTo == string.Empty) strTo = System.Configuration.ConfigurationManager.AppSettings["Openf2Admin"].ToString();
            _vcmMail.From = strFrom;
            _vcmMail.To = strTo;

            if (bFlag || UtilityHandler.bIsImportantMail(strTo) || strTo.Trim().ToLower() == System.Configuration.ConfigurationManager.AppSettings["Openf2Admin"].Trim().ToLower())
            {
                _vcmMail.CC = System.Configuration.ConfigurationManager.AppSettings["Openf2Admin"].ToString();
            }
            else
            {
                _vcmMail.CC = System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString();
            }

            _vcmMail.SendAsync = true;
            _vcmMail.Subject = strSubject;
            _vcmMail.Body = strBodyMsg;
            _vcmMail.IsBodyHtml = true;
            _vcmMail.SendMail(0);
            _vcmMail = null;
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURLPage", "SendMail()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURLPage", "SendMail", ex.Message, ex);
        }
    }

    #endregion
}
