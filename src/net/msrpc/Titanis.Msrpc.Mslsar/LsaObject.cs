using System;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc.Mslsar
{
	public class LsaObject : IDisposable
	{
		protected readonly LsaClient _lsaClient;
		protected readonly RpcContextHandle _handle;

		private protected LsaObject(LsaClient lsaClient, RpcContextHandle handle)
		{
			this._lsaClient = lsaClient;
			this._handle = handle;
		}

		private Task CloseAsync(CancellationToken cancellationToken)
			=> this._lsaClient.CloseAsync(this._handle, cancellationToken);

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

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_isDisposed = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~LsaObject()
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