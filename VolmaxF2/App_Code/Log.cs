using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Diagnostics;
using System.ComponentModel;
using System.Web.SessionState;
using VCM.Common;
using VCM.Common.Log;

/// <summary>
/// Author      : Harshad Prajapati
/// Date        : 1st February 2010 
/// Description : VCM_LogManager For Log Error in System Event Source [Event Viewer],Log Error into Text File,Log User Audit Trail and User Activity Into Database
/// </summary>
/// 
namespace VcmLogManager
{

    public sealed class Log : System.Web.UI.Page
    {
        #region VARIABLES

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

        static int iErrorCount;

        private static string pStrSqlConnection = System.Configuration.ConfigurationManager.ConnectionStrings["TradeCaptureConnectionString"].ToString();

        private static HttpContext pCurrentHttpContext;
        private static HttpSessionState pManagerSessionState;

        #endregion

        #region CallBack Delegate

        private delegate void LogManagerWriteEventLog(string userName, string mOrgdate, string location, string pageName, string functionName, string errorName, EventLogEntryType eLogEntryType, string strIPAddress);
        private delegate void LogManagerSendUserLoginNotification(string UserID, string UserName, int Status, int EventID, string EventDesc, string PageName, string IPAddress, string Session_ID, string ASPSessionID, string SessionLogincount);
        private delegate void LogManagerLogUserActivity(string sessionID, string UserID, string UserName, string pageName, string description, LogEntryType eLogEntryType, ApplicationName eApplicationName, string strUserIpAddress);
        private delegate void LogManagerWriteLogIntoFile(string strMessage, LogEntryType eEntryType);

        private delegate void LogManagerSendUserPriceOrder(string UserID, string UserName, int Status, int EventID, string EventDesc, string PageName, string IPAddress, string Session_ID, string ASPSessionID, string SessionLogincount);


        #endregion

        #region ENUM

        public enum ApplicationName
        {
            RR,
            OL,
            Weather,
            MV,
            INF,
            Amex,
            OutofPocket,
            AccountsPayable,
            BrokerApproval,
            Reports,
            Sharepoint,
            Attendance,
            DOCS,
            SWAP,
            IRS

        }

        public enum LogEntryType
        {
            Debug,
            Error,
            Info,
            Warn
        }

        public enum LoginMessage
        {
            [Description("Login failure - Invalid Password")]
            InvalidPassword = 0,
            [Description("Successfully Login")]
            Login = 1,
            [Description("Login failure - Account Blocked for the first time")]
            AccountBlocked = 2,
            [Description("Login failure - User Tried After Account Blocked")]
            AccountBlockedAfter = 3,
            [Description("Login failure - User was not Registered user")]
            NotRegistered = 4,
            [Description("User Logged Out")]
            LoggedOut = 5,
            [Description("User Register")]
            Registeruser = 6,
            [Description("User ForgotPassword")]
            ForgotPassword = 7,
            [Description("User Security Answer")]
            SecurityAnswer = 8,
            [Description("Dummy user not allowed to access out of domain")]
            Outofdomain = 9

        }

        #endregion

        #region LogManager Properties

        //private static HttpContext LogManagerCurrentHttpContext
        //{
        //    get { return pCurrentHttpContext; }
        //    set { pCurrentHttpContext = value; }
        //}

        //private static HttpSessionState LogManagerSessionState
        //{
        //    get { return pManagerSessionState; }
        //    set { pManagerSessionState = value; }
        //}

        // Remarks:
        //      Get Current Request Context and Session Info
        private static void GetLogManagerUserInfo()
        {
            if (HttpContext.Current != null)
            {
                pCurrentHttpContext = HttpContext.Current;
                pManagerSessionState = HttpContext.Current.Session;
            }
        }

        //public static string SqlConnectionString
        //{
        //    get { return pStrSqlConnection; }
        //    set { pStrSqlConnection = value; }
        //}


        #endregion

        public Log()
        { }

        #region WRITE LOG INTO SYSTEM EVENT LOG

