//debugger;
var UserID_SS29001 = "1";
 var CustomerID_SS29001= "2";
 var instanceMode_SS29001= "conn";
 var instanceType_SS29001 = "SS29001";
 F2.Apps["SS29001"] = (function () { 
  var App_Class = function (appConfig, appContent, root) {
  this.appConfig = appConfig;
 this.appContent = appContent;
 this.$root = $(root);
 var $tbody = $('tbody', this.$root);
 var $caption = $('caption', this.$root);
  if (appConfig.context != null) {
     UserID_SS29001 = appConfig.context.UserID;
    CustomerID_SS29001 = appConfig.context.CustomerID;
   instanceMode_SS29001 = appConfig.context.InstanceMode;
  }
  App_Class.prototype.init = function () {
     // perform init actions 
    F2.log("Init SS29001.");
 setupSignalRGeneric(instanceType_SS29001, UserID_SS29001, CustomerID_SS29001, instanceMode_SS29001);
 $('#SS29001 :text[title="datepick"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) { 
  try {
  var paraValues = UserID_SS29001+ "^" + CustomerID_SS29001 + "^" + instanceMode_SS29001;
  var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "")
  SendToBeast(instanceType_SS29001 + "#" + paraValues, idValPair);
  }
 catch (err) {
  var strerrordesc = "Function:datepick(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog(" SS29001_appclass.js", strerrordesc);
 }
 });
 $('#SS29001:text').click(function () {
     //debugger;
  if ($(this).hasClass("priceWidget")) {
 try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_SS29001+ "^" + CustomerID_SS29001 + "^" + instanceMode_SS29001;
 var itemType = "DDList";
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
   var eleInfo = instanceType_SS29001+ "^" + paraValues + "^" + idValPair;
  display_PriceWidget(eleInfo, $(this).val(), $(this).attr("name"), clsAryMed.split(' ')[2].split('_')[1], $(this));
  }
  catch (err) {
  var strerrordesc = "Function:text_priceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
  onJavascriptLog("SS29001_appclass.js", strerrordesc);
  }
  }
 else if ($(this).hasClass("termWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_SS29001 + "^" + CustomerID_SS29001+ "^" + instanceMode_SS29001;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
  var eleInfo = instanceType_SS29001 + "^" + paraValues + "^" + idValPair;
 display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
   catch (err) {
  var strerrordesc = "Function:text_termWidget(); Error is :" + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("SS29001_appclass.js", strerrordesc);
 }
  }
 else if ($(this).hasClass("basisWidget")) {
  try {
  var clsAryMed = $(this).attr("class");
 var paraValues = UserID_SS29001 + "^" + CustomerID_SS29001 + "^" + instanceMode_SS29001;
 var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1);
 var eleInfo = instanceType_SS29001 + "^" + paraValues + "^" + idValPair;
 display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));
 }
 catch (err) {
 var strerrordesc = "Function:text_basisWidget(); Error is : " + err.description + "; Error number is " + err.number +"; Message :" + err.message;
 onJavascriptLog("SS29001_appclass.js", strerrordesc);
 }
  }
  });
 $("#SS29001 input[type='button']").click(function () {
     //debugger;
  if (!$(this).hasClass("inputDisable")) {
    try {
       var itemType = "DDList";
    var paraValues = UserID_SS29001 + "^" + CustomerID_SS29001 + "^" + instanceMode_SS29001;
   var valToSubmit = $(this).attr("name");
  if (valToSubmit == "1")
     $(this).attr("name", "0");
 else
     $(this).attr("name", "1");
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + valToSubmit; 
   SendToBeast(instanceType_SS29001 + "#" + paraValues, idValPair);
   }
 catch (err) {
    var strerrordesc = "Function:button_inputDisable(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message; 
  alert(strerrordesc);
  onJavascriptLog("SS29001_appclass.js", strerrordesc);
  }
  }
  });
  $("#SS29001 select").change(function () {
      try {
          //debugger;
 var itemType = "DDList";
  var paraValues = UserID_SS29001 + "^" + CustomerID_SS29001 + "^" + instanceMode_SS29001;
 var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val();
 if ($(this).val() != "") {
 SendToBeast(instanceType_SS29001 + "#" + paraValues, idValPair);
 }
 }
 catch (err) {
  var strerrordesc = "Function:select_DDList(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
 onJavascriptLog("SS29001_appclass.js", strerrordesc);
  }
  });
  $("#SS29001 :text").bind("paste", function (e) {
      debugger;

      //var pastedText = undefined;
      if (window.clipboardData && window.clipboardData.getData) { // IE
          pastedText = window.clipboardData.getData('Text');
      } else if (e.clipboardData && e.clipboardData.getData) {
          pastedText = e.clipboardData.getData('text/plain');
      } else if (e.originalEvent)
      {
          pastedText = e.originalEvent;
      }
      //var text = (e.originalEvent || e).clipboardData.getData('text/plain') || prompt('Paste something..');
      alert(pastedText); // Process and handle text...

   if ($(this).attr('id') == 'SS29001_150' || $(this).attr('id') == 'SS29001_152' || $(this).attr('id') == 'SS29001_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
  $("#SS29001 :text").bind("paste", function (e) {
      debugger;

   if ($(this).attr('id') == 'SS29001_150' || $(this).attr('id') == 'SS29001_152' || $(this).attr('id') == 'SS29001_153') {
   }
   else {
   event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }
  });
  $("#SS29001 :text").bind('keydown', function (event) {
      try {
          //debugger;
      var keyNumber = event.keyCode;
     if ($(this).attr("title") == "datepick") event.preventDefault ? event.preventDefault() : event.returnValue = false; 
  if (keyNumber == 13) {
 var paraValues = UserID_SS29001 + "^" + CustomerID_SS29001 + "^" + instanceMode_SS29001;
  var itemType = "DDList";
  var idValPair = itemType + "#" + $(this).attr("id").substring($(this).attr("id").lastIndexOf('_') + 1) + "#" + $(this).val(); 
  if ($.trim($(this).val()) != "") {
    SendToBeast(instanceType_SS29001 + "#" + paraValues, idValPair);
  }


  event.preventDefault ? event.preventDefault() : event.returnValue = false;
  }

  if ($(this).val().length <= 9 || event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
      if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9) {
          // let it happen, don't do anything
      } else {
          // Ensure that it is a number and stop the keypress
          //if ((event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
          //    event.preventDefault();
          //}
      }
  } else {

      event.preventDefault();
  }
  }
 catch (err) {
  var strerrordesc = "Function:text_keydown(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
   onJavascriptLog("SS29001_appclass.js", strerrordesc);
  }
 });
  };
  };
 return App_Class;
 })();
 