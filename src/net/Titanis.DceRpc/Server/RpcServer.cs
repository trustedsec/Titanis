using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc.Communication;
using Titanis.DceRpc.WireProtocol;

namespace Titanis.DceRpc.Server
{
	/// <summary>
	/// Implements an RPC server.
	/// </summary>
	// TODO: Add endpoint functionality
	public class RpcServer : Runnable
	{
		public RpcServer(IAuthServer authServer)
		{
			this.AddEncoding(RpcEncoding.MsrpcNdr);
			this.AddEncoding(RpcEncoding.MsrpcNdr64);
			this._authServer = authServer;
		}

		private List<RpcServerChannel> _activeChannels = new List<RpcServerChannel>();

		private void OnChannelClosed(RpcServerChannel channel)
		{
			lock (this._activeChannels)
			{
				this._activeChannels.Remove(channel);
			}
		}

		private List<RpcEndpoint> _endpoints = new List<RpcEndpoint>();
		public void AddEndpoint(RpcEndpoint ep)
		{
			ArgumentNullException.ThrowIfNull(ep);

			ep.OnBinding(this);
			this._endpoints.Add(ep);
		}

		#region Bindings
		private Dictionary<RpcInterfaceKey, RpcServiceStub> _services = new Dictionary<RpcInterfaceKey, RpcServiceStub>();
		public void AddService(RpcServiceStub binding)
		{
			if (binding is null)
				throw new ArgumentNullException(nameof(binding));

			RpcInterfaceKey key = new RpcInterfaceKey(new SyntaxId(binding.InterfaceUuid, binding.InterfaceVersion));
			this._services.Add(key, binding);
		}

		private Dictionary<uint, RpcAssocGroup> _assocGroups = new Dictionary<uint, RpcAssocGroup>();
		private long _lastAssocGroupId;
		internal RpcAssocGroup GetOrCreateAssocGroup(uint id)
		{
			if (id != 0)
			{
				// TODO: Throw on unknown group ID
				return this._assocGroups[id];
			}
			else
			{
				var groupId = (uint)Interlocked.Increment(ref this._lastAssocGroupId);
				RpcAssocGroup group = new RpcAssocGroup(groupId);
				lock (this._assocGroups)
					this._assocGroups.Add(groupId, group);
				return group;
			}
		}

		internal RpcServiceStub TryGetService(SyntaxId syntaxId)
		{
			RpcInterfaceKey key = new RpcInterfaceKey(syntaxId);
			var binding = this._services.TryGetValue(key);
			return binding;
		}
		#endregion
		#region Encodings
		private Dictionary<RpcInterfaceKey, RpcEncoding> _encodings = new Dictionary<RpcInterfaceKey, RpcEncoding>();
		private readonly IAuthServer _authServer;

		internal RpcEncoding TryGetEncoding(SyntaxId syntaxId)
		{
			return this._encodings.TryGetValue(new RpcInterfaceKey(syntaxId));
		}
		internal void AddEncoding(RpcEncoding encoding)
		{
			if (encoding is null)
				throw new ArgumentNullException(nameof(encoding));

			lock (this._encodings)
				this._encodings.Add(new RpcInterfaceKey(new SyntaxId(encoding.InterfaceUuid, encoding.InterfaceVersion)), encoding);
		}
		#endregion

		protected override async Task OnStarting(CancellationToken cancellationToken)
		{
			if (this._endpoints.Count == 0)
				throw new InvalidOperationException("There are no endpoints registered.");

			foreach (var ep in this._endpoints)
			{
				await ep.Start().ConfigureAwait(false);
			}

			// TODO: Handle partial startup failures

			await base.OnStarting(cancellationToken).ConfigureAwait(false);
		}

		protected override Task Run(CancellationToken cancellationToken)
		{
			return base.Run(cancellationToken);
		}

		internal void OnClientConnected(Socket client)
		{
			var stream = new NetworkStream(client, true);
			var transport = new RpcStreamTransport(stream, RpcChannel.WindowsDefaultMaxFragCO);
			var channel = new RpcServerChannel(this, new RpcChannelParams(
				transport,
				Timeout.InfiniteTimeSpan
				), this._authServer);
			_ = channel.Start();

			lock (this._activeChannels)
			{
				this._activeChannels.Add(channel);
			}
		}
	}
}
