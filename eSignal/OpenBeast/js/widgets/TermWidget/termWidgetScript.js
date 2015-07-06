var EnumTermUnit = {
    "e_Days": 0,
    "e_Bdays": 1,
    "e_Weeks": 2,
    "e_Months": 3,
    "e_Years": 4,
    "e_MonthsOnly": 5,
    "e_None": 6
};

function GetTermNear(m_nTerm) {
    return ((m_nTerm >= 0) ? m_nTerm / 1000000 : -((-m_nTerm) / 1000000)) != 0;
}


function GetCount(m_nTerm) {
    return parseInt((m_nTerm >= 0) ? m_nTerm / 10 % 100000 : -((-m_nTerm) / 10 % 100000));
}


function ConvertToTerm(BeastValue) {
    try {

        //last char
        var lastchar = BeastValue.toString().charAt(BeastValue.toString().length - 1);

        if (GetTermNear(parseInt(BeastValue))) {
            BeastValue = GetCount(parseInt(BeastValue)).toString() + lastchar.toString();
        }

        //excluding last char
        var New_value = BeastValue.toString().substr(0, BeastValue.toString().length - 1);

        if (New_value == "")
            New_value = "0";

        if (EnumTermUnit.e_Days == lastchar) {
            //0
            if (New_value.charAt(0) == '-') {
                return "(" + New_value.substr(1, New_value.toString().length) + "D)";
            }
            else {
                return New_value + "D";
            }
        }
        else if (EnumTermUnit.e_Bdays == lastchar) {
            //1
            if (New_value.charAt(0) == '-') {
                return "(" + New_value.substr(1, New_value.toString().length) + "BD)";
            }
            else {
                return New_value + "BD";
            }
        }
        else if (EnumTermUnit.e_Weeks == lastchar) {
            //W
            if (New_value.charAt(0) == '-') {
                return "(" + New_value.substr(1, New_value.toString().length) + "W)";
            }
            else {
                return New_value + "W";
            }
        }
        else if (EnumTermUnit.e_Months == lastchar) {
            //3
            if (New_value.charAt(0) == '-') {

                var Year = parseInt(parseInt(New_value.substr(1, New_value.toString().length)) / 12);
                var Month = parseInt(parseInt(New_value.substr(1, New_value.toString().length)) % 12);

                return "(" + Year.toString() + "Y" + Month.toString() + "M)";
            }
            else {

                var Year = parseInt(parseInt(New_value.substr(0, New_value.toString().length)) / 12);
                var Month = parseInt(parseInt(New_value.substr(0, New_value.toString().length)) % 12);

                return Year.toString() + "Y" + Month.toString() + "M";
            }
        }
        else if (EnumTermUnit.e_Years == lastchar) {
            //4
            if (New_value.charAt(0) == '-') {
                return "(" + New_value.substr(1, New_value.toString().length) + "Y)";
            }
            else {
                return New_value + "Y";
            }
        }
        else if (EnumTermUnit.e_MonthsOnly == lastchar) {
            //5
            if (New_value.charAt(0) == '-') {

                return "(" + New_value.substr(1, New_value.toString().length) + "M)";
            }
            else {

                return New_value + "M";
            }
        }
        else if (EnumTermUnit.e_None == lastchar) {
            //6
        }
    }
    catch (err) {
        var strerrordesc = "Function:ConvertToTerm(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}


function ConvertToTermBeastValue(TermValue) {
    try {
        var IsNegativeFormat = 'False';
        if (TermValue.indexOf('(') > -1) {
            IsNegativeFormat = 'True';
        }

        TermValue = TermValue.replace('(', '').replace(')', '');
        var finalTermValue = '';

        if (TermValue.indexOf('Y') > -1) {

            var YearValue = TermValue.split('Y')[0];
            var MonthValue = TermValue.replace(TermValue.split('Y')[0] + 'Y', "").split('M')[0];

            if (MonthValue == "")
                MonthValue = "0";

            finalTermValue = ((parseInt(YearValue) * 12) + parseInt(MonthValue)).toString() + "3";

        }
        else if (TermValue.indexOf('M') > -1) {
            finalTermValue = TermValue.split('M')[0] + "5";
        }
        else if (TermValue.indexOf('W') > -1) {
            finalTermValue = TermValue.split('W')[0] + "2";
        }
        else if (TermValue.indexOf('BD') > -1) {
            finalTermValue = TermValue.split('BD')[0] + "1";
        }
        else if (TermValue.indexOf('D') > -1) {
            finalTermValue = TermValue.split('D')[0] + "0";
        }

        if (IsNegativeFormat == 'True') {
            finalTermValue = "-" + finalTermValue;
        }
    }
    catch (err) {
        var strerrordesc = "Function:ConvertToTermBeastValue(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
    return finalTermValue;
}

var TermFormat = {

    "Days": 0,
    "BusDays": 1,
    "Weeks": 2,
    "Year": 3,
    "Months": 4
};

///param1 int, param2 int, param3 int
function GetDays(PrevUnit, PrevValue, ConvertTermFormat) {
    try {
        ///int
        var nReturn = 0;
        switch (PrevUnit) {
            case 2: //Years/Months
                nReturn = parseInt(PrevValue * 365);
                break;
            case 3: //Months
                nReturn = parseInt(((PrevValue * 30.42) + 0.5));
                break;
            case 1: //Weeks
                nReturn = parseInt(PrevValue * 7);
                break;
            case 0: //Days/Business Days
                nReturn = PrevValue;
                break;
        }
        if (ConvertTermFormat == TermFormat.Days)
            return nReturn + "D";
        else
            return nReturn + "BD";
    }
    catch (err) {
        var strerrordesc = "Function:GetDays(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

///param1 int, param2 int
function GetWeeks(PrevUnit, PrevValue) {
    try {
        ///int
        var nReturn = 0;
        switch (PrevUnit) {
            case 2: //Years/Months
                nReturn = parseInt(PrevValue * 52);
                break;
            case 3: //Months
                nReturn = parseInt(((PrevValue * 4.33) + 0.5));
                break;
            case 1: //Weeks
                nReturn = PrevValue;
                break;
            case 0: //Days/Business Days
                nReturn = parseInt(PrevValue / 7);
                break;
        }
        return nReturn + "W";
    }
    catch (err) {
        var strerrordesc = "Function:GetWeeks(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

///param1 int, param2 int
function GetMonths(PrevUnit, PrevValue) {
    try {
        ///int
        var nReturn = 0;
        switch (PrevUnit) {
            case 2: //Years/Months
                nReturn = parseInt(PrevValue * 12);
                break;
            case 3: //Months
                nReturn = PrevValue;
                break;
            case 1: //Weeks
                nReturn = parseInt(((PrevValue / 4.33) + 0.5));
                break;
            case 0: //Days/Business Days
                nReturn = parseInt(((PrevValue / 30.42) + 0.5));
                break;
        }
        return nReturn + "M";
    }
    catch (err) {
        var strerrordesc = "Function:GetMonths(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

///param1 int, param2 int
function GetYearsMonths(PrevUnit, PrevValue) {
    try {
        ///int
        var nReturn = 0;
        ///string
        var OnlyMonths = GetMonths(parseInt(PrevUnit), PrevValue).toString();
        PrevUnit = 3;
        PrevValue = parseInt(OnlyMonths.substring(0, OnlyMonths.indexOf('M')));
        switch (PrevUnit) {
            case 2: //Years/Months
                nReturn = PrevValue;
                break;
            case 3: //Months
                nReturn = parseInt(PrevValue / 12);
                break;
            case 1: //Weeks
                nReturn = parseInt((PrevValue / 52));
                break;
            case 0: //Days/Business Days
                nReturn = parseInt((PrevValue / 365));
                break;
        }
        var Months = GetReminderMonths(PrevUnit, PrevValue);
        return nReturn + "Y" + Months + "M";
    }
    catch (err) {
        var strerrordesc = "Function:GetYearsMonths(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

///param1 int, param2 int
function GetReminderMonths(PrevUnit, PrevValue) {
    try {
        ///int
        var nReturn = 0;
        switch (PrevUnit) {
            case 2: //Years/Months
                nReturn = 0;
                break;
            case 3: //Months
                nReturn = parseInt(PrevValue % 12);
                break;
            case 1: //Weeks
                nReturn = parseInt(((PrevValue / 4.33) + 0.5) % 12);
                break;
            case 0: //Days/Business Days
                nReturn = parseInt(((PrevValue / 30.42) + 0.5) % 12);
                break;
        }
        return nReturn;
    }
    catch (err) {
        var strerrordesc = "Function:GetReminderMonths(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

//new added

var currentValueYearMth = '';
var currentValueMonths = '';
var currentValueWeeks = '';
var currentValueDays = '';

var DefaultTermFormat = '';
var OriginalTermFormat = '';
var ClickTermFormat = '';
var ClickTermFormatValue = '';
var globaltxtValue1 = '';
var IsChange = '';

function display_TermWidget(eleIDForWidget, defFormat, termEle) {
    try {

        if ($('#hdnMobileWidgetEnable').val() == "1") {
            TWM_Func_DisplayTermWidget(eleIDForWidget, defFormat, termEle);
            return;
        }

        DefaultTermFormat = TermFormat.Year;
        OriginalTermFormat = TermFormat.Year;

        //        var orgVal = $(termEle).attr("name");
        //        var widgetVal = $(termEle).val();

        var orgVal = $(termEle).val();
        var widgetVal = $(termEle).attr("name");

        if (widgetVal == "" || widgetVal == undefined || isNaN(widgetVal))
            widgetVal = "0";

        $('#txtBeastValue_TermWidget').attr("name", widgetVal);

        $('#txtBeastValue_TermWidget').val(orgVal);
        $('#txtLastClickedEleInfo_Term').val(eleIDForWidget);

        $('#txtValue1_TermWidget').val(parseFloat(widgetVal));

        $('#txtValue1_TermWidget').val(ConvertToTerm($('#txtBeastValue_TermWidget')[0].name));
        ClickTermFormatValue = $('#txtValue1_TermWidget').val();
        ClickTermFormat = TermFormat.Year;

        if ($('#txtValue1_TermWidget').val().indexOf('(') > -1)
            globaltxtValue1 = "(" + $('#txtValue1_TermWidget').val().replace('(', '').replace(')', '') + ")";
        else
            globaltxtValue1 = $('#txtValue1_TermWidget').val();

        Bind_Term_Widget(DefaultTermFormat);
        //    }
        //    catch (err) {
        //        alert('$(document).ready');
        //    }

        //    try {       

        $('#divTermWidget').modal(true);

        $('#hdnWgtElement').val($(termEle).attr('id'));

        positionWidget($(termEle).attr('id'));

        $('#divTermWidget').focus();
    }
    catch (err) {
        var strerrordesc = "Function:display_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

function Save_TermWidget() {
    try {
        //if (IsChange == 'TRUE') 
        {

            if ($('#txtValue1_TermWidget').val().indexOf('Y') > -1 && $('#txtValue1_TermWidget').val().indexOf('0M') > -1) {
                if ($('#txtValue1_TermWidget').val().indexOf('10M') > -1) {
                }
                else {
                    $('#txtValue1_TermWidget').val($('#txtValue1_TermWidget').val().split('Y')[0] + "Y");

                    if ($('#txtValue1_TermWidget').val().indexOf('(') > -1) {
                        if ($('#txtValue1_TermWidget').val().indexOf(')') > -1) {
                        }
                        else {
                            $('#txtValue1_TermWidget').val($('#txtValue1_TermWidget').val() + ")");
                        }
                    }
                }
            }

            IsChange = '';
            $('#txtBeastValue_TermWidget')[0].name = ConvertToTermBeastValue($('#txtValue1_TermWidget').val());
            $('#txtBeastValue_TermWidget').val($('#txtValue1_TermWidget').val());

            var eleAryInfo = $('#txtLastClickedEleInfo_Term').val().split('^');

            var instanceType = eleAryInfo[0];
            var paraValues = eleAryInfo[1] + "^" + eleAryInfo[2] + "^" + eleAryInfo[3];
            var idValPair = eleAryInfo[4] + "#" + $('#txtBeastValue_TermWidget')[0].name;

            SendToBeast(instanceType + "#" + paraValues, idValPair);
        }

        $('#divTermWidget').hide();
        $('#divTermWidget').modal(false);
    }
    catch (err) {
        var strerrordesc = "Function:Save_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

function Close_TermWidget() {
    try {

        $('#divTermWidget').hide();
        $('#divTermWidget').modal(false);
    }
    catch (err) {
        var strerrordesc = "Function:Close_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

function FindOriginalTermFormat() {
    try {
        // var txtValue1 = $('#txtValue1_TermWidget').val();
        var txtValue1 = ClickTermFormatValue;

        if (txtValue1.indexOf('Y') > -1) {
            OriginalTermFormat = 3;
        }
        else if (txtValue1.indexOf('M') > -1) {
            OriginalTermFormat = 3;
        }
        else if (txtValue1.indexOf('W') > -1) {
            OriginalTermFormat = 1;
        }
        else if (txtValue1.indexOf('BD') > -1) {
            OriginalTermFormat = 0;
        }
        else if (txtValue1.indexOf('D') > -1) {
            OriginalTermFormat = 0;
        }
    }
    catch (err) {
        var strerrordesc = "Function:FindOriginalTermFormat(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}


function ShowFullValue_TermWidget() {
    try {
        //string[] Array
        var Years;
        ///string
        var Months = "0";
        ///int
        var value = 0;

        //var txtValue1 = $('#txtValue1_TermWidget').val().replace('(', '').replace(')', '');
        var txtValue1 = ClickTermFormatValue.replace('(', '').replace(')', '');

        if (txtValue1 != "") {

            FindOriginalTermFormat();
            if (OriginalTermFormat == 3) //Original Format is Y/M or M
            {

                //                        if (txtValue1.indexOf('Y') > -1 && txtValue1.indexOf('0M') > -1) {
                //                            $('#txtValue1_TermWidget').val(txtValue1.split('Y')[0] + "Y");
                //                        }
                //                        else {
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
                // }
            }
            else if (OriginalTermFormat == 1) //Original Format is Weeks
            {
                if (txtValue1.indexOf('W') > -1)
                    value = parseInt(txtValue1.substring(0, txtValue1.indexOf('W')));
                else
                    value = parseInt(txtValue1);
            }
            else if (OriginalTermFormat == 0 || OriginalTermFormat == 0) //Original Format is Days/BusinessDays
            {
                if (txtValue1.indexOf("BD") > -1)
                    value = parseInt(txtValue1.substring(0, txtValue1.indexOf("BD")));
                else if (txtValue1.indexOf('D') > -1)
                    value = parseInt(txtValue1.substring(0, txtValue1.indexOf('D')));
                else
                    value = parseInt(txtValue1);
            }
            if (DefaultTermFormat == TermFormat.Days) //Convert into Days
            {
                $('#txtValue1_TermWidget').val(GetDays(parseInt(OriginalTermFormat), value, TermFormat.Days).toString());
            }
            else if (DefaultTermFormat == TermFormat.BusDays) //Convert into Business Days
            {
                $('#txtValue1_TermWidget').val(GetDays(parseInt(OriginalTermFormat), value, TermFormat.BusDays).toString());
            }
            else if (DefaultTermFormat == TermFormat.Weeks) //Convert into Weeks
            {
                $('#txtValue1_TermWidget').val(GetWeeks(parseInt(OriginalTermFormat), value).toString());
            }
            else if (DefaultTermFormat == TermFormat.Year) //Convert into Years/Months
            {
                $('#txtValue1_TermWidget').val(GetYearsMonths(parseInt(OriginalTermFormat), value).toString());
            }
            else if (DefaultTermFormat == TermFormat.Months) //Convert into Months
            {
                $('#txtValue1_TermWidget').val(GetMonths(parseInt(OriginalTermFormat), value).toString());
            }
        }

        setTermWidget();

        if (globaltxtValue1.indexOf("(")) {
            $('#btn11t_td1tr11').removeClass('btn btn-info');
            $('#btn11t_td1tr11').addClass('btn');
        }
        else {
            if ($('#txtValue1_TermWidget').val() != "0Y" && $('#txtValue1_TermWidget').val() != "0Y0M" && $('#txtValue1_TermWidget').val() != "0W" && $('#txtValue1_TermWidget').val() != "0M" && $('#txtValue1_TermWidget').val() != "0D" && $('#txtValue1_TermWidget').val() != "0BD") {
                $('#txtValue1_TermWidget').val("(" + $('#txtValue1_TermWidget').val().replace('(', '').replace(')', '') + ")");
            }
            else {
                $('#txtValue1_TermWidget').val($('#txtValue1_TermWidget').val());
            }
            $('#btn11t_td1tr11').removeClass('btn');
            $('#btn11t_td1tr11').addClass('btn btn-info');

        }
    }
    catch (err) {
        var strerrordesc = "Function:ShowFullValue_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }

}

function setTermWidget() {
    try {

        var txtValue1 = $('#txtValue1_TermWidget').val();

        if (txtValue1.indexOf('Y') > -1) {

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td1tr' + j.toString()).val() == txtValue1.split('Y')[0] + " Year") {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td2tr' + j.toString()).val() == txtValue1.split('Y')[0] + " Year") {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 11; j++) {
                var tempString = txtValue1.split('Y')[0] + " Year";
                if ($('#btn' + j.toString() + 't_td3tr' + j.toString()).val() == tempString) {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn');
                }
            }

            if (txtValue1.split('Y').length > 0) {
                for (var j = 0; j < 12; j++) {
                    if ($('#btn' + j.toString() + 't_td4tr' + j.toString()).val() == txtValue1.split('Y')[1].replace('M', '') + " Month") {
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn');
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn btn-info');
                    }
                    else {
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn btn-info');
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn');
                    }
                }
            }

        }
        else if (txtValue1.indexOf('M') > -1) {

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td1tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td2tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn');
                }
            }


            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td3tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td4tr' + j.toString()).val() == txtValue1.split('M')[0] + " Month") {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn');
                }
            }

        }
        else if (txtValue1.indexOf('W') > -1) {
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td1tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td2tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td3tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td4tr' + j.toString()).val() == txtValue1.split('W')[0] + " Week") {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn');
                }
            }
        }
        else if (txtValue1.indexOf('BD') > -1) {
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td1tr' + j.toString()).val() == txtValue1.split('BD')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td2tr' + j.toString()).val() == txtValue1.split('BD')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td3tr' + j.toString()).val() == txtValue1.split('BD')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 12; j++) {
                if ($('#btn' + j.toString() + 't_td4tr' + j.toString()).val() == txtValue1.split('BD')[0] + " Bus Day") {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn');
                }
            }
        }
        else if (txtValue1.indexOf('D') > -1) {
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td1tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td2tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td2tr' + j.toString()).addClass('btn');
                }
            }
            
            for (var j = 0; j < 10; j++) {
                if ($('#btn' + j.toString() + 't_td3tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).addClass('btn');
                }
            }

            for (var j = 0; j < 12; j++) {
                if ($('#btn' + j.toString() + 't_td4tr' + j.toString()).val() == txtValue1.split('D')[0] + " Day") {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).addClass('btn');
                }
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:setTermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }

}


//        $(document).ready(function () {
//            try {

//                DefaultTermFormat = TermFormat.Year;
//                OriginalTermFormat = TermFormat.Year;
//                $('#txtValue1_TermWidget').val(ConvertToTerm($('#txtBeastValue_TermWidget')[0].name));
//                ClickTermFormatValue = $('#txtValue1_TermWidget').val();
//                ClickTermFormat = TermFormat.Year;

//                if ($('#txtValue1_TermWidget').val().indexOf('(') > -1)
//                    globaltxtValue1 = "(" + $('#txtValue1_TermWidget').val().replace('(', '').replace(')', '') + ")";
//                else
//                    globaltxtValue1 = $('#txtValue1_TermWidget').val();

//                Bind_Term_Widget(DefaultTermFormat);

//                //               $('#btnYearMth').focus();
//                //               $('#btnYearMth').click();

//                //alert(ConvertToTerm(1223));

//                //alert(ConvertToTermBeastValue(ConvertToTerm(-1223)));

//            }
//            catch (err) {
//                alert('$(document).ready');
//            }
//        });



function selected_term_format() {
    try {

        if (DefaultTermFormat == TermFormat.Year) {
            $('#btnYearMth').removeClass('btn');
            $('#btnYearMth').addClass('btn btn-info');
            $('#btnMonths').removeClass('btn btn-info');
            $('#btnMonths').addClass('btn');
            $('#btnWeeks').removeClass('btn btn-info');
            $('#btnWeeks').addClass('btn');
            $('#btnDays').removeClass('btn btn-info');
            $('#btnDays').addClass('btn');
            $('#btnBusDays').removeClass('btn btn-info');
            $('#btnBusDays').addClass('btn');
        }
        else if (DefaultTermFormat == TermFormat.Months) {
            $('#btnYearMth').removeClass('btn btn-info');
            $('#btnYearMth').addClass('btn');
            $('#btnMonths').removeClass('btn');
            $('#btnMonths').addClass('btn btn-info');
            $('#btnWeeks').removeClass('btn btn-info');
            $('#btnWeeks').addClass('btn');
            $('#btnDays').removeClass('btn btn-info');
            $('#btnDays').addClass('btn');
            $('#btnBusDays').removeClass('btn btn-info');
            $('#btnBusDays').addClass('btn');
        }
        else if (DefaultTermFormat == TermFormat.Weeks) {
            $('#btnYearMth').removeClass('btn btn-info');
            $('#btnYearMth').addClass('btn');
            $('#btnMonths').removeClass('btn btn-info');
            $('#btnMonths').addClass('btn');
            $('#btnWeeks').removeClass('btn');
            $('#btnWeeks').addClass('btn btn-info');
            $('#btnDays').removeClass('btn btn-info');
            $('#btnDays').addClass('btn');
            $('#btnBusDays').removeClass('btn btn-info');
            $('#btnBusDays').addClass('btn');
        }
        else if (DefaultTermFormat == TermFormat.Days) {
            $('#btnYearMth').removeClass('btn btn-info');
            $('#btnYearMth').addClass('btn');
            $('#btnMonths').removeClass('btn btn-info');
            $('#btnMonths').addClass('btn');
            $('#btnWeeks').removeClass('btn btn-info');
            $('#btnWeeks').addClass('btn');
            $('#btnDays').removeClass('btn');
            $('#btnDays').addClass('btn btn-info');
            $('#btnBusDays').removeClass('btn btn-info');
            $('#btnBusDays').addClass('btn');
        }
        else if (DefaultTermFormat == TermFormat.BusDays) {
            $('#btnYearMth').removeClass('btn btn-info');
            $('#btnYearMth').addClass('btn');
            $('#btnMonths').removeClass('btn btn-info');
            $('#btnMonths').addClass('btn');
            $('#btnWeeks').removeClass('btn btn-info');
            $('#btnWeeks').addClass('btn');
            $('#btnDays').removeClass('btn btn-info');
            $('#btnDays').addClass('btn');
            $('#btnBusDays').removeClass('btn');
            $('#btnBusDays').addClass('btn btn-info');
        }
    }
    catch (err) {
        var strerrordesc = "Function:selected_term_format(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

function display_term_button_string() {
    try {

        if (DefaultTermFormat == TermFormat.Year)
            return " Year";
        else if (DefaultTermFormat == TermFormat.Months)
            return " Month";
        else if (DefaultTermFormat == TermFormat.Weeks)
            return " Week";
        else if (DefaultTermFormat == TermFormat.Days)
            return " Day";
        else if (DefaultTermFormat == TermFormat.BusDays)
            return " Bus Day";
    }
    catch (err) {        
        var strerrordesc = "Function:display_term_button_string(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}

function Bind_Term_Widget(Param_SelectedTermFormat) {
    try {
        //alert(DefaultTermFormat);

        DefaultTermFormat = Param_SelectedTermFormat;

        selected_term_format();

        $("#table_widget_Term").html("");

        for (var i = 0; i < 12; i++) {
            var varHtmlTemplate = term_widget_Template_tablerow();
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TR]', "tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD1]', "t_td1tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD2]', "t_td2tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD3]', "t_td3tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD4]', "t_td4tr" + i.toString());

            $("#table_widget_Term").append(varHtmlTemplate);
        }

        FirstTD_TermWidget();

        SecondTD_TermWidget();

        ThirdTD_TermWidget();

        ForthTD_TermWidget();


        /////////////////////////////////////////////////
        var txtValue1 = $('#txtValue1_TermWidget').val();

        if (DefaultTermFormat == TermFormat.Year) {
            if (currentValueYearMth != "" && IsChange != 'TRUE') {
                $('#txtValue1_TermWidget').val(currentValueYearMth);
            }
            currentValueYearMth = $('#txtValue1_TermWidget').val();
            
        }
        else if (DefaultTermFormat == TermFormat.Months) {
            if (currentValueMonths != "" && IsChange != 'TRUE') {
                $('#txtValue1_TermWidget').val(currentValueMonths);
            }
            currentValueMonths = $('#txtValue1_TermWidget').val();
        }
        else if (DefaultTermFormat == TermFormat.Weeks) {
            if (currentValueWeeks != "" && IsChange != 'TRUE') {
                $('#txtValue1_TermWidget').val(currentValueWeeks);
            }
            currentValueWeeks = $('#txtValue1_TermWidget').val();
        }
        else if (DefaultTermFormat == TermFormat.BusDays) {
            if (currentValueDays != "" && IsChange != 'TRUE') {
                $('#txtValue1_TermWidget').val(currentValueDays.replace('D', 'BD'));
            }
            currentValueDays = $('#txtValue1_TermWidget').val();
        }
        else if (DefaultTermFormat == TermFormat.Days) {
            if (currentValueDays != "" && IsChange != 'TRUE') {
                $('#txtValue1_TermWidget').val(currentValueDays);
            }
            currentValueDays = $('#txtValue1_TermWidget').val();
        }
        
        if (IsChange == 'TRUE') {
            if (DefaultTermFormat == TermFormat.Year) {

                currentValueMonths = '';
                currentValueWeeks = '';
                //currentValueBuyDays = '';
                currentValueDays = '';

                ClickTermFormat = TermFormat.Year;
                ClickTermFormatValue = $('#txtValue1_TermWidget').val();
            }
            else if (DefaultTermFormat == TermFormat.Months) {
                currentValueYearMth = '';
                currentValueWeeks = '';
                //currentValueBuyDays = '';
                currentValueDays = '';

                ClickTermFormat = TermFormat.Months;
                ClickTermFormatValue = $('#txtValue1_TermWidget').val();
            }
            else if (DefaultTermFormat == TermFormat.Weeks) {
                currentValueYearMth = '';
                currentValueMonths = '';
                //currentValueBuyDays = '';
                currentValueDays = '';

                ClickTermFormat = TermFormat.Weeks;
                ClickTermFormatValue = $('#txtValue1_TermWidget').val();
            }
            else if (DefaultTermFormat == TermFormat.BusDays) {
                currentValueYearMth = '';
                currentValueMonths = '';
                currentValueWeeks = '';
                //currentValueDays = '';

                ClickTermFormat = TermFormat.BusDays;
                ClickTermFormatValue = $('#txtValue1_TermWidget').val();
            }
            else if (DefaultTermFormat == TermFormat.Days) {
                currentValueYearMth = '';
                currentValueMonths = '';
                currentValueWeeks = '';
                //currentValueBuyDays = '';

                ClickTermFormat = TermFormat.Days;
                ClickTermFormatValue = $('#txtValue1_TermWidget').val();
            }
            IsChange = '';
        }
        ////////////////////////////////////////////////////

        ShowFullValue_TermWidget();        
    }
    catch (err) {       
        var strerrordesc = "Function:Bind_Term_Widget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("termWidgetScript.js", strerrordesc);
    }
}


function FirstTD_TermWidget() {
    //START ******************************************************** First TD ********************************************************************
    //td1tr[]           First td(column) for All tr(row)
    //btn[]td1tr[]      button  First td(column) for All tr(row)
    {
        try {
            var td1btnCount = 0;

            var DisplaybtnString = display_term_button_string();

            for (var j = 0; j < 12; j++) {

                if (j == 10) {
                    continue;
                }

                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "t_td1tr" + j.toString());
                $('#t_td1tr' + j.toString()).html(varbuttonTemplate);

                if (j == 11) {
                    $('#btn11t_td1tr11').val("Negative");

                    var txtValue1 = $('#txtValue1_TermWidget').val();

                    if (txtValue1.indexOf("(")) {
                        $('#btn11t_td1tr11').removeClass('btn btn-info');
                        $('#btn11t_td1tr11').addClass('btn');
                    }
                    else {
                        $('#btn11t_td1tr11').removeClass('btn');
                        $('#btn11t_td1tr11').addClass('btn btn-info');
                    }

                    $("#btn11t_td1tr11").click(function () {

                        var txtValue1 = $('#txtValue1_TermWidget').val();
                        //alert("Negative");

                        if ($('#txtValue1_TermWidget').val() != "0Y" && $('#txtValue1_TermWidget').val() != "0Y0M" && $('#txtValue1_TermWidget').val() != "0W" && $('#txtValue1_TermWidget').val() != "0M" && $('#txtValue1_TermWidget').val() != "0D" && $('#txtValue1_TermWidget').val() != "0BD") {

                        }
                        else {
                            return;
                        }

                        IsChange = 'TRUE';

                        if (txtValue1.indexOf("(")) {
                            $('#txtValue1_TermWidget').val("(" + txtValue1 + ")");

                            globaltxtValue1 = "(" + txtValue1 + ")";

                            $('#btn11t_td1tr11').removeClass('btn');
                            $('#btn11t_td1tr11').addClass('btn btn-info');
                        }
                        else {
                            $('#txtValue1_TermWidget').val(txtValue1.replace("(", "").replace(")", ""));

                            globaltxtValue1 = txtValue1.replace("(", "").replace(")", "");

                            $('#btn11t_td1tr11').removeClass('btn btn-info');
                            $('#btn11t_td1tr11').addClass('btn');
                        }

                    });
                }
                else {
                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).val(td1btnCount.toString() + DisplaybtnString);

                    $('#btn' + j.toString() + 't_td1tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        if ($(this).val().indexOf('Y') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_TermWidget').val().split('Y')[1]).toString());
                        else if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('BD') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Bus Day', 'BD'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Day', 'D'));

                        Bind_Term_Widget(DefaultTermFormat);

                    });

                    td1btnCount++;
                }

            }
        }
        catch (err) {            
            var strerrordesc = "Function:FirstTD_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("termWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** First TD ********************************************************************
}


function SecondTD_TermWidget() {
    //START ****************************************************** Second TD ********************************************************************
    //td2tr[]           Second td(column) for All tr(row)
    //btn[]td2tr[]      button  Second td(column) for All tr(row)
    {
        try {
            var td2btnCount = 0;
            var DisplaybtnString = display_term_button_string();

            for (var j = 0; j < 12; j++) {

                if (j == 10 || j == 11) {
                    continue;
                }

                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "t_td2tr" + j.toString());
                $('#t_td2tr' + j.toString()).html(varbuttonTemplate);

                $('#btn' + j.toString() + 't_td2tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 't_td1tr' + j.toString()).val()) + 10).toString() + DisplaybtnString);

                $('#btn' + j.toString() + 't_td2tr' + j.toString()).click(function () {
                    //alert($(this).val());

                    IsChange = 'TRUE';

                    if ($(this).val().indexOf('Y') > -1)
                        $('#txtValue1_TermWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_TermWidget').val().split('Y')[1]).toString());
                    else if ($(this).val().indexOf('M') > -1)
                        $('#txtValue1_TermWidget').val($(this).val().replace(' Month', 'M'));
                    else if ($(this).val().indexOf('W') > -1)
                        $('#txtValue1_TermWidget').val($(this).val().replace(' Week', 'W'));
                    else if ($(this).val().indexOf('BD') > -1)
                        $('#txtValue1_TermWidget').val($(this).val().replace(' Bus Day', 'BD'));
                    else if ($(this).val().indexOf('D') > -1)
                        $('#txtValue1_TermWidget').val($(this).val().replace(' Day', 'D'));

                    Bind_Term_Widget(DefaultTermFormat);
                });

                td2btnCount++;
            }
        }
        catch (err) {            
            var strerrordesc = "Function:SecondTD_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("termWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Second TD ********************************************************************
}

function ThirdTD_TermWidget() {
    //START ****************************************************** Third TD ********************************************************************
    //td3tr[]           Third td(column) for All tr(row)
    //btn[]td3tr[]      button  Third td(column) for All tr(row)
    {
        try {

            var td3btnCount = 0;
            var DisplaybtnString = display_term_button_string();

            for (var j = 0; j < 12; j++) {

                if (j == 11 || (j == 10 && DefaultTermFormat != TermFormat.Year)) {
                    continue;
                }

                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "t_td3tr" + j.toString());
                $('#t_td3tr' + j.toString()).html(varbuttonTemplate);

                if (j != 10) {

                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 't_td2tr' + j.toString()).val()) + 10).toString() + DisplaybtnString);

                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        if ($(this).val().indexOf('Y') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_TermWidget').val().split('Y')[1]).toString());
                        else if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('BD') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Bus Day', 'BD'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Day', 'D'));
                            
                        Bind_Term_Widget(DefaultTermFormat);
                    });
                }
                else if (j == 10 && DefaultTermFormat == TermFormat.Year) {
                    ///TermFormat.Year
                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).val("30" + DisplaybtnString);

                    $('#btn' + j.toString() + 't_td3tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        if ($(this).val().indexOf('Y') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Year', 'Y') + ($('#txtValue1_TermWidget').val().split('Y')[1]).toString());
                        else if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('BD') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Bus Day', 'BD'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Day', 'D'));
                            
                        Bind_Term_Widget(DefaultTermFormat);
                    });
                }
                td3btnCount++;
            }
        }
        catch (err) {            
            var strerrordesc = "Function:ThirdTD_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("termWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Third TD ********************************************************************
}


