<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="App.aspx.cs" Inherits="OpenF2.ESignalApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- Place favicon.ico and apple-touch-icon.png in the root directory -->
    <%--<link href="css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap-responsive.css" rel="stylesheet" type="text/css" />
    <link href="css/FinalStyleSheet.css" rel="stylesheet" type="text/css" />--%>
    <%--<script type="text/javascript" src="qrc:///webdot/webdot.js"></script>
    <script src="js/jqueryEsignal.js"></script>--%>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/site.css" rel="stylesheet" />
    
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <%--<link href="css/main.css" rel="stylesheet" type="text/css" />
    <link href="css/normalize.css" rel="stylesheet" type="text/css" />--%>
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
    <%--<link href="css/VCMComman.css" rel="stylesheet" type="text/css" />--%>
    <link href="js/vendor/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="js/vendor/bootstrap-datepicker.js" type="text/javascript"></script>
    <script src="js/plugins.js" type="text/javascript"></script>
    <script src="js/jquery.jmsajax.min.js" type="text/javascript"></script>
    <script src="js/jquery.numeric.js" type="text/javascript"></script>
    <script src="js/VolSeparate.js" type="text/javascript"></script>
    <script src="js/widgets/ShareCalculator.js" type="text/javascript"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDLOOAyBr_eEeocGCinFcoL-2jBlg52zEY&sensor=true" type="text/javascript"></script>
    <%-- For Highchats  --%>
    <%--<script src="js/chart/HighCharts/highcharts.js" type="text/javascript"></script>
    <script src="js/chart/HighCharts/exporting.js" type="text/javascript"></script>
    <script src="js/chart/HighCharts/chartCreateAndUpdate.js" type="text/javascript"></script>
    <script src="js/jquery.battatech.excelexport.js" type="text/javascript"></script>--%>
    <script src="js/jquery.base64.js" type="text/javascript"></script>

    <script src="js/bootstrap.js" type="text/javascript"></script>
    <script src="js/f2.min.js" type="text/javascript"></script>


    <script src="js/widgets/TermWidget/termWidgetScript.js" type="text/javascript"></script>
    <script src="js/widgets/TermWidget/termWidgetScriptMobile.js" type="text/javascript"></script>
    <script src="js/widgets/BasisWidget/basisWidgetScript.js" type="text/javascript"></script>
    <script src="js/VCMCommon.js" type="text/javascript"></script>



</head>
<body>
    <form id="form1" runat="server">
        <div class="calcHldr">
            <div class="sub-header">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <ul>
                               <%-- <li><div id="AppTitle" class="title" runat="server"></div></li>--%>
                                <li>Connection:<i id="SignalrStatus" class="fa fa-circle red"></i></li>
                                <li>Status:<i id="AppStatus" class="fa fa-circle red"></i></li>
                            </ul>
                        </div>

                       <%-- <div class="col-lg-6">
                            <div class="connection-text col-lg-8"></div>
                            <div class="connection-text col-lg-4"></div>
                        </div>--%>
                    </div>
                </div>
            </div>



            <div class="appDiv">
                <asp:Label Text="" runat="server" ID="lblErrorMsg" />
            </div>



            <%--<div class="span10 appDiv">

            <asp:Label Text="" runat="server" ID="lblErrorMsg" />

        </div>--%>

            <!--Footer-->

            <div id="myModal" class="modal hide fade modalDisconnect" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true">
                <%--<div class="modal-header">
                    <h3 id="myModalLabel">The Beast Apps</h3>
                </div>--%>
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


       <%-- <div id="ohlc">
