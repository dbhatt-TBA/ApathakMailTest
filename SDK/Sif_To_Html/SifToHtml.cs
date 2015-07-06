using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;
using System.Configuration;
using System.Web.Configuration;
using System.Web;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SIF_XML_ToHTMLUtility
{
    public partial class SifToHtml : Form
    {
        #region ======= Global Variable ========

        string strFILENAME;
        string strROOT;
        string strSUBDIR;
        string strXMLSIFFILE;
        string strXMLFILE;
        string strXMLFILENAME;
        string strSIFLASTNAME;
        string strCTGID;
        string strFILENAMEORG;

        int iHTMLSPANWIDTH;
        int iMAXCOL = 0;
        int iMAXCOLTEMP = 2;
        string strAPPName;


        DataTable dtCATEGORY;
        List<FieldNode> lstALLFILEDNODES = new List<FieldNode>();
        StringWriter swSTRINGWRITER;
        HtmlTextWriter hwWRITER;
        Hashtable htTITLESTRING = new Hashtable();
        BeastCommonMethod objCOMMONUTILITY;
        #endregion

        #region ======= Events ========
        private void browse_Click(object sender, EventArgs e)
        {

            OpenFileDialog sifFILE = new OpenFileDialog();
            sifFILE.Filter = "sif Files|*.sif";
            sifFILE.Title = "select an sif File (xml Format)";
            sifFILE.InitialDirectory = ConfigurationManager.AppSettings["SifDefaultPth"].Trim();
            if (sifFILE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtsif.Text = sifFILE.FileName;
                strSIFLASTNAME = Path.GetFileNameWithoutExtension(txtsif.Text);
                strSIFLASTNAME = strSIFLASTNAME.Substring(6).ToString();
                strFILENAME = Path.GetFileNameWithoutExtension(txtsif.Text);
                strFILENAME = strFILENAME.Substring(6).ToString();
                strFILENAMEORG = strFILENAME;

                string strSIFPath = sifFILE.FileName.Substring(0, sifFILE.FileName.IndexOf(sifFILE.SafeFileName));
                //WriteSetting("SifDefaultPth", strSIFPath);

            }
        }
        private void btnselectpath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = ConfigurationManager.AppSettings["AppDefaultPth"].Trim();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
            WriteSetting("AppDefaultPth", txtPath.Text);
        }
        private void btnProcessXML_Click(object sender, EventArgs e)
        {
            if (txtsif.Text == string.Empty && txtPath.Text == string.Empty)
            {
                MessageBox.Show("Please Select Sif File and Default Path");
            }
            else if (txtsif.Text == string.Empty || !txtsif.Text.Contains(".sif"))
            {
                MessageBox.Show("Please Select Sif File");
            }
            else if (txtPath.Text == string.Empty)
            {
                MessageBox.Show("Please Select Default Path");
            }
            else
            {
                DataAccess objDA = new DataAccess();
                try
                {
                    iMAXCOL = 0;
                    iMAXCOLTEMP = 2;
                    htTITLESTRING = new Hashtable();
                    swSTRINGWRITER = new StringWriter();
                    sifTOxml();
                    //caseCharStr();
                    if (strXMLFILE == null)
                    {
                        MessageBox.Show("path is not fond, please give XML File path");
                    }
                    else
                    {
                        readXMLFile(strXMLFILE);
                        caseCharStr();
                        startImageBody();

                        if (lstALLFILEDNODES.Count == 0)
                        {
                            MessageBox.Show("Please select XML first");
                        }
                        else
                        {
                            List<FieldNode> sortedList = new List<FieldNode>();
                            sortedList = lstALLFILEDNODES.OrderBy(x => x.FieldProps.Top).ThenBy(x => x.FieldProps.Left).ToList();
                            int crntTop = 0;
                            int lstTop = 0;

                            foreach (FieldNode lstItem in sortedList)
                            {
                                bool isFirstTD = false;

                                crntTop = lstItem.FieldProps.Top;
                                if (crntTop != lstTop)
                                {
                                    isFirstTD = true;
                                    lstTop = lstItem.FieldProps.Top;
                                }

                                addTD(isFirstTD, Convert.ToInt32(lstItem.FieldProps.FieldType.Trim()), lstItem.FieldID, lstItem.FieldProps.Title);
                            }
                            endImageBody();
                            getHTML();

                            strROOT = strXMLSIFFILE;
                            strSUBDIR = strXMLSIFFILE + "\\" + strFILENAME;
                            FilesGenerate();
                            strXMLFILENAME = Path.GetFileName(strXMLSIFFILE);
                            strXMLFILE = strXMLSIFFILE;
                            string strdir = txtPath.Text;
                            try
                            {
                                if (strXMLFILE == null)
                                {
                                    MessageBox.Show("Please select XML file path firstly.");
                                }
                                else
                                {
                                    string sourcePath = strXMLFILE;
                                    string targetPath = strdir;
                                    DirectoryCopy(strXMLFILE, strdir, true);
                                    int regid = Convert.ToInt32(-1);
                                    FillCategory();
                                    string strResult = objDA.InsertUpdateApplication(regid, strFILENAME, strAPPName, Convert.ToInt32(strCTGID), Convert.ToInt32(strSIFLASTNAME));
                                    //temp set Result hardcoded
                                    //string strResult = "#1";
                                    if (strResult.Split('#')[0] == "1")
                                    {
                                        lbldbmsg.Text = "File converted successfully.";
                                    }
                                    else
                                    {
                                        //lbldbmsg.Text = strResult.Split('#')[1];
                                    }
                                    Application.Exit();
                                    //clear();
                                }
                            }

                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    //UpdateAppSettings("AppDefaultPth", txtPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void btnshowfolder_Click(object sender, EventArgs e)
        {
            strXMLFILE = strXMLSIFFILE;
            try
            {
                if (strXMLFILE == null)
                {
                    MessageBox.Show("Please select XML file path firstly.");
                }
                else
                {
                    string sourcePath = strXMLFILE;

                    string appDefPath = ConfigurationManager.AppSettings["AppDefaultPth"].Trim();

                    if (!string.IsNullOrEmpty(txtPath.Text) && txtPath.Text != ConfigurationManager.AppSettings["AppDefaultPth"].Trim())
                    {
                        appDefPath = txtPath.Text;
                    }

                    appDefPath = appDefPath + "\\" + strFILENAME;
                    Process.Start(appDefPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnclear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void NewSIFtoXML_Load(object sender, EventArgs e)
        {

        }
        private void NewSIFtoXML_Load_1(object sender, EventArgs e)
        {
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region ======= Methods =======
        public SifToHtml()
        {
            InitializeComponent();
            string strSIFName = string.Empty;

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            txtPath.Text = configuration.AppSettings.Settings["AppDefaultPth"].Value;
            //txtsif.Text = configuration.AppSettings.Settings["SifDefaultPth"].Value;
            strSIFName = CheckForSIFEXIST(configuration.AppSettings.Settings["SifDefaultPth"].Value);

            txtsif.Text = configuration.AppSettings.Settings["SifDefaultPth"].Value + strSIFName;
            swSTRINGWRITER = new StringWriter();
        }

        private string CheckForSIFEXIST(string SifDirectory)
        {

            var directory = new DirectoryInfo(SifDirectory);

            var myFile = directory.GetFiles("*.sif")
              .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime))
              .FirstOrDefault();

            return Convert.ToString(myFile);
        }

        public static void WriteSetting(string key, string value)
        {
            ////load config document for current assembly 
            XmlDocument doc = loadConfigDocument();
            // retrieve appSettings node 
            XmlNode node = doc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found in config file.");
            }
            try
            {
                // select the 'add' element that contains the key 
                XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));
                if (elem != null)
                {
                    // add value for key 
                    elem.SetAttribute("value", value);
                }
                else
                {
                    // key was not found so create the 'add' element 
                    // and set it's key/value attributes 
                    elem = doc.CreateElement("add");
                    elem.SetAttribute("key", key);
                    elem.SetAttribute("value", value);
                    node.AppendChild(elem);
                }
                doc.Save(getConfigFilePath());
            }
            catch
            {
                throw;
            }
        }
        public static XmlDocument loadConfigDocument()
        {
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(getConfigFilePath());
                return doc;
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("No configuration file found.", e);
            }
        }
        public static string getConfigFilePath()
        {
            return Assembly.GetExecutingAssembly().Location + ".config";
        }
        private void DeleteDirectory(string xmlfile)
        {
            if (Directory.Exists(xmlfile))
            {
                //Delete all files from the Directory
                foreach (string file in Directory.GetFiles(xmlfile))
                {
                    File.Delete(file);
                }
                //Delete all child Directories
                foreach (string directory in Directory.GetDirectories(xmlfile))
                {
                    DeleteDirectory(directory);
                }
                //Delete a Directory
                Directory.Delete(xmlfile);
            }
        }
        private void caseCharStr()
        {
            string strARRAPPNAME = string.Empty;
            strFILENAME = strFILENAMEORG;
            string strTEMPAPPNAME = strAPPName.Trim();
            string[] arrAPPNAME = strTEMPAPPNAME.Split(' ');
            foreach (string sAPPNAME in arrAPPNAME)
            {
                strARRAPPNAME += (sAPPNAME[0] + "");
                //break;
            }
            //strFILENAMEORG = strFILENAME;
            strFILENAME = strARRAPPNAME + strFILENAME;
        }
        private void ProcessXML()
        {
            startImageBody();

            if (lstALLFILEDNODES.Count == 0)
            {
                MessageBox.Show("Please select XML first");
            }
            else
            {
                List<FieldNode> sortedList = new List<FieldNode>();
                sortedList = lstALLFILEDNODES.OrderBy(x => x.FieldProps.Top).ThenBy(x => x.FieldProps.Left).ToList();

                int crntTop = 0;
                int lstTop = 0;

                foreach (FieldNode lstItem in sortedList)
                {
                    bool isFirstTD = false;

                    crntTop = lstItem.FieldProps.Top;

                    if (crntTop != lstTop)
                    {
                        isFirstTD = true;
                        lstTop = lstItem.FieldProps.Top;
                    }
                    addTD(isFirstTD, Convert.ToInt32(lstItem.FieldProps.FieldType.Trim()), lstItem.FieldID, lstItem.FieldProps.Title);
                }
                endImageBody();
                getHTML();
                strROOT = txtPath.Text;
                strSUBDIR = strROOT + "\\" + strFILENAME;
                //strSUBDIR = strROOT + "\\" + strFILENAMEORG;

                FilesGenerate();
            }
        }
        private void sifTOxml()
        {
            try
            {
                strXMLSIFFILE = txtsif.Text;

                strXMLSIFFILE = strXMLSIFFILE.Replace(".sif", ".xml");
                string argu = txtsif.Text + " " + strXMLSIFFILE;
                DeleteDirectory(strXMLSIFFILE);
                strXMLFILENAME = Path.GetFileName(strXMLSIFFILE);

                strXMLFILE = txtPath.Text + "\\" + strXMLFILENAME;

                if (File.Exists(strXMLFILE))
                {
                    File.Delete(strXMLFILE);
                }

                Process p = new Process();
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.FileName = ConfigurationManager.AppSettings["InterFaceProperties"].Trim() + "InterFaceProperties.exe";
                ps.Arguments = argu;//"Screen2021.sif fs5.xml";
                p.StartInfo = ps;
                p.Start();

                //changes sleep time from 2000 to 800 for performance
                //System.Threading.Thread.Sleep(2000);
                System.Threading.Thread.Sleep(800);
                if (File.Exists(strXMLFILE))
                {
                    File.Delete(strXMLFILE);
                }
                System.IO.File.Move(strXMLSIFFILE, strXMLFILE);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        private void FillCategory()
        {
            try
            {

                string strCTGNAME;
                dtCATEGORY = SIF_XML_ToHTMLUtility.DataAccess.CategoryList();
                for (int i = 0; i < dtCATEGORY.Rows.Count; i++)
                {
                    strCTGNAME = Convert.ToString(dtCATEGORY.Rows[i]["CategoryName"]);
                    strCTGID = Convert.ToString(dtCATEGORY.Rows[i]["CategoryId"]);

                    if ((strCTGNAME == "Utilities"))
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public void FilesGenerate()
        {

            if (strROOT.Length == 0)
            {
                MessageBox.Show("please give path for storing files.");
            }
            else
            {
                if (!Directory.Exists(strROOT))
                {
                    Directory.CreateDirectory(strROOT);
                }
                if (!Directory.Exists(strSUBDIR))
                {
                    Directory.CreateDirectory(strSUBDIR);

                }

                try
                {
                    objCOMMONUTILITY = new BeastCommonMethod();
                    FileInfo f0 = new FileInfo(Path.Combine(strSUBDIR, strFILENAME + ".htm"));
                    objCOMMONUTILITY.WriteHTMLFile(strFILENAME, f0, swSTRINGWRITER, iHTMLSPANWIDTH, iMAXCOL);

                    FileInfo f1 = new FileInfo(Path.Combine(strSUBDIR, "app.css"));
                    objCOMMONUTILITY.WriteAppCSSFile(strFILENAME, f1);

                    FileInfo f2 = new FileInfo(Path.Combine(strSUBDIR, "appclass.js"));
                    objCOMMONUTILITY.WriteAppJSFile(strFILENAME, strSUBDIR, f2);

                    FileInfo f3 = new FileInfo(Path.Combine(strSUBDIR, "manifest.js"));
                    objCOMMONUTILITY.WriteManifestJSFile(strFILENAME, swSTRINGWRITER, f3, iMAXCOL);

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
        }
        public void startImageBody()
        {
            hwWRITER = new HtmlTextWriter(swSTRINGWRITER);
            //[NU Changes as discussed with AP] 
            //hwWRITER.AddAttribute(HtmlTextWriterAttribute.Class, "span12");
            //[NU Remove Additional DIV Tag as discussed with AP] 
            //hwWRITER.RenderBeginTag(HtmlTextWriterTag.Div); // Start Span12 # 1
            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Div); // Start Span4 # 2
            hwWRITER.AddAttribute(HtmlTextWriterAttribute.Class, "tblClass hdrBorder table table-condensed");

            hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strFILENAME);
            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Table); // Start Table # 3
            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Thead);

            int thCount = 0;
            float appPerc = 0;

            if (thCount != 0)
            {
                appPerc = 100 / thCount;
            }

            for (int thCtr = 0; thCtr < thCount; thCtr++)
            {
                hwWRITER.AddStyleAttribute(HtmlTextWriterStyle.Width, appPerc.ToString() + "%");
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Th);
                hwWRITER.RenderEndTag();
            }
            hwWRITER.RenderEndTag();
            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Tbody);

            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Tr);
            //addTD(false,0, "", "Name");

            //hwWRITER.Write("<td colspan = " + iTDMAX + ">");
            hwWRITER.Write("<td [COLSPAN]" + ">");
            //hwWRITER.RenderBeginTag(HtmlTextWriterTag.Td); // start TD
            hwWRITER.Write("<div class=" + '"' + "productNameHdr pull-left" + '"' + "" + ">");
            hwWRITER.Write("<div " + " style=" + '"' + "float: left; margin-left: 10px;" + '"' + ">");
            //hwWRITER.Write("<div " + "style=" + "float: left; margin-left: 10px;" + ">" + "<strong" + ">Gottex CHF Ref. Page Local</strong>" + "</div>");
            //hwWRITER.Write("<strong" + ">Gottex CHF Ref. Page Local</strong>");
            hwWRITER.Write("<strong" + ">" + strAPPName + "</strong>");

            hwWRITER.Write("</div>");
            hwWRITER.Write("</div>");
            //hwWRITER.RenderEndTag(); // End TD
            hwWRITER.Write("</td>");
            hwWRITER.RenderEndTag(); // End TR

            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Tr);
            //hwWRITER.RenderBeginTag(HtmlTextWriterTag.Td); // start TD
            //hwWRITER.Write("<td colspan = " + iTDMAX + ">");
            hwWRITER.Write("<td [COLSPAN]" + ">");

            //hwWRITER.Write("<div class=" + '"' +"ServerName" + '"' + '"' +  "style="+ "text-align: left!important;"+ '"' +"" + ">Server Name</div>");
            hwWRITER.Write("<div class=" + '"' + "ServerName" + '"' + " style=" + '"' + "text-align: left !important;" + '"' + "" + "></div>");

            //hwWRITER.RenderEndTag(); // End TD  
            hwWRITER.Write("</td>");
            //addTD(false, 0, "", "Server Name");
            hwWRITER.RenderEndTag(); // End TR

            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Tr);
        }
        public void endImageBody()
        {
            hwWRITER.RenderEndTag(); // End TR
            hwWRITER.RenderEndTag(); // End TBODY
            hwWRITER.RenderEndTag(); // End Table # 3
            hwWRITER.RenderEndTag(); // End Span4 # 2
            //[NU Remove Additional DIV Tag as discussed with AP] 
            //hwWRITER.RenderEndTag(); // End Span12 # 1
        }
        public void addTD(bool isFirstTD, int eleType, string eleClass, string eleTitle)
        {

            if (isFirstTD == true)
            {
                hwWRITER.RenderEndTag(); // End TR
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Tr); // start TR

                if (iMAXCOLTEMP > 0)
                {
                    if (iMAXCOLTEMP > iMAXCOL)
                        iMAXCOL = iMAXCOLTEMP;
                    iMAXCOLTEMP = 2;
                }
            }
            else
            {
                //writer.RenderEndTag(); // End TR
                //writer.RenderBeginTag(HtmlTextWriterTag.Tr); 
                iMAXCOLTEMP = iMAXCOLTEMP + 1;
                //eleTitle = null;
            }

            hwWRITER.RenderBeginTag(HtmlTextWriterTag.Td); // start TD

            //hwWRITER.Write("<td colspan = " + iMAXCOLTEMP + ">");

            if (isButtonField(eleType) == false)
            {
                if (!string.IsNullOrEmpty(eleTitle) && htTITLESTRING.ContainsKey(eleTitle.Trim()) == false)
                {
                    htTITLESTRING.Add(eleTitle.Trim(), eleTitle.Trim());
                    hwWRITER.Write(eleTitle.Trim());
                    if (!isFirstTD)
                    {
                        iMAXCOLTEMP = iMAXCOLTEMP + 1;
                    }

                    hwWRITER.RenderEndTag(); // End TD
                    //hwWRITER.Write("</td>");
                    hwWRITER.RenderBeginTag(HtmlTextWriterTag.Td); // start TD
                    //hwWRITER.Write("<td colspan = " + iMAXCOLTEMP + ">");
                    //hwWRITER.Write("colspan = " + iMAXCOLTEMP + "");
                }
            }
            createElement(eleType, eleClass, eleTitle);
            hwWRITER.RenderEndTag(); // End TD   
            //hwWRITER.Write("</td>"); 

        }
        public void createElement(int fieldType, string fieldId, string eleTitle)
        {
            string strElementId = strFILENAME + "_" + fieldId;
            //enumList += "case \"" + fieldId + "\": \n";

            if (isListField(fieldType))           //Drop Down list
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Select); // start element
                hwWRITER.RenderEndTag(); // End element

                //writer.Write(":[id:" + fieldId + "/type:" + fieldType + "]");
            }
            //            else if (isStringField(fieldType) || isCaptionField(fieldType))    //Textbox
            else if (isStringField(fieldType))    //Textbox
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Type, "text");

                if (!string.IsNullOrEmpty(eleTitle))
                {
                    hwWRITER.AddAttribute(HtmlTextWriterAttribute.Value, eleTitle.Trim());
                }

                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Input); // start element
                hwWRITER.RenderEndTag(); // End element
            }

            else if (isDoubleField(fieldType))
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Type, "text");

                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Input); // start element
                hwWRITER.RenderEndTag(); // End element
            }
            else if (isDateField(fieldType))      //Textbox
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Title, "datepick");
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Input); // start element
                hwWRITER.RenderEndTag(); // End element
            }
            else if (isButtonField(fieldType))    //Button
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                if (!string.IsNullOrEmpty(eleTitle))
                {
                    hwWRITER.AddAttribute(HtmlTextWriterAttribute.Value, eleTitle.Trim());
                }
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Input); // start element
                hwWRITER.RenderEndTag(); // End element
            }
            else if (isImageField(fieldType))     //Image
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Src, "");
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Alt, "");
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Img); // start element
                hwWRITER.RenderEndTag(); // End element
            }
            else if (isFileUpload(fieldType))    //Textbox
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Type, "file");
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Input); // start element                
                hwWRITER.RenderEndTag(); // End element

                hwWRITER.Write(":[id:" + fieldId + "/type:" + fieldType + "]");
            }
            else if (isHLineField(fieldType))
            {
                hwWRITER.Write("<hr/>");
            }
            else if (isSelectButtonField(fieldType))    //Button
            {
                //When clicked - background color changes until clicked next time

                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Type, "button");
                if (!string.IsNullOrEmpty(eleTitle))
                {
                    hwWRITER.AddAttribute(HtmlTextWriterAttribute.Value, eleTitle.Trim());
                }
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.Input); // start element
                hwWRITER.RenderEndTag(); // End element
            }
            else if (IsUrlField(fieldType))    //Textbox
            {
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, strElementId);
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Href, "www.google.co.in");  //Need to get url property
                hwWRITER.AddAttribute(HtmlTextWriterAttribute.Target, "_blank");
                hwWRITER.RenderBeginTag(HtmlTextWriterTag.A); // start element
                hwWRITER.Write("www.google.co.in");
                hwWRITER.RenderEndTag(); // End element
            }
            else
            {
                //Commented due to unhandeled control
                //hwWRITER.Write("Control (id:" + fieldId + "/type:" + fieldType + ") Not handled");
                //hwWRITER.Write("Control (id:" + fieldId + "/type:" + fieldType + ")");
            }
        }
        public void readXMLFile(string filePath)
        {
            lstALLFILEDNODES = new List<FieldNode>();
            lstALLFILEDNODES.RemoveRange(0, lstALLFILEDNODES.Count);

            XDocument doc = XDocument.Load(filePath);
            var allFields = doc.Descendants("Field");

            //Common Field Value Retrival Code start
            var CommonFields = doc.Descendants("CommonDetails");
            foreach (XElement field in CommonFields)
            {
                string FieldID = field.Attribute(XName.Get("Id")).Value;
                var visibleFields = from dispField in field.Descendants("Property") where dispField.Element(XName.Get("PropertyId")).Value != "8" select dispField;

                foreach (var fieldProperty in visibleFields)
                {
                    string propertyID = fieldProperty.Element(XName.Get("PropertyId")).Value;

                    if (propertyID == "9999") // Field Type
                    {
                        //set width
                        iHTMLSPANWIDTH = Convert.ToInt32(fieldProperty.Element(XName.Get("PropertyValue")).Value);
                    }
                    else if (propertyID == "8000") // Title
                    {
                        //set Name
                        strAPPName = fieldProperty.Element(XName.Get("PropertyValue")).Value;
                    }
                }
            }
            //Common Field Value Retrival Code end

            foreach (XElement field in allFields)
            {
                string FieldID = field.Attribute(XName.Get("Id")).Value;
                int orgCount = field.Descendants("Property").Count();

                var visibleFields = from dispField in field.Descendants("Property") where dispField.Element(XName.Get("PropertyId")).Value != "8" select dispField;
                int countAftrFilter = visibleFields.Count();

                if (orgCount == countAftrFilter)
                {
                    FieldNode tmpNode = new FieldNode();
                    FieldProperties tmpProps = new FieldProperties();

                    tmpNode.FieldID = FieldID;

                    bool isNodeUsable = true;

                    if (FieldID == "0")
                        isNodeUsable = false;

                    foreach (var fieldProperty in visibleFields)
                    {
                        string propertyID = fieldProperty.Element(XName.Get("PropertyId")).Value;

                        if (propertyID == "0") // Field Type
                        {
                            tmpProps.FieldType = fieldProperty.Element(XName.Get("PropertyValue")).Value;
                        }
                        else if (propertyID == "8000") // Title
                        {
                            tmpProps.Title = fieldProperty.Element(XName.Get("PropertyValue")).Value;
                        }
                        else if (propertyID == "3") // Width
                        {
                            tmpProps.Width = Convert.ToInt32(fieldProperty.Element(XName.Get("PropertyValue")).Value.Trim());
                        }
                        else if (propertyID == "1") // Left
                        {
                            tmpProps.Left = Convert.ToInt32(fieldProperty.Element(XName.Get("PropertyValue")).Value.Trim());
                            if (fieldProperty.Element(XName.Get("PropertyValue")).Value == null)
                                isNodeUsable = false;
                        }
                        else if (propertyID == "2") // Top
                        {
                            tmpProps.Top = Convert.ToInt32(fieldProperty.Element(XName.Get("PropertyValue")).Value.Trim());
                            if (fieldProperty.Element(XName.Get("PropertyValue")).Value == null)
                                isNodeUsable = false;
                        }
                        else if (propertyID == "6") // TitleWidth
                        {
                            tmpProps.TitleWidth = Convert.ToInt32(fieldProperty.Element(XName.Get("PropertyValue")).Value.Trim());
                        }

                        if (!string.IsNullOrEmpty(tmpProps.Title))
                        {
                            if (tmpProps.Title.Contains("&amp;"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace("&amp;", "&");
                            }
                            if (tmpProps.Title.Contains("&lt;"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace("&lt;", "<");
                            }
                            if (tmpProps.Title.Contains("&gt;"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace("&gt;", ">");
                            }
                            if (tmpProps.Title.Contains("o:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace("o:", "ö");
                            }
                            if (tmpProps.Title.Contains(";h:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";h:", "-");
                            }
                            if (tmpProps.Title.Contains(";t:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";t:", "~");
                            }
                            if (tmpProps.Title.Contains(";p:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";p:", "%");
                            }
                            if (tmpProps.Title.Contains(";I:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";I:", "|");
                            }
                            if (tmpProps.Title.Contains(";e:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";e:", "=");
                            }
                            if (tmpProps.Title.Contains(";l:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";l:", "(");
                            }
                            if (tmpProps.Title.Contains(";r"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";r", ")");
                            }
                            if (tmpProps.Title.Contains(";u:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";u:", "ü");
                            }
                            if (tmpProps.Title.Contains(";f:"))
                            {
                                tmpProps.Title = tmpProps.Title.Replace(";f:", "ø");
                            }
                        }
                    }

                    tmpNode.FieldProps = tmpProps;
                    if (isNodeUsable == true)
                        lstALLFILEDNODES.Add(tmpNode);
                }
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                if (File.Exists(temppath))
                {
                    File.Delete(temppath);
                }
                file.CopyTo(temppath, false);
                //file.MoveTo(temppath);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        public string getHTML()
        {
            return swSTRINGWRITER.ToString();
        }
        #region =========== CheckFieldType ===========

        private bool isListField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_CURRENCY) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_LIST) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_CURVE) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_CYCLE_BUTTON) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_COMBO_LIST))
                return true;
            return false;
        }

        private bool isDoubleField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_DOUBLE) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_ARROW_AND_DOUBLE) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_TERM) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_INT) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_BINARY))
                return true;
            return false;
        }

        private bool isDateField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_DATE))
                return true;
            return false;
        }

        private bool isCaptionField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_CAPTION) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_ELAPSED_TIME) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_TIMER))
                return true;
            return false;
        }

        private bool isStringField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_STRING) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_BASIS))
                return true;
            return false;
        }

        private bool isButtonField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_POPUP_BUTTON) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_PUSH_BUTTON) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_TOGGLE_BUTTON) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_DOUBLE_BUTTON) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_COMMAND_BUTTON))
                return true;
            return false;
        }

        private bool isImageField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_SIGNAL) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_ARROW_AND_DOUBLE) || FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_ARROW))
                return true;
            return false;
        }

        private bool isCheckboxField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_CHECKBOX_CTRL))
                return true;
            return false;
        }

        private bool isFileUpload(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_FILE))
                return true;
            return false;
        }

        private bool isHLineField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_HLINE))
                return true;
            return false;
        }

        private bool isSelectButtonField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_SELECT_BUTTON))
                return true;
            return false;
        }

        private bool IsUrlField(int FieldType)
        {
            if (FieldType == Convert.ToInt32(Definitations.FieldTypes.FT_URLBUTTON))
                return true;
            return false;
        }

        #endregion (CheckFieldType)
        private void clear()
        {
            txtsif.Text = "";
            txtPath.Text = "";
        }
        #endregion

    }
}
