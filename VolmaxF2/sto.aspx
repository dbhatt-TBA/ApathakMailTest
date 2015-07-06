<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sto.aspx.cs" Inherits="sto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TheBEAST Apps</title>
    <script type="text/javascript" src="js/jquery-1.8.3.min.js"></script>
    <link href="css/webApps.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <div class="navbar-inner">
                    <a class="brand pull-left" href="https://www.thebeastapps.com/"></a>
                    <div class="tagline-divider">
                    </div>

                    <div class="navbar-text pull-left">
                        The BEAST Financial Framework
                    </div>
                    <div class="collapse nav-collapse">
                        <ul class="nav pull-right">
                            <li><a href="https://www.thebeastapps.com/">Home</a></li>
                        </ul>
                    </div>
                </div>

            </div>
        </div>
        <div style="margin: 100px">
            <div style="text-align: center; font-size: 14px; margin: 10px 20px 10px 20px; color: Red;">
                <asp:Label ID="lblMessage" runat="server" Text="Your Session is timed-out. Please login again."></asp:Label>
            </div>
            <div>
                <table width="70%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="grey-border" align="left">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="quick-contact-bg" align="left">
                                        <div style="margin: 0px 20px 0px 20px">
                                            <img src="images/quick-contact.gif" width="193" height="46" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="margin: 20px 20px 20px 20px">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="50%" valign="top">
                                                        <div class="common-text" style="margin: 0px 20px 0px 0px">
                                                            <div class="blue-common-text-bold">
                                                                NewYork Office
                                                            </div>
                                                            <div class="dashed-line" style="margin: 5px 0px 5px 0px">
                                                            </div>
                                                            <div>
                                                                44 Wall Street, 21st Floor
                                                            <br />
                                                                New York NY 10005<br />
                                                                <br />
                                                                <span class="common-text-bold">Telephone:</span>+1-646-688-7500<br />
                                                                <span class="common-text-bold">Fax:</span> +1-646-688-7499<br />
                                                                <span class="common-text-bold">Email:</span> <a href="mailto:info@thebeastapps.com"
                                                                    class="general-blue-link">info@thebeastapps.com</a>
                                                            </div>
                                                            <span class="common-text-bold">Website:</span> <a href="https://www.thebeastapps.com"
                                                                class="general-blue-link">https://www.thebeastapps.com</a>
                                                        </div>
                                                    </td>

                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
