using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;
using Titanis.Net;
using Titanis.Socks.Pdus;

namespace Titanis.Socks
{
	struct SocketInfo
	{
		internal AddressFamily addressFamily;
		internal SocketType socketType;
		internal ProtocolType protocolType;

		public SocketInfo(
			AddressFamily addressFamily,
			SocketType socketType,
			ProtocolType protocolType)
		{
			this.addressFamily = addressFamily;
			this.socketType = socketType;
			this.protocolType = protocolType;
		}
	}

	/// <summary>
	/// Implements a <see cref="ISocketService"/> as a SOCKS client.
	/// </summary>
	/// <remarks>
	/// Call <see cref="CreateSocket(AddressFamily, SocketType, ProtocolType)"/> to
	/// create a socket that connects through the upsteram SOCKS server.
	/// </remarks>
	public class Socks5Client : ISocketService
	{
		private readonly ISocketService _socketService;

		public Socks5Client(
			EndPoint serverEP,
			ISocketService? underlyingSocketService = null,
			ILog? log = null
			)
		{
			ArgumentNullException.ThrowIfNull(serverEP);

			this.ServerEP = serverEP;
			this._socketService = underlyingSocketService ?? new PlatformSocketService(null, log);
			this._serverAddrFamily = serverEP.AddressFamily;
			if (this._serverAddrFamily == AddressFamily.Unspecified)
				// TODO: How to specify this default?
				this._serverAddrFamily = AddressFamily.InterNetwork;
		}

		/// <summary>
		/// Gets the endpoint of the SOCKS server.
		/// </summary>
		public EndPoint ServerEP { get; }
		private AddressFamily _serverAddrFamily;

		/// <inheritdoc/>
		public ISocket CreateSocket(
			AddressFamily addressFamily,
			SocketType socketType,
			ProtocolType protocolType
			)
		{
			return new SocksSocket(this, new SocketInfo(addressFamily, socketType, protocolType));
		}

		/// <summary>
		/// Creates a socket on the underlying transport.
		/// </summary>
		internal async Task<(ISocket socket, EndPoint remoteBindEP)> ConnectSocksAsync(SocketInfo socketInfo, EndPoint remoteEP, CancellationToken cancellationToken)
		{
			var serverEP = this.ServerEP;
			var socket = this._socketService.CreateSocket(this._serverAddrFamily, SocketType.Stream, ProtocolType.Tcp);
			await socket.ConnectAsync(serverEP, cancellationToken).ConfigureAwait(false);

			ByteWriter writer = new ByteWriter();
			byte[] recvBuf = new byte[1024];

			// SOCKS5 init request
			await socket.SendAsync(new Pdus.InitRequestPdu
			{
				version = SocksVersion.Socks5,
				methodCount = 1,
				methods = new Pdus.AuthMethod[] { Pdus.AuthMethod.None },
			}.ToBytes(writer), SocketFlags.None, cancellationToken).ConfigureAwait(false);

			// TODO: Gracefully call receive multiple times
			int cbRes = await socket.ReceiveAsync(recvBuf, SocketFlags.None, cancellationToken).ConfigureAwait(false);
			ByteMemoryReader reader = new ByteMemoryReader(recvBuf.AsMemory(0, cbRes));
			var initResp = reader.ReadPduStruct<InitResponsePdu>();
			if (initResp.version is not 5)
				throw new NotSupportedException($"The remote endpoint is not a SOCKS server or is a version not supported by this implementation: {initResp.version}.");
			if (initResp.authMethod is AuthMethod.NoneAccepted)
				throw new NotSupportedException("The remote SOCKS server does not support any authentication methods offered by the client.");
			if (initResp.authMethod is not AuthMethod.None)
				throw new NotSupportedException($"The remote SOCKS server selected an authentication method not supported by the client. (method={initResp.authMethod})");

			var socksAddr = CreateSocksAddr(remoteEP, out int port);
			Debug.Assert(port <= ushort.MaxValue);

			await socket.SendAsync(new Socks5RequestPdu
			{
				version = SocksVersion.Socks5,
				command = Socks5Command.Connect,
				endpoint = new Socks5EndPoint(socksAddr, (ushort)port)
			}.ToBytes(writer), SocketFlags.None, cancellationToken).ConfigureAwait(false);
			cbRes = await socket.ReceiveAsync(recvBuf, SocketFlags.None, cancellationToken).ConfigureAwait(false);
			if (cbRes == 0)
			{
				throw new Exception($"Upstream SOCKS 5 server closed connection prematurely for {remoteEP}.  This may indicate that the host could not be resolved.");
			}
			reader = new ByteMemoryReader(recvBuf.AsMemory(0, cbRes));

			var resp = reader.ReadPduStruct<Socks5ResponsePdu>();
			if (resp.version is not SocksVersion.Socks5)
				throw new NotSupportedException("The remote endpoint does not support SOCKS 5.");
			if (resp.resultCode is not Socks5ResultCode.Success)
				throw new Socks5Exception(resp.resultCode);

			var remoteBindEP = resp.bindEP.ToSocketEndpoint();
			return (socket, remoteBindEP);
		}

		private Pdus.Socks5Address CreateSocksAddr(EndPoint remoteEP, out int port)
		{
			ArgumentNullException.ThrowIfNull(remoteEP);

			if (remoteEP is DnsEndPoint dnsEP)
			{
				port = dnsEP.Port;
				return new Pdus.Socks5DnsAddress(dnsEP.Host);
			}
			else if (remoteEP is IPEndPoint ipep)
			{
				port = ipep.Port;
				return ipep.Address.AddressFamily switch
				{
					AddressFamily.InterNetwork => new Pdus.Socks5Ipv4Address(ipep.Address),
					AddressFamily.InterNetworkV6 => new Pdus.Socks5Ipv6Address(ipep.Address),
					_ => throw new ArgumentException($"Remote endpoint specifies an unsupported address family: {ipep.Address.AddressFamily}", nameof(remoteEP))
				};
			}
			else
				throw new ArgumentException($"The type of remote endpoint is not supported: {remoteEP.GetType().FullName}", nameof(remoteEP));
		}
	}
}
