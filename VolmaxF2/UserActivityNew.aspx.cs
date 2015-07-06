using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VCM.Common;
using System.Data;

public partial class UserActivityNew : System.Web.UI.Page
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
        if (CurrentSession.User == null)
        {
            Response.Redirect("Signout.aspx", false);
            return;
        }
        DataSet ds = new DataSet();
        DAL.clsDAL objDal = new DAL.clsDAL(false);
        ds = objDal.GetVendorID(Convert.ToString(CurrentSession.User.UserID));
        if (ds.Tables[0].Rows.Count > 0)
        {
            VendorID.Value = Convert.ToString(ds.Tables[0].Rows[0]["UserGroupId"]);
            if (Convert.ToString(ds.Tables[0].Rows[0]["IsTBAAdmin"]) == "1")
            {
                IsTBAAdmin.Value = "-1";
            }

        }
        else
        {
            Response.Redirect("Signout.aspx", false);
            return;
        }

        userid.Value = Convert.ToString(Convert.ToString(CurrentSession.User.UserID));
    }
}