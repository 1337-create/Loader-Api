using System;
using System.Runtime.InteropServices;

namespace Anubis.Win32.Loader.Environment.Statics
{
    public static class Nt
    {
        #region Import Enums
        public enum ProcessPrivileges : int
        {
            CreateThread = 0x0002,
            QueryInformation = 0x0400,
            MemoryOperate = 0x0008,
            MemoryWrite = 0x0020,
            MemoryRead = 0x0010,
            AllAccess = CreateThread | QueryInformation | MemoryOperate | MemoryWrite | MemoryRead,
        }

        public enum AllocationType : uint
        {
            MemoryCommit = 0x00001000,
            MemoryReserve = 0x00002000,
        }

        public enum AllocationProtect : uint
        {
            PageReadWrite = 0x00000004
        }
        #endregion

        #region Imports
        [DllImport( "kernel32.dll" )]
        public static extern IntPtr OpenProcess( int desired_access, bool inherited_handle, int process_id );

        [DllImport( "kernel32.dll", CharSet = CharSet.Auto )]
        public static extern IntPtr GetModuleHandle( string module_name );

        [DllImport( "kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true )]
        public static extern IntPtr GetProcAddress( IntPtr module, string procedure_name );

        [DllImport( "kernel32.dll", SetLastError = true, ExactSpelling = true )]
        public static extern IntPtr VirtualAllocEx( IntPtr process_handle, IntPtr address, uint size, uint allocation_type, uint protect );

        [DllImport( "kernel32.dll", SetLastError = true )]
        public static extern bool WriteProcessMemory( IntPtr process_handle, IntPtr destination, byte[] buffer, uint size, out UIntPtr written_bytes );

        [DllImport( "kernel32.dll" )]
        public static extern IntPtr CreateRemoteThread( IntPtr process_handle, IntPtr thread_attributes, uint stack_size, IntPtr start_address, IntPtr parameters, uint creation_flags, IntPtr thread_id );
        #endregion
    }
}
