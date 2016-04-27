using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArchBench.PlugIns.Hello
{
    public class Hello : IArchServerModulePlugIn
    {
        public string Name
        {
            get { return "Hello Module Plug In"; }
        }

        public string Description
        {
            get { return "Hello..."; }
        }

        public string Author
        {
            get { return "Leonel"; }
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
