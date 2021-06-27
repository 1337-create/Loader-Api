using Anubis.Loader.Core.Hardware.Collectors.Archetype;

namespace Anubis.Loader.Core.Hardware.Collectors
{
    public class VideoCaptionCollector : BaseCaptionCollector
    {
        public VideoCaptionCollector()
            : base(new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController"))
        { }

        public string GetRam()
            => Get<string>("AdapterRAM");

        public string GetVideoProcessor()
            => Get<string>("VideoProcessor");
    }
}
