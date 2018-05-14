using System.Diagnostics;
using HttpServer;
using HttpServer.HttpModules;
using HttpServer.Sessions;

namespace ArchBench.Server
{
    public class ModulePlugIns : HttpModule, IArchServerPlugInHost
    {
        public ModulePlugIns( IArchServerLogger aLogger )
        {
            Logger = aLogger;
            PlugInsManager = new PlugInsManager( this );
        }

        public PlugInsManager PlugInsManager { get; private set; }

        public override bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            foreach ( var archServerPlugIn in PlugInsManager.PlugIns )
            {
                var plugin = (IArchServerModulePlugIn) archServerPlugIn;
                Debug.Assert( plugin != null );

                if ( ! plugin.Enabled ) continue;
                if ( plugin.Process( aRequest, aResponse, aSession ) ) return true;
            }
            return false;
        }

        #region IArchServerPlugInHost Members

        public IArchServerLogger Logger
        {
            get; set;
        }

        #endregion
    }
}
