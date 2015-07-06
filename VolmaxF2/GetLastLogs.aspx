<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetLastLogs.aspx.cs" Inherits="GetLastLogs" %>

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

    <script type="text/javascript">

        $(document).ready(function () {
            $('#btnSysShowMore').css({ visibility: "visible" });

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

                //var FromDate = querySt('FromDate');
                var UserID = querySt('UserID');
                var LoginID = querySt('LoginID');

                $("#hdnLoginID").val(LoginID);
                $("#hdnUserId").val(UserID);
                //var ToDate = querySt('ToDate');
                var Count = 100;
                $.ajax({
                    url: "Service.asmx/GetLastLogsOf?UserID=" + UserID,
                    data: "{}",
                    contentType: "application/jsonp; charset=utf-8",
                    dataType: "jsonp",
                    success: function (data) {

                        var sysCtr = 0;

                        document.getElementById("hdnSysCount").value = parseInt(Count) + 100;
                        var index = $('#sysLogTable tbody tr').length;
                        var html = ' <thead><tr  class="tableHeader"><th align="center" valign="middle" style="cursor:pointer">Log Time</th><th style="cursor:pointer" align="center" valign="middle">Client</th><th style="cursor:pointer" align="center" valign="middle" style="width: 5%">Log In</th><th style="cursor:pointer" align="center" valign="middle" style="width: 5%">Log Out</th> <th  style="cursor:pointer" align="center" valign="middle">IP </th><th style="cursor:pointer" align="center" valign="middle">Org</th> <th style="cursor:pointer" width="13%" align="center" valign="middle">City </th><th style="cursor:pointer" align="center" valign="middle">Country</th><th style="cursor:pointer" align="center" valign="middle" style="width: 25%">Token</th><th style="cursor:pointer" align="center" valign="middle">Details</th> </tr></thead><tbody></tbody>';
                        $('#sysLogTable').append(html);


                        if (data.length > 0) {

                            $.each(data, function (i, jsondata) {



                                var id = "div_" + index;
                                html = '<tr id="' + index + '"><td align="left" valign="left" style="width:15%" >' + $.trim(jsondata.DateTimeLocal) + '</td><td   align="center"  style="width:5%"  >' + $.trim(jsondata.Client) + '</td><td   align="left"  style="width:15%" >' + $.trim(jsondata.LogInTime) + '</td><td   align="left"   >' + $.trim(jsondata.LogOutTime) + '</td><td    valign="left"   >' + $.trim(jsondata.IP) + '</td><td    align="left"   >' + $.trim(jsondata.Org) + '</td><td    align="left"   >' + $.trim(jsondata.City) + '</td><td    align="left"   >' + $.trim(jsondata.Country) + '</td><td align="left"  >' + $.trim(jsondata.Token) + '</td align="center"><td style="width:5%"><a style="cursor:pointer;color:blue" class="' + $.trim(jsondata.Token) + '" onclick="openLogDashBoard(this)" id="' + $.trim(UserID) + '">Details</a></td></tr>';
                                $('#sysLogTable tbody').append(html);
                                //.insertBefore('table.sysLogTable tr.sysLogTrLast');

                                index = index + 1;


                                sysCtr++;

                            });
                            $('#btnSysShowMore').css({ visibility: "hidden" });



                            var html = "<label style='color:white;font-weight:bold'>" + "&nbsp &nbsp &nbsp &nbsp Login Id:&nbsp  " + LoginID + "&nbsp &nbsp &nbsp User Id: &nbsp " + UserID + "</label>";
                            $("#sysLogTable_length").append(html);
                        }
                        else {
                            $('#btnSysShowMore').css({ visibility: "hidden" });
                        }
                        $('#sysLogTable').dataTable({
                            "bJQueryUI": true,
                            "iDisplayLength": 20,
                            "bProcessing": true,

                            "sPaginationType": "two_button"

                        });
                        var html = "<label style='color:white;font-weight:bold'>" + "&nbsp &nbsp &nbsp &nbsp Login Id:&nbsp  " + LoginID + "&nbsp &nbsp &nbsp User Id: &nbsp " + UserID + "</label>";
                        $("#sysLogTable_length").append(html);
                    },
                    error: function (request, status, error) {

                        $('#btnSysShowMore').css({ visibility: "hidden" });
                        alert("No More SysLog Data Available");

                    }
                });
            }

        });
        function openLogDashBoard(id) {

            var urlToSend = "UserAllActivity.aspx?UserID=" + $(id).attr('id') + "&Token=" + $(id).attr('class').split(',')[0] + "&LoginID=" + $("#hdnLoginID").val();;
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
            height: 2px;
            line-height: 2px;
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
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                                                <tr style="width: 100%">
                                                    <td align="left" valign="top" style="max-width: 70%">
                                                        <div style="overflow: auto; height: 780px; width: 100%; overflow: scroll;">
                                                            <table id="sysLogTable" width="98%" border="0" align="left" cellpadding="1" cellspacing="1"
                                                                class="table3 sysLogTable">
                                                            </table>
                                                            <table width="100%" style="text-align: center">
                                                                <tr class="sysShowMore">
                                                                    <td colspan="5" align="center" valign="middle">
                                                                        <img id="btnSysShowMore" style="cursor: pointer; visibility: hidden" src="AuditTrailCss/images/Loader.gif" />
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
        <asp:HiddenField ID="hdnLoginID" Value="" runat="server" />
        <asp:HiddenField ID="hdnUserId" Value="" runat="server" />
    </form>
</body>
</html>
