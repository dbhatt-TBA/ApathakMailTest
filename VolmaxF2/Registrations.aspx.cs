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

public partial class Registrations : System.Web.UI.Page
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
                FillSIF();
                FillVendor();
                FillCategory();
                Get_Register_List();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "Registrations", "Page_Load()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
                LogUtility.Error("Registrations", "Page_Load", ex.Message, ex);
            }
        }
    }

    protected void GridRegister_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GridRegister_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridRegister.Rows[index];
            ListItem item = new ListItem();

            HiddenField hdRegId = (HiddenField)GridRegister.Rows[index].FindControl("hdf_RegID");
            string RegID = hdRegId.Value;
            string SIDId = Server.HtmlDecode(row.Cells[3].Text);

            DataTable dTable = new DataTable();
            dTable = VcmUserNamespace.cUserDbHandler.FillRegisterList(RegID, SIDId);

            if (dTable.Rows.Count > 0)
            {
                lblSIFID.Visible = true;
                drpSIFID.Visible = false;

                hdnEditRegID.Value = RegID;

                txtName.Text = Convert.ToString(dTable.Rows[0]["AppName"]);
                lblSIFID.Text = Convert.ToString(dTable.Rows[0]["BeastImageSID"]);
                txtTitle.Text = Convert.ToString(dTable.Rows[0]["AppTitle"]);
                txtVersion.Text = Convert.ToString(dTable.Rows[0]["AppVersion"]);
                txtDescription.Text = Convert.ToString(dTable.Rows[0]["AppDescription"]);
                drpCategory.SelectedValue = Convert.ToString(dTable.Rows[0]["CategoryId"]);
                txtSupprotOS.Text = Convert.ToString(dTable.Rows[0]["AppSupportedOS"]);
                txtFileSize.Text = Convert.ToString(dTable.Rows[0]["AppFileSize"]);
                txtPrice.Text = Convert.ToString(dTable.Rows[0]["AppPrice"]);
                txtTag.Text = Convert.ToString(dTable.Rows[0]["AppTags"]);
                txtSupprotLanguage.Text = Convert.ToString(dTable.Rows[0]["AppSupportedLanguage"]);
                txtEmail.Text = Convert.ToString(dTable.Rows[0]["Email"]);
                txtContactNumber.Text = Convert.ToString(dTable.Rows[0]["ContactNumber"]);
                drpVendor.SelectedValue = Convert.ToString(dTable.Rows[0]["VendorId"]);
                txtVendorInfo.Text = Convert.ToString(dTable.Rows[0]["AppVendorInformation"]);
                txtRealeaseDate.Text = Convert.ToDateTime(dTable.Rows[0]["AppReleasedDate"]).ToString("MM/dd/yyyy");
                txtAgeCriteria.Text = Convert.ToString(dTable.Rows[0]["AppAgeCriteria"]);
                txtSupportDevice.Text = Convert.ToString(dTable.Rows[0]["AppSupportedDevices"]);
                txtCurrency.Text = Convert.ToString(dTable.Rows[0]["Currency"]);
                txtPackageURL.Text = Convert.ToString(dTable.Rows[0]["AppPackageURL"]);
                txtSupprotURL.Text = Convert.ToString(dTable.Rows[0]["SupportURL"]);
                txtYoutubeURL.Text = Convert.ToString(dTable.Rows[0]["YouTubeURL"]);
                txtFilePath.Text = Convert.ToString(dTable.Rows[0]["AppFilePath"]);
                txtName.Focus();

            }


        }
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblSIFID.Visible == true)
            {
                int regid = Convert.ToInt32(hdnEditRegID.Value);
                cUserDbHandler.InsertUpdateRegistration(regid, txtName.Text.Trim(), txtTitle.Text.Trim(), Convert.ToInt32(CurrentSession.User.UserID), txtDescription.Text.Trim(),
                                                    txtVersion.Text.Trim(), drpCategory.SelectedIndex, txtSupprotOS.Text.Trim(), txtFileSize.Text.Trim(),
                                                    txtPrice.Text.Trim(), txtTag.Text.Trim(), txtSupprotLanguage.Text.Trim(), txtEmail.Text.Trim(),
                                                    txtContactNumber.Text.Trim(), "N", txtPackageURL.Text.Trim(), txtCurrency.Text.Trim(),
                                                    txtSupprotLanguage.Text.Trim(), txtYoutubeURL.Text.Trim(), txtFilePath.Text.Trim(),
                                                    txtSupportDevice.Text.Trim(), Convert.ToInt32(drpVendor.SelectedValue), Convert.ToDateTime(txtRealeaseDate.Text.Trim()),
                                                    txtAgeCriteria.Text.Trim(), txtVendorInfo.Text.Trim(), Convert.ToInt32(lblSIFID.Text), 0, Convert.ToBoolean(drfIsShareable.SelectedValue), Convert.ToInt32(drfShareableMin.SelectedValue));
                GridRegister.EditIndex = -1;
            }
            else
            {
                int regid = -1;
                cUserDbHandler.InsertUpdateRegistration(regid, txtName.Text.Trim(), txtTitle.Text.Trim(), Convert.ToInt32(CurrentSession.User.UserID), txtDescription.Text.Trim(),
                                                     txtVersion.Text.Trim(), drpCategory.SelectedIndex, txtSupprotOS.Text.Trim(), txtFileSize.Text.Trim(),
                                                     txtPrice.Text.Trim(), txtTag.Text.Trim(), txtSupprotLanguage.Text.Trim(), txtEmail.Text.Trim(),
                                                     txtContactNumber.Text.Trim(), "N", txtPackageURL.Text.Trim(), txtCurrency.Text.Trim(),
                                                     txtSupprotLanguage.Text.Trim(), txtYoutubeURL.Text.Trim(), txtFilePath.Text.Trim(),
                                                     txtSupportDevice.Text.Trim(), Convert.ToInt32(drpVendor.SelectedValue), Convert.ToDateTime(txtRealeaseDate.Text.Trim()),
                                                     txtAgeCriteria.Text.Trim(), txtVendorInfo.Text.Trim(), Convert.ToInt32(drpSIFID.SelectedValue), 0, Convert.ToBoolean(drfIsShareable.SelectedValue), Convert.ToInt32(drfShareableMin.SelectedValue));
            }

            Clear();
        }
        catch (Exception ex)
        {
            lblErrMsg.Text = ex.Message.ToString();
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "Registrations", "btnSubmitUser_Click()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Registrations", "btnSubmitUser_Click", ex.Message, ex);
        }

    }

    public void Get_Register_List()
    {
        try
        {
            DataTable dTable = VcmUserNamespace.cUserDbHandler.FillRegisterList("0", "0");
            GridRegister.DataSource = dTable;

            GridRegister.DataBind();
        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), Convert.ToString(CurrentSession.User.MOrgDate), Convert.ToString(CurrentSession.User.LocationDesc), "Registrations", "Get_Customer_List()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Registrations", "Get_Register_List", ex.Message, ex);
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
            drpVendor.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        catch (Exception ex)
        {
            //VcmLogManager.Log.writeLog("", "", "", "Signin", "UserValidation()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Registrations", "Fill Vendor in Registration", ex.Message, ex);
        }
    }

    private void FillCategory()
    {
        try
        {
            DataTable dtCategory = cUserDbHandler.CategoryList();
            drpCategory.DataSource = dtCategory;
            drpCategory.DataValueField = dtCategory.Columns[0].ToString();
            drpCategory.DataTextField = dtCategory.Columns[1].ToString();
            drpCategory.DataBind();
            drpCategory.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        catch (Exception ex)
        {
            //VcmLogManager.Log.writeLog("", "", "", "Signin", "UserValidation()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("Registrations", "Fill Category in Registration", ex.Message, ex);
        }
    }

    private void FillSIF()
    {
        try
        {
            DataTable dtSIF = cUserDbHandler.SIFList();
            drpSIFID.DataSource = dtSIF;
            drpSIFID.DataValueField = dtSIF.Columns[0].ToString();
            drpSIFID.DataTextField = dtSIF.Columns[1].ToString();
            drpSIFID.DataBind();
            drpSIFID.Items.Insert(0, new ListItem("---Select---", "0"));
        }
        catch (Exception ex)
        {
            LogUtility.Error("Registrations", "Fill SIFID in Registration", ex.Message, ex);
        }
    }

    private void Clear()
    {
        txtName.Text = "";
        drpSIFID.Visible = true;
        lblSIFID.Text = "";
        lblSIFID.Visible = false;
        txtTitle.Text = "";
        txtVersion.Text = "";
        txtDescription.Text = "";
        drpCategory.SelectedIndex = 0;
        txtSupprotOS.Text = "";
        txtFileSize.Text = "";
        txtPrice.Text = "";
        txtTag.Text = "";
        txtSupprotLanguage.Text = "";
        txtEmail.Text = "";
        txtContactNumber.Text = "";
        drpVendor.SelectedIndex = 0;
        txtVendorInfo.Text = "";
        txtRealeaseDate.Text = "";
        txtAgeCriteria.Text = "";
        txtSupportDevice.Text = "";
        txtCurrency.Text = "";
        txtPackageURL.Text = "";
        txtSupprotURL.Text = "";
        txtYoutubeURL.Text = "";
        txtFilePath.Text = "";
        FillSIF();
        Get_Register_List();
        txtName.Focus();
    }

}