using System;
using System.IO;

using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Store
{
    public class PlugInStore : IArchServerModulePlugIn
    {
        private const int NumberOfMaximumItems = 20;

        static Random rand = new Random();

        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( aRequest.Uri.AbsolutePath.StartsWith( "/user/store" ) )
            {
                StreamWriter writer = new StreamWriter(aResponse.Body);

                int count = rand.Next( NumberOfMaximumItems );
                writer.WriteLine( "<p>User <strong>{0}</strong></p>", aSession["Username"] );
                writer.WriteLine( "<a href=\"/user/store/\">Store</a>" );
                writer.WriteLine( "<a href=\"/user/logout/\">Logout</a>" );
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

        public string Name
        {
            get { return "STORE"; }
        }

        public string Description
        {
            get { return "Process /user/store/ requests"; }
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
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
