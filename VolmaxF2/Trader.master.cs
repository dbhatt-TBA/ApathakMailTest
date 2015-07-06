using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VCM.Common.Log;
using VCM.Common;


public partial class Trader : System.Web.UI.MasterPage
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
        try
        {
            if (CurrentSession.User == null)
            {
                Response.Redirect("Signin.aspx", false);
                return;
            }

            if (!IsPostBack)
            {                              
                lblUserName.Text = CurrentSession.User.EmailID.ToString();               
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
           
        }
    }

    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Response.Redirect("Index.aspx");
    }
   
   
 

}

