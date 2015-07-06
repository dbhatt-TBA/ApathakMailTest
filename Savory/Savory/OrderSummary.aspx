<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="OrderSummary.aspx.cs" Inherits="Savory.OrderSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="js/json2.js"></script>
    <script src="Scripts/jquery.signalR-1.0.0-rc2.min.js" type="text/javascript"></script>
    <script src="js/properties.js"></script>
    <script src="js/FormatScript.js"></script>
    <script type="text/javascript" src="http://localhost:4828/signalr/hubs"></script>
    <script src="js/plugins.js"></script>
    <script src="js/main.js"></script>
    <script src="js/jquery.jmsajax.min.js"></script>
    <script src="js/VolSeparate.js"></script>
    <script src="js/f2.min.js"></script>
    <script src="js/VCMCommon.js"></script>
    <div class="bs-docs-section">
        <div class="row appDiv">
            <div class="progress progress-striped active">
                <div class="progress-bar"></div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hdn_userId" runat="server" />
    <input type="hidden" id="hdn_custId" runat="server" />
    <input type="hidden" id="hdn_senderEmailId" runat="server" />
    <input type="hidden" id="hdn_instanceMode" runat="server" value="conn" />
    <input type="hidden" id="hdn_instanceType" runat="server" value="savory_order_summary" />
    <input type="hidden" id="hdn_ClientType" runat="server" />
    <input type="hidden" id="hdn_ConnectionID" runat="server" />
    <input type="hidden" id="hdn_AuthToken" runat="server" />
    <script>
        var hdnUserId;
        var hdnCustId;
        var hdnAuthToken;
        var hdnClientType;
        var hdnSenderEmailId;
        var hdnConnectionID;
        var hdnInstanceMode;
        var hdnInstanceType;

        (function () {

            hdnUserId = document.getElementById('<%= hdn_userId.ClientID %>');
            hdnCustId = document.getElementById('<%= hdn_custId.ClientID %>');
            hdnAuthToken = document.getElementById('<%= hdn_AuthToken.ClientID %>');
            hdnClientType = document.getElementById('<%= hdn_ClientType.ClientID %>');
            hdnSenderEmailId = document.getElementById('<%= hdn_senderEmailId.ClientID %>');
            hdnConnectionID = document.getElementById('<%= hdn_ConnectionID.ClientID %>');
            hdnInstanceMode = document.getElementById('<%= hdn_instanceMode.ClientID %>');
            hdnInstanceType = document.getElementById('<%= hdn_instanceType.ClientID %>');

            F2.init({
                beforeAppRender: function (app) {
                    var appRoot = '<div id="mainProductSection" class="col-md-12"></div>';
                    if ($("#mainProductSection").length == 0) {
                        $(appRoot).appendTo('div.appDiv');
                    }
                    return appRoot;
                },

                afterAppRender: function (app, html) {
                    var ele;
                    ele = $("#mainProductSection");
                    ele.append(html);
                    return ele;
                }
            });
        })();

        $(document).ready(function () {
            var UserID_tmp = $(hdnUserId).val();
            var CustomerID_tmp = $(hdnCustId).val();
            instanceMode_tmp = "conn";
            instanceType_tmp = "savory_order_summary";

            var _appConfig_Single = [
            {
                appId: "savory_order_summary",
                description: "Savory Order Summary.",
                name: "Savory Order Summaryr",
                manifestUrl: "Products/savory_order_summary/manifest.js",
                context: {
                    UserID: UserID_tmp,
                    CustomerID: CustomerID_tmp,
                    InstanceMode: instanceMode_tmp,
                    InstanceType: instanceType_tmp
                }
            }
            ];

            F2.registerApps(_appConfig_Single);
            //generateOrderMaster();
        });
    </script>
    <link href="css/mystyle.css" rel="stylesheet" />
</asp:Content>
