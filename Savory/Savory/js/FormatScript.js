var FormatCode = {


    "DefaultFormat": 0,             /// integer formats


    ///N_FIRST
    "0DecimalDigits": 1000,       ///N_P0       ///< 1
    "1DecimalDigit": 1001,        ///N_P1       ///< 1.2
    "2DecimalDigits": 1002,       ///N_P2       ///< 1.23
    "3DecimalDigits": 1003,       ///N_P3       ///< 1.235
    "4DecimalDigits": 1004,       ///N_P4       ///< 1 2346
    "5DecimalDigits": 1005,       ///N_P5       ///< 1.23457


    "2SignificantDigits": 1141,       ///N_G2       ///< Show 2 significant digit in N_Pxx format
    "3SignificantDigits": 1142,       ///N_G3       ///< Show 3 significant digit in N_Pxx format
    "4SignificantDigits": 1143,       ///N_G4	    ///< Show 4 significant digit in N_Pxx format
    "5SignificantDigits": 1144,       ///N_G5		///< Show 5 significant digit in N_Pxx format
    "6SignificantDigits": 1145,       ///N_G6		///< Show 6 significant digit in N_Pxx format


    ///(price widget)   --------------------------------------------Start

    "0DecDig": 1200,                ///N_P0P	 ///< Same as P0 except number edited with price widget
    "1DecDig": 1201,                ///N_P1P	 ///< Same as P1 except number edited with price widget    
    "2DecDig": 1202,                ///N_P2P	 ///< Same as P2 except number edited with price widget
    "3DecDig": 1203,                ///N_P3P     ///< Same as P3 except number edited with price widget


    "Fractions(1/2)": 1300,         ///N_D2	    ///< 1
    "Fractions(1/4)": 1301,         ///N_D4		///< 1-1/4
    "Fractions(1/8)": 1302,         ///N_D8	    ///< 1'2
    "Fractions(1/16)": 1303,        ///N_D16	///< 1"4         
    "Fractions(1/32)": 1304,        ///N_D32	///< 1-08
    "Fractions(1/64)": 1305,        ///N_D64	///<  1-15/64
    "Fractions(1/128)": 1306,        ///N_D128	///< 1-30/128
    "Fractions(1/256)": 1307,        ///N_D256	///< 1-60/256


    "ReducedFractions(1/4)": 1401,      ///N_D4R	///< 1-1/4 
    "ReducedFractions(1/8)": 1402,      ///N_D8R	///< 1-1/4
    "ReducedFractions(1/16)": 1403,     ///N_D16R	///< 1-1/4
    "ReducedFractions(1/32)": 1404,     ///N_D32R	///< 1-1/4
    "ReducedFractions(1/64)": 1405,        ///N_D64R	///<  1-15/64
    "ReducedFractions(1/128)": 1406,        ///N_D128R	///< 1-30/128
    "ReducedFractions(1/256)": 1407,        ///N_D256R	///< 1-60/256


    "BondPrice": 1504,                  ///N_D32P	///< 1-07+                                  ///1/32+ 

    "FuturesPrice(1/2)": 1220,          ///N_P2PD2	///< 1.05+                                  ///0.00+(1/2)
    "FuturesPrice(1/4)": 1221,          ///N_P2PD4	///< 1.05+                                  ///0.00+(1/4)

    ///(price widget)  --------------------------------------------End

    /// Date	Formats	-------------------------------------------------------------------------------
    "Date": 2000,                       ///D_MDYY			///< 4/7/95
    "IMMDate": 2230,                     ///D_MMMYY			///< Apr 95
    "TimeStamp": 22403000,              ///(2240)///D_WWWMDYY		///< Fri 4/7/95     (3000)///T_HMMSS			///< 9:07:03


    /// Basis Points Number Formats (same as Number Formats, but the value is multiplied by 100 ------


    ///BP_FIRST
    "(BP)0DecimalDigits": 5000,             ///BP_P0		///< 1
    "(BP)1DecimalDigit": 5001,              ///BP_P1		///< 1.2
    "(BP)2DecimalDigits": 5002,             ///BP_P2    	///< 1.23
    "(BP)3DecimalDigits": 5003,             ///BP_P3		///< 1.235
    "(BP)4DecimalDigits": 5004,             ///BP_P4		///< 1 2346
    "(BP)5DecimalDigits": 5005,             ///BP_P5		///< 1.23457



    "(BP)2SignificantDigits": 5141,         ///BP_G2		///< Show 2 significant digit in BP_Pxx format
    "(BP)3SignificantDigits": 5142,         ///BP_G3		///< Show 3 significant digit in BP_Pxx format
    "(BP)4SignificantDigits": 5143,         ///BP_G4		///< Show 4 significant digit in BP_Pxx format
    "(BP)5SignificantDigits": 5144,         ///BP_G5		///< Show 5 significant digit in BP_Pxx format
    "(BP)6SignificantDigits": 5145,         ///BP_G6		///< Show 6 significant digit in BP_Pxx format


    ///Note: need to * 100 do on Value and after that pass value to price widget
    ///(price widget)   --------------------------------------------Start //////////////(Value * 100)

    "(BP)0DecDig": 5200,                    ///BP_P0P		///< Same as P0 except number edited with price widget     /// 1
    "(BP)1DecDig": 5201,                    ///BP_P1P		///< Same as P1 except number edited with price widget     /// 0.1
    "(BP)2DecDig": 5202,                    ///BP_P2P		///< Same as P2 except number edited with price widget     /// 0.01
    "(BP)3DecDig": 5203,                    ///BP_P3P	    ///< Same as P3 except number edited with price widget     /// 0.001



    "(BP)Fractions(1/2)": 5300,             ///BP_D2		///< 1
    "(BP)Fractions(1/4)": 5301,             ///BP_D4		///< 1-1/4
    "(BP)Fractions(1/8)": 5302,             ///BP_D8		///< 1'2
    "(BP)Fractions(1/16)": 5303,            ///BP_D16		///< 1"4
    "(BP)Fractions(1/32)": 5304,            ///BP_D32		///< 1-08



    "(BP)ReducedFractions(1/4)": 5401,      ///BP_D4R		///< 1-1/4
    "(BP)ReducedFractions(1/8)": 5402,      ///BP_D8R		///< 1-1/4
    "(BP)ReducedFractions(1/16)": 5403,     ///BP_D16R		///< 1-1/4
    "(BP)ReducedFractions(1/32)": 5404,     ///BP_D32R		///< 1-1/4



    "(BP)BondPrice": 5504,                  ///BP_D32P		///< 1-07+


    "(BP)FuturesPrice(1/2)": 5220,          ///BP_P2PD2			///< 1.05+
    "(BP)FuturesPrice(1/4)": 5221,           ///BP_P2PD4			///< 1.05+

    ///(price widget)  --------------------------------------------End



    "N_P6": 1006,                   ///< 1.234568
    "N_P7": 1007,                   ///< 1.2345679
    "N_P8": 1008,                   ///< 1.23456789
    "N_P9": 1009,                   ///< 1.234567890
    "N_P10": 1010,                  ///< 1.2345678901
    "N_P11": 1011,                  ///< 1.23456789012
    "N_P12": 1012,                  ///< 1.234567890123
    "N_P13": 1013,                  ///< 1.2345678901235
    "N_P14": 1014,                  ///< 1.23456789012346
    "N_P15": 1015,                  ///< 1.234567890123457
    "N_P16": 1016,                  ///< 1.2345678901234568



    "N_S0": 1020,               ///< +1
    "N_S1": 1021,               ///< +1.2
    "N_S2": 1022,               ///< +1.23
    "N_S3": 1023,               ///< +1.235
    "N_S4": 1024,               ///< +1 2346
    "N_S5": 1025,               ///< +1.23457
    "N_S6": 1026,               ///< +1.234568
    "N_S7": 1027,               ///< +1.2345679
    "N_S8": 1028,               ///< +1.23456789
    "N_S9": 1029,               ///< +1.234567890
    "N_S10": 1030,              ///< +1.2345678901
    "N_S11": 1031,              ///< +1.23456789012
    "N_S12": 1032,              ///< +1.234567890123
    "N_S13": 1033,              ///< +1.2345678901235
    "N_S14": 1034,              ///< +1.23456789012346
    "N_S15": 1035,              ///< +1.234567890123457
    "N_S16": 1036,               ///< +1.2345678901234568


    "N_C0": 1040,       ///< 1
    "N_C1": 1041,       ///< 1.2
    "N_C2": 1042,       ///< 1.23
    "N_C3": 1043,       ///< 1.235
    "N_C4": 1044,       ///< 1 2346
    "N_C5": 1045,       ///< 1.23457
    "N_C6": 1046,       ///< 1.234568
    "N_C7": 1047,       ///< 1.2345679
    "N_C8": 1048,       ///< 1.23456789
    "N_C9": 1049,       ///< 1.234567890
    "N_C10": 1050,       ///< 1.2345678901
    "N_C11": 1051,       ///< 1.23456789012
    "N_C12": 1052,       ///< 1.234567890123
    "N_C13": 1053,       ///< 1.2345678901235
    "N_C14": 1054,       ///< 1.23456789012346
    "N_C15": 1055,       ///< 1.234567890123457
    "N_C16": 1056,       ///< 1.2345678901234568


    "N_SC0": 1060, ///< +1
    "N_SC1": 1061, ///< +1.2
    "N_SC2": 1062, ///< +1.23
    "N_SC3": 1063, ///< +1.235
    "N_SC4": 1064, ///< +1 2346
    "N_SC5": 1065, ///< +1.23457
    "N_SC6": 1066, ///< +1.234568
    "N_SC7": 1067, ///< +1.2345679
    "N_SC8": 1068, ///< +1.23456789
    "N_SC9": 1069, ///< +1.234567890
    "N_SC10": 1070, ///< +1.2345678901
    "N_SC11": 1071, ///< +1.23456789012
    "N_SC12": 1072, ///< +1.234567890123
    "N_SC13": 1073, ///< +1.2345678901235
    "N_SC14": 1074, ///< +1.23456789012346
    "N_SC15": 1075, ///< +1.234567890123457
    "N_SC16": 1076, ///< +1.2345678901234568


    "N_S0KMB": 1120, ///< 1 [kmb]
    "N_S1KMB": 1121, ///< 1.2    [kmb]
    "N_S2KMB": 1122, ///< 1.23 [kmb]
    "N_S3KMB": 1123, ///< 1.235 [kmb]


    "N_P0KMB": 1100, ///< +1 [kmb]
    "N_P1KMB": 1101, ///< +1.2 [kmb]
    "N_P2KMB": 1102, ///< +1.23    [kmb]
    "N_P3KMB": 1103, ///< +1.235 [kmb]


    "N_G1": 1140,      ///< Show 1 significant digit in N_Pxx format
    "N_G7": 1146,      ///< Show 7 significant digit in N_Pxx format
    "N_G8": 1147,       ///< Show 8 significant digit in N_Pxx format

    "N_SG1": 1160, ///< Show 1 significant digit in N_Sxx format
    "N_SG2": 1161, ///< Show 2 significant digit in N_Sxx format
    "N_SG3": 1162, ///< Show 3 significant digit in N_Sxx format
    "N_SG4": 1163, ///< Show 4 significant digit in N_Sxx format
    "N_SG5": 1164, ///< Show 5 significant digit in N_Sxx format
    "N_SG6": 1165, ///< Show 6 significant digit in N_Sxx format
    "N_SG7": 1166, ///< Show 7 significant digit in N_Sxx format
    "N_SG8": 1167, ///< Show 8 significant digit in N_Sxx format



    "N_D64": 1305, ///< 1-15/64				
    "N_D128": 1306, ///< 1-30/128				
    "N_D256": 1307, ///< 1-60/256	


    "N_D64R": 1405, ///< 1-15/64
    "N_D128R": 1406, ///< 1-15/64
    "N_D256R": 1407, ///< 1-15/64


    "BP_D64": 5305, ///< 1-15/64
    "BP_D128": 5306, ///< 1-30/128
    "BP_D256": 5307, ///< 1-60/256


    "BP_D64R": 5405, ///< 1-15/64
    "BP_D128R": 5406, ///< 1-15/64
    "BP_D256R": 5407, ///< 1-15/64



    "N_SD2": 1320, ///< +1
    "N_SD4": 1321, ///< +1-1/4
    "N_SD8": 1322, ///< +1'2
    "N_SD16": 1323, ///< +1"4
    "N_SD32": 1324, ///< +1-08
    "N_SD64": 1325, ///< +1-15/64
    "N_SD128": 1326, ///< +1-30/128
    "N_SD256": 1327, ///< +1-60/256



    "N_SD4R": 1421, ///< +1-1/4
    "N_SD8R": 1422, ///< +1-1/4
    "N_SD16R": 1423, ///< +1-1/4
    "N_SD32R": 1424, ///< +1-1/4
    "N_SD64R": 1425, ///< +1-15/64
    "N_SD128R": 1426, ///< +1-15/64
    "N_SD256R": 1427, ///< +1-15/64



    "N_D2F": 1600, ///< Same    as D2 except use built in char in font for 1/2
    "N_D4F": 1601, ///< Same    as D4 except use built in char in font for 1/4,  1/2, and 3/4
    "N_SD2F": 1620, ///< Same    as SD2 except use built    in char  in font  for 1/2
    "N_SD4F": 1621, ///< Same    as SD4 except use built    in char  in font  for 1/4, 1/2, and 3/4


    "N_LAST": 1999,


    "BP_P6": 5006, ///< 1.234568
    "BP_P7": 5007, ///< 1.2345679
    "BP_P8": 5008, ///< 1.23456789
    "BP_P9": 5009, ///< 1.234567890
    "BP_P10": 5010, ///< 1.2345678901
    "BP_P11": 5011, ///< 1.23456789012
    "BP_P12": 5012, ///< 1.234567890123
    "BP_P13": 5013, ///< 1.2345678901235
    "BP_P14": 5014, ///< 1.23456789012346
    "BP_P15": 5015, ///< 1.234567890123457
    "BP_P16": 5016, ///< 1.2345678901234568


    "BP_S0": 5020, ///< +1
    "BP_S1": 5021, ///< +1.2
    "BP_S2": 5022, ///< +1.23
    "BP_S3": 5023, ///< +1.235
    "BP_S4": 5024, ///< +1 2346
    "BP_S5": 5025, ///< +1.23457
    "BP_S6": 5026, ///< +1.234568
    "BP_S7": 5027, ///< +1.2345679
    "BP_S8": 5028, ///< +1.23456789
    "BP_S9": 5029, ///< +1.234567890
    "BP_S10": 5030, ///< +1.2345678901
    "BP_S11": 5031, ///< +1.23456789012
    "BP_S12": 5032, ///< +1.234567890123
    "BP_S13": 5033, ///< +1.2345678901235
    "BP_S14": 5034, ///< +1.23456789012346
    "BP_S15": 5035, ///< +1.234567890123457
    "BP_S16": 5036, ///< +1.2345678901234568

    "BP_SC0": 5060, ///< +1
    "BP_SC1": 5061, ///< +1.2
    "BP_SC2": 5062, ///< +1.23
    "BP_SC3": 5063, ///< +1.235
    "BP_SC4": 5064, ///< +1 2346
    "BP_SC5": 5065, ///< +1.23457
    "BP_SC6": 5066, ///< +1.234568
    "BP_SC7": 5067, ///< +1.2345679
    "BP_SC8": 5068, ///< +1.23456789
    "BP_SC9": 5069, ///< +1.234567890
    "BP_SC10": 5070, ///< +1.2345678901
    "BP_SC11": 5071, ///< +1.23456789012
    "BP_SC12": 5072, ///< +1.234567890123
    "BP_SC13": 5073, ///< +1.2345678901235
    "BP_SC14": 5074, ///< +1.23456789012346
    "BP_SC15": 5075, ///< +1.234567890123457
    "BP_SC16": 5076, ///< +1.2345678901234568


    "BP_S0KMB": 5120, ///< 1 [kmb]
    "BP_S1KMB": 5121, ///< 1.2    [kmb]
    "BP_S2KMB": 5122, ///< 1.23 [kmb]
    "BP_S3KMB": 5123, ///< 1.235 [kmb]


    "BP_P0KMB": 5100, ///< +1 [kmb]
    "BP_P1KMB": 5101, ///< +1.2 [kmb]
    "BP_P2KMB": 5102, ///< +1.23    [kmb]
    "BP_P3KMB": 5103, ///< +1.235 [kmb]


    "BP_G1": 5140, ///< Show 1 significant digit in BP_Pxx format
    "BP_G2": 5141, ///< Show 2 significant digit in BP_Pxx format
    "BP_G3": 5142, ///< Show 3 significant digit in BP_Pxx format
    "BP_G4": 5143, ///< Show 4 significant digit in BP_Pxx format
    "BP_G5": 5144, ///< Show 5 significant digit in BP_Pxx format
    "BP_G6": 5145, ///< Show 6 significant digit in BP_Pxx format
    "BP_G7": 5146, ///< Show 7 significant digit in BP_Pxx format
    "BP_G8": 5147, ///< Show 8 significant digit in BP_Pxx format



    "BP_SG1": 5160, ///< Show 1 significant digit in BP_Sxx format
    "BP_SG2": 5161, ///< Show 2 significant digit in BP_Sxx format
    "BP_SG3": 5162, ///< Show 3 significant digit in BP_Sxx format
    "BP_SG4": 5163, ///< Show 4 significant digit in BP_Sxx format
    "BP_SG5": 5164, ///< Show 5 significant digit in BP_Sxx format
    "BP_SG6": 5165, ///< Show 6 significant digit in BP_Sxx format
    "BP_SG7": 5166, ///< Show 7 significant digit in BP_Sxx format
    "BP_SG8": 5167, ///< Show 8 significant digit in BP_Sxx format


    "BP_SD2": 5320, ///< +1
    "BP_SD4": 5321, ///< +1-1/4
    "BP_SD8": 5322, ///< +1'2
    "BP_SD16": 5323, ///< +1"4
    "BP_SD32": 5324, ///< +1-08
    "BP_SD64": 5325, ///< +1-15/64
    "BP_SD128": 5326, ///< +1-30/128
    "BP_SD256": 5327, ///< +1-60/256


    "BP_SD4R": 5421, ///< +1-1/4
    "BP_SD8R": 5422, ///< +1-1/4
    "BP_SD16R": 5423, ///< +1-1/4
    "BP_SD32R": 5424, ///< +1-1/4
    "BP_SD64R": 5425, ///< +1-15/64
    "BP_SD128R": 5426, ///< +1-15/64
    "BP_SD256R": 5427, ///< +1-15/64


    "BP_D2F": 5600, ///< Same    as D2 except use built in char in font for 1/2
    "BP_D4F": 5601, ///< Same    as D4 except use built in char in font for 1/4, 1/2, and 3/4
    "BP_SD2F": 5620, ///< Same    as SD2 except use built    in char  in font  for 1/2
    "BP_SD4F": 5621, ///< Same    as SD4 except use built    in char  in font  for 1/4, 1/2, and 3/4
    "BP_LAST": 5999,
    "BP_OFFSET": 4000



};