function ForthTD_TermWidget() {
    //START ****************************************************** Forth TD ********************************************************************
    //td4tr[]           Forth td(column) for All tr(row)
    //btn[]td4tr[]      button  Forth td(column) for All tr(row)
    {
        try {

            var td4btnCount = 0;
            var DisplaybtnString = display_term_button_string();

            for (var j = 0; j < 12; j++) {

                /// TermFormat.Months
                if ((j == 10 || j == 11) && (DefaultTermFormat == TermFormat.Months || DefaultTermFormat == TermFormat.Weeks))
                    continue;
                    
                var varbuttonTemplate = term_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "t_td4tr" + j.toString());
                $('#t_td4tr' + j.toString()).html(varbuttonTemplate);

                if (DefaultTermFormat == TermFormat.Year) {
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).val(td4btnCount.toString() + " Month");
                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_TermWidget').val(($('#txtValue1_TermWidget').val().split('Y')[0] + 'Y' + $(this).val().replace(' Month', 'M')).toString());
                            
                        Bind_Term_Widget(DefaultTermFormat);
                    });
                }
                else if (DefaultTermFormat == TermFormat.Months || DefaultTermFormat == TermFormat.Weeks || DefaultTermFormat == TermFormat.Days || DefaultTermFormat == TermFormat.BusDays) {

                    if ((j == 7 || j == 8 || j == 9) && (DefaultTermFormat == TermFormat.Months)) {
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).val((parseInt($('#btn' + (parseInt(j) - 1).toString() + 't_td4tr' + (parseInt(j) - 1).toString()).val()) + 12).toString().toString() + " Month");
                    }
                    else if ((j == 7 || j == 8 || j == 9) && (DefaultTermFormat == TermFormat.Weeks)) {
                        if (j == 7)
                            $('#btn' + j.toString() + 't_td4tr' + j.toString()).val("40" + " Week");
                        else if (j == 8)
                            $('#btn' + j.toString() + 't_td4tr' + j.toString()).val("50" + " Week");
                        else if (j == 9)
                            $('#btn' + j.toString() + 't_td4tr' + j.toString()).val("52" + " Week");
                    }
                    else if (DefaultTermFormat == TermFormat.Months) {
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 't_td3tr' + j.toString()).val()) + 10).toString().toString() + " Month");
                    }
                    else if (DefaultTermFormat == TermFormat.Weeks) {
                        $('#btn' + j.toString() + 't_td4tr' + j.toString()).val((parseInt($('#btn' + j.toString() + 't_td3tr' + j.toString()).val()) + 10).toString().toString() + " Week");
                    }
                    else if (DefaultTermFormat == TermFormat.Days || DefaultTermFormat == TermFormat.BusDays) {

                        var TypeFormat;
                        if (DefaultTermFormat == TermFormat.Days)
                            TypeFormat = " Day";
                        else if (DefaultTermFormat == TermFormat.BusDays)
                            TypeFormat = " Bus Day";

                        if (j == 0) {
                            $('#btn' + j.toString() + 't_td4tr' + j.toString()).val("30" + TypeFormat);
                        }
                        else {
                            $('#btn' + j.toString() + 't_td4tr' + j.toString()).val((parseInt($('#btn' + (parseInt(j) - 1).toString() + 't_td4tr' + (parseInt(j) - 1).toString()).val()) + 30).toString().toString() + TypeFormat);
                        }
                    }

                    $('#btn' + j.toString() + 't_td4tr' + j.toString()).click(function () {
                        // alert($(this).val());

                        IsChange = 'TRUE';

                        if ($(this).val().indexOf('M') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Month', 'M'));
                        else if ($(this).val().indexOf('W') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Week', 'W'));
                        else if ($(this).val().indexOf('BD') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Bus Day', 'BD'));
                        else if ($(this).val().indexOf('D') > -1)
                            $('#txtValue1_TermWidget').val($(this).val().replace(' Day', 'D'));
                            
                        Bind_Term_Widget(DefaultTermFormat);
                    });
                }
                td4btnCount++;
            }

        }
        catch (err) {            
            var strerrordesc = "Function:ForthTD_TermWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("termWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Forth TD ********************************************************************
}
