using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using VCM.Common.Log;

public class AppsInfo
{
    public Dictionary<string, int> _dirImgSID;
    public DataTable _dtAppsInfo;

    public enum Properties
    {
        AppId = 1,
        RegId = 2,
        SIF_Id = 3,
        AppName = 4,
        IsGridImage = 5,
        IsSharable = 6,
        ShareExpirationTime = 7
    }

    public enum APPSINFO_RESULTS         //to handle false results returned by the class
    {        
        NORECORDSFOUND = -1,
        EMPTYDEFAULT = -9876
    }

    private static volatile AppsInfo _instance = null;
    private static object syncRoot = new Object();

    public AppsInfo()
    {
        _dirImgSID = new Dictionary<string, int>();

        _dtAppsInfo = new DataTable();
        _dtAppsInfo.Columns.Add("App_Id");
        _dtAppsInfo.Columns.Add("Reg_Id");
        _dtAppsInfo.Columns.Add("SIF_Id");
        _dtAppsInfo.Columns.Add("Name");
        _dtAppsInfo.Columns.Add("IsGridImage");
        _dtAppsInfo.Columns.Add("IsSharable");
        _dtAppsInfo.Columns.Add("ShareExpirationTime");
    }

    public static AppsInfo Instance
    {
        get
        {
            lock (syncRoot)
            {
                if (_instance == null)
                {
                    _instance = new AppsInfo();
                }
            }
            return _instance;
        }
    }

    private string GetColumnName(Properties colId)
    {
        string _TargetCol = string.Empty;
        switch (colId)
        {
            case Properties.AppId:
                _TargetCol = "App_Id";
                break;

            case Properties.RegId:
                _TargetCol = "Reg_Id";
                break;

            case Properties.SIF_Id:
                _TargetCol = "SIF_Id";
                break;

            case Properties.AppName:
                _TargetCol = "Name";
                break;

            case Properties.IsGridImage:
                _TargetCol = "IsGridImage";
                break;

            case Properties.IsSharable:
                _TargetCol = "IsSharable";
                break;

            case Properties.ShareExpirationTime:
                _TargetCol = "ShareExpirationTime";
                break;
        }

        return _TargetCol;
    }

    public string GetPropertyInfo(Properties propToGet, Properties propForFilter, string filterPropValue)
    {
        string retValue = Convert.ToString((int)APPSINFO_RESULTS.EMPTYDEFAULT);

        string _getValue_colName = string.Empty;
        string _filterValue_colName = string.Empty;

        DataView dvResult = new DataView(_dtAppsInfo);

        try
        {
            _getValue_colName = GetColumnName(propToGet);
            _filterValue_colName = GetColumnName(propForFilter);

            if (_getValue_colName != _filterValue_colName)
            {
                dvResult.RowFilter = _filterValue_colName + " = " + filterPropValue;
            }
            else
            {
                //code to handle if developer has called this method using same params - propToGet ~ propForFilter
            }

            if (dvResult.Count > 0)
                retValue = Convert.ToString(dvResult[0][_getValue_colName]);
            else
                retValue = Convert.ToString((int)APPSINFO_RESULTS.NORECORDSFOUND); 
           
        }
        catch (Exception ex)
        {
            dvResult = null;
            string methodParams = _getValue_colName + "," + _filterValue_colName + "," + filterPropValue;
            LogUtility.Error("AppsInfo.cs", "GetPropertyInfo()", "Error reading App property. Params:" + methodParams, ex);
            throw ex;   //comment this after testing
        }

        dvResult = null;

        return retValue;
    }

    public enum NodeDataStatus
    {
        DATANODEVALUESTATE_NORMAL = 0,
        DATANODEVALUESTATE_BLANK = 1,
        DATANODEVALUESTATE_NA = 2,
        DATANODEVALUESTATE_ERROR = 3,
    }

    public enum BeastImageHTMLClientID
    {
        vcm_calc_swaptionVolPremStrike = 451,
        vcm_calc_bondYield = 152,
        tblCMEFuture = 852
    }
}
