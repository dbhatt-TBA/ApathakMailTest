
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
            ExampleMenu(this);
        });
    }
    catch (err) {
        var strerrordesc = "Function:CloseCalculator(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

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
        //JavaScriptActivitiesLogs("Applist.aspx", instanceType_tmp + " MenuClick()", descLog);
    }
    catch (err) {
        var descLog = " UserID : " + $("#hdn_userId").val() + " ; CustomerID : " + $("#hdn_custId").val() + " ; Instance Mode : " + instanceMode_tmp + " ; Instance Type : " + instanceType_tmp + " ; Click on " + instanceType_tmp + " Menu";
        var strerrordesc = "Function:ExampleMenu(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", "User Info : " + descLog + " </br> " + "Error Info : " + strerrordesc);
    }
}

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

            var appRoot = '<div id="mainProductSection" class="well span12"><div class="span12 wprow" id="' + getCurrentRowName() + '"></div></div>';

            if ($("#mainProductSection").length == 0) {
                $(appRoot).appendTo('div.appDiv');
            }


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


            var closeBtnHtml = '<div class="shareButtonPosition"><button class="btn btn-mini btn-primary imgShareButton" disabled="disabled" onclick="CS_Func_ShowMultiEmailBox(\'' + app.context.UserID + '\', \'' + app.context.CustomerID + '\', \'' + app.context.InstanceType + '\', \'' + app.context.InstanceMode + '\')" type="button">Share</button></div><div class="notfirst"><button onclick="CloseCalculator(\'' + app.appId + '\')" style="float:right;" type="button" class="close">&times;</button><div style="float:right;height:30px;"> </div></div>';
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
    if (obj.id == 'btnsearchlst2') {
        $('.bondYield_Manual2').show();
    }

}
function ShowSearchList() {
    $('#searchlst').show();


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
    //SignalRService = "Services/Service.asmx";
    //var SignalRSetup = "signalr/hubs";
    var SignalRSetup = $("#hdn_SignalRSetup").val() + "/signalr/hubs";
    var SignalRConntectionUrl = $("#hdn_SignalRSetup").val() + "/signalr";
    //var SignalRConntectionUrl = "signalr";
    //alert("Signalr setup:" + SignalRSetup);
    //alert("Signalr service:" + SignalRService);
    //if (hashes.indexOf('localhost:') > 0) {

    //    script.src = SignalRSetup;

    //}
    //else {

    script.src = SignalRSetup;
    //}
    head.appendChild(script);
    setVariableValue(SignalRService, SignalRConntectionUrl);



    $('#dvPriceWidgetMobile').draggable({ handle: "#dvPwmTitle" });
    $('#dvTermWidgetMobile').draggable({ handle: "#dvTwmTitle" });

    $(window).resize(checkWindowResize);
    $(window).scroll(checkWindowResize);

    $('#form1').append(CS_BoxPlaceHolder);

    BindMenu();

    $(".btnCalcRenderMode").click(function () {
        $(".btnCalcRenderMode").removeClass("btn-primary");
        $(this).addClass("btn-primary");
        $("#hdnCalcRenderMode").val($(this).attr("id"));
    });

    var varNewTabCalc = $("#hdnNewTabCalc").val();
    var GlobalCategoryID = 0;
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




function GetCategoryId(tmpCategoryID, varInstanceId) {
    try {
        var CategoryId = tmpCategoryID;
        $("#hdnCategoryID").val(CategoryId);
        $("#hdnMenuID").val(varInstanceId);
        openLastCalc();
    }
    catch (err) {
        var strerrordesc = "Function:GetCategoryId(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function openLastCalc() {

    $("#Divid_" + $("#hdnCategoryID").val()).html('');

    GlobalCategoryID = parseInt($("#hdnCategoryID").val());
    var varUserID = $("#hdn_userId").val();

    var url = $.jmsajaxurl({
        url: SignalRService,
        method: "GetSubMenuCategory",
        data: { CategoryId: $("#hdnCategoryID").val(), UserId: varUserID, ClientType: $("#hdn_ClientType").val(), GUID: $("#hdn_AuthToken").val() }
    });

    $.ajax({
        cache: false,
        dataType: "jsonp",
        url: url + "&format=json",
        success: function (data) {

            var CalcID = "Calc" + $("#hdnCategoryID").val();
            //  alert(CalcID);
            var listHtml = "";
            listHtml = " <ul id=" + CalcID + " class='nav nav-pills'>";
            $.each(data.d, function (key, value) {
                if (value.AppName == 'False') {
                    alert(value.AppTitle);  //Alert reson for invalid
                    window.location.href = $('#hdnLogoutpath').val();
                    return false;
                }
                var id = "menuItm_" + value.AppName;
                listHtml += " <li onclick='ExampleMenu(this)' id=" + id + " class='menulinks' >";
                listHtml += "<a href='#'>" + value.AppTitle + "</a>";
                listHtml += "</li>";
            });

            listHtml += " </ul>";

            $("#Divid_" + $("#hdnCategoryID").val()).append(listHtml);

            $("#Divid_" + $("#hdnCategoryID").val()).removeClass('hide');
            $("#Divid_" + $("#hdnCategoryID").val()).addClass('show');



            if ($('#hdnMenuID').val() != '') {
                var menuObj = $("#hdnMenuID").val(); //$("#" + $("#hdnMenuID").val());
            }
            else {
                var menuObj = $("#menuItm_vcm_calc_CdsCalculator"); //$("#" + $("#hdnNewTabCalc").val()); $("#" + varNewTabCalc)
            }
            setTimeout("funwait", 1000);
            ExampleMenu($(menuObj));

        },
        error: function (request, status, error) {
            //alert(error);
            GetLastOpenAppNameInfo("vcm_calc_CdsCalculator", $("#hdn_ClientType").val(), $("#hdn_AuthToken").val(), $("#hdn_userId").val());
        }
    });
}
function funwait() {

}
function BindMenu() {

    try {
        //alert("Service:" + SignalRService);
        var varUserID = $("#hdn_userId").val();

        var url = $.jmsajaxurl({
            url: SignalRService,
            method: "GetMainMenuCategory",
            data: { UserId: varUserID, ClientType: $("#hdn_ClientType").val(), GUID: $("#hdn_AuthToken").val() }
        });

        $.ajax({
            cache: false,
            dataType: "jsonp",
            url: url + "&format=json",
            success: function (tmpData) {

                var listHtml = "";

                listHtml = "<div class='smallDivider'> </div>";
                listHtml += " <div id='divCategory' class='span12' style='margin-left: 5px;'>";
                listHtml += " <span id='Category' class='page-header' style='cursor: pointer'> Apps</span>   ";
                listHtml += "<img src='images/RefreshMenu.png' alt='Alternate Text' style='cursor: pointer' onclick='setdirimg()' />";
                listHtml += " <div class='row-fluid'>";
                listHtml += " <div class='span9 offset1'>";
                listHtml += " <ul id='ulCategoryList' class='nav nav-pills-custom'>";

                $.each(tmpData.d, function (key, value) {
                    if (value.CategoryId == 'False') {
                        alert(value.CategoryName);  //Alert reson for invalid
                        window.location.href = $('#hdnLogoutpath').val();
                        return false;
                    }
                    listHtml += " <li>";
                    listHtml += " <a href='#' style='color: black' class='CategoryCalc'  id=" + value.CategoryId + ">" + value.CategoryName + "</a>";
                    var Divid = "Divid_" + value.CategoryId;
                    listHtml += " <div id=" + Divid + "  class='spna11 offset1 hide'></div>";
                    listHtml += "</li>";


                });

                listHtml += "</ul>";
                listHtml += "</div>";
                listHtml += "</div>";
                listHtml += "</div>";

                $('.exampleCaptions').html(listHtml);

                attachEventToMenu();

                var menuObj = $("#menuItm_vcm_calc_CdsCalculator");
                ExampleMenu($(menuObj));
            },
            error: function (request, status, error) {
                alert(error);
            }
        });
    }
    catch (err) {
        var strerrordesc = "Function:BindMenu(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function attachEventToMenu() {

    try {
        var GlobalCategoryID = 0;


        if ($("#hdnCategoryID").val() == "") {
            GlobalCategoryID = 0;
        }
        else {
            GlobalCategoryID = parseInt($("#hdnCategoryID").val());
        }

        $('#Category').click(function () {
            $("#ulCategoryList").slideToggle(1000);
        });

        $(".CategoryCalc").click(function () {

            // alert($(this)[0].id);
            var CategoryId = $(this)[0].id;

            $("#Calc" + GlobalCategoryID).slideToggle(1000);

            if (GlobalCategoryID == CategoryId) {
                GlobalCategoryID = 0;
                $("#GlobalCategoryID").val(GlobalCategoryID);
            }
            else {


                $("#Calc" + CategoryId).slideToggle(1000);
                GlobalCategoryID = CategoryId;

                if ($(this)[0].id > 0) {
                    $("#Divid_" + CategoryId).html('');
                    var varUserID = $("#hdn_userId").val();

                    var url = $.jmsajaxurl({
                        url: SignalRService,
                        method: "GetSubMenuCategory",
                        data: { CategoryId: $(this)[0].id, UserId: varUserID, ClientType: $("#hdn_ClientType").val(), GUID: $("#hdn_AuthToken").val() }

                    });

                    $.ajax({
                        cache: false,
                        dataType: "jsonp",
                        url: url + "&format=json",
                        success: function (data) {

                            var CalcID = "Calc" + CategoryId;
                            //  alert(CalcID);
                            var listHtml = "";
                            listHtml = " <ul id=" + CalcID + " class='nav nav-pills'>";
                            $.each(data.d, function (key, value) {
                                if (value.AppName == 'False') {
                                    alert(value.AppTitle);  //Alert reson for invalid
                                    window.location.href = $('#hdnLogoutpath').val();
                                    return false;
                                }
                                var id = "menuItm_" + value.AppName;
                                listHtml += " <li onclick='ExampleMenu(this)' id=" + id + " class='menulinks' >";
                                listHtml += "<a href='#'>" + value.AppTitle + "</a>";
                                listHtml += "</li>";
                            });

                            listHtml += " </ul>";

                            $("#Divid_" + CategoryId).append(listHtml);
                            $("#Divid_" + CategoryId).removeClass('hide');
                            $("#Divid_" + CategoryId).addClass('show');

                            if (CategoryId != $("#hdnCategoryID").val()) {
                                $("#Divid_" + $("#hdnCategoryID").val()).removeClass('show');
                                $("#Divid_" + $("#hdnCategoryID").val()).addClass('hide');

                            }
                            else {

                                $("#Divid_" + $("#hdnCategoryID").val()).removeClass('hide');
                                $("#Divid_" + $("#hdnCategoryID").val()).addClass('show');

                            }

                            var allCalc = getLoadedCalcId();
                            var visibleCalc = allCalc.split(',');
                            $.each(visibleCalc, function () {
                                var calcId = this;
                                if (calcId !== "") {
                                    $("#menuItm_" + calcId).addClass("disabled");
                                    $("#menuItm_" + calcId).attr('onclick', '').unbind('click');
                                    $("#menuItm_" + calcId).bind("click", function () {
                                        return false;
                                    });
                                }
                            });

                        },
                        error: function (request, status, error) {
                            alert(error);
                        }
                    });
                }
                //                }
            }
        });
        //}
    }
    catch (err) {
        var strerrordesc = "Function:attachEventToMenu(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function setdirimg() {
    var url = $.jmsajaxurl({
        url: SignalRService,
        method: "SetDirImgSIDNull",
        //data: { UserId: "0", ClientType: $("#hdn_ClientType").val(), GUID: $("#hdn_AuthToken").val() }
        data: {}
    });

    BindMenu();

    $.ajax({
        cache: false,
        dataType: "jsonp",
        url: url + "&format=json",
        success: function (tmpData) {
        },
        error: function (request, status, error) {

        }
    });
}

