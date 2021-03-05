#pragma warning disable

namespace ms_wmi
{
	using ms_dcom;
	using System;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_QUERY_FLAG_TYPE : int
	{
		WBEM_FLAG_DEEP = 0,
		WBEM_FLAG_SHALLOW = 1,
		WBEM_FLAG_PROTOTYPE = 2,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_CHANGE_FLAG_TYPE : int
	{
		WBEM_FLAG_CREATE_OR_UPDATE = 0,
		WBEM_FLAG_UPDATE_ONLY = 1,
		WBEM_FLAG_CREATE_ONLY = 2,
		WBEM_FLAG_UPDATE_SAFE_MODE = 32,
		WBEM_FLAG_UPDATE_FORCE_MODE = 64,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_CONNECT_OPTIONS : int
	{
		WBEM_FLAG_CONNECT_REPOSITORY_ONLY = 64,
		WBEM_FLAG_CONNECT_PROVIDERS = 256,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
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
		WBEM_FLAG_STRONG_VALIDATION = 1048576,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_STATUS_TYPE : int
	{
		WBEM_STATUS_COMPLETE = 0,
		WBEM_STATUS_REQUIREMENTS = 1,
		WBEM_STATUS_PROGRESS = 2,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_TIMEOUT_TYPE : uint
	{
		WBEM_NO_WAIT = 0,
		WBEM_INFINITE = 4294967295,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_BACKUP_RESTORE_FLAGS : int
	{
		WBEM_FLAG_BACKUP_RESTORE_FORCE_SHUTDOWN = 1,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEMSTATUS : uint
	{
		WBEM_S_NO_ERROR = 0,
		WBEM_S_FALSE = 1,
		WBEM_S_TIMEDOUT = 262148,
		WBEM_S_NEW_STYLE = 262399,
		WBEM_S_PARTIAL_RESULTS = 262160,
		WBEM_E_FAILED = 2147749889,
		WBEM_E_NOT_FOUND = 2147749890,
		WBEM_E_ACCESS_DENIED = 2147749891,
		WBEM_E_PROVIDER_FAILURE = 2147749892,
		WBEM_E_TYPE_MISMATCH = 2147749893,
		WBEM_E_OUT_OF_MEMORY = 2147749894,
		WBEM_E_INVALID_CONTEXT = 2147749895,
		WBEM_E_INVALID_PARAMETER = 2147749896,
		WBEM_E_NOT_AVAILABLE = 2147749897,
		WBEM_E_CRITICAL_ERROR = 2147749898,
		WBEM_E_NOT_SUPPORTED = 2147749900,
		WBEM_E_PROVIDER_NOT_FOUND = 2147749905,
		WBEM_E_INVALID_PROVIDER_REGISTRATION = 2147749906,
		WBEM_E_PROVIDER_LOAD_FAILURE = 2147749907,
		WBEM_E_INITIALIZATION_FAILURE = 2147749908,
		WBEM_E_TRANSPORT_FAILURE = 2147749909,
		WBEM_E_INVALID_OPERATION = 2147749910,
		WBEM_E_ALREADY_EXISTS = 2147749913,
		WBEM_E_UNEXPECTED = 2147749917,
		WBEM_E_INCOMPLETE_CLASS = 2147749920,
		WBEM_E_SHUTTING_DOWN = 2147749939,
		E_NOTIMPL = 2147500033,
		WBEM_E_INVALID_SUPERCLASS = 2147749901,
		WBEM_E_INVALID_NAMESPACE = 2147749902,
		WBEM_E_INVALID_OBJECT = 2147749903,
		WBEM_E_INVALID_CLASS = 2147749904,
		WBEM_E_INVALID_QUERY = 2147749911,
		WBEM_E_INVALID_QUERY_TYPE = 2147749912,
		WBEM_E_PROVIDER_NOT_CAPABLE = 2147749924,
		WBEM_E_CLASS_HAS_CHILDREN = 2147749925,
		WBEM_E_CLASS_HAS_INSTANCES = 2147749926,
		WBEM_E_ILLEGAL_NULL = 2147749928,
		WBEM_E_INVALID_CIM_TYPE = 2147749933,
		WBEM_E_INVALID_METHOD = 2147749934,
		WBEM_E_INVALID_METHOD_PARAMETERS = 2147749935,
		WBEM_E_INVALID_PROPERTY = 2147749937,
		WBEM_E_CALL_CANCELLED = 2147749938,
		WBEM_E_INVALID_OBJECT_PATH = 2147749946,
		WBEM_E_OUT_OF_DISK_SPACE = 2147749947,
		WBEM_E_UNSUPPORTED_PUT_EXTENSION = 2147749949,
		WBEM_E_QUOTA_VIOLATION = 2147749996,
		WBEM_E_SERVER_TOO_BUSY = 2147749957,
		WBEM_E_METHOD_NOT_IMPLEMENTED = 2147749973,
		WBEM_E_METHOD_DISABLED = 2147749974,
		WBEM_E_UNPARSABLE_QUERY = 2147749976,
		WBEM_E_NOT_EVENT_CLASS = 2147749977,
		WBEM_E_MISSING_GROUP_WITHIN = 2147749978,
		WBEM_E_MISSING_AGGREGATION_LIST = 2147749979,
		WBEM_E_PROPERTY_NOT_AN_OBJECT = 2147749980,
		WBEM_E_AGGREGATING_BY_OBJECT = 2147749981,
		WBEM_E_BACKUP_RESTORE_WINMGMT_RUNNING = 2147749984,
		WBEM_E_QUEUE_OVERFLOW = 2147749985,
		WBEM_E_PRIVILEGE_NOT_HELD = 2147749986,
		WBEM_E_INVALID_OPERATOR = 2147749987,
		WBEM_E_CANNOT_BE_ABSTRACT = 2147749989,
		WBEM_E_AMENDED_OBJECT = 2147749990,
		WBEM_E_VETO_PUT = 2147750010,
		WBEM_E_PROVIDER_SUSPENDED = 2147750017,
		WBEM_E_ENCRYPTED_CONNECTION_REQUIRED = 2147750023,
		WBEM_E_PROVIDER_TIMED_OUT = 2147750024,
		WBEM_E_NO_KEY = 2147750025,
		WBEM_E_PROVIDER_DISABLED = 2147750026,
		WBEM_E_REGISTRATION_TOO_BROAD = 2147753985,
		WBEM_E_REGISTRATION_TOO_PRECISE = 2147753986,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_REFR_VERSION_NUMBER : int
	{
		WBEM_REFRESHER_VERSION = 2,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_INSTANCE_BLOB_TYPE : int
	{
		WBEM_BLOB_TYPE_ALL = 2,
		WBEM_BLOB_TYPE_ERROR = 3,
		WBEM_BLOB_TYPE_ENUM = 4,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct WBEM_REFRESHED_OBJECT : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lRequestId);
			encoder.WriteValue(((int)(this.m_lBlobType)));
			encoder.WriteValue(this.m_lBlobLength);
			encoder.WritePointer(this.m_pbBlob);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_lRequestId = decoder.ReadInt32();
			this.m_lBlobType = ((WBEM_INSTANCE_BLOB_TYPE)(decoder.ReadInt32()));
			this.m_lBlobLength = decoder.ReadInt32();
			this.m_pbBlob = decoder.ReadPointer<byte[]>();
		}
		public int m_lRequestId;
		public WBEM_INSTANCE_BLOB_TYPE m_lBlobType;
		public int m_lBlobLength;
		public RpcPointer<byte[]> m_pbBlob;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.m_pbBlob))
			{
				encoder.WriteArrayHeader(this.m_pbBlob.value);
				for (int i = 0; (i < this.m_pbBlob.value.Length); i++
				)
				{
					byte elem_0 = this.m_pbBlob.value[i];
					encoder.WriteValue(elem_0);
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.m_pbBlob))
			{
				this.m_pbBlob.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < this.m_pbBlob.value.Length); i++
				)
				{
					byte elem_0 = this.m_pbBlob.value[i];
					elem_0 = decoder.ReadByte();
					this.m_pbBlob.value[i] = elem_0;
				}
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct _WBEM_REFRESH_INFO_REMOTE : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteInterfacePointer(this.m_pRefresher);
			encoder.WriteInterfacePointer(this.m_pTemplate);
			encoder.WriteValue(this.m_Guid);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_pRefresher = decoder.ReadInterfacePointer<IWbemRemoteRefresher>();
			this.m_pTemplate = decoder.ReadInterfacePointer<IWbemClassObject>();
			this.m_Guid = decoder.ReadUuid();
		}
		public Titanis.DceRpc.TypedObjref<IWbemRemoteRefresher> m_pRefresher;
		public Titanis.DceRpc.TypedObjref<IWbemClassObject> m_pTemplate;
		public System.Guid m_Guid;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteInterfacePointerBody(this.m_pRefresher);
			encoder.WriteInterfacePointerBody(this.m_pTemplate);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadInterfacePointer(this.m_pRefresher);
			decoder.ReadInterfacePointer(this.m_pTemplate);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct _WBEM_REFRESH_INFO_NON_HIPERF : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.m_wszNamespace);
			encoder.WriteInterfacePointer(this.m_pTemplate);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_wszNamespace = decoder.ReadPointer<string>();
			this.m_pTemplate = decoder.ReadInterfacePointer<IWbemClassObject>();
		}
		public RpcPointer<string> m_wszNamespace;
		public Titanis.DceRpc.TypedObjref<IWbemClassObject> m_pTemplate;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.m_wszNamespace))
			{
				encoder.WriteWideCharString(this.m_wszNamespace.value);
			}
			encoder.WriteInterfacePointerBody(this.m_pTemplate);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.m_wszNamespace))
			{
				this.m_wszNamespace.value = decoder.ReadWideCharString();
			}
			decoder.ReadInterfacePointer(this.m_pTemplate);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_REFRESH_TYPE : int
	{
		WBEM_REFRESH_TYPE_INVALID = 0,
		WBEM_REFRESH_TYPE_REMOTE = 3,
		WBEM_REFRESH_TYPE_NON_HIPERF = 6,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct WBEM_REFRESH_INFO_UNION : Titanis.DceRpc.IRpcFixedStruct
	{
		public int m_lType;
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lType);
			encoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.m_lType)) == 3))
			{
				encoder.WriteFixedStruct(this.m_Remote, Titanis.DceRpc.NdrAlignment.NativePtr);
			}
			else
			{
				if ((((int)(this.m_lType)) == 6))
				{
					encoder.WriteFixedStruct(this.m_NonHiPerf, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				else
				{
					if ((((int)(this.m_lType)) == 0))
					{
						encoder.WriteValue(this.m_hres);
					}
				}
			}
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_lType = decoder.ReadInt32();
			decoder.Align(Titanis.DceRpc.NdrAlignment.NativePtr);
			if ((((int)(this.m_lType)) == 3))
			{
				this.m_Remote = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO_REMOTE>(Titanis.DceRpc.NdrAlignment.NativePtr);
			}
			else
			{
				if ((((int)(this.m_lType)) == 6))
				{
					this.m_NonHiPerf = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO_NON_HIPERF>(Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				else
				{
					if ((((int)(this.m_lType)) == 0))
					{
						this.m_hres = decoder.ReadInt32();
					}
				}
			}
		}
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((((int)(this.m_lType)) == 3))
			{
				encoder.WriteStructDeferral(this.m_Remote);
			}
			else
			{
				if ((((int)(this.m_lType)) == 6))
				{
					encoder.WriteStructDeferral(this.m_NonHiPerf);
				}
				else
				{
					if ((((int)(this.m_lType)) == 0))
					{
					}
				}
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((((int)(this.m_lType)) == 3))
			{
				decoder.ReadStructDeferral<_WBEM_REFRESH_INFO_REMOTE>(ref this.m_Remote);
			}
			else
			{
				if ((((int)(this.m_lType)) == 6))
				{
					decoder.ReadStructDeferral<_WBEM_REFRESH_INFO_NON_HIPERF>(ref this.m_NonHiPerf);
				}
				else
				{
					if ((((int)(this.m_lType)) == 0))
					{
					}
				}
			}
		}
		public _WBEM_REFRESH_INFO_REMOTE m_Remote;
		public _WBEM_REFRESH_INFO_NON_HIPERF m_NonHiPerf;
		public int m_hres;
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct _WBEM_REFRESH_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lType);
			encoder.WriteUnion(this.m_Info);
			encoder.WriteValue(this.m_lCancelId);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_lType = decoder.ReadInt32();
			this.m_Info = decoder.ReadUnion<WBEM_REFRESH_INFO_UNION>();
			this.m_lCancelId = decoder.ReadInt32();
		}
		public int m_lType;
		public WBEM_REFRESH_INFO_UNION m_Info;
		public int m_lCancelId;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.m_Info);
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<WBEM_REFRESH_INFO_UNION>(ref this.m_Info);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct _WBEM_REFRESHER_ID : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WritePointer(this.m_szMachineName);
			encoder.WriteValue(this.m_dwProcessId);
			encoder.WriteValue(this.m_guidRefresherId);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_szMachineName = decoder.ReadPointer<string>();
			this.m_dwProcessId = decoder.ReadUInt32();
			this.m_guidRefresherId = decoder.ReadUuid();
		}
		public RpcPointer<string> m_szMachineName;
		public uint m_dwProcessId;
		public System.Guid m_guidRefresherId;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.m_szMachineName))
			{
				encoder.WriteUnsignedCharString(this.m_szMachineName.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.m_szMachineName))
			{
				this.m_szMachineName.value = decoder.ReadUnsignedCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public enum WBEM_RECONNECT_TYPE : int
	{
		WBEM_RECONNECT_TYPE_OBJECT = 0,
		WBEM_RECONNECT_TYPE_ENUM = 1,
		WBEM_RECONNECT_TYPE_LAST = 2,
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct _WBEM_RECONNECT_INFO : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lType);
			encoder.WritePointer(this.m_pwcsPath);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_lType = decoder.ReadInt32();
			this.m_pwcsPath = decoder.ReadPointer<string>();
		}
		public int m_lType;
		public RpcPointer<string> m_pwcsPath;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
			if ((null != this.m_pwcsPath))
			{
				encoder.WriteWideCharString(this.m_pwcsPath.value);
			}
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
			if ((null != this.m_pwcsPath))
			{
				this.m_pwcsPath.value = decoder.ReadWideCharString();
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public struct _WBEM_RECONNECT_RESULTS : Titanis.DceRpc.IRpcFixedStruct
	{
		public void Encode(Titanis.DceRpc.IRpcEncoder encoder)
		{
			encoder.WriteValue(this.m_lId);
			encoder.WriteValue(this.m_hr);
		}
		public void Decode(Titanis.DceRpc.IRpcDecoder decoder)
		{
			this.m_lId = decoder.ReadInt32();
			this.m_hr = decoder.ReadInt32();
		}
		public int m_lId;
		public int m_hr;
		public void EncodeDeferrals(Titanis.DceRpc.IRpcEncoder encoder)
		{
		}
		public void DecodeDeferrals(Titanis.DceRpc.IRpcDecoder decoder)
		{
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("dc12a681-737f-11cf-884d-00aa004b2e24")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemClassObject : IUnknown
	{
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("dc12a681-737f-11cf-884d-00aa004b2e24")]
	public class IWbemClassObjectClientProxy : IUnknownClientProxy, IWbemClassObject
	{
		private static System.Guid _interfaceUuid = new System.Guid("dc12a681-737f-11cf-884d-00aa004b2e24");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemClassObjectStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("dc12a681-737f-11cf-884d-00aa004b2e24");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemClassObject _obj;
		public IWbemClassObjectStub(IWbemClassObject obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("7c857801-7381-11cf-884d-00aa004b2e24")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemObjectSink : IUnknown
	{
		Task<int> Indicate(int lObjectCount, Titanis.DceRpc.TypedObjref<IWbemClassObject>[] apObjArray, System.Threading.CancellationToken cancellationToken);
		Task<int> SetStatus(int lFlags, int hResult, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strParam, Titanis.DceRpc.TypedObjref<IWbemClassObject> pObjParam, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("7c857801-7381-11cf-884d-00aa004b2e24")]
	public class IWbemObjectSinkClientProxy : IUnknownClientProxy, IWbemObjectSink
	{
		private static System.Guid _interfaceUuid = new System.Guid("7c857801-7381-11cf-884d-00aa004b2e24");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> Indicate(int lObjectCount, Titanis.DceRpc.TypedObjref<IWbemClassObject>[] apObjArray, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lObjectCount);
			if ((apObjArray != null))
			{
				encoder.WriteArrayHeader(apObjArray);
				for (int i = 0; (i < apObjArray.Length); i++
				)
				{
					Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
					encoder.WriteInterfacePointer(elem_0);
				}
			}
			for (int i = 0; (i < apObjArray.Length); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
				encoder.WriteInterfacePointerBody(elem_0);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> SetStatus(int lFlags, int hResult, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strParam, Titanis.DceRpc.TypedObjref<IWbemClassObject> pObjParam, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			encoder.WriteValue(hResult);
			encoder.WritePointer(strParam);
			if ((null != strParam))
			{
				encoder.WriteConformantStruct(strParam.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strParam.value);
			}
			encoder.WriteInterfacePointer(pObjParam);
			encoder.WriteInterfacePointerBody(pObjParam);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemObjectSinkStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("7c857801-7381-11cf-884d-00aa004b2e24");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemObjectSink _obj;
		public IWbemObjectSinkStub(IWbemObjectSink obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_Indicate,
					this.Invoke_SetStatus};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Indicate(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lObjectCount;
			Titanis.DceRpc.TypedObjref<IWbemClassObject>[] apObjArray;
			lObjectCount = decoder.ReadInt32();
			apObjArray = decoder.ReadArrayHeader<Titanis.DceRpc.TypedObjref<IWbemClassObject>>();
			for (int i = 0; (i < apObjArray.Length); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
				elem_0 = decoder.ReadInterfacePointer<IWbemClassObject>();
				apObjArray[i] = elem_0;
			}
			for (int i = 0; (i < apObjArray.Length); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjArray[i];
				decoder.ReadInterfacePointer(elem_0);
				apObjArray[i] = elem_0;
			}
			var invokeTask = this._obj.Indicate(lObjectCount, apObjArray, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_SetStatus(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lFlags;
			int hResult;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strParam;
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pObjParam;
			lFlags = decoder.ReadInt32();
			hResult = decoder.ReadInt32();
			strParam = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strParam))
			{
				strParam.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strParam.value);
			}
			pObjParam = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pObjParam);
			var invokeTask = this._obj.SetStatus(lFlags, hResult, strParam, pObjParam, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("027947e1-d731-11ce-a357-000000000001")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IEnumWbemClassObject : IUnknown
	{
		Task<int> Reset(System.Threading.CancellationToken cancellationToken);
		Task<int> Next(int lTimeout, uint uCount, RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<IWbemClassObject>>> apObjects, RpcPointer<uint> puReturned, System.Threading.CancellationToken cancellationToken);
		Task<int> NextAsync(uint uCount, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pSink, System.Threading.CancellationToken cancellationToken);
		Task<int> Clone(RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken);
		Task<int> Skip(int lTimeout, uint nCount, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("027947e1-d731-11ce-a357-000000000001")]
	public class IEnumWbemClassObjectClientProxy : IUnknownClientProxy, IEnumWbemClassObject
	{
		private static System.Guid _interfaceUuid = new System.Guid("027947e1-d731-11ce-a357-000000000001");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> Reset(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> Next(int lTimeout, uint uCount, RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<IWbemClassObject>>> apObjects, RpcPointer<uint> puReturned, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			encoder.WriteValue(uCount);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			apObjects.value = decoder.ReadArraySegmentHeader<Titanis.DceRpc.TypedObjref<IWbemClassObject>>();
			for (int i = 0; (i < apObjects.value.Count); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				elem_0 = decoder.ReadInterfacePointer<IWbemClassObject>();
				apObjects.value.Item(i) = elem_0;
			}
			for (int i = 0; (i < apObjects.value.Count); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				decoder.ReadInterfacePointer(elem_0);
				apObjects.value.Item(i) = elem_0;
			}
			puReturned.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> NextAsync(uint uCount, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pSink, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(uCount);
			encoder.WriteInterfacePointer(pSink);
			encoder.WriteInterfacePointerBody(pSink);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> Clone(RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> Skip(int lTimeout, uint nCount, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			encoder.WriteValue(nCount);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IEnumWbemClassObjectStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("027947e1-d731-11ce-a357-000000000001");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IEnumWbemClassObject _obj;
		public IEnumWbemClassObjectStub(IEnumWbemClassObject obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_Reset,
					this.Invoke_Next,
					this.Invoke_NextAsync,
					this.Invoke_Clone,
					this.Invoke_Skip};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Reset(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Reset(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Next(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lTimeout;
			uint uCount;
			RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<IWbemClassObject>>> apObjects = new RpcPointer<ArraySegment<Titanis.DceRpc.TypedObjref<IWbemClassObject>>>();
			RpcPointer<uint> puReturned = new RpcPointer<uint>();
			lTimeout = decoder.ReadInt32();
			uCount = decoder.ReadUInt32();
			var invokeTask = this._obj.Next(lTimeout, uCount, apObjects, puReturned, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(apObjects.value, true);
			for (int i = 0; (i < apObjects.value.Count); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				encoder.WriteInterfacePointer(elem_0);
			}
			for (int i = 0; (i < apObjects.value.Count); i++
			)
			{
				Titanis.DceRpc.TypedObjref<IWbemClassObject> elem_0 = apObjects.value.Item(i);
				encoder.WriteInterfacePointerBody(elem_0);
			}
			encoder.WriteValue(puReturned.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_NextAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			uint uCount;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pSink;
			uCount = decoder.ReadUInt32();
			pSink = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pSink);
			var invokeTask = this._obj.NextAsync(uCount, pSink, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Clone(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>>();
			var invokeTask = this._obj.Clone(ppEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppEnum.value);
			encoder.WriteInterfacePointerBody(ppEnum.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Skip(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lTimeout;
			uint nCount;
			lTimeout = decoder.ReadInt32();
			nCount = decoder.ReadUInt32();
			var invokeTask = this._obj.Skip(lTimeout, nCount, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("44aca674-e8fc-11d0-a07c-00c04fb68820")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemContext : IUnknown
	{
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("44aca674-e8fc-11d0-a07c-00c04fb68820")]
	public class IWbemContextClientProxy : IUnknownClientProxy, IWbemContext
	{
		private static System.Guid _interfaceUuid = new System.Guid("44aca674-e8fc-11d0-a07c-00c04fb68820");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemContextStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("44aca674-e8fc-11d0-a07c-00c04fb68820");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemContext _obj;
		public IWbemContextStub(IWbemContext obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("44aca675-e8fc-11d0-a07c-00c04fb68820")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemCallResult : IUnknown
	{
		Task<int> GetResultObject(int lTimeout, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppResultObject, System.Threading.CancellationToken cancellationToken);
		Task<int> GetResultString(int lTimeout, RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>> pstrResultString, System.Threading.CancellationToken cancellationToken);
		Task<int> GetResultServices(int lTimeout, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppServices, System.Threading.CancellationToken cancellationToken);
		Task<int> GetCallStatus(int lTimeout, RpcPointer<int> plStatus, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("44aca675-e8fc-11d0-a07c-00c04fb68820")]
	public class IWbemCallResultClientProxy : IUnknownClientProxy, IWbemCallResult
	{
		private static System.Guid _interfaceUuid = new System.Guid("44aca675-e8fc-11d0-a07c-00c04fb68820");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> GetResultObject(int lTimeout, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppResultObject, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppResultObject.value = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(ppResultObject.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> GetResultString(int lTimeout, RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>> pstrResultString, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			pstrResultString.value = decoder.ReadOutPointer<ms_oaut.FLAGGED_WORD_BLOB>(pstrResultString.value);
			if ((null != pstrResultString.value))
			{
				pstrResultString.value.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref pstrResultString.value.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> GetResultServices(int lTimeout, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppServices, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppServices.value = decoder.ReadInterfacePointer<IWbemServices>();
			decoder.ReadInterfacePointer(ppServices.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> GetCallStatus(int lTimeout, RpcPointer<int> plStatus, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lTimeout);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			plStatus.value = decoder.ReadInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemCallResultStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("44aca675-e8fc-11d0-a07c-00c04fb68820");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemCallResult _obj;
		public IWbemCallResultStub(IWbemCallResult obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_GetResultObject,
					this.Invoke_GetResultString,
					this.Invoke_GetResultServices,
					this.Invoke_GetCallStatus};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetResultObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppResultObject = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetResultObject(lTimeout, ppResultObject, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppResultObject.value);
			encoder.WriteInterfacePointerBody(ppResultObject.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetResultString(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>> pstrResultString = new RpcPointer<RpcPointer<ms_oaut.FLAGGED_WORD_BLOB>>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetResultString(lTimeout, pstrResultString, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(pstrResultString.value);
			if ((null != pstrResultString.value))
			{
				encoder.WriteConformantStruct(pstrResultString.value.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(pstrResultString.value.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetResultServices(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppServices = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetResultServices(lTimeout, ppServices, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppServices.value);
			encoder.WriteInterfacePointerBody(ppServices.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetCallStatus(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lTimeout;
			RpcPointer<int> plStatus = new RpcPointer<int>();
			lTimeout = decoder.ReadInt32();
			var invokeTask = this._obj.GetCallStatus(lTimeout, plStatus, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(plStatus.value);
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("9556dc99-828c-11cf-a37e-00aa003240c7")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemServices : IUnknown
	{
		Task<int> OpenNamespace(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strNamespace, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppWorkingNamespace, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppResult, System.Threading.CancellationToken cancellationToken);
		Task<int> CancelAsyncCall(Titanis.DceRpc.TypedObjref<IWbemObjectSink> pSink, System.Threading.CancellationToken cancellationToken);
		Task<int> QueryObjectSink(int lFlags, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemObjectSink>> ppResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> GetObject(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppObject, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken);
		Task<int> GetObjectAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> PutClass(Titanis.DceRpc.TypedObjref<IWbemClassObject> pObject, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken);
		Task<int> PutClassAsync(Titanis.DceRpc.TypedObjref<IWbemClassObject> pObject, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> DeleteClass(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken);
		Task<int> DeleteClassAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> CreateClassEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken);
		Task<int> CreateClassEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> PutInstance(Titanis.DceRpc.TypedObjref<IWbemClassObject> pInst, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken);
		Task<int> PutInstanceAsync(Titanis.DceRpc.TypedObjref<IWbemClassObject> pInst, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> DeleteInstance(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken);
		Task<int> DeleteInstanceAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> CreateInstanceEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken);
		Task<int> CreateInstanceEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> ExecQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken);
		Task<int> ExecQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> ExecNotificationQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken);
		Task<int> ExecNotificationQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
		Task<int> ExecMethod(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemClassObject> pInParams, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppOutParams, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken);
		Task<int> ExecMethodAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemClassObject> pInParams, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("9556dc99-828c-11cf-a37e-00aa003240c7")]
	public class IWbemServicesClientProxy : IUnknownClientProxy, IWbemServices
	{
		private static System.Guid _interfaceUuid = new System.Guid("9556dc99-828c-11cf-a37e-00aa003240c7");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> OpenNamespace(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strNamespace, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppWorkingNamespace, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strNamespace);
			if ((null != strNamespace))
			{
				encoder.WriteConformantStruct(strNamespace.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strNamespace.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WritePointer(ppWorkingNamespace);
			if ((null != ppWorkingNamespace))
			{
				encoder.WriteInterfacePointer(ppWorkingNamespace.value);
				encoder.WriteInterfacePointerBody(ppWorkingNamespace.value);
			}
			encoder.WritePointer(ppResult);
			if ((null != ppResult))
			{
				encoder.WriteInterfacePointer(ppResult.value);
				encoder.WriteInterfacePointerBody(ppResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppWorkingNamespace = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemServices>>(ppWorkingNamespace);
			if ((null != ppWorkingNamespace))
			{
				ppWorkingNamespace.value = decoder.ReadInterfacePointer<IWbemServices>();
				decoder.ReadInterfacePointer(ppWorkingNamespace.value);
			}
			ppResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppResult);
			if ((null != ppResult))
			{
				ppResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> CancelAsyncCall(Titanis.DceRpc.TypedObjref<IWbemObjectSink> pSink, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pSink);
			encoder.WriteInterfacePointerBody(pSink);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> QueryObjectSink(int lFlags, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemObjectSink>> ppResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppResponseHandler.value = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(ppResponseHandler.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> GetObject(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppObject, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strObjectPath);
			if ((null != strObjectPath))
			{
				encoder.WriteConformantStruct(strObjectPath.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WritePointer(ppObject);
			if ((null != ppObject))
			{
				encoder.WriteInterfacePointer(ppObject.value);
				encoder.WriteInterfacePointerBody(ppObject.value);
			}
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppObject = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>>(ppObject);
			if ((null != ppObject))
			{
				ppObject.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppObject.value);
			}
			ppCallResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppCallResult);
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> GetObjectAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strObjectPath);
			if ((null != strObjectPath))
			{
				encoder.WriteConformantStruct(strObjectPath.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> PutClass(Titanis.DceRpc.TypedObjref<IWbemClassObject> pObject, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pObject);
			encoder.WriteInterfacePointerBody(pObject);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppCallResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppCallResult);
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> PutClassAsync(Titanis.DceRpc.TypedObjref<IWbemClassObject> pObject, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pObject);
			encoder.WriteInterfacePointerBody(pObject);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> DeleteClass(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strClass);
			if ((null != strClass))
			{
				encoder.WriteConformantStruct(strClass.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strClass.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppCallResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppCallResult);
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> DeleteClassAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strClass);
			if ((null != strClass))
			{
				encoder.WriteConformantStruct(strClass.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strClass.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> CreateClassEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strSuperclass);
			if ((null != strSuperclass))
			{
				encoder.WriteConformantStruct(strSuperclass.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperclass.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> CreateClassEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strSuperclass);
			if ((null != strSuperclass))
			{
				encoder.WriteConformantStruct(strSuperclass.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperclass.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> PutInstance(Titanis.DceRpc.TypedObjref<IWbemClassObject> pInst, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pInst);
			encoder.WriteInterfacePointerBody(pInst);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppCallResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppCallResult);
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> PutInstanceAsync(Titanis.DceRpc.TypedObjref<IWbemClassObject> pInst, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteInterfacePointer(pInst);
			encoder.WriteInterfacePointerBody(pInst);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> DeleteInstance(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strObjectPath);
			if ((null != strObjectPath))
			{
				encoder.WriteConformantStruct(strObjectPath.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppCallResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppCallResult);
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> DeleteInstanceAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strObjectPath);
			if ((null != strObjectPath))
			{
				encoder.WriteConformantStruct(strObjectPath.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> CreateInstanceEnum(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strSuperClass);
			if ((null != strSuperClass))
			{
				encoder.WriteConformantStruct(strSuperClass.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperClass.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> CreateInstanceEnumAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strSuperClass);
			if ((null != strSuperClass))
			{
				encoder.WriteConformantStruct(strSuperClass.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strSuperClass.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ExecQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strQueryLanguage);
			if ((null != strQueryLanguage))
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}
			encoder.WritePointer(strQuery);
			if ((null != strQuery))
			{
				encoder.WriteConformantStruct(strQuery.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ExecQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strQueryLanguage);
			if ((null != strQueryLanguage))
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}
			encoder.WritePointer(strQuery);
			if ((null != strQuery))
			{
				encoder.WriteConformantStruct(strQuery.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ExecNotificationQuery(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strQueryLanguage);
			if ((null != strQueryLanguage))
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}
			encoder.WritePointer(strQuery);
			if ((null != strQuery))
			{
				encoder.WriteConformantStruct(strQuery.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppEnum.value = decoder.ReadInterfacePointer<IEnumWbemClassObject>();
			decoder.ReadInterfacePointer(ppEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ExecNotificationQueryAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strQueryLanguage);
			if ((null != strQueryLanguage))
			{
				encoder.WriteConformantStruct(strQueryLanguage.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQueryLanguage.value);
			}
			encoder.WritePointer(strQuery);
			if ((null != strQuery))
			{
				encoder.WriteConformantStruct(strQuery.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strQuery.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ExecMethod(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemClassObject> pInParams, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppOutParams, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strObjectPath);
			if ((null != strObjectPath))
			{
				encoder.WriteConformantStruct(strObjectPath.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}
			encoder.WritePointer(strMethodName);
			if ((null != strMethodName))
			{
				encoder.WriteConformantStruct(strMethodName.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strMethodName.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pInParams);
			encoder.WriteInterfacePointerBody(pInParams);
			encoder.WritePointer(ppOutParams);
			if ((null != ppOutParams))
			{
				encoder.WriteInterfacePointer(ppOutParams.value);
				encoder.WriteInterfacePointerBody(ppOutParams.value);
			}
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppOutParams = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>>(ppOutParams);
			if ((null != ppOutParams))
			{
				ppOutParams.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppOutParams.value);
			}
			ppCallResult = decoder.ReadOutPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>(ppCallResult);
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ExecMethodAsync(RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath, RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, Titanis.DceRpc.TypedObjref<IWbemClassObject> pInParams, Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WritePointer(strObjectPath);
			if ((null != strObjectPath))
			{
				encoder.WriteConformantStruct(strObjectPath.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strObjectPath.value);
			}
			encoder.WritePointer(strMethodName);
			if ((null != strMethodName))
			{
				encoder.WriteConformantStruct(strMethodName.value, Titanis.DceRpc.NdrAlignment._4Byte);
				encoder.WriteStructDeferral(strMethodName.value);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			encoder.WriteInterfacePointer(pInParams);
			encoder.WriteInterfacePointerBody(pInParams);
			encoder.WriteInterfacePointer(pResponseHandler);
			encoder.WriteInterfacePointerBody(pResponseHandler);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemServicesStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("9556dc99-828c-11cf-a37e-00aa003240c7");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemServices _obj;
		public IWbemServicesStub(IWbemServices obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_OpenNamespace,
					this.Invoke_CancelAsyncCall,
					this.Invoke_QueryObjectSink,
					this.Invoke_GetObject,
					this.Invoke_GetObjectAsync,
					this.Invoke_PutClass,
					this.Invoke_PutClassAsync,
					this.Invoke_DeleteClass,
					this.Invoke_DeleteClassAsync,
					this.Invoke_CreateClassEnum,
					this.Invoke_CreateClassEnumAsync,
					this.Invoke_PutInstance,
					this.Invoke_PutInstanceAsync,
					this.Invoke_DeleteInstance,
					this.Invoke_DeleteInstanceAsync,
					this.Invoke_CreateInstanceEnum,
					this.Invoke_CreateInstanceEnumAsync,
					this.Invoke_ExecQuery,
					this.Invoke_ExecQueryAsync,
					this.Invoke_ExecNotificationQuery,
					this.Invoke_ExecNotificationQueryAsync,
					this.Invoke_ExecMethod,
					this.Invoke_ExecMethodAsync};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_OpenNamespace(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strNamespace;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppWorkingNamespace;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppResult;
			strNamespace = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strNamespace))
			{
				strNamespace.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strNamespace.value);
			}
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppWorkingNamespace = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemServices>>();
			if ((null != ppWorkingNamespace))
			{
				ppWorkingNamespace.value = decoder.ReadInterfacePointer<IWbemServices>();
				decoder.ReadInterfacePointer(ppWorkingNamespace.value);
			}
			ppResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppResult))
			{
				ppResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppResult.value);
			}
			var invokeTask = this._obj.OpenNamespace(strNamespace, lFlags, pCtx, ppWorkingNamespace, ppResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppWorkingNamespace);
			if ((null != ppWorkingNamespace))
			{
				encoder.WriteInterfacePointer(ppWorkingNamespace.value);
				encoder.WriteInterfacePointerBody(ppWorkingNamespace.value);
			}
			encoder.WritePointer(ppResult);
			if ((null != ppResult))
			{
				encoder.WriteInterfacePointer(ppResult.value);
				encoder.WriteInterfacePointerBody(ppResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_CancelAsyncCall(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pSink;
			pSink = decoder.ReadInterfacePointer<IWbemObjectSink>();
			decoder.ReadInterfacePointer(pSink);
			var invokeTask = this._obj.CancelAsyncCall(pSink, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_QueryObjectSink(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lFlags;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemObjectSink>> ppResponseHandler = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemObjectSink>>();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.QueryObjectSink(lFlags, ppResponseHandler, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppResponseHandler.value);
			encoder.WriteInterfacePointerBody(ppResponseHandler.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetObject(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppObject;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult;
			strObjectPath = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strObjectPath))
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppObject = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>>();
			if ((null != ppObject))
			{
				ppObject.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppObject.value);
			}
			ppCallResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			var invokeTask = this._obj.GetObject(strObjectPath, lFlags, pCtx, ppObject, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppObject);
			if ((null != ppObject))
			{
				encoder.WriteInterfacePointer(ppObject.value);
				encoder.WriteInterfacePointerBody(ppObject.value);
			}
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetObjectAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strObjectPath = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strObjectPath))
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_PutClass(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pObject;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult;
			pObject = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pObject);
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			var invokeTask = this._obj.PutClass(pObject, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_PutClassAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pObject;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
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
		private async Task Invoke_DeleteClass(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult;
			strClass = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strClass))
			{
				strClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strClass.value);
			}
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			var invokeTask = this._obj.DeleteClass(strClass, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_DeleteClassAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strClass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strClass = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strClass))
			{
				strClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_CreateClassEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>>();
			strSuperclass = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strSuperclass))
			{
				strSuperclass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_CreateClassEnumAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperclass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strSuperclass = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strSuperclass))
			{
				strSuperclass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_PutInstance(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pInst;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult;
			pInst = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pInst);
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			var invokeTask = this._obj.PutInstance(pInst, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_PutInstanceAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pInst;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
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
		private async Task Invoke_DeleteInstance(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult;
			strObjectPath = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strObjectPath))
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			ppCallResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			var invokeTask = this._obj.DeleteInstance(strObjectPath, lFlags, pCtx, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_DeleteInstanceAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strObjectPath = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strObjectPath))
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_CreateInstanceEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>>();
			strSuperClass = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strSuperClass))
			{
				strSuperClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_CreateInstanceEnumAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strSuperClass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strSuperClass = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strSuperClass))
			{
				strSuperClass.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_ExecQuery(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>>();
			strQueryLanguage = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQueryLanguage))
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}
			strQuery = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQuery))
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_ExecQueryAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strQueryLanguage = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQueryLanguage))
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}
			strQuery = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQuery))
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_ExecNotificationQuery(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>> ppEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IEnumWbemClassObject>>();
			strQueryLanguage = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQueryLanguage))
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}
			strQuery = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQuery))
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_ExecNotificationQueryAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQueryLanguage;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strQuery;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strQueryLanguage = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQueryLanguage))
			{
				strQueryLanguage.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strQueryLanguage.value);
			}
			strQuery = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strQuery))
			{
				strQuery.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
		private async Task Invoke_ExecMethod(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pInParams;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>> ppOutParams;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>> ppCallResult;
			strObjectPath = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strObjectPath))
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}
			strMethodName = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strMethodName))
			{
				strMethodName.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strMethodName.value);
			}
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			pInParams = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pInParams);
			ppOutParams = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemClassObject>>();
			if ((null != ppOutParams))
			{
				ppOutParams.value = decoder.ReadInterfacePointer<IWbemClassObject>();
				decoder.ReadInterfacePointer(ppOutParams.value);
			}
			ppCallResult = decoder.ReadPointer<Titanis.DceRpc.TypedObjref<IWbemCallResult>>();
			if ((null != ppCallResult))
			{
				ppCallResult.value = decoder.ReadInterfacePointer<IWbemCallResult>();
				decoder.ReadInterfacePointer(ppCallResult.value);
			}
			var invokeTask = this._obj.ExecMethod(strObjectPath, strMethodName, lFlags, pCtx, pInParams, ppOutParams, ppCallResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WritePointer(ppOutParams);
			if ((null != ppOutParams))
			{
				encoder.WriteInterfacePointer(ppOutParams.value);
				encoder.WriteInterfacePointerBody(ppOutParams.value);
			}
			encoder.WritePointer(ppCallResult);
			if ((null != ppCallResult))
			{
				encoder.WriteInterfacePointer(ppCallResult.value);
				encoder.WriteInterfacePointerBody(ppCallResult.value);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_ExecMethodAsync(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strObjectPath;
			RpcPointer<ms_oaut.FLAGGED_WORD_BLOB> strMethodName;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pInParams;
			Titanis.DceRpc.TypedObjref<IWbemObjectSink> pResponseHandler;
			strObjectPath = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strObjectPath))
			{
				strObjectPath.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_oaut.FLAGGED_WORD_BLOB>(ref strObjectPath.value);
			}
			strMethodName = decoder.ReadPointer<ms_oaut.FLAGGED_WORD_BLOB>();
			if ((null != strMethodName))
			{
				strMethodName.value = decoder.ReadConformantStruct<ms_oaut.FLAGGED_WORD_BLOB>(Titanis.DceRpc.NdrAlignment._4Byte);
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
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("c49e32c7-bc8b-11d2-85d4-00105a1f8304")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemBackupRestore : IUnknown
	{
		Task<int> Backup(string strBackupToFile, int lFlags, System.Threading.CancellationToken cancellationToken);
		Task<int> Restore(string strRestoreFromFile, int lFlags, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("c49e32c7-bc8b-11d2-85d4-00105a1f8304")]
	public class IWbemBackupRestoreClientProxy : IUnknownClientProxy, IWbemBackupRestore
	{
		private static System.Guid _interfaceUuid = new System.Guid("c49e32c7-bc8b-11d2-85d4-00105a1f8304");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> Backup(string strBackupToFile, int lFlags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(strBackupToFile);
			encoder.WriteValue(lFlags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> Restore(string strRestoreFromFile, int lFlags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteWideCharString(strRestoreFromFile);
			encoder.WriteValue(lFlags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemBackupRestoreStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("c49e32c7-bc8b-11d2-85d4-00105a1f8304");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemBackupRestore _obj;
		public IWbemBackupRestoreStub(IWbemBackupRestore obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_Backup,
					this.Invoke_Restore};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Backup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string strBackupToFile;
			int lFlags;
			strBackupToFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Backup(strBackupToFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Restore(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string strRestoreFromFile;
			int lFlags;
			strRestoreFromFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Restore(strRestoreFromFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("a359dec5-e813-4834-8a2a-ba7f1d777d76")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemBackupRestoreEx : IWbemBackupRestore
	{
		Task<int> Pause(System.Threading.CancellationToken cancellationToken);
		Task<int> Resume(System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("a359dec5-e813-4834-8a2a-ba7f1d777d76")]
	public class IWbemBackupRestoreExClientProxy : IWbemBackupRestoreClientProxy, IWbemBackupRestoreEx
	{
		private static System.Guid _interfaceUuid = new System.Guid("a359dec5-e813-4834-8a2a-ba7f1d777d76");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> Pause(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> Resume(System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemBackupRestoreExStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("a359dec5-e813-4834-8a2a-ba7f1d777d76");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemBackupRestoreEx _obj;
		public IWbemBackupRestoreExStub(IWbemBackupRestoreEx obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_Backup,
					this.Invoke_Restore,
					this.Invoke_Pause,
					this.Invoke_Resume};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Backup(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string strBackupToFile;
			int lFlags;
			strBackupToFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Backup(strBackupToFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Restore(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string strRestoreFromFile;
			int lFlags;
			strRestoreFromFile = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Restore(strRestoreFromFile, lFlags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Pause(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Pause(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Resume(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Resume(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("f1e9c5b2-f59b-11d2-b362-00105a1f8177")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemRemoteRefresher : IUnknown
	{
		Task<int> RemoteRefresh(int lFlags, RpcPointer<int> plNumObjects, RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>> paObjects, System.Threading.CancellationToken cancellationToken);
		Task<int> StopRefreshing(int lNumIds, int[] aplIds, int lFlags, System.Threading.CancellationToken cancellationToken);
		Task<int> Opnum5NotUsedOnWire(int lFlags, RpcPointer<System.Guid> pGuid, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("f1e9c5b2-f59b-11d2-b362-00105a1f8177")]
	public class IWbemRemoteRefresherClientProxy : IUnknownClientProxy, IWbemRemoteRefresher
	{
		private static System.Guid _interfaceUuid = new System.Guid("f1e9c5b2-f59b-11d2-b362-00105a1f8177");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> RemoteRefresh(int lFlags, RpcPointer<int> plNumObjects, RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>> paObjects, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			plNumObjects.value = decoder.ReadInt32();
			paObjects.value = decoder.ReadOutPointer<WBEM_REFRESHED_OBJECT[]>(paObjects.value);
			if ((null != paObjects.value))
			{
				paObjects.value.value = decoder.ReadArrayHeader<WBEM_REFRESHED_OBJECT>();
				for (int i = 0; (i < paObjects.value.value.Length); i++
				)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					elem_0 = decoder.ReadFixedStruct<WBEM_REFRESHED_OBJECT>(Titanis.DceRpc.NdrAlignment.NativePtr);
					paObjects.value.value[i] = elem_0;
				}
				for (int i = 0; (i < paObjects.value.value.Length); i++
				)
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
		public virtual async Task<int> StopRefreshing(int lNumIds, int[] aplIds, int lFlags, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lNumIds);
			if ((aplIds != null))
			{
				encoder.WriteArrayHeader(aplIds);
				for (int i = 0; (i < aplIds.Length); i++
				)
				{
					int elem_0 = aplIds[i];
					encoder.WriteValue(elem_0);
				}
			}
			encoder.WriteValue(lFlags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> Opnum5NotUsedOnWire(int lFlags, RpcPointer<System.Guid> pGuid, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(lFlags);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			pGuid.value = decoder.ReadUuid();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemRemoteRefresherStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("f1e9c5b2-f59b-11d2-b362-00105a1f8177");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemRemoteRefresher _obj;
		public IWbemRemoteRefresherStub(IWbemRemoteRefresher obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_RemoteRefresh,
					this.Invoke_StopRefreshing,
					this.Invoke_Opnum5NotUsedOnWire};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_RemoteRefresh(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lFlags;
			RpcPointer<int> plNumObjects = new RpcPointer<int>();
			RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>> paObjects = new RpcPointer<RpcPointer<WBEM_REFRESHED_OBJECT[]>>();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.RemoteRefresh(lFlags, plNumObjects, paObjects, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(plNumObjects.value);
			encoder.WritePointer(paObjects.value);
			if ((null != paObjects.value))
			{
				encoder.WriteArrayHeader(paObjects.value.value);
				for (int i = 0; (i < paObjects.value.value.Length); i++
				)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
				for (int i = 0; (i < paObjects.value.value.Length); i++
				)
				{
					WBEM_REFRESHED_OBJECT elem_0 = paObjects.value.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_StopRefreshing(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lNumIds;
			int[] aplIds;
			int lFlags;
			lNumIds = decoder.ReadInt32();
			aplIds = decoder.ReadArrayHeader<int>();
			for (int i = 0; (i < aplIds.Length); i++
			)
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
		private async Task Invoke_Opnum5NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			int lFlags;
			RpcPointer<System.Guid> pGuid = new RpcPointer<System.Guid>();
			lFlags = decoder.ReadInt32();
			var invokeTask = this._obj.Opnum5NotUsedOnWire(lFlags, pGuid, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pGuid.value);
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("2c9273e0-1dc3-11d3-b364-00105a1f8177")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemRefreshingServices : IUnknown
	{
		Task<int> AddObjectToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken);
		Task<int> AddObjectToRefresherByTemplate(_WBEM_REFRESHER_ID pRefresherId, Titanis.DceRpc.TypedObjref<IWbemClassObject> pTemplate, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken);
		Task<int> AddEnumToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken);
		Task<int> RemoveObjectFromRefresher(_WBEM_REFRESHER_ID pRefresherId, int lId, int lFlags, uint dwClientRefrVersion, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken);
		Task<int> GetRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, uint dwClientRefrVersion, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemRemoteRefresher>> ppRemRefresher, RpcPointer<System.Guid> pGuid, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken);
		Task<int> ReconnectRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, int lNumObjects, uint dwClientRefrVersion, _WBEM_RECONNECT_INFO[] apReconnectInfo, RpcPointer<_WBEM_RECONNECT_RESULTS[]> apReconnectResults, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("2c9273e0-1dc3-11d3-b364-00105a1f8177")]
	public class IWbemRefreshingServicesClientProxy : IUnknownClientProxy, IWbemRefreshingServices
	{
		private static System.Guid _interfaceUuid = new System.Guid("2c9273e0-1dc3-11d3-b364-00105a1f8177");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> AddObjectToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszPath, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteWideCharString(wszPath);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pContext);
			encoder.WriteInterfacePointerBody(pContext);
			encoder.WriteValue(dwClientRefrVersion);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			pInfo.value = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESH_INFO>(ref pInfo.value);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> AddObjectToRefresherByTemplate(_WBEM_REFRESHER_ID pRefresherId, Titanis.DceRpc.TypedObjref<IWbemClassObject> pTemplate, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteInterfacePointer(pTemplate);
			encoder.WriteInterfacePointerBody(pTemplate);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pContext);
			encoder.WriteInterfacePointerBody(pContext);
			encoder.WriteValue(dwClientRefrVersion);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			pInfo.value = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESH_INFO>(ref pInfo.value);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> AddEnumToRefresher(_WBEM_REFRESHER_ID pRefresherId, string wszClass, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pContext, uint dwClientRefrVersion, RpcPointer<_WBEM_REFRESH_INFO> pInfo, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteWideCharString(wszClass);
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pContext);
			encoder.WriteInterfacePointerBody(pContext);
			encoder.WriteValue(dwClientRefrVersion);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			pInfo.value = decoder.ReadFixedStruct<_WBEM_REFRESH_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESH_INFO>(ref pInfo.value);
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> RemoveObjectFromRefresher(_WBEM_REFRESHER_ID pRefresherId, int lId, int lFlags, uint dwClientRefrVersion, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteValue(lId);
			encoder.WriteValue(lFlags);
			encoder.WriteValue(dwClientRefrVersion);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> GetRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, uint dwClientRefrVersion, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemRemoteRefresher>> ppRemRefresher, RpcPointer<System.Guid> pGuid, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteValue(lFlags);
			encoder.WriteValue(dwClientRefrVersion);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppRemRefresher.value = decoder.ReadInterfacePointer<IWbemRemoteRefresher>();
			decoder.ReadInterfacePointer(ppRemRefresher.value);
			pGuid.value = decoder.ReadUuid();
			pdwSvrRefrVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> ReconnectRemoteRefresher(_WBEM_REFRESHER_ID pRefresherId, int lFlags, int lNumObjects, uint dwClientRefrVersion, _WBEM_RECONNECT_INFO[] apReconnectInfo, RpcPointer<_WBEM_RECONNECT_RESULTS[]> apReconnectResults, RpcPointer<uint> pdwSvrRefrVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteFixedStruct(pRefresherId, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRefresherId);
			encoder.WriteValue(lFlags);
			encoder.WriteValue(lNumObjects);
			encoder.WriteValue(dwClientRefrVersion);
			if ((apReconnectInfo != null))
			{
				encoder.WriteArrayHeader(apReconnectInfo);
				for (int i = 0; (i < apReconnectInfo.Length); i++
				)
				{
					_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
					encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment.NativePtr);
				}
			}
			for (int i = 0; (i < apReconnectInfo.Length); i++
			)
			{
				_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
				encoder.WriteStructDeferral(elem_0);
			}
			encoder.WriteArrayHeader(apReconnectResults.value);
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
			}
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteStructDeferral(elem_0);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			apReconnectResults.value = decoder.ReadArrayHeader<_WBEM_RECONNECT_RESULTS>();
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				elem_0 = decoder.ReadFixedStruct<_WBEM_RECONNECT_RESULTS>(Titanis.DceRpc.NdrAlignment._4Byte);
				apReconnectResults.value[i] = elem_0;
			}
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
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
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemRefreshingServicesStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("2c9273e0-1dc3-11d3-b364-00105a1f8177");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemRefreshingServices _obj;
		public IWbemRefreshingServicesStub(IWbemRefreshingServices obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_AddObjectToRefresher,
					this.Invoke_AddObjectToRefresherByTemplate,
					this.Invoke_AddEnumToRefresher,
					this.Invoke_RemoveObjectFromRefresher,
					this.Invoke_GetRemoteRefresher,
					this.Invoke_ReconnectRemoteRefresher};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_AddObjectToRefresher(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			string wszPath;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pContext;
			uint dwClientRefrVersion;
			RpcPointer<_WBEM_REFRESH_INFO> pInfo = new RpcPointer<_WBEM_REFRESH_INFO>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			wszPath = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			pContext = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pContext);
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.AddObjectToRefresher(pRefresherId, wszPath, lFlags, pContext, dwClientRefrVersion, pInfo, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_AddObjectToRefresherByTemplate(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			Titanis.DceRpc.TypedObjref<IWbemClassObject> pTemplate;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pContext;
			uint dwClientRefrVersion;
			RpcPointer<_WBEM_REFRESH_INFO> pInfo = new RpcPointer<_WBEM_REFRESH_INFO>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			pTemplate = decoder.ReadInterfacePointer<IWbemClassObject>();
			decoder.ReadInterfacePointer(pTemplate);
			lFlags = decoder.ReadInt32();
			pContext = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pContext);
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.AddObjectToRefresherByTemplate(pRefresherId, pTemplate, lFlags, pContext, dwClientRefrVersion, pInfo, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_AddEnumToRefresher(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			string wszClass;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pContext;
			uint dwClientRefrVersion;
			RpcPointer<_WBEM_REFRESH_INFO> pInfo = new RpcPointer<_WBEM_REFRESH_INFO>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			wszClass = decoder.ReadWideCharString();
			lFlags = decoder.ReadInt32();
			pContext = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pContext);
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.AddEnumToRefresher(pRefresherId, wszClass, lFlags, pContext, dwClientRefrVersion, pInfo, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pInfo.value, Titanis.DceRpc.NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pInfo.value);
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_RemoveObjectFromRefresher(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			int lId;
			int lFlags;
			uint dwClientRefrVersion;
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			lId = decoder.ReadInt32();
			lFlags = decoder.ReadInt32();
			dwClientRefrVersion = decoder.ReadUInt32();
			var invokeTask = this._obj.RemoveObjectFromRefresher(pRefresherId, lId, lFlags, dwClientRefrVersion, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetRemoteRefresher(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			int lFlags;
			uint dwClientRefrVersion;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemRemoteRefresher>> ppRemRefresher = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemRemoteRefresher>>();
			RpcPointer<System.Guid> pGuid = new RpcPointer<System.Guid>();
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
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
		private async Task Invoke_ReconnectRemoteRefresher(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			_WBEM_REFRESHER_ID pRefresherId;
			int lFlags;
			int lNumObjects;
			uint dwClientRefrVersion;
			_WBEM_RECONNECT_INFO[] apReconnectInfo;
			RpcPointer<_WBEM_RECONNECT_RESULTS[]> apReconnectResults;
			RpcPointer<uint> pdwSvrRefrVersion = new RpcPointer<uint>();
			pRefresherId = decoder.ReadFixedStruct<_WBEM_REFRESHER_ID>(Titanis.DceRpc.NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<_WBEM_REFRESHER_ID>(ref pRefresherId);
			lFlags = decoder.ReadInt32();
			lNumObjects = decoder.ReadInt32();
			dwClientRefrVersion = decoder.ReadUInt32();
			apReconnectInfo = decoder.ReadArrayHeader<_WBEM_RECONNECT_INFO>();
			for (int i = 0; (i < apReconnectInfo.Length); i++
			)
			{
				_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
				elem_0 = decoder.ReadFixedStruct<_WBEM_RECONNECT_INFO>(Titanis.DceRpc.NdrAlignment.NativePtr);
				apReconnectInfo[i] = elem_0;
			}
			for (int i = 0; (i < apReconnectInfo.Length); i++
			)
			{
				_WBEM_RECONNECT_INFO elem_0 = apReconnectInfo[i];
				decoder.ReadStructDeferral<_WBEM_RECONNECT_INFO>(ref elem_0);
				apReconnectInfo[i] = elem_0;
			}
			apReconnectResults = new RpcPointer<_WBEM_RECONNECT_RESULTS[]>();
			apReconnectResults.value = decoder.ReadArrayHeader<_WBEM_RECONNECT_RESULTS>();
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				elem_0 = decoder.ReadFixedStruct<_WBEM_RECONNECT_RESULTS>(Titanis.DceRpc.NdrAlignment._4Byte);
				apReconnectResults.value[i] = elem_0;
			}
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				decoder.ReadStructDeferral<_WBEM_RECONNECT_RESULTS>(ref elem_0);
				apReconnectResults.value[i] = elem_0;
			}
			var invokeTask = this._obj.ReconnectRemoteRefresher(pRefresherId, lFlags, lNumObjects, dwClientRefrVersion, apReconnectInfo, apReconnectResults, pdwSvrRefrVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(apReconnectResults.value);
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteFixedStruct(elem_0, Titanis.DceRpc.NdrAlignment._4Byte);
			}
			for (int i = 0; (i < apReconnectResults.value.Length); i++
			)
			{
				_WBEM_RECONNECT_RESULTS elem_0 = apReconnectResults.value[i];
				encoder.WriteStructDeferral(elem_0);
			}
			encoder.WriteValue(pdwSvrRefrVersion.value);
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("423ec01e-2e35-11d2-b604-00104b703efd")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemWCOSmartEnum : IUnknown
	{
		Task<int> Next(System.Guid proxyGUID, int lTimeout, uint uCount, RpcPointer<uint> puReturned, RpcPointer<uint> pdwBuffSize, RpcPointer<RpcPointer<byte[]>> pBuffer, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("423ec01e-2e35-11d2-b604-00104b703efd")]
	public class IWbemWCOSmartEnumClientProxy : IUnknownClientProxy, IWbemWCOSmartEnum
	{
		private static System.Guid _interfaceUuid = new System.Guid("423ec01e-2e35-11d2-b604-00104b703efd");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> Next(System.Guid proxyGUID, int lTimeout, uint uCount, RpcPointer<uint> puReturned, RpcPointer<uint> pdwBuffSize, RpcPointer<RpcPointer<byte[]>> pBuffer, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(proxyGUID);
			encoder.WriteValue(lTimeout);
			encoder.WriteValue(uCount);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			puReturned.value = decoder.ReadUInt32();
			pdwBuffSize.value = decoder.ReadUInt32();
			pBuffer.value = decoder.ReadOutPointer<byte[]>(pBuffer.value);
			if ((null != pBuffer.value))
			{
				pBuffer.value.value = decoder.ReadArrayHeader<byte>();
				for (int i = 0; (i < pBuffer.value.value.Length); i++
				)
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
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemWCOSmartEnumStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("423ec01e-2e35-11d2-b604-00104b703efd");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemWCOSmartEnum _obj;
		public IWbemWCOSmartEnumStub(IWbemWCOSmartEnum obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_Next};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Next(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			System.Guid proxyGUID;
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
			encoder.WritePointer(pBuffer.value);
			if ((null != pBuffer.value))
			{
				encoder.WriteArrayHeader(pBuffer.value.value);
				for (int i = 0; (i < pBuffer.value.value.Length); i++
				)
				{
					byte elem_0 = pBuffer.value.value[i];
					encoder.WriteValue(elem_0);
				}
			}
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("1c1c45ee-4395-11d2-b60b-00104b703efd")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemFetchSmartEnum : IUnknown
	{
		Task<int> GetSmartEnum(RpcPointer<Titanis.DceRpc.TypedObjref<IWbemWCOSmartEnum>> ppSmartEnum, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("1c1c45ee-4395-11d2-b60b-00104b703efd")]
	public class IWbemFetchSmartEnumClientProxy : IUnknownClientProxy, IWbemFetchSmartEnum
	{
		private static System.Guid _interfaceUuid = new System.Guid("1c1c45ee-4395-11d2-b60b-00104b703efd");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> GetSmartEnum(RpcPointer<Titanis.DceRpc.TypedObjref<IWbemWCOSmartEnum>> ppSmartEnum, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppSmartEnum.value = decoder.ReadInterfacePointer<IWbemWCOSmartEnum>();
			decoder.ReadInterfacePointer(ppSmartEnum.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemFetchSmartEnumStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("1c1c45ee-4395-11d2-b60b-00104b703efd");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemFetchSmartEnum _obj;
		public IWbemFetchSmartEnumStub(IWbemFetchSmartEnum obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_GetSmartEnum};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_GetSmartEnum(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemWCOSmartEnum>> ppSmartEnum = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemWCOSmartEnum>>();
			var invokeTask = this._obj.GetSmartEnum(ppSmartEnum, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppSmartEnum.value);
			encoder.WriteInterfacePointerBody(ppSmartEnum.value);
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("d4781cd6-e5d3-44df-ad94-930efe48a887")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemLoginClientID : IUnknown
	{
		Task<int> SetClientInfo(string wszClientMachine, int lClientProcId, int lReserved, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("d4781cd6-e5d3-44df-ad94-930efe48a887")]
	public class IWbemLoginClientIDClientProxy : IUnknownClientProxy, IWbemLoginClientID
	{
		private static System.Guid _interfaceUuid = new System.Guid("d4781cd6-e5d3-44df-ad94-930efe48a887");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> SetClientInfo(string wszClientMachine, int lClientProcId, int lReserved, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((wszClientMachine == null));
			if ((wszClientMachine != null))
			{
				encoder.WriteWideCharString(wszClientMachine);
			}
			encoder.WriteValue(lClientProcId);
			encoder.WriteValue(lReserved);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemLoginClientIDStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("d4781cd6-e5d3-44df-ad94-930efe48a887");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemLoginClientID _obj;
		public IWbemLoginClientIDStub(IWbemLoginClientID obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_SetClientInfo};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_SetClientInfo(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string wszClientMachine;
			int lClientProcId;
			int lReserved;
			if ((decoder.ReadReferentId() == 0))
			{
				wszClientMachine = null;
			}
			else
			{
				wszClientMachine = decoder.ReadWideCharString();
			}
			lClientProcId = decoder.ReadInt32();
			lReserved = decoder.ReadInt32();
			var invokeTask = this._obj.SetClientInfo(wszClientMachine, lClientProcId, lReserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("f309ad18-d86a-11d0-a075-00c04fb68820")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemLevel1Login : IUnknown
	{
		Task<int> EstablishPosition(string reserved1, uint reserved2, RpcPointer<uint> LocaleVersion, System.Threading.CancellationToken cancellationToken);
		Task<int> RequestChallenge(string reserved1, string reserved2, RpcPointer<ArraySegment<byte>> reserved3, System.Threading.CancellationToken cancellationToken);
		Task<int> WBEMLogin(string reserved1, ArraySegment<byte> reserved2, int reserved3, Titanis.DceRpc.TypedObjref<IWbemContext> reserved4, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> reserved5, System.Threading.CancellationToken cancellationToken);
		Task<int> NTLMLogin(string wszNetworkResource, string wszPreferredLocale, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppNamespace, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("f309ad18-d86a-11d0-a075-00c04fb68820")]
	public class IWbemLevel1LoginClientProxy : IUnknownClientProxy, IWbemLevel1Login
	{
		private static System.Guid _interfaceUuid = new System.Guid("f309ad18-d86a-11d0-a075-00c04fb68820");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> EstablishPosition(string reserved1, uint reserved2, RpcPointer<uint> LocaleVersion, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((reserved1 == null));
			if ((reserved1 != null))
			{
				encoder.WriteWideCharString(reserved1);
			}
			encoder.WriteValue(reserved2);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			LocaleVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> RequestChallenge(string reserved1, string reserved2, RpcPointer<ArraySegment<byte>> reserved3, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((reserved1 == null));
			if ((reserved1 != null))
			{
				encoder.WriteWideCharString(reserved1);
			}
			encoder.WriteUniqueReferentId((reserved2 == null));
			if ((reserved2 != null))
			{
				encoder.WriteWideCharString(reserved2);
			}
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			reserved3.value = decoder.ReadArraySegmentHeader<byte>();
			for (int i = 0; (i < reserved3.value.Count); i++
			)
			{
				byte elem_0 = reserved3.value.Item(i);
				elem_0 = decoder.ReadUnsignedChar();
				reserved3.value.Item(i) = elem_0;
			}
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> WBEMLogin(string reserved1, ArraySegment<byte> reserved2, int reserved3, Titanis.DceRpc.TypedObjref<IWbemContext> reserved4, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> reserved5, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((reserved1 == null));
			if ((reserved1 != null))
			{
				encoder.WriteWideCharString(reserved1);
			}
			encoder.WriteUniqueReferentId((reserved2 == null));
			if ((reserved2 != null))
			{
				encoder.WriteArrayHeader(reserved2, true);
				for (int i = 0; (i < reserved2.Count); i++
				)
				{
					byte elem_0 = reserved2.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
			encoder.WriteValue(reserved3);
			encoder.WriteInterfacePointer(reserved4);
			encoder.WriteInterfacePointerBody(reserved4);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			reserved5.value = decoder.ReadInterfacePointer<IWbemServices>();
			decoder.ReadInterfacePointer(reserved5.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
		public virtual async Task<int> NTLMLogin(string wszNetworkResource, string wszPreferredLocale, int lFlags, Titanis.DceRpc.TypedObjref<IWbemContext> pCtx, RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppNamespace, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId((wszNetworkResource == null));
			if ((wszNetworkResource != null))
			{
				encoder.WriteWideCharString(wszNetworkResource);
			}
			encoder.WriteUniqueReferentId((wszPreferredLocale == null));
			if ((wszPreferredLocale != null))
			{
				encoder.WriteWideCharString(wszPreferredLocale);
			}
			encoder.WriteValue(lFlags);
			encoder.WriteInterfacePointer(pCtx);
			encoder.WriteInterfacePointerBody(pCtx);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			ppNamespace.value = decoder.ReadInterfacePointer<IWbemServices>();
			decoder.ReadInterfacePointer(ppNamespace.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemLevel1LoginStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("f309ad18-d86a-11d0-a075-00c04fb68820");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemLevel1Login _obj;
		public IWbemLevel1LoginStub(IWbemLevel1Login obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_EstablishPosition,
					this.Invoke_RequestChallenge,
					this.Invoke_WBEMLogin,
					this.Invoke_NTLMLogin};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_EstablishPosition(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string reserved1;
			uint reserved2;
			RpcPointer<uint> LocaleVersion = new RpcPointer<uint>();
			if ((decoder.ReadReferentId() == 0))
			{
				reserved1 = null;
			}
			else
			{
				reserved1 = decoder.ReadWideCharString();
			}
			reserved2 = decoder.ReadUInt32();
			var invokeTask = this._obj.EstablishPosition(reserved1, reserved2, LocaleVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(LocaleVersion.value);
			encoder.WriteValue(retval);
		}
		private async Task Invoke_RequestChallenge(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string reserved1;
			string reserved2;
			RpcPointer<ArraySegment<byte>> reserved3 = new RpcPointer<ArraySegment<byte>>();
			if ((decoder.ReadReferentId() == 0))
			{
				reserved1 = null;
			}
			else
			{
				reserved1 = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				reserved2 = null;
			}
			else
			{
				reserved2 = decoder.ReadWideCharString();
			}
			var invokeTask = this._obj.RequestChallenge(reserved1, reserved2, reserved3, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(reserved3.value, true);
			for (int i = 0; (i < reserved3.value.Count); i++
			)
			{
				byte elem_0 = reserved3.value.Item(i);
				encoder.WriteValue(elem_0);
			}
			encoder.WriteValue(retval);
		}
		private async Task Invoke_WBEMLogin(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string reserved1;
			ArraySegment<byte> reserved2;
			int reserved3;
			Titanis.DceRpc.TypedObjref<IWbemContext> reserved4;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> reserved5 = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>>();
			if ((decoder.ReadReferentId() == 0))
			{
				reserved1 = null;
			}
			else
			{
				reserved1 = decoder.ReadWideCharString();
			}
			reserved2 = decoder.ReadArraySegmentHeader<byte>();
			for (int i = 0; (i < reserved2.Count); i++
			)
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
		private async Task Invoke_NTLMLogin(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			string wszNetworkResource;
			string wszPreferredLocale;
			int lFlags;
			Titanis.DceRpc.TypedObjref<IWbemContext> pCtx;
			RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>> ppNamespace = new RpcPointer<Titanis.DceRpc.TypedObjref<IWbemServices>>();
			if ((decoder.ReadReferentId() == 0))
			{
				wszNetworkResource = null;
			}
			else
			{
				wszNetworkResource = decoder.ReadWideCharString();
			}
			if ((decoder.ReadReferentId() == 0))
			{
				wszPreferredLocale = null;
			}
			else
			{
				wszPreferredLocale = decoder.ReadWideCharString();
			}
			lFlags = decoder.ReadInt32();
			pCtx = decoder.ReadInterfacePointer<IWbemContext>();
			decoder.ReadInterfacePointer(pCtx);
			var invokeTask = this._obj.NTLMLogin(wszNetworkResource, wszPreferredLocale, lFlags, pCtx, ppNamespace, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteInterfacePointer(ppNamespace.value);
			encoder.WriteInterfacePointerBody(ppNamespace.value);
			encoder.WriteValue(retval);
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[System.Runtime.InteropServices.GuidAttribute("541679ab-2e5f-11d3-b34e-00104bcc4b4a")]
	[Titanis.DceRpc.RpcVersionAttribute(0, 0)]
	public interface IWbemLoginHelper : IUnknown
	{
		Task<int> SetEvent(byte sEventToSet, System.Threading.CancellationToken cancellationToken);
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	[Titanis.DceRpc.IidAttribute("541679ab-2e5f-11d3-b34e-00104bcc4b4a")]
	public class IWbemLoginHelperClientProxy : IUnknownClientProxy, IWbemLoginHelper
	{
		private static System.Guid _interfaceUuid = new System.Guid("541679ab-2e5f-11d3-b34e-00104bcc4b4a");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		public virtual async Task<int> SetEvent(byte sEventToSet, System.Threading.CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			Titanis.DceRpc.IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(sEventToSet);
			var sendTask = this.SendRequestAsync(req, cancellationToken);
			Titanis.DceRpc.IRpcDecoder decoder = await sendTask;
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("Animus IDL Compiler", "0.9.4")]
	public class IWbemLoginHelperStub : Titanis.DceRpc.Server.RpcObjectStub
	{
		private static System.Guid _interfaceUuid = new System.Guid("541679ab-2e5f-11d3-b34e-00104bcc4b4a");
		public override System.Guid InterfaceUuid
		{
			get
			{
				return _interfaceUuid;
			}
		}
		public override Titanis.DceRpc.RpcVersion InterfaceVersion
		{
			get
			{
				return new Titanis.DceRpc.RpcVersion(0, 0);
			}
		}
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable
		{
			get
			{
				return this._dispatchTable;
			}
		}
		private IWbemLoginHelper _obj;
		public IWbemLoginHelperStub(IWbemLoginHelper obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] {
					this.Invoke_Opnum0NotUsedOnWire,
					this.Invoke_Opnum1NotUsedOnWire,
					this.Invoke_Opnum2NotUsedOnWire,
					this.Invoke_SetEvent};
		}
		private async Task Invoke_Opnum0NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum0NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum1NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum1NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_Opnum2NotUsedOnWire(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum2NotUsedOnWire(cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
		private async Task Invoke_SetEvent(Titanis.DceRpc.IRpcDecoder decoder, Titanis.DceRpc.IRpcEncoder encoder, System.Threading.CancellationToken cancellationToken)
		{
			byte sEventToSet;
			sEventToSet = decoder.ReadUnsignedChar();
			var invokeTask = this._obj.SetEvent(sEventToSet, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}
	}
}
