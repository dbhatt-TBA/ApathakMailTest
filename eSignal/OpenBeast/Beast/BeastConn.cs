using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using BeastClientPlugIn;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Configuration;
using VCM.Common.Log;
using OpenBeast.Beast.ProductImages;

/// <summary>
/// Summary description for BeastConn
/// </summary>
/// 
public class BeastConn : IProductImageFactory
{
    #region Private Variable

    private ServerAgent _sa;
    private static volatile BeastConn instance = null;
    private static object syncRoot = new Object();

    public bool isConnection = false;
    private bool isForceUnload = false;

    //private swapService ss;
    private Dictionary<string, IProductImage> _dirBeastImg;
    #endregion

    #region Constructor
    public BeastConn()
    {
        _sa = new ServerAgent();
        isConnection = false;

        isForceUnload = false;
        //ss = new swapService();
        _dirBeastImg = new Dictionary<string, IProductImage>();
    }
    #endregion

    #region Beast Connection

    [LoaderOptimization(LoaderOptimization.MultiDomain)]
    void runPlugIn()
    {
        string userName = string.Empty;
        string Password = string.Empty;
        string ServerName = string.Empty;
        string ServerName2 = string.Empty;
        string RetryCount = string.Empty;
        try
        {
            isConnection = false;
            isForceUnload = false;
            userName = ConfigurationManager.AppSettings["UserName"].ToString();
            Password = ConfigurationManager.AppSettings["Password"].ToString();
            ServerName = ConfigurationManager.AppSettings["ServerName"].ToString();
            ServerName2 = ConfigurationManager.AppSettings["ServerName2"].ToString();
            RetryCount = ConfigurationManager.AppSettings["RetryCount"].ToString();

            Scripting.Dictionary props = new Scripting.Dictionary();
            _sa.StatusChanged += new _IServerAgentEvents_StatusChangedEventHandler(_sa_StatusChanged);
            _sa.AuthenticationFailed += new _IServerAgentEvents_AuthenticationFailedEventHandler(_sa_AuthenticationFailed);
            _sa.ConnectionLost += new _IServerAgentEvents_ConnectionLostEventHandler(_sa_ConnectionLost);
            _sa.ConnectionRestored += new _IServerAgentEvents_ConnectionRestoredEventHandler(_sa_ConnectionRestored);
            _sa.ForcedUnLoad += new _IServerAgentEvents_ForcedUnLoadEventHandler(_sa_ForcedUnLoad);
            _sa.ForceUpdate += new _IServerAgentEvents_ForceUpdateEventHandler(_sa_ForceUpdate);
            _sa.NotifyHost += new _IServerAgentEvents_NotifyHostEventHandler(_sa_NotifyHost);


            Object key = "Server0";
            Object value = ServerName;
            props.Add(ref key, ref value);
            key = "Port0";
            value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Port"]);
            props.Add(ref key, ref value);
            key = "retry0";
            value = RetryCount;
            props.Add(ref key, ref value);

            //key = "Server1";
            //value = ServerName2;
            //props.Add(ref key, ref value);
            //key = "Port1";
            //value = "8200";
            //props.Add(ref key, ref value);
            //key = "retry1";
            //value = RetryCount;
            //props.Add(ref key, ref value);

            _sa.Connect(userName, Password, props);

            string strlogDesc = "UserName: " + userName + "; " + "ServerName: " + ServerName + ";  run Plug In";
            LogUtility.Info("BeastConn.cs", "runPlugIn()", strlogDesc);
        }
        catch (Exception ex)
        {
            //Debug.WriteLine("********************************Run Plugin " + ex.Message);
            //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - runPlugIn() ERROR", ex.Message);

            string strErrorDesc = "UserName: " + userName + "; " + "ServerName: " + ServerName + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "runPlugIn()", strErrorDesc, ex);
        }
    }

    void _sa_NotifyHost(Scripting.Dictionary props)
    {
        try
        {

            //////ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_NotifyHost() :", "status:");

            StringBuilder sb = new StringBuilder();
            foreach (object key in props)
            {
                object rkey = key;
                sb.Append(key.ToString() + ":" + props.get_Item(ref rkey).ToString() + " ");
            }
            //Debug.WriteLine("*************************_sa_NotifyHost: " + sb);

            //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_NotifyHost()", sb.ToString());

            Dictionary<string, string> items = new Dictionary<string, string>();
            List<string> keys = new List<string>();
            List<string> vals = new List<string>();
            foreach (object key in props)
            {
                keys.Add((string)key);
                object rkey = key;
                vals.Add(Convert.ToString(props.get_Item(ref rkey)));
            }

            for (int i = 0; i < props.Count; i++)
            {
                items.Add(keys[i], vals[i]);
            }
            string typeKey = "Type";
            if (items.ContainsKey(typeKey) == true)
            {
                string type = items[typeKey];
                if (type == "Message")
                {
                    string message = "";
                    if (items["ErrorMessage"] != null)
                        message = items["ErrorMessage"];
                    object opt = items["MessageBoxOption"];

                    string strmsg = message.Replace("! Would you like to download?", ".");
                    //ss.AlertVersionMismatchMail(strmsg, "BeastConn");
                    //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_NotifyHost() --- ERROR " + message, "_sa_NotifyHost");
                    //MessageBoxResult res = MessageBox.Show(message, "BeastLite Update", MessageBoxButton.YesNo);
                    //props.Add("ReturnCode", (int)res);
                }
                else if (type == "LoadStatus")
                {
                    object key = "ReturnCode";
                    object returnVal = 1;
                    props.Add(ref key, ref returnVal);
                }
            }

            LogUtility.Info("BeastConn.cs", "_sa_NotifyHost()", "Sa Notify Host");
        }
        catch (Exception ex)
        {
            LogUtility.Error("BeastConn.cs", "_sa_NotifyHost()", ex.Message, ex);
        }
    }

    void _sa_ForceUpdate(string exeName)
    {
        //Debug.WriteLine("*************************_sa_ForceUpdate: status:" + exeName);

        LogUtility.Info("BeastConn.cs", "_sa_ForceUpdate()", exeName + "_sa_ForceUpdate");
        //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_ForceUpdate()", exeName);
    }

    void _sa_ForcedUnLoad()
    {
        //Debug.WriteLine("*************************_sa_ForcedUnLoad");

        isForceUnload = true;

        //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_ForcedUnLoad()", "_sa_ForcedUnLoad");

        LogUtility.Info("BeastConn.cs", "_sa_ForcedUnLoad()", "sa Forced UnLoad");
    }

    void _sa_ConnectionRestored()
    {
        //Debug.WriteLine("*************************_sa_ConnectionRestored: status:");

        LogUtility.Info("BeastConn.cs", "_sa_ConnectionRestored()", "_sa_ConnectionRestored");
        //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_ConnectionRestored()", "_sa_ConnectionRestored: status:");
    }

    void _sa_ConnectionLost()
    {
        //Debug.WriteLine("*************************_sa_ConnectionLost:");

        isConnection = false;

        LogUtility.Info("BeastConn.cs", "_sa_ConnectionLost()", "_sa_ConnectionLost");
        //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_ConnectionLost()", "_sa_ConnectionLost");
    }

    void _sa_AuthenticationFailed(string info)
    {
        //Debug.WriteLine("*************************_sa_AuthenticationFailed: status:" + info);

        LogUtility.Info("BeastConn.cs", "_sa_AuthenticationFailed()", info + "; sa AuthenticationFailed");

        //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_AuthenticationFailed()", info);
    }
    void _sa_StatusChanged(BeastClientPlugIn.ServerConnectionStatus Status, string info)
    {
        //Debug.WriteLine("*************************_sa_StatusChanged: status:" + Status + " :" + info);

        if (Status == BeastClientPlugIn.ServerConnectionStatus.CONNECTIONSTATUS_ACTIVE)
            isConnection = true;
        else if (Status == BeastClientPlugIn.ServerConnectionStatus.CONNECTIONSTATUS_INACTIVE)
        {
            string sMailSubject = "BeastClientPlugIn - Inactive Connection";
            string sMailBody = "<div style=\"font-family:Verdana;font-size:12px;text-align:left;color:#000;\">"
                                + "<p>Beast Connection is INACTIVE. Detail is as below:</p>"
                                + "<p>Status: " + Status + "</p>"
                                + "<p>Method: sa _sa_StatusChanged</p>"
                                + "<p>Information: " + info + "</p>"
                                + "<p>Please contact us if you have any questions.<br/><br/></p>"
                                + UtilityHandler.VCM_MailAddress_In_Html
                                + "</div>";

            string sMailTo = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
            string sMailToCC = ""; // System.Configuration.ConfigurationManager.AppSettings[""].ToString();
            string sMailToBCC = "";

            UtilityHandler.SendMail(sMailTo, sMailToCC, sMailToBCC, sMailSubject, sMailBody, false);
        }

        LogUtility.Info("BeastConn.cs", "_sa_StatusChanged()", Status + ";" + info + "; sa _sa_StatusChanged");
        //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - _sa_StatusChanged()", "status:" + Status + " :" + info);
    }
    #endregion

    #region Public Methods

    public static BeastConn Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {

                        instance = new BeastConn();
                        Thread trd = new Thread(new ThreadStart(instance.runPlugIn));
                        trd.Start();
                    }
                }
            }

            return instance;
        }
        set // for resetting the singleton object
        {
            instance = value;
        }
    }

    public ServerAgent SA
    {
        get
        {
            if (_sa == null)
            {
                if (_sa == null)
                {
                    _sa = new ServerAgent();
                }
            }

            return _sa;
        }
    }

    public int CreateBeastImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, string ActualProductID, bool IsSharedImage = false, string InstanceID = "", string username = "")
    {
        try
        {
            Thread.Sleep(1000);

            int iCount = 0;
            while (iCount < 15)
            {
                Thread.Sleep(1000);
                if (_sa.Status == ServerConnectionStatus.CONNECTIONSTATUS_ACTIVE)
                    break;

                if ((_sa.Status != ServerConnectionStatus.CONNECTIONSTATUS_AUTHENTICATING && _sa.Status != ServerConnectionStatus.CONNECTIONSTATUS_CONNECTING) && _sa.Status == ServerConnectionStatus.CONNECTIONSTATUS_INACTIVE)
                    break;

                if (isForceUnload)
                    break;

                iCount++;
            }

            if (isForceUnload)
            {
                return 3; // incompatible Beast Version
            }
            if (_sa.Status == ServerConnectionStatus.CONNECTIONSTATUS_INACTIVE) // if connection is inactive then return false
            {
                return 2; // connection inactive
            }

            #region create the beast image
            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID);
            bool isImageAvail = isImageAvailable(imageKey);
            //--------------------------------------------------------

            if (!isImageAvail)
            {
                IProductImage bi = createProductImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, IsSharedImage, InstanceID, username); // create the beast image.

                if (bi.DocImage.Status == DOMDataDocStatus.DATADOCSTATUS_NA)
                {
                    //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - CreateBeastImage() ERROR ", "Unable to create TheBeast Image.");
                    return 4; // Beast image creatin Failed
                }
                else
                {
                    bi.refCount = 1;
                    _dirBeastImg.Add(imageKey, bi);                    
                }
            }
            #endregion

            string strlogDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; SpeacialImageID :" + SpeacialImageID +"; Create Beast Image";
            LogUtility.Info("BeastConn.cs", "CreateBeastImage()", strlogDesc);

            //ss.SubmitUserLoginNotification_BeastPlugin("BeastConnection - CreateBeastImage()", "CREATE BEAST IMAGE SUCCESS");
            return 1;// successful creation of beast image
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; SpeacialImageID :" + SpeacialImageID + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "CreateBeastImage()", strErrorDesc, ex);
            throw;
        }
        finally
        {
            isForceUnload = false;
        }
        //return 1;
    }

    public IProductImage getBeastImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, string ActualProductID, bool IsSharedImage = false, string InstanceID = "", string username = "")
    {
        try
        {
            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID);
            if (_dirBeastImg.ContainsKey(imageKey))
            {
                return _dirBeastImg[imageKey];
            }
            else
            {
                CreateBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, IsSharedImage, InstanceID, username);
                return getBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, IsSharedImage, InstanceID, username);
            }

            string strlogDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; SpeacialImageID :" + SpeacialImageID + "; get Beast Image";
            LogUtility.Info("BeastConn.cs", "getBeastImage()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; SpeacialImageID :" + SpeacialImageID + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "getBeastImage()", strErrorDesc, ex);
        }
        return null;
    }

    public void RemoveBeastImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, bool createImage, string ActualProductID)
    {
        try
        {
            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID);

            _dirBeastImg.Remove(imageKey);

            if (createImage == true)
                CreateBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID);

            string strlogDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; SpeacialImageID :" + SpeacialImageID + "; Removed Beast Image";
            LogUtility.Info("BeastConn.cs", "RemoveBeastImage()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; SpeacialImageID :" + SpeacialImageID + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "RemoveBeastImage()", strErrorDesc, ex);
        }
    }

    public void DisconnectConnection()
    {
        //Debug.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ Disconnect");
        try
        {
            ////new
            _dirBeastImg.Clear();
            ////new
            _sa.Disconnect();
            Instance = null;
            LogUtility.Info("BeastConn.cs", "DisconnectConnection()", "Disconnect Connection");
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: DisconnectConnection :: " + errorMessage.ToString());
            LogUtility.Error("BeastConn.cs", "DisconnectConnection()", ex.Message, ex);
        }
    }

    public string getUserProductKeyForImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID)
    {
        return ProductID + "_" + ConnectionID + "_" + UserID + "_" + CustomerID + "_" + SpeacialImageID;
    }
    public void CloseImageBeastConn(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID)
    {
        try
        {

            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID);
            if (_dirBeastImg.ContainsKey(imageKey))
            {
                _dirBeastImg[imageKey].refCount = _dirBeastImg[imageKey].refCount - 1;
                if (_dirBeastImg[imageKey].refCount == 0)
                {
                    if (_dirBeastImg[imageKey].InstanceID != "")
                    {
                        _dirBeastImg[imageKey].distoryBeastImage();
                        _sa.CloseDocument("instid:" + _dirBeastImg[imageKey].InstanceID.Split(':')[1].Trim() + "", null);
                        _dirBeastImg.Remove(imageKey);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            // UtilityHandler.SendEmailForError("BeastConn.cs:: CloseImageBeastConn :: " + errorMessage.ToString());
            LogUtility.Error("BeastConn.cs", "CloseImageBeastConn()", ex.Message, ex);
        }

    }
    public void CloseInitiatorImageForExcel(string ConnectionID, string UserID)
    {
        try
        {
            var filterImages = from img in _dirBeastImg
                               where (img.Value.ConnectionID.Equals(ConnectionID) || img.Value.UserID.Equals(UserID))
                               select img;

            foreach (var img in filterImages.ToList())
            {
                img.Value.refCount = img.Value.refCount - 1;
                if (img.Value.refCount == 0)
                {
                    if (img.Value.InstanceID != "")
                    {
                        _sa.CloseDocument("instid:" + img.Value.InstanceID.Split(':')[1].Trim() + "", null);
                        RemoveBeastImage(img.Value.ProductID, img.Value.ConnectionID, img.Value.UserID, img.Value.CustomerID, img.Value.SpecialImageID, false, img.Value.ActualProductID);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            // UtilityHandler.SendEmailForError("BeastConn.cs:: CloseInitiatorImageForExcel :: " + errorMessage.ToString());
            LogUtility.Error("BeastConn.cs", "CloseInitiatorImageForExcel()", ex.Message, ex);
        }

    }
    public bool isImageAvailable(string imageKey)
    {
        return _dirBeastImg.ContainsKey(imageKey);
    }

    public IProductImage createProductImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, string ActualProductID, bool IsSharedImage, string InstanceID, string username)
    {
        string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; SpecialImageId: " + SpeacialImageID + "; ActualProductId: " + ActualProductID + "; IsSharedImage: " + IsSharedImage + "; InstanceID: " + InstanceID + "; ConnectionId: " + ConnectionID + "; ";

        try
        {
            if (AppsInfo.Instance._dirImgSID.ContainsKey(ActualProductID))
            {

                //Removable code start
                //if (AppsInfo.Instance._dirImgSID.ContainsKey("vcm_calc_swaptionVolPremStrike") && ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"]))
                //{
                //    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::SWAPTIONVOLPREMSTRIKE::");
                //    return new SwaptionVolPrem(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                //}
                //else if (AppsInfo.Instance._dirImgSID.ContainsKey("vcm_calc_cmefuture") && ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_cmefuture"]))
                //{
                //    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::CMEFUTURE::");
                //    return new CMEFuture(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                //}
                //else if (AppsInfo.Instance._dirImgSID.ContainsKey("vcm_calc_excelshare") && AppsInfo.Instance.GetPropertyInfo(AppsInfo.Properties.IsGridImage, AppsInfo.Properties.SIF_Id, ProductID) == "1" && ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_excelshare"]))
                //{
                //    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::EXCELSHARE::");
                //    return new ExcelShare(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                //}
                //else if (AppsInfo.Instance.GetPropertyInfo(AppsInfo.Properties.IsGridImage, AppsInfo.Properties.SIF_Id, ProductID) == "1")
                //{
                //    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::GRIDIMAGE::");
                //    return new GridImages(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                //}
                //else if (AppsInfo.Instance._dirImgSID.ContainsKey("vcm_calc_geoTrackerDashboard") && (ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_geoTrackerDashboard"]) || ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["2125"])))
                //{
                //    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::PERSISTANTIMAGE::");
                //    return new SingleInstanceApp(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                //}
                //else if (AppsInfo.Instance._dirImgSID.ContainsKey("trumid_client") && ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["trumid_client"]))
                //{
                //    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::PERSISTANTIMAGE::");
                //    return new TradingGeneric(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                //}
                //Removable code end
                //else
                {
                    LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::GENERICIMAGE::");
                    return new ProductClassGeneric(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, IsSharedImage, InstanceID, username);
                }
            }
            else
            {
                LogUtility.Info("BeastConn.cs", "createProductImage()", strlogDesc + "::Given product id is not found. Product ID: " + ProductID);
            }
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: createProductImage :: " + errorMessage.ToString());

            string strErrorDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "createProductImage()", ex.Message, ex);
        }

        return null;
    }

    public void openProductImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, string ConnectionIDSignalR, string ActualProductID, string UserMode, string username)
    {
        try
        {
            //string instanceID = "";

            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID);
            if (_dirBeastImg.ContainsKey(imageKey))
            {
                _dirBeastImg[imageKey].openBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, ConnectionIDSignalR, username);
                //instanceID = _dirBeastImg[imageKey].InstanceID;
                _dirBeastImg[imageKey].refCount = _dirBeastImg[imageKey].refCount + 1;
            }
            else
            {
                IProductImage iProdImg = BeastConn.Instance.getBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, false, "", username);
                //instanceID = iProdImg.InstanceID;
            }
            //string instanceInfo = UserID + "#" + CustomerID  + "#" + UserMode
            //2006149#100601#conn~vcm_calc_bondYield~2315f830-376b-4aec-b685-923383a0938a

            string strlogDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; open Product Image";
            LogUtility.Info("BeastConn.cs", "openProductImage()", strlogDesc);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: openProductImage :: " + errorMessage.ToString());

            string strErrorDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "openProductImage()", ex.Message, ex);
        }
    }

    public void shareProductImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, string ConnectionIDSignalR, string InstanceID, string ActualProductID, string username)
    {
        try
        {
            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID);
            if (_dirBeastImg.ContainsKey(imageKey))
            {
                _dirBeastImg[imageKey].openBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, ref _sa, ConnectionIDSignalR, username);
                _dirBeastImg[imageKey].refCount = _dirBeastImg[imageKey].refCount + 1;
            }
            else
            {
                BeastConn.Instance.getBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID, ActualProductID, true, InstanceID, username);
            }

            string strlogDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; Share Product Image";
            LogUtility.Info("BeastConn.cs", "shareProductImage()", strlogDesc);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: openProductImage :: " + errorMessage.ToString());

            string strErrorDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("BeastConn.cs", "shareProductImage()", ex.Message, ex);
        }
    }

    public void deleteBeastImageByConnectionID(string ConnectionID)
    {
        //Dictionary<string, IProductImage> imgList = _dirBeastImg.Where(img => img.Value.ConnectionID == ConnectionID).ToDictionary(imgList.  ;

        try
        {
            var filterImages = from img in _dirBeastImg
                               where (img.Value.ConnectionID.Equals(ConnectionID))
                               select img;

            foreach (var img in filterImages.ToList())
            {
                _sa.DeleteDocument("instid:" + img.Value.InstanceID.Split(':')[1].Trim() + "", null);
                RemoveBeastImage(img.Value.ProductID, img.Value.ConnectionID, img.Value.UserID, img.Value.CustomerID, img.Value.SpecialImageID, false, img.Value.ActualProductID);
            }

            string strlogDesc = "Delete Beast Image By ConnectionID";
            LogUtility.Info("BeastConn.cs", "deleteBeastImageByConnectionID()", strlogDesc);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: deleteBeastImageByConnectionID :: " + errorMessage.ToString());
            LogUtility.Error("BeastConn.cs", "deleteBeastImageByConnectionID()", ex.Message, ex);
        }
    }

    public void deleteBeastImageByConnectionIDGroupID(string ConnectionID, string GroupID)
    {
        //Dictionary<string, IProductImage> imgList = _dirBeastImg.Where(img => img.Value.ConnectionID == ConnectionID).ToDictionary(imgList.  ;

        try
        {
            var filterImages = from img in _dirBeastImg
                               where (img.Value.ConnectionID.Equals(ConnectionID) && img.Value.GruopID.Equals(GroupID))
                               select img;

            foreach (var img in filterImages.ToList())
            {
                _sa.DeleteDocument("instid:" + img.Value.InstanceID.Split(':')[1].Trim() + "", null);
                //RemoveBeastImage(img.Value.ProductID, img.Value.ConnectionID, img.Value.UserID, img.Value.CustomerID, img.Value.SpecialImageID, false, img.Value.ActualProductID);

                string strlogDescTmp = "Delete Beast Image By ConnectionID  GroupID";
                LogUtility.Info("BeastConn.cs", "deleteBeastImageByConnectionIDGroupID()", strlogDescTmp);
            }

            string strlogDesc = "Delete Beast Image By ConnectionID GroupID";
            LogUtility.Info("BeastConn.cs", "deleteBeastImageByConnectionIDGroupID()", strlogDesc);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: deleteBeastImageByConnectionIDGroupID :: " + errorMessage.ToString());
            LogUtility.Error("BeastConn.cs", "deleteBeastImageByConnectionIDGroupID()", ex.Message, ex);
        }
    }


    #region FullUpdate

    public void GetFullUpdate(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID)
    {
        try
        {
            string imageKey = getUserProductKeyForImage(ProductID, ConnectionID, UserID, CustomerID, SpeacialImageID);

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; SpecialImageId: " + SpeacialImageID + "; ConnectionId: " + ConnectionID + "; ";

            // Currently applied in TradeingGeneric.cs AND ProductClassGeneric.cs only
            // Replicate createProductImage() method's "if ladder" if full update is to be implemented for all kinds of imagages

            if (ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_swaptionVolPremStrike"]))
            {
                LogUtility.Info("BeastConn.cs", "GetFullUpdate()", strlogDesc + "::SWAPTIONVOLPREMSTRIKE::");
            }
            else if (ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_cmefuture"]))
            {
                LogUtility.Info("BeastConn.cs", "GetFullUpdate()", strlogDesc + "::CMEFUTURE::");
            }
            else if (AppsInfo.Instance.GetPropertyInfo(AppsInfo.Properties.IsGridImage, AppsInfo.Properties.SIF_Id, ProductID) == "1")
            {
                LogUtility.Info("BeastConn.cs", "GetFullUpdate()", strlogDesc + "::GRIDIMAGE::");
            }
            else if (ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_geoTrackerDashboard"]) || ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["2125"]))
            {
                LogUtility.Info("BeastConn.cs", "GetFullUpdate()", strlogDesc + "::PERSISTANTIMAGE::");
            }
            else if (ProductID == Convert.ToString(AppsInfo.Instance._dirImgSID["trumid_client"]))
            {
                LogUtility.Info("BeastConn.cs", "GetFullUpdate()", strlogDesc + "::TRUMID_CLIENT::");

                if (_dirBeastImg.ContainsKey(imageKey))
                {
                    TradingGeneric docImage = (TradingGeneric)_dirBeastImg[imageKey];
                    docImage.GetFullUpdate(ConnectionID);
                }
            }
            else
            {
                LogUtility.Info("BeastConn.cs", "GetFullUpdate()", strlogDesc + "::GENERICIMAGE::");

                if (_dirBeastImg.ContainsKey(imageKey))
                {
                    ProductClassGeneric docImage = (ProductClassGeneric)_dirBeastImg[imageKey];
                    docImage.GetFullUpdate(ConnectionID);
                }
            }
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("BeastConn.cs:: GetFullUpdate :: " + errorMessage.ToString());
            LogUtility.Error("BeastConn.cs", "GetFullUpdate()", ex.Message, ex);
        }
    }

    #endregion

    #endregion
}