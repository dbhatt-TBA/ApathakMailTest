<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RestaurantMenu.aspx.cs" Inherits="Savory.RestaurantMenu" %>

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
        <div class="row">
            <div class="col-md-3">
                <ul id="myTab" class="nav nav-tabs nav-stacked" role="tablist">
                </ul>
            </div>
            <div id="myTabContent" class="tab-content col-md-9">
            </div>
        </div>
    </div>

    <input type="hidden" id="hdn_userId" runat="server" />
    <input type="hidden" id="hdn_custId" runat="server" />
    <input type="hidden" id="hdn_senderEmailId" runat="server" />
    <input type="hidden" id="hdn_instanceMode" runat="server" value="conn" />
    <input type="hidden" id="hdn_instanceType" runat="server" value="savory_menu" />
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
            instanceType_tmp = "savory_menu";

            var _appConfig_Single = [
            {
                appId: "savory_menu",
                description: "Savory Restraurant Menu.",
                name: "Restraurant Menu",
                manifestUrl: "Products/savory_menu/manifest.js",
                context: {
                    UserID: UserID_tmp,
                    CustomerID: CustomerID_tmp,
                    InstanceMode: instanceMode_tmp,
                    InstanceType: instanceType_tmp
                }
            }
            ];

            F2.registerApps(_appConfig_Single);
        });

        function getTotalPrice(objtxtItemQty) {
            var txtid = $(objtxtItemQty).attr('id');
            var itemid = txtid.substring(4, txtid.length);
            var price = $("#lbl_price_" + itemid).text();
            var qty = $(objtxtItemQty).val();
            var total_price = price * qty;
            $("#lbl_total_" + itemid).text(total_price);
        }
        function addItem(objaddItem) {
            var btnid = $(objaddItem).attr('id');
            var itemid = btnid.substring(12, btnid.length);
            var qty = $("#txt_" + itemid).val();
            if (qty <= 0 || qty == "") {
                alert("Please Specify Quantity");
                return;
            }
            var price = $("#lbl_price_" + itemid).text();
            var itemName = $("#itemNane_" + itemid).text();
            AddItemInList(itemid, itemName, qty, price);
        }
        function AddItemInList(id, name, qty, price) {
            var parameter = { "itemId": id, "itemName": name, "Qty": qty, "price": price }
            $.ajax({
                type: "POST",
                url: "temp.aspx/add",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    $("#total_cart").text("My Cart(" + data.d + ")");
                },
                error: function (error) {
                    alert("error " + error.d);
                }
            });

            //$.ajax({
            //    type: 'POST',
            //    url: 'temp.aspx/add',   
            //    data: {
            //        'itemId': id,
            //        'itemName':name,
            //        'price': price
            //    },
            //    success: function (result) {
            //    }
            //});
        }
    </script>
</asp:Content>
