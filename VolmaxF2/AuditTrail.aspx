<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditTrail.aspx.cs" Inherits="AuditTrail" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <title>TheBeastApps | Dashboard</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no'
        name='viewport' />
    <!-- bootstrap 3.0.2 -->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- font Awesome -->
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="css/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <%--[if lt IE 9]>
   
    <![endif]--%>
    <!-- jQuery 2.0.2 -->
    <script src="js/jquery.min.js"></script>

    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="Javascript/jquery-ui.js"></script>
    <link href="Theme/jquery-ui.css" rel="stylesheet" />
    <script src="Javascript/jquery-ui-timepicker-addon.js"></script>
    <script src="js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/dataTables.bootstrap.js" type="text/javascript"></script>
    <script src="js/app.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(document).ready(function () {

            $('#from_date_sys').datetimepicker({
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                inline: true
            });

            var d = new Date();
            d.setDate(d.getDate() - 7);

            $("#from_date_sys").datepicker("setDate", d);

            $('#to_date_sys').datetimepicker({
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                inline: true
            });

            var dtodate = new Date();
            dtodate.setDate(dtodate.getDate());

            $("#to_date_sys").datepicker("setDate", dtodate);

            document.getElementById("hdnFromDate").value = $("#from_date_sys").val();
            document.getElementById("hdnToDate").value = $("#to_date_sys").val();

            MostUsedUsers($("#from_date_sys").val(), $("#to_date_sys").val());
            MostUsedApps($("#from_date_sys").val(), $("#to_date_sys").val());
            MostSharedApps($("#from_date_sys").val(), $("#to_date_sys").val());

        });




        function populateAppInteraction(SIFid) {


            $('#tblAppWiseSummary').empty();
            addSessionSummaryRowApps(SIFid);

            //var rndmNumber = rtnRandomValFromRange();

            $.ajax({
                url: "Service.asmx/AppInteraction?FromDate=" + $("#from_date_sys").val() + "&ToDate=" + $("#to_date_sys").val() + "&SIFid=" + SIFid + "&VendorID=" + $('#VendorID').val() + '',

                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    var summaryCtr = 1;
                    var summaryCtr = 1;
                    $.each(data, function (i, jsondata) {


                        var trClassSmry = "";

                        if (summaryCtr == 0)
                            trClassSmry = "";

                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr5";
                        else
                            trClassSmry = "tr6";

                        var tbleid = 'sessionSummaryRowUser' + SIFid;
                        $('<tr><td>' + $.trim(jsondata.UserName) + '</td><td>' + $.trim(jsondata.Seen) + '</td><td>' + $.trim(jsondata.LastSeen) + '</td></tr>').insertBefore('.' + tbleid);

                        summaryCtr++;

                    });
                    $('#tblAppWiseSummary' + SIFid).dataTable({
                        // "bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": true,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "bDestroy": true


                    });
                },
                error: function (request, status, error) {
                    $("#tblAppWiseSummary").empty();
                    // alert("User Detail Not Found For this App");
                }
            });
        }

        function populateSessionAverageForUser(RowID, UserID, LastSeen, ddl) {


            $('#tblUserWiseSummary' + UserID).empty();

            //var rndmNumber = rtnRandomValFromRange();

            $.ajax({
                url: "Service.asmx/getUserAllActivity?Userid=" + UserID + "&LastSeen=" + LastSeen + "&VendorID=" + $('#VendorID').val() + '',
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {
                    var flag = false;
                    var summaryCtr = 1;
                    var summaryCtr = 1;
                    addSessionSummaryRowUser(UserID);

                    $.each(data, function (i, jsondata) {

                        var trClassSmry = "";
                        var Activity = $.trim(jsondata.Activity);
                        if ($.trim(jsondata.Activity) == "App Created") {
                            trClassSmry = "success";
                        }
                        else if ($.trim(jsondata.Activity) == "App Closed") {
                            trClassSmry = "danger";
                        }
                        else if ($.trim(jsondata.Activity) == "App Shared") {
                            trClassSmry = "info";
                        }
                        else if ($.trim(jsondata.Activity) == "Shared App Opened") {
                            trClassSmry = "info";
                        }
                        else if ($.trim(jsondata.Activity) == "User Authenticated") {
                            trClassSmry = "warning";
                        }
                        else if ($.trim(jsondata.Activity) == "User Exited") {
                            trClassSmry = "warning";
                        }

                        else {
                            trClassSmry = "active";
                        }

                        var tbleid = 'sessionSummaryRowUser' + UserID;
                        $('<tr class=' + trClassSmry + '><td style="display:none"></td><td>' + $.trim(jsondata.DateTimeLocal) + ' <td>' + $.trim(jsondata.AppName) + '</td><td>' + Activity + '</td><td>' + $.trim(jsondata.OtherDetail).replace(/,/g, '\n') + '</td></tr>').insertBefore('.' + tbleid);

                        summaryCtr++;


                    });
                    $('#tblUserWiseSummary' + UserID).dataTable({
                        //    "bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": true,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "bDestroy": true,
                        "bSort": true

                    });
                    $('#loading' + UserID).hide();

                    getSessionList(RowID, UserID, LastSeen, ddl);


                },
                error: function (request, status, error) {
                    $("#tblUserWiseSummary" + UserID).empty();
                    //alert("Session Activity Not Found For this USER For Given Date/Dates (Default Last 1 Month)");
                }
            });
        }
        function getUserSessions(RowID, UserID, LastSeen) {
            var divid = 'divDetailsnew' + UserID;

            var flag = LastSeen;
            var id = 'tmpTR' + RowID;


            if ($("#tmpTR" + RowID).length > 0 && flag != "0") {

                if ($("#tmpTR" + RowID).css('display') == 'none') {
                    $('#tmpTR' + RowID).show('slow');
                }
                else {
                    $('#tmpTR' + RowID).hide();
                }
            }
            else {

                var loading = 'loading' + UserID;
                var tblid = 'tblUserWiseSummary' + UserID;

                if (flag == "0") {
                    LastSeen = $("#getsessionid" + UserID + ' :selected').val();
                    $('#' + divid).remove();
                    $('#loading' + UserID).show();
                    var div = '<div  id="' + divid + '" style="width: 100%;"><table id="' + tblid + '" class="' + tblid + ' table table-bordered table-hover" width="100%" align="center" cellpadding="0" cellspacing="0"></table>';

                    $(div).insertAfter('#' + loading + '');
                }
                else {

                    var div = '<tr  id=' + id + ' style="background-color: #f5f5f5;" ><td border="0px" colspan="4" id="tmpTD">  <div align="center" id="' + loading + '" style="z-index: 100; position: relative;display:none"><img src="images/ajax-loader.gif" /></div><div id="' + divid + '" style="width: 100%;"><table id="' + tblid + '" class="' + tblid + ' table table-bordered table-hover" width="100%" align="center" cellpadding="0" cellspacing="0"></table></div></td></tr>';

                    $(div).insertAfter('#' + RowID + '');
                    $('#' + divid).show('slow');

                }


                populateSessionAverageForUser(RowID, UserID, LastSeen, flag);

            }
        }

        function getAppInteraction(RowID, SifID) {

            if ($("#tmpTRApp" + RowID).length > 0) {

                if ($("#tmpTRApp" + RowID).css('display') == 'none') {
                    $('#tmpTRApp' + RowID).show('slow');
                }
                else {
                    $('#tmpTRApp' + RowID).hide();
                }
            }
            else {
                var id = 'tmpTRApp' + RowID;
                var tblid = "tblAppWiseSummary" + SifID;
                var div = '<tr  id=' + id + ' class="info" ><td border="0px" colspan="4" id="tmpTDApp"><div id="divAppDetailsnew" style="width: 100%;"><table id="' + tblid + '" class="' + tblid + ' table table-bordered table-striped" width="100%" align="center" cellpadding="0" cellspacing="0"></table></div></td></tr>';

                $(div).insertAfter('#' + RowID + '');

                $('#divAppDetailsnew').show('slow');

                populateAppInteraction(SifID);
            }

        }
        function getSharedAppInteraction(RowID, SifID) {

            if ($("#tmpTRAppShared" + RowID).length > 0) {

                if ($("#tmpTRAppShared" + RowID).css('display') == 'none') {
                    $('#tmpTRAppShared' + RowID).show('slow');
                }
                else {
                    $('#tmpTRAppShared' + RowID).hide();
                }
            }
            else {
                var id = 'tmpTRAppShared' + RowID;
                var tblid = "tblAppWiseSummaryShared" + SifID;
                var div = '<tr  id=' + id + ' class="success" ><td border="0px" colspan="4" ><div id="divAppDetailsnewShared" style="width: 100%;"><table id="' + tblid + '" class="' + tblid + ' table table-bordered table-striped" width="100%" align="center" cellpadding="0" cellspacing="0"></table></div></td></tr>';

                $(div).insertAfter('#' + RowID + '');

                $('#divAppDetailsnewShared').show('slow');

                populateAppInteractionShared(SifID);
            }

        }
        function populateAppInteractionShared(SIFid) {


            $('#tblAppWiseSummaryShared').empty();
            addSessionSummaryRowAppsShared(SIFid);

            //var rndmNumber = rtnRandomValFromRange();

            $.ajax({
                url: "Service.asmx/SharedAppInteraction?FromDate=" + $("#from_date_sys").val() + "&ToDate=" + $("#to_date_sys").val() + "&SIFid=" + SIFid + "&VendorID=" + $('#VendorID').val() + '',

                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    var summaryCtr = 1;
                    var summaryCtr = 1;
                    $.each(data, function (i, jsondata) {


                        var trClassSmry = "";

                        if (summaryCtr == 0)
                            trClassSmry = "";

                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr5";
                        else
                            trClassSmry = "tr6";

                        var tbleid = 'sessionSummaryRowUserShared' + SIFid;
                        $('<tr><td>' + $.trim(jsondata.UserName) + '</td><td>' + $.trim(jsondata.SharedUser) + '</td><td>' + $.trim(jsondata.DateTimeLocal) + '</td></tr>').insertBefore('.' + tbleid);

                        summaryCtr++;

                    });
                    $('#tblAppWiseSummaryShared' + SIFid).dataTable({
                        // "bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": true,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "bDestroy": true


                    });
                },
                error: function (request, status, error) {
                    $("#tblAppWiseSummary").empty();
                    // alert("User Detail Not Found For this App");
                }
            });
        }
        function addSessionSummaryRowAppsShared(SifID) {
            var tbleid = 'sessionSummaryRowUserShared' + SifID;
            $('#tblAppWiseSummaryShared' + SifID).append('<thead><tr><th>Initiator</th><th>Participants</th><th>DateTime (GMT)</th></thead></tr><tbody><tr id="99000" class="' + tbleid + '" style="display:none"><td></td><td></td><td></td></tr></tbody>');
        }

        function addSessionSummaryRowUser(UserID) {
            var tbleid = 'sessionSummaryRowUser' + UserID;
            $('#tblUserWiseSummary' + UserID).append('<thead><tr><th style="display:none"></th><th> Date Time (GMT)</th><th>App Name</th><th>Activity</th><th>Other Detail</th></tr></thead><tbody><tr id="99000" class="' + tbleid + '" style="display:none"><td></td><td></td><td></td><td></td><td></td></tr></tbody>');
        }
        function addSessionSummaryRowApps(SifID) {
            var tbleid = 'sessionSummaryRowUser' + SifID;
            $('#tblAppWiseSummary' + SifID).append('<thead><tr><th>User Name</th><th>Used</th><th>Last Seen (GMT)</th></thead></tr><tbody><tr id="99000" class="' + tbleid + '" style="display:none"><td></td><td></td><td></td></tr></tbody>');
        }


        function MostUsedUsers(FromDate, ToDate) {
            var ctrr = 1;
            //url: "Service.asmx/MostUsedUsers?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=10" + "&UserID=" + '' + "&VendorID=" + $('#VendorID').val(),
            $.ajax({
                url: "Service.asmx/MostUsedUsers?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=10" + "&UserID=" + '' + "&VendorID=" + $('#VendorID').val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {
                    var tmpJsonData = data.d;
                    var summaryCtr = 1;

                    $('#sessionSummaryTbl').empty();

                    $('table.sessionSummaryTbl').append("<thead><tr> <th style='display:none' width='5%' align='center' valign='middle'></th> <th>User Name</th><th>Seen</th><th>Last Seen (GMT) </th>    </tr></thead>  <tbody>   <tr id='1000000' class='sessionSummaryRow' style='display:none'><td></td><td></td><td></td><td></td></tr></tbody>");

                    $.each(data, function (i, jsondata) {
                        var trClassSmry = "tr3";
                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr4";
                        else
                            trClassSmry = "tr3";

                        var ctrrRow = summaryCtr + 'UserRow';


                        var rol = 1;
                        var over = 2;

                        var lastseen = $.trim(jsondata.LastSeen).replace(/ /gi, '|');
                        //var onclick = "onclick =getUserSessions(" + ctrrRow + "','" + $.trim(jsondata.UserID) + "','" + lastseen + "'" + " ) ";
                        var onclick = "onclick =getUserSessions('" + ctrrRow + "','" + $.trim(jsondata.UserID) + "','" + lastseen + "') ";

                        $('<tr onmouseover="mouseEvent(\'' + ctrrRow + '\', ' + rol + ', ' + summaryCtr + ')" onmouseout="mouseEvent(\'' + ctrrRow + '\', ' + over + ', ' + summaryCtr + ')" id="' + ctrrRow + '"  style="cursor:pointer"  ><td style="display:none"><  input type="checkbox" /> </td><td ' + onclick + '>' + $.trim(jsondata.UserName) + '</td><td ' + onclick + '>' + $.trim(jsondata.Seen) + '</td><td ' + onclick + '>' + $.trim(jsondata.LastSeen) + '</td></tr>').insertBefore('table.sessionSummaryTbl tr.sessionSummaryRow');

                        summaryCtr++;

                    });

                    $('#sessionSummaryTbl').dataTable({
                        //"bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": true,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "bDestroy": true,
                        "aoColumnDefs": [
      { "bSortable": false, "aTargets": [0] }
                        ]
                    });


                },

                error: function (request, status, error) {

                    $('#btnSysShowMore').css({ visibility: "hidden" });
                    //alert("No More SysLog Data Available");

                }
            })
        }
        function MostUsedApps(FromDate, ToDate) {

            var ctrr = 1;

            $.ajax({
                url: "Service.asmx/MostUsedApps?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=10" + "&UserID=" + '' + "&VendorID=" + $('#VendorID').val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {
                    var tmpJsonData = data.d;
                    var summaryCtr = 1;

                    $('#sessionSummaryTblApps').empty();

                    $('table.sessionSummaryTblApps').append("<thead><tr> <th style='display:none' width='5%' align='center' valign='middle'></th> <th>App Name</th><th>Used</th><th>Last Seen (GMT)</th></tr></thead><tbody><tr id='1000000' class='sessionSummaryRow' style='display:none'><td></td><td></td><td></td><td></td></tr></tbody>");

                    $.each(data, function (i, jsondata) {
                        var trClassSmry = "tr3";
                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr4";
                        else
                            trClassSmry = "tr3";

                        var ctrrRow = summaryCtr + 'RowApp';


                        var rol = 1;
                        var over = 2;

                        var onclick = "onclick =getAppInteraction('" + ctrrRow + "','" + $.trim(jsondata.ImageSIFID) + "') ";

                        // onclick = "onclick =getAppInteraction('" + ctrrRow + "','" + $.trim(jsondata.ImageSIFID) + "') ";
                        $('<tr onmouseover="mouseEvent(\'' + ctrrRow + '\', ' + rol + ', ' + summaryCtr + ')" onmouseout="mouseEvent(\'' + ctrrRow + '\', ' + over + ', ' + summaryCtr + ')" id="' + ctrrRow + '"  style="cursor:pointer"  ><td style="display:none"><input type="checkbox" /> </td><td ' + onclick + '>' + $.trim(jsondata.AppName) + '</td><td ' + onclick + '>' + $.trim(jsondata.Seen) + '</td><td ' + onclick + '>' + $.trim(jsondata.LastSeen) + '</td></tr>').insertBefore('table.sessionSummaryTblApps tr.sessionSummaryRow');

                        summaryCtr++;

                    });

                    $('#sessionSummaryTblApps').dataTable({
                        //"bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": false,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "aoColumnDefs": [
      { "bSortable": false, "aTargets": [0] }
                        ]

                    });


                }
            })
        }
        function MostSharedApps(FromDate, ToDate) {

            var ctrr = 1;

            $.ajax({
                url: "Service.asmx/MostSharedApps?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=10" + "&UserID=" + '' + "&VendorID=" + $('#VendorID').val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {
                    var tmpJsonData = data.d;
                    var summaryCtr = 1;

                    $('#sessionSummaryTblshared').empty();

                    $('table.sessionSummaryTblshared').append("<thead><tr> <th style='display:none' width='5%' align='center' valign='middle'></th> <th>App Name</th><th>Shared</th>  <th>Last Shared (GMT)   </th>        </tr><tbody>     <tr id='1000000' class='sessionSummaryRow' style='display:none'><td></td><td></td><td></td><td></td></tr></tbody>");

                    $.each(data, function (i, jsondata) {
                        var trClassSmry = "tr3";
                        if (summaryCtr % 2 == 0)
                            trClassSmry = "tr4";
                        else
                            trClassSmry = "tr3";

                        var ctrrRow = summaryCtr + 'SharedRow';


                        var rol = 1;
                        var over = 2;
                        var onclick = "onclick =getSharedAppInteraction('" + ctrrRow + "','" + $.trim(jsondata.ImageSIFID) + "') ";

                        //  var onclick = "onclick =getUserSessions('" + ctrrRow + "','" + $.trim(jsondata.UserID) + "','" + $.trim(jsondata.Token) + "') ";
                        $('<tr onmouseover="mouseEvent(\'' + ctrrRow + '\', ' + rol + ', ' + summaryCtr + ')" onmouseout="mouseEvent(\'' + ctrrRow + '\', ' + over + ', ' + summaryCtr + ')" id="' + ctrrRow + '"  style="cursor:pointer" ><td style="display:none"><input type="checkbox" /> </td><td ' + onclick + '>' + $.trim(jsondata.AppName) + '</td><td ' + onclick + '>' + $.trim(jsondata.Seen) + '</td><td ' + onclick + '>' + $.trim(jsondata.LastSeen) + '</td></tr>').insertBefore('table.sessionSummaryTblshared tr.sessionSummaryRow');

                        summaryCtr++;

                    });

                    $('#sessionSummaryTblshared').dataTable({
                        // "bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": false,
                        "bSort": true,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "aoColumnDefs": [
      { "bSortable": false, "aTargets": [0] }
                        ]

                    });



                }
            })
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
        function getSessionList(RowID, UserID, LastSeen, flag) {

            $.ajax({
                url: "Service.asmx/GetSessionList?UserID=" + UserID + "&VendorID=" + $('#VendorID').val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {
                    var id = "getsessionid" + UserID;

                    var onChange = "onChange =getUserSessions('" + RowID + "','" + UserID + "','" + '0' + "') ";

                    $('#tblUserWiseSummary' + UserID + '_wrapper .row .col-xs-6')[0].outerHTML = '<div class="col-xs-6"><label> Activites By: <select ' + onChange + ' class="getsessionid" id=' + id + ' ></select></label></div>';

                    $.each(data, function (i, jsondata) {

                        $("#" + id).append($("<option></option>").text(jsondata.SessionList).val($.trim(jsondata.SessionList)));
                    })

                    if (LastSeen != '' && flag == "0") {

                        $("#" + id).val($.trim(LastSeen));
                    }


                }



            })


        }



    </script>
    <style>
        .dataTables_info
        {
            display: none;
        }

        .no-print
        {
            display: none;
        }

        .pace .pace-progress
        {
            background-color: yellow;
        }
    </style>
</head>
<body class="skin-blue">

    <header class="header">
        <a href="AutoURL.aspx" class="logo">
            <!-- Add the class icon to your logo image or logo icon to add the margining -->
            <img class="pull-left" id="imgLogo" alt="TheBeast Apps" src="images/thebeastapps_logo.jpg"
                style="height: 40px !important;" />
        </a>
        <!-- Header Navbar: style can be found in header.less -->
        <nav class="navbar navbar-static-top" role="navigation">
            <!-- Sidebar toggle button-->
            <a href="#" class="navbar-btn sidebar-toggle" data-toggle="offcanvas" role="button">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </a>
            <div class="navbar-right">
                <ul class="nav navbar-nav">
                    <li>
                        <asp:Label ID="lblUserName" runat="server"></asp:Label></li>
                    <li><a href="AutoURL.aspx">Home</a></li>
                    <li><a href="AuditTrail.aspx">Audit Trail</a></li>
                    <li><a href="Signout.aspx">Log Out</a></li>
                </ul>
            </div>
        </nav>

    </header>
    <form id="form1" runat="server">
        <div class="wrapper row-offcanvas row-offcanvas-left">
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="left-side sidebar-offcanvas collapse-left">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                    <ul class="sidebar-menu">
                        <li class="active">
                            <a href="AuditTrail.aspx">
                                <i class="fa fa-dashboard"></i><span>Audit Trail</span>
                            </a>
                        </li>
                        <li>
                            <a href="UserActivityNew.aspx">
                                <i class="fa fa-th"></i><span>More Search Options</span>
                            </a>
                        </li>
                       <%-- <li>
                            <a href="AuditTrailCharts.aspx">
                                <i class="fa fa-th"></i><span>Charts</span>
                            </a>
                        </li>--%>
                    </ul>
                </section>
            </aside>
            <aside class="right-side strech">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <div class="row">
                        <div class="col-md-12">
                            <table style="display: none">
                                <tr>
                                    <td style="color: #0073ea; font-weight: bold">From Date :
                                        <input type="text" name="from_date_sys" id="from_date_sys" style="width: 140px;" />
                                    </td>
                                    <td style="color: #0073ea; font-weight: bold">To Date :
                                        <input id="to_date_sys" name="" type="text" style="width: 140px;" />
                                    </td>
                                    <td style="color: #0073ea; font-weight: bold">Show Logs of :
                                <select id="selectmenu">
                                    <option selected="selected" value="0">Last...</option>
                                    <option value="1">Last 24 hr</option>
                                    <option value="2">Last Week</option>
                                    <option value="3">Last Month</option>
                                </select>
                                    </td>
                                    <td>
                                        <input id="btnSearch" type="button" value="Search" class="btn btn-primary" />
                                    </td>
                                </tr>
                            </table>
                            <h4>Audit Trail</h4>
                        </div>
                    </div>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="box">
                                <div class="box-header">
                                    <h4 class="box-title">All Users</h4>

                                </div>
                                <!-- /.box-header -->
                                <div class="box-body table-responsive" id="divRecentUser">

                                    <table id="sessionSummaryTbl" class="sessionSummaryTbl table table-bordered table-hover">
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box" style="overflow-x: hidden;">
                                        <div class="box-header">
                                            <h4 class="box-title">Most Used Apps</h4>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body table-responsive">
                                            <table id="sessionSummaryTblApps" class="sessionSummaryTblApps table table-bordered table-hover">
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box" style="overflow-x: hidden;">
                                        <div class="box-header">
                                            <h4 class="box-title">Most Shared Apps</h4>
                                        </div>
                                        <!-- /.box-header -->
                                        <div class="box-body table-responsive">
                                            <table id="sessionSummaryTblshared" class="sessionSummaryTblshared table table-bordered table-hover">
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            </aside>
            <!-- /.right-side -->
        </div>
        <asp:HiddenField ID="hdnFromDate" runat="server" />
        <asp:HiddenField ID="hdnToDate" runat="server" />
        <asp:HiddenField ID="VendorID" runat="server" />
    </form>
</body>
</html>
