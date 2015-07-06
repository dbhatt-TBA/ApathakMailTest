using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SIF_XML_ToHTMLUtility
{
    class BeastField
    {
        public string FieldID;
        public string FieldTypeID;
        public string HtmlElementName;
    }

    class Definitations
    {
        public enum FieldTypes
        {
            [Description("lbl0")]
            FT_CAPTION = 0,

            [Description("")]
            FT_FRAME = 1,

            [Description("hr2")]
            FT_HLINE = 2,

            [Description("")]
            FT_VLINE = 3,

            [Description("")]
            FT_INFO = 4,

            [Description("")]
            FT_RAISED_FRAME = 5,

            [Description("")]
            FT_DROPPED_FRAME = 6,

            [Description("txt10")]
            FT_INT = 10,

            [Description("txt11")]
            FT_DOUBLE = 11,

            [Description("txt12")]
            FT_DATE = 12,

            [Description("txt13")]
            FT_STRING = 13,

            [Description("txt14")]
            FT_BINARY = 14,

            [Description("")]
            FT_SELECTBOX = 15,

            [Description("")]
            FT_3D_TOGGLE_BUTTON = 19,

            [Description("ddl20")]
            FT_LIST = 20,

            [Description("txt21")]
            FT_TERM = 21,

            [Description("ddl22")]
            FT_CYCLE_BUTTON = 22,

            [Description("btn23")]
            FT_SELECT_BUTTON = 23,

            [Description("")]
            FT_BITMAP_BUTTON = 24,

            [Description("btn25")]
            FT_PUSH_BUTTON = 25,

            [Description("btn26")]
            FT_TOGGLE_BUTTON = 26,

            [Description("img27")]
            FT_SIGNAL = 27,

            [Description("img28")]
            FT_ARROW = 28,

            [Description("")]
            FT_TERMINAL = 29,

            [Description("")]
            FT_CHART = 30,

            [Description("")]
            FT_PAGE = 31,

            [Description("txt32")]
            FT_ARROW_AND_DOUBLE = 32,

            [Description("txt33")]
            FT_ELAPSED_TIME = 33,

            [Description("btn34")]
            FT_DOUBLE_BUTTON = 34,

            [Description("btn35")]
            FT_POPUP_BUTTON = 35,

            [Description("")]
            FT_COMBO_LIST = 36,

            [Description("btn37")]
            FT_COMMAND_BUTTON = 37,

            [Description("chk38")]
            FT_CHECKBOX_CTRL = 38,

            [Description("")]
            FT_SPIN_CTRL = 39,

            [Description("")]
            FT_COMBOBOX_CTRL = 40,

            [Description("")]
            FT_EDIT_CTRL = 41,

            [Description("")]
            FT_GRID_CTRL = 42,

            [Description("")]
            FT_CELL = 43,

            [Description("")]
            FT_RADIO_CTRL = 44,

            [Description("")]
            FT_TREE_CTRL = 45,

            [Description("")]
            FT_TAB_CTRL = 46,

            [Description("")]
            FT_SCROLL_PANE_CTRL = 47,

            [Description("ddl50")]
            FT_CURRENCY = 50,

            [Description("ddl51")]
            FT_CURVE = 51,

            [Description("txt52")]
            FT_BASIS = 52,

            [Description("")]
            FT_STRING_OVERRIDE = 53,

            [Description("")]
            FT_DYNAMIC_LIST = 54,

            [Description("fu55")]
            FT_FILE = 55,

            [Description("lbl56")]
            FT_TIMER = 56,

            [Description("url57")]
            FT_URLBUTTON = 57,

            [Description("")]
            FT_WEBBROWSER = 58,

            [Description("")]
            FT_PROGRESSBAR = 59
        };

        public Dictionary<string, string> oFileMap = new Dictionary<string, string>();

    }

    class FieldNode
    {
        public string FieldID;
        public FieldProperties FieldProps;
    }

    class FieldProperties
    {
        public string Title;
        public int Left;
        public int Top;
        public int Width;
        public int TitleWidth;
        public string FieldType;
    }
}
