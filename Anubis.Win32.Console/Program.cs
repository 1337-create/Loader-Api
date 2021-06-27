using System;

namespace Anubis.Win32.Server
{
    class Program
    {
        static void Main( string[] args )
        {
            ServerContext.Build( args );
            ServerContext.GetNetwork().Listen();

            Hold();
        }

        private static void Hold()
        {
            while ( true )
            {
                var line = Console.ReadLine();
                if ( line == "quit" )
                    break;
            }
        }
    }
}
