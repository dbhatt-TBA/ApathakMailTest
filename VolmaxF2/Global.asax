<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
        {           
            string vRawUrl = Context.Request.RawUrl;
            string vHost = new Uri(Context.Request.Url.ToString()).Host;
            string vReqUrl = "https://www.thebeastapps.com" + vRawUrl;
            
            if (vHost.ToLower() != "www.thebeastapps.com")
            {               
                Context.Response.Clear();
                Context.Response.Status = "301 Moved Permanently";
                Context.Response.AddHeader("Location", vReqUrl);
                Context.Response.End();
            }
        }      
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
