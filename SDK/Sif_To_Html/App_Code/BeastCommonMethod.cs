using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SIF_XML_ToHTMLUtility
{
    class BeastCommonMethod
    {
        public void WriteHTMLFile(string strFILENAME, FileInfo f0, StringWriter swSTRINGWRITER, int iHTMLSPANWIDTH, int iMAXCOL)
        {
            try
            {
                if (f0.Exists)
                {
                    f0.Delete();
                }
                FileStream writingFile = f0.Create();

                using (StreamWriter sw = new StreamWriter(writingFile))
                {
                    string strOuterHtml = getHTML(swSTRINGWRITER);
                    strOuterHtml = strOuterHtml.Replace("[COLSPAN]", "colspan=" + '"' + iMAXCOL + '"' + "");
                    sw.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n" +
                       " <html xmlns=\"http://www.w3.org/1999/xhtml\">\n" +
                    "<head>\n" +
                        "</head>\n" +
                       " <body>\n" +
                        //[NU Changes as discussed with AP] 
                        //" <div class=\"span8\">\n" +
                        //" <div class=\"span12\">\n" +
                       " <div class=\"span" + RetriveWidthFromPixel(iHTMLSPANWIDTH) + "\">\n" +


                 strOuterHtml +
                 " </div>\n" +
                  "</body>\n" +
            "</html>\n"
                        );
                }
            }
            catch { }
        }

        public void WriteAppJSFile(string strFILENAME, string strSUBDIR, FileInfo f2)
        {
            try
            {
                if (f2.Exists)
                {
                    f2.Delete();
                }
                FileStream writingFile2 = f2.Create();

                using (StreamWriter sw = new StreamWriter(writingFile2))
                {

                    string strFILEDETAILS =

                        " var UserID_" + strFILENAME + " = \"1\";\n " +
    "var CustomerID_" + strFILENAME + "= \"2\";\n " +
    "var instanceMode_" + strFILENAME + "= \"conn\";\n " +

    //[NU Changes as discussed with AP] 
                        //"var instanceType_" + strFILENAME + " = " + strFILENAME + ";\n " +
    "var instanceType_" + strFILENAME + " = " + '"' + strFILENAME + '"' + ";\n " +

    "F2.Apps[\"" + strFILENAME + "\"] = (function () { \n " +
    " var App_Class = function (appConfig, appContent, root) {\n " +
                        // constructor
    " this.appConfig = appConfig;\n " +
    "this.appContent = appContent;\n " +
    "this.$root = $(root);\n " +
    "var $tbody = $('tbody', this.$root);\n " +
    "var $caption = $('caption', this.$root);\n " +

    " if (appConfig.context != null) {\n " +
    "    UserID_" + strFILENAME + " = appConfig.context.UserID;\n " +
     "   CustomerID_" + strFILENAME + " = appConfig.context.CustomerID;\n " +
      "  instanceMode_" + strFILENAME + " = appConfig.context.InstanceMode;\n " +
    " }\n " +

    " App_Class.prototype.init = function () {\n " +
    "    // perform init actions \n " +
     "   F2.log(\"Init " + strFILENAME + ".\");\n " +

        "setupSignalRGeneric(instanceType_" + strFILENAME + ", UserID_" + strFILENAME + ", CustomerID_" + strFILENAME + ", instanceMode_" + strFILENAME + ");\n " +


    "$('#" + strFILENAME + " :text[title=\"datepick\"]').datepicker({ format: 'mm/dd/yyyy', autoclose: true, weekStart: 0 }).on('changeDate', function (ev) { \n " +
           " try {\n " +

                " var paraValues = UserID_" + strFILENAME + "+ \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +


               " var itemType = \"DDList\";\n " +

                "var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf('_') + 1) + \"#\" + $(this).val();\n " +

                "if ($(this).val() != \"\")\n " +
                   " SendToBeast(instanceType_" + strFILENAME + " + \"#\" + paraValues, idValPair);\n " +
           " }\n " +
            "catch (err) {\n " +
               " var strerrordesc = \"Function:datepick(); Error is : \" + err.description + \"; Error number is \" + err.number + \"; Message :\" + err.message;\n " +
               " onJavascriptLog(\" " + strFILENAME + "_appclass.js\", strerrordesc);\n " +
            "}\n " +
        "});\n " +
        " $('#" + strFILENAME + ":text').click(function () {\n " +

           " if ($(this).hasClass(\"priceWidget\")) {\n " +
                "try {\n " +
                   " var clsAryMed = $(this).attr(\"class\");\n " +
                    "var paraValues = UserID_" + strFILENAME + "+ \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +
                    "var itemType = \"DDList\";\n " +
                    "var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf('_') + 1);\n " +

                  "  var eleInfo = instanceType_" + strFILENAME + "+ \"^\" + paraValues + \"^\" + idValPair;\n " +

                   " display_PriceWidget(eleInfo, $(this).val(), $(this).attr(\"name\"), clsAryMed.split(' ')[2].split('_')[1], $(this));\n " +
               " }\n " +
               " catch (err) {\n " +
                   " var strerrordesc = \"Function:text_priceWidget(); Error is : \" + err.description + \"; Error number is \" + err.number + \"; Message :\" + err.message;\n " +
                   " onJavascriptLog(\"" + strFILENAME + "_appclass.js\", strerrordesc);\n " +
               " }\n " +
           " }\n " +
            "else if ($(this).hasClass(\"termWidget\")) {\n " +
               " try {\n " +
                   " var clsAryMed = $(this).attr(\"class\");\n " +

                    "var paraValues = UserID_" + strFILENAME + " + \"^\" + CustomerID_" + strFILENAME + "+ \"^\" + instanceMode_" + strFILENAME + ";\n " +
                    "var itemType = \"DDList\";\n " +
                   " var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf('_') + 1);\n " +

                   " var eleInfo = instanceType_" + strFILENAME + " + \"^\" + paraValues + \"^\" + idValPair;\n " +

                    "display_TermWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));\n " +
                "}\n " +
              "  catch (err) {\n " +
                   " var strerrordesc = \"Function:text_termWidget(); Error is :\" + err.description + \"; Error number is \" + err.number + \"; Message :\" + err.message;\n " +
                    "onJavascriptLog(\"" + strFILENAME + "_appclass.js\", strerrordesc);\n " +
                "}\n " +
           " }\n " +
            "else if ($(this).hasClass(\"basisWidget\")) {\n " +
               " try {\n " +
                   " var clsAryMed = $(this).attr(\"class\");\n " +

                    "var paraValues = UserID_" + strFILENAME + " + \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +
                    "var itemType = \"DDList\";\n " +
                   " var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf('_') + 1);\n " +

                    "var eleInfo = instanceType_" + strFILENAME + " + \"^\" + paraValues + \"^\" + idValPair;\n " +

                    "display_BasisWidget(eleInfo, clsAryMed.split(' ')[2].split('_')[1], $(this));\n " +
                "}\n " +
                "catch (err) {\n " +
                    "var strerrordesc = \"Function:text_basisWidget(); Error is : \" + err.description + \"; Error number is \" + err.number +\"; Message :\" + err.message;\n " +
                    "onJavascriptLog(\"" + strFILENAME + "_appclass.js\", strerrordesc);\n " +
                "}\n " +
           " }\n " +
       " });\n " +
                        //Commented per suggestion of CP
                        //             " $('#" + strFILENAME + " :text').bind(\"blur\", function (event) {\n " +

       //                           " if ($(this).attr('id') == '" + strFILENAME + "_150' || $(this).attr('id') == '" + strFILENAME + "_152' || $(this).attr('id') == '" + strFILENAME + "_153') { \n " +
                        //                            "    GEN_FUNC_RevertNameValueSwipe($(this));\n " +
                        //                              "  FUNC_iPreoSubmitValue($(this));\n " +
                        //                           " }\n " +
                        //                       " });\n " +
                        //              " function GEN_FUNC_RevertNameValueSwipe(pElement) {\n " +
                        //  "   try {\n " +
                        //      "   var vCurrentValue = $(pElement).attr(\"name\");\n " +
                        //    "     var vNameValue = $(pElement).val();\n " +
                        //        " $(pElement).val(vCurrentValue);\n " +
                        //         "$(pElement).attr(\"name\", vNameValue);\n " +

       //    " }\n " +
                        //    " catch (excp) {\n " +
                        //        " alert(excp);\n " +
                        //    " }\n " +
                        //" }\n " +
                        //" function GEN_FUNC_RevertNameValueSwipe(pElement) {\n " +
                        //  "   try {\n " +
                        //      "   var vCurrentValue = $(pElement).attr(\"name\");\n " +
                        //    "     var vNameValue = $(pElement).val();\n " +
                        //        " $(pElement).val(vCurrentValue);\n " +
                        //         "$(pElement).attr(\"name\", vNameValue);\n " +
                        //                 //alert("Value = " + vNameValue + " # Name = " + vCurrentValue);
                        //    " }\n " +
                        //    " catch (excp) {\n " +
                        //        " alert(excp);\n " +
                        //    " }\n " +
                        //" }\n " +


      "$(\"#" + strFILENAME + " input[type='button']\").click(function () {\n " +

           " if (!$(this).hasClass(\"inputDisable\")) {\n " +
             "   try {\n " +
              "      var itemType = \"DDList\";\n " +
                 "   var paraValues = UserID_" + strFILENAME + " + \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +
                  "  var valToSubmit = $(this).attr(\"name\");\n " +
                   " if (valToSubmit == \"1\")\n " +
                    "    $(this).attr(\"name\", \"0\");\n " +
                    "else\n " +
                    "    $(this).attr(\"name\", \"1\");\n " +

                   " var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf(\'_\') + 1) + \"#\" + valToSubmit; \n " +

                    //if ($(this).val() != "") {        //Commented for this perticular app only
                  "  SendToBeast(instanceType_" + strFILENAME + " + \"#\" + paraValues, idValPair);\n " +
                        //}
              "  }\n " +
                "catch (err) {\n " +
                 "   var strerrordesc = \"Function:button_inputDisable(); Error is : \" + err.description + \"; Error number is \" + err.number + \"; Message :\" + err.message; \n " +
                   " alert(strerrordesc);\n " +
                   " onJavascriptLog(\"" + strFILENAME + "_appclass.js\", strerrordesc);\n " +
               " }\n " +
           " }\n " +
       " });\n " +


    " $(\"#" + strFILENAME + " select\").change(function () {\n " +
            "try {\n " +
                "var itemType = \"DDList\";\n " +
               " var paraValues = UserID_" + strFILENAME + " + \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +
                "var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf('_') + 1) + \"#\" + $(this).val();\n " +

                "if ($(this).val() != \"\") {\n " +
                    "SendToBeast(instanceType_" + strFILENAME + " + \"#\" + paraValues, idValPair);\n " +
                "}\n " +
            "}\n " +
            "catch (err) {\n " +
               " var strerrordesc = \"Function:select_DDList(); Error is : \" + err.description + \"; Error number is \" + err.number + \"; Message :\" + err.message;\n " +
                "onJavascriptLog(\"" + strFILENAME + "_appclass.js\", strerrordesc);\n " +
           " }\n " +
           " });\n " +


      "  $(\"#" + strFILENAME + " :text\").bind(\"paste\", function (e) {\n " +
          "  if ($(this).attr('id') == '" + strFILENAME + "_150' || $(this).attr('id') == '" + strFILENAME + "_152' || $(this).attr('id') == '" + strFILENAME + "_153') {\n " +
          "  }\n " +
          "  else {\n " +
              "  event.preventDefault ? event.preventDefault() : event.returnValue = false;\n " +
           " }\n " +
       " });\n " +


                    "  $(\"#" + strFILENAME + " :text\").bind(\"paste\", function (e) {\n " +
          "  if ($(this).attr('id') == '" + strFILENAME + "_150' || $(this).attr('id') == '" + strFILENAME + "_152' || $(this).attr('id') == '" + strFILENAME + "_153') {\n " +
          "  }\n " +
          "  else {\n " +
              "  event.preventDefault ? event.preventDefault() : event.returnValue = false;\n " +
           " }\n " +
       " });\n " +

       " $(\"#" + strFILENAME + " :text\").bind('keydown', function (event) {\n " +
           " try {\n " +
           "     var keyNumber = event.keyCode;\n " +
            "    if ($(this).attr(\"title\") == \"datepick\") event.preventDefault ? event.preventDefault() : event.returnValue = false; \n " +

               " if (keyNumber == 13) {\n " +

               //Commented per suggestion of CP
                        //   "if ($(this).attr('id') == '" + strFILENAME + "_150' || $(this).attr('id') == '" + strFILENAME + "_152' || $(this).attr('id') == '" + strFILENAME + "_153') {\n " +
                        //    "   GEN_FUNC_RevertNameValueSwipe($(this));\n " +
                        //      " FUNC_iPreoSubmitValue($(this));\n " +
                        //"   }\n " +

                    "var paraValues = UserID_" + strFILENAME + " + \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +
                   " var itemType = \"DDList\";\n " +
                   " var idValPair = itemType + \"#\" + $(this).attr(\"id\").substring($(this).attr(\"id\").lastIndexOf(\'_\') + 1) + \"#\" + $(this).val(); \n " +
                   " if ($.trim($(this).val()) != \"\") {\n " +
                     "   SendToBeast(instanceType_" + strFILENAME + " + \"#\" + paraValues, idValPair);\n " +
                   " } \n " +
                   " event.preventDefault ? event.preventDefault() : event.returnValue = false;\n " +
               " }\n " +
           " }\n " +
            "catch (err) {\n " +
               " var strerrordesc = \"Function:text_keydown(); Error is : \" + err.description + \"; Error number is \" + err.number + \"; Message :\" + err.message;\n " +
              "  onJavascriptLog(\"" + strFILENAME + "_appclass.js\", strerrordesc);\n " +
           " }\n " +
        "});\n " +

        //Commented per suggestion of CP
                        //"function FUNC_iPreoSubmitValue(pElement) {\n " +
                        //  "  try {\n " +
                        //      "  var paraValues = UserID_" + strFILENAME + " + \"^\" + CustomerID_" + strFILENAME + " + \"^\" + instanceMode_" + strFILENAME + ";\n " +
                        //        "var itemType = \"DDList\";\n " +
                        //       " var idValPair = itemType + \"#\" + $(pElement).attr(\"id\").substring($(pElement).attr(\"id\").lastIndexOf(\'_\') + 1) + \"#\" + $(pElement).val();\n " +
                        //       " SendToBeast(instanceType_" + strFILENAME + " + \"#\" + paraValues, idValPair);\n " +
                        //  "  }\n " +
                        //   " catch (excp) {\n " +
                        //      "  alert(excp);\n " +
                        //   " }\n " +
                        //"}\n " +
    " };\n " +
    " };\n " +
    "return App_Class;\n " +
    "})();\n ";

                    sw.Write(strFILEDETAILS);
                }
            }
            catch { }
        }

        //        public void WriteManifestJSFile(string strFILENAME, StringWriter swSTRINGWRITER, FileInfo f3, string strFILENAMEORG,int iMAXCOL)
        public void WriteManifestJSFile(string strFILENAME, StringWriter swSTRINGWRITER, FileInfo f3, int iMAXCOL)
        {
            if (f3.Exists)
            {
                f3.Delete();
            }

            FileStream writingFile3 = f3.Create();
            //writingFile.Write(getHTML(),0,
            string strOuterHtml3 = getHTML(swSTRINGWRITER);
            strOuterHtml3 = strOuterHtml3.Replace("[COLSPAN]", "colspan=" + '"' + iMAXCOL + '"' + "");
            var obj = strOuterHtml3.Replace("\n", "");
            obj = obj.Replace("\r", "");
            obj = obj.Replace("\t", "");
            using (StreamWriter sw = new StreamWriter(writingFile3))
            {
                string str3 = "F2_jsonpCallback_" + strFILENAME + "({ \n " +
                    //[NU Changes as discussed with AP] 
                    //" \"script\": [ \n " +
" \"scripts\": [ \n " +
"\"Products/VCM/" + strFILENAME + "/appclass.js\"\n " +
"],\n " +
" \"styles\": [\n " +
"\"Products/VCM/" + strFILENAME + "/app.css\"\n " +
" ],\n " +
"\"apps\": [{\n " +
"\"data\": {},\n " +
"\"html\": ['" + obj + "'].join(\"\")\n " +
"}]\n" +
"})\n " +
" \n ";
                sw.Write(str3);
            }
        }

        public void WriteAppCSSFile(string strFILENAME, FileInfo f1)
        {
            try
            {
                if (f1.Exists)
                {
                    f1.Delete();
                }

                FileStream writingFile1 = f1.Create();
                //writingFile.Write(getHTML(),0,

                using (StreamWriter sw = new StreamWriter(writingFile1))
                {
                    string str = "." + strFILENAME + " \n{ " +
    " margin: 0;\n" +
    "padding: 5px; \n " +
    "text-align: center; \n " +
    " background-color: white; \n " +
                        //" border: solid 1px #c3c3c3; \n " +
    " width: 300px;\n " +
    "}\n " +

    "." + strFILENAME + " tbody td { \n " +
    "text-align: center !important; \n " +
    " height: 25px !important;\n " +
    "vertical-align: middle;\n " +
    "} \n " +

    "." + strFILENAME + " .last {\n " +
    "} \n " +

    "." + strFILENAME + " .last-change {\n " +
      "font-size: 80%;\n " +
    "}\n " +

    "." + strFILENAME + ".positive {\n " +
    "color: green;\n " +
    "}\n " +

    "." + strFILENAME + ".negative {\n " +
    " color: red;\n " +
    "}\n " +

    "." + strFILENAME + " .input-append {\n " +
    "padding-bottom: 7px;\n " +
    "padding-top: 7px;\n " +
    " }\n " +

    "." + strFILENAME + " p.hi {\n " +
    "font-weight: bold;\n " +
    " font-size: 20px;\n " +
    " }\n " +

    "." + strFILENAME + " .MainHeader \n " +
    " { \n " +
    " background-color: #4E78A0;\n " +
    " } \n " +

    "." + strFILENAME + " .header_fcolor {\n " +
    "color: white;\n " +
    "font-weight: bold;\n " +
    "text-align: left;\n " +
    "font-size: 11px;\n " +
    " }\n " +

    "." + strFILENAME + " input[type=\"text\"] " + " \n " +
                "   { \n " +
    " font-size: 95%;\n " +
    "width: 80% !important; \n " +
    "font-family: Calibri !important;\n " +
    "padding: 0px !important;\n " +
    "margin: 0px !important;\n " +
    "border: none !important;\n " +
    "text-align: center !important;\n " +
    " }";


                    sw.Write(str);
                }
            }
            catch { }
        }

        public string getHTML(StringWriter swSTRINGWRITER)
        {
            return swSTRINGWRITER.ToString();
        }

        private int RetriveWidthFromPixel(int width)
        {
            int range = 0;
            int SpanWidth = 12;

            if (width > 0)
            {
                range = (width - 1) / 400;
            }


            switch (range)
            {
                case 0:
                    //0 to 400
                    SpanWidth = 4;
                    break;

                case 1:
                    //400 to 800
                    SpanWidth = 8;
                    break;

                case 2:
                    //800 to 1200
                    SpanWidth = 12;
                    break;
            }
            return SpanWidth;
        }
    }
}
