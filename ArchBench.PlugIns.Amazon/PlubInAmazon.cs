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
        public string Name
        {
            get { return "ArchBench Server PlugIn Counter"; }
        }

        public string Description
        {
            get { return "Count the number of requests."; }
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
            get;
            set;
        }

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
                "http://www.amazon.co.uk/s/ref=nb_sb_noss?url=search-alias%3Daps&field-keywords={0}", 
                aRequest.QueryString["search"].Value ) );

            StreamWriter writer = new StreamWriter( aResponse.Body, client.Encoding );
            writer.Write( client.Encoding.GetString( bytes ) );

            return true;
        }
    }
}
