var PWM_FRACTION_DISP_COUNT, PWM_SUB_FRACTION_DISP_COUNT, PWM_1_DIGIT_DISP_COUNT, PWM_01_DIGIT_DISP_COUNT, PWM_001_DIGIT_DISP_COUNT, PWM_PRICE_DISP_COUNT = 0;
var PWM_CALCULATED_VALUE;
var PWM_GLOBAL_SLTDVAL = '';
var PWM_GLOBAL_TEMP_SLTDVAL = '';
var IsChange = '';
var pwmIS_NEGATIVE = false;

var PWM_GLOBAL_TXTVAL_1 = '';
var PWM_GLOBAL_TXTVAL_2 = '';
var PWM_GLOBAL_TXTVAL_2_CALC = '';
var PWM_GLOBAL_DEFAULT_FORAMT = '';
var PWM_GLOBAL_SLTD_FORMAT = '';
var PWM_GLOBAL_LAST_CLICKED_ELEM_INFO = '';
var PWM_GLOBAL_BEAST_VALUE_1, PWM_GLOBAL_BEAST_VALUE_2;
var PWM_GLOBAL_PRICE = '';

var PWM_FRAC_UNIT_WIDTH = 56; //(px)
var PWM_MAX_LEFT_PADDING = 84; //(px)
var PWM_TXT1_DEFAULT_WIDTH = 82; //(px)
var PWM_TXT2_DEFAULT_WIDTH = 98; //(px)

var PWM_LIST_HTML = "<div style=\"float: left; width:" + PWM_FRAC_UNIT_WIDTH + "px; text-align:center;\">"
                        + "<span class=\"pwmSpnPriceSectionTitle\">[TITLE]</span><br />"
                        + "<div class=\"dvSpinUpArrow\" onclick=\"[UPARROW_METHOD]\"></div>"
                        + "<div id=\"[DIVID]\" class=\"dvSpinViewFrame\"></div>"
                        + "<div class=\"dvSpinDownArrow\" onclick=\"[DOWNARROW_METHOD]\"></div>"
                        + "</div>";

var PWM_FORMAT_SELECTION_LIST = "<div id=\"pwmDvFormatList\">                         <ul>                             <li id=\"pwmLi_0\">1/2</li>                             <li id=\"pwmLi_1\">1/4</li>                             <li id=\"pwmLi_2\">1/8</li>                             <li id=\"pwmLi_3\">1/16</li>                             <li id=\"pwmLi_4\">1/32</li>                             <li id=\"pwmLi_5\">1/64</li>                             <li id=\"pwmLi_6\">1/128</li>                             <li id=\"pwmLi_7\">1/256</li>                             <li id=\"pwmLi_8\">1/32+</li>                             <li id=\"pwmLi_9\">1/4R</li>                             <li id=\"pwmLi_10\">1/8R</li>                             <li id=\"pwmLi_11\">1/16R</li>                             <li id=\"pwmLi_12\">1/32R</li>                             <li id=\"pwmLi_13\">1/64R</li>                             <li id=\"pwmLi_14\">1/128R</li>                             <li id=\"pwmLi_15\">1/256R</li>                             <li id=\"pwmLi_16\">1</li>                             <li id=\"pwmLi_17\">0.1</li>                             <li id=\"pwmLi_18\">0.01</li>                             <li id=\"pwmLi_19\">0.001</li>                             <li id=\"pwmLi_20\">0.00+(1/2)</li>                             <li id=\"pwmLi_21\">0.00+(1/4)</li>                         </ul>                     </div>";

//var pwmWidgets = {
//    "Blue1": [{
//        "Style": "",
//        "Html": ['<input type="hidden" value="" id="pwmHdnSelectedFormat" />                     <div id="pwmDvFormatList">                         <ul>                             <li id="pwmLi_0">1/2</li>                             <li id="pwmLi_1">1/4</li>                             <li id="pwmLi_2">1/8</li>                             <li id="pwmLi_3">1/16</li>                             <li id="pwmLi_4">1/32</li>                             <li id="pwmLi_5">1/64</li>                             <li id="pwmLi_6">1/128</li>                             <li id="pwmLi_7">1/256</li>                             <li id="pwmLi_8">1/32+</li>                             <li id="pwmLi_9">1/4R</li>                             <li id="pwmLi_10">1/8R</li>                             <li id="pwmLi_11">1/16R</li>                             <li id="pwmLi_12">1/32R</li>                             <li id="pwmLi_13">1/64R</li>                             <li id="pwmLi_14">1/128R</li>                             <li id="pwmLi_15">1/256R</li>                             <li id="pwmLi_16">1</li>                             <li id="pwmLi_17">0.1</li>                             <li id="pwmLi_18">0.01</li>                             <li id="pwmLi_19">0.001</li>                             <li id="pwmLi_20">0.00+(1/2)</li>                             <li id="pwmLi_21">0.00+(1/4)</li>                         </ul>                     </div>'].join("")
//    }]    
//};

