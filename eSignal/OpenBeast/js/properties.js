function setEleProps(element, strProps, elePrntID) {
    try {
        //strProps = "V=1|M=0|E=0|F=3|R=-1|B=-1";
        var aryProps = strProps.split('|');

        if (!(aryProps[0] == "V=1")) {
            //$(element).removeClass().addClass(getClassForElement(aryProps[5])).addClass(getClassForElement(aryProps[4])).addClass(getClassForElement(aryProps[3]));
            $(element).hide();
        }
        else {

            var isInputNDisable = false;
            var isBasisField = false;
            var isTermField = false;

            //        if ($(element).hasClass('basisWidget')) {
            //            isBasisField = true;
            //        }
            //        else if ($(element).hasClass('termWidget')) {
            //            isTermField = true;
            //        }

            isBasisField = IsBasisWidget(aryProps[6]);
            isTermField = IsTermWidget(aryProps[6]);

            $(element).show();

            if (!(aryProps[2] == "E=1")) {
                if ($(element).is('input')) {
                    isInputNDisable = true;
                }
                else {
                    $(element).attr('disabled', 'disabled');
                }
            }
            else {
                if ($(element).is('input')) {
                    if ($(element).hasClass('inputDisable')) {
                        if ($(element).is('input[type="button"]')) {
                            $(element).attr('disabled', false).removeClass("inputDisable");
                        }
                        else {
                            $(element).removeClass("inputDisable").removeAttr('readonly').css("border", "");

                        }

                    }
                }
                else {
                    if (elePrntID.indexOf('BI_CMEFuture') == -1)
                        $(element).removeAttr('disabled');
                }
            }

            $(element).removeClass().addClass(getClassForElement(aryProps[5])).addClass(getClassForElement(aryProps[4])).addClass(getClassForElement(aryProps[3]));

            if (isInputNDisable == true) {
                if ($(element).is('input[type="button"]')) {
                    $(element).addClass('inputDisable').attr("disabled", true);
                }
                else {
                    $(element).addClass('inputDisable').attr('readonly', 'readonly').css("border", "0px");
                }
            }
            else {
                var isPriceWidget = UsesPriceWidget(parseInt(aryProps[3].split('=')[1]));

                if (isPriceWidget == true) {
                    $(element).addClass('priceWidget').css("cursor", "pointer").attr('readonly', 'readonly');
                }
                else if (isBasisField == true) {
                    $(element).addClass('basisWidget').css("cursor", "pointer").attr('readonly', 'readonly');
                }
                else if (isTermField == true) {
                    $(element).addClass('termWidget').css("cursor", "pointer").attr('readonly', 'readonly');
                }


            }

            if ($(element).is('input[type="text"]')) {
                setManualHighlighter(element, aryProps[1]); //setManualHighlighter
            }
        }
    }
    catch (err) {
        var strerrordesc = "Function:setEleProps(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("VolSeparate.js", strerrordesc);
    }
}

function setManualHighlighter(element, manualProp) {
    //    if ($(element).attr('id') == 'vcm_calc_SwapPX_260_807' || $(element).attr('id') == 'vcm_calc_SwapPX_260_808' || $(element).attr('id') == 'vcm_calc_SwapPX_260_809' || $(element).attr('id') == 'vcm_calc_SwapPX_260_810') {
    //        alert($(element).attr('id') + " : " + manualProp);
    //    }
    if (manualProp == "M=1") {
        $(element).addClass('ManualChange').trigger('ManualClassChange');
        //$(element).css('border', '1px solid #996515');
    }
    else {
        $(element).removeClass('ManualChange');
        //$(element).css('border', '');
    }
}

function UsesPriceWidget(nFormat) {
    return (nFormat >= 1200 && nFormat <= 1999) || (nFormat >= 5200 && nFormat <= 5999);
}

function IsTermWidget(fieldID) {
    if (fieldID == "T=21")
        return true;
    return false;
}

function IsBasisWidget(fieldID) {
    if (fieldID == "T=52")
        return true;
    return false;
}

function setEleColorProp() {

}

function getClassForElement(clrProp) {
    return clrProp.replace("=", "_");
}

