using System;
using Titanis.DceRpc;
using Titanis.IO;

namespace Titanis.Msrpc.Msdcom
{
	// [MS-DCOM] § 2.2.20 - Context
	sealed class ClientContext : Objref_Custom, IRpcObject, ICustomDcomMarshal
	{
		public sealed override Guid MarshalClsid => DcomIds.CLSID_ContextMarshaler;

		public sealed override Guid Iid => DcomIds.IID_IContext;

		public Objref CreateObjref()
		{
			return this;
		}

		public sealed override object GetObject()
		{
			return this;
		}

		protected sealed override void WriteObjectData(ByteWriter writer)
		{
			writer.WritePduStruct(new MarshaledContext
			{
				MajorVersion = 1,
				MinorVersion = 1,
				ContextId = Guid.NewGuid(),
				Flags = MarshaledContextFlags.ByValue,
				numExtents = 0,
				cbExtents = 0,
				MarshalFlags = 0,
				count = 0,
				IsFrozen = 1,
			});
		}
	}

	class ClientContextUnmarshaler : IUnmarshaler
	{
		public Objref Unmarshal(ByteMemoryReader reader)
		{
			var ctx = reader.ReadPduStruct<MarshaledContext>();
			return new ClientContext();
		}
	}
}
