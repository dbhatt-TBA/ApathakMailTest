using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using VCM.Common.Log;
using BeastClientPlugIn;
using Microsoft.AspNet.SignalR;

/// <summary>
/// Summary description for VCMComet
/// </summary>
public class VCMComet
{
    public static volatile VCMComet _instance = null;
    private static object syncRoot = new Object();

    private VCMComet()
    {

    }

    public static VCMComet Instance
    {
        get
        {
            lock (syncRoot)
            {
                if (_instance == null)
                {
                    _instance = new VCMComet();
                }
            }
            return _instance;
        }
    }

    //public void sendDataRequest(PublishingEvent ev)
    //{
    //    if (ev.Cancel)
    //        return;
    //}

    private void setNodeValueLocal(ref object val, DOMDataNode node, string value)
    {
        if (null != node)
        {
            switch (node.DataType)
            {
                case DOMDataNodeValueType.DATANODEVALUETYPE_INTEGER:
                    int intVal;
                    if (int.TryParse(value, out intVal))
                        val = int.Parse(value);
                    break;

                case DOMDataNodeValueType.DATANODEVALUETYPE_DOUBLE:
                    decimal decVal;
                    if (decimal.TryParse(value, out decVal))
                        val = double.Parse(value);
                    break;

                case DOMDataNodeValueType.DATANODEVALUETYPE_DATE:
                    DateTime datVal;
                    if (DateTime.TryParse(value, out datVal))
                        val = DateTime.Parse(value);
                    break;

                default:
                    val = value;
                    break;
            }

        }
    }

    //public void sendExplicitConfigurationDataRequest(PublishingEvent ev)
    //{
    //    if (ev.Cancel)
    //        return;

    //    object val = null;

    //    string msgContents = ev.Message.GetData<string>("message");
    //    string senderDet = ev.Message.GetData<string>("sender");

    //    if (senderDet == "ButtonList")
    //    {
    //        string parentId = msgContents.Split('#')[0];
    //        string value = msgContents.Split('#')[1];


    //        DOMDataDocument document = beastVolPrimGrid.Instance.volPremImage;
    //        document.BeginGroupedUpdate();

    //        DOMDataNode nodeList = null;
    //        IEnumerable<DOMDataNode> nNodeListGrid = document.RootNode.ChildNodes.OfType<DOMDataNode>().Where(eel => eel.NodeName.Equals(parentId));

    //        foreach (var nodeTmp in nNodeListGrid)
    //        {
    //            nodeList = nodeTmp;
    //            break;
    //        }

    //        setNodeValueLocal(ref val, nodeList, value);


    //        try
    //        {
    //            nodeList.DataValue = val;
    //        }
    //        catch (Exception e)
    //        {

    //        }

    //        document.EndGroupedUpdate();
    //    }
    //    else if (senderDet == "indiCell")
    //    {


    //        string parentId = msgContents.Split('#')[0];
    //        string fieldId = msgContents.Split('#')[1];
    //        string value = msgContents.Split('#')[2];

    //        DOMDataDocument document = beastVolPrimGrid.Instance.volPremImage;
    //        document.BeginGroupedUpdate();
    //        DOMDataNode nodeGrid = null;
    //        IEnumerable<DOMDataNode> nNodeListGrid = document.RootNode.ChildNodes.OfType<DOMDataNode>().Where(eel => eel.NodeName.Equals(parentId));

    //        foreach (var nodeTmp in nNodeListGrid)
    //        {
    //            nodeGrid = nodeTmp;
    //            break;
    //        }

    //        int row;
    //        int col;

    //        util.GetRowColGridField(fieldId, out row, out col);

    //        DOMDataNode node = null;

    //        node = nodeGrid.ChildNodes[row].ChildNodes[col];

    //        //IEnumerable<DOMDataNode> nNodeList = nodeGrid.ChildNodes.OfType<DOMDataNode>().Where(eel => eel.NodeID.Equals(fieldId));

