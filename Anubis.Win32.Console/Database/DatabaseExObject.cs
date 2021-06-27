using Anubis.System;

namespace Anubis.Win32.Server.Database
{
    public class DatabaseExObject : ExObject
    {
        public DatabaseContext CreateContext()
        {
            var args = ServerContext.GetArgs();

            return new DatabaseContext(
                args.GetArgValue( ArgKey.DatabaseHost ),
                args.GetArgValue( ArgKey.DatabaseUser ),
                args.GetArgValue( ArgKey.DatabasePassword ),
                args.GetArgValue( ArgKey.DatabaseReference )
                );
        }
    }
}