<p>Open: <span id="open" /></p>
<p>High: <span id="high" /></p>
<p>Low: <span id="low" /></p>
<p>Close: <span id="close" /></p>
</div>--%>

    </form>

    <script type="text/javascript">
        var SignalRService = '';
        var isSafari = false;
        /*Geo map functions*/
        var isMapCreated = false;
        var marker;
        var mulmarker = [];
        var infoWindowDash = [];
        function success(lat, long) {
            var coords = new google.maps.LatLng(lat, long);
            var infowindow = new google.maps.InfoWindow({
                maxWidth: 500
            });
            if (marker) {
                marker.setPosition(coords);
                map.setCenter(coords);
                map.setZoom(15);
            }
            else {
                marker = new google.maps.Marker({
                    position: coords,
                    map: map,
                    icon: "images/marker.png",
                    title: 'Hello World!'
                });
                map.setCenter(coords);
                map.setZoom(15);
            }
            google.maps.event.addListener(marker, 'click', function () {
                infowindow.setContent($("#hdn_userId").val());
                infowindow.open(map, this);
            });

        }

        function setMarkers(mapDash) {
            try {
                var d = JSON.parse(locations);
                debugger;
                for (var i = 0; i < d.length; i++) {
                    var data = d[i];
                    latLng = new google.maps.LatLng(data.lat, data.lng);
                    bounds.extend(latLng);
                    if (mulmarker[i]) {
                        mulmarker[i].setPosition(latLng);
                    }
                    else {
                        mulmarker[i] = new google.maps.Marker({
                            position: latLng,
                            map: mapDash,
                            icon: "images/marker.png",
                            title: data.content
                        });

                        mapDash.fitBounds(bounds);

                        infoWindowDash[i] = new google.maps.InfoWindow({
                            content: data.content
                        });

                        google.maps.event.addListener(mulmarker[i], 'click', (function (marker, infoWindowDash) {
                            return function () {
                                infoWindowDash.open(mapDash, marker);
                            };
                        })(mulmarker[i], infoWindowDash[i]));
                    }
                }

            }
            catch (err) {
                console.log(err);
            }
        }

    </script>
    <script type="text/javascript">

        function CloseMarketHubOnLogout() {
            var customerID = "0";
            var userid = "0";
            var SpecialImageID = "0";
            if ($("#hdn_instanceMode").val() == "cust") {
                customerID = $("#hdn_instanceMode").val();
            }
            if ($("#hdn_instanceMode").val() == "user") {
                userid = $("#hdn_userId").val();
            }
            if ($("#hdn_instanceMode").val() == "spe") {
                SpecialImageID = "sp1";
            }
            CloseImageBeast("initiator", $("#hdn_ConnectionID").val(), userid, customerID, SpecialImageID, "0");

            closeMarketHub();
        }

        function toogleMenu() {

            if ($("#divHorizontalMenu").css("display") == "none") {
                $("#divHorizontalMenu").css("display", "block");
                $('#liCalcMenuDisplay').addClass('hLnkActive');
            }
            else {
                $("#divHorizontalMenu").css("display", "none");
                $('#liCalcMenuDisplay').removeClass('hLnkActive');
            }
        }

        function CloseCalculator(calcID) {
            try {
                var tmpObjs = [getObjects("InstType", calcID)];

                if (tmpObjs != null) {
                    if (tmpObjs[0].IsVisible == true) {
                        sendLeaveGroupRequest(tmpObjs[0].UserID, tmpObjs[0].CustID, tmpObjs[0].InstMode, tmpObjs[0].InstType, false);
                        removeAppAtClient(tmpObjs[0].InstType);
                    }
                }
                if (calcID == "vcm_calc_geoTrack") {
                    isMapCreated = false;
                    marker = null;
                    if (timer !== null) {
                        clearInterval(timer);
                        timer = null
                    }
                }
                if (calcID == "vcm_calc_geoTrackerDashboard") {
                    isDashMapCreated = false;
                    mulmarker = [];
                    if (timerDash !== null) {
                        clearInterval(timerDash);
                        timerDash = null
                    }
                }

                $("#menuItm_" + calcID).removeClass("disabled");
                $("#menuItm_" + calcID).unbind("click");
                $("#menuItm_" + calcID).bind("click", function () {
                    //ExampleMenu(this);
                });
            }
            catch (err) {
                var strerrordesc = "Function:CloseCalculator(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                onJavascriptLog("AppList.aspx", strerrordesc);
            }
        }

    </script>
    <script type="text/javascript">

        function removeAppDiv(varInstTyp) {
            try {

                $("div#mainProductSection div." + varInstTyp).remove();

                var tmpIndx = 1;

                for (tmpIndx; tmpIndx <= getCurrentRowIndex() ; tmpIndx++) {

                    if ($("#mainProductSection div#" + getRelativeRowName(tmpIndx) + " > div").length > 0) {

                    }
                    else {
                        $("#mainProductSection div#" + getRelativeRowName(tmpIndx) + "").remove();
                    }
                }
            }
            catch (err) {
                var strerrordesc = "Function:removeAppDiv(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                onJavascriptLog("AppList.aspx", strerrordesc);
            }
        }

        function removeAppFromContainer(varInstTyp) {
            F2.removeApp(varInstTyp);
        }

        function removeAppAtClient(varInstTyp) {
            removeAppFromContainer(varInstTyp);
            removeAppDiv(varInstTyp);
        }

        function ExampleMenu(obj) {

            try {
                $("#hdnMenuID").val($(obj).attr('id'));

                if ($("#hdnCalcRenderMode").val() == "btnCalcRender_NewTab") {
                    window.open("AppList.aspx?calc=" + $(obj).attr("id") + "");
                    return;
                }
                else if ($("#hdnCalcRenderMode").val() == "btnCalcRender_ReplaceLast") {

                    try {
                        var lstOpenInst = $("#hdn_instanceType").val();

                        if (lstOpenInst != "")
                            CloseCalculator(lstOpenInst);
                    }
                    catch (e) {

                    }
                }


                $(obj).addClass("disabled");
                $(obj).attr('onclick', '').unbind('click');
                $(obj).bind("click", function () {
                    return false;
                });

                var UserID_tmp = $("#hdn_userId").val();
                var CustomerID_tmp = $("#hdn_custId").val();
                var instanceMode_tmp = "";
                var instanceType_tmp = "";

                if ($(obj).attr("id") == "menuItm_vcm_calc_swaptionVolPremStrike") {

                    instanceMode_tmp = "spe";
                    instanceType_tmp = "vcm_calc_swaptionVolPremStrike";

                    if (!$(obj).hasClass("isCalcAvail")) {

                        var _appConfig_Single = [
                    {
                        appId: "vcm_calc_swaptionVolPremStrike",
                        description: "Swaption VolPrem Strike using web interface.",
                        name: "Vol Beast App",
                        manifestUrl: "Products/VCM/vcm_calc_swaptionVolPremStrike/manifest.js",
                        context: {
                            UserID: UserID_tmp,
                            CustomerID: CustomerID_tmp,
                            InstanceMode: instanceMode_tmp,
                            InstanceType: instanceType_tmp
                        }
                    }
                        ];

                        F2.registerApps(_appConfig_Single);
                    }

                    $("div#vcm_calc_swaptionVolPremStrike").removeClass("hide");

                    // $("#divAppDesc").html("USD Interest rate Swaption Premium Volatility Strike Grid with real time update.");
                }
                else if ($(obj).attr("id") == "menuItmSwapVol") {

                    $("div#vcm_calc_swaptionVol").removeClass("hide");
                    // $("#divAppDesc").html("USD Interest rate swaption volatility grid with real time update.");

                }
                else if ($(obj).attr("id") == "menuItmSwapPrem") {
                    $("div#vcm_calc_swaptionPrim").removeClass("hide");
                    // $("#divAppDesc").html("USD Interest rate swaption premium grid with real time update.");
                }
                else if ($(obj).attr("id") == "menuItmSwapStrike") {
                    $("div#vcm_calc_swaptionStrike").removeClass("hide");
                    // $("#divAppDesc").html("USD Interest rate swaption strike grid with real time update.");

                }
                else {
                    if ($(obj).attr("id") != undefined) {
                        instanceMode_tmp = "conn";
                        instanceType_tmp = $(obj).attr("id").split('menuItm_')[1];

                        if (!$(obj).hasClass("isCalcAvail")) {
                            var url = 'Products/VCM/';
                            url = url + $(obj).attr("id").split('menuItm_')[1] + '/manifest.js';
                            var _appConfig_Single = [
                                              {
                                                  appId: $(obj).attr("id").split('menuItm_')[1],
                                                  description: "CDS Calculator using web interface.",
                                                  name: "VolBeastApp",

                                                  manifestUrl: url,
                                                  context: {
                                                      UserID: UserID_tmp,
                                                      CustomerID: CustomerID_tmp,
                                                      InstanceMode: instanceMode_tmp,
                                                      InstanceType: instanceType_tmp
                                                  }
                                              }
                            ];

                            F2.registerApps(_appConfig_Single);
                        }

                        $("div#" + $(obj).attr("id").split('menuItm_')[1]).removeClass("hide");
                    }
                    else {
                        // GetLastOpenAppNameInfo("vcm_calc_swaptionVolPremStrike", $("#hdn_ClientType").val(), $("#hdn_AuthToken").val(), $("#hdn_userId").val());
                    }
                }

                $("#hdn_instanceMode").val(instanceMode_tmp);
                $("#hdn_instanceType").val(instanceType_tmp);

                var descLog = " UserID : " + $("#hdn_userId").val() + " ; CustomerID : " + $("#hdn_custId").val() + " ; Instance Mode : " + instanceMode_tmp + " ; Instance Type : " + instanceType_tmp + " ; Click on " + instanceType_tmp + " Menu";
                JavaScriptActivitiesLogs("Applist.aspx", instanceType_tmp + " MenuClick()", descLog);
            }
            catch (err) {
                var descLog = " UserID : " + $("#hdn_userId").val() + " ; CustomerID : " + $("#hdn_custId").val() + " ; Instance Mode : " + instanceMode_tmp + " ; Instance Type : " + instanceType_tmp + " ; Click on " + instanceType_tmp + " Menu";
                var strerrordesc = "Function:ExampleMenu(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                onJavascriptLog("AppList.aspx", "User Info : " + descLog + " </br> " + "Error Info : " + strerrordesc);
            }
        }


        function CalcInvoke(obj) {
            var UserID_tmp = $("#hdn_userId").val();
            var CustomerID_tmp = $("#hdn_custId").val();

            if (obj != "") {
                instanceMode_tmp = "conn";
                instanceType_tmp = (obj);

                //if (!$(obj).hasClass("isCalcAvail")) {
                var url = 'Products/VCM/';
                url = url + (obj) + '/manifest.js';
                var _appConfig_Single = [
                                  {
                                      appId: (obj),
                                      description: "CDS Calculator using web interface.",
                                      name: "VolBeastApp",

                                      manifestUrl: url,
                                      context: {
                                          UserID: UserID_tmp,
                                          CustomerID: CustomerID_tmp,
                                          InstanceMode: instanceMode_tmp,
                                          InstanceType: instanceType_tmp
                                      }
                                  }
                ];

                F2.registerApps(_appConfig_Single);
                //}

                $("div#" + (obj)).removeClass("hide");
            }
            else {
                // GetLastOpenAppNameInfo("vcm_calc_swaptionVolPremStrike", $("#hdn_ClientType").val(), $("#hdn_AuthToken").val(), $("#hdn_userId").val());
            }
        }
    </script>
    <script type="text/javascript">

        function redirectFocus() {
            $("#vcm_calc_ChatApp_402").focus();
        }
        (function () {
            //define AppConfigs
            //com_openf2_examples_csharp_helloworld
            //../content/scripts/manifest.js
            var _appConfigs = [
                {
                    appId: "vcm_calc_swaptionVolPremStrike",
                    description: "USD Interest rate swaption premium volatility strike grid with real time update.",
                    name: "DefaultBeastApp",
                    manifestUrl: "Products/VCM/vcm_calc_swaptionVolPremStrike/manifest.js" //note the path to your manifest! 
                }

            ];

            /**
            * Setup ContainerConfig
            * The appRender() method allows for customizing where apps are inserted.
            * In this example, appRender() would insert apps after the <h1> element.
            */
            F2.init({
                beforeAppRender: function (app) {

                    var appRoot = '<div id="mainProductSection" ><div class="wprow" id="' + getCurrentRowName() + '"></div></div>';

                    if ($("#mainProductSection").length == 0) {
                        $(appRoot).appendTo('div.appDiv');
                    }


                    return appRoot;
                },

                afterAppRender: function (app, html) {
                    debugger;
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


                    //var closeBtnHtml = '<div class="notfirst"><button onclick="CloseCalculator(\'' + app.appId + '\')" style="float:right;" type="button" class="close">&times;</button><div style="float:right;height:30px;"> </div></div>';
                    var closeBtnHtml = '<div class="notfirst"></div>';
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
                        $("#mainProductSection").append("<div id='" + getCurrentRowName() + "'></div>");
                        ele = $("#mainProductSection #" + getCurrentRowName() + "");
                    }
                    if (app.appId == "vcm_calc_cmefuture") {
                        var closeBtnHtmlTmp = '<div class="notfirst"><button onclick="CloseCalculator(\'' + app.appId + '\')" style="float:right;" type="button" class="close">&times;</button></div>';
                        $(ele).append(html).find("#" + app.appId + " > tbody > tr:first-child > td:last-child").append(closeBtnHtmlTmp);

                        closeBtnHtmlTmp = '<div class="notfirst calcStatusFont">Status: <img style="padding-right:15px;" class="navbar-stus-img-per-img imgCalculatorStatus" src="images/red.png" alt="" /></div>';
                        $(ele).find("#BI_CMEFuture1 > tbody > tr:first-child > td:last-child").append(closeBtnHtmlTmp);
                        $(ele).find("#BI_CMEFuture2 > tbody > tr:first-child > td:last-child").append(closeBtnHtmlTmp);
                        $(ele).find("#BI_CMEFuture3 > tbody > tr:first-child > td:last-child").append(closeBtnHtmlTmp);
                        $(ele).find("#BI_CMEFuture4 > tbody > tr:first-child > td:last-child").append(closeBtnHtmlTmp);
                    }
                    if (app.appId == "vcm_calc_bond_analyzer") {
                        $(ele).append(html).find("#" + app.appId + " > tbody > tr:first-child > td:last-child > table > tbody > tr:first-child > td:last-child").append(closeBtnHtml);
                    }
                    else if (app.appId == "vcm_calc_bondYield_Manual" || app.appId == "vcm_calc_bondYield_Manual_2" || app.appId == "vcm_calc_bondYield_Manual_3") {
                        $(ele).append(html).find("#" + app.appId + " > tbody > tr:first-child > td:first-child > table >tbody>tr:first-child>td:first-child").append(closeBtnHtml);
                        $(ele).find("#" + app.appId + " .ServerName").append(statusimg);
                    }
                    else if ((app.appId == "vcm_calc_infosys") || (app.appId == "vcm_calc_infosys_new")) {
                        $(ele).append(html).find("#" + app.appId + " > tbody > tr:first-child > td:last-child > table > tbody > tr:first-child > td:last-child").append(closeBtnHtml);
                        $(ele).find("#" + app.appId + " .ServerName").append(statusimg);
                    }
                    else {
                        $(ele).append(html).find("#" + app.appId + " > tbody > tr:first-child > td:last-child").append(closeBtnHtml);
                        $(ele).find("#" + app.appId + " .ServerName").append(statusimg);

                    }

                    if (isSafari) {
                        $("#" + app.appId).find(".imgCalculatorStatus").hide();
                        $("#" + app.appId).find(".calcStatusFont").append("<strong id='textConnectStatusImage' style='color:Red;font-size:12px;'>D</strong>");
                    }
                    return ele;
                },
                UI: {
                    Mask: {
                        loadingIcon: 'images/ajax-loader.gif'
                    }
                }

                    , secureAppPagePath: "foo.html"
            });


        })();
        function CloseSearch(obj) {

            $('#' + obj.id.substring(3, obj.length)).hide();
            if (obj.id == 'btnsearchlst') {
                $('#bondYield_Manual').show();
            }

        }
        function ShowSearchList() {
            debugger;
            $('#searchlst').show();
            $('#bondYield_Manual').hide();


        }

        function ShowSearchListeSignal() {
            debugger;
            $('#searchlst').show();
            $('#BYCE0252').hide();


        }

        function ShowSearchList2() {
            $('#searchlst2').show();
            $('.bondYield_Manual2').hide();

        }
        function ShowSearchList3() {
            $('#searchlst3').show();

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

            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script');
            script.type = 'text/javascript';
            var hashes = window.location.href;
            SignalRService = $("#hdn_SignalRSetup").val() + "/Services/Service.asmx";
            var SignalRSetup = $("#hdn_SignalRSetup").val() + "/signalr/hubs";
            var SignalRConntectionUrl = $("#hdn_SignalRSetup").val() + "/signalr";

            script.src = SignalRSetup;
            head.appendChild(script);
            setVariableValue(SignalRService, SignalRConntectionUrl);



            $('#dvPriceWidgetMobile').draggable({ handle: "#dvPwmTitle" });
            $('#dvTermWidgetMobile').draggable({ handle: "#dvTwmTitle" });

            $(window).resize(checkWindowResize);
            $(window).scroll(checkWindowResize);

            $('#form1').append(CS_BoxPlaceHolder);



            $(".btnCalcRenderMode").click(function () {
                $(".btnCalcRenderMode").removeClass("btn-primary");
                $(this).addClass("btn-primary");
                $("#hdnCalcRenderMode").val($(this).attr("id"));
            });

            var varNewTabCalc = $("#hdnNewTabCalc").val();
            var GlobalCategoryID = 0;
            CalcInvoke(varNewTabCalc);

            //GetCategoryId("12", "#menuItm_" + "vcm_calc_CdsCalculator");
            //if (varNewTabCalc == "") {
            //    // getLastOpenCalc($("#hdn_userId").val(), $("#hdn_ClientType").val(), $("#hdn_AuthToken").val());

            //}
            //else {
            //    //var menuObj = $("#" + varNewTabCalc);
            //    var menuObj = varNewTabCalc.substring(8, varNewTabCalc.length)
            //    // GetLastOpenAppNameInfo(menuObj, $("#hdn_ClientType").val(), $("#hdn_AuthToken").val(), $("#hdn_userId").val());
            //}
            $(window).unload(function () {
                var customerID = "0";
                var userid = "0";
                var SpecialImageID = "0";
                if ($("#hdn_instanceMode").val() == "cust") {
                    customerID = $("#hdn_instanceMode").val();
                }
                if ($("#hdn_instanceMode").val() == "user") {
                    userid = $("#hdn_userId").val();
                }
                if ($("#hdn_instanceMode").val() == "spe") {
                    SpecialImageID = "sp1";
                }

                CloseImageBeast("initiator", $("#hdn_ConnectionID").val(), userid, customerID, SpecialImageID, "0", onBeforeUnload_Success, onBeforeUnload_Error);


                return undefined;
            });

            function onBeforeUnload_Success(response) {
                closeMarketHub();
            }
            function onBeforeUnload_Error(response) {

            }

            window.onbeforeunload = function (e) {

                var customerID = "0";
                var userid = "0";
                var SpecialImageID = "0";
                if ($("#hdn_instanceMode").val() == "cust") {
                    customerID = $("#hdn_instanceMode").val();
                }
                if ($("#hdn_instanceMode").val() == "user") {
                    userid = $("#hdn_userId").val();
                }
                if ($("#hdn_instanceMode").val() == "spe") {
                    SpecialImageID = "sp1";
                }
                CloseImageBeast("initiator", $("#hdn_ConnectionID").val(), userid, customerID, SpecialImageID, "0", onBeforeUnload_Success, onBeforeUnload_Error);

                return undefined;
            };
        });

        function funwait() {

        }


    </script>

    <%--Esignal Script--%>
    <%--<script>
        function updateData() {
            Mqd.datafeed.requestQuote(Mqd.content.symbol.value(), ["Open", "High",
           "Low", "Last"], false).done(function (resp) {
               $("#open").html(resp.data.Open);
               $("#high").html(resp.data.High);
               $("#low").html(resp.data.Low);
               $("#close").html(resp.data.Last);
               //alert("Update data Called");
           });
        }
        Mqd.header.symbol.bind('activated', function (combo) {
            //alert("Act Called");
            Mqd.content.symbol.value(combo.value());
            updateData();
        });
        Mqd.header.symbol.value(Mqd.content.symbol.value());
 </script>--%>

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
