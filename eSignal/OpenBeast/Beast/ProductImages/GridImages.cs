using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Text;
using BeastClientPlugIn;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Xml.Linq;
using System.Data.SqlClient;
using VCM.Common;
using VCM.Common.Log;
using Microsoft.AspNet.SignalR;
using System.Configuration;
using System.Collections.Concurrent;


/// <summary>
/// Summary description for GridImages
/// </summary>
/// 
public class GridImages : IProductImage
{
    #region member devlaration
    private DOMDataDocument _beastVolPrimGridImagesDataDoc = null;

    private string _userID = "0";
    private string _productID = "451";
    private string _customerID = "";
    private string _instanceID = "";
    private string _connectionID = "";

    private string _specialImageID = "";
    private string _gruopID = "";
    private string _htmlClientID = "";
    private string _actualProductID = "";

    private bool _isDocumentAlive;
    private bool _isStale;
    private bool _isSharedImage;
    private int _refCount;
    //START ------ Grid Field

    private DataTable _dtPremGrid;
    private DataTable _dtStrikeGrid;
    private DataTable _dtVolGrid;

    private DataTable _dtPremGridNodeIDs;
    private DataTable _dtStrikeGridNodeIDs;
    private DataTable _dtVolGridNodeIDs;

    public string Currency;
    public string Straddle;
    public string Vols;
    public string VolShift;
    public string List;
    public string PremGridTitle;

    public string CurrencyDataValue;

    //END ------  Grid Field

    #endregion

    #region constructor

    public GridImages(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent sa, bool strIsSharedImage, string strInstanceID, string username)
    {
        initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);

