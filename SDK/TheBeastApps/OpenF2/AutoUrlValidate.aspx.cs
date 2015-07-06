using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using VCM.Common.Log;

namespace OpenF2
{
    public partial class AutoUrlValidate : System.Web.UI.Page
    {
        string AutoURL;
        public DAL objadmin = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!IsPostBack)
            {
                Response.Redirect("SharedBeastApps.aspx" + "?id=" + Request.QueryString["id"]);
                //ValidateRequest();
            }
        }

        private void ValidateRequest()
        {
            objadmin = new DAL();
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

            string strBrows = Request.Browser.Version.ToString();

            DataSet dst = new DataSet();
            Session.Clear();
            AutoURL = Request.Url.ToString().Replace(" ", "%20");

            bool bIsValidate = false;
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
                    objadmin.BeastApps_SharedAutoURL_UpdateClickCount(Request.QueryString["id"].Trim());

                    string strIpAddr = UtilityHandler.Get_IPAddress(Request.UserHostAddress);

                    dst = objadmin.BeastApps_SharedAutoURL_Validate(Request.QueryString["id"], "Web");

                    if (dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        int MsgID = Convert.ToInt32(dst.Tables[0].Rows[0]["MsgId"]);

                        long vInitiatiorUserId = Convert.ToInt64(dst.Tables[0].Rows[0]["InitiatorUserId"]);
                        string vInitiatorEmailId = Convert.ToString(dst.Tables[0].Rows[0]["InitiatorEmailId"]);

                        string vInstanceId = Convert.ToString(dst.Tables[0].Rows[0]["InstanceId"]);
                        string vInstanceInfo = Convert.ToString(dst.Tables[0].Rows[0]["InstanceInfo"]);
                        string vStartDate = Convert.ToString(dst.Tables[0].Rows[0]["StartDate"]);
                        vEmail = Convert.ToString(dst.Tables[0].Rows[0]["EmailId"]);                        
                        RedirectTo = dst.Tables[0].Rows[0]["MovetoPage"].ToString();
                        AutoURL = dst.Tables[0].Rows[0]["AutoURL"].ToString();
                        //LoginID = dst.Tables[0].Rows[0]["LoginID"].ToString();

                        bool _isImp = false;
                        if (UtilityHandler.bIsImportantMail(vInitiatorEmailId) || UtilityHandler.bIsImportantMail(vEmail))
                            _isImp = true;
                        else
                            _isImp = false;

                        string strInitiatorName = objadmin.GetUserCustomerDetails(vInitiatiorUserId).Split('#')[0];

                        Session["URL_EXP"] = dst.Tables[0];

                        if (MsgID != 0)
                        {
                            // ............fail..............

                            switch (MsgID)
                            {
                                case -2:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Not able to login for AutoURL";
                                    Subject = "Failed to login for AutoURL";
                                    break;
                                case -6:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Not able to login because of no rights for the Session";
                                    Subject = "Failed to login because of no rights for the Session";
                                    break;
                                case -5:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Not able to login because of Unauthorized IP Address";
                                    Subject = "Failed to login because of Unauthorized IP Address";
                                    break;
                                case -3:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Failed to login with the AutoURL";
                                    Subject = "Failed to login with the AutoURL";
                                    break;
                                case -1:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Failed to login with the AutoURL";
                                    Subject = "Failed to login with the AutoURL as User is either not registered or blocked";
                                    break;
                                case -9:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Failed to login with the AutoURL";
                                    Subject = "Failed to login with the AutoURL as URL Expired";
                                    iUrlExpiredFlag = 1;
                                    break;
                                case -10:
                                    //lblMessage.Text = "Error Number: " + MsgID + ", Failed to login with the AutoURL";
                                    Subject = "Failed to login with the AutoURL as the initiator has closed the calculator";
                                    iUrlExpiredFlag = 1;
                                    break;
                            }

                            // .........................

                            MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<br/><br/>"
                                        + "User " + vEmail + " failed to login with the AutoURL mentioned below:&nbsp;"
                                        + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                        + "<tr><td width=\"20%\" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                                        + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;>Shared By: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + strInitiatorName + " (" + vInitiatorEmailId + ") </td></tr>"
                                        + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;>IP Address: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + IPAddress + " </td></tr>"
                                        + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt;color:navy; FONT-FAMILY: Verdana;>Organization: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Org + " </td></tr>"
                                        + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt;color:navy; FONT-FAMILY: Verdana;>City: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + City + " </td></tr>"
                                        + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;>Country: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Country + " </td></tr>"
                                        + "</table>"
                                        + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                        + "</div>";

                            if (_isImp)
                            {
                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                    , System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                    , "Shared BeastCalc -" + Subject
                                    , MsgBody, false);
                            }
                            else
                            {
                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                    , System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString()
                                    , "Shared BeastCalc -" + Subject
                                    , MsgBody, false);
                            }
                            // ..........................

                            // ........E mail to Initiator..............                          

                            MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear " + strInitiatorName + ",<br/><br/>"
                                        + "User " + vEmail + " failed to login the BEAST Calculator shared by you."
                                        + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                        + "</div>";

                            LogUtility.Info("AutoUrlValidate", "ValidateRequest()", "Email to initiator(id=" + vInitiatorEmailId + ").Case=FAIL.UseMirror=NO");
                           
                            SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                    , vInitiatorEmailId, "Shared Beast Calc - " + Subject
                                    , MsgBody, false);
                           
                            // ........E mail to Initiator..............
                        }
                        else
                        {
                            bIsValidate = true;

                            Session.Add("IsValidAutoURL", "TRUE");

                            Subject = "Successful login";

                            // ..........success...............                            

                            MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<br/><br/>"
                                    + "User " + vEmail + " logged in with the AutoURL mentioned below:&nbsp;</br>"
                                    + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                    + "<tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + IPAddress + " </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;>Shared By: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + strInitiatorName + " (" + vInitiatorEmailId + ") </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Organization: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Org + " </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>City: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + City + " </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Country: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Country + " </td></tr>"
                                    + "</table>"
                                    + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                    + "</div>";

                            if (_isImp)
                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                        , System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                        , "Shared Beast Calc - " + Subject
                                        , MsgBody, false);
                            else
                                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                        , System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString()
                                        , "Shared Beast Calc - " + Subject
                                        , MsgBody, false);

                            // ........E mail to Initiator..............                            

                            MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear " + strInitiatorName + ",<br/><br/>"
                                    + "User " + vEmail + " has opened the BEAST Calculator successfully shared by you.<br/><br/>"
                                    + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                    + "<tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + IPAddress + " </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Organization: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Org + " </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>City: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + City + " </td></tr>"
                                    + "<tr><td width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Country: </td><td  width=\"50%\" style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>" + Country + " </td></tr>"
                                    + "</table>"
                                    + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                    + "</div>";
                            
                            LogUtility.Info("AutoUrlValidate", "ValidateRequest()", "Email to initiator(id=" + vInitiatorEmailId + ").Case=SUCCESS.UseMirror=NO");
                            
                            SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                    , vInitiatorEmailId
                                    , "Shared Beast Calc - " + Subject
                                    , MsgBody, false);
                            
                            // ........E mail to Initiator..............
                        }
                    }
                    else
                    {
                        // .........fail................

                        Subject = "Login failed";

                        MsgBody = "<div style=\"font-size:8pt;color:navy;font-family:Verdana\">Dear Admin,<br/><br/>"
                                + "User " + vEmail + " Failed to login with the AutoURL mentioned below:&nbsp;"
                                + "<table style=\"FONT-SIZE: 8pt; color:navy;FONT-FAMILY: Verdana;\">"
                                + "<tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Auto URL:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;<a href =" + AutoURL + ">" + AutoURL + "</a></td></tr>"
                                + "<tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + IPAddress + "</td></tr>"
                                + "<tr><td width=\"20% \" style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">Reason:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;No recordss found for the url id (Ref. no.)</td></tr>"
                                + "</table>"
                                + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html
                                + "</div>";

                        string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
                        string mailto = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

                        SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString()
                                , mailto
                                , "Shared Bease Calc - " + Subject
                                , MsgBody, false);

                        bIsValidate = false;

                        // ..........................
                    }
                }
                else
                {
                    //lblMessage.Text = "Invalid AutoURL";
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message.ToString();
            }

            if (bIsValidate)
            {
                Response.Redirect(RedirectTo + "?id=" + Request.QueryString["id"]);
                //Response.Redirect(RedirectTo + "?id=" + Request.QueryString["id"], false);
            }
            else if (iUrlExpiredFlag == 1) //if Url expired
            {
                //Server.Transfer("UrlExpired.aspx" + "?id=" + Request.QueryString["id"]);
                Server.Transfer("UrlExpired.aspx");
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }

        private void SendMail(string strFrom, string strTo, string strSubject, string strBodyMsg, bool bFlag)
        {
            try
            {
                VCM_Mail _vcmMail = new VCM_Mail();

                _vcmMail.From = strFrom;
                _vcmMail.To = strTo;

                _vcmMail.SendAsync = false;
                _vcmMail.Subject = strSubject;
                _vcmMail.Body = strBodyMsg;
                _vcmMail.IsBodyHtml = true;
                _vcmMail.SendMail();
                _vcmMail = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("AutoUrlValidate", "SendMail()", "Send mail error", ex);
            }
        }
    }
}