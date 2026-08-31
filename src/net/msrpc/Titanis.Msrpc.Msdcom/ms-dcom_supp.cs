using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Titanis.DceRpc;

namespace ms_dcom
{
	partial class IRemUnknownClientProxy
	{
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public async Task<int> RemQueryInterface(Guid ripid, uint cRefs, ushort cIids, Guid[] iids, RpcPointer<RpcPointer<REMQIRESULT[]>> ppQIResults, CancellationToken cancellationToken)
		{
			Titanis.DceRpc.Client.IRpcRequestBuilder req = this.CreateRequest2(3);
			IRpcEncoder encoder = req.StubData;
			encoder.WriteValue(ripid);
			encoder.WriteValue(cRefs);
			encoder.WriteValue(cIids);
			if (iids is not null)
			{
				encoder.WriteArrayHeader(iids);
				for (int i = 0; i < iids.Length; i++)
				{
					Guid elem_0 = iids[i];
					encoder.WriteValue(elem_0);
				}
			}

			IRpcDecoder decoder = await this.SendRequestAsync2(req, cancellationToken);
			ppQIResults.value = decoder.ReadOutUniquePointer<REMQIRESULT[]>(ppQIResults.value);
			if (ppQIResults.value is not null)
			{
				ppQIResults.value.value = decoder.ReadArrayHeader<REMQIRESULT>();
				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					elem_0 = decoder.ReadFixedStruct<REMQIRESULT>(NdrAlignment._8Byte);
					ppQIResults.value.value[i] = elem_0;
				}

				for (int i = 0; i < ppQIResults.value.value.Length; i++)
				{
					REMQIRESULT elem_0 = ppQIResults.value.value[i];
					decoder.ReadStructDeferral<REMQIRESULT>(ref elem_0);
					ppQIResults.value.value[i] = elem_0;
				}
			}

			int retval;
			retval = decoder.ReadInt32();
			return retval;
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct ORPCTHIS2 : IRpcFixedStruct
	{
		public COMVERSION version;
		public uint flags;
		public uint reserved1;
		public Guid cid;
		public uint ext;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteFixedStruct(this.version, NdrAlignment._2Byte);
			encoder.WriteValue(this.flags);
			encoder.WriteValue(this.reserved1);
			encoder.WriteValue(this.cid);
			encoder.WriteValue(this.ext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.version = decoder.ReadFixedStruct<COMVERSION>(NdrAlignment._2Byte);
			this.flags = decoder.ReadUInt32();
			this.reserved1 = decoder.ReadUInt32();
			this.cid = decoder.ReadUuid();
			this.ext = decoder.ReadUInt32();
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void EncodeDeferrals(IRpcEncoder encoder)
		{
			encoder.WriteStructDeferral(this.version);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void DecodeDeferrals(IRpcDecoder decoder)
		{
			decoder.ReadStructDeferral<COMVERSION>(ref this.version);
		}
	}

	[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
	public partial struct ORPCTHAT2 : IRpcFixedStruct
	{
		public uint flags;
		public uint ext;
		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Encode(IRpcEncoder encoder)
		{
			encoder.WriteValue(this.flags);
			encoder.WriteValue(this.ext);
		}

		[GeneratedCodeAttribute("Animus IDL Compiler", "0.9.10")]
		public void Decode(IRpcDecoder decoder)
		{
			this.flags = decoder.ReadUInt32();
			this.ext = decoder.ReadUInt32();
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
}