        if (strIsSharedImage == true)
        {
            shareBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, strInstanceID, ref sa, username);
        }
        else
        {
            createBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, ref sa, username);
        }

        _isStale = false;
    }

    public void initDataForImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID)
    {
        IsDocumentAlive = false;

        ProductID = strProductID;
        ConnectionID = strConnectionID;
        UserID = strUserID;
        CustomerID = strCustomerID;
        SpecialImageID = strSpecialImageID;
        ActualProductID = strActualProductID;

        string eProductName = strActualProductID;

        if (SpecialImageID == "0")
            HtmlClientID = eProductName.ToString();
        else
            HtmlClientID = SpecialImageID;

        GruopID = BeastConn.Instance.getUserProductKeyForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID);

        _dtPremGrid = new DataTable("dtPremGrid");
        _dtVolGrid = new DataTable("dtVolGrid");
        _dtStrikeGrid = new DataTable("dtStrikeGrid");

        _dtPremGridNodeIDs = new DataTable("dtPremGridNodeIDs");
        _dtStrikeGridNodeIDs = new DataTable("dtStrikeGridNodeIDs");
        _dtVolGridNodeIDs = new DataTable("dtVolGridNodeIDs");

    }
    #endregion

    #region code for creating Beast Image and Event Handling
    public void createBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string username)
    {
        try
        {
            if (DocImage != null)
            {
                return;
            }

            Scripting.Dictionary Props = new Scripting.Dictionary();
            Object key = "AppDefs";
            Object value = strConnectionID;
            Props.Add(ref key, ref value);

            Object key1 = "ImpersonatedUser";
            Object value1 = username;
            Props.Add(ref key1, ref value1);

            Object key2 = "OnlyVisible";
            Object value2 = 1;
            Props.Add(ref key2, ref value2);

            string eProductName = strActualProductID;
            DocImage = _sa.RequestDocument("appid:" + Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]), Props);
            LogUtility.Info("GridImages.cs", "createBeastImage()ImpersonatedUser", "username:" + username + " ProductName:" + eProductName);
            //DocImage = _sa.RequestDocument("appid:" + Convert.ToString((int)Definations.BeastImageAppID.vcm_calc_realtimedata), Props);

            DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
            DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
            DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
            DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
            DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
            DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);

            string logDesc = getLogDescForGenericInfo();

            LogUtility.Info("GridImages.cs", "createBeastImage()", logDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "createBeastImage()", strErrorDesc, ex);
        }
    }

    public void openBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string ConnectionIDSignalR, string username)
    {
        //try
        //{
        //    string logDesc = getLogDescForGenericInfo();

        //    LogUtility.Info("GridImages.cs", "openBeastImage()", logDesc);

        //    sendDocumentStatusToConn(ConnectionIDSignalR);

        //    if (DocImage == null)
        //    {
        //        initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);
        //        createBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, ref _sa, username);
        //    }
        //    else if (IsDocumentAlive == false)
        //    {
        //        BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, true, ActualProductID); // remove beast image from the list

        //        //initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);

        //        //Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);

        //        //Scripting.Dictionary Props = new Scripting.Dictionary();

        //        //Object key = "SID";
        //        //Object value = Convert.ToString((int)eProductName);
        //        //Props.Add(ref key, ref value);

        //        //Object key1 = "AppDefs";
        //        //Object value1 = strConnectionID;
        //        //Props.Add(ref key1, ref value1);

        //        //DocImage = _sa.RequestDocument(InstanceID, Props);

        //        //DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
        //        //DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
        //        //DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
        //        //DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
        //        //DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
        //        //DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);
        //    }
        //    else
        //    {
        //        DataTable d = new DataTable();
        //        d.Columns.Add("i");
        //        d.Columns.Add("d");
        //        int batchSize = 100;
        //        int updatesCounter = 0;
        //        int totalUpdates = valStore.Count;
        //        int endPointer = totalUpdates - 1;
        //        int i = 0;

        //        try
        //        {
        //            batchSize = Convert.ToInt32(ConfigurationManager.AppSettings["SignalRSendRowSize"].ToString());
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        foreach (var valItem in valStore.OrderBy(valItem => valItem.Key))
        //        {
        //            DataRow r = d.NewRow();
        //            r["i"] = valItem.Key.ToString();
        //            r["d"] = valItem.Value.ToString();
        //            d.Rows.Add(r);
        //            if (updatesCounter >= batchSize || i == endPointer)
        //            {
        //                var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
        //                //context.Clients.Group(GruopID).ArrayFromServerGrid(d, "vcm_calc_Excelshare");
        //                context.Clients.Client(ConnectionIDSignalR).ArrayFromServerGrid(d, "vcm_calc_Excelshare");
        //                updatesCounter = 0;
        //            }
        //            i++;
        //        }


        //        //foreach (var valItem in valStore.OrderByDescending(valItem => valItem.Key))
        //        //{
        //        //    string strDataName = valItem.Key.ToString();
        //        //    // string strDataValue = valItem.Value.ToString();
        //        //    if (strDataName.IndexOf("PropertiesDummy") >= 0)
        //        //    {
        //        //    }
        //        //    else if (strDataName.IndexOf("ListDummy") >= 0)
        //        //    {
        //        //    }
        //        //    else if (strDataName.IndexOf("ToolTipDummy") >= 0)
        //        //    {
        //        //    }
        //        //    else
        //        //  {

        //        //    Int32 ss = ShareCOunt;
        //        //}
        //        // }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
        //    LogUtility.Error("GridImages.cs", "openBeastImage()", strErrorDesc, ex);
        //}
    }

    public void shareBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, string strInstanceID, ref ServerAgent _sa, string username)
    {
        //  VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "alrt", "m", "", "The App is not available ! Either it has been closed by the owner or there are no active viewers of it.", "");
    }
    public void distoryBeastImage()
    {
        DocImage.DocumentAlive -= new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
        DocImage.DocumentChanged -= new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
        DocImage.DocumentComplete -= new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
        DocImage.DocumentStale -= new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
        DocImage.ManualUpdateRequestStatus -= new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
        DocImage.StatusChanged -= new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);

        DocImage = null;
    }
    public void DocImage_StatusChanged(DOMDataDocStatus Status, string info)
    {
        try
        {
            if (Status == DOMDataDocStatus.DATADOCSTATUS_ERROR)
            {
                IsDocumentAlive = false;
                sendDocumentStatus();
                //BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, true, ActualProductID); // remove beast image from the list
                BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, false, ActualProductID); // remove beast image from the list
            }
            else if (Status == DOMDataDocStatus.DATADOCSTATUS_ALIVE)
            {
                IsDocumentAlive = true;
                sendDocumentStatus();
            }

            string logDesc = getLogDescForGenericInfo();
            logDesc += " ; DOMDataDocStatus: " + info;
            LogUtility.Info("GridImages.cs", "DocImage_StatusChanged()", logDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "DocImage_StatusChanged()", ex.Message, ex);
        }
    }
    void DocImage_ManualUpdateRequestStatus(bool Status, Scripting.Dictionary props)
    {

    }
    void DocImage_DocumentStale()
    {
        try
        {
            _isStale = true;
            IsDocumentAlive = false;
            sendDocumentStatus();

            string logDesc = getLogDescForGenericInfo();
            LogUtility.Info("GridImages.cs", "DocImage_DocumentStale()", logDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "DocImage_DocumentStale()", ex.Message, ex);
        }
    }
    void DocImage_DocumentComplete()
    {

    }

    public void Send_Array_To_Client_Group_Generic_Grid(string GroupName, string updateType, string updateEleType, string GridID, string Row, string Column, string EleValue, string HTMLClientID)
    {
        var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
        context.Clients.Group(GroupName).ArrayFromServerGrid(updateType, updateEleType, GridID, Row, Column, EleValue, HTMLClientID);
    }

    void DocImage_DocumentChanged(DOMDataNodeList changed)
    {
        try
        {
            DataTable d = new DataTable();
            d.Columns.Add("i");
            d.Columns.Add("d");

            var context = GlobalHost.ConnectionManager.GetHubContext<MarketDataHub>();
            // int updatesCounter = 0;
            int totalUpdates = changed.Length;
            //   int endPointer = totalUpdates - 1;

            int batchSize = 100;
            try
            {
                batchSize = Convert.ToInt32(ConfigurationManager.AppSettings["SignalRSendRowSize"].ToString());
            }
            catch (Exception ex)
            {

            }

            for (int i = 0; i < totalUpdates; i++)
            {
                DataRow r = d.NewRow();
                r["i"] = changed[i].NodeID;
                r["d"] = changed[i].DataValue;

                d.Rows.Add(r);
                //updatesCounter++;
                //if (updatesCounter >= batchSize || i == endPointer)
                //{
                //    updatesCounter = 0;
                //    d.Rows.Clear();
                //}
            }
            if (d.Rows.Count > 0)
                context.Clients.Group(GruopID).ArrayFromServerGrid(d, HtmlClientID);
            //updatesCounter = 0;
            d.Rows.Clear();
        }
        catch (Exception ex)
        {
            string logDesc = getLogDescForGenericInfo();
            LogUtility.Error("GridImages.cs", "DocImage_DocumentChanged()", logDesc, ex);
        }
    }

    public void DocImage_DocumentAlive()
    {
        try
        {
            IsDocumentAlive = true; //change the socument status
            _isStale = false; // change the stale state
            _instanceID = DocImage.Name;
            BeastConn.Instance.SA.LockDocument(_instanceID, null);
            sendDocumentStatus();
            LogUtility.Info("GridImages.cs", "DocImage_DocumentAlive()", "Doc Image Document Alive");
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "DocImage_DocumentAlive()", ex.Message, ex);
        }
    }

    public void sendDocumentStatus() 
    {
        try
        {
            string eProductName = ActualProductID;
            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "imgStatus", "m", "", imgStatus, eProductName.ToString());
            //LogUtility.Info("GridImages.cs", "sendDocumentStatus()", "send Document Status");
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "sendDocumentStatus()", ex.Message, ex);
        }
    }

    public void sendDocumentStatusToConn(string connID)
    {
        try
        {
            string eProductName = ActualProductID;

            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Connection_Generic(connID, "imgStatus", "m", "", imgStatus, eProductName.ToString());

            //LogUtility.Info("GridImages.cs", "sendDocumentStatusToConn()", "Send Document Status To Conn");
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "sendDocumentStatusToConn()", ex.Message, ex);
        }
    }
    #endregion

    #region Public Default Properties
    public bool IsDocumentAlive
    {
        get { return _isDocumentAlive; }
        set { _isDocumentAlive = value; }
    }

    public bool IsStale
    {
        get { return _isStale; }
        set { _isStale = value; }
    }

    public DOMDataDocument DocImage
    {
        get
        {
            return _beastVolPrimGridImagesDataDoc;
        }
        set
        {
            _beastVolPrimGridImagesDataDoc = value;
        }
    }

    public string UserID
    {

        get { return _userID; }
        set { _userID = value; }
    }

    public string ProductID
    {
        get { return _productID; }
        set { _productID = value; }
    }

    public string CustomerID
    {
        get { return _customerID; }
        set { _customerID = value; }
    }

    public string InstanceID
    {
        get { return _instanceID; }
        set { _instanceID = value; }
    }

    public string ConnectionID
    {
        get { return _connectionID; }
        set { _connectionID = value; }
    }

    public string SpecialImageID
    {
        get { return _specialImageID; }
        set { _specialImageID = value; }
    }

    public string GruopID
    {
        get { return _gruopID; }
        set { _gruopID = value; }
    }

    public string HtmlClientID
    {
        get { return _htmlClientID; }
        set { _htmlClientID = value; }
    }

    public string ActualProductID
    {
        get { return _actualProductID; }
        set { _actualProductID = value; }
    }

    public bool IsSharedImage
    {
        get { return _isSharedImage; }
        set { _isSharedImage = value; }
    }
    public int refCount
    {
        get { return _refCount; }
        set { _refCount = value; }
    }
    #endregion

    #region Public Methods

    public DataTable dtPremGrid
    {
        get { return _dtPremGrid; }
    }

    public DataTable dtVolGrid
    {
        get { return _dtVolGrid; }
    }

    public DataTable dtStrikeGrid
    {
        get { return _dtStrikeGrid; }
    }

    public DataTable dtPremGridNodeIDs
    {
        get { return _dtPremGridNodeIDs; }
    }

    public DataTable dtStrikeGridNodeIDs
    {
        get { return _dtStrikeGridNodeIDs; }
    }

    public DataTable dtVolGridNodeIDs
    {
        get { return _dtVolGridNodeIDs; }
    }

    public DOMDataDocument volPremImage
    {
        get { return _beastVolPrimGridImagesDataDoc; }
    }

    #endregion

    #region appendedFun

    public void updateDataTableFromGridField(ref DataTable dt, string fieldName, DOMDataNode node, string fieldTag, ref DataTable dtNodeIDs)
    {
        try
        {
            if (dt == null)
                dt = new DataTable();

            if (dtNodeIDs == null)
                dtNodeIDs = new DataTable();

            string nodeId = node.NodeID;
            if (nodeId.StartsWith("G" + fieldName))
            {
                int row, col;
                if (UtilComman.GetRowColGridField(nodeId, out row, out col))
                {
                    if (col == -1)
                        return;
                    while (col >= dt.Columns.Count)// && node.DataState != DOMDataNodeValueState.DATANODEVALUESTATE_BLANK)
                    {
                        dt.Columns.Add(Convert.ToString(strColumnName(col, fieldTag)), typeof(string));
                        dtNodeIDs.Columns.Add(Convert.ToString(strColumnName(col, fieldTag)), typeof(string));
                    }

                    while (row >= dt.Rows.Count)
                    {
                        dt.Rows.Add(dt.NewRow());
                        dtNodeIDs.Rows.Add(dtNodeIDs.NewRow());
                    }

                    if (row == -1)
                    {
                        dt.Columns[col].ColumnName = node.DataValue.ToString();
                        dtNodeIDs.Columns[col].ColumnName = node.DataValue.ToString();
                    }
                    if (col != -1 && row != -1)
                    {
                        dt.Rows[row][col] = node.DataValue;
                        dtNodeIDs.Rows[row][col] = node.NodeID;

                        VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();

                        cometVOrder.Send_Message_To_Client_Group_Generic_Grid(GruopID, "c", "c", nodeId, row.ToString(), col.ToString(), node.DataValue, HtmlClientID);

                        //if (fieldTag == "VolGrid")
                        //{
                        //    cometVOrder.sendVolGridDataResponse(GruopID, row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        //}
                        //else if (fieldTag == "PremGrid")
                        //{
                        //    cometVOrder.sendPremGridDataResponse(GruopID, row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        //}
                        //else if (fieldTag == "StrikeGrid")
                        //{
                        //    cometVOrder.sendStrikeGridDataResponse(GruopID, row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        //}
                    }
                }
            }
            else if (fieldTag == "Other")
            {
                //VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                //if (Convert.ToString(nodeId) == "100")
                //{
                //    string straddleVal = StraddleValue(Convert.ToInt32(node.DataValue));
                //    Straddle = straddleVal;
                //    cometVOrder.sendOtherDataResponse(GruopID, "Currency", "Straddle", straddleVal);
                //}
            }

            string strlogDesc = "FieldName: " + fieldName + "; FieldTag: " + fieldTag + "; NodeId: " + node.NodeID + "; GruopID: " + GruopID + "; Update Data Table From Grid Field";
            //LogUtility.Info("GridImages.cs", "updateDataTableFromGridField()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "FieldName: " + fieldName + "; FieldTag: " + fieldTag + "; NodeId: " + node.NodeID + "; GruopID: " + GruopID + "; " + ex.Message;
            LogUtility.Error("GridImages.cs", "updateDataTableFromGridField()", strErrorDesc, ex);
        }
    }
    #endregion

    #region Utility Functions

    string strColumnName(Int32 colIndex, string fieldTag)
    {
        string strColumnName = string.Empty;

        if (colIndex == 0)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 1)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 2)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 3)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 4)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 5)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 6)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 7)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 8)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 9)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 10)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 11)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 12)
            strColumnName = "col" + "_" + colIndex.ToString();
        else if (colIndex == 13)
            strColumnName = "col" + "_" + colIndex.ToString();

        return strColumnName;
    }

    public string StraddleValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 0:
                rtnValue = "None";
                break;
            case 1:
                rtnValue = "Payer";
                break;
            case 2:
                rtnValue = "Recv";
                break;
            case 3:
                rtnValue = "Straddle";
                break;
            default:
                break;
        }

        return rtnValue;
    }

    public string getLogDescForGenericInfo()
    {
        return "ProdId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustId: " + CustomerID + "; " + "ConnId: " + ConnectionID + "ConnIDSignalR:" + ConnectionID + "ActProdID:" + ActualProductID + "; InstID : " + InstanceID + "; SpeImgID : " + SpecialImageID + "; HtmCliID : " + HtmlClientID;
    }
    #endregion
}