function PWM_Func_LoadHTML() {
    try {

        $('#' + pwmWIDGET_DIV_ID).html("");

        var pwmHtml = "";

        //(Black theme)
        pwmHtml += "<style type=\"text/css\">         #dvPriceWidgetMobile {             height: 195px;             width: 270px;         }          .pwmDvTitle, .pwmDvContent, .pwmDvContentWrapper, .pwmDvTop, .pwmDvMiddle, .pwmDvBottom {             width: 260px;         }          .pwmDvTitle, .pwmDvContent {             padding: 0px 5px;             float: left;             font-family: 'Segoe UI',Verdana,sans-serif;             font-size: 14px;         }          .pwmDvTop {             height: 22px;             border: none;             overflow: hidden;         }          .pwmDvBottom {             height: 25px;             border: none;         }          .pwmDvMiddle {             height: 103px;             margin: 5px 0px 7px 0px;             font-size: 16px;         }    .pwmDvTitle {             border-bottom: 1px solid #000;             background-color: #000;             color: white;             height: 20px;             cursor: move;         }          .pwmDvContent {             height: 165px;             margin-top: 5px;         }          .pwmDvContentWrapper {             background-color: white;         }          .pwmDvBottom_0, .pwmDvBottom_2 {             background-color: #FFF;             font-weight: bold;             text-align: center;             float: left;             height: 22px;         }          .pwmDvBottom_0 {             width: 20px;         }          .pwmDvFormatBox {             float: left;             width: 56px;             text-align: left;             padding-left: 5px;             background-color: transparent;             font-size: 11px;        height:100%;     line-height:20px;   }          .pwmDvFormatDropArrow {             float: left;             background-image: url('images/DropdownOpen.png');             background-position: center center;             background-repeat: no-repeat;             height: 20px;             width: 14px;             background-color: transparent;         }          .pwmDvTop_1 {             color: #FFF;             width:  " + PWM_TXT1_DEFAULT_WIDTH + "px;    font-weight: bold;             background-color: #777777 !important;             border-top: 1px solid #777777 !important;             border-bottom: 1px solid #777777 !important;             overflow: hidden;         }          .pwmDvTop_2 {             width: " + PWM_TXT2_DEFAULT_WIDTH + "px;         background-color: #FFF !important;             border: 1px solid #777777 !important;             overflow: hidden;         }          .pwmDvTop_1, .pwmDvTop_2 {             background-color: #FFF;             text-align: center;             float: left;             height: 20px;         }          #pwmDvFormatList {             height: 105px;             width: 74px;             display: none;             overflow-y: scroll;             overflow-x: hidden;             position: absolute;             background-color: #FFF;             border: 1px solid #777777;             font-size: 11px;         }              #pwmDvFormatList ul {                 list-style: none;                 margin: 0px auto;                 text-align: left;                 padding-left: 5px;             }                  #pwmDvFormatList ul li {                     padding: 2px;                     list-style-type: none;                     margin: 0px;                     padding: 0px;                 }                      #pwmDvFormatList ul li:hover {                         background-color: #777777;                         color: white;                     }          .pwmHighlight {         }              .pwmHighlight:hover {                 cursor: pointer;                 background-color: #777777;                            }          .pwmDvPriceSection, .pwmDvFormatSection {             height: 100%;             float: left;             text-align: center;         }          .pwmDvPriceSection {             width: 60px;         }          .pwmDvFormatSection {                    }          .dvSpinViewFrame {             height: 25px;             width: 100%;             padding-top: 3px;             text-align: center;             vertical-align: middle;         }              .dvSpinViewFrame:hover {                 background-color: #777777;                 color: white;             }          .dvSpinUpArrow, .dvSpinDownArrow {             text-align: center;             vertical-align: middle;             height: 28px;             width: 100%;         }          .dvSpinUpArrow {             background-image: url('images/Widget_UpDown1.png');             background-repeat: no-repeat;             background-position: center 2px;         }          .dvSpinDownArrow {             background-image: url('images/Widget_UpDown1.png');             background-repeat: no-repeat;             background-position: center -32px;         }          .dvSpinUpArrow:hover {             background-color: #777777;             color: white;             cursor: pointer;             background-image: url('images/Widget_UpDown1HoverBlack.png');             background-repeat: no-repeat;             background-position: center 2px;         }          .dvSpinDownArrow:hover {             background-color: #777777;             color: white;             cursor: pointer;             background-image: url('images/Widget_UpDown1HoverBlack.png');             background-repeat: no-repeat;             background-position: center -32px;         }          .pwmStaticBox {             padding: 0px !important;             text-align: center !important;             border: 1px solid #FFF !important;             font-size: 98% !important;             margin: 0px !important;         }          .pwmEditBox {             text-align: right !important;             border: 1px solid Gray !important;             padding: 0px !important;             font-size: 98% !important;             margin: 0px !important;         }          .pwmDvNegative {             float: left;             padding-top: 22px;         }          .pwmPlus {             background-image: url('images/PlusMinusThinBlack.png');         }          .pwmMinus {             background-image: url('images/MinusPlusThinBlack.png');         }          .pwmPlus, .pwmMinus {             cursor: pointer;             background-repeat: no-repeat;             background-position: 5px 0px;             height: 20px;             width: 30px;         }          .pwmSpnPriceSectionTitle {             font-size: 70%;             height: 18px;         } .pwmBtnClose {             font-size: 20px;             line-height: 16px;             font-weight: bold;             color: #FFFFFF;             text-shadow: 0 1px 0 #111;             text-decoration: none;             float: right;             background-color: #000;             margin-right: 3px;             opacity: 0.8;             filter: alpha(opacity=80);         }              .pwmBtnClose:hover {                 color: #FFFFFF;                 text-decoration: none;                 cursor: pointer;                 opacity: 1.0;                 filter: alpha(opacity=100);             }    </style>";
        pwmHtml += "<div id=\"dvPwmTitle\" class=\"pwmDvTitle\">             Price Widget                                  <a class=\"pwmBtnClose\" data-dismiss=\"modal\" aria-hidden=\"true\" onclick=\"PWM_Func_Close_PriceWidget();\">&times;</a> </div>         <div class=\"pwmDvContent\">             <div class=\"pwmDvContentWrapper\">                 <div class=\"pwmDvTop\">                     <div class=\"pwmHighlight\"  style=\"width: 75px; border: 1px solid #777777; background-color: #FFF; text-align: center; float: left; height: 20px;\">                 <div class=\"pwmDvFormatBox\" onclick=\"PWM_Func_DisplayFormatOptions();\" id=\"pwmDvSelectedFormatDisplay\"></div>                         <div class=\"pwmDvFormatDropArrow\" onclick=\"PWM_Func_DisplayFormatOptions();\"></div>                     </div>                     <div class=\"pwmDvTop_1\" id=\"pwmDvTxtValue1\" title=\" Price Value \"></div>                     <div class=\"pwmDvTop_2\" id=\"pwmDvTxtValue2\"></div>                  </div>                 <div class=\"pwmDvMiddle\">                     <div class=\"pwmDvNegative\">         <div class=\"dvSpinUpArrow\" onclick=\"PWM_Func_ToggleNegative();\"></div>                         <div class=\"dvSpinViewFrame\">                             <div id=\"dvPwmNegative\" class=\"pwmPlus\" onclick=\"PWM_Func_ToggleNegative();\">                             </div>                         </div>                         <div class=\"dvSpinDownArrow\" onclick=\"PWM_Func_ToggleNegative();\"></div>                     </div>                     <input type=\"hidden\" id=\"hdnBeastValueHolder\" name=\"\" value=\"\" />                     <input id=\"txtLastClickedEleInfo\" type=\"hidden\" />                     <div class=\"pwmDvPriceSection\">                         <span class=\"pwmSpnPriceSectionTitle\">Price</span>                         <div class=\"dvSpinUpArrow\" onclick=\"PWM_Func_Rotate_PriceElements('more');\"></div>                         <div class=\"dvSpinViewFrame\">                             <input id=\"pwmTxtPrice\" type=\"text\" class=\"pwmStaticBox\" style=\"width: 54px; height: 21px !important;\" />                             <input id=\"pwmHdnPrice\" type=\"hidden\" />                         </div>                         <div class=\"dvSpinDownArrow\" onclick=\"PWM_Func_Rotate_PriceElements('less');\"></div>                     </div>                     <div id=\"dvPwmFormatSection\" class=\"pwmDvFormatSection\"></div>                 </div>                 <div class=\"pwmDvBottom\">                     <div style=\"text-align: center;\">                         <input type=\"button\" class=\"btn btn-xs btn-default\" value=\"OK\" onclick=\"PWM_Func_Save_PriceWidget();\" style=\"margin: 0px 5px 0px 0px; width: 60px;\"  data-dismiss=\"modal\" aria-hidden=\"true\" />                         <input type=\"button\" class=\"btn btn-xs\" value=\"CANCEL\" onclick=\"PWM_Func_Close_PriceWidget();\" style=\"margin: 0px 0px 0px 5px;\" data-dismiss=\"modal\" aria-hidden=\"true\"  />    <input type=\"button\" class=\"btn btn-xs btn-default\" value=\"CLEAR\" onclick=\"PWM_Func_Clear_PriceWidget();\" style=\"margin: 0px 0px 0px 5px;\" data-dismiss=\"modal\" aria-hidden=\"true\"  />                 </div>   " + PWM_FORMAT_SELECTION_LIST + "              </div>     </div>         </div>";
        //(Blue theme)
        //pwmHtml += "<style type=\"text/css\">         #dvPriceWidgetMobile {             height: 200px;             width: 270px;            }          .pwmDvTitle, .pwmDvContent, .pwmDvContentWrapper, .pwmDvTop, .pwmDvMiddle, .pwmDvBottom {             width: 260px;         }          .pwmDvTitle, .pwmDvContent {             padding: 0px 5px;             float: left;             font-family: 'Segoe UI',Verdana,sans-serif;             font-size: 14px;         }          .pwmDvTop {             height: 22px; /*border: 1px solid #0094ff;*/             border: none;    overflow:hidden;     }          .pwmDvBottom {             height: 22px; /*border: 1px solid #0094ff;*/             border: none;         }          .pwmDvMiddle {             height: 115px;             margin: 5px 0px 3px 0px;   font-size:16px;      }         /*.pwmDvBottom {             height: 20px;         }*/          .pwmDvTitle {             border-bottom: 1px solid #306EFF;             background-color: #006dcc; /*#5CB3FF;*/             color: white;             height: 20px;      cursor:move;   }          .pwmDvContent {             height: 170px;             margin-top: 5px;         }          .pwmDvContentWrapper {             background-color: white;         }          .pwmDvBottom_0, .pwmDvBottom_2 {             background-color: #FFF;             font-weight: bold;             text-align: center;             float: left;             height: 22px;         }          .pwmDvBottom_0 {             width: 20px;         }          .pwmDvFormatBox {             float: left;             width: 56px;             text-align: left;             padding-left: 5px;             background-color: transparent;             font-size: 11px;         }          .pwmDvFormatDropArrow {             float: left;             background-image: url('images/DropdownOpen.png');             background-position: center center;             background-repeat: no-repeat;             height: 20px;             width: 14px;             background-color: transparent;         }          .pwmDvTop_1 {             color: #000;             width:  " + PWM_TXT1_DEFAULT_WIDTH + "px;             font-weight: bold;             background-color: #F0F8FF !important;             border-top: 1px solid #63b8ff !important;             border-bottom: 1px solid #63b8ff !important;   overflow: hidden;      }          .pwmDvTop_2 {             width: " + PWM_TXT2_DEFAULT_WIDTH + "px;             background-color: #FFF !important;             border: 1px solid #63b8ff !important;   overflow: hidden;      }          .pwmDvTop_1, .pwmDvTop_2 {             background-color: #FFF; /*font-weight: bold;*/             text-align: center;             float: left;             height: 20px;         }          #pwmDvFormatList {             height: 105px;             width: 74px;             display: none;             overflow-y: scroll;             overflow-x: hidden;             position: absolute;             background-color: #FFF;             border: 1px solid #999;             font-size: 11px;         }              #pwmDvFormatList ul {                 list-style: none;                 margin: 0px auto;                 text-align: left;                 padding-left: 5px;             }                  #pwmDvFormatList ul li {                     padding: 2px;                     list-style-type: none;                     margin: 0px;                     padding: 0px;                 }                      #pwmDvFormatList ul li:hover {                         background-color: #F0F8FF;                     }          .pwmHighlight {         }              .pwmHighlight:hover {                 cursor: pointer;                 background-color: #F0F8FF;             }          .pwmDvPriceSection, .pwmDvFormatSection {             height: 100%;             float: left;             text-align: center;         }          .pwmDvPriceSection {             width: 60px;         }          .pwmDvFormatSection {            /* width: 170px; */        }          .dvSpinViewFrame {             height: 25px;             width: 100%;             padding-top: 3px;             text-align: center;             vertical-align: middle;         }              .dvSpinViewFrame:hover {                 background-color: #F0F8FF;             }          .dvSpinUpArrow, .dvSpinDownArrow {             text-align: center;             vertical-align: middle;             height: 30px;             width: 100%;         }          .dvSpinUpArrow {             background-image: url('images/Widget_UpDown1.png');             background-repeat: no-repeat;             background-position: center 2px;         }          .dvSpinDownArrow {             background-image: url('images/Widget_UpDown1.png');             background-repeat: no-repeat;             background-position: center -32px;         }          .dvSpinUpArrow:hover {             background-color: #F0F8FF;             color: Navy;             cursor: pointer;             background-image: url('images/Widget_UpDown1Hover.png');             background-repeat: no-repeat;             background-position: center 2px;         }          .dvSpinDownArrow:hover {             background-color: #F0F8FF;             color: Navy;             cursor: pointer;             background-image: url('images/Widget_UpDown1Hover.png');             background-repeat: no-repeat;             background-position: center -32px;         }          .pwmStaticBox {             padding: 0px !important;             text-align: center !important;             border: 1px solid #FFF !important;             font-size: 98% !important;             margin: 0px !important;         }          .pwmEditBox {             text-align: right !important;             border: 1px solid Gray !important;             padding: 0px !important;             font-size: 98% !important;             margin: 0px !important;         }          .pwmDvNegative {             width: 30px;             float: left;    height:100%;  padding-top:16px;       }          .pwmPlus {             background-image: url('images/PlusMinusThin.png');         }          .pwmMinus {             background-image: url('images/MinusPlusThin.png');         }          .pwmPlus, .pwmMinus {             cursor: pointer;             background-repeat: no-repeat;             background-position: 5px 0px;             height: 20px;             width: 30px;         }                      .pwmSpnPriceSectionTitle {             font-size: 70%;             height: 18px;         }     </style>";
        //pwmHtml += "<div id=\"dvPwmTitle\" class=\"pwmDvTitle\">             Price Widget                                   <button type=\"button\" class=\"close white\" data-dismiss=\"modal\" aria-hidden=\"true\" style=\"background-color: white;\" onclick=\"PWM_Func_Close_PriceWidget();\">×</button>         </div>         <div class=\"pwmDvContent\">             <div class=\"pwmDvContentWrapper\">                 <div class=\"pwmDvTop\">                     <div class=\"pwmHighlight\"  style=\"width: 75px; border: 1px solid #777777; background-color: #FFF; text-align: center; float: left; height: 20px;\">                 <div class=\"pwmDvFormatBox\" onclick=\"PWM_Func_DisplayFormatOptions();\" id=\"pwmDvSelectedFormatDisplay\"></div>                         <div class=\"pwmDvFormatDropArrow\" onclick=\"PWM_Func_DisplayFormatOptions();\"></div>                     </div>                     <div class=\"pwmDvTop_1\" id=\"pwmDvTxtValue1\" title=\" Price Value \"></div>                     <div class=\"pwmDvTop_2\" id=\"pwmDvTxtValue2\"></div>                  </div>                 <div class=\"pwmDvMiddle\">                     <div class=\"pwmDvNegative\">         <div class=\"dvSpinUpArrow\" onclick=\"PWM_Func_ToggleNegative();\"></div>                         <div class=\"dvSpinViewFrame\">                             <div id=\"dvPwmNegative\" class=\"pwmPlus\" onclick=\"PWM_Func_ToggleNegative();\">                             </div>                         </div>                         <div class=\"dvSpinDownArrow\" onclick=\"PWM_Func_ToggleNegative();\"></div>                     </div>                     <input type=\"hidden\" id=\"hdnBeastValueHolder\" name=\"\" value=\"\" />                     <input id=\"txtLastClickedEleInfo\" type=\"hidden\" />                     <div class=\"pwmDvPriceSection\">                         <span class=\"pwmSpnPriceSectionTitle\">Price</span>                         <div class=\"dvSpinUpArrow\" onclick=\"PWM_Func_Rotate_PriceElements('more');\"></div>                         <div class=\"dvSpinViewFrame\">                             <input id=\"pwmTxtPrice\" type=\"text\" class=\"pwmStaticBox\" style=\"width: 54px; height: 21px !important;\" />                             <input id=\"pwmHdnPrice\" type=\"hidden\" />                         </div>                         <div class=\"dvSpinDownArrow\" onclick=\"PWM_Func_Rotate_PriceElements('less');\"></div>                     </div>                     <div id=\"dvPwmFormatSection\" class=\"pwmDvFormatSection\"></div>                 </div>                 <div class=\"pwmDvBottom\">                     <div style=\"text-align: center;\">                         <input type=\"button\" class=\"btn btn-small btn-inverse\" value=\"OK\" onclick=\"PWM_Func_Save_PriceWidget();\" style=\"margin: 0px 5px 0px 0px; width: 60px;\"  data-dismiss=\"modal\" aria-hidden=\"true\" />                         <input type=\"button\" class=\"btn btn-small\" value=\"CANCEL\" onclick=\"PWM_Func_Close_PriceWidget();\" style=\"margin: 0px 0px 0px 5px;\" data-dismiss=\"modal\" aria-hidden=\"true\"  />                     </div>   " + PWM_FORMAT_SELECTION_LIST + "              </div>     </div>         </div>";

        $('#' + pwmWIDGET_DIV_ID).html(pwmHtml);

        twmIS_HTML_LOADED = true;

    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_LoadHTML", err);
    }
}

