using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using BeastClientPlugIn;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections;



/// <summary>
/// Summary description for util
/// </summary>
public class util
{


    public util()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    #region Incremental Update


    static string strColumnName(Int32 colIndex, string fieldTag)
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

        //if (fieldTag == "VolGrid")
        //{
        //    if (colIndex == 0)
        //        strColumnName = "1 Year";
        //    else if (colIndex == 1)
        //        strColumnName = "2 Year";
        //    else if (colIndex == 2)
        //        strColumnName = "3 Year";
        //    else if (colIndex == 3)
        //        strColumnName = "4 Year";
        //    else if (colIndex == 4)
        //        strColumnName = "5 Year";
        //    else if (colIndex == 5)
        //        strColumnName = "6 Year";
        //    else if (colIndex == 6)
        //        strColumnName = "7 Year";
        //    else if (colIndex == 7)
        //        strColumnName = "8 Year";
        //    else if (colIndex == 8)
        //        strColumnName = "9 Year";
        //    else if (colIndex == 9)
        //        strColumnName = "10 Year";
        //    else if (colIndex == 10)
        //        strColumnName = "15 Year";
        //    else if (colIndex == 11)
        //        strColumnName = "20 Year";
        //    else if (colIndex == 12)
        //        strColumnName = "25 Year";
        //    else if (colIndex == 13)
        //        strColumnName = "30 Year";
        //}
        //else if (fieldTag == "StrikeGrid")
        //{
            
        //}
        