        // Summary:
        //      Write Log into System EventSource
        // Parameters:
        //      pageName : From where we get error
        //      functionName : fucntioname which has error
        //      errorName : Error description
        // Remarks:
        //      Create EventSource in System Event Viewer and write log base on given parameter
        public static void writeLog(string userName, string mOrgdate, string location, string pageName, string functionName, string errorName, string strIPAddress)
        {
            if (errorName.Contains("Thread was being aborted"))
                return;

            GetLogManagerUserInfo();
            LogManagerWriteEventLog objLogManagerWriteLog = new LogManagerWriteEventLog(WriteLogIntoEventLog);
            //IAsyncResult result = objLogManagerWriteLog.BeginInvoke(userName, mOrgdate, location, pageName, functionName, errorName, EventLogEntryType.Error, strIPAddress, new AsyncCallback(writeLog_CallBack), objLogManagerWriteLog);
            IAsyncResult result = objLogManagerWriteLog.BeginInvoke(userName, mOrgdate, location + " - " + (pManagerSessionState["Sector"] != null ? pManagerSessionState["Sector"] : ""), pageName, functionName, errorName, EventLogEntryType.Error, strIPAddress, new AsyncCallback(writeLog_CallBack), objLogManagerWriteLog);
            objLogManagerWriteLog = null;
            result = null;
        }

        // Remarks:
        //      WriteLogIntoEventLog Method to Write log into System Event Log
        private static void WriteLogIntoEventLog(string userName, string mOrgdate, string location, string pageName, string functionName, string errorName, EventLogEntryType eLogEntryType, string strIPAddress)
        {
            //EventLog objEventLog;
            try
            {

                string Msgbody = string.Empty;
                              //if (errorName.Contains("Timeout expired"))
                //{

                if (pageName == "Iswap_status_price" || pageName == "Iswap_status_order" || pageName == "Iswap_No_open")
                {
                    Msgbody = "<div style=\"font-size:9pt;font-family:Verdana;color:navy;\">Dear Admin,<br/><br/><tr></td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + errorName + "<br/><br/> IP Address - " + strIPAddress + "</td></tr>" +
                                                                   "<tr><td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\"><br/><br/>Session:</td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + mOrgdate + " - " + location + "</td></tr></table>" +
                                                                  "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>thebeast@thebeastapps.com <br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
                }
              
                else if (pageName == "send_request")
                {
                    Msgbody = "<div style=\"font-size:9pt;font-family:Verdana;color:navy;\">Dear Admin,<br/><br/><tr></td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + errorName + "<br/><br/> IP Address - " + strIPAddress + "</td></tr>" +
                                                                              "<tr><td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\"><br/><br/>Session:</td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + mOrgdate + " - " + location + "</td></tr></table>" +
                                                                             "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>thebeast@thebeastapps.com <br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
                }
                else if (pageName == "beastConn__sa_NotifyHost")
                {
                    Msgbody = "<div style=\"font-size:9pt;font-family:Verdana;color:red;\">Dear Admin,<br/><br/><tr></td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:red;\"><p>" + userName + " not able to Open Web Manager due to <b>" + errorName + "</b><br/><br/> IP Address - " + strIPAddress + "</td></tr>" +
                                                                             "<tr><td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\"><br/><br/>Session:</td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + mOrgdate + " - " + location + "</td></tr></table>" +
                                                                            "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
                }
                else if (pageName == "VolmaxLauncherDownload")
                {

                    string[] UserInfo = userName.Split('#');

                    Msgbody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/>User has clicked to download Volmax Launcher <br/><br/> User name : " + UserInfo[0] + " <br/>  &nbsp;" +
                                                   " <table><tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + strIPAddress + "</td></tr></table>" +
                                                   "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
                }
                else if (pageName == "GenerateNewPassword")
                {
                    string newPassword = functionName;
                    Msgbody = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Admin,<br/><br/> System has reset user password. <br/><br/> User Name : " + userName + " <br/> Temporary Password : " + newPassword + " &nbsp; <br/><br/>" +
                                                   " <table><tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">IP Address:</td> <td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">&nbsp;" + strIPAddress + "</td></tr></table>" +
                                                   "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>volmax@thebeastapps.com<br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
                }
                else
                {
                    Msgbody = "<div style=\"font-size:9pt;font-family:Verdana;color:navy;\">Dear Admin,<br/><br/> User named " + userName + ", has got an error while using The BEAST Financial Framework system:&nbsp;<br/>" +
                                                               " Error Description:<br/><table><tr><td style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">Page Name:</td>&nbsp;<td align=\"left\" style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + pageName + "</td></tr> " +
                                                               "<tr><td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy; \">Function Name:</td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:red;\">" + functionName + "</td></tr>" +
                                                               "<tr><td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">Error Name:</td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + errorName + "<br/><br/> IP Address - " + strIPAddress + "</td></tr>" +
                                                                "<tr><td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">Session:</td>&nbsp;<td  style=\"FONT-SIZE: 10pt; FONT-FAMILY: Verdana;color:navy;\">" + mOrgdate + " - " + location + "</td></tr></table>" +
                                                               "<br/><br/>Sincerely,<br/>Volmax Team<br/>The Beast Apps<br/>thebeast@thebeastapps.com <br/>NY:&nbsp;+1-646-688-7545<br/>London:&nbsp;+44 (0)20-7398-2800</div>";
                }
                
                if (pageName == "Iswap_status_price")
                {
                    SendErrorEmail("" + userName + " Action has not been performed because the price phase has ended.", Msgbody, "1");
                }
                else if (pageName == "Iswap_status_order")
                {
                    SendErrorEmail("" + userName + " Action has not been performed because the order phase has ended.", Msgbody, "1");
                }
                else if (pageName == "Iswap_No_open")
                {
                    SendErrorEmail("No open order / order partially traded For structure to delete.", Msgbody, "1");
                }              
                else if (pageName == "send_request")
                {
                    SendErrorEmail("" + userName + " can't get Successful Message for Request", Msgbody, "1");
                }
                else if (pageName == "beastConn__sa_NotifyHost")
                {
                    SendErrorEmail(errorName, Msgbody, "1");
                }
                else if (pageName == "VolmaxLauncherDownload")
                {
                    SendErrorEmail("Volmax Launcher - " + userName.Replace('#', '-') + " has clicked to download ", Msgbody, "VolmaxLauncherDownload");
                }
                else if (pageName == "GenerateNewPassword")
                {
                    SendErrorEmail("" + userName + " The BEAST Financial Framework   - Password Reset", Msgbody, "1");
                }
                else
                {
                    iErrorCount += 1;
                    if (iErrorCount % 50 == 0 || iErrorCount == 1)
                        SendErrorEmail("ERROR IN The BEAST Financial Framework ", Msgbody, pageName);
                }
              
                Msgbody = "";
            }
            catch(Exception ex)
            {
                LogUtility.Error("Log.cs", "WriteLogIntoEventLog", ex.Message, ex);
            }
            finally
            {
                //objEventLog = null;
            }
        }

