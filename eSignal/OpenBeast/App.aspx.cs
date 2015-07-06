using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
//using OpenF2.AppCode;
using DAL;
using OpenF2.AppCode;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using OpenBeast.Utilities;
using VCM.Common.Log;




namespace OpenF2
{
    public partial class ESignalApp : System.Web.UI.Page
    {
        public clsDAL objadmin = null;
        //public Cadmin objadminbase = null;
        string username = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                hdn_senderEmailId.Value = "";
                if (CheckSAMLCerti())
                //if (true)
                {
                    hdn_userId.Value = "0";
                    hdnLogoutpath.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["LogoutPath"]);
                    hdn_ClientType.Value = "Web";
                    hdn_SignalRSetup.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SignalRSetup"]);


                    if (!IsPostBack)
                    {
                        if (Request.QueryString.Count > 0)
                        {
                            string CalcName = AppDetails.GetAppName(Request.QueryString["calc"].ToString());
                            hdnNewTabCalc.Value = CalcName;
                        }
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Invalid token";
                }

            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = "not proper token";
            }
        }

        private bool CheckSAMLCerti()
        {
            //added hardcoded encodexml as calc not called through esignal
            //string encodedXml = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48c2FtbHA6UmVzcG9uc2UgeG1sbnM6c2FtbHA9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpwcm90b2NvbCIgRGVzdGluYXRpb249Imh0dHA6Ly93d3cudGVzdHNwLmNvbS9hc3NlcnRpb25jb25zdW1lciIgSUQ9Il85MjJiNTczYS0xOTcwLTQxNDgtYTRmMi1jYjVlZmE2ZGQ3NGUiIElzc3VlSW5zdGFudD0iMjAxNC0xMi0yM1QxNTozNjowM1oiIFZlcnNpb249IjIuMCI+CiAgICA8c2FtbDpJc3N1ZXIgeG1sbnM6c2FtbD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmFzc2VydGlvbiI+SW50ZXJhY3RpdmUgRGF0YSBDb3Jwb3JhdGlvbjwvc2FtbDpJc3N1ZXI+CiAgICA8U2lnbmF0dXJlIHhtbG5zPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjIj48U2lnbmVkSW5mbz48Q2Fub25pY2FsaXphdGlvbk1ldGhvZCBBbGdvcml0aG09Imh0dHA6Ly93d3cudzMub3JnLzIwMDEvMTAveG1sLWV4Yy1jMTRuIyIvPjxTaWduYXR1cmVNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjcnNhLXNoYTEiLz48UmVmZXJlbmNlIFVSST0iI185MjJiNTczYS0xOTcwLTQxNDgtYTRmMi1jYjVlZmE2ZGQ3NGUiPjxUcmFuc2Zvcm1zPjxUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3htbGRzaWcjZW52ZWxvcGVkLXNpZ25hdHVyZSIvPjwvVHJhbnNmb3Jtcz48RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxkc2lnI3NoYTEiLz48RGlnZXN0VmFsdWU+QWpwcE8xQys3Rk1YVlRyS1VRQjM3cE9iOU9jPTwvRGlnZXN0VmFsdWU+PC9SZWZlcmVuY2U+PC9TaWduZWRJbmZvPjxTaWduYXR1cmVWYWx1ZT5xbGxIcUNXNGZSb3hKTjFjQVg3ekIzRUhsdTVMQ3ZsWVRHTkM3OEc3Z1NrVmtzME9uQTZnTEZRZGJkbmJMaHcwT2FCS0VCRGJSNlVGCmt2ck1hUmlRcVlFSjgvaDB5NDR1RXdhdjE3UnhBRHVYV085cU1FeXZ3Yys0bDJqbW1KOEJEUmZRVG9GdHgyMkx0U0pVK0RuNDFnTzQKNTZoZU83NW1ZUDdmSGRuSVJackhtNEJvQ25LNy9veldGODBvR2pkMEJYT2dLUmFMSytZRXY4ZjVLTGRISENucFRpYlR3ZHhFakIxSgowZmswN1hJdmFSbERPR3UvRktSMTMwVW1QZ091SkpXKzlxMnhHYVZJV3VvQi9MQ1pFNzNvUlFYVnVUbFZHU21sMWFLejNrZmJxYmlzCnAzbjhVa2ZtUG9KTWNjOFFENExNbW5BTEptcHhQN2tFN2NXWlVBPT08L1NpZ25hdHVyZVZhbHVlPjwvU2lnbmF0dXJlPjxzYW1scDpFeHRlbnNpb25zPgogICAgICAgIDxSZXF1ZXN0ZWRSZXNvdXJjZVVSTCB4bWxucz0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmFzc2VydGlvbiI+TVE8L1JlcXVlc3RlZFJlc291cmNlVVJMPgogICAgPC9zYW1scDpFeHRlbnNpb25zPgogICAgPHNhbWxwOlN0YXR1cz4KICAgICAgICA8c2FtbHA6U3RhdHVzQ29kZSBWYWx1ZT0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOnN0YXR1czpTdWNjZXNzIi8+CiAgICA8L3NhbWxwOlN0YXR1cz4KICAgIDxzYW1sOkFzc2VydGlvbiB4bWxuczpzYW1sPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIiBJRD0iX2ZhZTc1OGEzLTE3ZWUtNGQ4Ni1iODdiLTQwY2E4N2MwYTM2MCIgSXNzdWVJbnN0YW50PSIyMDE0LTEyLTIzVDE1OjM2OjAzWiIgVmVyc2lvbj0iMi4wIj4KICAgICAgICA8c2FtbDpJc3N1ZXI+SW50ZXJhY3RpdmUgRGF0YSBDb3Jwb3JhdGlvbjwvc2FtbDpJc3N1ZXI+CiAgICAgICAgPHNhbWw6U3ViamVjdD4KICAgICAgICAgICAgPHNhbWw6TmFtZUlEIEZvcm1hdD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOm5hbWVpZC1mb3JtYXQ6dW5zcGVjaWZpZWQiIE5hbWVRdWFsaWZpZXI9IiIgU1BOYW1lUXVhbGlmaWVyPSIiPnVzZXJuYW1lPC9zYW1sOk5hbWVJRD4KICAgICAgICAgICAgPHNhbWw6U3ViamVjdENvbmZpcm1hdGlvbiBNZXRob2Q9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpjbTpiZWFyZXIiLz4KICAgICAgICA8L3NhbWw6U3ViamVjdD4KICAgICAgICA8c2FtbDpDb25kaXRpb25zIE5vdEJlZm9yZT0iMjAxNC0xMi0yM1QxNTozNjowM1oiIE5vdE9uT3JBZnRlcj0iMjAxNC0xMi0yM1QxNTo0MTowM1oiPgogICAgICAgIDwvc2FtbDpDb25kaXRpb25zPgogICAgICAgIDxzYW1sOkF1dGhuU3RhdGVtZW50IEF1dGhuSW5zdGFudD0iMjAxNC0xMi0yM1QxNTozNjowM1oiPgogICAgICAgICAgICA8c2FtbDpBdXRobkNvbnRleHQ+CiAgICAgICAgICAgICAgICA8c2FtbDpBdXRobkNvbnRleHRDbGFzc1JlZj51cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YWM6Y2xhc3Nlczp1bnNwZWNpZmllZDwvc2FtbDpBdXRobkNvbnRleHRDbGFzc1JlZj4KICAgICAgICAgICAgPC9zYW1sOkF1dGhuQ29udGV4dD4KICAgICAgICA8L3NhbWw6QXV0aG5TdGF0ZW1lbnQ+CiAgICA8L3NhbWw6QXNzZXJ0aW9uPgo8L3NhbWxwOlJlc3BvbnNlPg==";
            bool Isvalid = false;
            try
            {
                
                string encodedXml = Request.Form[0];
                
                // decode Base64 data
                LogUtility.Info("App.aspx.cs", "CheckSAMLCerti", "SAML Resopnse encrypted XML" + encodedXml);

                byte[] xmlData = Convert.FromBase64String(encodedXml);

                // load XML document
                var xmlstream = new MemoryStream(xmlData);
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.Load(xmlstream);

                if (SAMLValidator.IsValid(doc))
                {
                    // extract user name from XML
                    XmlElement usernameElem = (XmlElement)doc.GetElementsByTagName("saml:NameID")[0];
                    username = usernameElem.InnerText;
                    LogUtility.Info("App.aspx.cs", "CheckSAMLCerti", "eSignal User Name:" + username);
                    SaveUSerGroup();

                    // save session to a cookie
                    bool persistentCookie = false;
                    FormsAuthenticationTicket tkt;
                    string cookiestr;
                    HttpCookie ck;
                    tkt = new FormsAuthenticationTicket(1, username, DateTime.Now,
                    DateTime.Now.AddMinutes(30), persistentCookie, "");
                    cookiestr = FormsAuthentication.Encrypt(tkt);
                    ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                    if (persistentCookie)
                        ck.Expires = tkt.Expiration;
                    ck.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(ck);
                    Isvalid = true;
                }
                else
                {
                    lblErrorMsg.Text = "The certificate is invalid or wrong SAML response.";
                }
            }
            catch { }

            return Isvalid;
        }
       
        private void SaveUSerGroup()
        {
            //username = "esTest114";
            string UserEmail = username +  "_eSignal@thebeastapps.com";
            hdn_senderEmailId.Value = UserEmail;

            int userid = 0;
            DataSet ds = new DataSet();
            objadmin = new clsDAL(false);

            LogUtility.Info("App.aspx.cs", "SaveUSerGroup", "eSignal User Email:" + UserEmail);

            if (!string.IsNullOrEmpty(username))
            {
                
                
                ds = objadmin.CreateUserGroup(username, "", UserEmail, "Passw0rd", 1, 1, "eSignal");

                userid = Convert.ToInt32(ds.Tables[0].Rows[0]["UserID"]);

                if (ds.Tables[0].Rows[0]["MSG"] != null)
                {
                   LogUtility.Info("App.aspx.cs", "SaveUserGroup", " Message: " + ds.Tables[0].Rows[0]["MSG"].ToString() + "  UserID:" + userid);
                }

                //DataTable userlist = new DataTable();
                //DataTable usrdtl = new DataTable();
                //usrdtl.Columns.Add("userid", typeof(Int32));
                //DataRow dr = usrdtl.NewRow();
                //{
                //    dr["userid"] = userid;
                //    usrdtl.Rows.Add(dr);
                //}
                //////2002002845
                //int UserGroupID = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["eSignalUserGroupID"]);
                //objadmin.Submit_AppStore_UserIn_Group(UserGroupID, usrdtl, 1);
            }
        }

    }
}