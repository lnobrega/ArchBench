using System;
using System.Collections.Generic;
using System.IO;

using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Store
{
    public class PlugInStore : IArchServerModulePlugIn
    {
        static Random rand = new Random();

        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( aRequest.Uri.AbsolutePath.StartsWith( "/user/store" ) )
            {
                StreamWriter writer = new StreamWriter(aResponse.Body);

                int number;
                if ( ! int.TryParse( Parameters["Number of Items"], out number ) ) number = 20;

                int count = rand.Next( number );
                writer.WriteLine( "<p>User <strong>{0}</strong></p>", aSession["Username"] );
                writer.WriteLine( "<a href=\"/user/store/\">Store</a>" );

                if ( ! string.IsNullOrEmpty( Parameters["Logout Url"] ) )
                {
                    writer.WriteLine( "<a href=\"{0}\">Logout</a>", Parameters["Logout Url"] );
                }

                writer.WriteLine( "<table>" );
                writer.WriteLine( "<caption>Products</caption>" );
                writer.WriteLine( "<th><td>Image</td><td>Description</td><td>Preco</td></th>" );
                for ( int i = 0 ; i < count ; i++ )
                {
                    writer.WriteLine( "<tr>" );
                    writer.WriteLine( "<td>&nbsp;</td>" );
                    writer.WriteLine( "<td><a href=\"/user/store\">Item {0}</a></td>", i + 1 );
                    writer.WriteLine( "<td>{0}</td>", rand.NextDouble() );
                    writer.WriteLine( "</tr>" ); 
                }
                writer.WriteLine("</table>");
                writer.Flush();

                return true;
            }

            return false;
        }

        #endregion

        #region IArchServerPlugIn Members

        public string Name => "ArchBench 'Store' Plugin";

        public string Description => "Process /user/store/ requests";

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
            Parameters.Add( "Number of Items", "20" );
            Parameters.Add( "Logout Url", "" );
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
