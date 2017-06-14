using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using HttpServer;
using HttpServer.Sessions;
using System.Linq;

namespace ArchBench.PlugIns.Broker
{
    public class PlugInBroker : IArchServerModulePlugIn
    {
        #region Fields
        private int mNextServer = -1;
        private Thread mRegisterThread;
        #endregion

        #region Regist/Unregist servers

        private void ReceiveThreadFunction()
        {
            TcpListener listener = null;
            try
            {
                // Start listening for client requests.
                listener = new TcpListener( IPAddress.Any, 9000 );
                listener.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];

                // Enter the listening loop.
                while (true)
                {
                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = listener.AcceptTcpClient();

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int count = stream.Read(bytes, 0, bytes.Length);
                    if ( count != 0 )
                    {
                        // Translate data bytes to a ASCII string.
                        String data = Encoding.ASCII.GetString( bytes, 0, count );

                        char operation = data[0];
                        String server  = data.Substring( 1, data.IndexOf( '-', 1 ) - 1 );
                        String port    = data.Substring( data.IndexOf( '-', 1 ) + 1 );
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
               if ( listener != null ) listener.Stop();
            }
        }

        private readonly List<KeyValuePair<string,int>> mServers = new List<KeyValuePair<string,int>>();
        
        private void Regist( String aAddress, int aPort )
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
            //if ( mServers.Count == 0 ) return false;
            //mNextServer = ( mNextServer + 1 ) % mServers.Count;

            //Host.Logger.WriteLine( "Forwarding request to server on {0}:{1}", mServers[mNextServer].Key, mServers[mNextServer].Value );

            //string sourceHost = String.Format( "{0}:{1}", aRequest.Uri.Host, aRequest.Uri.Port );
            //string targetHost = String.Format( "{0}:{1}", mServers[mNextServer].Key, mServers[mNextServer].Value );
            //string targetUrl  = String.Format( "http://{0}{1}{2}", targetHost, aRequest.Uri.AbsolutePath, GetQueryString( aRequest ) );

//            Uri uri = new Uri( targetUrl );
            Uri uri = new Uri( "http:/" + aRequest.Uri.AbsolutePath );

            //Host.Logger.WriteLine( String.Format( "Forwarding request from server {0} to server {1}", sourceHost, targetHost ) );

            WebClient client = new WebClient();
            try
            {
                ForwardCookie( client, aRequest );

                byte[] bytes = null;
                if ( aRequest.Method == Method.Post )
                {
	                NameValueCollection form = new NameValueCollection();
                    foreach ( HttpInputItem item in aRequest.Form )
                    {
                        form.Add( item.Name, item.Value );
                    }
	                bytes = client.UploadValues( uri, form );		
                }
                else
                {
                    bytes = client.DownloadData( uri );
                }

                BackwardCookie( client, aResponse );
                aResponse.ContentType = client.ResponseHeaders[HttpResponseHeader.ContentType];

                if ( aResponse.ContentType.StartsWith( "text/html" ) )
                {
                    string data = client.Encoding.GetString( bytes );
//                    data = data.Replace( targetHost, sourceHost );
                    
                    var writer = new StreamWriter( aResponse.Body, client.Encoding );
                    writer.Write(data); writer.Flush();
                }
                else
                {
                    aResponse.Body.Write( bytes, 0, bytes.Length );
                }
            }
            catch (Exception e)
            {
                Host.Logger.WriteLine( "Error on plugin Forwarder : {0}", e.Message );
            }

            return true;
        }

        private string GetQueryString( IHttpRequest aRequest )
        {
            int count = aRequest.QueryString.Count();
            if ( count == 0 ) return "";

            var parameters = new StringBuilder( "?" );
            foreach ( HttpInputItem item in aRequest.QueryString )
            {
                parameters.Append( $"{item.Name}={item.Value}" );
                if ( --count > 0 ) parameters.Append( '&' );
            }
            return parameters.ToString();
        }

        private void ForwardCookie( WebClient aClient, IHttpRequest aRequest )
        {
            if ( aRequest.Headers["Cookie"] == null ) return;
            aClient.Headers.Add( "Cookie", aRequest.Headers["Cookie"] );    
        } 

        private void BackwardCookie( WebClient aClient, IHttpResponse aResponse )
        {
            if ( aClient.ResponseHeaders["Set-Cookie"] == null ) return;
            aResponse.AddHeader( "Set-Cookie", aClient.ResponseHeaders["Set-Cookie"] );
        }

        #endregion

        #region IArchServerPlugIn Members

        public string Name => "BROKER";

        public string Description => "Forward any request to port 8083";

        public string Author => "Leonel Nobrega";

        public string Version => "1.0";

        public bool Enabled { get; set; }

        public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        public IArchServerPlugInHost Host { get; set; }

        public void Initialize()
        {
            mRegisterThread = new Thread( ReceiveThreadFunction ) { IsBackground = true };
            mRegisterThread.Start();
        }

        public void Dispose()
        {
        }

        #endregion
    }
}