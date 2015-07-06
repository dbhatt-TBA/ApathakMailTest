<%@ Page Title="" Language="C#" MasterPageFile="~/Trader.master" AutoEventWireup="true"
    ValidateRequest="false" Async="true" CodeFile="AutoURL.aspx.cs" Inherits="AutoURL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function disableDeleteBackSpace() {

            event.returnValue = false;
        }
        function SelectAll(ctrlId) {
            var bChecked = false;
            bChecked = document.getElementById(ctrlId).checked;
            var ctTextBox = document.getElementsByTagName('input');
            var cnt = ctTextBox.length;
            for (var i = 0; i <= cnt - 1; i++) {
                if (ctTextBox[i].type == 'checkbox') {
                    if (ctTextBox[i].id.indexOf('_CheckSingle') > 0) {
                        if (ctTextBox[i].disabled != true)
                            ctTextBox[i].checked = bChecked;
                    }
                }
            }
        }
        function checkValidate() {
            var checkBox = false;
            var ctTextBox = document.getElementsByTagName('input');
            var cnt = ctTextBox.length;
            for (var i = 0; i <= cnt - 1; i++) {
                if (ctTextBox[i].type == 'checkbox') {
                    if (ctTextBox[i].id.indexOf('_CheckSingle') > 0) {
                        if (ctTextBox[i].checked)
                            checkBox = true;
                    }
                }
            }
            if (checkBox == false)
                return false;
            else
                return true;
        }

        function setMessage() {
            alert("You are not authorized user.You will be redirected to Home page.");
            window.location.href = "signin.aspx";
        }
        function errorMessage() {
            alert("Please close all the browser instances and try again.");
        }

        /**
        * DHTML email validation script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
        */

        function echeck(str) {

            var at = "@"
            var dot = "."
            var lat = str.indexOf(at)
            var lstr = str.length
            var ldot = str.indexOf(dot)
            if (str.indexOf(at) == -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(at, (lat + 1)) != -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(dot, (lat + 2)) == -1) {
                alert("Invalid E-mail ID")
                return false
            }

            if (str.indexOf(" ") != -1) {
                alert("Invalid E-mail ID")
                return false
            }

            return true
        }

        function ValidateForm() {
            var emailID = document.getElementById('<%= txtEmail.ClientID %>');
            if ((emailID.value == null) || (emailID.value == "")) {
                alert("Please Enter your Email ID")
                emailID.focus()
                return false
            }
            if (echeck(emailID.value) == false) {
                emailID.value = ""
                emailID.focus()
                return false
            }
            return true
        }

        function validateEmail(field) {
            var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$/;
            return (regex.test(field)) ? true : false;
        }
        function validateMultipleEmailsCommaSeparated(emailcntl, seperator) {
            var value = emailcntl.value;
            if (value != '') {
                var result = value.split(seperator);
                for (var i = 0; i < result.length; i++) {
                    if (result[i] != '') {
                        if (!validateEmail(result[i])) {
                            emailcntl.focus();
                            alert('Please check, `' + result[i] + '` email addresses not valid!');
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //var imgCounter = 0;
        function ToggleIcapCmeView(vElemId) {
            var _elem = document.getElementById(vElemId);
            if (_elem.checked) {
                document.getElementById('<%= tblSelect.ClientID %>').className = "IcapCmeWatermark1";
                document.getElementById('<%= hdnCmeBackground.ClientID %>').value = "1";
            }
            else {
                document.getElementById('<%= tblSelect.ClientID %>').className = "";
                document.getElementById('<%= hdnCmeBackground.ClientID %>').value = "0";
            }
        }


        function GetUserNameInfo() {
            try {
                //alert('func called');
                openf2.GetUserName(CallBackSuccess, CallBackError);
            }
            catch (err) {
                var strerrordesc = "Function:GetUserNameInfo() <br>Error is : " + err.description + " <br>Error number is " + err.number + "<br>Message :" + err.message;
                //alert(strerrordesc);
            }
            //finally { setTimeout("GetUserNameInfo()", 5000); }
        }

        function CallBackSuccess(responseText) {
            try {
                //alert("callBackSuccess");
                document.getElementById("hdnPageFocusChange").value = "false";
                if (responseText.toString().length > 0) {
                    var UserDetails = responseText.toString().split('#');
                    //alert(UserDetails[0] + UserDetails[1]);
                    document.getElementById('<%= lblMess.ClientID%>').innerHTML = "You are Logged in as : " + UserDetails[0];
                    document.getElementById('<%= lblMessInfo.ClientID%>').innerHTML = "You are Logged in as : " + UserDetails[0];

                    //alert(document.getElementById('<%= hdnUserID.ClientID%>').value);
                    if (parseInt(document.getElementById('<%= hdnUserID.ClientID%>').value) != parseInt(UserDetails[1].toString())) {
                        alert('Your current session has been expired!\n Please login again to continue.');
                        window.location = "Signout.aspx"; // "Signin.aspx";                        
                    }
                }
            }
            catch (err) {
                var strerrordesc = "Function:CallBackSuccess() <br>Error is : " + err.description + " <br>Error number is " + err.number + "<br>Message :" + err.message + "<br>Parameter:" + responseText;
                //alert(strerrordesc);
            }
        }

        function CallBackError() {
            //alert('Error in CallBackError');
        }
        window.onfocus = function () {
            var isFocus = document.getElementById("hdnPageFocusChange").value;
            if (isFocus == "true")
                GetUserNameInfo();
        }

        window.onblur = function () {
            //alert('focus lost');
            document.getElementById("hdnPageFocusChange").value = "true";
        }

        function func_CMEHeilight() {
            var _elem = document.getElementById('<%= chkIsCmeEnabled.ClientID %>');
            if (_elem.checked) {
                _elem.parentNode.className = "HighlightCMEChecked";
            }
            else {
                _elem.parentNode.className = "";
            }
        }
    </script>
    <style type="text/css">
        .IcapCmeWatermark1
        {
            background-image: url(images/IcapCmeWatermark.png);
            background-repeat: repeat-y;
            background-position: center center;
        }
        .HighlightCMEChecked
        {
            background-color: #4E78A0;
            color: #FFF;
        }
    </style>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/openf2.asmx" />
        </Services>
    </asp:ScriptManagerProxy>
    <asp:UpdatePanel ID="UpnlAutoURL" runat="server">
        <ContentTemplate>
            <table id="tblSelect" runat="server" border="0" style="height: 10px; vertical-align: middle;
                width: 800px" align="center">
                <tr>
                    <td class="table" style="width: 100%;" colspan="3">
                        <div class="btn btn-inverse" style="width: 800px; display: inline-block;">
                            AUTO URL Settings
                        </div>
                        <table id="tblsearch" class="table-bordered" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdLbl" style="text-align: center">
                                    URL expire after:
                                </td>
                                <td valign="middle" style="vertical-align: middle">
                                    <asp:DropDownList ID="drpExpireHours" runat="server" CssClass="span2" Width="133px">
                                        <asp:ListItem Value="30" Text="30 minutes"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="1 hours"></asp:ListItem>
                                        <asp:ListItem Value="120" Text="2 hours"></asp:ListItem>
                                        <asp:ListItem Value="180" Text="3 hours"></asp:ListItem>
                                        <asp:ListItem Value="360" Text="6 hours"></asp:ListItem>
                                        <asp:ListItem Value="540" Text="9 hours"></asp:ListItem>
                                        <asp:ListItem Value="720" Text="12 hours"></asp:ListItem>
                                        <asp:ListItem Value="1440" Text="24 hours (1 Day)" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2880" Text="48 hours (2 days)"></asp:ListItem>
                                        <asp:ListItem Value="4320" Text="72 hours (3 days)"></asp:ListItem>
                                        <asp:ListItem Value="5760" Text="96 hours (4 days)"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="tdLbl" style="text-align: center">
                                    Select Page:
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="drpSelectPage" runat="server" CssClass="span2">
                                        <%-- <asp:ListItem Text="OpenF2-Comet" Value="Comet"></asp:ListItem>--%>
                                        <asp:ListItem Text="OpenF2-SignalR" Value="SignalR"></asp:ListItem>
                                        <%--<asp:ListItem Text="CME-ICAP" Value="IcapCme"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center">
                                    CME Rights:
                                </td>
                                <td colspan="3">
                                    <asp:CheckBox ID="chkIsCmeEnabled" runat="server" Text="" onclick="ToggleIcapCmeView(this.id);" />
                                    Assign CME Rights
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center">
                                    Comment:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtComment" runat='server' TextMode="MultiLine" Height="50px" Width="520px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <div class="btn btn-inverse" style="width: 800px; display: inline-block;">
                            User Creation and Send AUTO URL
                        </div>
                        <table width="100%" class="table table-bordered">
                            <tr>
                                <td class="tdLbl" align="right" valign="top" style="width: 39%; text-align: right;">
                                    <span style="color: Red;">*</span>&nbsp;First Name
                                </td>
                                <td class="tdLbl" align="center" valign="top" style="width: 1%;">
                                    :
                                </td>
                                <td align="left" valign="top" style="width: 60%;">
                                    <asp:TextBox ID="txtFname" runat='server' Height="15px" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvFname" runat="server" ControlToValidate="txtFname"
                                        ValidationGroup="NewUser" Display="None" ErrorMessage="Enter first name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" align="right" valign="top" style="text-align: right;">
                                    <span style="color: Red;">*</span>&nbsp;Last Name
                                </td>
                                <td class="tdLbl" align="center" valign="top">
                                    :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtLname" runat='server' Height="15px" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvLname" runat="server" ControlToValidate="txtLname"
                                        ValidationGroup="NewUser" Display="None" ErrorMessage="Enter last name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" align="right" valign="top" style="text-align: right;">
                                    <span style="color: Red;">*</span>&nbsp; Email Adrress
                                </td>
                                <td class="tdLbl" align="center" valign="top">
                                    :
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtEmail" class="input-xlarge" onblur="javascript:validateMultipleEmailsCommaSeparated(this,';');"
                                        runat="server" Height="15px" Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                        ValidationGroup="NewUser" Display="None" ErrorMessage="Enter email address" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <%--<tr>
                                <td class="tdLbl" align="right" valign="top" style="text-align: right;">
                                    CME Rights
                                </td>
                                <td class="tdLbl" align="center" valign="top">
                                    :
                                </td>
                                <td class="tdLbl" align="left" valign="top">
                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="3" align="center" valign="top" style="text-align: center;">
                                    <asp:Label ID="lblMess" runat="server" ont-Names="Verdana" Font-Size="8pt" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" valign="top" style="text-align: center;">
                                    <asp:Button ID="btnSubmitUser" runat="server" Text="Send Mail" OnClick="btnSubmitUser_Click"
                                        ValidationGroup="NewUser" CssClass="btn btn-info" ToolTip="If User does not exist then create & send email else send email." />
                                    <asp:ValidationSummary ID="vsumryNewUser" runat="server" ValidationGroup="NewUser"
                                        ShowMessageBox="true" ShowSummary="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" valign="top" style="text-align: center;">
                                    <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="blue-bold" style="font-size: 10px;" height="20px" align="center">
                        NOTE: UserId, LoginId, UserName and Mnemonic must be available for successful sending
                        of emails.
                        <div class="btn btn-inverse" style="width: 800px; font-weight: normal; display: inline-block;
                            float: left;">
                            Select User and Send AUTO URL
                        </div>
                        <%--<div style="width: 800px; font-weight: normal; display: inline-block; border: 1px solid #DDDDDD;
                            height: 25px; font-family: Verdana; font-size: 12px; float: left; padding: 2px 11px;">
                            <asp:CheckBox ID="chkCMERightsUrl" runat="server" Text="" onclick="ToggleIcapCmeView(this.id);" />
                            Assign CME Rights
                        </div>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%" align="center">
                            <tr valign="top">
                                <td style="vertical-align: top; text-align: left; margin: 0 fix; width: 100%;">
                                    <asp:GridView ID="GridAutoURL" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        AllowSorting="true" PageSize="1000" Width="100%" GridLines="Both" OnDataBound="GridAutoURL_DataBound"
                                        OnSorting="GridAutoURL_Sorting">
                                        <SelectedRowStyle BackColor="Gray" />
                                        <HeaderStyle BackColor="#f5f5f5" CssClass="table table-bordered " />
                                        <RowStyle CssClass="table table-bordered table-condensed" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Width="100%" ForeColor="Black" CssClass="gridRow gridRowHeight" />
                                        <PagerStyle Font-Names="Verdana" Font-Size="10pt" ForeColor="Black" CssClass="gridHeader"
                                            HorizontalAlign="Left" />
                                        <PagerSettings NextPageText="&amp;gt;Next" PreviousPageText="&amp;lt;Pre" />
                                        <EmptyDataTemplate>
                                            &nbsp;No records to display.
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="40px">
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="CheckAll" name="CheckAll" onclick="SelectAll(this.id)"
                                                        runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input type="checkbox" id="CheckSingle" name="CheckSingle" runat="server" />
                                                    <asp:HiddenField ID="hdf_CustomerID" runat="server" Value='<% #Eval("CustomerId")%>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="20px" ForeColor="#FFC080" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="User Id" DataField="UserID" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="User Name" DataField="UserName" SortExpression="UserName"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="220px"
                                                ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="Login Id" DataField="Login_ID" SortExpression="Login_ID"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="220px"
                                                ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="Mnemonic" DataField="Cust_Mnemonic" SortExpression="Customer_mnemonic"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px"
                                                ItemStyle-CssClass="tdLbl" />
                                        </Columns>
                                        <PagerTemplate>
                                            <table align="left">
                                                <tr>
                                                    <td align="left">
                                                        <asp:LinkButton ID="lnkPrevious" Text="&lt; Prev" CommandName="Page" CommandArgument="Prev"
                                                            ToolTip="Previous Page" runat="server" Style="font: normal 11px verdana; color: black;
                                                            text-align: left" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:Menu ID="menuPager" Orientation="Horizontal" OnMenuItemClick="menuPager_MenuItemClick"
                                                            StaticSelectedStyle-CssClass="selectedPage" CssClass="menu" Style="font: normal 11px verdana;
                                                            color: black; text-align: left" runat="server">
                                                        </asp:Menu>
                                                    </td>
                                                    <td align="left">
                                                        <asp:LinkButton ID="lnkNext" Text="Next &gt;" CommandName="Page" CommandArgument="Next"
                                                            ToolTip="Next Page" Style="font: normal 11px verdana; color: black; text-align: left"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 23px; text-align: center">
                                    <asp:UpdateProgress AssociatedUpdatePanelID="masterUpPnl" ID="updProgress" runat="server"
                                        DynamicLayout="true">
                                        <ProgressTemplate>
                                            <img alt="Please wait..." src="images/Loding.gif" align="middle" id="imgLoad" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblMessInfo" runat="server" Font-Names="Verdana" Font-Size="8pt" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <center>
                                        <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" OnClick="btnSendMail_Click"
                                            CssClass="btn btn-info"></asp:Button><br />
                                        <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="false"
                                            Font-Bold="true" Style="text-align: left" ForeColor="Red"></asp:Label>
                                    </center>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnCmeBackground" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnUserID" runat="server" />
                        <input type="hidden" id="hdnPageFocusChange" value="true" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