        return strColumnName;
    }

    public static bool GetRowColGridField(string name, out int row, out int col)
    {
        int rowPos, colPos;
        string rowStr, colStr;
        row = -1;
        col = -1;
        rowPos = name.IndexOf('R');
        if (rowPos != -1)
        {
            colPos = name.IndexOf('C');
            if (colPos != -1)
            {
                rowStr = name.Substring(rowPos + 1, colPos - rowPos - 1);
                colStr = name.Substring(colPos + 1);
                row = Convert.ToInt32(rowStr);
                col = Convert.ToInt32(colStr);
                // Row and column indices "-1" are reserved for column and row headings.
                return row >= 0 && col >= 0;
            }
        }
        return false;
    }


    static public void updateDataTableFromGridField(ref DataTable dt, string fieldName, DOMDataNode node, string fieldTag, ref DataTable dtNodeIDs)
    {

        try
        {
            if (dt == null)
                dt = new DataTable();

            if (dtNodeIDs == null)
                dtNodeIDs = new DataTable();

            string nodeId = node.NodeID;
            if (nodeId.StartsWith("G" + fieldName))
            {
                int row, col;
                if (util.GetRowColGridField(nodeId, out row, out col))
                {
                    if (col == -1)
                        return;
                    while (col >= dt.Columns.Count)// && node.DataState != DOMDataNodeValueState.DATANODEVALUESTATE_BLANK)
                    {
                        dt.Columns.Add(Convert.ToString(strColumnName(col, fieldTag)), typeof(string));
                        dtNodeIDs.Columns.Add(Convert.ToString(strColumnName(col, fieldTag)), typeof(string));
                    }

                    while (row >= dt.Rows.Count)
                    {
                        dt.Rows.Add(dt.NewRow());
                        dtNodeIDs.Rows.Add(dtNodeIDs.NewRow());
                    }
                    

                    if (row == -1)
                    {
                        //if (node.DataState == DOMDataNodeValueState.DATANODEVALUESTATE_BLANK)
                        //    dt.Columns[col].ColumnName = col.ToString();
                        //else
                            dt.Columns[col].ColumnName = node.DataValue.ToString();
                            dtNodeIDs.Columns[col].ColumnName = node.DataValue.ToString();
                    }
                    if (col != -1 && row != -1)
                    {
                        dt.Rows[row][col] = node.DataValue;
                        dtNodeIDs.Rows[row][col] = node.NodeID;

                        VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                        if (fieldTag == "VolGrid")
                        {
                            cometVOrder.sendVolGridDataResponse(row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        }
                        else if (fieldTag == "PremGrid")
                        {
                            cometVOrder.sendPremGridDataResponse(row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        }
                        else if (fieldTag == "StrikeGrid")
                        {
                            cometVOrder.sendStrikeGridDataResponse(row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                        }
                    }                                     
                }
            }
            else if (fieldTag == "Other")
            {
                VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                if (Convert.ToString(nodeId) == "1")
                {
                    string currencyVal = CurrencyValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.Currency = currencyVal;
                    beastVolPrimGrid.Instance.CurrencyDataValue = Convert.ToString(node.DataValue);
                    cometVOrder.sendOtherDataResponse("Currency", "Currency", currencyVal);
                }
                else if (Convert.ToString(nodeId) == "100")
                {
                    string straddleVal = StraddleValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.Straddle = straddleVal;
                    cometVOrder.sendOtherDataResponse("Currency", "Straddle", straddleVal);
                }
                else if (Convert.ToString(nodeId) == "52")
                {
                    string Val = VolValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.Vols = Val;
                    cometVOrder.sendOtherDataResponse("Vols", "Vols", Val);
                }
                else if (Convert.ToString(nodeId) == "333")
                {
                    string Val = VolShiftValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.VolShift = Val;
                    cometVOrder.sendOtherDataResponse("VolShift", "VolShift", Val);
                }
                else if (Convert.ToString(nodeId) == "4")
                {
                    //string Val = CurrencyThirdColumn(Convert.ToInt32(node.DataValue));
                    //beastVolPrimGrid.Instance.List = Val;
                    //cometVOrder.sendOtherDataResponse("Currency", "List", Val);
                }
                else if (Convert.ToString(nodeId) == "6")
                {
                    string Val = PremFPremValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.PremGridTitle = Val;
                    cometVOrder.sendOtherDataResponse("PremGridTitle", "Title", Val);
                }
                else if (Convert.ToString(nodeId) == "100000031")
                {
                    if (node.DataValue != "")
                    {
                        //string Val = CurrencyThirdColumn(Convert.ToInt32(node.DataValue));
                        string Val = node.DataValue.ToString();
                        beastVolPrimGrid.Instance.List = Val;
                        cometVOrder.sendOtherDataResponse("Currency", "List", Val);
                    }
                    
                }
            }

        }
        catch
        {
            throw;
        }
    }

    static public string returnDataTableFromGridField(ref DataTable dt, string fieldName, DOMDataNode node, string fieldTag)
    {

        try
        {
            if (dt == null)
                dt = new DataTable();

            string nodeId = node.NodeID;
            if (nodeId.StartsWith("G" + fieldName))
            {
                int row, col;
                if (util.GetRowColGridField(nodeId, out row, out col))
                {
                    if (col == -1)
                        return "";
                    while (col >= dt.Columns.Count)// && node.DataState != DOMDataNodeValueState.DATANODEVALUESTATE_BLANK)
                        dt.Columns.Add(Convert.ToString(strColumnName(col, fieldTag)), typeof(string));
                    while (row >= dt.Rows.Count)
                        dt.Rows.Add(dt.NewRow());

                    if (row == -1)
                    {
                        //if (node.DataState == DOMDataNodeValueState.DATANODEVALUESTATE_BLANK)
                        //    dt.Columns[col].ColumnName = col.ToString();
                        //else
                        dt.Columns[col].ColumnName = node.DataValue.ToString();
                    }
                    if (col != -1 && row != -1)
                    {
                        dt.Rows[row][col] = node.DataValue;

                        VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                        if (fieldTag == "VolGrid")
                        {
                            //cometVOrder.sendVolGridDataResponse(row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                            return row.ToString() + "#" + col.ToString() + "#" + dt.Rows[row][col].ToString();
                        }
                        else if (fieldTag == "PremGrid")
                        {
                            //cometVOrder.sendPremGridDataResponse(row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                            return row.ToString() + "#" + col.ToString() + "#" + dt.Rows[row][col].ToString();
                        }
                        else if (fieldTag == "StrikeGrid")
                        {
                            //cometVOrder.sendStrikeGridDataResponse(row.ToString(), col.ToString(), dt.Rows[row][col].ToString());
                            return row.ToString() + "#" + col.ToString() + "#" + dt.Rows[row][col].ToString();
                        }
                    }
                }
            }
            else if (fieldTag == "Other")
            {
                VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();
                if (Convert.ToString(nodeId) == "1")
                {
                    string currencyVal = CurrencyValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.Currency = currencyVal;
                    cometVOrder.sendOtherDataResponse("Currency", "Currency", currencyVal);
                }
                else if (Convert.ToString(nodeId) == "100")
                {
                    string straddleVal = StraddleValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.Straddle = straddleVal;
                    cometVOrder.sendOtherDataResponse("Currency", "Straddle", straddleVal);
                }
                else if (Convert.ToString(nodeId) == "52")
                {
                    string Val = VolValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.Vols = Val;
                    cometVOrder.sendOtherDataResponse("Vols", "Vols", Val);
                }
                else if (Convert.ToString(nodeId) == "333")
                {
                    string Val = VolShiftValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.VolShift = Val;
                    cometVOrder.sendOtherDataResponse("VolShift", "VolShift", Val);
                }
                else if (Convert.ToString(nodeId) == "4")
                {
                    string Val = CurrencyThirdColumn(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.List = Val;
                    cometVOrder.sendOtherDataResponse("Currency", "List", Val);
                }
                else if (Convert.ToString(nodeId) == "6")
                {
                    string Val = PremFPremValue(Convert.ToInt32(node.DataValue));
                    beastVolPrimGrid.Instance.List = Val;
                    cometVOrder.sendOtherDataResponse("PremGrid", "Title", Val);
                }

            }

        }
        catch
        {
            throw;
        }
        return "";
    }


    #endregion

    #region appendedFun

    public static string StraddleValue(int id)
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

    public static string PremFPremValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 0:
                rtnValue = "Prem";
                break;
            case 1:
                rtnValue = "F.Prem";
                break;
            default:
                break;
        }

        return rtnValue;
    }

    public static string VolValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 0:
                rtnValue = "Local";
                break;
            case 1:
                rtnValue = "Global";
                break;
            default:
                break;
        }

        return rtnValue;
    }

    public static string CurrencyThirdColumn(int id)
    {
        string rtnValue = "";

        int currentCurrencyID = Convert.ToInt32(beastVolPrimGrid.Instance.CurrencyDataValue);

        if (currentCurrencyID == 21)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 100962037:
                    rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                    break;
                case 61:
                    rtnValue = "Internal:Manually Maintained Data";
                    break;
                case 40970501:
                    rtnValue = "TTL-B:Tullett & Tokyo Liberty Swaption Vol [Bridge]";
                    break;
                case 163850501:
                    rtnValue = "TTL-B:Tullett & Tokyo Liberty Swaption Vol [Bridge]";
                    break;
                case 10501:
                    rtnValue = "TTL-B:Tullett & Tokyo Liberty Swaption Vol [Bridge]";
                    break;
                case 20501:
                    rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 86930501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                    break;
                case 30501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [Telerate]";
                    break;
                case 100962038:
                    rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                    break;
                case 100960600:
                    rtnValue = "VCM-B:VCM Swaption Vol [BEAST2]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 221)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - Internal:Manually Maintained Data";
                    break;
                case 61:
                    rtnValue = "Internal - Pref:Manually Maintained Data";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 522)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 100960007:
                    rtnValue = "Bloom-V:Bloomberg Swaption Vol [VCM]";
                    break;
                case 100962037:
                    rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                    break;
                case 22036:
                    rtnValue = "Eonia  Swaption Vol:ICPL Eonia  Swaption Vol [Reuters]";
                    break;
                case 61:
                    rtnValue = "Internal:Manually Maintained Data";
                    break;
                case 20501:
                    rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 86930501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                    break;
                case 100962038:
                    rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 215)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 61:
                    rtnValue = "Internal - Pref:Manually Maintained Data";
                    break;
                case 40970504:
                    rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                    break;
                case 163850504:
                    rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 15)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 61:
                    rtnValue = "Internal :Manually Maintained Data";
                    break;
                case 40970504:
                    rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                    break;
                case 163850504:
                    rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                    break;
                case 20501:
                    rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 86930501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 11)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 100960007:
                    rtnValue = "Bloom-V:Bloomberg Swaption Vol [VCM]";
                    break;
                case 100962037:
                    rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                    break;
                case 22036:
                    rtnValue = "Eonia  Swaption Vol:ICPL Eonia  Swaption Vol [Reuters]";
                    break;
                case 61:
                    rtnValue = "Internal:Manually Maintained Data";
                    break;
                case 40970504:
                    rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                    break;
                case 163850504:
                    rtnValue = "Prebo-B:Prebon Swaption Vol [Bridge]";
                    break;
                case 20501:
                    rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 86930501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                    break;
                case 100962038:
                    rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 4)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 100960007:
                    rtnValue = "Bloom-V:Bloomberg Swaption Vol [VCM]";
                    break;
                case 100962037:
                    rtnValue = "CMPN-V:CMPN Swaption Vol [VCM]";
                    break;
                case 61:
                    rtnValue = "Internal:Manually Maintained Data";
                    break;
                case 100962038:
                    rtnValue = "Trad-V:Trad Swaption Vol [VCM]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 807 || currentCurrencyID == 917)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - Internal:Manually Maintained Data";
                    break;
                case 61:
                    rtnValue = "Internal :Manually Maintained Data";
                    break;
                case 86930501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 1020)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - TTL-R:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 61:
                    rtnValue = "Internal :Manually Maintained Data";
                    break;
                case 20501:
                    rtnValue = "TTL-R - Pref:Tullett & Tokyo Liberty Swaption Vol [Reuters]";
                    break;
                case 86930501:
                    rtnValue = "TTL-T:Tullett & Tokyo Liberty Swaption Vol [TTKL]";
                    break;
                default:
                    break;
            }
        }
        else if (currentCurrencyID == 1703 || currentCurrencyID == 3 || currentCurrencyID == 2205 || currentCurrencyID == 2123 || currentCurrencyID == 324 ||currentCurrencyID == 2325 || currentCurrencyID == 2527 || currentCurrencyID == 2628 ||currentCurrencyID == 2729 ||currentCurrencyID == 2830 ||currentCurrencyID == 2931 ||currentCurrencyID == 3032 || currentCurrencyID == 3133 || currentCurrencyID == 3234)
        {
            switch (id)
            {
                case 0:
                    rtnValue = "Pref - Internal:Manually Maintained Data";
                    break;
                case 61:
                    rtnValue = "Internal :Manually Maintained Data";
                    break;
                default:
                    break;
            }
        }
        

        return rtnValue;
    }

    public static string VolShiftValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 0:
                rtnValue = "Off";
                break;
            case 1:
                rtnValue = "Percentage";
                break;
            case 2:
                rtnValue = "Parallel";
                break;
            case 3:
                rtnValue = "DK";
                break;
            default:
                break;
        }

        return rtnValue;
    }

    public static string CurrencyValue(int id)
    {
        string rtnValue = "";

        switch (id)
        {
            case 21:
                rtnValue = "USD.EU :US (London) Eurodollar LIBOR Market";
                break;
            case 221:
                rtnValue = "USD.TK :US (Tokyo) TIBOR Market";
                break;
            case 6:
                rtnValue = "DEM.LN :Germany (London) LIBOR Market";
                break;
            case 406:
                rtnValue = "DEM  :Germany (Frankfurt) Domestic/FIBOR Market";
                break;
            case 22:
                rtnValue = "EUR.LN  :Euro (London) LIBOR Market";
                break;
            case 522:
                rtnValue = "EUR  :Euro EURIBOR Market";
                break;
            case 215:
                rtnValue = "JPY.TK  :Japan (Tokyo) Domestic/TIBOR Market";
                break;
            case 15:
                rtnValue = "JPY.EU  :Japan (London) LIBOR Market";
                break;
            case 1703:
                rtnValue = "CAD.DM  :Canada (Toronto) Domestic/BA Market";
                break;
            case 3:
                rtnValue = "CAD.EU  :Canada (London) LIBOR Market";
                break;
            case 11:
                rtnValue = "GBP :UK (London) Domestic/LIBOR Market";
                break;
            case 1404:
                rtnValue = "CHF.ZU  :Switzerland (Zurich) Domestic Market";
                break;
            case 4:
                rtnValue = "CHF.EU  :Switzerland (London) LIBOR Market";
                break;
            case 14:
                rtnValue = "ITL.LN  :Italy (London) LIBOR Market";
                break;
            case 2205:
                rtnValue = "CZK :Czech (Prague) Domestic/PRIBOR Market";
                break;
            case 807:
                rtnValue = "DKK :Denmark (Copenhagen) Domestic/CIBOR Market";
                break;
            case 917:
                rtnValue = "NOK :Norway (Oslo) Domestic/NIBOR(OIBOR) Market";
                break;
            case 1020:
                rtnValue = "SEK :Sweden (Stockholm) Domestic/STIBOR Market";
                break;
            case 2123:
                rtnValue = "ZAR :South Africa (Johannesburg) Domestic/BA Market";
                break;
            case 324:
                rtnValue = "SGD :Singapore (Singapore) Domestic/SIBOR Market";
                break;
            case 2325:
                rtnValue = "MXN :Mexico (Mexico City) Domestic Market";
                break;
            case 2527:
                rtnValue = "ARP :Argentina (Buenos Aires) Domestic/BAIBOR Market";
                break;
            case 2628:
                rtnValue = "TRL :Turkey (Ankara) Domestic Market";
                break;
            case 2729:
                rtnValue = "EEK :Estonia (Tallin) Domestic/TALIBOR Market";
                break;
            case 2830:
                rtnValue = "GRD :Greece (Athens) Domestic/ATHIBOR Market";
                break;
            case 2931:
                rtnValue = "HUF :Hungary (Budapest) Domestic/BUBOR Market";
                break;
            case 3032:
                rtnValue = "PLN :Poland (Warzawa) Domestic/WIBOR Market";
                break;
            case 3133:
                rtnValue = "RUB :Russia (Moskva) Domestic/MOWIBOR Market";
                break;
            case 3234:
                rtnValue = "BRL :Brazil (Brasilia) Domestic Market";
                break;
            default:
                break;

        }
        return rtnValue;
    }
    #endregion



}


