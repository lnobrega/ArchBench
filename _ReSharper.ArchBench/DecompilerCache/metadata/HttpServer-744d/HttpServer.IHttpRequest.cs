// Type: HttpServer.IHttpRequest
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using HttpServer.FormDecoders;
using System;
using System.Collections.Specialized;
using System.IO;

namespace HttpServer
{
    public interface IHttpRequest : ICloneable
    {
        string[] AcceptTypes { get; }
        Stream Body { get; set; }
        bool BodyIsComplete { get; }
        ConnectionType Connection { get; set; }
        int ContentLength { get; set; }
        RequestCookies Cookies { get; }
        HttpForm Form { get; }
        NameValueCollection Headers { get; }
        string HttpVersion { get; set; }
        bool IsAjax { get; }
        string Method { get; set; }
        HttpParam Param { get; }
        HttpInput QueryString { get; }
        Uri Uri { get; set; }
        string[] UriParts { get; }
        string UriPath { get; set; }
        void AddHeader( string name, string value );
        int AddToBody( byte[] bytes, int offset, int length );
        void Clear();
        void DecodeBody( FormDecoderProvider providers );
        void SetCookies( RequestCookies cookies );
        IHttpResponse CreateResponse( IHttpClientContext context );
    }
}
