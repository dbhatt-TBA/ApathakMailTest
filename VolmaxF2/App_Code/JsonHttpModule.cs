using System;
using System.Web;

    public class JsonHttpModule : IHttpModule
    {
        private string JSON_CONTENT_TYPE = "application/jsonp; charset=utf-8";

        #region IHttpModule Members
        public void Dispose()
        {
        }
        public void Init(HttpApplication app)
        {
            app.BeginRequest += OnBeginRequest;
            app.ReleaseRequestState += OnReleaseRequestState;
        }
        #endregion
        public void OnBeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpRequest resquest = app.Request;
            if (!resquest.Url.AbsolutePath.Contains("Service.asmx")) 
                return;
            if (string.IsNullOrEmpty(app.Context.Request.ContentType))
            {
                app.Context.Request.ContentType = JSON_CONTENT_TYPE;
            }
        }
        public void OnReleaseRequestState(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpResponse response = app.Response;
            if (app.Context.Request.ContentType != JSON_CONTENT_TYPE) 
                return;

             response.ContentType = "application/jsonp; charset=utf-8";

             response.Filter = new JsonResponseFilter(response.Filter);
        }
    }