function PWM_Func_display_PriceWidget(eleIDForWidget, orgVal, widgetVal, defFormat, priceEle) {

    try {
        pwmWIDGET_DIV_ID = "dvPriceWidgetMobile";

        PWM_Func_LoadHTML();

        PWM_GLOBAL_DEFAULT_FORAMT = PWM_Func_ConvertFormatTypeIndex(defFormat);

        if (widgetVal == "" || widgetVal == undefined || isNaN(widgetVal))
            widgetVal = "0";

        if (widgetVal.toString().indexOf('-')) {    //= if(0){...} = non negative
            pwmIS_NEGATIVE = false;
            $('#dvPwmNegative').removeClass('pwmMinus');
            $('#dvPwmNegative').addClass('pwmPlus');
        }
        else {
            pwmIS_NEGATIVE = true;
            $('#dvPwmNegative').removeClass('pwmPlus');
            $('#dvPwmNegative').addClass('pwmMinus');
        }

        //$('#pwmHdnBeastValueHolder').val(orgVal);
        PWM_GLOBAL_BEAST_VALUE_1 = orgVal

        //$('#pwmHdnBeastValueHolder').attr("name", widgetVal);
        PWM_GLOBAL_BEAST_VALUE_2 = widgetVal;

        //$('#pwmHdnLastClickedEleInfo').val(eleIDForWidget);
        PWM_GLOBAL_LAST_CLICKED_ELEM_INFO = eleIDForWidget;

        //$('#pwmDvTxtValue2').text(parseFloat(widgetVal));
        PWM_Func_Set_Txt2Value(parseFloat(widgetVal));

        //new
        //$('#pwmHdnSelectedFormat').val(PWM_GLOBAL_DEFAULT_FORAMT);
        PWM_GLOBAL_SLTD_FORMAT = PWM_GLOBAL_DEFAULT_FORAMT;
        $('#pwmDvSelectedFormatDisplay').text($("#pwmLi_" + PWM_GLOBAL_DEFAULT_FORAMT).text());
        //new

        PWM_Func_START();

        PWM_Func_BindEvents();

        /* ============================ */

        //$('#' + pwmWIDGET_DIV_ID).show();
        $('#' + pwmWIDGET_DIV_ID).modal({ backdrop: true, keyboard: true });
        $("div:Last").removeClass("modal-backdrop in");
        positionWidget($(priceEle).attr('id'));
        $('#' + pwmWIDGET_DIV_ID).focus();
        $('#hdnWgtElement').val($(priceEle).attr('id'));

        /* ============================ */
    }
    catch (err) {
        PWM_Func_HandleJsError("display_PriceWidget", err);
    }
}

function PWM_Func_START() {
    try {
        var vSelectedFormat = parseInt(PWM_GLOBAL_SLTD_FORMAT, 10);
        var vTotalFractionElements = 0;
        var vTotalSubFractionElements = 0;
        var vShow_FractionList = false;
        var vShow_SubFractionList = false;
        var vShow_1_DigitList = false;
        var vShow_01_DigitList = false;
        var vShow_001_DigitList = false;

        PWM_Func_ShowFullValue_PriceWidget();

        var _valArray = PWM_CALCULATED_VALUE.split('#');

        PWM_PRICE_DISP_COUNT = parseInt(_valArray[0], 10);

        switch (vSelectedFormat) {
            case 0:     //eHalfs (1/2)

                vTotalFractionElements = 2;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 1:     //eQuarters (1/4)
            case 9:     //eQuartersReduced (1/4R)

                vTotalFractionElements = 4;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 2:     //eEighths (1/8)
            case 10:    //eEighthsReduced (1/8R)

                vTotalFractionElements = 8;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 3:     //eSixteenths (1/16)
            case 11:    //eSixteenthsReduced (1/16R)

                vTotalFractionElements = 16;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 4:     //eThirtySeconds (1/32)
            case 12:    //eThirtySecondsReduced (1/32R)

                vTotalFractionElements = 32;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 5:     //eSixtyFourths (1/64)
            case 13:    //eSixtyFourthsReduced (1/64R)

                vTotalFractionElements = 64;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 6:     //eOneTwentyEights (1/128)
            case 14:    //eOneTwentyEightsReduced (1/128R)

                vTotalFractionElements = 128;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 7:     //eTwoFiftySixths (1/256)
            case 15:    //eTwoFiftySixthsReduced (1/256R)

                vTotalFractionElements = 256;
                vShow_FractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                break;

            case 8:     //eBond (1/32+)

                vTotalFractionElements = 32;
                vTotalSubFractionElements = 8;
                vShow_FractionList = true;
                vShow_SubFractionList = true;
                PWM_FRACTION_DISP_COUNT = parseInt(_valArray[1].split('/')[0], 10);
                PWM_SUB_FRACTION_DISP_COUNT = parseInt(_valArray[2], 10);
                break;

            case 16:    //e0DecimalPlaces (1)
                break;

            case 17:    //e1DecimalPlace (0.1)

                vShow_1_DigitList = true;
                PWM_1_DIGIT_DISP_COUNT = parseInt(_valArray[1], 10);
                break;

            case 18:    //e2DecimalPlaces (0.01)

                vShow_1_DigitList = true;
                vShow_01_DigitList = true;
                PWM_1_DIGIT_DISP_COUNT = parseInt(_valArray[1], 10);
                PWM_01_DIGIT_DISP_COUNT = parseInt(_valArray[2], 10);
                break;

            case 19:    //e3DecimalPlaces (0.001)

                vShow_1_DigitList = true;
                vShow_01_DigitList = true;
                vShow_001_DigitList = true;
                PWM_1_DIGIT_DISP_COUNT = parseInt(_valArray[1], 10);
                PWM_01_DIGIT_DISP_COUNT = parseInt(_valArray[2], 10);
                PWM_001_DIGIT_DISP_COUNT = parseInt(_valArray[3], 10);
                break;

            case 20:    //eFurtureRateHalfs (0.00+(1/2))

                vShow_1_DigitList = true;
                vShow_01_DigitList = true;
                vShow_SubFractionList = true;
                vTotalSubFractionElements = 2;
                PWM_1_DIGIT_DISP_COUNT = parseInt(_valArray[1], 10);
                PWM_01_DIGIT_DISP_COUNT = parseInt(_valArray[2], 10);
                PWM_SUB_FRACTION_DISP_COUNT = parseInt(_valArray[3], 10);
                break;

            case 21:    //eFurtureRateQuarters (0.00+(1/4))

                vShow_1_DigitList = true;
                vShow_01_DigitList = true;
                vShow_SubFractionList = true;
                vTotalSubFractionElements = 4;
                PWM_1_DIGIT_DISP_COUNT = parseInt(_valArray[1], 10);
                PWM_01_DIGIT_DISP_COUNT = parseInt(_valArray[2], 10);
                PWM_SUB_FRACTION_DISP_COUNT = parseInt(_valArray[3], 10);
                break;
        }

        $('#pwmTxtPrice').val(PWM_PRICE_DISP_COUNT.toString());
        PWM_GLOBAL_PRICE = parseInt(PWM_PRICE_DISP_COUNT, 10);

        $('#dvPwmFormatSection').html('');

        var vRotatorCntt = 0;

        if (vShow_FractionList === true) {
            PWM_Func_Add_FractionList(vTotalFractionElements);
            vRotatorCntt++;
        }

        if (vShow_1_DigitList === true) {
            PWM_Func_Add_1_DigitList();
            vRotatorCntt++;
        }

        if (vShow_01_DigitList === true) {
            PWM_Func_Add_01_DigitList();
            vRotatorCntt++;
        }

        if (vShow_001_DigitList === true) {
            PWM_Func_Add_001_DigitList();
            vRotatorCntt++;
        }

        if (vShow_SubFractionList === true) {
            PWM_Func_Add_SubFractionList(vTotalSubFractionElements);
            vRotatorCntt++;
        }

        var vPadding = PWM_MAX_LEFT_PADDING - (vRotatorCntt * (PWM_FRAC_UNIT_WIDTH / 2));

        $('.pwmDvNegative').css('padding-left', vPadding + 'px');
        $('.pwmDvFormatSection').css('width', (vRotatorCntt * PWM_FRAC_UNIT_WIDTH) + 'px');
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_START", err);
    }
}

function PWM_Func_Add_FractionList(pTotalElements) {
    try {

        pTotalElements = parseInt(pTotalElements, 10);

        var vListHtml = PWM_LIST_HTML;

        vListHtml = vListHtml.replace("[TITLE]", "Fraction");
        vListHtml = vListHtml.replace("[UPARROW_METHOD]", "PWM_Func_Rotate_FractionList(" + pTotalElements + ", 'more')");
        vListHtml = vListHtml.replace("[DIVID]", "pwmDvFractionList");
        vListHtml = vListHtml.replace("[DOWNARROW_METHOD]", "PWM_Func_Rotate_FractionList(" + pTotalElements + ", 'less')");

        $('#dvPwmFormatSection').append(vListHtml);

        $('#pwmDvFractionList').text(PWM_FRACTION_DISP_COUNT.toString() + "/" + pTotalElements.toString());
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Add_FractionList", err);
    }
}
function PWM_Func_Rotate_FractionList(pTotalElem, pDirection) {
    try {
        var vTblHtml = "";
        var isChange = false;

        PWM_FRACTION_DISP_COUNT = parseInt(PWM_FRACTION_DISP_COUNT, 10);

        if (pDirection == 'more') {
            if (PWM_FRACTION_DISP_COUNT < (pTotalElem - 1)) {
                PWM_FRACTION_DISP_COUNT++;
                isChange = true;
            }
        }
        else {
            if (PWM_FRACTION_DISP_COUNT > 0) {
                PWM_FRACTION_DISP_COUNT--;
                isChange = true;
            }
        }

        if (isChange === true) {
            vTblHtml = PWM_FRACTION_DISP_COUNT.toString() + "/" + pTotalElem.toString();
            $('#pwmDvFractionList').text('');
            $('#pwmDvFractionList').text(vTblHtml);

            var iArray = PWM_GLOBAL_SLTDVAL.split("#").length;
            PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + vTblHtml;

            if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eBond || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e2DecimalPlaces)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2];
            else if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e3DecimalPlaces || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateHalfs || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateQuarters)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[3];

            PWM_GLOBAL_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL;
            var newValue = PWM_Func_DecimalPrice(PWM_GLOBAL_SLTDVAL, PWM_GLOBAL_SLTD_FORMAT);
            //$('#pwmDvTxtValue2').text(newValue.split('^')[1]);

            var txt2Val = newValue.split('^')[1];

            PWM_Func_Set_Txt2Value(txt2Val);

            if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
            }
            else {
                PWM_Func_CheckForZero();
            }

            PWM_Func_ShowFullValue_PriceWidget();
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Rotate_FractionList", err);
    }
}

