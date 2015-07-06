using System;
using System.Collections;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using VCM.Common.Log;
using VCM.Common;

/// <summary>
/// This Page class is common to all sample pages and exists as a place to
/// implement common functionality
/// </summary>
public class CommonPage : Page
{
     static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString();
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

	public CommonPage()
	{
		
	}
 
}

public enum AutoURLApplicationCode
{
    IRO = 8,
    ISWAP = 9,
    OddLot = 5
}

public interface IContentPlaceHolders
{
    IList GetContentPlaceHolders();
}

public static class BeastCalcGroups
{
    public const string Default = "Users";
    public const string CME = "CME_ICAP";
    public const string FinCAD = "FinCad";
}