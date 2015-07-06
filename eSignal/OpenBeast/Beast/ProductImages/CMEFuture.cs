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
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Concurrent;
using VCM.Common.Log;

public class CMEFuture : IProductImage
{
    #region member devlaration
    private DOMDataDocument _beastVolPrimGridImagesDataDoc = null;

    private string _userID = "0";
    private string _productID = "852";
    private string _customerID = "";
    private string _instanceID = "";
    private string _connectionID = "";

    private string _specialImageID = "";
    private string _gruopID = "";
    private string _htmlClientID = "";
    private string _actualProductID = "";

    private bool _isDocumentAlive;
    private bool _isStale;

    private bool _isFirstTime = true;
    private bool _isSharedImage;
    int _refCount { get; set; }
    //Dictionary<string, string> valStore;
    //OrderedDictionary  valStore;
    ConcurrentDictionary<string, string> valStore;
    Dictionary<string, string> valStoreChanged;

    #endregion

    #region constructor
    public CMEFuture(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent sa, bool strIsSharedImage, string strInstanceID, string username)
    {
        try
        {
            initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);

            if (strIsSharedImage == true)
            {
             shareBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, strInstanceID, ref sa,username);
                 }
            else
            {
                createBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, ref sa,username);
            }

            _isStale = false;
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "CMEFuture()", strErrorDesc, ex);
            throw;
        }
    }

    public void initDataForImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID)
    {
        try
        {
            IsDocumentAlive = false;

            ProductID = strProductID;
            ConnectionID = strConnectionID;
            UserID = strUserID;
            CustomerID = strCustomerID;
            SpecialImageID = strSpecialImageID;
            ActualProductID = strActualProductID;

            // Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);
            string eProductName = strActualProductID;


            if (SpecialImageID == "0")
                HtmlClientID = eProductName.ToString();
            else
                HtmlClientID = SpecialImageID;

            GruopID = BeastConn.Instance.getUserProductKeyForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID);
            valStore = new ConcurrentDictionary<string, string>();
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "initDataForImage()", strErrorDesc, ex);
            throw;
        }

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
            //Object key = "AppDefs:";
            //Object value = strConnectionID;
            //Props.Add(ref key, ref value);

            Object key1 = "RequestFieldPropertiesAsNode";
            Object value1 = true;
            Props.Add(ref key1, ref value1);

            Object key2 = "OnlyVisible";
            Object value2 = 1;
            Props.Add(ref key2, ref value2);

            Object key = "ImpersonatedUser";
            Object value = username;
            Props.Add(ref key, ref value);
            LogUtility.Info("CMEFuture.cs", "createBeastImage()ImpersonatedUser", "username:" +username+ " ProductName:" + "vcm_calc_cmefuture");

            DocImage = _sa.RequestDocument("appid:" + Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_cmefuture"]), Props);
            //DocImage = _sa.RequestDocument("appid:852", null);

            DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
            DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
            DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
            DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
            DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
            DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);

            string strlogDesc = "ProductId: " + strProductID + "; UserId: " + strUserID + "; CustomerId: " + strCustomerID + "; ConnectionId: " + strConnectionID + "; Create Beast Image";
            LogUtility.Info("CMEFuture.cs", "createBeastImage()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "createBeastImage()", strErrorDesc, ex);
            throw;
        }
    }

    public void openBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string ConnectionIDSignalR, string username)
    {
        try
        {
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

                //Object key1 = "RequestFieldPropertiesAsNode";
                //Object value1 = true;
                //Props.Add(ref key1, ref value1);

                //Object key2 = "OnlyVisible";
                //Object value2 = 1;
                //Props.Add(ref key2, ref value2);

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
                VCMComet cometVOrder = VCMComet.Instance;

                foreach (var valItem in valStore.OrderByDescending(valItem => valItem.Key))
                {
                    string strDataName = valItem.Key.ToString();
                    string strDataValue = valItem.Value.ToString();

                    if (strDataName.IndexOf("PropertiesDummy") >= 0)
                    {
                        cometVOrder.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "mnlupdt", "p", strDataName.Split('_')[1], strDataValue, HtmlClientID);
                    }
                    else if (strDataName.IndexOf("ListDummy") >= 0)
                    {
                        cometVOrder.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "mnlupdt", "lst", strDataName.Split('_')[1], strDataValue, HtmlClientID);
                    }
                    else
                    {
                        cometVOrder.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "updt", "i", strDataName, strDataValue, HtmlClientID);
                    }
                }
            }

            string strlogDesc = "ProductId: " + strProductID + "; UserId: " + strUserID + "; CustomerId: " + strCustomerID + "; ConnectionId: " + strConnectionID + "; open Beast Image";
            LogUtility.Info("CMEFuture.cs", "openBeastImage()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "openBeastImage()", strErrorDesc, ex);
            throw;
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

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; Doc Image Status Changed";
            LogUtility.Info("CMEFuture.cs", "DocImage_StatusChanged()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "DocImage_StatusChanged()", strErrorDesc, ex);
            throw;
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

            LogUtility.Info("CMEFuture.cs", "DocImage_DocumentStale()", "Doc Image Document State");
        }
        catch (Exception ex)
        {
            LogUtility.Error("CMEFuture.cs", "DocImage_DocumentStale()", ex.Message, ex);
        }
    }
    void DocImage_DocumentComplete()
    {

    }

    void AddUpdate_ValueStore(string name, string value)
    {
        try
        {
            if (valStore.ContainsKey(name))
            {
                valStore[name] = value;
            }
            else
            {
                valStore.AddOrUpdate(name, value, (key, oldValue) => value);
            }

            //string strlogDesc = "Name: " + name + "; Value: " + value + "; Add Update Value Store";
            //LogUtility.Info("CMEFuture.cs", "AddUpdate_ValueStore()", strlogDesc); 
        }
        catch (Exception ex)
        {
            string strErrorDesc = "Name: " + name + "; " + "Value: " + value + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "AddUpdate_ValueStore()", strErrorDesc, ex);
        }
    }

    void setDefaultValues()
    {
        try
        {
            //2Y : 2102010635
            //5Y : 2102010637
            //10Y : 210201060
            //30Y : 2102010636
            string name = "";
            string message = "";

            //Test

            //if (SpecialImageID == "BI_CMEFuture1")
            //{
            //    message = "DDList#" + "900#" + "521020106";
            //}
            //else if (SpecialImageID == "BI_CMEFuture2")
            //{
            //    message = "DDList#" + "900#" + "301020106";
            //}
            //if (SpecialImageID == "BI_CMEFuture4")
            //{
            //    message = "DDList#" + "900#" + "21020106";
            //}
            //if (SpecialImageID == "BI_CMEFuture3")
            //{
            //    message = "DDList#" + "900#" + "101020106";
            //}
            //Production

            if (SpecialImageID == "BI_CMEFuture1")
            {
                message = "DDList#" + "900#" + "2102010635";
            }
            else if (SpecialImageID == "BI_CMEFuture2")
            {
                message = "DDList#" + "900#" + "2102010637";
            }
            if (SpecialImageID == "BI_CMEFuture4")
            {
                message = "DDList#" + "900#" + "210201060";
            }
            if (SpecialImageID == "BI_CMEFuture3")
            {
                message = "DDList#" + "900#" + "2102010636";
            }
            VCMComet.Instance.setValueInBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, name, message, ActualProductID);

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; Set Default Values";
            LogUtility.Info("CMEFuture.cs", "setDefaultValues()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("CMEFuture.cs", "setDefaultValues()", ex.Message, ex);
        }
    }

    void DocImage_DocumentChanged(DOMDataNodeList changed)
    {
        try
        {
            if (_isFirstTime == true)
            {
                _isFirstTime = false;
                setDefaultValues();
            }
            VCMComet cometVOrder = VCMComet.Instance;

            for (int i = 0; i < changed.Length; i++)
            {
                if (changed[i].DataValue.ToString().Length > 0)
                {
                    switch (changed[i].NodeName)
                    {
                        case "7801":
                        case "7806":
                        case "900":
                        case "1000":
                        case "700":
                        case "1100":
                        case "1200":
                        case "1500":
                        case "7803":
                        case "7802":
                        case "1400":
                        case "500":
                        case "1300":
                        case "1600":
                        case "1800":
                        case "1700":
                        case "1900":
                            if (Convert.ToInt32(Definations.NodeDataStatus.DATANODEVALUESTATE_NORMAL) == Convert.ToInt32(changed[i].DataState))
                            {
                                AddUpdate_ValueStore(changed[i].NodeName, changed[i].DataValue.ToString() + "#" + changed[i].DisplayString);
                                cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "i", changed[i].NodeName, changed[i].DataValue.ToString() + "#" + changed[i].DisplayString, HtmlClientID);
                            }
                            else
                            {
                                AddUpdate_ValueStore(changed[i].NodeName, "#");
                                cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "i", changed[i].NodeName, "#", HtmlClientID);
                            }
                            break;
                        case "List":
                            string nName = changed[i].NodeName;
                            string nValue = changed[i].DataValue.ToString();
                            DOMDataNode prntNode = changed[i].ParentNode;
                            DOMDataNodeList chlNode = changed[i].ChildNodes;
                            //DOMDataNode chlNode = changed[i].ChildNodes.;
                            string ddNodeIDValPair = changed[i].ParentNode.NodeID + "#" + changed[i].ParentNode.DataValue;
                            AddUpdate_ValueStore(changed[i].NodeName + "Dummy_" + changed[i].ParentNode.NodeID, nValue + "#" + changed[i].ParentNode.DataValue);
                            cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "lst", changed[i].ParentNode.NodeID, nValue + "#" + changed[i].ParentNode.DataValue, HtmlClientID);
                            break;
                        case "Properties":
                            string nNameP = changed[i].NodeName;
                            string nValueP = "";
                            if (Convert.ToInt32(Definations.NodeDataStatus.DATANODEVALUESTATE_NORMAL) == Convert.ToInt32(changed[i].ParentNode.DataState))
                                nValueP = changed[i].DataValue.ToString() + "#" + changed[i].ParentNode.DataValue + "#" + changed[i].ParentNode.DisplayString;
                            else
                                nValueP = changed[i].DataValue.ToString() + "##";

                            DOMDataNode prntNodeP = changed[i].ParentNode;
                            DOMDataNodeList chlNodeP = changed[i].ChildNodes;
                            AddUpdate_ValueStore(changed[i].NodeName + "Dummy_" + changed[i].ParentNode.NodeID, nValueP);
                            cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "p", changed[i].ParentNode.NodeID, nValueP, HtmlClientID);
                            break;
                        default:
                            break;
                    }
                }

            }

            //LogUtility.Info("CMEFuture.cs", "DocImage_DocumentChanged()", "Doc Image Document Changed"); 
        }
        catch (Exception ex)
        {
            LogUtility.Error("CMEFuture.cs", "DocImage_DocumentChanged()", ex.Message, ex);
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

            LogUtility.Info("CMEFuture.cs", "DocImage_DocumentAlive()", "Doc Image Document Alive");
        }
        catch (Exception ex)
        {
            LogUtility.Error("CMEFuture.cs", "DocImage_DocumentAlive()", ex.Message, ex);
        }
    }

    public void sendDocumentStatus()
    {
        try
        {
            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "imgStatus", "m", "", imgStatus, HtmlClientID);

            //LogUtility.Info("CMEFuture.cs", "sendDocumentStatus()", "Send Document Status"); 
        }
        catch (Exception ex)
        {
            LogUtility.Error("CMEFuture.cs", "sendDocumentStatus()", ex.Message, ex);
        }
    }

    public void sendDocumentStatusToConn(string connID)
    {
        try
        {
            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Connection_Generic(connID, "imgStatus", "m", "", imgStatus, HtmlClientID);

            //LogUtility.Info("CMEFuture.cs", "sendDocumentStatusToconn()", "Send Document Status To ConnId"); 
        }
        catch (Exception ex)
        {
            LogUtility.Error("CMEFuture.cs", "sendDocumentStatusToConn()", ex.Message, ex);
        }
    }

    #endregion

    #region appendedFun


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
}

