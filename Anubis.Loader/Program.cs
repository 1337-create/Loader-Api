using Anubis.Loader.Core.Hardware;
using Anubis.Loader.Core.Network;
using Anubis.Loader.Core.Software;

using Jareem.Network.Packets;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace Anubis.Loader
{
    public static class ConsoleWriter
    {
        public static void Error(string msg)
        {
            SetupColor(ConsoleColor.Red);
            WriteLine("[-]" + msg);
            ResetColors();
        }

        public static void Warning(string msg)
        {
            SetupColor(ConsoleColor.Yellow);
            WriteLine("[!]" + msg);
            ResetColors();
        }

        public static void Success(string msg)
        {
            SetupColor(ConsoleColor.Green);
            WriteLine("[+]" + msg);
            ResetColors();
        }

        public static void Info(string msg)
        {
            SetupColor(ConsoleColor.White);
            WriteLine("[*]" + msg);
            ResetColors();
        }

        private static void WriteLine(string msg)
            => Write(msg + Environment.NewLine);
        private static void Write(string msg)
            => Console.Write(msg);

        private static void SetupColor(ConsoleColor color)
            => Console.ForegroundColor = color;

        private static void ResetColors()
            => Console.ResetColor();
    }

    class Program
    {
        private class WinStructureReserved
        {
            public string Useless { get; set; } = "";
            public bool Reserved1 { get; set; } = false;
            public int Reserved2 { get; set; } = 0;
            public string HDresultWindowParameter { get; set; } = "";

            public WinStructureReserved()
            { }
        }

        static void Main(string[] args)
            => Entry();

        public static void Entry()
        {
            AppDomain.CurrentDomain.UnhandledException += (ex, obj) => { };

            var client = new NetworkClient();

            ConsoleWriter.Info("Connecting ...");
            if(!client.Connect())
            {
                ConsoleWriter.Error("Can't connect to server");

                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                ConsoleWriter.Success("Connected");
            }

            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Encoding = Encoding.UTF8;

                    foreach (var process in Process.GetProcesses())
                    {
                        if (process.ProcessName == "loader")
                            process.Kill();

                        if (process.ProcessName == "notepad")
                            process.Kill();
                    }

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.exe"))
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.exe");

                    var collector = HardwareObject.Create();

                    string hardware = collector.GetHardwareIdentifier();
                    string locale = collector.GetTwoLetterLocaleCode();
                    ConsoleWriter.Warning("Put the key: ");
                    string key = Console.ReadLine();
                    Console.Clear();
                    ConsoleWriter.Warning("Connecting to server now ...");
                    var query = otebatTvoyuSestru(otebatTvoyuSestru($"{key}:{hardware}:{locale}"));

                    var result_raw = client.SendWait(new ApiAuthRequest() { Query = query });
                    if(result_raw == null)
                    {
                        ConsoleWriter.Error("Server not responded! Closing.");
                        return;
                    }

                    if(result_raw.PacketContent.Identifier != (int)Packets.ApiAuthResponse)
                    {
                        ConsoleWriter.Error("Invalid response! Closing.");
                        return;
                    }

                    var result = result_raw.PacketContent.Convert<ApiAuthResponse>();
                    if(result == null)
                    {
                        ConsoleWriter.Error("Invalid response! Closing.");
                        return;
                    }

                    Console.Clear();

                    if(result.Link != null)
                    {
                        var binder = new SoftwareBinder();
                        var downloader = new SoftwareDownloader();
                        downloader.ApplyRoute(result.Link);
                        binder.ApplyTemplate(downloader);
                        if (binder.Prepare())
                        {
                            if (binder.Execute(result.Seconds, result.Key))
                            {
                                ConsoleWriter.Success($"Done. Start the game. {result.Result}");
                            }
                            else
                            {
                                ConsoleWriter.Error($"Invalid application state. Please try again later");
                            }
                        }
                        else
                        {
                            ConsoleWriter.Error($"Can't prepare that hack for use it");
                        }
                    }
                    else
                    {
                        ConsoleWriter.Error(result.Result);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    ConsoleWriter.Error("An error occured!");
                }
            }

            ConsoleWriter.Info("Press any key to continue");
            Console.ReadKey();
        }

        public static string otebatTvoyuSestru(string sestra)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(sestra);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string otebatTvoyuSestru_backup(string mama)
        {
            var base64EncodedBytes = Convert.FromBase64String(mama);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
