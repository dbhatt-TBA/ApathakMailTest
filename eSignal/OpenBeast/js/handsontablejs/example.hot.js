var row = CurrentRC[0];
var col = CurrentRC[1];
var dFontcolor = 0;
var dFillcolor = 0;
var dBordercolor = 0;
var colorfvalue = null;
var colorbvalue = null;
var bordercolorvalue = null;
var borderTopcolor = null;
var borderBottomcolor = null;
var borderLeftcolor = null;
var borderRightcolor = null;
var borderTopStyle = "none";
var borderBottomStyle = "none";
var borderLeftStyle = "none";
var borderRightStyle = "none";
var top1 = 0;
var bottom = 0;
var left = 0;
var right = 0;
$.example.showExampleDialog = function () {

    $.example.toolbar.data.example(); // load form...
    $.example.fillExampleDialog();
}
$.example.fillExampleDialog = function () {
    

        var num = "Type: <br /><select size='5' id='numberTableSubId'><option>'0,0.0000'</option><option>'0,0'</option><option>'+0,0'</option><option>'0,0.0'</option><option>'0.000'</option><option>'0[.]00000'</option><option>'(0,0.0000)'</option>";
        num += "<option>'.00'</option><option>'(.00)'</option><option>'0.00000'</option><option>'0.0[0000]'</option><option>'0.0a'</option><option>'0 a'</option><option>'0a'</option><option>'0o'</option></select>";
        var cur = "Type: <br /><select size='5' id='numberTableSubId'><option>'$0,0.00'</option><option>'0,0[.]00 $'</option><option>'$ 0,0[.]00'</option><option>'($0,0)'</option><option>'$0.00'</option><option>'($ 0.00 a)'</option></select>";
        var per = "Type: <br /><select size='5' id='numberTableSubId'><option>'0%'</option><option>'0.000%'</option><option>'0 %'</option><option>'(0.000 %)'</option></select>";
        var time = "Type: <br /><select size='5' id='numberTableSubId'><option>'00:00:00'</option></select>";
        var date = "Type: <br /><select size='5' id='numberTableSubId'><option>yyyy/MM/dd</option><option>dd/MM/yyyy</option><option>MM/dd/yyyy</option><option>dd/MM/yy</option></select>";

        var numberTable = "<table><tr><td><div style='margin-bottom:10px;'><table><tr><td rowspan ='3'>Category :<br /><select size='8' id='numberTableId'><option>General</option><option>Number</option><option>Currency</option><option>Percentage</option><option>Time</option><option>Date</option><option>Text</option></select>";
        numberTable += "  </td></tr><tr><td><div id ='numberSubTable'></div> </td></tr></table></div></td></tr><tr><td><div><table><tr><td><div id='hdivalign'>Horizontal :<select style='width:80px;' Id='hAlign'><option value='hselect'>--Select--</option><option value='hl'>Left</option><option value='hr'>Right</option>";
        numberTable +="<option value='hc'>Center</option><option value='hj'>Justify</option></select></div></td></tr>";
        numberTable +="<tr><td><div id='vdivalign'>Vertical :<select style='width:80px; margin-left:11px;' Id='vAlign'><option value='vselect'>--Select--</option> <option value='vt'>Top</option><option value='vb'>Bottom</option><option value='vm'>Middle</option><option value='vc'>Central</option></select></div></td></tr></table></div></td></tr></table>";

        var fontTable = " <table id='font_table'><tr><td><div id='f_tab'><table><tr><td>Font:<br /><input type ='text' id='fontfamilyId' /><br /><select size='6' Id='fFamily'><option>Aharoni</option>";
        fontTable += "<option>Algerian</option><option>Andalus</option><option>Arial</option><option>Baskerville Old Face</option><option>Batang</option>";
        fontTable += "<option>Angsana New</option><option>AngsanaUPC Aparajita</option><option>Arabic Typesetting</option><option>Arial Unicode MS</option><option>BatangChe</option>";
        fontTable += "<option>Bauhaus 93</option><option>Bell MT</option><option>Berlin Sans FB</option><option>Bernard MT</option><option>Bodoni MT Poster</option>";
        fontTable += "<option>Book Antiqua</option><option>Bookman Old Style</option><option>Bookshelf Symbol 7</option><option>Britannic</option><option>Broadway</option>";
        fontTable += "<option>Browallia New</option><option>BrowalliaUPC</option><option>Brush Script MT</option><option>Buxton Sketch</option><option>Calibri</option>";
        fontTable += "<option>Californian FB</option><option>Cambria</option><option>Cambria Math</option><option>Candara</option><option>Centaur</option>";
        fontTable += "<option>Century</option><option>Century Gothic</option><option>Chiller</option><option>Colonna MT</option><option>Comic Sans MS</option>";
        fontTable += "<option>Consolas</option><option>Constantia</option><option>Cooper</option><option>Corbel</option><option>Cordia New</option>";
        fontTable += "<option>CordiaUPC</option><option>Courier New</option><option>cursive</option><option>DaunPenh</option><option>David</option>";
        fontTable += "<option>DejaVu Sans</option><option>DejaVu Sans Mono</option><option>DejaVu Serif</option><option>DFKai-SB</option><option>DilleniaUPC</option>";
        fontTable += "<option>DilleniaUPC</option><option> DokChampa</option><option> Dotum</option><option> DotumChe</option><option>Estrangelo Edessa</option><option> Ebrima</option>";
        fontTable += "<option> EucrosiaUPC</option><option> Euphemia</option><option> FrankRuehl</option><option> FangSong</option><option> fantasy</option><option>Footlight MT</option>";
        fontTable += "<option> Franklin Gothic</option><option> FrankRuehl</option><option> FreesiaUPC</option><option>Freestyle Script</option><option> Gabriola </option><option>Garamond</option>";
        fontTable += "<option> Gautami </option><option>Gentium Basic</option><option>Gentium Book Basic</option><option> Georgia</option><option> Gisha</option><option> Global Monospace</option>";
        fontTable += "<option> Global Sans Serif</option><option>Global Serif</option><option>Global User Interface</option><option> Gulim </option><option>GulimChe</option><option> Gungsuh</option>";
        fontTable += "<option> GungsuhChe</option><option>Harlow Solid</option><option> Harrington</option><option> High Tower Text</option><option> inherit</option><option> Impact </option><option>Informal Roman</option>";
        fontTable += "<option> initial</option><option> IrisUPC</option><option>Iskoola Pota</option><option> JasmineUPC</option><option> Jokerman </option><option>Juice ITC</option><option> KaiTi </option><option>Kalinga </option>";
        fontTable += "<option>Kartika</option><option> Khmer UI</option><option> KodchiangUPC</option><option> Kokila</option><option> Kristen ITC</option><option>Kunstler Script</option>";
        fontTable += "<option>Magneto</option><option>Malgun Gothic</option><option> Mangal</option><option> Marlett</option><option>Matura MT Script Capitals</option><option> Meiryo</option><option>Meiryo UI</option>";
        fontTable += "<option>Microsoft Himalaya</option><option>Microsoft JhengHei</option><option> Microsoft New Tai Lue</option><option> Microsoft PhagsPa</option><option> Microsoft Sans Serif</option><option>Microsoft Tai Le</option>";
        fontTable += "<option>Microsoft Uighur</option><option>Microsoft YaHei</option><option>Microsoft Yi Baiti</option><option> MingLiU </option><option>MingLiU-ExtB</option><option> MingLiU_HKSCS</option><option>  MingLiU_HKSCS-ExtB </option>";
        fontTable += "<option> Miriam </option><option>Miriam Fixed</option><option> Mistral</option><option> Modern No. 20</option><option>Mongolian Baiti</option><option> monospace</option><option> Monotype Corsiva</option><option> MoolBoran</option>";
        fontTable += "<option>MS Gothic</option><option> MS Mincho</option><option> MS PGothic</option><option>MS PMincho</option><option>MS Reference Sans Serif</option><option>MS Reference Specialty</option><option>MS UI Gothic</option><option>MT Extra</option>";
        fontTable += "<option>MV Boli</option><option> Narkisim</option><option>Niagara Engraved</option><option>Niagara Solid</option><option> NSimSun</option><option> Nyala</option><option>Old English Text MT</option><option> Onyx</option><option> Open Sans</option>";
        fontTable += "<option> OpenSymbol</option><option>Palatino Linotype</option><option> Parchment</option><option>Plantagenet Cherokee</option><option> Playbill</option><option> PMingLiU </option><option>PMingLiU-ExtB</option><option>Poor Richard</option><option>PT Serif</option>";
        fontTable += "<option>Segoe Print</option><option>Sakkal Majalla</option><option> sans-serif</option><option> Segoe Marker</option><option>Segoe Script</option><option>Segoe UI</option> <option>Segoe UI Symbol</option><option> serif</option>";
        fontTable += "<option>Shonar Bangla</option><option>Showcard Gothic</option><option> Shruti</option><option> SimHei</option><option> Simplified Arabic</option><option>Simplified Arabic Fixed</option><option> SimSun </option>        ";
        fontTable += "<option>SimSun-ExtB</option><option>SketchFlow Print</option><option>Snap ITC</option><option>Source Code Pro</option><option>Source Sans Pro</option><option> Stencil </option><option>Sylfaen </option>";
        fontTable += "<option>Symbol</option><option> Tahoma </option><option>Tempus Sans ITC</option><option>Times New Roman</option>Traditional Arabic</option><option>Trebuchet MS</option><option> Tunga</option><option> Utsaah</option>";
        fontTable += "<option> Vani </option><option>Verdana</option><option> Vijaya</option><option> Viner Hand ITC</option><option> Vivaldi</option><option> Vladimir Script</option><option> Vrinda</option><option> Webdings </option><option>Wide Latin</option><option> Wingdings</option><option>Wingdings 2</option><option>Wingdings 3</option><option> ZWAdobeF</option>";

        fontTable += "</select></td><td>Font Style :<br /><input type ='text' id='fontstyleId' /><br /><select size='6' Id='fStyle'><option>Regular</option><option>Italic</option><option>Bold</option><option>Bold Italic</option></select></td>";
        fontTable += "<td>Size:<br /><input type ='text' id='sizeid' /><br /><select size='6' Id='fSize'><option>8</option><option>9</option><option>10</option><option>11</option><option>12</option><option>14</option><option>16</option>";
        fontTable += "<option>18</option><option>20</option><option>22</option><option>24</option><option>26</option><option>28</option><option>36</option><option>48</option><option>72</option></select></td></tr></table></div></td></tr>";
        fontTable += "<tr><td><div id='u_line'>Underline: <input type='checkbox' id='underlineId' onclick='underline_click()'/></div></td></tr></table>";

        var borderStyle = "<div style='text-align:center; margin-bottom: 10px; height:20px; width:120px;'>Style : <select style='width:80px;' Id='bstyle'><option value='solid'>solid</option><option value='dashed'>dashed</option><option value='dotted'>dotted</option><option value='none'>none</option><option value='double'>double</option></select>";
        var bcheckbox = "<a class='b_set' onclick='lbcolorvalue()'><div id='l_img' class='bd_set'><img src='js/handsontablejs/dialogImage/border-left.jpg' /><img src='js/handsontablejs/dialogImage/border-left1.jpg' style='display:none'/></div></a><a class='b_set' onclick='rbcolorvalue()'><div id='r_img' class='bd_set'><img src='js/handsontablejs/dialogImage/border-right.jpg' /><img src='js/handsontablejs/dialogImage/border-right1.jpg' style='display:none'/></div></a>";
        bcheckbox += "<a class='b_set' onclick='tbcolorvalue()'><div id='t_img' class='bd_set'><img src='js/handsontablejs/dialogImage/border-top.jpg' /><img src='js/handsontablejs/dialogImage/border-top1.jpg' style='display:none' /></div></a><a class='b_set' onclick='bbcolorvalue()'><div id='b_img' class='bd_set'><img src='js/handsontablejs/dialogImage/border-bottom.jpg' /><img src='js/handsontablejs/dialogImage/border-bottom1.jpg' style='display:none'/></div></a>";
        var bcolorTable = "</div><div id='borderDiv'></div>";
        var borderTable = "<table><tr><td><div><table><tr><td>" + borderStyle + "</td><td><div style='height: 28px; width:152px; margin-left:2px;'>" + bcheckbox + "</div></td></tr></table></div></td></tr><tr><td><div><table><tr><td>" + bcolorTable + "</td><td>Border :<br /> <div id='sampledivborderId'><div id='sampledivinnerborderId'></div></div></td></tr></table></div></td></tr></table>"

        var fillTable = "<div><table><tr><td><input type='button' value='Font Color' id='f_button' onclick='setfontcolorvalue()' /><input type='button' id='b_button' onclick='setbackcolorvalue()' value='Background Color' /></td></tr><tr><td><input type='button' class='tab-color-button' id='nocolorId' name='nocolorId' value='No color' onclick='nocolorvalue()' /></td></tr><tr><td><div><table><tr><td><div id='cpDiv'></div></td><td>Font color :<br /> <div id='sampledivfontId'><div id='sampledivinnerfontId'></div></div>Background:<br /> <div id='sampledivbackId'><div id='sampledivinnerbackId'></div></div></td></tr></table></div></td></tr></table></div>";

        var dialog_div = "<div class='tabbable-line' style='height:300px; margin-bottom:5px;'>";
        dialog_div += "<ul class='nav nav-tabs' style='margin-bottom:5px;'><li class='active' id='number'><a href='#tab_default_1' data-toggle='tab'>Number&Alignment </a></li>";
        dialog_div += "<li id='font'><a href='#tab_default_2' data-toggle='tab'>Font </a></li><li id='fill'><a href='#tab_default_3' data-toggle='tab'>Color</a></li>";
        dialog_div += "<li id='border'><a href='#tab_default_4' data-toggle='tab'>Borders</a></li></ul>";

        dialog_div += "<div class='tab-content'><div class='tab-pane active' id='tab_default_1'><div class='data-table col-lg-1 table-responsive'>" + numberTable + "</div></div>";
        dialog_div += "<div class='tab-pane' id='tab_default_2'><div class='data-table col-lg-1 table-responsive'>" + fontTable + " </div></div>";
        dialog_div += "<div class='tab-pane' id='tab_default_3'><div class='data-table col-lg-8 table-responsive'>" + fillTable + "</div></div>";
        dialog_div += "<div class='tab-pane' id='tab_default_4'><div class='data-table col-lg-6 table-responsive'>" + borderTable + "</div></div>";
        dialog_div += "</div></div><div class='space10'></div>";
        dialog_div += "<div class='col-lg-8 text-left bottom-ptch'><input type ='button' id='ht_ok' value='Ok' class='tab-color-button' /><input type ='button' id='ht_cancel' value='Cancel' class='tab-color-button' /><input type ='button' id='ht_remove' value='Remove' class='tab-color-button' /></div>";
        $("#dialog_hot").html('').html(dialog_div);
        $('#borderDiv').colorpicker({ color: '#31859b' });
        $('#cpDiv').colorpicker({ color: '#31859b' });

        $('#numberTableId').on("change", function () {
            var general = "";
            var n_Format = $("#numberTableId option:selected").text();
            if (n_Format == "Number")
            { general = num; }
            else if (n_Format == "Currency")
            { general = cur; }
            else if (n_Format == "Percentage")
            { general = per; }
            else if (n_Format == "Time")
            { general = time; }
            else if (n_Format == "Date")
            { general = date; }
            $("#numberSubTable").html('').html(general);
        });

        $('#fill').on("click", function () {
            dBordercolor = 0;
        })
        $('#font').on("click", function () {
            dFillcolor = 0;
            dFontcolor = 0;
            dBordercolor = 0;
        })
        $('#border').on("click", function () {
            dFillcolor = 0;
            dFontcolor = 0;
            dBordercolor = 1;
        })
        $('#number').on("click", function () {
            dFillcolor = 0;
            dFontcolor = 0;
            dBordercolor = 0;
        })
         $('#fFamily').on("change", function () {
          var fontFamily = $("#fFamily option:selected").text();
          $('#fontfamilyId').val(fontFamily);
         });
         $('#fStyle').on("change", function () {
         var fontStyle = $("#fStyle option:selected").text();
         $('#fontstyleId').val(fontStyle);
         });
         $('#fSize').on("change", function () {
         var fontSize = $("#fSize option:selected").text();
         $('#sizeid').val(fontSize);
         });

         $('#ht_ok').on("click", function () {
          
            $.example.saveExampleData();
            $.example.destroyExampleData();
            $.example.exampleDialogs.example.dialog("close");
           
         })
         $('#ht_remove').on("click", function () {

             $.example.removeExampleData();
             $.example.destroyExampleData();
             $.example.exampleDialogs.example.dialog("close");

         })
       $('#ht_cancel').on("click", function () {
           $.example.destroyExampleData();
           $.example.exampleDialogs.example.dialog("close");
         
       });
}
function underline_click() {
    if ($("#underlineId").prop('checked') == true) {
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][5] = "1";
    }
    else if ($("#underlineId").prop('checked') == false) {
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][5] = "0";
    }
}

