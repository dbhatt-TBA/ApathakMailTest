using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBA.SifToHtmlUtility;
namespace SifToHtmlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /// <summary>
            /// Create sif to html class object with default path. 
            /// D:\BeastWebPublish\Sif folder to put your sif files.
            /// D:\BeastWebPublish\Exe folder to put your InterfaceProperties.exe and other dlls.
            /// D:\BeastWebPublish\Output folder for getting your html files.
            /// Html files are automatically available if conversion process completed successfully.
            /// It will take the latest sif available in folder for processing.
            /// </summary>
            //SifToHtmlGenrator ProcessSif = new SifToHtmlGenrator();
            //ProcessSif.GenrateHtml();

            //Way 1 using parameterize constructor
            /// <summary>
            /// Parameterized constructor to specify the path of sif and output.
            /// </summary>
            /// <param name="SifPath">Path of sif folder like D:\\Sif\\ </param>
            /// <param name="OutputPath">Output folder path where your html is placed after genrating it from Folder.</param>
            /// <param name="InterfacePropertiesPath">Path of InterfaceProperties.exe and related dll Folder like D:\\Exes\\.</param>
            //SifToHtmlGenrator ProcessSif1 = new SifToHtmlGenrator(
            //    "D:\\BeastWebPublish\\sif\\",
            //    "D:\\BeastWebPublish\\Output\\",
            //    "D:\\BeastWebPublish\\Exe\\",
            //    @"Data Source=beasttestnu3;Initial Catalog=AppStore;Integrated Security=false; User ID = rradmin; Password = rradmin;");
            //ProcessSif1.GenrateHtml();


            //Way 2 using Property
            /// <summary>
            /// Parameterized constructor to specify the path of sif and output.
            /// </summary>
            /// <param name="SifPath">Path of sif folder like D:\\Sif\\ </param>
            /// <param name="OutputPath">Output folder path where your html is placed after genrating it from Folder.</param>
            /// <param name="InterfacePropertiesPath">Path of InterfaceProperties.exe and related dll Folder like D:\\Exes\\.</param>
            SifToHtmlGenrator ProcessSif1 = new SifToHtmlGenrator();
            ProcessSif1.SifPath = "D:\\BeastWebPublish\\sif\\";
            ProcessSif1.OutputPath = "D:\\BeastWebPublish\\Output\\";
            ProcessSif1.InterfacePropertiesPath = "D:\\BeastWebPublish\\Exe\\";
            ProcessSif1.isAddEntryInDB = true;
            ProcessSif1.DBConnectionString = @"Data Source=beasttestnu3;Initial Catalog=AppStore;Integrated Security=false; User ID = rradmin; Password = rradmin;";
            ProcessSif1.GenrateHtml();
        }
    }
}
