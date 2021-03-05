using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Msefsr
{
	public partial class EfsOpenFile
	{
		private readonly RpcContextHandle handle;
		private readonly EfsClient _efs;

		internal EfsOpenFile(RpcContextHandle handle, EfsClient efs)
		{
			this.handle = handle;
			this._efs = efs;
		}
	}
	partial class EfsOpenFile : IAsyncDisposable
	{
		async ValueTask IAsyncDisposable.DisposeAsync()
		{
			await this._efs.CloseFile(this.handle, CancellationToken.None).ConfigureAwait(false);
		}
	}
	partial class EfsOpenFile : IDisposable
	{
		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					_ = this._efs.CloseFile(this.handle, CancellationToken.None);
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
