<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Signin.aspx.cs" Async="true" Inherits="Signin" %>

<!DOCTYPE html>
<html>
<head runat="server">
  <title></title>
    <link href="css/webApps.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div class="navbar-inner" >
                <a class="brand pull-left"  href="https://www.thebeastapps.com/">  </a>
                <div class="tagline-divider">
                </div>
              
                <div class="navbar-text pull-left">
                    The BEAST Financial Framework</div>
                <div class="collapse nav-collapse">
                    <ul class="nav pull-right">
                        <li><a href="https://www.thebeastapps.com/">Home</a></li></ul>
                </div>
            </div>
       
        </div>
    </div>
    <div style="margin:100px" >
        <table width="550" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="200" align="center" valign="middle" class="login-mid">
                    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="0" height="180px;">
                        <tr>
                            <td class="login-mid-mid">
                                <table width="68%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="center" colspan="2" valign="middle" class="brd">
                                            <div  class="login-text">
                                                The BEAST Financial Framework</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="10" colspan="2" align="right" valign="top" class="loginid">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="right" valign="middle" class="loginid">
                                            Username &nbsp;:
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="txtUserID" runat="server" Height="13px" Style="width: 90%; font: normal 11px verdana;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25" align="right" valign="middle" class="loginid">
                                            Password &nbsp;:
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="txtPass" runat="server" TextMode="Password" Height="13px" Style="width: 90%;
                                                font: normal 11px verdana;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30" align="right" valign="top" class="loginid">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="bottom">
                                       
                                                <asp:Button runat="server" id="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-mini btn-primary" Text="Login" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="baseline" style="padding-top: 8px; font: bold 10px verdana;
                                color: Red;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