function ConvertFormatTypeIndex(FormatIndex) {

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
function GetReducedFraction_Format(Numerator, Denominator) {
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
        alert('GetReducedFraction');
    }
}


/// <summary>
/// Code for converting decimal price to given price format. Pass Number format as per above enum (0 t0 21)
/// Usage - ParsePrice(99.9645,8) ;
/// </summary>
///param1 double, param2 int, param3 int       
function ParsePrice(DecPrice, Format) {
    try {

        //int
        var nPrice, nFrac1, nFrac2, nFrac3, nBaseUnit, min = 1;
        nPrice = parseInt((Math.floor(Math.abs(DecPrice))));
        nFrac1 = nFrac2 = nFrac3 = 0;
        nBaseUnit = Format;
        if (nBaseUnit == 5504 || nBaseUnit == 1504) // related to 1/32+ format=eBond
        {
            nFrac1 = parseInt(((Math.abs(DecPrice) - nPrice) * 256 + 0.5));
            if (nFrac1 == 256) {
                nFrac1 = 0;
                nPrice++;
            }
            nFrac2 = nFrac1 % 8;
            nFrac1 /= 8;

            //Show Output as  per selected Format
            return ShowFullOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));
        }
        else {
            //Set Min as per Number Format
            if (nBaseUnit == 5504 || nBaseUnit == 1504)
                min = 32;
            else if (nBaseUnit >= 5300 && nBaseUnit <= 5307 || nBaseUnit >= 1300 && nBaseUnit <= 1307 || nBaseUnit >= 1320 && nBaseUnit <= 1327 || nBaseUnit >= 1421 && nBaseUnit <= 1427 || nBaseUnit >= 5320 && nBaseUnit <= 5327 || nBaseUnit >= 5401 && nBaseUnit <= 5407 || nBaseUnit >= 5421 && nBaseUnit <= 5427 || nBaseUnit >= 1401 && nBaseUnit <= 1407 || nBaseUnit == 1504 || nBaseUnit == 5504 || nBaseUnit == 1600 || nBaseUnit == 1601 || nBaseUnit == 1620 || nBaseUnit == 1621 || nBaseUnit == 5600 || nBaseUnit == 5601 || nBaseUnit == 5620 || nBaseUnit == 5621) {
                min = (1 << ((ConvertFormatTypeIndex(nBaseUnit) % 8) + 1));
            }
            else if (nBaseUnit == 1201 || nBaseUnit == 5201)
                min = 10;
            else if (nBaseUnit == 1202 || nBaseUnit == 5202)
                min = 100;
            else if (nBaseUnit == 1203 || nBaseUnit == 5203)
                min = 1000;
            else if (nBaseUnit == 1220 || nBaseUnit == 5220) {
                min = 1000;
            }
            else if (nBaseUnit == 1221 || nBaseUnit == 5221) {
                min = 10000;
            }

            nFrac1 = parseInt(((Math.abs(DecPrice) - nPrice) * min + 0.5));
            if (nFrac1 == min) {
                nFrac1 = 0;
                nPrice++;
            }
            if (nBaseUnit == 1202 || nBaseUnit == 5202) {
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 1203 || nBaseUnit == 5203) {
                nFrac3 = nFrac1 % 10;
                nFrac1 /= 10;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 1220 || nBaseUnit == 5220) {
                nFrac3 = (nFrac1 % 10) / 5;
                nFrac1 /= 10;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }
            else if (nBaseUnit == 1221 || nBaseUnit == 5221) {
                nFrac3 = (nFrac1 % 100) / 25;
                nFrac1 /= 100;
                nFrac2 = nFrac1 % 10;
                nFrac1 /= 10;
            }

            //Show Output as  per selected Format

            return ShowFullOutput(parseFloat(DecPrice), parseInt(Format), parseInt(nPrice), parseInt(nFrac1), parseInt(nFrac2), parseInt(nFrac3));


        }
    }
    catch (err) {
        alert('ParsePrice');
    }
}




