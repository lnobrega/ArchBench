using System;
using System.IO;

using HttpServer;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Logout
{
    public class PlugInLogout : IArchServerModulePlugIn
    {
        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( ! aRequest.Uri.AbsolutePath.StartsWith("/user/logout") ) return false;

            Host.Logger.WriteLine( String.Format( "User [{0}] logged out.", aSession["Username"] ) );

            StreamWriter writer = new StreamWriter(aResponse.Body);
            writer.WriteLine( "<p>User <strong>{0}</strong> logout.</p>", aSession["Username"] );
            writer.WriteLine( "<a href=\"/user/login/\">Login</a>" );
            writer.Flush();

            aSession["Username"] = null;

            return true;
        }

        #endregion

        #region IArchServerPlugIn Members

        public string Name => "ArchServer Logout Plugin";

        public string Description => "Process /user/logout/ requests";

        public string Author => "Leonel Nobrega";

        public string Version => "1.0";

        public bool Enabled { get; set; }

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
