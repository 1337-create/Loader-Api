using Anubis.Server.Database;
using Anubis.Server.Networking;

using System;

namespace Anubis.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = NetworkServer.GetInstance();
            network.Initialize();

            ConsoleWriter.Success("Network initialized");

            var db = Db.GetInstance();
            var context = db.CreateContext();
            ConsoleWriter.Success($"Database initialized. Provider: {context.Database.ProviderName}");

            foreach(var key in context.Regions)
            {
                ConsoleWriter.Info(key.Name);
            }

            ConsoleWriter.Success("Service started");

            Console.ReadKey();
        }
    }
}
