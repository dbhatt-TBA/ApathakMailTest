<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SharedBeastApps.aspx.cs" Inherits="OpenBeast.SharedBeastApps" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>The Beast Apps</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <link href="css/FinalStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <%--<script src="css/bootstrap/bootstrap.js" type="text/javascript"></script>--%>
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
    <script type="text/javascript" src="http://localhost:4828/signalr/hubs"></script>
    <link href="js/vendor/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="js/vendor/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="js/plugins.js" type="text/javascript"></script>
    <script src="js/main.js" type="text/javascript"></script>
    <script src="js/jquery.jmsajax.min.js" type="text/javascript"></script>
    <script src="js/jquery.numeric.js" type="text/javascript"></script>
    <script src="js/VolSeparate.js" type="text/javascript"></script>
    <%-- For Highchats  --%>
    <script src="js/chart/HighCharts/highcharts.js" type="text/javascript"></script>
    <script src="js/chart/HighCharts/exporting.js" type="text/javascript"></script>
    <script src="js/chart/HighCharts/chartCreateAndUpdate.js" type="text/javascript"></script>


    <%-- handsontable js and css --%>
    <script src="js/handsontablejs/jquery.handsontable.full.js" type="text/javascript"></script>
    <script src="js/handsontablejs/numeral.sv-se.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/handsontablejs/jquery-migrate-1.2.1.min.js"></script>
    <script type="text/javascript" src="js/handsontablejs/jquery.validate-1.11.1.min.js"></script>
    <script type="text/javascript" src="js/handsontablejs/jquery.dform-1.1.0.min.js"></script>
    <script type="text/javascript" src="js/handsontablejs/jquery.ui.widget-1.10.4.js"></script>
    <script type="text/javascript" src="js/handsontablejs/jquery.tools.min.js"></script>
    <link rel="stylesheet" media="screen" href="js/handsontablejs/jquery.handsontable.full.css" type="text/css" />
    <link href="js/handsontablejs/jquery-ui-1.10.4.min.css" rel="stylesheet" type="text/css" />
    <%--<script src="js/handsontablejs/ExcelShareHandson.js" type="text/javascript"></script>--%>
    <script src="js/handsontablejs/numeral.js" type="text/javascript"></script>
    <script src="js/handsontablejs/dateFormat.js" type="text/javascript"></script>
    <script src="js/handsontablejs/evol.colorpicker.js" type="text/javascript"></script>
    <script src="js/handsontablejs/example.toolbar.js" type="text/javascript"></script>
    <script src="js/handsontablejs/example.hot.js" type="text/javascript"></script>
    <link href="js/handsontablejs/site.css" rel="stylesheet" />
    <link href="js/handsontablejs/evol.colorpicker.css" rel="stylesheet" />

    <style type="text/css">
        .idiv {
            /*position: absolute;*/
            /*top: 30%;
            left: 20%;*/
            /*display: block;
            background-color: white;*/
            border: 1px solid #777777;
            width: 45%;
            /* height: 15%;*/
            /*text-align: center;*/
        }
    </style>
    <%-- handsontable js and css --%>
