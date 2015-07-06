using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;


public class Global : System.Web.HttpApplication
{

    protected void Application_Start(object sender, EventArgs e)
    {
        /* SignalR BufferSize setting. Checked for Excel Cusip submission*/
        GlobalHost.Configuration.DefaultMessageBufferSize = 12000; // Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SignalRBufferSize"]);

        //GlobalHost.HubPipeline.EnableAutoRejoiningGroups();
        //RouteTable.Routes.MapHubs();
    }

    protected void Session_Start(object sender, EventArgs e)
    {

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        //this.Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
    }

    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {

    }

    protected void Application_Error(object sender, EventArgs e)
    {
        try
        {
            HttpException lastErrorWrapper = Server.GetLastError() as HttpException;

            Exception lastError = lastErrorWrapper;
            if (lastErrorWrapper.InnerException != null)
                lastError = lastErrorWrapper.InnerException;

            string lastErrorTypeName = lastError.GetType().ToString();
            string lastErrorMessage = lastError.Message;
            string lastErrorStackTrace = lastError.StackTrace;
            string rowURL = Request.RawUrl;

            if (lastErrorMessage != "File does not exist.")
            {
                string errorMessage = lastErrorTypeName + "</br>" + lastErrorMessage + "</br>" + lastErrorStackTrace + "</br>" + rowURL;
                UtilityHandler.SendEmailForError("Global.asax.cs:: Application_Error() :: " + errorMessage.ToString());
            }
        }
        catch (Exception ee)
        {

        }
    }

    protected void Session_End(object sender, EventArgs e)
    {

    }

    protected void Application_End(object sender, EventArgs e)
    {

    }
}
