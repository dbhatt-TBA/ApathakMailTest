using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Collections;
using System.IO;
using VCM.Common.Log;
using System.Text;

public class BulkShare
{    
    #region Variables

    int countDivider { get; set; }
    int totalMailCount { get; set; }

    DataTable dtUserList;

    string[] ValidToken = null;

    #endregion


    public BulkShare()
    {

    }

    public BulkShare(int MailCount)
    {
        this.totalMailCount = MailCount;
    }

    public string GetSharedUsersList(char cSeparator)
    {
        string[] arrResult = null;
        string strMailString = string.Empty;
        try
        {
            strMailString = getMailIdfromFile(cSeparator);
            //strMailString = strMailString.Replace(',', cSeparator);

            if (string.IsNullOrEmpty(strMailString))
            {
                strMailString = "-1";
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("BulkShare.cs", "GetSharedUsersList()", "Error Getting User list", ex);
        }

        return strMailString;
    }

    private string getMailIdfromFile(char cSeparator)
    {        
        string strFilePath = AppDomain.CurrentDomain.BaseDirectory + "EmailidSource\\";

        string strFileContents = string.Empty;
        StringBuilder sbEmailIds = new StringBuilder();

        try
        {
            string filename = "NumerixWebinar.xlsx";
            string extenstion = ".xlsx";

            if (File.Exists(strFilePath + filename))
            {
                DataSet ds = excelUtility.ImportExcelXLS(filename, true, strFilePath);

                if (ds.Tables.Count > 0)
                {
                    DataTable dtUsers = ds.Tables[0];
                    for (int _row = 0; _row < dtUsers.Rows.Count; _row++)
                    {
                        sbEmailIds.Append(Convert.ToString(dtUsers.Rows[_row]["EmailID"]) + cSeparator);
                    }

                    strFileContents = sbEmailIds.ToString();

                    strFileContents = strFileContents.TrimEnd(cSeparator);
                }                
            }
            else
            {
                LogUtility.Info("BulkShare.cs", "getMailIdfromFile()", "File Not Found for Bulk Share");
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("BulkShare.cs", "getMailIdfromFile()", "Error Getting User list", ex);
        }
        return strFileContents;
    }

    private static string GetMailBody(string strAutoUrl, string strUserName, string strTimeFrom, string strTimeTo, string strSenderMessage)
    {
        string _customMessage = string.IsNullOrEmpty(strSenderMessage.Trim()) ? "" : "<p><div style=\"border-top:1px dashed Gray;border-bottom:1px dashed Gray;padding:3px 0px;\">Your message : <br/> " + strSenderMessage + " </div></p>";

        string strMailBody = "<div style=\"color:navy;font:normal 12px verdana\">" +
                            (string.IsNullOrEmpty(_customMessage) ? "" : _customMessage) +
                           "<p>Dear User,</p><p>A BEAST Calculator has been shared with you by our customer " + strUserName + ".</p>" +
                           "<p>You may access <strong>The BEAST Apps</strong> by clicking on the following URL. You may copy and paste this URL in your browser as well.</p>" +
                           "<p><a href=\"" + strAutoUrl + "\">" + strAutoUrl + "</a></p>" +
                           "<p>This URL is valid as follows:</p> " +
                           "<p>User: " + strUserName + "<br/>" +
                           "URL Valid for: " + Convert.ToDateTime(strTimeFrom).ToString("dd-MMM-yyyy hh:mm:ss tt") + " To " + Convert.ToDateTime(strTimeTo).ToString("dd-MMM-yyyy hh:mm:ss tt") + " (GMT)</p>" +
                           "<p> <b>NOTE:</b><b><i>&nbsp;Please treat this URL confidential as this URL will give the recipient an access to your account.</i></b></p>" +
                           "<p>If you do not wish to receive these URLs, please let us know.</p>" +
                           "<p>Please contact us if you have any questions.<br/><br/></p>" +
                            UtilityHandler.VCM_MailAddress_In_Html +
                            "</div>";
        return strMailBody;
    }
}