function PWM_Func_Add_SubFractionList(pTotalElements) {
    try {

        pTotalElements = parseInt(pTotalElements, 10);

        var vListHtml = PWM_LIST_HTML;

        vListHtml = vListHtml.replace("[TITLE]", "Sub Fra");
        vListHtml = vListHtml.replace("[UPARROW_METHOD]", "PWM_Func_Rotate_SubFractionList(" + pTotalElements + ", 'more')");
        vListHtml = vListHtml.replace("[DIVID]", "pwmDvSubFractionList");
        vListHtml = vListHtml.replace("[DOWNARROW_METHOD]", "PWM_Func_Rotate_SubFractionList(" + pTotalElements + ", 'less')");

        $('#dvPwmFormatSection').append(vListHtml);

        $('#pwmDvSubFractionList').text(PWM_SUB_FRACTION_DISP_COUNT.toString());
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Add_SubFractionList", err);
    }
}
function PWM_Func_Rotate_SubFractionList(pTotalElem, pDirection) {
    try {

        var vTblHtml = "";
        var isChange = false;

        PWM_SUB_FRACTION_DISP_COUNT = parseInt(PWM_SUB_FRACTION_DISP_COUNT, 10);

        if (pDirection == 'more') {
            if (PWM_SUB_FRACTION_DISP_COUNT < (pTotalElem - 1)) {
                PWM_SUB_FRACTION_DISP_COUNT++;
                isChange = true;
            }
        }
        else {
            if (PWM_SUB_FRACTION_DISP_COUNT > 0) {
                PWM_SUB_FRACTION_DISP_COUNT--;
                isChange = true;
            }
        }

        if (isChange === true) {

            $('#pwmDvSubFractionList').text(PWM_SUB_FRACTION_DISP_COUNT.toString());

            var iArray = PWM_GLOBAL_SLTDVAL.split("#").length;

            if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eBond || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e2DecimalPlaces)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1] + "#" + PWM_SUB_FRACTION_DISP_COUNT;
            else if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e3DecimalPlaces || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateHalfs || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateQuarters)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2] + "#" + PWM_SUB_FRACTION_DISP_COUNT;
            else
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_SUB_FRACTION_DISP_COUNT;

            PWM_GLOBAL_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL;
            var newValue = PWM_Func_DecimalPrice(PWM_GLOBAL_SLTDVAL, PWM_GLOBAL_SLTD_FORMAT);
            //$('#pwmDvTxtValue2').text(newValue.split('^')[1]);

            var txt2Val = newValue.split('^')[1];

            PWM_Func_Set_Txt2Value(txt2Val);

            if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
            }
            else {
                PWM_Func_CheckForZero();
            }

            PWM_Func_ShowFullValue_PriceWidget();
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Rotate_SubFractionList", err);
    }
}

function PWM_Func_Add_1_DigitList() {
    try {

        var pTotalElements = 10;

        var vListHtml = PWM_LIST_HTML;

        vListHtml = vListHtml.replace("[TITLE]", ".1 Dgt");
        vListHtml = vListHtml.replace("[UPARROW_METHOD]", "PWM_Func_Rotate_1_DigitList(" + pTotalElements + ", 'more')");
        vListHtml = vListHtml.replace("[DIVID]", "pwmDv1DigitList");
        vListHtml = vListHtml.replace("[DOWNARROW_METHOD]", "PWM_Func_Rotate_1_DigitList(" + pTotalElements + ", 'less')");

        $('#dvPwmFormatSection').append(vListHtml);

        $('#pwmDv1DigitList').text(PWM_1_DIGIT_DISP_COUNT.toString());
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Add_1_DigitList", err);
    }
}
function PWM_Func_Rotate_1_DigitList(pTotalElem, pDirection) {
    try {
        var vTblHtml = "";
        var isChange = false;

        PWM_1_DIGIT_DISP_COUNT = parseInt(PWM_1_DIGIT_DISP_COUNT);

        if (pDirection == 'more') {
            if (PWM_1_DIGIT_DISP_COUNT < (pTotalElem - 1)) {
                PWM_1_DIGIT_DISP_COUNT++;
                isChange = true;
            }
        }
        else {
            if (PWM_1_DIGIT_DISP_COUNT > 0) {
                PWM_1_DIGIT_DISP_COUNT--;
                isChange = true;
            }
        }

        if (isChange === true) {

            $('#pwmDv1DigitList').text('');
            $('#pwmDv1DigitList').text(PWM_1_DIGIT_DISP_COUNT.toString());

            IsChange = 'TRUE';

            var iArray = PWM_GLOBAL_SLTDVAL.split("#").length;
            PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_1_DIGIT_DISP_COUNT;

            if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eBond || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e2DecimalPlaces)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2];
            else if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e3DecimalPlaces || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateHalfs || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateQuarters)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[3];

            PWM_GLOBAL_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL;
            var newValue = PWM_Func_DecimalPrice(PWM_GLOBAL_SLTDVAL, PWM_GLOBAL_SLTD_FORMAT);
            //$('#pwmDvTxtValue2').text(newValue.split('^')[1]);

            var txt2Val = newValue.split('^')[1];

            PWM_Func_Set_Txt2Value(txt2Val);

            if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
            }
            else {
                PWM_Func_CheckForZero();
            }

            PWM_Func_ShowFullValue_PriceWidget();
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Rotate_1_DigitList", err);
    }
}

function PWM_Func_Add_01_DigitList() {
    try {
        var pTotalElements = 10;

        var vListHtml = PWM_LIST_HTML;

        vListHtml = vListHtml.replace("[TITLE]", ".01 Dgt");
        vListHtml = vListHtml.replace("[UPARROW_METHOD]", "PWM_Func_Rotate_01_DigitList(" + pTotalElements + ", 'more')");
        vListHtml = vListHtml.replace("[DIVID]", "pwmDv01DigitList");
        vListHtml = vListHtml.replace("[DOWNARROW_METHOD]", "PWM_Func_Rotate_01_DigitList(" + pTotalElements + ", 'less')");

        $('#dvPwmFormatSection').append(vListHtml);

        $('#pwmDv01DigitList').text(PWM_01_DIGIT_DISP_COUNT.toString());

    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Add_01_DigitList", err);
    }
}
function PWM_Func_Rotate_01_DigitList(pTotalElem, pDirection) {
    try {
        var vTblHtml = "";
        var isChange = false;

        PWM_01_DIGIT_DISP_COUNT = parseInt(PWM_01_DIGIT_DISP_COUNT, 10);

        if (pDirection == 'more') {
            if (PWM_01_DIGIT_DISP_COUNT < (pTotalElem - 1)) {
                PWM_01_DIGIT_DISP_COUNT++;
                isChange = true;
            }
        }
        else {
            if (PWM_01_DIGIT_DISP_COUNT > 0) {
                PWM_01_DIGIT_DISP_COUNT--;
                isChange = true;
            }
        }

        if (isChange === true) {
            $('#pwmDv01DigitList').text('');
            $('#pwmDv01DigitList').text(PWM_01_DIGIT_DISP_COUNT.toString());

            var iArray = PWM_GLOBAL_SLTDVAL.split("#").length;

            if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eBond || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e2DecimalPlaces)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1] + "#" + PWM_01_DIGIT_DISP_COUNT;
            else if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e3DecimalPlaces || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateHalfs || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateQuarters)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1] + "#" + PWM_01_DIGIT_DISP_COUNT + "#" + PWM_GLOBAL_SLTDVAL.split("#")[3];
            else
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_01_DIGIT_DISP_COUNT;

            PWM_GLOBAL_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL;
            var newValue = PWM_Func_DecimalPrice(PWM_GLOBAL_SLTDVAL, PWM_GLOBAL_SLTD_FORMAT);
            //$('#pwmDvTxtValue2').text(newValue.split('^')[1]);
            var txt2Val = newValue.split('^')[1];

            PWM_Func_Set_Txt2Value(txt2Val);

            if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
            }
            else {
                PWM_Func_CheckForZero();
            }

            PWM_Func_ShowFullValue_PriceWidget();
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Rotate_01_DigitList", err);
    }
}

function PWM_Func_Add_001_DigitList() {
    try {
        var pTotalElements = 10;

        var vListHtml = PWM_LIST_HTML;

        vListHtml = vListHtml.replace("[TITLE]", ".001 Dgt");
        vListHtml = vListHtml.replace("[UPARROW_METHOD]", "PWM_Func_Rotate_001_DigitList(" + pTotalElements + ", 'more')");
        vListHtml = vListHtml.replace("[DIVID]", "pwmDv001DigitList");
        vListHtml = vListHtml.replace("[DOWNARROW_METHOD]", "PWM_Func_Rotate_001_DigitList(" + pTotalElements + ", 'less')");

        $('#dvPwmFormatSection').append(vListHtml);

        $('#pwmDv001DigitList').text(PWM_001_DIGIT_DISP_COUNT.toString());

    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Add_001_DigitList", err);
    }
}
function PWM_Func_Rotate_001_DigitList(pTotalElem, pDirection) {
    try {
        var vTblHtml = "";
        var isChange = false;

        PWM_001_DIGIT_DISP_COUNT = parseInt(PWM_001_DIGIT_DISP_COUNT, 10);

        if (pDirection == 'more') {
            if (PWM_001_DIGIT_DISP_COUNT < (pTotalElem - 1)) {
                PWM_001_DIGIT_DISP_COUNT++;
                isChange = true;
            }
        }
        else {
            if (PWM_001_DIGIT_DISP_COUNT > 0) {
                PWM_001_DIGIT_DISP_COUNT--;
                isChange = true;
            }
        }

        if (isChange === true) {
            $('#pwmDv001DigitList').text('');
            $('#pwmDv001DigitList').text(PWM_001_DIGIT_DISP_COUNT.toString());

            var iArray = PWM_GLOBAL_SLTDVAL.split("#").length;

            if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eBond || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e2DecimalPlaces)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1] + "#" + PWM_001_DIGIT_DISP_COUNT;
            else if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e3DecimalPlaces || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateHalfs || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateQuarters)
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2] + "#" + PWM_001_DIGIT_DISP_COUNT;
            else
                PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_SLTDVAL.split("#")[0] + "#" + PWM_001_DIGIT_DISP_COUNT;

            PWM_GLOBAL_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL;
            var newValue = PWM_Func_DecimalPrice(PWM_GLOBAL_SLTDVAL, PWM_GLOBAL_SLTD_FORMAT);
            //$('#pwmDvTxtValue2').text(newValue.split('^')[1]);
            var txt2Val = newValue.split('^')[1];

            PWM_Func_Set_Txt2Value(txt2Val);

            if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
            }
            else {
                PWM_Func_CheckForZero();
            }

            PWM_Func_ShowFullValue_PriceWidget();
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Rotate_001_DigitList", err);
    }
}

