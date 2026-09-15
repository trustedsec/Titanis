using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MS_EVEN6
{
	public partial struct EvtRpcVariant
	{
		public EvtRpcVariant(bool value)
		{
			this.type = EvtRpcVariantType.EvtRpcVarTypeBoolean;
			this.unnamed_1.type = this.type;
			this.unnamed_1.booleanVal = value;
		}
		public EvtRpcVariant(uint value)
		{
			this.type = EvtRpcVariantType.EvtRpcVarTypeUInt32;
			this.unnamed_1.type = this.type;
			this.unnamed_1.uint32Val = value;
		}
		public EvtRpcVariant(ulong value)
		{
			this.type = EvtRpcVariantType.EvtRpcVarTypeUInt64;
			this.unnamed_1.type = this.type;
			this.unnamed_1.uint64Val = value;
		}
		public EvtRpcVariant(Guid value)
		{
			this.type = EvtRpcVariantType.EvtRpcVarTypeGuid;
			this.unnamed_1.type = this.type;
			this.unnamed_1.guidVal = new Titanis.DceRpc.RpcPointer<Guid>(value);
		}
		public EvtRpcVariant(string value)
		{
			this.type = EvtRpcVariantType.EvtRpcVarTypeString;
			this.unnamed_1.type = this.type;
			this.unnamed_1.stringVal = new Titanis.DceRpc.RpcPointer<string>(value);
		}

		public object? GetValue() => this.unnamed_1.type switch
		{
			EvtRpcVariantType.EvtRpcVarTypeBoolean => this.unnamed_1.booleanVal,
			EvtRpcVariantType.EvtRpcVarTypeUInt32 => this.unnamed_1.uint32Val,
			EvtRpcVariantType.EvtRpcVarTypeUInt64 => this.unnamed_1.uint64Val,
			EvtRpcVariantType.EvtRpcVarTypeGuid => this.unnamed_1.guidVal?.value,
			EvtRpcVariantType.EvtRpcVarTypeString => this.unnamed_1.stringVal?.value,
			_ => null
		};
	}
}