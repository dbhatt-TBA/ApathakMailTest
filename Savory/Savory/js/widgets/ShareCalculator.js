/*==== Multiple Emails ======*/

var CS_vCSS = '';
var CS_BoxPlaceHolder = '<div id="dvEmailListPopup" class="modal" tabindex="-1"'
        + 'role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">'
        + '</div>'
        + '<input type="hidden" id="hdnShareCalcInfo" runat="server" />';

var CS_BoxHtml = '<div class="modal-dialog"><div class="modal-content"><div class="modal-header" id="dvShareCalcHeader" style="cursor: move;">'
            + '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>'
            + '<strong>Enter Email id/s to share this Calculator</strong>'
        + '</div>'
        + '<div class="modal-body">'
            + 'Email:'
            + '<div id="dvMultiEmails">'
                + '<ul id="emailUL">'
                + '</ul>'
                + '<input type="text" id="txtEmailId" class="email-input-box" />'
            + '</div>'
            //+ 'Message:'
            //+ '<div id="dvMultiEmailsMsg">'
            //    + '<input type="text" id="txtShareMsg" class="email-input-box" style="border: 1px solid #C7C7C7;" />'
            //+ '</div>'
        + '</div>'
        + '<div class="modal-footer">'
            + '<button type="button" class="btn btn-xs" data-dismiss="modal">Cancel</button> <button type="button" class="btn btn-xs btn-info" onclick="CS_Func_ShareCalculator();">Share</button>'
        + '</div></div></div>';


var CS_vCntr = 0;
var CS_vNewEmailTemplate = '<span class="email"><div class="[ITEMCLASS]">[EMAIL]</div><div class="email-delete">x</div></span>';
var CS_vEditEmailTemplate = '<input type="text" value="[RAWEMAIL]" id="[TXTBXID]" class="email-edit-box" [STYLE] /> ';

