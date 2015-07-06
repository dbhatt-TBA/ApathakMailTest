using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using TBA.SifToHtmlUtility.FieldSpecifier;
using TBA.SifToHtmlUtility.Utilities;
using TBA.SifToHtmlUtility.DB;
using System.Diagnostics;
using System.Xml.Linq;

namespace TBA.SifToHtmlUtility
{
    public class SifToHtmlGenrator
    {
        #region ======= Global Variable ========

        string strROOT;
        string strSUBDIR;
        string strXMLSIFFILE;
        string strXMLFILE;
        string strXMLFILENAME;
        string strCTGID;

        int maxTopAllow = 12;
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

        public string InterfacePropertiesPath { get; set; }

        private string _SifPath;
        public string SifPath
        {
            get
            {
                return _SifPath;
            }
            set
            {

                string SifName = CheckForSIFEXIST(value);
                _SifPath = value + SifName;

                SifLastName = Path.GetFileNameWithoutExtension(SifName);
                SifLastName = SifLastName.Substring(6).ToString();
                SifFileName = SifLastName;
                SifFileNameOrg = SifLastName;
            }
        }

        string SifFileNameOrg;
        string SifFileName;
        string SifLastName;
        public string OutputPath { get; set; }

        private bool _IsAddEntryInDb = true;
        public bool isAddEntryInDB
        {
            get
            {
                return _IsAddEntryInDb;
            }
            set
            {
                _IsAddEntryInDb = value;
            }
        }


        private string _DbString;
        public string DBConnectionString
        {
            get
            {
                return _DbString;
            }
            set
            {
                _DbString = value;
            }
        }

        #endregion

        /// <summary>
        /// Create sif to html class object with default path. 
        /// D:\BeastWebPublish\Sif folder to put your sif files.
        /// D:\BeastWebPublish\Exe folder to put your InterfaceProperties.exe and other dlls.
        /// D:\BeastWebPublish\Output folder for getting your html files.
        /// Html files are automatically available if conversion process completed successfully.
        /// It will take the latest sif available in folder for processing.
        /// </summary>
        public SifToHtmlGenrator()
        {
        //    string FilePath = CheckForSIFEXIST("D:\\BeastWebPublish\\Sif");
        //    this.SifPath = "D:\\BeastWebPublish\\Sif\\";
        //    this.OutputPath = "D:\\BeastWebPublish\\Output";
        //    this.InterfacePropertiesPath = "D:\\BeastWebPublish\\Exe\\";
        }

        /// <summary>
        /// Parameterized constructor to specify the path of sif and output.
        /// </summary>
        /// <param name="SifPath">Path of sif folder like D:\\Sif\\ </param>
        /// <param name="InterfacePropertiesPath">Path of InterfaceProperties.exe and related dll Folder like D:\\Exes\\.</param>
        /// <param name="OutputPath">Output folder path where your html is placed after genrating it from Folder.</param>
        /// <param name="OutputPath">MSSQl Database connection string</param>
        public SifToHtmlGenrator(string SifPath, string OutputPath, string InterfacePropertiesPath,string dbString)
        {
            //string FilePath = CheckForSIFEXIST(SifPath);
            this.SifPath = SifPath;
            this.OutputPath = OutputPath;
            this.InterfacePropertiesPath = InterfacePropertiesPath;
            this.DBConnectionString = dbString;
        }