        // Remarks:
        //      Call Back For WriteLogIntoEventLog Method
        private static void writeLog_CallBack(IAsyncResult callbackResult)
        {
            LogManagerWriteEventLog objLogManagerWriteLog = (LogManagerWriteEventLog)callbackResult.AsyncState;
            callbackResult.AsyncWaitHandle.WaitOne();
            objLogManagerWriteLog.EndInvoke(callbackResult);
            callbackResult.AsyncWaitHandle.Close();
            objLogManagerWriteLog = null;
        }

        #endregion

        #region WRITE LOG INTO FILE UNDER VIRTUAL DIRECTORY --> LOG FOLDER
        // Summary:
        //      Write Log into FIle Under Virtual Directory --> Log Floder
        // Parameters:
        //      strMessage : Message or descrion to write into file
        //      eEntryType : error type
        // Remarks:
        //      Create file and write given message into file

        public static void WriteLogFile(string strMessage, LogEntryType eEntryType)
        {
            GetLogManagerUserInfo();

            LogManagerWriteLogIntoFile objLogManagerWriteLogIntoFile = new LogManagerWriteLogIntoFile(WriteLogIntoFile);
            IAsyncResult result = objLogManagerWriteLogIntoFile.BeginInvoke(strMessage, eEntryType, new AsyncCallback(WriteLogIntoFile_CallBack), objLogManagerWriteLogIntoFile);
            objLogManagerWriteLogIntoFile = null;
            result = null;
        }

        private static void WriteLogIntoFile(string strMessage, LogEntryType eEntryType)
        {
            string strFileName = DateTime.Now.ToShortDateString().Replace("/", "") + ".txt";
            string strFilePath = pCurrentHttpContext.Request.PhysicalApplicationPath + "Log\\" + strFileName;

            try
            {
                StreamWriter sw = new StreamWriter(strFilePath, true);
                sw.WriteLine(DateTime.Now.ToShortTimeString() + ": " + eEntryType + " :- " + strMessage);
                sw.Flush();
                sw.Close();
            }
            catch(Exception ex)
            {
                LogUtility.Error("Log.cs", "WriteLogIntoEventLog", ex.Message, ex);
            }
        }

