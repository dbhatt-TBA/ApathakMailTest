using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VCM.Common;

public partial class GetLastLogs : System.Web.UI.Page
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
}