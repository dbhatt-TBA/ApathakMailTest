using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace SIF_XML_ToHTMLUtility
{
    public class DataAccess
    {
        //public int i;
        string AppStoreConnectionstr = ConfigurationManager.ConnectionStrings["AppStoreConnectionStringtest"].ToString();
        public int SIFid;
        string strReturnMsg;

        public string InsertUpdateRegistration(int RegId, string AppName, string AppTitle, int CategoryId, int SIFID)
        {
            strReturnMsg = "0#";
            try
            {
                using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(AppStoreConnectionstr))
                {

                    {
                        //using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_Registrations", cn))

                        using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_Registrations_sdk", cn))
                        {

                            sqlCmd.CommandType = CommandType.StoredProcedure;

                            sqlCmd.Parameters.Add("@p_RegId", SqlDbType.Int);
                            sqlCmd.Parameters["@p_RegId"].Value = RegId;

                            sqlCmd.Parameters.Add("@p_AppName", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppName"].Value = AppName;

                            sqlCmd.Parameters.Add("@p_AppTitle", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppTitle"].Value = AppTitle;

                            sqlCmd.Parameters.Add("@p_UserId", SqlDbType.Int);
                            sqlCmd.Parameters["@p_UserId"].Value = 1;

                            sqlCmd.Parameters.Add("@p_AppDescription", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppDescription"].Value = "TheBEAST® technology based framework is an application distribution architecture. It is real time, reliable, robust and scalable architecture that allows one to build and deploy financial applications rapidly, to its internal users and customers worldwide within days. These financial applications can be high performance, real time streaming and interactive applications allowing one to see market data, do complex analytics, trade, do straight through processing, perform portfolio analysis and risk management functions etc.";

                            sqlCmd.Parameters.Add("@p_AppVersion", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppVersion"].Value = "1.0.0.0";

                            sqlCmd.Parameters.Add("@p_CategoryId", SqlDbType.Int);
                            sqlCmd.Parameters["@p_CategoryId"].Value = CategoryId;

                            sqlCmd.Parameters.Add("@p_AppSupportedOS", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppSupportedOS"].Value = "WindowXP";

                            sqlCmd.Parameters.Add("@p_AppFileSize", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppFileSize"].Value = "1KB";

                            sqlCmd.Parameters.Add("@p_AppPrice", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppPrice"].Value = "";

                            sqlCmd.Parameters.Add("@p_AppTags", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppTags"].Value = "";

                            sqlCmd.Parameters.Add("@p_AppSupportedLanguage", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppSupportedLanguage"].Value = "";

                            //sqlCmd.Parameters.Add("@p_AppIconImage", SqlDbType.VarBinary);
                            //sqlCmd.Parameters["@p_AppIconImage"].Value = AppIconImage;

                            sqlCmd.Parameters.Add("@p_Email", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_Email"].Value = "thebeast@thebeastapps.com";

                            sqlCmd.Parameters.Add("@p_ContactNumber", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_ContactNumber"].Value = "646-688-7500";

                            sqlCmd.Parameters.Add("@p_Action", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_Action"].Value = "N";

                            sqlCmd.Parameters.Add("@p_AppPackageURL", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppPackageURL"].Value = "";

                            sqlCmd.Parameters.Add("@p_Currency", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_Currency"].Value = "";

                            sqlCmd.Parameters.Add("@p_SupportURL", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_SupportURL"].Value = "";

                            sqlCmd.Parameters.Add("@p_YouTubeURL", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_YouTubeURL"].Value = "";

                            sqlCmd.Parameters.Add("@p_AppFilePath", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppFilePath"].Value = "";

                            sqlCmd.Parameters.Add("@p_AppSupportedDevices", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppSupportedDevices"].Value = "";

                            sqlCmd.Parameters.Add("@p_VendorId", SqlDbType.Int);
                            sqlCmd.Parameters["@p_VendorId"].Value = 1;
                            //if (AppReleasedDate == null)
                            //    sqlCmd.Parameters.Add(new SqlParameter("@p_AppReleasedDate", ""));
                            //else
                            //    sqlCmd.Parameters.Add(new SqlParameter("@p_AppReleasedDate", AppReleasedDate));

                            sqlCmd.Parameters.Add("@p_AppReleasedDate", SqlDbType.DateTime);
                            sqlCmd.Parameters["@p_AppReleasedDate"].Value = DateTime.Now;

                            sqlCmd.Parameters.Add("@p_AppAgeCriteria", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppAgeCriteria"].Value = "";

                            sqlCmd.Parameters.Add("@p_AppVendorInformation", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppVendorInformation"].Value = "TheBeast Apps";

                            sqlCmd.Parameters.Add("@p_BeastImageSID", SqlDbType.Int);
                            sqlCmd.Parameters["@p_BeastImageSID"].Value = SIFID;

                            sqlCmd.Parameters.Add("@p_IsGridImage", SqlDbType.Int);
                            sqlCmd.Parameters["@p_IsGridImage"].Value = 0;

                            sqlCmd.Parameters.Add("@p_ISshareable", SqlDbType.Bit);
                            sqlCmd.Parameters["@p_ISshareable"].Value = true;

                            sqlCmd.Parameters.Add("@p_shareminitues", SqlDbType.Int);
                            sqlCmd.Parameters["@p_shareminitues"].Value = 60;

                            cn.Open();
                            int rowsAffected = sqlCmd.ExecuteNonQuery();
                            if (rowsAffected == -1)
                            {
                                strReturnMsg = "-1#Error saving entry into database.";
                            }
                            else
                            {
                                strReturnMsg = "1#Success";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strReturnMsg = "-1#" + ex.Message + " / " + ex.InnerException.Message;
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return strReturnMsg;
        }

        public static DataTable CategoryList()
        {
            DataTable dtTmp = new DataTable();
            try
            {
                string AppStoreConnectionstr = ConfigurationManager.ConnectionStrings["AppStoreConnectionStringtest"].ToString();

                using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(AppStoreConnectionstr))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("Proc_Get_AppStore_Category", cn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        cn.Open();

                        SqlDataAdapter sqlDA = new SqlDataAdapter();
                        sqlDA.SelectCommand = sqlCmd;
                        sqlDA.Fill(dtTmp);
                    }
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return dtTmp;
        }


        public string InsertUpdateApplication(int RegId, string AppName, string AppTitle, int CategoryId, int SIFID)
        {
            strReturnMsg = "0#";
            try
            {
                using (System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection(AppStoreConnectionstr))
                {

                    {
                        //using (SqlCommand sqlCmd = new SqlCommand("Proc_Submit_AppStore_Registrations", cn))

                        using (SqlCommand sqlCmd = new SqlCommand("proc_submit_Appstore_application_sdk", cn))
                        {

                            sqlCmd.CommandType = CommandType.StoredProcedure;

                            sqlCmd.Parameters.Add("@p_RegId", SqlDbType.Int);
                            sqlCmd.Parameters["@p_RegId"].Value = RegId;

                            sqlCmd.Parameters.Add("@p_AppName", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppName"].Value = AppName;

                            sqlCmd.Parameters.Add("@p_AppTitle", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_AppTitle"].Value = AppTitle;

                            sqlCmd.Parameters.Add("@p_CategoryId", SqlDbType.Int);
                            sqlCmd.Parameters["@p_CategoryId"].Value = CategoryId;
                                 
                            sqlCmd.Parameters.Add("@p_DisplayinMenu", SqlDbType.VarChar);
                            sqlCmd.Parameters["@p_DisplayinMenu"].Value = "N";

                            sqlCmd.Parameters.Add("@p_BeastImageSID", SqlDbType.Int);
                            sqlCmd.Parameters["@p_BeastImageSID"].Value = SIFID;

                            sqlCmd.Parameters.Add("@p_IsGridImage", SqlDbType.Int);
                            sqlCmd.Parameters["@p_IsGridImage"].Value = 0;

                            sqlCmd.Parameters.Add("@p_ISshareable", SqlDbType.Bit);
                            sqlCmd.Parameters["@p_ISshareable"].Value = true;

                            sqlCmd.Parameters.Add("@p_shareminitues", SqlDbType.Int);
                            sqlCmd.Parameters["@p_shareminitues"].Value = 60;


                            sqlCmd.Parameters.Add("@p_record_created_by", SqlDbType.Int);
                            sqlCmd.Parameters["@p_record_created_by"].Value = 0;
                            
                            cn.Open();
                            int rowsAffected = sqlCmd.ExecuteNonQuery();
                            if (rowsAffected == -1)
                            {
                                strReturnMsg = "-1#Error saving entry into database.";
                            }
                            else
                            {
                                strReturnMsg = "1#Success";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strReturnMsg = "-1#" + ex.Message + " / " + ex.InnerException.Message;
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return strReturnMsg;
        }

    }
}
