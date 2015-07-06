using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.ComponentModel;
using System.Net.Mail;
using System.Security.Cryptography;
using System.IO;
using System.Collections;
using System.Net;
using VCM.Common.Log;
using OpenF2.OpenBeastService;
using System.Reflection;

public class Enums
{

    public enum RegistrationMsg
    {
        [Description("This email id is already associated with another account. Do you wish to use the same for CMEICAP services ? ")]
        MessageA = 1,   // When UserID already exist with other vendor and set as CMEICAP Subscription

        [Description("Currently your free trial period is live. <a href='CMEPayment.aspx'>Click here</a> to go for paid version.")]
        MessageB = 2,   // User is already exist of CMEICAP & Free Period is live.

        [Description("This email id is already associated with another account. Do you wish to use the same for CMEICAP services ? ")]
        MessageC = 3,   // User is already exist of CMEICAP & Free Period is used.

        [Description("This email id is already associated with another account. Do you wish to use the same for CMEICAP services ? ")]
        MessageD = 4,   // User is already exist with using Paid Version.

        [Description("This email id is already associated with another account. Do you wish to use the same for CMEICAP services ? ")]
        MessageE = 5    // User is already exist & Used Payment period & for Renewal.
    }


    public enum VendorName
    {
        BeastApps = 1,
        Numerix = 2,
        CMEICAP = 3
    }

    public enum ValidationFlag
    {
        Success = 0,
        AddUserToGroup = 1,
        RemoveUserFromGroup = 0,
        UserNotFound = -2,
        UserLockedOutByHelpDesk = -8,
        PasswordWrongUserLockedOut = -5,
        PasswordWrongLastTime = -4,
        PasswordWrong = -3,
        PasswordMustChange = -6,
        MaxLoginExceeded = -7
    }
    
}
