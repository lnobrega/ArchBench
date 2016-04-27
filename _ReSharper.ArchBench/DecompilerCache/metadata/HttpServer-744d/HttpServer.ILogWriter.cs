// Type: HttpServer.ILogWriter
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

namespace HttpServer
{
    public interface ILogWriter
    {
        void Write( object source, LogPrio priority, string message );
    }
}
