using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;

public class JsonResponseFilter : Stream
{
private readonly Stream _responseStream;
private long _position;
StringBuilder _sb;
public JsonResponseFilter(Stream responseStream)
{
_responseStream = responseStream;
_sb = new StringBuilder();
}
    public override bool CanRead { get { return true; } }
    public override bool CanSeek { get { return true; } }
    public override bool CanWrite { get { return true; } }
    public override long Length { get { return 0; } }
    public override long Position { get { return _position; } set { _position = value; } }

    public override void Write(byte[] buffer, int offset, int count)
    {
        string strBuffer = System.Text.Encoding.UTF8.GetString(buffer, offset, count);
        strBuffer = strBuffer.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
        strBuffer = strBuffer.Replace("<string xmlns=\"http://tempuri.org/\">", "");
        strBuffer = strBuffer.Replace("Table", "d");
        strBuffer = strBuffer.Replace("</string>", "");

        _sb.Append(strBuffer);
        var endOfFile = new Regex("]");
        if (endOfFile.IsMatch(strBuffer))
        {

            string message = HttpContext.Current.Request.Params["callback"] + "(" + _sb.ToString() + ");";
            buffer = System.Text.Encoding.UTF8.GetBytes(message);
            _responseStream.Write(buffer, 0, buffer.Length);
        }
    }

    //public override void Write(byte[] buffer, int offset, int count)
    //{
    //    //string strBuffer = Encoding.UTF8.GetString(buffer, offset, count);
    //    //strBuffer = AppendJsonpCallback(strBuffer, HttpContext.Current.Request);
    //    //byte[] data = Encoding.UTF8.GetBytes(strBuffer);
    //    //_responseStream.Write(data, 0, data.Length);

    //    string strBuffer = Encoding.UTF8.GetString(buffer, offset, count);
    //    strBuffer = strBuffer.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>","");
    //    strBuffer = strBuffer.Replace("<string xmlns=\"http://tempuri.org/\">","");
    //    strBuffer = strBuffer.Replace("Table","d");
    //    strBuffer = strBuffer.Replace("</string>","");
       
    //    strBuffer = AppendJsonpCallback(strBuffer, HttpContext.Current.Request);
    //    byte[] data = Encoding.UTF8.GetBytes(strBuffer);
    //    _responseStream.Write(data, 0, data.Length);
    //}
    private string AppendJsonpCallback(string strBuffer, HttpRequest request)
    {
    return request.Params["callback"] + "(" + strBuffer + ");";
    }
    public override void Close()
    {
    _responseStream.Close();
    }
    public override void Flush()
    {
    _responseStream.Flush();
    }
    public override long Seek(long offset, SeekOrigin origin)
    {
    return _responseStream.Seek(offset, origin);
    }
    public override void SetLength(long length)
    {
    _responseStream.SetLength(length);
    }
    public override int Read(byte[] buffer, int offset, int count)
    {
    return _responseStream.Read(buffer, offset, count);
    }
}
