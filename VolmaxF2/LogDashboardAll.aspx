<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogDashboardAll.aspx.cs"
    Inherits="LogDashboardAll" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0)" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>The Beast Apps, LLC</title>
    <link rel="shortcut icon" type="image/icon" href="images/favicon.ico" />
    <link href="css/webApps.css" rel="stylesheet" type="text/css" />

    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />

    <link href="css/demo_table.css" rel="stylesheet" />
    <link href="css/demo_page.css" rel="stylesheet" />

    <link href="Theme/css/start/jquery-ui-1.10.4.custom.css" rel="stylesheet" />
    <script src="Javascript/jquery-1.8.3.min.js"></script>
    <script src="Theme/js/jquery-ui-1.10.4.custom.js"></script>

    <script src="Javascript/jquery.dataTables.js"></script>

    <script src="AuditTailjs/jsdate/js/datepicker.js"></script>

    <script src="AuditTailjs/jquery-ui-timepicker-addon.js" type="text/javascript"></script>
    <%--<link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript">
        var sessionAry4Graph = [];
        var sessionAry4GraphSeries = new Array();
        var sessionAry4GraphCtr = 0;

        var traderAry4Graph = [];
        var traderAry4GraphSeries = new Array();
        var traderAry4GraphCtr = 1;
        var lst24 = "";
        var lst7 = "";
        var lstMonth = "";

        function rtnRandomValFromRange() {
            var min = 1;
            var max = 200;

            return Math.floor(Math.random() * (max - min + 1)) + min;
        }

        $(document).ready(function () {
            //$('#tblSharedlogs').hide();
            //$('#tblActiveUser').hide();

            $('#from_date_sys').datetimepicker({
                showSecond: true,
                showMillisec: true,
                dateFormat: 'yy-mm-dd',
                timeFormat: 'hh:mm:ss.l',
                onClose: function (dateText, inst) {
                    var endDateTextBox = $('#to_date_sys');
                    if (endDateTextBox.val() != '') {
                        var testStartDate = new Date(dateText);
                        var testEndDate = new Date(endDateTextBox.val());
                        if (testStartDate > testEndDate)
                            endDateTextBox.val(dateText);
                    }
                    else {
                        endDateTextBox.val(dateText);
                    }

                },
                onSelect: function (selectedDateTime) {
                    var start = $(this).datetimepicker('getDate');
                    $('#to_date_sys').datetimepicker('option', 'minDate', new Date(start.getTime()));
                }
            });

            var d = new Date();
            d.setDate(d.getDate() - 7);

            $("#from_date_sys").datepicker("setDate", d);
            $('#to_date_sys').datetimepicker({
                showSecond: true,
                showMillisec: true,
                dateFormat: 'yy-mm-dd',
                timeFormat: 'hh:mm:ss.l',
                onClose: function (dateText, inst) {
                    var startDateTextBox = $('#from_date_sys');
                    if (startDateTextBox.val() != '') {
                        var testStartDate = new Date(startDateTextBox.val());
                        var testEndDate = new Date(dateText);
                        if (testStartDate > testEndDate)
                            startDateTextBox.val(dateText);
                    }
                    else {
                        startDateTextBox.val(dateText);
                    }

                },
                onSelect: function (selectedDateTime) {
                    var end = $(this).datetimepicker('getDate');
                    $('#from_date_sys').datetimepicker('option', 'maxDate', new Date(end.getTime()));
                }
            });

            $("#to_date_sys").datepicker("setDate", new Date());


            GetActiveUserSysLogsForUserFromDateToDate($('#from_date_sys').val(), $('#to_date_sys').val(), true);
            $('#from_date_w3c').datetimepicker({
                showSecond: true,
                showMillisec: true,
                dateFormat: 'yy-mm-dd',
                timeFormat: 'hh:mm:ss.l',
                onClose: function (dateText, inst) {
                    var endDateTextBox = $('#to_date_w3c');
                    if (endDateTextBox.val() != '') {
                        var testStartDate = new Date(dateText);
                        var testEndDate = new Date(endDateTextBox.val());
                        if (testStartDate > testEndDate)
                            endDateTextBox.val(dateText);
                    }
                    else {
                        endDateTextBox.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    var start = $(this).datetimepicker('getDate');
                    $('#to_date_w3c').datetimepicker('option', 'minDate', new Date(start.getTime()));
                }
            });

            $('#to_date_w3c').datetimepicker({
                showSecond: true,
                showMillisec: true,
                dateFormat: 'yy-mm-dd',
                timeFormat: 'hh:mm:ss.l',
                onClose: function (dateText, inst) {
                    var startDateTextBox = $('#from_date_w3c');
                    if (startDateTextBox.val() != '') {
                        var testStartDate = new Date(startDateTextBox.val());
                        var testEndDate = new Date(dateText);
                        if (testStartDate > testEndDate)
                            startDateTextBox.val(dateText);
                    }
                    else {
                        startDateTextBox.val(dateText);
                    }
                },
                onSelect: function (selectedDateTime) {
                    var end = $(this).datetimepicker('getDate');
                    $('#from_date_w3c').datetimepicker('option', 'maxDate', new Date(end.getTime()));
                }
            });

        });

        function filter(selector, query) {
            query = $.trim(query);
            query = query.replace(/ /gi, '|');

            $(selector).each(function () {

                if ($(this).hasClass('alwaysVisible') == false)
                    ($(this).text().search(new RegExp(query, "i")) < 0) ? $(this).hide().removeClass('visible') : $(this).show().addClass('visible');
            });
        }

        function filterBy(query) {
            query = $.trim(query);
            query = query.replace(/ /gi, '|');

            $('#filterInput').val("");

            if (query == "1secrow") {
                $('#filterInput').val("> 1 Sec");
                $('#a1secrow').css("background-color", "#bfbfbf");
                $('#aTrader').css("background-color", "#ffffff");
                $('#aBroker').css("background-color", "#ffffff");
                $('#aAll').css("background-color", "#ffffff");
            }
            else {
                $('#filterInput').val(query);
                if (query == "Trader") {
                    $('#a1secrow').css("background-color", "#ffffff");
                    $('#aTrader').css("background-color", "#bfbfbf");
                    $('#aBroker').css("background-color", "#ffffff");
                    $('#aAll').css("background-color", "#ffffff");
                }
                else if (query == "Broker") {
                    $('#a1secrow').css("background-color", "#ffffff");
                    $('#aTrader').css("background-color", "#ffffff");
                    $('#aBroker').css("background-color", "#bfbfbf");
                    $('#aAll').css("background-color", "#ffffff");
                }
                else if (query == "All") {
                    $('#a1secrow').css("background-color", "#ffffff");
                    $('#aTrader').css("background-color", "#ffffff");
                    $('#aBroker').css("background-color", "#ffffff");
                    $('#aAll').css("background-color", "#bfbfbf");
                }

            }

            $('.sessionTbl tr').each(function () {
                if ($(this).hasClass('alwaysVisible') == false) {
                    if ($(this).hasClass(query) == false) {
                        $(this).hide().removeClass('visible');
                    }
                    else {
                        $(this).show().addClass('visible');
                    }
                }
            });
        }

        function filterOnKeyUp(event, id) {

            if (event.keyCode == 27 || $('#' + id + '').val() == '') {
                $('#' + id + '').val('');
                $('.sessionTbl tr').removeClass('visible').show().addClass('visible');
            }
            else {
                filter('.sessionTbl tr', $('#' + id + '').val());
            }
        }

        function populateSessionDetails() {
            var SessionDetails = $("#cmbSessionList").val();
            var SessionID = SessionDetails.toString().split('^')[0];

            document.getElementById('lblSessionCode').innerText = SessionDetails.toString().split('^')[1];

            var SessionText = $("#cmbSessionList option:selected").text();

            document.getElementById('lblSessionDate').innerText = SessionText.toString().split('-')[0];
            document.getElementById('lblSessionName').innerText = SessionText.toString().split('-')[1];

            //GetSessionDetailsBySessionID(SessionID);
            var defValues = document.getElementById("hdnValueFromDefault").value;

            var vals = defValues.split('#');
            // alert(defValues);
            GetSysLogsForUserBySession(vals[0], vals[1], true);
            GetW3CLogsForUserBySession(vals[0], vals[1], true);

        }

        function setFromDateToDate(fromDate, toDate, IsUser) {
            if (IsUser == true) {
                document.getElementById('fromDateDispUser').innerText = fromDate;
                document.getElementById('toDateDispUser').innerText = toDate;
            }
            else {
                document.getElementById('fromDateDisp').innerText = fromDate;
                document.getElementById('toDateDisp').innerText = toDate;
            }
        }

        function populateSession(FromDateStr, ToDateStr) {

            var rndmNumber = rtnRandomValFromRange();

            $.ajax({
                url: "Service.asmx/getSessionFromDateToDate?FromDate=" + FromDateStr + "&ToDate=" + ToDateStr + "&rndmNumber=" + rndmNumber + "",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (msg) {
                    var tmpJsonData = msg.d;
                    var property_Tmp = document.getElementById("cmbSessionList");
                    property_Tmp.options.length = 0;
                    $.each(tmpJsonData, function () {
                        var sessionDetail = this['sessionDetail'];
                        var SessionDate = this['SessionDate'];
                        var SessionId = this['SessionId'];
                        var LocationCode = this['LocationCode'];
                        var SessionName = this['SessionName'];

                        var cmbValue = SessionId + "^" + LocationCode;

                        var opt = document.createElement("option");

                        $("<option value=" + cmbValue + ">" + this['sessionDetail'] + "</option>").appendTo("#cmbSessionList");
                    });

                    populateSessionDetails();
                },
                error: function (request, status, error) {
                    var property_Tmp = document.getElementById("cmbSessionList");
                    property_Tmp.options.length = 0;
                    $('#sessionTbl').empty();
                    $('#sessionSummaryTbl').empty();
                    $("#placeholderVCM").empty();
                    $("#placehoderSession").empty();


                    document.getElementById('lblSessionCode').innerText = "";
                    document.getElementById('lblSessionDate').innerText = "";
                    document.getElementById('lblSessionName').innerText = "";
                    document.getElementById("totAverage").innerText = "";

                    alert("Session Activity Not Found For Given Date/Dates (Default 24 Hrs)");

                }
            });
        }

        function pad(number) {
            var length = 3;

            var str = '' + number;
            while (str.length < length) {
                str = str + '0';
            }
            return str;
        }

        function populateTimeBaseAverageForUser(UserID, timeRange, RowID) {

            var FromDate = new Date();

            var ToDate = new Date();

            if (timeRange == 'time24') {
                FromDate.setDate(ToDate.getDate() - 1);
            }
            else if (timeRange == 'timeLastWeek') {
                FromDate.setDate(ToDate.getDate() - 7);
            }
            else if (timeRange == 'timeLastMonth') {
                FromDate.setDate(ToDate.getDate() - 30);
            }

            var ToDateStr = (ToDate.getMonth() + 1) + "/" + ToDate.getDate() + "/" + ToDate.getFullYear();
            var FromDateStr = (FromDate.getMonth() + 1) + "/" + FromDate.getDate() + "/" + FromDate.getFullYear();

            var rndmNumber = rtnRandomValFromRange();

            $.ajax({
                url: "Service.asmx/getResponseAverageForUserFromDateToDate?FromDate=" + FromDateStr + "&ToDate=" + ToDateStr + "&UserID=" + UserID + "&rndmNumber=" + rndmNumber + "",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (msg) {

                    var tmpJsonData = msg.d;

                    $.each(tmpJsonData, function () {
                        var SessionAverageResponse = this['SessionAverageResponse'];

                        var averageTmp = Math.round(SessionAverageResponse * 1000) / 1000;
                        var averageStrTmp = "";

                        try {
                            if (parseInt(averageTmp.toString().split('.')[0]) > 0) {
                                averageStrTmp = averageTmp.toString().split('.')[0] + " sec " + pad(averageTmp.toString().split('.')[1]) + " msec";
                            }
                            else {
                                if (averageTmp.toString().split('.').length > 1)
                                    averageStrTmp = pad(averageTmp.toString().split('.')[1]) + " msec";
                                else
                                    averageStrTmp = "0 msec";
                            }
                        }
                        catch (e) {
                            averageStrTmp = "0 msec";
                        }

                        if (timeRange == "timeLastWeek") {
                            $('#' + RowID + ' > td.week').html(averageStrTmp);
                        }
                        else if (timeRange == 'timeLastMonth') {
                            $('#' + RowID + ' > td.month').html(averageStrTmp);
                        }
                        else if (timeRange == 'time24') {
                            $('#' + RowID + ' > td.24hrs').html(averageStrTmp);
                        }

                    });
                },
                error: function (request, status, error) {

                }
            });
        }

        function populateSessionAverageForUser(FromDateStr, ToDateStr, UserID) {


            $('#tblUserWiseSummary').empty();
            addSessionSummaryRowUser();

            var rndmNumber = rtnRandomValFromRange();

            $.ajax({
                url: "Service.asmx/getSessionAverageForUserFromDateToDate?FromDate=" + FromDateStr + "&ToDate=" + ToDateStr + "&UserID=" + UserID + "&rndmNumber=" + rndmNumber + "",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (msg) {

                    var tmpJsonData = msg.d;
                    var summaryCtr = 1;

                    $.each(tmpJsonData, function () {
                        var SessionID = this['SessionID'];
                        var SessionAverageResponse = this['SessionAverageResponse'];
                        var SessionDetail = this['SessionDetail'];

                        var backColor;
                        var Performance;

                        var totAverage = document.getElementById("hdnTotalSessionAvg").value;

                        if (parseFloat(SessionAverageResponse) > parseFloat(totAverage)) {
                            backColor = "background-color:#ff545b";
                            Performance = "Worse Than Average";
                        }
                        else {
                            backColor = "background-color:#33e958";
                            Performance = "Better Than Average";
                        }

                        var trClassSmry = "";

                        if (summaryCtr == 0)
                            trClassSmry = "";

                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr5";
                        else
                            trClassSmry = "tr6";

                        var averageTmp = Math.round(SessionAverageResponse * 1000) / 1000;
                        var averageStrTmp = "";

                        try {
                            if (parseInt(averageTmp.toString().split('.')[0]) > 0) {
                                averageStrTmp = averageTmp.toString().split('.')[0] + " sec " + pad(averageTmp.toString().split('.')[1]) + " msec";
                            }
                            else {
                                if (averageTmp.toString().split('.').length > 1)
                                    averageStrTmp = pad(averageTmp.toString().split('.')[1]) + " msec";
                                else
                                    averageStrTmp = "0 msec";
                            }
                        }
                        catch (e) {
                            averageStrTmp = "0 msec";
                        }


                        $('<tr class="' + trClassSmry + '"><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + SessionDetail + '</td><td align="right" valign="middle" style="padding-right:7px">' + averageStrTmp + '</td><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; ; ' + backColor + ';">' + Performance + '</td></tr>').insertBefore('table.tblUserWiseSummary tr.sessionSummaryRowUser');

                        summaryCtr++;

                    });
                },
                error: function (request, status, error) {
                    $("#tblUserWiseSummary").empty();
                    alert("Session Activity Not Found For this USER For Given Date/Dates (Default Last 1 Month)");
                }
            });
        }

        function showTooltip(x, y, contents) {
            $('<div id="tooltip">' + contents + '</div>').css({
                position: 'absolute',
                display: 'none',
                top: y + 5,
                left: x + 5,
                border: '1px solid #fdd',
                padding: '2px',
                'background-color': '#fee',
                opacity: 0.80
            }).appendTo("body").fadeIn(200);
        }

        function populateSessionAverage(FromDateStr, ToDateStr, summaryCtr, currentSessionTime, totAverage) {
            var SessionAverageResponse = 0;
            var rndmNumber = rtnRandomValFromRange();

            var IsINDReq = document.getElementById('IsINDReq').value;

            $.ajax({
                url: "Service.asmx/getSessionAverageFromDateToDate?FromDate=" + FromDateStr + "&ToDate=" + ToDateStr + "&IsINDReq=" + IsINDReq + "&rndmNumber=" + rndmNumber + "",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (msg) {

                    var tmpJsonData = msg.d;

                    $.each(tmpJsonData, function () {
                        SessionAverageResponse = this['SessionAverageResponse'];

                        var backColor;
                        var Performance;

                        if (parseFloat(SessionAverageResponse) > parseFloat(totAverage)) {
                            backColor = "background-color:#ff545b";
                            Performance = "Worse Than Average";
                        }
                        else {
                            backColor = "background-color:#33e958";
                            Performance = "Better Than Average";
                        }

                        var trClassSmry = "";

                        if (summaryCtr == 0)
                            trClassSmry = "";

                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr4";
                        else
                            trClassSmry = "tr3";

                        var averageTmp = Math.round(SessionAverageResponse * 1000) / 1000;
                        var averageStrTmp = "";

                        try {
                            if (parseInt(averageTmp.toString().split('.')[0]) > 0) {
                                averageStrTmp = averageTmp.toString().split('.')[0] + " sec " + pad(averageTmp.toString().split('.')[1]) + " msec";
                            }
                            else {
                                if (averageTmp.toString().split('.').length > 1)
                                    averageStrTmp = pad(averageTmp.toString().split('.')[1]) + " msec";
                                else
                                    averageStrTmp = "0 msec";
                            }
                        }
                        catch (e) {
                            averageStrTmp = "0 msec";
                        }

                        //

                        if (currentSessionTime == "24 Hours") {

                            lst24 = backColor;

                            $('.24hrsAvg').html(averageStrTmp);
                            var currentAvg = document.getElementById("totAverage").innerText
                            $('.currentAvg').html(currentAvg);

                        }
                        else if (currentSessionTime == "Last Week") {
                            lst7 = backColor;
                            $('.weekAvg').html(averageStrTmp);
                        }
                        else if (currentSessionTime == "Last Month") {
                            lstMonth = backColor;
                            $('.monthAvg').html(averageStrTmp);
                        }

                        $('.performanceAvg').html('<table style="text-align:center; padding-left:0px; padding-right:0px;" width="100%"><tr><td style="' + lst24 + '" widht="33%">24 hr</td><td  style="' + lst7 + '" widht="34%">7 day</td><td  style="' + lstMonth + '" widht="33%">30 day</td></tr></table>');


                    });


                    sessionAry4Graph.push([0, SessionAverageResponse]);
                    sessionAry4GraphSeries[sessionAry4GraphCtr] = currentSessionTime;

                    if (sessionAry4GraphCtr >= 4) {

                        var d1 = [sessionAry4Graph[0], [traderAry4GraphCtr, sessionAry4Graph[0][1]]];
                        s1 = {
                            data: d1,
                            lines: { show: true },
                            label: sessionAry4GraphSeries[0],
                            color: 1
                        };

                        var d2 = [sessionAry4Graph[1], [traderAry4GraphCtr, sessionAry4Graph[1][1]]];
                        s2 = {
                            data: d2,
                            lines: { show: true },
                            label: sessionAry4GraphSeries[1],
                            color: 2
                        };

                        var d3 = [sessionAry4Graph[2], [traderAry4GraphCtr, sessionAry4Graph[2][1]]];
                        s3 = {
                            data: d3,
                            lines: { show: true },
                            label: sessionAry4GraphSeries[2],
                            color: 3
                        };

                        var d4 = [sessionAry4Graph[3], [traderAry4GraphCtr, sessionAry4Graph[3][1]]];
                        s4 = {
                            data: d4,
                            lines: { show: true },
                            label: sessionAry4GraphSeries[3],
                            color: 4
                        };

                        var d5 = [sessionAry4Graph[4], [traderAry4GraphCtr, sessionAry4Graph[4][1]]];
                        s5 = {
                            data: d5,
                            lines: { show: true },
                            label: sessionAry4GraphSeries[4],
                            color: 5
                        };

                        var d6 = new Array();

                        for (aryGraphCtr = 0; aryGraphCtr < traderAry4GraphCtr; aryGraphCtr++) {
                            d6.push(traderAry4Graph[aryGraphCtr]);
                        }


                        s6 = {
                            data: d6,
                            lines: { show: true },
                            points: { show: true }
                        };


                        var iCtr = 1;
                        var xaxBankShow = new Array(traderAry4GraphCtr);
                        var xaxBankShowNew = [];

                        for (iCtr = 0; iCtr < traderAry4GraphCtr; iCtr++) {
                            xaxBankShow[iCtr] = new Array(2);
                            if (iCtr == 0) {
                                //                                xaxBankShow.push[iCtr, "-"];
                                xaxBankShow[iCtr][0] = iCtr;
                                xaxBankShow[iCtr][1] = "";
                            }
                            else {
                                xaxBankShow[iCtr][0] = iCtr;
                                var tmpStr = traderAry4GraphSeries[iCtr].toString().split('#')[1];

                                tmpStr = $.trim(tmpStr);

                                if (tmpStr == "-")
                                    tmpStr = "Broker";
                                xaxBankShow[iCtr][1] = tmpStr;
                                //xaxBankShow += ",[" + iCtr + ", \"" + traderAry4GraphSeries[iCtr].toString().split('#')[1] + "\"]";
                            }
                            //xaxBankShowNew.push(iCtr, xaxBankShow[iCtr]);
                        }

                        xaxBankShow[iCtr] = new Array(2);
                        xaxBankShow[iCtr][0] = iCtr;
                        xaxBankShow[iCtr][1] = "";

                        //xaxBankShow += "]";

                        xax = { min: 0, max: traderAry4GraphCtr, tickFormatter: function (val, axis) { return parseInt(val); }, ticks: xaxBankShow };

                        //xax = { min: 0, max: traderAry4GraphCtr, tickFormatter: function (val, axis) { return parseInt(val); }, ticks: [[0,''], [1,  traderAry4GraphSeries[1]], [2,'']]};

                        //                        xax = { min: 0, max: traderAry4GraphCtr, ticks: traderAry4GraphCtr };

                        options = {
                            xaxis: xax, grid: { hoverable: true, clickable: true }

                        };

                        $.plot($("#placehoderSession"), [s1, s2, s3, s4, s5, s6], options);

                        var previousPoint = null;
                        $("#placehoderSession").bind("plothover", function (event, pos, item) {
                            //                            $("#x").text(pos.x.toFixed(2));
                            //                            $("#y").text(pos.y.toFixed(2));


                            if (item) {
                                if (previousPoint != item.datapoint) {
                                    previousPoint = item.datapoint;

                                    $("#tooltip").remove();
                                    var x = item.datapoint[0].toFixed(2),
                                        y = item.datapoint[1].toFixed(2);

                                    var tooltipContents = traderAry4GraphSeries[item.dataIndex + 1].toString().split('#')[0];
                                    tooltipContents = $.trim(tooltipContents);

                                    showTooltip(item.pageX, item.pageY, tooltipContents);
                                }
                            }
                            else {
                                $("#tooltip").remove();
                                previousPoint = null;
                            }
                        });

                        $("#placehoderSession").bind("plotclick", function (event, pos, item) {
                            if (item) {
                                //alert("You clicked point " + item.dataIndex + " in " + item.series.label + ".");
                                //plot.highlight(item.series, item.datapoint);

                                var rowID = parseInt(item.dataIndex) + 2;
                                var rowIDstr = rowID.toString() + "Row";

                                $("#" + rowIDstr + " td").click();

                                var tmpStrUser = traderAry4GraphSeries[item.dataIndex + 1].toString().split('#')[0];
                                tmpStrUser = $.trim(tmpStrUser);

                                filter('.sessionTbl tr', tmpStrUser);
                                $('#filterInput').val(tmpStrUser);

                                $('#a1secrow').css("background-color", "#ffffff");
                                $('#aTrader').css("background-color", "#ffffff");
                                $('#aBroker').css("background-color", "#ffffff");
                                $('#aAll').css("background-color", "#ffffff");
                            }
                        });
                        //$.plot($("#placehoderSession"), [s6], options);
                    }




                    //$.plot($("#placeholderVCM"), [{ label: "Response Time (All Users)", data: arrayTwo, bars: { show: true}}]);
                    //$.plot($("#placehoderSession"), [{ data: sessionAry4Graph, bars: { show: true}}]);
                    sessionAry4GraphCtr++;
                },
                error: function (request, status, error) {

                }
            });
            return SessionAverageResponse;
        }

        function getSessionAverageFromDateToDate(frmDate, toDate, summaryCtr, currentSessionTime, totAverage) {


            var FromDate = frmDate;
            var ToDate = toDate;

            var FromDateStr = (FromDate.getMonth() + 1) + "/" + FromDate.getDate() + "/" + FromDate.getFullYear();
            var ToDateStr = (ToDate.getMonth() + 1) + "/" + ToDate.getDate() + "/" + ToDate.getFullYear();
            var sessionAverage = 0;

            populateSessionAverage(FromDateStr, ToDateStr, summaryCtr, currentSessionTime, totAverage);
        }

        function setBackColorForTimeToolbar(timeRange, isUser) {

            var vartime24 = "time24";
            var vartimeLastWeek = "timeLastWeek";
            var vartimeLastMonth = "timeLastMonth";
            var vartimeDateRange = "timeDateRange";


            if (isUser == true) {
                vartime24 = vartime24 + "User";
                vartimeLastWeek = vartimeLastWeek + "User";
                vartimeLastMonth = vartimeLastMonth + "User";
                vartimeDateRange = vartimeDateRange + "User";
            }

            if (timeRange == "time24") {

                $('#' + vartime24 + ' .time').css("color", "Red");
                $('#' + vartimeLastWeek + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeLastMonth + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeDateRange + ' .time').css("color", "#3D3D3D");
            }
            else if (timeRange == "timeLastWeek") {
                $('#' + vartime24 + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeLastWeek + ' .time').css("color", "Red");
                $('#' + vartimeLastMonth + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeDateRange + ' .time').css("color", "#3D3D3D");
            }
            else if (timeRange == "timeLastMonth") {
                $('#' + vartime24 + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeLastWeek + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeLastMonth + ' .time').css("color", "Red");
                $('#' + vartimeDateRange + ' .time').css("color", "#3D3D3D");
            }
            else if (timeRange == "timeDateRange") {
                $('#' + vartime24 + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeLastWeek + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeLastMonth + ' .time').css("color", "#3D3D3D");
                $('#' + vartimeDateRange + ' .time').css("color", "Red");
            }


        }

        function ResetSessionActivityDetails(timeRange, isUser) {

            setBackColorForTimeToolbar(timeRange, isUser);

            var FromDate = new Date();

            var ToDate = new Date();

            if (timeRange == 'time24') {
                FromDate.setDate(ToDate.getDate() - 1);
            }
            else if (timeRange == 'timeLastWeek') {
                FromDate.setDate(ToDate.getDate() - 7);
            }
            else if (timeRange == 'timeLastMonth') {
                FromDate.setDate(ToDate.getDate() - 30);
            }
            else if (timeRange == 'timeDateRange') {
                FromDate.setDate(ToDate.getDate() - 60);
            }

            var ToDateStr = (ToDate.getMonth() + 1) + "/" + ToDate.getDate() + "/" + ToDate.getFullYear();
            var FromDateStr = (FromDate.getMonth() + 1) + "/" + FromDate.getDate() + "/" + FromDate.getFullYear();



            if (isUser == false) {

                document.getElementById("hdnFromDate").value = FromDate;
                document.getElementById("hdnToDate").value = ToDate;

                populateSession(FromDateStr, ToDateStr);
                setFromDateToDate(FromDateStr, ToDateStr, isUser);
            }
            else {
                var usrID = document.getElementById("hdnCurrentSelectedUser").value;

                populateSessionAverageForUser(FromDateStr, ToDateStr, usrID);
                setFromDateToDate(FromDateStr, ToDateStr, isUser);
            }
        }

        function getCurrentFromDate() {
            var FromTmp = new Date(document.getElementById("hdnFromDate").value);

            var FromDateStrTmp = (FromTmp.getDate() + "/" + FromTmp.getMonth() + 1) + "/" + FromTmp.getFullYear();

            return FromDateStrTmp;
        }

        function getCurrentToDate() {
            var ToTmp = new Date(document.getElementById("hdnToDate").value);

            var ToDateStrTmp = ToTmp.getDate() + "/" + (ToTmp.getMonth() + 1) + "/" + ToTmp.getFullYear();

            return ToDateStrTmp;
        }

        Array.prototype.avg = function () {
            var av = 0;
            var cnt = 0;
            var len = this.length;
            for (var i = 0; i < len; i++) {
                var e = +this[i];
                //                if (!e && this[i] !== 0 && this[i] !== '0') e--;
                if (this[i] == e) { av += e; cnt++; }
            }
            return av / cnt;
        }

        function closeDivSpan() {

            $('#divUserDetails').hide('slow');
            $('#divUserDetails').unwrap();
            document.getElementById("hdnCurrentSelectedUser").value = "";

        }


        function getUserSessions(RowID, UserID) {

            if (document.getElementById("hdnCurrentSelectedUser").value != UserID) {

                document.getElementById("hdnCurrentSelectedUser").value = UserID;

                $('#divUserDetails').hide('slow');
                $('#divUserDetails').unwrap();

                if ($("#divUserDetails").length > 0) {

                }
                else {
                    var div = $("<div>").html('<div id="divUserDetails" style="display: none; width: 100%;"><table border="0px" width="860" align="center" cellpadding="0" cellspacing="0" class="table4"><tr><td class="sessionbg" height="80" align="center" valign="middle"><table width="750" border="0" align="center" cellpadding="0" cellspacing="0"><tr><td align="center" valign="middle" class="bluesession">Session Transactions</td><td width="50%" height="50" rowspan="2" align="right" valign="middle"><ul style="list-style-type: none;"><li class="time" id="time24User"><span  class="time"  style="cursor:pointer;" onclick="ResetSessionActivityDetails(\'time24\', true);"> <b> 24 hours |  </b></span></li><li class="time"  id="timeLastWeekUser"><span  class="time" style="cursor:pointer;" onclick="ResetSessionActivityDetails(\'timeLastWeek\', true);"><b> Last Week |  </b></span></li><li  class="time" id="timeLastMonthUser"><span  class="time" style="cursor:pointer;" onclick="ResetSessionActivityDetails(\'timeLastMonth\', true);"><b> Last Month |  </b></span></li><li class="time" id="timeDateRangeUser"><a href="#" style="cursor:pointer;" id="dateUser" class="date-pick-user time" > Date Range  </a></li></ul></td></tr><tr><td height="30" align="center" valign="middle">From:<label id="fromDateDispUser" class="lablecss">01/01/2012</label>&nbsp;&nbsp;&nbsp;&nbsp;To:<label class="lablecss" id="toDateDispUser">21/01/2012</label></td></tr></table></td><td width="1%" valign="top" style="padding-right: 3px"><span onclick="closeDivSpan()" style="cursor: pointer">Close</span></td></tr><tr><td colspan="2"><table id="tblUserWiseSummary" class="tblUserWiseSummary table5" width="840" border="1" align="center" cellpadding="0" cellspacing="0"></table></td></tr></table></div>');

                    $("body").prepend(div);

                    $('#dateUser').DatePicker({
                        flat: false,
                        date: ['07/31/2008', '07/28/2008'],
                        format: 'm/d/Y',
                        calendars: 1,
                        mode: 'range',
                        onBeforeShow: function () {
                            $('#dateUser').DatePickerClear();
                            //$('#inputDate').DatePickerSetDate($('#inputDate').val(), true);
                        },
                        onChange: function (formated, dates) {
                            setBackColorForTimeToolbar("timeDateRange", true);
                            if (formated[0].toString() != formated[1].toString()) {
                                var usrID = document.getElementById("hdnCurrentSelectedUser").value;
                                populateSessionAverageForUser(formated[0], formated[1], usrID);
                                setFromDateToDate(formated[0], formated[1], true);
                            }
                        },
                        starts: 0
                    });

                }
                $("#tblUserWiseSummary").empty();
                //$("#tmpTR").remove();

                if ($("#tmpTD").length > 0) {
                    $('#divUserDetails').unwrap();
                }
                if ($("#tmpTR").length > 0) {
                    $('#divUserDetails').unwrap();
                }

                $('<tr id="tmpTR"><td border="0px" colspan="8" id="tmpTD"> </td></tr>').insertAfter('#' + RowID + '');
                $('#divUserDetails').appendTo('#tmpTD');
                $('#divUserDetails').show('slow');

                var FromDate = new Date();
                var ToDate = new Date();
                FromDate.setDate(ToDate.getDate() - 30);

                var ToDateStr = (ToDate.getMonth() + 1) + "/" + ToDate.getDate() + "/" + ToDate.getFullYear();
                var FromDateStr = (FromDate.getMonth() + 1) + "/" + FromDate.getDate() + "/" + FromDate.getFullYear();

                setFromDateToDate(FromDateStr, ToDateStr, true);

                setBackColorForTimeToolbar("timeLastMonth", true);
                populateSessionAverageForUser(FromDateStr, ToDateStr, UserID);
            }
        }

        function addSessionSummaryRowUser() {
            $('table.tblUserWiseSummary').append('<tr><td height="30" colspan="3" align="center" valign="middle"><h2>QOS Summery ( USER )</h2></td></tr><tr><td width="50%" align="center" valign="middle"><h3>Session Name</h3></td><td align="center" valign="middle"><h3>Avg. Resp.</h3></td><td width="35%" align="center" valign="middle"><h3>Performance</h3></td></tr><tr id="99000" class="sessionSummaryRowUser"><td colspan="3"></td></tr>');
        }

        function GetLogsFromDateToDate(LogType, isFirstTime) {

            if (LogType == "sys") {

                var FromDate = $('#from_date_sys').val();
                var ToDate = $('#to_date_sys').val();
                GetSysLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime);

            }
            else {
                var FromDate = $('#from_date_w3c').val();
                var ToDate = $('#to_date_w3c').val();
                GetW3CLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime);

            }

        }
        function GetSharedLogsFromDateToDate(LogType, isFirstTime) {

            var FromDate = $('#from_date_sys').val();
            var ToDate = $('#to_date_sys').val();
            GetSharedSysLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime);



        }

        function GetLogsForUserBySession(SessionID, UserID, LogType) {

            if (LogType == "sys") {
                document.getElementById("hdnSysCount").value = 100;
                GetSysLogsForUserBySession(SessionID, UserID, true);
            }
            else {
                document.getElementById("hdnW3CCount").value = 100;
                GetW3CLogsForUserBySession(SessionID, UserID, true);
            }
        }

        function GetSharedSysLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime) {
            showhidediv();

            if (FromDate == "") {
                alert('Please Select From Date');
                return false;
            }
            if (ToDate == "") {
                alert('Please Select To Date');
                return false;
            }


            $.ajax({

                url: "Service.asmx/getSharedSysLogsByDateTime?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=" + '400' + "&UserID=" + $('#txtEmail').val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    var sysCtr = 0;

                    var html = ' <table style="display:none" id="tblSharedlogs" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"   class="table3 sysLogTable  tablesorter" > <thead><tr class="header"><th style="text-decoration-line: underline;cursor:pointer">Log Time</th> <th style="text-decoration-line: underline; cursor: pointer">User ID</th><th align="center" style="width: 10%;cursor:pointer" valign="middle">Client</th><th  align="center" valign="middle" style="width: 30%;cursor:pointer">Shared User</th><th width="13%" align="center" valign="middle" style="width: 30%;cursor:pointer">App Name</th></tr></thead><tbody></tbody>  </table>';
                    var index = $('#tblSharedlogs tbody tr').length;
                    $('#divlogs').append(html);
                    $.each(data, function (i, jsondata) {

                        var id = "div_" + index;

                        html = '<tr id="' + index + '" ><td align="left" valign="left" style="width:15%" >' + this['DateTimeLocal'] + '</td><td align="left" valign="left" style="width:15%" >' + this['UserID'] + '</td><td align="center" valign="middle" style="padding-left:8px; padding-right:7px;width:5%" >' + $.trim(this['ClientType']) + '</td><td   align="left"  style="width:25%"  >' + $.trim(this['SharedUser']) + '</td><td align="left"  valign="left"  style="width:15%" >' + $.trim(this['ImageName']) + '</td></tr>';
                        $('#tblSharedlogs tbody').append(html);
                        index = index + 1;
                        sysCtr++;

                    });



                    $('#tblSharedlogs').dataTable({
                        "bJQueryUI": true,
                        "iDisplayLength": 20,
                        "bProcessing": true,

                        "sPaginationType": "two_button"

                    });

                    $('#tblSharedlogs').show();

                    $('#btnSysShowMore').css({ visibility: "hidden" });
                    $('#sysShowMore').css('display', 'none');

                },
                error: function (request, status, error) {
                    $('#btnSysShowMore').css({ visibility: "hidden" });
                    $('#sysShowMore').css('display', 'none');
                    alert("No More SysLog Data Available");

                }
            });
        }
        function GetActiveUserSysLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime) {

            showhidediv();



            var html = ' <table id="tblActiveUser" style="display:none" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"                                                                 class="table3 sysLogTable">                                                            <thead><tr class="tableHeader"><th style="cursor:pointer" align="center" valign="middle">Log Time</th><th style="cursor:pointer" align="center" valign="middle">User ID</th><th style="cursor:pointer" align="center" valign="middle">Client</th><th style="cursor:pointer" align="center" valign="middle" style="width: 5%">Log In</th> <th style="cursor:pointer" align="center" valign="middle">IP </th><th style="cursor:pointer" align="center" valign="middle">Org</th> <th style="cursor:pointer" width="13%" align="center" valign="middle">City </th><th style="cursor:pointer" align="center" valign="middle">Country</th><th  align="center" valign="middle" style="width: 25%;cursor:pointer">Token</th><th style="cursor:pointer" align="center" valign="middle">Details</th> </tr></thead><tbody></tbody> </table>';
            $('#divlogs').append(html);

            $.ajax({

                url: "Service.asmx/getActiveUserSysLogsByDateTime",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    var sysCtr = 0;
                    var index = $('#tblActiveUser tbody tr').length;
                    $.each(data, function (i, jsondata) {

                        var id = "div_" + index;

                        html = '<tr id="' + index + '"><td align="left" valign="left" style="width:15%" >' + $.trim(jsondata.DateTimeLocal) + '</td><td   align="center"  style="width:5%"  >' + $.trim(jsondata.UserID) + '</td><td   align="center"  style="width:5%"  >' + $.trim(jsondata.ClientType) + '</td><td   align="left"  style="width:15%" >' + $.trim(jsondata.LogInTime) + '</td><td    valign="left"   >' + $.trim(jsondata.IP) + '</td><td    align="left"   >' + $.trim(jsondata.Org) + '</td><td    align="left"   >' + $.trim(jsondata.City) + '</td><td    align="left"   >' + $.trim(jsondata.Country) + '</td><td align="left"  >' + $.trim(jsondata.Token) + '</td><td align="center" style="width:8%"><a style="cursor:pointer;color:blue" class="' + $.trim(jsondata.Token) + '" onclick="openLogDashBoard(this)" id="' + $.trim(jsondata.UserID) + '">Details</a></td></tr>';
                        $('#tblActiveUser tbody').append(html);
                        index = index + 1;
                        sysCtr++;

                    });


                    $('#tblActiveUser').show();


                    $('#tblActiveUser').dataTable({
                        "bJQueryUI": true,
                        "iDisplayLength": 20,
                        "bProcessing": true,
                        "sPaginationType": "two_button"

                    });


                    $('#btnSysShowMore').css({ visibility: "hidden" });
                    $('#sysShowMore').css('display', 'none');
                },
                error: function (request, status, error) {
                    $('#btnSysShowMore').css({ visibility: "hidden" });

                    $('#sysShowMore').css('display', 'none');
                    alert("No More SysLog Data Available");

                }
            });
        }

        function showhidediv() {
            $('#btnSysShowMore').css({ visibility: "visible" });
            $('#sysShowMore').css('display', 'block');
            $("#divlogs").html('');


        }
        function GetSysLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime) {
            showhidediv();


            var Count = document.getElementById("hdnSysCount").value;
            if (FromDate == "") {
                alert('Please Select From Date');
                return false;
            }
            if (ToDate == "") {
                alert('Please Select To Date');
                return false;
            }


            var html = ' <table  style="display:none"  id="sysLogTable" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"                                                                 class="table3 sysLogTable  tablesorter">                                                              <thead><tr class="tableHeader"><th style="cursor:pointer" align="center" valign="middle">Log Time</th><th style="cursor:pointer" align="center" valign="middle">Client</th><th align="center" valign="middle" style="width: 5%;cursor:pointer">Log In</th> <th style="cursor:pointer" align="center" valign="middle">IP </th><th style="cursor:pointer" align="center" valign="middle">Org</th> <th style="cursor:pointer" width="13%" align="center" valign="middle">City </th><th style="cursor:pointer" align="center" valign="middle">Country</th><th align="center" valign="middle" style="width: 45%;cursor:pointer">Token</th><th style="cursor:pointer;width:5%"  align="center" valign="middle">Details</th> </tr></thead><tbody></tbody></table>';
            $('#divlogs').append(html);


            $.ajax({

                url: "Service.asmx/getSysLogsByDateTime?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=" + Count + "&UserID=" + $('#txtEmail').val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    var sysCtr = 0;
                    var index = $('#sysLogTable tbody tr').length;
                    $.each(data, function (i, jsondata) {

                        var id = "div_" + index;

                        html = '<tr id="' + index + '"><td align="left" valign="left" style="width:15%" >' + $.trim(jsondata.DateTimeLocal) + '</td><td   align="center"  style="width:5%"  >' + $.trim(jsondata.ClientType) + '</td><td   align="left"  style="width:15%" >' + $.trim(jsondata.LogInTime) + '</td><td    valign="left"   >' + $.trim(jsondata.IP) + '</td><td    align="left"   >' + $.trim(jsondata.Org) + '</td><td    align="left"   >' + $.trim(jsondata.City) + '</td><td    align="left"   >' + $.trim(jsondata.Country) + '</td><td align="left" style="width:20%" >' + $.trim(jsondata.Token) + '</td><td style="width:5%"><a style="cursor:pointer;color:blue" class="' + $.trim(jsondata.Token) + '" onclick="openLogDashBoard(this)" id="' + $.trim(jsondata.UserID) + '">Details</a></td></tr>';
                        $('#sysLogTable tbody').append(html);
                        index = index + 1;
                        sysCtr++;

                    });



                    $('#sysLogTable').dataTable({
                        "bJQueryUI": true,
                        "iDisplayLength": 20,
                        "bProcessing": true,
                        "sPaginationType": "two_button"

                    });

                    $('#sysLogTable').show();
                    $('#btnSysShowMore').css({ visibility: "hidden" });
                    $('#sysShowMore').css('display', 'none');
                },
                error: function (request, status, error) {

                    $('#btnSysShowMore').css({ visibility: "hidden" });
                    $('#sysShowMore').css('display', 'none');
                    alert("No More SysLog Data Available");

                }
            });
        }
        function openLogDashBoard(id) {
            var urlToSend = "UserAuditTrail.aspx?UserID=" + $(id).attr('id') + "&FromDate=" + $(id).attr('class').split(',')[0] + "&ToDate=" + $(id).attr('class').split(',')[1];
            window.open(urlToSend, "UserAuditTrail", "location=1,scrollbars=1,resizable=1, left=100, width=1100,height=900");
        }


        function GetW3CLogsForUserFromDateToDate(FromDate, ToDate, isFirstTime) {


            if (isFirstTime == true) {
                $("table.w3cLogTable").find("tr:gt(1)").remove();
                $("table.w3cLogTable").append('<tr class="w3cLogTrLast"><td colspan="5" align="center" valign="middle"></td></tr>');
                $('#btnW3CShowMore').css({ visibility: "visible" });
                $('#btnW3CShowMore').unbind('click');
                $('#btnW3CShowMore').bind('click', function () {
                    GetLogsFromDateToDate('w3c', false);
                });
                document.getElementById("hdnW3CCount").value = 100;
            }


            var Count = document.getElementById("hdnW3CCount").value;

            $.ajax({
                url: "Service.asmx/getW3CLogsByDateTime?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=" + Count + "",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (msg) {

                    //$(".scanTable").empty();

                    var tmpJsonData = msg.d;

                    var w3cCtr = 0;

                    document.getElementById("hdnW3CCount").value = parseInt(Count) + 100;

                    $.each(tmpJsonData, function () {
                        var RequestDate = this['RequestDate'];
                        var SiteName = this['SiteName'];
                        var ComputerName = this['ComputerName'];
                        var ServerIP = this['ServerIP'];
                        var UrlStream = this['UrlStream'];
                        var HTTPStatusCode = this['HTTPStatusCode'];
                        var ResponseTime = this['ResponseTime'];
                        var MethodType = this['MethodType'];

                        var ActualResponseTime = parseFloat(ResponseTime) / 1000;
                        var averageTmp = ActualResponseTime.toString();
                        var averageStrTmp = "";

                        try {
                            if (parseInt(averageTmp.toString().split('.')[0]) > 0) {
                                averageStrTmp = averageTmp.toString().split('.')[0] + " sec " + pad(averageTmp.toString().split('.')[1]) + " msec";
                            }
                            else {
                                if (averageTmp.toString().split('.').length > 1)
                                    averageStrTmp = pad(averageTmp.toString().split('.')[1]) + " msec";
                                else
                                    averageStrTmp = "0 msec";
                            }
                        }
                        catch (e) {
                            averageStrTmp = "0 msec";
                        }


                        if (w3cCtr <= 200) {
                            //alert(aryLogDetails[1]);
                            $('#w3cSiteName').val(SiteName);
                            $('#w3cServerIP').val(ServerIP);
                            $('#w3cComputerName').val(ComputerName);

                            var trClass = 'tr9';
                            if (w3cCtr % 2 == 0)
                                trClass = 'tr10';

                            //trClass += rowClass;

                            $('<tr class="' + trClass + '"><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + RequestDate + '</td><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + UrlStream + '</td><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; " class="sortIPAddress">' + HTTPStatusCode + '</td><td align="left" valign="middle" style="padding-left:8px; padding-right:7px;" >' + MethodType + '</td><td    valign="middle" style="padding-right:7px;">' + averageStrTmp + '</td></tr>').insertBefore('table.w3cLogTable tr.w3cLogTrLast');
                        }

                        w3cCtr++;

                    });


                },
                error: function (request, status, error) {
                    if (isFirstTime == false) {
                        alert("No More W3CLog Data Available");
                    }
                    else {
                        alert("Error at W3CLog Activity Extraction");
                    }
                }
            });
        }

        function closeModal(hash) {
            hash.w.fadeOut('2000', function () { hash.o.remove(); });
        }



        function GetSessionDetailsBySessionID(SessionID) {
            var ctrr = 1;
            var rndmNumber = rtnRandomValFromRange();

            var IsINDReq = document.getElementById('IsINDReq').value;

            $.ajax({
                url: "Service.asmx/GetSessionDetailsBySessionID?SessionID=" + SessionID + "&IsINDReq=" + IsINDReq + "&rndmNumber=" + rndmNumber + "",
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (msg) {

                    var tmpJsonData = msg.d;

                    $('#sessionTbl').empty();
                    $('#sessionSummaryTbl').empty();

                    $('table.sessionTbl').append("<tr class='alwaysVisible'><td height='30px' align='left' style='border-right:0px; padding-left:10px;' colspan='2' valign='middle'><span id='toolbar_filter' class='toolbar'>Filter by: <a id='aTrader' onclick='filterBy(\"Trader\")' >Trader</a>&nbsp|&nbsp<a  id='aBroker' onclick='filterBy(\"Broker\")' >Broker</a>&nbsp|&nbsp<a  id='aAll'  onclick='filterBy(\"All\")' >All</a>&nbsp|&nbsp<a  id='a1secrow' onclick='filterBy(\"1secrow\")' >> 1 Sec</a></span></td><td style='border-left:0px; border-right:0px;' height='30px' align='left' colspan='2' valign='middle'><h2 style='padding-left:40px; text-align:left' align='left'>QOS Details</h2></td><td style='border-left:0px; ' height='30px' colspan='2' align='right' valign='middle'><div id='filter'><form height='30' method='get' id='searchform' action='#'><fieldset class='search'><input onkeyup='filterOnKeyUp(event,this.id);' id='filterInput' name='filter' type='text' class='box' /></fieldset></form></div></td></tr><tr class='alwaysVisible'><td width='30%' align='center' valign='middle'><h3>User Name</h3></td><td width='10%' align='center' valign='middle'><h3>Bank</h3></td><td width='18%' align='center' valign='middle'><h3>IP Address</h3></td><td width='13%' align='center' valign='middle'><h3>Reqeust Name</h3></td><td width='15%' align='center' valign='middle'><h3>Response time</h3></td><td width='14%' align='center' valign='middle'><h3>Avg. Resp.</h3></td></tr><tr  id='1000000' class='sessionRow alwaysVisible'><td colspan='6'></td></tr>");

                    $('table.sessionSummaryTbl').append("<tr><td height='30' colspan='8' align='center' valign='middle'><h2>QOS Summery ( Session )</h2></td></tr><tr><td width='24%' align='center' valign='middle'><h3>User Name / Time Trans.</h3></td><td width='10%' align='center' valign='middle'><h3>Bank</h3></td><td align='center' width='10%' valign='middle'><h3>Current Avg.</h3></td><td width='15%' align='center' valign='middle'><h3>Performance</h3></td><td align='center' width='10%' valign='middle'><h3>24 hr Avg.</h3></td><td align='center' width='10%' valign='middle'><h3>7 Day Avg.</h3></td><td align='center' width='10%' valign='middle'><h3>30 Day Avg.</h3></td><td align='center' width='11%' valign='middle'><h3>Logs</h3></td></tr><tr class='trAvg'><td align='left' valign='middle' style='padding-left:8px; padding-right:7px;' class='sortUserName'>Session Average</td><td align='left' valign='middle' style='padding-left:8px; padding-right:7px; '>-</td><td align='right' valign='middle' style='padding-right:7px' class='currentAvg'>Current Avg</td><td align='left' valign='middle' class='performanceAvg'></td><td class='24hrsAvg' align='right' valign='middle' style='padding-right:7px'>24Hrs Avg</td><td class='weekAvg' align='right' valign='middle' style='padding-right:7px'>week Avg</td><td align='right' valign='middle' style='padding-right:7px' class='monthAvg'>month Avg</td><td></td></tr><tr id='1000000' class='sessionSummaryRow'><td colspan='8'></td></tr>");


                    var tmpData = tmpJsonData;
                    var arrayTwo4Avg = new Array();
                    var arrayTwo4AvgCtr = 0;
                    var totAverage;

                    $.each(tmpData, function () {
                        var ActualResponseTime = this['ActualResponseTime'];
                        var ActualRespSec = ActualResponseTime.split('.')[0];
                        var ActualRespMilliSec = ActualResponseTime.split('.')[1];

                        var roundVal = parseFloat(ActualRespMilliSec / 1000);
                        roundVal = parseFloat(Math.round(roundVal * 1000) / 1000)
                        var secMiliSec = parseInt(ActualRespSec) + parseFloat(roundVal);
                        arrayTwo4Avg[arrayTwo4AvgCtr] = secMiliSec;
                        arrayTwo4AvgCtr++;
                    });

                    totAverage = parseFloat(arrayTwo4Avg.avg());
                    totAverage = Math.round(totAverage * 1000) / 1000;

                    document.getElementById("hdnTotalSessionAvg").value = totAverage;

                    traderAry4GraphSeries = new Array();
                    traderAry4GraphCtr = 1;
                    traderAry4Graph.length = 0;

                    sessionAry4GraphSeries = new Array();
                    sessionAry4GraphCtr = 0;
                    sessionAry4Graph.length = 0;

                    sessionAry4GraphSeries[sessionAry4GraphCtr] = "Current Avg";
                    //                    sessionAry4Graph.push([sessionAry4GraphCtr, totAverage]);
                    sessionAry4Graph.push([0, totAverage]);
                    sessionAry4GraphCtr++;

                    var array = new Array();
                    var arrayTwo = [];

                    var ctr = -1;
                    var currentUser = "";
                    var currentBank = "";
                    var isAvgToAdd = false;
                    var average;
                    var averageStr = "";
                    var summaryCtr = 1;
                    var trClassSmry = "tr3";
                    var UserID;
                    var lastUserID = "";

                    $.each(tmpJsonData, function () {

                        isAvgToAdd = false;

                        var UserName = this['UserName'];
                        UserID = this['UserId'];
                        var BankName = this['Customer'];
                        var Request = this['Request'];
                        var RequestMethod = this['RequestMethod'];
                        var IPAddress = this['IPAddress'];
                        var ActualResponseTime = this['ActualResponseTime'];
                        var ActualRespSec = ActualResponseTime.split('.')[0];
                        var ActualRespMilliSec = ActualResponseTime.split('.')[1];

                        if (lastUserID.toString().length == 0) {
                            lastUserID = this['UserId'];
                        }

                        var roundVal = parseFloat(ActualRespMilliSec / 1000);
                        roundVal = parseFloat(Math.round(roundVal * 1000) / 1000)
                        var secMiliSec = parseInt(ActualRespSec) + parseFloat(roundVal);

                        if ((currentUser.toString() != UserName)) {
                            if (ctr == -1) {
                                ctr = 0;
                                currentUser = UserName;
                                currentBank = BankName;
                            }
                            else {
                                var average = array.avg();
                                average = Math.round(average * 1000) / 1000;

                                traderAry4GraphSeries[traderAry4GraphCtr] = currentUser + ' # ' + currentBank;
                                traderAry4Graph.push([traderAry4GraphCtr, average]);
                                traderAry4GraphCtr++;

                                isAvgToAdd = true;
                                array = new Array();
                                ctr = 0;

                                if (parseInt(average.toString().split('.')[0]) > 0) {
                                    averageStr = average.toString().split('.')[0] + " sec " + pad(average.toString().split('.')[1]) + " msec";
                                }
                                else {
                                    if (average.toString().split('.').length > 1)
                                        averageStr = pad(average.toString().split('.')[1]) + " msec";
                                    else
                                        averageStr = "0 msec";
                                }




                                if (summaryCtr % 2 == 0)
                                    trClassSmry = "tr4";
                                else
                                    trClassSmry = "tr3";

                                var backColor;
                                var Performance;

                                if (average > totAverage) {
                                    backColor = "background-color:#ff545b";
                                    Performance = "Worse Than Average";
                                }
                                else {
                                    backColor = "background-color:#33e958";
                                    Performance = "Better Than Average";
                                }

                                var ctrrRow = traderAry4GraphCtr.toString() + 'Row';

                                //alert(lastUserID);

                                var rol = 1;
                                var over = 2;

                                $('<tr class="' + trClassSmry + '" onmouseover="mouseEvent(\'' + ctrrRow + '\', ' + rol + ', ' + summaryCtr + ')" onmouseout="mouseEvent(\'' + ctrrRow + '\', ' + over + ', ' + summaryCtr + ')" id="' + ctrrRow + '" style="cursor:pointer"><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="left" valign="middle" style="padding-left:8px; padding-right:7px;" class="sortUserName">' + currentUser + '</td><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + currentBank + '</td><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"     valign="middle" style="padding-right:7px">' + averageStr + '</td><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="left" valign="middle" style="padding-left:8px; padding-right:7px; ; ' + backColor + ';">' + Performance + '</td><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  class="24hrs" align="right" valign="middle" style="padding-right:7px">24Hrs</td><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  class="week" align="right" valign="middle" style="padding-right:7px">week</td><td onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="right" valign="middle" style="padding-right:7px" class="month">month</td><td><a onclick="GetLogsForUserBySession(\'' + SessionID + '\', \'' + lastUserID + '\', \'sys\')" >Sys</a> / <a onclick="GetLogsForUserBySession(\'' + SessionID + '\', \'' + lastUserID + '\', \'w3c\')" >W3C</a></td></tr>').insertBefore('table.sessionSummaryTbl tr.sessionSummaryRow');

                                populateTimeBaseAverageForUser(lastUserID, "time24", ctrrRow);
                                populateTimeBaseAverageForUser(lastUserID, "timeLastWeek", ctrrRow);
                                populateTimeBaseAverageForUser(lastUserID, "timeLastMonth", ctrrRow);

                                summaryCtr++;

                                currentUser = UserName;
                                currentBank = BankName;
                                lastUserID = UserID;
                            }
                        }

                        array[ctr] = secMiliSec;
                        arrayTwo.push([ctrr - 1, secMiliSec]);


                        ctr++;
                        //                        var ActualResponseStr = ActualRespSec + " sec " + ActualRespMilliSec + " MilliSec";

                        var respMilliSec;

                        if (roundVal.toString().split('.').length > 1)
                            respMilliSec = roundVal.toString().split('.')[1];
                        else
                            respMilliSec = 0;

                        //                        var ActualResponseStr = ActualRespSec + " sec " + roundVal.toString().split('.')[1] + " MilliSec";

                        var ActualResponseStr = "";
                        var backgroundcolor = "";
                        var rowClass = "";

                        try {
                            if (parseInt(ActualRespSec) > 0) {
                                ActualResponseStr = ActualRespSec + " sec " + pad(respMilliSec) + " msec";
                                backgroundcolor = "#fff78c";
                                rowClass = " 1secrow";
                            }
                            else {
                                ActualResponseStr = pad(respMilliSec) + " msec";
                                backgroundcolor = "none";
                            }

                        }
                        catch (e) {

                        }

                        var IsTrader = this['IsTrader'];
                        var IPAddress = this['IPAddress'];

                        if (IsTrader == true) {
                            rowClass += " Trader All";
                        }
                        else {
                            rowClass += " Broker All";
                        }


                        var trClass = 'tr1';
                        if (ctrr % 2 == 0)
                            trClass = 'tr2';

                        trClass += rowClass;

                        $('<tr class="' + trClass + ' visible" style="background-color:' + backgroundcolor + ';"><td class="sortUserName" align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + UserName + '</td><td class="sortBankName" align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + BankName + '</td><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; " class="sortIPAddress">' + IPAddress + '</td><td class="tdToolTip sortRequest" align="left" valign="middle" style="padding-left:8px; padding-right:7px;" title="' + RequestMethod + '">' + Request + '</td><td align="right" valign="middle" style="padding-right:7px;">' + ActualResponseStr + '</td><td align="right" valign="middle" style="padding-right:7px" id=' + ctrr + '></td></tr>').insertBefore('table.sessionTbl tr.sessionRow');


                        if (isAvgToAdd) {
                            var avgId = ctrr - 1;
                            document.getElementById(avgId).innerText = averageStr;
                        }

                        ctrr++;
                    });


                    $('.tdToolTip').tooltip();

                    $("table.sessionTbl > td.sortUserName").shorten({ width: 268, tail: '...' });
                    $("table.sessionTbl > td.sortBankName").shorten({ width: 88, tail: '...' });
                    $("table.sessionTbl > td.sortIPAddress").shorten({ width: 160, tail: '...' });
                    $("table.sessionTbl > td.sortRequest").shorten({ width: 115, tail: '...' });

                    $("table.sessionSummaryTbl > td.sortUserName").shorten({ width: 100, tail: '...' });

                    filterBy("1secrow");
                    $('#filterInput').val("> 1 Sec");

                    average = array.avg();
                    average = Math.round(average * 1000) / 1000;

                    traderAry4GraphSeries[traderAry4GraphCtr] = currentUser + ' # ' + currentBank;
                    traderAry4Graph.push([traderAry4GraphCtr, average]);
                    traderAry4GraphCtr++;

                    try {
                        if (parseInt(average.toString().split('.')[0]) > 0) {
                            averageStr = average.toString().split('.')[0] + " sec " + pad(average.toString().split('.')[1]) + " msec";
                        }
                        else {
                            if (average.toString().split('.').length > 1)
                                averageStr = pad(average.toString().split('.')[1]) + " msec";
                            else
                                averageStr = "0 msec";
                        }
                    }
                    catch (e) {
                        averageStr = "0 msec";
                    }

                    var avgId = ctrr - 1;
                    document.getElementById(avgId).innerText = averageStr;

                    if (summaryCtr % 2 == 0)
                        trClassSmry = "tr4";
                    else
                        trClassSmry = "tr3";

                    if (average > totAverage) {
                        backColor = "background-color:#ff545b";
                        Performance = "Worse Than Average";
                    }
                    else {
                        backColor = "background-color:#33e958";
                        Performance = "Better Than Average";
                    }

                    var ctrrRow = traderAry4GraphCtr.toString() + 'Row';

                    var rol = 1;
                    var over = 2;

                    //alert(lastUserID);

                    $('<tr class="' + trClassSmry + '"  onmouseover="mouseEvent(\'' + ctrrRow + '\', ' + rol + ', ' + summaryCtr + ')" onmouseout="mouseEvent(\'' + ctrrRow + '\', ' + over + ', ' + summaryCtr + ')"  id="' + ctrrRow + '" style="cursor:pointer"><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + currentUser + '</td><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + currentBank + '</td><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="right" valign="middle" style="padding-right:7px">' + averageStr + '</td><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="left" valign="middle" style="padding-left:8px; padding-right:7px; ; ' + backColor + ';">' + Performance + '</td><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  class="24hrs" align="right" valign="middle" style="padding-right:7px">24Hrs</td><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  class="week" align="right" valign="middle" style="padding-right:7px">week</td><td  onclick="getUserSessions(\'' + ctrrRow + '\', ' + lastUserID + ')"  align="right" valign="middle" style="padding-right:7px" class="month">month</td><td><a onclick="GetLogsForUserBySession(\'' + SessionID + '\', \'' + lastUserID + '\', \'sys\')" >Sys</a> / <a onclick="GetLogsForUserBySession(\'' + SessionID + '\', \'' + lastUserID + '\', \'w3c\')" >W3C</a></td></tr>').insertBefore('table.sessionSummaryTbl tr.sessionSummaryRow');

                    populateTimeBaseAverageForUser(lastUserID, "time24", ctrrRow);
                    populateTimeBaseAverageForUser(lastUserID, "timeLastWeek", ctrrRow);
                    populateTimeBaseAverageForUser(lastUserID, "timeLastMonth", ctrrRow);

                    $.plot($("#placeholderVCM"), [{ label: "Response Time (All Users)", data: arrayTwo, bars: { show: true } }]);


                    if (parseInt(totAverage.toString().split('.')[0]) > 0) {
                        document.getElementById("totAverage").innerText = totAverage.toString().split('.')[0] + " sec " + pad(totAverage.toString().split('.')[1]) + " msec";
                    }
                    else {
                        if (totAverage.toString().split('.').length > 1)
                            document.getElementById("totAverage").innerText = pad(totAverage.toString().split('.')[1]) + " msec";
                        else
                            document.getElementById("totAverage").innerText = "0 msec";
                    }

                    var fromDate = new Date();
                    var toDate = new Date();

                    //                    sessionAry4Graph.length = 0;


                    lst24 = "";
                    lst7 = "";
                    lstMonth = "";


                    summaryCtr++;
                    fromDate.setDate(toDate.getDate() - 1);
                    getSessionAverageFromDateToDate(fromDate, toDate, summaryCtr, "24 Hours", totAverage);

                    //summaryCtr++;
                    fromDate.setDate(toDate.getDate() - 7);
                    getSessionAverageFromDateToDate(fromDate, toDate, summaryCtr, "Last Week", totAverage);

                    //summaryCtr++;
                    fromDate.setDate(toDate.getDate() - 30);
                    getSessionAverageFromDateToDate(fromDate, toDate, summaryCtr, "Last Month", totAverage);

                    //summaryCtr++;
                    fromDate = new Date(document.getElementById("hdnFromDate").value);
                    toDate = new Date(document.getElementById("hdnToDate").value);
                    var crntSessionTrans = (fromDate.getMonth() + 1) + "/" + fromDate.getDate() + "/" + fromDate.getFullYear() + " - " + (toDate.getMonth() + 1) + "/" + toDate.getDate() + "/" + toDate.getFullYear();
                    getSessionAverageFromDateToDate(fromDate, toDate, summaryCtr, crntSessionTrans, totAverage);

                },
                error: function (request, status, error) {
                    $('#sessionTbl').empty();
                    $('#sessionSummaryTbl').empty();
                    $("#placeholderVCM").empty();
                    $("#placehoderSession").empty();

                    document.getElementById("totAverage").innerText = "";

                    alert("No Data Found, Please Try to Include Statistics From India");

                }
            });
        }

        function mouseEvent(RowID, evnt, RowCtr) {
            if (evnt == 1) {
                document.getElementById(RowID).className = "tdhover";
            }
            else {
                if (RowCtr % 2 == 0) {
                    document.getElementById(RowID).className = "tr4";
                }
                else {
                    document.getElementById(RowID).className = "tr3";
                }
            }
        }

        function filterStatistics() {
            var IsINDReq = document.getElementById('IsINDReq').value;

            if (IsINDReq == "0") {
                document.getElementById('IsINDReq').value = 1;
                document.getElementById('filterStatistics').innerText = "Exclude Statistics From India";
            }
            else {
                document.getElementById('IsINDReq').value = 0;
                document.getElementById('filterStatistics').innerText = "Include Statistics From India";
            }
            populateSessionDetails();
        }

        function getW3CData() {
            alert("This operation will take some time to complete.  Please discontinue if any session is going on.");

            if (confirm("Do you want to continue?")) {
                $.ajax({
                    url: "getWebLogs.ashx",
                    data: "{}",
                    success: function (msg) {
                        alert(msg);
                    },
                    error: function (request, status, error) {
                        alert("Error");
                    }
                });
            } else {

            }
        }

        function openLogDashBoard(id) {
            var urlToSend = "UserAllActivity.aspx?UserID=" + $(id).attr('id') + "&Token=" + $(id).attr('class').split(',')[0] + "&LoginID=";
            window.open(urlToSend, "UserAllActivity", "location=1,scrollbars=1,resizable=1, left=100, width=1100,height=900");
        }

    </script>

    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 18px;
            font-weight: normal;
            color: #1d4b8a;
            text-align: center;
            width: 33%;
        }

        .style2
        {
            width: 33%;
        }

        .bgcolor
        {
            background: linear-gradient(to bottom, rgba(255,255,255,1) 0%,rgba(229,229,229,1) 100%);
        }

        .buttoncolor
        {
            background: linear-gradient(to bottom, rgba(225,255,255,1) 0%,rgba(225,255,255,1) 7%,rgba(225,255,255,1) 12%,rgba(253,255,255,1) 12%,rgba(230,248,253,1) 30%,rgba(200,238,251,1) 54%,rgba(190,228,248,1) 75%,rgba(177,216,245,1) 100%); /* W3C */
            color: black;
        }

        .header
        {
            background-color: #B1FDF3;
            border-color: #CCCCCC;
            cursor: pointer;
            color: navy;
            border-bottom: 1px solid blue;
        }


        .tableHeader
        {
            color: Black;
            background-color: #B1FDF3;
            border-color: #CCCCCC;
            font-size: 14px;
            font-weight: bold;
        }

        .nestedtableHeader
        {
            background-color: #4687E7;
            border-color: #CCCCCC;
            color: #FFFFFF;
            font-size: 12px;
        }

        .nestedtr
        {
            background-color: #4687E7;
            border-color: #CCCCCC;
            color: #FFFFFF;
            font-size: 12px;
        }

        .hover
        {
            background-color: #ffff99 !important;
        }

        /*.divclass
        {
            background: linear-gradient(to bottom, rgba(225,255,255,1) 0%,rgba(225,255,255,1) 7%,rgba(225,255,255,1) 12%,rgba(253,255,255,1) 12%,rgba(230,248,253,1) 30%,rgba(200,238,251,1) 54%,rgba(190,228,248,1) 75%,rgba(177,216,245,1) 100%);
        }*/
        .filterbg
        {
            background: linear-gradient(to bottom, rgba(246,248,249,1) 0%,rgba(229,235,238,1) 50%,rgba(215,222,227,1) 51%,rgba(245,247,249,1) 100%);
            border: 1px solid;
        }

        .navbar-inner
        {
            /*min-height: 40px;*/
            padding-right: 20px;
            padding-left: 20px;
            background-color: #fafafa;
            background-repeat: repeat-x;
            border: 1px solid #d4d4d4;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            zoom: 1;
            -webkit-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.065);
            -moz-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.065);
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.065);
            background-image: linear-gradient(to bottom, #ffffff, #f2f2f2);
        }

        .pull-left
        {
            float: left;
        }

        .navbar-text
        {
            margin-bottom: 0;
            line-height: 40px;
            color: #777777;
        }

        .pull-right
        {
            float: right;
        }

        .blue-bold
        {
            font-family: Verdana, Geneva, sans-serif;
            font-size: 10px;
            font-weight: bold;
            color: #013a81;
            text-decoration: none;
        }

        a
        {
            color: #0088cc;
            text-decoration: none;
        }

        .oddtr
        {
            /* background-color: rgb(216, 238, 254);*/
            background: linear-gradient(to bottom, rgba(225,255,255,1) 0%,rgba(225,255,255,1) 7%,rgba(225,255,255,1) 12%,rgba(253,255,255,1) 12%,rgba(230,248,253,1) 30%,rgba(200,238,251,1) 54%,rgba(190,228,248,1) 75%,rgba(177,216,245,1) 100%);
        }

        .eventr
        {
            /*background-color: rgb(234, 248, 255);*/
            background: linear-gradient(to bottom, rgba(246,248,249,1) 0%,rgba(229,235,238,1) 50%,rgba(215,222,227,1) 51%,rgba(245,247,249,1) 100%);
        }

        .PopupPanel
        {
            z-index: 100;
            height: 55%;
            margin-top: -200px;
            width: 100%;
            margin-left: 7%;
            margin-top: -73%;
            position: relative;
            left: -110.703125px;
            top: 226.25px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--  <div class="span2">
                
            </div>
            <div class="span10">--%>
            <div class="navbar-inner" style="height: 42px!important">
                <a class="brand pull-left" href="http://www.thebeastapps.com/"></a>
                <div class="tagline-divider">
                    <img class="pull-left" id="imgLogo" alt="TheBeast Apps" src="images/thebeastapps_logo.jpg"
                        style="height: 40px !important;" />
                </div>
                <div class="navbar-text pull-left" style="padding-left: 10px !important; height: 40px">
                    The BEAST Financial Framework
                </div>
                <div class="navbar-text pull-right">
                    <a href="Signout.aspx" style="width: 60px; display: block;">Log Out</a>
                </div>

                <div class="navbar-text pull-right" style="padding: 0px 3px;">
                    |
                </div>
                <div class="navbar-text pull-right">
                    <a href="LogDashboardAll.aspx" style="width: 60px; display: block;">Audit Trail</a>
                </div>

                <div class="navbar-text pull-right" style="padding: 0px 3px;">
                    |
                </div>
                <div class="navbar-text pull-right">
                    <a href="AutoURL.aspx/" style="width: 60px; display: block;">Home</a>
                </div>
                <div class="navbar-text pull-right" style="padding: 0px 3px;">
                    |
                </div>
                <div class="navbar-text pull-right">
                    <span class="blue-bold">
                        <asp:Label ID="lblUserName" runat="server"></asp:Label></span>
                </div>
            </div>
            <%--</div>--%>
        </div>
        <table width="1200" align="center" cellpadding="0" cellspacing="0" class="whitebg">
            <tr align="left" valign="middle">
                <td height="390" align="center" valign="top">
                    <table width="1150" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" style="width: 100%; padding-top: 2px;" valign="middle">
                                <asp:Label ID="lblsessionmsg" Text="WebLog Transactions" runat="server" CssClass="bluename" Style="text-align: center; font-size: 12px;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdImpersonate" runat="server" style="width: 100%;" valign="top" align="left"
                                colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0" align="center">
                                    <tr>
                                        <td style="width: 35%">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                            </table>
                                        </td>
                                        <td align="right" valign="top" style="width: 15%;"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="20" align="left" valign="middle" class="prihead"></td>
                        </tr>
                        <tr>
                            <td style="width: 100%" valign="top" align="left" colspan="2" border="3">
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table width="100%" align="center" cellpadding="0" cellspacing="0" class="table1">
                                                <tr style="display: none">
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <input type="button" id="btnGetW3CData" onclick="getW3CData()" value="Retrieve Web Logs (W3C)" />
                                                                </td>
                                                                <td height="15" align="right" valign="middle" class="bluesmall">* Sys Log / W3c Log Dashboard
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr class="filterbg">
                                                    <td align="center" valign="middle">
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">

                                                            <tr align="center">
                                                                <td>E-mail :
                                        <input type="text" id="txtEmail" style="padding-left: 10px; margin-right: 10px; width: 230px;" />
                                                                </td>
                                                                <td>From Date :
                                        <input type="text" name="from_date_sys" id="from_date_sys" style="padding-left: 10px; margin-right: 10px; width: 180px;" />
                                                                </td>
                                                                <td>To Date :
                                        <input id="to_date_sys" name="" type="text" style="padding-left: 10px; padding-right: 10px; width: 180px;" />
                                                                </td>
                                                                <td>
                                                                    <input type="button" value="Show Logs" style="vertical-align: top" class="oddtr" id="btnShowSysLog" onclick="GetLogsFromDateToDate('sys', true)" />
                                                                </td>
                                                                <td>
                                                                    <input type="button" value="Shared Logs" style="vertical-align: top" class="oddtr" onclick="GetSharedLogsFromDateToDate('sys', true)" />
                                                                </td>
                                                                <td>
                                                                    <input type="button" value="Show Active User" style="vertical-align: top" class="oddtr" onclick="GetActiveUserSysLogsForUserFromDateToDate('sys', true)" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td align="center" valign="top">
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td align="right" valign="top"></td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                </tr>
                                                <%--   <tr id="sysShowMore">
                                                    <td style="width: 100%;" align="center">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="text-align: center;" align="center">
                                                                    <img id="btnSysShowMore" style="cursor: pointer; visibility: hidden" src="AuditTrailCss/images/Loader.gif" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>--%>
                                                <tr style="width: 100%">
                                                    <td align="left" valign="top" style="max-width: 70%">
                                                        <div id="divlogs" style="overflow: auto; height: 780px; width: 100%; overflow: scroll; background: linear-gradient(to bottom, rgba(238,238,238,1) 0%,rgba(238,238,238,1) 100%); /* W3C */">

                                                            <%-- <table id="sysLogTable" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"
                                                                class="table3 sysLogTable  tablesorter">
                                                            </table>--%>

                                                            <%-- <table id="tblSharedlogs" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"
                                                                class="table3 sysLogTable  tablesorter" style="display: none">
                                                            </table>--%>
                                                            <%--<table id="tblActiveUser" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"
                                                                class="table3 sysLogTable">
                                                            </table>--%>
                                                        </div>
                                                    </td>

                                                </tr>

                                            </table>

                                        </td>
                                    </tr>
                                    <tr class="sessionbg" style="display: none">
                                        <td height="80" align="center" valign="middle">
                                            <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td height="30" align="center" valign="top" class="bluesession">W3C Transactions
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="33%" align="center" valign="middle" class="syslable">From Date :
                                        <input name="" id="from_date_w3c" type="text" style="padding-left: 10px; margin-right: 10px; width: 180px;"
                                            onclick="return from_date_w3c_onclick()" />To Date :
                                        <input name="" id="to_date_w3c" type="text" style="padding-left: 10px; padding-right: 10px; width: 180px;" />
                                                        <img id="btnShowW3cLog" onclick="GetLogsFromDateToDate('w3c', true)" src="images/show-log.png"
                                                            style="cursor: pointer" width="88" height="20" hspace="8" border="0" align="absmiddle" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td height="80" align="center" valign="middle">
                                            <div style="overflow: auto; height: 380px; width: 1050px;">
                                                <table id="w3cLogTable" class="w3cLogTable table3" width="1000" border="0" align="center"
                                                    cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td height="30" colspan="3" align="center" valign="middle">
                                                            <h2>W3CLog Activities</h2>
                                                        </td>
                                                    </tr>
                                                    <tr class="w3cLogTr">
                                                        <td width="14%" align="center" valign="middle">
                                                            <h3>Log Time</h3>
                                                        </td>
                                                        <td width="48%" align="center" valign="middle">
                                                            <h3>URL Stream</h3>
                                                        </td>
                                                        <td width="13%" align="center" valign="middle">
                                                            <h3>HTTP Status Code</h3>
                                                        </td>
                                                        <td width="10%" align="center" valign="middle">
                                                            <h3>Method Type</h3>
                                                        </td>
                                                        <td width="12%" align="center" valign="middle">
                                                            <h3>Response Time</h3>
                                                        </td>
                                                    </tr>
                                                    <tr class="w3cLogTrLast">
                                                        <td colspan="5" align="center" valign="middle"></td>
                                                    </tr>
                                                </table>
                                                <table width="100%" style="text-align: center">
                                                    <tr class="w3cShowMore">
                                                        <td colspan="5" align="center" valign="middle">
                                                            <img id="btnW3CShowMore" style="cursor: pointer; visibility: hidden;" src="images/showmoreresult.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="70" align="center" valign="middle" class="btmbg">
                    <table width="99%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" valign="middle">
                                <img src="images/verisign-logo.jpg" width="70" height="51" hspace="10" />
                            </td>
                            <td align="right" valign="middle">
                                <div class="copyrighttxt" style="margin-top: 15px">
                                    <script type="text/javascript">
                                        copyright = new Date();
                                        update = copyright.getFullYear();
                                        document.write("Copyright © 2008-" + update + " The Beast Apps All rights reserved.");
                                    </script>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
        <div class="PopupPanel" id="sysShowMore">
            <img id="btnSysShowMore" style="cursor: pointer; visibility: hidden" src="AuditTrailCss/images/Loader.gif" />
        </div>
        <asp:HiddenField ID="hdnFromDate" runat="server" />
        <asp:HiddenField ID="hdnToDate" runat="server" />
        <asp:HiddenField ID="hdnTotalSessionAvg" runat="server" />
        <asp:HiddenField ID="hdnCurrentSelectedUser" runat="server" />
        <asp:HiddenField ID="IsINDReq" Value="0" runat="server" />
        <asp:HiddenField ID="hdnW3CCount" Value="100" runat="server" />
        <asp:HiddenField ID="hdnSysCount" Value="100" runat="server" />
        <asp:HiddenField ID="hdnValueFromDefault" Value="" runat="server" />

    </form>

</body>


</html>
