using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using HttpServer;
using HttpServer.Sessions;
using System.Net.Sockets;
using System.Net;

namespace ArchBench.PlugIns.Forwarder
{
    public class PlugIn : IArchServerModulePlugIn
    {
        private Dictionary<string,string> mServers = new Dictionary<string, string>();

        public PlugIn()
        {
            AddServer( "sidoc", "localhost:8083" );
        }

        private void AddServer( String aName, String aUrl )
        {
            if ( mServers.ContainsKey( aName ) )
                mServers[aName] = aUrl;
            else
                mServers.Add( aName, aUrl );
        }

        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            StringBuilder url = new StringBuilder();
            url.AppendFormat("http://{0}/", aRequest.Uri.Host, mServers[aRequest.UriParts[0]] );
            url.Append( aRequest.UriPath.Substring( aRequest.UriPath.IndexOf( '/', 1 ) + 1 ) );

            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url.ToString());
                response = (HttpWebResponse) request.GetResponse();

                Stream stream = response.GetResponseStream();

                int    count = 0;
                byte[] bytes = new byte[256];

                StringBuilder content = new StringBuilder();
                do
                {
                    count = stream.Read( bytes, 0, 256 );
                    content.Append( bytes, 0, count );

                } while (count > 0);

                stream.Close();
            }
            catch ( Exception e )
            {
                Host.Logger.WriteLine( String.Format( "Error on plugin Forwarder : {0}", e.Message ) );
            }
            finally
            {
                if ( response != null ) response.Close();
            }

            return true;
        } 

        #endregion

        #region IArchServerPlugIn Members

        public string Name
        {
            get { return "ArchServer Forwarder Plugin"; }
        }

        public string Description
        {
            get { return "Forward any request to port 8083"; }
        }

        public string Author
        {
            get { return "Leonel Nobrega"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

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