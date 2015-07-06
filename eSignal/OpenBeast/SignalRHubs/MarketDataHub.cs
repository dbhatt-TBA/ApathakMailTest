using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VCM.Common.Log;
using DAL;
using System.Data;
using System.IO;
using System.Web.SessionState;
/// <summary>
/// Summary description for MarketDataHub
/// </summary>
public class MarketDataHub : Hub
{
    Service ws = new Service();

    public void Send(string name, string message, string GUID, string ClientType)
    {
        
        //string strFileName = string.Empty;
        //strFileName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["LogTimerFile"]);
        //FileStream fs1 = new FileStream(strFileName, FileMode.OpenOrCreate, FileAccess.Write); ;
        //StreamWriter writer = new StreamWriter(strFileName,true);

        //File.AppendAllText(strFileName, "Image Creation Calling Start" + Environment.NewLine);

        //writer.WriteLine("Image Creation Calling Start");
        //writer.Write(Convert.ToString(System.DateTime.Now.Hour) + Convert.ToString(System.DateTime.Now.Minute) + Convert.ToString(System.DateTime.Now.Second) + Convert.ToString(System.DateTime.Now.Millisecond));
        //writer.Close();
        //fs1.Close();

        string ConnectionIDSignalR = "0";
        string ConnectionID = "0";
        string UserID = "0";
        string CustomerID = "0";
        string UserMode = "0";
        string SpecialImageID = "0";
        string InstanceID = "";

        string ProductID = "0";
        string GroupName = "";

        string actualUserID = "0";
        string actualCustID = "0";
        string username = "";

        /*TruMID*/
        string SwarmID = "";
        try
        {
            ConnectionIDSignalR = Context.ConnectionId;
            string[] userInfoAry = message.Split('#');

            if (userInfoAry.Length > 0)
            {
                actualUserID = userInfoAry[0];
                actualCustID = userInfoAry[1];

                if (userInfoAry.Length == 4)
                {
                    //SwarmID = userInfoAry[3];
                    username = userInfoAry[3];
                }

                if (userInfoAry[2].Split('~').Length > 1)
                {
                    UserMode = userInfoAry[2].Split('~')[0];
                    InstanceID = userInfoAry[2].Split('~')[1];
                }
                else
                {
                    UserMode = userInfoAry[2];
                }
            }

            if (UserMode.Trim().ToLower() == "spe")
            {
                SpecialImageID = "sp1";
            }
            else if (UserMode.Trim().ToLower() == "user")
            {
                UserID = userInfoAry[0];
            }
            else if (UserMode.Trim().ToLower() == "cust")
            {
                CustomerID = userInfoAry[1];
            }
            else if (UserMode.Trim().ToLower() == "conn")
            {
                ConnectionID = Context.ConnectionId;
            }

            if (AppsInfo.Instance._dirImgSID == null || AppsInfo.Instance._dirImgSID.Count == 0)
            {
                BeastImageSID();
            }

            string eProductName = name;
            ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]);
            string[] ValidToken = null;

            if (InstanceID == "")
            {
                ValidToken = VCMComet.Instance.ValidateAuthToken(actualUserID, ClientType, GUID);

                if (ValidToken[0] == "False")
                {
                    string eleID = "Send()^ConnectionID:" + ConnectionID + "^UserID:" + UserID + "^CustomerID:" + CustomerID + "^ConnectionIDSignalR:" + ConnectionIDSignalR + "^ClientType:" + ClientType + "^UserMode:" + UserMode + "^CalcName:" + eProductName;
                    VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", eleID, ValidToken[1], "AuthInvalid");

                    return;
                }

                //username = "";
                //if (SwarmID != "")
                //{
                //    username += "#" + SwarmID;
                //}

                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                JoinGroup(GroupName);
                VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR, eProductName.ToString(), UserMode, username);
                

