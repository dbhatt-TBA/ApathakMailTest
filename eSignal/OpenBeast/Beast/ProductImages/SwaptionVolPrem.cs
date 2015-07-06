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


/// <summary>
/// Summary description for SwaptionVolPrem
/// </summary>
/// 
public class SwaptionVolPrem : IProductImage
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
    private string nodeTitleOld = "";
    private string title = "";
    private bool IsFirstTime = true;

    //END ------  Grid Field

    #endregion

    #region constructor

    public SwaptionVolPrem(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent sa, bool strIsSharedImage, string strInstanceID, string username)
    {
        initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);

        if (strIsSharedImage == true)
        {
           shareBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, strInstanceID, ref sa,username);
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

        //Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);
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
            LogUtility.Info("SwaptionVolPrem.cs", "createBeastImage()ImpersonatedUser", "username:" + username + " ProductName:" + "vcm_calc_swaptionVolPremStrike");


            DocImage = _sa.RequestDocument("appid:" + Convert.ToString((AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"])), Props);

            DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
            DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
            DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
            DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
            DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
            DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);

            string logDesc = getLogDescForGenericInfo();

            LogUtility.Info("SwaptionVolPrem.cs", "createBeastImage()", logDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "createBeastImage()", strErrorDesc, ex);
        }
    }

    public void openBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string ConnectionIDSignalR, string username)
    {
        try
        {
            string logDesc = getLogDescForGenericInfo();

            LogUtility.Info("SwaptionVolPrem.cs", "openBeastImage()", logDesc);

            sendDocumentStatusToConn(ConnectionIDSignalR);

            if (DocImage == null)
            {
                initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);
                createBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, ref _sa, username);
            }
            else if (IsDocumentAlive == false)
            {
                BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, true, ActualProductID); // remove beast image from the list

                //initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);

                //Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);

                //Scripting.Dictionary Props = new Scripting.Dictionary();

                //Object key = "SID";
                //Object value = Convert.ToString((int)eProductName);
                //Props.Add(ref key, ref value);

                //Object key1 = "AppDefs";
                //Object value1 = strConnectionID;
                //Props.Add(ref key1, ref value1);

                //DocImage = _sa.RequestDocument(InstanceID, Props);

                //DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
                //DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
                //DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
                //DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
                //DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
                //DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);
            }
            else
            {
                VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Currency", "Currency", Currency);
                VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Currency", "Straddle", Straddle);
                VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Vols", "Vols", Vols);
                VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "VolShift", "VolShift", VolShift);
                VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Currency", "List", List);
                VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "PremGridTitle", "Title", PremGridTitle);
            }
            DOMDataNode nodeTitle = DocImage.get_NodeByID("AppTitle");
            string title = nodeTitle.DataValue.ToString();
            VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "updt", "title", DocImage.Name, title, ActualProductID);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "openBeastImage()", strErrorDesc, ex);
        }
    }

   public void shareBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, string strInstanceID, ref ServerAgent _sa,string username)
    {

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
                BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, true, ActualProductID); // remove beast image from the list
            }
            else if (Status == DOMDataDocStatus.DATADOCSTATUS_ALIVE)
            {
                IsDocumentAlive = true;
                sendDocumentStatus();
            }

            string logDesc = getLogDescForGenericInfo();
            logDesc += " ; DOMDataDocStatus: " + info;
            LogUtility.Info("SwaptionVolPrem.cs", "DocImage_StatusChanged()", logDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "DocImage_StatusChanged()", ex.Message, ex);
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
            LogUtility.Info("SwaptionVolPrem.cs", "DocImage_DocumentStale()", logDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "DocImage_DocumentStale()", ex.Message, ex);
        }
    }
    void DocImage_DocumentComplete()
    {

    }
    void DocImage_DocumentChanged(DOMDataNodeList changed)
    {
        try
        {
            string strVolGrid = "";
            string strPremGrid = "";
            string strStrikeGrid = "";

            for (int i = 0; i < changed.Length; i++)
            {
                switch (changed[i].NodeName)
                {
                    case "G100000028":    // Vol Grid Field
                        updateDataTableFromGridField(ref _dtVolGrid, "100000028", changed[i], "VolGrid", ref _dtVolGridNodeIDs);
                        //strVolGrid += returnDataTableFromGridField(ref _dtVolGrid, "100000028", changed[i], "VolGrid") + "^";
                        break;
                    case "G100000030":    // Prem Grid Field
                        updateDataTableFromGridField(ref _dtPremGrid, "100000030", changed[i], "PremGrid", ref _dtPremGridNodeIDs);
                        //strPremGrid += returnDataTableFromGridField(ref _dtPremGrid, "100000030", changed[i], "PremGrid") + "^";
                        break;
                    case "G100000029":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000029", changed[i], "StrikeGrid", ref _dtStrikeGridNodeIDs);
                        //strStrikeGrid += returnDataTableFromGridField(ref _dtStrikeGrid, "100000029", changed[i], "StrikeGrid") + "^";
                        break;
                    case "1":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    case "100":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    case "52":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    case "333":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    case "4":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    case "6":    // Strike Grid Field
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    case "100000031":
                        updateDataTableFromGridField(ref _dtStrikeGrid, "100000030", changed[i], "Other", ref _dtPremGridNodeIDs);
                        break;
                    default:
                        break;
                }
            }

            //For showing title on web image
            VCMComet cometVOrder = VCMComet.Instance;
            DOMDataNode nodeTitle = DocImage.get_NodeByID("AppTitle");
            title = nodeTitle.DataValue.ToString();
            if (IsFirstTime == true)
            {
                IsFirstTime = false;
                title = nodeTitle.DataValue.ToString();
                nodeTitleOld = title;
                if (title.Split(':')[0] == " " || title.Split(':')[0] == null)
                {
                    string logDesc = getLogDescForGenericInfo();
                    logDesc += ":BeastPluginError";
                    LogUtility.Info("ProductClassGeneric.cs", "DocImage_DocumentChanged()", logDesc);
                }
                cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "title", DocImage.Name, title, ActualProductID);
            }
            else if (title != nodeTitleOld)
            {
                title = nodeTitle.DataValue.ToString();
                nodeTitleOld = title;
                if (title.Split(':')[0] == " " || title.Split(':')[0] == null)
                {
                    string logDesc = getLogDescForGenericInfo();
                    logDesc += ":BeastPluginError";
                    LogUtility.Info("ProductClassGeneric.cs", "DocImage_DocumentChanged()", logDesc);
                }
                cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "title", DocImage.Name, title, ActualProductID);
            }

            //LogUtility.Info("SwaptionVolPrem.cs", "DocImage_DocumentChanged()", "Doc Image Document Changed");
        }
        catch (Exception ex)
        {
            string logDesc = getLogDescForGenericInfo();

            LogUtility.Error("SwaptionVolPrem.cs", "DocImage_DocumentChanged()", logDesc, ex);
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
            LogUtility.Info("SwaptionVolPrem.cs", "DocImage_DocumentAlive()", "Doc Image Document Alive");
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "DocImage_DocumentAlive()", ex.Message, ex);
        }
    }

    public void sendDocumentStatus()
    {
        try
        {
            //Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), ActualProductID);
            string eProductName = ActualProductID;
            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "imgStatus", "m", "", imgStatus, eProductName.ToString());
            //LogUtility.Info("SwaptionVolPrem.cs", "sendDocumentStatus()", "send Document Status");
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "sendDocumentStatus()", ex.Message, ex);
        }
    }

    public void sendDocumentStatusToConn(string connID)
    {
        try
        {
            //            Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), ActualProductID);
            string eProductName = ActualProductID;

            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Connection_Generic(connID, "imgStatus", "m", "", imgStatus, eProductName.ToString());

            //LogUtility.Info("SwaptionVolPrem.cs", "sendDocumentStatusToConn()", "Send Document Status To Conn");
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "sendDocumentStatusToConn()", ex.Message, ex);
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
                        //if (node.DataState == DOMDataNodeValueState.DATANODEVALUESTATE_BLANK)
                        //    dt.Columns[col].ColumnName = col.ToString();
                        //else
                        dt.Columns[col].ColumnName = node.DataValue.ToString();
                        dtNodeIDs.Columns[col].ColumnName = node.DataValue.ToString();
                    }
                    if (col != -1 && row != -1)
                    {
                        dt.Rows[row][col] = node.DataValue;
                        dtNodeIDs.Rows[row][col] = node.NodeID;

                        VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                        if (fieldTag == "VolGrid")
                        {
                            cometVOrder.sendVolGridDataResponse(GruopID, row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        }
                        else if (fieldTag == "PremGrid")
                        {
                            cometVOrder.sendPremGridDataResponse(GruopID, row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        }
                        else if (fieldTag == "StrikeGrid")
                        {
                            cometVOrder.sendStrikeGridDataResponse(GruopID, row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        }
                    }
                }
            }
            else if (fieldTag == "Other")
            {
                VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                if (Convert.ToString(nodeId) == "1")
                {
                    string currencyVal = CurrencyValue(Convert.ToInt32(node.DataValue));
                    Currency = currencyVal;
                    CurrencyDataValue = Convert.ToString(node.DataValue);
                    cometVOrder.sendOtherDataResponse(GruopID, "Currency", "Currency", currencyVal);
                }
                else if (Convert.ToString(nodeId) == "100")
                {
                    string straddleVal = StraddleValue(Convert.ToInt32(node.DataValue));
                    Straddle = straddleVal;
                    cometVOrder.sendOtherDataResponse(GruopID, "Currency", "Straddle", straddleVal);
                }
                else if (Convert.ToString(nodeId) == "52")
                {
                    string Val = VolValue(Convert.ToInt32(node.DataValue));
                    Vols = Val;
                    cometVOrder.sendOtherDataResponse(GruopID, "Vols", "Vols", Val);
                }
                else if (Convert.ToString(nodeId) == "333")
                {
                    string Val = VolShiftValue(Convert.ToInt32(node.DataValue));
                    VolShift = Val;
                    cometVOrder.sendOtherDataResponse(GruopID, "VolShift", "VolShift", Val);
                }
                else if (Convert.ToString(nodeId) == "4")
                {
                    //string Val = CurrencyThirdColumn(Convert.ToInt32(node.DataValue));
                    //List = Val;
                    //cometVOrder.sendOtherDataResponse("Currency", "List", Val);
                }
                else if (Convert.ToString(nodeId) == "6")
                {
                    string Val = PremFPremValue(Convert.ToInt32(node.DataValue));
                    PremGridTitle = Val;
                    cometVOrder.sendOtherDataResponse(GruopID, "PremGridTitle", "Title", Val);
                }
                else if (Convert.ToString(nodeId) == "100000031")
                {
                    if (node.DataValue != "")
                    {
                        //string Val = CurrencyThirdColumn(Convert.ToInt32(node.DataValue));
                        string Val = node.DataValue.ToString();
                        List = Val;
                        cometVOrder.sendOtherDataResponse(GruopID, "Currency", "List", Val);
                    }

                }
            }

            string strlogDesc = "FieldName: " + fieldName + "; FieldTag: " + fieldTag + "; NodeId: " + node.NodeID + "; GruopID: " + GruopID + "; Update Data Table From Grid Field";
            //LogUtility.Info("SwaptionVolPrem.cs", "updateDataTableFromGridField()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "FieldName: " + fieldName + "; FieldTag: " + fieldTag + "; NodeId: " + node.NodeID + "; GruopID: " + GruopID + "; " + ex.Message;
            LogUtility.Error("SwaptionVolPrem.cs", "updateDataTableFromGridField()", strErrorDesc, ex);
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

    public string PremFPremValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 0:
                rtnValue = "Prem";
                break;
            case 1:
                rtnValue = "F.Prem";
                break;
            default:
                break;
        }

        return rtnValue;
    }

    public string VolValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 0:
                rtnValue = "Local";
                break;
            case 1:
                rtnValue = "Global";
                break;
            default:
                break;
        }

        return rtnValue;
    }

    public string CurrencyThirdColumn(int id)
    {
        string rtnValue = "";
        try
        {
            int currentCurrencyID = Convert.ToInt32(CurrencyDataValue);

            if (currentCurrencyID == 21)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 100962037:
                        rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                        break;
                    case 61:
                        rtnValue = "Internal:Manually Maintained Data";
                        break;
                    case 40970501:
                        rtnValue = "TTL-B:Tullett & Tokyo Liberty Swaption Vol [Bridge]";
                        break;
                    case 163850501:
                        rtnValue = "TTL-B:Tullett & Tokyo Liberty Swaption Vol [Bridge]";
                        break;
                    case 10501:
                        rtnValue = "TTL-B:Tullett & Tokyo Liberty Swaption Vol [Bridge]";
                        break;
                    case 20501:
                        rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 86930501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                        break;
                    case 30501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [Telerate]";
                        break;
                    case 100962038:
                        rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                        break;
                    case 100960600:
                        rtnValue = "VCM-B:VCM Swaption Vol [BEAST2]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 221)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - Internal:Manually Maintained Data";
                        break;
                    case 61:
                        rtnValue = "Internal - Pref:Manually Maintained Data";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 522)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 100960007:
                        rtnValue = "Bloom-V:Bloomberg Swaption Vol [VCM]";
                        break;
                    case 100962037:
                        rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                        break;
                    case 22036:
                        rtnValue = "Eonia  Swaption Vol:ICPL Eonia  Swaption Vol [Reuters]";
                        break;
                    case 61:
                        rtnValue = "Internal:Manually Maintained Data";
                        break;
                    case 20501:
                        rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 86930501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                        break;
                    case 100962038:
                        rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 215)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 61:
                        rtnValue = "Internal - Pref:Manually Maintained Data";
                        break;
                    case 40970504:
                        rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                        break;
                    case 163850504:
                        rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 15)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 61:
                        rtnValue = "Internal :Manually Maintained Data";
                        break;
                    case 40970504:
                        rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                        break;
                    case 163850504:
                        rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                        break;
                    case 20501:
                        rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 86930501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 11)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 100960007:
                        rtnValue = "Bloom-V:Bloomberg Swaption Vol [VCM]";
                        break;
                    case 100962037:
                        rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                        break;
                    case 22036:
                        rtnValue = "Eonia  Swaption Vol:ICPL Eonia  Swaption Vol [Reuters]";
                        break;
                    case 61:
                        rtnValue = "Internal:Manually Maintained Data";
                        break;
                    case 40970504:
                        rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                        break;
                    case 163850504:
                        rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                        break;
                    case 20501:
                        rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 86930501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                        break;
                    case 100962038:
                        rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 4)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 100960007:
                        rtnValue = "Bloom-V:Bloomberg Swaption Vol [VCM]";
                        break;
                    case 100962037:
                        rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                        break;
                    case 61:
                        rtnValue = "Internal:Manually Maintained Data";
                        break;
                    case 100962038:
                        rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 807 || currentCurrencyID == 917)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - Internal:Manually Maintained Data";
                        break;
                    case 61:
                        rtnValue = "Internal :Manually Maintained Data";
                        break;
                    case 86930501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 1020)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 61:
                        rtnValue = "Internal :Manually Maintained Data";
                        break;
                    case 20501:
                        rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                        break;
                    case 86930501:
                        rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                        break;
                    default:
                        break;
                }
            }
            else if (currentCurrencyID == 1703 || currentCurrencyID == 3 || currentCurrencyID == 2205 || currentCurrencyID == 2123 || currentCurrencyID == 324 || currentCurrencyID == 2325 || currentCurrencyID == 2527 || currentCurrencyID == 2628 || currentCurrencyID == 2729 || currentCurrencyID == 2830 || currentCurrencyID == 2931 || currentCurrencyID == 3032 || currentCurrencyID == 3133 || currentCurrencyID == 3234)
            {
                switch (id)
                {
                    case 0:
                        rtnValue = "Pref - Internal:Manually Maintained Data";
                        break;
                    case 61:
                        rtnValue = "Internal :Manually Maintained Data";
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("SwaptionVolPrem.cs", "CurrencyThirdColumn()", ex.Message, ex);
        }

        return rtnValue;
    }

    public string VolShiftValue(int id)
    {
        string rtnValue = "";
        try
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Off";
                    break;
                case 1:
                    rtnValue = "Percentage";
                    break;
                case 2:
                    rtnValue = "Parallel";
                    break;
                case 3:
                    rtnValue = "DK";
                    break;
                default:
                    break;
            }

            //LogUtility.Info("SwaptionVolPrem.cs", "VolShiftValue()", "Vol Shift Value");
        }
        catch (Exception ex)
        {
            LogUtility.Error("SwaptionVolPrem.cs", "VolShiftValue()", ex.Message, ex);
        }

        return rtnValue;
    }

    public string CurrencyValue(int id)
    {
        string rtnValue = "";
        try
        {
            switch (id)
            {
                case 21:
                    rtnValue = "USD.EU :US (London) Eurodollar LIBOR Market";
                    break;
                case 221:
                    rtnValue = "USD.TK :US (Tokyo) TIBOR Market";
                    break;
                case 6:
                    rtnValue = "DEM.LN :Germany (London) LIBOR Market";
                    break;
                case 406:
                    rtnValue = "DEM  :Germany (Frankfurt) Domestic/FIBOR Market";
                    break;
                case 22:
                    rtnValue = "EUR.LN  :Euro (London) LIBOR Market";
                    break;
                case 522:
                    rtnValue = "EUR  :Euro EURIBOR Market";
                    break;
                case 215:
                    rtnValue = "JPY.TK  :Japan (Tokyo) Domestic/TIBOR Market";
                    break;
                case 15:
                    rtnValue = "JPY.EU  :Japan (London) LIBOR Market";
                    break;
                case 1703:
                    rtnValue = "CAD.DM  :Canada (Toronto) Domestic/BA Market";
                    break;
                case 3:
                    rtnValue = "CAD.EU  :Canada (London) LIBOR Market";
                    break;
                case 11:
                    rtnValue = "GBP :UK (London) Domestic/LIBOR Market";
                    break;
                case 1404:
                    rtnValue = "CHF.ZU  :Switzerland (Zurich) Domestic Market";
                    break;
                case 4:
                    rtnValue = "CHF.EU  :Switzerland (London) LIBOR Market";
                    break;
                case 14:
                    rtnValue = "ITL.LN  :Italy (London) LIBOR Market";
                    break;
                case 2205:
                    rtnValue = "CZK :Czech (Prague) Domestic/PRIBOR Market";
                    break;
                case 807:
                    rtnValue = "DKK :Denmark (Copenhagen) Domestic/CIBOR Market";
                    break;
                case 917:
                    rtnValue = "NOK :Norway (Oslo) Domestic/NIBOR(OIBOR) Market";
                    break;
                case 1020:
                    rtnValue = "SEK :Sweden (Stockholm) Domestic/STIBOR Market";
                    break;
                case 2123:
                    rtnValue = "ZAR :South Africa (Johannesburg) Domestic/BA Market";
                    break;
                case 324:
                    rtnValue = "SGD :Singapore (Singapore) Domestic/SIBOR Market";
                    break;
                case 2325:
                    rtnValue = "MXN :Mexico (Mexico City) Domestic Market";
                    break;
                case 2527:
                    rtnValue = "ARP :Argentina (Buenos Aires) Domestic/BAIBOR Market";
                    break;
                case 2628:
                    rtnValue = "TRL :Turkey (Ankara) Domestic Market";
                    break;
                case 2729:
                    rtnValue = "EEK :Estonia (Tallin) Domestic/TALIBOR Market";
                    break;
                case 2830:
                    rtnValue = "GRD :Greece (Athens) Domestic/ATHIBOR Market";
                    break;
                case 2931:
                    rtnValue = "HUF :Hungary (Budapest) Domestic/BUBOR Market";
                    break;
                case 3032:
                    rtnValue = "PLN :Poland (Warzawa) Domestic/WIBOR Market";
                    break;
                case 3133:
                    rtnValue = "RUB :Russia (Moskva) Domestic/MOWIBOR Market";
                    break;
                case 3234:
                    rtnValue = "BRL :Brazil (Brasilia) Domestic Market";
                    break;
                default:
                    break;

            }
            //LogUtility.Info("SwaptionVolPrem.cs", "CurrencyValue()", "setCurrencyValue");
        }
        catch (Exception ex)
        {
            LogUtility.Error("SwaptionVolPrem.cs", "CurrencyValue()", ex.Message, ex);
        }
        return rtnValue;
    }

    public string getLogDescForGenericInfo()
    {
        return "ProdId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustId: " + CustomerID + "; " + "ConnId: " + ConnectionID + "ConnIDSignalR:" + ConnectionID + "ActProdID:" + ActualProductID + "; InstID : " + InstanceID + "; SpeImgID : " + SpecialImageID + "; HtmCliID : " + HtmlClientID;
    }
    #endregion
}
