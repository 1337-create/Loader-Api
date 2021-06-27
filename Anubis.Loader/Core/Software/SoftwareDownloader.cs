using System;
using System.IO;
using System.Net;

namespace Anubis.Loader.Core.Software
{
    public class SoftwareDownloader
    {
        public static string PATH = Environment.GetFolderPath(Environment.SpecialFolder.System);
        //public static string API_FORWARD = "https://anubiss.cc/public/storage/retail/{zalupa}";
        private string ApiForward { get; set; }

        private WebClient m_WebClient;

        public SoftwareDownloader()
        { }

        public void ApplyRoute(string route)
        {
            ApiForward = route;
        }

        public bool Bind()
        {
            if(!ApplySoftware())
            {
                return false;
            }

            return true;
        }

        public bool IsRequiredItem(string item)
            => File.Exists(Path.Combine(PATH, item));

        private bool ApplySoftware()
        {
            if(m_WebClient == null)
            {
                m_WebClient = new WebClient();
            }
            
            if(!IsRequiredItem("msdia140.dll"))
            {
                if(!Download(AppendApi("msdia140.dll"), "msdia140.dll"))
                {
                    return false;
                }
            }

            if (!IsRequiredItem("symsrv.dll"))
            {
                if (!Download(AppendApi("symsrv.dll"), "symsrv.dll"))
                {
                    return false;
                }
            }

            return true;
        }

        public bool Download(string url, string item)
        {
            if(m_WebClient == null)
            {
                m_WebClient = new WebClient();
            }

            m_WebClient.DownloadFile(url, Path.Combine(PATH, item));

            return IsRequiredItem(item);
        }

        public string AppendApi(string item)
            => ApiForward.Replace("{zalupa}", item);
    }
}
