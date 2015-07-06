using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savory
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        public void BindData()
        {
            List<ItemInfo> Carts = (List<ItemInfo>)HttpContext.Current.Session["Cart"];
            DataTable dataTable = new DataTable(typeof(ItemInfo).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(ItemInfo).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (ItemInfo item in Carts)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            cartlist.DataSource = dataTable;
            cartlist.AllowSorting = true;
            CommandField cField = new CommandField();
            cartlist.DataBind();
        }

        protected void cartlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cartlist.PageIndex = e.NewPageIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void cartlist_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //Set the edit index.
            cartlist.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindData();
        }

        protected void cartlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //Reset the edit index.
            cartlist.EditIndex = -1;
            //Bind data to the GridView control.
            BindData();
        }

        protected void cartlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Update the values.
            GridViewRow row = cartlist.Rows[e.RowIndex];
            int ItemId = Convert.ToInt32(((TextBox)(row.Cells[1].Controls[0])).Text);
            string ItemName = ((TextBox)(row.Cells[2].Controls[0])).Text;
            int Qty = Convert.ToInt32(((TextBox)(row.Cells[3].Controls[0])).Text);
            string Price = ((TextBox)(row.Cells[4].Controls[0])).Text;
            string Total = ((TextBox)(row.Cells[5].Controls[0])).Text;
            cartlist.EditIndex = -1;
            BindData();
            DataTable dt = (DataTable)cartlist.DataSource;
            dt.Rows[row.DataItemIndex]["ItemId"] = ItemId;
            dt.Rows[row.DataItemIndex]["ItemName"] = ItemName;
            dt.Rows[row.DataItemIndex]["Qty"] = Qty;
            dt.Rows[row.DataItemIndex]["Price"] = Price;
            dt.Rows[row.DataItemIndex]["Total"] = (Convert.ToInt32(Qty) * Convert.ToDouble(Price)).ToString();

            //Reset the edit index.
            
            //Bind data to the GridView control.
            List<ItemInfo> list = dt.ToList<ItemInfo>();
            HttpContext.Current.Session["Cart"] = list;
            BindData();
        }
    }
}