///param1 double, param2 int, param3 int, param4 int, param5 int, param6 int
function ShowFullOutput(DecPrice, Format, nPrice, nFrac1, nFrac2, nFrac3) {
    try {
        //Show Output as  per selected Format
        if (Format == 1300 || Format == 5300 || Format == 1320 || Format == 5320 || Format == 1600 || Format == 1620 || Format == 5600 || Format == 5620) //1/2=eHalfs
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/2");
        }
        else if (Format == 1301 || Format == 5301 || Format == 1321 || Format == 5321 || Format == 1601 || Format == 1621 || Format == 5601 || Format == 5621) //1/4=eQuarters
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 1302 || Format == 5302 || Format == 1322 || Format == 5322) //1/8=eEighths
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "'" + nFrac1);
        }
        else if (Format == 1303 || Format == 5303 || Format == 1323 || Format == 5323) //1/16=eSixteenths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''00");
            else if (nFrac1 > 0 && nFrac1 < 10)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''0" + nFrac1);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "''" + nFrac1);
        }
        else if (Format == 1304 || Format == 5304 || Format == 1324 || Format == 5324) //1/32=eThirtySeconds
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-00");
            else if (nFrac1 > 0 && nFrac1 < 10)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-0" + nFrac1);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1);
        }
        else if (Format == 1305 || Format == 5305 || Format == 1325 || Format == 5325) //1/64=eSixtyFourths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 1306 || Format == 5306 || Format == 1326 || Format == 5326) //1/128=eOneTwentyEights
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 1307 || Format == 5307 || Format == 1327 || Format == 5327) //1/256=eTwoFiftySixths
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/256");
        }
        else if (Format == 1504 || Format == 5504) {
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
        else if (Format == 1401 || Format == 5401 || Format == 1421 || Format == 5421) //1/4R=eQuartersReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 == 2)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 4));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/4");
        }
        else if (Format == 1402 || Format == 5402 || Format == 1422 || Format == 5422) //1/8R=eEighthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 8));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/8");
        }
        else if (Format == 1403 || Format == 5403 || Format == 1423 || Format == 5423) //1/16R=eSixteenthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 16));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/16");
        }
        else if (Format == 1404 || Format == 5404 || Format == 1424 || Format == 5424) //1/32R=eThirtySecondsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 32));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/32");
        }
        else if (Format == 1405 || Format == 5405 || Format == 1425 || Format == 5425)  //1/64R=eSixtyFourthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 64));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/64");
        }
        else if (Format == 1406 || Format == 5406 || Format == 1426 || Format == 5426)  //1/128R=eOneTwentyEightsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 128));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/128");
        }
        else if (Format == 1407 || Format == 5407 || Format == 1427 || Format == 5427)  //1/256R=eTwoFiftySixthsReduced
        {
            if (nFrac1 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
            else if (nFrac1 % 2 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + GetReducedFraction_Format(nFrac1, 256));
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "-" + nFrac1 + "/256");
        }
        else if (Format == 1200 || Format == 5200) //1=e0DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice));
        }
        else if (Format == 1201 || Format == 5201) //0.1=e1DecimalPlace
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1);
        }
        else if (Format == 1202 || Format == 5202) //0.01=e2DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
        }
        else if (Format == 1203 || Format == 5203) //0.001=e3DecimalPlaces
        {
            return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + nFrac3);
        }
        else if (Format == 1220 || Format == 5220) //0.001=eFurtureRateHalfs
        {
            if (nFrac3 == 0)
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2);
            else
                return (((DecPrice < 0.0) ? nPrice * -1 : nPrice) + "." + nFrac1 + nFrac2 + "+");
        }
        else if (Format == 1221 || Format == 5221) //0.001=eFurtureRateQuarters
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
        alert('ShowFullOutput');
    }
}




