using System;
using System.Net.Mail;
using System.ComponentModel;
using VCM.MySmtpClient;
using VCM.Common.Log;
using System.Web;

/// <summary>
/// Summary description for VCM_Mail
/// </summary>
/// 
namespace VcmMailNamespace
{
    public class vcmMail
    {
        public vcmMail()
        { }

        public static string strVCM_RrMailAddress = "<div style=\"font-size:7pt;color:navy;font-family:Verdana\"> Sincerely, \n<b>THE BEAST Administration</b>\nThe Beast Apps \nthebeast@thebeastapps.com \nNY: +1-646-688-7500</div> ";
        public static string strVCM_RrMailAddress_Html = "<b>THE BEAST Administration</b> <br/>The Beast Apps <br/>thebeast@thebeastapps.com <br/>NY: +1-646-688-7500<br/>";

        public static string VCM_MailAddress = "Sincerely, \nThe Beast Apps \ninfo@thebeastapps.com \nNY: +1-646-688-7500";
        public static string VCM_MailAddress_In_Html = "Sincerely, <br/>The Beast Apps <br/>info@thebeastapps.com <br/>NY: +1-646-688-7500";
        public static string strAmazonServer = System.Configuration.ConfigurationManager.AppSettings["aws_SMTPServer"].Trim();
        public static string strAmazonUserName = System.Configuration.ConfigurationManager.AppSettings["aws_UserId"].Trim();
        public static string strAmazonPassword = System.Configuration.ConfigurationManager.AppSettings["aws_Password"].Trim();
        public static int iPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["aws_Port"].Trim());

        #region ENUM Applicantion Name and Mail Address

        public enum ApplicationLink
        {
            RiskRecycling = 1,        //	Web # Risk Recycling

            Inflation = 3,            //	Web # Inflation

            CataStrophe = 4,          //	Web # CATS

            Oddlot = 5,               //	Web # OddLot System

            Swaption = 7                //	Web # Swaption System
        }

        public enum ApplicationMailAddress
        {
            [Description("Sincerely, \nRisk Recycling Team \nThe Beast Apps \nriskrecycling@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800 ")]
            RiskRecycling = 1,       //	Web # Risk Recycling
            [Description("Sincerely, \nInflation Team \nThe Beast Apps \ninflation@thebeastapps.com \nNY: +1-646-688-7545 \nLondon: +44 (0)20-7398-2800 ")]
            Inflation = 3,            //	Web # Inflation
            //[Description("Sincerely, \nCatastrophe Markets \nThe Beast Apps \nCAT@thebeastapps.com \nDesk: +1-646-688-7565 \nSingapore: +65 6550 9878, 6550 9879")]
            [Description("Sincerely, \nCatastrophe Markets \nThe Beast Apps \nCAT@thebeastapps.com \nDesk: +1-646-688-7565")]
            CataStrophe = 4,          //	Web # CATS
            [Description("Sincerely, \nOddLot Team \nThe Beast Apps \noddlots@thebeastapps.com \nNY: +1-646-688-7550 ")]
            Oddlot = 5,                //	Web # OddLot System
            [Description("Sincerely, \nSwaption Team \nThe Beast Apps \nvolmax@thebeastapps.com \nNY: +1-646-688-7550 ")]
            Swaption = 7                //	Web # OddLot System
        }

        public enum ApplicationMailAddressInHTML
        {
            [Description("Sincerely, <br/>Risk Recycling Team <br/>The Beast Apps <br/>riskrecycling@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800  ")]
            RiskRecycling = 1,       //	Web # Risk Recycling

            [Description("Sincerely, <br/>Inflation Team <br/>The Beast Apps <br/>inflation@thebeastapps.com <br/>NY: +1-646-688-7545 <br/>London: +44 (0)20-7398-2800  ")]
            Inflation = 3,            //	Web # Inflation

            //[Description("Sincerely, <br/>Catastrophe Markets <br/>The Beast Apps <br/>CAT@thebeastapps.com <br/>Desk: +1-646-688-7565  <br/>Singapore: +65 6550 9878, 6550 9879")]
            [Description("Sincerely, <br/>Catastrophe Markets <br/>The Beast Apps <br/>CAT@thebeastapps.com <br/>Desk: +1-646-688-7565")]
            CataStrophe = 4,          //	Web # CATS

            [Description("Sincerely, <br/>OddLot Team  <br/>The Beast Apps <br/>oddlots@thebeastapps.com <br/>NY: +1-646-688-7550  ")]
            Oddlot = 5,                //	Web # OddLot System

            [Description("Sincerely, \nSwaption Team \nThe Beast Apps \nvolmax@thebeastapps.com \nNY: +1-646-688-7550 ")]
            Swaption = 7                //	Web # OddLot System
        }

        #endregion

        private MailMessage _mailMessage;
        //private SmtpClient _smptpClient;
        private MySmtpClient My_smptpClient;

        private string _mailTo, _mailCC, _mailBCC, _mailFrom, _mailSubject, _mailBody, _fileNameToAttach;
        bool _bSendAsync, _bIsBodyHtml;

        public string To
        {
            get
            {
                return _mailTo;
            }
            set
            {
                _mailTo = value;
            }
        }

        public string CC
        {
            get
            {
                return _mailCC;
            }
            set
            {
                _mailCC = value;
            }
        }

        public string BCC
        {
            get
            {
                return _mailBCC;
            }
            set
            {
                _mailBCC = value;
            }
        }

        public string From
        {
            get
            {
                return _mailFrom;
            }
            set
            {
                _mailFrom = value;
            }
        }

        public string Subject
        {
            get
            {
                return _mailSubject;
            }
            set
            {
                _mailSubject = value;
            }
        }

