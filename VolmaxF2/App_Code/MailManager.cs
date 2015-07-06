using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using VCM.Common.Log;

/// <summary>
/// Summary description for MailManager
/// </summary>
public class MailManager
{
    public enum MailType
    {
        Login_Success = 0,
        Login_AccountLocked = 1,
        Login_AccountBlocked = 2,
        Login_OutOfDomainAccess = 3,
        Login_IPUnauthorized = 4,
        Login_UserNotRegistered = 5,
        Login_AccountLockedByWrongPwd = 6,
        User_ResetPassword = 7
    }

    MailType _type;
    string _msgSubject, _msgBody, _mailFrom, _mailTo;
    string _targetUserEmailId, _targetUserIpAddress, _targetUserClient, _targetUserPwd;
    string _geoOrg, _geoCity, _geoCountry, _loginTime;

    string _defaultImpMail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
    string _defaultInternalMail = System.Configuration.ConfigurationManager.AppSettings["InternalEmail"];

    string strMsgTemplate_success = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Admin,<br/><br/>User [USEREMAIL] has successfully logged in on [LOGINTIME] (GMT)."
                                    + "<table style=font-size:8pt;color:navy;font-family:Verdana>"
                                    + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Client: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[CLIENT] </td></tr>"
                                    + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[IPADDRESS] </td></tr>"
                                    + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Organization: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[ORGANIZATION] </td></tr>"
                                    + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>City: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[CITY] </td></tr>"
                                    + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Country: </td><td  width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[COUNTRY] </td></tr>"
                                    + "</table>"
                                    + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

    string strMsgTemplate_fail = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Admin,<br/><br/>User [USEREMAIL] was not able to log in on [LOGINTIME] (GMT)."
                                + "<table style=font-size:8pt;color:navy;font-family:Verdana>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Cause: </td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[REASON]</td></tr>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Client: </td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[CLIENT]</td></tr>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>IP Address: </td><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[IPADDRESS] </td></tr>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Organization: </td><td width=50%  style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[ORGANIZATION] </td></tr>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>City: </td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[CITY] </td></tr>"
                                + "<tr><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>Country: </td><td width=50% style=FONT-SIZE: 8pt; FONT-FAMILY: Verdana;>[COUNTRY] </td></tr>"
                                + "</table>"
                                + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

    string strMsgTemplate_acBlock = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Customer,"
                                    + "<br/><br/>[FAILMESSAGE]"
                                    + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

    string strMsgTemplate_acLockByWrongPwd = "<div style=font-size:8pt;color:navy;font-family:Verdana>Dear Customer,"
                                    + "<br/><br/>[FAILMESSAGE]"
                                    + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";

    string strMsgTemplate_pwdReset = "<div style=font-size:9pt;color:navy;font-family:Verdana>Dear Customer,"
                                    + "<br/><br/>Your password has been reset.<br/><br/>"
                                    + "<table style=font-size:9pt;color:navy;font-family:Verdana>"
                                    + "<tr><td>Login : </td><td>[USEREMAIL]</td></tr>"
                                    + "<tr><td>Password : </td><td>[USERPASSWORD]</td></tr>"
                                    + "<tr><td>IP Address : </td><td>[IPADDRESS]</td></tr>"
                                    + "<tr><td>Date and Time : </td><td>[LOGINTIME]</td></tr>"
                                    + "</table>"
                                    + "<br/><br/>You may now login using your new password."
                                    + "<br/><br/>If you did not ask to reset your password, please goto <a href=[BEASTURL]>[BEASTURL]</a> "
                                    + " click on \"THE APP STORE\" and click on the link \"Forgot Password?\" to change it;"
                                    + " and inform us at <a href=mailto:[INFOMAIL]> [INFOMAIL] </a>. If it was you, please ignore this email notification."
                                    + "<br/><br/>" + UtilityHandler.VCM_MailAddress_In_Html.ToString() + "</div>";


