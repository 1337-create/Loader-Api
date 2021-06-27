using System;

namespace Anubis.Server
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

}
