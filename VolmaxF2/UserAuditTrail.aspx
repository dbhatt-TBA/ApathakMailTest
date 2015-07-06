<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAuditTrail.aspx.cs" Inherits="UserAuditTrail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0)" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>The Beast Apps, LLC</title>
    <link rel="shortcut icon" type="image/icon" href="images/favicon.ico" />
    <link href="css/webApps.css" rel="stylesheet" type="text/css" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="Javascript/jquery-1.7.1.js"></script>
    <link href="AuditTrailCss/style.css" rel="stylesheet" type="text/css" />

    <script src="AuditTailjs/jquery.flot.js" type="text/javascript"></script>

    <script src="AuditTailjs/excanvas.compiled.js" type="text/javascript"></script>


    <script src="AuditTailjs/jsdate/js/datepicker.js"></script>


    <%--   <link href="AuditTailjs/jsdate/css/datepicker.css" rel="stylesheet" />--%>
    <script src="AuditTailjs/jquery.tooltip.min.js" type="text/javascript"></script>


    <link href="AuditTrailCss/jquery.tooltip.css" rel="stylesheet" type="text/css" />

    <script src="AuditTailjs/jquery.shorten.min.js" type="text/javascript"></script>

    <link href="AuditTrailCss/filter.css" rel="stylesheet" type="text/css" />

    <script src="AuditTailjs/jqmodal/jqModal.js" type="text/javascript"></script>

    <link href="AuditTailjs/jqmodal/jqModal.css" rel="stylesheet" type="text/css" />

    <%--    <script src="AuditTailjs/slider/js/jquery-ui-1.8.18.custom.min.js" type="text/javascript"></script>--%>
    <script src="AuditTailjs/slider/js/jquery-ui-1.10.4.custom.js"></script>
    <link href="AuditTailjs/slider/css/jquery-ui-1.10.4.custom.css" rel="stylesheet" />
    <%--   <link href="AuditTailjs/slider/css/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />--%>


    <script src="AuditTailjs/jquery-ui-timepicker-addon.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#sysLogTable').on("click", "tr", function () {

                if ($("#div_" + $(this).attr('id')).find('table').length > 0) {
                    if ($("#div_" + $(this).attr('id')).css('display') == 'inline') {
                        $("#div_" + $(this).attr('id')).css('display', 'none');
                    }
                    else {
                        $("#div_" + $(this).attr('id')).css('display', 'inline');
                    }
                }
                else {

                    var html = '';
                    //   html = '<table id="tbalimagedtl"> <tr class="tr7"><td align="center" valign="middle"><h3>User</h3></td><td align="center" valign="middle"><h3>Connection</h3></td><td align="center" valign="middle"><h3>Image</h3></td><td align="center" valign="middle"><h3>Creation Time</h3></td><td align="center" valign="middle"><h3>Closed Time</h3></td><td align="center" valign="middle"><h3>Parent Id</h3></td><td align="center" valign="middle"><h3>Value</h3></td><td align="center" valign="middle"><h3>Send Value Time</h3></td><tr><td align="right" valign="middle"   >' + $(this).find('td').eq(2).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"  >' + $(this).find('td').eq(12).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word" >' + $(this).find('td').eq(13).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word" >' + $(this).find('td').eq(15).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"  >' + $(this).find('td').eq(16).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"  >' + $(this).find('td').eq(17).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word" >' + $(this).find('td').eq(19).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"  >' + $(this).find('td').eq(20).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"   >' + $(this).find('td').eq(21).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"  >' + $(this).find('td').eq(22).text() + '</td><td align="left" valign="left" style="max-width: 15px;word-wrap: break-word"  >' + $(this).find('td').eq(23).text() + '</td></tr></table>';
                    html = '<table id="tbalimagedtl" style="width:95%"> <tr class="nestedtableHeader"><td align="center" valign="middle" >User</td><td align="center" valign="middle">User Agent</td><td align="center" valign="middle">Connection</td><td align="center" valign="middle">Image</td><td align="center" valign="middle">Creation Time</td><td align="center" valign="middle">Closed Time</td><td align="center" valign="middle">Parent Id</td><td align="center" valign="middle">Value</td><td align="center" valign="middle">Send Value Time</td><tr><td align="left" valign="left"   >' + $(this).find('td').eq(23).text() + '</td><td align="right" valign="middle"   >' + $(this).find('td').eq(11).text() + '</td><td align="left" valign="left"  >' + $(this).find('td').eq(12).text() + '</td><td align="left" valign="left">' + $(this).find('td').eq(13).text() + '</td><td align="left" valign="left"  >' + $(this).find('td').eq(16).text() + '</td><td align="left" valign="left"  >' + $(this).find('td').eq(17).text() + '</td><td align="left" valign="left"  >' + $(this).find('td').eq(20).text() + '</td><td align="left" valign="left"  >' + $(this).find('td').eq(21).text() + '</td><td align="left" valign="left" >' + $(this).find('td').eq(22).text() + '</td></tr></table>';

                    $("#div_" + $(this).attr('id')).append(html);
                    $("#div_" + $(this).attr('id')).css('display', 'inline');
                }
            });
            UserDetails();
            function querySt(ji) {

                hu = window.location.search.substring(1);
                gy = hu.split("&");
                for (i = 0; i < gy.length; i++) {
                    ft = gy[i].split("=");
                    if (ft[0] == ji) {
                        return ft[1];
                    }
                }
            }
            function UserDetails() {

                var FromDate = querySt('FromDate');
                var UserID = querySt('UserID');
                var ToDate = querySt('ToDate');
                var Count = 100;
                $.ajax({
                    url: "Service.asmx/getSysLogsByDateTime?FromDate=" + FromDate + "&ToDate=" + ToDate + "&Count=" + '300' + "&UserID=" + UserID,
                    data: "{}",
                    contentType: "application/jsonp; charset=utf-8",
                    dataType: "jsonp",
                    success: function (data) {

                        var sysCtr = 0;

                        document.getElementById("hdnSysCount").value = parseInt(Count) + 100;
                        var index = $('#sysLogTable tbody tr').length;
                        $.each(data, function (i, jsondata) {

                            var DateTimeLocal = jsondata.LogDate;
                            var LogDetails = jsondata.LogDetails;
                            //var PageName = this['PageName'];
                            //var MethodName = this['MethodName'];
                            // var ASPSessionID = this['ASPSessionID'];

                            //                        PageName = PageName.split(':')[1];
                            //                        MethodName = MethodName.split(':')[1];

                            var ServerType = jsondata.ServerType;
                            var IPAddressDestination = jsondata.IPAddressDestination;
                            var HostnameSource = jsondata.HostnameSource;


                            var aryLogDetails = LogDetails.split('weblog');


                            var LogType = aryLogDetails[0].split(' ')[2];
                            var LogDetailsParsed = aryLogDetails[1];



                            // if (sysCtr <= 10) {
                            //alert(aryLogDetails[1]);
                            $('#sysServerSource').val(HostnameSource);
                            $('#sysServerIP').val(IPAddressDestination);
                            $('#sysServerType').val(ServerType);

                            var trClass = 'oddtr';
                            if (index % 2 == 0)
                                trClass = 'eventr';

                            var id = "div_" + index;
                            var html = '<tr id="' + index + '" class="' + trClass + '"><td align="left" valign="left" style="width:15%" >' + $.trim(DateTimeLocal) + '</td><td align="right" valign="middle"   style="display: none"  >' + $.trim(jsondata.UserId) + '</td><td align="left" valign="middle" style="padding-left:8px; padding-right:7px; ">' + $.trim(LogType) + '</td><td   align="left"  style="width:25%"  >' + $.trim(jsondata.Token) + '</td><td   valign="left"   >' + $.trim(jsondata.ClientType) + '</td><td   valign="left"  style="width:15%" >' + $.trim(jsondata.LogInTime) + '</td><td   valign="left"  style="width:15%"  >' + $.trim(jsondata.LogOutTime) + '</td><td    valign="left"   >' + $.trim(jsondata.IPAddress) + '</td><td    valign="left"   >' + $.trim(jsondata.Org) + '</td><td    valign="left"   >' + $.trim(jsondata.City) + '</td><td    valign="left"   >' + $.trim(jsondata.Country) + '</td><td    valign="left" style="display: none"    >' + $.trim(jsondata.UserAgent) + '</td> + </td><td align="right" valign="middle" style="display: none" >' + $.trim(jsondata.ConnectionId) + '</td><td align="right" valign="middle" style="display: none"  >' + $.trim(jsondata.ImageName) + '</td><td align="right" valign="middle" style="display: none"  >' + $.trim(jsondata.ImageSIFId) + '</td><td align="right" valign="middle" style="display: none"  >' + $.trim(jsondata.ImageValidity) + '</td><td align="right" valign="middle"  style="display: none" >' + $.trim(jsondata.ImageCreatedTime) + '</td><td align="right" valign="middle" style="display: none"  >' + $.trim(jsondata.ImageClosedTime) + '</td><td align="right" valign="middle" style="display: none"  >' + $.trim(jsondata.LastActivityOn) + '</td><td align="right" valign="middle" style="display: none"   >' + $.trim(jsondata.SenderDetails) + '</td><td align="right" valign="middle" style="display: none"   >' + $.trim(jsondata.ParentId) + '</td><td align="right" valign="middle" style="display: none"   >' + $.trim(jsondata.Value) + '</td><td align="right" valign="middle" style="display: none"  >' + $.trim(jsondata.SendValueTime) + '</td><td align="right" valign="middle"  style="display: none" >' + $.trim(jsondata.UserID) + '</td></tr><tr id="ChildTable" style="border-color: #99ffaa;"><td colspan="100%" style="border-color: #ccc" align="center" width="90%"><div id="' + id + '" class="divclass" style="overflow: auto; display: none; position: relative; overflow: auto; width: 90%"></div></td></tr>';
                            $('#sysLogTable tbody').append(html);
                            //.insertBefore('table.sysLogTable tr.sysLogTrLast');

                            index = index + 1;


                            sysCtr++;

                        });


                    },
                    error: function (request, status, error) {

                        alert("No More SysLog Data Available");

                    }
                });
            }

        });

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
            height: 2px;
            line-height: 2px;
        }

        .oddtr
        {
            /* background-color: rgb(216, 238, 254);*/
            background: linear-gradient(to bottom, rgba(225,255,255,1) 0%,rgba(225,255,255,1) 7%,rgba(225,255,255,1) 12%,rgba(253,255,255,1) 12%,rgba(230,248,253,1) 30%,rgba(200,238,251,1) 54%,rgba(190,228,248,1) 75%,rgba(177,216,245,1) 100%);
            cursor: pointer;
        }

        .eventr
        {
            /*background-color: rgb(234, 248, 255);*/
            background: linear-gradient(to bottom, rgba(246,248,249,1) 0%,rgba(229,235,238,1) 50%,rgba(215,222,227,1) 51%,rgba(245,247,249,1) 100%);
            cursor: pointer;
        }

        .tableHeader
        {
            background-color: #B1FDF3;
            border-color: #CCCCCC;
            cursor: pointer;
            color: navy;
            border-bottom: 1px solid blue;
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
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                                                <%-- <tr class="sessionbg">
                                                    <td align="center" valign="middle">
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">

                                                            <tr align="center">
                                                                <td align="center" valign="middle" class="style2">From Date :
                                        <input type="text" name="from_date_sys" id="from_date_sys" style="padding-left: 10px; margin-right: 10px; width: 180px;" />
                                                                    To Date :
                                        <input id="to_date_sys" name="" type="text" style="padding-left: 10px; padding-right: 10px; width: 180px;" />
                                                                    <img id="btnShowSysLog" onclick="GetLogsFromDateToDate('sys', true)" style="cursor: pointer"
                                                                        src="images/show-log.png" width="88" height="20" hspace="8" border="0" align="absmiddle" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>--%>

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
                                                <tr style="width: 100%">
                                                    <td align="left" valign="top" style="max-width: 70%">
                                                        <div style="overflow: auto; height: 780px; width: 100%; overflow: scroll">
                                                            <table id="sysLogTable" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"
                                                                class="table3 sysLogTable">
                                                                <%--<tr>
                                                                    <td height="30" colspan="13" align="center" valign="middle">
                                                                        <h2>SYS Log Activities</h2>
                                                                    </td>
                                                                </tr>--%>
                                                                <thead>
                                                                    <tr class="tableHeader">
                                                                        <th align="center" valign="middle" style="width: 15%">Log Time
                                                                        </th>

                                                                        <%--<td style="max-width: 30px;" align="center" valign="middle">
                                                                        <h3>Details</h3>
                                                                    </td>
                                                                     <td width="10%" align="center" valign="middle">
                                                                        <h3>Page Name</h3>
                                                                    </td>
                                                                    <td width="12%" align="center" valign="middle">
                                                                        <h3>Method Name</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>ASP Session ID</h3>
                                                                    </td>--%>
                                                                        <%--  <td width="13%" align="center" valign="middle">
                                                                        <h3>User Id</h3>
                                                                    </td>--%>
                                                                        <th align="center" valign="middle" style="display: none">User
                                                                        </th>
                                                                        <th align="center" valign="middle">Type
                                                                        </th>
                                                                        <th align="center" valign="middle" style="width: 25%">Token
                                                                        </th>
                                                                        <th align="center" valign="middle">Client
                                                                        </th>
                                                                        <%-- <td width="13%" align="center" valign="middle">
                                                                        <h3>Last Auth Time</h3>
                                                                    </td>--%>
                                                                        <th align="center" valign="middle" style="width: 15%">Log In
                                                                        </th>
                                                                        <th width="13%" align="center" valign="middle" style="width: 15%">Log Out 
                                                                        </th>
                                                                        <%-- <td width="13%" align="center" valign="middle">
                                                                        <h3>User Last Activity</h3>
                                                                    </td>--%>
                                                                        <th align="center" valign="middle">IP
                                                                        </th>
                                                                        <th align="center" valign="middle">Org
                                                                        </th>
                                                                        <th width="13%" align="center" valign="middle">City
                                                                        </th>
                                                                        <th align="center" valign="middle">Country
                                                                        </th>
                                                                        <%-- <td width="13%" align="center" valign="middle">
                                                                        <h3>Connection Id</h3>
                                                                    </td>--%>
                                                                        <%-- <td align="center" valign="middle">
                                                                        <h3>Agent</h3>
                                                                    </td>--%>
                                                                        <%-- <td width="13%" align="center" valign="middle">
                                                                        <h3>Image Name</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Image SIF Id</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Image Validity</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Image Created Time</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Image Closed Time</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Last Activity On</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Sender Details</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Parent Id</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Value</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Send Value Time</h3>
                                                                    </td>
                                                                    <td width="13%" align="center" valign="middle">
                                                                        <h3>Others</h3>--%>
                                                                    </tr>
                                                                </thead>

                                                                <tr class="sysLogTrLast" style="display: none">
                                                                    <td colspan="11" align="center" valign="middle"></td>
                                                                </tr>
                                                            </table>
                                                            <table width="100%" style="text-align: center">
                                                                <tr class="sysShowMore">
                                                                    <td colspan="5" align="center" valign="middle">
                                                                        <img id="btnSysShowMore" style="cursor: pointer; visibility: hidden" src="images/showmoreresult.gif" />
                                                                    </td>
                                                                </tr>
                                                            </table>
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
