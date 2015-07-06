
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace OpenF2
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
            {

                //string vFullURL = Context.Request.Url.ToString();
                //string vScheme = Context.Request.Url.Scheme;
                string vRawUrl = Context.Request.RawUrl;
                string vHost = new Uri(Context.Request.Url.ToString()).Host;
                string vReqUrl = "https://www.thebeastapps.com" + vRawUrl;
                //string vPort = new Uri(Context.Request.Url.ToString()).Port.ToString();            
                //string vIPAddress = Context.Request.UserHostAddress;            

                //VCM_Mail objMail = new VCM_Mail();
                //objMail.From = "thebeast@thebeastapps.com";
                //objMail.To = "apathak@thebeastapps.com";
                //objMail.Subject = "BeastApps(Test) URL redirect info";
                //string mailBody = "<html><head></head><body><div style='font-family:Verdana Calibri;'>URL Information:<br/>"
                //    + "<br/>Request URL: " + vFullURL
                //    + "<br/>Scheme: " + vScheme
                //    + "<br/>Raw URL: " + vRawUrl
                //    + "<br/>Host: " + vHost
                //    + "<br/>Port: " + vPort
                //    + "<br/>New URL: " + vReqUrl
                //    + "</div></body></html>";

                //objMail.IsBodyHtml = true;
                //objMail.Body = mailBody;

                if (vHost.ToLower() != "www.thebeastapps.com")
                {
                    //objMail.SendMail();                

                    Context.Response.Clear();
                    Context.Response.Status = "301 Moved Permanently";
                    Context.Response.AddHeader("Location", vReqUrl);
                    Context.Response.End();
                }
            }
            //objMail = null;
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}