using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for priceEntityLayer
/// </summary>
/// 
namespace vcmProductNamespace
{
    public class sessionEntityLayer
    {

   

        //public string SessionId
        //{
        //    get;
        //    set;
        //}
        public string SessionId
        {
            get;
            set;
        }

        public string SessionName
        {
            get;
            set;
        }
        public string CurrencyId
        {
            get;
            set;
        }

        public string ProductCode
        {
            get;
            set;
        }
        public string SessionMessage
        {
            get;
            set;
        }

       
         
        public sessionEntityLayer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public sessionEntityLayer(string strSessionId, string strSessionName, string strCurrencyId, string strProductCode, string strSessionMessage)
        {
            //this.SessionId = strSessionId;
            this.SessionId = strSessionId;
            this.SessionName = strSessionName;
            this.CurrencyId = strCurrencyId;
            this.ProductCode = strProductCode;
            this.SessionMessage = strSessionMessage;        
        }


    }
}