function ConvertToFormat(Format, paramValue) {


    var Value = 0;
    var IsNeg = '';

    switch (Format) {
        case 0:                             ///Default Format
            Value = paramValue.toFixed(3);
            break;
        case 1000:                          ///0 Decimal Digits
            Value = paramValue.toFixed(0);
            break;
        case 1001:                          ///1 Decimal Digit
            Value = paramValue.toFixed(1);
            break;
        case 1002:                          ///2 Decimal Digits
            Value = paramValue.toFixed(2);
            break;
        case 1003:                          ///3 Decimal Digits
            Value = paramValue.toFixed(3);
            break;
        case 1004:                          ///4 Decimal Digits
            Value = paramValue.toFixed(4);
            break;
        case 1005:                          ///5 Decimal Digits
            Value = paramValue.toFixed(5);
            break;
        case 1141:                          ///2 Significant Digits
            Value = Math.sig(paramValue, 2);
            break;
        case 1142:                          ///3 Significant Digits
            Value = Math.sig(paramValue, 3);
            break;
        case 1143:                          ///4 Significant Digits
            Value = Math.sig(paramValue, 4);
            break;
        case 1144:                          ///5 Significant Digits
            Value = Math.sig(paramValue, 5);
            break;
        case 1145:                          ///6 Significant Digits
            Value = Math.sig(paramValue, 6);
            break;
        case 5000:                          ///(BP)0 Decimal Digits
            Value = (paramValue * 100).toFixed(0);
            break;
        case 5001:                          ///(BP)1 Decimal Digit
            Value = (paramValue * 100).toFixed(1);
            break;
        case 5002:                          ///(BP)2 Decimal Digits
            Value = (paramValue * 100).toFixed(2);
            break;
        case 5003:                          ///(BP)3 Decimal Digits
            Value = (paramValue * 100).toFixed(3);
            break;
        case 5004:                          ///(BP)4 Decimal Digits
            Value = (paramValue * 100).toFixed(4);
            break;
        case 5005:                          ///(BP)5 Decimal Digits
            Value = (paramValue * 100).toFixed(5);
            break;
        case 5141:                          ///(BP)2 Significant Digits
            Value = Math.sig((paramValue * 100), 2);
            break;
        case 5142:                          ///(BP)3 Significant Digits
            Value = Math.sig((paramValue * 100), 3);
            break;
        case 5143:                          ///(BP)4 Significant Digits
            Value = Math.sig((paramValue * 100), 4);
            break;
        case 5144:                          ///(BP)5 Significant Digits
            Value = Math.sig((paramValue * 100), 5);
            break;
        case 5145:                          ///(BP)6 Significant Digits
            Value = Math.sig((paramValue * 100), 6);
            break;
        case 1006:                          ///6 Decimal Digits
            Value = paramValue.toFixed(6);
            break;
        case 1007:                          ///7 Decimal Digits
            Value = paramValue.toFixed(7);
            break;
        case 1008:                          ///8 Decimal Digits
            Value = paramValue.toFixed(8);
            break;
        case 1009:                          ///9 Decimal Digits
            Value = paramValue.toFixed(9);
            break;
        case 1010:                          ///10 Decimal Digits
            Value = paramValue.toFixed(10);
            break;
        case 1011:                          ///11 Decimal Digits
            Value = paramValue.toFixed(11);
            break;
        case 1012:                          ///12 Decimal Digits
            Value = paramValue.toFixed(12);
            break;
        case 1013:                          ///13 Decimal Digits
            Value = paramValue.toFixed(13);
            break;
        case 1014:                          ///14 Decimal Digits
            Value = paramValue.toFixed(14);
            break;
        case 1015:                          ///15 Decimal Digits
            Value = paramValue.toFixed(15);
            break;
        case 1016:                          ///16 Decimal Digits
            Value = paramValue.toFixed(16);
            break;
        case 1020:                          ///< +1
            Value = paramValue.toFixed(0);
            break;
        case 1021:                          ///< +1.2
            Value = paramValue.toFixed(1);
            break;
        case 1022:                          ///< +1.23
            Value = paramValue.toFixed(2);
            break;
        case 1023:                          ///< +1.235
            Value = paramValue.toFixed(3);
            break;
        case 1024:                          ///< +1 2346
            Value = paramValue.toFixed(4);
            break;
        case 1025:                          ///< +1.23457
            Value = paramValue.toFixed(5);
            break;
        case 1026:                         ///< +1.234568
            Value = paramValue.toFixed(6);
            break;
        case 1027:                         ///< +1.2345679
            Value = paramValue.toFixed(7);
            break;
        case 1028:                         ///< +1.23456790
            Value = paramValue.toFixed(8);
            break;
        case 1029:                         ///< +1.23456789
            Value = paramValue.toFixed(9);
            break;
        case 1030:                         ///< +1.2345678901
            Value = paramValue.toFixed(10);
            break;
        case 1031:                         ///< +1.23456789012
            Value = paramValue.toFixed(11);
            break;
        case 1032:                         ///< +1.234567890123 
            Value = paramValue.toFixed(12);
            break;
        case 1033:                         ///< +1.2345678901235
            Value = paramValue.toFixed(13);
            break;
        case 1034:                         ///< +1.23456789012346
            Value = paramValue.toFixed(14);
            break;
        case 1035:                         ///< +1.234567890123457
            Value = paramValue.toFixed(15);
            break;
        case 1036:                         ///< +1.2345678901234568
            Value = paramValue.toFixed(16);
            break;
        case 1040:                          /// 0 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(0));
            break;
        case 1041:                          /// 1 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(1));
            break;
        case 1042:                          /// 2 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(2));
            break;
        case 1043:                          /// 3 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(3));
            break;
        case 1044:                          /// 4 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(4));
            break;
        case 1045:                          /// 5 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(5));
            break;
        case 1046:                          /// 6 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(6));
            break;
        case 1047:                          /// 7 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(7));
            break;
        case 1048:                          /// 8 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(8));
            break;
        case 1049:                          /// 9 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(9));
            break;
        case 1050:                          /// 10 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(10));
            break;
        case 1051:                          /// 11 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(11));
            break;
        case 1052:                          /// 12 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(12));
            break;
        case 1053:                          /// 13 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(13));
            break;
        case 1054:                          /// 14 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(14));
            break;
        case 1055:                          /// 15 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(15));
            break;
        case 1056:                          /// 16 Separators for Thousands
            Value = ThousandSeparator(0, paramValue.toFixed(16));
            break;
        case 1060:                          ///< +1
            Value = paramValue.toFixed(0);
            break;
        case 1061:                          ///< +1.2
            Value = paramValue.toFixed(1);
            break;
        case 1062:                          ///< +1.23
            Value = paramValue.toFixed(2);
            break;
        case 1063:                          ///< +1.235
            Value = paramValue.toFixed(3);
            break;
        case 1064:                          ///< +1 2346
            Value = paramValue.toFixed(4);
            break;
        case 1065:                          ///< +1.23457
            Value = paramValue.toFixed(5);
            break;
        case 1066:                         ///< +1.234568
            Value = paramValue.toFixed(6);
            break;
        case 1067:                         ///< +1.2345679
            Value = paramValue.toFixed(7);
            break;
        case 1068:                         ///< +1.2345679
            Value = paramValue.toFixed(8);
            break;
        case 1069:                         ///< +1.23456789
            Value = paramValue.toFixed(9);
            break;
        case 1070:                         ///< +1.2345678901
            Value = paramValue.toFixed(10);
            break;
        case 1071:                         ///< +1.23456789012
            Value = paramValue.toFixed(11);
            break;
        case 1072:                         ///< +1.234567890123 
            Value = paramValue.toFixed(12);
            break;
        case 1073:                         ///< +1.2345678901235
            Value = paramValue.toFixed(13);
            break;
        case 1074:                         ///< +1.23456789012346
            Value = paramValue.toFixed(14);
            break;
        case 1075:                         ///< +1.234567890123457
            Value = paramValue.toFixed(15);
            break;
        case 1076:                         ///< +1.2345678901234568
            Value = paramValue.toFixed(16);
            break;
        case 1100:                         ///< +1 [kmb]
            Value = ConvertToKMB(paramValue, 0);
            break;
        case 1101:                          ///< +1.2    [kmb]
            Value = ConvertToKMB(paramValue, 1);
            break;
        case 1102:                          ///< +1.23 [kmb]
            Value = ConvertToKMB(paramValue, 2);
            break;
        case 1103:                         ///< +1.235 [kmb]
            Value = ConvertToKMB(paramValue, 3);
            break;
        case 1120:                         ///< 1 [kmb]
            Value = ConvertToKMB(paramValue, 0);
            break;
        case 1121:                          ///< 1.2    [kmb]
            Value = ConvertToKMB(paramValue, 1);
            break;
        case 1122:                          ///< 1.23 [kmb]
            Value = ConvertToKMB(paramValue, 2);
            break;
        case 1123:                         ///< 1.235 [kmb]
            Value = ConvertToKMB(paramValue, 3);
            break;
        case 1140:                          ///1 Significant Digits
            Value = Math.sig(paramValue, 1);
            break;
        case 1146:                          ///7 Significant Digits
            Value = Math.sig(paramValue, 7);
            break;
        case 1147:                          ///8 Significant Digits
            Value = Math.sig(paramValue, 8);
            break;
        case 1160:                          ///+ 1 Significant Digits
            Value = Math.sig(paramValue, 1);
            break;
        case 1161:                          ///+ 2 Significant Digits
            Value = Math.sig(paramValue, 2);
            break;
        case 1162:                          ///+ 3 Significant Digits
            Value = Math.sig(paramValue, 3);
            break;
        case 1163:                          ///+ 4 Significant Digits
            Value = Math.sig(paramValue, 4);
            break;
        case 1164:                          ///+ 5 Significant Digits
            Value = Math.sig(paramValue, 5);
            break;
        case 1165:                          ///+ 6 Significant Digits
            Value = Math.sig(paramValue, 6);
            break;
        case 1166:                          ///+ 7 Significant Digits
            Value = Math.sig(paramValue, 7);
            break;
        case 1167:                          ///+ 8 Significant Digits
            Value = Math.sig(paramValue, 8);
            break;
        case 5006:                          ///(BP)6 Decimal Digits
            Value = (paramValue * 100).toFixed(6);
            break;
        case 5007:                          ///(BP)7 Decimal Digits
            Value = (paramValue * 100).toFixed(7);
            break;
        case 5008:                          ///(BP)8 Decimal Digits
            Value = (paramValue * 100).toFixed(8);
            break;
        case 5009:                          ///(BP)9 Decimal Digits
            Value = (paramValue * 100).toFixed(9);
            break;
        case 5010:                          ///(BP)10 Decimal Digits
            Value = (paramValue * 100).toFixed(10);
            break;
        case 5011:                          ///(BP)11 Decimal Digits
            Value = (paramValue * 100).toFixed(11);
            break;
        case 5012:                          ///(BP)12 Decimal Digits
            Value = (paramValue * 100).toFixed(12);
            break;
        case 5013:                          ///(BP)13 Decimal Digits
            Value = (paramValue * 100).toFixed(13);
            break;
        case 5014:                          ///(BP)14 Decimal Digits
            Value = (paramValue * 100).toFixed(14);
            break;
        case 5015:                          ///(BP)15 Decimal Digits
            Value = (paramValue * 100).toFixed(15);
            break;
        case 5016:                          ///(BP)16 Decimal Digits
            Value = (paramValue * 100).toFixed(16);
            break;
        case 5020:                          ///(BP)< +1
            Value = (paramValue * 100).toFixed(0);
            break;
        case 5021:                          ///(BP)< +1.2
            Value = (paramValue * 100).toFixed(1);
            break;
        case 5022:                          ///(BP)< +1.23
            Value = (paramValue * 100).toFixed(2);
            break;
        case 5023:                          ///(BP)< +1.235
            Value = (paramValue * 100).toFixed(3);
            break;
        case 5024:                          ///(BP)< +1 2346
            Value = (paramValue * 100).toFixed(4);
            break;
        case 5025:                          ///(BP)< +1.23457
            Value = (paramValue * 100).toFixed(5);
            break;
        case 5026:                         ///(BP)< +1.234568
            Value = (paramValue * 100).toFixed(6);
            break;
        case 5027:                         ///(BP)< +1.2345679
            Value = (paramValue * 100).toFixed(7);
            break;
        case 5028:                         ///(BP)< +1.23456790
            Value = (paramValue * 100).toFixed(8);
            break;
        case 5029:                         ///(BP)< +1.23456789
            Value = (paramValue * 100).toFixed(9);
            break;
        case 5030:                         ///(BP)< +1.2345678901
            Value = (paramValue * 100).toFixed(10);
            break;
        case 5031:                         ///(BP)< +1.23456789012
            Value = (paramValue * 100).toFixed(11);
            break;
        case 5032:                         ///(BP)< +1.234567890123 
            Value = (paramValue * 100).toFixed(12);
            break;
        case 5033:                         ///(BP)< +1.2345678901235
            Value = (paramValue * 100).toFixed(13);
            break;
        case 5034:                         ///(BP)< +1.23456789012346
            Value = (paramValue * 100).toFixed(14);
            break;
        case 5035:                         ///(BP)< +1.234567890123457
            Value = (paramValue * 100).toFixed(15);
            break;
        case 5036:                         ///(BP)< +1.2345678901234568
            Value = (paramValue * 100).toFixed(16);
            break;
        case 5040:                          ///(BP) 0 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(0));
            break;
        case 5041:                          ///(BP) 1 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(1));
            break;
        case 5042:                          ///(BP) 2 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(2));
            break;
        case 5043:                          ///(BP) 3 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(3));
            break;
        case 5044:                          ///(BP) 4 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(4));
            break;
        case 5045:                          ///(BP) 5 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(5));
            break;
        case 5046:                          ///(BP) 6 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(6));
            break;
        case 5047:                          ///(BP) 7 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(7));
            break;
        case 5048:                          ///(BP) 8 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(8));
            break;
        case 5049:                          ///(BP) 9 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(9));
            break;
        case 5050:                          ///(BP) 10 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(10));
            break;
        case 5051:                          ///(BP) 11 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(11));
            break;
        case 5052:                          ///(BP) 12 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(12));
            break;
        case 5053:                          ///(BP) 13 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(13));
            break;
        case 5054:                          ///(BP) 14 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(14));
            break;
        case 5055:                          ///(BP) 15 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(15));
            break;
        case 5056:                          ///(BP) 16 Separators for Thousands
            Value = ThousandSeparator(0, (paramValue * 100).toFixed(16));
            break;
        case 5060:                          ///(BP)< +1
            Value = (paramValue * 100).toFixed(0);
            break;
        case 5061:                          ///(BP)< +1.2
            Value = (paramValue * 100).toFixed(1);
            break;
        case 5062:                          ///(BP)< +1.23
            Value = (paramValue * 100).toFixed(2);
            break;
        case 5063:                          ///(BP)< +1.235
            Value = (paramValue * 100).toFixed(3);
            break;
        case 5064:                          ///(BP)< +1 2346
            Value = (paramValue * 100).toFixed(4);
            break;
        case 5065:                          ///(BP)< +1.23457
            Value = (paramValue * 100).toFixed(5);
            break;
        case 5066:                         ///(BP)< +1.234568
            Value = (paramValue * 100).toFixed(6);
            break;
        case 5067:                         ///(BP)< +1.2345679
            Value = (paramValue * 100).toFixed(7);
            break;
        case 5068:                         ///< +1.2345679
            Value = (paramValue * 100).toFixed(8);
            break;
        case 5069:                         ///(BP)< +1.23456789
            Value = (paramValue * 100).toFixed(9);
            break;
        case 5070:                         ///(BP)< +1.2345678901
            Value = (paramValue * 100).toFixed(10);
            break;
        case 5071:                         ///(BP)< +1.23456789012
            Value = (paramValue * 100).toFixed(11);
            break;
        case 5072:                         ///(BP)< +1.234567890123 
            Value = (paramValue * 100).toFixed(12);
            break;
        case 5073:                         ///(BP)< +1.2345678901235
            Value = (paramValue * 100).toFixed(13);
            break;
        case 5074:                         ///(BP)< +1.23456789012346
            Value = (paramValue * 100).toFixed(14);
            break;
        case 5075:                         ///(BP)< +1.234567890123457
            Value = (paramValue * 100).toFixed(15);
            break;
        case 5076:                         ///(BP)< +1.2345678901234568
            Value = (paramValue * 100).toFixed(16);
            break;
        case 5100:                         ///<(BP) +1 [kmb]
            Value = ConvertToKMB((paramValue * 100), 0);
            break;
        case 5101:                          ///<(BP) +1.2    [kmb]
            Value = ConvertToKMB((paramValue * 100), 1);
            break;
        case 5102:                          ///<(BP) +1.23 [kmb]
            Value = ConvertToKMB((paramValue * 100), 2);
            break;
        case 5103:                         ///<(BP) +1.235 [kmb]
            Value = ConvertToKMB((paramValue * 100), 3);
            break;
        case 5120:                         ///<(BP) 1 [kmb]
            Value = ConvertToKMB((paramValue * 100), 0);
            break;
        case 5121:                          ///<(BP) 1.2    [kmb]
            Value = ConvertToKMB((paramValue * 100), 1);
            break;
        case 5122:                          ///<(BP) 1.23 [kmb]
            Value = ConvertToKMB((paramValue * 100), 2);
            break;
        case 5123:                         ///<(BP) 1.235 [kmb]
            Value = ConvertToKMB((paramValue * 100), 3);
            break;
        case 5140:                          ///(BP)1 Significant Digits
            Value = Math.sig((paramValue * 100), 1);
            break;
        case 5141:                          ///(BP)2 Significant Digits
            Value = Math.sig((paramValue * 100), 2);
            break;
        case 5142:                          ///(BP)3 Significant Digits
            Value = Math.sig((paramValue * 100), 3);
            break;
        case 5143:                          ///(BP)4 Significant Digits
            Value = Math.sig((paramValue * 100), 4);
            break;
        case 5144:                          ///(BP)5 Significant Digits
            Value = Math.sig((paramValue * 100), 5);
            break;
        case 5145:                          ///(BP)6 Significant Digits
            Value = Math.sig((paramValue * 100), 6);
            break;
        case 5146:                          ///(BP)7 Significant Digits
            Value = Math.sig((paramValue * 100), 7);
            break;
        case 5147:                          ///(BP)8 Significant Digits
            Value = Math.sig((paramValue * 100), 8);
            break;
        case 5160:                          ///(BP)+ 1 Significant Digits
            Value = Math.sig((paramValue * 100), 1);
            break;
        case 5161:                          ///(BP)+ 2 Significant Digits
            Value = Math.sig((paramValue * 100), 2);
            break;
        case 5162:                          ///(BP)+ 3 Significant Digits
            Value = Math.sig((paramValue * 100), 3);
            break;
        case 5163:                          ///(BP)+ 4 Significant Digits
            Value = Math.sig((paramValue * 100), 4);
            break;
        case 5164:                          ///(BP)+ 5 Significant Digits
            Value = Math.sig((paramValue * 100), 5);
            break;
        case 5165:                          ///(BP)+ 6 Significant Digits
            Value = Math.sig((paramValue * 100), 6);
            break;
        case 5166:                          ///(BP)+ 7 Significant Digits
            Value = Math.sig((paramValue * 100), 7);
            break;
        case 5167:                          ///(BP)+ 8 Significant Digits
            Value = Math.sig((paramValue * 100), 8);
            break;
        default:
            if ((Format >= 5200 && Format <= 5203) || (Format >= 5300 && Format <= 5307) || (Format >= 5401 && Format <= 5407) || (Format >= 5504) || (Format >= 5220) || (Format >= 5221) || (Format >= 5421 && Format <= 5427) || (Format >= 5320 && Format <= 5327) || Format == 5600 || Format == 5601 || Format == 5620 || Format == 5621) {
                ///(BP)
                if (paramValue < 0) {
                    IsNeg = 'T';
                }
                else {
                    IsNeg = '';
                }
                Value = ParsePrice(Math.abs(parseFloat(paramValue * 100)), parseInt(Format));
            }
            else {
                if (paramValue < 0) {
                    IsNeg = 'T';
                }
                else {
                    IsNeg = '';
                }
                Value = ParsePrice(Math.abs(parseFloat(paramValue)), parseInt(Format));
            }
            break;
    }

    if (IsNeg == 'T')
        Value = "-" + Value;
    return Value;
}