        private static void WriteLogIntoFile_CallBack(IAsyncResult callbackResult)
        {
            LogManagerWriteLogIntoFile objLogManagerWriteLogIntoFile = (LogManagerWriteLogIntoFile)callbackResult.AsyncState;
            callbackResult.AsyncWaitHandle.WaitOne();
            objLogManagerWriteLogIntoFile.EndInvoke(callbackResult);
            callbackResult.AsyncWaitHandle.Close();
            objLogManagerWriteLogIntoFile = null;
        }

        #endregion

        #region WRITE LOG INTO DATABASE SendUserLoginNotification
        // Summary:
        //      Write Log into Database for User Audit Trail
        // Parameters:
        //      UserID : ID of logged User
        //      UserName : Name of logged User
        //      Status : Notificaton Status
        //      EventID : Notification Event ID
        //      EventDesc : Notification Event Description
        //      PageName : Name of page for Notification
        //      IPAddress : User Ip Address
        // Remarks:
        //      Write log into Database table dbo.UserLoginNotification
        public static void SendUserLoginNotification(string UserID, string UserName, int Status, int EventID, string EventDesc, string PageName, string IPAddress, string Session_ID, string ASPSessionID, string SessionLogincount)
        {
            try
            {
                GetLogManagerUserInfo();
                LogManagerSendUserLoginNotification objLogManagerSendUserLoginNotification = new LogManagerSendUserLoginNotification(WriteLogIntoDatabase);
                IAsyncResult result = objLogManagerSendUserLoginNotification.BeginInvoke(UserID, UserName, Status, EventID, EventDesc, PageName, IPAddress, Session_ID, ASPSessionID, SessionLogincount, new AsyncCallback(WriteLogIntoDatabase_CallBack), objLogManagerSendUserLoginNotification);
                objLogManagerSendUserLoginNotification = null;
                result = null;
            }
            catch
            {
            }
        }

