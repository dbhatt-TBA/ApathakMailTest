function Get_DayCount_Frequency(SelectdValue) {
    try {
        var DayCount = '';
        var Frequency = '';

        if (SelectdValue == 'AM' || SelectdValue == 'A5' || SelectdValue == 'AB' || SelectdValue == 'AE') {
            //Annu
            Frequency = '1Y';
        }
        else if (SelectdValue == 'SM' || SelectdValue == 'S5' || SelectdValue == 'SB' || SelectdValue == 'SE') {
            //Semi
            Frequency = '6M';
        }
        else if (SelectdValue == 'QM' || SelectdValue == 'Q5' || SelectdValue == 'QB' || SelectdValue == 'QE') {
            //Quarter
            Frequency = '3M';
        }
        else if (SelectdValue == 'MM' || SelectdValue == 'M5' || SelectdValue == 'MB' || SelectdValue == 'ME') {
            //Month
            Frequency = '1M';
        }
        else if (SelectdValue == 'ZM' || SelectdValue == 'Z5' || SelectdValue == 'ZB' || SelectdValue == 'ZE') {
            //Zero
            Frequency = '0Y';
        }

        //DayCount
        if (SelectdValue == 'AM' || SelectdValue == 'SM' || SelectdValue == 'QM' || SelectdValue == 'MM' || SelectdValue == 'ZM') {
            DayCount = 'A60';
        }
        else if (SelectdValue == 'A5' || SelectdValue == 'S5' || SelectdValue == 'Q5' || SelectdValue == 'M5' || SelectdValue == 'Z5') {
            DayCount = 'A65';
        }
        else if (SelectdValue == 'AB' || SelectdValue == 'SB' || SelectdValue == 'QB' || SelectdValue == 'MB' || SelectdValue == 'ZB') {
            DayCount = 'BIS';
        }
        else if (SelectdValue == 'AE' || SelectdValue == 'SE' || SelectdValue == 'QE' || SelectdValue == 'ME' || SelectdValue == 'ZE') {
            DayCount = 'B60';
        }

    }
    catch (err) {
        var strerrordesc = "Function:Get_DayCount_Frequency(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }

    return DayCount + "#" + Frequency;
}


function DayCountSelection_MainValue(DayCountValue) {
    try {

        if (DayCountValue == "A60") {
            return "M";
        }
        else if (DayCountValue == "A65") {
            return "5";
        }
        else if (DayCountValue == "AJP") {
            return "AJP";
        }
        else if (DayCountValue == "AA") {
            return "AA";
        }
        else if (DayCountValue == "165") {
            return "165";
        }
        else if (DayCountValue == "BIS") {
            return "B";
        }
        else if (DayCountValue == "BPS") {
            return "BPS";
        }
        else if (DayCountValue == "BSI") {
            return "BSI";
        }
        else if (DayCountValue == "B60") {
            return "E";
        }
        else if (DayCountValue == "B65") {
            return "B65";
        }
    }
    catch (err) {
        var strerrordesc = "Function:DayCountSelection_MainValue(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function FinalValue(FrequencyValue, DayCountValue) {
    try {
        /// A
        if (DayCountValue == "A60" && (FrequencyValue == "1Y" || FrequencyValue == "12M")) {
            return "AM";
        }
        else if (DayCountValue == "A65" && (FrequencyValue == "1Y" || FrequencyValue == "12M")) {
            return "A5";
        }
        else if (DayCountValue == "BIS" && (FrequencyValue == "1Y" || FrequencyValue == "12M")) {
            return "AB";
        }
        else if (DayCountValue == "B60" && (FrequencyValue == "1Y" || FrequencyValue == "12M")) {
            return "AE";
        }
        /// S
        if (DayCountValue == "A60" && (FrequencyValue == "6M")) {
            return "SM";
        }
        else if (DayCountValue == "A65" && (FrequencyValue == "6M")) {
            return "S5";
        }
        else if (DayCountValue == "BIS" && (FrequencyValue == "6M")) {
            return "SB";
        }
        else if (DayCountValue == "B60" && (FrequencyValue == "6M")) {
            return "SE";
        }
        /// Q
        if (DayCountValue == "A60" && (FrequencyValue == "3M")) {
            return "QM";
        }
        else if (DayCountValue == "A65" && (FrequencyValue == "3M")) {
            return "Q5";
        }
        else if (DayCountValue == "BIS" && (FrequencyValue == "3M")) {
            return "QB";
        }
        else if (DayCountValue == "B60" && (FrequencyValue == "3M")) {
            return "QE";
        }
        /// M
        if (DayCountValue == "A60" && (FrequencyValue == "1M")) {
            return "MM";
        }
        else if (DayCountValue == "A65" && (FrequencyValue == "1M")) {
            return "M5";
        }
        else if (DayCountValue == "BIS" && (FrequencyValue == "1M")) {
            return "MB";
        }
        else if (DayCountValue == "B60" && (FrequencyValue == "1M")) {
            return "ME";
        }
        /// Z
        if (DayCountValue == "A60" && (FrequencyValue == "0Y")) {
            return "ZM";
        }
        else if (DayCountValue == "A65" && (FrequencyValue == "0Y")) {
            return "Z5";
        }
        else if (DayCountValue == "BIS" && (FrequencyValue == "0Y")) {
            return "ZB";
        }
        else if (DayCountValue == "B60" && (FrequencyValue == "0Y")) {
            return "ZE";
        }
        else {
            return FrequencyValue + '/' + DayCountValue;
        }
    }
    catch (err) {
        var strerrordesc = "Function:FinalValue(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function ConvertToDirectValue_Basis(BeastValue) {
    try {
        if (BeastValue == "12300")
            return "AM";
        else if (BeastValue == "12301")
            return "A5";
        else if (BeastValue == "12305")
            return "AB";
        else if (BeastValue == "12308")
            return "AE";
        else if (BeastValue == "6300")
            return "SM";
        else if (BeastValue == "6301")
            return "S5";
        else if (BeastValue == "6305")
            return "SB";
        else if (BeastValue == "6308")
            return "SE";
        else if (BeastValue == "3300")
            return "QM";
        else if (BeastValue == "3301")
            return "Q5";
        else if (BeastValue == "3305")
            return "QB";
        else if (BeastValue == "3308")
            return "QE";
        else if (BeastValue == "1300")
            return "MM";
        else if (BeastValue == "1301")
            return "M5";
        else if (BeastValue == "1305")
            return "MB";
        else if (BeastValue == "1308")
            return "ME";
        else if (BeastValue == "0300" || BeastValue == "300")
            return "ZM";
        else if (BeastValue == "0301" || BeastValue == "301")
            return "Z5";
        else if (BeastValue == "0305" || BeastValue == "305")
            return "ZB";
        else if (BeastValue == "0308" || BeastValue == "308")
            return "ZE";
        else
            return "";
    }
    catch (err) {
        var strerrordesc = "Function:ConvertToDirectValue_Basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function ConvertTo_Rev_DirectValue_Basis(BeastValue) {
    try {
        if (BeastValue == "AM")
            return "12300";
        else if (BeastValue == "A5")
            return "12301";
        else if (BeastValue == "AB")
            return "12305";
        else if (BeastValue == "AE")
            return "12308";
        else if (BeastValue == "SM")
            return "6300";
        else if (BeastValue == "S5")
            return "6301";
        else if (BeastValue == "SB")
            return "6305";
        else if (BeastValue == "SE")
            return "6308";
        else if (BeastValue == "QM")
            return "3300";
        else if (BeastValue == "Q5")
            return "3301";
        else if (BeastValue == "QB")
            return "3305";
        else if (BeastValue == "QE")
            return "3308";
        else if (BeastValue == "MM")
            return "1300";
        else if (BeastValue == "M5")
            return "1301";
        else if (BeastValue == "MB")
            return "1305";
        else if (BeastValue == "ME")
            return "1308";
        else if (BeastValue == "ZM")
            return "0300";
        else if (BeastValue == "Z5")
            return "0301";
        else if (BeastValue == "ZB")
            return "0305";
        else if (BeastValue == "ZE")
            return "0308";
        else
            return "";
    }
    catch (err) {
        var strerrordesc = "Function:ConvertTo_Rev_DirectValue_Basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }

}


function ConvertToDayCount_Basis(DayCountValue) {
    try {
        if (DayCountValue == "00" || DayCountValue == "0") {
            return "M";
        }
        else if (DayCountValue == "01" || DayCountValue == "1") {
            return "5";
        }
        else if (DayCountValue == "02" || DayCountValue == "2") {
            return "AJP";
        }
        else if (DayCountValue == "03" || DayCountValue == "3") {
            return "AA";
        }
        else if (DayCountValue == "04" || DayCountValue == "4") {
            return "165";
        }
        else if (DayCountValue == "05" || DayCountValue == "5") {
            return "B";
        }
        else if (DayCountValue == "06" || DayCountValue == "6") {
            return "BPS";
        }
        else if (DayCountValue == "07" || DayCountValue == "7") {
            return "BSI";
        }
        else if (DayCountValue == "08" || DayCountValue == "8") {
            return "E";
        }
        else if (DayCountValue == "09" || DayCountValue == "9") {
            return "B65";
        }
    }
    catch (err) {
        var strerrordesc = "Function:ConvertToDayCount_Basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function ConvertTo_Rev_DayCount_Basis(DayCountValue) {
    try {
        if (DayCountValue == "M") {
            return "00";
        }
        else if (DayCountValue == "5") {
            return "01";
        }
        else if (DayCountValue == "AJP") {
            return "02";
        }
        else if (DayCountValue == "AA") {
            return "03";
        }
        else if (DayCountValue == "165") {
            return "04";
        }
        else if (DayCountValue == "B") {
            return "05";
        }
        else if (DayCountValue == "BPS") {
            return "06";
        }
        else if (DayCountValue == "BSI") {
            return "07";
        }
        else if (DayCountValue == "E") {
            return "08";
        }
        else if (DayCountValue == "B65") {
            return "09";
        }
    }
    catch (err) {
        var strerrordesc = "Function:ConvertTo_Rev_DayCount_Basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


var EnumTermUnit = {
    "e_Days": 0,
    "e_Bdays": 1,
    "e_Weeks": 2,
    "e_Months": 3,
    "e_Years": 4,
    "e_MonthsOnly": 5,
    "e_None": 6
};

function GetTermNear_Basis(m_nTerm_Basis) {
    return ((m_nTerm_Basis >= 0) ? m_nTerm_Basis / 1000000 : -((-m_nTerm_Basis) / 1000000)) != 0;
}


function GetCount_Basis(m_nTerm_Basis) {
    return parseInt((m_nTerm_Basis >= 0) ? m_nTerm_Basis / 10 % 100000 : -((-m_nTerm_Basis) / 10 % 100000));
}

function ConvertToTerm_Basis(BeastValue) {

    try {
        //last char

        var lastchar = BeastValue.toString().charAt(BeastValue.toString().length - 1);

        if (GetTermNear_Basis(parseInt(BeastValue))) {
            BeastValue = GetCount_Basis(parseInt(BeastValue)).toString() + lastchar.toString();
        }

        //excluding last char
        var New_value = BeastValue.toString().substr(0, BeastValue.toString().length - 1);

        if (New_value == "")
            New_value = "0";

        if (EnumTermUnit.e_Days == lastchar) {
            //0
            if (New_value.charAt(0) == '-') {
                return "-" + New_value.substr(1, New_value.toString().length) + "D";
            }
            else {
                return New_value + "D";
            }
        }
        else if (EnumTermUnit.e_Bdays == lastchar) {
            //1
            if (New_value.charAt(0) == '-') {
                return "-" + New_value.substr(1, New_value.toString().length) + "B";
            }
            else {
                return New_value + "B";
            }
        }
        else if (EnumTermUnit.e_Weeks == lastchar) {
            //W
            if (New_value.charAt(0) == '-') {
                return "-" + New_value.substr(1, New_value.toString().length) + "W";
            }
            else {
                return New_value + "W";
            }
        }
        else if (EnumTermUnit.e_Months == lastchar) {
            //3
            //        if (New_value.charAt(0) == '-') {

            //            var Year = parseInt(parseInt(New_value.substr(1, New_value.toString().length)) / 12);
            //            var Month = parseInt(parseInt(New_value.substr(1, New_value.toString().length)) % 12);

            //            return "-" + Year.toString() + "Y" + Month.toString() + "M";
            //        }
            //        else {

            //            var Year = parseInt(parseInt(New_value.substr(0, New_value.toString().length)) / 12);
            //            var Month = parseInt(parseInt(New_value.substr(0, New_value.toString().length)) % 12);

            //            return Year.toString() + "Y" + Month.toString() + "M";
            //        }
            if (New_value.charAt(0) == '-') {

                return "-" + New_value.substr(1, New_value.toString().length) + "M";
            }
            else {

                return New_value + "M";
            }
        }
        else if (EnumTermUnit.e_Years == lastchar) {
            //4
            if (New_value.charAt(0) == '-') {
                return New_value.substr(1, New_value.toString().length) + "Y";
            }
            else {
                return New_value + "Y";
            }
        }
        else if (EnumTermUnit.e_MonthsOnly == lastchar) {
            //5
            if (New_value.charAt(0) == '-') {

                return "-" + New_value.substr(1, New_value.toString().length) + "MO";
            }
            else {

                return New_value + "MO";
            }
        }
        else if (EnumTermUnit.e_None == lastchar) {
            //6
        }
    }
    catch (err) {
        var strerrordesc = "Function:ConvertToTerm_Basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function ConvertToTermBeastValue_Basis(TermValue) {
    try {
        var IsNegativeFormat = 'False';
        if (TermValue.indexOf('-') > -1) {
            IsNegativeFormat = 'True';
        }

        TermValue = TermValue.replace('-', '');
        var finalTermValue = '';

        if (TermValue.indexOf('MO') > -1) {
            finalTermValue = TermValue.split('M')[0] + "5";
        }
        else if (TermValue.indexOf('Y') > -1 || TermValue.indexOf('M') > -1) {

            var YearValue = 0;
            var MonthValue = 0;
            if (TermValue.indexOf('Y') > -1) {
                YearValue = TermValue.split('Y')[0];
                MonthValue = TermValue.replace(TermValue.split('Y')[0] + 'Y', "").split('M')[0];
            }
            else if (TermValue.indexOf('M') > -1) {
                MonthValue = TermValue.replace(TermValue.split('Y')[0] + 'Y', "").split('M')[0];
            }
            if (MonthValue == "") {
                MonthValue = 0;
            }
            if (YearValue == "") {
                YearValue = 0;
            }

            finalTermValue = ((parseInt(YearValue) * 12) + parseInt(MonthValue)).toString() + "3";
        }
        else if (TermValue.indexOf('W') > -1) {
            finalTermValue = TermValue.split('W')[0] + "2";
        }
        else if (TermValue.indexOf('B') > -1) {
            finalTermValue = TermValue.split('B')[0] + "1";
        }
        else if (TermValue.indexOf('D') > -1) {
            finalTermValue = TermValue.split('D')[0] + "0";
        }

        if (IsNegativeFormat == 'True') {
            finalTermValue = "-" + finalTermValue;
        }

        return finalTermValue;
    }
    catch (err) {
        var strerrordesc = "Function:ConvertToTermBeastValue_Basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

//new added

function display_BasisWidget(eleIDForWidget, defFormat, basisEle) {
    try {

        //        var orgVal = $(basisEle).attr("name");
        //        var widgetVal = $(basisEle).val();

        var orgVal = $(basisEle).val();
        var widgetVal = $(basisEle).attr("name");

        if (widgetVal == "" || widgetVal == undefined || isNaN(widgetVal))
            widgetVal = "";

        $('#txtBeastValue_BasisWidget').attr("name", widgetVal);
        $('#txtLastClickedEleInfo_Basis').val(eleIDForWidget);

        var varbeast = $('#txtBeastValue_BasisWidget')[0].name;
        var varbeast2 = varbeast.substr(varbeast.toString().length - 2, varbeast.toString().length);
        var varbeastFull = varbeast.substr(0, varbeast.toString().length - 2);

        if (ConvertToDirectValue_Basis($('#txtBeastValue_BasisWidget')[0].name) == "")
            $('#txtBeastValue_BasisWidget').val(ConvertToTerm_Basis(varbeastFull) + "/" + ConvertToDayCount_Basis(varbeast2));
        else
            $('#txtBeastValue_BasisWidget').val(ConvertToDirectValue_Basis($('#txtBeastValue_BasisWidget')[0].name));


        Bind_Basis_Widget();
        ShowFullValue_Bt();

        //    }
        //    catch (err) {
        //        alert('$(document).ready');
        //    }        

        Close_Term_BasisWidget();
        $('#tblBasis').modal({ backdrop: true, keyboard: true });
        $("div:Last").removeClass("modal-backdrop in");
        $('#hdnWgtElement').val($(basisEle).attr('id'));

        positionWidget($(basisEle).attr('id'));

        $('#tblBasis').focus();
    }
    catch (err) {
        var strerrordesc = "Function:display_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function Save_BasisWidget() {
    try {
        //if (IsChange == 'TRUE') 
        {
            IsChange = '';

            if ($('#txtFrequency').val().indexOf('(') > -1) {
                $('#txtBeastValue_BasisWidget').val("-" + FinalValue($('#txtFrequency').val(), DayCountSelection_MainValue($('#DayCountSelection').val())).replace('(', '').replace(')', ''));

            }
            else {
                $('#txtBeastValue_BasisWidget').val(FinalValue($('#txtFrequency').val(), DayCountSelection_MainValue($('#DayCountSelection').val())));
            }

            /////
            if (ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val()) != "")
                $('#txtBeastValue_BasisWidget')[0].name = ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val());
            else {
                $('#txtBeastValue_BasisWidget')[0].name = ConvertToTermBeastValue_Basis($('#txtBeastValue_BasisWidget').val().split('/')[0]) + ConvertTo_Rev_DayCount_Basis($('#txtBeastValue_BasisWidget').val().split('/')[1]);
            }

            // alert($('#txtBeastValue_BasisWidget')[0].name);

            var newBeastfinalValue = ConvertToDirectValue_Basis($('#txtBeastValue_BasisWidget')[0].name);

            if (newBeastfinalValue != "")
                $('#txtBeastValue_BasisWidget').val(newBeastfinalValue);

            sendServerValue_BasisWidget();
        }
        $('#tblBasis').hide();
        Close_Term_BasisWidget();

        ShowFullValue_Bt();
    }
    catch (err) {
        var strerrordesc = "Function:Save_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function sendServerValue_BasisWidget() {
    var eleAryInfo = $('#txtLastClickedEleInfo_Basis').val().split('^');

    var instanceType = eleAryInfo[0];
    var paraValues = eleAryInfo[1] + "^" + eleAryInfo[2] + "^" + eleAryInfo[3];
    var idValPair = eleAryInfo[4] + "#" + $('#txtBeastValue_BasisWidget')[0].name;

    SendToBeast(instanceType + "#" + paraValues, idValPair);
}

function Close_BasisWidget() {
    try {

        Close_Term_BasisWidget();
        $('#tblBasis').hide();
    }
    catch (err) {
        //alert('Close_BasisWidget');
    }
}

function DayCount_selection_value() {
    try {
        //var value = document.getElementById('FormatSelection').value;
        var value = $('#DayCountSelection').val();
        IsChange_Bt = 'TRUE';

        for (var i = 2; i < 7; i++) {
            for (var j = 0; j < 5; j++) {
                $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).removeClass('btn btn-info');
                $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).addClass('btn');
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:DayCount_selection_value(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function ShowFullValue_Bt() {
    setBasisValue();
}

function setBasisValue() {
    try {
        if ($('#txtBeastValue_BasisWidget').val().indexOf('/') > 0) {

            if ($('#txtBeastValue_BasisWidget').val().split('/')[0].indexOf('-') == 0)
                $('#txtFrequency').val("(" + $('#txtBeastValue_BasisWidget').val().split('/')[0].replace('-', '') + ")");
            else
                $('#txtFrequency').val($('#txtBeastValue_BasisWidget').val().split('/')[0]);

            var ddl = document.getElementById('DayCountSelection');
            var opts = ddl.options.length;
            for (var i = 0; i < opts; i++) {
                if (ddl.options[i].value == $('#txtBeastValue_BasisWidget').val().split('/')[1]) {
                    ddl.options[i].selected = true;
                    break;
                }
            }
            for (var i = 2; i < 7; i++) {
                for (var j = 0; j < 5; j++) {
                    $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).addClass('btn');
                }
            }
        }
        else {
            //
            var DayCount = Get_DayCount_Frequency($('#txtBeastValue_BasisWidget').val()).split('#')[0].toString();
            var Frequency = Get_DayCount_Frequency($('#txtBeastValue_BasisWidget').val()).split('#')[1].toString();
            $('#txtFrequency').val(Frequency);

            for (var i = 2; i < 7; i++) {
                for (var j = 0; j < 5; j++) {
                    if ($('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).val() == $('#txtBeastValue_BasisWidget').val()) {
                        $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).removeClass('btn');
                        $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).addClass('btn btn-info');
                    }
                    else {
                        $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).removeClass('btn btn-info');
                        $('#btn' + j.toString() + 'B_td' + i.toString() + 'tr' + j.toString()).addClass('btn');
                    }
                }
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:setBasisValue(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function Bind_Basis_Widget() {
    try {
        //alert($('#FormatSelection').val());

        $("#table_widget_Basis").html("");
        for (var i = 0; i < 5; i++) {
            var varHtmlTemplate = basis_widget_Template_tablerow();
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TR]', "B_tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD1]', "B_td1tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD2]', "B_td2tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD3]', "B_td3tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD4]', "B_td4tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD5]', "B_td5tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD6]', "B_td6tr" + i.toString());

            $("#table_widget_Basis").append(varHtmlTemplate);
        }

        FirstTDBasis();
        SecondTDBasis();
        ThirdTDBasis();
        ForthTDBasis();
        FifthTDBasis();
        SixthTDBasis();
    }
    catch (err) {
        var strerrordesc = "Function:Bind_Basis_Widget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function FirstTDBasis() {

    //START ******************************************************** First TD ********************************************************************
    //B_td1tr[]           First B_td(column) for All tr(row)
    //btn[]B_td1tr[]      button  First B_td(column) for All tr(row)
    {
        try {
        
            for (var j = 0; j < 5; j++) {

                if (j == 0) {
                    continue;
                }
                else if (j == 1) {
                    $('#B_td1tr' + j.toString()).html("ACT/360");
                }
                else if (j == 2) {
                    $('#B_td1tr' + j.toString()).html("ACT/365");
                }
                else if (j == 3) {
                    $('#B_td1tr' + j.toString()).html("30/360");
                }
                else if (j == 4) {
                    $('#B_td1tr' + j.toString()).html("30E/360");
                }
            }
        }
        catch (err) {
            var strerrordesc = "Function:FirstTDBasis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** First TD ********************************************************************
}

function SecondTDBasis() {
    //START ****************************************************** Second TD ********************************************************************
    //B_td2tr[]           Second B_td(column) for All tr(row)
    //btn[]B_td2tr[]      button  Second B_td(column) for All tr(row)
    {
        try {


            for (var j = 0; j < 5; j++) {

                if (j == 0) {
                    $('#B_td2tr' + j.toString()).html("Annu");
                    $('#B_td2tr' + j.toString()).addClass("WidgetHeader");
                    continue;
                }

                var varbuttonTemplate = basis_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "B_td2tr" + j.toString());
                $('#B_td2tr' + j.toString()).html(varbuttonTemplate);

                if (j == 1) {
                    $('#btn' + j.toString() + 'B_td2tr' + j.toString()).val("AM");
                }
                else if (j == 2) {
                    $('#btn' + j.toString() + 'B_td2tr' + j.toString()).val("A5");
                }
                else if (j == 3) {
                    $('#btn' + j.toString() + 'B_td2tr' + j.toString()).val("AB");
                }
                else if (j == 4) {
                    $('#btn' + j.toString() + 'B_td2tr' + j.toString()).val("AE");
                }

                $('#btn' + j.toString() + 'B_td2tr' + j.toString()).click(function () {
                    //alert($(this).val());

                    var DayCount = Get_DayCount_Frequency($(this).val()).split('#')[0].toString();
                    var Frequency = Get_DayCount_Frequency($(this).val()).split('#')[1].toString();
                    $('#txtFrequency').val(Frequency);

                    var ddl = document.getElementById('DayCountSelection');
                    var opts = ddl.options.length;
                    for (var i = 0; i < opts; i++) {
                        if (ddl.options[i].value == DayCount) {
                            ddl.options[i].selected = true;
                            break;
                        }
                    }

                    $('#txtBeastValue_BasisWidget').val($(this).val());
                    /// $('#txtBeastValue_BasisWidget')[0].name = $(this).val();

                    /////
                    if (ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val()) != "")
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val());
                    else {
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertToTermBeastValue_Basis($('#txtBeastValue_BasisWidget').val().split('/')[0]) + ConvertTo_Rev_DayCount_Basis(DayCountSelection_MainValue($('#txtBeastValue_BasisWidget').val().split('/')[1]));
                    }

                    // alert($('#txtBeastValue_BasisWidget')[0].name);
                    sendServerValue_BasisWidget();

                    $('#tblBasis').hide();

                    ShowFullValue_Bt();

                });
            }
        }
        catch (err) {
            var strerrordesc = "Function:SecondTDBasis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Second TD ********************************************************************
}

function ThirdTDBasis() {
    //START ****************************************************** Third TD ********************************************************************
    //B_td3tr[]           Third B_td(column) for All tr(row)
    //btn[]B_td3tr[]      button  Third B_td(column) for All tr(row)
    {
        try {

            for (var j = 0; j < 5; j++) {

                if (j == 0) {
                    $('#B_td3tr' + j.toString()).addClass("WidgetHeader");
                    $('#B_td3tr' + j.toString()).html("Semi");
                    continue;
                }

                var varbuttonTemplate = basis_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "B_td3tr" + j.toString());
                $('#B_td3tr' + j.toString()).html(varbuttonTemplate);

                if (j == 1) {
                    $('#btn' + j.toString() + 'B_td3tr' + j.toString()).val("SM");
                }
                else if (j == 2) {
                    $('#btn' + j.toString() + 'B_td3tr' + j.toString()).val("S5");
                }
                else if (j == 3) {
                    $('#btn' + j.toString() + 'B_td3tr' + j.toString()).val("SB");
                }
                else if (j == 4) {
                    $('#btn' + j.toString() + 'B_td3tr' + j.toString()).val("SE");
                }

                $('#btn' + j.toString() + 'B_td3tr' + j.toString()).click(function () {
                    //alert($(this).val());

                    var DayCount = Get_DayCount_Frequency($(this).val()).split('#')[0].toString();
                    var Frequency = Get_DayCount_Frequency($(this).val()).split('#')[1].toString();
                    $('#txtFrequency').val(Frequency);

                    var ddl = document.getElementById('DayCountSelection');
                    var opts = ddl.options.length;
                    for (var i = 0; i < opts; i++) {
                        if (ddl.options[i].value == DayCount) {
                            ddl.options[i].selected = true;
                            break;
                        }
                    }

                    $('#txtBeastValue_BasisWidget').val($(this).val());
                    ///$('#txtBeastValue_BasisWidget')[0].name = $(this).val();
                    /////
                    if (ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val()) != "")
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val());
                    else {
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertToTermBeastValue_Basis($('#txtBeastValue_BasisWidget').val().split('/')[0]) + ConvertTo_Rev_DayCount_Basis(DayCountSelection_MainValue($('#txtBeastValue_BasisWidget').val().split('/')[1]));
                    }

                    //alert($('#txtBeastValue_BasisWidget')[0].name);
                    sendServerValue_BasisWidget();
                    $('#tblBasis').hide();

                    ShowFullValue_Bt();

                });
            }
        }
        catch (err) {
            var strerrordesc = "Function:ThirdTDBasis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Third TD ********************************************************************
}

function ForthTDBasis() {
    //START ****************************************************** Forth TD ********************************************************************
    //B_td4tr[]           Forth B_td(column) for All tr(row)
    //btn[]B_td4tr[]      button  Forth B_td(column) for All tr(row)
    {
        try {

            for (var j = 0; j < 5; j++) {

                if (j == 0) {
                    $('#B_td4tr' + j.toString()).html("Quarter");
                    continue;
                }

                var varbuttonTemplate = basis_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "B_td4tr" + j.toString());
                $('#B_td4tr' + j.toString()).html(varbuttonTemplate);

                if (j == 1) {
                    $('#btn' + j.toString() + 'B_td4tr' + j.toString()).val("QM");
                }
                else if (j == 2) {
                    $('#btn' + j.toString() + 'B_td4tr' + j.toString()).val("Q5");
                }
                else if (j == 3) {
                    $('#btn' + j.toString() + 'B_td4tr' + j.toString()).val("QB");
                }
                else if (j == 4) {
                    $('#btn' + j.toString() + 'B_td4tr' + j.toString()).val("QE");
                }

                $('#btn' + j.toString() + 'B_td4tr' + j.toString()).click(function () {
                    //alert($(this).val());

                    var DayCount = Get_DayCount_Frequency($(this).val()).split('#')[0].toString();
                    var Frequency = Get_DayCount_Frequency($(this).val()).split('#')[1].toString();
                    $('#txtFrequency').val(Frequency);

                    var ddl = document.getElementById('DayCountSelection');
                    var opts = ddl.options.length;
                    for (var i = 0; i < opts; i++) {
                        if (ddl.options[i].value == DayCount) {
                            ddl.options[i].selected = true;
                            break;
                        }
                    }

                    $('#txtBeastValue_BasisWidget').val($(this).val());
                    ///$('#txtBeastValue_BasisWidget')[0].name = $(this).val();
                    /////
                    if (ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val()) != "")
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val());
                    else {
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertToTermBeastValue_Basis($('#txtBeastValue_BasisWidget').val().split('/')[0]) + ConvertTo_Rev_DayCount_Basis(DayCountSelection_MainValue($('#txtBeastValue_BasisWidget').val().split('/')[1]));
                    }

                    //alert($('#txtBeastValue_BasisWidget')[0].name);
                    sendServerValue_BasisWidget();
                    $('#tblBasis').hide();

                    ShowFullValue_Bt();
                });
            }
        }
        catch (err) {
            var strerrordesc = "Function:ForthTDBasis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Forth TD ********************************************************************
}

function FifthTDBasis() {
    //START ****************************************************** Fifth TD ********************************************************************
    //B_td5tr[]           Fifth B_td(column) for All tr(row)
    //btn[]B_td5tr[]      button  Fifth B_td(column) for All tr(row)
    {
        try {

            for (var j = 0; j < 5; j++) {

                if (j == 0) {
                    $('#B_td5tr' + j.toString()).html("Month");
                    continue;
                }

                var varbuttonTemplate = basis_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "B_td5tr" + j.toString());
                $('#B_td5tr' + j.toString()).html(varbuttonTemplate);

                if (j == 1) {
                    $('#btn' + j.toString() + 'B_td5tr' + j.toString()).val("MM");
                }
                else if (j == 2) {
                    $('#btn' + j.toString() + 'B_td5tr' + j.toString()).val("M5");
                }
                else if (j == 3) {
                    $('#btn' + j.toString() + 'B_td5tr' + j.toString()).val("MB");
                }
                else if (j == 4) {
                    $('#btn' + j.toString() + 'B_td5tr' + j.toString()).val("ME");
                }

                $('#btn' + j.toString() + 'B_td5tr' + j.toString()).click(function () {
                    //alert($(this).val());

                    var DayCount = Get_DayCount_Frequency($(this).val()).split('#')[0].toString();
                    var Frequency = Get_DayCount_Frequency($(this).val()).split('#')[1].toString();
                    $('#txtFrequency').val(Frequency);

                    var ddl = document.getElementById('DayCountSelection');
                    var opts = ddl.options.length;
                    for (var i = 0; i < opts; i++) {
                        if (ddl.options[i].value == DayCount) {
                            ddl.options[i].selected = true;
                            break;
                        }
                    }
                    $('#txtBeastValue_BasisWidget').val($(this).val());
                    ///$('#txtBeastValue_BasisWidget')[0].name = $(this).val();
                    /////
                    if (ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val()) != "")
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val());
                    else {
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertToTermBeastValue_Basis($('#txtBeastValue_BasisWidget').val().split('/')[0]) + ConvertTo_Rev_DayCount_Basis(DayCountSelection_MainValue($('#txtBeastValue_BasisWidget').val().split('/')[1]));
                    }
                    // alert($('#txtBeastValue_BasisWidget')[0].name);
                    sendServerValue_BasisWidget();
                    $('#tblBasis').hide();

                    ShowFullValue_Bt();
                });
            }
        }
        catch (err) {
            var strerrordesc = "Function:FifthTDBasis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Fifth TD ********************************************************************
}

function SixthTDBasis() {
    //START ****************************************************** Sixth TD ********************************************************************
    //B_td6tr[]           Sixth B_td(column) for All tr(row)
    //btn[]B_td6tr[]      button  Sixth B_td(column) for All tr(row)
    {
        try {

            for (var j = 0; j < 5; j++) {

                if (j == 0) {
                    $('#B_td6tr' + j.toString()).html("Zero");
                    continue;
                }

                var varbuttonTemplate = basis_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "B_td6tr" + j.toString());
                $('#B_td6tr' + j.toString()).html(varbuttonTemplate);

                if (j == 1) {
                    $('#btn' + j.toString() + 'B_td6tr' + j.toString()).val("ZM");
                }
                else if (j == 2) {
                    $('#btn' + j.toString() + 'B_td6tr' + j.toString()).val("Z5");
                }
                else if (j == 3) {
                    $('#btn' + j.toString() + 'B_td6tr' + j.toString()).val("ZB");
                }
                else if (j == 4) {
                    $('#btn' + j.toString() + 'B_td6tr' + j.toString()).val("ZE");
                }

                $('#btn' + j.toString() + 'B_td6tr' + j.toString()).click(function () {
                    //alert($(this).val());
                    
                    var DayCount = Get_DayCount_Frequency($(this).val()).split('#')[0].toString();
                    var Frequency = Get_DayCount_Frequency($(this).val()).split('#')[1].toString();
                    $('#txtFrequency').val(Frequency);

                    var ddl = document.getElementById('DayCountSelection');
                    var opts = ddl.options.length;
                    for (var i = 0; i < opts; i++) {
                        if (ddl.options[i].value == DayCount) {
                            ddl.options[i].selected = true;
                            break;
                        }
                    }

                    $('#txtBeastValue_BasisWidget').val($(this).val());
                    /// $('#txtBeastValue_BasisWidget')[0].name = $(this).val();
                    /////
                    if (ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val()) != "")
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertTo_Rev_DirectValue_Basis($('#txtBeastValue_BasisWidget').val());
                    else {
                        $('#txtBeastValue_BasisWidget')[0].name = ConvertToTermBeastValue_Basis($('#txtBeastValue_BasisWidget').val().split('/')[0]) + ConvertTo_Rev_DayCount_Basis(DayCountSelection_MainValue($('#txtBeastValue_BasisWidget').val().split('/')[1]));
                    }

                    //alert($('#txtBeastValue_BasisWidget')[0].name);
                    sendServerValue_BasisWidget();
                    $('#tblBasis').hide();

                    ShowFullValue_Bt();

                });
            }
        }
        catch (err) {
            var strerrordesc = "Function:SixthTDBasis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Sixth TD ********************************************************************
}

var currentValueYearMth_Bt = '';
var currentValueMonths_Bt = '';
var currentValueWeeks_Bt = '';
var currentValueDays_Bt = '';

var DefaultTermFormat_Bt = '';
var OriginalTermFormat_Bt = '';
var ClickTermFormat_Bt = '';
var ClickTermFormatValue_Bt = '';
var globaltxtValue1_Bt = '';
var IsChange_Bt = '';

function display_Term_BasisWidget(objClicked) {
    try {
        var offset = $(objClicked).offset();
        var height = $(objClicked).outerHeight(true);

        var x = offset.top;
        var y = offset.left;

        $('#divTermBasisWidget').css({
            top: x + height,
            left: parseInt(y) + 280
        });

        $('#divTermBasisWidget').show();
        $('#childDivBasis').hide();

        DefaultTermFormat_Bt = TermFormat.Year;
        OriginalTermFormat_Bt = TermFormat.Year;
        $('#txtValue1_BasisWidget').val($('#txtFrequency').val());

        ClickTermFormatValue_Bt = $('#txtValue1_BasisWidget').val();
        ClickTermFormat_Bt = TermFormat.Year;

        if ($('#txtValue1_BasisWidget').val().indexOf('(') > -1)
            globaltxtValue1_Bt = "(" + $('#txtValue1_BasisWidget').val().replace('(', '').replace(')', '') + ")";
        else
            globaltxtValue1_Bt = $('#txtValue1_BasisWidget').val();

        Bind_Basis_Term_Widget(DefaultTermFormat_Bt);

        //                $('#btnYearMth_Basis_Term_Widget').focus();
        //                $('#btnYearMth_Basis_Term_Widget').click();

    }
    catch (err) {
        var strerrordesc = "Function:display_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function Save_Term_BasisWidget() {
    try {
        //if (IsChange_Bt == 'TRUE') 
        {
            var IsNegative = '';

            ////////////////////   Frequency

            if (DefaultTermFormat_Bt == TermFormat.Year) {
                var txtValue1 = $('#txtValue1_BasisWidget').val();


                if (txtValue1.indexOf('(') > -1) {
                    IsNegative = 'TRUE';
                    txtValue1 = txtValue1.replace('(', '').replace(')', '');
                }
                else {

                }

                if (txtValue1.indexOf('Y') > -1 && txtValue1.indexOf('0M') > -1) {
                    if (txtValue1.indexOf('10M') > -1) {
                        ///////////
                        if (txtValue1.indexOf('Y') > -1) {
                            Years = txtValue1.split('Y');
                            if (Years.length > 0) {
                                value = parseInt(Years[0]) * 12;
                                if (Years[1] == "")
                                    Months = "0";
                                else {
                                    if (Years[1].indexOf('M') > -1)
                                        Months = Years[1].substring(0, Years[1].indexOf('M'));
                                    else
                                        Months = Years[1];
                                }
                                value = value + parseInt(Months);
                            }
                        }
                        else {
                            if (txtValue1.indexOf('M') > -1)
                                Months = txtValue1.substring(0, txtValue1.indexOf('M'));
                            else
                                Months = txtValue1;
                            value = parseInt(Months);

                        }
                        $('#txtValue1_BasisWidget').val(GetMonths(parseInt(OriginalTermFormat_Bt), value).toString());
                        ////////////
                    }
                    else {
                        $('#txtValue1_BasisWidget').val(txtValue1.split('Y')[0] + "Y");

                        if ($('#txtValue1_BasisWidget').val().indexOf('(') > -1) {
                            if ($('#txtValue1_BasisWidget').val().indexOf(')') > -1) {
                            }
                            else {
                                $('#txtValue1_BasisWidget').val($('#txtValue1_BasisWidget').val() + ")");
                            }
                        }
                    }
                }
                else {
                    if (txtValue1.indexOf('Y') > -1) {
                        Years = txtValue1.split('Y');
                        if (Years.length > 0) {
                            value = parseInt(Years[0]) * 12;
                            if (Years[1] == "")
                                Months = "0";
                            else {
                                if (Years[1].indexOf('M') > -1)
                                    Months = Years[1].substring(0, Years[1].indexOf('M'));
                                else
                                    Months = Years[1];
                            }
                            value = value + parseInt(Months);
                        }
                    }
                    else {
                        if (txtValue1.indexOf('M') > -1)
                            Months = txtValue1.substring(0, txtValue1.indexOf('M'));
                        else
                            Months = txtValue1;
                        value = parseInt(Months);

                    }

                    $('#txtValue1_BasisWidget').val(GetMonths(parseInt(OriginalTermFormat_Bt), value).toString());
                }
            }

            if (IsNegative == 'TRUE' && $('#txtValue1_BasisWidget').val() != '0Y' && $('#txtValue1_BasisWidget').val() != '0M' && $('#txtValue1_BasisWidget').val() != '0W' && $('#txtValue1_BasisWidget').val() != '0D' && $('#txtValue1_BasisWidget').val() != '0B')
                $('#txtFrequency').val("(" + $('#txtValue1_BasisWidget').val().replace('(', '').replace(')', '') + ")");
            else
                $('#txtFrequency').val($('#txtValue1_BasisWidget').val().replace('(', '').replace(')', ''));

            /////////////////////////////     Frequency            
        }

        $('#divTermBasisWidget').hide();
        $('#childDivBasis').show();

    }
    catch (err) {
        var strerrordesc = "Function:Save_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}

function Close_Term_BasisWidget() {
    try {

        $('#divTermBasisWidget').hide();
        $('#childDivBasis').show();
    }
    catch (err) {
        var strerrordesc = "Function:Close_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function FindOriginalTermFormat_basis() {
    try {
        // var txtValue1 = $('#txtValue1_BasisWidget').val();
        var txtValue1 = ClickTermFormatValue_Bt;

        if (txtValue1.indexOf('Y') > -1) {
            OriginalTermFormat_Bt = 3;
        }
        else if (txtValue1.indexOf('M') > -1) {
            OriginalTermFormat_Bt = 3;
        }
        else if (txtValue1.indexOf('W') > -1) {
            OriginalTermFormat_Bt = 1;
        }
        else if (txtValue1.indexOf('B') > -1) {
            OriginalTermFormat_Bt = 0;
        }
        else if (txtValue1.indexOf('D') > -1) {
            OriginalTermFormat_Bt = 0;
        }
    }
    catch (err) {
        var strerrordesc = "Function:FindOriginalTermFormat(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function ShowFullValue_Term_BasisWidget() {
    try {
        //string[] Array
        var Years;
        ///string
        var Months = "0";
        ///int
        var value = 0;

        //var txtValue1 = $('#txtValue1_BasisWidget').val().replace('(', '').replace(')', '');
        var txtValue1 = ClickTermFormatValue_Bt.replace('(', '').replace(')', '');

        if (txtValue1 != "") {

            FindOriginalTermFormat_basis();
            if (OriginalTermFormat_Bt == 3) //Original Format is Y/M or M
            {
                if (txtValue1.indexOf('Y') > -1) {
                    Years = txtValue1.split('Y');
                    if (Years.length > 0) {
                        value = parseInt(Years[0]) * 12;
                        if (Years[1] == "")
                            Months = "0";
                        else {
                            if (Years[1].indexOf('M') > -1)
                                Months = Years[1].substring(0, Years[1].indexOf('M'));
                            else
                                Months = Years[1];
                        }
                        value = value + parseInt(Months);
                    }
                }
                else {
                    if (txtValue1.indexOf('M') > -1)
                        Months = txtValue1.substring(0, txtValue1.indexOf('M'));
                    else
                        Months = txtValue1;
                    value = parseInt(Months);

                }
            }
            else if (OriginalTermFormat_Bt == 1) //Original Format is Weeks
            {
                if (txtValue1.indexOf('W') > -1)
                    value = parseInt(txtValue1.substring(0, txtValue1.indexOf('W')));
                else
                    value = parseInt(txtValue1);
            }
            else if (OriginalTermFormat_Bt == 0 || OriginalTermFormat_Bt == 0) //Original Format is Days/BusinessDays
            {
                if (txtValue1.indexOf("B") > -1)
                    value = parseInt(txtValue1.substring(0, txtValue1.indexOf("B")));
                else if (txtValue1.indexOf('D') > -1)
                    value = parseInt(txtValue1.substring(0, txtValue1.indexOf('D')));
                else
                    value = parseInt(txtValue1);
            }
            if (DefaultTermFormat_Bt == TermFormat.Days) //Convert into Days
            {
                $('#txtValue1_BasisWidget').val(GetDays(parseInt(OriginalTermFormat_Bt), value, TermFormat.Days).toString());
            }
            else if (DefaultTermFormat_Bt == TermFormat.BusDays) //Convert into Business Days
            {
                $('#txtValue1_BasisWidget').val(GetDays(parseInt(OriginalTermFormat_Bt), value, TermFormat.BusDays).toString().replace('BD', 'B'));
            }
            else if (DefaultTermFormat_Bt == TermFormat.Weeks) //Convert into Weeks
            {
                $('#txtValue1_BasisWidget').val(GetWeeks(parseInt(OriginalTermFormat_Bt), value).toString());
            }
            else if (DefaultTermFormat_Bt == TermFormat.Year) //Convert into Years/Months
            {
                $('#txtValue1_BasisWidget').val(GetYearsMonths(parseInt(OriginalTermFormat_Bt), value).toString());
            }
            else if (DefaultTermFormat_Bt == TermFormat.Months) //Convert into Months
            {
                $('#txtValue1_BasisWidget').val(GetMonths(parseInt(OriginalTermFormat_Bt), value).toString() + "O");
            }
        }

        setTerm_BasisWidget();

        if (globaltxtValue1_Bt.indexOf("(")) {
            $('#btn11bt_td1tr11').removeClass('btn btn-info');
            $('#btn11bt_td1tr11').addClass('btn');
        }
        else {
            $('#txtValue1_BasisWidget').val("(" + $('#txtValue1_BasisWidget').val().replace('(', '').replace(')', '') + ")");
            $('#btn11bt_td1tr11').removeClass('btn');
            $('#btn11bt_td1tr11').addClass('btn btn-info');
        }
    }
    catch (err) {
        var strerrordesc = "Function:ShowFullValue_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }

}

function setTerm_BasisWidget() {
    try {

        var txtValue1 = $('#txtValue1_BasisWidget').val();

        if (txtValue1.indexOf('Y') > -1) {

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val() == txtValue1.split('Y')[0] + " Year") {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val() == txtValue1.split('Y')[0] + " Year") {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn');
                }
            }


            for (var j = 0; j < 11; j++) {
                if ($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val() == txtValue1.split('Y')[0] + " Year") {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn');
                }
            }

            if (txtValue1.split('Y').length > 0) {
                for (var j = 0; j < 12; j++) {
                    if ($('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val() == txtValue1.split('Y')[1].replace('M', '') + " Month") {
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn');
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn btn-info');
                    }
                    else {
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn btn-info');
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn');
                    }
                }
            }

        }
        else if (txtValue1.indexOf('M') > -1) {

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn');
                }
            }


            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn');
                }
            }

        }
        else if (txtValue1.indexOf('W') > -1) {
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn');
                }
            }
        }
        else if (txtValue1.indexOf('B') > -1) {
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val() == txtValue1.split('B')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val() == txtValue1.split('B')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn');
                }
            }
            
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val() == txtValue1.split('B')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 12; j++) {
                if ($('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val() == txtValue1.split('B')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn');
                }
            }
        }
        else if (txtValue1.indexOf('D') > -1) {
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).addClass('btn');
                }
            }
            
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 12; j++) {
                if ($('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).addClass('btn');
                }
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:setTermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function selected_term_format_basis() {
    try {

        if (DefaultTermFormat_Bt == TermFormat.Year) {
            $('#btnYearMth_Basis_Term_Widget').removeClass('btn');
            $('#btnYearMth_Basis_Term_Widget').addClass('btn btn-info');
            $('#btnMonths_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnMonths_Basis_Term_Widget').addClass('btn');
            $('#btnWeeks_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnWeeks_Basis_Term_Widget').addClass('btn');
            $('#btnDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnDays_Basis_Term_Widget').addClass('btn');
            $('#btnBusDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnBusDays_Basis_Term_Widget').addClass('btn');
        }
        else if (DefaultTermFormat_Bt == TermFormat.Months) {
            $('#btnYearMth_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnYearMth_Basis_Term_Widget').addClass('btn');
            $('#btnMonths_Basis_Term_Widget').removeClass('btn');
            $('#btnMonths_Basis_Term_Widget').addClass('btn btn-info');
            $('#btnWeeks_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnWeeks_Basis_Term_Widget').addClass('btn');
            $('#btnDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnDays_Basis_Term_Widget').addClass('btn');
            $('#btnBusDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnBusDays_Basis_Term_Widget').addClass('btn');
        }
        else if (DefaultTermFormat_Bt == TermFormat.Weeks) {
            $('#btnYearMth_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnYearMth_Basis_Term_Widget').addClass('btn');
            $('#btnMonths_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnMonths_Basis_Term_Widget').addClass('btn');
            $('#btnWeeks_Basis_Term_Widget').removeClass('btn');
            $('#btnWeeks_Basis_Term_Widget').addClass('btn btn-info');
            $('#btnDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnDays_Basis_Term_Widget').addClass('btn');
            $('#btnBusDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnBusDays_Basis_Term_Widget').addClass('btn');
        }
        else if (DefaultTermFormat_Bt == TermFormat.Days) {
            $('#btnYearMth_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnYearMth_Basis_Term_Widget').addClass('btn');
            $('#btnMonths_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnMonths_Basis_Term_Widget').addClass('btn');
            $('#btnWeeks_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnWeeks_Basis_Term_Widget').addClass('btn');
            $('#btnDays_Basis_Term_Widget').removeClass('btn');
            $('#btnDays_Basis_Term_Widget').addClass('btn btn-info');
            $('#btnBusDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnBusDays_Basis_Term_Widget').addClass('btn');
        }
        else if (DefaultTermFormat_Bt == TermFormat.BusDays) {
            $('#btnYearMth_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnYearMth_Basis_Term_Widget').addClass('btn');
            $('#btnMonths_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnMonths_Basis_Term_Widget').addClass('btn');
            $('#btnWeeks_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnWeeks_Basis_Term_Widget').addClass('btn');
            $('#btnDays_Basis_Term_Widget').removeClass('btn btn-info');
            $('#btnDays_Basis_Term_Widget').addClass('btn');
            $('#btnBusDays_Basis_Term_Widget').removeClass('btn');
            $('#btnBusDays_Basis_Term_Widget').addClass('btn btn-info');

        }
    }
    catch (err) {
        var strerrordesc = "Function:selected_term_format(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }

}

function display_term_button_string_basis() {
    try {

        if (DefaultTermFormat_Bt == TermFormat.Year)
            return " Year";
        else if (DefaultTermFormat_Bt == TermFormat.Months)
            return " Month";
        else if (DefaultTermFormat_Bt == TermFormat.Weeks)
            return " Week";
        else if (DefaultTermFormat_Bt == TermFormat.Days)
            return " Day";
        else if (DefaultTermFormat_Bt == TermFormat.BusDays)
            return " Bus Day";
    }
    catch (err) {
        var strerrordesc = "Function:display_term_button_string_basis(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function Bind_Basis_Term_Widget(Param_SelectedTermFormat) {
    try {
        //alert(DefaultTermFormat_Bt);

        DefaultTermFormat_Bt = Param_SelectedTermFormat;

        selected_term_format_basis();

        $("#table_widget_Term_Basis").html("");

        for (var i = 0; i < 12; i++) {
            var varHtmlTemplate = term_widget_Template_tablerow();
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TR]', "tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD1]', "bt_td1tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD2]', "bt_td2tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD3]', "bt_td3tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD4]', "bt_td4tr" + i.toString());

            $("#table_widget_Term_Basis").append(varHtmlTemplate);
        }

        FirstTD_Term_BasisWidget();
        SecondTD_Term_BasisWidget();
        ThirdTD_Term_BasisWidget();
        ForthTD_Term_BasisWidget();

        /////////////////////////////////////////////////
        var txtValue1 = $('#txtValue1_BasisWidget').val();

        if (DefaultTermFormat_Bt == TermFormat.Year) {
            if (currentValueYearMth_Bt != "" && IsChange_Bt != 'TRUE') {
                $('#txtValue1_BasisWidget').val(currentValueYearMth_Bt);
            }
            currentValueYearMth_Bt = $('#txtValue1_BasisWidget').val();
        }
        else if (DefaultTermFormat_Bt == TermFormat.Months) {
            if (currentValueMonths_Bt != "" && IsChange_Bt != 'TRUE') {
                $('#txtValue1_BasisWidget').val(currentValueMonths_Bt);
            }
            currentValueMonths_Bt = $('#txtValue1_BasisWidget').val();
        }
        else if (DefaultTermFormat_Bt == TermFormat.Weeks) {
            if (currentValueWeeks_Bt != "" && IsChange_Bt != 'TRUE') {
                $('#txtValue1_BasisWidget').val(currentValueWeeks_Bt);
            }
            currentValueWeeks_Bt = $('#txtValue1_BasisWidget').val();
        }
        else if (DefaultTermFormat_Bt == TermFormat.BusDays) {
            if (currentValueDays_Bt != "" && IsChange_Bt != 'TRUE') {
                $('#txtValue1_BasisWidget').val(currentValueDays_Bt.replace('D', 'BD'));
            }
            currentValueDays_Bt = $('#txtValue1_BasisWidget').val();
        }
        else if (DefaultTermFormat_Bt == TermFormat.Days) {
            if (currentValueDays_Bt != "" && IsChange_Bt != 'TRUE') {
                $('#txtValue1_BasisWidget').val(currentValueDays_Bt);
            }
            currentValueDays_Bt = $('#txtValue1_BasisWidget').val();
        }

        if (IsChange_Bt == 'TRUE') {
            if (DefaultTermFormat_Bt == TermFormat.Year) {

                currentValueMonths_Bt = '';
                currentValueWeeks_Bt = '';
                //currentValueBuyDays = '';
                currentValueDays_Bt = '';

                ClickTermFormat_Bt = TermFormat.Year;
                ClickTermFormatValue_Bt = $('#txtValue1_BasisWidget').val();
            }
            else if (DefaultTermFormat_Bt == TermFormat.Months) {
                currentValueYearMth_Bt = '';
                currentValueWeeks_Bt = '';
                //currentValueBuyDays = '';
                currentValueDays_Bt = '';

                ClickTermFormat_Bt = TermFormat.Months;
                ClickTermFormatValue_Bt = $('#txtValue1_BasisWidget').val();
            }
            else if (DefaultTermFormat_Bt == TermFormat.Weeks) {
                currentValueYearMth_Bt = '';
                currentValueMonths_Bt = '';
                //currentValueBuyDays = '';
                currentValueDays_Bt = '';

                ClickTermFormat_Bt = TermFormat.Weeks;
                ClickTermFormatValue_Bt = $('#txtValue1_BasisWidget').val();
            }
            else if (DefaultTermFormat_Bt == TermFormat.BusDays) {
                currentValueYearMth_Bt = '';
                currentValueMonths_Bt = '';
                currentValueWeeks_Bt = '';
                //currentValueDays_Bt = '';

                ClickTermFormat_Bt = TermFormat.BusDays;
                ClickTermFormatValue_Bt = $('#txtValue1_BasisWidget').val();
            }
            else if (DefaultTermFormat_Bt == TermFormat.Days) {
                currentValueYearMth_Bt = '';
                currentValueMonths_Bt = '';
                currentValueWeeks_Bt = '';
                //currentValueBuyDays = '';

                ClickTermFormat_Bt = TermFormat.Days;
                ClickTermFormatValue_Bt = $('#txtValue1_BasisWidget').val();
            }
            IsChange_Bt = '';
        }
        ////////////////////////////////////////////////////

        ShowFullValue_Term_BasisWidget();
    }
    catch (err) {
        var strerrordesc = "Function:Bind_Term_Widget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("basisWidgetScript.js", strerrordesc);
    }
}


function FirstTD_Term_BasisWidget() {
    //START ******************************************************** First TD ********************************************************************
    //td1tr[]           First td(column) for All tr(row)
    //btn[]td1tr[]      button  First td(column) for All tr(row)
    {
        try {
            var td1btnCount = 0;

            var DisplaybtnString = display_term_button_string_basis();

            for (var j = 0; j < 12; j++) {

                if (j == 10) {
                    continue;
                }

                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "bt_td1tr" + j.toString());
                $('#bt_td1tr' + j.toString()).html(varbuttonTemplate);

                if (j == 11) {
                    $('#btn11bt_td1tr11').val("Negative");

                    var txtValue1 = $('#txtValue1_BasisWidget').val();
                    if (txtValue1.indexOf("(")) {
                        $('#btn11bt_td1tr11').removeClass('btn btn-info');
                        $('#btn11bt_td1tr11').addClass('btn');
                    }
                    else {
                        $('#btn11bt_td1tr11').removeClass('btn');
                        $('#btn11bt_td1tr11').addClass('btn btn-info');
                    }

                    $("#btn11bt_td1tr11").click(function () {

                        var txtValue1 = $('#txtValue1_BasisWidget').val();
                        //alert("Negative");

                        IsChange_Bt = 'TRUE';

                        if (txtValue1.indexOf("(")) {
                            $('#txtValue1_BasisWidget').val("(" + txtValue1 + ")");

                            globaltxtValue1_Bt = "(" + txtValue1 + ")";

                            $('#btn11bt_td1tr11').removeClass('btn');
                            $('#btn11bt_td1tr11').addClass('btn btn-info');
                        }
                        else {
                            $('#txtValue1_BasisWidget').val(txtValue1.replace("(", "").replace(")", ""));

                            globaltxtValue1_Bt = txtValue1.replace("(", "").replace(")", "");

                            $('#btn11bt_td1tr11').removeClass('btn btn-info');
                            $('#btn11bt_td1tr11').addClass('btn');
                        }

                    });
                }
                else {
                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val(td1btnCount.toString() + DisplaybtnString);

                    $('#btn' + j.toString() + 'bt_td1tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange_Bt = 'TRUE';

                        if ($(this).val().indexOf('Y') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_BasisWidget').val().split('Y')[1]).toString());
                        else if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('B') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Bus Day', 'B'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Day', 'D'));

                        Bind_Basis_Term_Widget(DefaultTermFormat_Bt);

                    });

                    td1btnCount++;
                }
            }
        }
        catch (err) {
            var strerrordesc = "Function:FirstTD_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** First TD ********************************************************************
}


function SecondTD_Term_BasisWidget() {
    //START ****************************************************** Second TD ********************************************************************
    //td2tr[]           Second td(column) for All tr(row)
    //btn[]td2tr[]      button  Second td(column) for All tr(row)
    {
        try {
            var td2btnCount = 0;
            var DisplaybtnString = display_term_button_string_basis();

            for (var j = 0; j < 12; j++) {

                if (j == 10 || j == 11) {
                    continue;
                }

                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "bt_td2tr" + j.toString());
                $('#bt_td2tr' + j.toString()).html(varbuttonTemplate);
                
                $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 'bt_td1tr' + j.toString()).val()) + 10).toString() + DisplaybtnString);

                $('#btn' + j.toString() + 'bt_td2tr' + j.toString()).click(function () {
                    //alert($(this).val());

                    IsChange_Bt = 'TRUE';

                    if ($(this).val().indexOf('Y') > -1)
                        $('#txtValue1_BasisWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_BasisWidget').val().split('Y')[1]).toString());
                    else if ($(this).val().indexOf('M') > -1)
                        $('#txtValue1_BasisWidget').val($(this).val().replace(' Month', 'M'));
                    else if ($(this).val().indexOf('W') > -1)
                        $('#txtValue1_BasisWidget').val($(this).val().replace(' Week', 'W'));
                    else if ($(this).val().indexOf('B') > -1)
                        $('#txtValue1_BasisWidget').val($(this).val().replace(' Bus Day', 'B'));
                    else if ($(this).val().indexOf('D') > -1)
                        $('#txtValue1_BasisWidget').val($(this).val().replace(' Day', 'D'));

                    Bind_Basis_Term_Widget(DefaultTermFormat_Bt);

                });

                td2btnCount++;
            }
        }
        catch (err) {
            var strerrordesc = "Function:SecondTD_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Second TD ********************************************************************
}

function ThirdTD_Term_BasisWidget() {
    //START ****************************************************** Third TD ********************************************************************
    //td3tr[]           Third td(column) for All tr(row)
    //btn[]td3tr[]      button  Third td(column) for All tr(row)
    {
        try {

            var td3btnCount = 0;
            var DisplaybtnString = display_term_button_string_basis();

            for (var j = 0; j < 12; j++) {

                if (j == 11 || (j == 10 && DefaultTermFormat_Bt != TermFormat.Year)) {
                    continue;
                }

                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "bt_td3tr" + j.toString());
                $('#bt_td3tr' + j.toString()).html(varbuttonTemplate);

                if (j != 10) {

                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 'bt_td2tr' + j.toString()).val()) + 10).toString() + DisplaybtnString);

                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange_Bt = 'TRUE';

                        if ($(this).val().indexOf('Y') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_BasisWidget').val().split('Y')[1]).toString());
                        else if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('B') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Bus Day', 'B'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Day', 'D'));

                        Bind_Basis_Term_Widget(DefaultTermFormat_Bt);
                    });

                }
                else if (j == 10 && DefaultTermFormat_Bt == TermFormat.Year) {
                    ///TermFormat.Year
                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val("30" + DisplaybtnString);

                    $('#btn' + j.toString() + 'bt_td3tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange_Bt = 'TRUE';

                        if ($(this).val().indexOf('Y') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_BasisWidget').val().split('Y')[1]).toString());
                        else if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('B') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Bus Day', 'B'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Day', 'D'));

                        Bind_Basis_Term_Widget(DefaultTermFormat_Bt);

                    });
                }

                td3btnCount++;
            }
        }
        catch (err) {
            var strerrordesc = "Function:ThirdTD_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }

    }
    //END ******************************************************** Third TD ********************************************************************
}


