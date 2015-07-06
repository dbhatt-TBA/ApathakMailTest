using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenBeast.Utilities
{
    public static class AppDetails
    {
        public const string BondYield = "F36E71CA25";
        public const string BYCManual = "D1904425E9";
        public const string CDS = "3952269336";
        public const string CDSSwitch = "A25D190442";
        public const string BondYieldManual_eSignal = "608E943DA3";

        public static string GetAppName(string eSignalKey)
        {
            string _AppName = string.Empty;
            switch (eSignalKey)
            {
                case BondYield:
                    _AppName = "BYCE0252";
                    break;
                case BYCManual:
                    _AppName = "vcm_calc_bondYield_Manual";
                    break;
                case CDS:
                    _AppName = "vcm_calc_CdsCalculator";
                    break;
                case CDSSwitch:
                    _AppName = "vcm_calc_cds_switch_calculator";
                    break;
                case BondYieldManual_eSignal:
                    _AppName = "BYCE0252";

                    break;
            }
            return _AppName;
        }

        public static string GetAppActualName(string eSignalKey)
        {
            string _AppName = string.Empty;
            switch (eSignalKey)
            {
                case BondYield:
                    _AppName = "Bond Yield Calculator";
                    break;
                case BYCManual:
                    _AppName = "Bond Yield Calculator (Manual)";
                    break;
                case CDS:
                    _AppName = "CDS Calculator";
                    break;
                case CDSSwitch:
                    _AppName = "CDS Switch Calculator";
                    break;
            }
            return _AppName;
        }

    }

  
}