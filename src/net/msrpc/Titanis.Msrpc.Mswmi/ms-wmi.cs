namespace ms_wmi
{
	using ms_dcom;
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_QUERY_FLAG_TYPE : int
	{
		WBEM_FLAG_DEEP = 0,
		WBEM_FLAG_SHALLOW = 1,
		WBEM_FLAG_PROTOTYPE = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_CHANGE_FLAG_TYPE : int
	{
		WBEM_FLAG_CREATE_OR_UPDATE = 0,
		WBEM_FLAG_UPDATE_ONLY = 1,
		WBEM_FLAG_CREATE_ONLY = 2,
		WBEM_FLAG_UPDATE_SAFE_MODE = 32,
		WBEM_FLAG_UPDATE_FORCE_MODE = 64
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_CONNECT_OPTIONS : int
	{
		WBEM_FLAG_CONNECT_REPOSITORY_ONLY = 64,
		WBEM_FLAG_CONNECT_PROVIDERS = 256
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_GENERIC_FLAG_TYPE : int
	{
		WBEM_FLAG_RETURN_WBEM_COMPLETE = 0,
		WBEM_FLAG_RETURN_IMMEDIATELY = 16,
		WBEM_FLAG_FORWARD_ONLY = 32,
		WBEM_FLAG_NO_ERROR_OBJECT = 64,
		WBEM_FLAG_SEND_STATUS = 128,
		WBEM_FLAG_ENSURE_LOCATABLE = 256,
		WBEM_FLAG_DIRECT_READ = 512,
		WBEM_MASK_RESERVED_FLAGS = 126976,
		WBEM_FLAG_USE_AMENDED_QUALIFIERS = 131072,
		WBEM_FLAG_STRONG_VALIDATION = 1048576
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_STATUS_TYPE : int
	{
		WBEM_STATUS_COMPLETE = 0,
		WBEM_STATUS_REQUIREMENTS = 1,
		WBEM_STATUS_PROGRESS = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_TIMEOUT_TYPE : uint
	{
		WBEM_NO_WAIT = 0U,
		WBEM_INFINITE = 0xFFFFFFFF
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_BACKUP_RESTORE_FLAGS : int
	{
		WBEM_FLAG_BACKUP_RESTORE_FORCE_SHUTDOWN = 1
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEMSTATUS : uint
	{
		WBEM_S_NO_ERROR = 0U,
		WBEM_S_FALSE = 1U,
		WBEM_S_TIMEDOUT = 262148U,
		WBEM_S_NEW_STYLE = 262399U,
		WBEM_S_PARTIAL_RESULTS = 262160U,
		WBEM_E_FAILED = 0x80041001,
		WBEM_E_NOT_FOUND = 0x80041002,
		WBEM_E_ACCESS_DENIED = 0x80041003,
		WBEM_E_PROVIDER_FAILURE = 0x80041004,
		WBEM_E_TYPE_MISMATCH = 0x80041005,
		WBEM_E_OUT_OF_MEMORY = 0x80041006,
		WBEM_E_INVALID_CONTEXT = 0x80041007,
		WBEM_E_INVALID_PARAMETER = 0x80041008,
		WBEM_E_NOT_AVAILABLE = 0x80041009,
		WBEM_E_CRITICAL_ERROR = 0x8004100A,
		WBEM_E_NOT_SUPPORTED = 0x8004100C,
		WBEM_E_PROVIDER_NOT_FOUND = 0x80041011,
		WBEM_E_INVALID_PROVIDER_REGISTRATION = 0x80041012,
		WBEM_E_PROVIDER_LOAD_FAILURE = 0x80041013,
		WBEM_E_INITIALIZATION_FAILURE = 0x80041014,
		WBEM_E_TRANSPORT_FAILURE = 0x80041015,
		WBEM_E_INVALID_OPERATION = 0x80041016,
		WBEM_E_ALREADY_EXISTS = 0x80041019,
		WBEM_E_UNEXPECTED = 0x8004101D,
		WBEM_E_INCOMPLETE_CLASS = 0x80041020,
		WBEM_E_SHUTTING_DOWN = 0x80041033,
		E_NOTIMPL = 0x80004001,
		WBEM_E_INVALID_SUPERCLASS = 0x8004100D,
		WBEM_E_INVALID_NAMESPACE = 0x8004100E,
		WBEM_E_INVALID_OBJECT = 0x8004100F,
		WBEM_E_INVALID_CLASS = 0x80041010,
		WBEM_E_INVALID_QUERY = 0x80041017,
		WBEM_E_INVALID_QUERY_TYPE = 0x80041018,
		WBEM_E_PROVIDER_NOT_CAPABLE = 0x80041024,
		WBEM_E_CLASS_HAS_CHILDREN = 0x80041025,
		WBEM_E_CLASS_HAS_INSTANCES = 0x80041026,
		WBEM_E_ILLEGAL_NULL = 0x80041028,
		WBEM_E_INVALID_CIM_TYPE = 0x8004102D,
		WBEM_E_INVALID_METHOD = 0x8004102E,
		WBEM_E_INVALID_METHOD_PARAMETERS = 0x8004102F,
		WBEM_E_INVALID_PROPERTY = 0x80041031,
		WBEM_E_CALL_CANCELLED = 0x80041032,
		WBEM_E_INVALID_OBJECT_PATH = 0x8004103A,
		WBEM_E_OUT_OF_DISK_SPACE = 0x8004103B,
		WBEM_E_UNSUPPORTED_PUT_EXTENSION = 0x8004103D,
		WBEM_E_QUOTA_VIOLATION = 0x8004106C,
		WBEM_E_SERVER_TOO_BUSY = 0x80041045,
		WBEM_E_METHOD_NOT_IMPLEMENTED = 0x80041055,
		WBEM_E_METHOD_DISABLED = 0x80041056,
		WBEM_E_UNPARSABLE_QUERY = 0x80041058,
		WBEM_E_NOT_EVENT_CLASS = 0x80041059,
		WBEM_E_MISSING_GROUP_WITHIN = 0x8004105A,
		WBEM_E_MISSING_AGGREGATION_LIST = 0x8004105B,
		WBEM_E_PROPERTY_NOT_AN_OBJECT = 0x8004105C,
		WBEM_E_AGGREGATING_BY_OBJECT = 0x8004105D,
		WBEM_E_BACKUP_RESTORE_WINMGMT_RUNNING = 0x80041060,
		WBEM_E_QUEUE_OVERFLOW = 0x80041061,
		WBEM_E_PRIVILEGE_NOT_HELD = 0x80041062,
		WBEM_E_INVALID_OPERATOR = 0x80041063,
		WBEM_E_CANNOT_BE_ABSTRACT = 0x80041065,
		WBEM_E_AMENDED_OBJECT = 0x80041066,
		WBEM_E_VETO_PUT = 0x8004107A,
		WBEM_E_PROVIDER_SUSPENDED = 0x80041081,
		WBEM_E_ENCRYPTED_CONNECTION_REQUIRED = 0x80041087,
		WBEM_E_PROVIDER_TIMED_OUT = 0x80041088,
		WBEM_E_NO_KEY = 0x80041089,
		WBEM_E_PROVIDER_DISABLED = 0x8004108A,
		WBEM_E_REGISTRATION_TOO_BROAD = 0x80042001,
		WBEM_E_REGISTRATION_TOO_PRECISE = 0x80042002
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_REFR_VERSION_NUMBER : int
	{
		WBEM_REFRESHER_VERSION = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_INSTANCE_BLOB_TYPE : int
	{
		WBEM_BLOB_TYPE_ALL = 2,
		WBEM_BLOB_TYPE_ERROR = 3,
		WBEM_BLOB_TYPE_ENUM = 4
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct WBEM_REFRESHED_OBJECT : IRpcFixedStruct
	{
		public int m_lRequestId;
		public WBEM_INSTANCE_BLOB_TYPE m_lBlobType;
		public int m_lBlobLength;
		public RpcPointer<byte[]> m_pbBlob;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lRequestId);
			encoder.WriteValue((int)this.m_lBlobType);
			encoder.WriteValue(this.m_lBlobLength);
			encoder.WriteUniquePointer(this.m_pbBlob);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_lRequestId = decoder.ReadInt32();
			this.m_lBlobType = (WBEM_INSTANCE_BLOB_TYPE)decoder.ReadInt32();
			this.m_lBlobLength = decoder.ReadInt32();
			this.m_pbBlob = decoder.ReadUniquePointer<byte[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.m_pbBlob is not null)
			{
				encoder.WriteArrayHeader(this.m_pbBlob.value);
				for (int i = 0; i < this.m_pbBlob.value.Length; i++)
				{
					byte elem_0 = this.m_pbBlob.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.m_pbBlob is not null)
			{
				this.m_pbBlob.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < this.m_pbBlob.value.Length; i++)
				{
					byte elem_0 = this.m_pbBlob.value[i];
					elem_0 = decoder.ReadByte();
					this.m_pbBlob.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _WBEM_REFRESH_INFO_REMOTE : IRpcFixedStruct
	{
		public TypedObjref<IWbemRemoteRefresher> m_pRefresher;
		public TypedObjref<IWbemClassObject> m_pTemplate;
		public Guid m_Guid;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteInterfacePointer(this.m_pRefresher);
			encoder.WriteInterfacePointer(this.m_pTemplate);
			encoder.WriteValue(this.m_Guid);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_pRefresher = decoder.ReadInterfacePointer<IWbemRemoteRefresher>();
			this.m_pTemplate = decoder.ReadInterfacePointer<IWbemClassObject>();
			this.m_Guid = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteInterfacePointerBody(this.m_pRefresher);
			encoder.WriteInterfacePointerBody(this.m_pTemplate);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadInterfacePointer(this.m_pRefresher);
			decoder.ReadInterfacePointer(this.m_pTemplate);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _WBEM_REFRESH_INFO_NON_HIPERF : IRpcFixedStruct
	{
		public RpcPointer<string> m_wszNamespace;
		public TypedObjref<IWbemClassObject> m_pTemplate;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.m_wszNamespace);
			encoder.WriteInterfacePointer(this.m_pTemplate);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_wszNamespace = decoder.ReadUniquePointer<string>();
			this.m_pTemplate = decoder.ReadInterfacePointer<IWbemClassObject>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.m_wszNamespace is not null)
			{
				encoder.WriteWideCharString(this.m_wszNamespace.value);
			}

			encoder.WriteInterfacePointerBody(this.m_pTemplate);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.m_wszNamespace is not null)
			{
				this.m_wszNamespace.value = decoder.ReadWideCharString();
			}

			decoder.ReadInterfacePointer(this.m_pTemplate);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_REFRESH_TYPE : int
	{
		WBEM_REFRESH_TYPE_INVALID = 0,
		WBEM_REFRESH_TYPE_REMOTE = 3,
		WBEM_REFRESH_TYPE_NON_HIPERF = 6
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct WBEM_REFRESH_INFO_UNION : IRpcFixedStruct
	{
		public int m_lType;
		public _WBEM_REFRESH_INFO_REMOTE m_Remote;
		public _WBEM_REFRESH_INFO_NON_HIPERF m_NonHiPerf;
		public int m_hres;
		public void Encode(IRpcEncoder encoder)
		{
			encoder.AlignUnionTag(NdrAlignment.NativePtr);
			encoder.WriteValue(this.m_lType);
			encoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.m_lType)
			{
				case 3:
					encoder.WriteFixedStruct(this.m_Remote, NdrAlignment.NativePtr);
					break;
				case 6:
					encoder.WriteFixedStruct(this.m_NonHiPerf, NdrAlignment.NativePtr);
					break;
				case 0:
					encoder.WriteValue(this.m_hres);
					break;
			}
		}

		public void Decode(IRpcDecoder decoder)
		{
			decoder.AlignUnionTag(NdrAlignment.NativePtr);
			this.m_lType = decoder.ReadInt32();
			decoder.AlignUnionArm(NdrAlignment.NativePtr);
			switch ((int)this.m_lType)
			{
				case 3:
					this.m_Remote = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO_REMOTE>(NdrAlignment.NativePtr);
					break;
				case 6:
					this.m_NonHiPerf = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO_NON_HIPERF>(NdrAlignment.NativePtr);
					break;
				case 0:
					this.m_hres = decoder.ReadInt32();
					break;
			}
		}

		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			switch ((int)this.m_lType)
			{
				case 3:
					encoder.WriteStructDeferral(this.m_Remote);
					break;
				case 6:
					encoder.WriteStructDeferral(this.m_NonHiPerf);
					break;
				case 0:
					break;
			}
		}

		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			switch ((int)this.m_lType)
			{
				case 3:
					decoder.ReadStructDeferral<_WBEM_REFRESH_INFO_REMOTE>(ref this.m_Remote);
					break;
				case 6:
					decoder.ReadStructDeferral<_WBEM_REFRESH_INFO_NON_HIPERF>(ref this.m_NonHiPerf);
					break;
				case 0:
					break;
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _WBEM_REFRESH_INFO : IRpcFixedStruct
	{
		public int m_lType;
		public WBEM_REFRESH_INFO_UNION m_Info;
		public int m_lCancelId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lType);
			encoder.WriteUnion(this.m_Info);
			encoder.WriteValue(this.m_lCancelId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_lType = decoder.ReadInt32();
			this.m_Info = decoder.ReadUnion<WBEM_REFRESH_INFO_UNION>();
			this.m_lCancelId = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.m_Info);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<WBEM_REFRESH_INFO_UNION>(ref this.m_Info);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _WBEM_REFRESHER_ID : IRpcFixedStruct
	{
		public RpcPointer<string> m_szMachineName;
		public uint m_dwProcessId;
		public Guid m_guidRefresherId;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.m_szMachineName);
			encoder.WriteValue(this.m_dwProcessId);
			encoder.WriteValue(this.m_guidRefresherId);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_szMachineName = decoder.ReadUniquePointer<string>();
			this.m_dwProcessId = decoder.ReadUInt32();
			this.m_guidRefresherId = decoder.ReadUuid();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.m_szMachineName is not null)
			{
				encoder.WriteUnsignedCharString(this.m_szMachineName.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.m_szMachineName is not null)
			{
				this.m_szMachineName.value = decoder.ReadUnsignedCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public enum WBEM_RECONNECT_TYPE : int
	{
		WBEM_RECONNECT_TYPE_OBJECT = 0,
		WBEM_RECONNECT_TYPE_ENUM = 1,
		WBEM_RECONNECT_TYPE_LAST = 2
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _WBEM_RECONNECT_INFO : IRpcFixedStruct
	{
		public int m_lType;
		public RpcPointer<string> m_pwcsPath;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lType);
			encoder.WriteUniquePointer(this.m_pwcsPath);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_lType = decoder.ReadInt32();
			this.m_pwcsPath = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.m_pwcsPath is not null)
			{
				encoder.WriteWideCharString(this.m_pwcsPath.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.m_pwcsPath is not null)
			{
				this.m_pwcsPath.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct _WBEM_RECONNECT_RESULTS : IRpcFixedStruct
	{
		public int m_lId;
		public int m_hr;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lId);
			encoder.WriteValue(this.m_hr);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.m_lId = decoder.ReadInt32();
			this.m_hr = decoder.ReadInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("dc12a681-737f-11cf-884d-00aa004b2e24"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemClassObject : IUnknown
	{
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("dc12a681-737f-11cf-884d-00aa004b2e24")]
	public partial class IWbemClassObjectClientProxy : IUnknownClientProxy, IWbemClassObject
	{
		public sealed override Type InterfaceType => typeof(IWbemClassObject);
		private static Guid _interfaceUuid = new Guid("dc12a681-737f-11cf-884d-00aa004b2e24");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemClassObjectStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("dc12a681-737f-11cf-884d-00aa004b2e24");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemClassObject _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemClassObjectStub(IWbemClassObject obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("7c857801-7381-11cf-884d-00aa004b2e24"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemObjectSink : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Indicate(int lObjectCount, TypedObjref<IWbemClassObject>[] apObjArray, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> SetStatus(int lFlags, int hResult, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strParam, TypedObjref<IWbemClassObject> pObjParam, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("7c857801-7381-11cf-884d-00aa004b2e24")]
	public partial class IWbemObjectSinkClientProxy : IUnknownClientProxy, IWbemObjectSink
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Indicate(int lObjectCount, TypedObjref<IWbemClassObject>[] apObjArray, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lObjectCount);
			if (apObjArray is not null)
			{
				encoder.WriteArrayHeader(apObjArray);
				for (int i = 0; i < apObjArray.Length; i++)
				{
					TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
					encoder.WriteInterfacePointer(elem_0);
				}
			}

			for (int i = 0; i < apObjArray.Length; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
				encoder.WriteInterfacePointerBody(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> SetStatus(int lFlags, int hResult, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strParam, TypedObjref<IWbemClassObject> pObjParam, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			encoder.WriteValue(hResult);
			encoder.WriteUniquePointer(strParam);
			if (strParam is not null)
			{
				encoder.WriteConformantStruct(strParam.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strParam.value);
			}

			encoder.WriteInterfacePointer(pObjParam);
			encoder.WriteInterfacePointerBody(pObjParam);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemObjectSink);
		private static Guid _interfaceUuid = new Guid("7c857801-7381-11cf-884d-00aa004b2e24");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemObjectSinkStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Indicate(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lObjectCount;
			TypedObjref<IWbemClassObject>[] apObjArray;
			lObjectCount = decoder.ReadInt32();
			apObjArray = decoder.ReadArrayHeader<TypedObjref<IWbemClassObject>>();
			for (int i = 0; i < apObjArray.Length; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
				elem_0 = decoder.ReadInterfacePointer<IWbemClassObject>();
				apObjArray[i] = elem_0;
			}

			for (int i = 0; i < apObjArray.Length; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
				decoder.ReadInterfacePointer(elem_0);
				apObjArray[i] = elem_0;
			}

			var invokeTask = this._obj.Indicate(lObjectCount, apObjArray, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_SetStatus(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lFlags;
			int hResult;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strParam;
			TypedObjref<IWbemClassObject> pObjParam;
			lFlags = decoder.ReadInt32();
			hResult = decoder.ReadInt32();
			strParam = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strParam is not null)
			{
				strParam.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strParam.value);
			}

			pObjParam = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pObjParam);
			var invokeTask = this._obj.SetStatus(lFlags, hResult, strParam, pObjParam, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("7c857801-7381-11cf-884d-00aa004b2e24");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemObjectSink _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemObjectSinkStub(IWbemObjectSink obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Indicate, this.Invoke_SetStatus};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("027947e1-d731-11ce-a357-000000000001"), RpcVersionAttribute(0, 0)]
	public partial interface IEnumWbemClassObject : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Reset(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Next(int lTimeout, uint uCount, RpcPointer<ArraySegment<TypedObjref<IWbemClassObject>>> apObjects, RpcPointer<uint> puReturned, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> NextAsync(uint uCount, TypedObjref<IWbemObjectSink> pSink, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Clone(RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Skip(int lTimeout, uint nCount, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("027947e1-d731-11ce-a357-000000000001")]
	public partial class IEnumWbemClassObjectClientProxy : IUnknownClientProxy, IEnumWbemClassObject
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Reset(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Next(int lTimeout, uint uCount, RpcPointer<ArraySegment<TypedObjref<IWbemClassObject>>> apObjects, RpcPointer<uint> puReturned, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			encoder.WriteValue(uCount);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			apObjects.value = decoder.ReadArraySegmentHeader<TypedObjref<IWbemClassObject>>();
			for (int i = 0; i < apObjects.value.Count; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				elem_0 = decoder.ReadInterfacePointer<IWbemClassObject>();
				apObjects.value.Item(i) = elem_0;
			}

			for (int i = 0; i < apObjects.value.Count; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				decoder.ReadInterfacePointer(elem_0);
				apObjects.value.Item(i) = elem_0;
			}

			puReturned.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> NextAsync(uint uCount, TypedObjref<IWbemObjectSink> pSink, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(uCount);
			encoder.WriteInterfacePointer(pSink);
			encoder.WriteInterfacePointerBody(pSink);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Clone(RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Skip(int lTimeout, uint nCount, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			encoder.WriteValue(nCount);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IEnumWbemClassObject);
		private static Guid _interfaceUuid = new Guid("027947e1-d731-11ce-a357-000000000001");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IEnumWbemClassObjectStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Reset(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Reset(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Next(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lTimeout;
			uint uCount;
			RpcPointer<ArraySegment<TypedObjref<IWbemClassObject>>> apObjects = new RpcPointer<ArraySegment<TypedObjref<IWbemClassObject>>>();
			RpcPointer<uint> puReturned = new RpcPointer<uint>();
			lTimeout = decoder.ReadInt32();
			uCount = decoder.ReadUInt32();
			var invokeTask = this._obj.Next(lTimeout, uCount, apObjects, puReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(apObjects.value, true);
			for (int i = 0; i < apObjects.value.Count; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				encoder.WriteInterfacePointer(elem_0);
			}

			for (int i = 0; i < apObjects.value.Count; i++)
			{
				TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				encoder.WriteInterfacePointerBody(elem_0);
			}

			encoder.WriteValue(puReturned.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_NextAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			uint uCount;
			TypedObjref<IWbemObjectSink> pSink;
			uCount = decoder.ReadUInt32();
			pSink = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pSink);
			var invokeTask = this._obj.NextAsync(uCount, pSink, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Clone(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			var invokeTask = this._obj.Clone(ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Skip(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lTimeout;
			uint nCount;
			lTimeout = decoder.ReadInt32();
			nCount = decoder.ReadUInt32();
			var invokeTask = this._obj.Skip(lTimeout, nCount, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("027947e1-d731-11ce-a357-000000000001");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IEnumWbemClassObject _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IEnumWbemClassObjectStub(IEnumWbemClassObject obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Reset, this.Invoke_Next, this.Invoke_NextAsync, this.Invoke_Clone, this.Invoke_Skip};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("44aca674-e8fc-11d0-a07c-00c04fb68820"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemContext : IUnknown
	{
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("44aca674-e8fc-11d0-a07c-00c04fb68820")]
	public partial class IWbemContextClientProxy : IUnknownClientProxy, IWbemContext
	{
		public sealed override Type InterfaceType => typeof(IWbemContext);
		private static Guid _interfaceUuid = new Guid("44aca674-e8fc-11d0-a07c-00c04fb68820");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemContextStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("44aca674-e8fc-11d0-a07c-00c04fb68820");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemContext _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemContextStub(IWbemContext obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("44aca675-e8fc-11d0-a07c-00c04fb68820"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemCallResult : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetResultObject(int lTimeout, RpcPointer<TypedObjref<IWbemClassObject>> ppResultObject, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetResultString(int lTimeout, RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>> pstrResultString, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetResultServices(int lTimeout, RpcPointer<TypedObjref<IWbemServices>> ppServices, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetCallStatus(int lTimeout, RpcPointer<int> plStatus, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("44aca675-e8fc-11d0-a07c-00c04fb68820")]
	public partial class IWbemCallResultClientProxy : IUnknownClientProxy, IWbemCallResult
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetResultObject(int lTimeout, RpcPointer<TypedObjref<IWbemClassObject>> ppResultObject, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppResultObject.value = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(ppResultObject.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetResultString(int lTimeout, RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>> pstrResultString, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pstrResultString.value = decoder.ReadOutUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>(pstrResultString.value);
			if (pstrResultString.value is not null)
			{
				pstrResultString.value.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref pstrResultString.value.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetResultServices(int lTimeout, RpcPointer<TypedObjref<IWbemServices>> ppServices, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppServices.value = decoder.ReadInterfacePointer<IWbemServices>();
			decoder.ReadInterfacePointer(ppServices.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetCallStatus(int lTimeout, RpcPointer<int> plStatus, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			plStatus.value = decoder.ReadInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemCallResult);
		private static Guid _interfaceUuid = new Guid("44aca675-e8fc-11d0-a07c-00c04fb68820");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemCallResultStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetResultObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<TypedObjref<IWbemClassObject>> ppResultObject = new RpcPointer<TypedObjref<IWbemClassObject>>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetResultObject(lTimeout, ppResultObject, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppResultObject.value);
			encoder.WriteInterfacePointerBody(ppResultObject.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetResultString(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>> pstrResultString = new RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetResultString(lTimeout, pstrResultString, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(pstrResultString.value);
			if (pstrResultString.value is not null)
			{
				encoder.WriteConformantStruct(pstrResultString.value.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pstrResultString.value.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetResultServices(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<TypedObjref<IWbemServices>> ppServices = new RpcPointer<TypedObjref<IWbemServices>>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetResultServices(lTimeout, ppServices, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppServices.value);
			encoder.WriteInterfacePointerBody(ppServices.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetCallStatus(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<int> plStatus = new RpcPointer<int>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetCallStatus(lTimeout, plStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(plStatus.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("44aca675-e8fc-11d0-a07c-00c04fb68820");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemCallResult _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemCallResultStub(IWbemCallResult obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetResultObject, this.Invoke_GetResultString, this.Invoke_GetResultServices, this.Invoke_GetCallStatus};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("9556dc99-828c-11cf-a37e-00aa003240c7"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemServices : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> OpenNamespace(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strNamespace, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemServices>> ppWorkingNamespace, RpcPointer<TypedObjref<IWbemCallResult>> ppResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> CancelAsyncCall(TypedObjref<IWbemObjectSink> pSink, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> QueryObjectSink(int lFlags, RpcPointer<TypedObjref<IWbemObjectSink>> ppResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetObject(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemClassObject>> ppObject, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetObjectAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> PutClass(TypedObjref<IWbemClassObject> pObject, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> PutClassAsync(TypedObjref<IWbemClassObject> pObject, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> DeleteClass(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> DeleteClassAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> CreateClassEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> CreateClassEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> PutInstance(TypedObjref<IWbemClassObject> pInst, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> PutInstanceAsync(TypedObjref<IWbemClassObject> pInst, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> DeleteInstance(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> DeleteInstanceAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> CreateInstanceEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> CreateInstanceEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ExecQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ExecQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ExecNotificationQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ExecNotificationQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ExecMethod(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemClassObject> pInParams, RpcPointer<TypedObjref<IWbemClassObject>> ppOutParams, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ExecMethodAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemClassObject> pInParams, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("9556dc99-828c-11cf-a37e-00aa003240c7")]
	public partial class IWbemServicesClientProxy : IUnknownClientProxy, IWbemServices
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> OpenNamespace(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strNamespace, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemServices>> ppWorkingNamespace, RpcPointer<TypedObjref<IWbemCallResult>> ppResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strNamespace);
			if (strNamespace is not null)
			{
				encoder.WriteConformantStruct(strNamespace.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strNamespace.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteUniquePointer(ppWorkingNamespace);
			if (ppWorkingNamespace is not null)
			{
				encoder.WriteInterfacePointer(ppWorkingNamespace.value);
				encoder.WriteInterfacePointerBody(ppWorkingNamespace.value);
			}

			encoder.WriteUniquePointer(ppResult);
			if (ppResult is not null)
			{
				encoder.WriteInterfacePointer(ppResult.value);
				encoder.WriteInterfacePointerBody(ppResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppWorkingNamespace = decoder.ReadOutUniquePointer<TypedObjref<IWbemServices>>(ppWorkingNamespace);
			if (ppWorkingNamespace is not null)
			{
				ppWorkingNamespace.value = decoder.ReadInterfacePointer<IWbemServices>();
				decoder.ReadInterfacePointer(ppWorkingNamespace.value);
			}

			ppResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppResult);
			if (ppResult is not null)
			{
				ppResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> CancelAsyncCall(TypedObjref<IWbemObjectSink> pSink, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pSink);
			encoder.WriteInterfacePointerBody(pSink);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> QueryObjectSink(int lFlags, RpcPointer<TypedObjref<IWbemObjectSink>> ppResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppResponseHandler.value = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(ppResponseHandler.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetObject(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemClassObject>> ppObject, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strObjectPath);
			if (strObjectPath is not null)
			{
				encoder.WriteConformantStruct(strObjectPath.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteUniquePointer(ppObject);
			if (ppObject is not null)
			{
				encoder.WriteInterfacePointer(ppObject.value);
				encoder.WriteInterfacePointerBody(ppObject.value);
			}

			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppObject = decoder.ReadOutUniquePointer<TypedObjref<IWbemClassObject>>(ppObject);
			if (ppObject is not null)
			{
				ppObject.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppObject.value);
			}

			ppCallResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppCallResult);
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetObjectAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strObjectPath);
			if (strObjectPath is not null)
			{
				encoder.WriteConformantStruct(strObjectPath.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> PutClass(TypedObjref<IWbemClassObject> pObject, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pObject);
			encoder.WriteInterfacePointerBody(pObject);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppCallResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppCallResult);
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> PutClassAsync(TypedObjref<IWbemClassObject> pObject, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pObject);
			encoder.WriteInterfacePointerBody(pObject);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> DeleteClass(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strClass);
			if (strClass is not null)
			{
				encoder.WriteConformantStruct(strClass.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strClass.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppCallResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppCallResult);
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> DeleteClassAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strClass);
			if (strClass is not null)
			{
				encoder.WriteConformantStruct(strClass.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strClass.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> CreateClassEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strSuperclass);
			if (strSuperclass is not null)
			{
				encoder.WriteConformantStruct(strSuperclass.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperclass.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> CreateClassEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strSuperclass);
			if (strSuperclass is not null)
			{
				encoder.WriteConformantStruct(strSuperclass.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperclass.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> PutInstance(TypedObjref<IWbemClassObject> pInst, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pInst);
			encoder.WriteInterfacePointerBody(pInst);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppCallResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppCallResult);
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> PutInstanceAsync(TypedObjref<IWbemClassObject> pInst, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pInst);
			encoder.WriteInterfacePointerBody(pInst);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> DeleteInstance(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strObjectPath);
			if (strObjectPath is not null)
			{
				encoder.WriteConformantStruct(strObjectPath.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppCallResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppCallResult);
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> DeleteInstanceAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strObjectPath);
			if (strObjectPath is not null)
			{
				encoder.WriteConformantStruct(strObjectPath.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> CreateInstanceEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strSuperClass);
			if (strSuperClass is not null)
			{
				encoder.WriteConformantStruct(strSuperClass.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperClass.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> CreateInstanceEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strSuperClass);
			if (strSuperClass is not null)
			{
				encoder.WriteConformantStruct(strSuperClass.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperClass.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ExecQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strQueryLanguage);
			if (strQueryLanguage is not null)
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}

			encoder.WriteUniquePointer(strQuery);
			if (strQuery is not null)
			{
				encoder.WriteConformantStruct(strQuery.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ExecQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strQueryLanguage);
			if (strQueryLanguage is not null)
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}

			encoder.WriteUniquePointer(strQuery);
			if (strQuery is not null)
			{
				encoder.WriteConformantStruct(strQuery.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ExecNotificationQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strQueryLanguage);
			if (strQueryLanguage is not null)
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}

			encoder.WriteUniquePointer(strQuery);
			if (strQuery is not null)
			{
				encoder.WriteConformantStruct(strQuery.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ExecNotificationQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strQueryLanguage);
			if (strQueryLanguage is not null)
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}

			encoder.WriteUniquePointer(strQuery);
			if (strQuery is not null)
			{
				encoder.WriteConformantStruct(strQuery.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ExecMethod(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemClassObject> pInParams, RpcPointer<TypedObjref<IWbemClassObject>> ppOutParams, RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strObjectPath);
			if (strObjectPath is not null)
			{
				encoder.WriteConformantStruct(strObjectPath.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}

			encoder.WriteUniquePointer(strMethodName);
			if (strMethodName is not null)
			{
				encoder.WriteConformantStruct(strMethodName.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strMethodName.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pInParams);
			encoder.WriteInterfacePointerBody(pInParams);
			encoder.WriteUniquePointer(ppOutParams);
			if (ppOutParams is not null)
			{
				encoder.WriteInterfacePointer(ppOutParams.value);
				encoder.WriteInterfacePointerBody(ppOutParams.value);
			}

			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppOutParams = decoder.ReadOutUniquePointer<TypedObjref<IWbemClassObject>>(ppOutParams);
			if (ppOutParams is not null)
			{
				ppOutParams.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppOutParams.value);
			}

			ppCallResult = decoder.ReadOutUniquePointer<TypedObjref<IWbemCallResult>>(ppCallResult);
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ExecMethodAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, TypedObjref<IWbemContext> pCtx, TypedObjref<IWbemClassObject> pInParams, TypedObjref<IWbemObjectSink> pResponseHandler, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(strObjectPath);
			if (strObjectPath is not null)
			{
				encoder.WriteConformantStruct(strObjectPath.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}

			encoder.WriteUniquePointer(strMethodName);
			if (strMethodName is not null)
			{
				encoder.WriteConformantStruct(strMethodName.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strMethodName.value);
			}

			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pInParams);
			encoder.WriteInterfacePointerBody(pInParams);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemServices);
		private static Guid _interfaceUuid = new Guid("9556dc99-828c-11cf-a37e-00aa003240c7");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemServicesStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_OpenNamespace(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strNamespace;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemServices>> ppWorkingNamespace;
			RpcPointer<TypedObjref<IWbemCallResult>> ppResult;
			strNamespace = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strNamespace is not null)
			{
				strNamespace.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strNamespace.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppWorkingNamespace = decoder.ReadUniquePointer<TypedObjref<IWbemServices>>();
			if (ppWorkingNamespace is not null)
			{
				ppWorkingNamespace.value = decoder.ReadInterfacePointer<IWbemServices>();
				decoder.ReadInterfacePointer(ppWorkingNamespace.value);
			}

			ppResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppResult is not null)
			{
				ppResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppResult.value);
			}

			var invokeTask = this._obj.OpenNamespace(strNamespace, lFlags, pCtx, ppWorkingNamespace, ppResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppWorkingNamespace);
			if (ppWorkingNamespace is not null)
			{
				encoder.WriteInterfacePointer(ppWorkingNamespace.value);
				encoder.WriteInterfacePointerBody(ppWorkingNamespace.value);
			}

			encoder.WriteUniquePointer(ppResult);
			if (ppResult is not null)
			{
				encoder.WriteInterfacePointer(ppResult.value);
				encoder.WriteInterfacePointerBody(ppResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CancelAsyncCall(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			TypedObjref<IWbemObjectSink> pSink;
			pSink = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pSink);
			var invokeTask = this._obj.CancelAsyncCall(pSink, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_QueryObjectSink(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lFlags;
			RpcPointer<TypedObjref<IWbemObjectSink>> ppResponseHandler = new RpcPointer<TypedObjref<IWbemObjectSink>>();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.QueryObjectSink(lFlags, ppResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppResponseHandler.value);
			encoder.WriteInterfacePointerBody(ppResponseHandler.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetObject(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemClassObject>> ppObject;
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult;
			strObjectPath = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strObjectPath is not null)
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppObject = decoder.ReadUniquePointer<TypedObjref<IWbemClassObject>>();
			if (ppObject is not null)
			{
				ppObject.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppObject.value);
			}

			ppCallResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			var invokeTask = this._obj.GetObject(strObjectPath, lFlags, pCtx, ppObject, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppObject);
			if (ppObject is not null)
			{
				encoder.WriteInterfacePointer(ppObject.value);
				encoder.WriteInterfacePointerBody(ppObject.value);
			}

			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetObjectAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strObjectPath = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strObjectPath is not null)
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.GetObjectAsync(strObjectPath, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_PutClass(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			TypedObjref<IWbemClassObject> pObject;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult;
			pObject = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pObject);
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			var invokeTask = this._obj.PutClass(pObject, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_PutClassAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			TypedObjref<IWbemClassObject> pObject;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			pObject = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pObject);
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.PutClassAsync(pObject, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_DeleteClass(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult;
			strClass = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strClass is not null)
			{
				strClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strClass.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			var invokeTask = this._obj.DeleteClass(strClass, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_DeleteClassAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strClass = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strClass is not null)
			{
				strClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strClass.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.DeleteClassAsync(strClass, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CreateClassEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			strSuperclass = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strSuperclass is not null)
			{
				strSuperclass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strSuperclass.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			var invokeTask = this._obj.CreateClassEnum(strSuperclass, lFlags, pCtx, ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CreateClassEnumAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strSuperclass = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strSuperclass is not null)
			{
				strSuperclass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strSuperclass.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.CreateClassEnumAsync(strSuperclass, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_PutInstance(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			TypedObjref<IWbemClassObject> pInst;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult;
			pInst = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pInst);
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			var invokeTask = this._obj.PutInstance(pInst, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_PutInstanceAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			TypedObjref<IWbemClassObject> pInst;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			pInst = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pInst);
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.PutInstanceAsync(pInst, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_DeleteInstance(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult;
			strObjectPath = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strObjectPath is not null)
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			var invokeTask = this._obj.DeleteInstance(strObjectPath, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_DeleteInstanceAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strObjectPath = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strObjectPath is not null)
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.DeleteInstanceAsync(strObjectPath, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CreateInstanceEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			strSuperClass = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strSuperClass is not null)
			{
				strSuperClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strSuperClass.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			var invokeTask = this._obj.CreateInstanceEnum(strSuperClass, lFlags, pCtx, ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_CreateInstanceEnumAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strSuperClass = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strSuperClass is not null)
			{
				strSuperClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strSuperClass.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.CreateInstanceEnumAsync(strSuperClass, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ExecQuery(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			strQueryLanguage = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQueryLanguage is not null)
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}

			strQuery = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQuery is not null)
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQuery.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			var invokeTask = this._obj.ExecQuery(strQueryLanguage, strQuery, lFlags, pCtx, ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ExecQueryAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strQueryLanguage = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQueryLanguage is not null)
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}

			strQuery = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQuery is not null)
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQuery.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.ExecQueryAsync(strQueryLanguage, strQuery, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ExecNotificationQuery(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<TypedObjref<IEnumWbemClassObject>>();
			strQueryLanguage = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQueryLanguage is not null)
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}

			strQuery = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQuery is not null)
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQuery.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			var invokeTask = this._obj.ExecNotificationQuery(strQueryLanguage, strQuery, lFlags, pCtx, ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ExecNotificationQueryAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strQueryLanguage = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQueryLanguage is not null)
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}

			strQuery = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strQuery is not null)
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQuery.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.ExecNotificationQueryAsync(strQueryLanguage, strQuery, lFlags, pCtx, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ExecMethod(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemClassObject> pInParams;
			RpcPointer<TypedObjref<IWbemClassObject>> ppOutParams;
			RpcPointer<TypedObjref<IWbemCallResult>> ppCallResult;
			strObjectPath = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strObjectPath is not null)
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}

			strMethodName = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strMethodName is not null)
			{
				strMethodName.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strMethodName.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pInParams = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pInParams);
			ppOutParams = decoder.ReadUniquePointer<TypedObjref<IWbemClassObject>>();
			if (ppOutParams is not null)
			{
				ppOutParams.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppOutParams.value);
			}

			ppCallResult = decoder.ReadUniquePointer<TypedObjref<IWbemCallResult>>();
			if (ppCallResult is not null)
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}

			var invokeTask = this._obj.ExecMethod(strObjectPath, strMethodName, lFlags, pCtx, pInParams, ppOutParams, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppOutParams);
			if (ppOutParams is not null)
			{
				encoder.WriteInterfacePointer(ppOutParams.value);
				encoder.WriteInterfacePointerBody(ppOutParams.value);
			}

			encoder.WriteUniquePointer(ppCallResult);
			if (ppCallResult is not null)
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ExecMethodAsync(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			TypedObjref<IWbemClassObject> pInParams;
			TypedObjref<IWbemObjectSink> pResponseHandler;
			strObjectPath = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strObjectPath is not null)
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}

			strMethodName = decoder.ReadUniquePointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if (strMethodName is not null)
			{
				strMethodName.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strMethodName.value);
			}

			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pInParams = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pInParams);
			pResponseHandler = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pResponseHandler);
			var invokeTask = this._obj.ExecMethodAsync(strObjectPath, strMethodName, lFlags, pCtx, pInParams, pResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("9556dc99-828c-11cf-a37e-00aa003240c7");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemServices _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemServicesStub(IWbemServices obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_OpenNamespace, this.Invoke_CancelAsyncCall, this.Invoke_QueryObjectSink, this.Invoke_GetObject, this.Invoke_GetObjectAsync, this.Invoke_PutClass, this.Invoke_PutClassAsync, this.Invoke_DeleteClass, this.Invoke_DeleteClassAsync, this.Invoke_CreateClassEnum, this.Invoke_CreateClassEnumAsync, this.Invoke_PutInstance, this.Invoke_PutInstanceAsync, this.Invoke_DeleteInstance, this.Invoke_DeleteInstanceAsync, this.Invoke_CreateInstanceEnum, this.Invoke_CreateInstanceEnumAsync, this.Invoke_ExecQuery, this.Invoke_ExecQueryAsync, this.Invoke_ExecNotificationQuery, this.Invoke_ExecNotificationQueryAsync, this.Invoke_ExecMethod, this.Invoke_ExecMethodAsync};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("c49e32c7-bc8b-11d2-85d4-00105a1f8304"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemBackupRestore : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Backup(string strBackupToFile, int lFlags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Restore(string strRestoreFromFile, int lFlags, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("c49e32c7-bc8b-11d2-85d4-00105a1f8304")]
	public partial class IWbemBackupRestoreClientProxy : IUnknownClientProxy, IWbemBackupRestore
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Backup(string strBackupToFile, int lFlags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(strBackupToFile);
			encoder.WriteValue(lFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Restore(string strRestoreFromFile, int lFlags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(strRestoreFromFile);
			encoder.WriteValue(lFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public override Type InterfaceType => typeof(IWbemBackupRestore);
		private static Guid _interfaceUuid = new Guid("c49e32c7-bc8b-11d2-85d4-00105a1f8304");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemBackupRestoreStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Backup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string strBackupToFile;
			int lFlags;
			strBackupToFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Backup(strBackupToFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Restore(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string strRestoreFromFile;
			int lFlags;
			strRestoreFromFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Restore(strRestoreFromFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("c49e32c7-bc8b-11d2-85d4-00105a1f8304");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemBackupRestore _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemBackupRestoreStub(IWbemBackupRestore obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Backup, this.Invoke_Restore};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("a359dec5-e813-4834-8a2a-ba7f1d777d76"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemBackupRestoreEx : IWbemBackupRestore
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Pause(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Resume(CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("a359dec5-e813-4834-8a2a-ba7f1d777d76")]
	public partial class IWbemBackupRestoreExClientProxy : IWbemBackupRestoreClientProxy, IWbemBackupRestoreEx
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Pause(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Resume(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemBackupRestoreEx);
		private static Guid _interfaceUuid = new Guid("a359dec5-e813-4834-8a2a-ba7f1d777d76");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemBackupRestoreExStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Backup(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string strBackupToFile;
			int lFlags;
			strBackupToFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Backup(strBackupToFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Restore(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string strRestoreFromFile;
			int lFlags;
			strRestoreFromFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Restore(strRestoreFromFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Pause(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Pause(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Resume(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Resume(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("a359dec5-e813-4834-8a2a-ba7f1d777d76");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemBackupRestoreEx _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemBackupRestoreExStub(IWbemBackupRestoreEx obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Backup, this.Invoke_Restore, this.Invoke_Pause, this.Invoke_Resume};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("f1e9c5b2-f59b-11d2-b362-00105a1f8177"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemRemoteRefresher : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> RemoteRefresh(int lFlags, RpcPointer<int> plNumObjects, RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>> paObjects, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> StopRefreshing(int lNumIds, int[] aplIds, int lFlags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Opnum5NotUsedOnWire(int lFlags, RpcPointer<Guid> pGuid, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("f1e9c5b2-f59b-11d2-b362-00105a1f8177")]
	public partial class IWbemRemoteRefresherClientProxy : IUnknownClientProxy, IWbemRemoteRefresher
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> RemoteRefresh(int lFlags, RpcPointer<int> plNumObjects, RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>> paObjects, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			plNumObjects.value = decoder.ReadInt32();
			paObjects.value = decoder.ReadOutUniquePointer<WBEM_REFRESHED_OBJECT[]>(paObjects.value);
			if (paObjects.value is not null)
			{
				paObjects.value.value = decoder.ReadArrayHeader<WBEM_REFRESHED_OBJECT>();
				for (int i = 0; i < paObjects.value.value.Length; i++)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					elem_0 = decoder.ReadFixedStruct<WBEM_REFRESHED_OBJECT>(NdrAlignment.NativePtr);
					paObjects.value.value[i] = elem_0;
				}

				for (int i = 0; i < paObjects.value.value.Length; i++)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					decoder.ReadStructDeferral<WBEM_REFRESHED_OBJECT>(ref elem_0);
					paObjects.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> StopRefreshing(int lNumIds, int[] aplIds, int lFlags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lNumIds);
			if (aplIds is not null)
			{
				encoder.WriteArrayHeader(aplIds);
				for (int i = 0; i < aplIds.Length; i++)
				{
					int elem_0 = aplIds[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(lFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Opnum5NotUsedOnWire(int lFlags, RpcPointer<Guid> pGuid, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pGuid.value = decoder.ReadUuid();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemRemoteRefresher);
		private static Guid _interfaceUuid = new Guid("f1e9c5b2-f59b-11d2-b362-00105a1f8177");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemRemoteRefresherStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_RemoteRefresh(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lFlags;
			RpcPointer<int> plNumObjects = new RpcPointer<int>();
			RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>> paObjects = new RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>>();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.RemoteRefresh(lFlags, plNumObjects, paObjects, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(plNumObjects.value);
			encoder.WriteUniquePointer(paObjects.value);
			if (paObjects.value is not null)
			{
				encoder.WriteArrayHeader(paObjects.value.value);
				for (int i = 0; i < paObjects.value.value.Length; i++)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < paObjects.value.value.Length; i++)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_StopRefreshing(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lNumIds;
			int[] aplIds;
			int lFlags;
			lNumIds = decoder.ReadInt32();
			aplIds = decoder.ReadArrayHeader<int>();
			for (int i = 0; i < aplIds.Length; i++)
			{
				int elem_0 = aplIds[i];
				elem_0 = decoder.ReadInt32();
				aplIds[i] = elem_0;
			}

			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.StopRefreshing(lNumIds, aplIds, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum5NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			int lFlags;
			RpcPointer<Guid> pGuid = new RpcPointer<Guid>();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Opnum5NotUsedOnWire(lFlags, pGuid, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pGuid.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("f1e9c5b2-f59b-11d2-b362-00105a1f8177");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemRemoteRefresher _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemRemoteRefresherStub(IWbemRemoteRefresher obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_RemoteRefresh, this.Invoke_StopRefreshing, this.Invoke_Opnum5NotUsedOnWire};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("2c9273e0-1dc3-11d3-b364-00105a1f8177"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemRefreshingServices : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> AddObjectToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszPath, int lFlags, TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> AddObjectToRefresherByTemplate(_WBEM_REFRESHER_ID pRefresherId, TypedObjref<IWbemClassObject> pTemplate, int lFlags, TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> AddEnumToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszClass, int lFlags, TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> RemoveObjectFromRefresher(_WBEM_REFRESHER_ID pRefresherId, int lId, int lFlags, uint dwClientRefrVersion, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, uint dwClientRefrVersion, RpcPointer<TypedObjref<IWbemRemoteRefresher>> ppRemRefresher, RpcPointer<Guid> pGuid, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> ReconnectRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, int lNumObjects, uint dwClientRefrVersion, _WBEM_RECONNECT_INFO[] apReconnectInfo, RpcPointer<_WBEM_RECONNECT_RESULTS[]> apReconnectResults, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("2c9273e0-1dc3-11d3-b364-00105a1f8177")]
	public partial class IWbemRefreshingServicesClientProxy : IUnknownClientProxy, IWbemRefreshingServices
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> AddObjectToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszPath, int lFlags, TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteWideCharString(wszPath);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pContext);
			encoder.WriteInterfacePointerBody(pContext);
			encoder.WriteValue(dwClientRefrVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pInfo.value = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESH_INFO>(ref pInfo.value);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> AddObjectToRefresherByTemplate(_WBEM_REFRESHER_ID pRefresherId, TypedObjref<IWbemClassObject> pTemplate, int lFlags, TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteInterfacePointer(pTemplate);
			encoder.WriteInterfacePointerBody(pTemplate);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pContext);
			encoder.WriteInterfacePointerBody(pContext);
			encoder.WriteValue(dwClientRefrVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pInfo.value = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESH_INFO>(ref pInfo.value);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> AddEnumToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszClass, int lFlags, TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteWideCharString(wszClass);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pContext);
			encoder.WriteInterfacePointerBody(pContext);
			encoder.WriteValue(dwClientRefrVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pInfo.value = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESH_INFO>(ref pInfo.value);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> RemoveObjectFromRefresher(_WBEM_REFRESHER_ID pRefresherId, int lId, int lFlags, uint dwClientRefrVersion, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteValue(lId);
			encoder.WriteValue(lFlags);
			encoder.WriteValue(dwClientRefrVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, uint dwClientRefrVersion, RpcPointer<TypedObjref<IWbemRemoteRefresher>> ppRemRefresher, RpcPointer<Guid> pGuid, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteValue(lFlags);
			encoder.WriteValue(dwClientRefrVersion);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppRemRefresher.value = decoder.ReadInterfacePointer<IWbemRemoteRefresher>();
			decoder.ReadInterfacePointer(ppRemRefresher.value);
			pGuid.value = decoder.ReadUuid();
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> ReconnectRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, int lNumObjects, uint dwClientRefrVersion, _WBEM_RECONNECT_INFO[] apReconnectInfo, RpcPointer<_WBEM_RECONNECT_RESULTS[]> apReconnectResults, RpcPointer<uint> pdwSvrRefrVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteValue(lFlags);
			encoder.WriteValue(lNumObjects);
			encoder.WriteValue(dwClientRefrVersion);
			if (apReconnectInfo is not null)
			{
				encoder.WriteArrayHeader(apReconnectInfo);
				for (int i = 0; i < apReconnectInfo.Length; i++)
				{
					_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}
			}

			for (int i = 0; i < apReconnectInfo.Length; i++)
			{
				_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteArrayHeader(apReconnectResults.value);
			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}

			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteStructDeferral(elem_0);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			apReconnectResults.value = decoder.ReadArrayHeader<_WBEM_RECONNECT_RESULTS>();
			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				elem_0 = decoder.ReadFixedStruct<_WBEM_RECONNECT_RESULTS>(NdrAlignment._4Byte);
				apReconnectResults.value[i] = elem_0;
			}

			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				decoder.ReadStructDeferral<_WBEM_RECONNECT_RESULTS>(ref elem_0);
				apReconnectResults.value[i] = elem_0;
			}

			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemRefreshingServices);
		private static Guid _interfaceUuid = new Guid("2c9273e0-1dc3-11d3-b364-00105a1f8177");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemRefreshingServicesStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_AddObjectToRefresher(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			string wszPath;
			int lFlags;
			TypedObjref<IWbemContext> pContext;
			uint dwClientRefrVersion;
			RpcPointer<_WBEM_REFRESH_INFO> pInfo = new RpcPointer<_WBEM_REFRESH_INFO>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			wszPath = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			pContext = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pContext);
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.AddObjectToRefresher(pRefresherId, wszPath, lFlags, pContext, dwClientRefrVersion, pInfo, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_AddObjectToRefresherByTemplate(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			TypedObjref<IWbemClassObject> pTemplate;
			int lFlags;
			TypedObjref<IWbemContext> pContext;
			uint dwClientRefrVersion;
			RpcPointer<_WBEM_REFRESH_INFO> pInfo = new RpcPointer<_WBEM_REFRESH_INFO>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			pTemplate = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pTemplate);
			lFlags = decoder.ReadInt32();
			pContext = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pContext);
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.AddObjectToRefresherByTemplate(pRefresherId, pTemplate, lFlags, pContext, dwClientRefrVersion, pInfo, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_AddEnumToRefresher(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			string wszClass;
			int lFlags;
			TypedObjref<IWbemContext> pContext;
			uint dwClientRefrVersion;
			RpcPointer<_WBEM_REFRESH_INFO> pInfo = new RpcPointer<_WBEM_REFRESH_INFO>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			wszClass = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			pContext = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pContext);
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.AddEnumToRefresher(pRefresherId, wszClass, lFlags, pContext, dwClientRefrVersion, pInfo, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_RemoveObjectFromRefresher(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			int lId;
			int lFlags;
			uint dwClientRefrVersion;
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			lId = decoder.ReadInt32();
			lFlags = decoder.ReadInt32();
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.RemoveObjectFromRefresher(pRefresherId, lId, lFlags, dwClientRefrVersion, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetRemoteRefresher(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			int lFlags;
			uint dwClientRefrVersion;
			RpcPointer<TypedObjref<IWbemRemoteRefresher>> ppRemRefresher = new RpcPointer<TypedObjref<IWbemRemoteRefresher>>();
			RpcPointer<Guid> pGuid = new RpcPointer<Guid>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			lFlags = decoder.ReadInt32();
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.GetRemoteRefresher(pRefresherId, lFlags, dwClientRefrVersion, ppRemRefresher, pGuid, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppRemRefresher.value);
			encoder.WriteInterfacePointerBody(ppRemRefresher.value);
			encoder.WriteValue(pGuid.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_ReconnectRemoteRefresher(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			int lFlags;
			int lNumObjects;
			uint dwClientRefrVersion;
			_WBEM_RECONNECT_INFO[] apReconnectInfo;
			RpcPointer<_WBEM_RECONNECT_RESULTS[]> apReconnectResults;
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			lFlags = decoder.ReadInt32();
			lNumObjects = decoder.ReadInt32();
			dwClientRefrVersion = decoder.ReadUInt32();
			apReconnectInfo = decoder.ReadArrayHeader<_WBEM_RECONNECT_INFO>();
			for (int i = 0; i < apReconnectInfo.Length; i++)
			{
				_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
				elem_0 = decoder.ReadFixedStruct<_WBEM_RECONNECT_INFO>(NdrAlignment.NativePtr);
				apReconnectInfo[i] = elem_0;
			}

			for (int i = 0; i < apReconnectInfo.Length; i++)
			{
				_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
				decoder.ReadStructDeferral<_WBEM_RECONNECT_INFO>(ref elem_0);
				apReconnectInfo[i] = elem_0;
			}

			apReconnectResults = new RpcPointer<_WBEM_RECONNECT_RESULTS[]>();
			apReconnectResults.value = decoder.ReadArrayHeader<_WBEM_RECONNECT_RESULTS>();
			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				elem_0 = decoder.ReadFixedStruct<_WBEM_RECONNECT_RESULTS>(NdrAlignment._4Byte);
				apReconnectResults.value[i] = elem_0;
			}

			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				decoder.ReadStructDeferral<_WBEM_RECONNECT_RESULTS>(ref elem_0);
				apReconnectResults.value[i] = elem_0;
			}

			var invokeTask = this._obj.ReconnectRemoteRefresher(pRefresherId, lFlags, lNumObjects, dwClientRefrVersion, apReconnectInfo, apReconnectResults, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(apReconnectResults.value);
			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteFixedStruct(elem_0, NdrAlignment._4Byte);
			}

			for (int i = 0; i < apReconnectResults.value.Length; i++)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("2c9273e0-1dc3-11d3-b364-00105a1f8177");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemRefreshingServices _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemRefreshingServicesStub(IWbemRefreshingServices obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_AddObjectToRefresher, this.Invoke_AddObjectToRefresherByTemplate, this.Invoke_AddEnumToRefresher, this.Invoke_RemoveObjectFromRefresher, this.Invoke_GetRemoteRefresher, this.Invoke_ReconnectRemoteRefresher};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("423ec01e-2e35-11d2-b604-00104b703efd"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemWCOSmartEnum : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> Next(Guid proxyGUID, int lTimeout, uint uCount, RpcPointer<uint> puReturned, RpcPointer<uint> pdwBuffSize, RpcPointer<RpcPointer<byte[]>> pBuffer, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("423ec01e-2e35-11d2-b604-00104b703efd")]
	public partial class IWbemWCOSmartEnumClientProxy : IUnknownClientProxy, IWbemWCOSmartEnum
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> Next(Guid proxyGUID, int lTimeout, uint uCount, RpcPointer<uint> puReturned, RpcPointer<uint> pdwBuffSize, RpcPointer<RpcPointer<byte[]>> pBuffer, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(proxyGUID);
			encoder.WriteValue(lTimeout);
			encoder.WriteValue(uCount);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			puReturned.value = decoder.ReadUInt32();
			pdwBuffSize.value = decoder.ReadUInt32();
			pBuffer.value = decoder.ReadOutUniquePointer<byte[]>(pBuffer.value);
			if (pBuffer.value is not null)
			{
				pBuffer.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; i < pBuffer.value.value.Length; i++)
				{
					byte elem_0 = pBuffer.value.value[i];
					elem_0 = decoder.ReadByte();
					pBuffer.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemWCOSmartEnum);
		private static Guid _interfaceUuid = new Guid("423ec01e-2e35-11d2-b604-00104b703efd");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemWCOSmartEnumStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Next(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			Guid proxyGUID;
			int lTimeout;
			uint uCount;
			RpcPointer<uint> puReturned = new RpcPointer<uint>();
			RpcPointer<uint> pdwBuffSize = new RpcPointer<uint>();
			RpcPointer<RpcPointer<byte[]>> pBuffer = new RpcPointer<RpcPointer<byte[]>>();
			proxyGUID = decoder.ReadUuid();
			lTimeout = decoder.ReadInt32();
			uCount = decoder.ReadUInt32();
			var invokeTask = this._obj.Next(proxyGUID, lTimeout, uCount, puReturned, pdwBuffSize, pBuffer, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(puReturned.value);
			encoder.WriteValue(pdwBuffSize.value);
			encoder.WriteUniquePointer(pBuffer.value);
			if (pBuffer.value is not null)
			{
				encoder.WriteArrayHeader(pBuffer.value.value);
				for (int i = 0; i < pBuffer.value.value.Length; i++)
				{
					byte elem_0 = pBuffer.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("423ec01e-2e35-11d2-b604-00104b703efd");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemWCOSmartEnum _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemWCOSmartEnumStub(IWbemWCOSmartEnum obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_Next};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("1c1c45ee-4395-11d2-b60b-00104b703efd"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemFetchSmartEnum : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> GetSmartEnum(RpcPointer<TypedObjref<IWbemWCOSmartEnum>> ppSmartEnum, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("1c1c45ee-4395-11d2-b60b-00104b703efd")]
	public partial class IWbemFetchSmartEnumClientProxy : IUnknownClientProxy, IWbemFetchSmartEnum
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> GetSmartEnum(RpcPointer<TypedObjref<IWbemWCOSmartEnum>> ppSmartEnum, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppSmartEnum.value = decoder.ReadInterfacePointer<IWbemWCOSmartEnum>();
			decoder.ReadInterfacePointer(ppSmartEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemFetchSmartEnum);
		private static Guid _interfaceUuid = new Guid("1c1c45ee-4395-11d2-b60b-00104b703efd");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemFetchSmartEnumStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_GetSmartEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<TypedObjref<IWbemWCOSmartEnum>> ppSmartEnum = new RpcPointer<TypedObjref<IWbemWCOSmartEnum>>();
			var invokeTask = this._obj.GetSmartEnum(ppSmartEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppSmartEnum.value);
			encoder.WriteInterfacePointerBody(ppSmartEnum.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("1c1c45ee-4395-11d2-b60b-00104b703efd");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemFetchSmartEnum _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemFetchSmartEnumStub(IWbemFetchSmartEnum obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_GetSmartEnum};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("d4781cd6-e5d3-44df-ad94-930efe48a887"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemLoginClientID : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> SetClientInfo(string wszClientMachine, int lClientProcId, int lReserved, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("d4781cd6-e5d3-44df-ad94-930efe48a887")]
	public partial class IWbemLoginClientIDClientProxy : IUnknownClientProxy, IWbemLoginClientID
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> SetClientInfo(string wszClientMachine, int lClientProcId, int lReserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(wszClientMachine is null);
			if (wszClientMachine is not null)
				encoder.WriteWideCharString(wszClientMachine);
			encoder.WriteValue(lClientProcId);
			encoder.WriteValue(lReserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemLoginClientID);
		private static Guid _interfaceUuid = new Guid("d4781cd6-e5d3-44df-ad94-930efe48a887");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemLoginClientIDStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_SetClientInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string wszClientMachine;
			int lClientProcId;
			int lReserved;
			if (decoder.ReadReferentId() == 0)
				wszClientMachine = null;
			else
				wszClientMachine = decoder.ReadWideCharString();
			lClientProcId = decoder.ReadInt32();
			lReserved = decoder.ReadInt32();
			var invokeTask = this._obj.SetClientInfo(wszClientMachine, lClientProcId, lReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("d4781cd6-e5d3-44df-ad94-930efe48a887");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemLoginClientID _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemLoginClientIDStub(IWbemLoginClientID obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_SetClientInfo};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("f309ad18-d86a-11d0-a075-00c04fb68820"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemLevel1Login : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> EstablishPosition(string reserved1, uint reserved2, RpcPointer<uint> LocaleVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> RequestChallenge(string reserved1, string reserved2, RpcPointer<ArraySegment<byte>> reserved3, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> WBEMLogin(string reserved1, ArraySegment<byte> reserved2, int reserved3, TypedObjref<IWbemContext> reserved4, RpcPointer<TypedObjref<IWbemServices>> reserved5, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> NTLMLogin(string wszNetworkResource, string wszPreferredLocale, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemServices>> ppNamespace, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("f309ad18-d86a-11d0-a075-00c04fb68820")]
	public partial class IWbemLevel1LoginClientProxy : IUnknownClientProxy, IWbemLevel1Login
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> EstablishPosition(string reserved1, uint reserved2, RpcPointer<uint> LocaleVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(reserved1 is null);
			if (reserved1 is not null)
				encoder.WriteWideCharString(reserved1);
			encoder.WriteValue(reserved2);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			LocaleVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> RequestChallenge(string reserved1, string reserved2, RpcPointer<ArraySegment<byte>> reserved3, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(reserved1 is null);
			if (reserved1 is not null)
				encoder.WriteWideCharString(reserved1);
			encoder.WriteUniqueReferentId(reserved2 is null);
			if (reserved2 is not null)
				encoder.WriteWideCharString(reserved2);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			reserved3.value = decoder.ReadArraySegmentHeader<byte>();
			for (int i = 0; i < reserved3.value.Count; i++)
			{
				byte elem_0 = reserved3.value.Item(i);
				elem_0 = decoder.ReadUnsignedChar();
				reserved3.value.Item(i) = elem_0;
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> WBEMLogin(string reserved1, ArraySegment<byte> reserved2, int reserved3, TypedObjref<IWbemContext> reserved4, RpcPointer<TypedObjref<IWbemServices>> reserved5, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(reserved1 is null);
			if (reserved1 is not null)
				encoder.WriteWideCharString(reserved1);
			encoder.WriteUniqueReferentId(reserved2 == null);
			{
				encoder.WriteArrayHeader(reserved2, true);
				for (int i = 0; i < reserved2.Count; i++)
				{
					byte elem_0 = reserved2.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(reserved3);
			encoder.WriteInterfacePointer(reserved4);
			encoder.WriteInterfacePointerBody(reserved4);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			reserved5.value = decoder.ReadInterfacePointer<IWbemServices>();
			decoder.ReadInterfacePointer(reserved5.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> NTLMLogin(string wszNetworkResource, string wszPreferredLocale, int lFlags, TypedObjref<IWbemContext> pCtx, RpcPointer<TypedObjref<IWbemServices>> ppNamespace, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(wszNetworkResource is null);
			if (wszNetworkResource is not null)
				encoder.WriteWideCharString(wszNetworkResource);
			encoder.WriteUniqueReferentId(wszPreferredLocale is null);
			if (wszPreferredLocale is not null)
				encoder.WriteWideCharString(wszPreferredLocale);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppNamespace.value = decoder.ReadInterfacePointer<IWbemServices>();
			decoder.ReadInterfacePointer(ppNamespace.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemLevel1Login);
		private static Guid _interfaceUuid = new Guid("f309ad18-d86a-11d0-a075-00c04fb68820");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemLevel1LoginStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_EstablishPosition(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string reserved1;
			uint reserved2;
			RpcPointer<uint> LocaleVersion = new RpcPointer<uint>();
			if (decoder.ReadReferentId() == 0)
				reserved1 = null;
			else
				reserved1 = decoder.ReadWideCharString();
			reserved2 = decoder.ReadUInt32();
			var invokeTask = this._obj.EstablishPosition(reserved1, reserved2, LocaleVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(LocaleVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_RequestChallenge(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string reserved1;
			string reserved2;
			RpcPointer<ArraySegment<byte>> reserved3 = new RpcPointer<ArraySegment<byte>>();
			if (decoder.ReadReferentId() == 0)
				reserved1 = null;
			else
				reserved1 = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				reserved2 = null;
			else
				reserved2 = decoder.ReadWideCharString();
			var invokeTask = this._obj.RequestChallenge(reserved1, reserved2, reserved3, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(reserved3.value, true);
			for (int i = 0; i < reserved3.value.Count; i++)
			{
				byte elem_0 = reserved3.value.Item(i);
				encoder.WriteValue(elem_0);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_WBEMLogin(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string reserved1;
			ArraySegment<byte> reserved2;
			int reserved3;
			TypedObjref<IWbemContext> reserved4;
			RpcPointer<TypedObjref<IWbemServices>> reserved5 = new RpcPointer<TypedObjref<IWbemServices>>();
			if (decoder.ReadReferentId() == 0)
				reserved1 = null;
			else
				reserved1 = decoder.ReadWideCharString();
			reserved2 = decoder.ReadArraySegmentHeader<byte>();
			for (int i = 0; i < reserved2.Count; i++)
			{
				byte elem_0 = reserved2.Item(i);
				elem_0 = decoder.ReadUnsignedChar();
				reserved2.Item(i) = elem_0;
			}

			reserved3 = decoder.ReadInt32();
			reserved4 = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(reserved4);
			var invokeTask = this._obj.WBEMLogin(reserved1, reserved2, reserved3, reserved4, reserved5, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(reserved5.value);
			encoder.WriteInterfacePointerBody(reserved5.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_NTLMLogin(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string wszNetworkResource;
			string wszPreferredLocale;
			int lFlags;
			TypedObjref<IWbemContext> pCtx;
			RpcPointer<TypedObjref<IWbemServices>> ppNamespace = new RpcPointer<TypedObjref<IWbemServices>>();
			if (decoder.ReadReferentId() == 0)
				wszNetworkResource = null;
			else
				wszNetworkResource = decoder.ReadWideCharString();
			if (decoder.ReadReferentId() == 0)
				wszPreferredLocale = null;
			else
				wszPreferredLocale = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			var invokeTask = this._obj.NTLMLogin(wszNetworkResource, wszPreferredLocale, lFlags, pCtx, ppNamespace, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppNamespace.value);
			encoder.WriteInterfacePointerBody(ppNamespace.value);
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("f309ad18-d86a-11d0-a075-00c04fb68820");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemLevel1Login _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemLevel1LoginStub(IWbemLevel1Login obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_EstablishPosition, this.Invoke_RequestChallenge, this.Invoke_WBEMLogin, this.Invoke_NTLMLogin};
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("541679ab-2e5f-11d3-b34e-00104bcc4b4a"), RpcVersionAttribute(0, 0)]
	public partial interface IWbemLoginHelper : IUnknown
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<int> SetEvent(byte sEventToSet, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("541679ab-2e5f-11d3-b34e-00104bcc4b4a")]
	public partial class IWbemLoginHelperClientProxy : IUnknownClientProxy, IWbemLoginHelper
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> SetEvent(byte sEventToSet, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(sEventToSet);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(IWbemLoginHelper);
		private static Guid _interfaceUuid = new Guid("541679ab-2e5f-11d3-b34e-00104bcc4b4a");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class IWbemLoginHelperStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum0NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum1NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_Opnum2NotUsedOnWire(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_SetEvent(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			byte sEventToSet;
			sEventToSet = decoder.ReadUnsignedChar();
			var invokeTask = this._obj.SetEvent(sEventToSet, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("541679ab-2e5f-11d3-b34e-00104bcc4b4a");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(0, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private IWbemLoginHelper _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public IWbemLoginHelperStub(IWbemLoginHelper obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_Opnum0NotUsedOnWire, this.Invoke_Opnum1NotUsedOnWire, this.Invoke_Opnum2NotUsedOnWire, this.Invoke_SetEvent};
		}
	}
}