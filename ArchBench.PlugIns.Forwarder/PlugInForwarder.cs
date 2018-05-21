using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Forwarder
{
    public class PlugInForwarder : IArchServerHTTPPlugIn
    {
        private readonly Dictionary<string,string> mServers = new Dictionary<string, string>();

        public PlugInForwarder()
        {
            AddServer( "sidoc", "127.0.0.1:8083" );
        }

        private void AddServer( string aName, string aUrl )
        {
            if ( mServers.ContainsKey( aName ) )
                mServers[aName] = aUrl;
            else
                mServers.Add( aName, aUrl );
        }

        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( mServers.ContainsKey(aRequest.UriParts[0]) )
            {
                string sourceHost = $"{aRequest.Uri.Host}:{aRequest.Uri.Port}";
                string sourcePath = aRequest.UriPath;

                string targetHost = mServers[aRequest.UriParts[0]];
                string targetPath = aRequest.UriPath.Substring(aRequest.UriPath.IndexOf( '/', 1 ) );

                string targetUrl = $"http://{targetHost}{targetPath}";
                Uri uri = new Uri( targetUrl );

                Host.Logger.WriteLine( $"Forwarding request from server {sourceHost} to server {targetHost}" );

                WebClient client = new WebClient();
                try
                {
                    if ( aRequest.Headers["Cookie"] != null )
                    {
                        client.Headers.Add( "Cookie", aRequest.Headers["Cookie"] );    
                    }

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

                    aResponse.ContentType = client.ResponseHeaders[HttpResponseHeader.ContentType];
                    if ( client.ResponseHeaders["Set-Cookie"] != null )
                    {
                        aResponse.AddHeader( "Set-Cookie", client.ResponseHeaders["Set-Cookie"] );
                    }

                    if ( aResponse.ContentType.StartsWith( "text/html" ) )
                    {
                        string data = client.Encoding.GetString( bytes );
                        data = data.Replace( targetHost, sourceHost + "/" + aRequest.UriParts[0] + "/" );
                        
                        data = data.Replace( "href=\"/", "href=\"/" + aRequest.UriParts[0] + "/" );
                        data = data.Replace( "src=\"/", "src=\"/" + aRequest.UriParts[0] + "/" );
                        data = data.Replace( "action=\"/", "action=\"/" + aRequest.UriParts[0] + "/" );

                        StreamWriter writer = new StreamWriter( aResponse.Body, client.Encoding );
                        writer.Write(data); writer.Flush();
                    }
                    else
                    {
                        aResponse.Body.Write(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    Host.Logger.WriteLine( "Error on plugin Forwarder : {0}", e.Message );
                }

                return true;
            }

            return false;
        } 

        #endregion

        #region IArchServerPlugIn Members

        public string Name => "ArchServer Forwarder Plugin";

        public string Description => "Forward any request to port 8083";

        public string Author => "Leonel Nobrega";

        public string Version => "1.0";
        public bool Enabled { get; set; }

        public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        public IArchServerPlugInHost Host { get; set; }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        #endregion
    }
}