    public MailManager(MailType pType, string pUserEmail, string pUserIpAddress, string pClient)
    {
        try
        {
            LogUtility.Info("MailManager.cs", ":MailManager():", "Constructor:Type[" + (int)pType + "],Email[" + pUserEmail + "],Ip[" + pUserIpAddress + "]");

            this._type = pType;
            this._targetUserEmailId = pUserEmail;
            this._targetUserIpAddress = pUserIpAddress;
            this._targetUserClient = pClient;

            DataSet dsGeoIP = new DataSet();
            dsGeoIP = vcmProductNamespace.cDbHandler.VCM_AutoURL_GeoIP_Info(pUserIpAddress);
            this._geoCity = dsGeoIP.Tables[0].Rows[0][4].ToString();
            this._geoOrg = dsGeoIP.Tables[0].Rows[0][2].ToString();
            this._geoCountry = dsGeoIP.Tables[0].Rows[0][3].ToString();

            this._loginTime = VcmUserNamespace.cUserDbHandler.GetUtcSqlServerDate();

            switch (pType)
            {
                case MailType.Login_Success:
                    _mailTo = GetDefaultMailAddress(_targetUserEmailId);
                    _msgSubject = "User Login Notification";
                    _msgBody = strMsgTemplate_success;
                    _msgBody = _msgBody.Replace("[USEREMAIL]", _targetUserEmailId);
                    _msgBody = _msgBody.Replace("[LOGINTIME]", _loginTime);
                    _msgBody = _msgBody.Replace("[CLIENT]", _targetUserClient);
                    _msgBody = _msgBody.Replace("[IPADDRESS]", pUserIpAddress);
                    _msgBody = _msgBody.Replace("[ORGANIZATION]", _geoOrg);
                    _msgBody = _msgBody.Replace("[CITY]", _geoCity);
                    _msgBody = _msgBody.Replace("[COUNTRY]", _geoCountry);
                    break;

                case MailType.Login_AccountLocked:
                    _mailTo = GetDefaultMailAddress(_targetUserEmailId);
                    _msgSubject = "User Login Notification";
                    _msgBody = strMsgTemplate_fail;
                    _msgBody = _msgBody.Replace("[USEREMAIL]", _targetUserEmailId);
                    _msgBody = _msgBody.Replace("[LOGINTIME]", _loginTime);
                    _msgBody = _msgBody.Replace("[REASON]", "The user's account was locked.");
                    _msgBody = _msgBody.Replace("[CLIENT]", _targetUserClient);
                    _msgBody = _msgBody.Replace("[IPADDRESS]", pUserIpAddress);
                    _msgBody = _msgBody.Replace("[ORGANIZATION]", _geoOrg);
                    _msgBody = _msgBody.Replace("[CITY]", _geoCity);
                    _msgBody = _msgBody.Replace("[COUNTRY]", _geoCountry);
                    break;

                case MailType.Login_AccountBlocked:
                    _msgSubject = "Account Blocked";

                    _mailTo = pUserEmail.Trim();

                    _msgBody = strMsgTemplate_acBlock;
                    _msgBody = _msgBody.Replace("[FAILMESSAGE]", "Your account has been locked. To unlock your account or if this is an error, please contact us.");
                    break;

                case MailType.Login_OutOfDomainAccess:
                    _mailTo = GetDefaultMailAddress(_targetUserEmailId);
                    _msgSubject = "User Login Notification";
                    _msgBody = strMsgTemplate_fail;
                    _msgBody = _msgBody.Replace("[USEREMAIL]", _targetUserEmailId);
                    _msgBody = _msgBody.Replace("[LOGINTIME]", _loginTime);
                    _msgBody = _msgBody.Replace("[REASON]", "The dummy user's account is not allowed to access out of domain.");
                    _msgBody = _msgBody.Replace("[CLIENT]", _targetUserClient);
                    _msgBody = _msgBody.Replace("[IPADDRESS]", pUserIpAddress);
                    _msgBody = _msgBody.Replace("[ORGANIZATION]", _geoOrg);
                    _msgBody = _msgBody.Replace("[CITY]", _geoCity);
                    _msgBody = _msgBody.Replace("[COUNTRY]", _geoCountry);
                    break;

                case MailType.Login_IPUnauthorized:
                    _mailTo = GetDefaultMailAddress(_targetUserEmailId);
                    _msgSubject = "User Login Notification";
                    _msgBody = strMsgTemplate_fail;
                    _msgBody = _msgBody.Replace("[USEREMAIL]", _targetUserEmailId);
                    _msgBody = _msgBody.Replace("[LOGINTIME]", _loginTime);
                    _msgBody = _msgBody.Replace("[REASON]", "The user's IP Address is not authorized.");
                    _msgBody = _msgBody.Replace("[CLIENT]", _targetUserClient);
                    _msgBody = _msgBody.Replace("[IPADDRESS]", pUserIpAddress);
                    _msgBody = _msgBody.Replace("[ORGANIZATION]", _geoOrg);
                    _msgBody = _msgBody.Replace("[CITY]", _geoCity);
                    _msgBody = _msgBody.Replace("[COUNTRY]", _geoCountry);
                    break;

                case MailType.Login_UserNotRegistered:
                    _mailTo = GetDefaultMailAddress(_targetUserEmailId);
                    _msgSubject = "User Login Notification";
                    _msgBody = strMsgTemplate_fail;
                    _msgBody = _msgBody.Replace("[USEREMAIL]", _targetUserEmailId);
                    _msgBody = _msgBody.Replace("[LOGINTIME]", _loginTime);
                    _msgBody = _msgBody.Replace("[REASON]", "The user is not registered.");
                    _msgBody = _msgBody.Replace("[CLIENT]", _targetUserClient);
                    _msgBody = _msgBody.Replace("[IPADDRESS]", pUserIpAddress);
                    _msgBody = _msgBody.Replace("[ORGANIZATION]", _geoOrg);
                    _msgBody = _msgBody.Replace("[CITY]", _geoCity);
                    _msgBody = _msgBody.Replace("[COUNTRY]", _geoCountry);
                    break;

                case MailType.Login_AccountLockedByWrongPwd:
                    _mailTo = pUserEmail.Trim();
                    _msgSubject = "Account Llocked";
                    _msgBody = strMsgTemplate_acLockByWrongPwd;
                    _msgBody = _msgBody.Replace("[FAILMESSAGE]", "Your account has been locked. To unlock your account or if this is an error, please contact us.");
                    break;
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("MailManager.cs", ":MailManager():", "Error in constructor:" + ex.Message, ex);
        }
    }    

    public MailManager(MailType pType, string pUserEmail, string pUserPwd, string pUserIpAddress, string pClient)
    {
        try
        {
            LogUtility.Info("MailManager.cs", ":MailManager():", "Constructor(reset pwd):Type[" + (int)pType + "],Email[" + pUserEmail + "],Ip[" + pUserIpAddress + "]");

            this._type = pType;
            this._targetUserEmailId = pUserEmail;
            this._targetUserIpAddress = pUserIpAddress;
            this._targetUserClient = pClient;
            this._targetUserPwd = pUserPwd;
            this._loginTime = VcmUserNamespace.cUserDbHandler.GetUtcSqlServerDate();

            _mailTo = pUserEmail.Trim();
            _msgSubject = "Your password has been reset";
            _msgBody = strMsgTemplate_pwdReset;
            _msgBody = _msgBody.Replace("[USEREMAIL]", _targetUserEmailId);
            _msgBody = _msgBody.Replace("[USERPASSWORD]", _targetUserPwd);
            _msgBody = _msgBody.Replace("[LOGINTIME]", _loginTime);
            _msgBody = _msgBody.Replace("[CLIENT]", _targetUserClient);
            _msgBody = _msgBody.Replace("[IPADDRESS]", pUserIpAddress);
            _msgBody = _msgBody.Replace("[BEASTURL]", "https://thebeastapps.com/BeastApps1");
            _msgBody = _msgBody.Replace("[INFOMAIL]", "thebeast@thebeastapps.com");

        }
        catch (Exception ex)
        {
            LogUtility.Error("MailManager.cs", ":MailManager():", "Error in constructor(reset pwd):" + ex.Message, ex);
        }
    }

    string GetDefaultMailAddress(string _addr)
    {
        string _result = UtilityHandler.bIsImportantMail(_addr) ? this._defaultImpMail : this._defaultInternalMail;
        return _result;
    }

    public void SendMail()
    {
        try
        {
            VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();
            _vcmMail.From = _defaultImpMail;
            _vcmMail.To = _mailTo;

            if (_mailTo.ToLower() != this._defaultImpMail.ToLower() && _mailTo.ToLower() != this._defaultInternalMail.ToLower())   //If mail is being sent to User
            {
                if (this._type != MailType.User_ResetPassword)
                {
                    if (UtilityHandler.bIsImportantMail(_targetUserEmailId))
                        _vcmMail.CC = this._defaultImpMail;
                    else
                        _vcmMail.CC = this._defaultInternalMail;
                }
            }

            _vcmMail.Subject = _msgSubject;
            _vcmMail.Body = _msgBody;
            _vcmMail.IsBodyHtml = true;
            _vcmMail.SendMail(0);
            _vcmMail = null;
        }
        catch (Exception ex)
        {
            LogUtility.Error("MailManager.cs", "SendMail()", "Mail server dead", ex);
        }
    }
}