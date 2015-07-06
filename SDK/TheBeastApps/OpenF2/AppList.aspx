<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppList.aspx.cs" Inherits="OpenF2.AppList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" class="no-js">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>The Beast Apps</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <link href="css/FinalStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <%--<script src="js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>--%>
    <link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/normalize.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    <script src="js/jquery.ui.touch-punch.min.js" type="text/javascript"></script>
    <script src="js/json2.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-2.1.1.min.js" type="text/javascript"></script>
    <script src="js/properties.js" type="text/javascript"></script>
    <script src="js/widgets/PriceWidget/priceWidgetScript.js" type="text/javascript"></script>
    <script src="js/widgets/PriceWidget/priceWidgetScriptMobile.js" type="text/javascript"></script>
    <script src="js/widgets/htmlTemplate.js" type="text/javascript"></script>
    <script src="js/FormatScript.js" type="text/javascript"></script>
    <script src="js/vendor/modernizr-2.6.2.min.js" type="text/javascript"></script>
    <link href="css/VCMComman.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="https://www.thebeastapps.com/beastappscore/signalr/hubs"></script>--%>
    <link href="js/vendor/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="js/vendor/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="js/plugins.js" type="text/javascript"></script>
    <script src="js/jquery.jmsajax.min.js" type="text/javascript"></script>
    <script src="js/jquery.numeric.js" type="text/javascript"></script>
    <script src="js/VolSeparate.js" type="text/javascript"></script>
    <script src="js/widgets/ShareCalculator.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDLOOAyBr_eEeocGCinFcoL-2jBlg52zEY&sensor=true" type="text/javascript"></script>
    <%-- For Highchats  --%>
<%--    <script src="js/chart/HighCharts/highcharts.js" type="text/javascript"></script>
    <script src="js/chart/HighCharts/exporting.js" type="text/javascript"></script>
    <script src="js/chart/HighCharts/chartCreateAndUpdate.js" type="text/javascript"></script>
    <script src="js/jquery.battatech.excelexport.js" type="text/javascript"></script>
    <script src="js/jquery.base64.js" type="text/javascript"></script>--%>
    <!--[if lt IE 9]><script language="javascript" type="text/javascript" src="js/chart/jqPlot/excanvas.js"></script><![endif]-->
    <%--     <script language="javascript" type="text/javascript" src="js/chart/jqPlot/jquery.min.js"></script>
    --%>
    <%--https://www.thebeastapps.com/beastappscore/--%>

    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="js/f2.min.js" type="text/javascript"></script>
    
    <script src="js/widgets/TermWidget/termWidgetScript.js" type="text/javascript"></script>
    <script src="js/widgets/TermWidget/termWidgetScriptMobile.js" type="text/javascript"></script>
    <script src="js/widgets/BasisWidget/basisWidgetScript.js" type="text/javascript"></script>
    <script src="js/VCMCommon.js" type="text/javascript"></script>
    <script src="js/Applist.js" type="text/javascript"></script>
