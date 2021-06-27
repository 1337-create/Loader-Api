using Anubis.System;
using Anubis.Win32.Server.Database;
using Anubis.Win32.Server.Network;

namespace Anubis.Win32.Server
{
    internal static class ServerContext
    {
        private static Args m_Args;
        private static DatabaseExObject m_Database;
        private static NetworkExObject m_Network;

        public static void Build( string[] args )
        {
            m_Args = new Args( args );
            m_Database = ExObject.Instantiate<DatabaseExObject>();
            m_Network = ExObject.Instantiate<NetworkExObject>();
        }

        public static Args GetArgs()
            => m_Args;
        public static DatabaseExObject GetDb()
            => m_Database;
        public static NetworkExObject GetNetwork()
            => m_Network;
    }
}
