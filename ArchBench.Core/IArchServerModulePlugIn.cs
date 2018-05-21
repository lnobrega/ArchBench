using HttpServer;
using HttpServer.Sessions;

namespace ArchBench
{
    public interface IArchServerHTTPPlugIn : IArchServerPlugIn
    {
        bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession );
    }
}
