
using System.IO;
using System.Threading;
using HttpServer;
using HttpServer.Sessions;
using System.Collections.Generic;

namespace ArchBench.PlugIns.Events
{
    public class PlugInEvents : IArchServerHTTPPlugIn
    {
        private static int counter = 0;

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( aRequest.Uri.AbsolutePath.StartsWith( @"/favicon.ico" ) )
            {
                return false;
            }
            if ( aRequest.Uri.AbsolutePath.StartsWith( @"/footzon/event" ) )
            {
                Thread.Sleep( 5000 );

                aResponse.ContentType = @"text/event-stream";
                var writer = new StreamWriter( aResponse.Body );
                writer.Write( "Event : {0}", ++counter );
                writer.Flush();

                return true;
            }
            if ( aRequest.Uri.AbsolutePath.StartsWith( @"/footzon" ) )
            {
                var writer = new StreamWriter( aResponse.Body );
                writer.Write( Resource.index );
                writer.Flush();

                return true;
            }
            return false;
        }

        #region IArchServerPlugIn Members

        public string Name => "ArchServer Events Plugin";

        public string Description => "Testing of HTML5 Server Sent Events feature";

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
