using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VCM.Common.Log;

/// <summary>
/// Summary description for UtilityHandler
/// </summary>
public class UtilityHandler
{
    public static string strImpEmailIds = System.Configuration.ConfigurationManager.AppSettings["ImportantIds"].Trim().ToLower();
    public static string VCM_MailAddress = "Sincerely, \nThe Beast Apps \ninfo@thebeastapps.com \nNY: +1-646-688-7500";
    public static string VCM_MailAddress_In_Html = "Sincerely, <br/>The Beast Apps <br/>info@thebeastapps.com <br/>NY: +1-646-688-7500";

	public UtilityHandler()
	{
		//
		// TODO: Add constructor logic here
		//
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
}