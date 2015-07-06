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
using System.Collections.Concurrent;
using VCM.Common.Log;

namespace OpenBeast.Beast.ProductImages
{
    public class TradingGeneric : IProductImage
    {
        #region member devlaration
        private DOMDataDocument _beastVolPrimGridImagesDataDoc = null;

        private string _userID = "0";
        private string _productID = "0";
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
        private string nodeTitleOld = "";
        private string title = "";
        private bool IsFirstTime = true;
        ConcurrentDictionary<string, string> valStore;

        #endregion

        #region constructor
        public TradingGeneric(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent sa, bool strIsSharedImage, string strInstanceID, string username)
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

            //   Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);
            string eProductName = strActualProductID;


            if (SpecialImageID == "0")
                HtmlClientID = eProductName.ToString();
            else
                HtmlClientID = SpecialImageID;

            GruopID = BeastConn.Instance.getUserProductKeyForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID);

            valStore = new ConcurrentDictionary<string, string>();

        }
        #endregion

        #region code for creating Beast Image and Event Handling
        public void createBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string username)
        {
            try
            {
                string logDesc = getLogDescForGenericInfo();

                LogUtility.Info("TraddingGeneric.cs", "createBeastImage()", logDesc);

                if (DocImage != null)
                {
                    return;
                }

                Scripting.Dictionary Props = new Scripting.Dictionary();
                //Object key = "AppDefs";
                //Object value = strConnectionID;

                //Props.Add(ref key, ref value);

                Object key1 = "RequestFieldPropertiesAsNode";
                Object value1 = true;
                Props.Add(ref key1, ref value1);

                Object key2 = "OnlyVisible";
                Object value2 = 1;
                Props.Add(ref key2, ref value2);

                Object key3 = "RequestDisplayString";
                Object value3 = true;
                Props.Add(ref key3, ref value3);

                if (username.Contains('#'))
                {
                    string[] userInfoAry = username.Split('#');
                    strUserID = strUserID.Trim();
                    if (!string.IsNullOrEmpty(strUserID))
                    {
                        strUserID = new DAL.clsDAL(false).GetUserID(userInfoAry[0].ToString()).ToString();
                    }
                    Object key = "ImpersonatedUser";
                    Object value = userInfoAry[0].ToString();
                    Props.Add(ref key, ref value);

                    Object key4 = "AppDefs";
                    Object value4 = userInfoAry[1].ToString() + "," + strUserID;
                    Props.Add(ref key4, ref value4);
                }
                else
                {
                    Object key = "ImpersonatedUser";
                    Object value = username;
                    Props.Add(ref key, ref value);
                }


                // Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);
                string eProductName = strActualProductID;
                LogUtility.Info("TraddingGeneric.cs", "createBeastImage()ImpersonatedUser", "username:" + username + " ProductName:" + eProductName);


                DocImage = _sa.RequestDocument("appid:" + Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]), Props);

                DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
                DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
                DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
                DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
                DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
                DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);
            }
            catch (Exception ex)
            {
                string strErrorDesc = getLogDescForGenericInfo() + " MSG : " + ex.Message;
                LogUtility.Error("TraddingGeneric.cs", "createBeastImage()", strErrorDesc, ex);
                throw;
            }
        }

        public void openBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string ConnectionIDSignalR, string username)
        {
            try
            {
                string logDesc = getLogDescForGenericInfo();

                LogUtility.Info("TraddingGeneric.cs", "openBeastImage()", logDesc);

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
                DOMDataNode nodeTitle = DocImage.get_NodeByID("AppTitle");
                string title = nodeTitle.DataValue.ToString();
                VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "updt", "title", DocImage.Name, title, HtmlClientID);
            }
            catch (Exception ex)
            {
                string strErrorDesc = getLogDescForGenericInfo() + " MSG : " + ex.Message;
                LogUtility.Error("TraddingGeneric.cs", "openBeastImage()", strErrorDesc, ex);
                throw;
            }
        }

        public void shareBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, string strInstanceID, ref ServerAgent _sa, string username)
        {
            try
            {
                LogUtility.Info("ProductClassGeneric.cs", "shareBeastImage()", getLogDescForGenericInfo());

                if (DocImage != null)
                {
                    return;
                }

                // Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), strActualProductID);
                string eProductName = strActualProductID;


                Scripting.Dictionary Props = new Scripting.Dictionary();

                Object key = "SID";
                Object value = Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]);
                Props.Add(ref key, ref value);

                Object key1 = "RequestFieldPropertiesAsNode";
                Object value1 = true;
                Props.Add(ref key1, ref value1);

                Object key2 = "OnlyVisible";
                Object value2 = 1;
                Props.Add(ref key2, ref value2);

                Object key3 = "ImpersonatedUser";
                Object value3 = username;
                Props.Add(ref key3, ref value3);

                Object key4 = "RequestFieldProperties";
                Object value4 = true;
                Props.Add(ref key4, ref value4);


                DocImage = _sa.RequestDocument("instid:" + strInstanceID + "", Props);

                DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
                DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
                DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
                DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
                DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
                DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);
            }
            catch (Exception ex)
            {
                string strErrorDesc = getLogDescForGenericInfo() + " MSG : " + ex.Message;
                LogUtility.Error("ProductClassGeneric.cs", "shareBeastImage()", strErrorDesc, ex);
                throw;
            }
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

                    // Set Initiatior closed flag in AppStore_AutoURL 
                    //DAL.clsDAL objDal = new DAL.clsDAL(false);
                    //string pInstanceID = _instanceID.Split(':')[1].Trim();
                    //objDal.Set_SharedCalc_StoppedFlag(Convert.ToInt32(_userID), pInstanceID);
                    //

                    //VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "shareStatus", "m", "stop", "Initiator has stopped sharing this calculator.", "");
                    //VCMComet.Instance.Send_Message_To_Client_Group_Generic_Except_Conn(GruopID, "shareStatus", "m", "stop", "Initiator has stopped sharing this calculator.", HtmlClientID, ConnectionID);

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
                LogUtility.Info("TraddingGeneric.cs", "DocImage_StatusChanged()", logDesc);
            }
            catch (Exception ex)
            {
                string strErrorDesc = getLogDescForGenericInfo() + " MSG : " + ex.Message;
                LogUtility.Error("TraddingGeneric.cs", "DocImage_StatusChanged()", strErrorDesc, ex);
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

                string logDesc = getLogDescForGenericInfo();
                LogUtility.Info("TraddingGeneric.cs", "DocImage_DocumentStale()", logDesc);
            }
            catch (Exception ex)
            {

            }
        }
        void DocImage_DocumentComplete()
        {

        }

        void AddUpdate_ValueStore(string name, string value)
        {
            if (valStore.ContainsKey(name))
            {
                valStore[name] = value;
            }
            else
            {
                valStore.AddOrUpdate(name, value, (key, oldValue) => value);
            }
        }

        void DocImage_DocumentChanged(DOMDataNodeList changed)
        {
            try
            {
                VCMComet cometVOrder = VCMComet.Instance;

                for (int i = 0; i < changed.Length; i++)
                {
                    //if (changed[i].DataValue.ToString().Length > 0)
                    //{
                    switch (changed[i].NodeName)
                    {
                        case "List":
                            string nName = changed[i].NodeName;
                            string nValue = changed[i].DataValue.ToString();
                            DOMDataNode prntNode = changed[i].ParentNode;
                            DOMDataNodeList chlNode = changed[i].ChildNodes;
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
                        case "ToolTip":
                            string strtooltip1 = "";
                            string NId = changed[i].ParentNode.NodeID;
                            DOMDataNode cnode = DocImage.get_NodeByID(NId);//.get_NamedChild("ToolTip");
                            if (cnode.get_NamedChild("ToolTip") != null)
                            {
                                DOMDataNode tooltipNode = cnode.get_NamedChild("ToolTip");
                                if (tooltipNode.DataValue != null && tooltipNode.DataValue != "")
                                {
                                    strtooltip1 = tooltipNode.DataValue.ToString();
                                    string nNameT = changed[i].NodeName;
                                    string nValueT = changed[i].DataValue.ToString();
                                    DOMDataNode prntNodeT = changed[i].ParentNode;
                                    DOMDataNodeList chlNodeT = changed[i].ChildNodes;
                                    AddUpdate_ValueStore(changed[i].NodeName + "Dummy_" + changed[i].ParentNode.NodeID, nValueT);
                                    cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "tt", changed[i].ParentNode.NodeID, strtooltip1, HtmlClientID);

                                }
                            }
                            break;
                        default:
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
                    }
                    //}
                }

                //For showing title on web image
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
                        LogUtility.Info("TraddingGeneric.cs", "DocImage_DocumentChanged()", logDesc);
                    }
                    cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "title", DocImage.Name, title, HtmlClientID);
                }
                else if (title != nodeTitleOld)
                {
                    title = nodeTitle.DataValue.ToString();
                    nodeTitleOld = title;
                    if (title.Split(':')[0] == " " || title.Split(':')[0] == null)
                    {
                        string logDesc = getLogDescForGenericInfo();
                        logDesc += ":BeastPluginError";
                        LogUtility.Info("TraddingGeneric.cs", "DocImage_DocumentChanged()", logDesc);
                    }
                    cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "title", DocImage.Name, title, HtmlClientID);
                }
            }
            catch (Exception ex)
            {
                string strErrorDesc = getLogDescForGenericInfo() + " MSG : " + ex.Message;
                LogUtility.Error("TraddingGeneric.cs", "DocImage_DocumentChanged()", strErrorDesc, ex);
            }
        }

        public void DocImage_DocumentAlive()
        {
            try
            {
                IsDocumentAlive = true; //change the socument status
                _isStale = false; // change the stale state
                InstanceID = DocImage.Name;
                BeastConn.Instance.SA.LockDocument(_instanceID, null);

                sendDocumentStatus();

                string logDesc = getLogDescForGenericInfo();
                LogUtility.Info("TraddingGeneric.cs", "DocImage_DocumentAlive()", logDesc);
            }
            catch (Exception ex)
            {

            }
        }

        public void sendDocumentStatus()
        {
            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "imgStatus", "m", "", imgStatus, HtmlClientID);
        }

        public void sendDocumentStatusToConn(string connID)
        {
            string imgStatus = IsDocumentAlive ? "True" : "False";
            VCMComet.Instance.Send_Message_To_Client_Connection_Generic(connID, "imgStatus", "m", "", imgStatus, HtmlClientID);
        }

        #endregion

        #region appendedFun
        public string getLogDescForGenericInfo()
        {
            return "ProdId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustId: " + CustomerID + "; " + "ConnId: " + ConnectionID + "ConnIDSignalR:" + ConnectionID + "ActProdID:" + ActualProductID + "; InstID : " + InstanceID + "; SpeImgID : " + SpecialImageID + "; HtmCliID : " + HtmlClientID;
        }

        #endregion

        #region Public Default Properties
        public bool IsDocumentAlive
        {
            get { return _isDocumentAlive; }
            set
            {
                _isDocumentAlive = value;
                //sendDocumentStatus();
            }
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

        public void GetFullUpdate(string ConnectionIDSignalR)
        {
            //Push full update directly reading from valstore
            //Same as openBeastImage call 
            try
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
                    else if (strDataName.IndexOf("ToolTipDummy") >= 0)
                    {
                        cometVOrder.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "mnlupdt", "tt", strDataName.Split('_')[1], strDataValue, HtmlClientID);
                    }
                    else
                    {
                        cometVOrder.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "updt", "i", strDataName, strDataValue, HtmlClientID);
                    }
                }
            }
            catch (Exception ex)
            {
                string strErrorDesc = getLogDescForGenericInfo() + " MSG : " + ex.Message;
                LogUtility.Error("TradingGeneric.cs", "GetFullUpdate()", strErrorDesc, ex);
                throw;
            }
        }
    }
}