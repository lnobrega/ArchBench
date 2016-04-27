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

        public string Name
        {
            get { return "ArchServer Logout Plugin"; }
        }

        public string Description
        {
            get { return "Process /user/logout/ requests"; }
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
