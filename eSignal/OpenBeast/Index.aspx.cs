using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VCM.Common.Log;
//using OpenBeast.Beast;
using System.Net;
using VCM.Common;
namespace OpenBeast
{
    public partial class Index : System.Web.UI.Page
    {
        #region variable
        //public DAL objadmin = null;
        //public Cadmin objadminbase = null;
        string[] struserDtl;
        String pwdChange;
        #endregion

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

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] strUserInfo = new string[2];

            // Clear the session
            CurrentSession.User = null;
            CurrentSession.ClearSession();
            
            Session["SDKUser"] = null;
            Session["SDKPassword"] = null;

            if (!IsPostBack)
            {

            }

            lblSigninMsg.Text = "";

            btnSignIn.Enabled = true;

            lblMessage.Text = "";
            txtForgetEmail.Enabled = true;
            chkResetPass.Visible = false;
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            try
            {
                //UserValidation(txtSignInUserID.Text.Trim(), txtSignInUserPass.Text.Trim(), false, false);
                ValidateSDKUser(txtSignInUserID.Text.Trim(), txtSignInUserPass.Text.Trim());
            }
            catch (Exception ex)
            {
                LogUtility.Error("Index.aspx.cs", "btnSignIn_Click()", ex.Message.ToString(), ex);
                UtilityHandler.SendEmailForError("Index.aspx :: btnSignIn_Click() :: " + ex.Message.ToString() + "<br/>I.P.Address :: " + UtilityHandler.Get_IPAddress(Request.UserHostAddress));
            }
        }

        //private void UserValidation(string strUserName, string strPass, bool bSetCookie, bool bIsNewRegistration)
        //{
        //    bool bIsValidUser = false;
        //    long lUserid = -9999;
        //    try
        //    {
        //        //OpenBeastService.Service ws = new OpenBeastService.Service();

        //        //objadmin = new DAL();
        //        //string strMsg = string.Empty;
        //        //string strLoginTime = objadmin.GetUtcSqlServerDate();
        //        //lUserid = objadmin.CheckUserStatus(strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //        //string[] userInfo = null;
        //        //////Set Cookie
        //        //SetCookie(ws);
        //        ////

        //        userInfo = ws.ValidateUser(strUserName.Trim(), strPass.Trim(), "Web");
        //        bool isValid = Convert.ToBoolean(userInfo[0]);

        //        if (lUserid > 0)
        //        {
        //            if (isValid == true)
        //            {
        //                hdn_pswd.Value = strPass.Trim().ToString();
        //                Session["hdn_pswd"] = UtilityHandler.sMD5(hdn_pswd.Value);

        //                struserDtl = userInfo[1].ToString().Split('#');
        //                Session["AuthToken"] = struserDtl[9];

        //                CurrentSession.User = new SiteUserInfo(Convert.ToInt64(struserDtl[0]), struserDtl[3].ToString(), struserDtl[1].ToString(), 1, Convert.ToInt16(struserDtl[7]), struserDtl[8], Convert.ToInt64(struserDtl[2]), Convert.ToString(struserDtl[9]), Convert.ToString(struserDtl[10]));
        //                pwdChange = struserDtl[6].ToString();
        //                LogUtility.Debug("index.aspx.cs", "UserValidation()", "$$UserID:" + CurrentSession.User.UserID + "$$Token:" + Session["AuthToken"] + "$$ClientType:" + "Web$$");

        //                hdfId.Value = lUserid.ToString();

        //                if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //                {
        //                    LogUtility.Debug("index.aspx.cs", "UserValidation()", "$UserID:" + CurrentSession.User.UserID + "$Token:" + Session["AuthToken"] + "$ClientType:" + "Web$" + "EnableEmail=1");

        //                    string IPAddress = UtilityHandler.Get_IPAddress(Request.UserHostAddress);
        //                    LogUtility.Debug("index.aspx.cs", "UserValidation()", "$$UserID:" + CurrentSession.User.UserID + "$$Token:" + Session["AuthToken"] + "$$ClientType:" + "Web$$" + " IPAddress");

        //                    DataSet dsGeoIP = new DataSet();
        //                    dsGeoIP = objadmin.VCM_AutoURL_GeoIP_Info(IPAddress);
        //                    LogUtility.Debug("index.aspx.cs", "UserValidation()", "$$UserID:" + CurrentSession.User.UserID + "$$Token:" + Session["AuthToken"] + "$$ClientType:" + "Web$$" + " dsGeoIP");

        //                    string City = "";
        //                    string Org = "";
        //                    string Country = "";

        //                    if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
        //                    {
        //                        City = dsGeoIP.Tables[0].Rows[0][4].ToString();
        //                        Org = dsGeoIP.Tables[0].Rows[0][2].ToString();
        //                        Country = dsGeoIP.Tables[0].Rows[0][3].ToString();
        //                    }

        //                    MailManager objMail = new MailManager(MailManager.MailType.Login_Success, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                    objMail.SendMail();
        //                }

        //                bIsValidUser = true;
        //                LogUtility.Debug("index.aspx.cs", "UserValidation ::()", "$UserID:" + CurrentSession.User.UserID + "$Token:" + Session["AuthToken"] + "$$ClientType:" + "Web$" + "bIsValidUser");

        //            }
        //            else
        //            {
        //                struserDtl = userInfo[1].ToString().Split('#');
        //                if (struserDtl[0] == "-300")
        //                {
        //                    ViewState["PwdCount"] = struserDtl[3];

        //                    if (Convert.ToInt16(ViewState["PwdCount"]) == 0)
        //                    {
        //                        string CustmerName;
        //                        string strUserName_Mnemonic = objadmin.GetUserCustomerDetails(objadmin.GetUserID(strUserName));

        //                        if (string.IsNullOrEmpty(strUserName_Mnemonic))
        //                            CustmerName = "Customer";
        //                        else
        //                        {
        //                            string[] commandArgs = strUserName_Mnemonic.ToString().Split(new char[] { '#' });
        //                            CustmerName = commandArgs[0];
        //                        }

        //                        if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //                        {
        //                            MailManager objMail = new MailManager(MailManager.MailType.Login_AccountLockedByWrongPwd, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                            objMail.SendMail();
        //                        }

        //                        lblSigninMsg.Text = "Your account has been locked. E-mail has been sent to your registered id.";
        //                        btnSignIn.Enabled = false;
        //                    }
        //                    else if (Convert.ToInt16(ViewState["PwdCount"]) == 1)
        //                    {
        //                        lblSigninMsg.Text = "Invalid Password, This is your last attempt to login after this your account will be locked";
        //                    }
        //                    else
        //                    {
        //                        lblSigninMsg.Text = "Invalid Password, Remaining attempts: " + ViewState["PwdCount"];
        //                        txtSignInUserPass.Focus();
        //                    }
        //                }
        //                else if (struserDtl[0] == "-100")
        //                {
        //                    lblSigninMsg.Text = "User is not registered. Please register first or contact us for further assistance.";
        //                }
        //                else if (struserDtl[0] == "-200")
        //                {
        //                    lblSigninMsg.Text = "Your Account has been disabled. Please contact us for further assistance.";
        //                }
        //                else if (struserDtl[0] == "-900")
        //                {
        //                    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //                    {
        //                        MailManager objMail = new MailManager(MailManager.MailType.Login_AccountLockedByWrongPwd, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                        objMail.SendMail();
        //                    }

        //                    lblSigninMsg.Text = "Your Account has been locked. Email has been sent to your registered id.";
        //                }
        //            }
        //        }
        //        else if (lUserid == 0)
        //        {
        //            if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //            {
        //                MailManager objMail = new MailManager(MailManager.MailType.Login_AccountLocked, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                objMail.SendMail();
        //            }
        //            lblSigninMsg.Text = "Your Account has been Blocked, Email has been sent to your registered id.";
        //        }
        //        else if (lUserid == -2)
        //        {
        //            if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //            {
        //                MailManager objMail = new MailManager(MailManager.MailType.Login_OutOfDomainAccess, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                objMail.SendMail();
        //            }
        //            lblSigninMsg.Text = "Dummy user not allowed to access out of domain.";
        //            txtSignInUserID.Focus();
        //        }
        //        else if (lUserid == -5)
        //        {
        //            if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //            {
        //                MailManager objMail = new MailManager(MailManager.MailType.Login_IPUnauthorized, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                objMail.SendMail();
        //            }
        //            lblSigninMsg.Text = "Your IP Address is not authorized.";
        //            txtSignInUserID.Focus();
        //        }
        //        else
        //        {
        //            if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
        //            {
        //                MailManager objMail = new MailManager(MailManager.MailType.Login_UserNotRegistered, strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //                objMail.SendMail();
        //            }

        //            lblSigninMsg.Text = "User is not registered. Please register first or contact us for further assistance.";
        //            txtSignInUserID.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("Index.aspx", "UserValidation()", ex.Message.ToString(), ex);
        //        UtilityHandler.SendEmailForError("Index.aspx :: UserValidation() :: " + ex.Message.ToString() + "<br/>I.P.Address :: " + UtilityHandler.Get_IPAddress(Request.UserHostAddress));
        //    }

        //    int groupid = 0;

        //    if (bIsValidUser)
        //    {
        //        if (pwdChange.Equals("1"))
        //        {
        //            Response.Redirect("ChangePassword.aspx");
        //        }
        //        OpenBeastService.Service wsnew = new OpenBeastService.Service();
        //        DataSet ds = wsnew.GetUserGroups(lUserid);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        //        {
        //            groupid = Convert.ToInt32(ds.Tables[0].Rows[0]["GroupID"]);
        //            Session["GroupId"] = groupid;
        //            if (groupid == 20)  //Checking for ICAP group. Can fail if user has multiple groups.
        //            {
        //                Response.Redirect("IcapCme.aspx", false);
        //            }
        //            else
        //            {
        //                Response.Redirect("AppList.aspx", false);
        //            }
        //        }
        //        else
        //        {
        //            Response.Redirect("AppList.aspx", false);
        //        }

        //        Response.Redirect("AppList.aspx", false);
        //    }
        //}

        private void ValidateSDKUser(string strUserName,string strPassword)
        {
            //if (strUserName == Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SDKUserEmail"]) && strPassword == Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SDKPassword"]))
            if (strUserName!=string.Empty && strPassword!=string.Empty)
            {
                Session["SDKUser"] = strUserName;
                Session["SDKPassword"] = strPassword;
                Response.Redirect("AppList.aspx", false);
            }
            else
            {
                lblSigninMsg.Text = "Invalid Username/Password, Please try again.";
            }
        }

        //private void SetCookie(OpenBeastService.Service ws)
        //{
        //    ////Set Cookie
        //    try
        //    {
        //        if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
        //        {
        //            Cookie newCookie = new Cookie();

        //            ws.CookieContainer = new System.Net.CookieContainer();
        //            newCookie.Name = HttpContext.Current.Request.Cookies.Get("AWSELB").Name;
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Name newCookieName:" + newCookie.Name);

        //            newCookie.Domain = "thebeastapps.com";
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Value newCookieDomain:" + newCookie.Domain);

        //            newCookie.Expires = HttpContext.Current.Request.Cookies.Get("AWSELB").Expires;
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Value newCookieExpires:" + newCookie.Expires);

        //            newCookie.HttpOnly = HttpContext.Current.Request.Cookies.Get("AWSELB").HttpOnly;
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Value newCookieHttpOnly:" + newCookie.HttpOnly);

        //            newCookie.Path = HttpContext.Current.Request.Cookies.Get("AWSELB").Path;
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Value newCookiePath:" + newCookie.Path);

        //            newCookie.Secure = HttpContext.Current.Request.Cookies.Get("AWSELB").Secure;
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Value newCookieSecure:" + newCookie.Secure);

        //            newCookie.Value = HttpContext.Current.Request.Cookies.Get("AWSELB").Value;
        //            LogUtility.Debug("index.aspx", "UserValidation", "Set Value newCookieValue:" + newCookie.Value);

        //            ws.CookieContainer.Add(newCookie);
        //            LogUtility.Debug("index.aspx", "UserValidation", "Add into Container");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("Index.aspx", "SetCookie()", ex.Message.ToString(), ex);
        //    }
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //objadmin = new DAL();
            //long lUserStatus = objadmin.CheckUserStatus(txtForgetEmail.Text.ToString(), UtilityHandler.Get_IPAddress(Request.UserHostAddress));

            //if (lUserStatus == 0)
            //{
            //    lblEmailSubmitMsg.Text = "Your Account has been Blocked. For help, please call or send an email to us.";
            //    lblEmailSubmitMsg.ForeColor = System.Drawing.Color.Red;
            //    return;
            //}
            //else if (lUserStatus < 0)
            //{
            //    lblEmailSubmitMsg.Text = "This is not a Registered Email. For help, please call or send an email to us.";
            //    lblEmailSubmitMsg.ForeColor = System.Drawing.Color.Red;
            //    return;
            //}

            //if (lUserStatus > 0)
            //{
            //    hdfId.Value = Convert.ToString(lUserStatus);
            //    string strQuestionAsnwer = objadmin.GetUserSecurityQuestion_And_Answer(txtForgetEmail.Text.ToString());

            //    if (System.Configuration.ConfigurationManager.AppSettings["ForgotPwd_AskSecurityQuestion"].Trim() == "1")
            //    {
            //        mvUserLogin.SetActiveView(v_UserForgotPwd_SecQstn);
            //        ViewState["count"] = "5";

            //        lblMail.Text = txtForgetEmail.Text.ToString();

            //        if (string.IsNullOrEmpty(strQuestionAsnwer.Substring(0, strQuestionAsnwer.IndexOf("#"))) || strQuestionAsnwer.Substring(0, strQuestionAsnwer.IndexOf("#")) == "0")
            //        {
            //            lblMessage.Text = "You have not set any security question and answer yet ! For help, Please Call: NY: +1-646-688-7500, or send an email to " + System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
            //            txtForgetEmail.Enabled = false;
            //            chkResetPass.Visible = true;
            //            return;
            //        }

            //        lblSecQuestion.Text = strQuestionAsnwer.Substring(0, strQuestionAsnwer.IndexOf("#"));
            //        hdfAns.Value = strQuestionAsnwer.Substring(strQuestionAsnwer.IndexOf("#") + 1);

            //        //mvForgotPass.ActiveViewIndex = 1;
            //    }
            //    else
            //    {
            //        ResetPassAndSendMail(txtForgetEmail.Text.Trim());

            //        mvUserLogin.SetActiveView(v_UserLogin);
            //        lblSigninMsg.Text = "Your password has been reset. E-mail has been sent to your registered id.";
            //        lblSigninMsg.ForeColor = System.Drawing.Color.Navy;
            //    }
            //}

            //objadmin = null;
        }
        
        protected void btnSubmitAnswer_Click(object sender, EventArgs e)
        {
            if (txtAnswer.Text.ToString().Replace("'", "").Trim().ToUpper() == hdfAns.Value.ToString().Trim().ToUpper())
            {
                ResetPassAndSendMail(lblMail.Text.Trim());

                Response.Redirect("AppList.aspx");

                return;
            }
            else
            {
                //iCount = iCount - 1;
                ViewState["count"] = Convert.ToInt16(ViewState["count"]) - 1;
                lblAttempt.Text = "Incorrect Answer, Remaining attempts: " + ViewState["count"];
                lblAttempt.ForeColor = System.Drawing.Color.Red;
                txtAnswer.Text = "";
                if (Convert.ToInt16(ViewState["count"]) == 0)
                {
                    btnSubmitAnswer.Enabled = false;
                }
            }
        }

        protected void chkResetPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResetPass.Checked)
            {
                ResetPassAndSendMail(lblMail.Text.Trim());

                Response.Redirect("AppList.aspx");
            }
        }

        protected void ResetPassAndSendMail(string strEmailTo)
        {
            //string strResult = "";
            //MailManager objMail = new MailManager(MailManager.MailType.User_ResetPassword, strEmailTo, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
            //strResult = objMail.ResetPasswordAndSendMail();

            //lblAttempt.Text = "";
            //lblMessage.Text = strResult.Split('#')[1];
            //if (strResult.Split('#')[0] == "1")
            //    lblMessage.ForeColor = System.Drawing.Color.Navy;
            //else
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //btnSubmitAnswer.Enabled = false;
            //txtAnswer.Text = "";
            //txtForgetEmail.Text = "";
            //chkResetPass.Checked = false;
        }

        protected void lbtnForgotPwd_Click(object sender, EventArgs e)
        {
            mvUserLogin.SetActiveView(v_UserForgotPwd_EmailInput);
            if (System.Configuration.ConfigurationManager.AppSettings["ForgotPwd_AskSecurityQuestion"].Trim() == "1")
            {
                lblInputEmailTitle.Text = "Enter your registered email address";
            }
            else
            {
                lblInputEmailTitle.Text = "Enter your registered email address to receive your password";
            }
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            mvUserLogin.SetActiveView(v_UserLogin);
        }

        protected void v_UserLogin_Activate(object sender, EventArgs e)
        {
            txtSignInUserID.Text = "";
            txtSignInUserPass.Text = "";
            lblSigninMsg.Text = "";
        }

        protected void v_UserForgotPwd_EmailInput_Activate(object sender, EventArgs e)
        {
            txtForgetEmail.Text = "";
            lblEmailSubmitMsg.Text = "";
        }
    }
}