var UserID_vcm_calc_cds_switch_calculator = "1";
var CustomerID_vcm_calc_cds_switch_calculator = "2";
var instanceMode_vcm_calc_cds_switch_calculator = "conn";
var instanceType_vcm_calc_cds_switch_calculator = "vcm_calc_cds_switch_calculator";

F2.Apps["vcm_calc_cds_switch_calculator"] = (function () {
    var App_Class = function (appConfig, appContent, root) {
        // constructor
        this.appConfig = appConfig;
        this.appContent = appContent;
        this.$root = $(root);
        var $tbody = $('tbody', this.$root);
        var $caption = $('caption', this.$root);

        if (appConfig.context != null) {
            UserID_vcm_calc_cds_switch_calculator = appConfig.context.UserID;
            CustomerID_vcm_calc_cds_switch_calculator = appConfig.context.CustomerID;
            instanceMode_vcm_calc_cds_switch_calculator = appConfig.context.InstanceMode;
        }

        App_Class.prototype.init = function () {
            // perform init actions
            F2.log("Init vcm_calc_cds_switch_calculator.");

            setupSignalRGeneric(instanceType_vcm_calc_cds_switch_calculator, UserID_vcm_calc_cds_switch_calculator, CustomerID_vcm_calc_cds_switch_calculator, instanceMode_vcm_calc_cds_switch_calculator);

            $('#vcm_calc_cds_switch_calculator :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) {
                try {
                    //alert("abc" + ev.date);
                    var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;
                    //$('#inputDate').datepicker('setValue', ev.date);

                    var itemType = "DDList";

                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                    if ($(this).val() != "")
                        SendToBeast(instanceType_vcm_calc_cds_switch_calculator + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                }
            });

            $('#vcm_calc_cds_switch_calculator :text').click(function () {

                if ($(this).hasClass("priceWidget")) {
                    try {
                        debugger;
                        var clsAryMed = $(this).attr("class");
                        var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_vcm_calc_cds_switch_calculator + "^" + paraValues + "^" + idValPair;

                        display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("termWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_vcm_calc_cds_switch_calculator + "^" + paraValues + "^" + idValPair;

                        display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_termWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("basisWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_vcm_calc_cds_switch_calculator + "^" + paraValues + "^" + idValPair;

                        display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).attr("title") == "datepick") {
                    debugger;
                    try {
                        //$('#vcm_calc_bondYield :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 });
                        //$(this).datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).datepicker
                        //setupSignalRGeneric(instanceType_vcm_calc_bondYield, UserID_vcm_calc_bondYield, CustomerID_vcm_calc_bondYield, instanceMode_vcm_calc_bondYield);
                        //event.returnValue = true;
                        //event.defaultPrevented = false;
                        //$(this).show();
                        //$(this).disabled = true;
                        //$("#vcm_calc_bondYield select").eleInfo.
                        //$(this).datepicker('show');
                        $(this).datepicker('show');
                        //$("vcm_calc_bondYield_300").datetimepicker({ format: 'yyyy-mm-dd hh:ii' });

                    }
                    catch (err) {
                    }
                }

            });

            $("#vcm_calc_cds_switch_calculator input[type='button']").click(function () {

                if (!$(this).hasClass("inputDisable")) {
                    try {
                        var itemType = "DDList";
                        var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;

                        var valToSubmit = $(this).attr("name");
                        if (valToSubmit == "1")
                            $(this).attr("name", "0");
                        else
                            $(this).attr("name", "1");

                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit;

                        if ($(this).val() != "") {
                            SendToBeast(instanceType_vcm_calc_cds_switch_calculator + "#" + paraValues, idValPair);
                        }
                    }
                    catch (err) {
                        var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                    }
                }
            });


            $("#vcm_calc_cds_switch_calculator select").change(function () {
                try {
                    var itemType = "DDList";
                    var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                    if ($(this).val() != "") {
                        SendToBeast(instanceType_vcm_calc_cds_switch_calculator + "#" + paraValues, idValPair);
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                }
            });

            $("#vcm_calc_cds_switch_calculator :text").bind("paste", function (e) {
                e.preventDefault();
            });

            $("#vcm_calc_cds_switch_calculator :text").bind('keydown', function (event) {
                try {
                    if ($(this).hasClass("inputDisable")) {
                        event.preventDefault ? event.preventDefault() : event.returnValue = false;
                    }
                    else {
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
                            var paraValues = UserID_vcm_calc_cds_switch_calculator + "^" + CustomerID_vcm_calc_cds_switch_calculator + "^" + instanceMode_vcm_calc_cds_switch_calculator;
                            var itemType = "DDList";
                            var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
                            //if ($.trim($(this).val()) != "") {
                            SendToBeast(instanceType_vcm_calc_cds_switch_calculator + "#" + paraValues, idValPair);
                            //}
                        }
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("vcm_calc_cds_switch_calculator_appclass.js", strerrordesc);
                }
            });
        };
    };
    return App_Class;
})();

