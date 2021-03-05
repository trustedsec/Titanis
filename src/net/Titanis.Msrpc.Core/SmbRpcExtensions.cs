using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
		/// <param name="pipeUncPath">UNC path to the pipe, of the form \\server\IPC$\pipe.</param>
		/// <param name="cancellationToken">Cancellation token that may be used to cancel the operation.</param>
		/// <param name="authContext">Authentication context</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public static async Task ConnectPipe(
			this RpcClient rpcClient,
			RpcServiceClient service,
			Smb2Client smb2Client,
			string pipeUncPath,
			CancellationToken cancellationToken,
			AuthClientContext? authContext = null
			)
		{
			ArgumentNullException.ThrowIfNull(rpcClient);
			ArgumentNullException.ThrowIfNull(service);
			if (service.IsBound)
				throw new ArgumentException(@"The service client is already bound and cannot be bound again.", nameof(service));
			ArgumentNullException.ThrowIfNull(smb2Client);
			if (string.IsNullOrEmpty(pipeUncPath)) throw new ArgumentException($"'{nameof(pipeUncPath)}' cannot be null or empty.", nameof(pipeUncPath));
			if (!pipeUncPath.StartsWith(@"\\"))
				throw new ArgumentException(@"The pipeUnc must be the full UNC path of the form \\<server>\IPC$\<pipe>", nameof(pipeUncPath));

			var proxy = service.Proxy;

			Smb2Pipe? pipe = null;
			try
			{
				pipe = await smb2Client.OpenPipeAsync(pipeUncPath, cancellationToken).ConfigureAwait(false);
				var stream = pipe.GetStream(true);
				pipe = null;
				try
				{
					RpcClientChannel? channel = null;
					try
					{
						channel = rpcClient.BindTo(stream);
						stream = null;

						await proxy.BindToAsync(channel, true, authContext, cancellationToken).ConfigureAwait(false);
						channel = null;
					}
					finally
					{
						channel?.Dispose();
					}
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
