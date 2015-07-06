using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using VCM.Common.Log;
using Savory.OpenBeastService;
namespace Savory
{
    public partial class Index : System.Web.UI.Page
    {
        #region variable
        public DAL objadmin = null;
        public Cadmin objadminbase = null;
        string[] struserDtl;
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
            Int64 iUid = 0;
            string strGuid = "";
            string[] strUserInfo = new string[2];

            // Clear the session
            //CurrentSession.User = null;
            //CurrentSession.ClearSession();

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
                UserValidation(txtSignInUserID.Text.Trim(), txtSignInUserPass.Text.Trim(), false, false);
            }
            catch (Exception ex)
            {
                UtilityHandler.SendEmailForError("Login.aspx :: btnSignIn_Click() :: " + ex.Message.ToString());
            }
        }

        protected void createAccount_Click(object sender, EventArgs e)
        {
            try
            {
                DAL objDAL = new DAL(0);
                DataTable dt = objDAL.RegisterUser(emailid.Text, firstname.Text, lastname.Text, pwd.Text);
                if (Convert.ToInt32(dt.Rows[0]["Msg_ID"]) > 1)
                {
                    DAL objSessionDAL = new DAL(2);
                    objSessionDAL.RegisterUserDetail(Convert.ToInt32(dt.Rows[0]["UserId"]), firstname.Text, lastname.Text, phonenumber.Text, mobilenumber.Text, Convert.ToDateTime(month.SelectedValue + "/" + day.SelectedValue + "/1900"), Convert.ToDateTime(aniMonth.SelectedValue + "/" + aniDay.SelectedValue + "/1900"), (radioconform.SelectedValue == "Yes") ? true : false, (checkEmail.Checked) ? true : false, (checkText.Checked) ? true : false);
                }
            }
            catch (Exception ex)
            {
                UtilityHandler.SendEmailForError("Default.aspx :: createAccount_Click() :: " + ex.Message.ToString());
            }

        }
        private void UserValidation(string strUserName, string strPass, bool bSetCookie, bool bIsNewRegistration)
        {
            bool bIsValidUser = false;
            try
            {
                objadmin = new DAL();
                string strMsg = string.Empty;
                string strLoginTime = objadmin.GetUtcSqlServerDate();
                long lUserid = objadmin.CheckUserStatus(strUserName, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
                string[] userInfo = null;

                OpenBeastService.Service ws = new OpenBeastService.Service();
                userInfo = ws.ValidateUser(strUserName.Trim(), strPass.Trim(), "Web");
                bool isValid = Convert.ToBoolean(userInfo[0]);

                //long lUserid = objadmin.CheckUserStatus(txtSignInUserID.Text, UtilityHandler.Get_IPAddress(Request.UserHostAddress));
                if (lUserid > 0)
                {
                    //if (objadmin.ValidateUser(strUserName.Trim(), strPass.Trim()))
                    if (isValid == true)
                    {
                        //struserDtl = DAL._userState.ToString().Split('#');
                        struserDtl = userInfo[1].ToString().Split('#');
                        Session["AuthToken"] = struserDtl[9];

                        CurrentSession.User = new SiteUserInfo(Convert.ToInt64(struserDtl[0]), struserDtl[3].ToString(), struserDtl[1].ToString(), 1, Convert.ToInt16(struserDtl[7]), struserDtl[8], Convert.ToInt64(struserDtl[2]));
                        LogUtility.Info("Login.aspx.cs", "UserValidation()", "$$UserID:" + CurrentSession.User.UserID + "$$Token:" + Session["AuthToken"] + "$$ClientType:" + "Web$$");

                        hdfId.Value = lUserid.ToString();

                        if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                        {
                            string IPAddress = UtilityHandler.Get_IPAddress(Request.UserHostAddress);

                            DataSet dsGeoIP = new DataSet();
                            dsGeoIP = objadmin.VCM_AutoURL_GeoIP_Info(IPAddress);
                            string City = "";
                            string Org = "";
                            string Country = "";

                            if (dsGeoIP.Tables.Count > 0 && dsGeoIP.Tables[0].Rows.Count > 0)
                            {
                                City = dsGeoIP.Tables[0].Rows[0][4].ToString();
                                Org = dsGeoIP.Tables[0].Rows[0][2].ToString();
                                Country = dsGeoIP.Tables[0].Rows[0][3].ToString();
                            }

                            strMsg = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Admin,<br/><br/>User " + strUserName + " has successfully logged in on " + strLoginTime + " (GMT)." +
                                     "<table style=font-size:8pt;color:navy;font-family:Verdana><br/>" +
                                     "<tr><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + IPAddress + " </td></tr>" +
                                     "<tr><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Organization: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Org + " </td></tr>" +
                                     "<tr><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>City: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + City + " </td></tr>" +
                                     "<tr><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Country: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Country + " </td></tr>" +
                                     "</table>" +
                                     "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

                            SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), "User Login Notification", strMsg, false);
                        }

                        bIsValidUser = true;
                        //Response.Redirect("AppList.aspx");
                    }
                    else
                    {
                        // struserDtl = DAL._userState.ToString().Split('#');
                        struserDtl = userInfo[1].ToString().Split('#');
                        if (struserDtl[0] == "-300")
                        {
                            ViewState["PwdCount"] = Convert.ToInt16(ViewState["PwdCount"]) + 1;

                            if (Convert.ToInt16(ViewState["PwdCount"]) == 5)
                            {
                                objadmin.SetUserLoginActivatationFLag(Convert.ToInt64(struserDtl[1]));

                                if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                                {
                                    strMsg = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Customer,<br/><br/>" +
                                             "Your account has been blocked. To unblock your account or if this is an error, please contact us.<br/><br/>" +
                                            UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

                                    SendMail(strUserName, "Account Locked", strMsg, true);
                                }

                                lblSigninMsg.Text = "Your account has been blocked. E-mail has been sent to your registered id.";
                                btnSignIn.Enabled = false;
                            }
                            else
                            {
                                lblSigninMsg.Text = "Invalid Password, Remaining attempts: " + (5 - Convert.ToInt16(ViewState["PwdCount"]));
                                txtSignInUserPass.Focus();
                            }
                        }
                        else if (struserDtl[0] == "-100")
                        {
                            lblSigninMsg.Text = "User is not registered. Please register first or contact us for further assistance.";
                        }
                        else if (struserDtl[0] == "-200")
                        {
                            lblSigninMsg.Text = "Your Account has been Blocked, Email has been sent to your registered id.";
                        }
                    }
                }
                else if (lUserid == 0)
                {
                    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                    {
                        strMsg = "<div style=font-size:8pt;color:NAVY;font-family:Verdana>Dear Admin,<br/><br/>" +
                            "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), " +
                            " as the user's account was locked.<br/><br/><table style=font-size:8pt;color:NAVY;font-family:Verdana><tr><td  width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>" +
                            "<td  width=50% style=ONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + UtilityHandler.Get_IPAddress(Request.UserHostAddress) + " </td></tr></table><br/><br/>" +
                            UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), "User Login Notification", strMsg, false);
                    }
                    lblSigninMsg.Text = "Your Account has been Blocked, Email has been sent to your registered id.";
                }
                else if (lUserid == -2)
                {
                    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                    {
                        strMsg = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Admin,<br/><br/>" +
                                       "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), " +
                                       " as the dummy user's account is not allowed to access out of domain.<br/><br/><table style=font-size:8pt;color:navy;font-family:Verdana><tr><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>" +
                                        "<td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + UtilityHandler.Get_IPAddress(Request.UserHostAddress) + " </td></tr></table><br/><br/>" +
                                        UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), "User Login Notification", strMsg, false);
                    }
                    lblSigninMsg.Text = "Dummy user not allowed to access out of domain.";
                    txtSignInUserID.Focus();
                }
                else if (lUserid == -5)
                {
                    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                    {
                        strMsg = "<div style=font-size:8pt;font-family:Verdana>Dear Admin,<br/><br/>" +
                                       "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), " +
                                       " because user IP Address is not authorized.<br/><br/><table><tr><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>" +
                                        "<td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + UtilityHandler.Get_IPAddress(Request.UserHostAddress) + " </td></tr></table><br/><br/>" +
                                        UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), "User Login Notification", strMsg, false);
                    }
                    lblSigninMsg.Text = "Your IP Address is not authorized.";
                    txtSignInUserID.Focus();
                }
                else
                {
                    if (Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == "1")
                    {
                        strMsg = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Admin,<br/><br/>" +
                                   "User " + strUserName + " was not able to login on " + strLoginTime + " (GMT), " +
                                   " as user was not registered.<br/><br/><table style=font-size:8pt;color:navy;font-family:Verdana><tr><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td>" +
                                    "<td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + UtilityHandler.Get_IPAddress(Request.UserHostAddress) + " </td></tr></table><br/><br/>" +
                                    UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";
                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), "User Login Notification", strMsg, false);
                    }

                    lblSigninMsg.Text = "User is not registered. Please register first or contact us for further assistance.";
                    txtSignInUserID.Focus();
                }
            }
            catch (Exception ex)
            {
                UtilityHandler.SendEmailForError("Login.aspx :: UserValidation() :: " + ex.Message.ToString());
            }

            if (bIsValidUser)
            {
                Response.Redirect("OrderDetails.aspx");
            }
        }

        private void SendMail(string strTo, string strSubject, string strBodyMsg, bool bFlag)
        {
            VCM_Mail _vcmMail = new VCM_Mail();
            _vcmMail.To = strTo;
            _vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
            //if (bFlag)
            //{
            //}
            _vcmMail.Subject = strSubject;
            _vcmMail.Body = strBodyMsg;
            _vcmMail.IsBodyHtml = true;
            _vcmMail.SendMail();
            _vcmMail = null;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objadmin = new DAL();
            long lUserStatus = objadmin.CheckUserStatus(txtForgetEmail.Text.ToString(), UtilityHandler.Get_IPAddress(Request.UserHostAddress));

            if (lUserStatus == 0)
            {
                //lblEmailSubmitMsg.Text = "Your Account has been Blocked. For help, Please Call: NY: +1-646-688-7500, London: +44 (0)20-7398-2800, Singapore: +65 6408 3842 or send an email to " + System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                lblEmailSubmitMsg.Text = "Your Account has been Blocked. For help, please call or send an email to us.";
                lblEmailSubmitMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else if (lUserStatus < 0)
            {
                //lblEmailSubmitMsg.Text = "This is not a Registered Email. Please try again. For help, Please Call: NY: +1-646-688-7500, London: +44 (0)20-7398-2800, Singapore: +65 6408 3842 or send an email to " + System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                lblEmailSubmitMsg.Text = "This is not a Registered Email. For help, please call or send an email to us.";
                lblEmailSubmitMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (lUserStatus > 0)
            {
                hdfId.Value = Convert.ToString(lUserStatus);
                string strQuestionAsnwer = objadmin.GetUserSecurityQuestion_And_Answer(txtForgetEmail.Text.ToString());

                if (System.Configuration.ConfigurationManager.AppSettings["ForgotPwd_AskSecurityQuestion"].Trim() == "1")
                {
                    mvUserLogin.SetActiveView(v_UserForgotPwd_SecQstn);
                    ViewState["count"] = "5";

                    lblMail.Text = txtForgetEmail.Text.ToString();

                    if (string.IsNullOrEmpty(strQuestionAsnwer.Substring(0, strQuestionAsnwer.IndexOf("#"))) || strQuestionAsnwer.Substring(0, strQuestionAsnwer.IndexOf("#")) == "0")
                    {
                        lblMessage.Text = "You have not set any security question and answer yet ! For help, Please Call: NY: +1-646-688-7500, or send an email to " + System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                        txtForgetEmail.Enabled = false;
                        chkResetPass.Visible = true;
                        return;
                    }

                    lblSecQuestion.Text = strQuestionAsnwer.Substring(0, strQuestionAsnwer.IndexOf("#"));
                    hdfAns.Value = strQuestionAsnwer.Substring(strQuestionAsnwer.IndexOf("#") + 1);

                    //mvForgotPass.ActiveViewIndex = 1;
                }
                else
                {
                    ResetPassAndSendMail(txtForgetEmail.Text.Trim());

                    mvUserLogin.SetActiveView(v_UserLogin);
                    lblSigninMsg.Text = "Your password has been reset. E-mail has been sent to your registered id.";
                    lblSigninMsg.ForeColor = System.Drawing.Color.Navy;
                }
            }

            objadmin = null;
        }

        static int iCount = 5;
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
            objadmin = new DAL();

            Random rdm = new Random();
            long lNewPassword = rdm.Next(100000, 999999);
            objadmin.ChangePassword(Convert.ToInt64(hdfId.Value), "", lNewPassword.ToString());

            objadmin.SetUserPasswordFlag(0, 0, strEmailTo);

            if (Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["EnableEmail"]) == 1)
            {
                string _beastAppsUrl = System.Configuration.ConfigurationManager.AppSettings["BeastAppsUrl"].ToString();
                string _displayUrl = (_beastAppsUrl.ToLower().IndexOf("/Default.aspx") != -1) ? _beastAppsUrl.Remove(_beastAppsUrl.ToLower().IndexOf("/Default.aspx")) : _beastAppsUrl;

                VCM_Mail _vcmMail = new VCM_Mail();
                _vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                _vcmMail.To = strEmailTo;
                _vcmMail.CC = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                _vcmMail.Subject = "Your Password Has Been Reset";

                string strUserName_Mnemonic = objadmin.GetUserCustomerDetails(objadmin.GetUserID(strEmailTo));
                string strBody = "<div style='font: normal 12px verdana; color: Navy;'>Dear Customer,<br /><br />As per your request, we have reset your password.<br /><br />Your login details are as follows:<br /><br />" +
                                "<table style='font: normal 12px verdana; color: Navy;'><tr><td>Login :</td><td>" + strEmailTo + "</td></tr>" +
                                "<tr><td>Password :</td><td>" + lNewPassword.ToString() + "</td></tr>" +
                                "<tr><td>User Name :</td><td>" + strUserName_Mnemonic.Substring(0, strUserName_Mnemonic.IndexOf("#")) + "</td></tr>" +
                                "</table>" +
                                "<br /><br />Please goto <a href=" + _beastAppsUrl + ">" + _displayUrl + "</a> to login. <br /><br />Please contact us if you have any questions.<br /><br />" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

                _vcmMail.Body = strBody;
                _vcmMail.IsBodyHtml = true;
                _vcmMail.SendMail();
            }

            lblAttempt.Text = "";
            lblMessage.Text = "Your password is reset and email for your new temporary password is sent to you on your email id. Please login with that and change your password.";
            btnSubmitAnswer.Enabled = false;
            txtAnswer.Text = "";
            txtForgetEmail.Text = "";
            chkResetPass.Checked = false;
            objadmin = null;

            //mvForgotPass.ActiveViewIndex = 0;
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