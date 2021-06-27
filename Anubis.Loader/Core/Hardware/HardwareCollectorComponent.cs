using Anubis.Loader.Core.Hardware.Collectors;
using Anubis.Loader.Core.Hardware.Collectors.Archetype;

using System.Collections.Generic;
using System.Linq;

namespace Anubis.Loader.Core.Hardware
{
    public class HardwareCollectorComponent
    {
        private List<BaseCaptionCollector> ImplementedCaptions;

        public void Awake()
        {
            ImplementedCaptions = new List<BaseCaptionCollector>
            {
                new NetworkCaptionCollector(),
                new OSCaptionCollector(),
                new ProcessorCaptionCollector(),
                new VideoCaptionCollector(),
                new VolumeCaptionCollector()
            };
        }

        public T GetCaption<T>() where T : BaseCaptionCollector
        {
            if (ImplementedCaptions.Count > 0)
            {
                try
                {
                    return (T)ImplementedCaptions.FirstOrDefault((x) => x.GetType() == typeof(T));
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
            return GetCaption<NetworkCaptionCollector>()?.GetMacAddress()?.Trim() ?? "undefuned"
                + "__" + GetCaption<VolumeCaptionCollector>()?.GetSerialNumber()?.Trim() ?? "undefined"
                + "__" + GetCaption<ProcessorCaptionCollector>()?.GetProcessorId()?.Trim() ?? "undefined";
        }
    }
}