function CS_Func_ShowMultiEmailBox(varUserID, varCustID, varInstanceType, varInstanceMode) {
    try {

        CS_Func_CheckPlaceHolderAvailable();

        $("#hdnShareCalcInfo").val(varUserID + "^" + varCustID + "^" + varInstanceType + "^" + varInstanceMode);
        $('#dvEmailListPopup').html(CS_BoxHtml);

        $('#dvEmailListPopup').modal({ keyboard: true, backdrop: 'static' });
        $("div:Last").removeClass("modal-backdrop in");

        $('#txtEmailId').focus();

        //Bind event after control load-for FireFox
        $('#txtEmailId').bind('keypress', function (_evnt) {
            CS_Func_CheckKey(_evnt, this.id);
        });

        $('#dvEmailListPopup').draggable({ handle: "#dvShareCalcHeader" });
    }
    catch (err) {
        var strerrordesc = "Function:CS_Func_ShowMultiEmailBox(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function CS_Func_CheckPlaceHolderAvailable() {
    if ($('#dvEmailListPopup') === undefined || $('#dvEmailListPopup') === null) {
        $('#form1').append(CS_BoxPlaceHolder);
    }
}

function CS_Func_ShareCalculator() {
    try {
        var vEmailIds = CS_Func_GetAllEmailIds();

        if (vEmailIds == "") {
            alert("Invalid email address.");
        }
        else {
            var hdnShareInfo = $("#hdnShareCalcInfo").val().split('^');

            var varUserID = hdnShareInfo[0];
            var varCustID = hdnShareInfo[1];
            var varappId = hdnShareInfo[2];
            var varInstanceMode = hdnShareInfo[3];
            var varSenderEmailId = $("#hdn_senderEmailId").val();
            //var varShareMessage = $('#txtShareMsg').val();
            var varShareMessage = "";
            sendShareImageRequest(varappId, varUserID, varCustID, varInstanceMode, vEmailIds, varSenderEmailId, varShareMessage);

            $('#dvEmailListPopup').modal('hide');
        }
    }
    catch (err) {
        var strerrordesc = "Function:CS_Func_ShareCalculator(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function CS_Func_AddLi(pText, isValid) {

    var vCls = (isValid === true) ? 'email-item' : 'email-item-invalid';
    $('#txtEmailId').val('');
    $("#dvMultiEmails ul").append('<li id="li' + CS_vCntr + '">' + CS_vNewEmailTemplate.replace('[EMAIL]', pText).replace('[ITEMCLASS]', vCls) + '</li>');
    CS_vCntr++;
}

function CS_Func_EditLi(pObj) {
    try {
        var vRawEmailId = $(pObj).text();
        var vParentLiId = $(pObj).closest('li')[0].id;
        var vTxtBxId = vParentLiId.toString().replace('li', 'txt');
        var vWidth = 'style="width:' + parseInt($(pObj).parent()[0].scrollWidth - 10, 10) + 'px;"';
        var vTxtBxHtml = CS_vEditEmailTemplate.replace('[RAWEMAIL]', vRawEmailId).replace('[TXTBXID]', vTxtBxId).replace('[STYLE]', vWidth);

        $(pObj).closest('li').html(vTxtBxHtml);
        $('#' + vTxtBxId).focus();
    }
    catch (err) {
        var strerrordesc = "Function:CS_Func_EditLi(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function CS_Func_RestoreLi(pObjId, pIsValid) {

    var pObj = $('#' + pObjId);

    var vText = $.trim($(pObj)[0].value);
    var vCls = (pIsValid === true) ? 'email-item' : 'email-item-invalid';
    var vLiHtml = CS_vNewEmailTemplate.replace('[EMAIL]', vText).replace('[ITEMCLASS]', vCls);
    $(pObj).closest('li')[0].innerHTML = vLiHtml;
}

$('.email-item').live('click', function () {
    CS_Func_EditLi(this);
});

$('.email-item-invalid').live('click', function () {
    CS_Func_EditLi(this);
});

$('.email-delete').live('click', function () {
    $(this).closest('li').remove();
});

$('.email-edit-box').live('blur', function () {
    var vIsValid = GEN_Func_ValidateEMailAddress(this.value);
    CS_Func_RestoreLi(this.id, vIsValid);
});

$('.email-edit-box').live('keypress', function (_evnt) {
    CS_Func_CheckKey(_evnt, this.id);
});

function CS_Func_GetAllEmailIds() {

    try {
        var validIds = "";
        var tmpId = "";

        $('#dvMultiEmails div.email-item').each(function () {
            tmpId = $.trim($(this).text());
            if (validIds.indexOf(tmpId) == -1) {
                validIds += tmpId + '#';
            }
        });

        //                $('#dvMultiEmails ul li').each(function () {
        //                    if ($(this)[0].children[0].children[0].className == 'email-item') {
        //                        tmpId = $.trim($(this)[0].children[0].children[0].innerText);
        //                        alert(tmpId);
        //                        if (validIds.indexOf(tmpId) == -1) {
        //                            validIds += tmpId + '#';
        //                        }
        //                    }
        //                });

        var isValid = GEN_Func_ValidateEMailAddress($('#txtEmailId').val());

        if (isValid == true)
            validIds = validIds + $('#txtEmailId').val();

        return validIds;
    }
    catch (err) {
        var strerrordesc = "Function:CS_Func_GetAllEmailIds(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function CS_Func_CheckKey(pEvnt, pObjId) {

    try {
        var key;
        if (window.event)       //Required to check for FireFox
            key = pEvnt.keyCode;
        else
            key = pEvnt.which;

        key = key.toString();
        if (key == "13") {
            pEvnt.preventDefault();
        }

        if (key == "32" || key == "188" || key == "186" || key == "13") {

            var text = $.trim($('#' + pObjId).val());

            if (text.toString().length > 5) {

                var isValid = GEN_Func_ValidateEMailAddress(text);

                if (pObjId.toString().indexOf('txtEmailId') > -1) {
                    CS_Func_AddLi(text, isValid);
                }
                else {
                    CS_Func_RestoreLi(pObjId, isValid);
                }
            }

            pEvnt.returnValue = false;
        }
    }
    catch (err) {
        var strerrordesc = "Function:CS_Func_CheckKey(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        alert(strerrordesc);
        onJavascriptLog("AppList.aspx", strerrordesc);
        pEvnt.returnValue = false;
    }
}

function CS_Func_CountMinutes(pUnit, pNumber) {
    try {
        var vUnit = pUnit;
        var vNumber = pNumber;

        switch (vUnit) {

            case "minute":
                break;

            case "hour":
                vNumber = vNumber * 60;
                break;

            case "day":
                vNumber = vNumber * 1440;
                break;

            case "week":
                vNumber = vNumber * 10080;
                break;

            case "month":
                break;

            case "forever":
                vNumber = -1;
                break;

            default:

                break;
        }

        return vNumber;
    }

    catch (err) {
        var strerrordesc = "Function:CS_Func_CountMinutes(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("AppList.aspx", strerrordesc);
    }
}

function CS_Func_TimeUnitChange() {
    if ($('#selTimeUnit').val() == "forever") {
        $('#txtUnit').attr("disabled", "disabled");
    }
    else {
        $('#txtUnit').removeAttr("disabled");
    }
}