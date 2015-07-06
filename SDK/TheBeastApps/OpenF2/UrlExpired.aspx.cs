using System;
using System.Data;
using System.Web;
using VCM.Common.Log;

namespace OpenF2
{
    public partial class UrlExpired : System.Web.UI.Page
    {
        string AutoURL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidateRequest();
                lblReqAccessMessage.Text = "Please click on \"Request Access\" button below to request access to the app(s).";
                lblReqAccessMessage.Font.Bold = false;
            }
        }

        private void ValidateRequest()
        {
            string strBrows = Request.Browser.Version.ToString();
            
            AutoURL = Request.Url.ToString().Replace(" ", "%20");

            string MsgBody = string.Empty;
            string Subject = string.Empty;
            string LoginID = string.Empty;

            string vEmail = string.Empty;
            int MsgID = 1000;

            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string strIpAddr = UtilityHandler.Get_IPAddress(Request.UserHostAddress);

                    DataTable tblURlExp = (DataTable)Session["URL_EXP"];

                    if (tblURlExp != null && tblURlExp.Rows.Count > 0)
                    {
                        long vInitiatiorUserId = Convert.ToInt64(tblURlExp.Rows[0]["InitiatorUserId"]);
                        vEmail = Convert.ToString(tblURlExp.Rows[0]["EmailId"]);
                        AutoURL = tblURlExp.Rows[0]["AutoURL"].ToString();
                        int iUrlExpiredFlag = Convert.ToInt32(tblURlExp.Rows[0]["MsgId"]);
                        MsgID = Convert.ToInt32(tblURlExp.Rows[0]["MsgId"]);

                        /*===== SHOW MESSAGE =====*/
                        if (MsgID == -9)
                        {
                            lblMessage.Text = "The URL you clicked on has <i><strong>expired</strong></i>.";
                            lblValidityPeriodTitle.Text = "URL Expired On:";
                            lblValidityPeriod.Text = Convert.ToDateTime(tblURlExp.Rows[0]["EndDate"]).ToString("dd-MMM-yyyy HH:mm:ss tt");
                        }
                        else if (MsgID == -10)
                        {
                            lblMessage.Text = "The Calculator shared by this URL is <i><strong>closed by the initiator</strong></i> and no longer available";
                        }
                        else
                        {
                            lblMessage.Text = "The URL you clicked on has <i><strong>expired</strong></i>.";
                            lblValidityPeriodTitle.Text = "URL Expired On:";
                            lblValidityPeriod.Text = Convert.ToDateTime(tblURlExp.Rows[0]["EndDate"]).ToString("dd-MMM-yyyy HH:mm:ss tt");
                        }
                        lblUrl.Text = AutoURL;
                        lblUser.Text = tblURlExp.Rows[0]["EmailId"].ToString();
                        lblSharedBy.Text = tblURlExp.Rows[0]["InitiatorName"].ToString();
                        lblInitiatorEmail.Text = " (" + tblURlExp.Rows[0]["InitiatorEmailId"].ToString() + ")";
                        //lblSharedTo.Text = tblURlExp.Rows[0]["EmailId"].ToString();                        

                    }

                    tblURlExp = null;
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message.ToString();
                LogUtility.Error("UrlExpired.aspx", "ValidateRequest", ex.Message, ex);
            }
        }

        protected void btnRequestAccess_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable tblURlExp = (DataTable)Session["URL_EXP"];
                string strInitiatorInfo = Convert.ToString(tblURlExp.Rows[0]["InitiatorName"]) + "#" + Convert.ToString(tblURlExp.Rows[0]["InitiatorEmailId"]);
                string strTargetUserEmail = tblURlExp.Rows[0]["EmailId"].ToString();

                LogUtility.Info("UrlExpired.aspx", "btnRequestAccess_Click", strTargetUserEmail + "clicked Request Access for expired url");

                string strMailBody = "<p>Hi " + strInitiatorInfo.Split('#')[0] + ",</p>"
                                    + "<p>The AutoUrl link you shared with <strong>" + strTargetUserEmail + "</strong> is expired.</p>"
                                    + "<p>The person requests to get the access for the same. Please login to share the calculator again.</p>"
                                    + "<p>" + UtilityHandler.VCM_MailAddress_In_Html + "</p>";

                SendMail(System.Configuration.ConfigurationManager.AppSettings["FromEmail"], strInitiatorInfo.Split('#')[1], "Shared Auto Url - Access Request", strMailBody, false);

                btnRequestAccess.Visible = false;

                lblReqAccessMessage.Text = "Thank you. Your request has been sent to " + lblSharedBy.Text + ".";
                lblReqAccessMessage.Font.Bold = true;

                tblURlExp = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UrlExpired.aspx", "btnRequestAccess_Click", ex.Message, ex);
            }
        }

        private void SendMail(string strFrom, string strTo, string strSubject, string strBodyMsg, bool bFlag)
        {
            try
            {
                //strFrom = "apathak@thebeastapps.com";
                //strTo = "apathak@thebeastapps.com";

                VCM_Mail _vcmMail = new VCM_Mail();
                _vcmMail.To = strTo;
                _vcmMail.From = strFrom;
                _vcmMail.BCC = strFrom;

                _vcmMail.SendAsync = true;
                _vcmMail.Subject = strSubject;
                _vcmMail.Body = strBodyMsg;
                _vcmMail.IsBodyHtml = true;
                _vcmMail.SendMail();
                _vcmMail = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UrlExpired.aspx", "SendMail", ex.Message, ex);
            }
        }
    }
}