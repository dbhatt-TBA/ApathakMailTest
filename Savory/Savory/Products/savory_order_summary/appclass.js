var UserID_savory_order_summary = "1";
var CustomerID_savory_order_summary = "2";
var instanceMode_savory_order_summary = "conn";
var instanceType_savory_order_summary = "savory_order_summary";
var interval;
var timer;
var prog = 0;
F2.Apps["savory_order_summary"] = (function () {
    var App_Class = function (appConfig, appContent, root) {
        // constructor
        this.appConfig = appConfig;
        this.appContent = appContent;
        this.$root = $(root); //if you're using jQuery.
        var $tbody = $('tbody', this.$root);
        var $caption = $('caption', this.$root);

        if (appConfig.context != null) {
            UserID_savory_order_summary = appConfig.context.UserID;
            CustomerID_savory_order_summary = appConfig.context.CustomerID;
            instanceMode_savory_order_summary = appConfig.context.InstanceMode;
        }

        App_Class.prototype.init = function () {

            setupSignalRGeneric(instanceType_savory_order_summary, UserID_savory_order_summary, CustomerID_savory_order_summary, instanceMode_savory_order_summary);

            timer = setInterval(function () {
                prog = prog + 20;
                interval = $("#savory_order_summary_226").val();
                if (interval != "") {
                    $(".progress").hide();
                    clearInterval(timer);
                }
                else {
                    $(".progress-bar").css("width",prog+"%");
                }
            }, 300);


            $("#savory_order_summary :text").bind("paste", function (e) {
                e.preventDefault();
            });

            $("#savory_order_summary :text").bind('keydown', function (event) {
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

                        var paraValues = UserID_savory_order_summary + "^" + CustomerID_savory_order_summary + "^" + instanceMode_savory_order_summary;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                        if ($.trim($(this).val()) != "") {
                            SendToBeast(instanceType_savory_order_summary + "#" + paraValues, idValPair);
                        }
                        else {
                            alert("Input cannot be blank.");
                        }
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("savory_order_summary_appclass.js", strerrordesc);
                }
            });
        };
    };
    return App_Class;
})();

