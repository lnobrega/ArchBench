// Type: HttpServer.IHttpResponse
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpServer
{
    public interface IHttpResponse
    {
        Stream Body { get; set; }
        bool Chunked { get; set; }
        ConnectionType Connection { get; set; }
        Encoding Encoding { get; set; }
        int KeepAlive { get; set; }
        HttpStatusCode Status { get; set; }
        string Reason { get; set; }
        long ContentLength { get; set; }
        string ContentType { get; set; }
        bool HeadersSent { get; }
        bool Sent { get; }
        ResponseCookies Cookies { get; }
        void AddHeader( string name, string value );
        void Send();
        void SendBody( byte[] buffer, int offset, int count );
        void SendBody( byte[] buffer );
        void SendHeaders();
        void Redirect( Uri uri );
        void Redirect( string url );
    }
}
