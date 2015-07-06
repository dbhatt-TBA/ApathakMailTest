<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserActivityNew.aspx.cs" Inherits="UserActivityNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>TheBeastApps | Audit Trail</title>
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

    <!-- jQuery 2.0.2 -->
    <script src="js/jquery.min.js"></script>

    <!-- Bootstrap -->
    <script src="js/bootstrap.min.js" type="text/javascript"></script>
    <!-- AdminLTE App -->
    <script src="Javascript/jquery-ui.js"></script>
    <link href="Theme/jquery-ui.css" rel="stylesheet" />
    <%--    <script src="Javascript/jquery-ui-timepicker-addon.js"></script>--%>
    <script src="js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/dataTables.bootstrap.js" type="text/javascript"></script>
    <script src="js/app.js" type="text/javascript"></script>
    <script src="Javascript/jquery.multiple.select.js"></script>

    <link href="css/multiple-select.css" rel="stylesheet" />

    <script src="js/moment.js"></script>
    <script src="js/daterangepicker.js"></script>
    <link href="css/daterangepicker-bs2.css" rel="stylesheet" />

    <script type="text/javascript">



        $(document).ready(function () {
            $("#ddlActivity").multipleSelect({
                filter: true
            });
            var cb = function (start, end, label) {

                $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
            }
            var optionSet2 = {
                timePicker: true,
                timePicker12Hour: false,
                timePickerIncrement: 1,
                format: 'MMM/DD/YYYY HH:mm:ss ',
                startDate: moment().subtract('days', 7),
                endDate: moment(),
                opens: 'left',
                ranges: {
                    //'Last 5 min': [moment().subtract('time', 1), moment(), ],
                    //'Last 20 min': [moment(), moment(),],
                    //'Last 60 min': [moment(), moment()],
                    'last 24 hours': [moment().subtract('days', 1), moment()],
                    'Last 7 Days': [moment().subtract('days', 6), moment()],
                    'Last 15 Days': [moment().subtract('days', 14), moment()],
                    'Last 30 Days': [moment().subtract('days', 29), moment()]
                    //'This Month': [moment().startOf('month'), moment().endOf('month')],
                    //'Last Month': [moment().subtract('month', 1).startOf('month'), moment().subtract('month', 1).endOf('month')]
                }
            }
            $('#reportrange span').html(moment().subtract('days', 7).format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));

            $('#reportrange').daterangepicker(optionSet2, cb);


            var pageno = 1;
            $("#btnSearch").button();
            $("#selectmenu").selectmenu();


            FillUserList();
            FillAppList();
            var userid = '';
            var appid = '';
            var Activity = '';

            $('#btnSearch').click(function () {
                pageno = 1;
                userid = '';
                appid = '';
                Activity = '';

                if ($('#ddlUser').next('div').find('.ms-choice').find('span')[0].innerHTML == 'All selected') {
                    userid = '';
                }
                else {
                    $('#ddlUser').next('div').find('.ms-drop').find('ul .selected').each(function () {
                        $('#ddlUser').next('div').find('.ms-drop').find('ul .selected').parent().prepend(this);
                        userid = userid + ',' + $(this).find('label').find('input').attr('value');

                    });
                }
                $('#ddlUser').next('div').find('.ms-drop').find('ul .selected').parent().prepend($('#ddlUser').next('div').find('.ms-drop').find('ul .ms-select-all'));

                //if ($('#ddlApps').next('div').find('.ms-choice').find('span')[0].innerHTML == 'All selected') {
                //    appid = '';
                //}
                //else {
                $('#ddlApps').next('div').find('.ms-drop').find('ul .selected').each(function () {
                    $('#ddlApps').next('div').find('.ms-drop').find('ul .selected').parent().prepend(this);
                    appid = appid + ',' + $(this).find('label').find('input').attr('value');

                });
                $('#ddlApps').next('div').find('.ms-drop').find('ul .selected').parent().prepend($('#ddlApps').next('div').find('.ms-drop').find('ul .ms-select-all'));

                if (appid == '') {
                    $('#ddlApps').next('div').find('.ms-drop').find('ul li').each(function () {

                        appid = appid + ',' + $(this).find('label').find('input').attr('value');

                    });
                }
                //}
                if ($('#ddlActivity').next('div').find('.ms-choice').find('span')[0].innerHTML == 'All selected') {
                    Activity = '';
                }
                else {
                    $('#ddlActivity').next('div').find('.ms-drop').find('ul .selected').each(function () {

                        Activity = Activity + ',' + $(this).find('label').find('input').attr('value');

                    });
                }


                UserDetails($("input[name='daterangepicker_start']").val(), $("input[name='daterangepicker_end']").val(), userid, appid, pageno, Activity, "0");
            });
            $('#btnSearch').removeClass();
            $('#btnSearch').addClass('btn btn-primary');

        });
        function FillUserList() {


            $.ajax({
                url: "Service.asmx/GetuserlistbyVendorID?UserId=" + $("#userid").val(),
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    $.each(data, function (i, jsondata) {


                        $("#ddlUser").append($("<option></option>").text(jsondata.UserName + "(" + jsondata.Login_ID + ")").val(jsondata.UserID));

                    });
                    //$("#ddlUser").msDropdown({ childWidth: "250px", enableCheckbox: true });
                    $("#ddlUser").multipleSelect({
                        filter: true
                    });


                }
            })
        }
        function FillAppList() {
            var vendorid = $("#VendorID").val();


            if ($('#IsTBAAdmin').val() == '-1') {
                vendorid = '-1';
            }

            $.ajax({
                url: "Service.asmx/GetAppListbyVendorID?VendorId=" + vendorid,
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {

                    $.each(data, function (i, jsondata) {


                        $("#ddlApps").append($("<option></option>").text(jsondata.AppTitle).val(jsondata.BeastImageSID));

                    });

                    $("#ddlApps").multipleSelect({
                        filter: true
                    });


                }
            })
        }

        function UserDetails(FromDate, ToDate, UserID, appid, pageno, Activity, flag) {

            var vendorid = $('#VendorID').val();

            var ctrr = 1;

            $.ajax({
                url: "Service.asmx/UserDetails?FromDate=" + FromDate + "&ToDate=" + ToDate + "&UserID=" + UserID + "&Appid=" + appid + "&pageno=" + pageno + "&Activity=" + Activity + "&VendorID=" + vendorid,
                data: "{}",
                contentType: "application/jsonp; charset=utf-8",
                dataType: "jsonp",
                success: function (data) {
                    var tmpJsonData = data.d;
                    var summaryCtr = 1;

                    $('#sessionSummaryTbl').empty();

                    $('table.sessionSummaryTbl').append("<thead><tr><th style='display:none'> </th> <th  align='left' >Email</th><th align='center'  valign='middle'>App Name</th><th align='center'  valign='middle'>Activity </th> <th>Date Time (GMT)</th> <th>Other Detail</th>  </tr></thead>  <tbody></tbody>");

                    $.each(data, function (i, jsondata) {
                        var trClassSmry = "tr3";
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
                        else if ($.trim(jsondata.Activity) == "Connection Removed") {
                            trClassSmry = "warning";
                            Activity = "User Exited";
                        }
                        else {
                            trClassSmry = "active";
                        }

                        var rol = 1;
                        var over = 2;
                        var ctrrRow = summaryCtr + 'UserRow';
                        $('#sessionSummaryTbl tbody').append('<tr class="' + trClassSmry + '"  style="cursor:pointer"  ><td style="display:none" align="left" valign="middle" >' + $.trim(jsondata.DateTimeLocal) + '</td><td align="left" valign="middle"  >' + $.trim(jsondata.UserName) + '</td><td  align="left" valign="middle" >' + $.trim(jsondata.AppName) + '</td><td  align="left" valign="middle" >' + Activity + '</td><td align="left" valign="middle" >' + $.trim(jsondata.DateTimeLocal) + '</td><td align="left" valign="middle" >' + $.trim(jsondata.OtherDetail).replace(/,/g, '\n') + ' </td></tr>');
                        //$('<tr class="' + trClassSmry + '"  style="cursor:pointer"  ><td style="display:none" align="left" valign="middle" >' + $.trim(jsondata.DateTimeLocal) + '</td><td align="left" valign="middle"  >' + $.trim(jsondata.UserName) + '</td><td  align="left" valign="middle" >' + $.trim(jsondata.AppName) + '</td><td  align="left" valign="middle" >' + Activity + '</td><td align="left" valign="middle" >' + $.trim(jsondata.DateTimeLocal) + '</td><td align="left" valign="middle" >' + $.trim(jsondata.OtherDetail).replace(/,/g, '\n') + ' </td></tr>').insertBefore('table.sessionSummaryTbl tr.sessionSummaryRow');

                        summaryCtr++;

                    });

                    var table = $('#sessionSummaryTbl').dataTable({
                        //  "bProcessing": true,
                        "bPaginate": false,
                        "bLengthChange": false,
                        "bFilter": false,
                        "bInfo": true,
                        "bAutoWidth": false,
                        "bDestroy": true,
                        "iDisplayLength": 20


                    });


                    if ($('#sessionSummaryTbl tbody tr').length == 1) {
                        $('.dataTables_empty')[0].innerText = 'Your search did not match any activities.Please Refine your Search.';
                        $('.dataTables_empty')[0].innerHTML = 'Your search did not match any activities.Please Refine your Search.';

                    }

                    if (flag == "0" && $('#sessionSummaryTbl tbody tr').length == 1) {

                    }
                    else {
                        var html = '<div class="row"> <div class="col-xs-6">            </div>     <div class="col-xs-6">         <div class="dataTables_paginate paging_bootstrap">             <ul class="pagination">                 <li class="prev"><a href="#">Previous</a></li>                               <li class="next"><a href="#" >Next</a></li>             </ul>         </div>     </div></div>';
                        $('#sessionSummaryTbl_wrapper').append(html);

                        if ($('.wrapper #toppageing').length == 0) {
                            html = '<div id="toppageing"  class="row"> <div class="col-xs-6">            </div>     <div class="col-xs-6">         <div class="dataTables_paginate paging_bootstrap">             <ul class="pagination">                 <li class="prev"><a href="#">Previous</a></li>                               <li class="next"><a href="#">Next</a></li>             </ul>         </div>     </div></div>';

                            $('#sessionSummaryTbl').before(html);

                        }
                    }

                    $('.dataTables_paginate ul li a').on('click', function () {


                        if ($(this)[0].innerHTML == 'Next') {
                            pageno = parseInt(pageno) + 1;
                        }
                        if ($(this)[0].innerHTML == 'Previous') {
                            pageno = parseInt(pageno) - 1;
                            if (pageno == 0 || pageno < 0) {
                                pageno = 1;

                            }
                        }

                        UserDetails($("input[name='daterangepicker_start']").val(), $("input[name='daterangepicker_end']").val(), UserID, appid, pageno, Activity, "1");
                    });
                    if ($('#sessionSummaryTbl tbody tr').length == 1) {
                        $('.next').addClass('disabled');
                    }


                }
            })
        }

    </script>
    <style type="text/css">
        .dataTables_info
        {
            display: none;
        }

        .no-print
        {
            display: none;
        }

        .ddcommon
        {
            text-align: left;
        }

        .ui-timepicker-div dl
        {
            text-align: left !important;
        }

            .ui-timepicker-div dl dd
            {
                margin: 0 10px 10px 65px !important;
            }

            .ui-timepicker-div dl dt
            {
                height: 25px;
                margin-bottom: -25px;
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
            <img class="pull-left" id="img1" alt="TheBeast Apps" src="images/thebeastapps_logo.jpg"
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
                        <asp:Label ID="Label1" runat="server"></asp:Label></li>
                    <li><a href="AutoURL.aspx">Home</a></li>
                    <li><a href="AuditTrail.aspx">Audit Trail</a></li>
                    <li><a href="Signout.aspx">Log Out</a></li>
                </ul>
            </div>
        </nav>
    </header>
    <form id="form2" runat="server">
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
                    </ul>
                </section>
            </aside>
            <aside class="right-side strech">
              
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <div class="row">
                      
                        <div class="col-md-12">
                            <table >
                                <tr>
                                    
                                
                                    <td style="color: #0073ea; font-weight: bold">Users :
                                       
                                        <select style="width: 280px;" name="tech" id="ddlUser">
                        </select></td>
                                   
                                    <td style="color: #0073ea; font-weight: bold">&nbsp  Apps :
                                     <select style="width: 220px; text-align: left" name="tech" id="ddlApps">
                        </select>
                                    </td>
                                    <td style="color: #0073ea; font-weight: bold">&nbsp  Activity :
                                <select style="width: 160px" id="ddlActivity">

                            <option value="1">User Login</option>
                            <option value="3">App Created</option>
                            <option value="5">App Closed</option>
                            <option value="6">App Shared</option>
                            <option value="7">Shared App Opened</option>
                            <option value="9">User Log out</option>

                        </select>
                                    </td>
                                      <td style="color: #0073ea; font-weight: bold">&nbsp  Date Range :
                                           
                                        
                   </td>
                                   
                                    <td> <div id="reportrange" class="pull-right" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
                  <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>
                  <span></span> <b class="caret"></b> 
               </div>
</td>
                                
                                    <td>
                                        &nbsp<input id="btnSearch" type="button" value="Search" class="btn btn-primary" style="height:28px! important;padding:3px 12px" />
                                    </td>
                                </tr>
                            </table>
                          
                        </div>
                    </div>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="row">
                        <div class="col-md-12">
                         <%--   <div class="box">--%>
                               

                                <!-- /.box-header -->
                                <div class="box-body table-responsive" id="divRecentUser">
                                   <table id="sessionSummaryTbl" class="sessionSummaryTbl table table-bordered table-hover">
                                    </table>
                                </div>
                            <%--</div>--%>
                        </div>
                       
                    </div>
                </section>
                <!-- /.content -->
            </aside>
            <!-- /.right-side -->
        </div>
        <asp:HiddenField ID="hdnFromDate" runat="server" />
        <asp:HiddenField ID="hdnToDate" runat="server" />
        <asp:HiddenField ID="userid" runat="server" />
        <asp:HiddenField ID="VendorID" runat="server" />
        <asp:HiddenField ID="IsTBAAdmin" runat="server" />

    </form>
</body>
</html>

