using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Logger
{
    public class PlugInLogger : IArchServerModulePlugIn
    {
        public string Name
        {
            get { return "ArchBench Logger Server PlugIn"; }
        }

        public string Description
        {
            get { return "Log all HTTP requests."; }
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

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( Host != null )
            {
                Host.Logger.WriteLine( "HTTP Request -----------------------------------" );
                Host.Logger.WriteLine( "{0} {1} {2}", aRequest.Method, aRequest.UriPath, aRequest.HttpVersion );
                foreach ( var key in aRequest.Headers.AllKeys  )
                {
                    Host.Logger.WriteLine( "{0}: {1}", key, aRequest.Headers[key] );
                }
                Host.Logger.WriteLine();
            }

            return false;
        }
    }
}
