using dceidl;
using epm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;
using Titanis.DceRpc.Client;
using Titanis.DceRpc.Communication;
using Titanis.IO;
using Titanis.Security;

namespace Titanis.DceRpc.Epm
{
	public partial class EpmClient : RpcServiceClient<eptClientProxy>, IEndpointMapper
	{
		public const int EPMapperPort = 135;
		public const string EPMapperPipeName = "epmapper";

		public EpmClient()
		{
		}

		/// <inheritdoc/>
		public sealed override string? ServiceClass => ServiceClassNames.Rpc;
		// [MS-RPCE] § 2.1.1.2
		/// <inheritdoc/>
		public sealed override string? WellKnownPipeName => EPMapperPipeName;
		// [MS-RPCE] § 2.1.1.1
		/// <inheritdoc/>
		public sealed override int WellKnownTcpPort => EPMapperPort;
		// Observed in PCAP
		/// <inheritdoc/>
		public sealed override bool SupportsNdr64 => true;

		public Task<IPEndPoint?> TryMapTcp(
			RpcInterfaceId interfaceId,
			CancellationToken cancellationToken)
		{
			return this.TryMapIpv4(interfaceId, ProtocolId.Tcp4, IPAddress.None, cancellationToken);
		}
		public Task<IPEndPoint?> TryMapTcp(
			RpcInterfaceId interfaceId,
			IPAddress? hostAddress,
			CancellationToken cancellationToken)
		{
			return this.TryMapIpv4(interfaceId, ProtocolId.Tcp4, hostAddress ?? IPAddress.None, cancellationToken);
		}
		public Task<IPEndPoint?> TryMapUdp(
			RpcInterfaceId interfaceId,
			CancellationToken cancellationToken)
		{
			return this.TryMapIpv4(interfaceId, ProtocolId.Udp4, IPAddress.None, cancellationToken);
		}
		public Task<IPEndPoint?> TryMapUdp(
			RpcInterfaceId interfaceId,
			IPAddress? hostAddress,
			CancellationToken cancellationToken)
		{
			return this.TryMapIpv4(interfaceId, ProtocolId.Udp4, hostAddress ?? IPAddress.None, cancellationToken);
		}
		internal async Task<IPEndPoint?> TryMapIpv4(
			RpcInterfaceId interfaceId,
			ProtocolId protocol,
			IPAddress addr,
			CancellationToken cancellationToken)
		{
			RpcPointer<twr_t> pTower = new RpcPointer<twr_t>(Tower.EncodeIpv4(
				interfaceId,
				RpcEncoding.MsrpcSyntaxId,
				protocol,
				new System.Net.IPEndPoint(addr, 135)
				)._twr);
			var towers = await MapTower(pTower, cancellationToken).ConfigureAwait(false);

			foreach (var tower in towers)
			{
				var ep = tower?.TryExtractIPEndpoint();
				if (ep != null)
					return ep;
			}
			return null;
		}

		// [MS-RPCE] § 2.2.1.2.4 ept-Lookup Method
		enum InquiryType : uint
		{
			AllElements = 0,
			MatchByInterface = 1,
			MatchByObject = 2,
			MatchByBoth = 3
		}

