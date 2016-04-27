using System;
using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Counter
{
    public class PlugInCounter : IArchServerModulePlugIn
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
