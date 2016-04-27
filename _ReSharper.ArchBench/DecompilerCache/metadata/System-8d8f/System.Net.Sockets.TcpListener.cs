// Type: System.Net.Sockets.TcpListener
// Assembly: System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll

using System;
using System.Net;

namespace System.Net.Sockets
{
    public class TcpListener
    {
        public TcpListener( IPEndPoint localEP );
        public TcpListener( IPAddress localaddr, int port );

        [ Obsolete(
            "This method has been deprecated. Please use TcpListener(IPAddress localaddr, int port) instead. http://go.microsoft.com/fwlink/?linkid=14202"
            ) ]
        public TcpListener( int port );

        public Socket Server { get; }
        protected bool Active { get; }
        public EndPoint LocalEndpoint { get; }
        public bool ExclusiveAddressUse { get; set; }

        public void Start();
        public void Start( int backlog );
        public void Stop();
        public bool Pending();
        public Socket AcceptSocket();
        public TcpClient AcceptTcpClient();
        public IAsyncResult BeginAcceptSocket( AsyncCallback callback, object state );
        public Socket EndAcceptSocket( IAsyncResult asyncResult );
        public IAsyncResult BeginAcceptTcpClient( AsyncCallback callback, object state );
        public TcpClient EndAcceptTcpClient( IAsyncResult asyncResult );
    }
}
