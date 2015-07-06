var UserID_BYCE0252 = "1";
var CustomerID_BYCE0252 = "2";
var instanceMode_BYCE0252 = "conn";
var instanceType_BYCE0252 = "BYCE0252";
F2.Apps["BYCE0252"] = (function () {
    var App_Class = function (appConfig, appContent, root) {
        this.appConfig = appConfig;
        this.appContent = appContent;
        this.$root = $(root);
        var $tbody = $('tbody', this.$root);
        var $caption = $('caption', this.$root);
        if (appConfig.context != null) {
            UserID_BYCE0252 = appConfig.context.UserID;
            CustomerID_BYCE0252 = appConfig.context.CustomerID;
            instanceMode_BYCE0252 = appConfig.context.InstanceMode;
        }
        App_Class.prototype.init = function () {
            // perform init actions 
            F2.log("Init BYCE0252.");
            setupSignalRGeneric(instanceType_BYCE0252, UserID_BYCE0252, CustomerID_BYCE0252, instanceMode_BYCE0252);
            $('#BYCE0252 :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) {
                try {
                    var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                    var itemType = "DDList";
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
                    if ($(this).val() != "")
                        SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog(" BYCE0252_appclass.js", strerrordesc);
                }
            });
            $('#BYCE0252 :text').click(function () {
                if ($(this).hasClass("priceWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");
                        var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
                        var eleInfo = instanceType_BYCE0252 + "^" + paraValues + "^" + idValPair;
                        display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("termWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");
                        var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
                        var eleInfo = instanceType_BYCE0252 + "^" + paraValues + "^" + idValPair;
                        display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_termWidget(); Error is :" + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("basisWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");
                        var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
                        var eleInfo = instanceType_BYCE0252 + "^" + paraValues + "^" + idValPair;
                        display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
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
            $("#BYCE0252 input[type='button']").click(function () {
                
                if (!$(this).hasClass("inputDisable")) {
                    try {
                        if ($(this).attr("id") != 'BYCE0252_70') {
                            var itemType = "DDList";
                            var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                            var valToSubmit = $(this).attr("name");
                            if (valToSubmit == "1")
                                $(this).attr("name", "0");
                            else
                                $(this).attr("name", "1");
                            var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit;
                            SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                            if ($(this).attr("id") != 'BYCE0252_3004' && $(this).attr("id") != 'BYCE0252_3006' && $(this).attr("id") != 'BYCE0252_3005' && $(this).attr("id") != 'BYCE0252_4021' && $(this).attr("id") != 'BYCE0252_4022') {
                                $('#searchlst').hide();
                                $('#bondYield_Esignal').show();
                            }
                        }
                        else {                            
                            $('#searchlst').show();
                            $('#bondYield_Esignal').hide();
                        }
                    }
                    catch (err) {
                        var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        alert(strerrordesc);
                        onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                    }
                }
            });
            $("#btnsearchlst").click(function () {                
                $('#searchlst').hide();
                $('#bondYield_Esignal').show();
            });
            $("#BYCE0252 select").change(function () {
                try {
                    var itemType = "DDList";
                    var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
                    if ($(this).val() != "") {
                        SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                }
            });
            $("#BYCE0252 :text").bind("paste", function (e) {
                if ($(this).attr('id') == 'BYCE0252_150' || $(this).attr('id') == 'BYCE0252_152' || $(this).attr('id') == 'BYCE0252_153') {
                }
                else {
                    event.preventDefault ? event.preventDefault() : event.returnValue = false;
                }
            });
            $("#BYCE0252 :text").bind("paste", function (e) {
                if ($(this).attr('id') == 'BYCE0252_150' || $(this).attr('id') == 'BYCE0252_152' || $(this).attr('id') == 'BYCE0252_153') {
                }
                else {
                    event.preventDefault ? event.preventDefault() : event.returnValue = false;
                }
            });
            $("#BYCE0252 :text").bind('keydown', function (event) {
                try {
                    var keyNumber = event.keyCode;
                    if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false;
                    if (keyNumber == 13) {
                        alert("Called");
                        var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
                        if ($.trim($(this).val()) != "") {
                            SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                        }
                        event.preventDefault ? event.preventDefault() : event.returnValue = false;
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                }
            });
            

            $("#searchlst tbody tr").click(function () {
                
                if (!$(this).hasClass("inputDisable")) {
                    try {

                        var itemType = "DDList";
                        var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;

                        var valToSubmit = $(this).eq(0).find('td').find('input').attr("name");

                        if ($(this).eq(0).find('td').attr("class") != 'first1' && $(this).eq(0).find('td').attr("class") != "first" && $(this).eq(0).find('td').find('input').attr("name") != "" && $(this).eq(0).find('td').find('input').val() != "Next" && $(this).eq(0).find('td').find('input').val() != "Prev"
                            && $(this).eq(0).find('td').find('input:eq(1)').val() != "Pg Up" && $(this).eq(0).find('td').find('input:eq(1)').val() != "Pg Dn") {
                            if (valToSubmit == "1")
                                $(this).eq(0).find('td').find('input').attr("name", "0");
                            else
                                $(this).eq(0).find('td').find('input').attr("name", "1");



                            var idValPair = itemType + "#" + $(this).eq(0).find('td').find('input').attr("id").substring($(this).eq(0).find('td').find('input').attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit;

                            // if ($(this).val() != "") {
                            SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);

                            $('#searchlst').hide();
                            $('#bondYield_Esignal').show();
                            //}

                        }
                    }
                    catch (err) {
                        var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                    }
                }

            });


            $("#BYCE0252_3000").bind('blur', function (event) {
                try {
                    var keyNumber = event.keyCode; if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;

                    var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                    var itemType = "DDList";
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this)[0].name;

                    SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                }
            });
            $("#BYCE0252_3001").bind('blur', function (event) {
                try {
                    var keyNumber = event.keyCode; if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;

                    var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                    var itemType = "DDList";
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this)[0].name;

                    SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                }
            });
            //$("#BYCE0252_3002").bind('blur', function (event) {
            //    try {
            //        var keyNumber = event.keyCode; if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;

            //        var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
            //        var itemType = "DDList";
            //        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this)[0].name;

            //        SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
            //    }
            //    catch (err) {
            //        var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            //        onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
            //    }
            //});
            $("#BYCE0252_3003").bind('blur', function (event) {
                try {
                    var keyNumber = event.keyCode; if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;

                    var paraValues = UserID_BYCE0252 + "^" + CustomerID_BYCE0252 + "^" + instanceMode_BYCE0252;
                    var itemType = "DDList";
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this)[0].name;

                    SendToBeast(instanceType_BYCE0252 + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("BYCE0252_appclass.js", strerrordesc);
                }
            });

        };
    };
    return App_Class;
})();
