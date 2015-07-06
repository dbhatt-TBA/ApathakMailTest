var globalSelectedValue = '';
var IsChange = '';

var globaltxtValue1 = '';
var globaltxtValue2 = '';
var defaultFormat;
var priceEleCurrent;

var isPriceWidgetEnable = false;

var NumberFormat = {

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


///param1 int, param2  int
function GetReducedFraction(Numerator, Denominator) {
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
        var strerrordesc = "Function:GetReducedFraction(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

/// <summary>
/// Code for converting decimal price to given price format. Pass Number format as per above enum (0 t0 21)
/// Usage - ParsePriceForWidget(99.9645,8) ;
/// </summary>
///param1 double, param2 int, param3 int       
function ParsePriceForWidget(DecPrice, Format, ShowFullyFormatted) {
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
                return ShowFullOutput_PriceWidget(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
            else {
                return ShowPartitionedOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
        }
        else {
            //Set Min as per Number Format
            if (nBaseUnit == parseInt(NumberFormat.eBond))
                min = 32;
            else if (nBaseUnit >= parseInt(NumberFormat.eHalfs) && nBaseUnit <= parseInt(NumberFormat.eTwoFiftySixthsReduced))
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
                return ShowFullOutput_PriceWidget(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
            else {
                return ShowPartitionedOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
            }
        }
     }
    catch (err) {
        var strerrordesc = "Function:ParsePriceForWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}


///param1 double, param2 int, param3 int, param4 int, param5 int, param6 int
function ShowPartitionedOutput(DecPrice, Format, nPrice, nFrac1, nFrac2, nFrac3) {
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
        var strerrordesc = "Function:ShowPartitionedOutput(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}


///param1 double, param2 int, param3 int, param4 int, param5 int, param6 int
function ShowFullOutput_PriceWidget(DecPrice, Format, nPrice, nFrac1, nFrac2, nFrac3) {
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
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 4));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 10) //1/8R=eEighthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 8));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/8");
        }
        else if (Format == 11) //1/16R=eSixteenthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 16));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/16");
        }
        else if (Format == 12) //1/32R=eThirtySecondsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 32));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/32");
        }
        else if (Format == 13) //1/64R=eSixtyFourthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 64));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 14) //1/128R=eOneTwentyEightsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 128));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 15) //1/256R=eTwoFiftySixthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction(nFrac1, 256));
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
        var strerrordesc = "Function:ShowFullOutput_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

/////Code for coverting given partitioned formatted price into fully formatted & decimal format (# separated).
/////Pass Price  as 8#2#7#3 for 0.001 format
/////param1 string, param2 int
function DecimalPrice(Price, Format) {
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
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 4) + "^" + (dPrice + parseFloat(nFrac1) / 4));
                else
                    return (nPrice + "-" + nFrac1 + "/4" + "^" + (dPrice + parseFloat(nFrac1) / 4));
            }
            else if (Format == 10) //1/8R=eEighthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 8) + "^" + (dPrice + parseFloat(nFrac1) / 8));
                else
                    return (nPrice + "-" + nFrac1 + "/8" + "^" + (dPrice + parseFloat(nFrac1) / 8));
            }
            else if (Format == 11) //1/16R=eSixteenthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 16) + "^" + (dPrice + parseFloat(nFrac1) / 16));
                else
                    return (nPrice + "-" + nFrac1 + "/16" + "^" + (dPrice + parseFloat(nFrac1) / 16));
            }
            else if (Format == 12) //1/32R=eThirtySecondsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 32) + "^" + (dPrice + parseFloat(nFrac1) / 32));
                else
                    return (nPrice + "-" + nFrac1 + "/32" + "^" + (dPrice + parseFloat(nFrac1) / 32));
            }
            else if (Format == 13) //1/64R=eSixtyFourthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 64) + "^" + (dPrice + parseFloat(nFrac1) / 64));
                else
                    return (nPrice + "-" + nFrac1 + "/64" + "^" + (dPrice + parseFloat(nFrac1) / 64));
            }
            else if (Format == 14) //1/128R=eOneTwentyEightsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 128) + "^" + (dPrice + parseFloat(nFrac1) / 128));
                else
                    return (nPrice + "-" + nFrac1 + "/128" + "^" + (dPrice + parseFloat(nFrac1) / 128));
            }
            else if (Format == 15) //1/256R=eTwoFiftySixthsReduced
            {
                if (nFrac1 == 0)
                    return (nPrice + "^" + dPrice);
                else if (nFrac1 % 2 == 0)
                    return (nPrice + "-" + GetReducedFraction(nFrac1, 256) + "^" + (dPrice + parseFloat(nFrac1) / 256));
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
        var strerrordesc = "Function:DecimalPrice(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function display_PriceWidget(eleIDForWidget, orgVal, widgetVal, defFormat, priceEle) {
    try {

        if ($("#hdnMobileWidgetEnable").val() == "1") {
            return PWM_Func_display_PriceWidget(eleIDForWidget, orgVal, widgetVal, defFormat, priceEle);
        }

        priceEleCurrent = priceEle;

        defaultFormat = ConvertFormatTypeIndex(defFormat);

        if (widgetVal == "" || widgetVal == undefined || isNaN(widgetVal))
            widgetVal = "0";

        $('#txtBeastValue').attr("name", widgetVal);

        $('#txtBeastValue').val(orgVal);
        $('#txtLastClickedEleInfo').val(eleIDForWidget);

        $('#txtValue2').val(parseFloat(widgetVal));

        //new
        $('#FormatSelection').val(defaultFormat);
        //new

        format_selection_value();

        isPriceWidgetEnable = true;

        $('#modalPriceWidget').modal({ backdrop: true, keyboard: true });
        

        $('#hdnWgtElement').val($(priceEle).attr('id'));

        positionWidget($(priceEle).attr('id'));

        $('#modalPriceWidget').focus();        
    }
    catch (err) {
        var strerrordesc = "Function:display_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function hidePriceWidget_Disp() {
    priceEleCurrent = null;
    isPriceWidgetEnable = false; 
}

function Save_PriceWidget() {
    try {

        if (IsChange == 'TRUE') {
            IsChange = '';
            $('#txtBeastValue').val($('#txtValue1').val());
            $('#txtBeastValue')[0].name = $('#txtValue2').val();

            var eleAryInfo = $('#txtLastClickedEleInfo').val().split('^');
            var instanceType = eleAryInfo[0];
            var paraValues = eleAryInfo[1] + "^" + eleAryInfo[2] + "^" + eleAryInfo[3];
            var idValPair = eleAryInfo[4] + "#" + $('#txtValue2').val();

            //if ($(this).val() != "")
            //                alert(eleAryInfo);
            //                alert(instanceType + "#" + paraValues + "   ==IDValPair==   " + idValPair);

            SendToBeast(instanceType + "#" + paraValues, idValPair);
        }
        $('#modalPriceWidget').modal(false);
        hidePriceWidget_Disp();
    }
    catch (err) {
        var strerrordesc = "Function:Save_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function Close_PriceWidget() {
    try {
        $('#modalPriceWidget').modal(false);
        hidePriceWidget_Disp();
    }
    catch (err) {
        var strerrordesc = "Function:Close_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function ShowFullValue_PriceWidget() {
    try {
        var FullValue1;

        if ($('#txtBeastValue').attr("name") != "") {
            FullValue1 = ParsePriceForWidget(Math.abs(parseFloat($('#txtValue2').val())), parseInt($('#FormatSelection').val()), 0);
        }

        //if (globaltxtValue2 == "")
            globaltxtValue2 = $('#txtValue2').val();

        PartitionedValue_PriceWidget(FullValue1);
    }
    catch (err) {
        var strerrordesc = "Function:ShowFullValue_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function PartitionedValue_PriceWidget(varFullValue1) {
    try {
        var PartitionValue;
        if ($('#txtBeastValue')[0].name != "")
            PartitionValue = ParsePriceForWidget(Math.abs(parseFloat($('#txtValue2').val())), parseInt($('#FormatSelection').val()), 1);

        $('#txtValue1').val(varFullValue1);
        if (globaltxtValue2.indexOf("-")) {
        }
        else {
            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                $('#txtValue1').val("-" + $('#txtValue1').val());
            else
                $('#txtValue1').val($('#txtValue1').val());
        }
        SetPriceWidget(PartitionValue);
    }
    catch (err) {
        var strerrordesc = "Function:PartitionedValue_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

//////

function SetPriceWidget(PartitionValue) {
    try {
        if ($('#FormatSelection').val() == NumberFormat.e0DecimalPlaces)
            PartitionValue = PartitionValue + "#";

        globalSelectedValue = PartitionValue;
        SetFirstTD_PriceWidget(Math.abs(PartitionValue.split('#')[0]));
        if ($('#FormatSelection').val() == NumberFormat.e0DecimalPlaces)
            return;

        SetSecondTD_PriceWidget(PartitionValue.split('#')[1]);

        //if ($('#FormatSelection').val() == NumberFormat.eHalfs || $('#FormatSelection').val() == NumberFormat.eQuarters || $('#FormatSelection').val() == NumberFormat.eEighths || $('#FormatSelection').val() == NumberFormat.eSixteenths || $('#FormatSelection').val() == NumberFormat.eQuartersReduced || $('#FormatSelection').val() == NumberFormat.eEighthsReduced || $('#FormatSelection').val() == NumberFormat.eSixteenthsReduced || $('#FormatSelection').val() == NumberFormat.e1DecimalPlace)
        if ($('#FormatSelection').val() == NumberFormat.eHalfs || $('#FormatSelection').val() == NumberFormat.eQuarters || $('#FormatSelection').val() == NumberFormat.eEighths || $('#FormatSelection').val() == NumberFormat.eQuartersReduced || $('#FormatSelection').val() == NumberFormat.eEighthsReduced || $('#FormatSelection').val() == NumberFormat.e1DecimalPlace) /// || $('#FormatSelection').val() == NumberFormat.eSixteenths || $('#FormatSelection').val() == NumberFormat.eSixteenthsReduced
        {
            /// || $('#FormatSelection').val() == NumberFormat.eSixteenths || $('#FormatSelection').val() == NumberFormat.eSixteenthsReduced
            return;
        }

        if ($('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
            SetThirdTD_PriceWidget(PartitionValue.split('#')[2]);
        else
            SetThirdTD_PriceWidget(PartitionValue.split('#')[1]);

        if ($('#FormatSelection').val() == NumberFormat.eThirtySeconds || $('#FormatSelection').val() == NumberFormat.eThirtySecondsReduced || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
            return;

        if ($('#FormatSelection').val() == NumberFormat.eBond)
            SetForthTD_PriceWidget(PartitionValue.split('#')[2]);
        else
            SetForthTD_PriceWidget(PartitionValue.split('#')[3]);
    }
    catch (err) {
        var strerrordesc = "Function:SetPriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function SetFirstTD_PriceWidget(SelectedvalueFirstTD) {
    try {
        var btnVal = StartEndPointFirstTD_PriceWidget(0, 5, parseInt(SelectedvalueFirstTD));

        var i = parseInt(btnVal.split('#')[0]);

        for (var j = 3; j < 9; j++) {

            $('#btn' + j.toString() + 'td1tr' + j.toString()).val(i);
            if ($('#btn' + j.toString() + 'td1tr' + j.toString()).val().toString() == SelectedvalueFirstTD.toString()) {
                $('#btn' + j.toString() + 'td1tr' + j.toString()).removeClass('btn');
                $('#btn' + j.toString() + 'td1tr' + j.toString()).addClass('btn btn-info');
            }
            else {
                $('#btn' + j.toString() + 'td1tr' + j.toString()).removeClass('btn btn-info');
                $('#btn' + j.toString() + 'td1tr' + j.toString()).addClass('btn');
            }

            i++;
        }
    }
    catch (err) {
        var strerrordesc = "Function:SetFirstTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function StartEndPointFirstTD_PriceWidget(startvalue, endvalue, SelectedvalueFirstTD) {
    try {
        var temp1 = startvalue;
        var temp2 = endvalue;

        while (1) {
            if ((temp1 <= Math.abs(SelectedvalueFirstTD)) && (temp2 >= Math.abs(SelectedvalueFirstTD))) {
                break;
            }
            temp1 = temp1 + 6;
            temp2 = temp2 + 6;
        }

        return temp1 + "#" + temp2;
    }
    catch (err) {
        var strerrordesc = "Function:StartEndPointFirstTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}


function SetSecondTD_PriceWidget(SelectedvalueSecondTD) {
    try {

        var btnVal = '';

        if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
            btnVal = StartEndPointSecondTD_PriceWidget(0, 9, parseInt(SelectedvalueSecondTD.split('/')[0]));
        }
        else {
            btnVal = StartEndPointSecondTD_PriceWidget(0, 7, parseInt(SelectedvalueSecondTD.split('/')[0]));
        }

        var i = parseInt(btnVal.split('#')[0]);

        var td2btnCount = 0;
        for (var j = 1; j < 11; j++) {

            if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
            }
            else {
                if (j == 9 || j == 10) {
                    continue;
                }
            }

            if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {

                $('#btn' + j.toString() + 'td2tr' + j.toString()).val(i);
                if ($('#btn' + j.toString() + 'td2tr' + j.toString()).val().toString() == SelectedvalueSecondTD.toString()) {
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).addClass('btn');
                }

                i++;

                if (parseInt(td2btnCount) == parseInt(9))
                    return;
            }
            else {
                var Maxtd2btnCount = document.getElementById('FormatSelection').options[$('#FormatSelection').val()].innerHTML.split('/')[1].replace("R", "").replace("+", "");

                $('#btn' + j.toString() + 'td2tr' + j.toString()).val(i.toString() + '/' + Maxtd2btnCount.toString());
                if ($('#btn' + j.toString() + 'td2tr' + j.toString()).val().toString() == SelectedvalueSecondTD.toString()) {
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'td2tr' + j.toString()).addClass('btn');
                }

                i++;

                if (parseInt(td2btnCount) == parseInt(Maxtd2btnCount) - 1)
                    return;
            }
            td2btnCount++;
        }
    }
    catch (err) {
        var strerrordesc = "Function:SetSecondTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}


function StartEndPointSecondTD_PriceWidget(startvalue, endvalue, SelectedvalueSecondTD) {
    try {
        var temp1 = startvalue;
        var temp2 = endvalue;

        while (1) {
            if ((temp1 <= Math.abs(SelectedvalueSecondTD)) && (temp2 >= Math.abs(SelectedvalueSecondTD)))
                break;
            else if (temp2 > SelectedvalueSecondTD)
                break;

            temp1 = temp1 + 16;
            temp2 = temp2 + 16;
        }

        return temp1 + "#" + temp2;
    }
    catch (err) {
        var strerrordesc = "Function:StartEndPointSecondTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("priceWidgetScript.js", strerrordesc);
    }
}

function SetThirdTD_PriceWidget(SelectedvalueThirdTD) {
    try {
        if (SelectedvalueThirdTD == undefined)
            return;

        var btnVal, i;
        if ($('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {

        }
        else {
            btnVal = StartEndPointThirdTD_PriceWidget(8, 15, parseInt(SelectedvalueThirdTD.split('/')[0]));
            i = parseInt(btnVal.split('#')[0]);
        }

        var td3btnCount = 0;
        for (var j = 1; j < 11; j++) {

            if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
            }
            else {
                if (j == 9 || j == 10) {
                    continue;
                }
            }

            if ($('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {

                $('#btn' + j.toString() + 'td3tr' + j.toString()).val(td3btnCount);
                if ($('#btn' + j.toString() + 'td3tr' + j.toString()).val().toString() == SelectedvalueThirdTD.toString()) {
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).addClass('btn');
                }

                i++;

                if (parseInt(td3btnCount) == parseInt(9))
                    return;

            }
            else {

                var Maxtd3btnCount = document.getElementById('FormatSelection').options[$('#FormatSelection').val()].innerHTML.split('/')[1].replace("R", "").replace("+", "");
                $('#btn' + j.toString() + 'td3tr' + j.toString()).val(i.toString() + "/" + Maxtd3btnCount.toString());

                if ($('#btn' + j.toString() + 'td3tr' + j.toString()).val().toString() == SelectedvalueThirdTD.toString()) {
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).removeClass('btn');
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).addClass('btn btn-info');
                }
                else {
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).removeClass('btn btn-info');
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).addClass('btn');
                }

                var SecondTDButtonVal = $('#btn' + j.toString() + 'td3tr' + j.toString()).val().split('/')[0].replace("R", "").replace("+", "");
                $('#btn' + j.toString() + 'td2tr' + j.toString()).val(parseInt(parseInt(SecondTDButtonVal) - 8).toString() + "/" + Maxtd3btnCount.toString());

                i++;

                if (parseInt(td3btnCount) == parseInt(Maxtd3btnCount) - 1)
                    return;
            }
            td3btnCount++;
        }
    }
    catch (err) {
        var strerrordesc = "Function:SetThirdTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("PriceWidgetScript.js", strerrordesc);
    }
}

function StartEndPointThirdTD_PriceWidget(startvalue, endvalue, SelectedvalueThirdTD) {
    try {
        var temp1 = startvalue;
        var temp2 = endvalue;

        while (1) {
            if ((temp1 <= Math.abs(SelectedvalueThirdTD)) && (temp2 >= Math.abs(SelectedvalueThirdTD)))
                break;
            else if (temp2 > SelectedvalueThirdTD)
                break;

            temp1 = temp1 + 16;
            temp2 = temp2 + 16;
        }

        return temp1 + "#" + temp2;
    }
    catch (err) {
        var strerrordesc = "Function:StartEndPointThirdTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("PriceWidgetScript.js", strerrordesc);
    }
}

function SetForthTD_PriceWidget(SelectedvalueForthTD) {
    try {
        if (SelectedvalueForthTD == undefined)
            return;

        var td4btnCount = 0;
        for (var j = 1; j < 11; j++) {
            $('#btn' + j.toString() + 'td4tr' + j.toString()).val(td4btnCount);
            if ($('#btn' + j.toString() + 'td4tr' + j.toString()).val().toString() == SelectedvalueForthTD.toString()) {
                $('#btn' + j.toString() + 'td4tr' + j.toString()).removeClass('btn');
                $('#btn' + j.toString() + 'td4tr' + j.toString()).addClass('btn btn-info');
            }
            else {
                $('#btn' + j.toString() + 'td4tr' + j.toString()).removeClass('btn btn-info');
                $('#btn' + j.toString() + 'td4tr' + j.toString()).addClass('btn');
            }

            if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces) {
                if (parseInt(td4btnCount) == parseInt(9))
                    return;
            }
            else if ($('#FormatSelection').val() == NumberFormat.eBond) {
                if (parseInt(td4btnCount) == parseInt(7))
                    return;
            }
            else if ($('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs) {
                if (parseInt(td4btnCount) == parseInt(1))
                    return;
            }
            else if ($('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
                if (parseInt(td4btnCount) == parseInt(3))
                    return;
            }
            td4btnCount++;
        }
    }
    catch (err) {
        var strerrordesc = "Function:SetForthTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("PriceWidgetScript.js", strerrordesc);
    }
}



//defaultFormat = NumberFormat.eEighthsReduced;

//        $(document).ready(function () {
//            try {
//                $('#txtValue2').val(parseFloat($('#txtBeastValue')[0].name));
//                format_selection_value();
//            }
//            catch (err) {
//                alert('$(document).ready');
//            }
//        });



function format_selection_value() {
    try {
        //var value = document.getElementById('FormatSelection').value;
        var value = $('#FormatSelection').val();
        Bind_Price_Widget();
        ShowFullValue_PriceWidget();
    }
    catch (err) {
        var strerrordesc = "Function:format_selection_value(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("PriceWidgetScript.js", strerrordesc);
    }
}


function Bind_Price_Widget() {
    try {
        //alert($('#FormatSelection').val());

        $("#table_widget_Price").html("");

        for (var i = 0; i < 11; i++) {
            var varHtmlTemplate = price_widget_Template_tablerow();
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TR]', "tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD1]', "td1tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD2]', "td2tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD3]', "td3tr" + i.toString());
            varHtmlTemplate = ReplaceAll(varHtmlTemplate, '[TD4]', "td4tr" + i.toString());

            $("#table_widget_Price").append(varHtmlTemplate);
        }

        FirstTD_PriceWidget();

        if ($('#FormatSelection').val() == NumberFormat.e0DecimalPlaces)
            return;

        SecondTD_PriceWidget();

        if ($('#FormatSelection').val() == NumberFormat.eHalfs || $('#FormatSelection').val() == NumberFormat.eQuarters || $('#FormatSelection').val() == NumberFormat.eEighths || $('#FormatSelection').val() == NumberFormat.eQuartersReduced || $('#FormatSelection').val() == NumberFormat.eEighthsReduced || $('#FormatSelection').val() == NumberFormat.e1DecimalPlace) /// || $('#FormatSelection').val() == NumberFormat.eSixteenths || $('#FormatSelection').val() == NumberFormat.eSixteenthsReduced
            return;

        ThirdTD_PriceWidget();

        if ($('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eSixteenths || $('#FormatSelection').val() == NumberFormat.eSixteenthsReduced) // $('#FormatSelection').val() == NumberFormat.eThirtySeconds || $('#FormatSelection').val() == NumberFormat.eThirtySecondsReduced
            return;

        ForthTD_PriceWidget();
    }
    catch (err) {
        var strerrordesc = "Function:Bind_Price_Widget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
        onJavascriptLog("PriceWidgetScript.js", strerrordesc);
    }
}

function FirstTD_PriceWidget() {

    //START ******************************************************** First TD ********************************************************************
    //td1tr[]           First td(column) for All tr(row)
    //btn[]td1tr[]      button  First td(column) for All tr(row)
    {
        try {
            var td1btnCount = 0;
            for (var j = 0; j < 11; j++) {

                if (j == 0) {
                    $('#td1tr' + j.toString()).html("Price").attr("class", "priceWidgetHeader");
                    continue;
                }

                var varbuttonTemplate = price_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "td1tr" + j.toString());
                $('#td1tr' + j.toString()).html(varbuttonTemplate);

                if (j == 1) {
                    $('#btn1td1tr1').val("Negative");

                    var txtValue1 = $('#txtValue1').val();
                    var txtValue2 = $('#txtValue2').val();

                    //                            if (txtValue1.indexOf("-")) {
                    //                                $('#btn1td1tr1').removeClass('btn btn-info');
                    //                                $('#btn1td1tr1').addClass('btn');
                    //                            }
                    //                            else {
                    //                                $('#btn1td1tr1').removeClass('btn');
                    //                                $('#btn1td1tr1').addClass('btn btn-info');
                    //                            }

                    if (txtValue2.indexOf("-")) {
                        $('#btn1td1tr1').removeClass('btn btn-info');
                        $('#btn1td1tr1').addClass('btn');
                    }
                    else {
                        $('#btn1td1tr1').removeClass('btn');
                        $('#btn1td1tr1').addClass('btn btn-info');
                    }


                    $("#btn1td1tr1").click(function () {
                        //alert("Negative");
                        IsChange = 'TRUE';
                        var txtValue1 = $('#txtValue1').val();
                        var txtValue2 = $('#txtValue2').val();

                        if (txtValue1 != "0" || txtValue2 != "0.000000") {

                            if (txtValue1 == "0" || (txtValue2 == "0.000000" || txtValue2 == "0" || txtValue2 == "0.0" || txtValue2 == "0.00" || txtValue2 == "0.000"))
                                return;

                            if (txtValue1.indexOf("-")) {
                                $('#txtValue1').val("-" + txtValue1);
                                globaltxtValue1 = "-" + txtValue1;

                                $('#btn1td1tr1').removeClass('btn');
                                $('#btn1td1tr1').addClass('btn btn-info');
                            }
                            else {
                                $('#txtValue1').val(txtValue1.replace("-", ""));
                                globaltxtValue1 = txtValue1.replace("-", "");

                                $('#btn1td1tr1').removeClass('btn btn-info');
                                $('#btn1td1tr1').addClass('btn');
                            }

                            if (txtValue2.indexOf("-")) {
                                $('#txtValue2').val("-" + txtValue2);
                                globaltxtValue2 = "-" + txtValue2;

                                $('#btn1td1tr1').removeClass('btn');
                                $('#btn1td1tr1').addClass('btn btn-info');
                            }
                            else {
                                $('#txtValue2').val(txtValue2.replace("-", ""));
                                globaltxtValue2 = txtValue2.replace("-", "");

                                $('#btn1td1tr1').removeClass('btn btn-info');
                                $('#btn1td1tr1').addClass('btn');
                            }

                        }

                    });
                }
                else if (j == 2) {
                    $('#btn2td1tr2').val("Less");

                    $("#btn2td1tr2").click(function () {
                        //alert("Less");
                        var q = 3;
                        for (q = 3; q < 9; q++) {
                            var intLess = 0;
                            intLess = parseInt($('#btn' + q.toString() + 'td1tr' + q.toString()).val()) - 6;
                            if (intLess < 0)
                                return;
                            $('#btn' + q.toString() + 'td1tr' + q.toString()).val(intLess);

                            if (Math.abs(parseInt(globalSelectedValue.split('#')[0])) == intLess) {
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).removeClass('btn');
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).addClass('btn btn-info');
                            }
                            else {
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).removeClass('btn btn-info');
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).addClass('btn');
                            }
                        }
                    });

                }
                else if (j == 9) {
                    $('#btn9td1tr9').val("More");

                    $("#btn9td1tr9").click(function () {
                        //alert("More");
                        var q = 3;
                        for (q = 3; q < 9; q++) {
                            var intMore = 0;
                            intMore = parseInt($('#btn' + q.toString() + 'td1tr' + q.toString()).val()) + 6;
                            $('#btn' + q.toString() + 'td1tr' + q.toString()).val(intMore);

                            if (Math.abs(parseInt(globalSelectedValue.split('#')[0])) == intMore) {
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).removeClass('btn');
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).addClass('btn btn-info');
                            }
                            else {
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).removeClass('btn btn-info');
                                $('#btn' + q.toString() + 'td1tr' + q.toString()).addClass('btn');
                            }
                        }

                    });

                }
                else if (j == 10) {
                    $('#btn10td1tr10').val("==>100");

                    $("#btn10td1tr10").click(function () {
                        //alert("==>100");

                        //                                var ddl = document.getElementById('FormatSelection');
                        //                                var opts = ddl.options.length;
                        //                                for (var i = 0; i < opts; i++) {
                        //                                    if (ddl.options[i].value == defaultFormat) {
                        //                                        ddl.options[i].selected = true;
                        //                                        break;
                        //                                    }
                        //                                }


                        $('#txtValue1').val('100');
                        $('#txtValue2').val('100.000000');

                        if (globaltxtValue2.indexOf("-")) {
                            $('#txtValue2').val("100.000000");
                        }
                        else {
                            $('#txtValue2').val("-100.000000");
                        }

                        if (globaltxtValue2.indexOf("-")) {
                            $('#txtValue1').val("100");
                        }
                        else {
                            $('#txtValue1').val("-100");
                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();
                    });
                }
                else {
                    $('#btn' + j.toString() + 'td1tr' + j.toString()).val(td1btnCount.toString());
                    $('#btn' + j.toString() + 'td1tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        var iArray = globalSelectedValue.split("#").length;
                        var globalSelectedValue1Temp = $(this).val() + "#" + globalSelectedValue.split("#")[1];

                        if ($('#FormatSelection').val() == NumberFormat.eBond || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
                            globalSelectedValue1Temp = globalSelectedValue1Temp + "#" + globalSelectedValue.split("#")[2];
                        else if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
                            globalSelectedValue1Temp = globalSelectedValue1Temp + "#" + globalSelectedValue.split("#")[2] + "#" + globalSelectedValue.split("#")[3];

                        globalSelectedValue = globalSelectedValue1Temp;
                        var newValue = DecimalPrice(globalSelectedValue, $('#FormatSelection').val());
                        $('#txtValue2').val(newValue.split('^')[1]);

                        if (globaltxtValue2.indexOf("-")) {
                        }
                        else {
                            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                                $('#txtValue2').val("-" + $('#txtValue2').val());
                            else
                                $('#txtValue2').val($('#txtValue2').val());
                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();

                    });
                    td1btnCount++;
                }
            }
        }
        catch (err) {
            var strerrordesc = "Function:FirstTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("PriceWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** First TD ********************************************************************

}

function SecondTD_PriceWidget() {
    //START ****************************************************** Second TD ********************************************************************
    //td2tr[]           Second td(column) for All tr(row)
    //btn[]td2tr[]      button  Second td(column) for All tr(row)
    {
        try {
            var td2btnCount = 0;
            for (var j = 0; j < 11; j++) {

                if (j == 0) {
                    if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
                        $('#td2tr' + j.toString()).html(".1 Digit").attr("class", "priceWidgetHeader");
                        continue;
                    }
                    else {
                        $('#td2tr' + j.toString()).html("Fraction").attr("class", "priceWidgetHeader");
                        continue;
                    }
                }

                if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
                }
                else {
                    if (j == 9 || j == 10) {
                        continue;
                    }
                }


                var varbuttonTemplate = price_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "td2tr" + j.toString());
                $('#td2tr' + j.toString()).html(varbuttonTemplate);

                if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {

                    $('#btn' + j.toString() + 'td2tr' + j.toString()).val(td2btnCount.toString());

                    $('#btn' + j.toString() + 'td2tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        var iArray = globalSelectedValue.split("#").length;
                        var globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + $(this).val();

                        if ($('#FormatSelection').val() == NumberFormat.eBond || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
                            globalSelectedValue1Temp = globalSelectedValue1Temp + "#" + globalSelectedValue.split("#")[2];
                        else if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
                            globalSelectedValue1Temp = globalSelectedValue1Temp + "#" + globalSelectedValue.split("#")[2] + "#" + globalSelectedValue.split("#")[3];

                        globalSelectedValue = globalSelectedValue1Temp;
                        var newValue = DecimalPrice(globalSelectedValue, $('#FormatSelection').val());
                        $('#txtValue2').val(newValue.split('^')[1]);

                        if (globaltxtValue2.indexOf("-")) {
                        }
                        else {
                            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                                $('#txtValue2').val("-" + $('#txtValue2').val());
                            else
                                $('#txtValue2').val($('#txtValue2').val());
                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();

                    });

                    if (parseInt(td2btnCount) == parseInt(9))
                        return;

                }
                else {

                    var Maxtd2btnCount = document.getElementById('FormatSelection').options[$('#FormatSelection').val()].innerHTML.split('/')[1].replace("R", "").replace("+", "");

                    $('#btn' + j.toString() + 'td2tr' + j.toString()).val(td2btnCount.toString() + '/' + Maxtd2btnCount.toString());

                    $('#btn' + j.toString() + 'td2tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        var iArray = globalSelectedValue.split("#").length;
                        var globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + $(this).val();

                        if ($('#FormatSelection').val() == NumberFormat.eBond || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
                            globalSelectedValue1Temp = globalSelectedValue1Temp + "#" + globalSelectedValue.split("#")[2];
                        else if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
                            globalSelectedValue1Temp = globalSelectedValue1Temp + "#" + globalSelectedValue.split("#")[2] + "#" + globalSelectedValue.split("#")[3];

                        globalSelectedValue = globalSelectedValue1Temp;
                        var newValue = DecimalPrice(globalSelectedValue, $('#FormatSelection').val());
                        $('#txtValue2').val(newValue.split('^')[1]);

                        if (globaltxtValue2.indexOf("-")) {
                        }
                        else {
                            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                                $('#txtValue2').val("-" + $('#txtValue2').val());
                            else
                                $('#txtValue2').val($('#txtValue2').val());

                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();

                    });

                    if (parseInt(td2btnCount) == parseInt(Maxtd2btnCount) - 1)
                        return;

                }


                td2btnCount++;
            }
        }
        catch (err) {
            var strerrordesc = "Function:SecondTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("priceWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Second TD ********************************************************************
}


function ThirdTD_PriceWidget() {
    //START ****************************************************** Third TD ********************************************************************
    //td3tr[]           Third td(column) for All tr(row)
    //btn[]td3tr[]      button  Third td(column) for All tr(row)
    {
        try {
            var td3btnCount = 0;
            for (var j = 0; j < 11; j++) {

                if (j == 0) {
                    if ($('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
                        $('#td3tr' + j.toString()).html(".01 Digit").attr("class", "priceWidgetHeader");
                        continue;
                    }
                    else {
                        continue;
                    }
                }

                if ($('#FormatSelection').val() == NumberFormat.e1DecimalPlace || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
                }
                else {
                    if (j == 9 || j == 10) {
                        continue;
                    }
                }

                var varbuttonTemplate = price_widget_Template_button();
                varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "td3tr" + j.toString());
                $('#td3tr' + j.toString()).html(varbuttonTemplate);

                if ($('#FormatSelection').val() == NumberFormat.e2DecimalPlaces || $('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {

                    $('#btn' + j.toString() + 'td3tr' + j.toString()).val(td3btnCount.toString());
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';
                        var iArray = globalSelectedValue.split("#").length;

                        if ($('#FormatSelection').val() == NumberFormat.eBond || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + globalSelectedValue.split("#")[1] + "#" + $(this).val();
                        else if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + globalSelectedValue.split("#")[1] + "#" + $(this).val() + "#" + globalSelectedValue.split("#")[3];
                        else
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + $(this).val();

                        globalSelectedValue = globalSelectedValue1Temp;
                        var newValue = DecimalPrice(globalSelectedValue, $('#FormatSelection').val());
                        $('#txtValue2').val(newValue.split('^')[1]);

                        if (globaltxtValue2.indexOf("-")) {
                        }
                        else {
                            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                                $('#txtValue2').val("-" + $('#txtValue2').val());
                            else
                                $('#txtValue2').val($('#txtValue2').val());
                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();
                    });

                    if (parseInt(td3btnCount) == parseInt(9))
                        return;

                }
                else {

                    var Maxtd3btnCount = document.getElementById('FormatSelection').options[$('#FormatSelection').val()].innerHTML.split('/')[1].replace("R", "").replace("+", "");
                    var SecondTDButtonVal = $('#btn' + j.toString() + 'td2tr' + j.toString()).val().split('/')[0].replace("R", "").replace("+", "");
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).val(parseInt(parseInt(SecondTDButtonVal) + 8).toString() + "/" + Maxtd3btnCount.toString());
                    $('#btn' + j.toString() + 'td3tr' + j.toString()).click(function () {
                        //alert($(this).val());

                        IsChange = 'TRUE';

                        var iArray = globalSelectedValue.split("#").length;

                        if ($('#FormatSelection').val() == NumberFormat.eBond || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + $(this).val() + "#" + globalSelectedValue.split("#")[2];
                        else if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + globalSelectedValue.split("#")[1] + "#" + $(this).val() + "#" + globalSelectedValue.split("#")[3];
                        else
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + $(this).val();

                        globalSelectedValue = globalSelectedValue1Temp;
                        var newValue = DecimalPrice(globalSelectedValue, $('#FormatSelection').val());
                        $('#txtValue2').val(newValue.split('^')[1]);

                        if (globaltxtValue2.indexOf("-")) {
                        }
                        else {
                            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                                $('#txtValue2').val("-" + $('#txtValue2').val());
                            else
                                $('#txtValue2').val($('#txtValue2').val());
                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();
                    });

                    if (parseInt(td3btnCount) == parseInt(Maxtd3btnCount) - 1)
                        return;

                }
                td3btnCount++;
            }
        }
        catch (err) {
            var strerrordesc = "Function:ThirdTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("priceWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Third TD ********************************************************************
}


function ForthTD_PriceWidget() {
    //START ****************************************************** Forth TD ********************************************************************
    //td4tr[]           Forth td(column) for All tr(row)
    //btn[]td4tr[]      button  Forth td(column) for All tr(row)
    {
        try {
            var td4btnCount = 0;
            for (var j = 0; j < 11; j++) {

                if ($('#FormatSelection').val() == NumberFormat.eSixtyFourths || $('#FormatSelection').val() == NumberFormat.eOneTwentyEights || $('#FormatSelection').val() == NumberFormat.eTwoFiftySixths || $('#FormatSelection').val() == NumberFormat.eSixtyFourthsReduced || $('#FormatSelection').val() == NumberFormat.eOneTwentyEightsReduced || $('#FormatSelection').val() == NumberFormat.eTwoFiftySixthsReduced || $('#FormatSelection').val() == NumberFormat.eThirtySeconds || $('#FormatSelection').val() == NumberFormat.eThirtySecondsReduced || ($('#FormatSelection').val() == NumberFormat.eBond && (j == 9 || j == 10))) {

                    if (j == 9) {
                        var varbuttonTemplate = price_widget_Template_button();
                        varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "td4tr" + j.toString());
                        $('#td4tr' + j.toString()).html(varbuttonTemplate);

                        $('#btn' + j.toString() + 'td4tr' + j.toString()).val('Previous');
                        $('#btn' + j.toString() + 'td4tr' + j.toString()).attr('title', 'Previous Fractions');

                        $('#btn' + j.toString() + 'td4tr' + j.toString()).click(function () {
                            //alert($(this).val());

                            var q = 1;
                            for (q = 1; q < 9; q++) {
                                var intLess = 0;
                                intLess = parseInt($('#btn' + q.toString() + 'td2tr' + q.toString()).val().split('/')[0]) - 16;
                                if (intLess < 0)
                                    return;
                                $('#btn' + q.toString() + 'td2tr' + q.toString()).val(intLess + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString());


                                if (globalSelectedValue.split('#')[1] == intLess + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString()) {
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).removeClass('btn');
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).addClass('btn btn-info');
                                }
                                else {
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).removeClass('btn btn-info');
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).addClass('btn');
                                }

                            }

                            var q = 1;
                            for (q = 1; q < 9; q++) {
                                var intLess = 0;
                                intLess = parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[0]) - 16;
                                if (intLess < 0)
                                    return;
                                $('#btn' + q.toString() + 'td3tr' + q.toString()).val(intLess + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString());

                                if (globalSelectedValue.split('#')[1] == intLess + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString()) {
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).removeClass('btn');
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).addClass('btn btn-info');
                                }
                                else {
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).removeClass('btn btn-info');
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).addClass('btn');
                                }
                            }

                        });

                    }
                    else if (j == 10) {
                        var varbuttonTemplate = price_widget_Template_button();
                        varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "td4tr" + j.toString());
                        $('#td4tr' + j.toString()).html(varbuttonTemplate);

                        $('#btn' + j.toString() + 'td4tr' + j.toString()).val('Next');
                        $('#btn' + j.toString() + 'td4tr' + j.toString()).attr('title', 'Next Fractions');

                        $('#btn' + j.toString() + 'td4tr' + j.toString()).click(function () {
                            //alert($(this).val());

                            var q = 1;
                            for (q = 1; q < 9; q++) {
                                var intMore = 0;
                                intMore = parseInt($('#btn' + q.toString() + 'td2tr' + q.toString()).val().split('/')[0]) + 16;

                                if (intMore >= parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]))
                                    return;
                                $('#btn' + q.toString() + 'td2tr' + q.toString()).val(intMore + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString());

                                if (globalSelectedValue.split('#')[1] == intMore + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString()) {
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).removeClass('btn');
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).addClass('btn btn-info');
                                }
                                else {
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).removeClass('btn btn-info');
                                    $('#btn' + q.toString() + 'td2tr' + q.toString()).addClass('btn');
                                }

                            }

                            var q = 1;
                            for (q = 1; q < 9; q++) {
                                var intMore = 0;
                                intMore = parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[0]) + 16;
                                if (intMore >= parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]))
                                    return;
                                $('#btn' + q.toString() + 'td3tr' + q.toString()).val(intMore + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString());

                                if (globalSelectedValue.split('#')[1] == intMore + '/' + parseInt($('#btn' + q.toString() + 'td3tr' + q.toString()).val().split('/')[1]).toString()) {
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).removeClass('btn');
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).addClass('btn btn-info');
                                }
                                else {
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).removeClass('btn btn-info');
                                    $('#btn' + q.toString() + 'td3tr' + q.toString()).addClass('btn');
                                }
                            }

                        });

                    }

                }
                else {

                    if (j == 0) {
                        if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces) {
                            $('#td4tr' + j.toString()).html(".001 Digit").attr("class", "priceWidgetHeader");
                            continue;
                        }
                        else {
                            $('#td4tr' + j.toString()).html("Sub Fraction").attr("class", "priceWidgetHeader");
                            continue;
                        }
                    }

                    var varbuttonTemplate = price_widget_Template_button();
                    varbuttonTemplate = ReplaceAll(varbuttonTemplate, '[BUTTON]', j.toString() + "td4tr" + j.toString());
                    $('#td4tr' + j.toString()).html(varbuttonTemplate);

                    $('#btn' + j.toString() + 'td4tr' + j.toString()).val(td4btnCount.toString());

                    $('#btn' + j.toString() + 'td4tr' + j.toString()).click(function () {
                        // alert($(this).val());

                        IsChange = 'TRUE';

                        var iArray = globalSelectedValue.split("#").length;

                        if ($('#FormatSelection').val() == NumberFormat.eBond || $('#FormatSelection').val() == NumberFormat.e2DecimalPlaces)
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + globalSelectedValue.split("#")[1] + "#" + $(this).val();
                        else if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces || $('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs || $('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters)
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + globalSelectedValue.split("#")[1] + "#" + globalSelectedValue.split("#")[2] + "#" + $(this).val();
                        else
                            globalSelectedValue1Temp = globalSelectedValue.split("#")[0] + "#" + $(this).val();

                        globalSelectedValue = globalSelectedValue1Temp;
                        var newValue = DecimalPrice(globalSelectedValue, $('#FormatSelection').val());
                        $('#txtValue2').val(newValue.split('^')[1]);

                        if (globaltxtValue2.indexOf("-")) {
                        }
                        else {
                            if ($('#txtValue2').val() != "0" && $('#txtValue2').val() != "0.00" && $('#txtValue2').val() != "0.000" && $('#txtValue2').val() != "0.0")
                                $('#txtValue2').val("-" + $('#txtValue2').val());
                            else
                                $('#txtValue2').val($('#txtValue2').val());
                        }

                        Bind_Price_Widget();
                        ShowFullValue_PriceWidget();

                    });

                    if ($('#FormatSelection').val() == NumberFormat.e3DecimalPlaces) {
                        if (parseInt(td4btnCount) == parseInt(9))
                            return;
                    }
                    else if ($('#FormatSelection').val() == NumberFormat.eBond) {
                        //                                if (parseInt(td4btnCount) == parseInt(7))
                        //                                    return;
                    }
                    else if ($('#FormatSelection').val() == NumberFormat.eFurtureRateHalfs) {
                        if (parseInt(td4btnCount) == parseInt(1))
                            return;
                    }
                    else if ($('#FormatSelection').val() == NumberFormat.eFurtureRateQuarters) {
                        if (parseInt(td4btnCount) == parseInt(3))
                            return;
                    }

                }
                td4btnCount++;
            }
        }
        catch (err) {
            var strerrordesc = "Function:ForthTD_PriceWidget(); Error is : " + err.description + "; Error number is " + err.number + "; Message :" + err.message;
            onJavascriptLog("priceWidgetScript.js", strerrordesc);
        }
    }
    //END ******************************************************** Forth TD ********************************************************************
}

