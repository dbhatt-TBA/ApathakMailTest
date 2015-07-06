using System;
using System.ComponentModel;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;

/// <summary>
/// Summary description for MySmtpClient
/// </summary>
/// 
namespace VCM.MySmtpClient
{
    public class MySmtpClient : SmtpClient
    {
        public string strAutoURLID = "";
        public string strSessionID = "";
        public string strUserID = "";
        public string strIpAddress = "";
        public string strRecordCreateByID = "";
        


        public MySmtpClient()
        {
        }

        public MySmtpClient(string host)
        {
            this.Host = host;
        }

    }
}
