using Anubis.Loader.Core.Hardware.Collectors.Archetype;

namespace Anubis.Loader.Core.Hardware.Collectors
{
    public class ProcessorCaptionCollector : BaseCaptionCollector
    {
        public ProcessorCaptionCollector()
           : base(new System.Management.ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor"))
        { }

        public string GetProcessorId()
            => Get<string>("ProcessorId");

        public string GetProcessorName()
            => Get<string>("Name");
    }
}
