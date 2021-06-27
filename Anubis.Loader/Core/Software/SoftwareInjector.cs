using Ionic.Zip;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Anubis.Loader.Core.Software
{
    public class SoftwareInjector
    {
        public SoftwareDownloader m_Downloader;
        private string RandomFileName = "windowsTelemetryPackage__" + Guid.NewGuid().ToString() + ".zip";

        public SoftwareInjector(SoftwareDownloader downloader)
        {
            m_Downloader = downloader;
        }

        public bool IsReady()
        {
            if (m_Downloader == null)
                return false;

            return m_Downloader.Bind() && PrepareObjects();
        }

        private bool Extract(string key)
        {
            try
            {
                foreach(var process in Process.GetProcesses())
                {
                    if (process.ProcessName.Contains("loader"))
                        process.Kill();
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                File.Move(Environment.GetFolderPath(Environment.SpecialFolder.System) + "/" + RandomFileName, path + "/" + RandomFileName);

                File.SetAttributes(path + "/" + RandomFileName, FileAttributes.Hidden | FileAttributes.System);

                if (File.Exists(path + "/loader.exe"))
                {
                    File.Delete(path + "/loader.exe");
                }

                using (var zip = ZipFile.Read(path + "/" + RandomFileName))
                {
                    zip.Password = key;

                    foreach (var entry in zip.Entries)
                    {
                        entry.ExtractWithPassword(ExtractExistingFileAction.OverwriteSilently, key);
                    }
                }

                string move_path = "";
                //if (Directory.Exists("retail"))
                //{
                //    move_path = "retail\\";
                //}

                File.Move($"{move_path}loader.exe", Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.exe");

                //if (Directory.Exists("retail"))
                //    Directory.Delete("retail");

                File.SetAttributes(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.exe", FileAttributes.Hidden | FileAttributes.System);

                if (File.Exists("loader.exe"))
                {
                    File.Delete("loader.exe");
                }
                if (File.Exists("pubg.dll"))
                {
                    File.Delete("pubg.dll");
                }

                File.Delete(path + "/" + RandomFileName);

                return File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.exe");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public bool Execute(int seconds, string key)
        {
            if (!IsReady())
            {
                Console.WriteLine("Step 1");
                return false;
            }

            if (!Extract(key))
            {
                Console.WriteLine("Step 2");
                return false;
            }


            try
            {
                Console.WriteLine("Step 3");
                var file_name = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.dll";
                if(File.Exists(file_name))
                {
                    File.Delete(file_name);
                }

                Console.WriteLine("Step 4");

                File.Move(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "/loader.exe", file_name);

                Console.WriteLine("Step 5");

                ProcessStartInfo info = new ProcessStartInfo
                {
                    CreateNoWindow = false,
                    FileName = "notepad.exe",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    ErrorDialog = false
                };

                Console.WriteLine("Step 6");

                var process = Process.Start(info);
                var result = new EnvRunner().Setup(file_name, "notepad", process).Execute();

                return result;
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex);
            }

            return false;
        }

        private bool PrepareObjects()
        {
            string api_path = m_Downloader.AppendApi("retail.zip");
            return m_Downloader.Download(api_path, RandomFileName);
        }
    }
}
