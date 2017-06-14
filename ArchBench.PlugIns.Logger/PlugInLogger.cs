using System.Collections.Generic;
using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Logger
{
    public class PlugInLogger : IArchServerModulePlugIn
    {
        #region IArchServerPlugIn Members

        public string Name => "ArchBench Logger Server PlugIn";

        public string Description => "Log all HTTP requests.";

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
        }

        public void Dispose()
        {
        }

        #endregion

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( Host == null ) return false;

            Host.Logger.WriteLine( "HTTP Request -----------------------------------" );
            Host.Logger.WriteLine( "{0} {1} {2}", aRequest.Method, aRequest.UriPath, aRequest.HttpVersion );
            foreach ( var key in aRequest.Headers.AllKeys  )
            {
                Host.Logger.WriteLine( "{0}: {1}", key, aRequest.Headers[key] );
            }
            Host.Logger.WriteLine();

            return false;
        }
    }
}
