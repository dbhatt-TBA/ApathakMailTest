 var UserID_IDV2151 = "1";
 var CustomerID_IDV2151= "2";
 var instanceMode_IDV2151= "conn";
 var instanceType_IDV2151 = "IDV2151";
 F2.Apps["IDV2151"] = (function () { 
  var App_Class = function (appConfig, appContent, root) {
  this.appConfig = appConfig;
 this.appContent = appContent;
 this.$root = $(root);
 var $tbody = $('tbody', this.$root);
 var $caption = $('caption', this.$root);
  if (appConfig.context != null) {
     UserID_IDV2151 = appConfig.context.UserID;
    CustomerID_IDV2151 = appConfig.context.CustomerID;
   instanceMode_IDV2151 = appConfig.context.InstanceMode;
  }
  App_Class.prototype.init = function () {
     // perform init actions 
    F2.log("Init IDV2151.");
 setupSignalRGeneric(instanceType_IDV2151, UserID_IDV2151, CustomerID_IDV2151, instanceMode_IDV2151);
 $('#IDV2151 :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) { 
  try {
  var paraValues = UserID_IDV2151+ "^" + CustomerID_IDV2151 + "^" + instanceMode_IDV2151;
  var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "")
  SendToBeast(instanceType_IDV2151 + "#" + paraValues, idValPair);
  }
 catch (err) {
  var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog(" IDV2151_appclass.js", strerrordesc);
 }
 });
  $('#IDV2151:text').click(function () {
  if ($(this).hasClass("priceWidget")) {
 try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_IDV2151+ "^" + CustomerID_IDV2151 + "^" + instanceMode_IDV2151;
 var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
   var eleInfo = instanceType_IDV2151+ "^" + paraValues + "^" + idValPair;
  display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
  }
  catch (err) {
  var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog("IDV2151_appclass.js", strerrordesc);
  }
  }
 else if ($(this).hasClass("termWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_IDV2151 + "^" + CustomerID_IDV2151+ "^" + instanceMode_IDV2151;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
  var eleInfo = instanceType_IDV2151 + "^" + paraValues + "^" + idValPair;
 display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
   catch (err) {
  var strerrordesc = "Function:text_termWidget(); Error is :" + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("IDV2151_appclass.js", strerrordesc);
 }
  }
 else if ($(this).hasClass("basisWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_IDV2151 + "^" + CustomerID_IDV2151 + "^" + instanceMode_IDV2151;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
 var eleInfo = instanceType_IDV2151 + "^" + paraValues + "^" + idValPair;
 display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
 catch (err) {
 var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number +"; Message :" + err.message;
 onJavascriptLog("IDV2151_appclass.js", strerrordesc);
 }
  }
  });
 $("#IDV2151 input[type='button']").click(function () {
  if (!$(this).hasClass("inputDisable")) {
    try {
       var itemType = "DDList";
    var paraValues = UserID_IDV2151 + "^" + CustomerID_IDV2151 + "^" + instanceMode_IDV2151;
   var valToSubmit = $(this).attr("name");
  if (valToSubmit == "1")
     $(this).attr("name", "0");
 else
     $(this).attr("name", "1");
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit; 
   SendToBeast(instanceType_IDV2151 + "#" + paraValues, idValPair);
   }
 catch (err) {
    var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message; 
  alert(strerrordesc);
  onJavascriptLog("IDV2151_appclass.js", strerrordesc);
  }
  }
  });
  $("#IDV2151 select").change(function () {
 try {
 var itemType = "DDList";
  var paraValues = UserID_IDV2151 + "^" + CustomerID_IDV2151 + "^" + instanceMode_IDV2151;
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "") {
 SendToBeast(instanceType_IDV2151 + "#" + paraValues, idValPair);
 }
 }
 catch (err) {
  var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("IDV2151_appclass.js", strerrordesc);
  }
  });
   $("#IDV2151 :text").bind("paste", function (e) {
   if ($(this).attr('id') == 'IDV2151_150' || $(this).attr('id') == 'IDV2151_152' || $(this).attr('id') == 'IDV2151_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
   $("#IDV2151 :text").bind("paste", function (e) {
   if ($(this).attr('id') == 'IDV2151_150' || $(this).attr('id') == 'IDV2151_152' || $(this).attr('id') == 'IDV2151_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
  $("#IDV2151 :text").bind('keydown', function (event) {
  try {
      var keyNumber = event.keyCode;
     if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; 
  if (keyNumber == 13) {
 var paraValues = UserID_IDV2151 + "^" + CustomerID_IDV2151 + "^" + instanceMode_IDV2151;
  var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val(); 
  if ($.trim($(this).val()) != "") {
    SendToBeast(instanceType_IDV2151 + "#" + paraValues, idValPair);
  } 
  event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  }
 catch (err) {
  var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
   onJavascriptLog("IDV2151_appclass.js", strerrordesc);
  }
 });
  };
  };
 return App_Class;
 })();
 