    //        //foreach (var nodeTmp in nNodeList)
    //        //{
    //        //    node = nodeTmp;
    //        //    break;
    //        //}


    //        setNodeValueLocal(ref val, node, value);


    //        try
    //        {
    //            node.DataValue = val;
    //        }
    //        catch (Exception e)
    //        {

    //        }

    //        document.EndGroupedUpdate();
    //    }
    //    else if (senderDet == "indiCellOld")
    //    {


    //        string parentId = msgContents.Split('#')[0];
    //        string fieldId = msgContents.Split('#')[1];
    //        string value = msgContents.Split('#')[2];

    //        DOMDataDocument document = beastVolPrimGrid.Instance.volPremImage;
    //        document.BeginGroupedUpdate();

    //        DOMDataNode node = null;

    //        int row;
    //        int col;

    //        int oldNodeID;
    //        int valToMult = 0;


    //        util.GetRowColGridField(fieldId, out row, out col);

    //        if (parentId == "100000028")
    //        {
    //            valToMult = 2800; 
    //        }
    //        else if (parentId == "100000030")
    //        {
    //            valToMult = 4200;
    //        }
    //        else if (parentId == "100000029")
    //        {
    //            valToMult = 1400;
    //        }

    //        oldNodeID = valToMult + col + (row * 14);

    //        //IEnumerable<DOMDataNode> nNodeListGrid = document.RootNode.ChildNodes.OfType<DOMDataNode>().Where(eel => eel.NodeName.Equals(Convert.ToString(oldNodeID)));

    //        //foreach (var nodeTmp in nNodeListGrid)
    //        //{
    //        //    node = nodeTmp;
    //        //    break;
    //        //}

    //        node = document.get_NodeByID(oldNodeID.ToString());

    //        setNodeValueLocal(ref val, node, value);

    //        try
    //        {
    //            node.DataValue = val;
    //        }
    //        catch (Exception e)
    //        {

    //        }

    //        document.EndGroupedUpdate();
    //    }
    //    else if (senderDet == "")
    //    {
    //        sendExplicitOtherDataResponse();
    //    }

    //}



    public void sendExplicitOtherDataResponse()
    {

        //SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage("451", "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");

        //sendOtherDataResponse(bi.GruopID, "Currency", "Currency", bi.Currency);
        //sendOtherDataResponse(bi.GruopID, "Currency", "Straddle", bi.Straddle);
        //sendOtherDataResponse(bi.GruopID, "Vols", "Vols", bi.Vols);
        //sendOtherDataResponse(bi.GruopID, "VolShift", "VolShift", bi.VolShift);
        //sendOtherDataResponse(bi.GruopID, "Currency", "List", bi.List);
        //sendOtherDataResponse(bi.GruopID, "PremGridTitle", "Title", bi.PremGridTitle);
    }


    public void sendVolGridDataResponse(string GroupName, string row, string col, string value)
    {
        //Hashtable htCustomerIDs = getAllCustomerForChannel("/service/voldata");

        //foreach (string custIDtmp in htCustomerIDs.Keys)
        //{
        //    string strReturnValue = row + "#" + col + "#" + value;

        //    sendDataResponse("/service/voldata", custIDtmp, strReturnValue, "voldata");
        //}
        string custIDtmp = "";
        string strReturnValue = row + "#" + col + "#" + value;
        sendDataResponse(GroupName, "/service/voldata", custIDtmp, strReturnValue, "voldata");
    }

    public void sendPremGridDataResponse(string GroupName, string row, string col, string value)
    {
        //Hashtable htCustomerIDs = getAllCustomerForChannel("/service/premdata");

        //foreach (string custIDtmp in htCustomerIDs.Keys)
        //{
        //    string strReturnValue = row + "#" + col + "#" + value;

        //    sendDataResponse("/service/premdata", custIDtmp, strReturnValue, "premdata");
        //}

        string custIDtmp = "";
        string strReturnValue = row + "#" + col + "#" + value;
        sendDataResponse(GroupName, "/service/premdata", custIDtmp, strReturnValue, "premdata");
    }



