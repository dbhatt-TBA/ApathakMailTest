var marketHub;
var isHubSet = false;
var wasConnLost = false;
var isModalOpen = false;
var isBusyModalOpen = false;
var curntSpanIndex = 1;

var isSwapGrid_SpeVar = true;

var calcHash = {};

var isSWPVisible = true;

var visCalcName = "";
var signalService;
var signalhub;
function setVariableValue(signalServicetest, signalhubtest) {
    signalService = signalServicetest;
    signalhub = signalhubtest;
}
function getCurrentRowName() {
    return "rowNum" + curntSpanIndex;
}

function getRelativeRowName(spnIndx) {
    return "rowNum" + spnIndx;
}

function getCurrentRowIndex() {
    return curntSpanIndex;
}

function IncrCurrentRowIndex() {
    curntSpanIndex++;
}



function handleIncomingMessageFromBeastSignal(name, message) {
    // debugger;
    try {
        if (name == "premdata") {
            updatePrimGridDataIDVise(message, "signalr");
        }
        else if (name == "strikedata") {
            updateStrikeGridDataIDVise(message, "signalr");
        }
        else if (name == "voldata") {
            updateVolGridDataIDVise(message, "signalr");
        }
        else if (name == "otherdata") {
            updateOtherData(message);
        }
    }
    catch (err) {
        var strerrordesc = "Function:handleIncomingMessageFromBeastSignal(); name :" + name + "; message : " + message + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function setJSFunctionsForHTML(ReqInstanceType, UserID, CustomerID, instanceMode) {

    //    alert(ReqInstanceType);
    //    alert(UserID);
    //    alert(CustomerID);
    //    alert(instanceMode);
    try {
        $("#" + ReqInstanceType + " input[type='text']").focusout(function () {
            if (ReqInstanceType == 'vcm_calc_ChatApp') {
                return false;
            }

            var valFrmTitle = $(this).attr("name");

            if ($(this).attr("title") != "datepick" && $(this).hasClass("priceWidget") == false && $(this).hasClass("termWidget") == false && $(this).hasClass("basisWidget") == false && $(this).hasClass("inputDisable") == false) {
                //if ($(this).attr("title") != "datepick" && $(this).hasClass("inputDisable") == false) {
                var eleDispVal = $(this).val();
                $(this).val(valFrmTitle).attr("name", eleDispVal);
            }
        });

        $("#" + ReqInstanceType + " input[type='text']").focus(function () {
            if (ReqInstanceType == 'vcm_calc_ChatApp') {
                return false;
            }

            var valFrmTitle = $(this).attr("name");

            if ($(this).attr("title") != "datepick" && $(this).hasClass("priceWidget") == false && $(this).hasClass("termWidget") == false && $(this).hasClass("basisWidget") == false && $(this).hasClass("inputDisable") == false) {
                //if ($(this).attr("title") != "datepick" && $(this).hasClass("inputDisable") == false) {
                var eleDispVal = $(this).val();
                $(this).val(valFrmTitle).attr("name", eleDispVal);
            }
        });
    }
    catch (err) {
        var strerrordesc = "Function:setJSFunctionsForHTML(); ReqInstanceType :" + ReqInstanceType + "; UserID :" + UserID + "; CustomerID :" + CustomerID + ";InstanceMode : " + instanceMode + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }

}

function closeMarketHub() {
    $.connection.hub.stop();
}

function setupSignalRGeneric(ReqInstanceType, UserID, CustomerID, instanceMode) {
    //debugger;
    try {

        //        if (isBusyModalOpen == true) {
        //            $('#busyModal').modal('hide');
        //            isBusyModalOpen = false;
        //        }

        if (marketHub == null) {
            marketHub = $.connection.marketDataHub;
            $.connection.hub.url = signalhub; //"https://www.thebeastapps.com/beastappscore/signalr";

            /* Define servermethods before hub start */

            //handle connection state
            //SWAPTION VOL PREM
            marketHub.client.handleIncomingMessageFromBeastSignalR = function (name, message) {
                handleIncomingMessageFromBeastSignal(name, message);
            };

            //REST OF THE CALCULATOR
            marketHub.client.MessageFromServer = function (updtTyp, updtEleTyp, eleID, eleVal, elePrntID) {
                processServerMessageGeneric(updtTyp, updtEleTyp, eleID, eleVal, elePrntID);
            };
            //REST OF THE CALCULATOR
            marketHub.client.ArrayFromServerGrid = function (d, HtmlClientID) {
                /*SharedBeastApps.aspx method*/
                ExcelShare(d, HtmlClientID);
            };

            /* Define servermethods before hub start */
            $.connection.hub.start({ jsonp: true })
        .done(function () {
            isHubSet = true;
        })
        .fail(function () {
            F2.log("Could not connect!");
            var strdesc = "InstanceMode Name: " + ReqInstanceType + ", " + "UserID: " + UserID + "CustomerID: " + CustomerID + ", " + "InstanceMode: " + instanceMode + ", " + " Could not connect!";
            JavaScriptActivitiesLogs("VolSeparate.js", "setupSignalRGeneric()", strdesc);
            setTimeout(function () { setupSignalRGeneric(ReqInstanceType, UserID, CustomerID, instanceMode); }, 1500);
        });
        }

        //handle connection state
        $.connection.hub.stateChanged(function (change) {
            // connecting: 0,
            // connected: 1,
            // reconnecting: 2,
            // disconnected: 4

            //alert(change.newState);

            switch (change.newState) {
                case 0: // connecting
                    //$("#status").text("Connecting..");
                    $("#imgConnect").attr("src", "images/yellow.png");
                    if (isSafari) {
                        $("#textConnectStatus").text("C");
                        $("#textConnectStatus").css('color', 'Yellow');
                    }
                    var strdesc = "InstanceMode Name: " + ReqInstanceType + ", " + "UserID: " + UserID + "CustomerID: " + CustomerID + ", " + "InstanceMode: " + instanceMode + ", " + " connecting!";
                    JavaScriptActivitiesLogs("VolSeparate.js", "setupSignalRGeneric()", strdesc);
                    break;
                case 1: // connected
                    $("#status").removeClass("disconnected").addClass("connected"); //.text("Connected");
                    $("#imgConnect").attr("src", "images/green.png");

                    if (isModalOpen == true) {
                        isModalOpen = false;
                        $('#myModal').modal('hide');
                    }
                    if (isSafari) {
                        $("#textConnectStatus").text("C");
                        $("#textConnectStatus").css('color', 'Green');
                    }
                    var strdesc = "InstanceMode Name: " + ReqInstanceType + ", " + "UserID: " + UserID + "CustomerID: " + CustomerID + ", " + "InstanceMode: " + instanceMode + ", " + " connected!";
                    JavaScriptActivitiesLogs("VolSeparate.js", "setupSignalRGeneric()", strdesc);

                    break;
                case 2: // reconnecting
                    //$("#status").text("Reconnecting");
                    $("#imgConnect").attr("src", "images/yellow.png");
                    if (isSafari) {
                        $("#textConnectStatus").text("C");
                        $("#textConnectStatus").css('color', 'Yellow');
                    }
                    wasConnLost = true;
                    //alert(isModalOpen);
                    if (isModalOpen == false) {
                        isModalOpen = true;
                        $('#myModal').modal({ keyboard: false, backdrop: 'static' });
                    }

                    var strdesc = "InstanceMode Name: " + ReqInstanceType + ", " + "UserID: " + UserID + "CustomerID: " + CustomerID + ", " + "InstanceMode: " + instanceMode + ", " + "reconnecting!";
                    JavaScriptActivitiesLogs("VolSeparate.js", "setupSignalRGeneric()", strdesc);
                    break;
                case 4: // disconnected
                    $("#status").removeClass("connected").addClass("disconnected"); //.text("Disconnected");
                    $("#imgConnect").attr("src", "images/red.png");
                    if (isSafari) {
                        $("#textConnectStatus").text("C");
                        $("#textConnectStatus").css('color', 'Red');
                    }
                    wasConnLost = true;

                    var strdesc = "InstanceMode Name: " + ReqInstanceType + ", " + "UserID: " + UserID + "CustomerID: " + CustomerID + ", " + "InstanceMode: " + instanceMode + ", " + " disconnected!";
                    JavaScriptActivitiesLogs("VolSeparate.js", "setupSignalRGeneric()", strdesc);
                    break;
            }
        });


        if (isHubSet == true) {
            var paraValues = UserID + "#" + CustomerID + "#" + instanceMode;

            setJSFunctionsForHTML(ReqInstanceType, UserID, CustomerID, instanceMode);
            addEditCalcParameters(UserID, CustomerID, instanceMode, ReqInstanceType, true);

            SendValueToBeast(ReqInstanceType, paraValues);
        }
        else {
            setTimeout(function () { setupSignalRGeneric(ReqInstanceType, UserID, CustomerID, instanceMode); }, 1500);
        }
    }
    catch (err) {
        var strerrordesc = "Function:setupSignalRGeneric(); ReqInstanceType :" + ReqInstanceType + "; UserID :" + UserID + "; CustomerID :" + CustomerID + ";InstanceMode : " + instanceMode + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}
function parseXML(text) {
    var parser, xmlDoc;
    if (window.DOMParser) {
        parser = new DOMParser();
        xmlDoc = parser.parseFromString(text, "text/xml");
    } else {  // IE
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = "false";
        xmlDoc.loadXML(text);
    }
    return xmlDoc;
}

function SendValueToBeast(name, message) {

    marketHub.server.send(name, message, $("#hdn_AuthToken").val(), $("#hdn_ClientType").val());
}
function SendToBeast(name, message) {

    marketHub.server.sendbeast(name, message, $("#hdn_AuthToken").val(), $("#hdn_ClientType").val() + '!' + $('#hdnEmailId').val());
}
function CloseImageBeast(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID, Isshare) {
    try {

        if (ProductID == "initiator") {
            ProductID = "";
            $.each(calcHash, function () {
                var testObject = this;
                if (testObject.IsVisible == true) {
                    ProductID = ProductID + "," + testObject.InstType;
                }

            });
        }


        marketHub.server.closeimage(ProductID, ConnectionID, UserID, CustomerID, SpecialImageID);

        if (Isshare == "1") {
            marketHub.server.setimageclosetime($("#hdn_sharedUserMailID").val(), $("#hdn_AuthToken").val(), $("#hdn_ConnectionID").val(), ProductID);
        }
        else {
            marketHub.server.setimageclosetime($("#hdn_userId").val(), $("#hdn_AuthToken").val(), ConnectionID, ProductID);
        }
    }
    catch (err) {
        var strerrordesc = "Function:CloseImageBeast();" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);

    }
}
function sendShareImageRequest(ReqInstanceType, UserID, CustomerID, instanceMode, userToShare, initiatorEmailId, msgFromInitiator) {
    try {
        //debugger;

        var strdesc = "InstanceMode Name: " + ReqInstanceType + ", " + "UserID: " + UserID + "CustomerID: " + CustomerID + ", " + "InstanceMode: " + instanceMode + ", " + "userToShare: " + userToShare + ", initiatorEmailId : " + initiatorEmailId + ", MsgFromInitiator : " + msgFromInitiator;
        JavaScriptActivitiesLogs("VolSeparate.js", "sendShareImageRequest()", strdesc);

        var paraValues = UserID + "#" + CustomerID + "#" + instanceMode;
        //marketHub.server.sharerequest(ReqInstanceType, paraValues, userToShare, initiatorEmailId, msgFromInitiator);
        marketHub.server.sharerequest(ReqInstanceType, paraValues, userToShare, initiatorEmailId, $("#hdn_AuthToken").val(), $("#hdn_ClientType").val());
    }
    catch (err) {
        var strerrordesc = "Function:sendShareImageRequest(); ReqInstanceType :" + ReqInstanceType + "; UserID :" + UserID + "; CustomerID :" + CustomerID + ";InstanceMode : " + instanceMode + "; userToShare :" + userToShare + ";initiatorEmailId : " + initiatorEmailId + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function sendLeaveGroupRequest(varUID, varCustID, varInstMod, varInstTyp, varIsVisible) {
    try {
        //debugger;

        var strdesc = "InstanceMode Name: " + varInstTyp + ", " + "UserID: " + varUID + "CustomerID: " + varCustID + ", " + "InstanceMode: " + varInstMod + ", " + " Close Calc!";
        JavaScriptActivitiesLogs("VolSeparate.js", "sendLeaveGroupRequest()", strdesc);

        var tmpInstTyp = varInstTyp;

        //    if (varInstTyp == "vcm_calc_swaptionVolPremStrike")
        //        tmpInstTyp = "vcm_calc_swaptionVolPremStrike";

        var paraValues = varUID + "#" + varCustID + "#" + varInstMod;

        marketHub.server.unJoinGroupExplicit(tmpInstTyp, paraValues, $("#hdn_AuthToken").val(), $("#hdn_ClientType").val());
        addEditCalcParameters(varUID, varCustID, varInstMod, varInstTyp, varIsVisible);
    }
    catch (err) {
        var strerrordesc = "Function:sendLeaveGroupRequest(); InstanceType :" + varInstTyp + "; UserID :" + varUID + "; CustomerID :" + varCustID + ";InstanceMode : " + varInstMod + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function addEditCalcParameters(varUID, varCustID, varInstMod, varInstTyp, varIsVisible) {
    try {
        //debugger;
        var tmpObj = {};
        var isObjExists = false;

        for (var i in calcHash) {

            $.map(calcHash, function (valueT, keyT) {
                if (varInstTyp === keyT) {
                    calcHash[varInstTyp].IsVisible = varIsVisible;
                    isObjExists = true;
                }
            });

            if (isObjExists == true)
                break;
        }

        if (isObjExists == false) {

            //        if (varInstTyp == "vcm_calc_swaptionVolPremStrike")
            //            varInstTyp = "vcm_calc_swaptionVolPremStrike";

            tmpObj["UserID"] = varUID;
            tmpObj["CustID"] = varCustID;
            tmpObj["InstMode"] = varInstMod;
            tmpObj["IsVisible"] = varIsVisible;
            tmpObj["InstType"] = varInstTyp;

            calcHash[varInstTyp] = tmpObj;
        }
    }
    catch (err) {
        var strerrordesc = "Function:addEditCalcParameters(); InstanceType :" + varInstTyp + "; UserID :" + varUID + "; CustomerID :" + varCustID + ";InstanceMode : " + varInstMod + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}


function getObjects(key, val) {
    var retv = [];
    var newObj = null;
    $.each(calcHash, function () {
        var testObject = this;
        if (testObject.InstType == val) {
            newObj = testObject;
        }
    });

    return newObj;
}

function populateCalcProp() {
    $("#tblCalcObjValues").append("<tr><td colspan='5'>=====================================================</td></tr>");
    $.each(calcHash, function () {
        var testObject = this;
        $("#tblCalcObjValues").append("<tr><td>" + testObject.UserID + "</td><td>" + testObject.CustID + "</td><td>" + testObject.InstMode + "</td><td>" + testObject.IsVisible + "</td><td>" + testObject.InstType + "</td></tr>");
    });
}

function getLoadedCalcId() {

    var retVal = "";

    $.each(calcHash, function () {
        var testObject = this;
        if (testObject.IsVisible) {
            retVal += testObject.InstType + ",";
            //$("#tblCalcObjValues").append("<tr><td>" + testObject.UserID + "</td><td>" + testObject.CustID + "</td><td>" + testObject.InstMode + "</td><td>" + testObject.IsVisible + "</td><td>" + testObject.InstType + "</td></tr>");
        }
    });

    //    $.each(calcHash, function () {
    //        var _single = this;
    //        if (_single.IsVisible) {
    //            retVal += _single.InstType + ",";
    //        }
    //    });
    return retVal;
}

function processServerMessageGeneric(updtTyp, updtEleTyp, eleID, eleVal, elePrntID) {
    try {
        //debugger;
        $("#tblCalcValues").append("<tr><td>" + eleID + "</td><td>" + updtEleTyp + "</td><td>" + eleVal + "</td></tr>");

        //For showing title on web image
        if ((updtEleTyp == "title")) {
            var tempHeading = $("#" + elePrntID + " .ServerName").html();
            var pos = tempHeading.toString().indexOf("[");
            if (pos > 0) {
                $("#" + elePrntID + " .ServerName").html("<strong>" + tempHeading.split("[")[0] + " [" + eleVal.split("[")[0] + "]</strong>");
            }
            else {
                $("#" + elePrntID + " .ServerName").append("<strong> [" + eleVal.split("[")[0] + "]</strong>");
            }

        }
        if (updtEleTyp == "m") {

            //$("#divMsg").html(eleVal);
            //$('#msgModal').modal({ keyboard: false, backdrop: 'static' });

            if (updtTyp == "imgStatus") {
                if (eleVal == "True") {
                    $("#" + elePrntID + " .imgCalculatorStatus").attr("src", "images/green.png");
                    $("#" + elePrntID + " .imgShareButton").prop("disabled", "");
                    if (isSafari) {
                        $("#" + elePrntID + " #textConnectStatusImage").css("color", "Green");
                        $("#" + elePrntID + " #textConnectStatusImage").text("C");
                    }
                }
                else {
                    $("#" + elePrntID + " .imgCalculatorStatus").attr("src", "images/red.png");
                    $("#" + elePrntID + " .imgShareButton").prop("disabled", "disabled");
                    if (isSafari) {
                        $("#" + elePrntID + " #textConnectStatusImage").css("color", "Red");
                        $("#" + elePrntID + " #textConnectStatusImage").text("D");
                    }
                }
            }
            else if (updtTyp == "shareStatus") {
                closeMarketHub();
                $("#" + elePrntID + " input").attr("disabled", "disabled");
                $("#" + elePrntID + " select").attr("disabled", "disabled");
                $("#" + elePrntID + " input:button").attr("disabled", "disabled");
                alert(eleVal);
            }
            else {
                if (elePrntID == 'AuthInvalid') {

                    alert(eleVal);

                    window.location.href = $('#hdnLogoutpath').val();
                }
                else if (elePrntID == 'ConnectionIDSignalR') {

                    $('#hdn_ConnectionID').val(eleVal);
                }
                else {

                    alert(eleVal);
                }
            }
        }
        else {
            if ($("#" + elePrntID + "_" + eleID).length) {
                if (updtEleTyp == "i") {
                    var eleValAry = eleVal.split('#');
                    //chart
                    if (isChartField($.trim(eleID)) == false) {
                        setElementValueGeneric($("#" + elePrntID + "_" + eleID), eleValAry[0], elePrntID, eleID, updtTyp, updtEleTyp, eleValAry[1]);
                        if (elePrntID == "vcm_calc_CMEMaxelerDSF") {
                            // debugger
                            if (eleID == 3) {
                                $("#ifrm").css('width', eleValAry[1]);
                            }
                            else if (eleID == 2) {
                                $("#ifrm").css('height', eleValAry[1]);
                            }
                            else if (eleID == 1) {
                                $("#ifrm").attr('src', eleValAry[1]);
                            }
                            //prepareFrame(200, 200, "http://www.itxgen.com/");
                        }
                        if (elePrntID == "vcm_calc_ChatApp") {
                            $('#vcm_calc_ChatApp_401').scrollTop($('#vcm_calc_ChatApp_401')[0].scrollHeight);
                        }
                    }
                    else {
                        var strChartTmp = eleVal.split('#')[0];
                        //debugger;
                        //createUpdateDojoChart(elePrntID + "_" + eleID, strChartTmp);
                        // create(elePrntID + "_" + eleID, strChartTmp);
                        chartCreateAndUpdate(elePrntID + "_" + eleID, strChartTmp);

                        if (elePrntID == "vcm_calc_Transactional_CurrenexDetailView_New") {
                            $("#vcm_calc_Transactional_CurrenexDetailView_New_88888888hdn").val(strChartTmp);
                        }
                        else if (elePrntID == "2123") {

                            $("#2123_88888888hdn").val(strChartTmp);
                        }
                    }

                }
                else if (updtEleTyp == "p") {
                    if (isChartField(eleID) == false) {
                        var propArySpl = eleVal.split('#');
                        setEleProps($("#" + elePrntID + "_" + eleID), propArySpl[0], elePrntID);
                        setElementValueGeneric($("#" + elePrntID + "_" + eleID), propArySpl[1], elePrntID, eleID, updtTyp, updtEleTyp, propArySpl[2]);
                    }
                    else {
                        //alert(eleVal);
                    }
                }
                else if (updtEleTyp == "tt") {
                    var eleTitleInSetVal = $("#" + elePrntID + "_" + eleID).attr('title');
                    if (eleTitleInSetVal != "datepick") {
                        if (isChartField(eleID) == false) {
                            $("#" + elePrntID + "_" + eleID).attr('title', eleVal);
                        }
                    }
                }
                else {
                    //debugger;
                    populateDropDownGenericList(elePrntID, eleID, eleVal, updtTyp, updtEleTyp);
                    if (elePrntID == 'vcm_calc_Transactional_CurrenexDetailView_New') {
                        if (eleID == '100') {
                            //alert(eleID);
                            //alert($('#vcm_calc_Transactional_CurrenexDetailView_New_100').text());                            
                            $('#vcm_calc_Transactional_CurrenexDetailView_New_100 option[value=""]').remove();
                        }
                    }
                }
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:processServerMessageGeneric(); updtTyp :" + updtTyp + ";updtEleTyp : " + updtEleTyp + ";eleID : " + eleID + ";eleVal :" + eleVal + ";elePrntID : " + elePrntID + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function isChartField(eleID) {
    if (eleID == "88888888" || eleID == "88888889" || eleID == "88888890")
        return true;
    return false;
}
function DisableConnection(varToken, varUserId, varConnectionID) {

    var url = $.jmsajaxurl({
        url: signalService,
        method: "DisableConnection",
        data: { GUID: varToken, userid: varUserId, ConnectionID: varConnectionID }
    });

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

function setElementValueGeneric(Element, ElementVal, EleParent, eleID, updtTyp, updtEleTyp, EletDispStr) {
    try {
        //debugger;
        var isUpdatable = true;
        //var OrigVal = ElementVal;
        var strAllClass = $(Element).attr("class");
        var aryClass = null;

        if (strAllClass == undefined)
            isUpdatable = false;
        else
            aryClass = strAllClass.split(' ');

        var isDatField = false;
        if (updtTyp == "mnlupdt" && $(Element).hasClass("updt") == true) {

        }
        else {
            if ($(Element).is("input") && ElementVal != "") {

                var eleTitleInSetVal = $(Element).attr('title');

                if (eleTitleInSetVal != "" && eleTitleInSetVal != undefined) {
                    if (eleTitleInSetVal == "datepick") {
                        ElementVal = ElementVal.split(' ')[0];
                        isUpdatable = false;
                        isDatField = true;
                        $(Element).val(ElementVal);
                    }
                    else {
                        //                    if (eleTitleInSetVal == "txt52") {
                        //                        $(Element).addClass("basisWidget");
                        //                    }
                        //                    else if (eleTitleInSetVal == "txt21") {
                        //                        $(Element).addClass("termWidget");
                        //                    }

                        //                    $(Element).removeAttr("title")
                    }
                }
            }
            else {
                //            if ($(Element).is("select") && $(Element).find('option').length <= 1) {
                //                alert(EleParent + " :: " + $(Element).attr("class"));
                //                isUpdatable = false;
                //            }
            }

            if (!$(Element).is("input[type='text']")) {
                if (!$(Element).is("input[type='button']")) {
                    if (EleParent == 'vcm_calc_ChatApp') {
                        if (ElementVal.indexOf(' %23 ') >= 0) {

                            var value = ElementVal.replace(/ %23 /g, '#');
                            $(Element).text('');
                            $(Element).append(value);

                        }
                        else {
                            $(Element).text('');
                            $(Element).append(ElementVal);
                        }
                    }
                    else {
                        $(Element).val(ElementVal);
                    }
                }
                else {
                    var inputBtnName = $(Element).attr("name");

                    if (inputBtnName == undefined || inputBtnName == "")
                        $(Element).attr("name", "1");
                }
            }
            else {
                if (isDatField == false) {

                    var hasFocus = $(Element).is(':focus');

                    //                if (EletDispStr == "Value")
                    //                    EletDispStr = ElementVal;

                    if (hasFocus == false) {
                        if (EleParent == 'vcm_calc_ChatApp') {
                            if (EletDispStr.indexOf(' %23 ') >= 0) {

                                var value = EletDispStr.replace(/ %23 /g, '#');
                                $(Element).text('');
                                $(Element).append(value);

                            }
                            else {
                                $(Element).text('');
                                $(Element).append(EletDispStr);
                            }
                        }
                        else {
                            $(Element).val(EletDispStr);
                        }
                        $(Element).attr("name", ElementVal);
                    }
                    else {
                        $(Element).val(ElementVal);
                        if (EleParent == 'vcm_calc_ChatApp') {
                            if (EletDispStr.indexOf(' %23 ') >= 0) {
                                var value = EletDispStr.replace(/ %23 /g, '#');
                                $(Element).attr("name", value);
                            }
                            else {
                                $(Element).attr("name", EletDispStr);
                            }
                        }
                        else {
                            $(Element).attr("name", EletDispStr);
                        }
                    }

                    //                var tmpName = $(Element).attr("name");
                    //                tmpName += "#" + ElementVal;
                    //                $(Element).attr("name", tmpName);
                }
            }

            if ($(Element).hasClass("updt") == false) {
                $(Element).addClass("updt");
            };
            if (EleParent == 'vcm_calc_ChatApp') {
                return false;
            }
            if (aryClass != null) {
                $(Element).css({
                    'background-color': getColorFromClass(aryClass[1]),
                    'color': getColorFromClass(aryClass[0])
                });
                setTimeout(function () {
                    $(Element).css({
                        'background-color': getColorFromClass(aryClass[0]),
                        'color': getColorFromClass(aryClass[1])
                    });
                }, 1000);
            }

            var strdesc = "InstanceMode Name: " + tblID + ", " + "DDID: " + ddID + " Element value populated!";
            JavaScriptActivitiesLogs("VolSeparate.js", "setElementValueGeneric()", strdesc);
        }
    }
    catch (err) {
        var strerrordesc = "Function:setElementValueGeneric(); Element : " + Element + "; ElementVal :" + ElementVal + "; EleParent; " + EleParent + "; eleID : " + eleID + "; updtTyp : " + updtTyp + "; updtEleTyp : " + updtEleTyp + "; EletDispStr : " + EletDispStr + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        //onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function speAnmiation_CMEFuture() {

}

function populateDropDownGenericList(tblID, ddID, dataObj, updtTyp, updtEleTyp) {
    try {
        // debugger;
        var DropDownEle;

        var isFirst = true;

        var ddObjVal;
        var selectedVal;

        var dataArray = dataObj.split('#')[0].split('|');
        selectedVal = dataObj.split('#')[1];

        var dataLength = dataArray.length;

        var i = 0;
        var element = null;

        var crntEleID = -1;
        var crntEleStr = "";
        var crntEleTitle = "";

        var ddOptHtm = "";

        for (i = 0; i < dataLength; i++) {

            element = dataArray[i];

            if (isFirst == true) {

                //            ddObjVal = $.trim(ddID);
                //            ddID = ddObjVal;

                DropDownEle = $("select#" + tblID + "_" + ddID);
                $(DropDownEle).empty();

                ddOptHtm += "<option value=''>" + "--Select--" + "</option>";
                isFirst = false;
            }

            var isHavingVal = false;

            if (element.indexOf("=") == -1) {
                crntEleID = parseInt(crntEleID) + 1;
            }
            else {
                crntEleID = parseInt($.trim(element.split('=')[1]));
                isHavingVal = true;
            }

            //            if (element.indexOf("~") == -1) {
            //                if (isHavingVal == false) {
            //                    crntEleStr = element;
            //                }
            //                else {
            //                    crntEleStr = element.split('=')[0];
            //                }
            //            }
            //            else {
            //                crntEleStr = element.split('~')[0];
            //            }

            //            ddOptHtm += "<option value=" + crntEleID + ">" + crntEleStr + "</option>";

            if (element.indexOf("~") == -1) {
                if (isHavingVal == false) {
                    crntEleStr = element;
                }
                else {
                    var eleSplitAry = element.split('=');
                    crntEleStr = eleSplitAry[0];
                    crntEleTitle = eleSplitAry[0];
                }
            }
            else {
                var eleSplitAry = element.split('~');
                crntEleStr = eleSplitAry[0];
                crntEleTitle = eleSplitAry[1].split('=')[0];
            }

            ddOptHtm += "<option value='" + crntEleID + "' title='" + crntEleTitle + "'>" + crntEleStr + "</option>";


            //$("<option value=" + crntEleID + ">" + crntEleStr + "</option>").appendTo(DropDownEle);
        }

        $(DropDownEle).html(ddOptHtm);

        //    setElementValueGeneric(DropDownEle, selectedVal, tblID, ddID, updtTyp, updtEleTyp);
        $(DropDownEle).val(selectedVal);

                var strdesc = "InstanceMode Name: " + tblID + ", " + "DDID: " + ddID + " DropDown values populated!";
                JavaScriptActivitiesLogs("VolSeparate.js", "populateDropDownGenericList()", strdesc);
    }
    catch (err) {
        var strerrordesc = "Function:populateDropDownGenericList(); tblID :" + tblID + "; ddID : " + ddID + "; dataObj : " + dataObj + "; updtTyp : " + updtTyp + "; updtEleTyp : " + updtEleTyp + "; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function getDataFromWebService() {
    getDataFromWebService_Vol(0, 451);
}


$.jmsajaxurl = function (options) {
    var url = options.url;
    url += "/" + options.method;
    if (options.data) {
        var data = ""; for (var i in options.data) {
            if (data != "")
                data += "&"; data += i + "=" + msJSON.stringify(options.data[i]);
        }
        url += "?" + data; data = null; options.data = "{}";
    }
    return url;
};


function trueRound(value, digits) {
    return (Math.round((value * Math.pow(10, digits)).toFixed(digits - 1)) / Math.pow(10, digits)).toFixed(digits);
}


function getLastOpenCalcTemp(varUserID, varClientType, varGUID) {

    //var url = $.jmsajaxurl({
    //    url: signalService,
    //    method: "GetLastOpenCalc",
    //    data: { UserId: varUserID }
    //});

    //$.ajax({
    //    cache: false,
    //    dataType: "jsonp",
    //    url: url + "&format=json",
    //    success: function (tmpData) {

    //        var tmpInstID = tmpData.d[0].InstanceId;
    //        var tmpInstInfo = tmpData.d[0].InstanceInfo;

    //        if (tmpInstID == 'False') {
    //            alert(tmpInstInfo);

    //            window.location.href = $('#hdnLogoutpath').val();
    //        }
    //        else {
    //            var tmpInstID = tmpData.d[0].InstanceId;
    //            var tmpInstInfo = tmpData.d[0].InstanceInfo;

    //            //alert(tmpInstID + " : " + tmpInstInfo);

    //            var menuObj = $("#menuItm_" + tmpInstID);
    //            ExampleMenu(menuObj);
    //            //return tmpInstID + " : " + tmpInstInfo;
    //        }
    //    },
    //    error: function (request, status, error) {

    //        //alert(error);
    //        ExampleMenu($("#menuItm_vcm_calc_swaptionVolPremStrike"));
    //    }
    //});
}

function getLastOpenCalc(varUserID, varClientType, varGUID) {
    //ExampleMenu($("#menuItm_vcm_calc_CdsCalculator"));
    //var url = $.jmsajaxurl({
    //    url: signalService,
    //    method: "GetLastOpenCalc",
    //    data: { UserId: varUserID, ClientType: varClientType, GUID: varGUID }
    //});

    //$.ajax({
    //    cache: false,
    //    dataType: "jsonp",
    //    url: url + "&format=json",
    //    success: function (tmpData) {

    //        var tmpInstID = tmpData.d[0].InstanceId;
    //        var tmpInstInfo = tmpData.d[0].InstanceInfo;

    //        if (tmpInstID == 'False') {
    //            alert(tmpInstInfo);

    //            window.location.href = $('#hdnLogoutpath').val();
    //        }
    //        else {
    GetLastOpenAppNameInfo();
    //        }

    //    },
    //    error: function (request, status, error) {

    //        //alert(error);
    //        GetLastOpenAppNameInfo("vcm_calc_swaptionVolPremStrike", varClientType, varGUID, varUserID);
    //        //ExampleMenu($("#menuItm_vcm_calc_swaptionVolPremStrike"));
    //    }
    //});
}


function GetLastOpenAppNameInfo() {
    //ExampleMenu($("#menuItm_vcm_calc_CdsCalculator"));
    //var tmpCategoryID;

    //var url = $.jmsajaxurl({
    //    url: signalService,
    //    method: "GetLastOpenAppName",
    //    data: { InstanceId: varInstanceId, ClientType: varClientType, GUID: varGUID, UserId: varUserID }
    //});

    //$.ajax({
    //    cache: false,
    //    dataType: "jsonp",
    //    url: url + "&format=json",
    //    success: function (tmpData) {


    //        var tmpCategoryID;
    //        $.each(tmpData.d, function (key, value) {
    //            if (value.CategoryId == 'False') {
    //                alert(value.Message);

    //                window.location.href = $('#hdnLogoutpath').val();
    //            }
    //            else {
    //                // if (varInstanceId == value.AppName) {
    //                tmpCategoryID = value.CategoryId
    //            }
    //            // alert(tmpCategoryID);
    //            // }
    //        });

   // GetCategoryId("12", "#menuItm_" + "vcm_calc_CdsCalculator");
    //    },
    //    error: function (request, status, error) {

    //        //GetLastOpenAppNameInfo("vcm_calc_swaptionVolPremStrike");
    //        ExampleMenu($("#menuItm_vcm_calc_swaptionVolPremStrike"));
    //    }
    //});
}


function getDataFromWebService_Vol(varUserID, varProductID) {

    var url = $.jmsajaxurl({
        url: signalService,
        method: "GetCurrentMarketDataVol",
        data: { UserID: varUserID, ProductID: varProductID }
    });

    $.ajax({
        cache: false,
        dataType: "jsonp",
        url: url + "&format=json",
        success: function (tmpData) {

            var tmpJsonData = tmpData.dtVolGrid;

            $.each(tmpJsonData, function (index, value) {
                var tmpCtr = 0;

                for (tmpCtr = 0; tmpCtr < 14; tmpCtr++) {
                    var colIndex = "col_" + tmpCtr;

                    var valStr = index + "#" + tmpCtr + "#" + value[colIndex];

                    updateVolGridDataIDVise(valStr, "ws");
                }
            });

            getDataFromWebService_Prem();
            getDataFromWebService_Strike();
        },
        error: function (request, status, error) {
            //alert("Hit error fn!");
            setTimeout(function () { getDataFromWebService(); }, 1500);
        }
    });
}


function getDataFromWebService_Prem() {

    var url = $.jmsajaxurl({
        url: signalService,
        method: "GetCurrentMarketDataPrem",
        data: {}
    });

    $.ajax({
        cache: false,
        dataType: "jsonp",
        url: url + "&format=json",
        success: function (tmpData) {

            var tmpJsonData = tmpData.dtPremGrid;

            $.each(tmpJsonData, function (index, value) {
                var tmpCtr = 0;

                for (tmpCtr = 0; tmpCtr < 14; tmpCtr++) {
                    var colIndex = "col_" + tmpCtr;

                    var valStr = index + "#" + tmpCtr + "#" + value[colIndex];

                    updatePrimGridDataIDVise(valStr, "ws");
                }
            });
        },
        error: function (request, status, error) {
            //alert("Hit error fn!");
        }
    });
}

function getDataFromWebService_Strike() {

    var url = $.jmsajaxurl({
        url: signalService,
        method: "GetCurrentMarketDataStrike",
        data: {}
    });

    $.ajax({
        cache: false,
        dataType: "jsonp",
        url: url + "&format=json",
        success: function (tmpData) {

            var tmpJsonData = tmpData.dtStrikeGrid;

            $.each(tmpJsonData, function (index, value) {
                var tmpCtr = 0;

                for (tmpCtr = 0; tmpCtr < 14; tmpCtr++) {
                    var colIndex = "col_" + tmpCtr;

                    var valStr = index + "#" + tmpCtr + "#" + value[colIndex];

                    updateStrikeGridDataIDVise(valStr, "ws");
                }
            });
        },
        error: function (request, status, error) {
            //alert("Hit error fn!");
        }
    });
}

function list2OnLoad() {
    var btnVal = $("#hdnLastSelList2Title").val();

    if (btnVal != "") {
        $(".btnList2").css('background-color', '#ECE9D8');

        $("." + btnVal + "").css('background-color', 'Yellow');
    }
}

function volsListOnLoad() {
    var btnVal = $("#hdnLastSelVolsTitle").val();

    if (btnVal != "") {
        $(".btnVols").css('background-color', '#ECE9D8');

        $("." + btnVal + "").css('background-color', 'Yellow');
    }
}

function volShiftListOnLoad() {
    var btnVal = $("#hdnLastSelVolShiftTitle").val();

    if (btnVal != "") {
        $(".btnVolShift").css('background-color', '#ECE9D8');

        $("." + btnVal + "").css('background-color', 'Yellow');
    }
}


function overrideXMLHTTPRequest() {
    if ('XDomainRequest' in window && window.XDomainRequest !== null) {
        // override default jQuery transport
        jQuery.ajaxSettings.xhr = function () {
            try { return new XDomainRequest(); }
            catch (e) { }
        };

        // also, override the support check
        jQuery.support.cors = true;
    }
}

function getVolPrimInCSV() {

    var str = '';

    $('table.tblVol tr').each(function () {

        var line = '';
        $(this).find('td').each(function () {
            line += $(this).text() + ',';
        });

        line.slice(0, line.Length - 1);
        str += line + '\r\n';
    });

    saveFile(str);

}

function saveFile(str) {
    if (navigator.appName != 'Microsoft Internet Explorer') {
        //window.open('data:text/csv;charset=utf-8,' + str);
        var w = window.open('', 'csvWindow'); // popup, may be blocked though
        w.document.open("text/csv");
        w.document.write(str);
        w.document.close();
    } else {
        var popupDoc = window.open('', 'csv', '');
        popupDoc.document.body.innerHTML = '<pre>' + str + '</pre>';
    }
}

function rowCol() {
    $('td').click(function () {
        var colIndex = $(this).parent().children().index($(this));
        var rowIndex = $(this).parent().parent().children().index($(this).parent());
        alert('Row: ' + rowIndex + ', Column: ' + colIndex);
    });
}

function popup(data) {
    setTimeout('win.document.execCommand("SaveAs")', 500);
    return true;
}

function SaveContents(element) {
    if (typeof element == "string")
        element = document.getElementById(element);
    if (element) {
        if (document.execCommand) {
            var oWin = window.open("about:blank", "_blank");
            oWin.document.write(element.value);
            oWin.document.close();
            var success = oWin.document.execCommand('SaveAs', true, element.id)
            oWin.close();
            if (!success)
                alert("Sorry, your browser does not support this feature");
        }
    }
}

function formatDate(dat) {
    try {
        var datMonth = dat.getMonth() + 1;

        var rtnFormatDate = dat.getFullYear() + "-" + datMonth + "-" + dat.getDate() + " " + dat.getHours() + ":" + dat.getMinutes() + ":" + dat.getSeconds() + "." + dat.getMilliseconds();

        return rtnFormatDate;
    }
    catch (err) {

        var strerrordesc = "Function:formatDate(); Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);

    }
    return null;
}

function getVolPrimInCSVServer() {
    var currentDateTime = formatDate(new Date());
    window.open('https://www.thebeastapps.com/beastappscore/handler/CSVHandler.ashx?curntDate=' + currentDateTime + '');
}

function GetExportServer(Calcname, strValue) {  // Called from individual app appclass.js

    window.open('https://www.thebeastapps.com/beastappscore/handler/ExportAppDataHandler.ashx?strValue=' + strValue + '&Calcname=' + Calcname + '&ConnectionID=' + $("#hdn_ConnectionID").val() + '');
}

function getCurrenexCSVServer(strValue) {
    window.open('https://www.thebeastapps.com/beastappscore/handler/ExportxmlToExcel.ashx?strValue=' + strValue + '');
}

function ExportxmlToExcel() {
    var url = $.jmsajaxurl({
        url: "https://www.thebeastapps.com/beastappscore/handler/ExportxmlToExcel.ashx",
        data: "{'Id': '10000'}"
    });

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



function refreshGrids() {
    $.ajax({
        url: "forceRefresh.ashx",
        data: "{}",
        success: function (msg) {

        },
        error: function (request, status, error) {

        }
    });
}

//COMET

function updateOtherData(strValue) {
    //debugger
    var ary = strValue.split('#');
    var valueOfTitle = ary[0];
    var subTitle = ary[1];
    var value = ary[2];

    if (valueOfTitle == "Currency") {
        if (subTitle == "Currency") {
            $("#lblCurrency").text(value.split(':')[0]);
            $(".lblCurrency").attr("title", value);
            $('.lblCurrency').tooltip();
        }
        else if (subTitle == "Straddle") {
            $("#lblStraddle").text(value);
            $(".lblStraddle").attr("title", value);
            $('.lblStraddle').tooltip();
        }
        else if (subTitle == "List") {
            $("#lblList").text(value);
            $(".lblList").attr("title", value);
            $('.lblList').tooltip();

            //$('#lblList').dotdotdot();
        }
    } else if (valueOfTitle == "Vols") {
        $("#lblVols").text(value);
        $(".lblVols").attr("title", value);
        $('.lblVols').tooltip();
    } else if (valueOfTitle == "VolShift") {
        $("#lblVolShift").text(value);
        $(".lblVolShift").attr("title", value);
        $('.lblVolShift').tooltip();
        //$('#lblVolShift').dotdotdot();
    } else if (valueOfTitle == "PremGridTitle") {
        $("#gridPremBtn").val(value);
    }

    if ($("#lblCurrency").text() == "") {
        //getGridSettings();
    }
}


function TooglePremFPrem() {

    var premBtnVal = $("#gridPremBtn").val();
    var btnVal = 0;
    var msg = parentID + "#" + btnVal;
    if (premBtnVal.toString().toLowerCase() == "prem") {
        btnVal = 1;
    }

    var parentID = "6";
    var msg = parentID + "#" + btnVal;

    $.cometd.publish('/service/otherdata', { sender: 'ButtonList', message: msg });

}

function animateTD(eleID) {
    $("td#" + eleID).css('background-color', '#C0C0C0').css('color', "#000000");
    setTimeout(function () { $("#" + eleID).css('background-color', '#FFFFFF').css('color', "#333333"); }, 1000);
}

function animateTDVol(eleID) {
    $("#" + eleID).css('background-color', '#8DA0D8').css('color', "#000000");
    setTimeout(function () { $("#" + eleID).css('background-color', '#FFFFFF').css('color', "#333333"); }, 1000);
}

function animateTDPrim(eleID) {

    $("#" + eleID).css('background-color', '#B9D6B9').css('color', "#000000");

    setTimeout(function () { $("#" + eleID).css('background-color', '#FFFFFF').css('color', "#333333"); }, 1000);

}

function animateTDStrike(eleID) {

    $("#" + eleID).css('background-color', '#ADD8E6').css('color', "#000000");

    setTimeout(function () { $("#" + eleID).css('background-color', '#FFFFFF').css('color', "#333333"); }, 1000);

}

function updateSwapVolPremStrikeEle(ele, eleID, eleValue, updateType) {
    //    if (updateType == "ws") {
    //        if ($(ele).hasClass('updt') == false) {
    $(ele).text(eleValue);
    animateTD(eleID);
    //        }
    //    }
    //    else {
    //        
    //        if ($(ele).hasClass("updt") == false) {
    //            $(ele).addClass("updt");
    //        };

    //        $(ele).text(eleValue);
    //        animateTD(eleID);
    //    }
}

function updateVolGridDataIDVise(strValue, updateType) {
    try {
        //debugger;
        var ary = strValue.split('#');
        var row = parseInt(ary[0]) + 1;
        var col = parseInt(ary[1]) + 2;
        var value = ary[2];

        if (row >= 10) {
            row = "0" + row.toString();
        }
        var eleID = "v" + row.toString() + col.toString();

        //    if (updateType == "ws") {
        //        if ($("#" + eleID).hasClass('wsupdatable') == true) {
        //            $("#" + eleID).removeClass("wsupdatable");
        //            $("#" + eleID).text(value);
        //            animateTD(eleID);
        //        }
        //    }
        //    else {
        //        $("#" + eleID).text(value);
        //        animateTD(eleID);
        //    }

        updateSwapVolPremStrikeEle($("td#" + eleID), eleID, value, updateType);

        eleID = "sv" + row.toString() + col.toString();

        //    if (updateType == "ws") {
        //        if ($("#" + eleID).hasClass('wsupdatable') == true) {
        //            $("#" + eleID).removeClass("wsupdatable");
        //            $("#" + eleID).text(value);
        //            animateTDVol(eleID);
        //        }
        //    }
        //    else {
        //        $("#" + eleID).text(value);
        //        animateTDVol(eleID);
        //    }
        ////updateSwapVolPremStrikeEle($("td#" + eleID), eleID, value, updateType);
    }
    catch (err) {
        var strerrordesc = "Function:updateVolGridDataIDVise(); strValue : " + strValue + "; updateType : " + updateType + " ; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function updatePrimGridDataIDVise(strValue, updateType) {
    try {
        //debugger;
        var ary = strValue.split('#');
        var row = parseInt(ary[0]) + 1;
        var col = parseInt(ary[1]) + 2;
        var value = ary[2];

        if (row >= 10) {
            row = "0" + row.toString();
        }
        var eleID = "p" + row.toString() + col.toString();

        //    if (updateType == "ws") {
        //        if ($("#" + eleID).hasClass('wsupdatable') == true) {
        //            $("#" + eleID).removeClass("wsupdatable");
        //            $("#" + eleID).text(value);
        //            animateTD(eleID);
        //        }
        //    }
        //    else {
        //        $("#" + eleID).text(value);
        //        animateTD(eleID);
        //    }

        updateSwapVolPremStrikeEle($("td#" + eleID), eleID, value, updateType);

        eleID = "sp" + row.toString() + col.toString();

        //    if (updateType == "ws") {
        //        if ($("#" + eleID).hasClass('wsupdatable') == true) {
        //            $("#" + eleID).removeClass("wsupdatable");
        //            $("#" + eleID).text(value);
        //            animateTDPrim(eleID);
        //        }
        //    }
        //    else {
        //        $("#" + eleID).text(value);
        //        animateTDPrim(eleID);
        //    }

        ////updateSwapVolPremStrikeEle($("td#" + eleID), eleID, value, updateType);
    }
    catch (err) {
        var strerrordesc = "Function:updatePrimGridDataIDVise(); strValue : " + strValue + "; updateType : " + updateType + " ; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function updateStrikeGridDataIDVise(strValue, updateType) {
    try {
        //debugger;
        var ary = strValue.split('#');
        var row = parseInt(ary[0]) + 1;
        var col = parseInt(ary[1]) + 2;
        var value = ary[2];

        if (row >= 10) {
            row = "0" + row.toString();
        }
        var eleID = "s" + row.toString() + col.toString();

        //    if (updateType == "ws") {
        //        if ($("#" + eleID).hasClass('wsupdatable') == true) {
        //            $("#" + eleID).removeClass("wsupdatable");
        //            $("#" + eleID).text(value);
        //            animateTD(eleID);
        //        }
        //    }
        //    else {
        //        $("#" + eleID).text(value);
        //        animateTD(eleID);
        //    }

        updateSwapVolPremStrikeEle($("td#" + eleID), eleID, value, updateType);

        eleID = "ss" + row.toString() + col.toString();

        //    if (updateType == "ws") {
        //        if ($("#" + eleID).hasClass('wsupdatable') == true) {
        //            $("#" + eleID).removeClass("wsupdatable");
        //            $("#" + eleID).text(value);
        //            animateTDStrike(eleID);
        //        }
        //    }
        //    else {
        //        $("#" + eleID).text(value);
        //        animateTDStrike(eleID);
        //    }

        ////updateSwapVolPremStrikeEle($("td#" + eleID), eleID, value, updateType);
    }
    catch (err) {
        var strerrordesc = "Function:updateStrikeGridDataIDVise(); strValue : " + strValue + "; updateType : " + updateType + " ; Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function onJavascriptLog(pName, ErrorDesc) {

        $.ajax({
            url: "handler/loghandler.ashx",
            data: { pageName: pName, methodName: "1", desc: ErrorDesc },
            cache: false,
            success: function (msg) {
                //alert(msg);
            },
            error: function (request, status, error) {
                // alert("Log : " + error);
            }
        });
}

function JavaScriptActivitiesLogs(pName, pMethod, ErrorDesc) {

        $.ajax({
            url: "handler/loghandler.ashx",
            data: { pageName: pName, methodName: pMethod, desc: ErrorDesc },
            cache: false,
            success: function (msg) {
                //alert(msg);
            },
           error: function (request, status, error) {
                // alert("Log : " + error);
            }
        });
}