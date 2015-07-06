using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for volEntity
/// </summary>
/// 
namespace vcmProductNamespace
{
    public class cEntityLayer
    {
        //SwaptionId,	your_curve	Consensus_Curve,OPEN_ORDER			
        //EntityName,Ticker,maturitytenor,Action_Desc,ORDER_STACK_TIME,ORDER_STATUS,Traded
        public string SwaptionId
        {
            get;
            set;
        }

        public string SwaptionType
        {
            get;
            set;
        }

        public string SwaptionStructure
        {
            get;
            set;
        }

        public string YourCurve
        {
            get;
            set;
        }

        public string ConsensusCurve
        {
            get;
            set;
        }

        public string Action
        {
            get;
            set;
        }

        public string OrderStatus
        {
            get;
            set;
        }

        public string Traded
        {
            get;
            set;
        }

        public string OpenOrder
        {
            get;
            set;
        }

        public string OrderStack
        {
            get;
            set;
        }

        public string BuySell
        {
            get;
            set;
        }

        public string AspSessionID
        {
            get;
            set;
        }

        public string TradeToolTip
        {
            get;
            set;
        }
        public string SwitchTraded
        {
            get;
            set;
        }
        public cEntityLayer()
        {
            // TODO: Add constructor logic here
        }
        public string PhaseType
        {
            get;
            set;
        }
        public string BpsDiff
        {
            get;
            set;
        }
        public string OrderIndication
        {
            get;
            set;
        }
        //public cEntityLayer_IRS(string strAspSessionID, string swaptionId, string action, string orderStatus, string traded, string openOrder, string orderStack, string buysell, string strTradedTooltip, string strSwitchTraded)
        //{
        //    string strOrderStack = orderStack.Replace("\\s", " ");
        //    string strTradeTooltip = strTradedTooltip.Replace("\\s", " ");
        //    this.SwaptionId = swaptionId;
        //    this.SwaptionType = swaptionId;
        //    this.Action = action;
        //    this.OrderStatus = orderStatus;
        //    this.Traded = traded;
        //    this.OpenOrder = openOrder;
        //    this.OrderStack = strOrderStack.Replace("\\n", "\n");
        //    this.BuySell = buysell;
        //    this.AspSessionID = strAspSessionID;
        //    this.TradeToolTip = strTradeTooltip.Replace("\\n", "\n");
        //    this.SwitchTraded = strSwitchTraded;

        //}





        public cEntityLayer(string strAspSessionID, string swaptionId, string action, string orderStatus, string traded, string openOrder, string orderStack, string buysell, string strTradedTooltip, string strSwitchTraded,string strPhaseType, string strBpsdiff,string strOrderIndication)
        {
            string strOrderStack = orderStack.Replace("\\s", " ");
            string strTradeTooltip = strTradedTooltip.Replace("\\s", " ");
            this.SwaptionId = swaptionId;
            this.SwaptionType = swaptionId;
            this.Action = action;
            this.OrderStatus = orderStatus;
            this.Traded = traded;
            this.OpenOrder = openOrder;
            this.OrderStack = strOrderStack.Replace("\\n", "\n");
            this.BuySell = buysell;
            this.AspSessionID = strAspSessionID;
            this.TradeToolTip = strTradeTooltip.Replace("\\n", "\n");
            this.SwitchTraded = strSwitchTraded;
            this.PhaseType = strPhaseType;
            this.BpsDiff = strBpsdiff;
            this.OrderIndication = strOrderIndication;

        }
    }
}