// Type: HttpServer.IHttpInput
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using System.Collections;
using System.Collections.Generic;

namespace HttpServer
{
    public interface IHttpInput : IEnumerable<HttpInputItem>, IEnumerable
    {
        HttpInputItem this[ string name ] { get; }
        void Add( string name, string value );
        bool Contains( string name );
    }
}