        private static void WriteLogIntoDatabase(string UserID, string UserName, int Status, int EventID, string EventDesc, string PageName, string IPAddress, string Session_ID, string ASPSessionID, string SessionLogincount)
        {
            try
            {
                SqlConnection objSqlConnection = new SqlConnection(pStrSqlConnection);
                objSqlConnection.Open();

                string StatusMsg = null;
                SqlCommand objSqlCommand = new SqlCommand("PROC_SUBMIT_UserLoginNotification", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                objSqlCommand.Parameters.Add("@p_UserID", SqlDbType.Int);
                objSqlCommand.Parameters["@p_UserID"].Value = UserID;

                objSqlCommand.Parameters.Add("@p_UserName", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_UserName"].Value = UserName;

                objSqlCommand.Parameters.Add("@p_status", SqlDbType.Int);
                objSqlCommand.Parameters["@p_status"].Value = Status;

                StatusMsg = ((DescriptionAttribute[])((LoginMessage)Status).GetType().GetField(((LoginMessage)Status).ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false))[0].Description;
                objSqlCommand.Parameters.Add("@p_statusMsg", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_statusMsg"].Value = StatusMsg;

                objSqlCommand.Parameters.Add("@p_SessionID", SqlDbType.Int);

                // if (pManagerSessionState["Session_ID"] != null)
                if (Session_ID != null)
                {
                    objSqlCommand.Parameters["@p_SessionID"].Value = Session_ID;
                }
                else
                {
                    objSqlCommand.Parameters["@p_SessionID"].Value = 0;
                }

                objSqlCommand.Parameters.Add("@p_ASPSessionID", SqlDbType.NVarChar);

                if (EventID.Equals(50) || EventID.Equals(51) || EventID.Equals(52))
                {
                    objSqlCommand.Parameters["@p_ASPSessionID"].Value = "0";
                }
                else
                {
                    objSqlCommand.Parameters["@p_ASPSessionID"].Value = Convert.ToString(ASPSessionID);
                }

                objSqlCommand.Parameters.Add("@p_EventID", SqlDbType.Int);
                objSqlCommand.Parameters["@p_EventID"].Value = EventID;

                objSqlCommand.Parameters.Add("@p_EventDescriptions", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_EventDescriptions"].Value = EventDesc;


                objSqlCommand.Parameters.Add("@p_PageName", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_PageName"].Value = PageName;

                objSqlCommand.Parameters.Add("@p_IPAddress", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_IPAddress"].Value = IPAddress;

                objSqlCommand.Parameters.Add("@p_SessionLoginCount", SqlDbType.Int);
                if (pManagerSessionState["SessionLogincount"] == null)
                {
                    objSqlCommand.Parameters["@p_SessionLoginCount"].Value = 0;
                }
                else
                {
                    objSqlCommand.Parameters["@p_SessionLoginCount"].Value = Convert.ToInt32(SessionLogincount);
                }

                objSqlCommand.Parameters.Add("@p_Application", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_Application"].Value = ApplicationName.IRS;
                //string s = "Browser Capabilities: " + "Type = " + HttpContext.Current.Request.Browser.Type + " Name = " + HttpContext.Current.Request.Browser.Browser + " Version = " + HttpContext.Current.Request.Browser.Version + " Major Version = " + HttpContext.Current.Request.Browser.MajorVersion + " Minor Version = " + HttpContext.Current.Request.Browser.MinorVersion + " Platform = " + HttpContext.Current.Request.Browser.Platform + " Is Beta = " + HttpContext.Current.Request.Browser.Beta;
                string s = "Browser Capabilities: " + "Type = " + pCurrentHttpContext.Request.Browser.Type + " Name = " + pCurrentHttpContext.Request.Browser.Browser + " Version = " + pCurrentHttpContext.Request.Browser.Version + " Major Version = " + pCurrentHttpContext.Request.Browser.MajorVersion + " Minor Version = " + pCurrentHttpContext.Request.Browser.MinorVersion + " Platform = " + pCurrentHttpContext.Request.Browser.Platform + " Is Beta = " + pCurrentHttpContext.Request.Browser.Beta + " Is Crawler = " + pCurrentHttpContext.Request.Browser.Crawler + " Is Win16 = " + pCurrentHttpContext.Request.Browser.Win16 + " Is Win32 = " + pCurrentHttpContext.Request.Browser.Win32 + " Supports Frames = " + pCurrentHttpContext.Request.Browser.Frames + " Supports Tables = " + pCurrentHttpContext.Request.Browser.Tables + " Supports Cookies = " + pCurrentHttpContext.Request.Browser.Cookies + " Supports JavaScript = " + pCurrentHttpContext.Request.Browser.EcmaScriptVersion.ToString() + " Supports Java Applets = " + pCurrentHttpContext.Request.Browser.JavaApplets + " Supports ActiveX Controls = " + pCurrentHttpContext.Request.Browser.ActiveXControls + " Supports JavaScript = " + pCurrentHttpContext.Request.Browser.JavaScript.ToString();

                //string s = "Browser Capabilities: " + "Type = " + pCurrentHttpContext.Request.Browser.Type + " Name = " + pCurrentHttpContext.Request.Browser.Browser + " Version = " + pCurrentHttpContext.Request.Browser.Version + " Major Version = " + pCurrentHttpContext.Request.Browser.MajorVersion + " Minor Version = " + pCurrentHttpContext.Request.Browser.MinorVersion + " Platform = " + pCurrentHttpContext.Request.Browser.Platform + " Is Beta = " + pCurrentHttpContext.Request.Browser.Beta;


                objSqlCommand.Parameters.Add("@p_BrowserInfo", SqlDbType.VarChar);
                objSqlCommand.Parameters["@p_BrowserInfo"].Value = s;

                try
                {
                    objSqlCommand.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    objSqlConnection.Close();
                    objSqlCommand = null;
                    StatusMsg = null;
                }
            }
            catch { }
        }

        private static void WriteLogIntoDatabase_CallBack(IAsyncResult callbackResult)
        {
            LogManagerSendUserLoginNotification objLogManagerSendUserLoginNotification = (LogManagerSendUserLoginNotification)callbackResult.AsyncState;
            callbackResult.AsyncWaitHandle.WaitOne();
            objLogManagerSendUserLoginNotification.EndInvoke(callbackResult);
            callbackResult.AsyncWaitHandle.Close();
            objLogManagerSendUserLoginNotification = null;
        }

        #endregion

        #region WRITE LOG INTO DATABASE FOR USER ACTIVITY
        // Summary:
        //      Write Log into Database for User Activity
        // Parameters:
        //      pageName : page name from where wanto to log User Activity
        //      description : User Activity Description 
        //      eLogEntryType : Acitivty Type
        //      eApplicationName : Applciaiton Name
        //      strUserIpAddress : User Ip Address
        // Remarks:
        //      Write log into Database table dbo.UserLoginNotification
        public static void LogUserActivity(string sessionID, string UserID, string UserName, string pageName, string description, LogEntryType eLogEntryType, ApplicationName eApplicationName, string strUserIpAddress)
        {
            GetLogManagerUserInfo();
            LogManagerLogUserActivity objLogManagerLogUserActivity = new LogManagerLogUserActivity(LogUserActivityInToDatabase);
            IAsyncResult result = objLogManagerLogUserActivity.BeginInvoke(sessionID, UserID, UserName, pageName, description, eLogEntryType, eApplicationName, strUserIpAddress, new AsyncCallback(LogUserActivityInToDatabase_CallBack), objLogManagerLogUserActivity);
            objLogManagerLogUserActivity = null;
            result = null;
        }

        private static void LogUserActivityInToDatabase(string sessionID, string UserID, string UserName, string pageName, string description, LogEntryType eLogEntryType, ApplicationName eApplicationName, string strUserIpAddress)
        {
            SqlConnection objSqlConnection = new SqlConnection(pStrSqlConnection);

            objSqlConnection.Open();

            SqlCommand objSqlCommand = new SqlCommand("Proc_Submit_Logs_AuditTrail", objSqlConnection);
            objSqlCommand.CommandType = CommandType.StoredProcedure;

            objSqlCommand.Parameters.Add("@p_SessionId", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_SessionId"].Value = sessionID;

            objSqlCommand.Parameters.Add("@p_Session_Location", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_Session_Location"].Value = "";

            objSqlCommand.Parameters.Add("@p_UserName", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_UserName"].Value = UserName;

            objSqlCommand.Parameters.Add("@p_PageName", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_PageName"].Value = pageName;

            objSqlCommand.Parameters.Add("@p_Description", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_Description"].Value = description;

            objSqlCommand.Parameters.Add("@p_Application", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_Application"].Value = eApplicationName;

            objSqlCommand.Parameters.Add("@p_UserId", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_UserId"].Value = UserID;

            objSqlCommand.Parameters.Add("@p_LogEntryType", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_LogEntryType"].Value = eLogEntryType;

            objSqlCommand.Parameters.Add("@p_UserIPAddress", SqlDbType.NVarChar);
            objSqlCommand.Parameters["@p_UserIPAddress"].Value = strUserIpAddress;

            try
            {
                objSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Log.cs", "LogUserActivityInToDatabase", ex.Message, ex);
            }
            finally
            {
                objSqlConnection.Close();
                objSqlConnection = null;
                objSqlCommand = null;
            }
        }

        private static void LogUserActivityInToDatabase_CallBack(IAsyncResult callbackResult)
        {
            LogManagerLogUserActivity objLogManagerLogUserActivity = (LogManagerLogUserActivity)callbackResult.AsyncState;
            callbackResult.AsyncWaitHandle.WaitOne();
            objLogManagerLogUserActivity.EndInvoke(callbackResult);
            callbackResult.AsyncWaitHandle.Close();
            objLogManagerLogUserActivity = null;
        }

        #endregion

        #region MAIL

        public static void SendErrorEmail(string strSubject, string strBody, string pagename)
        {
            try
            {

                string strFrom = System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"].ToString();
                string strTo = (pagename == "VolmaxLauncherDownload" ? System.Configuration.ConfigurationManager.AppSettings["VolmaxLauncherEmail"].ToString() : (pagename == "Jscript" ? "vcmweb@thebeastapps.com" : (pagename == "1" ? System.Configuration.ConfigurationManager.AppSettings["volmaxAdmin"].ToString() : System.Configuration.ConfigurationManager.AppSettings["ErrorEmail"].ToString())));
                //string strSMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"].ToString();

                MailMessage SendErrorEmail;
                SendErrorEmail = new MailMessage(strFrom, strTo);
                SendErrorEmail.Subject = strSubject;
                SendErrorEmail.Body = strBody;
                SendErrorEmail.IsBodyHtml = true;

                SmtpClient sendMailClient = new SmtpClient();
                if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
                {
                    sendMailClient.Host = VcmMailNamespace.vcmMail.strAmazonServer;
                    sendMailClient.EnableSsl = true;
                    sendMailClient.Port = VcmMailNamespace.vcmMail.iPort;
                    sendMailClient.Credentials = new System.Net.NetworkCredential(VcmMailNamespace.vcmMail.strAmazonUserName, VcmMailNamespace.vcmMail.strAmazonPassword);
                }
                else
                {
                    sendMailClient.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
                }

                sendMailClient.Send(SendErrorEmail);
                sendMailClient = null;
                SendErrorEmail = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Log.cs", "SendErrorEmail", ex.Message, ex);
            }
        }

        #endregion

        #region WRITE LOG INTO DATABASE SendUserPriceOrder Entry Details
        // Summary:
        //      Write Log into Database for User Audit Trail
        // Parameters:
        //      UserID : ID of logged User
        //      UserName : Name of logged User
        //      Status : Notificaton Status
        //      EventID : Notification Event ID
        //      EventDesc : Notification Event Description
        //      PageName : Name of page for Notification
        //      IPAddress : User Ip Address
        // Remarks:
        //      Write log into Database table dbo.UserLoginNotification
        public static void SendUserPriceOrder(string UserID, string UserName, int Status, int EventID, string EventDesc, string PageName, string IPAddress, string Session_ID, string ASPSessionID, string SessionLogincount)
        {
            try
            {
                GetLogManagerUserInfo();
                LogManagerSendUserPriceOrder objLogManagerSendUserActivity = new LogManagerSendUserPriceOrder(WriteUserPriceOrderActivity);
                IAsyncResult result = objLogManagerSendUserActivity.BeginInvoke(UserID, UserName, Status, EventID, EventDesc, PageName, IPAddress, Session_ID, ASPSessionID, SessionLogincount, new AsyncCallback(WriteLogUserPriceOrderActivity_CallBack), objLogManagerSendUserActivity);
                objLogManagerSendUserActivity = null;
                result = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Log.cs", "SendUserPriceOrder", ex.Message, ex);
            }
        }

        private static void WriteUserPriceOrderActivity(string UserID, string UserName, int Status, int EventID, string EventDesc, string PageName, string IPAddress, string Session_ID, string ASPSessionID, string SessionLogincount)
        {
            try
            {
                SqlConnection objSqlConnection = new SqlConnection(pStrSqlConnection);
                objSqlConnection.Open();

                string StatusMsg = null;
                SqlCommand objSqlCommand = new SqlCommand("Proc_Submit_UserLoginNotification_WebAudit", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;

                objSqlCommand.Parameters.Add("@p_UserID", SqlDbType.Int);
                objSqlCommand.Parameters["@p_UserID"].Value = UserID;

                objSqlCommand.Parameters.Add("@p_UserName", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_UserName"].Value = UserName;

                objSqlCommand.Parameters.Add("@p_status", SqlDbType.Int);
                objSqlCommand.Parameters["@p_status"].Value = Status;

                StatusMsg = ((DescriptionAttribute[])((LoginMessage)Status).GetType().GetField(((LoginMessage)Status).ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false))[0].Description;
                objSqlCommand.Parameters.Add("@p_statusMsg", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_statusMsg"].Value = StatusMsg;

                objSqlCommand.Parameters.Add("@p_SessionID", SqlDbType.Int);

                // if (pManagerSessionState["Session_ID"] != null)
                if (Session_ID != null)
                {
                    objSqlCommand.Parameters["@p_SessionID"].Value = Session_ID;
                }
                else
                {
                    objSqlCommand.Parameters["@p_SessionID"].Value = 0;
                }

                objSqlCommand.Parameters.Add("@p_ASPSessionID", SqlDbType.NVarChar);

                if (EventID.Equals(50) || EventID.Equals(51) || EventID.Equals(52))
                {
                    objSqlCommand.Parameters["@p_ASPSessionID"].Value = "0";
                }
                else
                {
                    objSqlCommand.Parameters["@p_ASPSessionID"].Value = Convert.ToString(ASPSessionID);
                }

                objSqlCommand.Parameters.Add("@p_EventID", SqlDbType.Int);
                objSqlCommand.Parameters["@p_EventID"].Value = EventID;

                objSqlCommand.Parameters.Add("@p_EventDescriptions", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_EventDescriptions"].Value = EventDesc;


                objSqlCommand.Parameters.Add("@p_PageName", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_PageName"].Value = PageName;

                objSqlCommand.Parameters.Add("@p_IPAddress", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_IPAddress"].Value = IPAddress;

                objSqlCommand.Parameters.Add("@p_SessionLoginCount", SqlDbType.Int);
                if (pManagerSessionState["SessionLogincount"] == null)
                {
                    objSqlCommand.Parameters["@p_SessionLoginCount"].Value = 0;
                }
                else
                {
                    objSqlCommand.Parameters["@p_SessionLoginCount"].Value = Convert.ToInt32(SessionLogincount);
                }

                objSqlCommand.Parameters.Add("@p_Application", SqlDbType.NVarChar);
                objSqlCommand.Parameters["@p_Application"].Value = ApplicationName.IRS;
                //string s = "Browser Capabilities: " + "Type = " + HttpContext.Current.Request.Browser.Type + " Name = " + HttpContext.Current.Request.Browser.Browser + " Version = " + HttpContext.Current.Request.Browser.Version + " Major Version = " + HttpContext.Current.Request.Browser.MajorVersion + " Minor Version = " + HttpContext.Current.Request.Browser.MinorVersion + " Platform = " + HttpContext.Current.Request.Browser.Platform + " Is Beta = " + HttpContext.Current.Request.Browser.Beta;
                string s = "Browser Capabilities: " + "Type = " + pCurrentHttpContext.Request.Browser.Type + " Name = " + pCurrentHttpContext.Request.Browser.Browser + " Version = " + pCurrentHttpContext.Request.Browser.Version + " Major Version = " + pCurrentHttpContext.Request.Browser.MajorVersion + " Minor Version = " + pCurrentHttpContext.Request.Browser.MinorVersion + " Platform = " + pCurrentHttpContext.Request.Browser.Platform + " Is Beta = " + pCurrentHttpContext.Request.Browser.Beta + " Is Crawler = " + pCurrentHttpContext.Request.Browser.Crawler + " Is Win16 = " + pCurrentHttpContext.Request.Browser.Win16 + " Is Win32 = " + pCurrentHttpContext.Request.Browser.Win32 + " Supports Frames = " + pCurrentHttpContext.Request.Browser.Frames + " Supports Tables = " + pCurrentHttpContext.Request.Browser.Tables + " Supports Cookies = " + pCurrentHttpContext.Request.Browser.Cookies + " Supports JavaScript = " + pCurrentHttpContext.Request.Browser.EcmaScriptVersion.ToString() + " Supports Java Applets = " + pCurrentHttpContext.Request.Browser.JavaApplets + " Supports ActiveX Controls = " + pCurrentHttpContext.Request.Browser.ActiveXControls + " Supports JavaScript = " + pCurrentHttpContext.Request.Browser.JavaScript.ToString();

                //string s = "Browser Capabilities: " + "Type = " + pCurrentHttpContext.Request.Browser.Type + " Name = " + pCurrentHttpContext.Request.Browser.Browser + " Version = " + pCurrentHttpContext.Request.Browser.Version + " Major Version = " + pCurrentHttpContext.Request.Browser.MajorVersion + " Minor Version = " + pCurrentHttpContext.Request.Browser.MinorVersion + " Platform = " + pCurrentHttpContext.Request.Browser.Platform + " Is Beta = " + pCurrentHttpContext.Request.Browser.Beta;


                objSqlCommand.Parameters.Add("@p_BrowserInfo", SqlDbType.VarChar);
                objSqlCommand.Parameters["@p_BrowserInfo"].Value = s;

                try
                {
                    objSqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogUtility.Error("Log.cs", "WriteUserPriceOrderActivity", ex.Message, ex);
                }
                finally
                {
                    objSqlConnection.Close();
                    objSqlCommand = null;
                    StatusMsg = null;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Log.cs", "WriteUserPriceOrderActivity", ex.Message, ex);
            }
        }

        private static void WriteLogUserPriceOrderActivity_CallBack(IAsyncResult callbackResult)
        {
            LogManagerSendUserPriceOrder objLogManagerSendUser = (LogManagerSendUserPriceOrder)callbackResult.AsyncState;
            callbackResult.AsyncWaitHandle.WaitOne();
            objLogManagerSendUser.EndInvoke(callbackResult);
            callbackResult.AsyncWaitHandle.Close();
            objLogManagerSendUser = null;
        }

        #endregion
    }
}