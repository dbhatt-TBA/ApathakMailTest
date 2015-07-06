<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoURLRedirect.aspx.cs"
    Inherits="AutoURLRedirect" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppNl" runat='server'>
            <ContentTemplate>
                <center>
                    <br />
                    <div style="text-align: center; padding: 30px 0px 30px 0px; font-size: 14px; font-family: Verdana; display:none;"
                        runat="Server" id="divredirect">
                        You are being redirected. If redirect does not happen automatically, please
                        <asp:LinkButton ID="lnkbtnReload" runat="server" Text="click here" Font-Names="Verdana"
                            OnClick="lnkbtnReload_Click" ForeColor="Black"></asp:LinkButton>
                    </div>
                    <div style="text-align: center; padding: 30px 0px 30px 0px; font-size: 14px; display: none;"
                        runat="Server" id="divBeastPlugin">
                        <br />
                        <br />
                        <asp:Label ID="lblMsg" runat="server" Style="font-family: Verdana; color: Red;"></asp:Label>
                        <br />
                        <br />
                        <asp:HyperLink ID="hylnkHome" runat="server" Font-Bold="True" Font-Names="Verdana"
                            Text="Click here to try again" Font-Size="8pt" ForeColor="Navy"></asp:HyperLink>
                    </div>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdfConversionID" runat="server" />
    </div>
    </form>
</body>
</html>