function nocolorvalue() {
    if (dFillcolor == 1) {
        $("#sampledivinnerbackId").css({ "background-color": "#eee" });
        colorbvalue = "#FFFFFF";
    }
    else if (dFontcolor == 1) {
        $("#sampledivinnerfontId").css({ "background-color": "#eee" });
        colorfvalue = "#000000";
    }
    else {
        $("#sampledivinnerbackId").css({ "background-color": "#eee" });
        colorbvalue = "#FFFFFF";
        $("#sampledivinnerfontId").css({ "background-color": "#eee" });
        colorfvalue = "#000000";
       }
}
function setfontcolorvalue() {
    dFillcolor = 0;
    dFontcolor = 1;
    dBordercolor = 0;
}

function setbackcolorvalue() {
    dFillcolor = 1;
    dFontcolor = 0;
    dBordercolor = 0;
}

function tbcolorvalue() {
    var tcolor = "#ffffff";
    $("#t_img").find('img').toggle();
    if (top1 == 0) {
        top1 = 1;
        borderTopcolor = bordercolorvalue;
        borderTopStyle = $("#bstyle").val();
        tcolor = bordercolorvalue;
    }
    else if (top1 == 1) {
        top1 = 0;
        borderTopcolor = "#d3d3d3";
        borderTopStyle = "none";
       
    }
    $("#sampledivinnerborderId").css({ "border-top-color": tcolor });
    $("#sampledivinnerborderId").css({ "border-top-style": borderTopStyle});
    dBordercolor = 1;
}
function bbcolorvalue() {
    var bcolor = "#ffffff";
    $("#b_img").find('img').toggle();
    if (bottom == 0) {
        bottom = 1;
        borderBottomcolor = bordercolorvalue;
        borderBottomStyle = $("#bstyle").val();
        //ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][14] = bordercolorvalue;
        //ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][18] = 
        bcolor = bordercolorvalue;
    }
    else if (bottom == 1) {
        bottom = 0;
       borderBottomcolor = "#d3d3d3";
       borderBottomStyle = "none";
    }
    $("#sampledivinnerborderId").css({ "border-bottom-color": bcolor });
    $("#sampledivinnerborderId").css({ "border-bottom-style": borderBottomStyle});
    dBordercolor = 1;
}
function lbcolorvalue() {
    var lcolor = "#ffffff";
    $("#l_img").find('img').toggle();
    if (left == 0) {
        left = 1;
        borderLeftcolor = bordercolorvalue;
        borderLeftStyle = $("#bstyle").val();
        //ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][15] = bordercolorvalue;
        //ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][19] = 
        lcolor = bordercolorvalue;
    }
    else if (left == 1) {
        left = 0;
        borderLeftcolor = "#d3d3d3";
        borderLeftStyle = "none";
    }
    $("#sampledivinnerborderId").css({ "border-left-color": lcolor });
    $("#sampledivinnerborderId").css({ "border-left-style": borderLeftStyle });
    dBordercolor = 1;
}

