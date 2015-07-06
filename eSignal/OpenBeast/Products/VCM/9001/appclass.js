 var UserID_9001 = "1";
 var CustomerID_9001= "2";
 var instanceMode_9001= "conn";
 var instanceType_9001 = "9001";
 F2.Apps["9001"] = (function () { 
  var App_Class = function (appConfig, appContent, root) {
  this.appConfig = appConfig;
 this.appContent = appContent;
 this.$root = $(root);
 var $tbody = $('tbody', this.$root);
 var $caption = $('caption', this.$root);
  if (appConfig.context != null) {
     UserID_9001 = appConfig.context.UserID;
    CustomerID_9001 = appConfig.context.CustomerID;
   instanceMode_9001 = appConfig.context.InstanceMode;
  }
  App_Class.prototype.init = function () {
     // perform init actions 
    F2.log("Init 9001.");
 setupSignalRGeneric(instanceType_9001, UserID_9001, CustomerID_9001, instanceMode_9001);
 $('#9001 :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) { 
  try {
  var paraValues = UserID_9001+ "^" + CustomerID_9001 + "^" + instanceMode_9001;
  var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "")
  SendToBeast(instanceType_9001 + "#" + paraValues, idValPair);
  }
 catch (err) {
  var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog(" 9001_appclass.js", strerrordesc);
 }
 });
  $('#9001:text').click(function () {
  if ($(this).hasClass("priceWidget")) {
 try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_9001+ "^" + CustomerID_9001 + "^" + instanceMode_9001;
 var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
   var eleInfo = instanceType_9001+ "^" + paraValues + "^" + idValPair;
  display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
  }
  catch (err) {
  var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog("9001_appclass.js", strerrordesc);
  }
  }
 else if ($(this).hasClass("termWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_9001 + "^" + CustomerID_9001+ "^" + instanceMode_9001;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
  var eleInfo = instanceType_9001 + "^" + paraValues + "^" + idValPair;
 display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
   catch (err) {
  var strerrordesc = "Function:text_termWidget(); Error is :" + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("9001_appclass.js", strerrordesc);
 }
  }
 else if ($(this).hasClass("basisWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_9001 + "^" + CustomerID_9001 + "^" + instanceMode_9001;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
 var eleInfo = instanceType_9001 + "^" + paraValues + "^" + idValPair;
 display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
 catch (err) {
 var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number +"; Message :" + err.message;
 onJavascriptLog("9001_appclass.js", strerrordesc);
 }
  }
  });
  $('#9001 :text').bind("blur", function (event) {
  if ($(this).attr('id') == '9001_150' || $(this).attr('id') == '9001_152' || $(this).attr('id') == '9001_153') { 
     GEN_FUNC_RevertNameValueSwipe($(this));
   FUNC_iPreoSubmitValue($(this));
  }
  });
  function GEN_FUNC_RevertNameValueSwipe(pElement) {
    try {
    var vCurrentValue = $(pElement).attr("name");
      var vNameValue = $(pElement).val();
  $(pElement).val(vCurrentValue);
 $(pElement).attr("name", vNameValue);
  }
  catch (excp) {
  alert(excp);
  }
  }
  function GEN_FUNC_RevertNameValueSwipe(pElement) {
    try {
    var vCurrentValue = $(pElement).attr("name");
      var vNameValue = $(pElement).val();
  $(pElement).val(vCurrentValue);
 $(pElement).attr("name", vNameValue);
  }
  catch (excp) {
  alert(excp);
  }
  }
 $("#9001 input[type='button']").click(function () {
  if (!$(this).hasClass("inputDisable")) {
    try {
       var itemType = "DDList";
    var paraValues = UserID_9001 + "^" + CustomerID_9001 + "^" + instanceMode_9001;
   var valToSubmit = $(this).attr("name");
  if (valToSubmit == "1")
     $(this).attr("name", "0");
 else
     $(this).attr("name", "1");
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit; 
   SendToBeast(instanceType_9001 + "#" + paraValues, idValPair);
   }
 catch (err) {
    var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message; 
  alert(strerrordesc);
  onJavascriptLog("9001_appclass.js", strerrordesc);
  }
  }
  });
  $("#9001 select").change(function () {
 try {
 var itemType = "DDList";
  var paraValues = UserID_9001 + "^" + CustomerID_9001 + "^" + instanceMode_9001;
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "") {
 SendToBeast(instanceType_9001 + "#" + paraValues, idValPair);
 }
 }
 catch (err) {
  var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("9001_appclass.js", strerrordesc);
  }
  });
   $("#9001 :text").bind("paste", function (e) {
   if ($(this).attr('id') == '9001_150' || $(this).attr('id') == '9001_152' || $(this).attr('id') == '9001_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
   $("#9001 :text").bind("paste", function (e) {
   if ($(this).attr('id') == '9001_150' || $(this).attr('id') == '9001_152' || $(this).attr('id') == '9001_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
  $("#9001 :text").bind('keydown', function (event) {
  try {
      var keyNumber = event.keyCode;
     if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; 
   if ($(this).attr('id') == '9001_150' || $(this).attr('id') == '9001_152' || $(this).attr('id') == '9001_153') {
    }
    else {
      if (event.shiftKey) event.preventDefault ? event.preventDefault() : event.returnValue = false;
  if ((keyNumber > 47 && keyNumber < 58) || (keyNumber > 95 && keyNumber < 106) || (keyNumber > 34 && keyNumber < 41) || keyNumber == 8 || keyNumber == 13 || keyNumber == 46) {
      event.returnValue = true;
   }
  else {
      if (event.keyCode == 9) {
        event.returnValue = true;
   }
 else {
     if (event.keyCode == 110 || event.keyCode == 190) {     //Decimal (Numpad/Keyboard)
   if ($(this).val().indexOf(".") != -1)
    event.preventDefault ? event.preventDefault() : event.returnValue = false;
   }
   else
      event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  }
  }
  if (keyNumber == 13) {
 if ($(this).attr('id') == '9001_150' || $(this).attr('id') == '9001_152' || $(this).attr('id') == '9001_153') {
    GEN_FUNC_RevertNameValueSwipe($(this));
  FUNC_iPreoSubmitValue($(this));
    }
 var paraValues = UserID_9001 + "^" + CustomerID_9001 + "^" + instanceMode_9001;
  var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val(); 
  if ($.trim($(this).val()) != "") {
    SendToBeast(instanceType_9001 + "#" + paraValues, idValPair);
  } 
  event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  }
 catch (err) {
  var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
   onJavascriptLog("9001_appclass.js", strerrordesc);
  }
 });
 function FUNC_iPreoSubmitValue(pElement) {
   try {
   var paraValues = UserID_9001 + "^" + CustomerID_9001 + "^" + instanceMode_9001;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(pElement).attr("id").substring($(pElement).attr("id").lastIndexOf('_') + 1) + "#" + $(pElement).val();
  SendToBeast(instanceType_9001 + "#" + paraValues, idValPair);
   }
  catch (excp) {
   alert(excp);
  }
 }
  };
  };
 return App_Class;
 })();
 