using Anubis.Win32.Loader.Hardware.Behaviours.Collectors.Archetype;
using SysManagement = System.Management;

namespace Anubis.Win32.Loader.Hardware.Behaviours.Collectors
{
    public class VolumeCaptionCollector : BaseCaptionCollector
    {
        public VolumeCaptionCollector()
            : base( new SysManagement.ManagementObjectSearcher( "SELECT * FROM Win32_DiskDrive" ) )
        { }

        public string GetInterfaceType()
            => Get<string>( "InterfaceType" );

        public string GetManufacturer()
            => Get<string>( "Manufacturer" );

        public string GetModel()
            => Get<string>( "Model" );

        public string GetSerialNumber()
            => Get<string>( "SerialNumber" );
    }
}