		public Task<IList<EndpointEntry>> LookupByAll(int pageSize, CancellationToken cancellationToken)
			=> this.Lookup(pageSize, null, null, InquiryVersionOptions.All, new RpcVersion(), cancellationToken);
		public async Task<IList<EndpointEntry>> Lookup(int pageSize, Guid? objectId, Guid? interfaceId, InquiryVersionOptions versionMatchOptions, RpcVersion version, CancellationToken cancellationToken)
		{
			if (pageSize < 1)
				throw new ArgumentException("Page size must be > 0.", nameof(pageSize));
			RpcPointer<ArraySegment<ept_entry_t>> pEntries = new(new ArraySegment<ept_entry_t>(new ept_entry_t[pageSize]));
			RpcPointer<uint> num_ents = new();
			RpcPointer<int> status = new();
			RpcPointer<RpcContextHandle> entry_handle = new(new RpcContextHandle());
			var ptrObjId = objectId.HasValue ? new RpcPointer<Guid>(objectId.Value) : null;
			var ptrInterfaceId = (interfaceId.HasValue) ? new RpcPointer<RpcInterfaceId>(new RpcInterfaceId(interfaceId.Value, version)) : null;

			InquiryType inqType = 0;
			if (ptrObjId != null)
				inqType |= InquiryType.MatchByObject;
			if (ptrInterfaceId != null)
				inqType |= InquiryType.MatchByInterface;

			List<EndpointEntry> entries = new List<EndpointEntry>();
			do
			{
				await this._proxy.ept_lookup(
					(uint)inqType,
					ptrObjId,
					ptrInterfaceId,
					(uint)versionMatchOptions,
					entry_handle,
					(uint)pageSize,
					num_ents,
					pEntries,
					status,
					cancellationToken).ConfigureAwait(false);
				if (status.value == 0)
					entries.AddRange(pEntries.value.Select(r => new EndpointEntry(r)));
				else
					break;
			} while (status.value == 0 && !entry_handle.value.contextId.IsEmpty);

			if (!entry_handle.value.contextId.IsEmpty)
				await this._proxy.ept_lookup_handle_free(entry_handle, new RpcPointer<int>(), cancellationToken).ConfigureAwait(false);

			return entries;
		}

		public async Task<IPEndPoint?> MapNamedPipe(RpcInterfaceId interfaceId, CancellationToken cancellationToken)
		{
			RpcPointer<twr_t> pTower = new RpcPointer<twr_t>(Tower.EncodeNamedPipe(
				interfaceId,
				RpcEncoding.MsrpcSyntaxId,
				null
				)._twr);
			var towers = await MapTower(pTower, cancellationToken).ConfigureAwait(false);

			foreach (var tower in towers)
			{
				var ep = tower.TryExtractIPEndpoint();
				if (ep != null)
					return ep;
			}
			return null;
		}

		private async Task<Tower[]> MapTower(RpcPointer<twr_t> pTower, CancellationToken cancellationToken)
		{
			RpcPointer<uint> pNumTowers = new RpcPointer<uint>();
			RpcPointer<ArraySegment<RpcPointer<twr_t>>> pTowers = new RpcPointer<ArraySegment<RpcPointer<twr_t>>>();
			RpcPointer<int> pStatus = new RpcPointer<int>();
			await this._proxy.ept_map(
				new RpcPointer<Guid>(),
				pTower,
				new RpcPointer<RpcContextHandle>(new RpcContextHandle()),
				4, /* This value is used by MSRPC */
				pNumTowers,
				pTowers,
				pStatus,
				cancellationToken
				).ConfigureAwait(false);
			var towers = pTowers.value;
			return towers.Select(r => new Tower(r.value)).ToArray();
		}
	}

	// [MS-RPCE] § 2.2.1.2.4 - ept_lookup Method
	public enum InquiryVersionOptions : uint
	{
		All = 1,
		Compatible = 2,
		Exact = 3,
		MajorOnly = 4,
		UpTo = 5
	}

	public sealed class EndpointEntry
	{
		internal EndpointEntry(ref readonly ept_entry_t entry)
		{
			this._entry = entry;
		}

		private readonly ept_entry_t _entry;

		public sealed override string ToString()
		{
			return $"Tower: {{{this.Tower}}}; {(this.ObjectGuid != Guid.Empty ? $"Object ID: {this.ObjectGuid}; " : null)}Annotation: {this.annotation}";
		}

		public Guid ObjectGuid => this._entry.@object;
		public string? annotation => this._entry.annotation.AsUtf8String();
		public Tower Tower => new Tower(this._entry.tower.value);
	}
}
