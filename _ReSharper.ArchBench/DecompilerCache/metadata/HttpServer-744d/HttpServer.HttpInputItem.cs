// Type: HttpServer.HttpInputItem
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using System.Collections;
using System.Collections.Generic;

namespace HttpServer
{
    public class HttpInputItem : IHttpInput, IEnumerable<HttpInputItem>, IEnumerable
    {
        public static readonly HttpInputItem Empty;
        public HttpInputItem( string name, string value );
        public HttpInputItem( HttpInputItem item );
        public int Count { get; }
        public HttpInputItem this[ string name ] { get; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string LastValue { get; }
        public IList<string> Values { get; }

        #region IHttpInput Members

        public bool Contains( string name );
        public void Add( string name, string value );
        IEnumerator<HttpInputItem> IEnumerable<HttpInputItem>.GetEnumerator();
        public IEnumerator GetEnumerator();
        HttpInputItem IHttpInput.this[ string name ] { get; }

        #endregion

        public void Add( string value );
        public override string ToString();
        public string ToString( string prefix, bool asQuerySting );
        public string ToString( string prefix );
    }
}
