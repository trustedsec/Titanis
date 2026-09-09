namespace atsvc
{
	using System;
	using System.CodeDom.Compiler;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Titanis;
	using Titanis.DceRpc;

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct AT_ENUM : IRpcFixedStruct
	{
		public uint JobId;
		public UIntPtr JobTime;
		public uint DaysOfMonth;
		public byte DaysOfWeek;
		public byte Flags;
		public RpcPointer<char> Command;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.JobId);
			encoder.WriteValue(this.JobTime);
			encoder.WriteValue(this.DaysOfMonth);
			encoder.WriteValue(this.DaysOfWeek);
			encoder.WriteValue(this.Flags);
			encoder.WriteUniquePointer(this.Command);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.JobId = decoder.ReadUInt32();
			this.JobTime = decoder.ReadUInt3264();
			this.DaysOfMonth = decoder.ReadUInt32();
			this.DaysOfWeek = decoder.ReadUnsignedChar();
			this.Flags = decoder.ReadUnsignedChar();
			this.Command = decoder.ReadUniquePointer<char>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Command is not null)
			{
				encoder.WriteValue(this.Command.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Command is not null)
			{
				this.Command.value = decoder.ReadWideChar();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct AT_INFO : IRpcFixedStruct
	{
		public UIntPtr JobTime;
		public uint DaysOfMonth;
		public byte DaysOfWeek;
		public byte Flags;
		public RpcPointer<string> Command;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.JobTime);
			encoder.WriteValue(this.DaysOfMonth);
			encoder.WriteValue(this.DaysOfWeek);
			encoder.WriteValue(this.Flags);
			encoder.WriteUniquePointer(this.Command);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.JobTime = decoder.ReadUInt3264();
			this.DaysOfMonth = decoder.ReadUInt32();
			this.DaysOfWeek = decoder.ReadUnsignedChar();
			this.Flags = decoder.ReadUnsignedChar();
			this.Command = decoder.ReadUniquePointer<string>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Command is not null)
			{
				encoder.WriteWideCharString(this.Command.value);
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Command is not null)
			{
				this.Command.value = decoder.ReadWideCharString();
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct AT_ENUM_CONTAINER : IRpcFixedStruct
	{
		public uint EntriesRead;
		public RpcPointer<AT_ENUM[]> Buffer;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.EntriesRead);
			encoder.WriteUniquePointer(this.Buffer);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.EntriesRead = decoder.ReadUInt32();
			this.Buffer = decoder.ReadUniquePointer<AT_ENUM[]>();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			if (this.Buffer is not null)
			{
				encoder.WriteArrayHeader(this.Buffer.value);
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					AT_ENUM elem_0 = this.Buffer.value[i];
					encoder.WriteFixedStruct(elem_0, NdrAlignment.NativePtr);
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					AT_ENUM elem_0 = this.Buffer.value[i];
					encoder.WriteStructDeferral(elem_0);
				}
			}
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			if (this.Buffer is not null)
			{
				this.Buffer.value = decoder.ReadArrayHeader<AT_ENUM>();
				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					AT_ENUM elem_0 = this.Buffer.value[i];
					elem_0 = decoder.ReadFixedStruct<AT_ENUM>(NdrAlignment.NativePtr);
					this.Buffer.value[i] = elem_0;
				}

				for (int i = 0; i < this.Buffer.value.Length; i++)
				{
					AT_ENUM elem_0 = this.Buffer.value[i];
					decoder.ReadStructDeferral<AT_ENUM>(ref elem_0);
					this.Buffer.value[i] = elem_0;
				}
			}
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), GuidAttribute("1ff70682-0a51-30e8-076d-740be8cee98b"), RpcVersionAttribute(1, 0)]
	public partial interface atsvc
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<uint> NetrJobAdd(string ServerName, AT_INFO pAtInfo, RpcPointer<uint> pJobId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<uint> NetrJobDel(string ServerName, uint MinJobId, uint MaxJobId, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<uint> NetrJobEnum(string ServerName, RpcPointer<AT_ENUM_CONTAINER> pEnumContainer, uint PreferedMaximumLength, RpcPointer<uint> pTotalEntries, RpcPointer<uint> pResumeHandle, CancellationToken cancellationToken);
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		Task<uint> NetrJobGetInfo(string ServerName, uint JobId, RpcPointer<RpcPointer<AT_INFO>> ppAtInfo, CancellationToken cancellationToken);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10"), IidAttribute("1ff70682-0a51-30e8-076d-740be8cee98b")]
	public partial class atsvcClientProxy : Titanis.DceRpc.Client.RpcClientProxy, atsvc, Titanis.DceRpc.IRpcClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<uint> NetrJobAdd(string ServerName, AT_INFO pAtInfo, RpcPointer<uint> pJobId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(0);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(pAtInfo, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pAtInfo);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pJobId.value = decoder.ReadUInt32();
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<uint> NetrJobDel(string ServerName, uint MinJobId, uint MaxJobId, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(1);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(MinJobId);
			encoder.WriteValue(MaxJobId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<uint> NetrJobEnum(string ServerName, RpcPointer<AT_ENUM_CONTAINER> pEnumContainer, uint PreferedMaximumLength, RpcPointer<uint> pTotalEntries, RpcPointer<uint> pResumeHandle, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(2);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteFixedStruct(pEnumContainer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pEnumContainer.value);
			encoder.WriteValue(PreferedMaximumLength);
			encoder.WriteUniquePointer(pResumeHandle);
			if (pResumeHandle is not null)
			{
				encoder.WriteValue(pResumeHandle.value);
			}

			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			pEnumContainer.value = decoder.ReadFixedStruct<AT_ENUM_CONTAINER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<AT_ENUM_CONTAINER>(ref pEnumContainer.value);
			pTotalEntries.value = decoder.ReadUInt32();
			pResumeHandle = decoder.ReadOutUniquePointer<uint>(pResumeHandle);
			if (pResumeHandle is not null)
			{
				pResumeHandle.value = decoder.ReadUInt32();
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<uint> NetrJobGetInfo(string ServerName, uint JobId, RpcPointer<RpcPointer<AT_INFO>> ppAtInfo, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteUniqueReferentId(ServerName is null);
			if (ServerName is not null)
				encoder.WriteWideCharString(ServerName);
			encoder.WriteValue(JobId);
			IRpcDecoder decoder = await this.SendRequestAsync(req, cancellationToken);
			ppAtInfo.value = decoder.ReadOutUniquePointer<AT_INFO>(ppAtInfo.value);
			if (ppAtInfo.value is not null)
			{
				ppAtInfo.value.value = decoder.ReadFixedStruct<AT_INFO>(NdrAlignment.NativePtr);
				decoder.ReadStructDeferral<AT_INFO>(ref ppAtInfo.value.value);
			}

			uint retval;
			retval = decoder.ReadUInt32();
			return retval;
		}

		public sealed override Type InterfaceType => typeof(atsvc);
		private static Guid _interfaceUuid = new Guid("1ff70682-0a51-30e8-076d-740be8cee98b");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial class atsvcStub : Titanis.DceRpc.Server.RpcServiceStub
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_NetrJobAdd(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			AT_INFO pAtInfo;
			RpcPointer<uint> pJobId = new RpcPointer<uint>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			pAtInfo = decoder.ReadFixedStruct<AT_INFO>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<AT_INFO>(ref pAtInfo);
			var invokeTask = this._obj.NetrJobAdd(ServerName, pAtInfo, pJobId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(pJobId.value);
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_NetrJobDel(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint MinJobId;
			uint MaxJobId;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			MinJobId = decoder.ReadUInt32();
			MaxJobId = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrJobDel(ServerName, MinJobId, MaxJobId, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_NetrJobEnum(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			RpcPointer<AT_ENUM_CONTAINER> pEnumContainer;
			uint PreferedMaximumLength;
			RpcPointer<uint> pTotalEntries = new RpcPointer<uint>();
			RpcPointer<uint> pResumeHandle;
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			pEnumContainer = new RpcPointer<AT_ENUM_CONTAINER>();
			pEnumContainer.value = decoder.ReadFixedStruct<AT_ENUM_CONTAINER>(NdrAlignment.NativePtr);
			decoder.ReadStructDeferral<AT_ENUM_CONTAINER>(ref pEnumContainer.value);
			PreferedMaximumLength = decoder.ReadUInt32();
			pResumeHandle = decoder.ReadUniquePointer<uint>();
			if (pResumeHandle is not null)
			{
				pResumeHandle.value = decoder.ReadUInt32();
			}

			var invokeTask = this._obj.NetrJobEnum(ServerName, pEnumContainer, PreferedMaximumLength, pTotalEntries, pResumeHandle, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteFixedStruct(pEnumContainer.value, NdrAlignment.NativePtr);
			encoder.WriteStructDeferral(pEnumContainer.value);
			encoder.WriteValue(pTotalEntries.value);
			encoder.WriteUniquePointer(pResumeHandle);
			if (pResumeHandle is not null)
			{
				encoder.WriteValue(pResumeHandle.value);
			}

			encoder.WriteValue(retval);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task Invoke_NetrJobGetInfo(IRpcDecoder decoder, IRpcEncoder encoder, CancellationToken cancellationToken)
		{
			string ServerName;
			uint JobId;
			RpcPointer<RpcPointer<AT_INFO>> ppAtInfo = new RpcPointer<RpcPointer<AT_INFO>>();
			if (decoder.ReadReferentId() == 0)
				ServerName = null;
			else
				ServerName = decoder.ReadWideCharString();
			JobId = decoder.ReadUInt32();
			var invokeTask = this._obj.NetrJobGetInfo(ServerName, JobId, ppAtInfo, cancellationToken);
			var retval = await invokeTask;
			encoder.WriteUniquePointer(ppAtInfo.value);
			if (ppAtInfo.value is not null)
			{
				encoder.WriteFixedStruct(ppAtInfo.value.value, NdrAlignment.NativePtr);
				encoder.WriteStructDeferral(ppAtInfo.value.value);
			}

			encoder.WriteValue(retval);
		}

		private static Guid _interfaceUuid = new Guid("1ff70682-0a51-30e8-076d-740be8cee98b");
		public override Guid InterfaceUuid => _interfaceUuid;
		public override Titanis.DceRpc.RpcVersion InterfaceVersion => new Titanis.DceRpc.RpcVersion(1, 0);
		private Titanis.DceRpc.Server.OperationImplFunc[] _dispatchTable;
		public override Titanis.DceRpc.Server.OperationImplFunc[] DispatchTable => this._dispatchTable;
		private atsvc _obj;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public atsvcStub(atsvc obj)
		{
			this._obj = obj;
			this._dispatchTable = new Titanis.DceRpc.Server.OperationImplFunc[]{this.Invoke_NetrJobAdd, this.Invoke_NetrJobDel, this.Invoke_NetrJobEnum, this.Invoke_NetrJobGetInfo};
		}
	}
}