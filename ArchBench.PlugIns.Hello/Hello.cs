using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArchBench.PlugIns.Hello
{
    public class Hello : IArchServerHTTPPlugIn
    {
        public string Name => "Hello Module Plug In";

        public string Description => "Hello...";

        public string Author => "Leonel Nóbrega";

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
            if (aRequest.Uri.AbsolutePath == "/user/hello/")
            {
                if ( Host != null )
                {
                    Host.Logger.WriteLine( String.Format( "Accept request for : {0}", aRequest.Uri.ToString() ) );
                }

                var writer = new StreamWriter( aResponse.Body );
                writer.WriteLine( "Olá Malta..." );
                writer.Flush();

                aResponse.Send();

                return true;
            }
            return false;
        }
    }
}
