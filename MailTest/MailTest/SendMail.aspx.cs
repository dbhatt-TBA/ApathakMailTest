using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using VCM.Common.Log;

namespace MailTest
{
    public partial class SendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                string strCC = txtTo.Text;
                string strSubject = txtSubject.Text;
                string strBody = txtBody.Text;

                string strIpAddress = "";

                if (HttpContext.Current.Request.Headers["X-Forwarded-For"] != null)
                {
                    //HttpContext.Current.Request.Headers["X-Forwarded-For"].Split(new char[] { ',' }).FirstOrDefault();  
                    strIpAddress = "<br/><br/>[#] Attrib:X-Forwarded-For - found / ELB=true <br/>[#] Ip Address = " + HttpContext.Current.Request.Headers["X-Forwarded-For"].Split(',')[0];
                }
                else
                {
                    string strHostName = "";
                    strHostName = System.Net.Dns.GetHostName();

                    IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

                    IPAddress[] addr = ipEntry.AddressList;

                    //strIpAddress = "<br/><br/>[#] Attrib:X-Forwarded-For - NOT found / ELB=false <br/>[#] Ip Address = " + addr[1].ToString();

                    strIpAddress = Request.UserHostAddress;
                }

                strBody += strIpAddress;

                string strFrom = string.IsNullOrEmpty(txtFrom.Text.Trim()) ? System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString() : txtFrom.Text;
                string strTo = string.IsNullOrEmpty(txtTo.Text.Trim()) ? System.Configuration.ConfigurationManager.AppSettings["ToEmail"].ToString() : txtTo.Text;

                VCM_Mail objMail = new VCM_Mail(ddlSmtpServers.SelectedIndex);
                objMail.From = strFrom;
                objMail.To = strTo;

                objMail.Subject = strSubject;
                objMail.Body = strBody;
                objMail.IsBodyHtml = true;
                objMail.SendAsync = true;
                objMail.SendMail();

                lblMessage.Text = "Mail sent";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message + "<br/>";
                if (ex.InnerException != null)
                {
                    lblMessage.Text += ex.InnerException.Message + "<br/>";
                    if (ex.InnerException.InnerException != null)
                    {
                        lblMessage.Text += ex.InnerException.InnerException.Message;
                    }
                }
                lblMessage.ForeColor = System.Drawing.Color.Red;
                //throw;
            }
        }
    }
}