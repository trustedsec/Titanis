using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.Net;
using Titanis.Security;
using Titanis.Smb2;

namespace Titanis.Msrpc
{
	/// <summary>
	/// Implements extensions for using RPC over named pipes.
	/// </summary>
	public static class SmbRpcExtensions
	{
		/// <summary>
		/// Connects an RPC channel over a named pipe.
		/// </summary>
		/// <param name="service">Unbound RPC service client</param>
		/// <param name="smb2Client">SMB2 client</param>
		/// <param name="pipeUncPath">UNC path to the pipe</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static async Task ConnectPipe(
			this RpcClient rpcClient,
			RpcServiceClient service,
			Smb2Client smb2Client,
			UncPath pipeUncPath,
			CancellationToken cancellationToken
			)
		{
			ArgumentNullException.ThrowIfNull(rpcClient);
			ArgumentNullException.ThrowIfNull(service);
			ArgumentNullException.ThrowIfNull(smb2Client);
			ArgumentNullException.ThrowIfNull(pipeUncPath);

			if (service.IsBound)
				throw new ArgumentException(@"The service client is already bound and cannot be bound again.", nameof(service));

			var proxy = service.Proxy;

			Smb2Pipe? pipe = null;
			try
			{
				pipe = await smb2Client.OpenPipeAsync(pipeUncPath, cancellationToken).ConfigureAwait(false);
				var stream = pipe.GetStream(true);
				pipe = null;

				try
				{
					var spn = service.GetSpnFor(pipeUncPath.ServerName);
					await rpcClient.BindProxyToStream(proxy, spn, RpcAuthLevel.ConfiguredDefault, stream, cancellationToken).ConfigureAwait(false);
					stream = null;
				}
				finally
				{
					stream?.Dispose();
				}
			}
			finally
			{
				pipe?.Dispose();
			}
		}
	}
}
