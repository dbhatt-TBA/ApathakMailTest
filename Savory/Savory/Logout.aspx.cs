using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savory.OpenBeastService;
using System.Net;
using VCM.Common.Log;
using System.Net;

namespace Savory
{
    public partial class Logout : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            OpenBeastService.Service ws = new OpenBeastService.Service();
            //Set Cookie
            if (!IsPostBack)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
                {
                    Cookie newCookie = new Cookie();

                    ws.CookieContainer = new System.Net.CookieContainer();
                    newCookie.Name = HttpContext.Current.Request.Cookies.Get("AWSELB").Name;
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Name newCookieName:" + newCookie.Name);

                    newCookie.Domain = "thebeastapps.com";
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Value newCookieDomain:" + newCookie.Domain);

                    newCookie.Expires = HttpContext.Current.Request.Cookies.Get("AWSELB").Expires;
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Value newCookieExpires:" + newCookie.Expires);


                    newCookie.HttpOnly = HttpContext.Current.Request.Cookies.Get("AWSELB").HttpOnly;
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Value newCookieHttpOnly:" + newCookie.HttpOnly);

                    newCookie.Path = HttpContext.Current.Request.Cookies.Get("AWSELB").Path;
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Value newCookiePath:" + newCookie.Path);

                    newCookie.Secure = HttpContext.Current.Request.Cookies.Get("AWSELB").Secure;
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Value newCookieSecure:" + newCookie.Secure);

                    newCookie.Value = HttpContext.Current.Request.Cookies.Get("AWSELB").Value;
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Set Value newCookieValue:" + newCookie.Value);

                    ws.CookieContainer.Add(newCookie);
                    LogUtility.Info("UserListWithToken.aspx", "UserValidation", "Add into Container");
                }
            }
            if (CurrentSession.User != null)
            {
                ws.DisableToken(Convert.ToString(Session["AuthToken"]), Convert.ToString(CurrentSession.User.UserID), "Web");
            }
            
            CurrentSession.User = null;
            CurrentSession.ClearSession();

            HttpCookie userCk;

            if (Request.Cookies["VCMSITE"] != null)
            {
                userCk = new HttpCookie("VCMSITE");
                userCk.Expires = DateTime.Now.AddDays(-5);
                Response.Cookies.Add(userCk);
                Request.Cookies.Remove("VCMSITE");
            }
            if (Request.Cookies["VCMCME"] != null)
            {
                userCk = new HttpCookie("VCMCME");
                userCk.Expires = DateTime.Now.AddDays(-5);
                Response.Cookies.Add(userCk);
                Request.Cookies.Remove("VCMCME");
            }

            Response.Redirect("Default.aspx");
        }
    }
}