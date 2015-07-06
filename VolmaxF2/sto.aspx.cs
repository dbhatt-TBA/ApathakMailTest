using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class sto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["RefNo"]))
        {
            string msgId = Convert.ToString(Request.QueryString["RefNo"]);
            switch (msgId)
            {
                case "-2":  //lnkExpired
                    lblMessage.Text = "The AutoUrl link you used is expired. Please contact helpdesk.";
                    break;

                case "-5":  //UnauthorizedIP
                    lblMessage.Text = "Your IP address is unauthorized. Please contact helpdesk.";
                    break;

                case "-3":  //lnkInvalid
                case "999992":
                    lblMessage.Text = "The AutoUrl link you used is invalid. Please contact helpdesk.";
                    break;

                case "-1":  //InvalidUser
                    lblMessage.Text = "User is invalid or outside the domain of the system. Please contact helpdesk.";
                    break;

                case "999991":
                    lblMessage.Text = "Your Session is timed-out. Please <a href=\"Signin.aspx\" target=\"_self\"> login </a> again.";
                    break;

                case "999990":
                    lblMessage.Text = "Your Login information is corrupted. Please <a href=\"Signin.aspx\" target=\"_self\"> login </a> again.";
                    break;
            }
        }
    }
}