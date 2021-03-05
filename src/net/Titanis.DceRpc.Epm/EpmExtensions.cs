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
		public static async Task<IPEndPoint?> TryMapViaTcp(
			this RpcClient client,
			RpcInterfaceId abstractSyntaxId,
			string remoteHost,
			CancellationToken cancellationToken,
			ISocketService? socketService = null,
			INameResolverService? nameResolver = null,
			ILog? log = null
			)
		{
			nameResolver ??= new PlatformNameResolverService(log: log);

			var addr = await nameResolver.ResolveAsync(remoteHost, cancellationToken).ConfigureAwait(false);
			return await TryMapViaTcp(client, abstractSyntaxId, addr[0], cancellationToken, socketService).ConfigureAwait(false);
		}

		public static async Task<IPEndPoint?> TryMapViaTcp(
			this RpcClient client,
			RpcInterfaceId abstractSyntaxId,
			IPAddress addr,
			CancellationToken cancellationToken,
			ISocketService? socketService = null
			)
		{
			socketService ??= Singleton.SingleInstance<PlatformSocketService>();
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
							IPEndPoint? serviceEP = await epm.TryMapTcp(abstractSyntaxId, addr, cancellationToken).ConfigureAwait(false);
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

		//public static async Task<IPEndPoint> TryMapViaNamedPipe<TProxy>(this RpcClient client, TProxy proxy, string remoteHost)
		//	where TProxy : RpcClientProxy
		//{
		//	var addr = await ResolveIpv4Address(remoteHost);
		//	using (NamedPipeClientStream pipe = new NamedPipeClientStream(remoteHost, "epmapper", PipeDirection.InOut, PipeOptions.Asynchronous))
		//	{
		//		await pipe.ConnectAsync();
		//		pipe.ReadMode = PipeTransmissionMode.Message;
		//		using (RpcClientChannel channel = client.Connect(pipe))
		//		{
		//			EpmClient epm = new EpmClient(channel);
		//			await epm.Connect();
		//			IPEndPoint serviceEP = await epm.MapTcp(proxy.AbstractSyntaxId, addr);
		//			return serviceEP;
		//		}
		//	}
		//}

		public static async Task<TClient> Map<TClient>(
			this RpcClient client,
			string remoteHost,
			CancellationToken cancelTlationoken,
			AuthClientContext? authContext = null,
			ISocketService? socketService = null
			)
			where TClient : RpcServiceClient, new()
		{
			TClient svc = new TClient();
			// TODO: Pass EPM auth context
			IPEndPoint? serviceEP = await client.TryMapViaTcp(svc.Proxy.AbstractSyntaxId, remoteHost, cancelTlationoken, socketService).ConfigureAwait(false);

			if (serviceEP == null)
				throw new InvalidOperationException(Epm.Messages.Epm_NoEndpoint);

			await client.ConnectTcp(svc, serviceEP, cancelTlationoken, authContext).ConfigureAwait(false);
			return svc;
		}
	}
}
