using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using HttpServer;
using HttpServer.Sessions;
using System.Net.Sockets;
using System.Linq;


namespace ArchBench.PlugIns.Dispatcher
{
    public class PlugInDispatcher : IArchServerModulePlugIn
    {
        private readonly TcpListener mListener;
        private int                  mNextServer;
        private Thread               mRegisterThread;

        public PlugInDispatcher()
        {
            mListener = new TcpListener( IPAddress.Any, 9000 );
        }

        #region Regist/Unregist servers

        private void ReceiveThreadFunction()
        {
            try
            {
                // Start listening for client requests.
                mListener.Start();

                // Buffer for reading data
                byte[] bytes = new byte[256];

                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = mListener.AcceptTcpClient();

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int count = stream.Read(bytes, 0, bytes.Length);
                    if ( count != 0 )
                    {
                        // Translate data bytes to a ASCII string.
                        string data = Encoding.ASCII.GetString( bytes, 0, count );

                        char operation = data[0];
                        string server  = data.Substring( 1, data.IndexOf( '-', 1 ) - 1 );
                        string port    = data.Substring( data.IndexOf( '-', 1 ) + 1 );
                        switch ( operation )
                        {
                            case '+' : 
                                Regist( server, int.Parse( port ) );
                                break;
                            case '-' : 
                                Unregist( server, int.Parse( port ) );
                                break;
                        }
                    }

                    client.Close();
                }
            }
            catch ( SocketException e )
            {
                Host.Logger.WriteLine( "SocketException: {0}", e );
            }
            finally
            {
               mListener.Stop();
            }
        }

        private readonly List<KeyValuePair<string,int>> mServers = new List<KeyValuePair<string,int>>();
        
        private void Regist( string aAddress, int aPort )
        {
            if ( mServers.Any( p => p.Key == aAddress && p.Value == aPort ) ) return;
            mServers.Add( new KeyValuePair<string, int>( aAddress, aPort) );
            Host.Logger.WriteLine( "Added server {0}:{1}.", aAddress, aPort );
        }

        private void Unregist( string aAddress, int aPort )
        {
            if ( mServers.Remove( new KeyValuePair<string, int>( aAddress, aPort ) ) )
            {
                Host.Logger.WriteLine( "Removed server {0}:{1}.", aAddress, aPort );
            }
            else
            {
                Host.Logger.WriteLine( "The server {0}:{1} is not registered.", aAddress, aPort );
            }
        }

        #endregion

        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( mServers.Count == 0 ) return false;
            mNextServer = ( mNextServer + 1 ) % mServers.Count;

            Host.Logger.WriteLine( string.Format( "Dispatching to server on port {0}", mServers[mNextServer] ) );

            var redirection = new StringBuilder();
            redirection.AppendFormat("http://{0}:{1}", mServers[mNextServer].Key, mServers[mNextServer].Value );
            redirection.Append( aRequest.Uri.AbsolutePath );

            int count = aRequest.QueryString.Count();
            if ( count > 0 )
            {
                redirection.Append( '?' );
                foreach ( HttpInputItem item in aRequest.QueryString )
                {
                    redirection.Append( string.Format( "{0}={1}", item.Name, item.Value ) );
                    if ( --count > 0 ) redirection.Append( '&' );
                }
            }

            aResponse.Redirect( redirection.ToString() );

            return true;
        }

        #endregion

        #region IArchServerPlugIn Members

        public string Name => "ArchServer Dispatcher Plugin";

        public string Description => "Dispatch clients to the proper server";

        public string Author => "Leonel Nobrega";

        public string Version => "1.0";

        public bool Enabled { get; set; }

        public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        public IArchServerPlugInHost Host
        {
            get; set;
        }

        public void Initialize()
        {
            mRegisterThread = new Thread( ReceiveThreadFunction );
            mRegisterThread.IsBackground = true;
            mRegisterThread.Start();
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