Math.sig = function (num, sig) {
    if (num == 0)
        return 0;
    if (Math.round(num) == num)
        return num;
    var digits = parseInt((-Math.log(Math.abs(num)) / Math.LN10) + (sig || 2)); //round to significant digits (sig)
    if (digits < 0)
        digits = 0;
    return num.toFixed(digits);
}


function ThousandSeparator(decimalDigits, Value) {

    // Separator Length. Here this is thousand separator
    var separatorLength = 3;

    var OriginalValue = Value;

    var TempValue = "" + OriginalValue;

    var NewValue = "";

    // Store digits after decimal
    var pStr;

    // store digits before decimal
    var dStr;

    // Add decimal point if it is not there
    if (TempValue.indexOf(".") == -1) { TempValue += "." }

    dStr = TempValue.substr(0, TempValue.indexOf("."));

    pStr = TempValue.substr(TempValue.indexOf("."))

    // Add "0" for remaining digits after decimal point
    while (pStr.length - 1 < decimalDigits) { pStr += "0" }

    if (pStr == '.') pStr = '';

    if (dStr.length > separatorLength) {
        // Logic of separation    
        while (dStr.length > separatorLength) {
            NewValue = "," + dStr.substr(dStr.length - separatorLength) + NewValue;
            dStr = dStr.substr(0, dStr.length - separatorLength);
        }

        NewValue = dStr + NewValue;

    }
    else {
        NewValue = dStr;
    }


    //  Add decimal part
    NewValue = NewValue + pStr;

    // Show Final value
    return (NewValue);

}


function ConvertToKMB(x, p) {

    if (x < 1000) {
        return x.toFixed(p);
    }

    if (x < 1000000) {
        return (x / 1000).toFixed(p) + "k";
    }

    if (x < 1000000000) {
        return (x / 1000000).toFixed(p) + "m";
    }

    if (x < 1000000000000) {
        return (x / 1000000000).toFixed(p) + "b";
    }

}