function PWM_Function_checkKey(pKeyCode) {
    try {
        //event = event || window.event //For IE        
        //var keycode = event.keyCode ? event.keyCode : event.which;
        //alert(pKeyCode);
        var keycode = pKeyCode;
        var vAllow = false;

        if (keycode == $.keyCode.UP) {

            var _val = $.trim($('#pwmTxtPrice').val());

            if (PWM_Func_CheckNaNValue(_val)) {
                PWM_PRICE_DISP_COUNT = parseInt(_val, 10);
                PWM_GLOBAL_PRICE = parseInt(_val, 10);
            }
            else {
                PWM_PRICE_DISP_COUNT = parseInt(PWM_GLOBAL_PRICE, 10);
            }

            PWM_Func_Rotate_PriceElements('more');
            return true;
        }
        else if (keycode == $.keyCode.DOWN) {

            var _val = $.trim($('#pwmTxtPrice').val());

            if (PWM_Func_CheckNaNValue(_val)) {
                PWM_PRICE_DISP_COUNT = parseInt(_val, 10);
                PWM_GLOBAL_PRICE = parseInt(_val, 10);
            }
            else {
                PWM_PRICE_DISP_COUNT = parseInt(PWM_GLOBAL_PRICE, 10);
            }

            PWM_Func_Rotate_PriceElements('less');
            return true;
        }
        else if (keycode == $.keyCode.ENTER) {

            var _val = $.trim($('#pwmTxtPrice').val());

            if (PWM_Func_CheckNaNValue(_val)) {
                PWM_PRICE_DISP_COUNT = parseInt(_val, 10);
                PWM_GLOBAL_PRICE = parseInt(_val, 10);
            }
            else {
                PWM_PRICE_DISP_COUNT = parseInt(PWM_GLOBAL_PRICE, 10);
            }

            PWM_Func_PriceChange(PWM_PRICE_DISP_COUNT);

            return true;
        }
        else if (GEN_Func_IsDefaultAllowedKeys(keycode)) {
            return true;
        }

        vAllow = GEN_Func_IsNumberKey(keycode);

        if (vAllow === false)
            return false;
        //event.returnValue = false;
        else
            return true;
        //event.returnValue == true;

    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Function_checkKey", err);
    }
}

function PWM_Func_CheckNaNValue(pVal) {
    try {
        var _val = pVal;
        if (_val === '' || _val === null) {
            return false;
        }
        else if (isNaN(_val)) {
            return false;
        }
        else {
            return true;
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_CheckPriceBoxValue", err);
    }
}

function PWM_Func_BindEvents() {
    //$('#' + pwmWIDGET_DIV_ID).blur(PWM_Func_Close_PriceWidget());

    $('#pwmTxtPrice').click(function (e) {
        $(this).removeClass('pwmStaticBox');
        $(this).addClass('pwmEditBox');
        var _val = $.trim($(this).val());
        //if (_val !== '' && _val !== null) {
        if (PWM_Func_CheckNaNValue(_val)) {
            PWM_GLOBAL_PRICE = parseInt(_val, 10);
        }
    });

    //$('#pwmTxtPrice').keydown(function (e) {
    //    var _key = e.keyCode ? e.keyCode : e.which;
    //    var isOk = PWM_Function_checkKey(_key);
    //    return isOk;
    //});

    $('#pwmTxtPrice').numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });

    $('#pwmTxtPrice').blur(function () {
        $(this).removeClass('pwmEditBox');
        $(this).addClass('pwmStaticBox');

        var _val = $.trim($('#pwmTxtPrice').val());
        if (_val === '' || _val === null) {
            PWM_Func_PriceChange(parseInt(PWM_GLOBAL_PRICE, 10));
        }
        else {
            PWM_Func_PriceChange(parseInt($('#pwmTxtPrice').val(), 10));
        }

        //var _val = $.trim($(this).val());        
        //if (PWM_Func_CheckNaNValue(_val)) {
        //    PWM_GLOBAL_PRICE = parseInt(_val, 10);
        //}
    });

    $("#pwmTxtPrice").bind('paste', function () {
        return false;
    });

    $('#pwmDvFormatList ul li').on('click', function () {
        PWM_Func_On_Format_Selection_Change($(this));
    });

    //$('#pwmDvFormatList').bind('blur', function (index) {
    //    PWM_Func_HideFormatOptionList();
    //});

    $('#dvPwmTitle').bind('click', function (index) {
        PWM_Func_HideFormatOptionList();
    });
}

function PWM_Func_DisplayFormatOptions(pElementId) {

    if ($('#pwmDvFormatList').css('display') == 'none') {

        var _vListHeight = $('#pwmDvFormatList').outerHeight(true);
        var _eleHeight = $('#pwmDvSelectedFormatDisplay').outerHeight(true);
        var eleOffSet = $('#pwmDvSelectedFormatDisplay').offset();
        $('#pwmDvFormatList').css('display', 'block');

        eleOffSet.top = (eleOffSet.top + _eleHeight);

        $('#pwmDvFormatList').offset(eleOffSet);

        $('#pwmDvFormatList').focus();
    }
    else {
        PWM_Func_HideFormatOptionList();
    }
}

function PWM_Func_HideFormatOptionList() {
    var eleOffSet = $('#pwmDvFormatList').offset();
    eleOffSet.top = 0;
    eleOffSet.left = 0;
    $('#pwmDvFormatList').offset(eleOffSet);
    $('#pwmDvFormatList').css('display', 'none');
}

function PWM_Func_PriceChange(pNewPrice) {
    try {
        $('#pwmTxtPrice').val('');
        $('#pwmTxtPrice').val(pNewPrice.toString());

        var iArray = PWM_GLOBAL_SLTDVAL.split("#").length;
        PWM_GLOBAL_TEMP_SLTDVAL = pNewPrice.toString() + "#" + PWM_GLOBAL_SLTDVAL.split("#")[1];

        if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eBond || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e2DecimalPlaces)
            PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2];
        else if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e3DecimalPlaces || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateHalfs || PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.eFurtureRateQuarters)
            PWM_GLOBAL_TEMP_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL + "#" + PWM_GLOBAL_SLTDVAL.split("#")[2] + "#" + PWM_GLOBAL_SLTDVAL.split("#")[3];

        PWM_GLOBAL_SLTDVAL = PWM_GLOBAL_TEMP_SLTDVAL;
        var newValue = PWM_Func_DecimalPrice(PWM_GLOBAL_SLTDVAL, PWM_GLOBAL_SLTD_FORMAT);
        //$('#pwmDvTxtValue2').text(newValue.split('^')[1]);
        var txt2Val = newValue.split('^')[1];

        PWM_Func_Set_Txt2Value(txt2Val);

        if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
        }
        else {
            PWM_Func_CheckForZero();
        }

        PWM_Func_ShowFullValue_PriceWidget();
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_PriceChange", err);
    }
}

