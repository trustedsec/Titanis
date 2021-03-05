using System;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mssamr
{
	public abstract class SamObject : IDisposable, IAsyncDisposable
	{
		protected readonly SamClient _samClient;
		protected readonly RpcContextHandle _handle;

		internal SamObject(SamClient samClient, RpcContextHandle handle)
		{
			this._samClient = samClient;
			this._handle = handle;
		}

		#region Dispose pattern
		private bool _isDisposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					_ = this.CloseAsync(CancellationToken.None);
				}

				_isDisposed = true;
			}
		}

		public async ValueTask DisposeAsync()
		{
			if (!_isDisposed)
			{
				await this.CloseAsync(CancellationToken.None).ConfigureAwait(false);

				_isDisposed = true;
			}
		}

		public async Task CloseAsync(CancellationToken cancellationToken)
		{
			if (!this._isDisposed)
			{
				await this._samClient.CloseHandle(this._handle, cancellationToken).ConfigureAwait(false);
				this._isDisposed = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~SamObject()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}