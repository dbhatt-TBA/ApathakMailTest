using System;
using System.Collections.Generic;
using System.Web;
using BeastClientPlugIn;

/// <summary>
/// Summary description for IProductImage
/// </summary>
public interface IProductImage
{
    DOMDataDocument DocImage { get; set; }

    string UserID { get; set; }
    string ProductID { get; set; }
    string CustomerID { get; set; }
    string ConnectionID { get; set; }
    string SpecialImageID { get; set; }
    string GruopID { get; set; }

    string HtmlClientID { get; set; }

    string ActualProductID { get; set; }

    string InstanceID { get; set; }

    bool IsDocumentAlive { get; set; }
    bool IsStale { get; set; }
    bool IsSharedImage { get; set; }
    int refCount { get; set; }

    //Dictionary<string, string> specialProps;

    void createBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, ref ServerAgent _sa, string username);

    //event _IDOMDataDocumentEvents_DocumentAliveEventHandler DocumentAliveHandler;
    //event _IDOMDataDocumentEvents_DocumentChangedEventHandler DocumentChangedHandler;
    //event _IDOMDataDocumentEvents_DocumentCompleteEventHandler DocumentCompleteHandler;
    //event _IDOMDataDocumentEvents_DocumentStaleEventHandler DocumentStaleHandler;
    //event _IDOMDataDocumentEvents_ManualUpdateRequestStatusEventHandler ManualUpdateRequestStatusHandler;
    //event _IDOMDataDocumentEvents_StatusChangedEventHandler StatusChangedHandler;

    //void DocImage_StatusChanged(DOMDataDocStatus Status, string info);
    //void DocImage_ManualUpdateRequestStatus(bool Status, Scripting.Dictionary props);
    //void DocImage_DocumentStale();
    //void DocImage_DocumentComplete();
    //void DocImage_DocumentChanged(DOMDataNodeList changed);
    //void DocImage_DocumentAlive();

    void openBeastImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpeacialImageID, string ActualProductID, ref ServerAgent _sa, string ConnectionIDSignalR, string username);
     void shareBeastImage(string strProductID, string strConnectionID, string strUserID, string strCustomerID, string strSpecialImageID, string strActualProductID, string strInstanceID, ref ServerAgent _sa,string username);
    void distoryBeastImage();
}