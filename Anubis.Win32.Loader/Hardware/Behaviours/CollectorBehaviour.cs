using Anubis.System;
using Anubis.Win32.Loader.Hardware.Behaviours.Collectors;
using Anubis.Win32.Loader.Hardware.Behaviours.Collectors.Archetype;

using System.Collections.Generic;
using System.Linq;

namespace Anubis.Win32.Loader.Hardware.Behaviours
{
    public class CollectorBehaviour : ExBehaviour
    {
        private List<BaseCaptionCollector> ImplementedCaptions;

        public override void Awake()
        {
            ImplementedCaptions = new List<BaseCaptionCollector>
            {
                new NetworkCaptionCollector(),
                new ProcessorCaptionCollector(),
                new VolumeCaptionCollector()
            };
        }

        public T GetCaption<T>() where T : BaseCaptionCollector
        {
            if ( ImplementedCaptions.Count > 0 )
            {
                try
                {
                    return ( T )ImplementedCaptions.FirstOrDefault( ( x ) => x.GetType() == typeof( T ) );
                }
                catch
                {
                    return default;
                }
            }

            return default;
        }

        public string GenerateIdentifier()
        {
            return GetCaption<NetworkCaptionCollector>()?.GetMacAddress()?.Trim() ?? "undefined"
                + "__" + GetCaption<VolumeCaptionCollector>()?.GetSerialNumber()?.Trim() ?? "undefined"
                + "__" + GetCaption<ProcessorCaptionCollector>()?.GetProcessorId()?.Trim() ?? "undefined";
        }
    }
}
