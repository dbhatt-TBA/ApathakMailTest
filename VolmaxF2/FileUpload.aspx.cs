using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using VCM.Common;
using VCM.Common.Log;
using System.Threading;
using vcmProductNamespace;
public partial class FileUpload : System.Web.UI.Page
{

    string filename = "";

    private SessionInfo _session;

    public SessionInfo CurrentSession
    {
        get
        {
            if (_session == null)
            {
                _session = new SessionInfo(HttpContext.Current.Session);
            }
            return _session;
        }
        set
        {
            _session = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentSession.User == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("SessionTimeOut.htm", false);
            return;
        }
        if (!IsPostBack)
        {
            DataSet dtUserInfo = cDbHandler.GetFileUploadTracking(Convert.ToString(CurrentSession.User.UserID));
            if (dtUserInfo.Tables[0].Rows.Count > 0)
            {
                rptrUsers.DataSource = dtUserInfo;
                rptrUsers.DataBind();
            }
        }
    }

    protected void UploadButton_Click(object sender, EventArgs e)
    {
        if (FileUploadControl.HasFile)
        {
            try
            {
                string FileName = "";
                bool falg = true;
                DataSet dtUserInfo = new DataSet();
                HttpFileCollection uploadFiles = Request.Files;
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    string extenstion = Path.GetExtension(uploadFiles[i].FileName);

                    if (extenstion.ToUpper() != ".DLL")
                    {
                        falg = false;
                    }


                }
                if (falg == false)
                {
                    StatusLabel.Text = "Only dll file is accepted";

                    dtUserInfo = cDbHandler.GetFileUploadTracking(Convert.ToString(CurrentSession.User.UserID));
                    if (dtUserInfo.Tables[0].Rows.Count > 0)
                    {
                        rptrUsers.DataSource = dtUserInfo;
                        rptrUsers.DataBind();

                    }
                    return;
                }
                for (int i = 0; i < uploadFiles.Count; i++)
                {
                    filename = Path.GetFileName(uploadFiles[i].FileName);
                    FileName = FileName + filename + ",";
                    string extenstion = Path.GetExtension(uploadFiles[i].FileName);
                    uploadFiles[i].SaveAs(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FileUpload"]) + System.IO.Path.GetFileNameWithoutExtension(uploadFiles[i].FileName) + System.DateTime.Now.ToString("ddMMyyyy hhmmss") + System.IO.Path.GetExtension(uploadFiles[i].FileName));
                    hdnFile.Value = filename;


                    cDbHandler.SubmitFileUploadTracking(Convert.ToString(CurrentSession.User.UserID), filename, System.IO.Path.GetFileNameWithoutExtension(uploadFiles[i].FileName) + System.DateTime.Now.ToString("ddMMyyyy hhmmss") + System.IO.Path.GetExtension(uploadFiles[i].FileName), System.Configuration.ConfigurationManager.AppSettings["FileUpload"] + System.IO.Path.GetFileNameWithoutExtension(uploadFiles[i].FileName) + System.DateTime.Now.ToString("ddMMyyyy hhmmss") + System.IO.Path.GetExtension(uploadFiles[i].FileName));
                }
                dtUserInfo = cDbHandler.GetFileUploadTracking(Convert.ToString(CurrentSession.User.UserID));
                if (dtUserInfo.Tables[0].Rows.Count > 0)
                {
                    rptrUsers.DataSource = dtUserInfo;
                    rptrUsers.DataBind();
                    StatusLabel.Text = "Upload status: File uploaded Successfully!";

                }

                SendFileUploadedMail(FileName.Substring(0, FileName.Length - 1));
            }
            catch (Exception ex)
            {
                StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                LogUtility.Error("AutoURL", "UploadButton_Click", ex.Message, ex);
            }
        }
    }

    private void SendFileUploadedMail(string FileName)
    {
        try
        {
            string strMsgBody = "<div style=\"font-size:12pt;color:Navy;font-family:Verdana\"><b>THE BEAST APPS</b></div><br/><div style=\"font-size:8pt;color:Navy;font-family:Verdana\">Dear User,<p> BNP Client " + Convert.ToString(CurrentSession.User.UserName) + " has uploaded " + FileName + " on server.</p>" +
                                 "<p>" + Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["FileUpload"]) + "<br/><br/>" +
                                 "Sincerely, <br/><br/> " + VcmMailNamespace.vcmMail.strVCM_RrMailAddress_Html.ToString();

            //== Send email ==//                

            VcmMailNamespace.vcmMail _vcmMail = new VcmMailNamespace.vcmMail();
            _vcmMail.From = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
            _vcmMail.To = System.Configuration.ConfigurationManager.AppSettings["FromEmail"];
            _vcmMail.CC = "";
            _vcmMail.BCC = "";

            _vcmMail.SendAsync = true;
            _vcmMail.Subject = "TheBeastApps - File Uploaded";
            _vcmMail.Body = strMsgBody;
            _vcmMail.IsBodyHtml = true;
            _vcmMail.UserID = Convert.ToString(CurrentSession.User.UserID);
            _vcmMail.RecordCreateBy = Convert.ToString(CurrentSession.User.UserID);
            _vcmMail.SessionID = Convert.ToString(CurrentSession.User.SessionID);
            _vcmMail.IPAddress = Convert.ToString(CurrentSession.User.IPAddress);
            _vcmMail.SendMail(0);
            _vcmMail = null;

        }
        catch (Exception ex)
        {
            VcmLogManager.Log.writeLog(Convert.ToString(CurrentSession.User.UserName), "", "", "AutoURL", "SendFileUploadedMail()", ex.StackTrace.ToString() + "<br/><br/>" + ex.Message, VcmMailNamespace.vcmMail.Get_IPAddress(Request.UserHostAddress));
            LogUtility.Error("AutoURL", "SendFileUploadedMail", ex.Message, ex);
        }
    }





}
