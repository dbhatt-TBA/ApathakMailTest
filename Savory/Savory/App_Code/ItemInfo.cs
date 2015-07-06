using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

public class ItemInfo
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public int Qty { get; set; }
    public double Price { get; set; }
    public double Total { get; set; }
    //  public static List<ItemInfo> list;

    //public ItemInfo()
    //{
    //}

    //public List<ItemInfo> AddItem(int itemId, string itemName, double qty)
    //{
    //    ItemInfo obj = new ItemInfo();
    //    obj.ItemId = itemId;
    //    obj.ItemName = itemName;
    //    obj.Qty = qty;
    //    list.Add(obj);
    //    return list;
    //}

    
}