        public void GenrateHtml()
        {
            if (String.IsNullOrWhiteSpace(SifPath))
            {
                throw new Exception("Please Specify Sif-file path properly.");
            }
            else if (String.IsNullOrWhiteSpace(SifPath) || !SifPath.Contains(".sif"))
            {
                throw new Exception("Please Select Sif File only.");
            }
            else if (String.IsNullOrWhiteSpace(OutputPath))
            {
                throw new Exception("Please must Specify Output Path.");
            }
            else
            {
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
                        throw new Exception("path is not fond, please give XML File path");
                    }
                    else
                    {
                        readXMLFile(strXMLFILE);
                        caseCharStr();
                        startImageBody();

                        if (lstALLFILEDNODES.Count == 0)
                        {
                            throw new Exception("Please select XML first");
                        }
                        else
                        {
                            //Old Code
                            //List<FieldNode> sortedList = new List<FieldNode>();
                            //sortedList = lstALLFILEDNODES.OrderBy(x => x.FieldProps.Top).ThenBy(x => x.FieldProps.Left).ToList();
                            //int crntTop = 0;
                            //int lstTop = 0;

                            //foreach (FieldNode lstItem in sortedList)
                            //{
                            //    bool isFirstTD = false;

                            //    crntTop = lstItem.FieldProps.Top;
                            //    if (crntTop != lstTop)
                            //    {
                            //        isFirstTD = true;
                            //        lstTop = lstItem.FieldProps.Top;
                            //    }

                            //    addTD(isFirstTD, Convert.ToInt32(lstItem.FieldProps.FieldType.Trim()), lstItem.FieldID, lstItem.FieldProps.Title);
                            //}
                            
                            //New Code
                            List<FieldNode> sortedList = new List<FieldNode>();
                            List<FieldNode> sortedList2 = new List<FieldNode>();
                            sortedList = lstALLFILEDNODES.OrderBy(x => x.FieldProps.Top).ThenBy(x => x.FieldProps.Left).ToList();
                            int crntTop = 0;
                            int lstTop = 0;

                            foreach (FieldNode lstItem in sortedList)
                            {
                                bool isFirstTD = false;

                                crntTop = lstItem.FieldProps.Top;
                                int diff = Math.Abs(crntTop - lstTop);

                                //if (crntTop != lstTop)
                                if (diff > maxTopAllow)
                                {
                                    isFirstTD = true;
                                    foreach (FieldNode lst in sortedList2.OrderBy(x => x.FieldProps.Left))
                                    {
                                        addTD(isFirstTD, Convert.ToInt32(lst.FieldProps.FieldType.Trim()), lst.FieldID, lst.FieldProps.Title);
                                        isFirstTD = false;
                                    }
                                    sortedList2.Clear();
                                    lstTop = lstItem.FieldProps.Top;
                                }
                                sortedList2.Add(lstItem);
                                //addTD(isFirstTD, Convert.ToInt32(lstItem.FieldProps.FieldType.Trim()), lstItem.FieldID, lstItem.FieldProps.Title);
                            }
                            endImageBody();
                            getHTML();

                            strROOT = strXMLSIFFILE;
                            strSUBDIR = strXMLSIFFILE + "\\" + SifFileName;
                            FilesGenerate();
                            strXMLFILENAME = Path.GetFileName(strXMLSIFFILE);
                            strXMLFILE = strXMLSIFFILE;
                            string strdir = OutputPath;
                            try
                            {
                                if (strXMLFILE == null)
                                {
                                    throw new Exception("Please select XML file path firstly.");
                                }
                                else
                                {
                                    string sourcePath = strXMLFILE;
                                    string targetPath = strdir;
                                    DirectoryCopy(strXMLFILE, strdir, true);
                                    DeleteDirectory(strXMLFILE);
                                    int regid = Convert.ToInt32(-1);
                                    FillCategory();
                                    //Todo : 
                                    if (_IsAddEntryInDb)
                                    {
                                        DBHandler db = new DBHandler(_DbString);
                                        string strResult = db.InsertUpdateApplication(regid, SifFileName, strAPPName, Convert.ToInt32(strCTGID), Convert.ToInt32(SifLastName));
                                    }
                                    //string strResult = db.InsertUpdateRegistration(regid, SifFileName, strAPPName, Convert.ToInt32(strCTGID), Convert.ToInt32(SifLastName));
                                    //temp set Result hardcoded
                                    //string strResult = "#1";
                                    //if (strResult.Split('#')[0] == "1")
                                    //{
                                    //  lbldbmsg.Text = "File converted successfully.";
                                    //}
                                    // else
                                    //{
                                    //lbldbmsg.Text = strResult.Split('#')[1];
                                    //}
                                    //Application.Exit();
                                    //clear();
                                }
                            }

                            catch (Exception ex)
                            {
                                throw;
                                //MessageBox.Show(ex.Message);
                            }
                        }
                    }
                    //UpdateAppSettings("AppDefaultPth", txtPath.Text);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void sifTOxml()
        {
            try
            {
                strXMLSIFFILE = SifPath;

                strXMLSIFFILE = strXMLSIFFILE.Replace(".sif", ".xml");
                string argu = SifPath + " " + strXMLSIFFILE;
                DeleteDirectory(strXMLSIFFILE);
                strXMLFILENAME = Path.GetFileName(strXMLSIFFILE);

                strXMLFILE = OutputPath + "\\" + strXMLFILENAME;

                if (File.Exists(strXMLFILE))
                {
                    File.Delete(strXMLFILE);
                }

                Process p = new Process();
                ProcessStartInfo ps = new ProcessStartInfo();
                ps.WindowStyle = ProcessWindowStyle.Hidden;
                ps.FileName = InterfacePropertiesPath + "InterFaceProperties.exe";
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
                throw;
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
        private void caseCharStr()
        {
            string strARRAPPNAME = string.Empty;
            SifFileName = SifFileNameOrg;
            string strTEMPAPPNAME = strAPPName.Trim();
            string[] arrAPPNAME = strTEMPAPPNAME.Split(' ');
            foreach (string sAPPNAME in arrAPPNAME)
            {
                strARRAPPNAME += (sAPPNAME[0] + "");
                //break;
            }
            //strFILENAMEORG = strFILENAME;
            SifFileName = strARRAPPNAME + SifFileName;
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

            hwWRITER.AddAttribute(HtmlTextWriterAttribute.Id, SifFileName);
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
        public string getHTML()
        {
            return swSTRINGWRITER.ToString();
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
        public void FilesGenerate()
        {

            if (strROOT.Length == 0)
            {
                throw new Exception("please give path for storing files.");
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
                    FileInfo f0 = new FileInfo(Path.Combine(strSUBDIR, SifFileName + ".htm"));
                    objCOMMONUTILITY.WriteHTMLFile(SifFileName, f0, swSTRINGWRITER, iHTMLSPANWIDTH, iMAXCOL);

                    FileInfo f1 = new FileInfo(Path.Combine(strSUBDIR, "app.css"));
                    objCOMMONUTILITY.WriteAppCSSFile(SifFileName, f1);

                    FileInfo f2 = new FileInfo(Path.Combine(strSUBDIR, "appclass.js"));
                    objCOMMONUTILITY.WriteAppJSFile(SifFileName, strSUBDIR, f2);

                    FileInfo f3 = new FileInfo(Path.Combine(strSUBDIR, "manifest.js"));
                    objCOMMONUTILITY.WriteManifestJSFile(SifFileName, swSTRINGWRITER, f3, iMAXCOL);

                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.ToString());
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
        private void FillCategory()
        {
            try
            {

                string strCTGNAME;
                dtCATEGORY = new DBHandler(_DbString).CategoryList();
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
        private string CheckForSIFEXIST(string SifDirectory)
        {

            var directory = new DirectoryInfo(SifDirectory);

            var myFile = directory.GetFiles("*.sif")
              .OrderByDescending(f => (f == null ? DateTime.MinValue : f.LastWriteTime)).FirstOrDefault();

            return Convert.ToString(myFile);
        }
        public void createElement(int fieldType, string fieldId, string eleTitle)
        {
            string strElementId = SifFileName + "_" + fieldId;
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
    }
}
