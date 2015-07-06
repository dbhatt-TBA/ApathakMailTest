using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for SessionInfo
/// </summary>
public class SessionInfo
{
    private HttpSessionState UserSession;

    public SessionInfo()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SessionInfo(HttpSessionState session)
    {
        this.UserSession = session;
    }

    public void ClearSession()
    {
        HttpContext.Current.Session.Clear();
    }

    #region User
    private const string keyUser = "UserInfo";
    public virtual SiteUserInfo User
    {
        get
        {
            return (SiteUserInfo)UserSession[keyUser];
        }
        set
        {
            UserSession[keyUser] = value;
        }
    }
    #endregion

}
