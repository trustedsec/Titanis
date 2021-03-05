using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanis.IO;

namespace Titanis.Msrpc.Mswmi.Wmio
{
	// [MS-WMIO] § 2.2.60 - Qualifier
	[PduStruct]
	internal partial struct Qualifier
	{
		public HeapStringRef name;
		// [MS-WMIO] § 2.2.62 - QualifierFlavor
		public WmiQualifierFlavor flavor;
		// [MS-WMIO] § 2.2.63 - QualifierType
		// [MS-WMIO] § 2.2.82 - CimType
		public CimType qualifierType;

		// [MS-WMIO] § 2.2.64 - QualifierValue
		// [MS-WMIO] § 2.2.71 - EncodedValue
		[PduField(ReadMethod = nameof(ReadValue), WriteMethod = nameof(WriteValue))]
		public EncodedValue value;
		private EncodedValue ReadValue(IByteSource source, PduByteOrder byteOrder)
			=> source.ReadEncodedValue(this.qualifierType);
		private void WriteValue(ByteWriter writer, EncodedValue value, PduByteOrder byteOrder)
		{
			writer.WriteEncodedValue(value);
		}

		internal WmiQualifier Resolve(Heap heap)
		{
			var name = heap.ResolveString(this.name);
			var value = this.value.Resolve(heap, null, $"Qualifier: {name}");
			return new WmiQualifier(
				name,
				this.flavor,
				this.qualifierType,
				value
				);
		}
	}
}
