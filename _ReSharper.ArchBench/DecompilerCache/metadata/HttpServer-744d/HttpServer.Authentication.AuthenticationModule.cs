// Type: HttpServer.Authentication.AuthenticationModule
// Assembly: HttpServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=d7063c47c583695a
// Assembly location: C:\Archive.Academics\Disciplines\Software Architecture\Editions\2011-2012\ArchBench\DLLs\HttpServer.dll

using HttpServer;

namespace HttpServer.Authentication
{
    public abstract class AuthenticationModule
    {
        public const string AuthenticationTag = "__authtag";

        protected AuthenticationModule( AuthenticationHandler authenticator,
                                        AuthenticationRequiredHandler authenticationRequiredHandler );

        protected AuthenticationModule( AuthenticationHandler authenticator );
        public abstract string Name { get; }
        public abstract string CreateResponse( string realm, params object[] options );

        public abstract object Authenticate( string authenticationHeader, string realm, string httpVerb,
                                             params object[] options );

        protected bool CheckAuthentication( string realm, string userName, ref string password, out object login );
        public bool AuthenticationRequired( IHttpRequest request );
    }
}