function rbcolorvalue() {
    var rcolor = "#ffffff";
    $("#r_img").find('img').toggle();
    if (right == 0) {
        right = 1;
        borderRightcolor = bordercolorvalue;
        borderRightStyle = $("#bstyle").val();
        //ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][16] = bordercolorvalue;
        //ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][20] =
        rcolor = bordercolorvalue;
    }
    else if (right == 1) {
        right = 0;
        borderRightcolor = "#d3d3d3";
        borderRightStyle = "none";
    }
    $("#sampledivinnerborderId").css({ "border-right-color": rcolor });
    $("#sampledivinnerborderId").css({ "border-right-style": borderRightStyle});
    dBordercolor = 1;
}

$.example.loadExample = function () {
};

$.example.destroyExampleData = function () {
    row = null;
    col = null;
    dFontcolor = 0;
    dFillcolor = 0;
    dBordercolor = 0;
    colorfvalue = null;
    colorbvalue = null;
    bordercolorvalue = null;
    borderTopcolor = null;
    borderBottomcolor = null;
    borderLeftcolor = null;
    borderRightcolor = null;
    borderTopStyle = "none";
    borderBottomStyle = "none";
    borderLeftStyle = "none";
    borderRightStyle = "none";
    top1 = 0;
    bottom = 0;
    left = 0;
    right = 0;
       
    $("#dialog_hot").html('').html('');

    var ht = $("#dvshared").handsontable('getInstance');
    ht.selectCell(CurrentRC[0], CurrentRC[1]);
}

