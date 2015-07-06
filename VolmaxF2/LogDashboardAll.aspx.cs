using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VCM.Common;
using System.Data;
using System.Web.UI.HtmlControls;
using VcmUserNamespace;
using VCM.Common.Log;
using VCM.Common;

public partial class LogDashboardAll : System.Web.UI.Page
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
