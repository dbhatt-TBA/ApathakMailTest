using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Savory
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void createAccount_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DAL objDAL = new DAL(0);
            //    DataTable dt = objDAL.RegisterUser(emailid.Text, firstname.Text, lastname.Text, pwd.Text);
            //    if (Convert.ToInt32(dt.Rows[0]["Msg_ID"]) > 1)
            //    {
            //        DAL objSessionDAL = new DAL(2);
            //        objSessionDAL.RegisterUserDetail(Convert.ToInt32(dt.Rows[0]["UserId"]), firstname.Text, lastname.Text, phonenumber.Text, mobilenumber.Text, Convert.ToDateTime(month.SelectedValue + "/" + day.SelectedValue + "/1900"), Convert.ToDateTime(aniMonth.SelectedValue + "/" + aniDay.SelectedValue + "/1900"), (radioconform.SelectedValue == "Yes") ? true : false, (checkEmail.Checked) ? true : false, (checkText.Checked) ? true : false);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    UtilityHandler.SendEmailForError("Default.aspx :: createAccount_Click() :: " + ex.Message.ToString());
            //}

        }

        protected void btn_createAccount_Click(object sender, EventArgs e)
        {

        }
    }
}