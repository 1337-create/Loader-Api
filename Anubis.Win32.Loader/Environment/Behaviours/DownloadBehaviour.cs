using Anubis.System;

using System.IO;
using System.Linq;
using System.Net;
using Sys = System;

namespace Anubis.Win32.Loader.Environment.Behaviours
{
    public class DownloadBehaviour : ExBehaviour
    {
        private WebClient m_WebClient;
        private string m_Api;

        public override void Awake()
        {
            m_WebClient = new WebClient();
            m_Api = "http://pubg.anubiss.cc/api/";
        }
        public bool DownloadFromLink(string link, out string path)
        {
            path = CachePath() + "\\" + GetFileNameFromLink( link );

            RemoveIfExists( path );

            m_WebClient.DownloadFile( link, path );

            return File.Exists(path);
        }
        public bool DownloadFromStorage(string file)
        {
            string link = $"http://anubiss.cc/public/storage/e70bafea599b20ad05a6804de1340b06/{file}";
            string downloadPath = SystemPath() + $"\\{file}";

            RemoveIfExists( downloadPath ); 

            m_WebClient.DownloadFile( link, SystemPath() + $"\\{file}" );
            return File.Exists( downloadPath );
        }
        public bool DownloadFromStorage(string[] files)
        {
            foreach(var file in files)
            {
                if ( !DownloadFromStorage( file ) )
                    return false;
            }

            return true;
        }

        public string SystemPath()
            => Sys.Environment.GetFolderPath( Sys.Environment.SpecialFolder.System );
        public string CachePath()
            => Sys.Environment.GetFolderPath( Sys.Environment.SpecialFolder.InternetCache );

        private string GetFileNameFromLink( string link )
            => link.Split( '/' ).Last();
        private void RemoveIfExists(string path)
        {
            if(File.Exists(path))
            {
                try
                {
                    File.Delete( path );
                }
                catch
                { }
            }
        }
    }
}
