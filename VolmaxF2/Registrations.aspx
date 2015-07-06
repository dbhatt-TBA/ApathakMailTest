<%@ Page Title="" Language="C#" MasterPageFile="~/Trader.master" AutoEventWireup="true"
    CodeFile="Registrations.aspx.cs" Inherits="Registrations" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=chkTermsConditions.ClientID %>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
    
    <asp:UpdatePanel ID="UpnlAutoURL" runat="server">
        <ContentTemplate>
            <table id="tblSelect" runat="server" border="0" style="height: 10px; vertical-align: middle;
                width: 800px" align="center">
                <tr>
                    <td class="table" style="width: 100%;" colspan="3">
                        <div class="btn btn-inverse" style="width: 800px; display: inline-block;">
                            App Registrations
                        </div>
                        <table id="tblsearch" class="table-bordered" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    <span style="color: Red">* </span>App Name:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtName" runat="server" Width="80%" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                        ValidationGroup="Register" Display="None" ErrorMessage="Enter app name" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:HiddenField runat="server" ID="hdnEditRegID" />
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    <span style="color: Red">* </span>SIF ID:
                                </td>
                                <td align="center" style="width: 35%">
                                <asp:UpdatePanel runat="server" ID="upnlSIFID">
                                <ContentTemplate>
                                <asp:DropDownList ID="drpSIFID" runat="server" CssClass="span2" Width="85%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvSIFID" runat="server" ErrorMessage="Select SIF id"
                                        ControlToValidate="drpSIFID" ValidationGroup="Register" Display="None" InitialValue="0"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:Label runat="server" ID="lblSIFID" Visible=false></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    <span style="color: Red">* </span>Title:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtTitle" runat="server" Width="80%" MaxLength="200"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle"
                                        ValidationGroup="Register" Display="None" ErrorMessage="Enter title" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    <span style="color: Red">* </span>Version:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtVersion" runat="server" Width="80%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVersion" runat="server" ControlToValidate="txtVersion"
                                        ValidationGroup="Register" Display="None" ErrorMessage="Enter version" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center">
                                    Description:
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtDescription" runat='server' TextMode="MultiLine" Height="50px" MaxLength="4000"
                                        Width="520px">TheBEAST® technology based framework is an application distribution architecture. It is real time, reliable, robust and scalable architecture that allows one to build and deploy financial applications rapidly, to its internal users and customers worldwide within days. These financial applications can be high performance, real time streaming and interactive applications allowing one to see market data, do complex analytics, trade, do straight through processing, perform portfolio analysis and risk management functions etc.
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    <span style="color: Red">* </span>Category:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:DropDownList ID="drpCategory" runat="server" CssClass="span2" Width="85%">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="Select category"
                                        ControlToValidate="drpCategory" ValidationGroup="Register" Display="None" InitialValue="0"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Support OS:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtSupprotOS" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    File Size:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtFileSize" runat="server" Width="80%" MaxLength="20"></asp:TextBox>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Price:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtPrice" runat="server" Width="80%" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Tag:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtTag" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Support Language:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtSupprotLanguage" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Email:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtEmail" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="expEmail" runat="server" ControlToValidate="txtEmail"
                                        Display="None" ValidationGroup="Register" SetFocusOnError="true" ErrorMessage="Enter valid email address"
                                        ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$">
                                    </asp:RegularExpressionValidator>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Contact No.:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtContactNumber" runat="server" Width="80%" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Vendor:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:DropDownList ID="drpVendor" runat="server" CssClass="span2" Width="85%">
                                    </asp:DropDownList>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Vendor Information:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtVendorInfo" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Realease Date:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtRealeaseDate" runat="server" Width="60%" MaxLength="10"></asp:TextBox>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/images/cal.jpg" Height="30px" Width="30px"  CssClass="icon-calendar" style="vertical-align:top" />
                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtRealeaseDate" PopupButtonID="imgbtnCalendar"
                                        runat="server" Format="MM/dd/yyyy"/>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Age Criteria:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtAgeCriteria" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Support Device:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtSupportDevice" runat="server" Width="80%" MaxLength="500"></asp:TextBox>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Currency:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtCurrency" runat="server" Width="80%" MaxLength="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Package URL:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtPackageURL" runat="server" Width="80%" MaxLength="750"></asp:TextBox>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Supprot URL:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtSupprotURL" runat="server" Width="80%" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Youtube URL:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:TextBox ID="txtYoutubeURL" runat="server" Width="80%" MaxLength="200"></asp:TextBox>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    File Path:
                                </td>
                                <td align="center" style="width: 35%">
                                    <asp:TextBox ID="txtFilePath" runat="server" Width="80%" MaxLength="150"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    App Shareable:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 35%">
                                    <asp:DropDownList ID="drfIsShareable" runat="server">
                                        <asp:ListItem Text="Yes" Value="True"></asp:ListItem>
                                        <asp:ListItem Text="No" Value="False"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Shareable Minitues:
                                </td>
                                <td align="center" style="width: 35%">
                                     <asp:DropDownList ID="drfShareableMin" runat="server">
                                     <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="15 Minitues" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="30 Minitues" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="45 Minitues" Value="45"></asp:ListItem>
                                        <asp:ListItem Text="1 Hour" Value="60"></asp:ListItem>
                                        <asp:ListItem Text="2 Hours" Value="120"></asp:ListItem>
                                        <asp:ListItem Text="5 Hours" Value="300"></asp:ListItem>
                                        <asp:ListItem Text="12 Hours" Value="720"></asp:ListItem>
                                        <asp:ListItem Text="1 Day" Value="1440"></asp:ListItem>
                                        <asp:ListItem Text="2 Days" Value="2880"></asp:ListItem>
                                        <asp:ListItem Text="4 Days" Value="5760"></asp:ListItem>
                                        <asp:ListItem Text="1 Week" Value="10080"></asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfvShareableMin" runat="server" ErrorMessage="Select shareable minitues"
                                        ControlToValidate="drfShareableMin" ValidationGroup="Register" Display="None" InitialValue="0"
                                        SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:Label runat="server" ID="lblShareableMin" Visible=false></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="padding-left: 20px">
                                    <asp:CheckBox ID="chkTermsConditions" runat="server" Style="vertical-align: super" />
                                    I agree for &nbsp;<a href="TermsandConditions.aspx" target="_blank" style="vertical-align: top"
                                        title="View Terms and Conditions">Terms and condition</a>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="None" ErrorMessage="Select terms and condition"
                                        ValidationGroup="Register" ClientValidationFunction="ValidateCheckBox" SetFocusOnError="true"></asp:CustomValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <center>
                                        <asp:Button ID="btnRegister" runat="server" Text="Register" ValidationGroup="Register"
                                            CssClass="btn btn-info" OnClick="btnRegister_Click"></asp:Button><br />
                                        <asp:ValidationSummary ID="vsumryNewUser" runat="server" ValidationGroup="Register"
                                            ShowMessageBox="true" ShowSummary="false" />
                                        <%-- <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="8pt" Visible="false"
                                            Font-Bold="true" Style="text-align: left" ForeColor="Red"></asp:Label>--%>
                                    </center>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" valign="top" style="text-align: center;">
                                    <asp:Label ID="lblErrMsg" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td class="blue-bold" style="font-size: 10px;" height="10px" align="center">
                        <%--NOTE: UserId, LoginId, UserName and Mnemonic must be available for successful sending
                        of emails.--%>
                        <div class="btn btn-inverse" style="width: 800px; font-weight: normal; display: inline-block;
                            float: left;">
                            Register App Details
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
                                    <asp:GridView ID="GridRegister" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        AllowSorting="true" PageSize="1000" Width="100%" GridLines="Both" OnRowCommand="GridRegister_RowCommand" OnRowEditing="GridRegister_RowEditing" >
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
                                                    
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="btnEdit" ImageUrl="~/images/edit.jpg"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Height="20px" Width="20px" CommandName="Edit" ToolTip="Edit" />
                                                    <asp:HiddenField ID="hdf_RegID" runat="server" Value='<% #Eval("RegId")%>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="20px" ForeColor="#FFC080" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="App Name" DataField="AppName" HeaderStyle-HorizontalAlign="Center" SortExpression="AppName"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px" ItemStyle-CssClass="tdLbl" />
                                                <asp:BoundField HeaderText="App Title" DataField="AppTitle" HeaderStyle-HorizontalAlign="Center" SortExpression="AppTitle"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px" ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="SIF ID" DataField="BeastImageSID" SortExpression="BeastImageSID"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px"
                                                ItemStyle-CssClass="tdLbl" />
                                                 <asp:BoundField HeaderText="Category" DataField="CategoryName" SortExpression="CategoryName"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px"
                                                ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="Record Last Action" DataField="Record_Last_Action" SortExpression="Record_Last_Action"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px"
                                                ItemStyle-CssClass="tdLbl" />
                                           
                                        </Columns>
                                        <PagerTemplate>
                                           <%-- <table align="left">
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
                                            </table>--%>
                                        </PagerTemplate>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 23px; text-align: center">
                                    <asp:UpdateProgress AssociatedUpdatePanelID="masterUpPnl" ID="UpdateProgress1" runat="server"
                                        DynamicLayout="true">
                                        <ProgressTemplate>
                                            <img alt="Please wait..." src="images/Loding.gif" align="middle" id="imgLoad" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="8pt" Font-Bold="true"
                                        ForeColor="Red"></asp:Label>
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
