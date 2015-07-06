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
using System.Collections;
using System.Collections.Concurrent;

namespace OpenBeast.Beast.ProductImages
{
    public class SingleInstanceApp : IProductImage
    {
         #region member devlaration
        private DOMDataDocument _beastVolPrimGridImagesDataDoc = null;

        //private ServerAgent _sa;
        private string _userID = "0";
        private string _productID = "2125";
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
        public string Tempuserid;
        public string CurrencyDataValue;
        public bool IsShare = false;
        private string nodeTitleOld = "";
        private string title = "";
        private bool IsFirstTime = true;
        ConcurrentDictionary<string, string> valStore;


        //END ------  Grid Field

        #endregion

        #region constructor

        public SingleInstanceApp(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent sa, bool strIsSharedImage, string strInstanceID, string username)
        {
            // _sa = sa;
            DataSet ds = new DataSet();

            DAL.clsDAL objDAL = new DAL.clsDAL(false);
            ds = objDAL.GetTokenInstanceID(Convert.ToInt32(strProductID));

            initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                strInstanceID = Convert.ToString(ds.Tables[0].Rows[0]["InstanceID"]);
                IsShare = true;
                shareBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, strInstanceID, ref sa, username);
            }
            else
            {
                createBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, ref sa, username);
                /*****************************************/

                //objDAL.SubmitTokenImageKey(_instanceID.Split(':')[1], UserID,9597);
                /**********************************************/

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

            valStore = new ConcurrentDictionary<string, string>();
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

                               


                string eProductName = strActualProductID;
                //   DocImage = _sa.RequestDocument("appid:" + Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]), Props);
                DocImage = _sa.RequestDocument("appid:" + strProductID, Props);

                //DocImage = _sa.RequestDocument("appid:" + Convert.ToString((int)Definations.BeastImageAppID.vcm_calc_realtimedata), Props);

                DocImage.DocumentAlive += new _IDOMDataDocumentEvents_DocumentAliveEventHandler(DocImage_DocumentAlive);
                DocImage.DocumentChanged += new _IDOMDataDocumentEvents_DocumentChangedEventHandler(DocImage_DocumentChanged);
                DocImage.DocumentComplete += new _IDOMDataDocumentEvents_DocumentCompleteEventHandler(DocImage_DocumentComplete);
                DocImage.DocumentStale += new _IDOMDataDocumentEvents_DocumentStaleEventHandler(DocImage_DocumentStale);
                DocImage.ManualUpdateRequestStatus += new _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler(DocImage_ManualUpdateRequestStatus);
                DocImage.StatusChanged += new _IDOMDataDocumentEvents_StatusChangedEventHandler(DocImage_StatusChanged);

                string logDesc = getLogDescForGenericInfo();

                LogUtility.Info("SingleInstanceApp.cs", "createBeastImage()", logDesc);
            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "createBeastImage()", strErrorDesc, ex);
            }
        }

        public void openBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string ConnectionIDSignalR, string username)
        {
            try
            {
                string logDesc = getLogDescForGenericInfo();

                LogUtility.Info("SingleInstanceApp.cs", "openBeastImage()", logDesc);

                sendDocumentStatusToConn(ConnectionIDSignalR);

                if (DocImage == null)
                {
                    initDataForImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID);
                    createBeastImage(strProductID, strConnectionID, strUserID, strCustomerID, strSpecialImageID, strActualProductID, ref _sa, username);
                }
                else if (IsDocumentAlive == false)
                {
                    BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, true, ActualProductID); // remove beast image from the list
                }
                else
                {
                    //VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Currency", "Currency", Currency);
                    //VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Currency", "Straddle", Straddle);
                    //VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Vols", "Vols", Vols);
                    //VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "VolShift", "VolShift", VolShift);
                    //VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "Currency", "List", List);
                    //VCMComet.Instance.sendOtherDataResponseToConnection(ConnectionIDSignalR, "PremGridTitle", "Title", PremGridTitle);
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

            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + strProductID + "; " + "UserId: " + strUserID + "; " + "CustomerId: " + strCustomerID + "; " + "ConnectionId: " + strConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "openBeastImage()", strErrorDesc, ex);
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
                Object value = strProductID;
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

                    //BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, true, ActualProductID); // remove beast image from the list
                    BeastConn.Instance.RemoveBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, false, ActualProductID); // remove beast image from the list
                    //shareBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ActualProductID, InstanceID, ref  _sa);

                    //BeastConn.Instance.createProductImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ActualProductID, true, InstanceID);

                }
                else if (Status == DOMDataDocStatus.DATADOCSTATUS_ALIVE)
                {
                    IsDocumentAlive = true;
                    sendDocumentStatus();
                }

                string logDesc = getLogDescForGenericInfo();
                logDesc += " ; DOMDataDocStatus: " + info;
                LogUtility.Info("SingleInstanceApp.cs", "DocImage_StatusChanged()", logDesc);
            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "DocImage_StatusChanged()", ex.Message, ex);
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
                LogUtility.Info("SingleInstanceApp.cs", "DocImage_DocumentStale()", logDesc);
            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "DocImage_DocumentStale()", ex.Message, ex);
            }
        }
        void DocImage_DocumentComplete()
        {

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
                        LogUtility.Info("ProductClassGeneric.cs", "DocImage_DocumentChanged()", logDesc);
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
                        LogUtility.Info("ProductClassGeneric.cs", "DocImage_DocumentChanged()", logDesc);
                    }
                    cometVOrder.Send_Message_To_Client_Group_Generic(GruopID, "updt", "title", DocImage.Name, title, HtmlClientID);
                }


            }
            catch (Exception ex)
            {
                string logDesc = getLogDescForGenericInfo();

                LogUtility.Error("SingleInstanceApp.cs", "DocImage_DocumentChanged()", logDesc, ex);
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

                if (IsShare == false)
                {
                    /*****************************************/
                    DAL.clsDAL objDAL = new DAL.clsDAL(false);

                    objDAL.SubmitTokenImageKey(_instanceID.Split(':')[1], UserID, Convert.ToInt32(ProductID));
                    /**********************************************/

                }
                LogUtility.Info("SingleInstanceApp.cs", "DocImage_DocumentAlive()", "Doc Image Document Alive");
            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "DocImage_DocumentAlive()", ex.Message, ex);
            }
        }

        public void sendDocumentStatus()
        {
            try
            {
                string eProductName = ActualProductID;
                string imgStatus = IsDocumentAlive ? "True" : "False";
                VCMComet.Instance.Send_Message_To_Client_Group_Generic(GruopID, "imgStatus", "m", "", imgStatus, eProductName.ToString());
                //LogUtility.Info("GeoTrack.cs", "sendDocumentStatus()", "send Document Status");
            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "sendDocumentStatus()", ex.Message, ex);
            }
        }

        public void sendDocumentStatusToConn(string connID)
        {
            try
            {
                string eProductName = ActualProductID;

                string imgStatus = IsDocumentAlive ? "True" : "False";
                VCMComet.Instance.Send_Message_To_Client_Connection_Generic(connID, "imgStatus", "m", "", imgStatus, eProductName.ToString());

                //LogUtility.Info("GeoTrack.cs", "sendDocumentStatusToConn()", "Send Document Status To Conn");
            }
            catch (Exception ex)
            {
                string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
                LogUtility.Error("SingleInstanceApp.cs", "sendDocumentStatusToConn()", ex.Message, ex);
            }
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
}