function ForthTD_Term_BasisWidget() {
    //START ****************************************************** Forth TD ********************************************************************
    //td4tr[]           Forth td(column) for All tr(row)
    //btn[]td4tr[]      button  Forth td(column) for All tr(row)
    {
        try {

            var td4btnCount = 0;
            var DisplaybtnString = display_term_button_string_basis();

            for (var j = 0; j < 12; j++) {
            
                /// TermFormat.Months
                if ((j == 10 || j == 11) && (DefaultTermFormat_Bt == TermFormat.Months || DefaultTermFormat_Bt == TermFormat.Weeks))
                    continue;
                    
                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "bt_td4tr" + j.toString());
                $('#bt_td4tr' + j.toString()).html(varbuttonTemplate);

                if (DefaultTermFormat_Bt == TermFormat.Year) {
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val(td4btnCount.toString() + " Month");
                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange_Bt = 'TRUE';

                        if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_BasisWidget').val(($('#txtValue1_BasisWidget').val().split('Y')[0] + 'Y' + $(this).val().replace(' Month', 'M')).toString());

                        Bind_Basis_Term_Widget(DefaultTermFormat_Bt);

                    });
                }
                else if (DefaultTermFormat_Bt == TermFormat.Months || DefaultTermFormat_Bt == TermFormat.Weeks || DefaultTermFormat_Bt == TermFormat.Days || DefaultTermFormat_Bt == TermFormat.BusDays) {

                    if ((j == 7 || j == 8 || j == 9) && (DefaultTermFormat_Bt == TermFormat.Months)) {
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val((parseInt($('#btn' + (parseInt(j) - 1).toString() + 'bt_td4tr' + (parseInt(j) - 1).toString()).val()) + 12).toString().toString() + " Month");
                    }
                    else if ((j == 7 || j == 8 || j == 9) && (DefaultTermFormat_Bt == TermFormat.Weeks)) {
                        if (j == 7)
                            $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val("40" + " Week");
                        else if (j == 8)
                            $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val("50" + " Week");
                        else if (j == 9)
                            $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val("52" + " Week");
                    }
                    else if (DefaultTermFormat_Bt == TermFormat.Months) {
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val()) + 10).toString().toString() + " Month");
                    }
                    else if (DefaultTermFormat_Bt == TermFormat.Weeks) {
                        $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 'bt_td3tr' + j.toString()).val()) + 10).toString().toString() + " Week");
                    }
                    else if (DefaultTermFormat_Bt == TermFormat.Days || DefaultTermFormat_Bt == TermFormat.BusDays) {

                        var TypeFormat;
                        if (DefaultTermFormat_Bt == TermFormat.Days)
                            TypeFormat = " Day";
                        else if (DefaultTermFormat_Bt == TermFormat.BusDays)
                            TypeFormat = " Bus Day";

                        if (j == 0) {
                            $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val("30" + TypeFormat);
                        }
                        else {
                            $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).val((parseInt($('#btn' + (parseInt(j) - 1).toString() + 'bt_td4tr' + (parseInt(j) - 1).toString()).val()) + 30).toString().toString() + TypeFormat);
                        }
                    }

                    $('#btn' + j.toString() + 'bt_td4tr' + j.toString()).click(function () {
                        // alert($(this).val());

                        IsChange_Bt = 'TRUE';

                        if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('B') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Bus Day', 'B'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_BasisWidget').val($(this).val().replace(' Day', 'D'));

                        Bind_Basis_Term_Widget(DefaultTermFormat_Bt);

                    });
                }

                td4btnCount++;
            }
        }
        catch (err) {
            var strerrordesc = "Function:ForthTD_Term_BasisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("basisWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Forth TD ********************************************************************
}

