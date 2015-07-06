var UserID_savory_geoTrack = "1";
var CustomerID_savory_geoTrack = "2";
var instanceMode_savory_geoTrack = "conn";
var instanceType_savory_geoTrack = "savory_geoTrack";
var Isfirsttime_savory_geoTrack = true;
var interval = 5000;
var timer = null;
var map;
F2.Apps["savory_geoTrack"] = (function () {
    var App_Class = function (appConfig, appContent, root) {
        // constructor
        this.appConfig = appConfig;
        this.appContent = appContent;
        this.$root = $(root); //if you're using jQuery.
        var $tbody = $('tbody', this.$root);
        var $caption = $('caption', this.$root);

        if (appConfig.context != null) {
            UserID_savory_geoTrack = appConfig.context.UserID;
            CustomerID_savory_geoTrack = appConfig.context.CustomerID;
            instanceMode_savory_geoTrack = appConfig.context.InstanceMode;
        }

        App_Class.prototype.init = function () {
            // perform init actions
            F2.log("Init Bond Yield.");

            setupSignalRGeneric(instanceType_savory_geoTrack, UserID_savory_geoTrack, CustomerID_savory_geoTrack, instanceMode_savory_geoTrack);
            var options = {
                zoom: 4,
                center: new google.maps.LatLng(40.714352, -74.005973),
                mapTypeControl: false,
                navigationControlOptions: {
                    style: google.maps.NavigationControlStyle.SMALL
                },
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            map = new google.maps.Map(document.getElementById("mapcontainer"), options);
            isMapCreated = true;
            $("#start").click(function () {
                if (timer !== null) return;
                timer = setInterval(function () {
                    interval = $('#savory_geoTrack_108').val();
                    if (interval != "") {
                        interval = interval * 1000;
                        if (navigator.geolocation) {
                            geoLoc = navigator.geolocation.getCurrentPosition(showLocation);
                        } else {
                            alert("Sorry, browser does not support geolocation!");
                        }
                        function showLocation(position) {

                            var latitude = position.coords.latitude;
                            var longitude = position.coords.longitude;
                            try {
                                var itemType = "DDList";
                                var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                                var idValPair = itemType + "#" + "100" + "#" + UserID_savory_geoTrack + "-" + latitude + "-" + longitude;
                                SendToBeast(instanceType_savory_geoTrack + "#" + paraValues, idValPair);
                                success(latitude, longitude);
                            }
                            catch (err) {

                            }
                        }
                        function errorHandler(err) {
                            if (err.code == 1) {
                                alert("Error: Access is denied!");
                            } else if (err.code == 2) {
                                alert("Error: Position is unavailable!");
                            }
                        }
                    }
                }, interval);
            });

            $("#stop").click(function () {
                clearInterval(timer);
                timer = null
            });
            $('#savory_geoTrack :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) {
                try {
                    //alert("abc" + ev.date);
                    var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                    //$('#inputDate').datepicker('setValue', ev.date);

                    var itemType = "DDList";

                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                    if ($(this).val() != "")
                        SendToBeast(instanceType_savory_geoTrack + "#" + paraValues, idValPair);
                }
                catch (err) {
                    var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                }
            });

            $('#savory_geoTrack :text').click(function () {

                if ($(this).hasClass("priceWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_savory_geoTrack + "^" + paraValues + "^" + idValPair;

                        display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("termWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_savory_geoTrack + "^" + paraValues + "^" + idValPair;

                        display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_termWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                    }
                }
                else if ($(this).hasClass("basisWidget")) {
                    try {
                        var clsAryMed = $(this).attr("class");

                        var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);

                        var eleInfo = instanceType_savory_geoTrack + "^" + paraValues + "^" + idValPair;

                        display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
                    }
                    catch (err) {
                        var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                    }
                }

            });

            $("#savory_geoTrack input[type='button']").click(function () {

                if (!$(this).hasClass("inputDisable")) {
                    try {
                        var itemType = "DDList";
                        var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;

                        var valToSubmit = $(this).attr("name");
                        if (valToSubmit == "1")
                            $(this).attr("name", "0");
                        else
                            $(this).attr("name", "1");

                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit;

                        if ($(this).val() != "") {
                            SendToBeast(instanceType_savory_geoTrack + "#" + paraValues, idValPair);
                        }
                    }
                    catch (err) {
                        var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                        onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                    }
                }

            });

            $("#savory_geoTrack .imgCalculatorStatus").on('load', function () {
                if (Isfirsttime_savory_geoTrack == true && $("#savory_geoTrack .imgCalculatorStatus").attr('src') == "images/green.png") {
                    Isfirsttime_savory_geoTrack = false;
                    var itemType = "DDList";
                    var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                    var idValPair = itemType + "#" + "1" + "#" + UserID_savory_geoTrack;
                    SendToBeast(instanceType_savory_geoTrack + "#" + paraValues, idValPair);

                }
            });

            $("#savory_geoTrack select").change(function () {
                try {
                    var itemType = "DDList";
                    var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                    var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                    if ($(this).val() != "") {
                        SendToBeast(instanceType_savory_geoTrack + "#" + paraValues, idValPair);

                    }
                }
                catch (err) {
                    var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                }
            });

            $("#savory_geoTrack :text").bind("paste", function (e) {
                e.preventDefault();
            });

            $("#savory_geoTrack :text").bind('keydown', function (event) {
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

                        var paraValues = UserID_savory_geoTrack + "^" + CustomerID_savory_geoTrack + "^" + instanceMode_savory_geoTrack;
                        var itemType = "DDList";
                        var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();

                        if ($.trim($(this).val()) != "") {
                            SendToBeast(instanceType_savory_geoTrack + "#" + paraValues, idValPair);
                        }
                        else {
                            alert("Input cannot be blank.");
                        }
                    }
                }
                catch (err) {
                    var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
                    onJavascriptLog("savory_geoTrack_appclass.js", strerrordesc);
                }
            });
        };
    };
    return App_Class;
})();

