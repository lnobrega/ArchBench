// Type: HttpServer.HttpModules.HttpModule
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using HttpServer;
using HttpServer.Sessions;

namespace HttpServer.HttpModules
{
    public abstract class HttpModule
    {
        public virtual bool AllowSecondaryProcessing { get; }
        public abstract bool Process( IHttpRequest request, IHttpResponse response, IHttpSession session );
        public void SetLogWriter( ILogWriter writer );
        protected virtual void Write( LogPrio prio, string message );
    }
}
