// Type: HttpServer.HttpServer
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using HttpServer.Authentication;
using HttpServer.Exceptions;
using HttpServer.FormDecoders;
using HttpServer.HttpModules;
using HttpServer.Rules;
using HttpServer.Sessions;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace HttpServer
{
    public class HttpServer
    {
        public HttpServer( IComponentProvider provider );
        public HttpServer();
        public HttpServer( FormDecoderProvider decoderProvider );
        public HttpServer( IHttpSessionStore sessionStore );
        public HttpServer( ILogWriter logWriter );
        public HttpServer( FormDecoderProvider decoderProvider, ILogWriter logWriter );
        public HttpServer( FormDecoderProvider decoderProvider, IHttpSessionStore sessionStore, ILogWriter logWriter );
        public static HttpServer Current { get; }
        public IList<AuthenticationModule> AuthenticationModules { get; }
        public FormDecoderProvider FormDecoderProviders { get; }
        public string ServerName { get; set; }
        public string SessionCookieName { get; set; }
        public ILogWriter LogWriter { get; set; }
        public int BackLog { get; set; }
        public int MaxRequestCount { get; set; }
        public int MaxQueueSize { get; set; }
        public void Add( IRule rule );
        public void Add( HttpModule module );
        protected virtual void DecodeBody( IHttpRequest request );
        protected virtual void ErrorPage( IHttpResponse response, HttpStatusCode error, string body );
        protected virtual void ErrorPage( IHttpResponse response, HttpException err );
        protected virtual string GetRealm( IHttpRequest request );

        protected virtual void HandleRequest( IHttpClientContext context, IHttpRequest request, IHttpResponse response,
                                              IHttpSession session );

        protected virtual void OnClientDisconnected( IHttpClientContext client, SocketError error );
        protected virtual bool ProcessAuthentication( IHttpRequest request, IHttpResponse response, IHttpSession session );

        protected virtual void RequestAuthentication( AuthenticationModule mod, IHttpRequest request,
                                                      IHttpResponse response );

        public void Start( IPAddress address, int port );
        public void Start( IPAddress address, int port, X509Certificate certificate );
        public void Stop();
        protected virtual void WriteLog( LogPrio prio, string message );
        public void WriteLog( object source, LogPrio prio, string message );
        public event RealmHandler RealmWanted;
        public event ExceptionHandler ExceptionThrown;
    }
}
