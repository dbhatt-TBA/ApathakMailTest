<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMail.aspx.cs" Inherits="MailTest.SendMail" Async="true" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>TheBeastApps | MailTest</title>
    <script src="tinymce/js/tinymce/tinymce.min.js"></script>
    <script>tinymce.init({ selector: 'textarea' });</script>
    <style type="text/css">
        body, input, textarea
        {
            font-family: Calibri, Verdana, 'Segoe UI';
            font-size: 12px;
        }

        .h2
        {
            color: #006633;
            font-family: Calibri;
        }

        table td
        {
            vertical-align: top;
        }

        .btnSelected
        {
            color: White;
            background-color: Navy;
        }

        .btnNormal
        {
            color: Black;
            background-color: White;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2 style="margin-left:150px">The BEAST Apps {Mail Test}</h2>
        <div style="border: 1px solid #999999; width: 600px;">
            <table>
                <tr>
                    <td style="width: 25%; text-align: right;">From:
                    </td>
                    <td style="width: 75%;">
                        <asp:TextBox runat="server" ID="txtFrom" Width="260px">sysadmin@thebeastapps.com</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">To:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTo" Width="260px">sysadmin@thebeastapps.com</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Subject:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSubject" Width="260px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Message:
                    </td>
                    <td>
                        <%--   <asp:TextBox runat="server" ID ="txtBody" TextMode="MultiLine"></asp:TextBox>--%>
                        <asp:TextBox runat="server" ID="txtBody" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">Use this Server:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSmtpServers" runat="server" Width="150px">
                            <asp:ListItem Selected="True" Value="0"> Amazon AWS </asp:ListItem>
                            <asp:ListItem Value="1"> Test </asp:ListItem>
                            <asp:ListItem Value="2"> Demo </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSendMail" Text="Send Mail" OnClick="btnSendMail_Click" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblMessage"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
