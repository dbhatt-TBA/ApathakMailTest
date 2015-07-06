using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savory
{
    public partial class temp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static string add(string itemId, string itemName, string Qty,string price)
        {
            int count = 0;
            ItemInfo obj = new ItemInfo();
            obj.ItemId = Convert.ToInt32(itemId);
            obj.ItemName = itemName;
            obj.Qty = Convert.ToInt32(Qty);
            obj.Price = Convert.ToDouble(price);
            obj.Total = obj.Price * obj.Qty;
            if (HttpContext.Current.Session["Cart"] == null)
            {
                List<ItemInfo> objlist = new List<ItemInfo>();
                objlist.Add(obj);
                HttpContext.Current.Session["Cart"] = objlist;
                count=objlist.Count;
            }
            else
            {
                List<ItemInfo> objlist = (List<ItemInfo>)HttpContext.Current.Session["Cart"];
                objlist.Add(obj);
                HttpContext.Current.Session["Cart"] = objlist;
                count=objlist.Count;
            }

            // List<ItemInfo> list = obj.AddItem(Convert.ToInt32(itemId), itemName, Convert.ToDouble(Qty));
            // List<ItemInfo> list=obj.AddItem(Convert.ToInt32(itemId), itemName, Convert.ToDouble(Qty));
             JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(count);
        }
    }
}