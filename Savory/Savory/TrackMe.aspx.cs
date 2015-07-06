using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savory
{
    public partial class TrackMe : System.Web.UI.Page
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
                hdn_userId.Value = Convert.ToString(CurrentSession.User.UserID); ;
                hdn_custId.Value = Convert.ToString(CurrentSession.User.CustID);
                hdn_senderEmailId.Value = Convert.ToString(CurrentSession.User.EmailID);
                hdn_ClientType.Value = "Web";
                hdn_AuthToken.Value = Session["AuthToken"].ToString();

            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}