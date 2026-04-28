using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanis.Linterop.Fuse
{
	internal partial class FuseSession
	{
		internal FuseSession(FuseSessionHandle handle)
		{
			this._handle = handle;
		}

		private readonly FuseSessionHandle _handle;
		private string _mountpoint;

		internal void Mount(string mountpoint)
		{
			this._mountpoint = mountpoint;
			var res = FuseNativeMethods.fuse_session_mount(this._handle, mountpoint);
			if (res != 0)
				FuseHelper.ThrowLastError();
		}

		internal int RunLoop(CancellationToken cancellationToken)
		{
			using (var reg = cancellationToken.Register(this.ExitSession))
			{
				int res = FuseNativeMethods.fuse_session_loop(this._handle);
				return res;
			}
		}

		public void ExitSession()
		{
			if (!(this._handle?.IsInvalid ?? true))
			{
				FuseNativeMethods.fuse_session_exit(this._handle);
				Task.Factory.StartNew(async () =>
				{
					try
					{
						// Kick the session loop
						Directory.GetFiles(this._mountpoint);
					}
					catch { }
				});
			}
		}
	}

	partial class FuseSession : IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this._handle.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
