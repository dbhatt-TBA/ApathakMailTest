using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VCM.Common;
using VCM.Common.Log;

namespace OpenF2.handler
{
    /// <summary>
    /// Summary description for loghandler
    /// </summary>
    public class loghandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string pageName = context.Request.QueryString["pageName"].ToString();
                string description = context.Request.QueryString["desc"].ToString();
                string methodName = context.Request.QueryString["methodName"].ToString();

                if (methodName == "1")
                    JavaScriptErrorLogs(pageName, description);
                else
                    JavaScriptActivitiesLogs(pageName, methodName, description);

                context.Response.ContentType = "text/plain";
                context.Response.Write("successsss");
            }
            catch (Exception ex)
            {
                LogUtility.Error("loghandler", "ProcessRequest", ex.Message, ex);
            }
        }

        public void JavaScriptErrorLogs(string pageName, string description)
        {
            // if (CurrentSession.User != null)
            {
                //string JSONJavaScriptLogSubmit = string.Empty;
                try
                {
                    LogUtility.Error(pageName, description);
                    //JSONJavaScriptLogSubmit = "[{'IsSave': 'TRUE'}]";
                }
                catch (Exception ex)
                {
                    LogUtility.Error("loghandler", "JavaScriptErrorLogs", ex.Message, ex);
                }
                //finally
                //{
                //}
                // return JSONJavaScriptLogSubmit;
            }
        }


        public void JavaScriptActivitiesLogs(string pageName,string methodName, string description)
        {
                try
                {
                    LogUtility.Info(pageName, methodName, description);
                }
                catch (Exception ex)
                {
                    LogUtility.Error("loghandler", "JavaScriptActivitiesLogs", ex.Message, ex);
                }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}