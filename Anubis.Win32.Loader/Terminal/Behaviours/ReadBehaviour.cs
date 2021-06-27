using Anubis.System;

using System;

namespace Anubis.Win32.Loader.Terminal.Behaviours
{
    public class ReadBehaviour : ExBehaviour
    {
        //TODO: make command listener

        public T GetLineAs<T>()
        {
            var line = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(line) && !string.IsNullOrEmpty(line))
            {
                try
                {
                    return ( T )Convert.ChangeType( line, typeof( T ) );
                }
                catch
                {
                    return default;
                }
            }

            return default;
        }
    }
}