                //if (name == "vcm_calc_bond_depth_grid_excel" || name == "vcm_calc_bond_grid_Excel" || name == "vcm_calc_kcg_bonds_submit_order_excel" || name == "vcm_calc_excelshare"))
                if (AppsInfo.Instance.GetPropertyInfo(AppsInfo.Properties.IsGridImage, AppsInfo.Properties.SIF_Id, ProductID) == "1")
                {
                    // this if condition is added because of we need to block submit last calc entry in database of these 3 calc
                    // these calc is available in excel but not in web so.
                }
                else
                {
                    //ws.SubmitLastOpenCalc(Convert.ToInt64(actualUserID.Trim()), name, message); // Last Calc
                }
                //  VCMComet.Instance.AddConnectionToDir(actualUserID, GUID, ConnectionIDSignalR, eProductName, ProductID);

                LogUtility.Info("MarketDataHub.cs", "Send()", "$$UserID:" + actualUserID + ":" + (int)OpenBeast.Utilities.SysLogEnum.CREATECALCULATOR + " $$Token:" + GUID + "$$ClientType:" + ClientType + "$$ConnectionID:" + ConnectionIDSignalR + "$$ImageName:" + eProductName + "$$ImageSIFID:" + ProductID + "$$ImageCreationTime:" + System.DateTime.Now);
            }
            else
            {
                ValidToken = VCMComet.Instance.ValidateAuthToken(userInfoAry[2].Split('~')[3], ClientType, GUID);

                if (ValidToken[0] == "False")
                {
                    string eleID = "Send()^ConnectionID:" + ConnectionID + "^UserID:" + UserID + "^CustomerID:" + CustomerID + "^ConnectionIDSignalR:" + ConnectionIDSignalR + "^ClientType:" + ClientType + "^UserMode:" + UserMode + "^CalcName:" + eProductName;

                    VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", eleID, ValidToken[1], "AuthInvalid");

                    return;
                }
                if (userInfoAry[2].Split('~').Length >= 4)
                {
                    username = userInfoAry[2].Split('~')[4];
                }
                ConnectionID = userInfoAry[2].Split('~')[2];
                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                JoinGroup(GroupName);

                VCMComet.Instance.connectToSharedBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR, eProductName.ToString(), InstanceID, username);
                //VCMComet.Instance.AddConnectionToDir(userInfoAry[2].Split('~')[3], GUID, ConnectionIDSignalR, eProductName, ProductID);
                LogUtility.Info("MarketDataHub.cs", "Send()", "$$UserID:" + "0" + ":" + (int)OpenBeast.Utilities.SysLogEnum.OPENSHAREDCALCULATOR + " $$Token:" + GUID + "$$ClientType:" + ClientType + "$$ConnectionID:" + ConnectionIDSignalR + "$$ImageName:" + eProductName + "$$ImageSIFID:" + ProductID + "$$ImageCreationTime:" + System.DateTime.Now);

            }
            //LogUtility.Info("MarketDataHub.cs", "Send()", "$$UserID:" + actualUserID + ":" + (int)OpenBeast.Utilities.SysLogEnum.CREATECALCULATOR + " $$Token:" + GUID + "$$ClientType:" + ClientType + "$$ConnectionID:" + ConnectionIDSignalR + "$$ImageName:" + eProductName + "$$ImageSIFID:" + ProductID + "$$ImageCreationTime:" + System.DateTime.Now);

            VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "", "m", "", ConnectionIDSignalR, "ConnectionIDSignalR");

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID " + SpecialImageID + ";  ConnectionIDSignalR :" + ConnectionIDSignalR + "; send ";
            LogUtility.Debug("MarketDataHub.cs", "Send()", strlogDesc);
        }
        catch (Exception ex)
        {

            //writer.WriteLine("Image Creation exception");
            //writer.Write(Convert.ToString(System.DateTime.Now.Hour) + Convert.ToString(System.DateTime.Now.Minute) + Convert.ToString(System.DateTime.Now.Second) + Convert.ToString(System.DateTime.Now.Millisecond));
            //writer.Close();

            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("MarketDataHub.cs:: Send :: " + errorMessage.ToString());

            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID " + SpecialImageID + ";  ConnectionIDSignalR :" + ConnectionIDSignalR + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "Send()", strErrorDesc, ex);
        }

        //writer.WriteLine("Image Creation Calling End");
        //writer.Write(Convert.ToString(System.DateTime.Now.Hour) + Convert.ToString(System.DateTime.Now.Minute) + Convert.ToString(System.DateTime.Now.Second) + Convert.ToString(System.DateTime.Now.Millisecond));
        //writer.Close();

    }

    public void BeastImageSID()
    {
        try
        {
            AppsInfo.Instance._dirImgSID = new Dictionary<string, int>();
            clsDAL objclsDAL = new clsDAL(false);
            DataSet dsImgSID = new DataSet();
            dsImgSID = objclsDAL.GetBeastImageSID();

            DataRow _dr;

            for (int index = 0; index < dsImgSID.Tables[0].Rows.Count; index++)
            {
                AppsInfo.Instance._dirImgSID.Add(Convert.ToString(dsImgSID.Tables[0].Rows[index]["AppName"]), Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["BeastImageSID"]));

                _dr = AppsInfo.Instance._dtAppsInfo.NewRow();
                _dr["App_Id"] = Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["AppId"]);
                _dr["Reg_Id"] = Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["RegId"]);
                _dr["SIF_Id"] = Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["BeastImageSID"]);
                _dr["Name"] = Convert.ToString(dsImgSID.Tables[0].Rows[index]["AppName"]).Trim();
                _dr["IsGridImage"] = Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["IsGridImage"]);
                _dr["IsSharable"] = Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["ISshareable"]);
                _dr["ShareExpirationTime"] = Convert.ToInt32(dsImgSID.Tables[0].Rows[index]["shareminitues"]);
                //_dr["eSignalKey"] = Convert.ToString(dsImgSID.Tables[0].Rows[index]["eSignalKey"]).Trim();

                AppsInfo.Instance._dtAppsInfo.Rows.Add(_dr);

                //_dirImgExpirationTime
            }
        }
        catch (Exception ex)
        {
            LogUtility.Error("MarketDataHub.cs", "BeastImageSID()", "", ex);
        }
    }


    public void sharerequest(string name, string message, string userToShare, string senderEmail, string GUID, string ClientType)
    {
        string senderMessage = "";
        string ConnectionIDSignalR = "0";
        string ConnectionID = "0";
        string UserID = "0";
        string CustomerID = "0";
        string UserMode = "0";
        string SpecialImageID = "0";
        string InstanceID = "";
        long SenderID = 0;
        string ProductID = "0";
        string GroupName = "";
        //   string AuthToken = "";
        try
        {
            //  AuthToken = ws.getAuthToken(userToShare, ClientType);

            ConnectionIDSignalR = Context.ConnectionId;
            string[] userInfoAry = message.Split('#');

            if (userInfoAry.Length > 0)
            {
                UserMode = userInfoAry[2].Split('~')[0];
            }

            if (UserMode.Trim().ToLower() == "spe")
            {
                SpecialImageID = "sp1";
            }
            else if (UserMode.Trim().ToLower() == "user")
            {
                UserID = userInfoAry[0];
            }
            else if (UserMode.Trim().ToLower() == "cust")
            {
                CustomerID = userInfoAry[1];
            }
            else if (UserMode.Trim().ToLower() == "conn")
            {
                ConnectionID = Context.ConnectionId;
            }

            SenderID = Convert.ToInt64(userInfoAry[0]);

            //  Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), name);

            string eProductName = name;
            ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]);

            //int isAppSharable = Convert.ToInt32(AppsInfo.Instance.GetPropertyInfo(AppsInfo.Properties.IsSharable, AppsInfo.Properties.SIF_Id, ProductID));
            //if (isAppSharable == 1)
            //{
            int iExpirationTime = 0;
            iExpirationTime = Convert.ToInt32(AppsInfo.Instance.GetPropertyInfo(AppsInfo.Properties.ShareExpirationTime, AppsInfo.Properties.SIF_Id, ProductID));

            GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
            InstanceID = VCMComet.Instance.getImageInstanceID(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR, eProductName.ToString());

            string shareStatusMsg = "";
            if (userToShare.Trim().ToLower() == "bulkshare@thebeastapps.com#")
            {
                shareStatusMsg = ws.SubmitBeastCalcAutoUrlShare(SenderID, InstanceID, message + "~" + name + "~" + ConnectionID, "", userToShare, senderEmail, senderMessage, ClientType, GUID, iExpirationTime, true);
            }
            else
            {
                shareStatusMsg = ws.SubmitBeastCalcAutoUrlShare(SenderID, InstanceID, message + "~" + name + "~" + ConnectionID, "", userToShare, senderEmail, senderMessage, ClientType, GUID, iExpirationTime, false);
            }

            if (shareStatusMsg.Contains("#"))
            {
                if (shareStatusMsg.Split('#')[0] == "AuthInvalid")
                {
                    string invalidmessage = "Session expired";

                    if (shareStatusMsg.Split('#')[1] == "Your session has expired. Please relogin")
                    {
                        invalidmessage = "Your session has expired";
                    }
                    else if (shareStatusMsg.Split('#')[1] == "Authentication list is empty")
                    {
                        invalidmessage = "Token list is empty";
                    }
                    string eleID = "sharerequest()^ConnectionID:" + ConnectionID + "^UserID:" + UserID + "^CustomerID:" + CustomerID + "^ConnectionIDSignalR:" + ConnectionIDSignalR + "^ClientType:" + ClientType + "^UserMode:" + UserMode + "^CalcName:" + eProductName + "^userToShare: " + userToShare + "^senderEmail:" + senderEmail + "^senderMessage:" + senderMessage;

                    VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", eleID, invalidmessage, "AuthInvalid");
                }
                else if (shareStatusMsg.Split('#')[0] == "-1")
                {
                    //No emails to share
                    string invalidmessage = shareStatusMsg.Split('#')[1];
                    VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", "", invalidmessage, "");
                }
            }
            else
            {
                VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", "", shareStatusMsg, "");

                string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; userToShare : " + userToShare + "; senderEmail :" + senderEmail + "; senderMessage :" + senderMessage + "; sharerequest ";
                LogUtility.Debug("MarketDataHub.cs", "sharerequest()", strlogDesc);
                if (!userToShare.Contains('#'))
                {
                    userToShare = userToShare + "#";
                }
                LogUtility.Info("MarketDataHub.cs", "sharerequest()", "$$UserID:" + SenderID + ":" + (int)OpenBeast.Utilities.SysLogEnum.SHARECALCULATOR + " $$Token:" + GUID + "$$ClientType:" + ClientType + "$$ConnectionID:" + ConnectionIDSignalR + "$$ImageName:" + eProductName + "$$ImageSIFID:" + ProductID + "$$SharedUser:" + userToShare + "$$SharedCalcTime:" + System.DateTime.Now);

            }
            //}
            //else
            //{
            //    string invalidmessage = "This App is not sharable !";
            //    VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", "", invalidmessage, "");

            //    string strlogDesc = "Share request rejected from server. " + "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; userToShare : " + userToShare + "; senderEmail :" + senderEmail + "; senderMessage :" + senderMessage + "; sharerequest ";
            //    LogUtility.Info("MarketDataHub.cs", "sharerequest()", strlogDesc);
            //}
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("MarketDataHub.cs:: sharerequest :: " + errorMessage.ToString());

            string strErrorDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; userToShare : " + userToShare + "; senderEmail :" + senderEmail + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "sharerequest()", strErrorDesc, ex);
        }
    }

    public void closeimage(string ImageName, string ConnectionID, string UserID, string CustomerID, string SpecialImageID)
    {

        try
        {


            string ProductID = "0";
            if (ImageName.Contains(','))
            {

                string[] ArrImagename = ImageName.Split(',');
                foreach (var item in ArrImagename)
                {

                    if (AppsInfo.Instance._dirImgSID.ContainsKey(item))
                    {
                        ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[item]);
                        BeastConn.Instance.CloseImageBeastConn(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID);

                    }
                }
            }
            else
            {
                if (AppsInfo.Instance._dirImgSID.ContainsKey(ImageName))
                {

                    ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[ImageName]);
                    BeastConn.Instance.CloseImageBeastConn(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID);
                }
            }
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            //UtilityHandler.SendEmailForError("MarketDataHub.cs:: CloseImage :: " + errorMessage.ToString());
            LogUtility.Error("MarketDataHub.cs", "CloseImage()", "", ex);
        }

    }


    public void closeimageForExcel(string ConnectionID, string UserID)
    {

        try
        {

            BeastConn.Instance.CloseInitiatorImageForExcel(ConnectionID, UserID);


        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("MarketDataHub.cs:: closeimageForExcel :: " + errorMessage.ToString());
            LogUtility.Error("MarketDataHub.cs", "closeimageForExcel()", "", ex);
        }

    }
    //public void joinGroupExplicit(string name, string message)
    //{
    //    try
    //    {
    //        string ConnectionIDSignalR = "0";
    //        string ConnectionID = "0";
    //        string UserID = "0";
    //        string CustomerID = "0";
    //        string UserMode = "0";
    //        string SpecialImageID = "0";

    //        string ProductID = "0";
    //        string GroupName = "";

    //        ConnectionIDSignalR = Context.ConnectionId;

    //        string[] userInfoAry = message.Split('#');

    //        if (userInfoAry.Length > 0)
    //        {
    //            UserMode = userInfoAry[2];
    //        }

    //        if (UserMode.Trim().ToLower() == "spe")
    //        {
    //            SpecialImageID = "sp1";
    //        }
    //        else if (UserMode.Trim().ToLower() == "user")
    //        {
    //            UserID = userInfoAry[0];
    //        }
    //        else if (UserMode.Trim().ToLower() == "cust")
    //        {
    //            CustomerID = userInfoAry[1];
    //        }
    //        else if (UserMode.Trim().ToLower() == "conn")
    //        {
    //            ConnectionID = Context.ConnectionId;
    //        }

    //        Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), name);
    //        ProductID = Convert.ToString((int)eProductName);

    //        if (name == Definations.BeastImageAppID.vcm_calc_cmefuture.ToString())
    //        {
    //            if (SpecialImageID != "0")
    //            {
    //                ProductID = Convert.ToString((int)Definations.BeastImageAppID.vcm_calc_cmefuture);
    //                SpecialImageID = "BI_CMEFuture1";
    //                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
    //                JoinGroup(GroupName);
    //                VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR);

    //                SpecialImageID = "BI_CMEFuture2";
    //                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
    //                JoinGroup(GroupName);
    //                VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR);

    //                SpecialImageID = "BI_CMEFuture3";
    //                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
    //                JoinGroup(GroupName);
    //                VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR);

    //                SpecialImageID = "BI_CMEFuture4";
    //                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
    //                JoinGroup(GroupName);
    //                VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR);
    //            }
    //            else
    //            {
    //                ProductID = Convert.ToString((int)Definations.BeastImageAppID.vcm_calc_cmefuture);
    //                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
    //                JoinGroup(GroupName);
    //                VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR);
    //            }
    //        }
    //        else
    //        {
    //            GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
    //            JoinGroup(GroupName);
    //            VCMComet.Instance.connectToBeastImage(GroupName, ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, ConnectionIDSignalR);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
    //        UtilityHandler.SendEmailForError("MarketDataHub.cs:: Send :: " + errorMessage.ToString());
    //    }

    //}

    public void unJoinGroupExplicit(string name, string message, string GUID, string ClientType)
    {
        string ConnectionIDSignalR = "0";
        string ConnectionID = "0";
        string UserID = "0";
        string CustomerID = "0";
        string UserMode = "0";
        string SpecialImageID = "0";

        string ProductID = "0";
        string GroupName = "";
        string[] ValidToken = null;
        try
        {

            ConnectionIDSignalR = Context.ConnectionId;
            string[] userInfoAry = message.Split('#');

            if (userInfoAry.Length > 0)
            {
                UserMode = userInfoAry[2];
            }

            if (UserMode.Trim().ToLower() == "spe")
            {
                SpecialImageID = "sp1";
            }
            else if (UserMode.Trim().ToLower() == "user")
            {
                UserID = userInfoAry[0];
            }
            else if (UserMode.Trim().ToLower() == "cust")
            {
                CustomerID = userInfoAry[1];
            }
            else if (UserMode.Trim().ToLower() == "conn")
            {
                ConnectionID = ConnectionIDSignalR;
            }
            ValidToken = VCMComet.Instance.ValidateAuthToken(userInfoAry[0], ClientType, GUID);


            if (ValidToken[0] == "False")
            {
                string eleID = "unJoinGroupExplicit()^ConnectionID:" + ConnectionID + "^UserID:" + UserID + "^CustomerID:" + CustomerID + "^ConnectionIDSignalR:" + ConnectionIDSignalR + "^ClientType:" + ClientType + "^UserMode:" + UserMode + "^CalcName:" + name;

                VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", eleID, ValidToken[1], "AuthInvalid");

                return;
            }
            //Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), name);
            string eProductName = name;
            ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]);

            if (name == "vcm_calc_cmefuture")
            {
                if (SpecialImageID != "0")
                {
                    ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_cmefuture"]);

                    SpecialImageID = "BI_CMEFuture1";
                    GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                    UnJoinGroup(GroupName, ConnectionIDSignalR);

                    SpecialImageID = "BI_CMEFuture2";
                    GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                    UnJoinGroup(GroupName, ConnectionIDSignalR);

                    SpecialImageID = "BI_CMEFuture3";
                    GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                    UnJoinGroup(GroupName, ConnectionIDSignalR);

                    SpecialImageID = "BI_CMEFuture4";
                    GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                    UnJoinGroup(GroupName, ConnectionIDSignalR);
                }
                else
                {
                    ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID["vcm_calc_cmefuture"]);
                    GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                    UnJoinGroup(GroupName, ConnectionIDSignalR);
                }
            }
            else
            {
                GroupName = createGroupName(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, UserMode);
                UnJoinGroup(GroupName, ConnectionIDSignalR);
            }
            //   VCMComet.Instance.SetImageClosingTime(userInfoAry[0], GUID, ConnectionIDSignalR, ProductID);

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; ConnectionIDSignalR :" + ConnectionIDSignalR + "; SpecialImageID :" + SpecialImageID + "; unJoinGroupExplicit ";
            LogUtility.Debug("MarketDataHub.cs", "unJoinGroupExplicit()", strlogDesc);
            LogUtility.Info("MarketDataHub.cs", "unJoinGroupExplicit()", "$$UserID:" + userInfoAry[0] + "$$Token:" + GUID + "$$ClientType:" + ClientType + "$$ConnectionID:" + ConnectionIDSignalR + "$$ImageName:" + eProductName + "$$ImageSIFID:" + ProductID + "$$ImageCloseTime:" + System.DateTime.Now);
            closeimage(eProductName, ConnectionID, UserID, CustomerID, SpecialImageID);
        }
        catch (Exception ex)
        {
            string errorMessage = ex.Message + "</br>" + ex.Source + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("MarketDataHub.cs:: unJoinGroupExplicit :: " + errorMessage.ToString());
            string strErrorDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; ConnectionIDSignalR :" + ConnectionIDSignalR + "; SpecialImageID :" + SpecialImageID + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "unJoinGroupExplicit()", strErrorDesc, ex);
        }

    }

    public void sendbeast(string name, string message, string GUID, string ClientType)
    {
        string ConnectionID = "0";
        string UserID = "0";
        string CustomerID = "0";
        string UserMode = "0";
        string SpecialImageID = "0";
        string ProductID = "0";

        string paraValues = name.Split('#')[1];
        string paraName = name.Split('#')[0];
        string InstanceID = "";
        string connectionIDFromShareReq = "";
        string ConnectionIDSignalR = "0";
        string actualCustID = "0";

        string eProductName = "";
        string username = "";

        try
        {
            ConnectionIDSignalR = Context.ConnectionId;

            string[] userInfoAry = paraValues.Split('^');

            if (userInfoAry.Length > 0)
            {
                actualCustID = userInfoAry[0];
                if (userInfoAry[2].Split('~').Length > 1)
                {
                    UserMode = userInfoAry[2].Split('~')[0];
                    InstanceID = userInfoAry[2].Split('~')[1];
                    connectionIDFromShareReq = userInfoAry[2].Split('~')[2];
                }
                else
                {
                    UserMode = userInfoAry[2];
                }
            }

            if (UserMode.Trim().ToLower() == "spe")
            {
                SpecialImageID = "sp1";
            }
            else if (UserMode.Trim().ToLower() == "user")
            {
                UserID = userInfoAry[0];
            }
            else if (UserMode.Trim().ToLower() == "cust")
            {
                CustomerID = userInfoAry[1];
            }
            else if (UserMode.Trim().ToLower() == "conn")
            {
                if (connectionIDFromShareReq == "")
                    ConnectionID = Context.ConnectionId;
                else
                    ConnectionID = connectionIDFromShareReq;
            }
            string[] ValidToken = null;
            string SenderUserID = "";
            if (userInfoAry[2].Split('~').Length > 3)
            {
                SenderUserID = userInfoAry[2].Split('~')[3];
            }
            else
            {
                SenderUserID = actualCustID;
            }
            if (ClientType.Contains("!"))
            {
                username = ClientType.Split('!')[1];
                ClientType = ClientType.Split('!')[0];
            }
            if (userInfoAry[2].Split('~').Length >= 4)
            {
                username = userInfoAry[2].Split('~')[4];
            }
            ValidToken = VCMComet.Instance.ValidateAuthToken(SenderUserID, ClientType, GUID);
            eProductName = paraName;
            ProductID = Convert.ToString(AppsInfo.Instance._dirImgSID[eProductName]);

            if (ValidToken[0] == "False")
            {
                string eleID = "sendbeast()^ConnectionID:" + ConnectionID + "^UserID:" + UserID + "^CustomerID:" + CustomerID + "^ConnectionIDSignalR:" + ConnectionIDSignalR + "^ClientType:" + ClientType + "^UserMode:" + UserMode + "^CalcName:" + paraName + "^$ImageName:" + eProductName + "^ImageSIFID:" + ProductID + "^senderDet:" + message.Split('#')[0] + "^parentId:" + message.Split('#')[1] + "^Value:" + message.Split('#')[2];

                VCMComet.Instance.Send_Message_To_Client_Connection_Generic(ConnectionIDSignalR, "alrt", "m", eleID, ValidToken[1], "AuthInvalid");

                return;
            }

            LogUtility.Info("MarketDataHub.cs", "sendbeast()", "$$UserID:" + SenderUserID + ":" + (int)OpenBeast.Utilities.SysLogEnum.CHANGECALCVALUE + " $$Token:" + GUID + "$$ClientType:" + ClientType + "$$ConnectionID:" + ConnectionIDSignalR + "$$ImageName:" + eProductName + "$$ImageSIFID:" + ProductID + "$$senderDet:" + message.Split('#')[0] + "$$parentId:" + message.Split('#')[1] + "$$Value:" + message.Split('#')[2] + "$$SendValueTime:" + System.DateTime.Now);
            //Definations.BeastImageAppID eProductName = (Definations.BeastImageAppID)Enum.Parse(typeof(Definations.BeastImageAppID), paraName);
            if (eProductName == "vcm_calc_ChatApp")
            {


                message = message + "|" + "(" + String.Format("{0:00}", System.DateTime.UtcNow.Hour) + ":" + String.Format("{0:00}", System.DateTime.UtcNow.Minute) + ")";
            }
            VCMComet.Instance.setValueInBeastImage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, paraName, message, eProductName.ToString(), username);
            //  VCMComet.Instance.SetImageLastActivityTime(SenderUserID, GUID, ConnectionIDSignalR, ProductID);

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; paraName" + paraName + "; sendbeast ";
            LogUtility.Debug("MarketDataHub.cs", "sendbeast()", strlogDesc);
        }
        catch (Exception ex)
        {
            string errorMessage = Convert.ToString(ex.Message) + "</br>" + ex.Source;
            UtilityHandler.SendEmailForError("MarketDataHub.cs:: sendbeast :: App=" + eProductName + " Error=" + errorMessage);
            string strErrorDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; paraName" + paraName + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "sendbeast()", strErrorDesc, ex);
        }
    }

    private string createGroupName(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID, string UserMode)
    {
        string grpName = "";
        try
        {
            grpName = ProductID + "_" + ConnectionID + "_" + UserID + "_" + CustomerID + "_" + SpecialImageID;

            string strlogDesc = "ProductId: " + ProductID + "; UserId: " + UserID + "; CustomerId: " + CustomerID + "; ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; createGroupName ";
            LogUtility.Info("MarketDataHub.cs", "createGroupName()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "ProductId: " + ProductID + "; " + "UserId: " + UserID + "; " + "CustomerId: " + CustomerID + "; " + "ConnectionId: " + ConnectionID + "; UserMod : " + UserMode + "; SpecialImageID :" + SpecialImageID + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "createGroupName()", strErrorDesc, ex);
        }

        return grpName;
    }

    //private void JoinGroup(string groupName)
    //{
    //    Groups.Add(Context.ConnectionId, groupName);
    //}

    public void JoinGroup(string groupName)
    {
        try
        {
            Groups.Add(Context.ConnectionId, groupName);

            string strlogDesc = "GroupName: " + groupName + "; ConnectionId: " + Context.ConnectionId + "; JoinGroup ";
            LogUtility.Info("MarketDataHub.cs", "JoinGroup()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "GroupName: " + groupName + "; " + "ConnectionId: " + Context.ConnectionId + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "JoinGroup()", strErrorDesc, ex);
        }
    }

    public void UnJoinGroup(string groupName, string ConnectionID)
    {
        try
        {
            Groups.Remove(ConnectionID, groupName);

            string strlogDesc = "GroupName: " + groupName + "; ConnectionId: " + ConnectionID + "; UnJoinGroup ";
            LogUtility.Info("MarketDataHub.cs", "UnJoinGroup()", strlogDesc);
        }
        catch (Exception ex)
        {
            string strErrorDesc = "GroupName: " + groupName + "; " + "ConnectionId: " + ConnectionID + "; " + ex.Message;
            LogUtility.Error("MarketDataHub.cs", "UnJoinGroup()", strErrorDesc, ex);
        }
    }

    public void Receive(string name, string message)
    {
        try
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.handleIncomingMessageFromBeastSignalR("premdata", message);

            string strlogDesc = "name: " + name + "; message: " + message + "; Receive ";
            LogUtility.Info("MarketDataHub.cs", "Receive()", strlogDesc);
        }
        catch (Exception ex)
        {
            LogUtility.Error("MarketDataHub.cs", "Receive()", "Name: " + name + "; " + "Message: " + message + "; " + ex.Message, ex);
        }
    }

    public override Task OnConnected()
    {
        string ConnectionID = Context.ConnectionId;
        return null;
    }

    public override Task OnDisconnected(bool bDisconnectType)
    {
        //bDisconnectType = True : indicates whether it was due to a timeout.
        //bDisconnectType = False : indicates whether it was NOT due to a timeout.
        try
        {

            TaskScheduler.UnobservedTaskException += (object sender, UnobservedTaskExceptionEventArgs excArgs) =>
            {
                excArgs.SetObserved();
            };

            string ConnectionID = Context.ConnectionId;

            //  VCMComet.Instance.deleteBeastImagesForConnectionID(ConnectionID);
        }
        catch (Exception ex)
        {
            LogUtility.Error("MarketDataHub.cs", "OnDisconnected()", "ConnectionId: " + Context.ConnectionId + "; " + ex.Message, ex);
        }

        return null;
    }

    #region LoadTest
    public void loadtesthub(string name, string message)
    {
        try
        {
            if (name == "joinloadtest")
            {
                Clients.Group("loadtestadmin").LoadTestAdmin(name, message);
            }
            else if (name == "loadtestadmin")
            {
                JoinGroup(name);
            }
            else
            {
                Clients.Group("loadtestadmin").LoadTestAdmin(name, message);
            }

            string strlogDesc = "name: " + name + "; message: " + message + "; loadtesthub ";
            LogUtility.Info("MarketDataHub.cs", "loadtesthub()", strlogDesc);
        }
        catch (Exception ex)
        {
            LogUtility.Error("MarketDataHub.cs", "loadtesthub()", "Name: " + name + "; " + "Message: " + message + "; " + ex.Message, ex);
        }
    }
    #endregion

    //public override Task OnReconnected()
    //{
    //    string ConnectionID = Context.ConnectionId;
    //    return Clients.All.rejoined(Context.ConnectionId, DateTime.Now.ToString());
    //}

    #region FullUpdate

    public void getfullupdate(string pAppName, string pConnectionId, string pUserId, string pCustomerId, string pSpecialImageId)
    {
        string pProductId = Convert.ToString(AppsInfo.Instance._dirImgSID[pAppName]);
        BeastConn.Instance.GetFullUpdate(pProductId, pConnectionId, pUserId, pCustomerId, pSpecialImageId);
    }

    #endregion
}