using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Text;
using System.Reflection;
using System.IO;

namespace CurrenexDetails
{
    public class CurrenexExportDetails
    {
        private static CurrenexExportDetails _instance = new CurrenexExportDetails();
        private static string _connectionId;
        public static string ConnIdExport
        {
            get { return _connectionId; }
            set { _connectionId = value; }
        }

        VCMComet cometVOrder = VCMComet.Instance; //ServiceLocator.Current.GetInstance<VCMComet>();

        public static CurrenexExportDetails Instance()
        {
            return _instance;
        }

        public DataTable GetdtCurrenex()
        {
            ProductClassGeneric pcg = (ProductClassGeneric)BeastConn.Instance.getBeastImage("813", _connectionId, "0", "0", "0", "vcm_calc_Transactional_CurrenexDetailView_New");
            string XMLValue = pcg.ExportField;
            DataTable dt = new DataTable();

            if (!string.IsNullOrEmpty(pcg.ExportField.Trim()))
            {
                StringReader strReader = new StringReader(XMLValue);
                DataSet ds = new DataSet();
                ds.ReadXml(strReader);
                dt = ds.Tables[1];
            }
            else
            {

            }
            return dt;
        }
    }
}