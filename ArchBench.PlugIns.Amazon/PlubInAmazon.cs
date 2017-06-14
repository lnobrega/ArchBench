using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Amazon
{
    public class PlubInAmazon : IArchServerModulePlugIn
    {
        public string Name => "ArchBench 'Amazon' PlugIn";

        public string Description => "Forward requests to amazon.co.uk.";

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

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( ! aRequest.Uri.AbsolutePath.StartsWith("/user/amazon") ) return false;

            WebClient client = new WebClient();

            byte[] bytes = client.DownloadData( string.Format( 
                "http://www.amazon.co.uk/s/ref=nb_sb_noss_2?url=search-alias%3Daps&field-keywords={0}",
                aRequest.QueryString["search"].Value ) );

            StreamWriter writer = new StreamWriter( aResponse.Body, client.Encoding );
            writer.Write( client.Encoding.GetString( bytes ) );

            return true;
        }
    }
}