</head>
<body style="background: url(images/body_bg_small.jpg) repeat-x top left;">
    <!--[if lt IE 7]>
            <p class="chromeframe">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> or <a href="http://www.google.com/chromeframe/?redirect=true">activate Google Chrome Frame</a> to improve your experience.</p>
        <![endif]-->
    <form id="form1" runat="server">
        <div class="row-fluid">
            <div class="span12">
                <!--Header-->
                <%--<div class="row-fluid" style="background: url(images/body_bg.jpg) repeat-x top left;">--%>
                <div class="row-fluid" style="background-color: #1E1A1B;">
                    <div class="span8 offset2">
                        <div class="row-fluid">
                            <div class="span4">
                                <img src="images/beastlogo-1.png" class="pull-left" style="padding: 10px 0 3px 1px;" />
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
                                                <%--<li><a href="LogOut.aspx?isIndex=1">Log Out</a></li>--%>
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
                    <div class="12">
                        <table>
                            <tr>
                                <td onclick="toogleMenu()" align="center" style="width: 20%;">
                                    <div id="liCalcMenuDisplay" class="hLinks hLnkActive">
                                        Calculator Menu
                                        <%--<img src="images/RefreshMenu.png" alt="Alternate Text"  onclick="setdirimg()" />--%>
                                    </div>
                                </td>
                                <td onclick="toogleMenu()" align="center" style="width: 60%;">
                                    <div class="footer-copyright" style="border: none; width: 120px; margin: 3px 0px 0px 0px;" id="textConnect">
                                        Connection:<img src="images/red.png" id="imgConnect" alt="" style="margin: 0px 0px 2px 3px;" />
                                    </div>
                                </td>
                                <td style="width: 10%;" align="center">
                                    <div class="hLinks UserNameIconHover">
                                        <asp:Label ID="lblLoggedInUser"  runat="server"></asp:Label>
                                        <div class="UserLabelHover">
                                            <div style="margin: 5px;">
                                                <a href="ChangePassword.aspx">Change Password</a>
                                            </div>
                                            <div style="margin: 5px;">
                                                <a onclick="CloseMarketHubOnLogout()" href="index.aspx?isIndex=1">Log Out</a>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td align="right">
                                    <div class="btn-group" data-toggle="buttons-radio">
                                        <button id="btnCalcRender_SamePage" type="button" class="btn btn-custom-darken btnCalcRenderMode">
                                            Same Page</button>
                                        <button id="btnCalcRender_NewTab" type="button" class="btn btn-custom-darken btnCalcRenderMode">
                                            New Tab</button>
                                        <button id="btnCalcRender_ReplaceLast" type="button" class="btn btn-custom-darken btnCalcRenderMode active">
                                            Replace Last</button>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="login-menu-pointer img-sprite" style="display: none;">
            &nbsp;
        </div>
        <div class="container-fluid mainContainer examplePage">
            <div class="row-fluid">
                <div id="divHorizontalMenu" class="span2 sideBarDiv">
                    <div class="collapse in">
                        <div class="">
                            <div class="row-fluid">
                                <div class="sideBarDiv">
                                    <div class="exampleCaptions">
                                        <div class="smallDivider">
                                        </div>
                                        <div>
                                            <%-- <asp:DropDownList ID="ddlCountry" runat="server">
                                        </asp:DropDownList>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div style="margin-left: 0px;" class="span10 appDiv">
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
            </div>
            <%-- <div class="span2 visible-desktop">
        </div>--%>
        </div>
        <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-header">
                <h3 id="myModalLabel">The Beast Apps</h3>
            </div>
            <div class="modal-body">
                <p>
                    Application got disconnected with server. Please reconnect.
                </p>
            </div>
            <div class="modal-footer">
                <button id="btnReconnect" onclick="document.location.reload(true)" class="btn btn-primary">
                    Reconnect</button>
            </div>
        </div>
        <div id="busyModal" class="modal hide" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-header">
                <h3 id="H3">The Beast Apps</h3>
            </div>
            <div style="text-align: center" class="modal-body">
                <img alt="" src="images/ajax-loader.gif" />
            </div>
            <div class="modal-footer">
                <p>
                    Application Loading...
                </p>
            </div>
        </div>
        <div id="msgModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-header">
                <h3 id="H1">The Beast Apps</h3>
            </div>
            <div class="modal-body">
                <div id="divMsg">
                </div>
            </div>
            <div class="modal-footer">
                <a id="btnCloseMsgModal" data-dismiss="modal" class="close">Close</a>
            </div>
        </div>
        <div id="shareAppModal" class="modal hide fade"
            tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-header">
                <h3 id="H2">The Beast Apps</h3>
            </div>
            <div class="modal-body">
                <textarea rows="3" id="txtShareReceiptsList"></textarea>
            </div>
            <div class="modal-footer">
                <a id="A1" data-dismiss="modal" class="btn btn-primary">Share App</a>
            </div>
        </div>
        <div id="modalPriceWidget" style="width: 295px;" class="modal hide" tabindex="-1"
            role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <input id="txtBeastValue" type="hidden" />
            <input id="txtLastClickedEleInfo" type="hidden" />
            <div style="padding: 5px;">
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>
                            <table border="0" style="font-family: Verdana; width: 100%; font-size: 12px;">
                                <tr>
                                    <td class="WidgetHeader">
                                        <table>
                                            <tr>
                                                <td>Value
                                                </td>
                                                <td>
                                                    <input id="txtValue1" class="input-small" type="text" readonly="readonly" value="" />
                                                </td>
                                                <td>
                                                    <input id="txtValue2" class="input-small" type="text" readonly="readonly" value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Format
                                                </td>
                                                <td>
                                                    <select id="FormatSelection" class="input-small" onchange="format_selection_value();">
                                                        <option value="0">1/2</option>
                                                        <option value="1">1/4</option>
                                                        <option value="2">1/8</option>
                                                        <option value="3">1/16</option>
                                                        <option value="4">1/32</option>
                                                        <option value="5">1/64</option>
                                                        <option value="6">1/128</option>
                                                        <option value="7">1/256</option>
                                                        <option value="8">1/32+</option>
                                                        <option value="9">1/4R</option>
                                                        <option value="10">1/8R</option>
                                                        <option value="11">1/16R</option>
                                                        <option value="12">1/32R</option>
                                                        <option value="13">1/64R</option>
                                                        <option value="14">1/128R</option>
                                                        <option value="15">1/256R</option>
                                                        <option value="16">1</option>
                                                        <option value="17">0.1</option>
                                                        <option value="18">0.01</option>
                                                        <option value="19">0.001</option>
                                                        <option value="20">0.00+(1/2)</option>
                                                        <option value="21">0.00+(1/4)</option>
                                                    </select>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="table_widget_Price" cellpadding="2" cellspacing="0" style="width: 100%; height: 50%;">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="modal-footer" style="padding: 5px">
                <button class="btn" data-dismiss="modal" aria-hidden="true">
                    Close</button>
                <input id="btnOk" type="button" class="btn btn-primary" data-dismiss="modal" aria-hidden="true"
                    value="OK" style="width: 65px;" onclick="Save_PriceWidget();" />
            </div>
        </div>
        <div style="width: 390px;" class="modal hide" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" id="divTermWidget">
            <input id="txtBeastValue_TermWidget" type="hidden" />
            <input id="txtLastClickedEleInfo_Term" type="hidden" />
            <div>
                <table style="width: 100%;" border="1">
                    <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="left">
                                        <table>
                                            <tr>
                                                <td class="WidgetHeader">Value
                                                </td>
                                                <td>
                                                    <input id="txtValue1_TermWidget" class="input-small" type="text" readonly="readonly"
                                                        value="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <input id="btnYearMth" type="button" value="Year/Mth" class="btn" onclick="Bind_Term_Widget(3);"
                                                        style="width: 75px;" />
                                                </td>
                                                <td>
                                                    <input id="btnMonths" type="button" value="Months" class="btn" onclick="Bind_Term_Widget(4);"
                                                        style="width: 75px;" />
                                                </td>
                                                <td>
                                                    <input id="btnWeeks" type="button" value="Weeks" class="btn" onclick="Bind_Term_Widget(2);"
                                                        style="width: 75px;" />
                                                </td>
                                                <td>
                                                    <input id="btnDays" type="button" value="Days" class="btn" onclick="Bind_Term_Widget(0);"
                                                        style="width: 75px;" />
                                                </td>
                                                <td>
                                                    <input id="btnBusDays" type="button" value="Bus Days" class="btn" onclick="Bind_Term_Widget(1);"
                                                        style="width: 75px;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table id="table_widget_Term" style="width: 100%;">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="padding: 5px;" class="modal-footer">
                <input id="btnCancel" type="button" value="Close" class="btn" onclick="Close_TermWidget();" />
                <input id="Button1" type="button" value="OK" class="btn btn-primary" onclick="Save_TermWidget();" />
            </div>
        </div>
        <div id="tblBasis" border="1" style="width: 390px;" class="modal hide" tabindex="-1"
            role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <input id="txtBeastValue_BasisWidget" type="hidden" />
            <input id="txtLastClickedEleInfo_Basis" type="hidden" />
            <div id="childDivBasis">
                <table>
                    <tr>
                        <td class="WidgetHeader">&nbsp;Frequency&nbsp;
                        <input id="txtFrequency" class="input-medium" type="text" readonly="readonly" value=""
                            onclick="display_Term_BasisWidget(this);" />
                            &nbsp;
                        </td>
                        <td rowspan="2" align="right">
                            <input id="Button3" type="button" value="Close" class="btn" data-dismiss="modal"
                                aria-hidden="true" onclick="Close_BasisWidget();" />
                            <input id="Button2" type="button" value="OK" class="btn btn-primary" data-dismiss="modal"
                                aria-hidden="true" onclick="Save_BasisWidget();" />
                        </td>
                    </tr>
                    <tr>
                        <td class="WidgetHeader">&nbsp;Day Count&nbsp;
                        <select id="DayCountSelection" class="input-medium" onchange="DayCount_selection_value();">
                            <option value="A60">A60 : Act360</option>
                            <option value="A65">A65 : Act365</option>
                            <option value="AJP">AJP : Act365Japan</option>
                            <option value="AA">AA : ActAct</option>
                            <option value="165">165 : Act365ISDA</option>
                            <option value="BIS">BIS : 30360ISDA</option>
                            <option value="BPS">BPS : 303060PSA</option>
                            <option value="BSI">BSI : 30360SIA</option>
                            <option value="B60">B60 : 30360E</option>
                            <option value="B65">B65 : 30365E</option>
                        </select>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table id="table_widget_Basis" style="width: 100%;">
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 390px; display: none;" id="divTermBasisWidget">
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <table style="font-family: Verdana; width: 100%; font-size: 14px;">
                                    <tr>
                                        <td class="WidgetHeader" align="left">
                                            <table>
                                                <tr>
                                                    <td>Value
                                                    </td>
                                                    <td>
                                                        <input id="txtValue1_BasisWidget" type="text" class="input-small" type="text" readonly="readonly"
                                                            value="" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <input id="btnYearMth_Basis_Term_Widget" type="button" value="Year/Mth" class="btn"
                                                            onclick="Bind_Basis_Term_Widget(3);" style="width: 75px;" />
                                                    </td>
                                                    <td>
                                                        <input id="btnMonths_Basis_Term_Widget" type="button" value="Months" class="btn"
                                                            onclick="Bind_Basis_Term_Widget(4);" style="width: 75px;" />
                                                    </td>
                                                    <td>
                                                        <input id="btnWeeks_Basis_Term_Widget" type="button" value="Weeks" class="btn" onclick="Bind_Basis_Term_Widget(2);"
                                                            style="width: 75px;" />
                                                    </td>
                                                    <td>
                                                        <input id="btnDays_Basis_Term_Widget" type="button" value="Days" class="btn" onclick="Bind_Basis_Term_Widget(0);"
                                                            style="width: 75px;" />
                                                    </td>
                                                    <td>
                                                        <input id="btnBusDays_Basis_Term_Widget" type="button" value="Bus Days" class="btn"
                                                            onclick="Bind_Basis_Term_Widget(1);" style="width: 75px;" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="table_widget_Term_Basis" style="width: 100%;">
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer" style="padding: 5px">
                    <input id="btnTermCancel" type="button" value="Close" class="btn" onclick="Close_Term_BasisWidget();" />
                    <input id="btnTermOK" type="button" class="btn btn-primary" value="OK" onclick="Save_Term_BasisWidget();" />
                </div>
            </div>
        </div>
        <div id="dvPriceWidgetMobile" border="1" class="modal hide" tabindex="-1" role="dialog"
            aria-hidden="true">
        </div>
        <div id="dvTermWidgetMobile" border="1" class="modal hide" tabindex="-1" role="dialog"
            aria-hidden="true">
        </div>
    </form>

    <input type="hidden" id="hdn_userId" runat="server" />
    <input type="hidden" id="hdn_custId" runat="server" />
    <input type="hidden" id="hdn_senderEmailId" runat="server" />
    <input type="hidden" id="hdn_instanceMode" runat="server" />
    <input type="hidden" id="hdn_instanceType" runat="server" />
    <input type="hidden" id="hdnCurrCalc" runat="server" />
    <input type="hidden" id="hdnUserInfo" runat="server" />
    <input type="hidden" id="hdnNewTabCalc" runat="server" value="" />
    <input type="hidden" id="hdnMenuID" runat="server" value="" />
    <input type="hidden" id="hdnCalcRenderMode" runat="server" value="btnCalcRender_ReplaceLast" />
    <input type="hidden" id="hdnWgtElement" />
    <input type="hidden" id="hdnCategoryID" runat="server" />
    <input type="hidden" id="hdnInitialID" runat="server" value="0" />
    <input type="hidden" id="hdnMobileWidgetEnable" runat="server" value="1" />
    <input type="hidden" id="hdn_AuthToken" runat="server" />
    <input type="hidden" id="hdn_ClientType" runat="server" />
    <input type="hidden" id="hdnLogoutpath" runat="server" />
    <input type="hidden" id="hdn_ConnectionID" runat="server" />
    <input type="hidden" id="hdnEmailId" runat="server" />
    <input type="hidden" id="hdn_SignalRSetup" runat="server" />
</body>
</html>