        public string Body
        {
            get
            {
                return _mailBody;
            }
            set
            {
                _mailBody = value;
            }
        }

        public string FileToAttach
        {
            get
            {
                return _fileNameToAttach;
            }
            set
            {
                _fileNameToAttach = value;
            }
        }

        public bool SendAsync
        {
            get
            {
                return _bSendAsync;
            }
            set
            {
                _bSendAsync = value;
            }
        }

        public bool IsBodyHtml
        {
            get
            {
                return _bIsBodyHtml;
            }
            set
            {
                _bIsBodyHtml = value;
            }
        }

        // START --------------- AutoURL 
        private string _AutoURLID, _SessionID, _UserID, _IpAddress, _RecordCreateBy;

        public string AutoURLGuID
        {
            get
            {
                return _AutoURLID;
            }
            set
            {
                _AutoURLID = value;
            }
        }

        public string SessionID
        {
            get
            {
                return _SessionID;
            }
            set
            {
                _SessionID = value;
            }
        }

        public string UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return _IpAddress;
            }
            set
            {
                _IpAddress = value;
            }
        }

        public string RecordCreateBy
        {
            get
            {
                return _RecordCreateBy;
            }
            set
            {
                _RecordCreateBy = value;
            }
        }
        // END --------------- AutoURL 

        public void SendMail(int SetReply)
        {
            try
            {
                _mailMessage = new MailMessage(_mailFrom, _mailTo);
                if (!string.IsNullOrEmpty(_mailCC)) { _mailMessage.CC.Add(_mailCC); }
                if (!string.IsNullOrEmpty(_mailBCC)) { _mailMessage.Bcc.Add(_mailBCC); }

                _mailMessage.Subject = _mailSubject;
                _mailMessage.Body = _mailBody;
                _mailMessage.IsBodyHtml = _bIsBodyHtml;
                if (SetReply == 1)
                {
                    _mailMessage.ReplyToList.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ReplyTo"].ToString()));
                }

                My_smptpClient = new MySmtpClient();

                if (System.Configuration.ConfigurationManager.AppSettings["SystemRunningOn"].ToLower() == "amazon")
                {
                    My_smptpClient.Host = strAmazonServer;
                    My_smptpClient.EnableSsl = true;
                    My_smptpClient.Port = iPort;
                    My_smptpClient.Credentials = new System.Net.NetworkCredential(strAmazonUserName, strAmazonPassword);
                }
                else
                {
                    My_smptpClient.Host = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"];
                }                
                
                if (_bSendAsync)
                {
                    My_smptpClient.strAutoURLID = _AutoURLID;
                    My_smptpClient.strSessionID = _SessionID;
                    My_smptpClient.strUserID = _UserID;
                    My_smptpClient.strIpAddress = _IpAddress;
                    My_smptpClient.strRecordCreateByID = _RecordCreateBy;

                    My_smptpClient.SendCompleted += new SendCompletedEventHandler(_smptpClient_SendCompleted);
                    My_smptpClient.SendAsync(_mailMessage, "VCMmail");
                }
                else
                {
                    My_smptpClient.Send(_mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //LogUtility.Error("vcmMail", "SendMail()", ex.Message, ex);
            }
            finally
            {
                _mailMessage = null;                
                My_smptpClient = null;
            }
        }

        private void _smptpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                try
                {
                    if (((MySmtpClient)(sender)).strAutoURLID != null && ((MySmtpClient)(sender)).strIpAddress != null && ((MySmtpClient)(sender)).strRecordCreateByID != null && ((MySmtpClient)(sender)).strSessionID != null && ((MySmtpClient)(sender)).strUserID != null)
                    {
                        //// 1 - Send Successful
                        //// 2 - Send Failed
                        vcmProductNamespace.cDbHandler.SuccessfulSendAutoURL(Convert.ToString(((MySmtpClient)(sender)).strSessionID), Convert.ToString(((MySmtpClient)(sender)).strUserID), "2", Convert.ToString(((MySmtpClient)(sender)).strAutoURLID), Convert.ToString(((MySmtpClient)(sender)).strIpAddress), Convert.ToString(((MySmtpClient)(sender)).strRecordCreateByID));
                    }
                }
                catch (Exception ex)
                {
                    LogUtility.Error("vcmMail", "_smptpClient_SendCompleted()", ex.Message, ex);
                }

                VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();
                _vcmMail.To = System.Configuration.ConfigurationManager.AppSettings["volmaxAdmin"].ToString();
                _vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();

                _vcmMail.SendAsync = true;
                _vcmMail.Subject = "Mail has not been sent " + _mailSubject;

                _vcmMail.Body = "<div style=\"font-size:8pt;font-family:Verdana\">Dear Broker,<br/><br/> Mail has not been sent to " + _mailTo + " users for below session " +
               " <table><tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + _mailSubject + " </td></tr> <tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">ERROR MESSAGE : " + e.Error.Message + " </td></tr> <tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">ERROR INNEREXCEPTION : " + e.Error.InnerException + " </td></tr> <tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">ERROR STAKETRACE : " + e.Error.StackTrace + " </td></tr>  <tr> <td>&nbsp; </br></br> </td></tr>  <tr><td style=\"FONT-SIZE: 8pt; FONT-FAMILY: Verdana;\">" + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString() + " </td></tr></table> " +
                "<br/><br/> ";

                _vcmMail.IsBodyHtml = true;
                _vcmMail.SendMail(0);
                _vcmMail = null;

                LogUtility.Error("vcmMail", "_smptpClient_SendCompleted()", e.Error.Message, e.Error);
            }
            else if (e.Cancelled)
            {
                LogUtility.Info("vcmMail", "_smptpClient_SendCompleted()", "The asynchronous operation has been cancelled.");
            }
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
    }
}