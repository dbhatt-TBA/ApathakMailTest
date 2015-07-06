using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VCM.Common;
using VCM.Common.Log;
public class BasePage : System.Web.UI.Page
{
	public BasePage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private SessionInfo _session;
    public SessionInfo CurrentSession
    {
        get
        {
            if (_session == null)
            {
                _session = new SessionInfo(HttpContext.Current.Session);
            }
            return _session;
        }
        set
        {
            _session = value;
        }
    }


    /// <summary>
    /// This function used to Convert string into base64 Encode string value.
    /// </summary>
    /// <param name="strData"></param>
    /// <returns>Encode string value</returns>
    public static string base64Encode(string strData)
    {

        byte[] encData_byte = new byte[strData.Length];

        encData_byte = System.Text.Encoding.UTF8.GetBytes(strData);

        string encodedData = Convert.ToBase64String(encData_byte);

        return encodedData;

    }

    /// <summary>
    /// This function used to Convert base64 Encode string value into base64 Decode string value
    /// </summary>
    /// <param name="strData"></param>
    /// <returns>Decode string value</returns>
    public static string base64Decode(string strData)
    {

        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

        System.Text.Decoder utf8Decode = encoder.GetDecoder();

        byte[] todecode_byte = Convert.FromBase64String(strData);

        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

        char[] decoded_char = new char[charCount];

        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

        string result = new String(decoded_char);

        return result;

    }

}