    public void sendStrikeGridDataResponse(string GroupName, string row, string col, string value)
    {
        //Hashtable htCustomerIDs = getAllCustomerForChannel("/service/strikedata");

        //foreach (string custIDtmp in htCustomerIDs.Keys)
        //{
        //    string strReturnValue = row + "#" + col + "#" + value;

        //    sendDataResponse("/service/strikedata", custIDtmp, strReturnValue, "strikedata");
        //}

        string custIDtmp = "";
        string strReturnValue = row + "#" + col + "#" + value;
        sendDataResponse(GroupName, "/service/strikedata", custIDtmp, strReturnValue, "strikedata");
    }

    public void sendOtherDataResponse(string GroupName, string row, string col, string value)
    {
        //Hashtable htCustomerIDs = getAllCustomerForChannel("/service/otherdata");

        //foreach (string custIDtmp in htCustomerIDs.Keys)
        //{
        //    string strReturnValue = row + "#" + col + "#" + value;

        //    sendDataResponse("/service/otherdata", custIDtmp, strReturnValue, "otherdata");
        //}

        string custIDtmp = "";
        string strReturnValue = row + "#" + col + "#" + value;
        sendDataResponse(GroupName, "/service/otherdata", custIDtmp, strReturnValue, "otherdata");
    }



    public void sendDataResponse(string GroupName, string channel, string customerID, string data, string sender)
    {
        //IEnumerable<IClient> allReceiverCust = (IEnumerable<IClient>)clientRepository.GetByCustomerID(customerID);

        //foreach (IClient recvrClient in allReceiverCust)
        //{
        //    Message message = new Message
        //    {
        //        channel = channel,
        //        clientId = recvrClient.ID
        //    };

        //    message.SetData("sender", sender);
        //    recvrClient.Enqueue(message);
        //    recvrClient.FlushQueue();
        //}

        Send(GroupName, sender, data);
    }

