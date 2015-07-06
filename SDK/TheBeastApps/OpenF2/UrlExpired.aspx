<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UrlExpired.aspx.cs" Inherits="OpenF2.UrlExpired"
    Async="true" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>The Beast Apps</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/FinalStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="css/bootstrap/bootstrap.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row-fluid">
            <div class="span12">
                <!--Header-->
                <div class="row-fluid" style="background: url(images/body_bg.jpg) repeat-x top left;">
                    <div class="span8 offset2">
                        <div class="row-fluid">
                            <div class="span4">
                                <img src="images/beastlogo-1.png" class="pull-left" style="padding: 10px 0 3px 1px;"
                                    alt="" />
                            </div>
                            <div class="span8">
                                <div class="navbar navbar-inverse pull-right">
                                    <div class="container">
                                        <!-- .btn-navbar is used as the toggle for collapsed navbar content -->
                                        <a class="btn btn-navbar" data-toggle="collapse" data-target=".nav-collapse"><span
                                            class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span>
                                        </a>
                                        <!-- Everything you want hidden at 940px or less, place within here -->
                                        <!--<div class="nav-collapse collapse" style="padding-left: 25px; float: left;">-->
                                        <div class="nav-collapse collapse">
                                            <ul class="nav menuborde">
                                                <div class="row-fluid visible-desktop">
                                                    <div class="span12">
                                                    </div>
                                                </div>
                                                <li><a href="http://www.thebeastapps.com/">Home</a></li>
                                                <li class="active"><a href="#">The App Store</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/DevZone">Dev Zone</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/ProductsServices">Products &amp; Services</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/ManagementTeam">Management Team</a></li>
                                                <li><a href="http://www.thebeastapps.com/Home/Contact">Contact Us</a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
                <div class="row-fluid" style="background-color: #353535;">
                    <div class="span10 offset2">
                        <h2 style="color: #FFFFFF;">THE BEAST APP STORE</h2>
                    </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
                <!--Body-->
                <div class="row-fluid">
                    <div class="span10 offset2">
                        <div style="width: 100%; margin: 0px auto; text-align: center;">
                            <div style="text-align: left; font-family: Verdana, Calibri, 'Segoe UI'; font-size: 11px; width: 90%;">
                                <br />
                                <br />
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                <br />
                                <br />
                                <table style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th style="width: 15%;"></th>
                                            <%--<th style="width: 5%;">
                                        </th>--%>
                                            <th style="width: 85%;"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="text-align: left;">URL:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblUrl" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">User:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblUser" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">URL Sent By:
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblSharedBy" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="lblInitiatorEmail" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblValidityPeriodTitle" runat="server"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblValidityPeriod" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; padding: 15px 0px;" colspan="3">
                                                <asp:Label ID="lblReqAccessMessage" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; padding: 5px 0px 15px 0px;" colspan="2">
                                                <%--<asp:TextBox ID="txtRequestAccessMessage" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;--%>
                                                <asp:Button ID="btnRequestAccess" runat="server" CssClass="btn btn-success" Text="Request Access"
                                                    OnClick="btnRequestAccess_Click" /><br />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <!--Footer-->
                <div class="row-fluid footer">
                    <div class="span8 offset2">
                        <div class="row-fluid">
                            <div class="span3">
                                <span>CONTACT US</span>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div>
                                            <br />
                                            <p style="color: #66AE1D;">
                                                NEW YORK
                                            </p>
                                            <p>
                                                The Beast Apps
                                            </p>
                                            <p>
                                                Email: <a href="#">info@thebeastapps.com</a>
                                            </p>
                                            <p style="color: #66AE1D;">
                                                INDIA
                                            </p>
                                            <p>
                                                The Beast Apps (India) Private Limited
                                            </p>
                                            <p>
                                                Email: <a href="#">infoindia@thebeastapps.com</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span4">
                                <span>Products &amp; Services</span>
                                <div class="row-fluid">
                                    <div class="span12" style="margin-left: 0px;">
                                        <div class="footer-link">
                                            <br />
                                            <p>
                                                <a href="#">THE BEAST APP STORE</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">PRIVATE APP STORE</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">TRADING & RISK MANAGEMENT
                                                SOLUTIONS</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">VOLMAX® Trading Solution</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">Risk Recycling® Solution</a>
                                            </p>
                                            <p>
                                                <a href="http://www.thebeastapps.com/Home/ProductsServices">Weather Electronic Trading
                                                Platform</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span2">
                                <span>Others</span>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="footer-link">
                                            <br />
                                            <p>
                                                <a>Career</a>
                                            </p>
                                            <p>
                                                <a>Privacy</a>
                                            </p>
                                            <p>
                                                <a>Terms of Use</a>
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="span3">
                                <span>GET IN TOUCH</span>
                                <div class="row-fluid">
                                    <div class="span12">
                                        <div class="footer-link">
                                            <ul class="social_icons">
                                                <li><a href="#">
                                                    <img src="images/icon_social_fb.png" border="0" /></a></li>
                                                <li><a href="#">
                                                    <img src="images/icon_social_twitter.png" border="0" /></a></li>
                                                <li><a href="#">
                                                    <img src="images/icon_social_gplus.png" border="0" /></a></li>
                                                <li><a href="#">
                                                    <img src="images/icon_social_linked.png" border="0" /></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-fluid">
                            <div class="span12 footer-copyright">
                                <script type="text/javascript">
                                    copyright = new Date();
                                    update = copyright.getFullYear();
                                    document.write("Copyright © 2005-" + update + ". THE BEAST APPS. ALL RIGHTS RESERVED.");
                                </script>
                            </div>
                        </div>
                    <div class="span2 visible-desktop">
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
