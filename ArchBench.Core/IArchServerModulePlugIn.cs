using HttpServer;
using HttpServer.Sessions;

namespace ArchBench
{
    public interface IArchServerModulePlugIn : IArchServerPlugIn
    {
        bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession );
    }
}
