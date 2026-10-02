namespace ms_scmp
{
	using ms_dcom;
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	// -----------------------------------------------------------------------
	// Enumerations
	// -----------------------------------------------------------------------
	//
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum VSS_OBJECT_TYPE : short
	{
		VSS_OBJECT_UNKNOWN = 0,
		VSS_OBJECT_NONE = 1,
		VSS_OBJECT_SNAPSHOT_SET = 2,
		VSS_OBJECT_SNAPSHOT = 3,
		VSS_OBJECT_PROVIDER = 4,
		VSS_OBJECT_TYPE_COUNT = 5,
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum VSS_SNAPSHOT_STATE : short
	{
		VSS_SS_UNKNOWN = 0,
		VSS_SS_PREPARING = 1,
		VSS_SS_PROCESSING_PREPARE = 2,
		VSS_SS_PREPARED = 3,
		VSS_SS_PROCESSING_PRECOMMIT = 4,
		VSS_SS_PRECOMMITTED = 5,
		VSS_SS_PROCESSING_COMMIT = 6,
		VSS_SS_COMMITTED = 7,
		VSS_SS_PROCESSING_POSTCOMMIT = 8,
		VSS_SS_PROCESSING_PREFINALCOMMIT = 9,
		VSS_SS_PREFINALCOMMITTED = 10,
		VSS_SS_PROCESSING_POSTFINALCOMMIT = 11,
		VSS_SS_CREATED = 12,
		VSS_SS_ABORTED = 13,
		VSS_SS_DELETED = 14,
		VSS_SS_POSTCOMMITTED = 15,
		VSS_SS_COUNT = 16,
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum VSS_PROVIDER_TYPE : short
	{
		VSS_PROV_UNKNOWN = 0,
		VSS_PROV_SYSTEM = 1,
		VSS_PROV_SOFTWARE = 2,
		VSS_PROV_HARDWARE = 3,
		VSS_PROV_FILESHARE = 4,
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[Flags]
	public enum VSS_VOLUME_SNAPSHOT_ATTRIBUTES : int
	{
		VSS_VOLSNAP_ATTR_PERSISTENT = 0x00000001,
		VSS_VOLSNAP_ATTR_NO_AUTORECOVERY = 0x00000002,
		VSS_VOLSNAP_ATTR_CLIENT_ACCESSIBLE = 0x00000004,
		VSS_VOLSNAP_ATTR_NO_AUTO_RELEASE = 0x00000008,
		VSS_VOLSNAP_ATTR_NO_WRITERS = 0x00000010,
		VSS_VOLSNAP_ATTR_TRANSPORTABLE = 0x00000020,
		VSS_VOLSNAP_ATTR_NOT_SURFACED = 0x00000040,
		VSS_VOLSNAP_ATTR_NOT_TRANSACTED = 0x00000080,
		VSS_VOLSNAP_ATTR_HARDWARE_ASSISTED = 0x00010000,
		VSS_VOLSNAP_ATTR_DIFFERENTIAL = 0x00020000,
		VSS_VOLSNAP_ATTR_PLEX = 0x00040000,
		VSS_VOLSNAP_ATTR_IMPORTED = 0x00080000,
		VSS_VOLSNAP_ATTR_EXPOSED_LOCALLY = 0x00100000,
		VSS_VOLSNAP_ATTR_EXPOSED_REMOTELY = 0x00200000,
		VSS_VOLSNAP_ATTR_AUTORECOVER = 0x00400000,
		VSS_VOLSNAP_ATTR_ROLLBACK_RECOVERY = 0x00800000,
		VSS_VOLSNAP_ATTR_DELAYED_POSTSNAPSHOT = 0x01000000,
		VSS_VOLSNAP_ATTR_TXF_RECOVERY = 0x02000000,
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public enum VSS_SNAPSHOT_CONTEXT : long
	{
		VSS_CTX_BACKUP = 0x00000000,
		VSS_CTX_FILE_SHARE_BACKUP = VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_WRITERS,
		VSS_CTX_NAS_ROLLBACK = VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_PERSISTENT | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_AUTO_RELEASE | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_WRITERS,
		VSS_CTX_APP_ROLLBACK = VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_PERSISTENT | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_AUTO_RELEASE,
		VSS_CTX_CLIENT_ACCESSIBLE = VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_PERSISTENT | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_CLIENT_ACCESSIBLE | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_AUTO_RELEASE | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_WRITERS,
		VSS_CTX_CLIENT_ACCESSIBLE_WRITERS = VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_PERSISTENT | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_CLIENT_ACCESSIBLE | VSS_VOLUME_SNAPSHOT_ATTRIBUTES.VSS_VOLSNAP_ATTR_NO_AUTO_RELEASE,
		VSS_CTX_ALL = 0xFFFFFFFF
	}

	// -----------------------------------------------------------------------
	// Structures — union arms for VSS_OBJECT_PROP
	// -----------------------------------------------------------------------

	/// <summary>
	/// Properties of a shadow copy snapshot (VSS_OBJECT_SNAPSHOT arm of VSS_OBJECT_PROP).
	/// </summary>
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public struct VSS_SNAPSHOT_PROP : IRpcFixedStruct
	{
		public Guid m_SnapshotId;
		public Guid m_SnapshotSetId;
		public int m_lSnapshotsCount;
		public RpcPointer<string>? m_pwszSnapshotDeviceObject;
		public RpcPointer<string>? m_pwszOriginalVolumeName;
		public RpcPointer<string>? m_pwszOriginatingMachine;
		public RpcPointer<string>? m_pwszServiceMachine;
		public RpcPointer<string>? m_pwszExposedName;
		public RpcPointer<string>? m_pwszExposedPath;
		public Guid m_ProviderId;
		public VSS_VOLUME_SNAPSHOT_ATTRIBUTES m_lSnapshotAttributes;
		public long m_tsCreationTimestamp;
		public VSS_SNAPSHOT_STATE m_eStatus;

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(m_SnapshotId);
			encoder.WriteValue(m_SnapshotSetId);
			encoder.WriteValue(m_lSnapshotsCount);

			encoder.WritePointer(m_pwszSnapshotDeviceObject);
			encoder.WritePointer(m_pwszOriginalVolumeName);
			encoder.WritePointer(m_pwszOriginatingMachine);
			encoder.WritePointer(m_pwszServiceMachine);
			encoder.WritePointer(m_pwszExposedName);
			encoder.WritePointer(m_pwszExposedPath);

			encoder.WriteValue(m_ProviderId);
			encoder.WriteValue((int)m_lSnapshotAttributes);
			encoder.WriteValue(m_tsCreationTimestamp);
			encoder.WriteValue((short)m_eStatus);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			m_SnapshotId = decoder.ReadUuid();
			m_SnapshotSetId = decoder.ReadUuid();
			m_lSnapshotsCount = decoder.ReadInt32();
			m_pwszSnapshotDeviceObject = decoder.ReadUniquePointer<string>();
			m_pwszOriginalVolumeName = decoder.ReadUniquePointer<string>();
			m_pwszOriginatingMachine = decoder.ReadUniquePointer<string>();
			m_pwszServiceMachine = decoder.ReadUniquePointer<string>();
			m_pwszExposedName = decoder.ReadUniquePointer<string>();
			m_pwszExposedPath = decoder.ReadUniquePointer<string>();
			m_ProviderId = decoder.ReadUuid();
			m_lSnapshotAttributes = (VSS_VOLUME_SNAPSHOT_ATTRIBUTES)decoder.ReadInt32();
			m_tsCreationTimestamp = decoder.ReadInt64();
			m_eStatus = (VSS_SNAPSHOT_STATE)decoder.ReadInt16();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (m_pwszSnapshotDeviceObject is not null)
				encoder.WriteWideCharString(m_pwszSnapshotDeviceObject.value);
			if (m_pwszOriginalVolumeName is not null)
				encoder.WriteWideCharString(m_pwszOriginalVolumeName.value);
			if (m_pwszOriginatingMachine is not null)
				encoder.WriteWideCharString(m_pwszOriginatingMachine.value);
			if (m_pwszServiceMachine is not null)
				encoder.WriteWideCharString(m_pwszServiceMachine.value);
			if (m_pwszExposedName is not null)
				encoder.WriteWideCharString(m_pwszExposedName.value);
			if (m_pwszExposedPath is not null)
				encoder.WriteWideCharString(m_pwszExposedPath.value);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (m_pwszSnapshotDeviceObject is not null)
				m_pwszSnapshotDeviceObject.value = decoder.ReadWideCharString();
			if (m_pwszOriginalVolumeName is not null)
				m_pwszOriginalVolumeName.value = decoder.ReadWideCharString();
			if (m_pwszOriginatingMachine is not null)
				m_pwszOriginatingMachine.value = decoder.ReadWideCharString();
			if (m_pwszServiceMachine is not null)
				m_pwszServiceMachine.value = decoder.ReadWideCharString();
			if (m_pwszExposedName is not null)
				m_pwszExposedName.value = decoder.ReadWideCharString();
			if (m_pwszExposedPath is not null)
				m_pwszExposedPath.value = decoder.ReadWideCharString();
		}
	}

	/// <summary>
	/// Properties of a VSS provider (VSS_OBJECT_PROVIDER arm of VSS_OBJECT_PROP).
	/// </summary>
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public struct VSS_PROVIDER_PROP : IRpcFixedStruct
	{
		public Guid m_ProviderId;
		public RpcPointer<string>? m_pwszProviderName;
		public VSS_PROVIDER_TYPE m_eProviderType;
		public RpcPointer<string>? m_pwszProviderVersion;
		public Guid m_ProviderVersionId;
		public Guid m_ClassId;

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(m_ProviderId);
			encoder.WritePointer(m_pwszProviderName);
			encoder.WriteValue((short)m_eProviderType);
			encoder.WritePointer(m_pwszProviderVersion);
			encoder.WriteValue(m_ProviderVersionId);
			encoder.WriteValue(m_ClassId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			m_ProviderId = decoder.ReadUuid();
			m_pwszProviderName = decoder.ReadUniquePointer<string>();
			m_eProviderType = (VSS_PROVIDER_TYPE)decoder.ReadInt16();
			m_pwszProviderVersion = decoder.ReadUniquePointer<string>();
			m_ProviderVersionId = decoder.ReadUuid();
			m_ClassId = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (m_pwszProviderName is not null)
				encoder.WriteWideCharString(m_pwszProviderName.value);
			if (m_pwszProviderVersion is not null)
				encoder.WriteWideCharString(m_pwszProviderVersion.value);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (m_pwszProviderName is not null)
				m_pwszProviderName.value = decoder.ReadWideCharString();
			if (m_pwszProviderVersion is not null)
				m_pwszProviderVersion.value = decoder.ReadWideCharString();
		}
	}

	/// <summary>
	/// A VSS object property — a discriminated union keyed by <see cref="Type"/>.
	/// <list type="bullet">
	///   <item><see cref="VSS_OBJECT_TYPE.VSS_OBJECT_SNAPSHOT"/> — use <see cref="Snap"/>.</item>
	///   <item><see cref="VSS_OBJECT_TYPE.VSS_OBJECT_PROVIDER"/> — use <see cref="Prov"/>.</item>
	/// </list>
	/// </summary>
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public struct VSS_OBJECT_PROP : IRpcFixedStruct
	{
		public VSS_OBJECT_TYPE Type;
		public VSS_SNAPSHOT_PROP Snap;
		public VSS_PROVIDER_PROP Prov;

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue((short)Type);

			switch (Type)
			{
				case VSS_OBJECT_TYPE.VSS_OBJECT_SNAPSHOT:
					encoder.WriteFixedStruct(Snap, NdrAlignment._8Byte);
					break;
				case VSS_OBJECT_TYPE.VSS_OBJECT_PROVIDER:
					encoder.WriteFixedStruct(Prov, NdrAlignment._4Byte);
					break;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			Type = (VSS_OBJECT_TYPE)decoder.ReadInt16();

			switch (Type)
			{
				case VSS_OBJECT_TYPE.VSS_OBJECT_SNAPSHOT:
					Snap = decoder.ReadFixedStruct<VSS_SNAPSHOT_PROP>(NdrAlignment._8Byte);
					break;
				case VSS_OBJECT_TYPE.VSS_OBJECT_PROVIDER:
					Prov = decoder.ReadFixedStruct<VSS_PROVIDER_PROP>(NdrAlignment._4Byte);
					break;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch (Type)
			{
				case VSS_OBJECT_TYPE.VSS_OBJECT_SNAPSHOT:
					encoder.WriteStructDeferral(Snap);
					break;
				case VSS_OBJECT_TYPE.VSS_OBJECT_PROVIDER:
					encoder.WriteStructDeferral(Prov);
					break;
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch (Type)
			{
				case VSS_OBJECT_TYPE.VSS_OBJECT_SNAPSHOT:
					decoder.ReadStructDeferral<VSS_SNAPSHOT_PROP>(ref Snap);
					break;
				case VSS_OBJECT_TYPE.VSS_OBJECT_PROVIDER:
					decoder.ReadStructDeferral<VSS_PROVIDER_PROP>(ref Prov);
					break;
			}
		}
	}

	// -----------------------------------------------------------------------
	// IVssEnumObject — interface, client proxy
	// IID: AE1C7110-2F60-11D3-8A39-00C04F72D8E3
	// Opnums: 0-2 not used on wire (IUnknown), 3=Next, 4=Skip, 5=Reset, 6=Clone
	// -----------------------------------------------------------------------
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[GuidAttribute("AE1C7110-2F60-11D3-8A39-00C04F72D8E3")]
	[RpcVersionAttribute(0, 0)]
	public interface IVssEnumObject : IUnknown
	{
		/// <summary>Returns up to <paramref name="celt"/> objects into <paramref name="rgelt"/>.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Next(uint celt, RpcPointer<ArraySegment<VSS_OBJECT_PROP>> rgelt, RpcPointer<uint> pceltFetched, CancellationToken cancellationToken);

		/// <summary>Advances the enumerator by <paramref name="celt"/> positions.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Skip(uint celt, CancellationToken cancellationToken);

		/// <summary>Resets the enumerator to the beginning of the collection.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Reset(CancellationToken cancellationToken);

		/// <summary>Creates a copy of the enumerator at the same position.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Clone(RpcPointer<TypedObjref<IVssEnumObject>> ppenum, CancellationToken cancellationToken);
	}

	// -----------------------------------------------------------------------
	// Client proxy
	// -----------------------------------------------------------------------
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[IidAttribute("AE1C7110-2F60-11D3-8A39-00C04F72D8E3")]
	public class IVssEnumObjectClientProxy : IUnknownClientProxy, IVssEnumObject
	{
		public override Type InterfaceType => typeof(IVssEnumObject);
		private static Guid _interfaceUuid = new Guid("AE1C7110-2F60-11D3-8A39-00C04F72D8E3");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override RpcVersion InterfaceVersion => new RpcVersion(0, 0);

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Next(uint celt, RpcPointer<ArraySegment<VSS_OBJECT_PROP>> rgelt, RpcPointer<uint> pceltFetched, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;

			encoder.WriteValue(celt);

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			rgelt.value = decoder.ReadArraySegmentHeader<VSS_OBJECT_PROP>();

			for (int i = 0; i < rgelt.value.Count; i++)
			{
				VSS_OBJECT_PROP elem_0 = rgelt.value.Item(i);
				elem_0 = decoder.ReadFixedStruct<VSS_OBJECT_PROP>(NdrAlignment._8Byte);
				rgelt.value.Item(i) = elem_0;
			}

			for (int i = 0; i < rgelt.value.Count; i++)
			{
				VSS_OBJECT_PROP elem_0 = rgelt.value.Item(i);
				decoder.ReadStructDeferral<VSS_OBJECT_PROP>(ref elem_0);
				rgelt.value.Item(i) = elem_0;
			}

			pceltFetched.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Skip(uint celt, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;

			encoder.WriteValue(celt);

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Reset(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Clone(RpcPointer<TypedObjref<IVssEnumObject>> ppenum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppenum.value = decoder.ReadInterfacePointer<IVssEnumObject>();
			decoder.ReadInterfacePointer(ppenum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}

	// -----------------------------------------------------------------------
	// IVssAsync — interface, client proxy
	// IID: 507C37B7-2E4E-11D3-B0A7-00105A1B54C6
	// Opnums: 0-2 not used on wire (IUnknown), 3=Cancel, 4=Wait, 5=QueryStatus
	// -----------------------------------------------------------------------
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[GuidAttribute("507C37B7-2E4E-11D3-B0A7-00105A1B54C6")]
	[RpcVersionAttribute(0, 0)]
	public interface IVssAsync : IUnknown
	{
		/// <summary>Cancels an incomplete asynchronous operation.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Cancel(CancellationToken cancellationToken);

		/// <summary>Waits until an incomplete asynchronous operation finishes.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> Wait(uint dwMilliseconds, CancellationToken cancellationToken);

		/// <summary>Queries the status of an asynchronous operation.</summary>
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> QueryStatus(RpcPointer<int> pHrResult, RpcPointer<uint> pReserved, CancellationToken cancellationToken);
	}

	// -----------------------------------------------------------------------
	// Client proxy
	// -----------------------------------------------------------------------
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[IidAttribute("507C37B7-2E4E-11D3-B0A7-00105A1B54C6")]
	public class IVssAsyncClientProxy : IUnknownClientProxy, IVssAsync
	{
		public override Type InterfaceType => typeof(IVssAsync);
		private static Guid _interfaceUuid = new Guid("507C37B7-2E4E-11D3-B0A7-00105A1B54C6");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override RpcVersion InterfaceVersion => new RpcVersion(0, 0);

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Cancel(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> Wait(uint dwMilliseconds, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(dwMilliseconds);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> QueryStatus(RpcPointer<int> pHrResult, RpcPointer<uint> pReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pHrResult.value = decoder.ReadInt32();
			pReserved.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}

	// -----------------------------------------------------------------------
	// IVssAdmin — interface, client proxy
	// IID:   77ED5996-2F63-11D3-8A39-00C04F72D8E3
	// Opnums: 0-2 not used on wire (IUnknown)
	//         3 = RegisterProvider
	//         4 = UnregisterProvider
	//         5 = QueryProviders
	//         6 = AbortAllSnapshotsInProgress
	// -----------------------------------------------------------------------
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[GuidAttribute("77ED5996-2F63-11D3-8A39-00C04F72D8E3")]
	[RpcVersionAttribute(0, 0)]
	public interface IVssAdmin : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> RegisterProvider(Guid pProviderId, Guid ClassId, string pwszProviderName, VSS_PROVIDER_TYPE eProviderType, string pwszProviderVersion, Guid ProviderVersionId, CancellationToken cancellationToken);
	
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> UnregisterProvider(Guid ProviderId, CancellationToken cancellationToken);
	
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> QueryProviders(RpcPointer<TypedObjref<IVssEnumObject>> ppEnum, CancellationToken cancellationToken);
	
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> AbortAllSnapshotsInProgress(CancellationToken cancellationToken);
	}
	
	// -----------------------------------------------------------------------
	// Client proxy
	// -----------------------------------------------------------------------
	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	[IidAttribute("77ED5996-2F63-11D3-8A39-00C04F72D8E3")]
	public class IVssAdminClientProxy : IUnknownClientProxy, IVssAdmin
	{
		public override Type InterfaceType => typeof(IVssAdmin);
		private static Guid _interfaceUuid = new Guid("77ED5996-2F63-11D3-8A39-00C04F72D8E3");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override RpcVersion InterfaceVersion => new RpcVersion(0, 0);
		
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> RegisterProvider(Guid pProviderId, Guid ClassId, string pwszProviderName, VSS_PROVIDER_TYPE eProviderType, string pwszProviderVersion, Guid ProviderVersionId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			
			encoder.WriteValue(pProviderId);
			encoder.WriteValue(ClassId);
			encoder.WriteUniqueReferentId(pwszProviderName is null);
			
			if (pwszProviderName is not null)
			{
				encoder.WriteWideCharString(pwszProviderName);
			}
			
			encoder.WriteValue((short)eProviderType);
			encoder.WriteUniqueReferentId(pwszProviderVersion is null);
			
			if (pwszProviderVersion is not null)
			{
				encoder.WriteWideCharString(pwszProviderVersion);
			}
			
			encoder.WriteValue(ProviderVersionId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> UnregisterProvider(Guid ProviderId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			
			encoder.WriteValue(ProviderId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> QueryProviders(RpcPointer<TypedObjref<IVssEnumObject>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			
			ppEnum.value = decoder.ReadInterfacePointer<IVssEnumObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> AbortAllSnapshotsInProgress(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
}
