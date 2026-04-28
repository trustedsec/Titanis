using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	/// <summary>
	/// Marshals a string array.
	/// </summary>
	class ArgvMarshal : CriticalFinalizerObject, IDisposable
	{
		internal ArgvMarshal(ReadOnlySpan<string> args)
		{
			if (args.Length == 0)
				throw new ArgumentNullException(nameof(args));

			IntPtr[] ptrs = new nint[args.Length + 1];
			for (int i = 0; i < args.Length; i++)
			{
				var str = args[i];
				if (str != null)
					ptrs[i] = Marshal.StringToCoTaskMemUTF8(str);
			}

			this.Argv = ptrs;
		}

		private bool disposedValue;

		public nint[] Argv { get; private set; }

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
				}

				if (this.Argv != null)
				{
					for (int i = 0; i < this.Argv.Length; i++)
					{
						var ptr = this.Argv[i];
						if (ptr != IntPtr.Zero)
						{
							this.Argv[i] = 0;
							Marshal.FreeCoTaskMem(ptr);
						}
					}
				}

				disposedValue = true;
			}
		}

		~ArgvMarshal()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