function getColorFromClass(clrClass) {
    try {
        var clrName = "";
        var clrAry = clrClass.split('_');
        var clrNum = clrAry[1];
        var clrTyp = clrAry[0];

        switch (parseInt(clrNum)) {
            case -1:
                if (clrTyp == "B")
                    clrName = "RGB(255, 255, 255)";
                else
                    clrName = "RGB(47,47,47)"; //1B1B1B
                break;
            case 0:
                clrName = "RGB(51,51,51)";
                break;
            case 1:
                clrName = "RGB(0, 0, 0)";
                break;
            case 2:
                clrName = "RGB(0, 255, 255)";
                break;
            case 3:
                clrName = "RGB(0, 0, 0)";
                break;
            case 4:
                clrName = "RGB(0, 0, 255)";
                break;
            case 5:
                clrName = "RGB(255, 255, 0)";
                break;
            case 6:
                clrName = "RGB(255, 255, 255)";
                break;
            case 7:
                clrName = "RGB(0, 0, 0)";
                break;
            case 8:
                clrName = "RGB(0, 0, 0)";
                break;
            case 9:
                clrName = "RGB(255, 0, 0)";
                break;
            case 10:
                clrName = "RGB(0, 255, 255)";
                break;
            case 11:
                clrName = "RGB(0, 0, 0)";
                break;
            case 12:
                clrName = "RGB(0, 255, 0)";
                break;
            case 13:
                clrName = "RGB(0, 0, 0)";
                break;
            case 14:
                clrName = "RGB(141, 160, 216)";
                break;
            case 15:
                clrName = "RGB(141, 160, 216)";
                break;
            case 16:
                clrName = "RGB(141, 160, 216)";
                break;
            case 17:
                clrName = "RGB(255, 255, 0)";
                break;
            case 18:
                clrName = "RGB(0, 0, 0)";
                break;
            case 19:
                clrName = "RGB(0, 0, 0)";
                break;
            case 20:
                clrName = "RGB(255, 255, 255)";
                break;
            case 21:
                clrName = "RGB(0, 0, 0)";
                break;
            case 22:
                clrName = "RGB(255, 255, 255)";
                break;
            case 23:
                clrName = "RGB(255, 255, 0)";
                break;
            case 24:
                clrName = "RGB(255, 0, 0)";
                break;
            case 25:
                clrName = "RGB(192, 192, 192)";
                break;
            case 26:
                clrName = "RGB(0, 0, 255)";
                break;
            case 27:
                clrName = "RGB(0, 255, 0)";
                break;
            case 28:
                clrName = "RGB(0, 0, 0)";
                break;
            case 29:
                clrName = "RGB(255, 255, 255)";
                break;
            case 30:
                clrName = "RGB(255, 255, 0)";
                break;
            case 31:
                clrName = "RGB(255, 0, 0)";
                break;
            case 32:
                //clrName = "RGB(128, 255, 255)";
                clrName = "RGB(91, 163, 240)";
                break;
            case 33:
                clrName = "RGB(255, 128, 255)";
                break;
            case 34:
                clrName = "RGB(255, 0, 0)";
                break;
            case 35:
                clrName = "RGB(0, 160, 0)";
                break;
            case 36:
                clrName = "RGB(0, 0, 0)";
                break;
            case 37:
                clrName = "RGB(240, 240, 240)";
                break;
            case 38:
                clrName = "RGB(255, 255, 0)";
                break;
            case 39:
                clrName = "RGB(255, 0, 0)";
                break;
            case 40:
                clrName = "RGB(0, 255, 255)";
                break;
            case 41:
                clrName = "RGB(64, 64, 255)";
                break;
            case 42:
                clrName = "RGB(128, 128, 255)";
                break;
            case 43:
                clrName = "RGB(0, 0, 0)";
                break;
            case 44:
                clrName = "RGB(192, 192, 192)";
                break;
            case 45:
                clrName = "RGB(255, 0, 0)";
                break;
            case 46:
                clrName = "RGB(255, 0, 0)";
                break;
            case 47:
                clrName = "RGB(0, 0, 0)";
                break;
            case 48:
                clrName = "RGB(0, 255, 0)";
                break;
            case 49:
                clrName = "RGB(160, 255, 160)";
                break;
            case 50:
                clrName = "RGB(255, 255, 255)";
                break;
            case 51:
                clrName = "RGB(255, 255, 0)";
                break;
            case 52:
                clrName = "RGB(192, 0, 0)";
                break;
            case 53:
                //clrName = "RGB(128, 255, 128)";
                clrName = "RGB(0, 128, 0)";
                break;
            case 54:
                clrName = "RGB(255, 128, 128)";
                break;
            case 55:
                //clrName = "RGB(128, 255, 255)";
                clrName = "RGB(91, 163, 240)";
                break;
            case 56:
                clrName = "RGB(255, 0, 255)";
                break;
            case 57:
                //clrName = "RGB(128, 255, 255)";
                clrName = "RGB(91, 163, 240)";
                break;
            case 58:
                clrName = "RGB(0, 128, 0)";
                break;
            case 59:
                clrName = "RGB(128, 128, 0)";
                break;
            case 60:
                clrName = "RGB(128, 0, 0)";
                break;
            case 61:
                clrName = "RGB(0, 255, 0)";
                break;
            case 62:
                clrName = "RGB(255, 255, 0)";
                break;
            case 63:
                clrName = "RGB(255, 0, 0)";
                break;
            case 64:
                clrName = "RGB(0, 0, 0)";
                break;
            case 65:
                clrName = "RGB(64, 64, 64)";
                break;
            case 66:
                clrName = "RGB(128, 128, 128)";
                break;
            case 67:
                clrName = "RGB(192, 192, 192)";
                break;
            case 68:
                clrName = "RGB(255, 255, 255)";
                break;
            case 69:
                clrName = "RGB(160, 160, 160)";
                break;
            case 70:
                clrName = "RGB(96, 96, 96)";
                break;
            case 71:
                clrName = "RGB(224, 224, 255)";
                break;
            case 72:
                clrName = "RGB(255, 224, 255)";
                break;
            case 73:
                clrName = "RGB(255, 224, 192)";
                break;
            case 74:
                clrName = "RGB(255, 255, 192)";
                break;
            case 75:
                clrName = "RGB(224, 255, 192)";
                break;
            case 76:
                clrName = "RGB(224, 208, 255)";
                break;
            case 77:
                clrName = "RGB(255, 208, 208)";
                break;
            case 78:
                clrName = "RGB(208, 224, 255)";
                break;
            case 79:
                clrName = "RGB(32,  32,  64)";
                break;
            case 80:
                clrName = "RGB(64,  32,  64)";
                break;
            case 81:
                clrName = "RGB(64,  32,  0)";
                break;
            case 82:
                clrName = "RGB(64,  64,  0)";
                break;
            case 83:
                clrName = "RGB(32,  64,  0)";
                break;
            case 84:
                clrName = "RGB(32,  32,  64)";
                break;
            case 85:
                clrName = "RGB(64,  32,  32)";
                break;
            case 86:
                clrName = "RGB(32,  32,  64)";
                break;
            case 87:
                clrName = "RGB(0, 0, 0)";
                break;
            case 88:
                clrName = "RGB(0, 0, 0)";
                break;
            case 89:
                clrName = "RGB(255, 255, 255)";
                break;
            case 90:
                clrName = "RGB(0, 255, 0)";
                break;
            case 91:
                clrName = "RGB(255, 0, 0)";
                break;
            case 92:
                clrName = "RGB(255, 255, 0)";
                break;
            case 93:
                clrName = "RGB(0, 0, 255)";
                break;
            case 94:
                clrName = "RGB(255, 0, 0)";
                break;
            case 95:
                clrName = "RGB(128, 0, 0)";
                break;
            case 96:
                clrName = "RGB(255, 255, 255)";
                break;
            case 97:
                clrName = "RGB(255, 255, 255)";
                break;
            case 98:
                clrName = "RGB(0, 0, 255)";
                break;
            case 99:
                clrName = "RGB(255, 255, 255)";
                break;
            case 100:
                clrName = "RGB(0, 0, 127)";
                break;
            case 101:
                clrName = "RGB(255, 255, 255)";
                break;
            case 102:
                clrName = "RGB(255, 255, 192)";
                break;
            case 103:
                clrName = "RGB(192, 255, 192)";
                break;
            case 104:
                clrName = "RGB(255, 255, 192)";
                break;
            case 105:
                clrName = "RGB(255, 192, 192)";
                break;
            case 106:
                clrName = "RGB(255, 64, 64)";
                break;
            case 107:
                clrName = "RGB(255, 0, 0)";
                break;
            case 108:
                clrName = "RGB(240, 240, 240)";
                break;
            case 109:
                clrName = "RGB(255, 192, 192)";
                break;
            case 110:
                clrName = "RGB(192, 255, 192)";
                break;
            case 111:
                clrName = "RGB(192, 192, 255)";
                break;
            case 112:
                clrName = "RGB(255, 0, 0)";
                break;
            case 113:
                clrName = "RGB(255, 255, 0)";
                break;
            case 114:
                clrName = "RGB(0, 255, 0)";
                break;
            case 115:
                clrName = "RGB(0, 0, 255)";
                break;
            case 116:
                clrName = "RGB(0, 255, 255)";
                break;
            case 117:
                clrName = "RGB(255, 0, 255)";
                break;
            case 118:
                clrName = "RGB(255, 128, 128)";
                break;
            case 119:
                clrName = "RGB(128, 128, 255)";
                break;
            case 120:
                clrName = "RGB(128, 255, 128)";
                break;
            case 121:
                clrName = "RGB(80, 80, 80)";
                break;
            case 122:
                clrName = "RGB(255, 255, 255)";
                break;
            case 123:
                clrName = "RGB(0, 0, 0)";
                break;
            case 124:
                clrName = "RGB(0, 0, 255)";
                break;
            case 125:
                clrName = "RGB(255, 0, 0)";
                break;
            case 126:
                clrName = "RGB(200, 200, 200)";
                break;
            case 127:
                clrName = "RGB(0, 0, 0)";
                break;
            case 128:
                clrName = "RGB(255, 255, 255)";
                break;
            case 129:
                clrName = "RGB(255,255,0)";
                break;
            case 130:
                clrName = "RGB(255, 0,0)";
                break;
            case 131:
                clrName = "RGB(0, 255,255)";
                break;
            case 132:
                clrName = "RGB(200,200,200)";
                break;
            case 133:
                clrName = "RGB(0, 0, 0)";
                break;
            case 134:
                clrName = "RGB(0, 255,255)";
                break;
            case 135:
                clrName = "RGB(255,255,0)";
                break;
            case 136:
                clrName = "RGB(0, 0, 0)";
                break;
            case 137:
                clrName = "RGB(128, 128, 128)";
                break;
            case 138:
                clrName = "RGB(192, 192, 192)";
                break;
            case 139:
                clrName = "RGB(255, 255, 255)";
                break;
            case 140:
                clrName = "RGB(255, 255, 0)";
                break;
            case 141:
                clrName = "RGB(0, 0, 0)";
                break;
            case 142:
                clrName = "RGB(0, 255, 255)";
                break;
            case 143:
                clrName = "RGB(0, 0, 0)";
                break;
            case 144:
                clrName = "RGB(255, 255, 255)";
                break;
            case 145:
                clrName = "RGB(255, 0, 0)";
                break;
            case 146:
                clrName = "RGB(255, 255, 255)";
                break;
            case 147:
                clrName = "RGB(0, 0, 255)";
                break;
            case 148:
                clrName = "RGB(255, 255, 255)";
                break;
            case 149:
                clrName = "RGB(255, 0, 0)";
                break;
            case 150:
                clrName = "RGB(0, 0, 0)";
                break;
            case 151:
                clrName = "RGB(0, 255, 255)";
                break;
            case 152:
                clrName = "RGB(255, 0, 0)";
                break;
            case 153:
                clrName = "RGB(255, 255, 255)";
                break;
            case 154:
                clrName = "RGB(0, 0, 255)";
                break;
            case 155:
                clrName = "RGB(255, 255, 255)";
                break;
            case 156:
                clrName = "RGB(0, 0, 0)";
                break;
            case 157:
                clrName = "RGB(0, 0, 0)";
                break;
            case 158:
                clrName = "RGB(0, 255, 255)";
                break;
            case 159:
                clrName = "RGB(0, 0, 0)";
                break;
            case 160:
                clrName = "RGB(255, 255, 0)";
                break;
            case 161:
                clrName = "RGB(0, 0, 0)";
                break;
            case 162:
                clrName = "RGB(0, 255, 255)";
                break;
            case 163:
                clrName = "RGB(0, 0, 0)";
                break;
            case 164:
                clrName = "RGB(0, 0, 255)";
                break;
            case 165:
                clrName = "RGB(255, 255, 0)";
                break;
            case 166:
                clrName = "RGB(255, 255, 255)";
                break;
            case 167:
                clrName = "RGB(0, 0, 255)";
                break;
            case 168:
                clrName = "RGB(255, 0, 0)";
                break;
            case 169:
                clrName = "RGB(255, 255, 255)";
                break;
            case 170:
                clrName = "RGB(255, 255, 255)";
                break;
            case 171:
                clrName = "RGB(0, 0, 0)";
                break;
            case 172:
                clrName = "RGB(128, 128, 128)";
                break;
            case 173:
                clrName = "RGB(0, 0, 0)";
                break;
            case 174:
                clrName = "RGB(64, 128, 64)";
                break;
            case 175:
                clrName = "RGB(255, 192, 0)";
                break;
            case 176:
                clrName = "RGB(255, 64, 64)";
                break;
            case 177:
                clrName = "RGB(0, 0, 0)";
                break;
            case 178:
                clrName = "RGB(255, 255, 128)";
                break;
            case 179:
                clrName = "RGB(128, 255, 128)";
                break;
            case 180:
                clrName = "RGB(224, 32, 255)";
                break;
            case 181:
                clrName = "RGB(255, 96, 96)";
                break;
            case 182:
                clrName = "RGB(240, 240, 240)";
                break;
            case 183:
                //clrName = "RGB(226, 19, 19)";
                clrName = "RGB(255, 0, 0)";
                break;
            case 184:
                //            clrName = "RGB(0, 255, 0)";
                clrName = "RGB(0, 128, 0)";
                break;
            case 185:
                clrName = "RGB(65, 76, 202)";
                break;
            case 186:
                clrName = "RGB(255, 255, 255)";
                break;
            default:
                clrName = "RGB(255, 255, 255)";
                break;
        }
    }
    catch (err) {
        var strerrordesc = "Function:getColorFromClass(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        //onJavascriptLog("VolSeparate.js", strerrordesc);
    }

    return clrName;
}