using System;
using System.Collections.Generic;
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
		public RpcServer()
		{
			this.AddEncoding(RpcEncoding.MsrpcNdr);
			this.AddEncoding(RpcEncoding.MsrpcNdr64);
		}

		private List<RpcServerChannel> _activeChannels = new List<RpcServerChannel>();

		private void OnChannelClosed(RpcServerChannel channel)
		{
			lock (this._activeChannels)
			{
				this._activeChannels.Remove(channel);
			}
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
	}
}
