namespace ms_rrp
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RVALENT : IRpcFixedStruct
	{
		public RpcPointer<ms_dtyp.RPC_UNICODE_STRING> ve_valuename;
		public uint ve_valuelen;
		public RpcPointer<uint> ve_valueptr;
		public uint ve_type;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.ve_valuename);
			encoder.WriteValue(this.ve_valuelen);
			encoder.WriteUniquePointer(this.ve_valueptr);
			encoder.WriteValue(this.ve_type);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.ve_valuename = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			this.ve_valuelen = decoder.ReadUInt32();
			this.ve_valueptr = decoder.ReadUniquePointer<uint>();
			this.ve_type = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.ve_valuename is not null)
			{
				encoder.WriteFixedStruct(this.ve_valuename.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(this.ve_valuename.value);
			}

			if (this.ve_valueptr is not null)
			{
				encoder.WriteValue(this.ve_valueptr.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.ve_valuename is not null)
			{
				this.ve_valuename.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref this.ve_valuename.value);
			}

			if (this.ve_valueptr is not null)
			{
				this.ve_valueptr.value = decoder.ReadUInt32();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RPC_SECURITY_DESCRIPTOR : IRpcFixedStruct
	{
		public RpcPointer<ArraySegment<byte>> lpSecurityDescriptor;
		public uint cbInSecurityDescriptor;
		public uint cbOutSecurityDescriptor;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteUniquePointer(this.lpSecurityDescriptor);
			encoder.WriteValue(this.cbInSecurityDescriptor);
			encoder.WriteValue(this.cbOutSecurityDescriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.lpSecurityDescriptor = decoder.ReadUniquePointer<ArraySegment<byte>>();
			this.cbInSecurityDescriptor = decoder.ReadUInt32();
			this.cbOutSecurityDescriptor = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.lpSecurityDescriptor is not null)
			{
				encoder.WriteArrayHeader(this.lpSecurityDescriptor.value, true);
				for (int i = 0; i < this.lpSecurityDescriptor.value.Count; i++)
				{
					byte elem_0 = this.lpSecurityDescriptor.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.lpSecurityDescriptor is not null)
			{
				this.lpSecurityDescriptor.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < this.lpSecurityDescriptor.value.Count; i++)
				{
					byte elem_0 = this.lpSecurityDescriptor.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					this.lpSecurityDescriptor.value.Item(i) = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial struct RPC_SECURITY_ATTRIBUTES : IRpcFixedStruct
	{
		public uint nLength;
		public RPC_SECURITY_DESCRIPTOR RpcSecurityDescriptor;
		public byte bInheritHandle;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.nLength);
			encoder.WriteFixedStruct(this.RpcSecurityDescriptor, NdrAlignment.NativePtr);
			encoder.WriteValue(this.bInheritHandle);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void Decode(IRpcDecoder decoder)
		{
			this.nLength = decoder.ReadUInt32();
			this.RpcSecurityDescriptor = decoder.ReadFixedStruct<RPC_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			this.bInheritHandle = decoder.ReadUnsignedChar();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.RpcSecurityDescriptor);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<RPC_SECURITY_DESCRIPTOR>(ref this.RpcSecurityDescriptor);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), GuidAttribute("338cd001-2244-31f1-aaaa-900038001003"), RpcVersionAttribute(1, 0)]
	public partial interface winreg
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenClassesRoot(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenCurrentUser(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenLocalMachine(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenPerformanceData(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenUsers(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegCloseKey(RpcPointer<RpcContextHandle> hKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegCreateKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, ms_dtyp.RPC_UNICODE_STRING lpClass, uint dwOptions, uint samDesired, RpcPointer<RPC_SECURITY_ATTRIBUTES> lpSecurityAttributes, RpcPointer<RpcContextHandle> phkResult, RpcPointer<uint> lpdwDisposition, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegDeleteKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegDeleteValue(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpValueName, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegEnumKey(RpcContextHandle hKey, uint dwIndex, ms_dtyp.RPC_UNICODE_STRING lpNameIn, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpNameOut, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassIn, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> lplpClassOut, RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegEnumValue(RpcContextHandle hKey, uint dwIndex, ms_dtyp.RPC_UNICODE_STRING lpValueNameIn, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpValueNameOut, RpcPointer<uint> lpType, RpcPointer<ArraySegment<byte>> lpData, RpcPointer<uint> lpcbData, RpcPointer<uint> lpcbLen, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegFlushKey(RpcContextHandle hKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegGetKeySecurity(RpcContextHandle hKey, uint SecurityInformation, RPC_SECURITY_DESCRIPTOR pRpcSecurityDescriptorIn, RpcPointer<RPC_SECURITY_DESCRIPTOR> pRpcSecurityDescriptorOut, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegLoadKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, ms_dtyp.RPC_UNICODE_STRING lpFile, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum14NotImplemented(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegOpenKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, uint dwOptions, uint samDesired, RpcPointer<RpcContextHandle> phkResult, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegQueryInfoKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpClassIn, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassOut, RpcPointer<uint> lpcSubKeys, RpcPointer<uint> lpcbMaxSubKeyLen, RpcPointer<uint> lpcbMaxClassLen, RpcPointer<uint> lpcValues, RpcPointer<uint> lpcbMaxValueNameLen, RpcPointer<uint> lpcbMaxValueLen, RpcPointer<uint> lpcbSecurityDescriptor, RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegQueryValue(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpValueName, RpcPointer<uint> lpType, RpcPointer<ArraySegment<byte>> lpData, RpcPointer<uint> lpcbData, RpcPointer<uint> lpcbLen, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegReplaceKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, ms_dtyp.RPC_UNICODE_STRING lpNewFile, ms_dtyp.RPC_UNICODE_STRING lpOldFile, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegRestoreKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpFile, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegSaveKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpFile, RpcPointer<RPC_SECURITY_ATTRIBUTES> pSecurityAttributes, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegSetKeySecurity(RpcContextHandle hKey, uint SecurityInformation, RPC_SECURITY_DESCRIPTOR pRpcSecurityDescriptor, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegSetValue(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpValueName, uint dwType, byte[] lpData, uint cbData, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegUnLoadKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum24NotImplemented(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum25NotImplemented(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegGetVersion(RpcContextHandle hKey, RpcPointer<uint> lpdwVersion, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenCurrentConfig(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum28NotImplemented(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegQueryMultipleValues(RpcContextHandle hKey, ArraySegment<RVALENT> val_listIn, RpcPointer<ArraySegment<RVALENT>> val_listOut, uint num_vals, RpcPointer<ArraySegment<byte>> lpvalueBuf, RpcPointer<uint> ldwTotsize, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task Opnum30NotImplemented(CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegSaveKeyEx(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpFile, RpcPointer<RPC_SECURITY_ATTRIBUTES> pSecurityAttributes, uint Flags, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenPerformanceText(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> OpenPerformanceNlsText(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegQueryMultipleValues2(RpcContextHandle hKey, ArraySegment<RVALENT> val_listIn, RpcPointer<ArraySegment<RVALENT>> val_listOut, uint num_vals, RpcPointer<ArraySegment<byte>> lpvalueBuf, uint ldwTotsize, RpcPointer<uint> ldwRequiredSize, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		Task<int> BaseRegDeleteKeyEx(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, uint AccessMask, uint Reserved, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9"), IidAttribute("338cd001-2244-31f1-aaaa-900038001003")]
	public partial class winregClientProxy : Titanis.DceRpc.Client.RpcClientProxy, winreg, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenClassesRoot(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenCurrentUser(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenLocalMachine(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenPerformanceData(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenUsers(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(4);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegCloseKey(RpcPointer<RpcContextHandle> hKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(5);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			hKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegCreateKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, ms_dtyp.RPC_UNICODE_STRING lpClass, uint dwOptions, uint samDesired, RpcPointer<RPC_SECURITY_ATTRIBUTES> lpSecurityAttributes, RpcPointer<RpcContextHandle> phkResult, RpcPointer<uint> lpdwDisposition, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(6);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			encoder.WriteFixedStruct(lpClass, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpClass);
			encoder.WriteValue(dwOptions);
			encoder.WriteValue(samDesired);
			encoder.WriteUniquePointer(lpSecurityAttributes);
			if (lpSecurityAttributes is not null)
			{
				encoder.WriteFixedStruct(lpSecurityAttributes.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(lpSecurityAttributes.value);
			}

			encoder.WriteUniquePointer(lpdwDisposition);
			if (lpdwDisposition is not null)
			{
				encoder.WriteValue(lpdwDisposition.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phkResult.value = decoder.ReadContextHandle();
			lpdwDisposition = decoder.ReadOutUniquePointer<uint>(lpdwDisposition);
			if (lpdwDisposition is not null)
			{
				lpdwDisposition.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegDeleteKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(7);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegDeleteValue(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpValueName, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(8);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpValueName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpValueName);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegEnumKey(RpcContextHandle hKey, uint dwIndex, ms_dtyp.RPC_UNICODE_STRING lpNameIn, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpNameOut, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassIn, RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> lplpClassOut, RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(9);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteValue(dwIndex);
			encoder.WriteFixedStruct(lpNameIn, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpNameIn);
			encoder.WriteUniquePointer(lpClassIn);
			if (lpClassIn is not null)
			{
				encoder.WriteFixedStruct(lpClassIn.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(lpClassIn.value);
			}

			encoder.WriteUniquePointer(lpftLastWriteTime);
			if (lpftLastWriteTime is not null)
			{
				encoder.WriteFixedStruct(lpftLastWriteTime.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(lpftLastWriteTime.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpNameOut.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpNameOut.value);
			lplpClassOut.value = decoder.ReadOutUniquePointer<ms_dtyp.RPC_UNICODE_STRING>(lplpClassOut.value);
			if (lplpClassOut.value is not null)
			{
				lplpClassOut.value.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lplpClassOut.value.value);
			}

			lpftLastWriteTime = decoder.ReadOutUniquePointer<ms_dtyp.FILETIME>(lpftLastWriteTime);
			if (lpftLastWriteTime is not null)
			{
				lpftLastWriteTime.value = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref lpftLastWriteTime.value);
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegEnumValue(RpcContextHandle hKey, uint dwIndex, ms_dtyp.RPC_UNICODE_STRING lpValueNameIn, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpValueNameOut, RpcPointer<uint> lpType, RpcPointer<ArraySegment<byte>> lpData, RpcPointer<uint> lpcbData, RpcPointer<uint> lpcbLen, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(10);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteValue(dwIndex);
			encoder.WriteFixedStruct(lpValueNameIn, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpValueNameIn);
			encoder.WriteUniquePointer(lpType);
			if (lpType is not null)
			{
				encoder.WriteValue(lpType.value);
			}

			encoder.WriteUniquePointer(lpData);
			if (lpData is not null)
			{
				encoder.WriteArrayHeader(lpData.value, true);
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(lpcbData);
			if (lpcbData is not null)
			{
				encoder.WriteValue(lpcbData.value);
			}

			encoder.WriteUniquePointer(lpcbLen);
			if (lpcbLen is not null)
			{
				encoder.WriteValue(lpcbLen.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpValueNameOut.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpValueNameOut.value);
			lpType = decoder.ReadOutUniquePointer<uint>(lpType);
			if (lpType is not null)
			{
				lpType.value = decoder.ReadUInt32();
			}

			lpData = decoder.ReadOutUniquePointer<ArraySegment<byte>>(lpData);
			if (lpData is not null)
			{
				lpData.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpData.value.Item(i) = elem_0;
				}
			}

			lpcbData = decoder.ReadOutUniquePointer<uint>(lpcbData);
			if (lpcbData is not null)
			{
				lpcbData.value = decoder.ReadUInt32();
			}

			lpcbLen = decoder.ReadOutUniquePointer<uint>(lpcbLen);
			if (lpcbLen is not null)
			{
				lpcbLen.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegFlushKey(RpcContextHandle hKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(11);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegGetKeySecurity(RpcContextHandle hKey, uint SecurityInformation, RPC_SECURITY_DESCRIPTOR pRpcSecurityDescriptorIn, RpcPointer<RPC_SECURITY_DESCRIPTOR> pRpcSecurityDescriptorOut, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(12);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteValue(SecurityInformation);
			encoder.WriteFixedStruct(pRpcSecurityDescriptorIn, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRpcSecurityDescriptorIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pRpcSecurityDescriptorOut.value = decoder.ReadFixedStruct<RPC_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_SECURITY_DESCRIPTOR>(ref pRpcSecurityDescriptorOut.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegLoadKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, ms_dtyp.RPC_UNICODE_STRING lpFile, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(13);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			encoder.WriteFixedStruct(lpFile, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpFile);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum14NotImplemented(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(14);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegOpenKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, uint dwOptions, uint samDesired, RpcPointer<RpcContextHandle> phkResult, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(15);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			encoder.WriteValue(dwOptions);
			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phkResult.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegQueryInfoKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpClassIn, RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassOut, RpcPointer<uint> lpcSubKeys, RpcPointer<uint> lpcbMaxSubKeyLen, RpcPointer<uint> lpcbMaxClassLen, RpcPointer<uint> lpcValues, RpcPointer<uint> lpcbMaxValueNameLen, RpcPointer<uint> lpcbMaxValueLen, RpcPointer<uint> lpcbSecurityDescriptor, RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(16);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpClassIn, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpClassIn);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpClassOut.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpClassOut.value);
			lpcSubKeys.value = decoder.ReadUInt32();
			lpcbMaxSubKeyLen.value = decoder.ReadUInt32();
			lpcbMaxClassLen.value = decoder.ReadUInt32();
			lpcValues.value = decoder.ReadUInt32();
			lpcbMaxValueNameLen.value = decoder.ReadUInt32();
			lpcbMaxValueLen.value = decoder.ReadUInt32();
			lpcbSecurityDescriptor.value = decoder.ReadUInt32();
			lpftLastWriteTime.value = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
			decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref lpftLastWriteTime.value);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegQueryValue(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpValueName, RpcPointer<uint> lpType, RpcPointer<ArraySegment<byte>> lpData, RpcPointer<uint> lpcbData, RpcPointer<uint> lpcbLen, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(17);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpValueName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpValueName);
			encoder.WriteUniquePointer(lpType);
			if (lpType is not null)
			{
				encoder.WriteValue(lpType.value);
			}

			encoder.WriteUniquePointer(lpData);
			if (lpData is not null)
			{
				encoder.WriteArrayHeader(lpData.value, true);
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(lpcbData);
			if (lpcbData is not null)
			{
				encoder.WriteValue(lpcbData.value);
			}

			encoder.WriteUniquePointer(lpcbLen);
			if (lpcbLen is not null)
			{
				encoder.WriteValue(lpcbLen.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpType = decoder.ReadOutUniquePointer<uint>(lpType);
			if (lpType is not null)
			{
				lpType.value = decoder.ReadUInt32();
			}

			lpData = decoder.ReadOutUniquePointer<ArraySegment<byte>>(lpData);
			if (lpData is not null)
			{
				lpData.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpData.value.Item(i) = elem_0;
				}
			}

			lpcbData = decoder.ReadOutUniquePointer<uint>(lpcbData);
			if (lpcbData is not null)
			{
				lpcbData.value = decoder.ReadUInt32();
			}

			lpcbLen = decoder.ReadOutUniquePointer<uint>(lpcbLen);
			if (lpcbLen is not null)
			{
				lpcbLen.value = decoder.ReadUInt32();
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegReplaceKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, ms_dtyp.RPC_UNICODE_STRING lpNewFile, ms_dtyp.RPC_UNICODE_STRING lpOldFile, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(18);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			encoder.WriteFixedStruct(lpNewFile, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpNewFile);
			encoder.WriteFixedStruct(lpOldFile, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpOldFile);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegRestoreKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpFile, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(19);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpFile, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpFile);
			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegSaveKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpFile, RpcPointer<RPC_SECURITY_ATTRIBUTES> pSecurityAttributes, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(20);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpFile, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpFile);
			encoder.WriteUniquePointer(pSecurityAttributes);
			if (pSecurityAttributes is not null)
			{
				encoder.WriteFixedStruct(pSecurityAttributes.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(pSecurityAttributes.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegSetKeySecurity(RpcContextHandle hKey, uint SecurityInformation, RPC_SECURITY_DESCRIPTOR pRpcSecurityDescriptor, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(21);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteValue(SecurityInformation);
			encoder.WriteFixedStruct(pRpcSecurityDescriptor, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRpcSecurityDescriptor);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegSetValue(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpValueName, uint dwType, byte[] lpData, uint cbData, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(22);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpValueName, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpValueName);
			encoder.WriteValue(dwType);
			if (lpData is not null)
			{
				encoder.WriteArrayHeader(lpData);
				for (int i = 0; i < lpData.Length; i++)
				{
					byte elem_0 = lpData[i];
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(cbData);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegUnLoadKey(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(23);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum24NotImplemented(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(24);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum25NotImplemented(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(25);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegGetVersion(RpcContextHandle hKey, RpcPointer<uint> lpdwVersion, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(26);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			lpdwVersion.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenCurrentConfig(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(27);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum28NotImplemented(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(28);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegQueryMultipleValues(RpcContextHandle hKey, ArraySegment<RVALENT> val_listIn, RpcPointer<ArraySegment<RVALENT>> val_listOut, uint num_vals, RpcPointer<ArraySegment<byte>> lpvalueBuf, RpcPointer<uint> ldwTotsize, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(29);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteArrayHeader(val_listIn, true);
			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}

			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteValue(num_vals);
			encoder.WriteUniquePointer(lpvalueBuf);
			if (lpvalueBuf is not null)
			{
				encoder.WriteArrayHeader(lpvalueBuf.value, true);
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(ldwTotsize.value);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			val_listOut.value = decoder.ReadArraySegmentHeader<RVALENT>();
			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				elem_0 = decoder.ReadFixedStruct<RVALENT>(NdrAlignment.NativePtr);
				val_listOut.value.Item(i) = elem_0;
			}

			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				decoder.ReadStructDeferral<RVALENT>(ref elem_0);
				val_listOut.value.Item(i) = elem_0;
			}

			lpvalueBuf = decoder.ReadOutUniquePointer<ArraySegment<byte>>(lpvalueBuf);
			if (lpvalueBuf is not null)
			{
				lpvalueBuf.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpvalueBuf.value.Item(i) = elem_0;
				}
			}

			ldwTotsize.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Opnum30NotImplemented(CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(30);
			IRpcEncoder encoder = req.StubData;
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegSaveKeyEx(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpFile, RpcPointer<RPC_SECURITY_ATTRIBUTES> pSecurityAttributes, uint Flags, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(31);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpFile, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpFile);
			encoder.WriteUniquePointer(pSecurityAttributes);
			if (pSecurityAttributes is not null)
			{
				encoder.WriteFixedStruct(pSecurityAttributes.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(pSecurityAttributes.value);
			}

			encoder.WriteValue(Flags);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenPerformanceText(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(32);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> OpenPerformanceNlsText(RpcPointer<char> ServerName, uint samDesired, RpcPointer<RpcContextHandle> phKey, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(33);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniquePointer(ServerName);
			if (ServerName is not null)
			{
				encoder.WriteValue(ServerName.value);
			}

			encoder.WriteValue(samDesired);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			phKey.value = decoder.ReadContextHandle();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegQueryMultipleValues2(RpcContextHandle hKey, ArraySegment<RVALENT> val_listIn, RpcPointer<ArraySegment<RVALENT>> val_listOut, uint num_vals, RpcPointer<ArraySegment<byte>> lpvalueBuf, uint ldwTotsize, RpcPointer<uint> ldwRequiredSize, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(34);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteArrayHeader(val_listIn, true);
			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}

			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteValue(num_vals);
			encoder.WriteUniquePointer(lpvalueBuf);
			if (lpvalueBuf is not null)
			{
				encoder.WriteArrayHeader(lpvalueBuf.value, true);
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(ldwTotsize);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			val_listOut.value = decoder.ReadArraySegmentHeader<RVALENT>();
			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				elem_0 = decoder.ReadFixedStruct<RVALENT>(NdrAlignment.NativePtr);
				val_listOut.value.Item(i) = elem_0;
			}

			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				decoder.ReadStructDeferral<RVALENT>(ref elem_0);
				val_listOut.value.Item(i) = elem_0;
			}

			lpvalueBuf = decoder.ReadOutUniquePointer<ArraySegment<byte>>(lpvalueBuf);
			if (lpvalueBuf is not null)
			{
				lpvalueBuf.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpvalueBuf.value.Item(i) = elem_0;
				}
			}

			ldwRequiredSize.value = decoder.ReadUInt32();
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task<int> BaseRegDeleteKeyEx(RpcContextHandle hKey, ms_dtyp.RPC_UNICODE_STRING lpSubKey, uint AccessMask, uint Reserved, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(35);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteContextHandle(hKey);
			encoder.WriteFixedStruct(lpSubKey, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpSubKey);
			encoder.WriteValue(AccessMask);
			encoder.WriteValue(Reserved);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(winreg);
		private static Guid _interfaceUuid = new Guid("338cd001-2244-31f1-aaaa-900038001003");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
	public partial class winregStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenClassesRoot(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenClassesRoot(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenCurrentUser(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenCurrentUser(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenLocalMachine(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenLocalMachine(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenPerformanceData(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenPerformanceData(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenUsers(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenUsers(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegCloseKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<RpcContextHandle> hKey;
			hKey = new RpcPointer<RpcContextHandle>();
			hKey.value = decoder.ReadContextHandle();
			var invokeTask = this._obj.BaseRegCloseKey(hKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(hKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegCreateKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			ms_dtyp.RPC_UNICODE_STRING lpClass;
			uint dwOptions;
			uint samDesired;
			RpcPointer<RPC_SECURITY_ATTRIBUTES> lpSecurityAttributes;
			RpcPointer<RpcContextHandle> phkResult = new RpcPointer<RpcContextHandle>();
			RpcPointer<uint> lpdwDisposition;
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			lpClass = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpClass);
			dwOptions = decoder.ReadUInt32();
			samDesired = decoder.ReadUInt32();
			lpSecurityAttributes = decoder.ReadUniquePointer<RPC_SECURITY_ATTRIBUTES>();
			if (lpSecurityAttributes is not null)
			{
				lpSecurityAttributes.value = decoder.ReadFixedStruct<RPC_SECURITY_ATTRIBUTES>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<RPC_SECURITY_ATTRIBUTES>(ref lpSecurityAttributes.value);
			}

			lpdwDisposition = decoder.ReadUniquePointer<uint>();
			if (lpdwDisposition is not null)
			{
				lpdwDisposition.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.BaseRegCreateKey(hKey, lpSubKey, lpClass, dwOptions, samDesired, lpSecurityAttributes, phkResult, lpdwDisposition, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phkResult.value);
			encoder.WriteUniquePointer(lpdwDisposition);
			if (lpdwDisposition is not null)
			{
				encoder.WriteValue(lpdwDisposition.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegDeleteKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			var invokeTask = this._obj.BaseRegDeleteKey(hKey, lpSubKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegDeleteValue(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpValueName;
			hKey = decoder.ReadContextHandle();
			lpValueName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpValueName);
			var invokeTask = this._obj.BaseRegDeleteValue(hKey, lpValueName, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegEnumKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			uint dwIndex;
			ms_dtyp.RPC_UNICODE_STRING lpNameIn;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpNameOut = new RpcPointer<ms_dtyp.RPC_UNICODE_STRING>();
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassIn;
			RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>> lplpClassOut = new RpcPointer<RpcPointer<ms_dtyp.RPC_UNICODE_STRING>>();
			RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime;
			hKey = decoder.ReadContextHandle();
			dwIndex = decoder.ReadUInt32();
			lpNameIn = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpNameIn);
			lpClassIn = decoder.ReadUniquePointer<ms_dtyp.RPC_UNICODE_STRING>();
			if (lpClassIn is not null)
			{
				lpClassIn.value = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpClassIn.value);
			}

			lpftLastWriteTime = decoder.ReadUniquePointer<ms_dtyp.FILETIME>();
			if (lpftLastWriteTime is not null)
			{
				lpftLastWriteTime.value = decoder.ReadFixedStruct<ms_dtyp.FILETIME>(NdrAlignment._4Byte);
				decoder.ReadStructDeferral<ms_dtyp.FILETIME>(ref lpftLastWriteTime.value);
			}

			var invokeTask = this._obj.BaseRegEnumKey(hKey, dwIndex, lpNameIn, lpNameOut, lpClassIn, lplpClassOut, lpftLastWriteTime, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpNameOut.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpNameOut.value);
			encoder.WriteUniquePointer(lplpClassOut.value);
			if (lplpClassOut.value is not null)
			{
				encoder.WriteFixedStruct(lplpClassOut.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(lplpClassOut.value.value);
			}

			encoder.WriteUniquePointer(lpftLastWriteTime);
			if (lpftLastWriteTime is not null)
			{
				encoder.WriteFixedStruct(lpftLastWriteTime.value, NdrAlignment._4Byte);
				encoder.WriteStructDeferral(lpftLastWriteTime.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegEnumValue(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			uint dwIndex;
			ms_dtyp.RPC_UNICODE_STRING lpValueNameIn;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpValueNameOut = new RpcPointer<ms_dtyp.RPC_UNICODE_STRING>();
			RpcPointer<uint> lpType;
			RpcPointer<ArraySegment<byte>> lpData;
			RpcPointer<uint> lpcbData;
			RpcPointer<uint> lpcbLen;
			hKey = decoder.ReadContextHandle();
			dwIndex = decoder.ReadUInt32();
			lpValueNameIn = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpValueNameIn);
			lpType = decoder.ReadUniquePointer<uint>();
			if (lpType is not null)
			{
				lpType.value = decoder.ReadUInt32();
			}

			lpData = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (lpData is not null)
			{
				lpData.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpData.value.Item(i) = elem_0;
				}
			}

			lpcbData = decoder.ReadUniquePointer<uint>();
			if (lpcbData is not null)
			{
				lpcbData.value = decoder.ReadUInt32();
			}

			lpcbLen = decoder.ReadUniquePointer<uint>();
			if (lpcbLen is not null)
			{
				lpcbLen.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.BaseRegEnumValue(hKey, dwIndex, lpValueNameIn, lpValueNameOut, lpType, lpData, lpcbData, lpcbLen, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpValueNameOut.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpValueNameOut.value);
			encoder.WriteUniquePointer(lpType);
			if (lpType is not null)
			{
				encoder.WriteValue(lpType.value);
			}

			encoder.WriteUniquePointer(lpData);
			if (lpData is not null)
			{
				encoder.WriteArrayHeader(lpData.value, true);
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(lpcbData);
			if (lpcbData is not null)
			{
				encoder.WriteValue(lpcbData.value);
			}

			encoder.WriteUniquePointer(lpcbLen);
			if (lpcbLen is not null)
			{
				encoder.WriteValue(lpcbLen.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegFlushKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			hKey = decoder.ReadContextHandle();
			var invokeTask = this._obj.BaseRegFlushKey(hKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegGetKeySecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			uint SecurityInformation;
			RPC_SECURITY_DESCRIPTOR pRpcSecurityDescriptorIn;
			RpcPointer<RPC_SECURITY_DESCRIPTOR> pRpcSecurityDescriptorOut = new RpcPointer<RPC_SECURITY_DESCRIPTOR>();
			hKey = decoder.ReadContextHandle();
			SecurityInformation = decoder.ReadUInt32();
			pRpcSecurityDescriptorIn = decoder.ReadFixedStruct<RPC_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_SECURITY_DESCRIPTOR>(ref pRpcSecurityDescriptorIn);
			var invokeTask = this._obj.BaseRegGetKeySecurity(hKey, SecurityInformation, pRpcSecurityDescriptorIn, pRpcSecurityDescriptorOut, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pRpcSecurityDescriptorOut.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pRpcSecurityDescriptorOut.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegLoadKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			ms_dtyp.RPC_UNICODE_STRING lpFile;
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			lpFile = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpFile);
			var invokeTask = this._obj.BaseRegLoadKey(hKey, lpSubKey, lpFile, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum14NotImplemented(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum14NotImplemented(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegOpenKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			uint dwOptions;
			uint samDesired;
			RpcPointer<RpcContextHandle> phkResult = new RpcPointer<RpcContextHandle>();
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			dwOptions = decoder.ReadUInt32();
			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegOpenKey(hKey, lpSubKey, dwOptions, samDesired, phkResult, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phkResult.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegQueryInfoKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpClassIn;
			RpcPointer<ms_dtyp.RPC_UNICODE_STRING> lpClassOut = new RpcPointer<ms_dtyp.RPC_UNICODE_STRING>();
			RpcPointer<uint> lpcSubKeys = new RpcPointer<uint>();
			RpcPointer<uint> lpcbMaxSubKeyLen = new RpcPointer<uint>();
			RpcPointer<uint> lpcbMaxClassLen = new RpcPointer<uint>();
			RpcPointer<uint> lpcValues = new RpcPointer<uint>();
			RpcPointer<uint> lpcbMaxValueNameLen = new RpcPointer<uint>();
			RpcPointer<uint> lpcbMaxValueLen = new RpcPointer<uint>();
			RpcPointer<uint> lpcbSecurityDescriptor = new RpcPointer<uint>();
			RpcPointer<ms_dtyp.FILETIME> lpftLastWriteTime = new RpcPointer<ms_dtyp.FILETIME>();
			hKey = decoder.ReadContextHandle();
			lpClassIn = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpClassIn);
			var invokeTask = this._obj.BaseRegQueryInfoKey(hKey, lpClassIn, lpClassOut, lpcSubKeys, lpcbMaxSubKeyLen, lpcbMaxClassLen, lpcValues, lpcbMaxValueNameLen, lpcbMaxValueLen, lpcbSecurityDescriptor, lpftLastWriteTime, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(lpClassOut.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(lpClassOut.value);
			encoder.WriteValue(lpcSubKeys.value);
			encoder.WriteValue(lpcbMaxSubKeyLen.value);
			encoder.WriteValue(lpcbMaxClassLen.value);
			encoder.WriteValue(lpcValues.value);
			encoder.WriteValue(lpcbMaxValueNameLen.value);
			encoder.WriteValue(lpcbMaxValueLen.value);
			encoder.WriteValue(lpcbSecurityDescriptor.value);
			encoder.WriteFixedStruct(lpftLastWriteTime.value, NdrAlignment._4Byte);
			encoder.WriteStructDeferral(lpftLastWriteTime.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegQueryValue(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpValueName;
			RpcPointer<uint> lpType;
			RpcPointer<ArraySegment<byte>> lpData;
			RpcPointer<uint> lpcbData;
			RpcPointer<uint> lpcbLen;
			hKey = decoder.ReadContextHandle();
			lpValueName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpValueName);
			lpType = decoder.ReadUniquePointer<uint>();
			if (lpType is not null)
			{
				lpType.value = decoder.ReadUInt32();
			}

			lpData = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (lpData is not null)
			{
				lpData.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpData.value.Item(i) = elem_0;
				}
			}

			lpcbData = decoder.ReadUniquePointer<uint>();
			if (lpcbData is not null)
			{
				lpcbData.value = decoder.ReadUInt32();
			}

			lpcbLen = decoder.ReadUniquePointer<uint>();
			if (lpcbLen is not null)
			{
				lpcbLen.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.BaseRegQueryValue(hKey, lpValueName, lpType, lpData, lpcbData, lpcbLen, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(lpType);
			if (lpType is not null)
			{
				encoder.WriteValue(lpType.value);
			}

			encoder.WriteUniquePointer(lpData);
			if (lpData is not null)
			{
				encoder.WriteArrayHeader(lpData.value, true);
				for (int i = 0; i < lpData.value.Count; i++)
				{
					byte elem_0 = lpData.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteUniquePointer(lpcbData);
			if (lpcbData is not null)
			{
				encoder.WriteValue(lpcbData.value);
			}

			encoder.WriteUniquePointer(lpcbLen);
			if (lpcbLen is not null)
			{
				encoder.WriteValue(lpcbLen.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegReplaceKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			ms_dtyp.RPC_UNICODE_STRING lpNewFile;
			ms_dtyp.RPC_UNICODE_STRING lpOldFile;
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			lpNewFile = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpNewFile);
			lpOldFile = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpOldFile);
			var invokeTask = this._obj.BaseRegReplaceKey(hKey, lpSubKey, lpNewFile, lpOldFile, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegRestoreKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpFile;
			uint Flags;
			hKey = decoder.ReadContextHandle();
			lpFile = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpFile);
			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegRestoreKey(hKey, lpFile, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegSaveKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpFile;
			RpcPointer<RPC_SECURITY_ATTRIBUTES> pSecurityAttributes;
			hKey = decoder.ReadContextHandle();
			lpFile = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpFile);
			pSecurityAttributes = decoder.ReadUniquePointer<RPC_SECURITY_ATTRIBUTES>();
			if (pSecurityAttributes is not null)
			{
				pSecurityAttributes.value = decoder.ReadFixedStruct<RPC_SECURITY_ATTRIBUTES>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<RPC_SECURITY_ATTRIBUTES>(ref pSecurityAttributes.value);
			}

			var invokeTask = this._obj.BaseRegSaveKey(hKey, lpFile, pSecurityAttributes, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegSetKeySecurity(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			uint SecurityInformation;
			RPC_SECURITY_DESCRIPTOR pRpcSecurityDescriptor;
			hKey = decoder.ReadContextHandle();
			SecurityInformation = decoder.ReadUInt32();
			pRpcSecurityDescriptor = decoder.ReadFixedStruct<RPC_SECURITY_DESCRIPTOR>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<RPC_SECURITY_DESCRIPTOR>(ref pRpcSecurityDescriptor);
			var invokeTask = this._obj.BaseRegSetKeySecurity(hKey, SecurityInformation, pRpcSecurityDescriptor, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegSetValue(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpValueName;
			uint dwType;
			byte[] lpData;
			uint cbData;
			hKey = decoder.ReadContextHandle();
			lpValueName = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpValueName);
			dwType = decoder.ReadUInt32();
			lpData = decoder.ReadArrayHeader<byte>();
			for (int i = 0; i < lpData.Length; i++)
			{
				byte elem_0 = lpData[i];
				elem_0 = decoder.ReadUnsignedChar();
				lpData[i] = elem_0;
			}

			cbData = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegSetValue(hKey, lpValueName, dwType, lpData, cbData, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegUnLoadKey(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			var invokeTask = this._obj.BaseRegUnLoadKey(hKey, lpSubKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum24NotImplemented(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum24NotImplemented(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum25NotImplemented(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum25NotImplemented(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegGetVersion(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			RpcPointer<uint> lpdwVersion = new RpcPointer<uint>();
			hKey = decoder.ReadContextHandle();
			var invokeTask = this._obj.BaseRegGetVersion(hKey, lpdwVersion, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(lpdwVersion.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenCurrentConfig(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenCurrentConfig(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum28NotImplemented(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum28NotImplemented(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegQueryMultipleValues(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ArraySegment<RVALENT> val_listIn;
			RpcPointer<ArraySegment<RVALENT>> val_listOut = new RpcPointer<ArraySegment<RVALENT>>();
			uint num_vals;
			RpcPointer<ArraySegment<byte>> lpvalueBuf;
			RpcPointer<uint> ldwTotsize;
			hKey = decoder.ReadContextHandle();
			val_listIn = decoder.ReadArraySegmentHeader<RVALENT>();
			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				elem_0 = decoder.ReadFixedStruct<RVALENT>(NdrAlignment.NativePtr);
				val_listIn.Item(i) = elem_0;
			}

			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				decoder.ReadStructDeferral<RVALENT>(ref elem_0);
				val_listIn.Item(i) = elem_0;
			}

			num_vals = decoder.ReadUInt32();
			lpvalueBuf = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (lpvalueBuf is not null)
			{
				lpvalueBuf.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpvalueBuf.value.Item(i) = elem_0;
				}
			}

			ldwTotsize = new RpcPointer<uint>();
			ldwTotsize.value = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegQueryMultipleValues(hKey, val_listIn, val_listOut, num_vals, lpvalueBuf, ldwTotsize, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(val_listOut.value, true);
			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}

			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteUniquePointer(lpvalueBuf);
			if (lpvalueBuf is not null)
			{
				encoder.WriteArrayHeader(lpvalueBuf.value, true);
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(ldwTotsize.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_Opnum30NotImplemented(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			var invokeTask = this._obj.Opnum30NotImplemented(cancellationToken);
			await invokeTask;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegSaveKeyEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpFile;
			RpcPointer<RPC_SECURITY_ATTRIBUTES> pSecurityAttributes;
			uint Flags;
			hKey = decoder.ReadContextHandle();
			lpFile = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpFile);
			pSecurityAttributes = decoder.ReadUniquePointer<RPC_SECURITY_ATTRIBUTES>();
			if (pSecurityAttributes is not null)
			{
				pSecurityAttributes.value = decoder.ReadFixedStruct<RPC_SECURITY_ATTRIBUTES>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<RPC_SECURITY_ATTRIBUTES>(ref pSecurityAttributes.value);
			}

			Flags = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegSaveKeyEx(hKey, lpFile, pSecurityAttributes, Flags, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenPerformanceText(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenPerformanceText(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_OpenPerformanceNlsText(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcPointer<char> ServerName;
			uint samDesired;
			RpcPointer<RpcContextHandle> phKey = new RpcPointer<RpcContextHandle>();
			ServerName = decoder.ReadUniquePointer<char>();
			if (ServerName is not null)
			{
				ServerName.value = decoder.ReadWideChar();
			}

			samDesired = decoder.ReadUInt32();
			var invokeTask = this._obj.OpenPerformanceNlsText(ServerName, samDesired, phKey, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteContextHandle(phKey.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegQueryMultipleValues2(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ArraySegment<RVALENT> val_listIn;
			RpcPointer<ArraySegment<RVALENT>> val_listOut = new RpcPointer<ArraySegment<RVALENT>>();
			uint num_vals;
			RpcPointer<ArraySegment<byte>> lpvalueBuf;
			uint ldwTotsize;
			RpcPointer<uint> ldwRequiredSize = new RpcPointer<uint>();
			hKey = decoder.ReadContextHandle();
			val_listIn = decoder.ReadArraySegmentHeader<RVALENT>();
			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				elem_0 = decoder.ReadFixedStruct<RVALENT>(NdrAlignment.NativePtr);
				val_listIn.Item(i) = elem_0;
			}

			for (int i = 0; i < val_listIn.Count; i++)
			{
				RVALENT elem_0 = val_listIn.Item(i);
				decoder.ReadStructDeferral<RVALENT>(ref elem_0);
				val_listIn.Item(i) = elem_0;
			}

			num_vals = decoder.ReadUInt32();
			lpvalueBuf = decoder.ReadUniquePointer<ArraySegment<byte>>();
			if (lpvalueBuf is not null)
			{
				lpvalueBuf.value = decoder.ReadArraySegmentHeader<byte>();
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					elem_0 = decoder.ReadUnsignedChar();
					lpvalueBuf.value.Item(i) = elem_0;
				}
			}

			ldwTotsize = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegQueryMultipleValues2(hKey, val_listIn, val_listOut, num_vals, lpvalueBuf, ldwTotsize, ldwRequiredSize, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteArrayHeader(val_listOut.value, true);
			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
			}

			for (int i = 0; i < val_listOut.value.Count; i++)
			{
				RVALENT elem_0 = val_listOut.value.Item(i);
				encoder.WriteStructDeferral(elem_0);
			}

			encoder.WriteUniquePointer(lpvalueBuf);
			if (lpvalueBuf is not null)
			{
				encoder.WriteArrayHeader(lpvalueBuf.value, true);
				for (int i = 0; i < lpvalueBuf.value.Count; i++)
				{
					byte elem_0 = lpvalueBuf.value.Item(i);
					encoder.WriteValue(elem_0);
				}
			}

			encoder.WriteValue(ldwRequiredSize.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public async Task Invoke_BaseRegDeleteKeyEx(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			RpcContextHandle hKey;
			ms_dtyp.RPC_UNICODE_STRING lpSubKey;
			uint AccessMask;
			uint Reserved;
			hKey = decoder.ReadContextHandle();
			lpSubKey = decoder.ReadFixedStruct<ms_dtyp.RPC_UNICODE_STRING>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<ms_dtyp.RPC_UNICODE_STRING>(ref lpSubKey);
			AccessMask = decoder.ReadUInt32();
			Reserved = decoder.ReadUInt32();
			var invokeTask = this._obj.BaseRegDeleteKeyEx(hKey, lpSubKey, AccessMask, Reserved, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("338cd001-2244-31f1-aaaa-900038001003");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private winreg _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.9")]
		public winregStub(winreg obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[] { this.Invoke_OpenClassesRoot, this.Invoke_OpenCurrentUser, this.Invoke_OpenLocalMachine, this.Invoke_OpenPerformanceData, this.Invoke_OpenUsers, this.Invoke_BaseRegCloseKey, this.Invoke_BaseRegCreateKey, this.Invoke_BaseRegDeleteKey, this.Invoke_BaseRegDeleteValue, this.Invoke_BaseRegEnumKey, this.Invoke_BaseRegEnumValue, this.Invoke_BaseRegFlushKey, this.Invoke_BaseRegGetKeySecurity, this.Invoke_BaseRegLoadKey, this.Invoke_Opnum14NotImplemented, this.Invoke_BaseRegOpenKey, this.Invoke_BaseRegQueryInfoKey, this.Invoke_BaseRegQueryValue, this.Invoke_BaseRegReplaceKey, this.Invoke_BaseRegRestoreKey, this.Invoke_BaseRegSaveKey, this.Invoke_BaseRegSetKeySecurity, this.Invoke_BaseRegSetValue, this.Invoke_BaseRegUnLoadKey, this.Invoke_Opnum24NotImplemented, this.Invoke_Opnum25NotImplemented, this.Invoke_BaseRegGetVersion, this.Invoke_OpenCurrentConfig, this.Invoke_Opnum28NotImplemented, this.Invoke_BaseRegQueryMultipleValues, this.Invoke_Opnum30NotImplemented, this.Invoke_BaseRegSaveKeyEx, this.Invoke_OpenPerformanceText, this.Invoke_OpenPerformanceNlsText, this.Invoke_BaseRegQueryMultipleValues2, this.Invoke_BaseRegDeleteKeyEx };
		}
	}
}