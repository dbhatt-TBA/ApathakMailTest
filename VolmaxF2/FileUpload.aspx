<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="FileUpload" MasterPageFile="~/Trader.master" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Javascript/jquery-1.8.3.min.js"></script>
    <script src="Javascript/jquery.dataTables.js"></script>
    <link href="css/demo_table.css" rel="stylesheet" />
    <link href="css/demo_page.css" rel="stylesheet" />

    <script type="text/javascript">


        $(document).ready(function () {
            $('.menuAuditTrail').hide();

            $('#tblUsers').dataTable({

                "bPaginate": false,
                "bDestroy": true,
                "fnDrawCallback": function (oSettings) {
                    $(".paginate_enabled_previous").bind('click', pageChanged);
                    $(".paginate_enabled_next").bind('click', pageChanged);
                }
            });
        });

        function ResetDetails() {
            $('#ctl00_ContentPlaceHolder1_StatusLabel').text("");

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
        <Triggers>
            <asp:PostBackTrigger ControlID="UploadButton" />

        </Triggers>
        <ContentTemplate>

            <table id="tblSelect" runat="server" border="0" style="height: 10px; vertical-align: middle; width: 800px"
                align="center">
                <tr>
                    <td style="border-top: 5px solid Navy; border-bottom: 2px solid Navy;">
                        <table style="width: 100%">
                            <tr>
                                <td align="left" style="width: 20%;">
                                    <%--   <img runat="server" id="imgCompanyLogo" alt="" src="" style="width: 124px; height: 81px" />--%>
                                </td>
                                <td align="right" style="width: 80%" class="AutoUrl_CompanyTitle">
                                    <asp:Label ID="lblCompanyTitle" runat="server" Text="BNP"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr id="trUserCreation" runat="server">
                    <td>Select file to upload : 
                        <asp:FileUpload ID="FileUploadControl" runat="server" ValidationGroup="valFileUpload" AllowMultiple="true" />

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUploadControl" ValidationGroup="valFileUpload"
                            ValidationExpression="^.+(.dll|.DLL)$" ErrorMessage="*Only dll file is accepted." ForeColor="Red" >
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>

                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="UploadButton" ValidationGroup="valFileUpload" Text="Upload File" OnClientClick="ResetDetails();" OnClick="UploadButton_Click" class="btn btn-info" />

                                </td>
                                <td style="padding: 20px">
                                    <asp:Label ID="StatusLabel" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <%--<td>
                                    <asp:Button ID="btnSaveList" runat="server" Text="Save" CssClass="btn btn-info" OnClick="btnSaveList_Click"></asp:Button>
                                </td>--%>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td>
                        <table style="width: 100%" align="center" class="AutoUrl_Table">
                            <tr>
                                <td class="AutoUrl_TableTitle">Uploaded DLL Files
                                </td>
                            </tr>
                            <tr valign="top">
                                <td style="vertical-align: top; text-align: left; margin: 0 fix; width: 100%;">

                                    <asp:Repeater ID="rptrUsers" runat="server">
                                        <HeaderTemplate>
                                            <table id="tblUsers" style="width: 100%">
                                                <thead>
                                                    <tr class="tblHdr">

                                                        <th style="cursor: pointer">User Id
                                                        </th>
                                                        <th style="cursor: pointer">File Name
                                                        </th>
                                                        <th style="cursor: pointer">Date Time
                                                        </th>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>

                                                <td style="margin-left: 1px;">
                                                    <%#Eval("UserID")%>
                                                </td>
                                                <td>
                                                    <%#Eval("ActualFileName")%>
                                                </td>

                                                <td>
                                                    <%#Eval("UploadedDateTime") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </tbody> </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <%--  </td>
                            </tr>
                        </table>--%>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnFile" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnUserID" runat="server" />
                                    <asp:HiddenField ID="hdnMailId" runat="server" />
                                    <asp:HiddenField ID="hdnMsgId" runat="server" />
                                    <asp:HiddenField ID="hdnMsg" runat="server" />
                                    <asp:HiddenField ID="hdnStoreUserID" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
