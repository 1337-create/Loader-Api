using Anubis.Win32.Loader.Hardware.Behaviours.Collectors.Archetype;
using SysManagement = System.Management;

namespace Anubis.Win32.Loader.Hardware.Behaviours.Collectors
{
    public class ProcessorCaptionCollector : BaseCaptionCollector
    {
        public ProcessorCaptionCollector()
           : base( new SysManagement.ManagementObjectSearcher( "root\\CIMV2", "SELECT * FROM Win32_Processor" ) )
        { }

        public string GetProcessorId()
            => Get<string>( "ProcessorId" );

        public string GetProcessorName()
            => Get<string>( "Name" );
    }
}
