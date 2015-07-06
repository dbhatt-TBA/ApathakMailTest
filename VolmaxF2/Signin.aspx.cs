using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VCM.Common.Log;
using VCM.Common;

public partial class Signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        UserValidation(txtUserID.Text.ToString(), txtPass.Text.ToString());
    }

    private void UserValidation(string strUserName, string strPass)
    {
        try
        {
            ViewState["UserName"] = strUserName;
            string[] struserDtl;
            string strMsg = string.Empty;
            string strLoginTime = VcmUserNamespace.cUserDbHandler.GetUtcSqlServerDate();

            long lUserid = VcmUserNamespace.cUserDbHandler.CheckUserStatus(strUserName, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            if (lUserid > 0)
            {
                if (VcmUserNamespace.cUserDbHandler.ValidateUser(strUserName.Trim(), strPass.Trim()))
                {
                    struserDtl = VcmUserNamespace.cUserDbHandler._userState.ToString().Split('#');
                    string strSecurityQuestion_Pass_Flag = VcmUserNamespace.cUserDbHandler.GetUserPrimarySetting(lUserid, 2);

                    if (Convert.ToInt16(strSecurityQuestion_Pass_Flag.IndexOf("#")) < 0)
                    {
                        lblMessage.Text = strSecurityQuestion_Pass_Flag;
                        return;
                    }

                    SessionInfo CurrentSession = new SessionInfo(HttpContext.Current.Session);

                    CurrentSession.User = new SiteUserInfo(Convert.ToInt64(struserDtl[0]), struserDtl[3].ToString(), struserDtl[1].ToString(), 1, Convert.ToInt16(struserDtl[7]), struserDtl[8], Convert.ToString(Guid.NewGuid()), "", "", VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress), Convert.ToString(struserDtl[10]));

                    CurrentSession.User.CustomerId = Convert.ToInt64(struserDtl[2]); //Convert.ToInt64(VcmUserNamespace.cUserDbHandler.GetCustomerId(CurrentSession.User.UserID.ToString()));
                    //CurrentSession.User.IsTrader = VcmUserNamespace.cUserDbHandler.CheckIsTrader(CurrentSession.User.UserID.ToString());
                    CurrentSession.User.IsTrader = VcmUserNamespace.cUserDbHandler.CheckIsTraderNew(CurrentSession.User.UserID.ToString());
                    CurrentSession.User.IPAddress = VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress);

                    if (CurrentSession.User.IsTrader == "TRUE")
                    {
                        lblMessage.Text = "You are not authorized user for this site. Admin Login only.";
                        return;
                    }

                    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                    {
                        strMsg = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Admin,<br/><br/>User " + strUserName + " has successfully logged in on " + strLoginTime + " (GMT)."
                                + "<table style=font-size:8pt;color:navy;font-family:Verdana><tr><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress) + " </td></tr>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Site:</td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>AutoURLAdmin</td></tr>"
                                + "</table>" +
                                 "<br/><br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div>";
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["LoginInfo"].ToString(), "Admin : User Login Notification", strMsg, false);
                    }

                    if (strSecurityQuestion_Pass_Flag.Substring(0, strSecurityQuestion_Pass_Flag.IndexOf("#")) == "0")
                    {
                        //Response.Redirect("AccountInformation.aspx?val=S");
                        VcmLogManager.Log.SendUserLoginNotification(Convert.ToString(CurrentSession.User.UserID), Convert.ToString(CurrentSession.User.UserName), 1, -1, "User login", "Index", VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress), Convert.ToString(CurrentSession.User.SessionID), Convert.ToString(CurrentSession.User.ASPSessionID), Convert.ToString(CurrentSession.User.SessionLogincount));
                        Response.Redirect("AutoUrl.aspx", false);
                    }
                    else if (strSecurityQuestion_Pass_Flag.Substring(strSecurityQuestion_Pass_Flag.IndexOf("#") + 1) == "0")
                    {
                        //Response.Redirect("AccountInformation.aspx?val=P");
                        VcmLogManager.Log.SendUserLoginNotification(Convert.ToString(CurrentSession.User.UserID), Convert.ToString(CurrentSession.User.UserName), 1, -1, "User login", "Index", VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress), Convert.ToString(CurrentSession.User.SessionID), Convert.ToString(CurrentSession.User.ASPSessionID), Convert.ToString(CurrentSession.User.SessionLogincount));
                        Response.Redirect("AutoUrl.aspx", false);
                    }
                    else
                    {
                        VcmLogManager.Log.SendUserLoginNotification(Convert.ToString(CurrentSession.User.UserID), Convert.ToString(CurrentSession.User.UserName), 1, -1, "User login", "Index", VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress), Convert.ToString(CurrentSession.User.SessionID), Convert.ToString(CurrentSession.User.ASPSessionID), Convert.ToString(CurrentSession.User.SessionLogincount));
                        Response.Redirect("AutoUrl.aspx", false);
                    }
                    //}
                }
                else
                {
                    struserDtl = VcmUserNamespace.cUserDbHandler._userState.ToString().Split('#');
                    if (struserDtl[0] == "-300")
                    {
                        ViewState["PwdCount"] = Convert.ToInt16(ViewState["PwdCount"]) + 1;

                        if (Convert.ToInt16(ViewState["PwdCount"]) == 5)
                        {
                            VcmUserNamespace.cUserDbHandler.SetUserLoginActivatationFLag(Convert.ToInt64(struserDtl[1]));

                            if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                            {
                                strMsg = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Customer,<br/><br/>" +
                                         "Your account has been blocked. To unblock your account or if this is an error, please contact us.<br/><br/>" +
                                        VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div>";

                                SendMail(strUserName, "Account Locked", strMsg, true);
                            }

                            lblMessage.Text = "Your account has been blocked. E-mail has been sent to your registered id.";
                            btnLogin.Enabled = false;
                        }
                        else
                        {
                            lblMessage.Text = "Invalid Password, Remaining attempts: " + (5 - Convert.ToInt16(ViewState["PwdCount"]));
                            txtPass.Focus();
                        }
                    }
                    else if (struserDtl[0] == "-100")
                    {
                        lblMessage.Text = "User is not registered. Please register first or contact us for further assistance.";
                    }
                    else if (struserDtl[0] == "-200")
                    {
                        lblMessage.Text = "Your Account has been Blocked, Email has been sent to your registered id.";
                    }
                    else if (struserDtl[0] == "-900")
                    {
                        lblMessage.Text = "Your Account has been locked. Email has been sent to your registered id.";
                    }
                }
            }
            else if (lUserid == 0)
            {
                if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                {
                    strMsg = "<div style=font-size:8pt;color:NAVY;font-family:Verdana>Dear Admin,<br/><br/>"
                            + "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), "
                            + " as the user's account was locked.<br/><br/>"
                            + "<table style=font-size:8pt;color:NAVY;font-family:Verdana><tr><td  width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>"
                            + "<td  width=50% style=ONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress) + " </td></tr>"
                            + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Site:</td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>AutoURLAdmin</td></tr>"
                            + "</table>"
                            + "<br/><br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div>";
                    SendMail(System.Configuration.ConfigurationManager.AppSettings["LoginInfo"].ToString(), "Admin : User Login Notification", strMsg, false);
                }
                lblMessage.Text = "Your Account has been Blocked, Email has been sent to your registered id.";
            }
            else if (lUserid == -2)
            {
                if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                {
                    strMsg = "<div style=font-size:8pt;color:NAVY;font-family:Verdana>Dear Admin,<br/><br/>"
                            + "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), "
                            + " as the dummy user's account is not allowed to access out of domain.<br/><br/>"
                            + "<table style=font-size:8pt;color:NAVY;font-family:Verdana><tr><td  width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>"
                            + "<td  width=50% style=ONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress) + " </td></tr>"
                            + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Site:</td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>AutoURLAdmin</td></tr>"
                            + "</table>"
                            + "<br/><br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div>";

                    SendMail(System.Configuration.ConfigurationManager.AppSettings["LoginInfo"].ToString(), "Admin : User Login Notification", strMsg, false);
                }
                lblMessage.Text = "Dummy user not allowed to access out of domain.";
                txtUserID.Focus();
            }
            else if (lUserid == -5)
            {
                if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                {
                    strMsg = "<div style=font-size:8pt;color:NAVY;font-family:Verdana>Dear Admin,<br/><br/>"
                           + "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), "
                           + " because user IP Address is not authorized.<br/><br/>"
                           + "<table style=font-size:8pt;color:NAVY;font-family:Verdana><tr><td  width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>"
                           + "<td  width=50% style=ONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress) + " </td></tr>"
                           + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Site:</td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>AutoURLAdmin</td></tr>"
                           + "</table>"
                           + "<br/><br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div>";

                    SendMail(System.Configuration.ConfigurationManager.AppSettings["LoginInfo"].ToString(), "Admin : User Login Notification", strMsg, false);
                }
                lblMessage.Text = "Your IP Address is not authorized.";
                txtUserID.Focus();
            }
            else
            {
                if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                {
                    strMsg = "<div style=font-size:8pt;color:NAVY;font-family:Verdana>Dear Admin,<br/><br/>"
                           + "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), "
                           + " as user was not registered.<br/><br/>"
                           + "<table style=font-size:8pt;color:NAVY;font-family:Verdana><tr><td  width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>"
                           + "<td  width=50% style=ONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress) + " </td></tr>"
                           + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Site:</td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>AutoURLAdmin</td></tr>"
                           + "</table>"
                           + "<br/><br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div>";
                    SendMail(System.Configuration.ConfigurationManager.AppSettings["LoginInfo"].ToString(), "Admin : User Login Notification", strMsg, false);
                }

                lblMessage.Text = "User is not registered. Please register first or contact us for further assistance.";
                txtUserID.Focus();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Please contact thebeast@thebeastapps.com";
            if (ex.Message.Contains("Thread was being aborted"))
                return;
            SendMail(System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"].ToString(), "The BEAST Financial Framework - AutoURL :: User Login Notification", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, false);
            //VcmLogManager.Log.writeLog("", "", "", "Signin", "UserValidation()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Signin", "UserValidation", ex.Message, ex);
        }
    }

    private void SendMail(string strTo, string strSubject, string strBodyMsg, bool bFlag)
    {
        try
        {
            VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();
            _vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

            if (UtilityHandler.bIsImportantMail(Convert.ToString(ViewState["UserName"])))
            {
                //_vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                _vcmMail.To = strTo;
            }
            else
            {
                //_vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString();
                _vcmMail.To = System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString();
            }

            if (bFlag)
            {
                //  _vcmMail.CC = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                _vcmMail.BCC = System.Configuration.ConfigurationManager.AppSettings["LoginInfo"].ToString();
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
            //VcmLogManager.Log.writeLog("", "", "", "Signin", "SendMail()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Signin", "SendMail", ex.Message, ex);
        }
    }
}