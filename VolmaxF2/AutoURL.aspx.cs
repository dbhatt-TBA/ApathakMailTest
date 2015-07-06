using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VCM.Common;
using System.Data;
using System.Web.UI.HtmlControls;
using VcmUserNamespace;
using VCM.Common.Log;

public partial class AutoURL : System.Web.UI.Page
{
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

    string ApplicationCode = Convert.ToString((int)AutoURLApplicationCode.ISWAP);

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentSession.User == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Signout.aspx", false);
            return;
        }
        if (CurrentSession.User.IsTrader == "TRUE")
        {
            string script = @"setMessage();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "setM", script, true);
            return;
        }

        if (!IsPostBack)
        {
            try
            {
                hdnUserID.Value = Convert.ToString(CurrentSession.User.UserID);
                drpSelectPage.SelectedValue = "TradePage";
                Get_Customer_List();

                SetCMEView();
            }
            catch (Exception ex)
            {
                VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "Page_Load()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
                LogUtility.Error("AutoURL", "Page_Load", ex.Message, ex);
            }
        }
    }

    protected void GridAutoURL_DataBound(object sender, EventArgs e)
    {
        if (GridAutoURL.Rows.Count == 0)
            return;

        try
        {
            Menu menuPager = (Menu)GridAutoURL.BottomPagerRow.FindControl("menuPager");

            for (int i = 0; i < GridAutoURL.PageCount; i++)
            {
                MenuItem item = new MenuItem();
                item.Text = String.Format("{0}", i + 1);
                item.Value = i.ToString();
                if (GridAutoURL.PageIndex == i)
                    item.Selected = true;
                menuPager.Items.Add(item);
            }
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "GridAutoURL_DataBound()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "GridAutoURL_DataBound", ex.Message, ex);
        }
    }

    protected void menuPager_MenuItemClick(object sender, MenuEventArgs e)
    {
        GridAutoURL.PageIndex = Int32.Parse(e.Item.Value);
        GridAutoURL.DataBind();
    }

    protected void btnSendMail_Click(object sender, System.EventArgs e)
    {
        SessionInfo CurrentSession = new SessionInfo(HttpContext.Current.Session);

        if (CurrentSession.User.UserID.ToString() != hdnUserID.Value)
        {
            LogUtility.Info("AutoURL", "btnSendMail_Click", "UserID Mismatch in Session [" + CurrentSession.User.UserID.ToString() + "] vs Hidden field [" + hdnUserID.Value + "]. Sign out occurs.");
            Response.Redirect("sto.aspx?RefNo=999990", false);
        }

        if (CurrentSession.User.IsTrader == "TRUE")
        {
            string script = @"setMessage();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "setM", script, true);
            return;
        }

        LogUtility.Info("AutoURL", "btnSendMail_Click", "Clicked to send AutoUrl");

        DateTime fromTm = DateTime.UtcNow;
        DateTime toDtTm = fromTm.AddMinutes(int.Parse(drpExpireHours.SelectedValue));
        int RowCount = GridAutoURL.Rows.Count;
        int ColCount = GridAutoURL.Columns.Count;
        string MsgBody; string strURL;
        string strfailuer = string.Empty;
        string FailMsgBody = string.Empty;

        string AutoURL = Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.Segments[0] + Request.Url.Segments[1] + "AutoURLRedirect.aspx";
        string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
        string strBcc = System.Configuration.ConfigurationManager.AppSettings["BCCEMAIL"].ToString();
        string strMinuteInterval = Convert.ToString(drpExpireHours.SelectedValue);
        string UserID = string.Empty;

        string strComment = txtComment.Text.Trim().Replace("'", "");
        strComment = strComment.Replace("\r\n", "<br/>"); //For IE and OPERA
        strComment = strComment.Replace("\n", "<br/>"); // For Mozilla, Chrome
        strComment = strComment.Replace("\r", "<br/>");  //For rest Browser

        bool bCME_Rights = chkIsCmeEnabled.Checked ? true : false;

        for (int i = 0; i < RowCount; i++)
        {
            if (((HtmlInputCheckBox)GridAutoURL.Rows[i].FindControl("CheckSingle")).Checked)
            {
                try
                {
                    if (GridAutoURL.Rows[i].Cells[2].Text.Trim() != "&nbsp;" && GridAutoURL.Rows[i].Cells[3].Text.Trim() != "&nbsp;") //&& GridAutoURL.Rows[i].Cells[4].Text.Trim() != "&nbsp;")
                    {

                        string AutoURLGUID = Guid.NewGuid().ToString();
                        UserID = Convert.ToString(GridAutoURL.Rows[i].Cells[1].Text.Trim());
                        string CustomerID = ((HiddenField)GridAutoURL.Rows[i].FindControl("hdf_CustomerID")).Value;
                        MsgBody = "<div style=\"font-size:12pt;color:navy;font-family:Verdana\"><b>THE BEAST APPS</b></div><br/><div style=\"font-size:8pt;color:navy;font-family:Verdana\">" +
                                (string.IsNullOrEmpty(strComment) ? "" : "<p>" + strComment + "</p>") + "<br/>Dear Customer,<p> You may access <b>The BEAST Apps </b> by clicking on the following URL. You may copy and paste this URL in your browser as well.</p>" +
                                                 "<p><a href=" + AutoURL + "?RefNo=" + AutoURLGUID + ">" + AutoURL + "?RefNo=" + AutoURLGUID + "</a></p>" +
                                                 "<p>This " + (drpSelectPage.SelectedIndex == 1 ? "<strong>" + drpSelectPage.SelectedItem.Text + "</strong>" : "") + " URL is valid for is as follows:<br/></p> " +
                                                 "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">User:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + GridAutoURL.Rows[i].Cells[2].Text.Trim() + "</td></tr><br/> " +
                                                 "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">URL Valid For:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(fromTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT) To " + Convert.ToDateTime(toDtTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT)" + "</td></tr></table></p> " +
                                                 "<p><b>NOTE:</b><b><i>&nbsp;Please treat this URL confidential as this URL will give the recipient an access to your account and the information contained there in.</i></b></p>" +
                                                 "<p>If you do not wish to receive these URLs, please let us know.</p>" +
                                                 "<p>Please contact us if you have any questions.<br/><br/>Sincerely, <br/>" + CurrentSession.User.UserName.ToString() + "<br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div></p>";
                        strURL = AutoURL + "?RefNo=" + AutoURLGUID;
                        string strto = "";
                        string strmNemonic = Convert.ToString((!string.IsNullOrEmpty(GridAutoURL.Rows[i].Cells[4].Text.Trim()) ? (GridAutoURL.Rows[i].Cells[4].Text.Trim().IndexOf(".") > 0 ? GridAutoURL.Rows[i].Cells[4].Text.Trim().Remove(GridAutoURL.Rows[i].Cells[4].Text.Trim().IndexOf(".")) : GridAutoURL.Rows[i].Cells[4].Text.Trim()) : ""));
                        strto = GridAutoURL.Rows[i].Cells[3].Text.ToString();
                        if (i % 30 == 0)
                            System.Threading.Thread.Sleep(1500);

                        string SelectedPage;
                        SelectedPage = drpSelectPage.SelectedValue.ToString();

                        //if (vcmProductNamespace.cDbHandler.SubmitAutoUrl(GridAutoURL.Rows[i].Cells[1].Text.Trim(), strURL.ToString().Trim(), AutoURLGUID.Trim(), Convert.ToDateTime(txtDate.Text), SelectedPage, Convert.ToString(CurrentSession.User.SessionID)))
                        vcmProductNamespace.cDbHandler.SubmitVCMAutoURL(UserID, "0", ApplicationCode, Convert.ToString(fromTm), Convert.ToString(toDtTm), AutoURLGUID, strURL, SelectedPage, "", MsgBody, strmNemonic, CustomerID, "", Convert.ToString(CurrentSession.User.IPAddress), Convert.ToString(CurrentSession.User.UserID), strMinuteInterval, "", bCME_Rights);

                        SendMail(FromEmail, strto, strBcc, "The BEAST Financial Framework - AutoURL", MsgBody, true);

                        (((HtmlInputCheckBox)GridAutoURL.Rows[i].FindControl("CheckSingle"))).Checked = false;
                        SelectedPage = null;
                        lblMessage.Visible = false;
                        lblMessage.Text = "";
                    }
                    else
                    {
                        lblMessage.Visible = true;
                        strfailuer += GridAutoURL.Rows[i].Cells[3].Text.ToString() + ",<br/>";
                        GridAutoURL.Rows[i].ForeColor = System.Drawing.Color.Red;
                    }

                    SetCMEView();
                }
                catch (Exception ex)
                {
                    VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "AutoURL", "btnSendMail_Click()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
                    LogUtility.Error("AutoURL", "btnSendMail_Click", ex.Message, ex);
                }
            }
        }

        //***********Code for Email Textbox AutoURL*******************

        if (txtEmail.Text.Trim() != "" && txtEmail.Text.Trim() != "&nbsp;")
        {
            try
            {
                string AutoURLGUID = Guid.NewGuid().ToString();
                UserID = "0";
                string CustomerID = "0";
                MsgBody = "<div style=\"font-size:12pt;color:navy;font-family:Verdana\"><b>THE BEAST APPS</b></div><br/><div style=\"font-size:8pt;color:navy;font-family:Verdana\">"
                                + (string.IsNullOrEmpty(strComment) ? "" : "<p>" + strComment + "</p>") + "<br/>Dear Customer,<p> You may access <b>The BEAST Apps </b> by clicking on the following URL. You may copy and paste this URL in your browser as well.</p>" +
                                         "<p><a href=" + AutoURL + "?RefNo=" + AutoURLGUID + ">" + AutoURL + "?RefNo=" + AutoURLGUID + "</a></p>" +
                                         "<p>This " + (drpSelectPage.SelectedIndex == 1 ? "<strong>" + drpSelectPage.SelectedItem.Text + "</strong>" : "") + " URL is valid for is as follows:<br/></p> " +
                                         "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">User:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + txtFname.Text + " " + txtLname.Text + "</td></tr><br/> " +
                                         "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">URL Valid For:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(fromTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT) To " + Convert.ToDateTime(toDtTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT)" + "</td></tr></table></p> " +
                                         "<p><b>NOTE:</b><b><i>&nbsp;Please treat this URL confidential as this URL will give the recipient an access to your account and the information contained there in.</i></b></p>" +
                                         "<p>If you do not wish to receive these URLs, please let us know.</p>" +
                                         "<p>Please contact us if you have any questions.<br/><br/>Sincerely, <br/>" + CurrentSession.User.UserName.ToString() + "<br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div></p>";
                strURL = AutoURL + "?RefNo=" + AutoURLGUID;
                string strto = "";
                string strmNemonic = "-";
                strto = txtEmail.Text.Trim();
                string SelectedPage;
                SelectedPage = drpSelectPage.SelectedValue.ToString();

                //if (vcmProductNamespace.cDbHandler.SubmitAutoUrl(GridAutoURL.Rows[i].Cells[1].Text.Trim(), strURL.ToString().Trim(), AutoURLGUID.Trim(), Convert.ToDateTime(txtDate.Text), SelectedPage, Convert.ToString(CurrentSession.User.SessionID)))
                vcmProductNamespace.cDbHandler.SubmitVCMAutoURL(UserID, "0", ApplicationCode, Convert.ToString(fromTm), Convert.ToString(toDtTm), AutoURLGUID, strURL, SelectedPage, "", MsgBody, strmNemonic, CustomerID, "", Convert.ToString(CurrentSession.User.IPAddress), Convert.ToString(CurrentSession.User.UserID), strMinuteInterval, txtEmail.Text.Trim(), bCME_Rights);
                SendMail(FromEmail, strto, strBcc, "The BEAST Financial Framework - AutoURL", MsgBody, true);
                txtEmail.Text = "";
                SelectedPage = null;
                lblMessage.Visible = false;
                lblMessage.Text = "";

            }
            catch (Exception ex)
            {
                strfailuer += txtEmail.Text.Trim() + ",<br/>";
                VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "AutoURL", "btnSendMail_Click()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
                LogUtility.Error("AutoURL", "btnSendMail_Click", ex.Message, ex);
            }
        }

        lblMessage.Visible = true;
        if (string.IsNullOrEmpty(strfailuer.Trim()))
            lblMessage.Text = "Mail has been sent.";
        else
            lblMessage.Text = "Mail has been sent to selected users except <br/> " + strfailuer;

        /*****************************************************************************/
        SetCMEView();

        VcmLogManager.Log.SendUserLoginNotification(Convert.ToString(CurrentSession.User.UserID), Convert.ToString(CurrentSession.User.UserName), 1, -1, "Click on Send Auto URL Button", "Auto URL", VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress), Convert.ToString(CurrentSession.User.SessionID), Convert.ToString(CurrentSession.User.ASPSessionID), Convert.ToString(CurrentSession.User.SessionLogincount));
        //UpnlAutoURL.Update();
    }

    protected void btnsearchTrader_Click(object sender, EventArgs e)
    {
        try
        {
            GridAutoURL.PageIndex = 0;
            lblMessage.Text = "";
            Get_Customer_List();
            LogUtility.Info("AutoURL", "btnsearchTrader_Click", "Click on Search Button");
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "AutoURL", "btnsearchTrader_Click()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "btnsearchTrader_Click", ex.Message, ex);
        }
    }

    protected void btnSubmitUser_Click(object sender, EventArgs e)
    {
        if (CurrentSession.User.UserID.ToString() != hdnUserID.Value)
        {
            //Response.Redirect("Signout.aspx");
            Response.Redirect("sto.aspx?RefNo=999990", false);
        }

        LogUtility.Info("AutoURL", "btnSubmitUser_Click", "Clicked to create user");

        Random random = new Random();
        long genNumber = random.Next(100000, 999999);
        string strPassword = genNumber.ToString();
        string UserID = string.Empty;
        bool bIsCMEuser = chkIsCmeEnabled.Checked ? true : false;
        string sMsgForLog = "";
        try
        {
            DataTable dtUserInfo = cUserDbHandler.CreateUserWithMinInfo(txtFname.Text.Replace("'", ""), txtLname.Text.Replace("'", ""), txtEmail.Text.Replace("'", ""), bIsCMEuser, strPassword, CurrentSession.User.UserID.ToString());

            if (dtUserInfo.Rows.Count > 0)
            {
                if (Convert.ToInt64(dtUserInfo.Rows[0]["Msg_Id"]) > 0)
                {
                    lblErrMsg.Text = dtUserInfo.Rows[0]["Msg"].ToString() + "AutoURL Sent.";

                    #region ==========Create User ==========

                    SendUserCreatedMail(Convert.ToString(dtUserInfo.Rows[0]["Msg_Id"]), txtFname.Text.Replace("'", "") + " " + txtLname.Text.Replace("'", ""), txtEmail.Text.Replace("'", ""), strPassword);
                    UserID = Convert.ToString(dtUserInfo.Rows[0]["Msg_Id"]);

                    sMsgForLog = "User created [ID=" + UserID + ", Name=" + txtFname.Text + " " + txtLname.Text + ", Email=" + txtEmail.Text + "].";

                    #endregion
                }
                else
                {
                    UserID = Convert.ToString(dtUserInfo.Rows[0]["UserId"]);
                    lblErrMsg.Text = dtUserInfo.Rows[0]["Msg"].ToString() + " But AutoURL Sent!";
                    sMsgForLog = lblErrMsg.Text;
                }
                #region ==========Send Mail after Creating User ==========

                string AutoURLGUID = Guid.NewGuid().ToString();

                string CustomerID = "0";
                string strmNemonic = "-";
                string AutoURL = Request.Url.Scheme + "://" + Request.Url.Authority + Request.Url.Segments[0] + Request.Url.Segments[1] + "AutoURLRedirect.aspx";
                string strURL = AutoURL + "?RefNo=" + AutoURLGUID;
                string SelectedPage = drpSelectPage.SelectedValue.ToString();

                string strComment = txtComment.Text.Trim().Replace("'", "");
                strComment = strComment.Replace("\r\n", "<br/>"); //For IE and OPERA
                strComment = strComment.Replace("\n", "<br/>"); // For Mozilla, Chrome
                strComment = strComment.Replace("\r", "<br/>");  //For rest Browser                

                DateTime fromTm = DateTime.UtcNow;
                DateTime toDtTm = fromTm.AddMinutes(int.Parse(drpExpireHours.SelectedValue));
                string strMinuteInterval = Convert.ToString(drpExpireHours.SelectedValue);

                string strto = txtEmail.Text.Trim();
                string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                string strBcc = System.Configuration.ConfigurationManager.AppSettings["BCCEMAIL"].ToString();

                string MsgBody = "<div style=\"font-size:12pt;color:navy;font-family:Verdana\"><b>THE BEAST APPS</b></div><br/><div style=\"font-size:8pt;color:navy;font-family:Verdana\">" +
                     (string.IsNullOrEmpty(strComment) ? "" : "<p>" + strComment + "</p>") + "<br/>Dear Customer,<p> You may access <b>The BEAST Apps </b> by clicking on the following URL. You may copy and paste this URL in your browser as well.</p>" +
                                         "<p><a href=" + AutoURL + "?RefNo=" + AutoURLGUID + ">" + AutoURL + "?RefNo=" + AutoURLGUID + "</a></p>" +
                                         "<p>This " + (drpSelectPage.SelectedIndex == 1 ? "<strong>" + drpSelectPage.SelectedItem.Text + "</strong>" : "") + " URL is valid for is as follows:<br/></p> " +
                                         "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">User:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + txtFname.Text + " " + txtLname.Text + "</td></tr><br/> " +
                                         "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">URL Valid For:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + Convert.ToDateTime(fromTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT) To " + Convert.ToDateTime(toDtTm).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT)" + "</td></tr></table></p> " +
                                         "<p><b>NOTE:</b><b><i>&nbsp;Please treat this URL confidential as this URL will give the recipient an access to your account and the information contained there in.</i></b></p>" +
                                         "<p>If you do not wish to receive these URLs, please let us know.</p>" +
                                         "<p>Please contact us if you have any questions.<br/><br/>Sincerely, <br/>" + CurrentSession.User.UserName.ToString() + "<br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div></p>";

                vcmProductNamespace.cDbHandler.SubmitVCMAutoURL(UserID, "0", ApplicationCode, Convert.ToString(fromTm), Convert.ToString(toDtTm), AutoURLGUID, strURL, SelectedPage, "", MsgBody, strmNemonic, CustomerID, "", Convert.ToString(CurrentSession.User.IPAddress), Convert.ToString(CurrentSession.User.UserID), strMinuteInterval, txtEmail.Text.Trim(), bIsCMEuser);

                SendMail(FromEmail, strto, strBcc, "The BEAST Financial Framework - AutoURL", MsgBody, true);

                LogUtility.Info("AutoURL", "btnSubmitUser_Click", sMsgForLog + "AutoUrl=" + strURL);

                #endregion

                Get_Customer_List();
                txtFname.Text = "";
                txtLname.Text = "";
                txtEmail.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message.ToString();
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "AutoURL", "btnSubmitUser_Click()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "btnSubmitUser_Click", ex.Message, ex);
        }

        SetCMEView();
    }
    #endregion

    #region Functions

    public void Get_Customer_List()
    {
        try
        {
            DataTable dTable = vcmProductNamespace.cDbHandler.FillUsersList(Convert.ToString(CurrentSession.User.UserID));
            GridAutoURL.DataSource = dTable;

            GridAutoURL.DataBind();
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "AutoURL", "Get_Customer_List()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "Get_Customer_List", ex.Message, ex);
        }
    }

    private void SendMail(string FromId, string strTo, string strBCC, string strSubject, string strBodyMsg, bool bFlag)
    {
        try
        {
            VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();

            _vcmMail.From = FromId;
            _vcmMail.To = (strTo == "" ? FromId : strTo);
            _vcmMail.CC = Convert.ToString(CurrentSession.User.EmailID);

            if (bFlag)
            {
                if (UtilityHandler.bIsImportantMail(strTo))
                {
                    _vcmMail.BCC = strBCC;
                }
                else
                {
                    _vcmMail.BCC = System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString();
                }
            }
            _vcmMail.SendAsync = true;
            _vcmMail.Subject = strSubject;
            _vcmMail.Body = strBodyMsg;
            _vcmMail.IsBodyHtml = true;
            _vcmMail.SendMail(1);
            _vcmMail = null;
            LogUtility.Info("AutoURL", "SendMail", "AutoUrl sent to " + strTo + ".");
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "SendMail()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "SendMail", ex.Message, ex);
        }
    }

    private void SendUserCreatedMail(string sUserID, string sUserName, string sEmail, string sNewPassword)
    {
        try
        {
            string strMsgBody = "<div style=\"font-size:12pt;color:navy;font-family:Verdana\"><b>THE BEAST APPS</b></div><br/><div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<p> New login is created with following details.</p>" +
                                                        "<table>" +
                                                        "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">Trader Code:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + sUserID + "</td></tr> " +
                                                        "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">Username:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + sUserName + "</td></tr> " +
                                                        "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">LoginID:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + sEmail + "</td></tr>" +
                                                        "<tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">Temporary Password:</td> <td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;\">&nbsp;" + sNewPassword + "</td></tr> " +
                                                        "</table> " +
                                                        "<p>Please contact us if you have any questions.<br/><br/>Sincerely, <br/>" + CurrentSession.User.UserName.ToString() + "<br/>" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + "</div></p>";

            VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();
            _vcmMail.From = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["volmaxAdmin"]);


            if (UtilityHandler.bIsImportantMail(sEmail))
            {
                _vcmMail.To = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["volmaxAdmin"]);
            }
            else
            {
                _vcmMail.To = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["InternalEmail"]);
            }

            _vcmMail.SendAsync = true;
            _vcmMail.Subject = "Login Created";
            _vcmMail.Body = strMsgBody;
            _vcmMail.IsBodyHtml = true;
            _vcmMail.SendMail(0);//if 0 then Replyto will NOT set in the sendMail function
            _vcmMail = null;
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "SendUserCreatedMail()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "SendUserCreatedMail", ex.Message, ex);
        }
    }

    private void SetCMEView()
    {
        if (Convert.ToString(HttpContext.Current.Session["UserGroup"]).Trim() == "CME_ICAP")
        {
            chkIsCmeEnabled.Checked = true;
            hdnCmeBackground.Value = "1";
            tblSelect.Attributes.Add("class", "IcapCmeWatermark1");
            chkIsCmeEnabled.Enabled = false;
        }
        else
        {
            chkIsCmeEnabled.Enabled = true;
            chkIsCmeEnabled.Checked = false;
            hdnCmeBackground.Value = "0";
            tblSelect.Attributes.Add("class", "");            
        }
    }

    #endregion

    #region Sorting

    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["sortDirection"] == null)
                ViewState["sortDirection"] = SortDirection.Ascending;

            return (SortDirection)ViewState["sortDirection"];
        }
        set { ViewState["sortDirection"] = value; }
    }

    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            //  You can cache the DataTable for improving performance
            //string Search_Type = Convert.ToString(drpTraderSearch.SelectedValue.Trim() == "" ? "ALL" : drpTraderSearch.SelectedValue.Trim());
            DataTable dTable = vcmProductNamespace.cDbHandler.FillUsersList(Convert.ToString(CurrentSession.User.UserID));

            DataView dv = new DataView(dTable);
            dv.Sort = sortExpression + direction;

            GridAutoURL.DataSource = dv;
            GridAutoURL.DataBind();
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "SortGridView()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "SortGridView", ex.Message, ex);
        }
    }

    protected void GridAutoURL_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            string sortExpression = e.SortExpression;

            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, DESCENDING);
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "GridAutoURL_Sorting()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "GridAutoURL_Sorting", ex.Message, ex);
        }
    }

    #endregion
}
