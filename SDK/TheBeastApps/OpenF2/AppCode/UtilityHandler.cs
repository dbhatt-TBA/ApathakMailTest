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
using OpenF2;
using OpenF2.OpenBeastService;
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
        string fn = HttpContext.Current.Request.PhysicalApplicationPath.ToString() + "\\NewsLetters\\" + strFileName;

        System.IO.FileInfo fi = new System.IO.FileInfo(fn);
        if (fi.Exists)
        {
            return true;
        }

        return false;
    }

    public static void DownloadFile(string strFileName)
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

    public static void DownloadBEAST(string strForApplicaton, bool bIsFullVersion)
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
        try
        {
            if (pstrBody.Contains("Thread was being aborted."))
                return;

            //string strSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();

            string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
            string ErrorEmail = System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"].ToString();

            System.Net.Mail.MailMessage sendMail = new System.Net.Mail.MailMessage(FromEmail, ErrorEmail);
            //System.Net.Mail.MailMessage sendMail = new System.Net.Mail.MailMessage("apathak@thebeastapps.com", "apathak@thebeastapps.com");
            sendMail.Subject = "Error in TheBeastApps: SystemRunningOn : " + Convert.ToString(ConfigurationManager.AppSettings["SystemRunningOn"]);
            sendMail.Body = pstrBody;

            //SmtpClient sendMailClient = new SmtpClient(strSMTPServer);
            SmtpClient sendMailClient = new SmtpClient();

            if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
            {
                sendMailClient.Host = strAmazonServer;
                sendMailClient.EnableSsl = true;
                sendMailClient.Port = iPort;
                sendMailClient.Credentials = new System.Net.NetworkCredential(strAmazonUserName, strAmazonPassword);
            }
            else
            {
                sendMailClient.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
            }

            sendMailClient.Send(sendMail);

            sendMailClient = null;
            sendMail = null;
        }
        catch { }
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
        catch
        {
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
        catch (System.Exception ex)
        {
            return (string.Empty);
        }
    }

    #endregion

    #region VOLMAX Launcher Download

    public static string GetDotNetFrameWorkVersion()
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

    public static bool IsDotNetFrameWorkInstalled()
    {
        ArrayList agentVariables = new ArrayList(HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].Split(';'));
        string versionString = "";

        foreach (string variable in agentVariables)
        {
            if (variable.StartsWith(" .NET"))
                versionString += variable.Trim() + "#";
        }

        if (versionString.Contains(".NET4.0C"))
            return true;
        else
            return false;
    }

    #endregion

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
   public static Service SetCookiesForAmazon(string PageName, Service ws, Cookie newCookie)
    {
        try
        {
            ws.CookieContainer = new System.Net.CookieContainer();
            LogUtility.Info(PageName, "SetCookiesForAmazon", "CookieContainer created");

            if (HttpContext.Current.Request.Cookies == null)
            {
                LogUtility.Info(PageName, "SetCookiesForAmazon", "Cookies is null");
                newCookie = GetCookies();
            }
            if (HttpContext.Current.Request.Cookies.Get("AWSELB") == null)
            {
                LogUtility.Info(PageName, "SetCookiesForAmazon", "AWSELB is null");
                newCookie = GetCookies();

            }

            if (HttpContext.Current.Request.Cookies.Get("AWSELB").Name == null)
            {
                LogUtility.Info(PageName, "SetCookiesForAmazon", "AWSELB.Name is null");
                newCookie = GetCookies();

            }
            if (HttpContext.Current.Request.Cookies.Get("AWSELB").Name == "")
            {
                LogUtility.Info(PageName, "SetCookiesForAmazon", "AWSELB.Name is blank ");
                newCookie = GetCookies();

            }

            newCookie.Name = HttpContext.Current.Request.Cookies.Get("AWSELB").Name;
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Name newCookieName:" + newCookie.Name);

            newCookie.Domain = "thebeastapps.com";
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Value newCookieDomain:" + newCookie.Domain);

            newCookie.Expires = HttpContext.Current.Request.Cookies.Get("AWSELB").Expires;
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Value newCookieExpires:" + newCookie.Expires);

            newCookie.HttpOnly = HttpContext.Current.Request.Cookies.Get("AWSELB").HttpOnly;
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Value newCookieHttpOnly:" + newCookie.HttpOnly);

            newCookie.Path = HttpContext.Current.Request.Cookies.Get("AWSELB").Path;
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Value newCookiePath:" + newCookie.Path);

            newCookie.Secure = HttpContext.Current.Request.Cookies.Get("AWSELB").Secure;
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Value newCookieSecure:" + newCookie.Secure);

            newCookie.Value = HttpContext.Current.Request.Cookies.Get("AWSELB").Value;
            LogUtility.Info(PageName, "SetCookiesForAmazon", "Set Value newCookieValue:" + newCookie.Value);

            ws.CookieContainer.Add(newCookie);
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "SetCookiesForAmazon", PageName, ex);

        }
        return ws;
    }
    public static Cookie GetCookies()
    {
        try
        {
            Cookie awsCookie;
            WebRequest webRequest = WebRequest.Create(System.Configuration.ConfigurationManager.AppSettings["CookiesURL"].ToString()) as HttpWebRequest;
            webRequest.UseDefaultCredentials = true;
            webRequest.PreAuthenticate = true;
            webRequest.Proxy = WebRequest.DefaultWebProxy;
            webRequest.CachePolicy = WebRequest.DefaultCachePolicy;
            webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            webRequest.Timeout = 5000;
            HttpWebResponse webResponse = null;
            using (var response = webRequest.GetResponse() as HttpWebResponse)
            {
                try
                {
                    string[] cookieVal = response.Headers["Set-Cookie"].ToString().Split(';');
                    awsCookie = new Cookie(cookieVal[0].Split('=')[0], cookieVal[0].Split('=')[1], cookieVal[1].Split('=')[1], response.ResponseUri.Host);
                }
                catch (Exception ex)
                {
                    LogUtility.Error("UtilityHandler.cs", "GetCookies();", "Inner Message of GetCookies fuction..", ex);
                    return null;
                }
            }
            return awsCookie;
        }
        catch (Exception ex)
        {
            LogUtility.Error("UtilityHandler.cs", "GetCookies();", ex.Message, ex);
            return null;
        }
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

    public static string sMD5(string str)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider md5Prov = new System.Security.Cryptography.MD5CryptoServiceProvider();
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] md5 = md5Prov.ComputeHash(encoding.GetBytes(str));
        string _result = "";
        for (int i = 0; i < md5.Length; i++)
        {
            // _result += String.Format("{0:x}", md5[i]);
            _result += ("0" + String.Format("{0:X}", md5[i])).Substring(Convert.ToInt32(md5[i]) <= 15 ? 0 : 1, 2);
        }
        return _result;
    }
}
