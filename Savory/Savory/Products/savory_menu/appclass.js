var UserID_savory_menu = "1";
var CustomerID_savory_menu = "2";
var instanceMode_savory_menu = "conn";
var instanceType_savory_menu = "savory_menu";
var interval;
var timer;
var prog = 0;
F2.Apps["savory_menu"] = (function () {
    var App_Class = function (appConfig, appContent, root) {
        // constructor
        this.appConfig = appConfig;
        this.appContent = appContent;
        this.$root = $(root); //if you're using jQuery.
        var $tbody = $('tbody', this.$root);
        var $caption = $('caption', this.$root);

        if (appConfig.context != null) {
            UserID_savory_menu = appConfig.context.UserID;
            CustomerID_savory_menu = appConfig.context.CustomerID;
            instanceMode_savory_menu = appConfig.context.InstanceMode;
        }

        App_Class.prototype.init = function () {

            setupSignalRGeneric(instanceType_savory_menu, UserID_savory_menu, CustomerID_savory_menu, instanceMode_savory_menu);

            timer = setInterval(function () {
                interval = $("#savory_menu_401").val();
                prog = prog + 20;
                if (interval != "") {
                    $(".progress").hide();
                    clearInterval(timer);

                    generateMenu();
                }
                else {
                    $(".progress-bar").css("width", prog + "%");
                }
            }, 300);
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

            function generateMenu() {
                // DOM parsing object
                var xmlString = parseXML($("#savory_menu_401").val());
                var x = xmlString.getElementsByTagName("Category");
                length = xmlString.getElementsByTagName("Category").length;
                for (var i = 0; i < length; i++) {
                    if (i == 0) {
                        var menucat = "<li class='active'><a href='#" + x[i].attributes[0].value + "' role='tab' data-toggle='tab'>" + x[i].attributes[0].value + "</a></li>";
                    }
                    else {
                        var menucat = "<li><a href='#" + x[i].attributes[0].value + "' role='tab' data-toggle='tab'>" + x[i].attributes[0].value + "</a></li>";
                    }
                    $("#myTab").append(menucat);
                    //alert(x[i].attributes[0].value);
                    totalItem = x[i].getElementsByTagName("ItemInfo").length;
                    node = x[i].getElementsByTagName("ItemInfo");
                    var item = "";
                    if (i == 0) {
                        item = "<div class='tab-pane fade active in' id='" + x[i].attributes[0].value + "'>";
                    }
                    else {
                        item = "<div class='tab-pane fade' id='" + x[i].attributes[0].value + "'>";
                    }
                    for (var j = 0; j < totalItem; j++) {
                        item = item + "<div class='row well'><div class='col-md-12'><h3 id='itemNane_" + node[j].attributes["ItemID"].value + "'>" + node[j].attributes["ItemName"].value + "</h3></div>";
                        item = item + "<div class='col-md-5'><strong>Price : $</strong><lable id='lbl_price_" + node[j].attributes["ItemID"].value + "'>" + node[j].attributes["ItemPrice"].value + "</lable></div>";
                        item = item + "<div class='col-md-4'><strong>Qty : </strong><input type='text' placeholder='0' onchange='getTotalPrice(this);' id='txt_" + node[j].attributes["ItemID"].value + "'/></div>";
                        item = item + "<div class='col-md-3'><strong>Total Price : $</strong><lable id='lbl_total_" + node[j].attributes["ItemID"].value + "'>0</lable></div>";
                        item = item + "<div class='col-md-10'><p style='text-align: justify;'>" + node[j].attributes["ItemIngred"].value + "</p></div>";
                        item = item + "<div class='col-md-2'><input type='button' id='btn_addItem_" + node[j].attributes["ItemID"].value + "' onclick='addItem(this)' class='btn btn-primary' value='Add Item'  /></div></div>";
                    }
                    item = item + "</div>";
                    $("#myTabContent").append(item);
                }

            }

            $("#savory_menu :text").bind("paste", function (e) {
                e.preventDefault();
            });

            $("#savory_menu :text").bind('keydown', function (event) {
                try {
                    var keyNumber = event.keyCode; if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;

                    if ((keyNumber > 47 && keyNumber < 58) || (keyNumber > 95 && keyNumber < 106) || (keyNumber > 34 && keyNumber < 41) || keyNumber == 8 || keyNumber == 13 || keyNumber == 46) {
                        event.returnValue = true;
                    }
                    else {
                        if (event.keyCode == 9) {
                            event.returnValue = true;
                        }
                        else {
                            if (event.keyCode == 110 || event.keyCode == 190) {
                                if ($(this).val().indexOf(".") != -1)
                                    event.preventDefault ? event.preventDefault() : event.returnValue = false;
                            }
                            else
                                event.preventDefault ? event.preventDefault() : event.returnValue = false;
                        }
                    }

                    if (keyNumber == 13) {
                        if (($.browser.mozilla == true) || ($.browser.chrome == true))
                            event.preventDefault ? event.preventDefault() : event.returnValue = false;

                        var paraValues = UserID_savory_menu + "^" + CustomerID_savory_menu + "^" + instanceMode_savory_menu;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                        if ($.trim($(this).val()) != "") {
                            SendToBeast(instanceType_savory_menu + "#" + paraValues, idValPair);
                        }
                        else {
                            alert("Input cannot be blank.");
                        }
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("savory_menu_appclass.js", strerrordesc);
                }
            });
        };
    };
    return App_Class;
})();

