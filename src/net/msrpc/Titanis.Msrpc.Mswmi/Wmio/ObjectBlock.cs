using System.Diagnostics;
using System.IO;
using Titanis.IO;
using Titanis.PduStruct;


namespace Titanis.Msrpc.Mswmi.Wmio
{
	[PduStruct]
	partial struct ObjectBlock
	{
		public ObjectBlock(WmiObject? obj)
		{
			this.obj = obj;
		}

		partial void OnBeforeReadPdu(IByteSource source)
		{
		}

		// [MS-WMIO] § 2.2.5 - ObjectFlags
		internal WmiObjectFlags flags;

		private bool HasDecoration => 0 != (flags & WmiObjectFlags.HasDecoration);

		// [MS-WMIO] § 2.2.7 - Decoration
		[PduConditional(nameof(HasDecoration))]
		internal Decoration? decoration;

		// [MS-WMIO] § 2.2.10 - Encoding
		[PduField(ReadMethod = nameof(ReadEncoding), WriteMethod = nameof(WriteEncoding))]
		internal WmiObject? obj;
		private WmiObject? ReadEncoding(IByteSource source, PduByteOrder byteOrder)
		{
			var reader = (ByteMemoryReader)source;

			var deco = this.decoration?.ToDecoration();
			if (0 != (flags & WmiObjectFlags.CimInstance))
			{
				// [MS-WMIO] § 2.2.53 - InstanceType
				var instanceType = reader.ReadPduStruct<InstanceType>();
				return instanceType.CreateInstance(deco);
			}
			else if (0 != (flags & WmiObjectFlags.CimClass))
			{
				// [MS-WMIO] § 2.2.11 - ClassType
				var classType = reader.ReadClassType(deco);
				return classType;
			}
			else
			{
				throw new InvalidDataException("The data did not indicate whether the object is a class or instance.");
			}
		}
		private void WriteEncoding(ByteWriter writer, WmiObject? obj, PduByteOrder byteOrder)
		{
			obj.EncodeObjectBlockTo(writer);
		}

		partial void OnBeforeWritePdu(ByteWriter writer)
		{
			this.flags = this.obj.ObjectFlags;
			// TODO: Take decoration from object?
			if (this.decoration.HasValue)
				this.flags |= WmiObjectFlags.HasDecoration;
			else
				this.flags &= ~WmiObjectFlags.HasDecoration;
		}
		partial void OnAfterReadPdu(IByteSource source)
		{
			Debug.Assert(((int)flags & 0x03) != 0);
		}
	}
}