function PWM_Func_Rotate_PriceElements(pDirection) {
    try {
        var vTblHtml = "";
        var isChange = false;

        var _val = $.trim($('#pwmTxtPrice').val());
        if (PWM_Func_CheckNaNValue(_val)) {
            if (PWM_PRICE_DISP_COUNT.toString() != _val) {
                PWM_PRICE_DISP_COUNT = parseInt(_val, 10);
            }
        }

        PWM_PRICE_DISP_COUNT = parseInt(PWM_PRICE_DISP_COUNT, 10);

        $('#pwmTxtPrice').val(PWM_PRICE_DISP_COUNT.toString());

        if (pDirection == 'more') {
            PWM_PRICE_DISP_COUNT++;
            isChange = true;
        }
        else {
            if (PWM_PRICE_DISP_COUNT > 0) {
                PWM_PRICE_DISP_COUNT--;
                isChange = true;
            }
        }

        if (isChange === true) {
            PWM_Func_PriceChange(PWM_PRICE_DISP_COUNT);
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Rotate_PriceElements", err);
    }
}

function PWM_Func_ToggleNegative() {
    try {
        pwmIS_NEGATIVE = !pwmIS_NEGATIVE;
        //if ($('#dvPwmNegative').hasClass('pwmPlus')) {
        if (pwmIS_NEGATIVE === true) {
            $('#dvPwmNegative').removeClass('pwmPlus');
            $('#dvPwmNegative').addClass('pwmMinus');
        }
        else {
            $('#dvPwmNegative').removeClass('pwmMinus');
            $('#dvPwmNegative').addClass('pwmPlus');
        }

        var val1 = $.trim($('#pwmDvTxtValue1').text());
        var val2 = $.trim(PWM_GLOBAL_TXTVAL_2_CALC.toString());

        if (val1 != "0" || val2 != "0.000000") {

            if (val1 == "0" || (val2 == "0.000000" || val2 == "0" || val2 == "0.0" || val2 == "0.00" || val2 == "0.000")) {
                //pwmIS_NEGATIVE = !pwmIS_NEGATIVE;
                if (pwmIS_NEGATIVE === true) {
                    PWM_GLOBAL_TXTVAL_2 = "-" + PWM_GLOBAL_TXTVAL_2;
                }
                else {

                }
                return;
            }

            if (pwmIS_NEGATIVE === false) {
                val1 = val1.replace('-', '');
                val2 = val2.replace('-', '');
            }
            else {
                val1 = "-" + val1;
                val2 = "-" + val2;
            }

            //$('#pwmDvTxtValue1').text(val1);
            PWM_Func_Set_Txt1Value(val1);
            PWM_GLOBAL_TXTVAL_1 = val1;
            //$('#pwmDvTxtValue2').text(val2);
            PWM_GLOBAL_TXTVAL_2 = val2;

            PWM_Func_Set_Txt2Value(val2);

            //pwmIS_NEGATIVE = !pwmIS_NEGATIVE;
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_ToggleNegative", err);
    }
}

function PWM_Func_On_Format_Selection_Change(pLi) {

    var disp = $(pLi).text();
    $('#pwmDvSelectedFormatDisplay').text(disp);
    var pValue = $(pLi).attr('id').replace("pwmLi_", "");

    PWM_GLOBAL_SLTD_FORMAT = pValue;

    PWM_Func_HideFormatOptionList();

    PWM_Func_START();
}

function PWM_Func_Set_Txt2Value(pVal) {
    try {

        PWM_GLOBAL_TXTVAL_2_CALC = pVal;

        var vLength = pVal.toString().length;
        var chkWidth = $('#pwmDvTxtValue2').width();

        var vDispLimit = parseInt((chkWidth / 8) - 1, 10);

        if (vLength > vDispLimit) {

            var vDispVal = PWM_GLOBAL_TXTVAL_2_CALC.toString();
            $('#pwmDvTxtValue2').attr('title', vDispVal).attr('name', vDispVal);

            //if (chkWidth < (PWM_TXT2_DEFAULT_WIDTH - 20)) {
            //    var _diff = parseInt((PWM_TXT2_DEFAULT_WIDTH - chkWidth), 10);
            //    vDispLimit -= Math.floor(_diff / 8);
            //    vDispVal = vDispVal.substring(0, vDispLimit) + "..";
            //}
            //else {
            vDispVal = vDispVal.substring(0, vDispLimit) + "..";
            //}
            //vDispVal = vDispVal.substring(0, vDispLimit);

            pVal = vDispVal;
        }
        else {
            $('#pwmDvTxtValue2').attr('title', '');
        }

        $('#pwmDvTxtValue2').text(pVal);

    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Set_Txt2Value", err);
    }
}

function PWM_Func_Set_Txt1Value(pVal) {
    try {
        var vLength = pVal.toString().length;
        var vDispLimit = 8;

        if (vLength > vDispLimit) {

            var vIncWidth = parseInt((vLength - vDispLimit) * 5, 10);

            PWM_Func_Resize_ValueBoxes(vIncWidth, "true");
        }
        else {
            PWM_Func_Resize_ValueBoxes(0, "false");
        }

        $('#pwmDvTxtValue1').text(pVal);
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Set_Txt1Value", err);
    }
}

function PWM_Func_Resize_ValueBoxes(pWidth, pIsRestore) {

    var vWdth_1 = PWM_TXT1_DEFAULT_WIDTH;
    var vWdth_2 = PWM_TXT2_DEFAULT_WIDTH;

    if (pIsRestore == "true") {
        vWdth_1 += pWidth;
        vWdth_2 -= pWidth;

        var _num = $('#pwmDvTxtValue2').attr('name');
        $('#pwmDvTxtValue2').attr('title', _num);
    }
    else {
        $('#pwmDvTxtValue2').attr('title', '');
    }

    $('.pwmDvTop_2').animate({ width: vWdth_2 + 'px' }, 100);
    $('.pwmDvTop_1').animate({ width: vWdth_1 + 'px' }, 100);
}

function PWM_Func_CheckForZero() {
    try {

        //if (pwmIS_NEGATIVE === true) {
        //    PWM_Func_Set_Txt2Value("-" + PWM_GLOBAL_TXTVAL_2_CALC);            
        //}

        if (PWM_GLOBAL_TXTVAL_2_CALC != "0" && PWM_GLOBAL_TXTVAL_2_CALC != "0.00" && PWM_GLOBAL_TXTVAL_2_CALC != "0.000" && PWM_GLOBAL_TXTVAL_2_CALC != "0.0") {
            PWM_Func_Set_Txt2Value("-" + PWM_GLOBAL_TXTVAL_2_CALC);
            pwmIS_NEGATIVE = true;
        }
        else {
            PWM_Func_Set_Txt2Value(PWM_GLOBAL_TXTVAL_2_CALC);
            PWM_Func_ToggleNegative();
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_CheckForZero", err);
    }
}

/* ==NUMBER FUNCTIONS== */

var PWM_NumberFormats = {

    "eHalfs": 0,                    ///1/2
    "eQuarters": 1,                 ///1/4   
    "eEighths": 2,                  ///1/8                                      
    "eSixteenths": 3,               ///1/16

    "eThirtySeconds": 4,            ///1/32
    "eSixtyFourths": 5,             ///1/64
    "eOneTwentyEights": 6,          ///1/128
    "eTwoFiftySixths": 7,           ///1/256
    "eBond": 8,                     ///1/32+

    "eQuartersReduced": 9,          ///1/4R
    "eEighthsReduced": 10,          ///1/8R
    "eSixteenthsReduced": 11,       ///1/16R

    "eThirtySecondsReduced": 12,    ///1/32R 
    "eSixtyFourthsReduced": 13,     ///1/64R
    "eOneTwentyEightsReduced": 14,  ///1/128R
    "eTwoFiftySixthsReduced": 15,   ///1/256R

    "e0DecimalPlaces": 16,          ///1

    "e1DecimalPlace": 17,           ///0.1
    "e2DecimalPlaces": 18,          ///0.01
    "e3DecimalPlaces": 19,          ///0.001  
    "eFurtureRateHalfs": 20,        ///0.00+(1/2)
    "eFurtureRateQuarters": 21      ///0.00+(1/4)
};

function PWM_Func_ConvertFormatTypeIndex(FormatIndex) {

    if (FormatIndex == 1300 || FormatIndex == 5300 || FormatIndex == 1320 || FormatIndex == 5320 || FormatIndex == 1600 || FormatIndex == 1620 || FormatIndex == 5600 || FormatIndex == 5620)
        return 0;
    else if (FormatIndex == 1301 || FormatIndex == 5301 || FormatIndex == 1321 || FormatIndex == 5321 || FormatIndex == 1601 || FormatIndex == 1621 || FormatIndex == 5601 || FormatIndex == 5621)
        return 1;
    else if (FormatIndex == 1302 || FormatIndex == 5302 || FormatIndex == 1322 || FormatIndex == 5322)
        return 2;
    else if (FormatIndex == 1303 || FormatIndex == 5303 || FormatIndex == 1323 || FormatIndex == 5323)
        return 3;
    else if (FormatIndex == 1304 || FormatIndex == 5304 || FormatIndex == 1324 || FormatIndex == 5324)
        return 4;
    else if (FormatIndex == 1305 || FormatIndex == 5305 || FormatIndex == 1325 || FormatIndex == 5325)
        return 5;
    else if (FormatIndex == 1306 || FormatIndex == 5306 || FormatIndex == 1326 || FormatIndex == 5326)
        return 6;
    else if (FormatIndex == 1307 || FormatIndex == 5307 || FormatIndex == 1327 || FormatIndex == 5327)
        return 7;
    else if (FormatIndex == 1504 || FormatIndex == 5504)
        return 8;
    else if (FormatIndex == 1401 || FormatIndex == 5401 || FormatIndex == 1421 || FormatIndex == 5421)
        return 9;
    else if (FormatIndex == 1402 || FormatIndex == 5402 || FormatIndex == 1422 || FormatIndex == 5422)
        return 10;
    else if (FormatIndex == 1403 || FormatIndex == 5403 || FormatIndex == 1423 || FormatIndex == 5423)
        return 11;
    else if (FormatIndex == 1404 || FormatIndex == 5404 || FormatIndex == 1424 || FormatIndex == 5424)
        return 12;
    else if (FormatIndex == 1405 || FormatIndex == 5405 || FormatIndex == 1425 || FormatIndex == 5425)
        return 13;
    else if (FormatIndex == 1406 || FormatIndex == 5406 || FormatIndex == 1426 || FormatIndex == 5426)
        return 14;
    else if (FormatIndex == 1407 || FormatIndex == 5407 || FormatIndex == 1427 || FormatIndex == 5427)
        return 15;
    else if (FormatIndex == 1200 || FormatIndex == 5200)
        return 16;
    else if (FormatIndex == 1201 || FormatIndex == 5201)
        return 17;
    else if (FormatIndex == 1202 || FormatIndex == 5202)
        return 18;
    else if (FormatIndex == 1203 || FormatIndex == 5203)
        return 19;
    else if (FormatIndex == 1220 || FormatIndex == 5220)
        return 20;
    else if (FormatIndex == 1221 || FormatIndex == 5221)
        return 21;
    else
        return 0;

}

///param1 int, param2  int
function PWM_Func_GetReducedFraction(Numerator, Denominator) {
    try {
        //int
        var i = 0;
        for (i = Denominator; i >= 1; i--) {
            if (Numerator % i == 0 && Denominator % i == 0) {
                break;
            }
        }
        Numerator = Numerator / i;
        Denominator = Denominator / i;
        return Numerator + "/" + Denominator;
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_GetReducedFraction", err);
    }
}

/// <summary>
/// Code for converting decimal price to given price format. Pass Number format as per above enum (0 t0 21)
/// Usage - PWM_Func_ParsePriceForWidget(99.9645,8) ;
/// </summary>
///param1 double, param2 int, param3 int       
function PWM_Func_ParsePriceForWidget(DecPrice, Format, ShowFullyFormatted) {
    try {
        //int
        var nPrice, nFrac1, nFrac2, nFrac3, nBaseUnit, min = 1;
        nPrice = parseInt((Math.floor(Math.abs(DecPrice))));
        nFrac1 = nFrac2 = nFrac3 = 0;
        nBaseUnit = Format;
        if (nBaseUnit == 8) // related to 1/32+ format=eBond
        {
            nFrac1 = parseInt(((Math.abs(DecPrice) - nPrice) * 256 + 0.5));
            if (nFrac1 == 256) {
                nFrac1 = 0;
                nPrice++;
            }
            nFrac2 = nFrac1 % 8;
            nFrac1 /= 8;

            //Show Output as  per selected Format
            if (ShowFullyFormatted == 0) {
                return PWM_Func_ShowFullOutput_PriceWidget(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
            else {
                return PWM_Func_ShowPartitionedOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
        }
        else {
            //Set Min as per Number Format
            if (nBaseUnit == parseInt(PWM_NumberFormats.eBond))
                min = 32;
            else if (nBaseUnit >= parseInt(PWM_NumberFormats.eHalfs) && nBaseUnit <= parseInt(PWM_NumberFormats.eTwoFiftySixthsReduced))
                min = (1 << ((nBaseUnit % 8) + 1));
            else if (nBaseUnit == 17)
                min = 10;
            else if (nBaseUnit == 18)
                min = 100;
            else if (nBaseUnit == 19)
                min = 1000;
            else if (nBaseUnit == 20) {
                min = 1000;
            }
            else if (nBaseUnit == 21) {
                min = 10000;
            }

            nFrac1 = parseInt(((Math.abs(DecPrice) - nPrice) * min + 0.5));
            if (nFrac1 == min) {
                nFrac1 = 0;
                nPrice++;
            }
            if (nBaseUnit == 18) {
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 19) {
                nFrac3 = nFrac1 % 10;
                nFrac1 /= 10;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 20) {
                nFrac3 = (nFrac1 % 10) / 5;
                nFrac1 /= 10;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 21) {
                nFrac3 = (nFrac1 % 100) / 25;
                nFrac1 /= 100;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }

            //Show Output as  per selected Format
            if (ShowFullyFormatted == 0) {
                return PWM_Func_ShowFullOutput_PriceWidget(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
            else {
                return PWM_Func_ShowPartitionedOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_ParsePriceForWidget", err);
    }
}

///param1 double, param2 int, param3 int, param4 int, param5 int, param6 int
function PWM_Func_ShowPartitionedOutput(DecPrice, Format, nPrice, nFrac1, nFrac2, nFrac3) {
    try {

        if (Format == 0) //1/2=eHalfs
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/2");
        }
        else if (Format == 1) //1/4=eQuarters
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/4");
        }
        else if (Format == 2) //1/8=eEighths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/8");
        }
        else if (Format == 3) //1/16=eSixteenths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/16");
        }
        else if (Format == 4) //1/32=eThirtySeconds
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/32");
        }
        else if (Format == 5) //1/64=eSixtyFourths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/64");
        }
        else if (Format == 6) //1/128=eOneTwentyEights
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/128");
        }
        else if (Format == 7) //1/256=eTwoFiftySixths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/256");
        }
        else if (Format == 8) {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/32" + "#" + nFrac2);
        }
        else if (Format == 9) //1/4R=eQuartersReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/4");
        }
        else if (Format == 10) //1/8R=eEighthsReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/8");
        }
        else if (Format == 11) //1/16R=eSixteenthsReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/16");
        }
        else if (Format == 12) //1/32R=eThirtySecondsReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/32");
        }
        else if (Format == 13) //1/64R=eSixtyFourthsReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/64");
        }
        else if (Format == 14) //1/128R=eOneTwentyEightsReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/128");
        }
        else if (Format == 15) //1/256R=eTwoFiftySixthsReduced
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "/256");
        }
        else if (Format == 16) //1=e0DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
        }
        else if (Format == 17) //0.1=e1DecimalPlace
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1);
        }
        else if (Format == 18) //0.01=e2DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "#" + nFrac2);
        }
        else if (Format == 19) //0.001=e3DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "#" + nFrac2 + "#" + nFrac3);
        }
        else if (Format == 20) //0.001=eFurtureRateHalfs
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "#" + nFrac2 + "#" + nFrac3);
        }
        else if (Format == 21) //0.001=eFurtureRateQuarters
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "#" + nFrac1 + "#" + nFrac2 + "#" + nFrac3);
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_ShowPartitionedOutput", err);
    }
}

