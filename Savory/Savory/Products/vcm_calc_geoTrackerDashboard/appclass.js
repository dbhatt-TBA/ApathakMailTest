var UserID_vcm_calc_geoTrackerDashboard = "1";
var CustomerID_vcm_calc_geoTrackerDashboard = "2";
var instanceMode_vcm_calc_geoTrackerDashboard = "conn";
var instanceType_vcm_calc_geoTrackerDashboard = "vcm_calc_geoTrackerDashboard";
var mapDash;
var isDashMapCreated = false;
var locations;
var timerDash = null;
var intervalDash = 5000;
var bounds;
var interval;
var timer;
var prog = 0;
F2.Apps["vcm_calc_geoTrackerDashboard"] = (function () {
    var App_Class = function (appConfig, appContent, root) {
        // constructor
        this.appConfig = appConfig;
        this.appContent = appContent;
        this.$root = $(root); //if you're using jQuery.
        var $tbody = $('tbody', this.$root);
        var $caption = $('caption', this.$root);

        if (appConfig.context != null) {
            UserID_vcm_calc_geoTrackerDashboard = appConfig.context.UserID;
            CustomerID_vcm_calc_geoTrackerDashboard = appConfig.context.CustomerID;
            instanceMode_vcm_calc_geoTrackerDashboard = appConfig.context.InstanceMode;
        }

        App_Class.prototype.init = function () {
            // perform init actions
            setupSignalRGeneric(instanceType_vcm_calc_geoTrackerDashboard, UserID_vcm_calc_geoTrackerDashboard, CustomerID_vcm_calc_geoTrackerDashboard, instanceMode_vcm_calc_geoTrackerDashboard);

            timer = setInterval(function () {
                interval = $("#vcm_calc_geoTrackerDashboard_401").val();
                prog = prog + 20;
                if (interval != "XML") {
                    $(".progress").hide();
                    clearInterval(timer);
                }
                else {
                    $(".progress-bar").css("width", prog + "%");
                }
            }, 300);

            var options = {
                zoom: 2,
                center: new google.maps.LatLng(40.714352, -74.005973),
                mapTypeControl: false,
                navigationControlOptions: {
                    style: google.maps.NavigationControlStyle.SMALL
                },
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            mapDash = new google.maps.Map(document.getElementById("mapdashboard"), options);
            bounds = new google.maps.LatLngBounds();
            isDashMapCreated = true;
            $("#start").click(function () {
                if (timerDash !== null) return;
                timerDash = setInterval(function () {
                    locations = $("#vcm_calc_geoTrackerDashboard_401").val();
                    setMarkers(mapDash);
                }, intervalDash)
            });

            $('#vcm_calc_geoTrackerDashboard :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) {
                try {
                    //alert("abc" + ev.date);
                    var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;
                    //$('#inputDate').datepicker('setValue', ev.date);

                    var itemType = "DDList";

                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                    if ($(this).val() != "")
                        SendToBeast(instanceType_vcm_calc_geoTrackerDashboard + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                }
            });

            $('#vcm_calc_geoTrackerDashboard :text').click(function () {

                if ($(this).hasClass("priceWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_vcm_calc_geoTrackerDashboard + "^" + paraValues + "^" + idValPair;

                        display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("termWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_vcm_calc_geoTrackerDashboard + "^" + paraValues + "^" + idValPair;

                        display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_termWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("basisWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_vcm_calc_geoTrackerDashboard + "^" + paraValues + "^" + idValPair;

                        display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                    }
                }

            });

            $("#vcm_calc_geoTrackerDashboard input[type='button']").click(function () {

                if (!$(this).hasClass("inputDisable")) {
                    try {
                        var itemType = "DDList";
                        var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;

                        var valToSubmit = $(this).attr("name");
                        if (valToSubmit == "1")
                            $(this).attr("name", "0");
                        else
                            $(this).attr("name", "1");

                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit;

                        if ($(this).val() != "") {
                            SendToBeast(instanceType_vcm_calc_geoTrackerDashboard + "#" + paraValues, idValPair);
                        }
                    }
                    catch (err) {
                        var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                    }
                }

            });


            $("#vcm_calc_geoTrackerDashboard select").change(function () {
                try {
                    var itemType = "DDList";
                    var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                    if ($(this).val() != "") {
                        SendToBeast(instanceType_vcm_calc_geoTrackerDashboard + "#" + paraValues, idValPair);

                    }
                }
                catch (err) {
                    var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                }
            });

            $("#vcm_calc_geoTrackerDashboard :text").bind("paste", function (e) {
                e.preventDefault();
            });

            $("#vcm_calc_geoTrackerDashboard :text").bind('keydown', function (event) {
                try {
                    var keyNumber = event.keyCode; if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;

                    if ((keyNumber > 47 && keyNumber < 58) || (keyNumber > 95 && keyNumber < 106) || (keyNumber > 34 && keyNumber < 41) || keyNumber == 8 || keyNumber == 13 || keyNumber == 46 || keyNumber == 109 || keyNumber == 189) {
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

                        var paraValues = UserID_vcm_calc_geoTrackerDashboard + "^" + CustomerID_vcm_calc_geoTrackerDashboard + "^" + instanceMode_vcm_calc_geoTrackerDashboard;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                        if ($.trim($(this).val()) != "") {
                            SendToBeast(instanceType_vcm_calc_geoTrackerDashboard + "#" + paraValues, idValPair);
                        }
                        else {
                            alert("Input cannot be blank.");
                        }
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("vcm_calc_geoTrackerDashboard_appclass.js", strerrordesc);
                }
            });
        };
    };
    return App_Class;
})();

