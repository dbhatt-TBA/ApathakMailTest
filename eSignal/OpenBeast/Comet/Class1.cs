using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;

namespace SwaptionVolPrimGrid
{
    public class Class1
    {
        public static GridView volPrimGrid = new GridView();

        private static Class1 _instance = new Class1();
        //private static DataSet ds;
        //Random _r = new Random();

        VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();

        public static Class1 Instance()
        {
            return _instance;
        }

        //public DataTable GetdtVolGrid()
        //{
        //    SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage("451", "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
        //    return bi.dtVolGrid;
        //}

        //public DataTable GetdtPremGrid()
        //{
        //    SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage("451", "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
        //    return bi.dtPremGrid;
        //}


        //public DataTable GetdtStrikeGrid()
        //{
        //    SwaptionVolPrem bi = (SwaptionVolPrem)BeastConn.Instance.getBeastImage("451", "0", "0", "0", "sp1", "vcm_calc_swaptionVolPremStrike");
        //    return bi.dtStrikeGrid;
        //}

        private Class1()
        {
            //ds = new DataSet();

            //DataTable dt = new DataTable();
            //dt = GetdtVolGrid();

            //volPrimGrid.DataSource = dt;
            //volPrimGrid.DataBind();
        }

        //public void loadDS()
        //{
        //    //BeastImg bi = (BeastImg)BeastConn.Instance.getBeastImage("G100");
        //    //DataTable dt = new DataTable();
        //    //dt = bi.dtPremGrid;

        //    //if (volPrimGrid.DataSource == null)
        //    //{
        //    //    volPrimGrid.DataSource = dt;
        //    //    volPrimGrid.DataBind();
        //    //}
        //}

        //static int NextFloat(Random random)
        //{
        //    //double mantissa = (random.NextDouble() * 2.0) - 1.0;
        //    //double exponent = Math.Pow(2.0, random.Next(-126, 128));
        //    //return (float)(mantissa * exponent);
        //    return random.Next(1, 50);
        //}

        //public int NextFloatPub()
        //{
        //    //double mantissa = (random.NextDouble() * 2.0) - 1.0;
        //    //double exponent = Math.Pow(2.0, random.Next(-126, 128));
        //    //return (float)(mantissa * exponent);
        //    return _r.Next(1, 50);
        //}

        //public void changeValue(int row, int col, string value)
        //{
        //    volPrimGrid.Rows[row].Cells[col].Text = Convert.ToString(value);
        //    cometVOrder.sendVolGridDataResponse(row.ToString(), col.ToString(), value.ToString());
        //}

        //public int NextRow(Random random)
        //{
        //    int row = random.Next(1, 50);

        //    if (row > 13)
        //    {
        //        int val = row / 13;
        //        row = row - (val * 13);
        //    }

        //    if (row >= 13)
        //        row = 12;
        //    return row;
        //}

        //public int NextCol(Random random)
        //{
        //    int col = random.Next(1, 50);

        //    if (col > 14)
        //    {
        //        int val = col / 14;
        //        col = col - (val * 14);
        //    }

        //    if (col >= 14)
        //        col = 13;
        //    return col;
        //}

        //public void updateGrid()
        //{
        //    int RandSleep = 500;

        //    for (int row = 0; row < volPrimGrid.Rows.Count; row++)
        //    {
        //        for (int coll = 0; coll < Class1.volPrimGrid.Rows[row].Cells.Count; coll++)
        //        {
        //            //cometVOrder.sendGridDataResponse(row.ToString(), coll.ToString(), Class1.volPrimGrid.Rows[row].Cells[coll].Text);
        //           // changeValue(NextRow(_r), NextCol(_r), NextFloat(_r));
        //            Thread.Sleep(RandSleep);
        //        }
        //    }
        //}
        //protected void volPrimGrid_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //protected void volPrimGrid_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        //{

        //}

        //protected void volPrimGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    //VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();

        //    //if (e.Row.RowIndex != -1)
        //    //{
        //    //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    //    {
        //    //        cometVOrder.sendGridDataResponse(e.Row.RowIndex.ToString(), i.ToString(), e.Row.Cells[i].Text);
        //    //    }
        //    //}
        //}



    }
}
