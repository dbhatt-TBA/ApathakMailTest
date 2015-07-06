 var UserID_GD8839 = "1";
 var CustomerID_GD8839= "2";
 var instanceMode_GD8839= "conn";
 var instanceType_GD8839 = "GD8839";
 F2.Apps["GD8839"] = (function () { 
  var App_Class = function (appConfig, appContent, root) {
  this.appConfig = appConfig;
 this.appContent = appContent;
 this.$root = $(root);
 var $tbody = $('tbody', this.$root);
 var $caption = $('caption', this.$root);
  if (appConfig.context != null) {
     UserID_GD8839 = appConfig.context.UserID;
    CustomerID_GD8839 = appConfig.context.CustomerID;
   instanceMode_GD8839 = appConfig.context.InstanceMode;
  }
  App_Class.prototype.init = function () {
     // perform init actions 
    F2.log("Init GD8839.");
 setupSignalRGeneric(instanceType_GD8839, UserID_GD8839, CustomerID_GD8839, instanceMode_GD8839);
 $('#GD8839 :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) { 
  try {
  var paraValues = UserID_GD8839+ "^" + CustomerID_GD8839 + "^" + instanceMode_GD8839;
  var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "")
  SendToBeast(instanceType_GD8839 + "#" + paraValues, idValPair);
  }
 catch (err) {
  var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog(" GD8839_appclass.js", strerrordesc);
 }
 });
  $('#GD8839:text').click(function () {
  if ($(this).hasClass("priceWidget")) {
 try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_GD8839+ "^" + CustomerID_GD8839 + "^" + instanceMode_GD8839;
 var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
   var eleInfo = instanceType_GD8839+ "^" + paraValues + "^" + idValPair;
  display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
  }
  catch (err) {
  var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog("GD8839_appclass.js", strerrordesc);
  }
  }
 else if ($(this).hasClass("termWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_GD8839 + "^" + CustomerID_GD8839+ "^" + instanceMode_GD8839;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
  var eleInfo = instanceType_GD8839 + "^" + paraValues + "^" + idValPair;
 display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
   catch (err) {
  var strerrordesc = "Function:text_termWidget(); Error is :" + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("GD8839_appclass.js", strerrordesc);
 }
  }
 else if ($(this).hasClass("basisWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_GD8839 + "^" + CustomerID_GD8839 + "^" + instanceMode_GD8839;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
 var eleInfo = instanceType_GD8839 + "^" + paraValues + "^" + idValPair;
 display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
 catch (err) {
 var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number +"; Message :" + err.message;
 onJavascriptLog("GD8839_appclass.js", strerrordesc);
 }
  }
  });
 $("#GD8839 input[type='button']").click(function () {
  if (!$(this).hasClass("inputDisable")) {
    try {
       var itemType = "DDList";
    var paraValues = UserID_GD8839 + "^" + CustomerID_GD8839 + "^" + instanceMode_GD8839;
   var valToSubmit = $(this).attr("name");
  if (valToSubmit == "1")
     $(this).attr("name", "0");
 else
     $(this).attr("name", "1");
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit; 
   SendToBeast(instanceType_GD8839 + "#" + paraValues, idValPair);
   }
 catch (err) {
    var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message; 
  alert(strerrordesc);
  onJavascriptLog("GD8839_appclass.js", strerrordesc);
  }
  }
  });
  $("#GD8839 select").change(function () {
 try {
 var itemType = "DDList";
  var paraValues = UserID_GD8839 + "^" + CustomerID_GD8839 + "^" + instanceMode_GD8839;
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "") {
 SendToBeast(instanceType_GD8839 + "#" + paraValues, idValPair);
 }
 }
 catch (err) {
  var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("GD8839_appclass.js", strerrordesc);
  }
  });
   $("#GD8839 :text").bind("paste", function (e) {
   if ($(this).attr('id') == 'GD8839_150' || $(this).attr('id') == 'GD8839_152' || $(this).attr('id') == 'GD8839_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
   $("#GD8839 :text").bind("paste", function (e) {
   if ($(this).attr('id') == 'GD8839_150' || $(this).attr('id') == 'GD8839_152' || $(this).attr('id') == 'GD8839_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
  $("#GD8839 :text").bind('keydown', function (event) {
  try {
      var keyNumber = event.keyCode;
     if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; 
  if (keyNumber == 13) {
 var paraValues = UserID_GD8839 + "^" + CustomerID_GD8839 + "^" + instanceMode_GD8839;
  var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val(); 
  if ($.trim($(this).val()) != "") {
    SendToBeast(instanceType_GD8839 + "#" + paraValues, idValPair);
  } 
  event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  }
 catch (err) {
  var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
   onJavascriptLog("GD8839_appclass.js", strerrordesc);
  }
 });
  };
  };
 return App_Class;
 })();
 