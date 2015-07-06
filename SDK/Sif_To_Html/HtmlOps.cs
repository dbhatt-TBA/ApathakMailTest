using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web.UI;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace SIF_XML_ToHTMLUtility
{
    public partial class HtmlOps : Form
    {
        #region variables
        List<FieldNode> AllFieldNodes = new List<FieldNode>();
        List<BeastField> lstAllImageFields = new List<BeastField>();
        string[] strHtmlLines;
        string strFILENAMEinPROCESS;
        string strHTMLFILEPATH;
        bool bIsSourceSelcted = false;
        bool bIsDestinationSelected = false;

        string strSOURCEFOLDER = "";
        string strDESTFOLDER = "";

        Hashtable htTitleString = new Hashtable();

        Dictionary<string, string> oFileMap = new Dictionary<string, string>()
            {
                {"vcm_calc_Bond_Futures", "161"},
                {"vcm_calc_bondYield", "152"},
                {"vcm_calc_Cap_Floor_Calculator", "152"},
                {"vcm_calc_CapFloorVols", "134"},
                {"vcm_calc_CMCapFloor", "123"},
                {"vcm_calc_CMEDSF_vs_Bond_Future", "858"},
                {"vcm_calc_CMEDSF_vs_EuroDollar_Future_Hedge", "859"},
                {"vcm_calc_cmeotcpage", "851"},
                {"vcm_calc_Collar_Strangle_Corridor", "168"},
                {"vcm_calc_CPINSA", "9091"},
                {"vcm_calc_European_Swaption", "229"},
                {"vcm_calc_Fed_Funds_Futures", "162"},
                {"vcm_calc_FRADiffPage", "127"},
                {"vcm_calc_iimswappagemid", "215"},
                {"vcm_calc_IMM_Swap_Short", "403"},
                {"vcm_calc_IR_Futures", "101"},
                {"vcm_calc_IR_Futures_Wide", "176"},
                {"vcm_calc_irsfuture", "850"},
                {"vcm_calc_ISDAFix", "9095"},
                {"vcm_calc_MedTermSwapPage", "153"},
                {"vcm_calc_MidTermSwapPageMid", "253"},
                {"vcm_calc_Odd_FRA_Page", "102"},
                {"vcm_calc_OIS_Swap_Page", "166"},
                {"vcm_calc_Pers_Med_Term_Swap_Mid", "233"},
                {"vcm_calc_ShortTermSwapPage", "224"},
                {"vcm_calc_SimpleSwapCalculator", "194"},
                {"vcm_calc_Spread_Lock_Calculator", "200"},
                {"vcm_calc_Swap_Calculator", "105"},
                {"vcm_calc_Swap_Spread_Page", "198"},
                {"vcm_calc_Swap_Spread_Page_Mid", "217"},
                {"vcm_calc_Swap_Spread_Spread_Page", "222"},
                {"vcm_calc_SwapBondCalculator", "145"},
                {"vcm_calc_SwapDiffCalculator", "210"},
                {"vcm_calc_SwapSwitch", "171"},                
                {"vcm_calc_SwapVolPremStrike", "453"}                
            };

        #endregion

        public HtmlOps()
        {
            InitializeComponent();
        }
               

        #region ======= Events ========

        private void btnStartOperation_Click(object sender, EventArgs e)
        {
            strFILENAMEinPROCESS = lstBxAddedFiles.SelectedValue.ToString().Replace(".htm", "").Trim();

            if (oFileMap.ContainsKey(strFILENAMEinPROCESS) == false)
            {
                MessageBox.Show("No Data found for relative XML. Please update library [oFileMap Dictionary] in the code");
            }
            else
            {
                string strXmlFileName = oFileMap[strFILENAMEinPROCESS] + ".xml";

                if (File.Exists(Path.Combine(strDESTFOLDER, strXmlFileName)))
                {
                    strHTMLFILEPATH = Path.Combine(strDESTFOLDER, strFILENAMEinPROCESS + ".htm");
                    readHTMLFile(strHTMLFILEPATH);
                    readXMLFile(Path.Combine(strDESTFOLDER, strXmlFileName));
                }
                else
                {
                    string sMsg = "Related XML file - " + strXmlFileName + " is missing in : " + strDESTFOLDER + ". Please add in folder.";
                    MessageBox.Show(sMsg);
                }

                ProcessHtml();
            }
        }
        
        private void btnLoadFiles_Click(object sender, EventArgs e)
        {
            if (bIsSourceSelcted && bIsDestinationSelected)
            {
                string[] files = Directory.GetFiles(strSOURCEFOLDER, "*.htm", SearchOption.AllDirectories);
                ArrayList vFileNames = new ArrayList();

                string strSourceFile = "";
                string strDestinationFile = "";

                if (files.Length > 0)
                {
                    lblMsg.Text = "Added file :\n";

                    for (int i = 0; i < files.Length; i++)
                    {
                        strSourceFile = files[i];
                        strDestinationFile = Path.Combine(strDESTFOLDER, Path.GetFileName(files[i]));

                        File.Copy(strSourceFile, strDestinationFile, true);

                        FileInfo fi = new FileInfo(strDestinationFile);
                        fi.IsReadOnly = false;
                        fi.Refresh();

                        vFileNames.Add(Path.GetFileName(files[i]));
                    }

                    FillAddedFilesList(vFileNames);
                }
                else
                {
                    MessageBox.Show("No HTML files found on given location !");
                }
            }
            else
            {
                MessageBox.Show("Please select both Source and Destination folder");
            }
        }

        private void btnSelectSrc_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();

            if (folderBrowse.ShowDialog() == DialogResult.OK)
            {
                strSOURCEFOLDER = folderBrowse.SelectedPath;
                lblSrcDir.Text = strSOURCEFOLDER;
                bIsSourceSelcted = true;
            }
        }

        private void btnSelectDest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowseDest = new FolderBrowserDialog();

            if (folderBrowseDest.ShowDialog() == DialogResult.OK)
            {
                strDESTFOLDER = folderBrowseDest.SelectedPath;
                lblDstDir.Text = strDESTFOLDER;
                bIsDestinationSelected = true;
            }
        }

        private void btnListOutExisting_Click(object sender, EventArgs e)
        {
            strDESTFOLDER = "C:\\Documents and Settings\\apathak\\Desktop\\Rapid Development Kit\\Files";
            string[] files = Directory.GetFiles(strDESTFOLDER, "*.htm", SearchOption.AllDirectories);
            ArrayList vFileNames = new ArrayList();

            if (files.Length > 0)
            {
                lblMsg.Text = "Added file :\n";

                for (int i = 0; i < files.Length; i++)
                {
                    vFileNames.Add(Path.GetFileName(files[i]));
                }

                FillAddedFilesList(vFileNames);
            }
            else
            {
                MessageBox.Show("No html Files found in " + strDESTFOLDER);
            }
        }

        private void btnProcessForClassAttrib_Click(object sender, EventArgs e)
        {
            ArrayList arrDfctvFiles = new ArrayList();

            string _fldId = "";
            int _iFldId;
            bool _isNumbr = false;
            string matchString = "class=\"";

            //for (int cnt = 0; cnt < lstBxAddedFiles.Items.Count; cnt++)
            //{
            //strFILENAMEinPROCESS = lstBxAddedFiles.Items[cnt].ToString().Replace(".htm", "").Trim());
            //readHTMLFile(Path.Combine(strDESTFOLDER, strFILENAMEinPROCESS);

            strFILENAMEinPROCESS = lstBxAddedFiles.SelectedItem.ToString().Replace(".htm", "").Trim();
            strHTMLFILEPATH = Path.Combine(strDESTFOLDER, strFILENAMEinPROCESS + ".htm");

            readHTMLFile(strHTMLFILEPATH);

            for (int i = 0; i < strHtmlLines.Length; i++)
            {
                if (strHtmlLines[i].Contains(matchString))
                {
                    string sTemp = strHtmlLines[i].Substring(strHtmlLines[i].IndexOf(matchString) + matchString.Length);
                    _fldId = sTemp.Split('"')[0];
                    _isNumbr = int.TryParse(_fldId, out _iFldId);
                    if (_isNumbr)
                    {
                        strHtmlLines[i] = strHtmlLines[i].Replace(matchString + _iFldId + "\"", "id=\"" + strFILENAMEinPROCESS + "_" + _iFldId + "\"");
                        //arrDfctvFiles.Add(lstBxAddedFiles.Items[cnt].ToString());
                        arrDfctvFiles.Add(lstBxAddedFiles.SelectedItem.ToString());
                        break;
                    }
                }
            }

            File.WriteAllLines(strHTMLFILEPATH, strHtmlLines);

            //}

            string _rusltList = "";
            if (arrDfctvFiles.Count > 0)
            {
                _rusltList = "Found files : \n";
                for (int j = 0; j < arrDfctvFiles.Count; j++)
                {
                    _rusltList += arrDfctvFiles[j].ToString() + "\n";
                }
            }
            else
            {
                _rusltList = "No files found having numeric CLASS attribute.";
            }

            MessageBox.Show(_rusltList);
        }

        private void btnRemoveName_Click(object sender, EventArgs e)
        {            
            string matchString = "name=\"";

            strFILENAMEinPROCESS = lstBxAddedFiles.SelectedItem.ToString().Replace(".htm", "").Trim();
            strHTMLFILEPATH = Path.Combine(strDESTFOLDER, strFILENAMEinPROCESS + ".htm");

            readHTMLFile(strHTMLFILEPATH);

            for (int i = 0; i < strHtmlLines.Length; i++)
            {
                if (strHtmlLines[i].Contains(matchString))
                {
                    if (strHtmlLines[i].Contains("datepick") || strHtmlLines[i].Contains("description") || strHtmlLines[i].Contains("viewport"))
                    {

                    }
                    else
                    {
                        strHtmlLines[i] = strHtmlLines[i].Replace(matchString, "title=\"");                                           
                    }
                }
            }

            File.WriteAllLines(strHTMLFILEPATH, strHtmlLines);

            MessageBox.Show("Process done");
        }

        #endregion

        #region ======= Methods =======

        public void readXMLFile(string filePath)
        {
            lstAllImageFields.RemoveRange(0, lstAllImageFields.Count);

            XDocument doc = XDocument.Load(filePath);

            var allFields = doc.Descendants("Field");

            foreach (XElement field in allFields)
            {
                string FieldID = field.Attribute(XName.Get("Id")).Value;
                int orgCount = field.Descendants("Property").Count();

                var visibleFields = from dispField in field.Descendants("Property") where dispField.Element(XName.Get("PropertyId")).Value != "8" select dispField;
                int countAftrFilter = visibleFields.Count();

                if (orgCount == countAftrFilter)
                {
                    BeastField objFld = new BeastField();
                    objFld.FieldID = FieldID;

                    bool isNodeUsable = true;

                    if (FieldID == "0")
                        isNodeUsable = false;

                    foreach (var fieldProperty in visibleFields)
                    {
                        string propertyID = fieldProperty.Element(XName.Get("PropertyId")).Value;

                        if (propertyID == "0") // Field Type
                        {
                            objFld.FieldTypeID = fieldProperty.Element(XName.Get("PropertyValue")).Value;
                        }
                    }

                    objFld.HtmlElementName = ((DescriptionAttribute[])((Definitations.FieldTypes)Convert.ToInt16(objFld.FieldTypeID)).GetType().GetField(((Definitations.FieldTypes)Convert.ToInt16(objFld.FieldTypeID)).ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false))[0].Description;

                    if (isNodeUsable == true)
                    {
                        lstAllImageFields.Add(objFld);
                    }
                }
            }
        }

        public void readHTMLFile(string filePath)
        {
            strHtmlLines = File.ReadAllLines(filePath);
        }

        public void ProcessHtml()
        {
            string _fldId = "";
            string matchString = "id=\"" + strFILENAMEinPROCESS + "_";
            string insertString = "";
            //string titleString = "";

            for (int i = 0; i < strHtmlLines.Length; i++)
            {
                if (strHtmlLines[i].Contains(matchString))
                {
                    string sTemp = strHtmlLines[i].Substring(strHtmlLines[i].IndexOf(matchString) + matchString.Length);
                    _fldId = sTemp.Split('"')[0];

                    insertString = GetHtmlNameProp(_fldId);
                    strHtmlLines[i] = strHtmlLines[i].Insert(strHtmlLines[i].IndexOf(matchString), insertString);
                }
            }

            File.WriteAllLines(strHTMLFILEPATH, strHtmlLines);

            MessageBox.Show("Process done");

        }

        private void FillAddedFilesList(ArrayList oFileNameArray)
        {
            //DataTable dtFileList = new DataTable();
            lstBxAddedFiles.DataSource = oFileNameArray;
        }

        private string GetHtmlNameProp(string pFldId)
        {
            string sResult = "name=\"\" ";
            try
            {
                BeastField bFld = (from oFld in lstAllImageFields
                                   where oFld.FieldID == pFldId
                                   select oFld).First();

                if (bFld != null)
                {
                    sResult = "name=\"" + bFld.HtmlElementName + "\" ";
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(InvalidOperationException))
                {
                    if (ex.Message == "Sequence contains no elements")
                    {
                        sResult = "name=\"\" ";
                    }
                }
            }
            return sResult;
        }

        #endregion
    }
}
