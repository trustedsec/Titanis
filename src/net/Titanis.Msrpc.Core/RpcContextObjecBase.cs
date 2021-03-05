using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace Titanis.Msrpc
{
	/// <summary>
	/// Represents an RPC context object.
	/// </summary>
	/// <remarks>
	/// An RPC context object wraps a context handle.
	/// Implementers must inherit <see cref="RpcContextObjecBase{TClient}"/>
	/// must implement <see cref="CloseAsync(CancellationToken)"/> to close
	/// the context handle.
	/// </remarks>
	public abstract class RpcContextObjecBase : IDisposable, IAsyncDisposable
	{
		/// <summary>
		/// Initializes a new <see cref="RpcContextObjecBase"/>.
		/// </summary>
		/// <param name="handle">Context handle</param>
		private protected RpcContextObjecBase(RpcContextHandle handle)
		{
			this.handle = handle;
		}

		protected readonly RpcContextHandle handle;

		/// <summary>
		/// Closes the handle.
		/// </summary>
		/// <param name="cancellationToken">Cancellation token that make be used to cancel the operation</param>
		protected abstract Task CloseAsync(CancellationToken cancellationToken);

		#region Dispose pattern
		/// <summary>
		/// Gets a value indicating whether the object has been disposed.
		/// </summary>
		public bool IsDisposed { get; private set; }

		/// <inheritdoc/>
		protected virtual void Dispose(bool disposing)
		{
			if (!IsDisposed)
			{
				if (disposing)
				{
					_ = this.CloseAsync(CancellationToken.None);
				}

				IsDisposed = true;
			}
		}

		/// <inheritdoc/>
		public async ValueTask DisposeAsync()
			=> await this.CloseAsync(CancellationToken.None).ConfigureAwait(false);

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}

	/// <summary>
	/// Represents an RPC context object.
	/// </summary>
	/// <remarks>
	/// An RPC context object wraps a context handle.  Implementers
	/// must implement <see cref="RpcContextObjecBase.CloseAsync(CancellationToken)"/> to close
	/// the context handle.
	/// </remarks>
	public abstract class RpcContextObjecBase<TClient> : RpcContextObjecBase
	{
		/// <summary>
		/// Initializes a new <see cref="RpcContextObjecBase"/>.
		/// </summary>
		/// <param name="handle">Context handle</param>
		/// <param name="client">Client object used to service the object</param>
		protected RpcContextObjecBase(RpcContextHandle handle, TClient client)
			: base(handle)
		{
			this.client = client;
		}

		/// <summary>
		/// Client object used to service the object
		/// </summary>
		protected readonly TClient client;
	}
}
