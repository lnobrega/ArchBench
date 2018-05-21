using System.Collections.Generic;
using System.IO;

namespace ArchBench.PlugIns.Hello
{
    public class Goodbye : IArchServerHTTPPlugIn
    {
        public string Name => "Goodbye Module Plug In";

        public string Description => "Goodbye...";

        public string Author => "Leonel";

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

        public bool Process( HttpServer.IHttpRequest aRequest, HttpServer.IHttpResponse aResponse, HttpServer.Sessions.IHttpSession aSession )
        {
            if (aRequest.Uri.AbsolutePath == "/user/bye/")
            {
                Host?.Logger.WriteLine( $"Accept request for : {aRequest.Uri}" );

                var writer = new StreamWriter( aResponse.Body );
                writer.WriteLine( "<library><book>Eu e eu...</book></library>" );
                writer.Flush();

                aResponse.Send();

                return true;
            }
            return false;
        }
    }
}
