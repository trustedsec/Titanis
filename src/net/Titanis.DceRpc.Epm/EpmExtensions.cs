using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.Epm;
using Titanis.IO;
using Titanis.Net;
using Titanis.Security;

namespace Titanis.DceRpc.Client
{
	public static class EpmExtensions
	{
		public static async Task<TClient> Map<TClient>(
			this RpcClient client,
			string remoteHost,
			CancellationToken cancelTlationToken
			)
			where TClient : RpcServiceClient, new()
		{
			TClient svc = new TClient();
			// TODO: Pass EPM auth context
			IPEndPoint? serviceEP = await client.TryMapViaTcp(svc.Proxy.AbstractSyntaxId, remoteHost, cancelTlationToken).ConfigureAwait(false);

			if (serviceEP == null)
				throw new InvalidOperationException(Epm.Messages.Epm_NoEndpoint);

			var spn = svc.GetSpnFor(remoteHost);
			await client.ConnectTcp(svc, serviceEP, spn, cancelTlationToken).ConfigureAwait(false);
			return svc;
		}

		public static async Task<IPEndPoint?> TryMapViaTcp(
			this RpcClient client,
			RpcInterfaceId abstractSyntaxId,
			string remoteHost,
			CancellationToken cancellationToken
			)
		{
			var nameResolver = client._resolver ?? new PlatformNameResolverService(log: client._log);

			var addr = await nameResolver.ResolveAsync(remoteHost, cancellationToken).ConfigureAwait(false);
			return await TryMapViaTcp(client, abstractSyntaxId, addr[0], cancellationToken).ConfigureAwait(false);
		}

		public static async Task<IPEndPoint?> TryMapViaTcp(
			this RpcClient client,
			RpcInterfaceId abstractSyntaxId,
			IPAddress addr,
			CancellationToken cancellationToken
			)
		{
			var socketService = client._socketService;
			var epmEP = new IPEndPoint(addr, EpmClient.EPMapperPort);
			ISocket? s = null;
			try
			{
				s = socketService.CreateTcpSocket(addr.AddressFamily);
				await s.ConnectAsync(epmEP, cancellationToken).ConfigureAwait(false);
				Stream? ns = null;
				try
				{
					ns = s.GetStream(true);
					s = null;

					RpcClientChannel? channel = null;
					try
					{
						channel = client.BindTo(ns);
						ns = null;
						using (EpmClient epm = new EpmClient())
						{
							// TODO: Enable EPM auth context
							await epm.BindToAsync(channel, true, cancellationToken).ConfigureAwait(false);
							channel = null;
							IPEndPoint? serviceEP = await epm.TryMapTcp(abstractSyntaxId, cancellationToken).ConfigureAwait(false);
							return serviceEP;
						}
					}
					finally
					{
						channel?.Dispose();
					}
				}
				finally
				{
					ns?.Dispose();
				}
			}
			finally
			{
				s?.Dispose();
			}
		}
	}
}
