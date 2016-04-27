using System;
using System.IO;
using HttpServer;
using HttpServer.HttpModules;
using HttpServer.Sessions;

namespace ArchBench.Server
{
    class ModuleHello : HttpModule
    {
        #region Field
        private readonly IArchServerLogger mLogger;
        #endregion

        public ModuleHello( IArchServerLogger aPublisher )
        {
            mLogger = aPublisher;
        }

        /// <summary>
        /// Method that process the URL
        /// </summary>
        /// <param name="aRequest">Information sent by the browser about the request</param>
        /// <param name="aResponse">Information that is being sent back to the client.</param>
        /// <param name="aSession">Session used to </param>
        /// <returns>true if this module handled the request.</returns>
        public override bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if (aRequest.Uri.AbsolutePath == "/user/hello/")
            {
                mLogger.WriteLine( String.Format( "Accept request for : {0}", aRequest.Uri.ToString() ) );

                var writer = new StreamWriter( aResponse.Body );
                writer.WriteLine( "Hello dude!" );
                writer.Flush();

                aResponse.Send();

                return true;
            }
            return false;
        }
    }
}

