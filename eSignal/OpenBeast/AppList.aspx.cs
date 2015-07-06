using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OpenBeast
{
    public partial class AppList : System.Web.UI.Page
    {
        //public DAL objadmin = null;
        //public Cadmin objadminbase = null;
        // dummy change
        //private SessionInfo _session;
        //public SessionInfo CurrentSession
        //{
        //    get
        //    {
        //        if (_session == null)
        //        {
        //            _session = new SessionInfo(HttpContext.Current.Session);
        //        }
        //        return _session;

        //    }
        //    set
        //    {
        //        _session = value;
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                CheckValidUser();
                hdn_userId.Value = "0";

                hdnLogoutpath.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["LogoutPath"]);
                hdn_ClientType.Value = "Web";
                hdn_SignalRSetup.Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SignalRSetup"]);


                if (!IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        hdnNewTabCalc.Value = Request.QueryString["calc"].ToString();

                    }

                }



            }
            catch (Exception ee)
            {
                Response.Redirect("Index.aspx");
            }
        }

        private void CheckValidUser()
        {
            if (Session["SDKUser"] != null && Session["SDKPassword"] != null)
            {
                //if (Convert.ToString(Session["SDKUser"]) == Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SDKUserEmail"])  && Convert.ToString(Session["SDKPassword"]) == Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SDKPassword"]))
                if (Convert.ToString(Session["SDKUser"]) != string.Empty && Convert.ToString(Session["SDKPassword"]) != string.Empty)
                {
                    lblLoggedInUser.Text = "Hi," + Convert.ToString(Session["SDKUser"]);
                }
                else
                {
                    Response.Redirect("Index.aspx");
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }
}