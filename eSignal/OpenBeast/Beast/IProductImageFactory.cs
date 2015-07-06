using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IProductImageFactory
/// </summary>
public interface IProductImageFactory
{
    IProductImage createProductImage(string ProductID, string ConnectionID, string UserID, string CustomerID, string SpecialImageID, string ActualProductID, bool IsSharedImage = false, string InstanceID = "", string username = "");
}