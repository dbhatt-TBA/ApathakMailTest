<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="Savory.OrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="js/json2.js"></script>
    <script src="Scripts/jquery.signalR-1.0.0-rc2.min.js" type="text/javascript"></script>
    <script src="js/properties.js"></script>
    <script src="js/FormatScript.js"></script>
    <script type="text/javascript" src="http://localhost:4828/signalr/hubs"></script>
    <link href="js/vendor/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="js/vendor/bootstrap-datepicker.js" type="text/javascript"></script>
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
    <div id="myModal" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
            <div class="modal-header">
                <h3 id="myModalLabel">The Beast Apps</h3>
            </div>
            <div class="modal-body">
                <p>
                    Application got disconnected with server. Please reconnect.
                </p>
            </div>
            <div class="modal-footer">
                <button id="btnReconnect" onclick="document.location.reload(true)" class="btn btn-primary">
                    Reconnect</button>
            </div>
        </div>
    <input type="hidden" id="hdn_userId" runat="server" />
    <input type="hidden" id="hdn_custId" runat="server" />
    <input type="hidden" id="hdn_senderEmailId" runat="server" />
    <input type="hidden" id="hdn_instanceMode" runat="server" value="conn" />
    <input type="hidden" id="hdn_instanceType" runat="server" value="savory_order_master" />
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
                    var appRoot = '<div id="mainProductSection" class="row"></div>';
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
            instanceType_tmp = "savory_order_master";

            var _appConfig_Single = [
            {
                appId: "savory_order_master",
                description: "Savory Order Master.",
                name: "Savory Order Master",
                manifestUrl: "Products/savory_order_master/manifest.js",
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


        function parseXML(text) {
            var parser, xmlDoc;
            if (window.DOMParser) {
                parser = new DOMParser();
                xmlDoc = parser.parseFromString(text, "text/xml");
            } else {  // IE
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = "false";
                xmlDoc.loadXML(text);
            }
            return xmlDoc;
        }

        function generateOrderMaster(orders) {
            // DOM parsing object
            var xmlString = parseXML(orders);
            var x = xmlString.getElementsByTagName("Order");
            length = xmlString.getElementsByTagName("Order").length;

            var orderdetails = "";

            for (var i = 0; i < length; i++) {
                if (x[i].attributes[0].value != "") {
                    orderdetails += "<div class='container' style='background-color:#F0FFF0'>";
                }
                else {
                    orderdetails += "<div class='container'>";
                }
                node = x[i].getElementsByTagName("OrderDetail");
                orderdetails += "<div style='border:1px solid #dddddd; padding:5px;' class='row'><div class='col-md-2' ><strong>Order : " + node[0].attributes[0].value + "</div>";
                orderdetails += "<div class='col-md-2'>Name  : " + node[0].attributes[2].value + "</div>";
                orderdetails += "<div class='col-md-3'>Order Date : " + node[0].attributes[5].value + "</div>";
                orderdetails += "<div class='col-md-3'>Delivery Date : " + node[0].attributes[6].value + "</div>";
                orderdetails += "<div class='col-md-2'>Status : " + node[0].attributes[7].value + "</div></div>";
                orderdetails += "<table class='table table-hover'><thead><tr><th>Item</th><th>Quantity</th><th>Price</th></tr></thead><tbody>";
                itemdetails = x[i].getElementsByTagName("ItemDetail");
                items = itemdetails[0].getElementsByTagName("Item");
                for (var j = 0; j < items.length; j++) {
                    orderdetails += "<tr>"
                    orderdetails += "<td>" + items[j].attributes[0].value + "</td>";
                    orderdetails += "<td>" + items[j].attributes[1].value + "</td>";
                    orderdetails += "<td>$0</td></tr>";
                    subitems = items[j].getElementsByTagName("SubItem");

                    if (subitems.length > 0) {
                        for (var k = 0; k < subitems.length; k++) {
                            orderdetails += "<tr>"
                            orderdetails += "<td style='padding-left:40px;'>-" + subitems[k].attributes[0].value + "</td>";
                            orderdetails += "<td>" + subitems[k].attributes[1].value + "</td>";
                            orderdetails += "<td>$0</td></tr>";
                        }
                    }
                }
                orderdetails += "</tbody></table>";
                orderdetails += "</div>";
                orderdetails += "<hr/>";
            }
            $("#mainProductSection").append(orderdetails);

        }
    </script>
</asp:Content>
