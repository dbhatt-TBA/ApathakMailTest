<%@ Page Title="" Language="C#" MasterPageFile="~/Trader.master" AutoEventWireup="true"
    CodeFile="Apps.aspx.cs" Inherits="Apps" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpnlAutoURL" runat="server">
        <ContentTemplate>
            <table id="tblSelect" runat="server" border="0" style="height: 10px; vertical-align: middle;
                width: 800px" align="center">
                <tr>
                    <td class="table" style="width: 100%;" colspan="3">
                        <div class="btn btn-inverse" style="width: 800px; display: inline-block;">
                            Vendor's App Details
                        </div>
                        <table id="tblsearch" class="table-bordered" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdLbl" style="text-align: center; width: 15%">
                                    Vendor:
                                </td>
                                <td valign="middle" style="vertical-align: middle; width: 85%">
                                    <asp:DropDownList ID="drpVendor" runat="server" CssClass="span2" Width="35%">
                                    </asp:DropDownList>
                                    &nbsp;
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="vertical-align: top"
                                        CssClass="btn btn-info" OnClick="btnSearch_Click"></asp:Button>
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
                            App Details
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
                                    <asp:GridView ID="GridApps" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                        AllowSorting="true" PageSize="1000" Width="100%" GridLines="Both">
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
                                            <asp:BoundField HeaderText="Vendor Name" DataField="VendorName" HeaderStyle-HorizontalAlign="Center"
                                                SortExpression="VendorName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px"
                                                ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="App Name" DataField="AppName" HeaderStyle-HorizontalAlign="Center"
                                                SortExpression="AppName" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px"
                                                ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="App Title" DataField="WebAppTitle" HeaderStyle-HorizontalAlign="Center"
                                                SortExpression="AppTitle" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px"
                                                ItemStyle-CssClass="tdLbl" />
                                            <asp:BoundField HeaderText="SIF ID" DataField="BeastImageSID" SortExpression="BeastImageSID"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="70px"
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
