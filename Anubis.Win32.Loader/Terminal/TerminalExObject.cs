using Anubis.System;
using Anubis.System.Attributes;
using Anubis.Win32.Loader.Terminal.Behaviours;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anubis.Win32.Loader.Terminal
{
    public enum WriteBehaviourType
    {
        Info = 7,
        Error = 12,
        Success = 10, 
        Warning = 10,
        Debug = 11
    }

    [RequiredBehaviour(typeof(ReadBehaviour))]
    [RequiredBehaviour(typeof(WriteBehaviour))]
    public class TerminalExObject : ExObject
    {
        public WriteBehaviour Write()
            => GetComponent<WriteBehaviour>();

        public ReadBehaviour Read()
            => GetComponent<ReadBehaviour>();
    }
}