///param1 double, param2 int, param3 int, param4 int, param5 int, param6 int
function PWM_Func_ShowFullOutput_PriceWidget(DecPrice, Format, nPrice, nFrac1, nFrac2, nFrac3) {
    try {
        //Show Output as  per selected Format
        if (Format == 0) //1/2=eHalfs
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/2");
        }
        else if (Format == 1) //1/4=eQuarters
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 2) //1/8=eEighths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "'" + nFrac1);
        }
        else if (Format == 3) //1/16=eSixteenths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''00");
            else if (nFrac1 > 0 && nFrac1 < 10)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''0" + nFrac1);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''" + nFrac1);
        }
        else if (Format == 4) //1/32=eThirtySeconds
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-00");
            else if (nFrac1 > 0 && nFrac1 < 10)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-0" + nFrac1);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1);
        }
        else if (Format == 5) //1/64=eSixtyFourths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 6) //1/128=eOneTwentyEights
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 7) //1/256=eTwoFiftySixths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/256");
        }
        else if (Format == 8) {
            if (nFrac2 == 4) // if 2nd fraction is 4 then display "+" after 1st fraction
            {
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "+");
            }
            else if (nFrac2 == 0) //if 2nd fraction is 0 then no need to display 2nd fraction
            {
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()));
            }
            else //show 2nd fraction after 1st fraction and dot
            {
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "." + nFrac2);
            }
        }
        else if (Format == 9) //1/4R=eQuartersReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 == 2)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 4));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 10) //1/8R=eEighthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 8));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/8");
        }
        else if (Format == 11) //1/16R=eSixteenthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 16));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/16");
        }
        else if (Format == 12) //1/32R=eThirtySecondsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 32));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/32");
        }
        else if (Format == 13) //1/64R=eSixtyFourthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 64));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 14) //1/128R=eOneTwentyEightsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 128));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 15) //1/256R=eTwoFiftySixthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 256));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/256");
        }
        else if (Format == 16) //1=e0DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
        }
        else if (Format == 17) //0.1=e1DecimalPlace
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1);
        }
        else if (Format == 18) //0.01=e2DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
        }
        else if (Format == 19) //0.001=e3DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + nFrac3);
        }
        else if (Format == 20) //0.001=eFurtureRateHalfs
        {
            if (nFrac3 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "+");
        }
        else if (Format == 21) //0.001=eFurtureRateQuarters
        {
            if (nFrac3 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
            else if (nFrac3 == 2)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "+");
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "." + nFrac3);
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_ShowFullOutput_PriceWidget", err);
    }
}

/////Code for coverting given partitioned formatted price into fully formatted & decimal format (# separated).
/////Pass Price  as 8#2#7#3 for 0.001 format
/////param1 string, param2 int
function PWM_Func_DecimalPrice(Price, Format) {
    try {

        ///int
        var nPrice = 0, nFrac1 = 0, nFrac2 = 0, nFrac3 = 0;
        //Decimal
        //var dPrice = 0M;
        var dPrice = 0;
        //string[]
        var PriceParts = Price.toString().split('#');

        if (PriceParts.length > 0) {
            for (var index = 0; index < PriceParts.length; index++) {
                if (index == 0)
                    nPrice = parseInt(PriceParts[index]);
                else if (index == 1) {
                    if (Format < 16)
                        nFrac1 = parseInt(PriceParts[index].substring(0, PriceParts[index].indexOf("/")));
                    else
                        nFrac1 = parseInt(PriceParts[index]);
                }
                else if (index == 2)
                    nFrac2 = parseInt(PriceParts[index]);
                else if (index == 3)
                    nFrac3 = parseInt(PriceParts[index]);
            }
            dPrice = parseFloat(nPrice);
            if (Format == 0) //1/2=eHalfs
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else
                    return (nPrice + "-" + nFrac1 + "/2" + "^" + (dPrice + parseFloat(nFrac1) / 2));
            }
            else if (Format == 1) //1/4=eQuarters
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else
                    return (nPrice + "-" + nFrac1 + "/4" + "^" + (dPrice + parseFloat(nFrac1) / 4));
            }
            else if (Format == 2) //1/8=eEighths
            {
                return (nPrice + "'" + nFrac1 + "^" + (dPrice + parseFloat(nFrac1) / 8));
            }
            else if (Format == 3) //1/16=eSixteenths
            {
                if (nFrac1 == 0)
                    return (nPrice + "''00" + "^" + dPrice);
                else if (nFrac1 > 0 && nFrac1 < 10)
                    return (nPrice + "''0" + nFrac1 + "^" + (dPrice + parseFloat(nFrac1) / 16));
                else
                    return (nPrice + "''" + nFrac1 + "^" + (dPrice + parseFloat(nFrac1) / 16));
            }
            else if (Format == 4) //1/32=eThirtySeconds
            {
                if (nFrac1 == 0)
                    return (nPrice + "-00" + "^" + dPrice);
                else if (nFrac1 > 0 && nFrac1 < 10)
                    return (nPrice + "-0" + nFrac1 + "^" + (dPrice + parseFloat(nFrac1) / 32));
                else
                    return (nPrice + "-" + nFrac1 + "^" + (dPrice + parseFloat(nFrac1) / 32));
            }
            else if (Format == 5) //1/64=eSixtyFourths
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else
                    return (nPrice + "-" + nFrac1 + "/64" + "^" + (dPrice + parseFloat(nFrac1) / 64));
            }
            else if (Format == 6) //1/128=eOneTwentyEights
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else
                    return (nPrice + "-" + nFrac1 + "/128" + "^" + (dPrice + parseFloat(nFrac1) / 128));
            }
            else if (Format == 7) //1/256=eTwoFiftySixths
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else
                    return (nPrice + "-" + nFrac1 + "/256" + "^" + (dPrice + parseFloat(nFrac1) / 256));
            }
            else if (Format == 8) {
                if (nFrac2 == 4) // if 2nd fraction is 4 then display "+" after 1st fraction
                {
                    return (nPrice + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "+" + "^" + (dPrice + parseFloat(nFrac1) / 32 + parseFloat(0.031250 / 8 * nFrac2)));
                }
                else if (nFrac2 == 0) //if 2nd fraction is 0 then no need to display 2nd fraction
                {
                    return (nPrice + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "^" + (dPrice + parseFloat(nFrac1) / 32));
                }
                else //show 2nd fraction after 1st fraction and dot
                {
                    return (nPrice + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "." + nFrac2 + "^" + (dPrice + parseFloat(nFrac1) / 32 + parseFloat(0.031250 / 8 * nFrac2)));
                }
            }
            else if (Format == 9) //1/4R=eQuartersReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 == 2)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 4) + "^" + (dPrice + parseFloat(nFrac1) / 4));
                else
                    return (nPrice + "-" + nFrac1 + "/4" + "^" + (dPrice + parseFloat(nFrac1) / 4));
            }
            else if (Format == 10) //1/8R=eEighthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 8) + "^" + (dPrice + parseFloat(nFrac1) / 8));
                else
                    return (nPrice + "-" + nFrac1 + "/8" + "^" + (dPrice + parseFloat(nFrac1) / 8));
            }
            else if (Format == 11) //1/16R=eSixteenthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 16) + "^" + (dPrice + parseFloat(nFrac1) / 16));
                else
                    return (nPrice + "-" + nFrac1 + "/16" + "^" + (dPrice + parseFloat(nFrac1) / 16));
            }
            else if (Format == 12) //1/32R=eThirtySecondsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 32) + "^" + (dPrice + parseFloat(nFrac1) / 32));
                else
                    return (nPrice + "-" + nFrac1 + "/32" + "^" + (dPrice + parseFloat(nFrac1) / 32));
            }
            else if (Format == 13) //1/64R=eSixtyFourthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 64) + "^" + (dPrice + parseFloat(nFrac1) / 64));
                else
                    return (nPrice + "-" + nFrac1 + "/64" + "^" + (dPrice + parseFloat(nFrac1) / 64));
            }
            else if (Format == 14) //1/128R=eOneTwentyEightsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 128) + "^" + (dPrice + parseFloat(nFrac1) / 128));
                else
                    return (nPrice + "-" + nFrac1 + "/128" + "^" + (dPrice + parseFloat(nFrac1) / 128));
            }
            else if (Format == 15) //1/256R=eTwoFiftySixthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + PWM_Func_GetReducedFraction(nFrac1, 256) + "^" + (dPrice + parseFloat(nFrac1) / 256));
                else
                    return (nPrice + "-" + nFrac1 + "/256" + "^" + (dPrice + parseFloat(nFrac1) / 256));
            }
            else if (Format == 16) //1=e0DecimalPlaces
            {
                return (nPrice + "^" + dPrice);
            }
            else if (Format == 17) //0.1=e1DecimalPlace
            {
                return (nPrice + "." + (nFrac1.toString()).substring(0, 1) + "^" + dPrice + "." + nFrac1);
            }
            else if (Format == 18) //0.01=e2DecimalPlaces
            {
                return (nPrice + "." + nFrac1 + (nFrac2.toString()).substring(0, 1) + "^" + dPrice + "." + nFrac1 + nFrac2);
            }
            else if (Format == 19) //0.001=e3DecimalPlaces
            {
                return (nPrice + "." + nFrac1 + nFrac2 + (nFrac3.toString()).substring(0, 1) + "^" + dPrice + "." + nFrac1 + nFrac2 + nFrac3);
            }
            else if (Format == 20) //0.001=eFurtureRateHalfs
            {
                if (nFrac3 == 0)
                    return (nPrice + "." + nFrac1 + nFrac2 + "^" + dPrice + "." + nFrac1 + nFrac2);
                else
                    return (nPrice + "." + nFrac1 + nFrac2 + "+" + "^" + dPrice + "." + nFrac1 + nFrac2 + "5");
            }
            else if (Format == 21) //0.001=eFurtureRateQuarters
            {
                if (nFrac3 == 0)
                    return (nPrice + "." + nFrac1 + nFrac2 + "^" + dPrice + "." + nFrac1 + nFrac2);
                else if (nFrac3 == 2)
                    return (nPrice + "." + nFrac1 + nFrac2 + "+" + "^" + dPrice + "." + nFrac1 + nFrac2 + "5");
                else
                    return (nPrice + "." + nFrac1 + nFrac2 + "." + nFrac3 + "^" + dPrice + "." + nFrac1 + nFrac2 + ((nFrac3 == 1) ? "25" : "75"));
            }
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_DecimalPrice", err);
    }
}

