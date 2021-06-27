namespace Anubis.Win32.Loader
{
    class Program
    {
        static void Main( string[] args )
        {
            LoaderContext.Build();
            LoaderContext.Execute();
        }
    }
}