</head>
<body style="background: url(images/body_bg_small.jpg) repeat-x top left;">

    <!--[if lt IE 7]>
            <p class="chromeframe">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> or <a href="http://www.google.com/chromeframe/?redirect=true">activate Google Chrome Frame</a> to improve your experience.</p>
        <![endif]-->
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
                                            <li class="active"><a href="Index.aspx">The App Store</a></li>
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
                <div class="span8 offset2">
                    <div class="row-fluid">
                        <div class="span12">
                            <table>
                                <tr>
                                    <td align="center" style="width: 90%;">
                                        <div class="footer-copyright" style="border: none; width: 120px; margin: 3px 0px 0px 0px;" id="textConnect">
                                            Connection:<img src="images/red.png" id="imgConnect" alt="" style="margin: 0px 0px 2px 3px;" />
                                        </div>
                                    </td>
                                    <td style="width: 10%;" align="center">
                                        <div class="hLinks">
                                            <a href="LogOut.aspx?isIndex=1&shared=true">Log Out</a>

                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <%-- <table width="100%">
                        <tr>
                            <td align="center" style="width: 80%;">
                            </td>
                            <td style="width: 20%;">
                                
                                </div>
                            </td>
                        </tr>
                    </table>--%>
                </div>
                <div class="span2 visible-desktop">
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid mainContainer examplePage">
        <div class="row-fluid">
            <div class="idiv" id="divStatus" runat="server" style="visibility: hidden;">
                <div class="display">
                    AutoUrl Expired ! But validity is extended for 1 hour access. 
                    <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
                    UTC
                </div>
            </div>
            <div class="span12 appDiv">
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

    <script type="text/javascript">
        var isSafari = false;
        function CloseBeastImage() {
            var customerID = "0";
            var userid = "0";
            var SpecialImageID = "0";
            if ($("#hdn_InstanceInfo").val().split('~')[0] == "cust") {
                customerID = $("#hdn_custId").val();
            }
            if ($("#hdn_InstanceInfo").val().split('~')[0] == "user") {
                userid = $("#hdn_userId").val();
            }
            if ($("#hdn_InstanceInfo").val().split('~')[0] == "spe") {
                SpecialImageID = "sp1";
            }

            var hdn_InstanceInfo = $("#hdn_InstanceInfo").val();
            CloseImageBeast($("#hdn_instanceType").val(), hdn_InstanceInfo.split('~')[2], userid, customerID, SpecialImageID, "1");

        }
        function getManifestURL(appID) {

            var varManifestUrl = "";


            if (appID == "vcm_calc_swaptionVolPrimStrike") {
                varManifestUrl = "SwapVolPrimStrike";
            }
            else if (appID == "vcm_calc_swaptionVol") {
                varManifestUrl = "SwapVol";
            }
            else if (appID == "vcm_calc_swaptionPrim") {
                varManifestUrl = "SwapPrim";
            }
            else if (appID == "vcm_calc_swaptionStrike") {
                varManifestUrl = "SwapStrike";
            }
            else {
                varManifestUrl = appID;
            }

            return varManifestUrl;
        }

        $(document).ready(function () {

            var ua = navigator.userAgent.toLowerCase();
            if (ua.indexOf('safari') != -1) {
                if (ua.indexOf('chrome') > -1) {
                    // Chrome
                    isSafari = false;
                } else {
                    // Safari
                    isSafari = true;
                    $("#imgConnect").hide();
                    $("#textConnect").append("<strong id='textConnectStatus' style='color:Red;font-size:14px;'>D</strong>");
                }
            }
            window.onbeforeunload = function (e) {
                var customerID = "0";
                var userid = "0";
                var SpecialImageID = "0";
                if ($("#hdn_InstanceInfo").val().split('~')[0] == "cust") {
                    customerID = $("#hdn_custId").val();
                }
                if ($("#hdn_InstanceInfo").val().split('~')[0] == "user") {
                    userid = $("#hdn_userId").val();
                }
                if ($("#hdn_InstanceInfo").val().split('~')[0] == "spe") {
                    SpecialImageID = "sp1";
                }

                CloseImageBeast($("#hdn_instanceType").val(), $("#hdn_InstanceInfo").val().split('~')[2], userid, customerID, SpecialImageID, "1");
                DisableConnection($("#hdn_AuthToken").val(), $("#hdn_sharedUserMailID").val(), $("#hdn_ConnectionID").val());

                return undefined;

            };
            $('#dvPriceWidgetMobile').draggable({ handle: "#dvPwmTitle" });
            $('#dvTermWidgetMobile').draggable({ handle: "#dvTwmTitle" });

            $(window).resize(checkWindowResize);
            $(window).scroll(checkWindowResize);

            var varappId = $("#hdn_instanceType").val();



            var varUserID = $("#hdn_userId").val();
            var varCustID = $("#hdn_custId").val();
            var varInstanceMode = $("#hdn_InstanceInfo").val();
            // ------------------------------------------------ Excel Share -------------------------------------------
            if (varappId == "vcm_calc_excelshare") {

                setupSignalRGeneric("vcm_calc_excelshare", $("#hdn_sharedUserMailID").val(), $("#hdn_custId").val(), varInstanceMode);
            }
                // ------------------------------------------------ Excel Share -------------------------------------------
            else {
                $("#hdn_manifestUrl").val("Products/VCM/" + getManifestURL(varappId) + "/manifest.js");
                var vardescription = $("#hdn_description").val();
                var varname = $("#hdn_name").val();
                var varmanifestUrl = $("#hdn_manifestUrl").val();

                var _appConfigs = [
                    {
                        appId: varappId,
                        description: vardescription,
                        name: varname,
                        manifestUrl: varmanifestUrl,
                        context: {
                            UserID: varUserID,
                            CustomerID: varCustID,
                            InstanceMode: varInstanceMode
                        }
                    }
                ];

                F2.init({
                    beforeAppRender: function (app) {
                        var appRoot = '<div id="mainProductSection" class="well span12"><div class="span12 wprow" id="' + getCurrentRowName() + '"></div></div>';

                        if ($("#mainProductSection").length == 0) {
                            $(appRoot).appendTo('div.appDiv');
                        }

                        if (app.name == "DefaultBeastApp") {
                            $("#divAppDesc").html(app.description);
                            //return $("<div class='allProd' id='" + app.appId + "'> </div>").appendTo("#mainProductSection");
                        }
                        else {
                            //return $("<div class='hide allProd' id='" + app.appId + "'> </div>").appendTo("#mainProductSection");
                        }
                        //return $('div.appDiv');

                        return appRoot;
                    },

                    afterAppRender: function (app, html) {

                        var spancls = "";
                        var spanInt;
                        var allclasses;

                        if ($(html).filter("." + app.appId).attr("class").length > 0) {
                            allclasses = $(html).filter("." + app.appId).attr("class").split(' ');
                        }

                        for (var icc = 0; icc < allclasses.length; icc++) {
                            var spancls = allclasses[icc];
                            if (spancls.indexOf("span") === 0) {
                                spanInt = spancls.substring(4);
                                break;
                            }
                        }

                        //                    var closeBtnHtml = '<div class="notfirst"><button onclick="CloseCalculator(\'' + app.appId + '\')" style="float:right;" type="button" class="close">&times;</button>Status: <img style="padding-right:15px;" class="navbar-stus-img-per-img imgCalculatorStatus" src="images/red.png" alt="" /></div>';

                        //var closeBtnHtml = '<div class="notfirst">Status: <img style="padding-right:15px;" class="navbar-stus-img-per-img imgCalculatorStatus" src="images/red.png" alt="" /></div>';
                        var statusimg = '<div style="float:right"><img class="navbar-stus-img-per-img imgCalculatorStatus" src="images/red.png" alt="" /></div><div class="calcStatusFont" style="float:right">Status:</div>';
                        var ele;

                        var tmpCrntRowIndx = getCurrentRowIndex();
                        var tmpRowIndx = 1;
                        var isCalcPlaced = false;

                        for (tmpRowIndx; tmpRowIndx <= tmpCrntRowIndx; tmpRowIndx++) {

                            var rowNameCrnt = getRelativeRowName(tmpRowIndx);
                            var totSpan = 0;
                            var isRowExists = false;

                            if ($("#mainProductSection div#" + rowNameCrnt + "").length > 0) {

                                isRowExists = true;

                                $("#mainProductSection div#" + rowNameCrnt + " > div").each(function () {
                                    var divclasses = $(this).attr("class").split(' ');

                                    for (var icc = 0; icc < divclasses.length; icc++) {
                                        var spancls = divclasses[icc];
                                        if (spancls.indexOf("span") === 0) {
                                            totSpan = parseInt(totSpan) + parseInt(spancls.substring(4));
                                        }
                                    }
                                });
                            }

                            var sumOfSpan = parseInt(totSpan) + parseInt(spanInt);

                            if (sumOfSpan <= 12 && isRowExists == true) {
                                ele = $("#mainProductSection div#" + rowNameCrnt + "");
                                isCalcPlaced = true;
                                break;
                            }
                        }

                        if (isCalcPlaced == false) {
                            IncrCurrentRowIndex();
                            $("#mainProductSection").append("<div style='margin-left:0px;' class='span12 wprow row-fluid' id='" + getCurrentRowName() + "'></div>");
                            ele = $("#mainProductSection #" + getCurrentRowName() + "");
                        }
                        $(ele).append(html).find("#" + app.appId + " .ServerName").append(statusimg);
                        //F2.UI.hideMask(app.instanceId, "#" + app.appId);
                        if (isSafari) {
                            $("#" + app.appId).find(".imgCalculatorStatus").hide();
                            $("#" + app.appId).find(".calcStatusFont").append("<strong id='textConnectStatusImage' style='color:Red;font-size:12px;'>D</strong>");
                        }
                        return ele;
                    }
                    , secureAppPagePath: "foo.html"
                });

                F2.registerApps(_appConfigs); //pass _appConfigs to initialize apps
            }
            if ($('#<%=hdnStst.ClientID%>').val() == "1") {

                document.getElementById('<%=divStatus.ClientID%>').style.visibility = "visible";
                $('#<%=divStatus.ClientID%>').delay(5000).hide(0, function () {
                });
            }
        });

    </script>
    <input type="hidden" id="hdnStst" runat="server" />
    <input type="hidden" id="hdn_instanceType" runat="server" />
    <input type="hidden" id="hdn_description" value="" runat="server" />
    <input type="hidden" id="hdn_name" runat="server" />
    <input type="hidden" id="hdn_manifestUrl" runat="server" />
    <input type="hidden" id="hdn_userId" runat="server" />
    <input type="hidden" id="hdn_custId" runat="server" />
    <input type="hidden" id="hdn_instanceMode" runat="server" />
    <input type="hidden" id="hdn_InstanceInfo" runat="server" />
    <input type="hidden" id="hdn_InstanceID" runat="server" />
    <input type="hidden" id="hdnCurrCalc" runat="server" />
    <input type="hidden" id="hdnUserInfo" runat="server" />
    <input type="hidden" id="hdnNewTabCalc" runat="server" value="" />
    <input type="hidden" id="hdnCalcRenderMode" runat="server" value="btnCalcRender_SamePage" />
    <input type="hidden" id="hdnWgtElement" />
    <input type="hidden" id="hdnMobileWidgetEnable" runat="server" value="1" />
    <input type="hidden" id="hdn_AuthToken" runat="server" />
    <input type="hidden" id="hdn_ClientType" runat="server" />
    <input type="hidden" id="hdn_ConnectionID" runat="server" />
    <input type="hidden" id="hdn_sharedUserMailID" runat="server" />
    <input type="hidden" id="hdnLogoutpath" runat="server" />
    <input type="hidden" id="hdn_Service" runat="server" />
    <input type="hidden" id="hdn_Setup" runat="server" />
    <input type="hidden" id="hdnEmailId" runat="server" />
    <input type="hidden" id="hdnWorksheetname" runat="server" />
    <input type="hidden" id="hdnWorknookname" runat="server" />
    <input type="hidden" id="hdnRangeAddress" runat="server" />


    <input type="hidden" id="hdn_ExcelRange" runat="server" />
    <input type="hidden" id="hdn_ExcelShareID" runat="server" />

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
    <div id="shareAppModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
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
    <input type="hidden" id="hdnSharedId" />
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="js/f2.min.js" type="text/javascript"></script>
    <script src="js/widgets/TermWidget/termWidgetScript.js" type="text/javascript"></script>
    <script src="js/widgets/TermWidget/termWidgetScriptMobile.js" type="text/javascript"></script>
    <script src="js/widgets/BasisWidget/basisWidgetScript.js" type="text/javascript"></script>
    <script src="js/VCMCommon.js" type="text/javascript"></script>
</body>
</html>
