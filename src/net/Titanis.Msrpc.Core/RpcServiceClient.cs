using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;

namespace Titanis.Msrpc
{
	public abstract class RpcServiceClient : IDisposable
	{
		private protected RpcClientChannel _ownedChannel;
		private bool isDisposed;

		protected RpcServiceClient()
		{
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!isDisposed)
			{
				if (disposing)
				{
					this._ownedChannel?.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				isDisposed = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~MsrpcServiceClient()
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
	}

	public abstract class MsrpcServiceClient<TProxy> : RpcServiceClient
		where TProxy : RpcClientProxy, new()
	{
		protected TProxy _proxy;

		public MsrpcServiceClient()
		{
			this._proxy = new TProxy();
		}

		protected const string LocalName = ".";
		protected static readonly RpcPointer<string> LocalNamePtr = new DceRpc.RpcPointer<string>(LocalName);

		public const int ERROR_MORE_DATA = 0xEA;

		protected static Exception GetExceptionForCode(uint err) => GetExceptionForCode((int)err);
		protected static Exception GetExceptionForCode(int err)
		{
			return new Win32Exception(err);
		}

		public async Task Connect(RpcClientChannel channel, bool ownsChannel)
		{
			if (channel is null)
				throw new ArgumentNullException(nameof(channel));
			if (this._proxy != null)
				throw new InvalidOperationException(Messages.MsrpcClient_AlreadyConnected);

			TProxy proxy = new TProxy();
			await proxy.BindTo(channel);
			this._proxy = proxy;

			if (ownsChannel)
				this._ownedChannel = channel;
		}

		public static RpcPointer<string> StringToPointer(string str)
		{
			return (str != null) ? new RpcPointer<string>(str) : null;
		}
	}
}
