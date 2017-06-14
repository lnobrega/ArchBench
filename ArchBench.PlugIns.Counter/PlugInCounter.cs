using System;
using System.Collections.Generic;
using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Counter
{
    public class PlugInCounter : IArchServerModulePlugIn
    {
        public string Name => "ArchBench Server PlugIn Counter";

        public string Description => "Count the number of requests.";

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

        private int Count { get; set; }

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( Host != null )
            {
                this.Count++;
                Host.Logger.WriteLine( "Request number: {0}", Count );
            }
            return false;
        }
    }
}
