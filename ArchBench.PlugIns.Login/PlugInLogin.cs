using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using HttpServer;
using HttpServer.Exceptions;
using HttpServer.Helpers;
using HttpServer.Sessions;

namespace ArchBench.PlugIns.Login
{
    public class PlugInLogin : IArchServerModulePlugIn
    {
        private readonly ResourceManager mManager = new ResourceManager();

        public PlugInLogin()
        {
            mManager.LoadResources(
                "/user/login/", Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly().GetName().Name );
            this.MimeTypes = new Dictionary<string, string>();
            AddDefaultMimeTypes();
        }

        #region IArchServerModulePlugIn Members

        public bool Process( IHttpRequest aRequest, IHttpResponse aResponse, IHttpSession aSession )
        {
            if ( aRequest.Method == Method.Get )
            {
                if ( IsResourceRequest(aRequest) )
                {
                    return ProcessResource( aRequest, aResponse );
                }

                if ( aRequest.Uri.AbsolutePath.StartsWith("/user/login") )
                {
                    StreamWriter writer = new StreamWriter(aResponse.Body);
                    writer.Write(Resource.login);
                    writer.Flush();

                    return true;
                }
            }
            else if ( aRequest.Method == Method.Post )
            {
                foreach ( HttpInputItem item in aRequest.Form )
                {
                    Host.Logger.WriteLine( "[{0}] := {1}", item.Name, item.Value );
                }

                if (aRequest.Form.Contains("Username"))
                {
                    aSession["Username"] = aRequest.Form["Username"].Value;
                    Host.Logger.WriteLine( "User [{0}] logged on.", aSession["Username"] );

                    StreamWriter writer = new StreamWriter(aResponse.Body);
                    writer.WriteLine( "<p>User <strong>{0}</strong> logged on.</p>", aSession["Username"] );
                    writer.WriteLine("<a href=\"/user/store/\">Store</a>");
                    writer.WriteLine("<a href=\"/user/logout/\">Logout</a>");
                    writer.Flush();

                    return true;
                }
                else
                {
                    Host.Logger.WriteLine("Error: invalid login data.");
                }
            }

            return false;
        }

        #endregion

        private bool ProcessResource( IHttpRequest aRequest, IHttpResponse aResponse )
        {
            if ( ! IsResourceRequest( aRequest ) ) return false;

            string type;
            Stream stream = GetResourceStream( GetResourceFilename(aRequest), out type );
            if ( stream == null ) return false;

            aResponse.ContentType = type;

            // Force the load of the resource
            aResponse.Status = HttpStatusCode.OK;
            aResponse.AddHeader( "Last-modified", DateTime.Now.ToString("r") );

            aResponse.ContentLength = stream.Length;
            aResponse.SendHeaders();

            
            if ( aRequest.Method != "Headers" && aResponse.Status != HttpStatusCode.NotModified )
            {
                byte[] buffer = new byte[8192];
                int bytesRead = stream.Read( buffer, 0, 8192 );
                while (bytesRead > 0)
                {
                    aResponse.SendBody(buffer, 0, bytesRead);
                    bytesRead = stream.Read( buffer, 0, 8192 );
                }
            }

            return true;
        }

        private string GetResourceFilename( IHttpRequest aRequest )
        {
            if ( aRequest.UriParts[aRequest.UriParts.Length - 1].IndexOf('.') != -1 )
                return aRequest.Uri.AbsolutePath;
            if ( aRequest.Uri.AbsolutePath.EndsWith("/") )
                return String.Format( "{0}.html",
                    aRequest.Uri.AbsolutePath.Substring( 0, aRequest.Uri.AbsolutePath.Length - 1 ) );
            return String.Format( "{0}.html", aRequest.Uri.AbsolutePath );
        }

        /// <summary>
        /// Returns true if the module can handle the request
        /// </summary>
        private bool IsResourceRequest( IHttpRequest aRequest )
        {
            if ( aRequest.Uri.AbsolutePath.EndsWith("*") ) return false;
            if (  ! mManager.ContainsResource( GetResourceFilename(aRequest) ) ) return false;
            return true;
        }

        /// <summary>
        /// List with all mime-type that are allowed. 
        /// </summary>
        /// <remarks>All other mime types will result in a Forbidden http status code.</remarks>
        public IDictionary<string, string> MimeTypes
        {
            get; private set;
        }

        /// <summary>
        /// Mimtypes that this class can handle per default
        /// </summary>
        public void AddDefaultMimeTypes()
        {
            MimeTypes.Add("default", "application/octet-stream");
            MimeTypes.Add("txt", "text/plain");
            MimeTypes.Add("html", "text/html");
            MimeTypes.Add("htm", "text/html");
            MimeTypes.Add("jpg", "image/jpg");
            MimeTypes.Add("jpeg", "image/jpg");
            MimeTypes.Add("bmp", "image/bmp");
            MimeTypes.Add("gif", "image/gif");
            MimeTypes.Add("png", "image/png");
            MimeTypes.Add("ico", "image/vnd.microsoft.icon");

            MimeTypes.Add("css", "text/css");
            MimeTypes.Add("gzip", "application/x-gzip");
            MimeTypes.Add("zip", "multipart/x-zip");
            MimeTypes.Add("tar", "application/x-tar");
            MimeTypes.Add("pdf", "application/pdf");
            MimeTypes.Add("rtf", "application/rtf");
            MimeTypes.Add("xls", "application/vnd.ms-excel");
            MimeTypes.Add("ppt", "application/vnd.ms-powerpoint");
            MimeTypes.Add("doc", "application/application/msword");
            MimeTypes.Add("js", "application/javascript");
            MimeTypes.Add("au", "audio/basic");
            MimeTypes.Add("snd", "audio/basic");
            MimeTypes.Add("es", "audio/echospeech");
            MimeTypes.Add("mp3", "audio/mpeg");
            MimeTypes.Add("mp2", "audio/mpeg");
            MimeTypes.Add("mid", "audio/midi");
            MimeTypes.Add("wav", "audio/x-wav");
            MimeTypes.Add("swf", "application/x-shockwave-flash");
            MimeTypes.Add("avi", "video/avi");
            MimeTypes.Add("rm", "audio/x-pn-realaudio");
            MimeTypes.Add("ram", "audio/x-pn-realaudio");
            MimeTypes.Add("aif", "audio/x-aiff");
        }

        private Stream GetResourceStream( String aPath, out String aType )
        {
            int position = aPath.LastIndexOf('.');
            string extension = position == -1 ? null : aPath.Substring( position + 1 );
            if ( extension == null )
                throw new InternalServerException("Failed to find file extension");

            if ( MimeTypes.ContainsKey(extension) )
                aType = MimeTypes[extension];
            else
                throw new ForbiddenException("Forbidden file type: " + extension);

            return mManager.GetResourceStream(aPath);
        }


        #region IArchServerPlugIn Members

        public string Name
        {
            get { return "ArchServer Login Plugin"; }
        }

        public string Description
        {
            get { return "Process /user/login/ requests"; }
        }

        public string Author
        {
            get { return "Leonel Nobrega"; }
        }

        public string Version
        {
            get { return "1.0"; }
        }

        public IArchServerPlugInHost Host
        {
            get; set;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