$.example.removeExampleData = function () {
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][0] = "#000000";//FontColor
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][1] = "#ffffff";//BackgroundColor
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][2] = 14;//FontSize
    
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][3] = "0";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][4] = "0";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][5] = "0";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][6] = "Calibri";//Font Family
    
    //ExcelShareData[row][col][7] = text_alignment[0];
    //ExcelShareData[row][col][8] = text_alignment[1];
    
   
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][13] = "#cccccc";//top
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][14] = "#cccccc";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][15] = "#cccccc";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][16] = "#cccccc";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][17] = "solid";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][18] = "solid";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][19] = "solid";
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][20] = "solid";
    SubmitExceltoBeast();
}

$.example.saveExampleData = function () {

    /////////Border
    if(borderTopcolor != null)
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][13] = borderTopcolor;
    if(borderTopStyle !="none")
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][17] = borderTopStyle;
    if (borderBottomcolor != null)
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][14] = borderBottomcolor;
    if (borderBottomStyle != "none")
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][18] = borderBottomStyle;
    if (borderLeftcolor != null)
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][15] = borderLeftcolor;
    if (borderLeftStyle != "none")
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][19] = borderLeftStyle;
    if (borderRightcolor != null)
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][16] = borderRightcolor;
    if (borderRightStyle != "none")
    ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][20] = borderRightStyle;


    var datahAlignment = $("#hAlign").val();
    var datavAlignment = $("#vAlign").val();

    //////////////// dataAlignment
    if (datahAlignment == "hl") {
        ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][7] = "left"; 
     }
     else if (datahAlignment == "hc") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][7]= "center";
     }
     else if (datahAlignment == "hr") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][7]= "right";
     }
     else if (datahAlignment == "hj") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][7] = "justify";
     }
     if (datavAlignment == "vt") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][8]= "top" ;
      }
      else if (datavAlignment == "vm") {
          ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][8]= "middle";
      }
      else if (datavAlignment == "vb") {
          ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][8]="bottom";
      }
      else if (datavAlignment == "vc") {
          ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][8]= "central";
     }

     var fontStyle = $("#fontstyleId").val();
     var fontFamily = $("#fontfamilyId").val();
     var fontSize = $("#sizeid").val() ;

    ////////////////////font 
     if (fontFamily != "") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][6]= fontFamily;
     }
     if (fontSize != "") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][2]= fontSize;
      }
     if (fontStyle == "Bold")
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][3] = "1";
     else if (fontStyle == "Italic")
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][4]="1";
     else if (fontStyle == "Bold Italic") {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][3] = "1";
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][4] = "1";
      }
    ////////background 
     if (colorbvalue != null) {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][1]= colorbvalue ;
      }
     if (colorfvalue != null) {
         ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][0]= colorfvalue ;
      }  
    
    var id = "#R_" + row + "_C_" + col;
    var numberFormat = $("#numberTableSubId option:selected").text();

        /////Number
    var text = ExcelShareData[(CurrentRC[0])][(CurrentRC[1])][12];
    var n_Format = $("#numberTableId option:selected").text();
        if (n_Format == "Number" && n_Format == "Currency" && n_Format == "Percentage" && n_Format == "Time") {
               if (text != "" && numberFormat != "") {
                 if (!isNaN(text)) {
                    var abc = numeral(text).format(numberFormat);
                    data[(CurrentRC[0])][(CurrentRC[1])]=abc;
                  }
                }
       }
       else if (n_Format == "Date")
         {
             if (text != "" && numberFormat != "") {
                  var xyz;
                if (!isNaN(text)) {
                     xyz = DateFormat.format.parseDate(parseInt(text));
                     xyz = DateFormat.format.date(xyz.date, numberFormat);
                 }
                else {
                       xyz = DateFormat.format.date(text, numberFormat);
                      }
                data[(CurrentRC[0])][(CurrentRC[1])] = xyz;
           }
      }

    SubmitExceltoBeast();
    colorbvalue = null;
    colorfvalue = null;
    $.example.exampleDialogs.example.dialog("close");
}
