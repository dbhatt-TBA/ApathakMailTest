using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Definations
{
    public Dictionary<string, int> _dirImgSID;

    private static volatile Definations _instance = null;
    private static object syncRoot = new Object();

    public Definations()
    {
        _dirImgSID = new Dictionary<string, int>();
    }

    public static Definations Instance
    {
        get
        {
            lock (syncRoot)
            {
                if (_instance == null)
                {
                    _instance = new Definations();
                }
            }
            return _instance;
        }
    }
    public enum NodeDataStatus
    {
        DATANODEVALUESTATE_NORMAL = 0,
        DATANODEVALUESTATE_BLANK = 1,
        DATANODEVALUESTATE_NA = 2,
        DATANODEVALUESTATE_ERROR = 3,
    }

    //public enum BeastImageAppID
    //{
    //    vcm_calc_swaptionVolPremStrike = 451,
    //    vcm_calc_bondYield = 152,
    //    vcm_calc_cmefuture = 852,
    //    vcm_calc_cmeotcpage = 851,
    //    vcm_calc_irsfuture = 850,
    //    BI_LoadTest = 723,
    //    vcm_calc_iimswappagemid = 215,
    //    vcm_calc_MedTermSwapPage = 153,
    //    vcm_calc_Pers_Med_Term_Swap_Mid = 233,
    //    vcm_calc_CapFloorVols = 134,
    //    vcm_calc_CMCapFloor = 123,
    //    vcm_calc_CPINSA = 9091,
    //    vcm_calc_European_Swaption = 229,
    //    vcm_calc_ISDAFix = 9095,
    //    vcm_calc_IMM_Swap_Short = 403,
    //    vcm_calc_MidTermSwapPageMid = 253,
    //    vcm_calc_ShortTermSwapPage = 224,
    //    vcm_calc_SimpleSwapCalculator = 194,
    //    vcm_calc_SwapBondCalculator = 145,
    //    vcm_calc_SwapDiffCalculator = 210,
    //    vcm_calc_SwapSwitch = 171,
    //    //vcm_calc_swaptionVolPremStrike = 453,
    //    vcm_calc_Swap_Calculator = 105,
    //    vcm_calc_CMEDSF_vs_Bond_Future = 858,
    //    vcm_calc_CMEDSF_vs_EuroDollar_Future_Hedge = 859,
    //    vcm_calc_Cap_Floor_Calculator = 122,
    //    vcm_calc_Spread_Lock_Calculator = 200,
    //    vcm_calc_Swap_Spread_Page = 198,
    //    vcm_calc_Swap_Spread_Page_Mid = 217,
    //    vcm_calc_Swap_Spread_Spread_Page = 222,
    //    vcm_calc_Collar_Strangle_Corridor = 168,
    //    vcm_calc_Odd_FRA_Page = 102,
    //    vcm_calc_OIS_Swap_Page = 166,
    //    vcm_calc_Bond_Futures = 161,
    //    vcm_calc_Fed_Funds_Futures = 162,
    //    vcm_calc_FRA_Diff_Page = 127,
    //    vcm_calc_IR_Futures_Wide = 176,
    //    vcm_calc_IR_Futures = 101,
    //    vcm_calc_Tube_State_Presentation = 805,
    //    vcm_calc_CdsCalculator = 9074,
    //    vcm_calc_cds_switch_calculator = 9075,
    //    vcm_calc_CMEDSF_vs_OTCIRS_Calcs = 853,
    //    vcm_calc_CMEDSF_vs_Bond = 857,
    //    vcm_calc_CMEDSF_vs_OTC_IRS_Hedge = 861,
    //    vcm_calc_CME_DSF_vs_CTD_Bond = 863,
    //    vcm_calc_ESPN_Sports_Scores = 9010,
    //    vcm_calc_bond_option_page = 155,
    //    vcm_calc_forward_treasury_switch = 212,
    //    vcm_calc_clearing_broker_blotter = 778,
    //    vcm_calc_bond_select_page = 109,
    //    vcm_calc_tipsbond_select_page = 336,
    //    vcm_calc_treasury_bonds = 131,
    //    vcm_calc_treasury_bonds_ust_database = 189,
    //    vcm_calc_bond_feed_tracer = 2055,
    //    Vcm_Calc_Bond_Option_Calculator = 154,
    //    Vcm_Calc_Yield_Diff_Page = 156,
    //    Vcm_Calc_Yield_Diff_Page_Mid = 211,
    //    vcm_calc_calibration_result = 557,
    //    vcm_calc_cf_grid_page_3m = 419,
    //    vcm_calc_cf_grid_page_year = 418,
    //    vcm_calc_cf_volatility_smile = 417,
    //    vcm_calc_cap_floor_page = 120,
    //    vcm_calc_cap_vol_grid = 241,
    //    vcm_calc_atm_cap_vol = 245,
    //    vcm_calc_bermudan_swaption_calculator = 110,
    //    vcm_calc_cm_cap_floor_page = 121,
    //    vcm_calc_basis_swap_page = 111,
    //    vcm_calc_fra_page_short = 401,
    //    vcm_calc_imm_fra_page = 130,
    //    vcm_calc_fra_calculator = 400,
    //    vcm_calc_eonia_sonia_page = 128,
    //    vcm_calc_fra_page = 129,
    //    vcm_calc_fra_page_small = 169,
    //    vcm_calc_european_spreadlocks = 144,
    //    vcm_calc_par_swap_rates = 133,
    //    vcm_calc_3month_fras = 188,
    //    vcm_calc_6month_fras = 231,
    //    vcm_calc_tc_iro_swaption = 727,
    //    vcm_calc_tc_inf_infzero = 736,
    //    vcm_calc_tc_inf_infcpi = 735,
    //    vcm_calc_fixing_data = 103,
    //    vcm_calc_market_indicator = 182,
    //    vcm_calc_tc_infcapfloor = 772,
    //    vcm_calc_news = 655,
    //    vcm_calc_FFBasisSwapSpreads = 158,
    //    vcm_calc_TC_IRO_CapFloor = 728,
    //    vcm_calc_TC_CDS_CDSRisk = 732,
    //    vcm_calc_PageMonitor = 650,
    //    vcm_calc_swap_spreads = 137,
    //    vcm_calc_gmarket_par_swap = 9996,
    //    vcm_calc_Deposit_Rates = 178,
    //    vcm_calc_Market_Info = 219,
    //    vcm_calc_Overnight_Rates = 163,
    //    vcm_calc_tc_interest_rate_swaps = 795,
    //    vcm_calc_YieldCurves_YieldCurveChart = 170,
    //    vcm_calc_YieldCurves_YieldCurveChartDual = 174,
    //    vcm_calc_fx_cross_rates_bidask = 237,
    //    vcm_calc_curve_definition = 186,
    //    Vcm_Calc_Discount_Factor = 100,
    //    vcm_calc_ff_curve_definition = 164,
    //    vcm_calc_gov_asset_swap_page_new = 243,
    //    vcm_calc_manual_deposits = 118,
    //    vcm_calc_manual_deposits_full = 411,
    //    Vcm_Calc_Synthetic_FRA = 228,
    //    vcm_calc_synthetic_futures = 157,
    //    vcm_calc_synthetic_par_swaps = 160,       
    //    vcm_calc_Swaption_Historical = 762,
    //    vcm_calc_IRF_Convexity_Adjustment = 142,
    //    vcm_calc_CTD_ConversionFactor = 870,
    //    vcm_calc_fra_futures_daily = 407,                     
    //    vcm_calc_tc_generic = 731,
    //    vcm_calc_FX_FRA_Arbitrage = 183,
    //    vcm_calc_fx_cross_rates_flex = 196,
    //    vcm_calc_fx_deposit_arbitrage = 173,
    //    vcm_calc_fx_spot_rates = 180,
    //    vcm_calc_fx_cross_rates_mid = 238,
    //    vcm_calc_fx_cross_rate_grid = 239,
    //    vcm_calc_fx_forward_points = 179,
    //    vcm_calc_fx_ibor_comparison = 181,
    //    vcm_calc_fxzeroarbitrage = 227,       
    //    vcm_calc_cm_swap_calculator_new = 1119,
    //    vcm_calc_cm_spread_option = 1120,
    //    vcm_calc_caplet_vols = 135,
    //    vcm_calc_RGGI_options_calculator = 255,
    //    vcm_calc_smile_config_swaption_vols = 150,
    //    vcm_calc_swaption_vols = 136,
    //    vcm_calc_collar_strangle_corridor_page = 232,
    //    vcm_calc_swaption_vol_spreads = 452,
    //    vcm_calc_swaption_ab_vol_spreads = 454,
    //    vcm_calc_swaption_page = 107,
    //    vcm_calc_swaption_page_vol_smile = 124,
    //    vcm_calc_collar_strangle_corridor_page_mid = 234,
    //    vcm_calc_caps_floors_volatility_surface = 416,
    //    vcm_calc_Swap_Diff_Page_Wide = 223,
    //    vcm_calc_Basis_Swap_Calculator = 117,
    //    vcm_calc_CM_Swap_Page = 143,
    //    vcm_calc_Gov_Asset_Swap_Page = 165,
    //    vcm_calc_IMM_Swap_Page = 106,
    //    vcm_calc_Inf_Swap_Page = 412,
    //    vcm_calc_Inf_Swap_Page_Mid = 413,
    //    vcm_calc_Inf_ZC_Swap_Page_Mid = 713,
    //    vcm_calc_Simple_Asset_Swap_Calculator = 149,
    //    vcm_calc_Spread_Between_Swaps = 116,
    //    vcm_calc_Swap_Page_Horz = 213,
    //    vcm_calc_Swap_Spread_Table_Mid = 216,
    //    Vcm_Calc_Asset_Swap_Calculator = 185,
    //    vcm_calc_forward_swap_page = 112,
    //    vcm_calc_fixed_vs_cm_swap_calculator = 139,
    //    vcm_calc_imm_swap_arb_page = 146,
    //    vcm_calc_simple_irr_page = 195,
    //    vcm_calc_simple_swap_grid = 226,
    //    vcm_calc_forward_swap_page_large = 242,
    //    vcm_calc_swap_diff_page = 114,
    //    vcm_calc_usd_swaps_vs_1s = 251,
    //    vcm_calc_simple_swap_switch = 300,
    //    vcm_calc_tips_break_even_grid = 963,
    //    vcm_calc_odddated_forward_swap_page = 115,
    //    vcm_calc_cm_swap_calculator = 119,
    //    vcm_calc_cm_swap_grid = 140,
    //    vcm_calc_par_swap_rates_wide = 167,
    //    vcm_calc_cms_cmt_rsl_page = 184,
    //    vcm_calc_pers_med_term_swap_page = 236,
    //    vcm_calc_butterfly = 244,
    //    vcm_calc_swap_page = 108,
    //    vcm_calc_swap_spread_spread_page_small = 225,
    //    vcm_calc_swap_spread_table = 172,      
    //    vcm_calc_SwapPX_260 = 897,
    //    vcm_calc_SwapPX_262 = 898,
    //    vcm_calc_usd_libor_vs_libor_basis_swaps = 899,

    //    vcm_calc_realtimedata =60,
    //    vcm_calc_bondinfo = 2090,
    //    vcm_calc_kcg_bondpoint_bondlist = 2093,
    //    vcm_calc_transactional_currenexdetailview = 0812,
    //    vcm_calc_kcg_bondpoint_DepthOfBook = 2095,
    //    vcm_calc_kalotay_bond_analyzer = 8804
    //}

    public enum BeastImageHTMLClientID
    {
        vcm_calc_swaptionVolPremStrike = 451,
        vcm_calc_bondYield = 152,
        tblCMEFuture = 852
    }
}