///param1 double, param2 int, param3 int, param4 int, param5 int, param6 int
function PWM_Func_ShowFullOutput_PriceWidget(DecPrice, Format, nPrice, nFrac1, nFrac2, nFrac3) {
    try {
        //Show Output as  per selected Format
        if (Format == 0) //1/2=eHalfs
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/2");
        }
        else if (Format == 1) //1/4=eQuarters
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 2) //1/8=eEighths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "'" + nFrac1);
        }
        else if (Format == 3) //1/16=eSixteenths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''00");
            else if (nFrac1 > 0 && nFrac1 < 10)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''0" + nFrac1);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''" + nFrac1);
        }
        else if (Format == 4) //1/32=eThirtySeconds
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-00");
            else if (nFrac1 > 0 && nFrac1 < 10)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-0" + nFrac1);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1);
        }
        else if (Format == 5) //1/64=eSixtyFourths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 6) //1/128=eOneTwentyEights
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 7) //1/256=eTwoFiftySixths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/256");
        }
        else if (Format == 8) {
            if (nFrac2 == 4) // if 2nd fraction is 4 then display "+" after 1st fraction
            {
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "+");
            }
            else if (nFrac2 == 0) //if 2nd fraction is 0 then no need to display 2nd fraction
            {
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()));
            }
            else //show 2nd fraction after 1st fraction and dot
            {
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + ((nFrac1 == 0) ? "00" : nFrac1.toString()) + "." + nFrac2);
            }
        }
        else if (Format == 9) //1/4R=eQuartersReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 == 2)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 4));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 10) //1/8R=eEighthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 8));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/8");
        }
        else if (Format == 11) //1/16R=eSixteenthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 16));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/16");
        }
        else if (Format == 12) //1/32R=eThirtySecondsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 32));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/32");
        }
        else if (Format == 13) //1/64R=eSixtyFourthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 64));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 14) //1/128R=eOneTwentyEightsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 128));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 15) //1/256R=eTwoFiftySixthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + PWM_Func_GetReducedFraction(nFrac1, 256));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/256");
        }
        else if (Format == 16) //1=e0DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
        }
        else if (Format == 17) //0.1=e1DecimalPlace
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1);
        }
        else if (Format == 18) //0.01=e2DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
        }
        else if (Format == 19) //0.001=e3DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + nFrac3);
        }
        else if (Format == 20) //0.001=eFurtureRateHalfs
        {
            if (nFrac3 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "+");
        }
        else if (Format == 21) //0.001=eFurtureRateQuarters
        {
            if (nFrac3 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
            else if (nFrac3 == 2)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "+");
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "." + nFrac3);
        }
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_ShowFullOutput_PriceWidget", err);
    }
}

function PWM_Func_ShowFullValue_PriceWidget() {
    try {
        var FullValue1;

        //if ($('#pwmHdnBeastValueHolder').attr("name") != "") {
        if (PWM_GLOBAL_BEAST_VALUE_2 !== "") {
            FullValue1 = PWM_Func_ParsePriceForWidget(Math.abs(parseFloat(PWM_GLOBAL_TXTVAL_2_CALC)), parseInt(PWM_GLOBAL_SLTD_FORMAT), 0);
        }

        //PWM_GLOBAL_TXTVAL_2 = $('#pwmDvTxtValue2').text();
        PWM_GLOBAL_TXTVAL_2 = PWM_GLOBAL_TXTVAL_2_CALC.toString();

        PWM_Func_PartitionedValue_PriceWidget(FullValue1);
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_ShowFullValue_PriceWidget", err);
    }
}

/// <summary>
/// Code for converting decimal price to given price format. Pass Number format as per above enum (0 t0 21)
/// Usage - PWM_Func_ParsePriceForWidget(99.9645,8) ;
/// </summary>
///param1 double, param2 int, param3 int       
function PWM_Func_ParsePriceForWidget(DecPrice, Format, ShowFullyFormatted) {
    try {
        //int
        var nPrice, nFrac1, nFrac2, nFrac3, nBaseUnit, min = 1;
        nPrice = parseInt((Math.floor(Math.abs(DecPrice))));
        nFrac1 = nFrac2 = nFrac3 = 0;
        nBaseUnit = Format;
        if (nBaseUnit == 8) // related to 1/32+ format=eBond
        {
            nFrac1 = parseInt(((Math.abs(DecPrice) - nPrice) * 256 + 0.5));
            if (nFrac1 == 256) {
                nFrac1 = 0;
                nPrice++;
            }
            nFrac2 = nFrac1 % 8;
            nFrac1 /= 8;

            //Show Output as  per selected Format
            if (ShowFullyFormatted == 0) {
                return PWM_Func_ShowFullOutput_PriceWidget(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
            else {
                return PWM_Func_ShowPartitionedOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
        }
        else {
            //Set Min as per Number Format
            if (nBaseUnit == parseInt(PWM_NumberFormats.eBond))
                min = 32;
            else if (nBaseUnit >= parseInt(PWM_NumberFormats.eHalfs) && nBaseUnit <= parseInt(PWM_NumberFormats.eTwoFiftySixthsReduced))
                min = (1 << ((nBaseUnit % 8) + 1));
            else if (nBaseUnit == 17)
                min = 10;
            else if (nBaseUnit == 18)
                min = 100;
            else if (nBaseUnit == 19)
                min = 1000;
            else if (nBaseUnit == 20) {
                min = 1000;
            }
            else if (nBaseUnit == 21) {
                min = 10000;
            }

            nFrac1 = parseInt(((Math.abs(DecPrice) - nPrice) * min + 0.5));
            if (nFrac1 == min) {
                nFrac1 = 0;
                nPrice++;
            }
            if (nBaseUnit == 18) {
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 19) {
                nFrac3 = nFrac1 % 10;
                nFrac1 /= 10;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 20) {
                nFrac3 = (nFrac1 % 10) / 5;
                nFrac1 /= 10;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 21) {
                nFrac3 = (nFrac1 % 100) / 25;
                nFrac1 /= 100;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }

            //Show Output as  per selected Format
            if (ShowFullyFormatted == 0) {
                return PWM_Func_ShowFullOutput_PriceWidget(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
            else {
                return PWM_Func_ShowPartitionedOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:PWM_Func_ParsePriceForWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function PWM_Func_PartitionedValue_PriceWidget(varFullValue1) {
    try {
        var PartitionValue;
        if (PWM_GLOBAL_BEAST_VALUE_2 !== "")
            PartitionValue = PWM_Func_ParsePriceForWidget(Math.abs(parseFloat(PWM_GLOBAL_TXTVAL_2)), parseInt(PWM_GLOBAL_SLTD_FORMAT), 1);

        //$('#pwmDvTxtValue1').text(varFullValue1);
        //PWM_Func_Set_Txt1Value(varFullValue1);

        if (PWM_GLOBAL_TXTVAL_2.toString().indexOf("-")) {
            PWM_Func_Set_Txt1Value(varFullValue1);
        }
        else {
            if (PWM_GLOBAL_TXTVAL_2_CALC != "0" && PWM_GLOBAL_TXTVAL_2_CALC != "0.00" && PWM_GLOBAL_TXTVAL_2_CALC != "0.000" && PWM_GLOBAL_TXTVAL_2_CALC != "0.0") {
                //$('#pwmDvTxtValue1').text("-" + $('#pwmDvTxtValue1').text());
                //PWM_Func_Set_Txt1Value("-" + $('#pwmDvTxtValue1').text());
                PWM_Func_Set_Txt1Value("-" + varFullValue1);
            }
            else {
                //$('#pwmDvTxtValue1').text($('#pwmDvTxtValue1').text());
            }
        }

        if (PWM_GLOBAL_SLTD_FORMAT == PWM_NumberFormats.e0DecimalPlaces) {
            PartitionValue = PartitionValue + "#";
        }
        PWM_CALCULATED_VALUE = PartitionValue;

        PWM_GLOBAL_SLTDVAL = PartitionValue;
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_PartitionedValue_PriceWidget", err);
    }
}

/* ==END NUMBER FUNCTIONS== */

function PWM_Func_Save_PriceWidget() {
    try {
        IsChange = '';

        PWM_GLOBAL_BEAST_VALUE_1 = $('#pwmDvTxtValue1').text();
        PWM_GLOBAL_BEAST_VALUE_2 = PWM_GLOBAL_TXTVAL_2;

        var eleAryInfo = PWM_GLOBAL_LAST_CLICKED_ELEM_INFO.split('^');
        var instanceType = eleAryInfo[0];
        var paraValues = eleAryInfo[1] + "^" + eleAryInfo[2] + "^" + eleAryInfo[3];
        var idValPair = eleAryInfo[4] + "#" + PWM_GLOBAL_TXTVAL_2;
        SendToBeast(instanceType + "#" + paraValues, idValPair);

        PWM_Func_Close_PriceWidget();
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Save_PriceWidget", err);
    }
}

function PWM_Func_Close_PriceWidget() {
    try {
        $('#pwmTxtPrice').unbind();
        //$('#' + pwmWIDGET_DIV_ID).hide();
        $('#' + pwmWIDGET_DIV_ID).modal(false);
        PWM_Func_ClearGlobalVars();
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Close_PriceWidget", err);
    }
}

function PWM_Func_Clear_PriceWidget() {
    try {
        IsChange = '';

        PWM_GLOBAL_BEAST_VALUE_1 = $('#pwmDvTxtValue1').text();
        PWM_GLOBAL_BEAST_VALUE_2 = PWM_GLOBAL_TXTVAL_2;

        var eleAryInfo = PWM_GLOBAL_LAST_CLICKED_ELEM_INFO.split('^');
        var instanceType = eleAryInfo[0];
        var paraValues = eleAryInfo[1] + "^" + eleAryInfo[2] + "^" + eleAryInfo[3];
        var idValPair = eleAryInfo[4] + "#clr";

        SendToBeast(instanceType + "#" + paraValues, idValPair);

        PWM_Func_Close_PriceWidget();
    }
    catch (err) {
        PWM_Func_HandleJsError("PWM_Func_Clear_PriceWidget", err);
    }
}

function PWM_Func_ClearGlobalVars() {
    PWM_GLOBAL_SLTDVAL = '';
    PWM_GLOBAL_TEMP_SLTDVAL = '';
    PWM_GLOBAL_TXTVAL_1 = '';
    PWM_GLOBAL_TXTVAL_2 = '';
    PWM_GLOBAL_TXTVAL_2_CALC = '';
    PWM_GLOBAL_DEFAULT_FORAMT = '';
    PWM_GLOBAL_SLTD_FORMAT = '';
    PWM_GLOBAL_LAST_CLICKED_ELEM_INFO = '';
    PWM_GLOBAL_BEAST_VALUE_1 = ''; PWM_GLOBAL_BEAST_VALUE_2 = '';
    PWM_GLOBAL_PRICE = '';

    $('#hdnWgtElement').val('');
}

function PWM_Func_HandleJsError(pFuncName, pErrorObj) {
    var strerrordesc = "Function:" + pFuncName + "(); Error is : " + pErrorObj.description + "; Error number is " + pErrorObj.number + "; Message :" + pErrorObj.message;
    alert(strerrordesc);
}
