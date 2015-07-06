using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VCM.Common.Log;
using VCM.Common;
using System.Data;
using System.Data.SqlClient;
using VcmUserNamespace;

public partial class Apps : System.Web.UI.Page
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
        if (CurrentSession.User.IsTrader == "TRUE")
        {
            string script = @"setMessage();";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "setM", script, true);
            return;
        }

        if (hdnCmeBackground.Value == "1")
            tblSelect.Attributes.Add("class", "IcapCmeWatermark1");
        else
            tblSelect.Attributes.Add("class", "");

        if (!IsPostBack)
        {
            try
            {
                hdnUserID.Value = Convert.ToString(CurrentSession.User.UserID);
                FillVendor();
                Get_Apps_List(Convert.ToInt32(drpVendor.SelectedValue));
            }
            catch (Exception ex)
            {
                VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "Apps", "Page_Load()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
                LogUtility.Error("Apps", "Page_Load", ex.Message, ex);
            }
        }
    }


    public void Get_Apps_List(int VendorID)
    {
        try
        {
            DataTable dTable = VcmUserNamespace.cUserDbHandler.FillAppsList(VendorID);
            GridApps.DataSource = dTable;

            GridApps.DataBind();
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "Apps", "Get_Customer_List()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Apps", "Get_Apps_List", ex.Message, ex);
        }
    }

    private void FillVendor()
    {
        try
        {
            DataTable dtVendor = cUserDbHandler.VendorList();
            drpVendor.DataSource = dtVendor;
            drpVendor.DataValueField = dtVendor.Columns[0].ToString();
            drpVendor.DataTextField = dtVendor.Columns[1].ToString();
            drpVendor.DataBind();
            drpVendor.Items.Insert(0, new ListItem("---All---", "0"));
        }
        catch (Exception ex)
        {
            //VcmLogManager.Log.writeLog("", "", "", "Signin", "UserValidation()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Apps", "Fill Vendor in Registration", ex.Message, ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Get_Apps_List(Convert.ToInt32(drpVendor.SelectedValue));
        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message.ToString();
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "Apps", "btnSearch_Click()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Apps", "btnSearch_Click", ex.Message, ex);
        }

    }
}