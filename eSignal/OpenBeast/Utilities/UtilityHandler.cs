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
using VCM.Common.Log;

public class UtilityHandler
{

    //public static string VCM_MailAddress = "Sincerely, \nThe Beast Apps \ninfo@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800";
    //public static string VCM_MailAddress_In_Html = "Sincerely, <br/>The Beast Apps <br/>info@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800";

    public static string VCM_MailAddress = "Sincerely, \nThe Beast Apps \ninfo@thebeastapps.com \nNY: +1-646-688-7500";
    public static string VCM_MailAddress_In_Html = "Sincerely, <br/>The Beast Apps <br/>info@thebeastapps.com <br/>NY: +1-646-688-7500";
    public static string strAmazonServer = System.Configuration.ConfigurationManager.AppSettings["aws_SMTPServer"].Trim();
    public static string strAmazonUserName = System.Configuration.ConfigurationManager.AppSettings["aws_UserId"].Trim();
    public static string strAmazonPassword = System.Configuration.ConfigurationManager.AppSettings["aws_Password"].Trim();
    public static int iPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["aws_Port"].Trim());

    public static string strImpEmailIds = System.Configuration.ConfigurationManager.AppSettings["ImportantIds"].Trim().ToLower();

    public UtilityHandler()
    { }

    public static bool checkFileIsExists(string strFileName)
    {
        //string fn = HttpContext.Current.Server.MapPath("").ToString() + "\\NewsLetters\\" + strFileName;
        try
        {
            string fn = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\NewsLetters\\" + strFileName;

            System.IO.FileInfo fi = new System.IO.FileInfo(fn);
            if (fi.Exists)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "checkFileIsExists()", ex.Message, ex);
        }

        return false;
    }

    public static void DownloadFile(string strFileName)
    {
        try
        {
            string strFileNameWithExt = strFileName.Substring(strFileName.IndexOf("\\") + 1);
            HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName.Substring(strFileName.IndexOf("\\") + 1));
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Convert.ToString(strFileNameWithExt.Replace(" ", "_")));
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            switch (strFileName.ToUpper().Substring(strFileName.LastIndexOf(".") + 1))
            {
                case "DOC":
                    HttpContext.Current.Response.ContentType = "application/vnd.doc";
                    break;
                case "XLS": HttpContext.Current.Response.ContentType = "application/vnd.xls";
                    break;
                case "PDF": HttpContext.Current.Response.ContentType = "application/vnd.pdf";
                    break;
            }
            //HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath("").ToString() + "\\NewsLetters\\" + strFileName);
            HttpContext.Current.Response.WriteFile(HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\NewsLetters\\" + strFileName);
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "DownloadFile()", ex.Message, ex);
        }
    }

    public static void DownloadBEAST(string strForApplicaton, bool bIsFullVersion)
    {
        try
        {
            string strBeastInstallationPackage = string.Empty;
            string strFileName = string.Empty;

            if (strForApplicaton.ToString().ToUpper() == "BWS")
            {
                strBeastInstallationPackage = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\VCMCodeofConduct.pdf";
                strFileName = "BEASTPackage.pdf";
                HttpContext.Current.Response.ContentType = "application/vnd.pdf";
            }
            else if (strForApplicaton.ToString().ToUpper() == "WWS")
            {
                //TO DOWNLOAD DEMO VERSION
                strBeastInstallationPackage = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\WWSApp\\ExeWWS\\TheBeastWorkstationInstall.exe";
                strFileName = "TheBeastWorkstationInstall.exe";
                HttpContext.Current.Response.ContentType = "application/exe";
            }
            else if (strForApplicaton.ToString().ToUpper() == "WWSPROD")
            {
                //TO DOWNLOAD PRODUCT VERSION
                strBeastInstallationPackage = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\WWSApp\\ExeWeatherProd\\TheBeastWorkstationInstall.exe";
                strFileName = "TheBeastWorkstationInstall.exe";
                HttpContext.Current.Response.ContentType = "application/exe";
            }
            else if (strForApplicaton.ToString().ToUpper() == "VOLMAXAPP")
            {
                if (bIsFullVersion)
                    strBeastInstallationPackage = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\WWSApp\\VolmaxApp\\VolmaxLauncher.exe";
                else
                    strBeastInstallationPackage = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\WWSApp\\VolmaxApp\\VolmaxInstall.msi";

                if (strBeastInstallationPackage.Contains("exe"))
                    strFileName = "VolmaxLauncher.exe";
                else
                    strFileName = "VolmaxInstall.msi";

                HttpContext.Current.Response.ContentType = "application/exe";
            }
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
            HttpContext.Current.Response.Charset = "";

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            //HttpContext.Current.Response.ContentType = "application/vnd.pdf";
            HttpContext.Current.Response.WriteFile(strBeastInstallationPackage);
            HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "DownloadBEAST()", ex.Message, ex);
        }
    }

    #region ENUM Applicantion Name and Mail Address

    public enum ApplicationLink
    {
        RiskRecycling = 1,        //	Web # Risk Recycling

        Inflation = 3,            //	Web # Inflation

        CataStrophe = 4,          //	Web # CATS

        Oddlot = 5,               //	Web # OddLot System

        Weather = 7,              //	Web # Weather

        Swaption = 8,             //	Web # Swaption System

        IRS = 9                   //	Web # IRS
    }

    public enum ApplicationMailAddress
    {
        [Description("Sincerely, \nRisk Recycling Team \nThe Beast Apps \nriskrecycling@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800")]
        RiskRecycling = 1,       //	Web # Risk Recycling
        [Description("Sincerely, \nInflation Team \nThe Beast Apps \ninflation@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800")]
        Inflation = 3,            //	Web # Inflation
        //[Description("Sincerely, \nCatastrophe Markets \nThe Beast Apps \nCAT@thebeastapps.com \nDesk: +1-646-688-7565")]
        [Description("Sincerely, \nCatastrophe Markets \nThe Beast Apps \nCAT@thebeastapps.com \nDesk: +1-646-688-7565")]
        CataStrophe = 4,          //	Web # CATS
        [Description("Sincerely, \nOddLot Team \nThe Beast Apps \noddlots@thebeastapps.com \nNY: +1-646-688-7550")]
        Oddlot = 5,               //	Web # OddLot System
        [Description("Sincerely, \nWeather Team \nThe Beast Apps \nweather@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800")]
        Weather = 7,               //	Web # Weather
        [Description("Sincerely, \nSwaption Team \nThe Beast Apps \nvolmax@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800")]
        Swaption = 8,                //	Web # Swaption System
        [Description("Sincerely, \nIRS Team \nThe Beast Apps \nIRS@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800")]
        IRS = 9,                //	Web # IRS System
        [Description("Sincerely, \nCMEDIRSF Team \nThe Beast Apps \ninfo@thebeastapps.com \nNY: +1-646-688-7500")]
        CMEDIRSF = 10                //	Web # IRS System
    }

    public enum ApplicationMailAddressInHTML
    {
        [Description("Sincerely, <br/>Risk Recycling Team <br/>The Beast Apps <br/>riskrecycling@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800")]
        RiskRecycling = 1,       //	Web # Risk Recycling

        [Description("Sincerely, <br/>Inflation Team <br/>The Beast Apps <br/>inflation@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800")]
        Inflation = 3,            //	Web # Inflation

        //[Description("Sincerely, <br/>Catastrophe Markets <br/>The Beast Apps <br/>CAT@thebeastapps.com <br/>Desk: +1-646-688-7565")]
        [Description("Sincerely, <br/>Catastrophe Markets <br/>The Beast Apps <br/>CAT@thebeastapps.com <br/>Desk: +1-646-688-7565")]
        CataStrophe = 4,          //	Web # CATS

        [Description("Sincerely, <br/>OddLot Team  <br/>The Beast Apps <br/>oddlots@thebeastapps.com <br/>NY: +1-646-688-7550")]
        Oddlot = 5,                //	Web # OddLot System

        [Description("Sincerely, <br/>Weather Team  <br/>The Beast Apps <br/>weather@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800")]
        Weather = 7,                //	Web # Weather

        [Description("Sincerely, <br/>Swaption Team <br/>The Beast Apps <br/>volmax@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800")]
        Swaption = 8,       //	Web # Swaption

        [Description("Sincerely, <br/>IRS Team <br/>The Beast Apps <br/>IRS@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800")]
        IRS = 9       //	Web # IRS        
    }

    #endregion

    public static void SendEmailForError(string pstrBody)
    {
        //try
        //{
        //    if (pstrBody.Contains("Thread was being aborted."))
        //        return;

        //    //string strSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
        //    System.Net.Mail.MailMessage sendMail = new System.Net.Mail.MailMessage(System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString(), System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"].ToString());
        //    sendMail.Subject = "Error in TheBeastApps SystemRunningOn:" + System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"];
        //    sendMail.Body = pstrBody;
        //    //SmtpClient sendMailClient = new SmtpClient(strSMTPServer);
        //    SmtpClient sendMailClient = new SmtpClient();

        //    if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
        //    {
        //        sendMailClient.Host = strAmazonServer;
        //        sendMailClient.EnableSsl = true;
        //        sendMailClient.Port = iPort;
        //        sendMailClient.Credentials = new System.Net.NetworkCredential(strAmazonUserName, strAmazonPassword);
        //    }
        //    else
        //    {
        //        sendMailClient.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
        //    }

        //    sendMailClient.Send(sendMail);
        //    sendMailClient = null;
        //    sendMail = null;
        //}
        //catch (Exception ex)
        //{
        //    LogUtility.Error("UtilityHandler.cs", "SendEmailForError()", ex.Message, ex);
        //}
    }

    //public static void SendMail(string _mailFrom, string _mailTo, string _mailCC, string _mailBCC, string _mailSubject, string _mailBody)
    public static void SendMail(string _mailTo, string _mailCC, string _mailBCC, string _mailSubject, string _mailBody, bool useInternalId)
    {
        MailMessage _mailMessage;
        SmtpClient _smptpClient;
        bool _bSendAsync = true;

        try
        {
            string _mailFrom = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
            //if (useInternalId)
            //    _mailFrom = System.Configuration.ConfigurationManager.AppSettings["InternalEmail"].ToString();
            //else
            //    _mailFrom = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

            _mailMessage = new MailMessage(_mailFrom, _mailTo);
            if (!string.IsNullOrEmpty(_mailCC)) { _mailMessage.CC.Add(_mailCC); }
            if (!string.IsNullOrEmpty(_mailBCC)) { _mailMessage.Bcc.Add(_mailBCC); }

            _mailMessage.Subject = _mailSubject;
            _mailMessage.Body = _mailBody;
            _mailMessage.IsBodyHtml = true;

            //if (!string.IsNullOrEmpty(_fileNameToAttach))
            //{
            //    string strFileName = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\NewsLetters\\" + _fileNameToAttach;
            //    Attachment itm = new Attachment(strFileName);
            //    _mailMessage.Attachments.Add(itm);
            //}

            //string strSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
            //_smptpClient = new SmtpClient(strSMTPServer);

            _smptpClient = new SmtpClient();

            if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
            {
                _smptpClient.Host = strAmazonServer;
                _smptpClient.EnableSsl = true;
                _smptpClient.Port = iPort;
                _smptpClient.Credentials = new System.Net.NetworkCredential(strAmazonUserName, strAmazonPassword);
            }
            else
            {
                _smptpClient.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
            }

            if (_bSendAsync)
            {
                _smptpClient.SendCompleted += new SendCompletedEventHandler(_smptpClient_SendCompleted);
                _smptpClient.SendAsync(_mailMessage, "VCMmail");
            }
            else
            {
                _smptpClient.Send(_mailMessage);
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "SendMail()", ex.Message, ex);
        }
        finally
        {
            _mailMessage = null;
            _smptpClient = null;
        }
    }

    private static void _smptpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
    }

    public static string Get_IPAddress(string reqUserHostAddr)
    {
        string ip = "";
        if (HttpContext.Current.Request.Headers["X-Forwarded-For"] != null)
        {
            ip = HttpContext.Current.Request.Headers["X-Forwarded-For"].Split(',')[0];
        }
        else
        {
            ip = reqUserHostAddr;

            /*
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            ip = addr[1].ToString();
             */
        }

        return ip;
    }

    public static bool bIsImportantMail(string strEmailId)
    {
        bool bReturn = false;
        strEmailId = strEmailId.Trim().ToLower();

        if (!strEmailId.Contains("@thebeastapps.com"))
        {
            bReturn = true;
        }
        else if (strImpEmailIds.Contains("#" + strEmailId + "#"))
        {
            bReturn = true;
        }
        else
        {
            bReturn = false;
        }
        LogUtility.Info("UtilityHandler.cs", "bIsImportantMail()", "Email:" + strEmailId + " Return:" + (bReturn ? "TRUE" : "FALSE"));
        return bReturn;
    }

    #region Encyption and Decryption for User Name and Password in cookiees

    public static string Encrypt(string stringToEncrypt)
    {
        string sEncryptionKey = "98765432";
        byte[] key = { };
        byte[] IV = { 40, 50, 60, 70, 80, 90, 20, 30 };
        byte[] inputByteArray;

        try
        {
            key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "Encrypt()", ex.Message, ex);
            return (string.Empty);
        }
    }

    public static string Decrypt(string stringToDecrypt)
    {
        string sEncryptionKey = "98765432";
        byte[] key = { };
        byte[] IV = { 40, 50, 60, 70, 80, 90, 20, 30 };
        byte[] inputByteArray = new byte[stringToDecrypt.Length];

        try
        {
            key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "Decrypt()", ex.Message, ex);
            return (string.Empty);
        }
    }

    #endregion

    #region VOLMAX Launcher Download

    public static string GetDotNetFrameWorkVersion()
    {
        try
        {
            ArrayList agentVariables = new ArrayList(HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].Split(';'));
            string versionString = "";

            foreach (string variable in agentVariables)
            {
                if (variable.StartsWith(" .NET"))
                    versionString += variable.Trim() + "#";
            }

            if (string.IsNullOrEmpty(versionString))
                return "N/A";
            else
                return versionString;
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "GetDotNetFrameWorkVersion()", ex.Message, ex);
            return (string.Empty);
        }
    }

    public static bool IsDotNetFrameWorkInstalled()
    {
        string versionString = "";
        try
        {
            ArrayList agentVariables = new ArrayList(HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].Split(';'));

            foreach (string variable in agentVariables)
            {
                if (variable.StartsWith(" .NET"))
                    versionString += variable.Trim() + "#";
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "IsDotNetFrameWorkInstalled()", ex.Message, ex);
        }

        if (versionString.Contains(".NET4.0C"))
            return true;
        else
            return false;

    }

    #endregion
}
