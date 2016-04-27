// Type: HttpServer.HttpInput
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using System.Collections;
using System.Collections.Generic;

namespace HttpServer
{
    public class HttpInput : IHttpInput, IEnumerable<HttpInputItem>, IEnumerable
    {
        public static readonly HttpInput Empty;
        protected readonly bool _ignoreChanges;
        public HttpInput( string name );
        protected HttpInput( string name, bool ignoreChanges );
        protected HttpInput( HttpInput input );
        public string Name { get; set; }

        #region IHttpInput Members

        public void Add( string name, string value );
        public bool Contains( string name );
        IEnumerator<HttpInputItem> IEnumerable<HttpInputItem>.GetEnumerator();
        public IEnumerator GetEnumerator();
        public HttpInputItem this[ string name ] { get; }

        #endregion

        public static HttpInputItem ParseItem( string name, string value );
        public override string ToString();
        public string ToString( bool asQueryString );
        public static string ExtractOne( string value );
        public virtual void Clear();
    }
}