    public void Send(string GroupName, string name, string message)
    {
        // Call the broadcastMessage method to update clients.
        try
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Group(GroupName).handleIncomingMessageFromBeastSignalR(name, message);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("VCMComet.cs:: Send :: " + errorMessage.ToString());

            string strErrorDesc = "GroupName: " + GroupName + "; " + "SenderName: " + name + "; " + "Message: " + message + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "Send()", strErrorDesc, ex);
        }
    }
    /*Token Method*/
    /*****************/

    public string[] ValidateAuthToken(string UserID, string ClientType, string GUID)
    {

        string[] TokenMessage = { "True", "" };

   
        return TokenMessage;
    }




    /******************/

    public void sendOtherDataResponseToConnection(string ConnectionID, string row, string col, string value)
    {
        string custIDtmp = "";
        string strReturnValue = row + "#" + col + "#" + value;

        try
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Client(ConnectionID).handleIncomingMessageFromBeastSignalR("otherdata", strReturnValue);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ConnectionID: " + ConnectionID + "; " + "Row: " + row + "; " + "Col: " + col + "; " + "Value: " + value + ex.Message;
            LogUtility.Error("VCMComet.cs", "sendOtherDataResponseToConnection()", strErrorDesc, ex);
        }
    }

    #region Generic

    public void deleteBeastImagesForConnectionID(string ConnectionID)
    {
        BeastConn.Instance.deleteBeastImageByConnectionID(ConnectionID);
    }

    public void connectToSharedBeastImage(string GroupName, string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID, string ConnectionIDSignalR, string ActualProductID, string InstanceID, string username)
    {
        BeastConn.Instance.shareProductImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR, InstanceID, ActualProductID, username);
    }

    public void connectToBeastImage(string GroupName, string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID, string ConnectionIDSignalR, string ActualProductID, string UserMode, string username)
    {
        BeastConn.Instance.openProductImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR, ActualProductID, UserMode, username);
        //DOMDataDocument document = BeastConn.Instance.getBeastImage(UserID, "152").DocImage;
        //document.BeginGroupedUpdate();

        //DOMDataNode nodeList = null;
        //IEnumerable<DOMDataNode> nNodeListGrid = document.RootNode.ChildNodes.OfType<DOMDataNode>();

        //Hashtable tmpHashTable = new Hashtable();

        //foreach (var nodeTmp in nNodeListGrid)
        //{
        //    try
        //    {
        //        int nodeNameInt = 0;
        //        bool isValideNodeName = int.TryParse(nodeTmp.NodeName, out nodeNameInt);
        //        string nodeDataValue = nodeTmp.DataValue.ToString();

        //        if (nodeDataValue.ToString().Length > 0)
        //        {
        //            if (nodeDataValue.IndexOf("<items><item>") == -1)
        //            {
        //                if (Convert.ToInt32(Definations.NodeDataStatus.DATANODEVALUESTATE_NORMAL) == Convert.ToInt32(nodeTmp.DataState))
        //                {
        //                    Send_BondYield_Calc(nodeTmp.NodeName, nodeTmp.DataValue.ToString());
        //                    tmpHashTable.Add(nodeTmp.NodeName, nodeTmp.DataValue.ToString());
        //                }
        //                else
        //                {
        //                    Send_BondYield_Calc(nodeTmp.NodeName, "");
        //                }
        //            }
        //            else
        //            {
        //                TextReader trForXML = new StringReader(nodeTmp.DataValue.ToString().Replace("\"", ""));
        //                XDocument _XMLDoc = XDocument.Load(trForXML);

        //                string nodeValueStr = Convert.ToString(nodeNameInt - 5000);

        //                Send_BondYield_Calc("DDList#" + nodeValueStr, UtilComman.ConvertXMLToJSONString(_XMLDoc));

        //                if (tmpHashTable.ContainsKey(nodeValueStr))
        //                {
        //                    Send_BondYield_Calc(nodeValueStr, tmpHashTable[nodeValueStr].ToString());
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ee)
        //    { 

        //    }
        //}
    }

    public void setValueInBeastImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID, string name, string message, string ActualProductID, string username = "")
    {
        try
        {
            object val = null;
            string[] msgContents = message.Split('#');
            string senderDet = msgContents[0];
            string parentId = msgContents[1];
            string value = msgContents[2];

            if (senderDet == "DDList")
            {
                //string parentId = msgContents.Split('#')[0];
                //string value = msgContents.Split('#')[1];

                DOMDataDocument document = BeastConn.Instance.getBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ActualProductID, false, "", username).DocImage;
                document.BeginGroupedUpdate();

                DOMDataNode nodeList = null;
                IEnumerable<DOMDataNode> nNodeListGrid = document.RootNode.ChildNodes.OfType<DOMDataNode>().Where(eel => eel.NodeName.Equals(parentId));

                foreach (var nodeTmp in nNodeListGrid)
                {
                    nodeList = nodeTmp;
                    break;
                }

                if (value == "clr")
                {
                    nodeList.DataState = DOMDataNodeValueState.DATANODEVALUESTATE_BLANK;
                }
                else
                {
                    setNodeValueLocal(ref val, nodeList, value);
                    nodeList.DataValue = val;
                }
                document.EndGroupedUpdate();
            }
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "setValueInBeastImage()", strErrorDesc, ex);
        }
    }

    public string getImageInstanceID(string GroupName, string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID, string ConnectionIDSignalR, string ActualProductID)
    {
        return BeastConn.Instance.getBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ActualProductID).InstanceID.Split(':')[1].Trim();
    }
    #endregion

    #region Generic

    public void Send_Message_To_Client_Connection_Generic(string connectionID, string updateType, string updateEleType, string eleID, string eleValue, string HTMLClientID)
    {
        try
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Client(connectionID).MessageFromServer(updateType, updateEleType, eleID, eleValue, HTMLClientID);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("VCMComet.cs:: Send_Message_To_Client_Connection_Generic :: " + errorMessage.ToString());

            string strErrorDesc = "UpdateEleType: " + updateEleType + "; " + "EleID: " + eleID + "; " + "EleValue: " + eleValue + "; " + "ConnectionId: " + connectionID + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "Send_Message_To_Client_Connection_Generic()", strErrorDesc, ex);
        }
    }

    public void Send_Message_To_Client_Group_Generic(string GroupName, string updateType, string updateEleType, string eleID, string eleValue, string HTMLClientID)
    {
        try
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Group(GroupName).MessageFromServer(updateType, updateEleType, eleID, eleValue, HTMLClientID);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("VCMComet.cs:: Send_Message_To_Client_Group_Generic :: " + errorMessage.ToString());

            string strErrorDesc = "UpdateEleType: " + updateEleType + "; " + "EleID: " + eleID + "; " + "EleValue: " + eleValue + "; " + "GroupName: " + GroupName + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "Send_Message_To_Client_Group_Generic()", ex.Message, ex);
        }
    }

    public void Send_Message_To_Client_Group_Generic_Except_Conn(string GroupName, string updateType, string updateEleType, string eleID, string eleValue, string HTMLClientID, string ConnectionID)
    {
        try
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Group(GroupName, ConnectionID).MessageFromServer(updateType, updateEleType, eleID, eleValue, HTMLClientID);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("VCMComet.cs:: Send_Message_To_Client_Group_Generic :: " + errorMessage.ToString());

            string strErrorDesc = "UpdateEleType: " + updateEleType + "; " + "EleID: " + eleID + "; " + "EleValue: " + eleValue + "; " + "GroupName: " + GroupName + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "Send_Message_To_Client_Group_Generic()", ex.Message, ex);
        }
    }

    #region Grid Image
    public void Send_Message_To_Client_Connection_Generic_Grid(string connectionID, string updateType, string updateEleType, string GridID, string Row, string Column, string EleValue, string HTMLClientID)
    {
        try
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Client(connectionID).MessageFromServerGrid(updateType, updateEleType, GridID, Row, Column, EleValue, HTMLClientID);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("VCMComet.cs:: Send_Message_To_Client_Connection_Generic_Grid :: " + errorMessage.ToString());

            string strErrorDesc = "GridID: " + GridID + "; " + "Row Column: " + Row + "_ " + Column + "; " + "EleValue: " + EleValue + "; " + "ConnectionId: " + connectionID + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "Send_Message_To_Client_Connection_Generic_Grid()", strErrorDesc, ex);
        }
    }

    public void Send_Message_To_Client_Group_Generic_Grid(string GroupName, string updateType, string updateEleType, string GridID, string Row, string Column, string EleValue, string HTMLClientID)
    {
        try
        {
            string strLogDesc = "GridID: " + GridID + "; " + "Row Column: " + Row + "_ " + Column + "; " + "EleValue: " + EleValue + "; " + "GroupName: " + GroupName;
            LogUtility.Info("VCMComet.cs", "Send_Message_To_Client_Group_Generic_Grid()", strLogDesc);
            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            context.Clients.Group(GroupName).MessageFromServerGrid(updateType, updateEleType, GridID, Row, Column, EleValue, HTMLClientID);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("VCMComet.cs:: Send_Message_To_Client_Group_Generic_Grid :: " + errorMessage.ToString());

            string strErrorDesc = "GridID: " + GridID + "; " + "Row Column: " + Row + "_ " + Column + "; " + "EleValue: " + EleValue + "; " + "GroupName: " + GroupName + "; " + ex.Message;
            LogUtility.Error("VCMComet.cs", "Send_Message_To_Client_Group_Generic_Grid()", ex.Message, ex);
        }
    }
    #endregion


    #endregion



}
