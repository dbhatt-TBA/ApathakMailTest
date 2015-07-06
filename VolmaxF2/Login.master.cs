using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VCM.Common.Log;
using VCM.Common;

public partial class Login : System.Web.UI.MasterPage
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
       

    }


   
}
