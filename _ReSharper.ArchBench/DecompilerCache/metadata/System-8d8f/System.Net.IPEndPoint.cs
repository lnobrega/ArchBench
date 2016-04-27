// Type: System.Net.IPEndPoint
// Assembly: System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll

using System;
using System.Net.Sockets;

namespace System.Net
{
    [ Serializable ]
    public class IPEndPoint : EndPoint
    {
        public const int MinPort = 0;
        public const int MaxPort = 65535;
        public IPEndPoint( long address, int port );
        public IPEndPoint( IPAddress address, int port );
        public override AddressFamily AddressFamily { get; }
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public override string ToString();
        public override SocketAddress Serialize();
        public override EndPoint Create( SocketAddress socketAddress );
        public override bool Equals( object comparand );
        public override int GetHashCode();
    }
}
